using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class RemarkMaster : Page
{
    string sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!Page.IsPostBack)
        {
            LoadRemark();
        }
    }
    public void LoadRemark()
    {
        sql = "select id, Remark, LoginName, format(RecordDate,'dd-MMM-yyyy hh:mm:ss tt') recordDate, status from RemarksMaster where BranchCode=" + Session["BranchCode"] + " ";
        Grid.DataSource = _oo.Fetchdata(sql);
        Grid.DataBind();
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            
            Label Remark = (Label)Grid.Rows[i].FindControl("Remark");
            sql = "SELECT 1 as Yes FROM RemarksMaster WHERE Remark=" + Remark.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "union all SELECT 1 as Yes FROM Exam_NURtoPREPRemark WHERE Remark=" + Remark.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "union all SELECT 1 as Yes FROM Exam_ItoVRemark WHERE Caption=" + Remark.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "union all SELECT 1 as Yes FROM VItoVIIISelfAwareness WHERE Caption=" + Remark.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "union all SELECT 1 as Yes FROM IXtoXSelfAwareness WHERE Caption=" + Remark.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "union all SELECT 1 as Yes FROM Exam_XItoXIIRemark WHERE Caption=" + Remark.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + "";
            LinkButton lbtnEdit = (LinkButton)Grid.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)Grid.Rows[i].FindControl("lbtnDelete");
            Label lblIsLock = (Label)Grid.Rows[i].FindControl("lblIsLock");
            lblIsLock.Text = "";
            if (_oo.Duplicate(sql))
            {
                lblIsLock.Text = "1";
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
        }
    }
    protected void btnInserts_Click(object sender, EventArgs e)
    {
        sql = "SELECT 1 FROM RemarksMaster WHERE Remark='" + txtRemark.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Remark!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Sp_RemarksMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@Status", drpStatus.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    LoadRemark();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    private void Reset()
    {
        txtRemark.Text = string.Empty;
        drpStatus.SelectedValue = "Active";
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblIsLock = (Label)chk.NamingContainer.FindControl("lblIsLock");
        Label lblid = (Label)chk.NamingContainer.FindControl("lblid");
        lblEditValueId.Text = lblid.Text;

        Label Status = (Label)chk.NamingContainer.FindControl("Status");
        drpEditStatus.SelectedValue = Status.Text;

        Label lblRemark = (Label)chk.NamingContainer.FindControl("Remark");
        txtEditRemark.Text = lblRemark.Text;
        if (lblIsLock.Text == "1")
        {
            lblRemark.Enabled = false;
        }
        else
        {
            lblRemark.Enabled = true;
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "Sp_RemarksMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Id", lblEditValueId.Text.Trim());
            cmd.Parameters.AddWithValue("@Remark", txtEditRemark.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", drpEditStatus.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "Update");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                LoadRemark();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {

        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("lblid");
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "Sp_RemarksMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Id", lblValue.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "delete");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                LoadRemark();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
}