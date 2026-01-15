using System;

public partial class admin_server_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
            //Response.Redirect(ResolveClientUrl("~/default.aspx"));
        }
        catch (Exception)
        {
        }
    }
}