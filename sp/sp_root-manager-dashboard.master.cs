using System;
using System.Data.SqlClient;

public partial class students_admin_root_manager : System.Web.UI.MasterPage
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
