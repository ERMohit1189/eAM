using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;


public partial class admin_ChangePassword : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            txtNewPanel.Focus();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txtNewPanel.Text.Trim() != TextConfNewPassw.Text.Trim())
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Passwords not matched!", "A");
        }
        else
        {
            try
            {
                string qry = "update AlumniRegistration set Password='" + txtNewPanel.Text.Trim() + "' where  Password=" + "'" + Session["Password"] + "' and ContactNo='" + Session["LoginName"] + "' ";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = qry;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Password changed successfully.", "S");
                txtNewPanel.Text = "";
                TextConfNewPassw.Text = "";
            }
            catch (SqlException ee) { }
        }
    }
}
