using System;
using System.Data.SqlClient;

public partial class staff_root_manager : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_preinit(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            //sql = "SELECT FORMAT(getdate(), 'dddd, MMMM dd, yyyy') as date";
            //Label5.Text = oo.ReturnTag(sql, "date");
        }
    }


    //protected void UpdateTimer_Tick(object sender, EventArgs e)
    //{
    //    sql = "SELECT FORMAT(GETDATE(), 'hh') as hh,FORMAT(GETDATE(), 'mm') as mm,FORMAT(GETDATE(), 'ss') as ss,FORMAT(GETDATE(), 'tt') as tt";
    //    Label1.Text = oo.ReturnTag(sql, "hh");
    //    Label2.Text = oo.ReturnTag(sql, "mm");
    //    Label3.Text = oo.ReturnTag(sql, "ss");
    //    Label4.Text = oo.ReturnTag(sql, "tt");
    //}
}
