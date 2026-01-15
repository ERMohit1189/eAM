using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class ExamMaster : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public ExamMaster()
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

            loadTerm();
        }
    }
    public void GetTime()
    {
        BLL.FillHourInDropDownlist(ddlFromHour);
        ddlFromHour.SelectedIndex = 1;
        BLL.FillMinuteInDropDownlist(ddlFromMinute);
        ddlFromMinute.SelectedIndex = 2;
        BLL.FillHourInDropDownlist(ddlToHour);
        ddlToHour.SelectedIndex = 12;
        BLL.FillMinuteInDropDownlist(ddlToMinute);
        ddlToMinute.SelectedIndex = 60;

        BLL.FillHourInDropDownlist(ddlFromHour0);
        BLL.FillMinuteInDropDownlist(ddlFromMinute0);
        BLL.FillHourInDropDownlist(ddlToHour0);
        BLL.FillMinuteInDropDownlist(ddlToMinute0);


        BLL.FillHourInDropDownlist(ddlShowResultFromHH);
        ddlShowResultFromHH.SelectedIndex = 1;
        BLL.FillMinuteInDropDownlist(ddlShowResultFromMM);
        ddlShowResultFromMM.SelectedIndex = 2;
        BLL.FillHourInDropDownlist(ddlShowResultToHH);
        ddlShowResultToHH.SelectedIndex = 1;
        BLL.FillMinuteInDropDownlist(ddlShowResultToMM);
        ddlShowResultToMM.SelectedIndex = 3;

        BLL.FillHourInDropDownlist(ddlShowResultFromHH0);
        BLL.FillMinuteInDropDownlist(ddlShowResultFromMM0);
        BLL.FillHourInDropDownlist(ddlShowResultToHH0);
        BLL.FillMinuteInDropDownlist(ddlShowResultToMM0);
        txtShowResultFromDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtShowResultToDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");

    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        _oo.FillDropDown_withValue(_sql, ddlClassPanel, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadTerm()
    {
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        _oo.FillDropDown_withValue(_sql, ddlTermPanel, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubjectLoad();
    }
    protected void SubjectLoad()
    {
        _sql = "select Id, Subject from OT_SubjectMaster where classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        _oo.FillDropDown_withValue(_sql, ddlSubjectPanel, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Paper from OT_PaperMaster where classId=" + ddlClass.SelectedValue + " and subjectid=" + ddlSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
        _oo.FillDropDown_withValue(_sql, ddlPaperPanel, "Paper", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }

    private void LoadData()
    {
        _sql = "select (select top(1) ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=SM.classId) classname, EM.id, SM.Subject, PM.Paper, TermName, EM.ExamName, EM.Duration, FileType, EM.FilePath, format(ExamStart, 'dd-MMM-yyyy hh:mm tt') +' To '+ format(ExamEnd, 'dd-MMM-yyyy hh:mm tt') Examdate, format(ResultShow, 'dd-MMM-yyyy hh:mm tt') +' To '+ format(ResultHide, 'dd-MMM-yyyy hh:mm tt') Resultdate, ResultStting, TestSetting, Status,  EM.SessionName, EM.Loginname, EM.Recordeddate   from OT_ExamMaster EM";
        _sql = _sql + " inner join OT_SubjectMaster SM on SM.id = EM.SubjectId  and SM.SessionName='" + Session["SessionName"] + "' and SM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_TermMaster TM on TM.id = EM.Termid  and TM.SessionName='" + Session["SessionName"] + "' and TM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_PaperMaster PM on PM.id = EM.PaperId  and PM.SessionName='" + Session["SessionName"] + "' and PM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " where EM.SubjectId =case when '" + ddlSubject.SelectedValue+ "' = '' then 0 else '" + ddlSubject.SelectedValue + "' end";
        _sql = _sql + " and EM.PaperId =case when '" + ddlPaper.SelectedValue+ "' = '' then EM.PaperId else '" + ddlPaper.SelectedValue + "' end";
        _sql = _sql + "  and em.SessionName='" + Session["SessionName"] + "' and em.BranchCode=" + Session["BranchCode"] + " order by id asc";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        string FileType = _oo.ReturnTag(_sql, "FileType");
        if (FileType=="Manual")
        {
            Grd.HeaderRow.Cells[7].Visible = false;
        }
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            if (FileType == "Manual")
            {
                Grd.Rows[i].Cells[7].Visible = false;
            }
            Label ExamID = (Label)Grd.Rows[i].FindControl("Label37");
            LinkButton Edit = (LinkButton)Grd.Rows[i].FindControl("LinkButton2");
            LinkButton delete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            _sql = "select count(*) cnt from OT_ExamAllotment where ExamID="+ ExamID.Text + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "cnt") != "0")
            {
                Edit.Enabled = false;
                delete.Enabled = false;
                Edit.Text = "<i class='fa fa-lock'></i>";
                delete.Text = "<i class='fa fa-lock'></i>";
            }
            else
            {
                Edit.Enabled = true;
                delete.Enabled = true;
            }
        }
    }
    
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        var ss = lblId.Text;
        lblvalue.Text = ss;
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        _sql = "select classId, SubjectId, PaperId, TermId, ExamName, FileType, FilePath, Duration,  ExamStart, ExamEnd, ResultShow, ResultHide, ResultStting, TestSetting, Status, SessionName, Branchcode, Loginname, Recordeddate, DoNotShow from OT_ExamMaster where  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id=" + ss;

        string classId = "", DoNotShow = "", SubjectId = "", PaperId = "", ExamName = "", Duration = "", FilePath = "", TermId = "", Status = "", TestSetting = "", ResultStting = "", FileType = "";
        DoNotShow=_oo.ReturnTag(_sql, "DoNotShow");
        classId = _oo.ReturnTag(_sql, "classId");
        SubjectId = _oo.ReturnTag(_sql, "SubjectId");
        PaperId = _oo.ReturnTag(_sql, "PaperId");
        TermId = _oo.ReturnTag(_sql, "TermId");
        ExamName = _oo.ReturnTag(_sql, "ExamName");
        Duration = _oo.ReturnTag(_sql, "Duration");
        FilePath = _oo.ReturnTag(_sql, "FilePath");
        Status = _oo.ReturnTag(_sql, "Status");
        TestSetting = _oo.ReturnTag(_sql, "TestSetting");
        ResultStting = _oo.ReturnTag(_sql, "ResultStting");
        FileType = _oo.ReturnTag(_sql, "FileType");

        string ExamStart = _oo.ReturnTag(_sql, "ExamStart");
        string ExamEnd = _oo.ReturnTag(_sql, "ExamEnd");

        chkAutoAttempt0.Checked = (TestSetting == "False" ? false : true);
        chkInstantResult0.Checked= (ResultStting == "False" ? false : true);
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
        if (FileType == "Manual")
        {
            divdivFileUploadPanel.Visible = false;
            divGoogleDrivePathPanel.Visible = false;
        }
        if (FileType == "FileUpload")
        {
            divdivFileUploadPanel.Visible = true;
            divGoogleDrivePathPanel.Visible = false;
        }
        if (FileType == "GoogleDrivePath")
        {
            txtGoogleDrivePathPanel.Text = FilePath;
            divdivFileUploadPanel.Visible = false;
            divGoogleDrivePathPanel.Visible = true;
            
        }

        ddlClassPanel.SelectedValue = classId;

        _sql = "select Id, Subject from OT_SubjectMaster where classId=" + classId + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubjectPanel, "Subject", "Id");
        ddlSubjectPanel.SelectedValue = SubjectId;

        _sql = "select Id, Paper from OT_PaperMaster where subjectId="+ SubjectId + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaperPanel, "Paper", "Id");
        ddlPaperPanel.SelectedValue = PaperId;
        ddlTermPanel.SelectedValue = TermId;
        txtExamNamePanel.Text = ExamName;
        hdnSigName.Value = ExamName;
        ddlFileTypePanel.SelectedValue = (FileType==""?"Manual": FileType);
        hdnFilePanel.Value ="";
        txtDurationPanel.Text = Duration;
        ddlStatusPanel.SelectedValue = (Status.ToLower()=="true"?"1":"0");
        if (DoNotShow=="True")
        {
            ddlDontshowPanel.Checked = true;
            TrResultFrom.Visible = false;
            TrResultTo.Visible = false;
            TrResSetrting.Visible = false;
            txtShowResultFromDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtShowResultToDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            ddlShowResultToTT0.SelectedIndex = 0;
            BLL.FillHourInDropDownlist(ddlShowResultToHH0);
            ddlShowResultToHH0.SelectedIndex = 1;
            BLL.FillMinuteInDropDownlist(ddlShowResultToMM0);
            ddlShowResultToMM0.SelectedIndex = 3;
        }
        else
        {
            ddlDontshowPanel.Checked = false;
            txtShowResultFromDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtShowResultToDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            TrResultFrom.Visible = true;
            TrResultTo.Visible = true;
            TrResSetrting.Visible = true;
            BLL.FillHourInDropDownlist(ddlShowResultToHH0);
            ddlShowResultToHH0.SelectedIndex = 12;
            BLL.FillMinuteInDropDownlist(ddlShowResultToMM0);
            ddlShowResultToMM0.SelectedIndex = 60;
            ddlShowResultToTT0.SelectedIndex = 1;
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "select t1.ExamName from (select ExamName from OT_ExamMaster where ExamName<>'" + hdnSigName.Value + "' and  SubjectId='" + ddlSubjectPanel.SelectedValue + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId='" + ddlPaperPanel.SelectedValue + "' and classid=" + ddlClassPanel.SelectedValue + " and TermId=" + ddlTermPanel.SelectedValue + ")t1 where t1.ExamName='" + txtExamNamePanel.Text.Trim() + "'";
        if (_oo.Duplicate(_sql))
        {
            _oo.msgbox(Page, msgbox2, "Duplicate Test Name!", "A");
            Panel1_ModalPopupExtender.Show();
            return;
        }
        if (ddlFileTypePanel.SelectedIndex == 1 && txtGoogleDrivePathPanel.Text.Trim() == "")
        {
            _oo.msgbox(Page, msgbox2, "Plaese enter Google Drive Path!", "A");
            Panel1_ModalPopupExtender.Show();
            return;
        }
        if (ddlFileTypePanel.SelectedIndex == 2 && hdnFilePanel.Value == "")
        {
            _oo.msgbox(Page, msgbox2, "Plaese enter Google Drive Path!", "A");
            Panel1_ModalPopupExtender.Show();
            return;
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@ClassId", ddlClassPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@SubjectId", ddlSubjectPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@PaperId", ddlPaperPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@TermId", ddlTermPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@ExamName", txtExamNamePanel.Text);
            string filePath = "";
            string fileName = "";
            if (ddlFileTypePanel.SelectedIndex != 0 && ddlFileTypePanel.SelectedIndex != 1)
            {
                string base64std = hdnFilePanel.Value;
                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/ExamFile/";
                    fileName = ddlSubjectPanel.SelectedValue + ddlPaperPanel.SelectedValue + DateTime.Now.ToString("ddmmmyyyyhhmmss") + "." + hdnExtentionPanel.Value;

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
            if (ddlFileTypePanel.SelectedIndex == 0)
            {
                cmd.Parameters.AddWithValue("@FilePath", "");
            }
            if (ddlFileTypePanel.SelectedIndex == 1)
            {
                if (txtGoogleDrivePathPanel.Text.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@FilePath", txtGoogleDrivePathPanel.Text.Trim());
                }
            }
            if (ddlFileTypePanel.SelectedIndex == 2)
            {
                if (hdnFilePanel.Value != "")
                {
                    cmd.Parameters.AddWithValue("@FilePath", filePath + fileName);
                }
            }
            cmd.Parameters.AddWithValue("@FileType", ddlFileTypePanel.SelectedValue);
            _sql = "select format(getdate(), 'dd-MMM-yyyy hh:mm tt') curDate";
            string curDate = _oo.ReturnTag(_sql, "curDate");
            cmd.Parameters.AddWithValue("@Duration", txtDurationPanel.Text);
            cmd.Parameters.AddWithValue("@ExamStart", (txtDateFromPanel.Text + " " + ddlFromHour0.SelectedValue + ":" + ddlFromMinute0.SelectedValue + " " + ddlFromType0.SelectedValue));
            cmd.Parameters.AddWithValue("@ExamEnd", (txtDateToPanel.Text + " " + ddlToHour0.SelectedValue + ":" + ddlToMinute0.SelectedValue + " " + ddlToType0.SelectedValue));
            cmd.Parameters.AddWithValue("@ResultShow", (ddlDontshowPanel.Checked?curDate:txtShowResultFromDate0.Text + " " + ddlShowResultFromHH0.SelectedValue + ":" + ddlShowResultFromMM0.SelectedValue + " " + ddlShowResultFromTT0.SelectedValue));
            cmd.Parameters.AddWithValue("@ResultHide", (ddlDontshowPanel.Checked ? curDate : txtShowResultToDate0.Text + " " + ddlShowResultToHH0.SelectedValue + ":" +ddlShowResultToMM0.SelectedValue + " " + ddlShowResultToTT0.SelectedValue));
            cmd.Parameters.AddWithValue("@ResultStting", (chkInstantResult0.Checked ? "1" : "0"));
            cmd.Parameters.AddWithValue("@TestSetting", (chkAutoAttempt0.Checked ? "1" : "0"));
            cmd.Parameters.AddWithValue("@DoNotShow", (ddlDontshowPanel.Checked ? "1" : "0"));
            cmd.Parameters.AddWithValue("@Status", ddlStatusPanel.SelectedValue);
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "update");

            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _oo.msgbox(Page, msgbox, "Updated successfully.", "S");
                LoadData();
                Panel1_ModalPopupExtender.Hide();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        _sql = "Delete from OT_ExamMaster where Id=" + lblvalue.Text+ " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _oo.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        ddlTerm.SelectedIndex = 0;
        txtExamName.Text = "";
    }

    protected void LinkSubmit_Click(object sender, EventArgs e)
    {
        
        _sql = "select ExamName from OT_ExamMaster where SubjectId='" + ddlSubject.SelectedValue + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId='" + ddlPaper.SelectedValue + "' and classid=" + ddlClass.SelectedValue + " and TermId=" + ddlTerm.SelectedValue + "";
        if (_oo.Duplicate(_sql))
        {
            _oo.msgbox(Page, msgbox, "Duplicate Test Name!", "A");
            return;
        }
        if (ddlFileType.SelectedIndex==1 && txtGoogleDrivePath.Text.Trim()=="")
        {
            _oo.msgbox(Page, msgbox, "Plaese enter Google Drive Path!", "A");
            return;
        }
        if (ddlFileType.SelectedIndex == 2 && hdnFile.Value == "")
        {
            _oo.msgbox(Page, msgbox, "Please upload file!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "OT_ExamMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@ClassId", ddlClass.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue);
                cmd.Parameters.AddWithValue("@TermId", ddlTerm.SelectedValue);
                cmd.Parameters.AddWithValue("@ExamName", txtExamName.Text);
                cmd.Parameters.AddWithValue("@FileType", ddlFileType.SelectedValue);
                string filePath = "";
                string fileName = "";
                if (ddlFileType.SelectedIndex != 0 && ddlFileType.SelectedIndex != 1)
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
                _sql = "select format(getdate(), 'dd-MMM-yyyy hh:mm tt') curDate";
                string curDate = _oo.ReturnTag(_sql, "curDate");
                cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
                cmd.Parameters.AddWithValue("@ExamStart", (txtDateFrom.Text + " " + ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromType.SelectedValue));
                cmd.Parameters.AddWithValue("@ExamEnd", (txtDateTo.Text + " " +  ddlToHour.SelectedValue  + ":" + ddlToMinute.SelectedValue + " " + ddlToType.SelectedValue));
                cmd.Parameters.AddWithValue("@ResultShow", (ddlDontshow.Checked? curDate : txtShowResultFromDate.Text + " " + ddlShowResultFromHH.SelectedValue + ":" + ddlShowResultFromMM.SelectedValue + " " + ddlShowResultFromTT.SelectedValue));
                cmd.Parameters.AddWithValue("@ResultHide", (ddlDontshow.Checked ? curDate : txtShowResultToDate.Text + " " + ddlShowResultToHH.SelectedValue + ":" + ddlShowResultToMM.SelectedValue + " " + ddlShowResultToTT.SelectedValue));
                cmd.Parameters.AddWithValue("@ResultStting", (chkInstantResult.Checked ? "1" : "0"));
                cmd.Parameters.AddWithValue("@TestSetting", (chkAutoAttempt.Checked ? "1" : "0"));
                cmd.Parameters.AddWithValue("@DoNotShow", (ddlDontshow.Checked ? "1" : "0"));
                cmd.Parameters.AddWithValue("@Status", "0");
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    _oo.ClearControls(Page);
                    LoadData();
                }
                catch (Exception ex)
                {
                    // ignored
                }
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
        if (ddlFileType.SelectedIndex==2)
        {
            divFileUpload.Visible = true;
            divGoogleDrivePath.Visible = false;
        }
        
    }

    protected void ddlFileTypePanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFileTypePanel.SelectedIndex == 0)
        {
            divdivFileUploadPanel.Visible = false;
            divGoogleDrivePathPanel.Visible = false;
        }
        if (ddlFileTypePanel.SelectedIndex == 1)
        {
            divGoogleDrivePathPanel.Visible = true;
            divdivFileUploadPanel.Visible = false;
        }
        if (ddlFileTypePanel.SelectedIndex == 2)
        {
            divdivFileUploadPanel.Visible = true;
            divGoogleDrivePathPanel.Visible = false;
        }
        Panel1_ModalPopupExtender.Show();
    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTerm.SelectedIndex != 0)
        {
            txtExamName.Text = ddlTerm.SelectedItem.Text + " Class " + ddlClass.SelectedItem.Text + " " + ddlPaper.SelectedItem.Text;
        }
        else
        {
            txtExamName.Text = "";
        }
    }

    protected void ddlDontshow_CheckedChanged(object sender, EventArgs e)
    {

        if (ddlDontshow.Checked)
        {
            divResultFrom.Visible = false;
            divResultTo.Visible = false;
            divResSetrting.Visible = false;
            txtShowResultFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtShowResultToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            ddlShowResultToTT.SelectedIndex = 0;
            BLL.FillHourInDropDownlist(ddlShowResultToHH);
            ddlShowResultToHH.SelectedIndex = 1;
            BLL.FillMinuteInDropDownlist(ddlShowResultToMM);
            ddlShowResultToMM.SelectedIndex = 2;
        }
        else
        {
            txtShowResultFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtShowResultToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            divResultFrom.Visible = true;
            divResultTo.Visible = true;
            divResSetrting.Visible = true;
            BLL.FillHourInDropDownlist(ddlShowResultToHH);
            ddlShowResultToHH.SelectedIndex = 12;
            BLL.FillMinuteInDropDownlist(ddlShowResultToMM);
            ddlShowResultToMM.SelectedIndex = 60;
            ddlShowResultToTT.SelectedIndex = 1;
        }
    }
    protected void ddlDontshowPanel_CheckedChanged(object sender, EventArgs e)
    {

        if (ddlDontshowPanel.Checked)
        {
            TrResultFrom.Visible = false;
            TrResultTo.Visible = false;
            TrResSetrting.Visible = false;
            txtShowResultFromDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtShowResultToDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            ddlShowResultToTT0.SelectedIndex = 0;
            BLL.FillHourInDropDownlist(ddlShowResultToHH0);
            ddlShowResultToHH0.SelectedIndex = 1;
            BLL.FillMinuteInDropDownlist(ddlShowResultToMM0);
            ddlShowResultToMM0.SelectedIndex = 2;
        }
        else
        {
            txtShowResultFromDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtShowResultToDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            TrResultFrom.Visible = true;
            TrResultTo.Visible = true;
            TrResSetrting.Visible = true;
            BLL.FillHourInDropDownlist(ddlShowResultToHH0);
            ddlShowResultToHH0.SelectedIndex = 12;
            BLL.FillMinuteInDropDownlist(ddlShowResultToMM0);
            ddlShowResultToMM0.SelectedIndex = 60;
            ddlShowResultToTT0.SelectedIndex = 1;
        }
        Panel1_ModalPopupExtender.Show();
    }
}