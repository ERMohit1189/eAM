using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_AddMobileNo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if(!IsPostBack)
        {
            txtMoNo.Focus();
            getdata();
        }
    }
    
    public void getdata()
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_VISITORMONO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "S");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                repeatermember.DataSource = dt;
                repeatermember.DataBind();
            }
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            sendsinglemesg();
        }
        catch (SqlException ee) { throw ee; }
    }

    public void sendsinglemesg()
    {
        if (txtMoNo.Text != "")
        {
            sql = "SELECT MOBILENO  FROM VISITORMONO where MOBILENO='" + txtMoNo.Text + "'";
            if (oo.Duplicate(sql))
            {
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Mobile No. already exist!", "A");
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "USP_VISITORMONO";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Connection = con;
                cmd1.Parameters.AddWithValue("@QueryFor", "I");
                cmd1.Parameters.AddWithValue("@MOBILENO", txtMoNo.Text);

                try
                {
                    con.Open();
                    int n1 = cmd1.ExecuteNonQuery();
                    if (n1 > 0)
                    {
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                        oo.ClearControls(this.Page);
                        oo.MessageBox("", this.Page);
                        getdata();
                    }
                    con.Close();
                }
                catch (SqlException ee) { throw ee; }
                finally { if (con.State == ConnectionState.Open) { con.Close(); } }
            }
        }
    }

    
    
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lbldeleteeditid");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_VISITORMONO";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@QueryFor", "D");
        cmd.Parameters.AddWithValue("@ID", lblvalue.Text);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
            getdata();
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void btnedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lbldeleteeditid");
        string ss = lblId.Text;
        lblID.Text = ss;
        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_VISITORMONO";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "SU");
        cmd.Parameters.AddWithValue("Id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            TextBox1.Text = dt1.Rows[0]["MOBILENO"].ToString();
            Panel1_ModalPopupExtender.Show();
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        sql = "SELECT MOBILENO  FROM VISITORMONO where MOBILENO='" + TextBox1.Text + "'";
        if (oo.Duplicate(sql))
        {
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Mobile No. already exist!", "W");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_VISITORMONO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "U");
            cmd.Parameters.AddWithValue("@Id", lblID.Text);
            cmd.Parameters.AddWithValue("@MOBILENO", TextBox1.Text);

            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                getdata();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "U");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
            }
            catch (SqlException ee) { throw ee; }
        }
    }

}