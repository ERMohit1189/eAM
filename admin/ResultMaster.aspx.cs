using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
 
public partial class ResultMaster : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if (!IsPostBack)
        {
            DisplayGrid();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "Select ResultText from ResultMaster where ResultText='"+ txtResultText.Text.Trim() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Result Text already exists", "A");       
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ResultMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ResultText", txtResultText.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayGrid();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submited successfully", "S");
            }
            catch (SqlException) { }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId2 = (Label)chk.NamingContainer.FindControl("Label36");
        Label lblResultText = (Label)chk.NamingContainer.FindControl("lblResultText");
        lblID.Text = lblId2.Text;
        txtResultTextPanel.Text = lblResultText.Text;
        Panel1_ModalPopupExtender.Show();
    }
    
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId3.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "Delete from ResultMaster where Id=" + lblvalue.Text;
        sql +=  " and BranchCode=" + Session["BranchCode"].ToString() + "";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayGrid();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");       
        }
        catch (SqlException) { }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        sql = "Select id from ResultMaster where ResultText='" + txtResultTextPanel.Text.Trim() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and Id<>'" + lblID.Text + "'";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Result Text already exists!", "A");       
            DisplayGrid();
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ResultMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", lblID.Text);
            cmd.Parameters.AddWithValue("@ResultText", txtResultTextPanel.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "update");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayGrid();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");       
            }
            catch (Exception) { }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    public void DisplayGrid()
    {
        sql = "select Id, ResultText from ResultMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }

}