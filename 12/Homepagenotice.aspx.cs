using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class Homepagenotice : System.Web.UI.Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;
    DataTable dt = new DataTable();
    public Homepagenotice()
    {
        con = new SqlConnection();
        oo = new Campus();
        sql = string.Empty;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        Campus camp1 = new Campus(); camp1.LoadLoader(loader);
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
        cmd.CommandText = "w_spHomepageNotice";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@Action", "select");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text != "" && txtDescription.Text != "")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "w_spHomepageNotice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                txtTitle.Text = "";
                txtDescription.Text = "";
                bindData();
            }
            catch (SqlException ee) { throw ee; }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Fill News Title and News Text.", "W");
        }
    }

    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;

        Label LabelTitle = (Label)currentrow.FindControl("LabelTitle");
        Label LabelDescription = (Label)currentrow.FindControl("LabelDescription");
        ptxtTitle.Text = LabelTitle.Text;
        ptxtDescription.Text = LabelDescription.Text;
        Panel1_ModalPopupExtender.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ptxtTitle.Text != "" && ptxtDescription.Text != "")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "w_spHomepageNotice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@Title", ptxtTitle.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Description", ptxtDescription.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "update");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                bindData();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                ptxtTitle.Text = "";
                ptxtDescription.Text = "";
            }
            catch (SqlException ee) { throw ee; }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_spHomepageNotice";
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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            txtTitle.Text = "";
            txtDescription.Text = "";
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void btnNodelete_Click(object sender, EventArgs e)
    {

    }
}