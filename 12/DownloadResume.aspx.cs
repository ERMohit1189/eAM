using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class website_DownloadResume : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt = new DataTable();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp1 = new Campus(); camp1.LoadLoader(loader);

        if (!IsPostBack)
        {
            loadjobTitel();
        }
    }

    protected void bindData()
    {
        Repeater1.DataSource = null;
        Repeater1.DataBind();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spJobApplication";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
        cmd.Parameters.AddWithValue("@AppliedFor", DropDownList1.SelectedItem.Text.ToString());
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            divList.Visible = true;
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        else
        {
            divList.Visible = false;
        }
    }

    public void loadjobTitel()
    {
        sql = "Select JobTitle from w_JobPosting";
        oo.FillDropDown(sql, DropDownList1, "JobTitle");
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        string ss = lblId.Text;

        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "w_spJobApplication";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Action", "delete");
            cmd.Parameters.AddWithValue("@AppId", lblvalue.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "W");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
            bindData();
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    
}