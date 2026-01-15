using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_VisitorOut : Page
{
    Campus clsCam = new Campus();
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
#pragma warning disable 169
    string sql , ss = string.Empty;
#pragma warning restore 169
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            try
            {
                getdate();
                getdataprint();
            }
            catch (SqlException)
            { }
        }
         camp.LoadLoader(loader);
    }
    public void getdate()
    {
        oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
        oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);
        oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
        oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);
    }
    public void getdataprint()
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_Visitors";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "O");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            repeatermember.DataSource = dt;
            repeatermember.DataBind();
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        string ss = lblId.Text;
        visitorout(ss);
    }

    public void visitorout(string id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "USP_Visitors";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "U");
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
              //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('VisitorReport.aspx'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);           
               // ClientScript.RegisterStartupScript(GetType(), "alter", "alter('Checked out successfully.');window.location.href='VisitorReport.aspx'", true);
             // Response.Write("<script>alter('Checked out successfully.');</script>");
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Checked out successfully", "S");
            }
            con.Close();
            try
            {
               getdataprint();
            }
            catch (SqlException)
            { }
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string todate = "", fromdate = "";
            todate = ToDD.SelectedItem.ToString() + " " + ToMM.SelectedItem.ToString() + " " + ToYY.SelectedItem.ToString();
            fromdate = FromDD.SelectedItem.ToString() + " " + FromMM.SelectedItem.ToString() + " " + FromYY.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_Visitors";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "TF");
            cmd.Parameters.AddWithValue("@FromDate", fromdate);
            cmd.Parameters.AddWithValue("@Todate", todate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                repeatermember.DataSource = dt;
                repeatermember.DataBind();
            }
            else
            { }
        }
        catch (SqlException ee) { throw ee; }
        finally { if (con.State == ConnectionState.Open) { con.Close(); } }
    }


    //protected void btnedit_Click(object sender, EventArgs e)
    //{
    //    LinkButton chk = (LinkButton)sender;
    //    Label lblId = (Label)chk.NamingContainer.FindControl("lbledit");
    //    string ss = lblId.Text;
    //    Response.Redirect("Visitorpage.aspx?print={0}"+ ss + "", false);
    //}
}