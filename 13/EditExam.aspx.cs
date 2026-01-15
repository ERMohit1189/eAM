using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class EditExam : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public EditExam()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString().ToLower() != "admin")
        {
            Response.Redirect("~/default.aspx");
        }
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);
        if (!IsPostBack)
        {
            GetTime();
            loadClass();
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));
            //loadTerm();
        }
    }
    public void GetTime()
    {
        BLL.FillHourInDropDownlist(ddlShowResultFromHH0);
        ddlShowResultFromHH0.SelectedIndex = 1;
        BLL.FillMinuteInDropDownlist(ddlShowResultFromMM0);
        ddlShowResultFromMM0.SelectedIndex = 2;
        BLL.FillHourInDropDownlist(ddlShowResultToHH0);
        ddlShowResultToHH0.SelectedIndex = 12;
        BLL.FillMinuteInDropDownlist(ddlShowResultToMM0);
        ddlShowResultToMM0.SelectedIndex = 60;


        BLL.FillHourInDropDownlist(ddlFromHour0);
        ddlFromHour0.SelectedIndex = 1;
        BLL.FillMinuteInDropDownlist(ddlFromMinute0);
        ddlFromMinute0.SelectedIndex = 2;
        BLL.FillHourInDropDownlist(ddlToHour0);
        ddlToHour0.SelectedIndex = 12;
        BLL.FillMinuteInDropDownlist(ddlToMinute0);
        ddlToMinute0.SelectedIndex = 60;

        

    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
   
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubjectLoad();
    }
    protected void SubjectLoad()
    {
        _sql = "select Id, Subject from OT_SubjectMaster where classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Paper from OT_PaperMaster where classId=" + ddlClass.SelectedValue + " and subjectid=" + ddlSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {

        _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and TermId="+ddlTerm.SelectedValue+"";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void LoadData()
    {

        _sql = "select id, classId, SubjectId, PaperId, TermId, ExamName, FileType, FilePath, Duration,  ExamStart, ExamEnd, ResultShow, ResultHide, ResultStting, TestSetting, Status, SessionName, Branchcode, Loginname, Recordeddate from OT_ExamMaster where  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
        _sql = _sql + "  and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and ";
        _sql = _sql + " paperid=" + ddlPaper.SelectedValue + " and id=" + ddlExam.SelectedValue + " order by id asc";
        if (_oo.Fetchdata(_sql).Rows.Count > 0)
        {
            divUpdate.Visible = true;

            string Status = "", TestSetting = "", ResultStting = "";
            TestSetting = _oo.ReturnTag(_sql, "TestSetting");
            ResultStting = _oo.ReturnTag(_sql, "ResultStting");
            string ExamStart = _oo.ReturnTag(_sql, "ExamStart");
            string ExamEnd = _oo.ReturnTag(_sql, "ExamEnd");
            Status = _oo.ReturnTag(_sql, "Status");
            lblID.Text= _oo.ReturnTag(_sql, "id");
            chkAutoAttempt0.Checked = (TestSetting == "False" ? false : true);
            chkInstantResult0.Checked = (ResultStting == "False" ? false : true);

            txtDateFromPanel.Text = DateTime.Parse(ExamStart).ToString("dd-MMM-yyyy");
            ddlFromHour0.SelectedValue = DateTime.Parse(ExamStart).ToString("hh");
            ddlFromMinute0.SelectedValue = DateTime.Parse(ExamStart).ToString("mm");
            ddlFromType0.SelectedValue = DateTime.Parse(ExamStart).ToString("tt");

            txtDateToPanel.Text = DateTime.Parse(ExamEnd).ToString("dd-MMM-yyyy");
            ddlToHour0.SelectedValue = DateTime.Parse(ExamEnd).ToString("hh");
            ddlToMinute0.SelectedValue = DateTime.Parse(ExamEnd).ToString("mm");
            ddlToType0.SelectedValue = DateTime.Parse(ExamEnd).ToString("tt");

            string ResultShow = _oo.ReturnTag(_sql, "ResultShow");
            string ResultHide = _oo.ReturnTag(_sql, "ResultHide");
            txtShowResultFromDate0.Text = DateTime.Parse(ResultShow).ToString("dd-MMM-yyyy");
            ddlShowResultFromHH0.SelectedValue = DateTime.Parse(ResultShow).ToString("hh");
            ddlShowResultFromMM0.SelectedValue = DateTime.Parse(ResultShow).ToString("mm");
            ddlShowResultFromTT0.SelectedValue = DateTime.Parse(ResultShow).ToString("tt");

            txtShowResultToDate0.Text = DateTime.Parse(ResultHide).ToString("dd-MMM-yyyy");
            ddlShowResultToHH0.SelectedValue = DateTime.Parse(ResultHide).ToString("hh");
            ddlShowResultToMM0.SelectedValue = DateTime.Parse(ResultHide).ToString("mm");
            ddlShowResultToTT0.SelectedValue = DateTime.Parse(ResultHide).ToString("tt");

            ddlStatusPanel.SelectedValue = (Status.ToLower() == "true" ? "1" : "0");
            string FilePath = _oo.ReturnTag(_sql, "FilePath");
            string FileType = _oo.ReturnTag(_sql, "FileType");
            if (FileType == "Manual")
            {
                divFileUpload.Visible = false;
                divGoogleDrivePath.Visible = false;
            }
            if (FileType == "FileUpload")
            {
                divFileUpload.Visible = true;
                divGoogleDrivePath.Visible = false;
            }
            if (FileType == "GoogleDrivePath")
            {
                txtGoogleDrivePath.Text = FilePath;
                divFileUpload.Visible = false;
                divGoogleDrivePath.Visible = true;

            }
        }
        else
        {
            divUpdate.Visible = true;
        }
    }
    
    
    protected void Button3_Click(object sender, EventArgs e)
    {
        
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
           
            cmd.Parameters.AddWithValue("@ExamStart", (txtDateFromPanel.Text + " " + ddlFromHour0.SelectedValue + ":" + ddlFromMinute0.SelectedValue + " " + ddlFromType0.SelectedValue));
            cmd.Parameters.AddWithValue("@ExamEnd", (txtDateToPanel.Text + " " + ddlToHour0.SelectedValue + ":" + ddlToMinute0.SelectedValue + " " + ddlToType0.SelectedValue));
            cmd.Parameters.AddWithValue("@ResultShow", (txtShowResultFromDate0.Text + " " + ddlShowResultFromHH0.SelectedValue + ":" + ddlShowResultFromMM0.SelectedValue + " " + ddlShowResultFromTT0.SelectedValue));
            cmd.Parameters.AddWithValue("@ResultHide", (txtShowResultToDate0.Text + " " + ddlShowResultToHH0.SelectedValue + ":" + ddlShowResultToMM0.SelectedValue + " " + ddlShowResultToTT0.SelectedValue));
            cmd.Parameters.AddWithValue("@ResultStting", (chkInstantResult0.Checked ? "1" : "0"));
            cmd.Parameters.AddWithValue("@TestSetting", (chkAutoAttempt0.Checked ? "1" : "0"));
            cmd.Parameters.AddWithValue("@Status", ddlStatusPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@FileType", ddlFileType.SelectedValue);
            string filePath = "";
            string fileName = "";
            if (ddlFileType.SelectedIndex == 2)
            {
                string base64std = hdnFile.Value;
                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/ExamFile/";
                    fileName = ddlSubject.SelectedValue + ddlPaper.SelectedValue + DateTime.Now.ToString("ddmmmyyyyhhmmss") + "." + hdnExtention.Value;

                    using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
            }
            if (ddlFileType.SelectedIndex == 1)
            {
                cmd.Parameters.AddWithValue("@FilePath", txtGoogleDrivePath.Text.Trim());
            }
            if (ddlFileType.SelectedIndex == 2)
            {
                cmd.Parameters.AddWithValue("@FilePath", filePath + fileName);
            }
            cmd.Parameters.AddWithValue("@Action", "updateDates");

            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _oo.msgbox(Page, msgbox2, "Updated successfully.", "S");
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    protected void ddlFileType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFileType.SelectedIndex == 0)
        {
            divFileUpload.Visible = false;
            divGoogleDrivePath.Visible = false;
        }
        if (ddlFileType.SelectedIndex == 1)
        {
            divFileUpload.Visible = false;
            divGoogleDrivePath.Visible = true;
        }
        if (ddlFileType.SelectedIndex == 2)
        {
            divFileUpload.Visible = true;
            divGoogleDrivePath.Visible = false;
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
    }
    
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
    
    

    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}