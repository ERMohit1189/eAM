using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;


public partial class website_Career : System.Web.UI.Page
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
         camp.LoadLoader(loader);

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
       // sql = "SELECT ROW_NUMBER() OVER (ORDER BY JobId ASC) AS SrNo, JobId,convert(nvarchar,Fdate,106) Fdate,convert(nvarchar,Tdate,106) as Tdate,JobTitle,DepartMent,Post,Experience,Qualification,Salary,NoofPosition,JobDescription FROM PostJob order by jobid desc";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_PostJob";
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        sql = "Select JobTitle From PostJob where JobTitle='" + txtTitle.Text.Trim().Replace("'", "") + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Change Job Title", "W");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_PostJob";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            string Fdate = txtFromDate.Text.Trim().Replace("'","");
            string Tdate = txtToDate.Text.Trim().Replace("'", "");
            cmd.Parameters.AddWithValue("@QueryFor", "I");
            cmd.Parameters.AddWithValue("@FDate", Fdate);
            cmd.Parameters.AddWithValue("@TDate", Tdate);
            cmd.Parameters.AddWithValue("@JobTitle", txtTitle.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@DepartMent", txtDep.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Post", txtPost.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Experience", txtExp.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Qualification", txtQuali.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@NoofPosition", txtNoofPos.Text.Trim().Replace("'", ""));
            cmd.Parameters.AddWithValue("@JobDescription", txtJobDisc.Text.Trim().Replace("'", ""));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            oo.MessageBox("Submitted Successfully", this.Page);
            bindData();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
            oo.ClearControls(this.Page);
            oo.MessageBox("", this.Page);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Show();

        LinkButton lnk = (LinkButton)sender;
        var currentrow = (RepeaterItem)lnk.NamingContainer;
        Label lblId = (Label)currentrow.FindControl("lblId");
        string ss = lblId.Text;
        lblID.Text = ss;

        //sql = "SELECT convert(nvarchar,Fdate,106) as Fdate,convert(nvarchar,TDate,106) as TDate,";
        //sql = sql + "JobTitle,DepartMent,Post,Experience,Qualification,Salary,NoofPosition,JobDescription FROM PostJob where JobId=" + lblId.Text;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_PostJob";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "SS");
        cmd.Parameters.AddWithValue("@JobId", lblId.Text);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Fdate0.Text = dt.Rows[0]["Fdate"].ToString();
            Tdate0.Text = dt.Rows[0]["Tdate"].ToString() ;
            txtDep0.Text = dt.Rows[0]["DepartMent"].ToString() ;
            txtJobDisc0.Text = dt.Rows[0]["JobDescription"].ToString();
            txtExp0.Text = dt.Rows[0]["Experience"].ToString();
            txtNoofPos0.Text = dt.Rows[0]["NoofPosition"].ToString();
            txtPost0.Text = dt.Rows[0]["Post"].ToString();
            txtQuali0.Text = dt.Rows[0]["Qualification"].ToString();
            txtSalary0.Text = dt.Rows[0]["Salary"].ToString();
            txtTitle0.Text = dt.Rows[0]["JobTitle"].ToString();
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
        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_PostJob";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            string Fdate = Fdate0.Text;
            string Tdate = Tdate0.Text;
            cmd.Parameters.AddWithValue("@QueryFor", "U");
            cmd.Parameters.AddWithValue("@JobId", lblID.Text);
            cmd.Parameters.AddWithValue("@FDate", Fdate);
            cmd.Parameters.AddWithValue("@TDate", Tdate);
            cmd.Parameters.AddWithValue("@JobTitle", txtTitle0.Text);
            cmd.Parameters.AddWithValue("@DepartMent", txtDep0.Text);
            cmd.Parameters.AddWithValue("@Post", txtPost0.Text);
            cmd.Parameters.AddWithValue("@Experience", txtExp0.Text);
            cmd.Parameters.AddWithValue("@Qualification", txtQuali0.Text);
            cmd.Parameters.AddWithValue("@Salary", txtSalary0.Text);
            cmd.Parameters.AddWithValue("@NoofPosition", txtNoofPos0.Text);
            cmd.Parameters.AddWithValue("@JobDescription", txtJobDisc0.Text);

           object o = cmd.ExecuteNonQuery();
           if (o != null)
           {
               bindData();
               Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated Successfully", "U");
               oo.ClearControls(this.Page);
               oo.MessageBox("", this.Page);
           }
        }
        catch (Exception ex)
        { throw ex; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_PostJob";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@QueryFor", "D");
            cmd.Parameters.AddWithValue("@JobId", lblvalue.Text);


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