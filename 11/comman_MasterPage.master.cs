using System;
using System.Data.SqlClient;
using System.Web;

public partial class comman_comman_MasterPage : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginType"].ToString().ToLower() == "staff" || Session["LoginType"].ToString().ToLower() == "guardian" || Session["LoginType"].ToString().ToLower() == "student")
        {
            DrpSessionName.Enabled = false;
        }
        Session["logout"] = null;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect("default.aspx");
    }

    protected void lnkLogout1_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect("default.aspx");
    }
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }

}
