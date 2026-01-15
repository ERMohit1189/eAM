using System;
using System.Data;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for APIConfig
/// </summary>
public class APIConfig
{
    public static APIConfig APIConfigInstance = new APIConfig();
    public static string surl, furl, service_provider, productinfo, key, salt, PayuBaseURL, HashSeq;
    public static bool isactive = false;
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    public Int32 _BranchCode;
    public string _SessionName;
    public string _LoginName;
    public string _LoggedEmpID;
    public APIConfig()
    {
        if (HttpContext.Current.Session["SessionName"] != null)
        {
            _BranchCode = Convert.ToInt32(HttpContext.Current.Session["BranchCode"].ToString());
            _SessionName = HttpContext.Current.Session["SessionName"].ToString();
            _LoginName = HttpContext.Current.Session["LoginName"].ToString();

            
        }
        else
        {
            HttpContext.Current.Response.Redirect("../default.aspx");
        }
    }
    public void setvalue(string GateWayName, string GateWayFor, int BranchCode)
    {
        sql = "select * from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='"+ GateWayFor + "' and GateWayName='"+ GateWayName + "' and Isactive=1";
        if (oo.Duplicate(sql))
        {
            surl = oo.ReturnTag(sql, "SuccessURL");
            furl = oo.ReturnTag(sql, "FailurURL");
            service_provider = oo.ReturnTag(sql, "ServiceProvider");
            productinfo = oo.ReturnTag(sql, "ProductInfo");
            key = oo.ReturnTag(sql, "MarchentKEY");
            salt = oo.ReturnTag(sql, "Marchentsalt");
            PayuBaseURL = oo.ReturnTag(sql, "BaseURL");
            HashSeq = oo.ReturnTag(sql, "HashSequence");
            isactive = true;
        }
        else
        {
            surl = "";
            furl = "";
            service_provider = "";
            productinfo = "";
            key = "";
            salt = "";
            PayuBaseURL = "";
            HashSeq = "";
        }
    }

    public DataSet GetPaymentGateway()
    {
        var param = new List<SqlParameter>
        {
            (new SqlParameter("@QueryFor","S")),
            (new SqlParameter("@ID", 1))
        };

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = ParameterDirection.Output;

        param.Add(para);

        DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_PayMentGatewaySetting", param);

        return ds;
    }

    //public static readonly string key = "rjQUPktU", salt = "e5iIg1jwi8", PayuBaseURL = "https://test.payu.in", HashSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";

    public void SetTransaction(string TxnID, string HashStr ,string SrNo, string Session ,decimal amount, string GatewayName, string PaymentFor, int BranchCode, Page pg)
    {
        try
        {
            
            setvalue(GatewayName, PaymentFor, BranchCode);
            //amount = Convert.ToDecimal(.1);
            string[] hashVarsSeq;
            string txnid = "", hash_string = "", hash = "", action = "", firstname = "", email = "", phone = "";

            hash_string = HashStr;
            txnid = TxnID;

            DataTable dt = new DataTable();
            dt = DAL.DALInstance.GetValueInTable("SELECT DISTINCT S.SrNo,S.FirstName,F.FamilyContactNo,F.FamilyEmail FROM StudentFamilyDetails F JOIN StudentGenaralDetail S ON F.SrNo=S.SrNo and F.BranchCode=S.BranchCode and F.SessionName=S.SessionName WHERE  Case When Left('" + SrNo + "',3)='eAM' Then S.StEnRCode Else S.SrNo End='" + SrNo + "' AND S.SessionName='" + Session + "' AND F.SessionName='" + Session + "' and F.BranchCode="+ _BranchCode + "");

            if (dt != null && dt.Rows.Count > 0)
            {
                firstname = dt.Rows[0][1].ToString() != "" ? dt.Rows[0][1].ToString() : "abc";
                email = dt.Rows[0][3].ToString() != "" ? dt.Rows[0][3].ToString() : "abc@xyz.com";
                phone = dt.Rows[0][2].ToString() != "" ? dt.Rows[0][2].ToString() : "9876543210";
            } 
        
            if (
                !string.IsNullOrEmpty(key) &&
                !string.IsNullOrEmpty(txnid) &&
                amount>0 &&
                !string.IsNullOrEmpty(firstname) &&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(phone) &&
                !string.IsNullOrEmpty(productinfo) &&
                !string.IsNullOrEmpty(surl) &&
                !string.IsNullOrEmpty(furl) &&
                !string.IsNullOrEmpty(service_provider)
                )        
               {
                   hashVarsSeq = HashSeq.Split('|'); 
                   hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + key;
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
                            hash_string = hash_string + productinfo;
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

                    hash_string += salt;

                    hash = GenerateHash512(hash_string).ToLower();
                    action = PayuBaseURL + "/_payment";
                }

            //if (isFeeType.ToString() != "" && isFeeType.ToString().ToLower() == "hostelfee")
            //{
            //    surl = "";
            //    furl = "";
            //}

            if (!string.IsNullOrEmpty(hash))
            {
                System.Collections.Hashtable data = new System.Collections.Hashtable(); 
                data.Add("hash", hash);
                data.Add("txnid", txnid);
                data.Add("key", key);
                data.Add("amount", amount.ToString("g29"));
                data.Add("firstname", firstname);
                data.Add("email", email);
                data.Add("phone", phone);
                data.Add("productinfo", productinfo);
                data.Add("surl", surl);
                data.Add("furl", furl);
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
                data.Add("service_provider", service_provider);

                if(isactive)
                {
                    string strForm = PreparePOSTForm(action, data);
                    pg.Controls.Add(new LiteralControl(strForm));
                }

            }
            else
            {
                //no hash       
            }
        }
        catch (Exception)
        {
            
        }
    }

