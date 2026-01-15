using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

public partial class SMSTemplateMaster : System.Web.UI.Page
{
    readonly Campus _oo = new Campus();

    private SqlConnection _con;

    //readonly SMSDummyText smsDummyText = new SMSDummyText();
    //readonly List<SMSDummyText> smsDummyTextList = new List<SMSDummyText>();
    //smsDummyTextList

    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!Page.IsPostBack)
        {
            SetInitialRow();
            LoadGrid();


            //string sql1 = "select ParameterName from SMSTemplatePapameres where PageName='AdminSignup' and TemplateFor='Default' and BranchCode=" + Session["BranchCode"] + "";
            //var dt = _oo.Fetchdata(sql1);
            //string sql2 = "select Template from SMSTemplate where PageName='AdminSignup' and TemplateFor='Default' and BranchCode=" + Session["BranchCode"] + "";
            //string Template = _oo.ReturnTag(sql2, "Template");
            //string sql3 = "select LoginName, pass from LoginTab where LoginName='axon' and BranchId=" + Session["BranchCode"] + "";


            //string finalString = "";
            //string[] TemplateStrLen = Template.Split(new string[] { "{{{}}}" }, StringSplitOptions.None);

            //for (int i = 0; i < dt.Rows.Count+1; i++)
            //{
            //    if ((dt.Rows.Count)==i)
            //    {
            //        finalString = finalString + TemplateStrLen[i];
            //    }
            //    else
            //    {
            //        string ParameterName = dt.Rows[i]["ParameterName"].ToString();
            //        finalString = finalString + TemplateStrLen[i] + " " + (ParameterName == "Username" ? _oo.ReturnTag(sql3, "LoginName") : _oo.ReturnTag(sql3, "pass"));
            //    }

            //}
        }


    }

    public void SetDummyMsg()
    {
        string templateFor = (ddlTemplateFor.SelectedValue.ToString() == "<--Select-->" || ddlTemplateFor.SelectedValue.ToString() == "") ? "Default" : ddlTemplateFor.SelectedValue.ToString();
        var xml = XDocument.Load(Server.MapPath("~/admin/SMStemplateText.xml"));
        // Query the data and write out a subset of contacts
        var SMSTemplate = from c in xml.Root.Descendants("data")
                          where c.Element("Title").Value == ddlPageName.SelectedValue.ToString()
                          && c.Element("TemplateFor").Value == templateFor
                          select c.Element("SMSTemplate").Value;

        if (SMSTemplate != null && SMSTemplate.Count() > 0)
        {
            lblDummyMsg.Text = SMSTemplate.FirstOrDefault();
        }
        else
        {
            lblDummyMsg.Text = "";
        }

    }
    protected void ddlPageName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTemplateFor.Items.Clear();
        if (ddlPageName.SelectedValue == "")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else if (ddlPageName.SelectedValue == "StudentRegistration")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Student's credentials", "StudentsCredentials"));
            ddlTemplateFor.Items.Insert(2, new ListItem("Guardian's credentials", "GuardiansCredentials"));
        }
        else if (ddlPageName.SelectedValue == "StudentDailyAttendanceAuto")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Present", "Present"));
            ddlTemplateFor.Items.Insert(2, new ListItem("Absent", "Absent"));
            ddlTemplateFor.Items.Insert(3, new ListItem("Late", "Late"));
        }
        else if (ddlPageName.SelectedValue == "StudentDailyAttendanceManual")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Present", "Present"));
            ddlTemplateFor.Items.Insert(2, new ListItem("Absent", "Absent"));
            ddlTemplateFor.Items.Insert(3, new ListItem("Late", "Late"));
        }
        else if (ddlPageName.SelectedValue == "TransactionClearance")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("On cheque Bounce", "OnChequeBounce"));
        }
        else if (ddlPageName.SelectedValue == "StaffAttendanceAuto")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Present", "Present"));
            ddlTemplateFor.Items.Insert(2, new ListItem("Absent", "Absent"));
            ddlTemplateFor.Items.Insert(3, new ListItem("Late", "Late"));
        }
        else if (ddlPageName.SelectedValue == "StaffAttendanceManual")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Present", "Present"));
            ddlTemplateFor.Items.Insert(2, new ListItem("Absent", "Absent"));
            ddlTemplateFor.Items.Insert(3, new ListItem("Late", "Late"));
        }
        else if (ddlPageName.SelectedValue == "AlumniPortal")
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Signup OTP", "SignupOTP"));
            ddlTemplateFor.Items.Insert(2, new ListItem("Forgot Password", "ForgotPassword"));
            ddlTemplateFor.Items.Insert(3, new ListItem("Approval SMS", "ApprovalSMS"));
            ddlTemplateFor.Items.Insert(4, new ListItem("Rejection SMS", "RejectionSMS"));
        }
        else
        {
            ddlTemplateFor.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTemplateFor.Items.Insert(1, new ListItem("Default", "Default"));
        }
        LoadGrid();
        SetDummyMsg();
    }
    protected void ddlTemplateFor_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTemplateFor.SelectedValue == "")
        {
            SetInitialRow();
            return;
        }

        DropDownList ddlParameters = (DropDownList)Gridview1.Rows[0].FindControl("ddlParameters");
        ddlParameters.Items.Clear();
        if (ddlPageName.SelectedValue == "AdminSignup" || ddlPageName.SelectedValue == "ForgotPassword")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));

            ddlParameters.Items.Insert(1, new ListItem("Name", "Name"));
            ddlParameters.Items.Insert(2, new ListItem("Username", "Username"));
            ddlParameters.Items.Insert(3, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "StudentRegistration" && (ddlTemplateFor.SelectedValue == "StudentsCredentials" || ddlTemplateFor.SelectedValue == "GuardiansCredentials"))
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentName"));
            ddlParameters.Items.Insert(2, new ListItem("Username", "Username"));
            ddlParameters.Items.Insert(3, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "StudentLoginCredentials")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentName"));
            ddlParameters.Items.Insert(2, new ListItem("Username", "Username"));
            ddlParameters.Items.Insert(3, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "GuardianLoginCredentials")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentName"));
            ddlParameters.Items.Insert(2, new ListItem("Username", "Username"));
            ddlParameters.Items.Insert(3, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "StudentDailyAttendanceAuto")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentsName"));
            ddlParameters.Items.Insert(2, new ListItem("Date", "Date"));
            ddlParameters.Items.Insert(3, new ListItem("Date with time", "Datewithtime"));
        }
        else if (ddlPageName.SelectedValue == "StudentDailyAttendanceManual")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentsName"));
            ddlParameters.Items.Insert(2, new ListItem("Date", "Date"));
        }
        else if (ddlPageName.SelectedValue == "StudentGatePass")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentsName"));
            ddlParameters.Items.Insert(2, new ListItem("Visitor's Name", "VisitorsName"));
        }
        else if (ddlPageName.SelectedValue == "CompositeFee" || ddlPageName.SelectedValue == "TransferCertificateFee"
            || ddlPageName.SelectedValue == "CharacterCertificateFee" || ddlPageName.SelectedValue == "OtherFee"
            || ddlPageName.SelectedValue == "AdditionalFee" || ddlPageName.SelectedValue == "ProductFee"
            || ddlPageName.SelectedValue == "AdmissionFormFee" || ddlPageName.SelectedValue == "LibraryFine")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentName"));
            ddlParameters.Items.Insert(2, new ListItem("Amount", "Amount"));
            ddlParameters.Items.Insert(3, new ListItem("Receipt No.", "ReceiptNo"));
        }
        else if (ddlPageName.SelectedValue == "TransactionClearance" || ddlPageName.SelectedValue == "ReceiptCancellation")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Receipt No.", "ReceiptNo"));
            ddlParameters.Items.Insert(2, new ListItem("Student's Name", "StudentsName"));
            ddlParameters.Items.Insert(3, new ListItem("Amount", "Amount"));
        }
        else if (ddlPageName.SelectedValue == "FeeOverdueReminder")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentsName"));
            ddlParameters.Items.Insert(2, new ListItem("Amount", "Amount"));
        }
        else if (ddlPageName.SelectedValue == "TransportAlert")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Student's Name", "StudentsName"));
            ddlParameters.Items.Insert(2, new ListItem("Location", "Location"));
        }
        else if (ddlPageName.SelectedValue == "AdmissionPortal")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("OTP", "OTP"));
        }
        else if (ddlPageName.SelectedValue == "StaffRegistration")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Staff's Name", "StaffsName"));
            ddlParameters.Items.Insert(2, new ListItem("Username", "Username"));
            ddlParameters.Items.Insert(3, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "StaffAttendanceManual")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Staff's Name", "StaffsName"));
            ddlParameters.Items.Insert(2, new ListItem("Date", "Date"));
        }
        else if (ddlPageName.SelectedValue == "StaffAttendanceAuto")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Staff's Name", "StaffsName"));
            ddlParameters.Items.Insert(2, new ListItem("Date", "Date"));
            ddlParameters.Items.Insert(3, new ListItem("Date with time", "Datewithtime"));
        }
        else if (ddlPageName.SelectedValue == "EmploymentFormAlert")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Candidate's Name", "CandidatesName"));
            ddlParameters.Items.Insert(2, new ListItem("Date with time", "Datewithtime"));
        }
        else if (ddlPageName.SelectedValue == "AlumniPortal" && ddlTemplateFor.SelectedValue == "SignupOTP")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Signup OTP", "SignupOTP"));
        }
        else if (ddlPageName.SelectedValue == "AlumniPortal" && ddlTemplateFor.SelectedValue == "ForgotPassword")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Alumni Name", "AlumniName"));
            ddlParameters.Items.Insert(2, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "AlumniPortal" && ddlTemplateFor.SelectedValue == "ApprovalSMS")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Alumni Name", "AlumniName"));
            ddlParameters.Items.Insert(2, new ListItem("Username", "Username"));
            ddlParameters.Items.Insert(3, new ListItem("Password", "Password"));
        }
        else if (ddlPageName.SelectedValue == "AlumniPortal" && ddlTemplateFor.SelectedValue == "RejectionSMS")
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlParameters.Items.Insert(1, new ListItem("Alumni Name", "AlumniName"));
        }
        else
        {
            ddlParameters.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        ddlParametersHide.Items.Clear();
        for (int i = 0; i < ddlParameters.Items.Count; i++)
        {
            ddlParametersHide.Items.Insert(i, new ListItem(ddlParameters.Items[i].Text, ddlParameters.Items[i].Value));
        }

        SetDummyMsg();
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        DataRow dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        Gridview1.DataSource = dt;
        Gridview1.DataBind();
        if (Gridview1.Rows.Count == 1)
        {
            LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
            ButtonRemove.Visible = false;
        }
        DropDownList ddlParameters = (DropDownList)Gridview1.Rows[0].FindControl("ddlParameters");
        TextBox txtOrderNo = (TextBox)Gridview1.Rows[0].FindControl("txtOrderNo");
        txtOrderNo.Text = "";
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    private void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    DropDownList ddlParameters = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlParameters");
                    TextBox txtOrderNo = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtOrderNo");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Column1"] = ddlParameters.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Column2"] = txtOrderNo.Text;

                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();

                for (int j = 0; j < Gridview1.Rows.Count; j++)
                {
                    DropDownList ddd = (DropDownList)Gridview1.Rows[j].FindControl("ddlParameters");
                    if (ddd.Items.Count == 1)
                    {
                        ddd.Items.Clear();
                        for (int i = 0; i < ddlParametersHide.Items.Count; i++)
                        {
                            ddd.Items.Insert(i, new ListItem(ddlParametersHide.Items[i].Text, ddlParametersHide.Items[i].Value));
                        }

                    }
                }
            }
            if (dtCurrentTable.Rows.Count == 1)
            {
                LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
                ButtonRemove.Visible = false;
            }
            DropDownList ddlParameters1 = (DropDownList)Gridview1.Rows[0].FindControl("ddlParameters");
            if (dtCurrentTable.Rows.Count == (ddlParameters1.Items.Count - 1))
            {
                ButtonAdd.Visible = false;
            }
            else
            {
                ButtonAdd.Visible = true;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks  
        SetPreviousData();
    }
    protected void ButtonRemove_Click(object sender, EventArgs e)
    {
        var lnk = (LinkButton)sender;
        var lblid = (Label)lnk.NamingContainer.FindControl("txtIndex");
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            SetPreviousData();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            dtCurrentTable.Rows.Remove(dtCurrentTable.Rows[int.Parse(lblid.Text)]);
            int idx = 0;
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {

                DropDownList ddlParameters = (DropDownList)Gridview1.Rows[i].FindControl("ddlParameters");
                TextBox txtOrderNo = (TextBox)Gridview1.Rows[i].FindControl("txtOrderNo");
                if (int.Parse(lblid.Text) != i)
                {
                    var drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[idx]["Column1"] = ddlParameters.SelectedValue;
                    dtCurrentTable.Rows[idx]["Column2"] = txtOrderNo.Text;
                    rowIndex++;
                    idx++;
                }

            }

            Gridview1.DataSource = dtCurrentTable;
            Gridview1.DataBind();
            ViewState["CurrentTable"] = dtCurrentTable;
            for (int j = 0; j < Gridview1.Rows.Count; j++)
            {
                DropDownList ddd = (DropDownList)Gridview1.Rows[j].FindControl("ddlParameters");
                TextBox txtOrderNo = (TextBox)Gridview1.Rows[j].FindControl("txtOrderNo");
                if (ddd.Items.Count == 1)
                {
                    ddd.Items.Clear();
                    for (int i = 0; i < ddlParametersHide.Items.Count; i++)
                    {
                        ddd.Items.Insert(i, new ListItem(ddlParametersHide.Items[i].Text, ddlParametersHide.Items[i].Value));
                    }

                }
                ddd.SelectedValue = dtCurrentTable.Rows[j][1].ToString();
                txtOrderNo.Text = dtCurrentTable.Rows[j][2].ToString();
            }
            if (dtCurrentTable.Rows.Count == 1)
            {
                LinkButton ButtonRemove = (LinkButton)Gridview1.Rows[0].FindControl("ButtonRemove");
                ButtonRemove.Visible = false;
            }
            DropDownList ddlParameters1 = (DropDownList)Gridview1.Rows[0].FindControl("ddlParameters");
            if (dtCurrentTable.Rows.Count == (ddlParameters1.Items.Count - 1))
            {
                ButtonAdd.Visible = false;
            }
            else
            {
                ButtonAdd.Visible = true;
            }
        }

    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlParameters = (DropDownList)Gridview1.Rows[rowIndex].FindControl("ddlParameters");
                    TextBox txtOrderNo = (TextBox)Gridview1.Rows[rowIndex].FindControl("txtOrderNo");
                    ddlParameters.SelectedValue = dt.Rows[i]["Column1"].ToString();
                    txtOrderNo.Text = dt.Rows[i]["Column2"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    public void LoadGrid()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            if (ddlPageName.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@PageName", ddlPageName.SelectedValue));
            }
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            param.Add(new SqlParameter("@Action", "select"));
            DataSet dss = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SMSTemplateProc", param);
            Grd.DataSource = dss.Tables[0];
            Grd.DataBind();
            //if (Grd.Rows.Count > 0)
            //{
            //    for (int j = 0; j < Grd.Rows.Count; j++)
            //    {
            //        string paramss = "";
            //        int cnt = dss.Tables[1].Rows.Count;
            //        for (int i = 0; i < cnt; i++)
            //        {
            //            paramss = paramss + (i > 0 ? ", " : "") + dss.Tables[1].Rows[i]["ParameterName"].ToString();
            //        }
            //        Label Label3ParameterName = (Label)Grd.Rows[j].FindControl("Label3ParameterName");
            //        Label3ParameterName.Text = paramss;
            //    }
            //}
        }
        catch
        {

        }

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label Label2PageName = (Label)chk.NamingContainer.FindControl("Label2PageName");
        Label Label7TemplateFor = (Label)chk.NamingContainer.FindControl("Label7TemplateFor");
        Label Label7SendFor = (Label)chk.NamingContainer.FindControl("Label7SendFor");

        lblPageName.Text = Label2PageName.Text;
        lblTemplateFor.Text = Label7TemplateFor.Text;
        lblSendFor.Text = Label7SendFor.Text;

        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "SMSTemplateProc",
            CommandType = CommandType.StoredProcedure,
            Connection = _con
        };

        cmd.Parameters.AddWithValue("@PageName", lblPageName.Text);
        cmd.Parameters.AddWithValue("@TemplateFor", lblTemplateFor.Text);
        cmd.Parameters.AddWithValue("@SendFor", lblSendFor.Text);
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        cmd.Parameters.AddWithValue("@Action", "delete");
        try
        {
            _con.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            _con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            LoadGrid();
        }
        catch (Exception) { }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string[] TemplateStrLen = txtSMSTemplate.Text.ToString().Split(' ').Where((obj) => obj.EndsWith(@"{{{}}}")).ToArray(); ;
        if (Gridview1.Rows.Count != TemplateStrLen.Length)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Parameters for template not matched with SMS template placeholder(s)!", "A");
            return;
        }
        SqlCommand cmd = new SqlCommand
        {
            CommandText = "SMSTemplateProc",
            CommandType = CommandType.StoredProcedure,
            Connection = _con
        };

        cmd.Parameters.AddWithValue("@PageName", ddlPageName.SelectedValue);
        cmd.Parameters.AddWithValue("@TemplateFor", ddlTemplateFor.SelectedValue);
        cmd.Parameters.AddWithValue("@SendFor", drpSendFor.SelectedValue);
        cmd.Parameters.AddWithValue("@Template", txtSMSTemplate.Text.Trim());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
        cmd.Parameters.AddWithValue("@Action", "Template");

        _con.Open();
        int x = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        _con.Close();
        if (x > 0)
        {
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                DropDownList ddlParameters = (DropDownList)Gridview1.Rows[i].FindControl("ddlParameters");
                TextBox txtOrderNo = (TextBox)Gridview1.Rows[i].FindControl("txtOrderNo");
                SqlCommand cmd1 = new SqlCommand
                {
                    CommandText = "SMSTemplateProc",
                    CommandType = CommandType.StoredProcedure,
                    Connection = _con
                };
                cmd1.Parameters.AddWithValue("@PageName", ddlPageName.SelectedValue);
                cmd1.Parameters.AddWithValue("@TemplateFor", ddlTemplateFor.SelectedValue);
                cmd1.Parameters.AddWithValue("@SendFor", drpSendFor.SelectedValue);
                cmd1.Parameters.AddWithValue("@ParameterName", ddlParameters.SelectedValue);
                cmd1.Parameters.AddWithValue("@OrderNo", txtOrderNo.Text.Trim());
                cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                cmd1.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
                cmd1.Parameters.AddWithValue("@Action", "Parameers");

                _con.Open();
                cmd1.ExecuteNonQuery();
                cmd1.Parameters.Clear();
                _con.Close();

            }
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            LoadGrid();
        }
    }


}

public class SMSDummyText
{
    public string value { get; set; }
}

public class Datum
{
    [XmlElement("Srno")]
    public int Srno { get; set; }
    [XmlElement("Title")]
    public string Title { get; set; }
    [XmlElement("TemplateFor")]
    public string TemplateFor { get; set; }
    [XmlElement("SMSTemplate")]
    public string SMSTemplate { get; set; }
}

public class Root
{
    [XmlArray("data")]
    [XmlArrayItem("Datum")]
    public List<Datum> data { get; set; }
}