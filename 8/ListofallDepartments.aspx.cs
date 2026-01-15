using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_ListofallDepartments : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    { if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
    con = oo.dbGet_connection();
    Campus camp = new Campus(); camp.LoadLoader(loader); 

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {
            sql = "Select  Row_Number() over (order by EmpDepId Asc) as SrNo,EmpDepName from EmpDepMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("ListofAllDepartment.xls", GridView1);

    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        //ScriptManager.RegisterClientScriptBlock(ImageButton4,this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>",false);
        ScriptManager.RegisterClientScriptBlock(ImageButton4, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }



    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        
        oo.ExportToWord(Response, "ListOfDepartment.doc", abc);
    }
}