using System.Data.SqlClient;
using System;


public partial class Administrator_dashbord : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();

        if (!IsPostBack)
        {
        }

    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
       
    }
}
