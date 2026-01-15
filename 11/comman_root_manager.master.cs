using System;
using System.Data.SqlClient;

public partial class comman_comman_root_manager : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            SiteMapPath1.SiteMapProvider = "W1";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            SiteMapPath1.SiteMapProvider = "W2";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            SiteMapPath1.SiteMapProvider = "W3";
        }
#pragma warning disable 219
        string str = "";
#pragma warning restore 219
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
}
