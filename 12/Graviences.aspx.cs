using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class website_GenralEnquiry : System.Web.UI.Page
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
        Campus camp1 = new Campus(); camp1.LoadLoader(loader);

        if (!IsPostBack)
        {
            bindData();
        }
    }

    protected void bindData()
    {
        //sql = "Select Id,Name,PhoneNo,Email,Subject,Remark, RecordDate from Graviences order by id Desc";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_Graviences";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "S");
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
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
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_Graviences";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@QueryFor", "D");
        cmd.Parameters.AddWithValue("@Id", lblvalue.Text);

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

}