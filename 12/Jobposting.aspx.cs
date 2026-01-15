using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Drawing;


public partial class Jobposting : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            bindData();
        }
    }

    protected void bindData()
    {

        Repeater1.DataSource = null;
        Repeater1.DataBind();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spJobPosting";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GRID.Visible = true;
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        else
        {
            GRID.Visible = false;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "w_spJobPosting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Action", "insert");
                cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                cmd.Parameters.AddWithValue("@Department", txtDepartment.Text);
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                cmd.Parameters.AddWithValue("@JobDescription", txtJobDescription.Text);
                cmd.Parameters.AddWithValue("@NoofPost", txtNoofPost.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    bindData();
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    oo.ClearControls(this.Page);
                }
                catch (SqlException ee) { throw ee; }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Linkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblids");
        string ss = lblId.Text;
        lblID.Text = ss;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spJobPosting";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@id", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ptxtFromDate.Text= dt.Rows[0]["FromDate"].ToString();
            ptxtToDate.Text= dt.Rows[0]["ToDate"].ToString();
            ptxtJobTitle.Text= dt.Rows[0]["JobTitle"].ToString();
            ptxtDepartment.Text= dt.Rows[0]["Department"].ToString();
            ptxtDesignation.Text= dt.Rows[0]["Designation"].ToString();
            ptxtExperience.Text= dt.Rows[0]["Experience"].ToString();
            ptxtQualification.Text= dt.Rows[0]["Qualification"].ToString();
            ptxtSalary.Text= dt.Rows[0]["Salary"].ToString();
            ptxtJobDescription.Text= dt.Rows[0]["JobDescription"].ToString();
            ptxtNoofPost.Text= dt.Rows[0]["NoofPost"].ToString();
            Panel1_ModalPopupExtender.Show();
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "w_spJobPosting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Action", "update");
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@FromDate", ptxtFromDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", ptxtToDate.Text);
            cmd.Parameters.AddWithValue("@JobTitle", ptxtJobTitle.Text);
            cmd.Parameters.AddWithValue("@Department", ptxtDepartment.Text);
            cmd.Parameters.AddWithValue("@Designation", ptxtDesignation.Text);
            cmd.Parameters.AddWithValue("@Experience", ptxtExperience.Text);
            cmd.Parameters.AddWithValue("@Qualification", ptxtQualification.Text);
            cmd.Parameters.AddWithValue("@Salary", ptxtSalary.Text);
            cmd.Parameters.AddWithValue("@JobDescription", ptxtJobDescription.Text);
            cmd.Parameters.AddWithValue("@NoofPost", ptxtNoofPost.Text);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());

            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                bindData();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
            }
            catch (SqlException ee) { throw ee; }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblids");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spJobPosting";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Action", "delete");
        cmd.Parameters.AddWithValue("@id", lblvalue.Text);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindData();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    public override void Dispose()
    {
        dt.Dispose();
    }
    
}