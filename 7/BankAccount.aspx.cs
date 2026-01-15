using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_BankAccount : Page
{
    string sql = "", _sql = "";
    Campus _oo = new Campus();
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
                GetBankMaster();
                GetBankAcc();
            }
            catch(Exception){}
        }
    }

    private void GetBankMaster()
    {
        _sql = "Select BankName, id from AccBankMaster";
        _oo.FillDropDown_withValue(_sql, ddlBank, "BankName", "id");
        _oo.FillDropDown_withValue(_sql, ddlBank0, "BankName", "id");
        ddlBank.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlBankBranch.Items.Insert(0, new ListItem("<--Select-->", ">"));
    }
    

    private void Reset()
    {
        ddlBank.SelectedIndex = 0;
        ddlBankBranch.Items.Clear();
        ddlBankBranch.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlBankBranch0.Items.Clear();
        txtAccName.Text = "";
        txtAccName0.Text = "";
        txtAccNo.Text = "";
        txtAccNo0.Text = "";
    }
    
    private void GetBankAcc()
    {
        _sql = "select acc.*, bm.id BankId, BankName, bb.id BranchId, BankBranchName from AccBankAccount acc ";
        _sql = _sql+ "inner join AccBankMaster bm on bm.ID=acc.BankId ";
        _sql = _sql + "inner join  AccBankBranchMaster bb on bb.id=acc.BranchId where BranchCode=" + Session["BranchCode"] + " ";
        if (ddlBank.SelectedIndex!=0)
        {
            _sql = _sql + "and  acc.BankId=" + ddlBank.SelectedValue + "";

        }
        if (ddlBankBranch.SelectedIndex != 0)
        {
            _sql = _sql + "and  acc.BranchId=" + ddlBankBranch.SelectedValue + "";
        }
        gvBankAcc.DataSource = _oo.Fetchdata(_sql);
        gvBankAcc.DataBind();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        _sql = "SELECT AccountNo FROM AccBankAccount WHERE AccountNo='" + txtAccNo.Text + "' and BankId="+ ddlBank.SelectedValue + " and BranchId="+ ddlBankBranch.SelectedValue + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Account No.!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccBankAccountProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@BankId", ddlBank.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBankBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@AccountNo", txtAccNo.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountName", txtAccName.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountType", ddlAccType.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    GetBankAcc();
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

        Label BankId = (Label)chk.NamingContainer.FindControl("BankId");
        Label BranchId = (Label)chk.NamingContainer.FindControl("BranchId");
        Label AccountNo = (Label)chk.NamingContainer.FindControl("AccountNo");
        Label AccountName = (Label)chk.NamingContainer.FindControl("AccountName");
        Label AccountType = (Label)chk.NamingContainer.FindControl("AccountType");

        _sql = "Select BankName, id from AccBankMaster";
        _oo.FillDropDown_withValue(_sql, ddlBank0, "BankName", "id");
        ddlBank0.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlBank0.SelectedValue = BankId.Text;

        ddlBankBranch0.Items.Clear();
        _sql = "Select BankBranchName, id from AccBankBranchMaster where BankId=" + ddlBank0.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch0, "BankBranchName", "id");
        ddlBankBranch0.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlBankBranch0.SelectedValue = BranchId.Text;

        txtAccNo0.Text = AccountNo.Text;
        txtAccName0.Text = AccountName.Text;
        ddlAccType0.SelectedValue = AccountType.Text;
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
        _sql = "Delete from AccBankAccount where Id=" + lblValue.Text + "";

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
                GetBankAcc();
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
        _sql = "SELECT AccountNo FROM AccBankAccount WHERE AccountNo='" + txtAccNo0.Text + "' and BankId=" + ddlBank0.SelectedValue + " and BranchId=" + ddlBankBranch0.SelectedValue + " and id<>" + lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Account No.!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccBankAccountProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@BankId", ddlBank0.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBankBranch0.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@AccountNo", txtAccNo0.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountName", txtAccName0.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountType", ddlAccType0.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    GetBankAcc();
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

    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBankBranch.Items.Clear();
        _sql = "Select BankBranchName, id from AccBankBranchMaster where BankId=" + ddlBank.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch, "BankBranchName", "id");
        ddlBankBranch.Items.Insert(0, new ListItem("<--Select-->", ">"));
        GetBankAcc();
    }
    protected void ddlBankBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBankAcc();
    }

    protected void ddlBank0_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBankBranch0.Items.Clear();
        _sql = "Select BankBranchName, id from AccBankBranchMaster where BankId=" + ddlBank0.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch0, "BankBranchName", "id");
        ddlBankBranch0.Items.Insert(0, new ListItem("<--Select-->", ">"));
        Panel1_ModalPopupExtender.Show();
    }
    
}