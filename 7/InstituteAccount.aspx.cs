using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_InstituteAccount : Page
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
            _sql = "select AcceptNegativeBalance, OpeningAmount, OpeningAmountRemark from BranchTab where BranchId=" + Session["BranchCode"] + "";
            txtOpeningAmount.Text = (_oo.ReturnTag(_sql, "OpeningAmount")==""?"0": _oo.ReturnTag(_sql, "OpeningAmount"));
            txtOpeningAmountRemark.Text = _oo.ReturnTag(_sql, "OpeningAmountRemark");
            var sts = _oo.ReturnTag(_sql, "AcceptNegativeBalance");
            if (sts == "True")
            {
                chkAcceptNegativeOrNot.Checked = true;
            }
            else
            {
                chkAcceptNegativeOrNot.Checked = false;
            }
            GetBank();
            GetBankBranch();

            GetInstituteAccount();
        }
    }

    private void GetBank()  
    {
        _sql = "Select BankName, id from AccBankMaster";
        _oo.FillDropDown_withValue(_sql, ddlBankName, "BankName", "id");
        ddlBankName.Items.Insert(0, new ListItem("<--Select-->", ">"));
    }

    private void GetBankBranch()
    {
        _sql = "Select * from AccBankBranchMaster where BankId=" + ddlBankName.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch, "BankBranchName", "id");
        ddlBankBranch.Items.Insert(0, new ListItem("<--Select-->", ">"));

        
    }

    private void GetBank0()
    {
        _sql = "Select BankName, id from AccBankMaster";
        _oo.FillDropDown_withValue(_sql, ddlBankName0, "BankName", "id");
        ddlBankName0.Items.Insert(0, new ListItem("<--Select-->", ">"));
    }

    private void GetBankBranch0()
    {
        _sql = "Select BankBranchName, id from AccBankBranchMaster where BankId=" + ddlBankName0.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch0, "BankBranchName", "id");
        ddlBankBranch0.Items.Insert(0, new ListItem("<--Select-->", ">"));
    }

    private void Reset()
    {
        txtAccountName.Text = string.Empty;
        txtAccountNo.Text = string.Empty;
        txtAddress0.Text = string.Empty;
        txtIFSC.Text = string.Empty;
        txtIFSC0.Text = string.Empty;
        txtPIN0.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRemark0.Text = string.Empty;

        ddlBankName.SelectedIndex = 0;
        ddlBankName0.Items.Clear();
    }

    
    private void GetInstituteAccount()
    {
        _sql = "select acc.*, bm.id BankId, BankName, bb.id BranchId, BankBranchName from AccInstituteAccount acc ";
        _sql = _sql + "inner join AccBankMaster bm on bm.ID=acc.BankId ";
        _sql = _sql + "inner join  AccBankBranchMaster bb on bb.id=acc.BranchId where BranchCode=" + Session["BranchCode"] + " ";
        if (ddlBankName.SelectedIndex != 0)
        {
            _sql = _sql + "and  acc.BankId=" + ddlBankName.SelectedValue + "";

        }
        if (ddlBankBranch.SelectedIndex != 0)
        {
            _sql = _sql + "and  acc.BranchId=" + ddlBankBranch.SelectedValue + "";
        }
        gvBankBranchList.DataSource = _oo.Fetchdata(_sql);
        gvBankBranchList.DataBind();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        _sql = "SELECT AccountNo FROM AccInstituteAccount WHERE AccountNo='" + txtAccountNo.Text + "' and BankId=" + ddlBankName.SelectedValue + " and BranchId=" + ddlBankBranch.SelectedValue + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Account No.!", "A");
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccInstituteAccountProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@BankId", ddlBankName.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBankBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@AccountNo", txtAccountNo.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountName", txtAccountName.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountType", rblAccountType.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@OpeningAmount", txtAccOpeningAmount.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@IFSC", txtIFSC.Text.Trim());
                cmd.Parameters.AddWithValue("@PostalCode", txtPIN.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", rblIsActive.SelectedValue);
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
                    GetInstituteAccount();
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
        Label OpeningAmount = (Label)chk.NamingContainer.FindControl("OpeningAmount");
        Label Address = (Label)chk.NamingContainer.FindControl("Address");
        Label IFSC = (Label)chk.NamingContainer.FindControl("IFSC");
        Label PostalCode = (Label)chk.NamingContainer.FindControl("PostalCode");
        Label Remark = (Label)chk.NamingContainer.FindControl("Remark");
        Label IsActive = (Label)chk.NamingContainer.FindControl("IsActive");


        _sql = "Select BankName, id from AccBankMaster";
        _oo.FillDropDown_withValue(_sql, ddlBankName0, "BankName", "id");
        ddlBankName0.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlBankName0.SelectedValue = BankId.Text;

        ddlBankBranch0.Items.Clear();
        _sql = "Select BankBranchName, id from AccBankBranchMaster where BankId=" + ddlBankName0.SelectedValue + "";
        _oo.FillDropDown_withValue(_sql, ddlBankBranch0, "BankBranchName", "id");
        ddlBankBranch0.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlBankBranch0.SelectedValue = BranchId.Text;

        txtAccountNo0.Text = AccountNo.Text;
        txtAccountName0.Text = AccountName.Text;
        rblAccountType0.SelectedValue = AccountType.Text;
        txtAccOpeningAmount0.Text = OpeningAmount.Text;
        txtAddress0.Text = Address.Text;
        txtIFSC0.Text = IFSC.Text;
        txtPIN0.Text = PostalCode.Text;
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
        DeleteRecord(btnYes);
    }

    public void DeleteRecord(Control cntrl)
    {
        _sql = "Delete from AccInstituteAccount where Id=" + lblValue.Text + "";

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
                GetInstituteAccount();
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
        _sql = "SELECT AccountNo FROM AccInstituteAccount WHERE AccountNo='" + txtAccountNo0.Text + "' and BankId=" + ddlBankName0.SelectedValue + " and BranchId=" + ddlBankBranch0.SelectedValue + " and id<>" + lblID.Text + "";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate Account No.!", "A");
            Panel1_ModalPopupExtender.Show();
        }
        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccInstituteAccountProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@id", lblID.Text.Trim());
                cmd.Parameters.AddWithValue("@BankId", ddlBankName0.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBankBranch0.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@AccountNo", txtAccountNo0.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountName", txtAccountName0.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountType", rblAccountType0.Text.Trim());
                cmd.Parameters.AddWithValue("@OpeningAmount", txtAccOpeningAmount0.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress0.Text.Trim());
                cmd.Parameters.AddWithValue("@IFSC", txtIFSC0.Text.Trim());
                cmd.Parameters.AddWithValue("@PostalCode", txtPIN0.Text.Trim());
                cmd.Parameters.AddWithValue("@Remark", txtRemark0.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", rblIsActive0.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "update");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                    GetInstituteAccount();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBankBranch();
    }
    protected void ddlBankName0_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBankBranch0();
        Panel1_ModalPopupExtender.Show();
    }

    protected void ddlBankBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        _sql = "Select Address, IFSC, PIN from AccBankBranchMaster where BankId=" + ddlBankName.SelectedValue + " and Id="+ ddlBankBranch.SelectedValue + "";

        txtAddress.Text = _oo.ReturnTag(_sql, "Address");
        txtIFSC.Text = _oo.ReturnTag(_sql, "IFSC");
        txtPIN.Text = _oo.ReturnTag(_sql, "PIN");
    }
    protected void ddlBankBranch0_SelectedIndexChanged1(object sender, EventArgs e)
    {
        _sql = "Select Address, IFSC, PIN from AccBankBranchMaster where BankId=" + ddlBankName0.SelectedValue + " and Id=" + ddlBankBranch0.SelectedValue + "";

        txtAddress0.Text = _oo.ReturnTag(_sql, "Address");
        txtIFSC0.Text = _oo.ReturnTag(_sql, "IFSC");
        txtPIN0.Text = _oo.ReturnTag(_sql, "PIN");
        Panel1_ModalPopupExtender.Show();
    }



    protected void btnAddBankAccount_Click(object sender, EventArgs e)
    {
        rw1.Visible = true;
        rw2.Visible = true;
        rw3.Visible = true;
        rw4.Visible = true;
        btnBack.Visible = true;

        btnAddBankAccount.Visible = false;
        rw0.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        rw1.Visible = false;
        rw2.Visible = false;
        rw3.Visible = false;
        rw4.Visible = false;
        btnBack.Visible = false;

        btnAddBankAccount.Visible = true;
        rw0.Visible = true;
    }
    protected void btnInsertOpeningAmount_Click(object sender, EventArgs e)
    {
        _sql = "SELECT BranchId FROM BranchTab WHERE (IsOpeningAmountFilled<>0 OR IsOpeningAmountFilled <> NULL) AND BranchId=" + Session["BranchCode"].ToString() + "";
        if (_oo.Duplicate(_sql))
        {
            _sql = "update BranchTab set OpeningAmountRemark='" + txtOpeningAmountRemark.Text + "' where BranchId=" + Session["BranchCode"] + "";
            var cmd1 = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = _sql,
                Connection = _con
            };
            try
            {
                _con.Open();
                cmd1.ExecuteNonQuery();
                _con.Close();
                _sql = "select AcceptNegativeBalance, OpeningAmount, OpeningAmountRemark from BranchTab where BranchId=" + Session["BranchCode"] + "";
                txtOpeningAmount.Text = (_oo.ReturnTag(_sql, "OpeningAmount") == "" ? "0" : _oo.ReturnTag(_sql, "OpeningAmount"));
                txtOpeningAmountRemark.Text = _oo.ReturnTag(_sql, "OpeningAmountRemark");
                var sts = _oo.ReturnTag(_sql, "AcceptNegativeBalance");
                if (sts == "True")
                {
                    chkAcceptNegativeOrNot.Checked = true;
                }
                else
                {
                    chkAcceptNegativeOrNot.Checked = false;
                }
                Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Opening Balance Amount Already submitted, Only remark submitted successfully.", "A");
            }
            catch (SqlException ex)
            {
            }
            
        }
        else
        {
            _sql = "update BranchTab set OpeningAmount=" + txtOpeningAmount.Text.Trim() + ",IsOpeningAmountFilled=1,OpeningAmountRemark='" + txtOpeningAmountRemark.Text+ "' where BranchId=" + Session["BranchCode"] + "";

            var cmd1 = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = _sql,
                Connection = _con
            };
            try
            {
                _con.Open();
                cmd1.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Submitted successfully.", "S");
            }
            catch (SqlException ex) {
            }
        }
    }

    protected void chkAcceptNegativeOrNot_CheckedChanged(object sender, EventArgs e)
    {
        _sql = "update BranchTab set AcceptNegativeBalance="+ (chkAcceptNegativeOrNot.Checked==false?"0":"1") + " where BranchId=" + Session["BranchCode"] + "";

        var cmd1 = new SqlCommand
        {
            CommandType = CommandType.Text,
            CommandText = _sql,
            Connection = _con
        };

        try
        {

            _con.Open();
            cmd1.ExecuteNonQuery();
            _con.Close();
            _sql = "select AcceptNegativeBalance, OpeningAmount, OpeningAmountRemark from BranchTab where BranchId=" + Session["BranchCode"] + "";
            txtOpeningAmount.Text = (_oo.ReturnTag(_sql, "OpeningAmount") == "" ? "0" : _oo.ReturnTag(_sql, "OpeningAmount"));
            txtOpeningAmountRemark.Text = _oo.ReturnTag(_sql, "OpeningAmountRemark");
            var sts = _oo.ReturnTag(_sql, "AcceptNegativeBalance");
            if (sts == "True")
            {
                chkAcceptNegativeOrNot.Checked = true;
            }
            else
            {
                chkAcceptNegativeOrNot.Checked = false;
            }
            Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Submitted successfully.", "S");
        }
        catch (SqlException ex) { }
    }
}