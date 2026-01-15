using System.Data;
using System.Data.SqlClient;
using System;

public partial class admin_TruncateTable : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {  
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
    if (!IsPostBack)
    {
    }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "Truncate table " + TextBox1.Text.ToString() + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Deleted successfully.", this.Page);
            oo.ClearControls(this.Page);
        }
        catch (Exception) { oo.MessageBox("Table Not Found!", this.Page); con.Close(); }
    }
}