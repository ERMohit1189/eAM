using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Master_Exampattern_ng : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
           DisplayGrid();                      
        }
    }



    public void DisplayGrid()
    {
        sql = "Select * from master_ExamPattern where BranchCode=" + Session["BranchCode"] + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    
    public void save()
    {
        cmd = new SqlCommand();
        cmd.CommandText = "usp_master_ExamPattern";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "save");
        cmd.Parameters.AddWithValue("@pattern", txtExamPattern.Text);
        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
        }
        catch(Exception ex)
        {
        }
    }
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;
        Label id = (Label)gvr.FindControl("LblIdForEdit");
        sql = "Select pattern from master_ExamPattern where id=" + id.Text + " and BranchCode=" + Session["BranchCode"] + "";
        txtExamPatternUpdate.Text = oo.ReturnTag(sql, "pattern");
        LblForUpdate.Text = id.Text;
        Panel1_ModalPopupExtender.Show();
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        sql = "Select pattern from master_ExamPattern where pattern='" + txtExamPatternUpdate.Text + "' and BranchCode=" + Session["BranchCode"] + "";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
        }

        else if (txtExamPatternUpdate.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Enter Exam Pattern", "A");
        }

        else
        {
            update();
            DisplayGrid();
        }
    }
    public void update()
    {
        cmd = new SqlCommand();
        cmd.CommandText = "usp_master_ExamPattern";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@QueryFor", "update");
        cmd.Parameters.AddWithValue("@id", LblForUpdate.Text);
        cmd.Parameters.AddWithValue("@pattern", txtExamPatternUpdate.Text);
        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully.", "S");
        }
        catch (Exception ex)
        {
        }
    }



    protected void LinkDelete_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(sender as Control).Parent.Parent;
        Label LblIdForDelete = (Label)gvr.FindControl("LblIdForDelete");
        LblidForDelete.Text = LblIdForDelete.Text;
        Panel2_ModalPopupExtender.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sql = "delete master_exampattern where id=" + LblidForDelete.Text+" and BranchCode="+Session["BranchCode"] +"";
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
            DisplayGrid();
        }
        catch (SqlException) { }
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        sql = "Select pattern from master_ExamPattern where pattern='" + txtExamPattern.Text + "' and BranchCode=" + Session["BranchCode"] + "";

        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, Duplicate Entry!", "A");
        }

        else if (txtExamPattern.Text == "")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Enter Exam Pattern", "A");
        }

        else
        {
            save();
            DisplayGrid();
        }
    }
}