using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllotedTestForAdmin : System.Web.UI.Page
{
    string sql = "";
    SqlConnection con;
    Campus oo = new Campus();
    string _sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        oo.LoadLoader(loader);
        //if (string.IsNullOrEmpty(Session["LoginName"] as string))
        //{
        //    Response.Redirect("default.aspx");
        //}
        //if (Session["Logintype"].ToString().ToLower() == "admin")
        //{
        //    Response.Redirect("default.aspx");
        //}
        if (!IsPostBack)
        {
            setwidgets();
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        setwidgets();
    }
    protected void TxtEnter_TextChanged(object sender, EventArgs e)
    {
        setwidgets();//aSHashsh
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton image = (ImageButton)sender;
        sql = "select Url from Menueam where MenuID='" + image.AlternateText.ToString() + "' and  SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        Response.Redirect(BAL.objBal.ReturnTag(sql, "Url"));

    }
    private void setwidgets()
    {
        divDetails.Visible = false;
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (studentId == string.Empty)
        {
            studentId = TxtEnter.Text.Trim();
        }
        Grd.DataSource = null;
        Grd.DataBind();
        _sql = "select distinct EA.ExamID, SM.Subject, PM.Paper, EM.ExamName, EM.id,  (case when em.ExamStart>convert(datetime,getdate())  then 'Test Not Started' else  case when getdate() between em.ExamStart and em.ExamEnd and  EA.status = 0 then  'Not Attempted' else case when getdate() > em.ExamEnd and  EA.status = 0 then 'Test Left' else case when getdate()  between em.ExamStart and em.ExamEnd and  EA.status = 1 then  'Attempted' else 'Attempted' end end end end) status  from OT_ExamAllotment EA";
        _sql = _sql + " inner join AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "','" + Session["BranchCode"].ToString() + "') Reg on Reg.SRNo = EA.SRNo";
        _sql = _sql + " inner join OT_ExamMaster EM on EM.id = EA.examid  and  ea.SessionName='" + Session["SessionName"] + "' and ea.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_SubjectMaster SM on SM.id = EA.SubjectId and  sm.SessionName='" + Session["SessionName"] + "' and sm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_PaperMaster PM on PM.id = EA.PaperId and  pm.SessionName='" + Session["SessionName"] + "' and pm.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " inner join OT_AnswerMaster Am on Am.examid = EA.examid and  am.SessionName='" + Session["SessionName"] + "' and am.BranchCode=" + Session["BranchCode"] + "";
        _sql = _sql + " where  EA.SRNo = '" + studentId + "' and  em.SessionName='" + Session["SessionName"] + "' and em.BranchCode=" + Session["BranchCode"] + "  order by EM.id asc";

        SqlDataAdapter sdaa = new SqlDataAdapter(_sql, con);
        DataTable dtt = new DataTable();
        sdaa.Fill(dtt);
        if (dtt.Rows.Count > 0)
        {
            divDetails.Visible = true;
            Grd.DataSource = dtt;
            Grd.DataBind();
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label Status = (Label)Grd.Rows[i].FindControl("Status");
                Label ExamID = (Label)Grd.Rows[i].FindControl("ExamID");

                if (Status.Text.Trim() == "Not Attempted")
                {
                    Status.Text = "Pending Test";
                    Status.Visible = true;
                }
                else
                {
                    Status.Visible = true;
                }

            }
        }
    }
}