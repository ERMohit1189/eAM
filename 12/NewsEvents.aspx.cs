using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Web.UI;

public partial class website_NewsEvents : System.Web.UI.Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;
    DataTable dt = new DataTable();
    public website_NewsEvents()
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
        string frtodate = DateTime.Now.ToString("dd-MMM-yyyy");
        txtFromDate.Text = frtodate;
        txtToDate.Text = frtodate;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "w_SpNewsEvents";
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
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label LabelNoIcon = (Label)Repeater1.Items[i].FindControl("LabelNoIcon");
                Image imgIcon = (Image)Repeater1.Items[i].FindControl("imgIcon");
                if (dt.Rows[i]["IconStatus"].ToString().ToLower() == "true")
                {
                    LabelNoIcon.Visible = false;
                    imgIcon.Visible = true;
                }
            }

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
            cmd.CommandText = "w_SpNewsEvents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            string DateFrom = "";
            string DateTo = "";
            DateFrom = DateTime.Parse(txtFromDate.Text).ToString("dd-MMM-yyyy");
            DateTo = DateTime.Parse(txtToDate.Text).ToString("dd-MMM-yyyy");
            cmd.Parameters.AddWithValue("@FromDate", DateFrom);
            cmd.Parameters.AddWithValue("@Todate", DateTo);
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@IconStatus", chkIcon.Checked==true?"1":"0");
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                string frtodate = DateTime.Now.ToString("dd-MMM-yyyy");
                txtFromDate.Text = frtodate;
                txtToDate.Text = frtodate;
                txtTitle.Text = "";
                txtDescription.Text = "";
                chkIcon.Checked = false;
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

        Label LabelFromDate = (Label)currentrow.FindControl("LabelFromDate");
        Label LabelTodate = (Label)currentrow.FindControl("LabelTodate");
        Label LabelTitle = (Label)currentrow.FindControl("LabelTitle");
        Label LabelDescription = (Label)currentrow.FindControl("LabelDescription");
        Label LabelNoIcon = (Label)currentrow.FindControl("LabelNoIcon");
        Image imgIcon = (Image)currentrow.FindControl("imgIcon");

        ptxtFromDate.Text = DateTime.Parse(LabelFromDate.Text).ToString("dd-MMM-yyyy");
        ptxtToDate.Text = DateTime.Parse(LabelTodate.Text).ToString("dd-MMM-yyyy");
        ptxtTitle.Text = LabelTitle.Text;
        ptxtDescription.Text = LabelDescription.Text;
        pchkIcon.Checked = imgIcon.Visible == true ? true : false;
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
            cmd.CommandText = "w_SpNewsEvents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            string pFromDate = DateTime.Parse(ptxtFromDate.Text).ToString("dd-MMM-yyyy");
            string pToDate = DateTime.Parse(ptxtToDate.Text).ToString("dd-MMM-yyyy");
            cmd.Parameters.AddWithValue("@FromDate", pFromDate);
            cmd.Parameters.AddWithValue("@Todate", pToDate);
            cmd.Parameters.AddWithValue("@Title", ptxtTitle.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Description", ptxtDescription.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@IconStatus", pchkIcon.Checked == true ? "1" : "0");
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"]);
            cmd.Parameters.AddWithValue("@Action", "update");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                bindData();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
                string frtodate = DateTime.Now.ToString("dd-MMM-yyyy");
                ptxtFromDate.Text = frtodate;
                ptxtToDate.Text = frtodate;
                ptxtTitle.Text = "";
                ptxtDescription.Text = "";
                pchkIcon.Checked = false;
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
        cmd.CommandText = "w_SpNewsEvents";
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
            string frtodate = DateTime.Now.ToString("dd-MMM-yyyy");
            txtFromDate.Text = frtodate;
            txtToDate.Text = frtodate;
            txtTitle.Text = "";
            txtDescription.Text = "";
            chkIcon.Checked = false;
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void btnNodelete_Click(object sender, EventArgs e)
    {

    }
}