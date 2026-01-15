using c4SmsNew;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AdminPaf : Page
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
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            switch (Session["Logintype"].ToString())
            {
                case "Admin":
                    MasterPageFile = "~/Master/admin_root-manager.master";
                    break;
                case "Guardian":
                    MasterPageFile = "~/sp/sp_root-manager.master";
                    break;
            }
        }
        else
        {
            if (Session["mobilenochk"] == null) { Response.Redirect("../ap/default.aspx"); }
            MasterPageFile = "~/ap/main-ap.master";
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
        }
        else
        {
            divsearch1.Visible = false;
            txtAdmissiondate.Enabled = false;
            divadmissiontype.Visible = false;
            DropDownMOD.Text = "Online";
            divpaymentmode.Visible = false;
            txtContactNo.Text = Request.QueryString["id"];
            txtContactNo.Enabled = false;
            divdatabind.Visible = false;
        }


        if (!IsPostBack)
        {
            //_oo.LoadLoader(loader);
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
                        txtConcession.Enabled = (dtp.Rows[0]["AdmissionDiscount"].ToString() == "0" ? false : true);
                        txtAdmissiondate.Enabled = (dtp.Rows[0]["AdmissionDate"].ToString() == "0" ? false : true);
                    }
                }
                catch (Exception ex)
                {
                }
            }


            _oo.AddDateMonthYearDropDown(DDChkYear, DDChkMonth, DDChkDate);
            _oo.FindCurrentDateandSetinDropDown(DDChkYear, DDChkMonth, DDChkDate);
            Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            txtAdmissiondate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            txtFromDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            if (string.IsNullOrEmpty(Request.QueryString["check"]))
            {
                drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));

                Display();
                Permission_Values();
                LoadSession();
            }
            else
            {
                GetSessionClass();
            }
            GetClassFromAdmissionFeeForm();
            drpBranchss.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));

            _sql = "select TOP(1) Template, IsLock from AdmissionFormTemplate where BranchCode=" + Session["BranchCode"] + "";
            ddlTemplateAdmission.SelectedValue = _oo.ReturnTag(_sql, "Template");
            if (_oo.ReturnTag(_sql, "IsLock").ToLower() == "true" || _oo.ReturnTag(_sql, "IsLock").ToLower() == "1")
            {
                ddlTemplateAdmission.Enabled = false;
            }

        }
    }

    private void GetClassFromAdmissionFeeForm()
    {
        _sql = "Select Class from (Select 'All' Class,0 CIDOrder Union Select Distinct Class,cm.CIDOrder from AdmissionFormCollection afc left join ClassMaster cm on cm.classname=afc.Class)T1 Order by CIDOrder";
        _oo.FillDropDownWithOutSelect(_sql, drpClass, "Class");
    }

    private void GetSessionClass()
    {
        try
        {
            _sql = "select SessionName from SessionMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDownWithOutSelect(_sql, drpSession, "SessionName");
            _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and DATEDIFF(day, FromDate, GETDATE())>200 order by SessionId desc";
            if (_oo.Duplicate(_sql))
            {
                drpSession.SelectedIndex = (drpSession.Items.Count - 1);
            }
            else
            {
                try
                {
                    _sql = "select top(1) SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " and SessionId <>";
                    _sql += "(select top(1) SessionId from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionId desc) order by SessionId desc";
                    drpSession.SelectedValue = _oo.ReturnTag(_sql, "SessionName");
                }
                catch (Exception ex)
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
            _sql = "select medium from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown_withValue(_sql, ddlMedium, "medium", "medium");
            ddlMedium.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
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
        _sql = "select medium from MediumMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
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
            _sql += "(select top(1) SessionId from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionId desc) order by SessionId desc";
            drpSession.SelectedValue = _oo.ReturnTag(_sql, "SessionName");
        }
        drpSession.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        drpSessionPanel.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        _sql = "select ClassName,Id from ClassMaster where sessionName='" + drpSession.SelectedValue + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CIDORDER";
        _oo.FillDropDown_withValue(_sql, drpClassss, "ClassName", "Id");
        drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        _sql = "select medium from MediumMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
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

    public void setvalue(string GateWayName, string GateWayFor, int BranchCode)
    {
        string sql = "select * from TblPaymentGateway where BranchCode=" + BranchCode + " and GateWayFor='" + GateWayFor + "' and GateWayName='" + GateWayName + "' and Isactive=1";
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
    protected void SetTransaction(string txnId, string hashStr, string srNo, string session, decimal amount, string GatewayName, string PaymentFor, int BranchCode, Page pg)
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
            dt = DAL.DALInstance.GetValueInTable("select id,StudentName +''+ MiddleName +' '+ LastName Name,Mobile,email  from AdmissionFormOnline where Mobile='" + srNo + "' AND SessionName='" + session + "' and BranchCode=" + Session["BranchCode"].ToString() + " and AdmissionType='new' and txnid='" + txnid + "' order by ID desc");

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
                data.Add("surl", surl);
                data.Add("furl", furl);
                data.Add("lastname", "");
                data.Add("curl", "");
                data.Add("address1", "");
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
    protected void Submit_Click(object sender, EventArgs e)
    {
        string xx, dd;
        xx = _oo.FindRecieptNo();
        if (xx == "")
        {
            _oo.MessageBox("Please Initialize Receipt No.!", Page);
            return;
        }
        if (drpClassss.SelectedItem.ToString() == "<--Select-->")
        {
            _oo.MessageBox("Please Select Condition!", Page);
        }
        else
        {
            _sql = "select max(id) as id from AdmissionFormCollection";

            dd = txtAdmissiondate.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AdmissionFormCollectionProc";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@RecieptNo", xx);
            cmd.Parameters.AddWithValue("@AdmissionFromDate", dd);
            cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@Middle_name", txtStudentName0.Text);
            cmd.Parameters.AddWithValue("@Last_Name", txtStudentName1.Text);
            cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
            cmd.Parameters.AddWithValue("@Class", drpClassss.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@sex", drpSex.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@fatherContactNo", txtContactNo.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@amount", txtAmt.Text);
            if (string.IsNullOrEmpty(txtDob.Text))
                cmd.Parameters.AddWithValue("@dob", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@dob", txtDob.Text);
            cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString());
            if (txtConcession.Text != "")
            {
                cmd.Parameters.AddWithValue("@Concession", txtConcession.Text.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@Concession", "0");
            }
            cmd.Parameters.AddWithValue("@ReceivedAmount", txtReceivedAmount.Text.Trim());
            if (TextBox2.Text != "")
            {
                cmd.Parameters.AddWithValue("@CheckDDNo", TextBox2.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CheckDDNo", "N/A");
            }
            if (TextBox3.Text != "")
            {
                cmd.Parameters.AddWithValue("@BankName", TextBox2.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BankName", "N/A");
            }
            cmd.Parameters.AddWithValue("@Status", "Paid");
            if (TextBox1.Text.Trim() != "")
            {
                cmd.Parameters.AddWithValue("@EnquiryNo", TextBox1.Text.Trim());
            }
            cmd.Parameters.AddWithValue("@MOP", DropDownMOD.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SessionFor", drpSession.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Branch", drpBranchss.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionType.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Template", ddlTemplateAdmission.SelectedValue);


            cmd.Parameters.AddWithValue("@bookletno", TextBox5.Text.Trim());


            decimal chqBounceAmount;
            decimal.TryParse(txtCBAmount.Text, out chqBounceAmount);
            cmd.Parameters.AddWithValue("@ChqBounceAmount", chqBounceAmount);

            if (drpAdmissionType.SelectedIndex == 1)
            {
                cmd.Parameters.AddWithValue("@Srno", hfStudentId.Value);
            }

            SqlParameter outputValue = new SqlParameter("@result", "");
            outputValue.Size = 0x100;
            outputValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputValue);

            cmd.Connection = _con;
            string recieptSrNo = "";
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //Session["AdmissionRecieptNo"] = xx;
                recieptSrNo = cmd.Parameters["@result"].Value.ToString();
                Display();
                _oo.MessageBox("Submited successfully", Page);
                drpSession.Enabled = true;
                drpAdmissionType.SelectedIndex = 0;
                drpAdmissionType.Enabled = true;
                txtStudentName.Enabled = true;
                txtStudentName0.Enabled = true;
                txtStudentName1.Enabled = true;
                txtFatherName.Enabled = true;
                txtContactNo.Enabled = true;
                drpSex.Enabled = true;
                drpClassss.Enabled = true;
                Session["isDuplicate"] = "No";
                Session["txnchk"] = Session["LoginName"].ToString();
                //SendFeesSms(txtContactNo.Text, recieptSrNo, txtAmt.Text);
                ComposeSMS(recieptSrNo);
                Response.Redirect("../2/pafReciept.aspx?print=1&AdmissionRecieptNo=" + recieptSrNo, false);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }

    public void ComposeSMS(string recieptNo)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@SessionName",Session["SessionName"]),
                new SqlParameter("@ReceiptNo",recieptNo),
                new SqlParameter("@BranchCode",Session["BranchCode"])
            };
            DataSet ds = _oo.ReturnDataSet("USP_AdmissionFeeTemplate", param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                string msg = SendSms(ds);
            }

        }
        catch
        {

        }

    }
    public string SendSms(DataSet ds)
    {
        string msg;
        try
        {
            DataTable data = new DataTable();
            data = ds.Tables[0];

            var fatherContactNo = data.Rows[0]["FatherContactNo"].ToString();

            DataTable template = new DataTable();
            template = ds.Tables[1];

            msg = template.Rows[0][0].ToString();
            string[] param = template.Rows[0][1].ToString().Split(',');

            string[] daynamicVariables = msg.Split(new char[0]);
            foreach (var para in param)
            {
                string value = data.Rows[0][para].ToString();
                for (int i = 0; i < daynamicVariables.Count(); i++)
                {
                    if (daynamicVariables[i].ToString() == "{{{}}}")
                    {
                        daynamicVariables[i] = value;
                        break;
                    }
                }
            }

            msg = string.Join(" ", daynamicVariables);

            SendFeesSms(fatherContactNo, msg, "2");

        }
        catch
        {
            msg = "";
        }
        return msg;
    }

    public string SendFeesSms(string fmobileNo, string msg, string smsPageId)
    {
        string res = "0";
        try
        {
            _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "HitValue") == "") return res;
            if (_oo.ReturnTag(_sql, "HitValue") != "true") return res;
            string sql1 = "Select SmsSent From SmsEmailMaster Where Id='\" + smsPageId + \"' and BranchCode='" + Session["BranchCode"] + "'";
            if (_oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
            {
                var sadpNew = new SMSAdapterNew();
                if (fmobileNo == "") return "0";
                sadpNew.Send(msg, fmobileNo, smsPageId);
                res = "1";
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return res;
    }

    //public void SendFeesSms(string fmobileNo, string recieptNo, string amount)
    //{
    //	_sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
    //	if (_oo.ReturnTag(_sql, "HitValue") != "")
    //	{
    //		if (_oo.ReturnTag(_sql, "HitValue") == "true")
    //		{
    //			SMSAdapterNew sadpNew = new SMSAdapterNew();
    //			// INR {#var#} received towards Admission Form. Receipt No. {#var#}From, RRSLKO
    //			var mess = "INR " + amount + " received towards Admission Form. Receipt No. " + recieptNo + " From, ";
    //			if (fmobileNo != "")
    //			{
    //				_sql = "Select SmsSent From SmsEmailMaster where Id='11' ";
    //				if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
    //				{
    //					sadpNew.Send(mess, fmobileNo, "11");
    //				}
    //			}
    //		}
    //	}
    //}

    public void SendFeescancleSms(string fmobileNo, string recieptNo, string amount)
    {
        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(_sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(_sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                // Receipt No.{#var#} has been cancelled of Admission Form. Refunded Amount is INR {#var#}From, RRSLKO
                var mess = "Receipt No. " + recieptNo + " has been cancelled of Admission Form. Refunded Amount is INR " + amount + " From, ";

                if (fmobileNo != "")
                {
                    _sql = "Select SmsSent From SmsEmailMaster where Id='2' and BranchCode='" + Session["BranchCode"] + "'";
                    if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                    {
                        sadpNew.Send(mess, fmobileNo, "32");
                    }
                }
            }
        }
    }
    public void Display(string btnName = "")
    {
        if (drpClass.SelectedValue == "All")
        {
            _sql = "select Row_Number() over (order by Id Asc) as SNo ,id, Status, AdmissionType,ReceivedAmount,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,dbo.Name(StudentName,MiddleName,LastName) as StudentNames,FatherName,Class,ISNULL(Branch,'Other') Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount,status as Cancel, mop, Template  from AdmissionFormCollection where BranchCode=" + Session["BranchCode"].ToString() + "";
            _sql += " and convert(date, AdmissionFromDate) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' order by id Desc";
        }
        else
        {
            _sql = "select Row_Number() over (order by Id Asc) as SNo ,id, Status, AdmissionType,ReceivedAmount,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,dbo.Name(StudentName,MiddleName,LastName) as StudentNames,FatherName,Class,ISNULL(Branch,'Other') Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount,status as Cancel, mop, Template  from AdmissionFormCollection where BranchCode=" + Session["BranchCode"].ToString() + "";
            _sql += " and convert(date, AdmissionFromDate) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' and Class='" + drpClass.SelectedValue + "' order by id Desc";
        }

        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            if (btnName == "View")
            {
                Grd.HeaderRow.Cells[8].Visible = false;
            }
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                LinkButton linkButton1 = (LinkButton)Grd.Rows[i].FindControl("LinkButton1");
                Label label32 = (Label)Grd.Rows[i].FindControl("Label32");
                Label label34 = (Label)Grd.Rows[i].FindControl("Label34");
                Label label18 = (Label)Grd.Rows[i].FindControl("Label18");

                if (label32.Text == "")
                {
                    _sql = "select top(1) StudentName as firstname, admissiontype from AdmissionFormCollection where RecieptNo='" + label18.Text + "'  and BranchCode=" + Session["BranchCode"].ToString() + "";
                    //label32.Text = _oo.ReturnTag(_sql, "firstname");
                    if (_oo.ReturnTag(_sql, "admissiontype").ToLower() == "old")
                    {
                        linkButton1.Visible = false;
                    }
                    else
                    {
                        linkButton1.Visible = true;
                    }
                }
                //_sql = "select top(1) StudentName as firstname, admissiontype from AdmissionFormCollection where RecieptNo='" + label18.Text + "'  and BranchCode=" + Session["BranchCode"].ToString() + "";
                //label32.Text = _oo.ReturnTag(_sql, "firstname");
                //if (_oo.ReturnTag(_sql, "admissiontype").ToLower() == "old")
                //{
                //    linkButton1.Visible = false;
                //}
                //else
                //{
                //    linkButton1.Visible = true;
                //}
                if (label34.Text == "Cancelled")
                {

                    //LinkButton linkButton2 = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
                    LinkButton linkButton3 = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
                    LinkButton lnkPrintAf = (LinkButton)Grd.Rows[i].FindControl("lnkPrintAF");

                    linkButton1.Visible = false;
                    //linkButton2.Visible = false; 
                    linkButton3.Visible = false;
                    lnkPrintAf.Visible = false;
                }
                if (btnName == "View")
                {
                    Grd.Rows[i].Cells[8].Visible = false;
                }
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Record(s) not found!", "A");
        }
    }

    protected void LinkView_Click(object sender, EventArgs e)
    {
        Display("View");

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        var ss = lblId.Text;
        lblID.Text = ss;
        drpClassssPanel.Items.Clear();
        _sql = "select Id,Srno,template,AdmissionType,RecieptNo,convert(nvarchar,AdmissionFromDate,106) as AdmissionFromDate,convert(nvarchar,convert(date,dob),106) dob,Concession,ReceivedAmount,ChqBounceAmount,left(convert(nvarchar,AdmissionFromDate,106),2) as DD,Right(left(convert(nvarchar,AdmissionFromDate,106),6),3) as MM ,  RIGHT(convert(nvarchar,AdmissionFromDate,106),4) as YY,StudentName,MiddleName,LastName,FatherName,dbo.Name(StudentName,MiddleName,LastName) StdName,Class,Branch,LoginName,BranchCode,convert(nvarchar,RecordDate,106) as RecordDate,Sex,FatherContactNo,Amount,SessionFor,SessionName,bookletno from AdmissionFormCollection where ID=" + lblID.Text + "  and BranchCode=" + Session["BranchCode"].ToString() + "";

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
            if (drpAdmissionTypePanel.SelectedValue.ToLower() == "old")
            {
                TextBox1Panel.Enabled = false;
                txtSrNo1.Enabled = false;
                txtStudentNamePanel.Enabled = false;
                txtStudentNamePanel0.Enabled = false;
                txtStudentNamePanel1.Enabled = false;
                txtFatherNamePanel.Enabled = false;
                txtContactNoPanel.Enabled = false;
                drpSexPanel.Enabled = false;
                txtdobPanel.Enabled = false;
            }
            else
            {
                TextBox1Panel.Enabled = true;
                txtSrNo1.Enabled = false;
                txtStudentNamePanel.Enabled = true;
                txtStudentNamePanel0.Enabled = true;
                txtStudentNamePanel1.Enabled = true;
                txtFatherNamePanel.Enabled = true;
                txtContactNoPanel.Enabled = true;
                drpSexPanel.Enabled = true;
                txtdobPanel.Enabled = true;
            }
            drpSexPanel.SelectedItem.Text = _oo.ReturnTag(_sql, "Sex");
            txtContactNoPanel.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            txtAmtPanel.Text = _oo.ReturnTag(_sql, "Amount");
            txtdobPanel.Text = _oo.ReturnTag(_sql, "dob");
            txtStudentNamePanel1.Text = _oo.ReturnTag(_sql, "LastName");
            TextBox6.Text = _oo.ReturnTag(_sql, "bookletno");
            try
            {
                DropDownList1.SelectedValue = _oo.ReturnTag(_sql, "template");
            }
            catch (Exception ex)
            {

            }

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

            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            _oo.MessageBoxforUpdatePanel("Sorry, You can update this record in " + _oo.ReturnTag(_sql, "SessionName") + ".", lblId);
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
        //Session["AdmissionRecieptNo"] = ss;
        //Response.Redirect("AdmissionReciept_duplicate_New.aspx?print=1");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/pafReciept_duplicate.aspx?print=1&AdmissionRecieptNo=" + ss + "','_blank')", true);
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
            if (string.IsNullOrEmpty(txtdobPanel.Text))
                cmd.Parameters.AddWithValue("@dob", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@dob", txtdobPanel.Text);
            cmd.Parameters.AddWithValue("@AdmissionType", drpAdmissionTypePanel.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@bookletno", TextBox6.Text.Trim());
            cmd.Parameters.AddWithValue("@Template", DropDownList1.Text.Trim());
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
                _oo.msgbox(Page, msgbox2, "Updated Successfully.", "S");
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
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            TextBox1.Text = TextBox1.Text.Replace(" ", string.Empty);
        }
        string sql1s = "select EnquiryNo from AdmissionFormCollection where EnquiryNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (_oo.Duplicate(sql1s))
        {
            _oo.msgbox(Page, msgView, "This student has taken the Admission Form already!", "A");
            drpSession.Enabled = true;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = true;
            txtStudentName.Enabled = true;
            txtStudentName.Text = "";
            txtStudentName0.Enabled = true;
            txtStudentName0.Text = "";
            txtStudentName1.Enabled = true;
            txtStudentName1.Text = "";
            txtFatherName.Enabled = true;
            txtFatherName.Text = "";
            txtContactNo.Enabled = true;
            txtContactNo.Text = "";
            drpSex.Enabled = true;
            drpSex.SelectedIndex = 0;
            drpClassss.Enabled = true;
            TextBox1.Text = "";
            return;
        }
        string _sql2 = "select Name,MiddleName,LastName,FatherName,Gender,ContactNo,MobileNo,EMail,Address,AdmissionClass,SessionFor, Status from AdmissionEnquiry where EnquiryNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (!_oo.Duplicate(_sql2))
        {
            _oo.msgbox(Page, msgView, "Invalid Enquiry No.!", "A");
            drpSession.Enabled = true;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = true;
            txtStudentName.Enabled = true;
            txtStudentName.Text = "";
            txtStudentName0.Enabled = true;
            txtStudentName0.Text = "";
            txtStudentName1.Enabled = true;
            txtStudentName1.Text = "";
            txtFatherName.Enabled = true;
            txtFatherName.Text = "";
            txtContactNo.Enabled = true;
            txtContactNo.Text = "";
            drpSex.Enabled = true;
            drpSex.SelectedIndex = 0;
            drpClassss.Enabled = true;
            TextBox1.Text = "";
            return;
        }
        if (_oo.ReturnTag(_sql2, "Status") != "Pending")
        {
            _oo.msgbox(Page, msgView, "This Enquiry No. already processed!", "A");
            drpSession.Enabled = true;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = true;
            txtStudentName.Enabled = true;
            txtStudentName.Text = "";
            txtStudentName0.Enabled = true;
            txtStudentName0.Text = "";
            txtStudentName1.Enabled = true;
            txtStudentName1.Text = "";
            txtFatherName.Enabled = true;
            txtFatherName.Text = "";
            txtContactNo.Enabled = true;
            txtContactNo.Text = "";
            drpSex.Enabled = true;
            drpSex.SelectedIndex = 0;
            drpClassss.Enabled = true;
            TextBox1.Text = "";
            return;
        }
        else
        {
            if (_oo.ReturnTag(_sql2, "SessionFor") != "")
            {
                drpSession.SelectedValue = drpSession.Items.FindByText(_oo.ReturnTag(_sql2, "SessionFor")).Value;
                GetSessionClass();
            }
            txtStudentName.Text = _oo.ReturnTag(_sql2, "Name");
            txtStudentName0.Text = _oo.ReturnTag(_sql2, "MiddleName");
            txtStudentName1.Text = _oo.ReturnTag(_sql2, "LastName");
            txtContactNo.Text = _oo.ReturnTag(_sql2, "ContactNo");
            txtEmail.Text = _oo.ReturnTag(_sql2, "Email");
            if (_oo.ReturnTag(_sql2, "AdmissionClass") != "")
            {
                drpClassss.SelectedValue = drpClassss.Items.FindByText(_oo.ReturnTag(_sql2, "AdmissionClass")).Value;
            }

            txtFatherName.Text = _oo.ReturnTag(_sql2, "FatherName");
            drpSex.SelectedValue = _oo.ReturnTag(_sql2, "Gender");

            drpSession.Enabled = false;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = false;
            txtStudentName.Enabled = false;
            txtStudentName0.Enabled = false;
            txtStudentName1.Enabled = false;
            txtFatherName.Enabled = false;
            txtContactNo.Enabled = false;
            drpSex.Enabled = false;
            drpClassss.Enabled = false;
            LoadBranch();
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text != "")
        {
            TextBox1.Text = TextBox1.Text.Replace(" ", string.Empty);
        }
        string sql1s = "select EnquiryNo from AdmissionFormCollection where EnquiryNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (_oo.Duplicate(sql1s))
        {
            _oo.msgbox(Page, msgView, "This student has taken the Admission Form already!", "A");
            drpSession.Enabled = true;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = true;
            txtStudentName.Enabled = true;
            txtStudentName.Text = "";
            txtStudentName0.Enabled = true;
            txtStudentName0.Text = "";
            txtStudentName1.Enabled = true;
            txtStudentName1.Text = "";
            txtFatherName.Enabled = true;
            txtFatherName.Text = "";
            txtContactNo.Enabled = true;
            txtContactNo.Text = "";
            drpSex.Enabled = true;
            drpSex.SelectedIndex = 0;
            drpClassss.Enabled = true;
            TextBox1.Text = "";
            return;
        }
        string _sql2 = "select Name,MiddleName,LastName,FatherName,Gender,ContactNo,MobileNo,EMail,Address,AdmissionClass,SessionFor, Status from AdmissionEnquiry where EnquiryNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (!_oo.Duplicate(_sql2))
        {
            _oo.msgbox(Page, msgView, "Invalid Enquiry No.!", "A");
            drpSession.Enabled = true;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = true;
            txtStudentName.Enabled = true;
            txtStudentName.Text = "";
            txtStudentName0.Enabled = true;
            txtStudentName0.Text = "";
            txtStudentName1.Enabled = true;
            txtStudentName1.Text = "";
            txtFatherName.Enabled = true;
            txtFatherName.Text = "";
            txtContactNo.Enabled = true;
            txtContactNo.Text = "";
            drpSex.Enabled = true;
            drpSex.SelectedIndex = 0;
            drpClassss.Enabled = true;
            TextBox1.Text = "";
            return;
        }
        if (_oo.ReturnTag(_sql2, "Status") != "Pending")
        {
            _oo.msgbox(Page, msgView, "This Enquiry No. already processed!", "A");
            drpSession.Enabled = true;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = true;
            txtStudentName.Enabled = true;
            txtStudentName.Text = "";
            txtStudentName0.Enabled = true;
            txtStudentName0.Text = "";
            txtStudentName1.Enabled = true;
            txtStudentName1.Text = "";
            txtFatherName.Enabled = true;
            txtFatherName.Text = "";
            txtContactNo.Enabled = true;
            txtContactNo.Text = "";
            drpSex.Enabled = true;
            drpSex.SelectedIndex = 0;
            drpClassss.Enabled = true;
            TextBox1.Text = "";
            return;
        }
        else
        {
            if (_oo.ReturnTag(_sql2, "SessionFor") != "")
            {
                drpSession.SelectedValue = drpSession.Items.FindByText(_oo.ReturnTag(_sql2, "SessionFor")).Value;
                GetSessionClass();
            }
            txtStudentName.Text = _oo.ReturnTag(_sql2, "Name");
            txtStudentName0.Text = _oo.ReturnTag(_sql2, "MiddleName");
            txtStudentName1.Text = _oo.ReturnTag(_sql2, "LastName");
            txtContactNo.Text = _oo.ReturnTag(_sql2, "ContactNo");
            txtEmail.Text = _oo.ReturnTag(_sql2, "Email");
            if (_oo.ReturnTag(_sql2, "AdmissionClass") != "")
            {
                drpClassss.SelectedValue = drpClassss.Items.FindByText(_oo.ReturnTag(_sql2, "AdmissionClass")).Value;
            }

            txtFatherName.Text = _oo.ReturnTag(_sql2, "FatherName");
            drpSex.SelectedValue = _oo.ReturnTag(_sql2, "Gender");

            drpSession.Enabled = false;
            drpAdmissionType.SelectedIndex = 0;
            drpAdmissionType.Enabled = false;
            txtStudentName.Enabled = false;
            txtStudentName0.Enabled = false;
            txtStudentName1.Enabled = false;
            txtFatherName.Enabled = false;
            txtContactNo.Enabled = false;
            drpSex.Enabled = false;
            drpClassss.Enabled = false;
            LoadBranch();
        }
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
            _sql = "Select Amount,Concession from AdmissionFormFeeMaster where Classid='" + drpClassss.SelectedValue + "' and  Gender='" + drpSex.SelectedValue + "'  and AdminssionType='" + drpAdmissionType.SelectedValue + "' and (Branchid='" + drpBranchss.SelectedValue + "' or Branchid is null)  and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            txtAmt.Text = _oo.ReturnTag(_sql, "Amount");
            txt.Text = _oo.ReturnTag(_sql, "Concession") == "" ? "0" : _oo.ReturnTag(_sql, "Concession");
            CalculateAmount();
        }
        else
        {
            _sql = "Select Amount,Concession from AdmissionFormFeeMaster where Classid='" + drpClassss.SelectedValue + "' and  Gender='" + drpSex.SelectedValue + "'  and AdminssionType='" + drpAdmissionType.SelectedValue + "' and (Branchid='" + drpBranchss.SelectedValue + "' or Branchid is null)  and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            txtAmt.Text = _oo.ReturnTag(_sql, "Amount");
            txt.Text = _oo.ReturnTag(_sql, "Concession") == "" ? "0" : _oo.ReturnTag(_sql, "Concession");
            CalculateAmount();
        }
        _sql = "Select Amount,Concession from AdmissionFormFeeMaster where Classid='" + drpClassss.SelectedValue + "' and  Gender='" + drpSex.SelectedValue + "'  and AdminssionType='" + drpAdmissionType.SelectedValue + "' and (Branchid='" + drpBranchss.SelectedValue + "' or Branchid is null)  and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (!_oo.Duplicate(_sql))
        {
            Submit.Visible = false;
        }
    }
    protected void DropDownMOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        divsts.Visible = false;
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
        if (DropDownMOD.SelectedIndex == 1)
        {
            divsts.Visible = true;
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
        Label lblTemplate = (Label)chk.NamingContainer.FindControl("lblTemplate");
        Label lblId4 = (Label)chk.NamingContainer.FindControl("Label39");
        string ss = lblId4.Text;
        Session["AdmissionRecieptNo"] = ss;
        if (lblTemplate.Text.ToLower() == "template 1")
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate1.aspx?print=1','_blank')", true);
        }
        else if (lblTemplate.Text.ToLower() == "template 2")
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate2.aspx?print=1','_blank')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../2/admtemplate3.aspx?print=1','_blank')", true);
        }
    }
    protected void drpBranchss_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetAmount(drpClassss, drpBranchss, txtConcession);
        drpBranchss.Focus();
    }

    private void LoadBranch()

    {
        _sql = "Select BranchName,Id from BranchMaster where Classid='" + drpClassss.SelectedValue + "' and SessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(_sql, drpBranchss, "BranchName", "Id");
        drpBranchss.Items.Insert(0, new ListItem("<-- Select-->", "<-- Select-->"));
        drpClassss.Focus();

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

        _sql = "Select TOP (1) FirstName,MiddleName,LastName,FatherName,FamilyContactNo,Gender,SessionName, FatherEmail, classid, BranchId, medium, dob from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where SrNo ='" + studentId + "'";
        //drpSession.SelectedValue = _oo.ReturnTag(_sql, "SessionName"); ;
        txtStudentName.Text = _oo.ReturnTag(_sql, "FirstName");
        txtStudentName0.Text = _oo.ReturnTag(_sql, "MiddleName");
        txtStudentName1.Text = _oo.ReturnTag(_sql, "LastName");
        txtFatherName.Text = _oo.ReturnTag(_sql, "FatherName");
        txtContactNo.Text = _oo.ReturnTag(_sql, "FamilyContactNo");
        txtEmail.Text = _oo.ReturnTag(_sql, "FatherEmail");
        txtDob.Text = Convert.ToDateTime(_oo.ReturnTag(_sql, "dob")).ToString("dd-MMM-yyyy");
        string branchid = _oo.ReturnTag(_sql, "BranchId");

        drpSex.SelectedIndex = _oo.ReturnTag(_sql, "Gender").ToUpper() == "FEMALE" ? 2 : _oo.ReturnTag(_sql, "Gender").ToUpper() == "MALE" ? 1 : 3;
        //string _sql2 = "select ClassName,Id from ClassMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + " order by CIDORDER";
        //_oo.FillDropDown_withValue(_sql2, drpClassss, "ClassName", "Id");
        //drpClassss.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        //drpClassss.SelectedValue= _oo.ReturnTag(_sql, "classid");
        //string _sql3 = "select medium from MediumMaster where sessionName='" + drpSession.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        //_oo.FillDropDown_withValue(_sql3, ddlMedium, "medium", "medium");
        //ddlMedium.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
        //ddlMedium.SelectedValue = _oo.ReturnTag(_sql, "medium");
        //LoadBranch();
        //drpBranchss.SelectedValue =branchid; 
        //GetAmount(drpClassss, drpBranchss, txtConcession);
    }

    protected void drpAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpAdmissionType.SelectedIndex == 1)
        {
            txtStudentName.Enabled = false;
            txtStudentName0.Enabled = false;
            txtStudentName1.Enabled = false;
            txtFatherName.Enabled = false;
            txtContactNo.Enabled = false;
            txtEmail.Enabled = false;
            drpSex.Enabled = false;
            txtDob.Enabled = false;
        }
        else
        {
            txtStudentName.Enabled = true;
            txtStudentName0.Enabled = true;
            txtStudentName1.Enabled = true;
            txtFatherName.Enabled = true;
            txtContactNo.Enabled = true;
            txtEmail.Enabled = true;
            drpSex.Enabled = true;
            txtDob.Enabled = true;
        }
        txtSrNo.Focus();
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    protected void LinkButtons_Click(object sender, EventArgs e)
    {

    }






}