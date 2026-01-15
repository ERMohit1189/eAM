using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;
using Layered_TimeTable;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

public partial class Gpaf : Page
{
    public string surl, furl, service_provider, productinfo, key, salt, PayuBaseURL, HashSeq;
    public static bool isactive = false;
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql = string.Empty;
    
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["check"]))
        {
            if (Session["Logintype"].ToString()== "Guardian")
            {
                MasterPageFile = "~/sp/sp_root-manager.master";
            }
        }
        else
        {
            MasterPageFile = "~/ap/admin_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        TextBox1.Focus();
        _con = _oo.dbGet_connection();
        if (string.IsNullOrEmpty(Request.QueryString["check"]))
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            divGateway.Visible = false;
        }
        else
        {
            divsearch1.Visible = false;
            txtAdmissiondate.Enabled = false;
            divadmissiontype.Visible = false;
            divpaymentmode.Visible = false;
            txtContactNo.Text = Request.QueryString["id"];
            txtContactNo.Enabled = false;
            divdatabind.Visible = false;
        }


        if (!IsPostBack)
        {
            DataTable dtp = new DataTable();
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = "FeePermissionSettingProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtp);
                    if (dtp.Rows.Count > 0)
                    {
                        //txtConcession.Enabled = (dtp.Rows[0]["AdmissionDiscount"].ToString() == "0" ? false : true);
                        //txtReceivedAmount.Enabled = (dtp.Rows[0]["AdmissionPaid"].ToString() == "0" ? false : true);
                    }
                }
                catch (Exception ex)
                {
                }
            }


            _oo.AddDateMonthYearDropDown(DDChkYear, DDChkMonth, DDChkDate);
            _oo.FindCurrentDateandSetinDropDown(DDChkYear, DDChkMonth, DDChkDate);
            getGatway();
            txtAdmissiondate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            _sql = "select SessionName from SessionMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDownWithOutSelect(_sql, drpSession, "SessionName");
            _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and DATEDIFF(day, FromDate, GETDATE())>200 order by SessionId desc";
            if (_oo.Duplicate(_sql))
            {
                drpSession.SelectedIndex = (drpSession.Items.Count - 1);
            }
            else
            {

                if (drpSession.Items.Count > 2)
                {
                    _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and SessionId <>";
                    _sql = _sql + "(select top(1) SessionId from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionId desc) order by SessionId desc";
                    drpSession.SelectedValue = _oo.ReturnTag(_sql, "SessionName");
                }
                else
                {
                    drpSession.SelectedIndex = (drpSession.Items.Count - 1);
                }
            }
            drpSession.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
            drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
            drpBranchss.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select -->"));
            _sql = "select ClassName,Id from ClassMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CIDORDER";
            _oo.FillDropDown_withValue(_sql, drpClassss, "ClassName", "Id");
            drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
            _sql = "select medium from MediumMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown_withValue(_sql, ddlMedium, "medium", "medium");
            ddlMedium.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
            drpBranchss.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));

        }
       
    }

    public void getGatway()
    {
        if (DropDownMOD.SelectedIndex == 0)
        {
            Submit.Visible = false;
            _sql = "select case when GateWayName='PayUMoney' then 'PayU' else GateWayName end GateWayName, paymentCharges, Logo from TblPaymentGateway where BranchCode=" + Session["BranchCode"].ToString() + " and GateWayFor='Admission' and Isactive=1 order by id asc";
            _oo.FillDropDown_withValue(_sql, ddlPaymentGateway, "GateWayName", "GateWayName");
            ddlPaymentGateway.Items.Insert(0, new ListItem("<--Select-->", ""));
            string chks = "select count(*)cnt from TblPaymentGateway where BranchCode=" + Session["BranchCode"].ToString() + " and GateWayFor='Admission' and Isactive=1 order by id asc";
            if (_oo.ReturnTag(chks, "cnt") != "0")
            {
                var dt = _oo.Fetchdata(_sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Session[dt.Rows[i]["GateWayName"].ToString()] = dt.Rows[i]["Logo"].ToString() + "##" + dt.Rows[i]["paymentCharges"].ToString();
                }
                Submit.Visible = true;
            }
            _sql = "select LivePublicKey, Email, PseudoUniqueReference from TblPaymentGateway where BranchCode=" + Session["BranchCode"].ToString() + " and GateWayFor='Admission' and GateWayName='PayStack' and Isactive=1";
            if (_oo.Duplicate(_sql))
            {
                hdnTxtNos.Value = "";
                data_key.Text = _oo.ReturnTag(_sql, "LivePublicKey");
                data_email.Text = _oo.ReturnTag(_sql, "Email");
                data_PseudoUniqueReference.Text = _oo.ReturnTag(_sql, "PseudoUniqueReference");
            }
        }
    }
    private void GetSessionClass()
    {
        try
        {
            
        }
        catch (Exception)
        {
            // ignored
        }
    }
    protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select ClassName,Id from ClassMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CIDORDER";
        _oo.FillDropDown_withValue(_sql, drpClassss, "ClassName", "Id");
        drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        _sql = "select medium from MediumMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(_sql, ddlMedium, "medium", "medium");
        ddlMedium.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
    }
    public void LoadSession()
    {
        _sql = "select SessionName from SessionMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDownWithOutSelect(_sql, drpSession, "SessionName");
        _oo.FillDropDownWithOutSelect(_sql, drpSessionPanel, "SessionName");
        _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and DATEDIFF(day, FromDate, GETDATE())>200 order by SessionId desc";
        if (_oo.Duplicate(_sql))
        {
            drpSession.SelectedIndex = (drpSession.Items.Count - 1);
        }
        else
        {
            _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and SessionId <>";
            _sql = _sql + "(select top(1) SessionId from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionId desc) order by SessionId desc";
            drpSession.SelectedValue = _oo.ReturnTag(_sql, "SessionName");
        }
        drpSession.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        drpSession.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        drpSessionPanel.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        drpSession.SelectedIndex = (drpSession.Items.Count - 1);
        _sql = "select ClassName,Id from ClassMaster where sessionName='" + drpSession.SelectedValue + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CIDORDER";
        _oo.FillDropDown_withValue(_sql, drpClassss, "ClassName", "Id");
        drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        _sql = "select medium from MediumMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(_sql, ddlMedium, "medium", "medium");
        ddlMedium.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
    }

    public void Permission_Values()
    {
        string sql8;
        sql8 = "select Enable from Admin_fee_permission_setting where Tableid='5' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (_oo.ReturnTag(sql8, "Enable") == "" || _oo.ReturnTag(sql8, "Enable") == "No,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1" || _oo.ReturnTag(sql8, "Enable") == "Yes,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,2" || _oo.ReturnTag(sql8, "Enable") == "Yes,1,3" || _oo.ReturnTag(sql8, "Enable") == "Yes,2,3")
        {
            string[] permission2 = _oo.ReturnTag(sql8, "Enable").Split(',');
            if (permission2.Length > 1)
            {
                txtAdmissiondate.Enabled = false;
                txtConcession.Enabled = false;
                txtAmt.Enabled = false;
                if (permission2[0] == "Yes")
                {
                    for (int i = 1; i <= permission2.Length - 1; i++)
                    {
                        if (permission2[i] == "1")
                        {
                            txtAdmissiondate.Enabled = true;
                        }
                        if (permission2[i] == "2")
                        {
                            txtConcession.Enabled = true;
                        }
                        if (permission2[i] == "3")
                        {
                            txtAmt.Enabled = true;
                        }
                    }
                }
            }
        }
    }
    public string IdGeneration(string fixedString, string x)
    {
        string xx;
        switch (x.Length)
        {
            case 1:
                xx = fixedString + "000000" + x;
                break;
            case 2:
                xx = fixedString + "00000" + x;
                break;
            case 3:
                xx = fixedString + "0000" + x;
                break;
            case 4:
                xx = fixedString + "000" + x;
                break;
            case 5:
                xx = fixedString + "00" + x;
                break;
            case 6:
                xx = fixedString + "0" + x;
                break;
            default:
                xx = fixedString + x;
                break;
        }
        return "PAF/" + Session["SessionName"] + "/" + xx;
    }

    private void SetOnlinePayUmoney()
    {
        
            Session["SessionName"] = drpSession.Text.Trim();
            Session["LoginName"] = txtContactNo.Text.Trim();
            string dd;
            if (drpClassss.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBox("Please Select Condition!", Page);
            }
            else
            {
                var rnd = new Random();
                var hashStr = ApiConfigAdmission.ApiConfigInstance.GenerateHash512(rnd + DAL.DALInstance.GetDateTime());
                var txnId = "";
                try
                {
                    txnId = hashStr.Substring(0, 20);
                }
                catch (Exception)
                {
                    // ignored
                }

                dd = txtAdmissiondate.Text;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "USP_AdmissionFormOnline";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QueryFor", "I");
                    cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
                    cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtStudentName0.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtStudentName1.Text);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                    cmd.Parameters.AddWithValue("@Class", drpClassss.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LoginName", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@sex", drpSex.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Mobile", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@amount", txtReceivedAmount.Text);
                    cmd.Parameters.AddWithValue("@sessionName", drpSession.Text);
                    cmd.Parameters.AddWithValue("@SessionFor", drpSession.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@Branch", drpBranchss.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionType.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceivedAmount.Text.Trim());
                    cmd.Parameters.AddWithValue("@TxnID", txnId);
                    cmd.Parameters.AddWithValue("@GateWayName", "PayUMoney");
                    cmd.Connection = _con;
                    try
                    {
                        _con.Open();
                        var o = cmd.ExecuteNonQuery();
                        if (o == 1)
                        {
                            _con.Close();
                            //_oo.MessageBox("Submited successfully", Page);
                            try
                            {
                                if (DropDownMOD.SelectedIndex == 0)
                                {
                                    decimal balance;
                                    decimal.TryParse(txtReceivedAmount.Text.Trim(), out balance);
                                    USP_InsertAPITransactionAdmission(txnId, txtContactNo.Text, balance);
                                    //SetTransaction(txnId, hashStr, txtContactNo.Text, drpSession.Text, balance, ddlPaymentGateway.SelectedValue, "Admission", int.Parse(Session["BranchCode"].ToString()), Page);
                                SetTransaction(txnId, hashStr, txtContactNo.Text, drpSession.Text, balance, ddlPaymentGateway.SelectedValue, "Admission", int.Parse(Session["BranchCode"].ToString()), "Guardian", txtContactNo.Text, "../img/user-pic/user-pic.jpg", Page);

                            }
                        }
                            catch (Exception)
                            {
                                _con.Close();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _con.Close();
                    }
                }
            }
       
    }
    public void setvalue(string GateWayName, string GateWayFor, int BranchCode)
    {
        string sql = "select * from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='" + GateWayFor + "' and GateWayName='" + (GateWayName== "PayU"? "PayUMoney" : GateWayName) + "' and Isactive=1";
        if (_oo.Duplicate(sql))
        {
            surl = _oo.ReturnTag(sql, "SuccessURL");
            furl = _oo.ReturnTag(sql, "FailurURL");
            service_provider = _oo.ReturnTag(sql, "ServiceProvider");
            productinfo = _oo.ReturnTag(sql, "ProductInfo");
            key = _oo.ReturnTag(sql, "MarchentKEY");
            salt = _oo.ReturnTag(sql, "Marchentsalt");
            PayuBaseURL = _oo.ReturnTag(sql, "BaseURL");
            HashSeq = _oo.ReturnTag(sql, "HashSequence");
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
    protected void SetTransaction(string TxnID, string HashStr, string SrNo, string Session, decimal amount, string GatewayName, string PaymentFor, int BranchCode, string Logintype, string LoginName, string ImageUrl, Page Page)
    {
        try
        {
            setvalue(GatewayName, PaymentFor, BranchCode);
            string[] hashVarsSeq;
            string txnid = "", hash_string = "", hash = "", action = "", firstname = "", email = "", phone = "";

            hash_string = HashStr;
            txnid = TxnID;

            DataTable dt = new DataTable();
            dt = DAL.DALInstance.GetValueInTable("select id,StudentName +''+ MiddleName +' '+ LastName Name,Mobile,email  from AdmissionFormOnline where Mobile='" + SrNo + "' AND SessionName='" + Session + "' and BranchCode=" + BranchCode + " and AdmissionType='new' and txnid='" + txnid + "' order by ID desc");

            if (dt != null && dt.Rows.Count > 0)
            {
                firstname = dt.Rows[0][1].ToString().Trim() != "" ? dt.Rows[0][1].ToString().Trim() : "abc";
                email = dt.Rows[0][3].ToString().Trim() != "" ? dt.Rows[0][3].ToString().Trim() : "abc@xyz.com";
                phone = dt.Rows[0][2].ToString().Trim() != "" ? dt.Rows[0][2].ToString().Trim() : "9876543210";
            }
            if (
                !string.IsNullOrEmpty(key) &&
                !string.IsNullOrEmpty(txnid) &&
                amount > 0 &&
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

            if (!string.IsNullOrEmpty(hash))
            {
                System.Collections.Hashtable data = new System.Collections.Hashtable();
                data.Add("hash", hash);
                data.Add("txnid", txnid);
                data.Add("key", key);
                data.Add("amount", Convert.ToDecimal(amount).ToString("g29"));
                data.Add("firstname", firstname);
                data.Add("email", email);
                data.Add("phone", phone);
                data.Add("productinfo", productinfo);
                data.Add("surl", (surl+"?amt="+ amount.ToString()+ "&SessionName=" + Session + "&BranchCode=" + BranchCode + "&SrNo=" + SrNo + "&Logintype=" + Logintype + "&LoginName=" + LoginName+ "&ImageUrl="+ ImageUrl));
                data.Add("furl", (furl + "?SessionName=" + Session + "&BranchCode=" + BranchCode + "&SrNo=" + SrNo + "&Logintype=" + Logintype + "&LoginName=" + LoginName + "&ImageUrl=" + ImageUrl));
                data.Add("lastname", "");
                data.Add("curl", (furl + "?SessionName=" + Session + "&BranchCode=" + BranchCode + "&SrNo=" + SrNo + "&Logintype=" + Logintype+ "&LoginName="+ LoginName + "&ImageUrl=" + ImageUrl));
                data.Add("address1", "NA");
                data.Add("address2", "");
                data.Add("city", "");
                data.Add("state", "");
                data.Add("country", "");
                data.Add("zipcode", "");
                data.Add("udf1", "");
                data.Add("udf2", "");
                data.Add("udf3", "");
                data.Add("udf4", "");
                data.Add("udf5", "");
                data.Add("pg", "");
                data.Add("service_provider", service_provider);
                if (isactive)
                {
                    string strForm = PreparePOSTForm(action, data);
                    Page.Controls.Add(new LiteralControl(strForm));
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
    private string PreparePOSTForm(string url, Hashtable data)
    {
        string formID = "PostForm";
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
        foreach (DictionaryEntry key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\" >");
        }
        strForm.Append("</form>");
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." + formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        return strForm.ToString() + strScript.ToString();
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
        catch (Exception)
        {
        }
        return hex;
    }
    private void SetOnlineEasyPay()
    {
        
            var txnId = "";
            string sql = "select * from TblPaymentGateway where BranchCode=" + Session["BranchCode"] + " and GateWayFor='Fee' and Isactive=1 and GateWayName='EassyPay'";
            string ReferenceNop = _oo.ReturnTag(sql, "ReferenceNo");
            string SubMerchantIdp = _oo.ReturnTag(sql, "SubMerchantId");
            string Isactive = _oo.ReturnTag(sql, "Isactive");
            txnId = ReferenceNop;

            Session["SessionName"] = drpSession.Text.Trim();
            Session["LoginName"] = txtContactNo.Text.Trim();
            string dd;
            if (drpClassss.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBox("Please Select Condition!", Page);
            }
            else
            {
                dd = txtAdmissiondate.Text;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "USP_AdmissionFormOnline";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QueryFor", "I");
                    cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
                    cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txtStudentName0.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtStudentName1.Text);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                    cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Class", drpClassss.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LoginName", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@sex", drpSex.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Mobile", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@amount", txtAmt.Text);
                    cmd.Parameters.AddWithValue("@sessionName", drpSession.Text);
                    cmd.Parameters.AddWithValue("@SessionFor", drpSession.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@Branch", drpBranchss.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionType.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceivedAmount.Text.Trim());
                    cmd.Parameters.AddWithValue("@TxnID", txnId);
                    cmd.Parameters.AddWithValue("@GateWayName", "EasyPay");
                    cmd.Connection = _con;
                    try
                    {
                        _con.Open();
                        var o = cmd.ExecuteNonQuery();
                        if (o == 1)
                        {
                            _con.Close();
                            cmd.Parameters.Clear();
                            try
                            {
                                if (DropDownMOD.SelectedIndex == 0)
                                {
                                    decimal balance;
                                    decimal.TryParse(txtReceivedAmount.Text.Trim(), out balance);
                                    USP_InsertAPITransactionAdmission(txnId, txtContactNo.Text, balance);
                                    Session["isDuplicate"] = "No";
                                    using (SqlCommand cmd2 = new SqlCommand())
                                    {
                                        cmd2.CommandText = "UPDATE dbo.TblPaymentGateway SET ReferenceNo = ReferenceNo+1,  SubMerchantId=SubMerchantId+1 where BranchCode=" + Session["BranchCode"] + " and Isactive=1 and GateWayName='EassyPay'";
                                        cmd2.CommandType = CommandType.Text;
                                        cmd2.Connection = _con;
                                        try
                                        {
                                            _con.Open();
                                            cmd2.ExecuteNonQuery();
                                            _con.Close();
                                            if (Isactive == "1" || Isactive.ToLower() == "true")
                                            {
                                                Session["PaymentFor"] = "PaymentForAdmissionFee";
                                                Session["EasyPayTxnId"] = txnId;
                                                decimal balanceAmt;
                                                decimal.TryParse(txtReceivedAmount.Text.Trim(), out balanceAmt);
                                                SetICICITransaction(ReferenceNop, SubMerchantIdp, txtContactNo.Text, Session["SessionName"].ToString(), balanceAmt, int.Parse(Session["BranchCode"].ToString()), "Admission", Page);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            throw;
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                _con.Close();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _con.Close();
                    }
                }
            }
        
    }
    protected void SetICICITransaction(string TxnID, string SubMerchantIdp, string SrNo, string Session, decimal amount, int BranchCode, string PaymentFor, Page pg)
    {
        try
        {

            string sql = "select top(1) ReturnUrl, ReferenceNo, SubMerchantId, MerchantID, AESKey, Isactive from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='" + PaymentFor + "' and Isactive=1 and GateWayName='EassyPay'";
            string MerchantID = _oo.ReturnTag(sql, "MerchantID");
            string AESKey = _oo.ReturnTag(sql, "AESKey");
            string Isactive = _oo.ReturnTag(sql, "Isactive");
            string rUrl = _oo.ReturnTag(sql, "ReturnUrl");
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
                Response.Redirect(action);
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
    
    protected void USP_InsertAPITransactionAdmission(string txnId, string ContactNo, decimal amount)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "USP_InsertAPITransactionAdmission";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TxnID", txnId);
                cmd.Parameters.AddWithValue("@SrNo", ContactNo);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Connection = _con;
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
        }
        catch (Exception ex)
        {
            _con.Close();
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (ddlPaymentGateway.SelectedValue == "PayU")
        {
            SetOnlinePayUmoney();
        }
        if (ddlPaymentGateway.SelectedValue == "EassyPay")
        {
            SetOnlineEasyPay();
        }
    }

    public void SendFeesSms(string fmobileNo, string recieptNo, string amount)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(_sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();

                var mess = "INR " + amount + " received towards Admission Form. Receipt No. " + recieptNo + "";
                if (fmobileNo != "")
                {
                    _sql = "Select SmsSent From SmsEmailMaster where Id='11' ";
                    if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                    {
                        sadpNew.Send(mess, fmobileNo, "");
                    }
                }
            }
        }
    }

    public void SendFeescancleSms(string fmobileNo, string recieptNo, string amount)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(_sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();

                var mess = "Receipt No. " + recieptNo + " has been cancelled of Admission Form. Refunded Amount is INR " + amount + " ";

                if (fmobileNo != "")
                {
                    _sql = "Select SmsSent From SmsEmailMaster where Id='11' ";
                    if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                    {
                        sadpNew.Send(mess, fmobileNo, "");
                    }
                }
            }
        }
    }
    public void Display()
    {
        _sql = "select Row_Number() over (order by Id Asc) as SNo ,id, Status, AdmissionType,ReceivedAmount,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,StudentName+' '+MiddleName+' '+LastName as StudentName,FatherName,Class,ISNULL(Branch,'Other') Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount,status as Cancel, Template  from AdmissionFormCollection where BranchCode=" + Session["BranchCode"].ToString() + " and convert(date, AdmissionFromDate)=convert(date, getdate()) order by id Desc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();

        for (int i = 0; i < Grd.Rows.Count; i++)
        {

            Label label32 = (Label)Grd.Rows[i].FindControl("Label32");
            Label label34 = (Label)Grd.Rows[i].FindControl("Label34");
            Label label18 = (Label)Grd.Rows[i].FindControl("Label18");

            if (label32.Text == "")
            {
                _sql = "select top(1) StudentName as firstname from AdmissionFormCollection where RecieptNo='" + label18.Text + "'  and BranchCode=" + Session["BranchCode"].ToString() + "";
                label32.Text = _oo.ReturnTag(_sql, "firstname");
            }
            if (label34.Text == "Cancelled")
            {
                LinkButton linkButton1 = (LinkButton)Grd.Rows[i].FindControl("LinkButton1");
                LinkButton linkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                LinkButton linkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                LinkButton lnkPrintAf = (LinkButton)Grd.Rows[i].FindControl("lnkPrintAF");

                linkButton1.Visible = false; linkButton2.Visible = false; linkButton3.Visible = false; lnkPrintAf.Visible = false;
            }

        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        var ss = lblId.Text;
        lblID.Text = ss;
        drpClassssPanel.Items.Clear();
        _sql = "select Id,Srno,AdmissionType,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,Concession,ReceivedAmount,ChqBounceAmount,left(convert(nvarchar,AdmissionFromDate,106),2) as DD,Right(left(convert(nvarchar,AdmissionFromDate,106),6),3) as MM ,  RIGHT(convert(nvarchar,AdmissionFromDate,106),4) as YY, StudentName,MiddleName,LastName,FatherName,Class,Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount,SessionFor,SessionName from AdmissionFormCollection where ID=" + lblID.Text + "  and BranchCode=" + Session["BranchCode"].ToString() + "";

        if (_oo.ReturnTag(_sql, "SessionName") == Session["SessionName"].ToString())
        {
            txtStudentNamePanel.Text = _oo.ReturnTag(_sql, "StudentName");
            txtStudentNamePanel0.Text = _oo.ReturnTag(_sql, "MiddleName");
            txtStudentNamePanel1.Text = _oo.ReturnTag(_sql, "LastName");
            string _sqls = "select ClassName,Id from ClassMaster where sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CIDORDER";
            _oo.FillDropDown_withValue(_sqls, drpClassssPanel, "ClassName", "Id");
            string _sqls2 = "select ClassName,Id from ClassMaster where sessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ClassName='" + _oo.ReturnTag(_sql, "Class") + "' order by CIDORDER";

            drpClassssPanel.SelectedValue = _oo.ReturnTag(_sqls2, "Id");
            txtAdmissiondatePanel.Text = _oo.ReturnTag(_sql, "AdmissionFromDate");
            txtFatherNamePanel.Text = _oo.ReturnTag(_sql, "FatherName");

            if (_oo.ReturnTag(_sql, "AdmissionType") != string.Empty)
            {
                drpAdmissionTypePanel.SelectedValue = drpAdmissionTypePanel.Items.FindByText(_oo.ReturnTag(_sql, "AdmissionType")).Value;
            }
            else
            {
                drpAdmissionTypePanel.SelectedIndex = 1;
            }
            drpSexPanel.SelectedItem.Text = _oo.ReturnTag(_sql, "Sex");
            txtContactNoPanel.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            txtAmtPanel.Text = _oo.ReturnTag(_sql, "Amount");
            txtCBAmountPanel.Text = _oo.ReturnTag(_sql, "ChqBounceAmount");
            txtReceivedAmountPanel.Text = Convert.ToDecimal(_oo.ReturnTag(_sql, "ReceivedAmount")).ToString(CultureInfo.InvariantCulture);
            txtConcessionPanel.Text = _oo.ReturnTag(_sql, "Concession");
            txtSrNo1.Text = _oo.ReturnTag(_sql, "Srno");
            if (_oo.ReturnTag(_sql, "SessionFor") != "")
            {
                drpSessionPanel.SelectedIndex = drpSessionPanel.Items.IndexOf(drpSessionPanel.Items.FindByText(_oo.ReturnTag(_sql, "SessionFor")));
            }
            else
            {
                drpSessionPanel.SelectedIndex = 1;
            }
            string sqlnew = _sql;
            LoadBranchPanel();
            try
            {
                drpBranchssPanel.SelectedValue = drpBranchssPanel.Items.FindByText(_oo.ReturnTag(sqlnew, "branch")).Value;
            }
            catch
            {
                // ignored
            }
            divPopups.Visible = true;
            //Button2_ModalPopupExtender.Show();
        }
        else
        {
            _oo.MessageBoxforUpdatePanel("Sorry, You can update this record(s) in " + _oo.ReturnTag(_sql, "SessionName"), lblId);
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId2.Text;
        lblvalue.Text = ss;
        Button10_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label38");
        var ss = lblId3.Text;
        Session["AdmissionRecieptNo"] = ss;
        //Response.Redirect("AdmissionReciept_duplicate_New.aspx?print=1");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/pafReciept_duplicate.aspx?print=1','_blank')", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        var dd = txtAdmissiondatePanel.Text.Trim();
        using (var cmd = new SqlCommand())
        {
            cmd.CommandText = "AdmissionFormCollectionUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
            cmd.Parameters.AddWithValue("@StudentName", txtStudentNamePanel.Text);
            cmd.Parameters.AddWithValue("@MiddleName", txtStudentNamePanel0.Text);
            cmd.Parameters.AddWithValue("@LastName", txtStudentNamePanel1.Text);
            cmd.Parameters.AddWithValue("@FatherName", txtFatherNamePanel.Text);
            cmd.Parameters.AddWithValue("@Class", drpClassssPanel.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@sex", drpSexPanel.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@fatherContactNo", txtContactNoPanel.Text);
            cmd.Parameters.AddWithValue("@amount", txtAmtPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@Concession", txtConcessionPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceivedAmountPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@ChqBounceAmount", txtCBAmountPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionFor", drpSessionPanel.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Branch", drpBranchssPanel.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionTypePanel.SelectedItem.Text.Trim());
            if (drpAdmissionTypePanel.SelectedIndex == 0)
            {
                cmd.Parameters.AddWithValue("@Srno", hfStudentId1.Value);
            }

            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _oo.msgbox(Page, msg2, "Updated Successfully.", "S");
                Display();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "select FatherContactNo,Amount from AdmissionFormCollection where RecieptNo='" + lblvalue.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Cancel is null";
        var fatherContactNo = _oo.ReturnTag(_sql, "FatherContactNo");
        var amount = _oo.ReturnTag(_sql, "Amount");
        _sql = "update AdmissionFormCollection set Cancel='Y' where RecieptNo='" + lblvalue.Text + "'";
        _oo.ProcedureDatabase(_sql);
        SendFeescancleSms(fatherContactNo, lblvalue.Text, amount);
        _oo.MessageBox("Cancelled successfully", Page);
        Display();
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text != "")
        {
            TextBox1.Text = TextBox1.Text.Replace(" ", string.Empty);
        }
        _sql = "select Name,MiddleName,LastName,FatherName,Gender,ContactNo,MobileNo,EMail,Address,AdmissionClass,SessionFor from AdmissionEnquiry where EnquiryNo='" + TextBox1.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (_oo.ReturnTag(_sql, "SessionFor") != "")
        {
            drpSession.SelectedValue = drpSession.Items.FindByText(_oo.ReturnTag(_sql, "SessionFor")).Value;
        }
        txtStudentName.Text = _oo.ReturnTag(_sql, "Name");
        txtStudentName0.Text = _oo.ReturnTag(_sql, "MiddleName");
        txtStudentName1.Text = _oo.ReturnTag(_sql, "LastName");
        txtContactNo.Text = _oo.ReturnTag(_sql, "ContactNo");
        drpClassss.SelectedValue = drpClassss.Items.FindByText(_oo.ReturnTag(_sql, "AdmissionClass")).Value;
        txtFatherName.Text = _oo.ReturnTag(_sql, "FatherName");
        drpSex.SelectedItem.Text = _oo.ReturnTag(_sql, "Gender");
    }
    protected void Grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grd.PageIndex = e.NewPageIndex;
        Display();
    }
    protected void txtAmt_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
        // ReSharper disable once RedundantBoolCompare
        if (txtConcession.Enabled == true)
        {
            txtConcession.Focus();
        }
        else
        {
            Submit.Focus();
        }
    }
    protected void txtConcession_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
        Submit.Focus();
    }

    public void CalculateAmount()
    {
        try
        {
            double concession;
            double cbAmount;
            if (txtConcession.Text != "")
            {
                concession = Convert.ToDouble(txtConcession.Text);
            }
            else
            {
                concession = 0;
            }
            if (txtAmt.Text == "")
            {
                txtAmt.Text = "0";
            }
            var amount = Convert.ToDouble(txtAmt.Text);
            double.TryParse(txtCBAmount.Text, out cbAmount);
            var rAmount = (amount + cbAmount) - concession;
            txtReceivedAmount.Text = rAmount.ToString(CultureInfo.InvariantCulture);
        }
        catch
        {
            // ignored
        }
    }
    protected void txtAmtPanel_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount1();
    }
    public void CalculateAmount1()
    {
        double concession;
        double cbAmount;
        if (txtConcessionPanel.Text != "")
        {
            concession = Convert.ToDouble(txtConcessionPanel.Text);
        }
        else
        {
            concession = 0;
        }
        if (txtAmtPanel.Text == "")
        {
            txtAmtPanel.Text = "0";
        }
        var amount = Convert.ToDouble(txtAmtPanel.Text);
        double.TryParse(txtCBAmountPanel.Text, out cbAmount);
        var rAmount = (amount + cbAmount) - concession;
        txtReceivedAmountPanel.Text = rAmount.ToString(CultureInfo.InvariantCulture);
    }
    protected void txtConcession1_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount1();
    }
    protected void drpClassss_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
    }

    private void GetAmount(DropDownList drpClassss, DropDownList drpBranchss, TextBox txt)
    {
        if (string.IsNullOrEmpty(Request.QueryString["check"]))
        {
            _sql = "Select Amount,Concession from AdmissionFormFeeMaster where Classid='" + drpClassss.SelectedValue + "'  and AdminssionType='" + drpAdmissionType.SelectedValue + "' and  Gender='" + drpSex.SelectedValue + "' and (Branchid='" + drpBranchss.SelectedValue + "' or Branchid is null)  and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            txtAmt.Text = _oo.ReturnTag(_sql, "Amount");
            txt.Text = _oo.ReturnTag(_sql, "Concession") == "" ? "0" : _oo.ReturnTag(_sql, "Concession");
            CalculateAmount();
        }
        else
        {
            _sql = "Select Amount,Concession from AdmissionFormFeeMaster where Classid='" + drpClassss.SelectedValue + "'  and AdminssionType='" + drpAdmissionType.SelectedValue + "' and  Gender='" + drpSex.SelectedValue + "' and (Branchid='" + drpBranchss.SelectedValue + "' or Branchid is null)  and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            txtAmt.Text = _oo.ReturnTag(_sql, "Amount");
            txt.Text = _oo.ReturnTag(_sql, "Concession") == "" ? "0" : _oo.ReturnTag(_sql, "Concession");
            CalculateAmount();
        }
        _sql = "Select Amount,Concession from AdmissionFormFeeMaster where Classid='" + drpClassss.SelectedValue + "'  and AdminssionType='" + drpAdmissionType.SelectedValue + "' and  Gender='" + drpSex.SelectedValue + "' and (Branchid='" + drpBranchss.SelectedValue + "' or Branchid is null)  and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (!_oo.Duplicate(_sql))
        {
            Submit.Visible = false;
        }
    }
    protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownMOD.SelectedIndex == 0)
        {
            div5.Visible = false;
            div8.Visible = false;
            div6.Visible = false;
        }
        else
        {
            div5.Visible = true;
            div8.Visible = true;
            div6.Visible = true;
        }
        if (DropDownMOD.SelectedIndex == 1 || DropDownMOD.SelectedIndex == 2)
        {
            Label54.Text = "Instrument Date";
            Label42.Text = "Instrument No.";
            Label2.Text = "Issuer";

        }
        if (DropDownMOD.SelectedIndex == 3)
        {
            Label54.Text = "Transaction Date";
            Label42.Text = "Card No.";
            Label2.Text = "Issuer";

        }
        if (DropDownMOD.SelectedIndex == 4)
        {
            Label54.Text = "Transaction Date";
            Label42.Text = "Ref. No.";
            Label2.Text = "Issuer";

        }
        if (DropDownMOD.SelectedIndex == 5)
        {
            Label54.Text = "Transaction Date";
            Label42.Text = "Ref. No.";
            Label2.Text = "Reference Name";

        }
    }
    protected void lnkPrintAF_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId4 = (Label)chk.NamingContainer.FindControl("Label39");
        Label lblTemplate = (Label)chk.NamingContainer.FindControl("lblTemplate");
        string ss = lblId4.Text;
        Session["AdmissionRecieptNo"] = ss;
        if (lblTemplate.Text.ToLower() == "template 1")
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate1.aspx?print=1','_blank')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate2.aspx?print=1','_blank')", true);
        }
    }
    protected void drpBranchss_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAmount(drpClassss, drpBranchss, txtConcession);
        drpBranchss.Focus();
    }

    private void LoadBranch()

    {
        if (string.IsNullOrEmpty(Request.QueryString["check"]))
        {
            _sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClassss.SelectedValue + "' and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpBranchss, "BranchName", "Id");
            drpBranchss.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));
        }
        else
        {
            _sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClassss.SelectedValue + "' and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpBranchss, "BranchName", "Id");
            drpBranchss.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));
        }

    }

    private void LoadBranchPanel()
    {
        _sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClassssPanel.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(_sql, drpBranchssPanel, "BranchName", "Id");
        drpBranchssPanel.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));
    }

    protected void drpBranchssPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAmount(drpClassssPanel, drpBranchssPanel, txtConcessionPanel);
    }
    protected void drpClassssPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranchPanel();
    }

    protected void DDChkYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDChkYear, DDChkMonth, DDChkDate);
    }

    protected void DDChkMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        _oo.YearDropDown(DDChkYear, DDChkMonth, DDChkDate);
    }

    protected void txtCBAmount_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }

    protected void txtCBAmountPanel_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount1();
    }

    protected void txtSrNo_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            txtSrNo.Text = txtSrNo.Text.Replace("&", "/").Trim();
        }

        _sql = "Select TOP (1) FirstName,MiddleName,LastName,FatherName,FamilyContactNo,Gender from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SrNo ='" + studentId + "'";
        txtStudentName.Text = _oo.ReturnTag(_sql, "FirstName");
        txtStudentName0.Text = _oo.ReturnTag(_sql, "MiddleName");
        txtStudentName1.Text = _oo.ReturnTag(_sql, "LastName");
        txtFatherName.Text = _oo.ReturnTag(_sql, "FatherName");
        txtContactNo.Text = _oo.ReturnTag(_sql, "FamilyContactNo");

        drpSex.SelectedIndex = _oo.ReturnTag(_sql, "Gender").ToUpper() == "FEMALE" ? 1 : _oo.ReturnTag(_sql, "Gender").ToUpper() == "MALE" ? 0 : 2;
        drpClassss.Focus();
    }

    protected void drpAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpAdmissionType.SelectedIndex == 1)
        {
            txtStudentName.ReadOnly = true;
            txtStudentName0.ReadOnly = true;
            txtStudentName1.ReadOnly = true;
            txtFatherName.ReadOnly = true;
            txtContactNo.ReadOnly = true;
            drpSex.Enabled = false;
        }
        else
        {
            txtStudentName.ReadOnly = false;
            txtStudentName0.ReadOnly = false;
            txtStudentName1.ReadOnly = false;
            txtFatherName.ReadOnly = false;
            txtContactNo.ReadOnly = false;
            drpSex.Enabled = true;
        }
        txtSrNo.Focus();
    }

    protected void LinkButtons_Click(object sender, EventArgs e)
    {
        divPopups.Visible = false;
    }

    protected void btnPayStack_Click(object sender, EventArgs e)
    {


        Session["SessionName"] = drpSession.Text.Trim();
        Session["LoginName"] = txtContactNo.Text.Trim();
        string dd;
        if (drpClassss.SelectedItem.ToString() == "<--Select-->")
        {
            _oo.MessageBox("Please Select Condition!", Page);
        }
        else
        {
            var txnId = "";
            txnId = hdnTxtNos.Value;
            dd = txtAdmissiondate.Text;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "USP_AdmissionFormOnline";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryFor", "I");
                cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
                cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@MiddleName", txtStudentName0.Text);
                cmd.Parameters.AddWithValue("@LastName", txtStudentName1.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Class", drpClassss.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@LoginName", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                cmd.Parameters.AddWithValue("@sex", drpSex.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Mobile", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@amount", txtReceivedAmount.Text);
                cmd.Parameters.AddWithValue("@sessionName", drpSession.Text);
                cmd.Parameters.AddWithValue("@SessionFor", drpSession.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@Branch", drpBranchss.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionType.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceivedAmount.Text.Trim());
                cmd.Parameters.AddWithValue("@TxnID", txnId);
                cmd.Parameters.AddWithValue("@GateWayName", "PayStack");
                cmd.Connection = _con;
                try
                {
                    _con.Open();
                    var o = cmd.ExecuteNonQuery();
                    if (o == 1)
                    {
                        _con.Close();
                        try
                        {
                            if (DropDownMOD.SelectedIndex == 0)
                            {
                                decimal balance;
                                decimal.TryParse(txtReceivedAmount.Text.Trim(), out balance);
                                USP_InsertAPITransactionAdmission(txnId, txtContactNo.Text, balance);
                                InserApiResponse(txnId, 1, balance);
                            }
                        }
                        catch (Exception)
                        {
                            _con.Open();
                        }
                    }

                }
                catch (Exception ex)
                {
                    _con.Open();
                }
            }
        }
    }

    protected void InserApiResponse(string txnid, int status, decimal charges)
    {

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "[USP_SetAPITransactionUpdate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@TxnID", txnid.Trim());
            cmd.Parameters.AddWithValue("@Charges", charges);
            cmd.Parameters.AddWithValue("@Status", status);
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Open();
                InserManageResponse(txnid);
            }
            catch (Exception ex)
            {
                _con.Open();
            }
        }
    }
    public void InserManageResponse(string txnid)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@txtno", txnid));

            DataSet ds = new DataSet();

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetAdmissionOnline", param);
            var recMobile = String.Empty;
            if (ds != null)
            {
                DataTable dt;
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    var recid = dt.Rows[0]["RecieptNo"].ToString();
                    var recStatus = dt.Rows[0]["Status"].ToString();
                    recMobile = dt.Rows[0]["Mobile"].ToString();
                    if (recStatus.Trim().ToLower() == "paid")
                    {
                        Response.Redirect("../2/pafReciept.aspx?print=1&rid=" + recid + "", false);
                    }
                    else if (recStatus.Trim().ToLower() == "cancelled")
                    {
                        Response.Redirect("~/ap/Admission_Details.aspx?txtno=" + recMobile + "", false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../ap/default.aspx?from=ManageR" + ex.Message);
        }

    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}