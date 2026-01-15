using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Layered_TimeTable
{
    /// <summary>
    /// Summary description for APIConfig
    /// </summary>
    public class ApiConfigAdmission
    {
        public static ApiConfigAdmission ApiConfigInstance = new ApiConfigAdmission();
        public static string Surl, Furl, ServiceProvider, Productinfo, Key, Salt, PayuBaseUrl, HashSeq;

        // ReSharper disable once IdentifierTypo
        public static bool Isactive = false;
        SqlConnection con = new SqlConnection();
        Campus oo = new Campus();
        string sql = string.Empty;
        public ApiConfigAdmission()
        {
            
        }

        public void setvalue(string GateWayName, string GateWayFor, int BranchCode)
        {
            sql = "select * from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='" + GateWayFor + "' and GateWayName='" + GateWayName + "' and Isactive=1";
            if (oo.Duplicate(sql))
            {
                Surl = oo.ReturnTag(sql, "SuccessURL");
                Furl = oo.ReturnTag(sql, "FailurURL");
                ServiceProvider = oo.ReturnTag(sql, "ServiceProvider");
                Productinfo = oo.ReturnTag(sql, "ProductInfo");
                Key = oo.ReturnTag(sql, "MarchentKEY");
                Salt = oo.ReturnTag(sql, "Marchentsalt");
                PayuBaseUrl = oo.ReturnTag(sql, "BaseURL");
                HashSeq = oo.ReturnTag(sql, "HashSequence");
                Isactive = oo.ReturnTag(sql, "isactive") == "1" ? true : false;
            }
            else
            {
                Surl = "";
                Furl = "";
                ServiceProvider = "";
                Productinfo = "";
                Key = "";
                Salt = "";
                PayuBaseUrl = "";
                HashSeq = "";
            }
        }

        public DataSet GetPaymentGateway()
        {
            var param = new List<SqlParameter>
            {
                (new SqlParameter("@QueryFor","S")),
                (new SqlParameter("@ID", 2))
            };

            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;

            param.Add(para);

            var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PayMentGatewaySetting", param);

            return ds;
        }
        public void SetTransaction(string txnId, string hashStr ,string srNo, string session ,decimal amount, string GatewayName, string PaymentFor, int BranchCode, Page pg)
        {
            try
            {
                setvalue(GatewayName, PaymentFor, BranchCode);
                //amount = Convert.ToDecimal(.1);
                string[] hashVarsSeq;
                string txnid = "", hash_string = "", hash = "", action = "", firstname = "", email = "", phone = "";

                hash_string = hashStr;
                txnid = txnId;

                DataTable dt = new DataTable();
                dt = DAL.DALInstance.GetValueInTable("select id,StudentName +''+ MiddleName +' '+ LastName Name,Mobile,email  from AdmissionFormOnline where Mobile='" + srNo+"' AND SessionName='"+session+ "' and AdmissionType='new' and txnid='"+ txnid + "' order by ID desc");

                if (dt != null && dt.Rows.Count > 0)
                {
                    firstname = dt.Rows[0][1].ToString().Trim() != "" ? dt.Rows[0][1].ToString().Trim() : "abc";
                    email = dt.Rows[0][3].ToString().Trim() != "" ? dt.Rows[0][3].ToString().Trim() : "abc@xyz.com";
                    phone = dt.Rows[0][2].ToString().Trim() != "" ? dt.Rows[0][2].ToString().Trim() : "9876543210";
                }
                if (
                    !string.IsNullOrEmpty(Key) &&
                    !string.IsNullOrEmpty(txnid) &&
                    amount>0 &&
                    !string.IsNullOrEmpty(firstname) &&
                    !string.IsNullOrEmpty(email) &&
                    !string.IsNullOrEmpty(phone) &&
                    !string.IsNullOrEmpty(Productinfo) &&
                    !string.IsNullOrEmpty(Surl) &&
                    !string.IsNullOrEmpty(Furl) &&
                    !string.IsNullOrEmpty(ServiceProvider)
                )        
                {
                    hashVarsSeq = HashSeq.Split('|'); 
                    hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + Key;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + txnid;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            hash_string = hash_string + Convert.ToDecimal(amount).ToString("g29");
                            hash_string = hash_string + '|';
                        } 
                        else if (hash_var == "productinfo")
                        {
                            hash_string = hash_string + Productinfo;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "firstname")
                        {
                            hash_string = hash_string + firstname;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "email")
                        {
                            hash_string = hash_string + email;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "phone")
                        {
                            hash_string = hash_string + phone;
                            hash_string = hash_string + '|';
                        }
                        else
                        {
                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';
                        }
                    }

                    hash_string += Salt;

                    hash = GenerateHash512(hash_string).ToLower();
                    action = PayuBaseUrl + "/_payment";
                }

                if (!string.IsNullOrEmpty(hash))
                {
                    System.Collections.Hashtable data = new System.Collections.Hashtable(); 
                    data.Add("hash", hash);
                    data.Add("txnid", txnid);
                    data.Add("key", Key);
                    data.Add("amount", Convert.ToDecimal(amount).ToString("g29"));
                    data.Add("firstname", firstname);
                    data.Add("email", email);
                    data.Add("phone", phone);
                    data.Add("productinfo", Productinfo);
                    data.Add("surl", Surl);
                    data.Add("furl", Furl);
                    data.Add("lastname", "");
                    data.Add("curl", "");
                    data.Add("address1", "");
                    data.Add("address2", "");
                    data.Add("city", "");
                    data.Add("state", "");
                    data.Add("country", "");
                    data.Add("zipcode","");
                    data.Add("udf1", "");
                    data.Add("udf2", "");
                    data.Add("udf3", "");
                    data.Add("udf4", "");
                    data.Add("udf5", "");
                    data.Add("pg", "");
                    data.Add("service_provider", ServiceProvider);

                    InsertApiTransaction(srNo,txnid,amount);

                    if(Isactive)
                    {
                        string strForm = PreparePOSTForm(action, data);
                        pg.Controls.Add(new LiteralControl(strForm));
                    }
                }
                else
                {
                    // ignored
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public string GenerateHash512(string txt)
        {
            string hex = "";
            try
            {
                byte[] MSG = Encoding.UTF8.GetBytes(txt);

                UnicodeEncoding ue = new UnicodeEncoding();
                byte[] HashValue;
                SHA512Managed hashString = new SHA512Managed();

                HashValue = hashString.ComputeHash(MSG);

                foreach (byte x in HashValue)
                {
                    // ReSharper disable once UseStringInterpolation
                    hex += String.Format("{0:x2}", x);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return hex;
        }

        public void InsertApiTransaction(string srNo, string txnid,decimal amount)
        {
            try
            {
                BAL.ClsApiTransactionAdmission obj = new BAL.ClsApiTransactionAdmission();
                obj.TxnId = txnid;
                obj.SrNo = srNo;
                obj.Amount = amount;
                obj.Charges = 0;
                obj.Status = 2;
                obj.Sql = "I";

                DAL.DALInstance.SetApiTransactionAdmission(obj);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)     
        {
            //Set a name for the form
            string formID = "PostForm";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\">");
            }

            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm + strScript.ToString();
        }
    }
}



