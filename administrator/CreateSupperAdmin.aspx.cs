using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class CreateSupperAdmin : System.Web.UI.Page
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

            dataDisplay();
        }

    }
    protected void dataDisplay()
    {
        sql = "Select Name, FatherName, ContactNo, Email, UserId, Password, BranchCode, DisplayName, case when IsActive=1 then 'Active' else 'Inactive' end IsActive from NewSuperAdminInformation e inner join LoginTab l on l.LoginName=e.UserId and l.BranchId=e.BranchCode  and LoginTypeId=1";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            LinkButton lnkDelete = (LinkButton)Grd.Rows[i].FindControl("LinkButton3");
            string sqls = "select LoginTypeId from LoginTab where LoginTypeId=2";
            if (oo.Duplicate(sqls))
            {
                lnkDelete.Text = "<i class='fa fa-lock'></i>";
                lnkDelete.Enabled = false;
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("UserIdE");
        string ss = lblId2.Text;
        lblID.Text = ss;

        sql = "Select Name, FatherName, ContactNo, Email, UserId, Password, BranchCode, DisplayName, l.IsActive from NewSuperAdminInformation e inner join LoginTab l on l.LoginName=e.UserId and l.BranchId=e.BranchCode  and LoginTypeId=1";
        sql = sql + "  where e.UserId='" + ss+"'";
        txtPanelName.Text = oo.ReturnTag(sql, "Name");
        txtPanelFatherName.Text = oo.ReturnTag(sql, "FatherName");
        txtPanelContactNo.Text = oo.ReturnTag(sql, "ContactNo");
        txtPanelEmail.Text = oo.ReturnTag(sql, "Email");
        txtPanelPassword.Text = oo.ReturnTag(sql, "Password");
        txtPanelDisplayName.Text = oo.ReturnTag(sql, "DisplayName");
        ddlPanelStatus.SelectedValue = (oo.ReturnTag(sql, "IsActive")=="True"?"1":"0");

        ModalPopupExtender1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from NewSuperAdminInformation where UserId='" + lblvalue.Text+"'";

        int val = 0;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            val = 1;
        }
        catch (SqlException ex) { }
        string sql2 = "Delete from LoginTab where LoginName='" + lblvalue.Text + "' and LoginTypeId=1";
        SqlCommand cmd2 = new SqlCommand();
        cmd2.CommandText = sql2;
        cmd2.CommandType = CommandType.Text;
        cmd2.Connection = con;
        try
        {
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            if (val == 1)
            {
                dataDisplay();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            }
        }
        catch (SqlException) { }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("UserIdD");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        if (oo.Duplicate("Select  * from NewSuperAdminInformation where UserId='" + TextUserId.Text.Trim() + "'"))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Username!", "A");
        }
        else if (oo.Duplicate("Select  * from NewSuperAdminInformation where Email='" + txtEmail.Text.Trim() + "'"))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Email Address!", "A");
        }
        else
        {
            cmd.CommandText = "sp_CreateSuperAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text.Trim());
            cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", TextUserId.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", ddlStatus.SelectedValue);
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                txtName.Text = "";
                txtFatherName.Text = "";
                txtContactNo.Text = "";
                txtEmail.Text = "";
                TextUserId.Text = "";
                txtPassword.Text = "";
                txtDisplayName.Text = "";
                ddlStatus.SelectedIndex = 0;
                dataDisplay();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            }
            catch (SqlException ee) { oo.MessageBox(ee.Message.ToString(), this.Page); }
        }
    }
    
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "sp_CreateSuperAdmin";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Name", txtPanelName.Text.Trim());
        cmd.Parameters.AddWithValue("@FatherName", txtPanelFatherName.Text.Trim());
        cmd.Parameters.AddWithValue("@ContactNo", txtPanelContactNo.Text.Trim());
        cmd.Parameters.AddWithValue("@Email", txtPanelEmail.Text.Trim());
        cmd.Parameters.AddWithValue("@Password", txtPanelPassword.Text.Trim());
        cmd.Parameters.AddWithValue("@UserId", lblID.Text.Trim());
        cmd.Parameters.AddWithValue("@DisplayName", txtPanelDisplayName.Text.Trim());
        cmd.Parameters.AddWithValue("@IsActive", ddlPanelStatus.SelectedValue);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            txtPanelName.Text = "";
            txtPanelFatherName.Text = "";
            txtPanelContactNo.Text = "";
            txtPanelEmail.Text = "";
            lblID.Text = "";
            txtPanelPassword.Text = "";
            txtPanelDisplayName.Text = "";
            ddlPanelStatus.SelectedIndex = 0;
            dataDisplay();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        catch (SqlException ex) { }
    }
}