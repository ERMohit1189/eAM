using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class ReceiptSrNoMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadbackupname();
        }
        TextBox1.Focus();
    }

    public void loadbackupname()
    {
        sql = "select BackupNameStart,remark from BackupNameStart";
        if (oo.Duplicate(sql))
        {

            TextBox1.Text = oo.ReturnTag(sql, "BackupNameStart");
            TextBox2.Text = oo.ReturnTag(sql, "remark");
        }
        else
        {
            TextBox1.Text = "";
            TextBox2.Text = "";

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == "")
        {
            oo.MessageBox("Enter Backup File Name!", this.Page);
            return;
        }
        sql = "select BackupNameStart BackupNameStart from BackupNameStart";
        if (oo.Duplicate(sql))
        {
            sql = "Update BackupNameStart set BackupnameStart='" + TextBox1.Text.ToString() + "', Remark='" + TextBox2.Text.ToString() + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            loadbackupname();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BackupNameStartProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@BackupNameStart", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", TextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loadbackupname();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            catch (SqlException ex) { }
        }
    }
    
}