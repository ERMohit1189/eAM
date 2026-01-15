using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StartTest : System.Web.UI.Page
{
    string sql = "";
    SqlConnection con;
    Campus oo = new Campus();
    string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
       
        if (!IsPostBack)
        {
            
            sql = "select SRNo, ExamID,isnull(ea.Duration, 0) Lettime, case when isnull(TestSetting,0)=0 then 0 else 1 end Willattempted,  case when(em.Duration * 60)>  ";
            sql = sql+ " ea.Duration then 'Isattempted' else 'notattempted' end ExamDuration, case when GETDATE() between em.ExamStart and ExamEnd then 'examLive' else 'examnotlive' end timeDuration ";
            sql = sql + " from OT_ExamMaster em inner ";
            sql = sql + " join OT_ExamAllotment ea on ea.ExamID = em.id and ea.Branchcode = em.Branchcode and ea.sessionName = em.sessionName ";
            sql = sql + " where SRNo = '" + Session["LoginName"].ToString() + "' and em.BranchCode=" + Session["BranchCode"] + " and em.SessionName='" + Session["SessionName"] + "'";
            var dt = oo.Fetchdata(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Willattempted"].ToString()== "0" && dt.Rows[i]["ExamDuration"].ToString()== "Isattempted")
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update OT_ExamAllotment set Status=1, Duration=0 where ExamID=" + dt.Rows[i]["ExamID"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + Session["LoginName"].ToString() + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                if (dt.Rows[i]["Willattempted"].ToString() == "1" && dt.Rows[i]["ExamDuration"].ToString() == "Isattempted" && int.Parse(dt.Rows[i]["Lettime"].ToString())<10)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update OT_ExamAllotment set Status=1, Duration=0 where ExamID=" + dt.Rows[i]["ExamID"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + Session["LoginName"].ToString() + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
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
                        cmd.CommandText = "update OT_ExamAllotment set Status=1, Duration=0 where ExamID=" + dt.Rows[i]["ExamID"].ToString() + " and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SRNo='" + Session["LoginName"].ToString() + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            setwidgets();
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton image = (ImageButton)sender;
        sql = "select Url from Menueam where MenuID='" + image.AlternateText.ToString() + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        Response.Redirect(BAL.objBal.ReturnTag(sql, "Url"));

    }
    private void setwidgets()
    {

        Grd.DataSource = null;
        Grd.DataBind();
        _sql = "select distinct EA.ExamID, SM.Subject, PM.Paper, EM.ExamName, EM.id,  (case when em.ExamStart>convert(datetime,getdate())  then 'Test Not Started' else  case when getdate() between em.ExamStart and em.ExamEnd and  EA.status = 0 then  'Not Attempted' else case when getdate() > em.ExamEnd and  EA.status = 0 then 'Test Left' else case when getdate()  between em.ExamStart and em.ExamEnd and  EA.status = 1 then  'Attempted' else 'Attempted' end end end end) status  from OT_ExamAllotment EA";
        _sql = _sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') Reg on Reg.SRNo = EA.SRNo";
        _sql = _sql + " inner join OT_ExamMaster EM on EM.id = EA.examid  and  ea.SessionName='" + Session["SessionName"] + "' and ea.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_SubjectMaster SM on SM.id = EA.SubjectId and  sm.SessionName='" + Session["SessionName"] + "' and sm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_PaperMaster PM on PM.id = EA.PaperId and  pm.SessionName='" + Session["SessionName"] + "' and pm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_AnswerMaster Am on Am.examid = EA.examid and  am.SessionName='" + Session["SessionName"] + "' and am.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " where  EA.SRNo = '" + Session["LoginName"].ToString() + "' and  em.SessionName='" + Session["SessionName"] + "' and em.BranchCode=" + Session["BranchCode"] + "  order by EM.id asc";

        SqlDataAdapter sdaa = new SqlDataAdapter(_sql, con);
        DataTable dtt = new DataTable();
        sdaa.Fill(dtt);
        if (dtt.Rows.Count > 0)
        {
            Grd.DataSource = dtt;
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label Status = (Label)Grd.Rows[i].FindControl("Status");
                Label ExamID = (Label)Grd.Rows[i].FindControl("ExamID");
                HyperLink hy = (HyperLink)Grd.Rows[i].FindControl("lnk");
                hy.NavigateUrl = "~/13/exampreview.aspx?p=" + ExamID.Text;

                if (Status.Text.Trim() == "Not Attempted")
                {
                    hy.Visible = true;
                    Status.Visible = false;
                }
                else
                {
                    hy.Visible = false;
                    Status.Visible = true;
                }

            }
        }
    }
}