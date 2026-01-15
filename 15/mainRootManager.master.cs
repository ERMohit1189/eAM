using System;
using System.Data.SqlClient;

public partial class mainRootManager : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SiteMapPath1.SiteMapProvider = "W5";
        }
    }
}
