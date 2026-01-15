using System.Data;
using System.Data.SqlClient;
using System;


public partial class SubscriptionActivation : System.Web.UI.Page
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
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            try
            {
                txtdob.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            catch (Exception) { }
            Load();
        }
    }
    protected void Load()
    {
        sql = "Select format(dob, 'dd-MMM-yyyy') expd, * from tblManagerInfo";
        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count>0)
        {
            txtEmpCode.Text = dt.Rows[0]["EmpCode"].ToString();
            txtEmpName.Text = dt.Rows[0]["EmpName"].ToString();
            txtdob.Text = dt.Rows[0]["expd"].ToString();
            txtContact.Text = dt.Rows[0]["Contact"].ToString();
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        if (oo.Duplicate("Select EmpCode from tblManagerInfo"))
        {
            cmd.CommandText = "update tblManagerInfo set EmpCode='"+ txtEmpCode.Text + "', EmpName='" + txtEmpName.Text + "', dob='" + txtdob.Text + "', Contact='" + txtContact.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                Load();
            }
            catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
        }
        else
        {
            cmd.CommandText = "insert into tblManagerInfo (EmpCode,EmpName,dob,Contact) values ('" + txtEmpCode.Text + "','" + txtEmpName.Text + "','" + txtdob.Text + "','" + txtContact.Text + "')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                Load();
            }
            catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
        }
        
    }
}
