using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminDayBook : Page
{
    string sql = "", _sql = "", Sql="";
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
                GetOpeningBalance();
                ddlHead.Items.Insert(0, new ListItem("<--Select-->", ""));
                ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDDChequeUTRDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                GetTodayReport();
            }
            catch (Exception)
            {
            }
        }
        
    }
    public double CheckSufficeantBalance()
    {
        double totalBalance = 0;
        Sql = "select HeadType from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode='Cash'";
        if (_oo.Duplicate(Sql))
        {
            Sql = "select top(1) Balance from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode='Cash' order by id desc";
            totalBalance = double.Parse(_oo.ReturnTag(Sql, "Balance"));
        }
        return totalBalance;
    }
    protected void GetOpeningBalance()
    {
        string openingBalae = "0.00";
        var Sql = "";
        Sql = " declare @RecordDate date ";
        Sql = Sql + " select top(1) @RecordDate=convert(date, RecordDate) from AccDayBook ";
        Sql = Sql + " where convert(date, RecordDate)<convert(date, getdate()) and BranchCode=" + Session["BranchCode"] + " ";
        Sql = Sql + " order by id desc ";
        Sql = Sql + " select top(1)  Balance from AccDayBook   ";
        Sql = Sql + " where convert(date, RecordDate)=convert(date, @RecordDate) and BranchCode=" + Session["BranchCode"] + " ";
        Sql = Sql + " and PaymentMode='Cash' order by id desc  ";
        if (_oo.Duplicate(Sql))
        {
            txtOpeningBalance.Text = _oo.ReturnTag(Sql, "Balance");
        }
        else
        {
            txtOpeningBalance.Text = openingBalae;
        }
        
    }
    private void GetHeadMaster()
    {
        ddlHead.Items.Clear();
        _sql = "select HeadName from AccHeadMaster where HeadType='" + ddlHeadType.SelectedValue + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHead, "HeadName", "HeadName");
        ddlHead.Items.Insert(0, new ListItem("<--Select-->", ""));

        ddlHeadCategory.Items.Clear();
        _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='" + ddlHeadType.SelectedValue + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHeadCategory, "HeadCategory", "HeadCategory");
        ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }

    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDDChequeUTRDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtDDChequeUTRNo.Text = string.Empty;
        txtBank.Text = string.Empty;
        ddlHead.Items.Clear();
        ddlPaymentMode.SelectedIndex = 0;
        double TotalBalance = CheckSufficeantBalance();
        if (TotalBalance == 0 && ddlHeadType.SelectedValue== "Expense")
        {
            btnInsert.Visible = false;
            btnReset.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, You do not have sufficient balance!", "A");
        }
        else
        {
            btnInsert.Visible = true;
            btnReset.Visible = true;
            GetHeadMaster();
        }
    }
    private void GetTodayReport()
    {
        Sql = "select *, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordedDate from AccDayBook where convert(date, RecordDate)=convert(date, getdate()) and branchcode=" + Session["BranchCode"] + " order by id desc";
        var dt = _oo.Fetchdata(Sql);
        gvDayBook.DataSource = dt;
        gvDayBook.DataBind();
        if (gvDayBook.Rows.Count>0)
        {
            LinkButton btnDelete = (LinkButton)gvDayBook.Rows[0].FindControl("btnDelete");
            btnDelete.Visible = true;
            double Income = 0; double Expens = 0; double Balance = 0;
            double Inc = 0; double Exp = 0; double Bal = 0;
            for (int i = gvDayBook.Rows.Count-1; i >=0; i--)
            {
                
                Label lblIncome = (Label)gvDayBook.Rows[i].FindControl("lblIncome");
                Label lblExpens = (Label)gvDayBook.Rows[i].FindControl("lblExpens");
                Label lblBalances = (Label)gvDayBook.Rows[i].FindControl("lblBalances");
				Label lblMode = (Label)gvDayBook.Rows[i].FindControl("lblMode");
				Label Label36 = (Label)gvDayBook.Rows[i].FindControl("Label36");
                LinkButton lbtnEdit = (LinkButton)gvDayBook.Rows[i].FindControl("lbtnEdit");
                if (i == (gvDayBook.Rows.Count - 1))
                {
                    Bal = Bal + double.Parse(txtOpeningBalance.Text);
                }
                if (i == 0)
                {
                    string ss = "select count(*) cnt from AccDayBook where id=" + Label36.Text.Trim() + "  and HeadName<>'Fee' and BranchCode=" + Session["BranchCode"] + " and LoginName='" + Session["LoginName"] + "' and convert(date, RecordDate)=convert(date, getdate())";
                    if (int.Parse(_oo.ReturnTag(ss, "cnt")) == 0)
                    {
                        lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                        lbtnEdit.Enabled = false;
                    }
                }
                else
                {
                    lbtnEdit.Text = "<i class='fa fa-lock'></i>";
                    lbtnEdit.Enabled = false;
                }

                if (lblMode.Text == "Cash")
                {
                    double.TryParse(lblIncome.Text, out Inc);
                    double.TryParse(lblExpens.Text, out Exp);
                    Bal = Bal + Inc;
                    Bal = Bal - Exp;
                    lblBalances.Text = Bal.ToString();
                    Income = Income + Inc;
                    Expens = Expens + Exp;
                    Balance = Balance + Bal;
                }
                
                
                
            }
            Label lblTotalIncome = (Label)gvDayBook.FooterRow.FindControl("lblTotalIncome");
            Label lblTotalExpens = (Label)gvDayBook.FooterRow.FindControl("lblTotalExpens");
            Label lblTotalBalance = (Label)gvDayBook.FooterRow.FindControl("lblTotalBalance");
            lblTotalIncome.Text = Income.ToString("0.00");
            lblTotalExpens.Text = Expens.ToString("0.00");
            lblTotalBalance.Text = Balance.ToString("0.00");
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "AccDayBookProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Date", txtDate.Text.Trim());
            cmd.Parameters.AddWithValue("@HeadType", ddlHeadType.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@HeadName", ddlHead.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@HeadCategory", ddlHeadCategory.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
            cmd.Parameters.AddWithValue("@PaymentMode", ddlPaymentMode.SelectedValue);
            cmd.Parameters.AddWithValue("@Particulars", txtDescriptionGen.Text.Trim());
            cmd.Parameters.AddWithValue("@ChequeDate", txtDDChequeUTRDate.Text.Trim());
            cmd.Parameters.AddWithValue("@ChequeNo", txtDDChequeUTRNo.Text.Trim());
            cmd.Parameters.AddWithValue("@BankName", txtBank.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                txtAmount.Text = "";
                ddlPaymentMode.SelectedIndex = 0;
                txtDDChequeUTRNo.Text = "";
                txtBank.Text = "NA";
                txtDescriptionGen.Text = "";
                GetTodayReport();
                GetOpeningBalance();
               
            }
            catch (Exception ex)
            {
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

        Label lblIncome = (Label)chk.NamingContainer.FindControl("lblIncome");
        Label lblExpens = (Label)chk.NamingContainer.FindControl("lblExpens");
        Label HeadName = (Label)chk.NamingContainer.FindControl("HeadName");
        Label HeadCategory = (Label)chk.NamingContainer.FindControl("HeadCategory");
        Label lblParticulars = (Label)chk.NamingContainer.FindControl("lblParticulars");
        string HeadType = "";
        if (double.Parse(lblIncome.Text)>0)
        {
            HeadType = "Income";
        }
        if (double.Parse(lblExpens.Text) > 0)
        {
            HeadType = "Expense";
        }
        _sql = "select HeadName from AccHeadMaster where HeadType='" + HeadType + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHead0, "HeadName", "HeadName");
        ddlHead0.Items.Insert(0, new ListItem("<--Select-->", ""));
        ddlHead0.SelectedValue = HeadName.Text.Trim();

        _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='" + HeadType + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHeadCategory0, "HeadCategory", "HeadCategory");
        ddlHeadCategory0.Items.Insert(0, new ListItem("<--Select-->", ""));
        ddlHeadCategory0.SelectedValue = HeadCategory.Text.Trim();

        

        txtParticulars0.Text = lblParticulars.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "update AccDayBook set HeadName='"+ ddlHead0.SelectedItem.Text + "', HeadCategory='" + ddlHeadCategory0.SelectedItem.Text + "', Particulars='" + txtParticulars0.Text.Trim() + "' where id="+ lblID.Text.Trim() + " and BranchCode=" + Session["BranchCode"] + " and LoginName='" + Session["LoginName"] + "' and convert(date, RecordDate)=convert(date, getdate())";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updatted successfully.", "S");
                GetTodayReport();
                Reset();
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        var chk = (LinkButton)sender;
        var Label37 = (Label)chk.NamingContainer.FindControl("Label37");
        lblValue.Text = Label37.Text;
        mpeDelete.Show();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        _sql = "Delete from AccDayBook where Id=" + lblValue.Text + "";

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
                GetTodayReport();
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


    
    private void Reset()
    {

        txtAmount.Text = string.Empty;

        txtDescriptionGen.Text = string.Empty;

        txtDDChequeUTRDate.Text = string.Empty;
        txtDDChequeUTRNo.Text = string.Empty;

        if (ddlPaymentMode.Items.Count > 0)
        {
            ddlPaymentMode.SelectedIndex = 0;
        }
        ddlHead.Items.Clear();
        ddlHeadCategory.Items.Clear();
        txtDDChequeUTRDate.Text = "";
        txtDDChequeUTRNo.Text = "";
        txtBank.Text = "";
        txtDescriptionGen.Text = "";
        txtAmount.Text = "";
        ddlHead.Items.Insert(0, new ListItem("<--Select-->", ""));
        ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
        ddlPaymentMode.SelectedIndex = 0;
        ddlHeadType.SelectedIndex = 0;


    }
}