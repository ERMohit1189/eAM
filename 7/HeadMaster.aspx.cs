using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_HeadMaster : Page
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
                LoadHeadCategory();
                GetHeadMaster();

            }
            catch(Exception ){}
        }
    }
    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHeadCategory();
    }
    protected void ddlHeadType0_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHeadCategory1();
    }
    protected void LoadHeadCategory()
    {
        divHeadCat.Visible = false;
        rbHeadCategory.Items.Clear();
        if (ddlHeadType.SelectedIndex == 0)
        {
            divHeadCat.Visible = false;
        }
        if (ddlHeadType.SelectedIndex == 1)
        {
            rbHeadCategory.Items.Add("General");
            rbHeadCategory.SelectedIndex = 0;
        }
        if (ddlHeadType.SelectedIndex == 2)
        {
            rbHeadCategory.Items.Add("General");
            rbHeadCategory.SelectedIndex = 0;
        }
    }
    protected void LoadHeadCategory1()
    {
        rbHeadCategory0.Items.Clear();
        if (ddlHeadType0.SelectedIndex == 1)
        {
            rbHeadCategory0.Items.Add("General");
            rbHeadCategory0.SelectedIndex = 0;
        }
        if (ddlHeadType0.SelectedIndex == 2)
        {
            rbHeadCategory0.Items.Add("General");
            rbHeadCategory0.SelectedIndex = 0;
        }
        Panel1_ModalPopupExtender.Show();
    }

    private void Reset()
    {
        txtHeadName.Text = string.Empty;
        txtHeadName0.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRemark0.Text = string.Empty;
    }

    
    private void GetHeadMaster()
    {
        _sql = "select * from AccHeadMaster where branchcode="+Session["BranchCode"]+"";
        var dt = _oo.Fetchdata(_sql);
        gvHeadMaster.DataSource = dt;
        gvHeadMaster.DataBind();
        for (int i = 0; i < gvHeadMaster.Rows.Count; i++)
        {
            Label HeadName = (Label)gvHeadMaster.Rows[i].FindControl("HeadName");
            LinkButton lbtnEdit = (LinkButton)gvHeadMaster.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)gvHeadMaster.Rows[i].FindControl("lbtnDelete");
            _sql = "select HeadName from AccDayBook where HeadName='" + HeadName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (_oo.Duplicate(_sql))
            {
                lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                lbtnDelete.Text = "<i class='fa fa-lock'></i>";
                lbtnEdit.Enabled = false;
                lbtnDelete.Enabled = false;
            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        _sql = "SELECT HeadType FROM AccHeadMaster WHERE HeadType='" + ddlHeadType.SelectedValue+ "' AND HeadName='" + txtHeadName.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Head Name!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccHeadMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@HeadName", txtHeadName.Text.Trim());
                cmd.Parameters.AddWithValue("@HeadType", ddlHeadType.SelectedValue);
                cmd.Parameters.AddWithValue("@HeadCategory", rbHeadCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", rblIsActive.SelectedValue);
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
                    GetHeadMaster();
                    Reset();
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
        Label HeadName = (Label)chk.NamingContainer.FindControl("HeadName");
        Label HeadCategory = (Label)chk.NamingContainer.FindControl("HeadCategory");
        Label Remark = (Label)chk.NamingContainer.FindControl("Remark");
        Label IsActive = (Label)chk.NamingContainer.FindControl("IsActive");

        ddlHeadType0.SelectedValue= HeadType.Text;
        txtHeadName0.Text = HeadName.Text;
        rbHeadCategory0.Items.Clear();
        if (HeadType.Text == "Income")
        {
            rbHeadCategory0.Items.Add("General");
        }
        if (HeadType.Text == "Expense")
        {
            rbHeadCategory0.Items.Add("General");
        }
        rbHeadCategory0.SelectedValue = HeadCategory.Text;
        txtRemark0.Text = Remark.Text;
        rblIsActive0.SelectedValue = (IsActive.Text== "Active"?"1":"0");

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
        _sql = "Delete from AccHeadMaster where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + "";

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
                GetHeadMaster(); 
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
        _sql = "SELECT HeadType FROM AccHeadMaster WHERE HeadType='" + ddlHeadType0.SelectedValue + "' AND HeadName='" + txtHeadName0.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and id<>"+ lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Head Name!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccHeadMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@HeadName", txtHeadName0.Text.Trim());
                cmd.Parameters.AddWithValue("@HeadType", ddlHeadType0.SelectedValue);
                cmd.Parameters.AddWithValue("@HeadCategory", rbHeadCategory0.SelectedValue);
                cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", rblIsActive0.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    GetHeadMaster();
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
}