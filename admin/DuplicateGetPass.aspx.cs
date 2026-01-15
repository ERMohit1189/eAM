using System;
using System.Web.UI;
using System.Data.SqlClient;

public partial class admin_DuplicateGetPass :  Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadHeader("Receipt", header);
        BLL.BLLInstance.LoadHeader("Receipt", header1);
        if (!IsPostBack)
        {
            loadadat();
            
        }
    }

    public void loadadat()
    {
        sql = "Select Id as MaxId,gpd.SrNo,asr.Name as StudentName,asr.FatherName,ClassName,SectionName,gpd.LoginName,";
        sql = sql + " FamilyContactNo,Reason,GuardianName,Relation,GuardionContact,gpd.RecordDate as date,ISNULL(gpd.StudentPhotoPath,ISNULL(asr.PhotoPath,'')) as StudentPhotopath,gpd.GuardianPhotoPath";
        sql = sql + " from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr";
        sql = sql + " inner join GatePassData gpd on gpd.SrNo=asr.SrNo";
        sql = sql + " where gpd.Id='" + Session["Id"].ToString() + "' and gpd.BranchCode="+ Session["BranchCode"] + " order by id desc";
        Repeater1.DataSource = oo.GridFill(sql);
        Repeater1.DataBind();
        Repeater2.DataSource = oo.GridFill(sql);
        Repeater2.DataBind();
        
    }
    
   
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc2;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}