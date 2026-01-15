using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class website_SpecialNotice : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
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
        camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            bindData();
        }
    }

    protected void bindData()
    {
      //  sql = " select ROW_NUMBER() OVER (ORDER BY NoticeId ASC) AS SrNo ,NoticeId,NoticeHeading,NoticeHeading2,NoticeMessage from SpecialInformation  order by noticeId desc";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_SpecialInformation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "S");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Repeater1.DataSource =dt;
            Repeater1.DataBind();
        }
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtHeading1.Text != "" && txtDescription.Text != "")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_SpecialInformation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "I");
            cmd.Parameters.AddWithValue("@NoticeHeading", txtHeading1.Text.ToString().Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@NoticeMessage", txtDescription.Text.ToString().Trim().Replace("'", ""));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(this.Page);
                oo.MessageBox("", this.Page);
                txtHeading1.Text = "";
                txtDescription.Text = "";
                bindData();
            }
            catch (SqlException ee) { throw ee; }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }
        else
        {
            txtHeading1.BorderColor = System.Drawing.Color.Red;
            txtHeading1.Focus();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Fill Notice Title  and Notice Text.", "W");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;
       // sql = " select NoticeId,NoticeHeading,NoticeHeading2,NoticeMessage from SpecialInformation where NoticeId=" + lblId.Text;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_SpecialInformation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "SS");
        cmd.Parameters.AddWithValue("@NoticeId", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtHeadingUpdate1.Text = dt.Rows[0]["NoticeHeading"].ToString();
            txtDescriptionUPdate.Text =dt.Rows[0]["NoticeMessage"].ToString();

            Panel1_ModalPopupExtender.Show();
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

    protected void btnupdate_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_SpecialInformation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "U");
        cmd.Parameters.AddWithValue("@NoticeId", lblID.Text);
        cmd.Parameters.AddWithValue("@NoticeHeading", txtHeadingUpdate1.Text.ToString().Trim().Replace("'", ""));
        cmd.Parameters.AddWithValue("@NoticeMessage", txtDescriptionUPdate.Text.ToString().Trim().Replace("'", ""));
     
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "U");
            oo.ClearControls(this.Page);

            oo.MessageBox("", this.Page);

            bindData();
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_SpecialInformation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@QueryFor", "D");
        cmd.Parameters.AddWithValue("@NoticeId", lblvalue.Text);
      
        try
        {
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
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}