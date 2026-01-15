using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_TypeofPeriodMaster : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            DisplayGrid();
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        save();
        oo.ClearControls(abc);
    }
    public void save()
    {
        sql = "Select *from PeriodTypeMaster where PtypeName='"+txtPeriodtype.Text+"'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        if (oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate Record!", lnkSubmit);
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "PeriodTypeMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", "Insert");
            cmd.Parameters.AddWithValue("@Id", "");
            cmd.Parameters.AddWithValue("@PTypeName", txtPeriodtype.Text.Trim());
            cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text.Trim());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayGrid();
            oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
        }
    }
    public void DisplayGrid()
    {
        sql = "Select Id,PtypeName,ShortName from PeriodTypeMaster where";
        sql = sql + " SessionName='" + Session["SessionName"].ToString() + "' and BranchCode='" + Session["BranchCode"] .ToString() + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        lblId.Text = lnk.Text;
        sql = "Select PtypeName,ShortName from PeriodTypeMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblId.Text;
        txtPeriodtype1.Text = oo.ReturnTag(sql, "PtypeName");
        txtShortName1.Text = oo.ReturnTag(sql, "ShortName");
        Panel1_ModalPopupExtender.Show();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        lblId.Text = lnk.Text;
        Panel2_ModalPopupExtender.Show();
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }
    public void Update()
    {
        sql = "Select *from PeriodTypeMaster where PtypeName='" + txtPeriodtype1.Text + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "and BranchCode=" + Session["BranchCode"].ToString() + " and Id<>'" + lblId.Text + "'";
        if (oo.Duplicate(sql))
        {
            oo.MessageBoxforUpdatePanel("Duplicate Record!", lnkSubmit);
        }
        else
        {
            cmd = new SqlCommand();
            cmd.CommandText = "PeriodTypeMasterProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Type", "Update");
            cmd.Parameters.AddWithValue("@PTypeName", txtPeriodtype1.Text.Trim());
            cmd.Parameters.AddWithValue("@ShortName", txtShortName1.Text.Trim());
            cmd.Parameters.AddWithValue("@Id", lblId.Text);
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DisplayGrid();
            oo.MessageBoxforUpdatePanel("Updated successfully", lnkSubmit);
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        delete();
    }
    public void delete()
    {
        sql = "Delete From PeriodTypeMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblId.Text;
        cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        DisplayGrid();
        oo.MessageBoxforUpdatePanel("Deleted successfully", btnyes);
    }
}