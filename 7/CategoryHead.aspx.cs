using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class CategoryHead : Page
{
    public string _sql = "";
    private readonly Campus _oo = new Campus();
    private SqlConnection _con;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {
            try
            {
                GetHeadCategoryMaster();

            }
            catch(Exception ){}
        }
    }
    
    private void Reset()
    {
        txtHeadCategoryName.Text = string.Empty;
        txtHeadCategoryName0.Text = string.Empty;
    }

    
    private void GetHeadCategoryMaster()
    {
        _sql = "select * from AccHeadCategoryMaster where branchcode="+Session["BranchCode"]+ "";
        if (ddlHeadType.SelectedIndex!=0)
        {
            _sql =_sql+ " and HeadType='" + ddlHeadType.SelectedValue + "'";
        }
        
        var dt = _oo.Fetchdata(_sql);
        gvHeadMaster.DataSource = dt;
        gvHeadMaster.DataBind();
        for (int i = 0; i < gvHeadMaster.Rows.Count; i++)
        {
            Label HeadType = (Label)gvHeadMaster.Rows[i].FindControl("HeadType");
            Label HeadCategory = (Label)gvHeadMaster.Rows[i].FindControl("HeadCategory");
            LinkButton lbtnEdit = (LinkButton)gvHeadMaster.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)gvHeadMaster.Rows[i].FindControl("lbtnDelete");
            _sql = "select HeadCategory from AccDayBook where HeadType='"+ HeadType.Text.Trim() + "' and HeadCategory='" + HeadCategory.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
            Reset();
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        _sql = "SELECT HeadCategory FROM AccHeadCategoryMaster WHERE HeadType='" + ddlHeadType.SelectedValue+ "' AND HeadCategory='" + txtHeadCategoryName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Head Category!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccHeadCategoryMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@HeadType", ddlHeadType.SelectedValue);
                cmd.Parameters.AddWithValue("@HeadCategory", txtHeadCategoryName.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    Reset();
                    GetHeadCategoryMaster();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        string ss = lblId.Text;
        lblID.Text = ss;

        Label HeadType = (Label)chk.NamingContainer.FindControl("HeadType");
        Label HeadCategory = (Label)chk.NamingContainer.FindControl("HeadCategory");
        ddlHeadType0.SelectedValue= HeadType.Text;
        txtHeadCategoryName0.Text = HeadCategory.Text;
        Panel1_ModalPopupExtender.Show();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        btnNo.Focus();
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId.Text;
        lblValue.Text = ss;
        mpeDelete.Show();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
    }

    public void DeleteRecord()
    {
        _sql = "Delete from AccHeadCategoryMaster where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                Reset();
                GetHeadCategoryMaster();
            }
            catch (Exception)
            {
            }
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        _sql = "SELECT HeadType FROM AccHeadCategoryMaster WHERE HeadType='" + ddlHeadType0.SelectedValue + "' AND HeadName='" + txtHeadCategoryName0.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and id<>"+ lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Head Name!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccHeadCategoryMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@HeadType", ddlHeadType0.SelectedValue);
                cmd.Parameters.AddWithValue("@HeadCategory", txtHeadCategoryName0.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    Reset();
                    GetHeadCategoryMaster();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    protected void rblIsExam_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetHeadCategoryMaster();
    }
}