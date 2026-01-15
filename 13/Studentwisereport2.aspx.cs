using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class Studentwisereport2 : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public Studentwisereport2()
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
                if (dt.Rows[i]["Willattempted"].ToString() == "0" && dt.Rows[i]["ExamDuration"].ToString() == "Isattempted")
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
                if (dt.Rows[i]["Willattempted"].ToString() == "1" && dt.Rows[i]["ExamDuration"].ToString() == "Isattempted" && int.Parse(dt.Rows[i]["Lettime"].ToString()) < 10)
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
            }
        }
    }
    private void loadClass()
    {
        _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
        _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
        ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }

    private void loadStream()
    {
        _sql = "select Id, BranchName from BranchMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid="+ ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlStream, "BranchName", "Id");
        ddlStream.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    private void loadSection()
    {
        _sql = "select Id, SectionName from SectionMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlSection, "SectionName", "Id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
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

        _sql = "select Id, Subject from OT_SubjectMaster where classId="+ ddlClass.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlSubject, "Subject", "Id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        
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
        _sql = "select Id, Paper from OT_PaperMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlPaper, "Paper", "Id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = "select Id, ExamName from OT_ExamMaster where SubjectId=" + ddlSubject.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and PaperId=" + ddlPaper.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlExam, "ExamName", "Id");
        ddlExam.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        Grd.DataSource = null;
        Grd.DataBind();
        LoadData();
    }
    protected void LoadData()
    {
        loadUpdate();
        Grd.DataSource =null;
        Grd.DataBind();
        if (ddltestStatus.SelectedValue== "Attempted")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " (select count(*) from OT_AnswerMaster where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") Total,";
            _sql = _sql + " (select count(*) from OT_ExamAnswerResult where SubjectId=" + ddlSubject.SelectedValue + " and PaperId=" + ddlPaper.SelectedValue + " and ExamID=" + ddlExam.SelectedValue + " and SrNO=asr.SrNO and ISNULL(AnswerStatus, '')<>'' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") Attempted,";
            _sql = _sql + " (select COUNT(*) from OT_AnswerMaster a inner join OT_ExamAnswerResult ar on ar.QuestionId=a.id and ISNULL(a.RightOption,'')<>ISNULL(ar.AnswerStatus, '')  and a.SubjectId=ar.SubjectId  and a.PaperId=ar.PaperId  and a.ExamID=ar.ExamID ";
            _sql = _sql + " and ISNULL(ar.AnswerStatus, '')<>'' and a.SubjectId=" + ddlSubject.SelectedValue + " and a.PaperId=" + ddlPaper.SelectedValue + " and a.ExamID=" + ddlExam.SelectedValue + " and ar.SrNO=asr.SrNO  and  ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + " and  a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + ") wrongAns,";
            _sql = _sql + " (select COUNT(*) from OT_AnswerMaster a inner join OT_ExamAnswerResult ar on ar.QuestionId=a.id and ISNULL(a.RightOption,'')=ISNULL(ar.AnswerStatus, '')  and a.SubjectId=ar.SubjectId  and a.PaperId=ar.PaperId  and a.ExamID=ar.ExamID";
            _sql = _sql + " and ISNULL(ar.AnswerStatus, '')<>'' and a.SubjectId=" + ddlSubject.SelectedValue + " and a.PaperId=" + ddlPaper.SelectedValue + " and a.ExamID=" + ddlExam.SelectedValue + " and ar.SrNO=asr.SrNO  and  ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + " and  a.SessionName='" + Session["SessionName"] + "' and a.BranchCode=" + Session["BranchCode"] + ") RightAns";
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
        }
        if (ddltestStatus.SelectedValue == "Test Left")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " '' Total, '' Attempted, '' wrongAns, '' RightAns ";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr";
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
        }
        if (ddltestStatus.SelectedValue == "Not Attempted")
        {
            _sql = "Select SRNo, Name, FatherContactNo MobileNo, FatherName, DOB DateofBirth, CombineClassName, ";
            _sql = _sql + " '' Total, '' Attempted, '' wrongAns, '' RightAns ";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') asr";
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
        }
            Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count>0)
        {
            title.Text = "Test : " + ddlExam.SelectedItem.Text + " | Subject : " +  ddlSubject.SelectedItem.Text+" | Paper : "+ ddlPaper.SelectedItem.Text;
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
        _oo.ExportDivToExcelWithFormatting(Response, "ListOfStudents.xls", gdv, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        PrintHelper_New.ctrl = abc;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        LoadData();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
}