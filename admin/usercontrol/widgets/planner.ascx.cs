using System;

public partial class admin_usercontrol_widgets_planner : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
          
            }
        }
    }
}