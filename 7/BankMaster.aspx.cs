using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_BankMaster : Page
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
            GetBankMaster();
        }
    }

    private void Reset()
    {
        txtBankName.Text = string.Empty;
        txtBankName0.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRemark0.Text = string.Empty;
    }
    
    
    private void GetBankMaster()
    {
        _sql = "select * from AccBankMaster";
        var dt = _oo.Fetchdata(_sql);
        gvBankList.DataSource = dt;
        gvBankList.DataBind();
        for (int i = 0; i < gvBankList.Rows.Count; i++)
        {
            Label Bankid = (Label)gvBankList.Rows[i].FindControl("Label36");
            LinkButton lbtnEdit = (LinkButton)gvBankList.Rows[i].FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)gvBankList.Rows[i].FindControl("lbtnDelete");
            _sql = "SELECT BankBranchName FROM AccBankBranchMaster WHERE Bankid='" + Bankid.Text + "'";
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
        _sql = "SELECT BankName FROM AccBankMaster WHERE BankName='" + txtBankName.Text + "'";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Bank Name!", "A");
        }
        else
        { 
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccBankMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", rblIsActive.SelectedValue);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    GetBankMaster();
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

        Label BankName = (Label)chk.NamingContainer.FindControl("BankName");
        Label Remark = (Label)chk.NamingContainer.FindControl("Remark");
        Label IsActive = (Label)chk.NamingContainer.FindControl("IsActive");

        txtBankName0.Text = BankName.Text;
        txtRemark0.Text = Remark.Text;
        rblIsActive0.SelectedValue = (IsActive.Text == "Active" ? "1" : "0");
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
        _sql = "Delete from AccBankMaster where Id=" + lblValue.Text + "";

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
                GetBankMaster();
                Reset();
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
        _sql = "SELECT BankName FROM AccBankMaster WHERE BankName='" + txtBankName0.Text + "' and id<>" + lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Bank Name!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccBankMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@BankName", txtBankName0.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", rblIsActive0.SelectedValue);
                cmd.Parameters.AddWithValue("@Action", "Update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    GetBankMaster();
                    Reset();
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