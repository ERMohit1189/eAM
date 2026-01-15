using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class Studentwisereport : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public Studentwisereport()
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
        
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {
            loadClass();
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlStream.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlgroup.Items.Insert(0, new ListItem("<--Select-->", ""));

            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void loadUpdate()
    {
        _sql = "select distinct SRNo from OT_ExamAllotment where Branchcode = " + Session["BranchCode"] + " and sessionName = '" + Session["SessionName"] + "' and ExamID = "+ ddlExam.SelectedValue + "";
        var dts = _oo.Fetchdata(_sql);
        for (int j = 0; j < dts.Rows.Count; j++)
        {
            var studentId = dts.Rows[j]["SRNo"].ToString();
            _sql = "select SRNo, ExamID,isnull(ea.Duration, 0) Lettime, case when isnull(TestSetting,0)=0 then 0 else 1 end Willattempted,  case when(em.Duration * 60)>  ";
            _sql = _sql + " ea.Duration then 'Isattempted' else 'notattempted' end ExamDuration, case when GETDATE() between em.ExamStart and ExamEnd then 'examLive' else 'examnotlive' end timeDuration ";
            _sql = _sql + " from OT_ExamMaster em inner ";
            _sql = _sql + " join OT_ExamAllotment ea on ea.ExamID = em.id and ea.Branchcode = em.Branchcode and ea.sessionName = em.sessionName ";
            _sql = _sql + " where SRNo = '" + studentId + "' and em.BranchCode=" + Session["BranchCode"] + " and em.SessionName='" + Session["SessionName"] + "'";
            var dt = _oo.Fetchdata(_sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ExamDuration"].ToString() == "Isattempted" && dt.Rows[i]["timeDuration"].ToString() == "examnotlive")
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update OT_ExamAllotment set Status=1 where ExamID=" + dt.Rows[i]["ExamID"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + studentId + "'";
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

                if (dt.Rows[i]["Willattempted"].ToString() == "0" && dt.Rows[i]["ExamDuration"].ToString() == "Isattempted" && dt.Rows[i]["timeDuration"].ToString() == "examLive")
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update OT_ExamAllotment set Status=1 where ExamID=" + dt.Rows[i]["ExamID"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + studentId + "'";
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

            }
        }
    }
    private void loadClass()
    {
        if (Session["Logintype"].ToString().ToLower() == "staff")
        {
            _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select distinct classid from OT_AllotTestToStaff where ECode='"+ Session["LoginName"]+"' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") order by CIDOrder";
            _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
            _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }

    private void loadStream()
    {
        _sql = "select Id, BranchName from BranchMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid="+ ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlStream, "BranchName", "Id");
        ddlStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadSection()
    {
        if (Session["Logintype"].ToString().ToLower() == "staff")
        {
            _sql = "select Id, SectionName from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + ddlClass.SelectedValue + " and Id in (select Sectionid from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            _oo.FillDropDown_withValue(_sql, ddlSection, "SectionName", "Id");
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            _sql = "select Id, SectionName from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + ddlClass.SelectedValue + "";
            _oo.FillDropDown_withValue(_sql, ddlSection, "SectionName", "Id");
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    private void loadGroup()
    {
        _sql = "select Id, Stream from StreamMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid=" + ddlClass.SelectedValue + "  and Branchid=" + ddlStream.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlgroup, "Stream", "Id");
        ddlgroup.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, Medium from MediumMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlMedium, "Medium", "Id");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));

        if (Session["Logintype"].ToString().ToLower() == "staff")
        {
            _sql = "select Id, Subject from OT_SubjectMaster where classId=" + ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id in (select Subjectid from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            _sql = "select Id, Subject from OT_SubjectMaster where classId=" + ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        
    }
    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStream();
    }
    protected void ddlStream_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGroup();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["Logintype"].ToString().ToLower() == "staff")
        {
            _sql = "select Id, Paper from OT_PaperMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Id in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ")";
            _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            _sql = "select Id, Paper from OT_PaperMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId=" + ddlPaper.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblQuesType.Text = "";
        _sql = "select QuestionType from OT_SigmentMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and examid=" + ddlExam.SelectedValue + " and QuestionType='Descriptive' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            lblQuesType.Text = "Descriptive";
        }
        else
        {
            lblQuesType.Text = "MCQs";
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        Grd.DataSource = null;
        Grd.DataBind();
        Grd2.DataSource = null;
        Grd2.DataBind();
        if (lblQuesType.Text == "MCQs")
        {
            LoadData2();
        }
        else
        {
            LoadData();
        }
    }
    protected void LoadData()
    {
        loadUpdate();
        
        Grd.DataSource =null;
        Grd.DataBind();
        if (ddltestStatus.SelectedValue == "Completed")
        {
            _sql = "Select asr.SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, rs.ExamId, ";
            _sql = _sql + " (select sum(MaxMarks) from OT_AnswerMaster ans where ans.classId=" + ddlClass.SelectedValue + " and ans.SubjectId=" + ddlSubject.SelectedValue + " and ans.PaperId=" + ddlPaper.SelectedValue + " and ans.ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")MaxMarks, ";
            _sql = _sql + " (select sum(ObtaindMarks) from OT_ExamAnswerResult ans where ans.classId=" + ddlClass.SelectedValue + " and ans.SubjectId=" + ddlSubject.SelectedValue + " and ans.PaperId=" + ddlPaper.SelectedValue + " and ans.ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SrNO=asr.SrNo) ObtaindMarks ";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr inner join OT_ExamAnswerResult rs on rs.classId=asr.ClassId and asr.SrNo=rs.SrNO ";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')='' and rs.classId=" + ddlClass.SelectedValue + " and rs.SubjectId=" + ddlSubject.SelectedValue + " and rs.PaperId=" + ddlPaper.SelectedValue + " and rs.ExamId=" + ddlExam.SelectedValue + " and isnull(AnswerStatus, '')<>'' ";
            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and asr.BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and asr.SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(asr.StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAllotment where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and Status=1)";

            _sql = _sql + " group by asr.SRNo, Name, FatherContactNo, FatherName, DOB, CombineClassName, rs.ExamId ";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
        }
        if (ddltestStatus.SelectedValue == "Attempted")
        {
            _sql = "Select asr.SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, rs.ExamId, ";
            _sql = _sql + " (select sum(MaxMarks) from OT_AnswerMaster ans where ans.classId=" + ddlClass.SelectedValue + " and ans.SubjectId=" + ddlSubject.SelectedValue + " and ans.PaperId=" + ddlPaper.SelectedValue + " and ans.ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")MaxMarks, ";
            _sql = _sql + " (select sum(ObtaindMarks) from OT_ExamAnswerResult ans where ans.classId=" + ddlClass.SelectedValue + " and ans.SubjectId=" + ddlSubject.SelectedValue + " and ans.PaperId=" + ddlPaper.SelectedValue + " and ans.ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SrNO=asr.SrNo) ObtaindMarks ";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr inner join OT_ExamAnswerResult rs on rs.classId=asr.ClassId and asr.SrNo=rs.SrNO ";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')='' and rs.classId=" + ddlClass.SelectedValue + " and rs.SubjectId=" + ddlSubject.SelectedValue + " and rs.PaperId=" + ddlPaper.SelectedValue + " and rs.ExamId=" + ddlExam.SelectedValue + " and isnull(AnswerStatus, '')<>'' ";
            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and asr.BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and asr.SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(asr.StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAllotment where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and Status=0)";

            _sql = _sql + " group by asr.SRNo, Name, FatherContactNo, FatherName, DOB, CombineClassName, rs.ExamId ";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
        }
        if (ddltestStatus.SelectedValue == "Test Left")
        {
            _sql = "Select asr.SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " 0 MaxMarks, ";
            _sql = _sql + " 0 ObtaindMarks ";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr ";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')='' and asr.classId=" + ddlClass.SelectedValue + " ";

            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and asr.BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and asr.SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(asr.StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            _sql = _sql + " and getdate()> (select ExamEnd from OT_ExamMaster where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and Id=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";

            _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAllotment where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and status=0)";

            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
        }
        if (ddltestStatus.SelectedValue == "Not Attempted")
        {
            _sql = "Select asr.SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " 0 MaxMarks, ";
            _sql = _sql + " 0 ObtaindMarks ";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr ";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')='' and asr.classId=" + ddlClass.SelectedValue + " ";

            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and asr.BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and asr.SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(asr.StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            _sql = _sql + " and getdate()<= (select ExamEnd from OT_ExamMaster where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and id=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";

            _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAllotment where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and status=0)";

            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
        }
        
        if (Grd.Rows.Count>0)
        {
            title.Text = "Test : " + ddlExam.SelectedItem.Text + " | Subject : " +  ddlSubject.SelectedItem.Text+" | Paper : "+ ddlPaper.SelectedItem.Text;
            listdisplay.Visible = true;
            
        }
    }
    protected void LoadData2()
    {
        loadUpdate();
        Grd2.DataSource = null;
        Grd2.DataBind();
        if (ddltestStatus.SelectedValue == "Completed")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " (select count(*) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") TotalQus,";
            _sql = _sql + " (select count(*) from OT_ExamAnswerResult where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SrNO=asr.SrNO and ISNULL(AnswerStatus, '')<>'' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") Attempted,";
            _sql = _sql + " (select COUNT(*) from OT_AnswerMaster a inner join OT_ExamAnswerResult ar on ar.QuestionId=a.id and ISNULL(a.RightOption,'')<>ISNULL(ar.ChooseOption, '')  and a.SubjectId=ar.SubjectId  and a.PaperId=ar.PaperId  and a.ExamID=ar.ExamID ";
            _sql = _sql + " and ISNULL(ar.AnswerStatus, '')<>'' and a.SubjectId=" + ddlSubject.SelectedValue + " and a.PaperId=" + ddlPaper.SelectedValue + " and a.ExamID=" + ddlExam.SelectedValue + " and ar.SrNO=asr.SrNO  and  ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + " and  a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + ") wrongAns,";
            _sql = _sql + " (select COUNT(*) from OT_AnswerMaster a inner join OT_ExamAnswerResult ar on ar.QuestionId=a.id and ISNULL(a.RightOption,'')=ISNULL(ar.ChooseOption, '')  and a.SubjectId=ar.SubjectId  and a.PaperId=ar.PaperId  and a.ExamID=ar.ExamID";
            _sql = _sql + " and ISNULL(ar.AnswerStatus, '')<>'' and a.SubjectId=" + ddlSubject.SelectedValue + " and a.PaperId=" + ddlPaper.SelectedValue + " and a.ExamID=" + ddlExam.SelectedValue + " and ar.SrNO=asr.SrNO  and  ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + " and  a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + ") RightAns, ";
            _sql = _sql + " (select sum(MaxMarks) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") MaxMarks, ";
            _sql = _sql + " (select sum(ObtaindMarks) from OT_ExamAnswerResult where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNO=asr.SrNO) ObtaindMarks ";

            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')=''";
            _sql = _sql + "  and SRNo in (select SRNo from OT_ExamAllotment where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and Status=1 and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and classid=" + ddlClass.SelectedValue + "";
            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            Grd2.DataSource = _oo.GridFill(_sql);
            Grd2.DataBind();
        }
        if (ddltestStatus.SelectedValue == "Attempted")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " (select count(*) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") TotalQus,";
            _sql = _sql + " (select count(*) from OT_ExamAnswerResult where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SrNO=asr.SrNO and ISNULL(AnswerStatus, '')<>'' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") Attempted,";
            _sql = _sql + " (select COUNT(*) from OT_AnswerMaster a inner join OT_ExamAnswerResult ar on ar.QuestionId=a.id and ISNULL(a.RightOption,'')<>ISNULL(ar.ChooseOption, '')  and a.SubjectId=ar.SubjectId  and a.PaperId=ar.PaperId  and a.ExamID=ar.ExamID ";
            _sql = _sql + " and ISNULL(ar.AnswerStatus, '')<>'' and a.SubjectId=" + ddlSubject.SelectedValue + " and a.PaperId=" + ddlPaper.SelectedValue + " and a.ExamID=" + ddlExam.SelectedValue + " and ar.SrNO=asr.SrNO  and  ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + " and  a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + ") wrongAns,";
            _sql = _sql + " (select COUNT(*) from OT_AnswerMaster a inner join OT_ExamAnswerResult ar on ar.QuestionId=a.id and ISNULL(a.RightOption,'')=ISNULL(ar.ChooseOption, '')  and a.SubjectId=ar.SubjectId  and a.PaperId=ar.PaperId  and a.ExamID=ar.ExamID";
            _sql = _sql + " and ISNULL(ar.AnswerStatus, '')<>'' and a.SubjectId=" + ddlSubject.SelectedValue + " and a.PaperId=" + ddlPaper.SelectedValue + " and a.ExamID=" + ddlExam.SelectedValue + " and ar.SrNO=asr.SrNO  and  ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + " and  a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + ") RightAns, ";
            _sql = _sql + " (select sum(MaxMarks) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") MaxMarks, ";
            _sql = _sql + " (select sum(ObtaindMarks) from OT_ExamAnswerResult where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SrNO=asr.SrNO) ObtaindMarks ";

            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')=''";
            _sql = _sql + "  and SRNo in (select SRNo from OT_ExamAllotment where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and Status=0 and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and classid=" + ddlClass.SelectedValue + "";
            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            Grd2.DataSource = _oo.GridFill(_sql);
            Grd2.DataBind();
        }
        if (ddltestStatus.SelectedValue == "Test Left")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " (select count(*) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") TotalQus,";
            _sql = _sql + " 0 Attempted, 0 wrongAns, 0 RightAns, ";
            _sql = _sql + " (select sum(MaxMarks) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") MaxMarks, ";
            _sql = _sql + " 0 ObtaindMarks from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')=''";
            _sql = _sql + "  and SRNo in (select SRNo from OT_ExamAllotment where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and Status=0 and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and classid=" + ddlClass.SelectedValue + " and (select count(*) from OT_ExamMaster where GETDATE() > ExamEnd and id=" + ddlExam.SelectedValue + ")>0";
            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            Grd2.DataSource = _oo.GridFill(_sql);
            Grd2.DataBind();
        }
        if (ddltestStatus.SelectedValue == "Not Attempted")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " (select count(*) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") TotalQus,";
            _sql = _sql + " 0 Attempted, 0 wrongAns, 0 RightAns, ";
            _sql = _sql + " (select sum(MaxMarks) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") MaxMarks, ";
            _sql = _sql + " 0 ObtaindMarks from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr";
            _sql = _sql + " where Withdrwal is null and isnull(blocked, '')=''";
            _sql = _sql + "  and SRNo in (select SRNo from OT_ExamAllotment where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and Status=0 and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") and classid=" + ddlClass.SelectedValue + " and (select count(*) from OT_ExamMaster where GETDATE() between ExamStart and ExamEnd and id=" + ddlExam.SelectedValue + ")>0";
            if (ddlStream.SelectedValue != "")
            {
                _sql = _sql + " and BranchId=" + ddlStream.SelectedValue + "";
            }
            if (ddlSection.SelectedValue != "")
            {
                _sql = _sql + " and SectionID=" + ddlSection.SelectedValue + "";
            }
            if (ddlgroup.SelectedValue != "")
            {
                _sql = _sql + " and isnull(StreamId, '')=" + ddlgroup.SelectedValue + "";
            }
            if (Session["Logintype"].ToString().ToLower() == "staff")
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId in (select PaperId from OT_AllotTestToStaff where ECode='" + Session["LoginName"] + "' and classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + " and SectionId=" + ddlSection.SelectedValue + ") and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            else
            {
                _sql = _sql + " and asr.srno not in (select distinct SrNO from OT_ExamAnswerResult where classId=" + ddlClass.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamId=" + ddlExam.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and Branchcode=" + Session["BranchCode"] + ")";
            }
            Grd2.DataSource = _oo.GridFill(_sql);
            Grd2.DataBind();
        }
        
        if (Grd2.Rows.Count > 0)
        {
            title.Text = "Test : " + ddlExam.SelectedItem.Text + " | Subject : " + ddlSubject.SelectedItem.Text + " | Paper : " + ddlPaper.SelectedItem.Text;
            listdisplay.Visible = true;

        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "ListOfStudents", gdv);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "ListOfStudents", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        Grd.Style.Add("text-transform", "uppercase");
        _oo.ExportDivToExcelWithFormatting(Response, "Classwiseresult.xls", gdv, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        //Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        PrintHelper_New.ctrl = gdv;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        //LoadData();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    
}