using System.Web.UI;
using System.Data.SqlClient;
using System;

public partial class admin_ListofAllVehicles : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {

         if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
         con = oo.dbGet_connection();
        //Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {

            sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,Id,Agency  ,Country  ,State  ,City  ,PhoneNo  ,Email,";
            sql = sql + "      Address  ,Owner  ,";
            sql = sql + "   ContactPerson  ,ContactNo  ,SessionName  ,BranchCode ,LoginName,RecordDate from DealerMaster";
            sql = sql + "  where  SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "ListofAllDealers.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("ListofAllDealers.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}