    public void SetICICITransaction(string TxnID, string SubMerchantIdp, string SrNo, string Session, decimal amount, int BranchCode, string PaymentFor, Page pg)
    {
        try
        {

            string sql = "select top(1) ReturnUrl, ReferenceNo, SubMerchantId, MerchantID, AESKey, Isactive from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='"+ PaymentFor + "' and Isactive=1 and GateWayName='EassyPay'";
            string MerchantID = oo.ReturnTag(sql, "MerchantID");
            string AESKey = oo.ReturnTag(sql, "AESKey");
            string Isactive = oo.ReturnTag(sql, "Isactive");
            string rUrl = oo.ReturnTag(sql, "ReturnUrl");
            if (Isactive == "1" || Isactive.ToLower() == "true")
            {
                //InsertAPITransaction(SrNo, TxnID, amount);
                
                string mandatoryfields = encryptFile((TxnID + "|" + SubMerchantIdp + "|" + amount.ToString()), AESKey);
                string optionalfields = encryptFile("20|20|20|20", AESKey);
                string returnurl = encryptFile(rUrl, AESKey);
                string ReferenceNo = encryptFile(TxnID, AESKey);
                string submerchantid = encryptFile(SubMerchantIdp, AESKey);
                string transactionamount = encryptFile(amount.ToString("0.0"), AESKey);
                string paymode = encryptFile("9", AESKey);
                string action = "https://eazypay.icicibank.com/EazyPG?merchantid=" + MerchantID + "&mandatory fields=" + mandatoryfields + "&optional fields=&returnurl= " + returnurl + "&Reference No=" + ReferenceNo + "&submerchantid=" + submerchantid + "&transaction amount=" + transactionamount + "&paymode=" + paymode + "";
                //string strForm = PrepareICICIPOSTForm(action);
                //pg.Controls.Add(new LiteralControl(strForm));
            }
            else
            {
                throw new Exception("Please Update EasyPay Gateway Setting!");
            }
        }
        catch (Exception)
        {

        }
    }
    private string PrepareICICIPOSTForm(string url)
    {
        string formID = "PostForm";

        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\"></form>");

        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." + formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");

        return strForm.ToString() + strScript.ToString();
    }
    public static string encryptFile(string textToEncrypt, string key)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.ECB;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[0x10];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
        {
            len = keyBytes.Length;
        }
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;


        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0,
        plainText.Length));
    }

    public string GenerateHash512(string txt)
    {
        string hex = "";
        try
        {
            byte[] MSG = Encoding.UTF8.GetBytes(txt);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue;
            SHA512Managed HashString = new SHA512Managed();
            HashValue = HashString.ComputeHash(MSG);
            foreach (byte x in HashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
        }
        catch(Exception)
        { 
        }
        return hex;
    }

    public void InsertAPITransaction(string SrNo, string txnid,decimal Amount)
    {
        try
        {
            BAL.clsAPITransaction obj = new BAL.clsAPITransaction();
            obj.TxnID = txnid;
            obj.SrNo = SrNo;
            obj.Amount = Amount;
            obj.Charges = 0;
            obj.Status = 2;
            obj.SQL = "I";

            DAL.DALInstance.SetAPITransaction(obj);
        }
        catch(Exception){}
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
            strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\" >");
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
        return strForm.ToString() + strScript.ToString();
    }
}



