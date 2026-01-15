using System;
using System.Data.SqlClient;

public partial class stuRootManager : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

   
}
