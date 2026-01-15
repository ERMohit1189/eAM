using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class ExamAdminResult : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, sql, _sql1 = String.Empty;
    public ExamAdminResult()
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
        
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        setwidgets();
    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        sql = "select SRNo, ExamID,isnull(ea.Duration, 0) Lettime, case when isnull(testSetting,0)=0 then 0 else 1 end Willattempted,  case when(em.Duration * 60)>  ";
        sql = sql + " ea.Duration then 'Isattempted' else 'notattempted' end ExamDuration, case when GETDATE() between em.ExamStart and ExamEnd then 'examLive' else 'examnotlive' end timeDuration ";
        sql = sql + " from OT_ExamMaster em inner ";
        sql = sql + " join OT_ExamAllotment ea on ea.ExamID = em.id and ea.Branchcode = em.Branchcode and ea.sessionName = em.sessionName ";
        sql = sql + " where SRNo = '" + studentId + "' and em.BranchCode=" + Session["BranchCode"] + " and em.SessionName='" + Session["SessionName"] + "'";
        var dt = _oo.Fetchdata(sql);
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
            
        setwidgets();
    }
    private void setwidgets()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }

         Grd.DataSource = null;
        Grd.DataBind();
        _sql = "select EM.ResultShow, EM.ResultHide, em.ExamStart, em.ExamEnd, Reg.SRNo, (FirstName+' '+MiddleName+' '+LastName) as Name, FatherName, ExamID, EM.id, SM.Subject, PM.Paper, EM.ExamName, EM.FilePath, EM.Duration, EM.SessionName, EM.Loginname, EM.Recordeddate, EA.Duration,  (case when EA.status = 0 and em.ExamEnd < convert(datetime, getdate()) then 'Test Left' else case when EA.status = 0 and em.ExamEnd >= convert(datetime, getdate()) then 'Not Attempted' else case when EA.status = 1 and (GETDATE() between em.ResultShow and em.ResultHide) then 'showresult' else case when EA.status = 1 then 'Attempted'  end end end end) status ";
        _sql = _sql + " from OT_ExamAllotment EA";
        _sql = _sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') Reg on Reg.SRNo = EA.SRNo";
        _sql = _sql + " inner join OT_ExamMaster EM on EM.id = EA.ExamID  and  EM.SessionName='" + Session["SessionName"] + "' and EM.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_SubjectMaster SM on SM.id = EA.SubjectId  and  sm.SessionName='" + Session["SessionName"] + "' and sm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_PaperMaster PM on PM.id = EA.PaperId  and  pm.SessionName='" + Session["SessionName"] + "' and pm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " where  EA.SRNo = '" + studentId + "' and  ea.SessionName='" + Session["SessionName"] + "' and ea.BranchCode=" + Session["BranchCode"] + "  order by EM.id asc";
        
        SqlDataAdapter sdaa = new SqlDataAdapter(_sql, _con);
        DataTable dtt = new DataTable();
        sdaa.Fill(dtt);
        if (dtt.Rows.Count > 0)
        {
            Session["SRNo"] = studentId;
            Grd.DataSource = dtt;
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {

                Label Status = (Label)Grd.Rows[i].FindControl("Status");
                Label ExamID = (Label)Grd.Rows[i].FindControl("ExamID");
                HyperLink hy = (HyperLink)Grd.Rows[i].FindControl("lnk");
                hy.NavigateUrl = "~/13/ResultAdmin.aspx?p=" + ExamID.Text;
                if (Status.Text == "showresult" || Status.Text == "Attempted")
                {
                    hy.Visible = true;
                    Status.Visible = false;
                    string _sql1 = "select ChooseOption from OT_ExamAnswerResult where srno='"+ studentId + "' and ExamId=" + ExamID.Text + " and QuestionType='Descriptive' and isnull(AnswerStatus, '')='' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                    if (_oo.Duplicate(_sql1))
                    {
                        hy.Visible = false;
                        Status.Visible = true;
                        Status.Text = "Test Not Checked";
                    }
                }
                else
                {
                    hy.Visible = false;
                    Status.Visible = true;
                }
            }
        }
    }

    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }
    
}