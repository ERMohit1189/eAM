using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class Assisment : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    string ExamID = "";
    public Assisment()
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
        if (Session["Logintype"].ToString().ToLower() != "staff")
        {
            Response.Redirect("~/default.aspx");
        }
        
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);

        if (!IsPostBack)
        {
            ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
            loadTerm();

            _sql = "select * from OT_AllotTestToStaff  where SessionName = '" + Session["SessionName"] + "' and Branchcode = " + Session["BranchCode"] + " and ECode = '" + Session["LoginName"] + "'";
            if (!_oo.Duplicate(_sql))
            {
                maindiv.Visible = false;
                lblerror.InnerText = "Please allot test first!";
                divError.Visible = true;
            }
            else
            {
                maindiv.Visible = true;
                divError.Visible = false;
            }
            string sq = "select ExamId from OT_ExamAnswerResult where ExamId=" + ExamID + " and isnull(AnswerStatus, '')='' and QuestionType='Descriptive' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ExamId=" + ExamID + "";
            if (_oo.Duplicate(sq))
            {
                btnSave.Visible = false;
                msgbox.InnerText = "Test successfully checked.";
            }
        }

    }

    protected void loadDate()
    {
        ExamID = ddlExam.SelectedValue;

        try
        {
            if (Request.QueryString.Keys.Count > 0)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "update OT_ExamAllotment set Status=1, Duration=0 where ExamID=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + Session["SRNo"].ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = _con;
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            _sql = "select name, srno from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where SrNo='" + Session["SRNo"].ToString() + "'";
            StuName.Text = _oo.ReturnTag(_sql, "name");
            rollNo.Text = _oo.ReturnTag(_sql, "srno");

            _sql = "select status from OT_ExamAllotment where SRNo = '" + rollNo.Text + "' and examid=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and status=1";
            if (_oo.Duplicate(_sql))
            {
                
            }
            else
            {
                _oo.msgbox(Page, Div1, "Test not completed!", "A");
            }
        }
        catch (Exception)
        {

        }


        hdnExamID.Value = ExamID;
        _sql = "select filepath, fileType from OT_ExamMaster where id=" + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string fileType = _oo.ReturnTag(_sql, "fileType");
        string filepath = _oo.ReturnTag(_sql, "filepath");
        if (filepath != "")
        {
            if (fileType == "FileUpload")
            {
                string data = filepath.Replace("../", "~/");
                string[] str = data.Split(new string[] { "." }, StringSplitOptions.None);
                if (str[1].ToLower() == "pdf")
                {
                    string strs = "<iframe id='fraDisabled' src='" + filepath + "#&embedded=true&toolbar=0&navpanes=0&view=FitH' style='width:100%; height:700px;'></iframe>";
                    ltEmbed.InnerHtml = strs;
                    ltEmbed.Visible = true;
                    imgExam.Visible = false;
                }
                else
                {
                    ltEmbed.Visible = false;
                    imgExam.Visible = true;
                    imgExam.ImageUrl = data;
                }
            }
            else
            {
                string strs = "<iframe id='fraDisabled' src='" + filepath + "' style='width:100%; height:700px;'></iframe>";
                strs = strs + "<div style='width: 80px; height: 80px; position: absolute; opacity: 0; right: 0px; top: 0px; '></div>";
                ltEmbed.InnerHtml = strs;
                ltEmbed.Visible = true;
                imgExam.Visible = false;
            }

        }
        else if (fileType == "Manual")
        {
            divPdf.Visible = false;
            tblAppend.Attributes.Add("class", "col-sm-12");
        }
        else
        {
            ltEmbed.Visible = false;
            imgExam.Visible = false;
        }
        DataTable dt = new DataTable();
        try
        {
            _sql = "select id, SigmentName, QuestionType, DeductionOn from OT_SigmentMaster where ExamId = " + ExamID + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            dt = _oo.Fetchdata(_sql);
        }
        catch (Exception)
        {

        }
    }
    private void loadTerm()
    {
        _sql = "select Id, TermName from OT_TermMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id asc";
        _oo.FillDropDown_withValue(_sql, ddlTerm, "TermName", "Id");
        ddlTerm.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamAnswerResultProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamId", ddlExam.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@SrNO", ddlsrno.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "FinalSaveTest");
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                Session["SRNo"] = ddlsrno.SelectedValue;
                loadDate();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "LoadData();", true);
                string sq = "select ExamId from OT_ExamAnswerResult where ExamId=" + ddlExam.SelectedValue + " and isnull(AnswerStatus, '')='' and QuestionType='Descriptive' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (!_oo.Duplicate(sq))
                {
                    btnSave.Visible = false;
                    msgbox.InnerText = "Test successfully checked.";
                    _oo.msgbox(Page, msgbox, "Submitted successfully.", "S");
                }
                else
                {
                    //_oo.msgbox(Page, msgbox, "Some questions not checked!", "A");
                }
                _con.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = " select distinct e.id, e.ExamName from OT_AllotTestToStaff s ";
        _sql = _sql + " inner join OT_ExamMaster e on e.classId = s.classid and e.SubjectId = s.SubjectId and e.PaperId = s.Paperid and e.SessionName = s.SessionName and e.Branchcode = s.Branchcode ";
        _sql = _sql + " inner join OT_SigmentMaster si on si.classId = s.classid and si.SubjectId = si.SubjectId and si.PaperId = s.Paperid and si.SessionName = s.SessionName and si.Branchcode = s.Branchcode and e.TermId = si.TermId ";
        _sql = _sql + " where e.TermId = "+ ddlTerm.SelectedValue+" and s.SessionName = '" + Session["SessionName"] + "' and s.Branchcode = " + Session["BranchCode"] + " and s.ECode = '"+ Session["LoginName"]+ "' and si.QuestionType <> 'MCQs'";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = " select distinct s.SrNO, (si.Name+'-'+s.SrNO) as Name from OT_ExamAnswerResult s ";
        _sql = _sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") si on si.SessionName = s.SessionName and si.Branchcode = s.Branchcode and s.SrNO = si.SrNo ";
        _sql = _sql + " where s.TermId = " + ddlTerm.SelectedValue + " and ExamId =" + ddlExam.SelectedValue + " and s.SessionName = '" + Session["SessionName"] + "' and s.Branchcode = " + Session["BranchCode"] + " and  isnull(AnswerStatus,'')=case when s.QuestionType='MCQs' then isnull(AnswerStatus,'') else '' end ";
        _sql = _sql + " and si.Sectionid in (select Sectionid from OT_AllotTestToStaff st where st.SessionName = s.SessionName and st.Branchcode = s.Branchcode and PaperId=s.PaperId and classid=s.classId and SubjectId=s.SubjectId and st.Ecode='" + Session["LoginName"] + "') ";
        _oo.FillDropDown_withValue(_sql, ddlsrno, "Name", "SrNO");
        ddlsrno.Items.Insert(0, new ListItem("<--Select-->", ""));
        hdnExamID.Value = ddlExam.SelectedValue;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["SRNo"] = ddlsrno.SelectedValue;
        loadDate();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "LoadData();", true);
        string sq = "select ExamId from OT_ExamAnswerResult where srno='" + ddlsrno.SelectedValue + "' and ExamId=" + ddlExam.SelectedValue + " and isnull(AnswerStatus, '')='' and QuestionType='Descriptive' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (!_oo.Duplicate(sq))
        {
            btnSave.Visible = false;
            msgbox.InnerText = "Test successfully checked.";
        }
        else
        {
            btnSave.Visible = true;
            //_oo.msgbox(Page, msgbox, "Some questions not checked!", "A");
        }
    }


    
}