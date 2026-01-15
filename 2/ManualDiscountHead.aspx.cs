using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class ManualDiscountHead : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader);
            loadGrid();
        }
    }
    

    protected void LinkButton1_Click(object sender, EventArgs e)
    {      
        sql= "select DiscHeadName  from ManualDiscountHeads where DiscHeadName='" + txtDiscountHeadName.Text.Trim()+"'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Entry!", "A");  
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ManualDiscountHead_proc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@DiscHeadName", txtDiscountHeadName.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");  
                oo.ClearControls(this.Page);
                loadGrid();
            }
            catch (Exception) { }
        }
    }
    
    protected void LinkButtonEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("LblidEdit");
        string ss = lblId.Text;
        lblID.Text = ss;      
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo, * from ManualDiscountHeads";
        sql = sql + " where id=" + ss;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        txtHeadNamePanel.Text = oo.ReturnTag(sql, "DiscHeadName");
        Panel1_ModalPopupExtender.Show();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "ManualDiscountHead_proc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", lblID.Text);
        cmd.Parameters.AddWithValue("@DiscHeadName", txtHeadNamePanel.Text.Trim());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@action", "update");
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
            loadGrid();
        }
        catch (SqlException) { }
    }

    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        Button8.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("LblIdDelete");
        string ss = lblId.Text;
        lblvalue.Text = ss.ToString();

        sql = "select count(*) as cnt from DiscountMaster where DiscountName='" + lblvalue.Text.Trim() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        if (int.Parse(oo.ReturnTag(sql, "cnt")) > 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Can not be delete, record already submitted for this head!", "A");
        }
        else
        {
            Panel2_ModalPopupExtender.Show();
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        sql = "Delete from ManualDiscountHeads where Id=" + lblvalue.Text;
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully.", "S");
            loadGrid();
        }
        catch (SqlException) { }

    }

    private void loadGrid()
    {
        sql = "Select  ROW_NUMBER() OVER (ORDER BY id ASC) AS SrNo,* from ManualDiscountHeads";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    
    protected void Button4_Click(object sender, EventArgs e)
    {
        
    }
}