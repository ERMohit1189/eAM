using System;
using System.Data;

public partial class admin_usercontrol_widgets_Wid2 : System.Web.UI.UserControl
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {

        }
    }
}