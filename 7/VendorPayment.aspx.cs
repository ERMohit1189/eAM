using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VendorPayment : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            ddlHeadCategory.Items.Clear();
            _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='Expense' and branchcode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlHeadCategory, "HeadCategory", "HeadCategory");
            ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));

            ddlHeadCategoryP.Items.Clear();
            _oo.FillDropDown_withValue(_sql, ddlHeadCategoryP, "HeadCategory", "HeadCategory");
            ddlHeadCategoryP.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
    }
    protected void loadInvoiceData()
    {
        var studentId = hfVendorId.Value;
        GridView1.DataSource = null;
        GridView1.DataBind();
        sql = "declare @temp1 as table(InvoiceNo nvarchar(50)) ";
        sql = sql + " insert into @temp1 ";
        sql = sql + " select distinct InvoiceNo from invInvoiceEntry where branchcode=" + Session["BranchCode"] + "  and isnull(status,'')<>'Cancelled' ";
        if (txtInvoiceNo.Text != "")
        {
            sql = sql + " and InvoiceNo='" + txtInvoiceNo.Text.Trim() + "'";
        }
        if (studentId != "")
        {
            sql = sql + " and VendorId='" + studentId.Trim() + "'";
        }
        sql = sql + " declare @temp as table(InvoiceNo nvarchar(50), VendorId int, amount decimal(10, 2)) ";
        sql = sql + " while(select count(*) from @temp1)>0 ";
        sql = sql + " begin ";
        sql = sql + " if not exists(select invNo from AccVendorPayment where isnull(Status,'')='Complete' and invNo= (select top(1) InvoiceNo from @temp1)) begin insert into @temp ";
        sql = sql + " select top(1) invNo, VendorId, balance from AccVendorPayment where branchcode=" + Session["BranchCode"] + " and isnull(Status,'')<>'Complete'  ";
        sql = sql + " and invNo=(select top(1) InvoiceNo from @temp1) order by id desc end";
        sql = sql + " delete top(1) from @temp1 ";
        sql = sql + " end ";
        sql = sql + " select * from (  ";
        sql = sql + " (select InvoiceNo, VendorId, sum(Total) amount from invInvoiceEntry where branchcode=" + Session["BranchCode"] + " and isnull(status,'')<>'Cancelled'  and InvoiceNo not in ";
        sql = sql + " (select invNo from AccVendorPayment where branchcode=" + Session["BranchCode"] + ") ";
        if (txtInvoiceNo.Text != "")
        {
            sql = sql + " and InvoiceNo='" + txtInvoiceNo.Text.Trim() + "'";
        }
        if (studentId != "")
        {
            sql = sql + " and VendorId='" + studentId.Trim() + "'";
        }
        sql = sql + " group by InvoiceNo, VendorId ) ";
        sql = sql + " union all ";
        sql = sql + " (select InvoiceNo, VendorId, amount from @temp))T1 ";

        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (txtInvoiceNo.Text != "" && studentId == "")
            {
                sql = "select top(1) VendorId from invInvoiceEntry where branchcode=" + Session["BranchCode"] + " and '" + txtInvoiceNo.Text.Trim() + "'";
                studentId= _oo.ReturnTag(sql, "VendorId");
            }
            lblAvilableBal.Text = CheckSufficeantBalance().ToString("0.00");
            lblAvilableAdvanceBal.Text = CheckAdvanceBalance(studentId).ToString("0.00");
            divInvoice.Visible = true;
            divControls.Visible = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            double amt = 0, tot=0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");
                Label amount = (Label)GridView1.Rows[i].FindControl("amount");
                tot = tot + double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text);
                amt = amt + double.Parse(amount.Text == "" ? "0" : amount.Text);
            }
            Label lblAmount = (Label)GridView1.FooterRow.FindControl("lblAmount");
            Label lblTotalTotal = (Label)GridView1.FooterRow.FindControl("lblTotalTotal");
            lblAmount.Text = amt.ToString("0.00");
            lblTotalTotal.Text = tot.ToString("0.00");
        }
        else
        {
            divInvoice.Visible = false;
            divControls.Visible = false;
            Reset();
        }
    }
    protected void rblInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
        if (rblInvoiceType.SelectedValue == "InvoiceWise")
        {
            divPo.Visible = true;
            divVendor.Visible = false;
        }
        else
        {
            divPo.Visible = false;
            divVendor.Visible = true;
        }
    }
    protected void txtVendorID_TextChanged(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtVendorID.Text != string.Empty && studentId != String.Empty)
        {
            loadInvoiceData();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtVendorID.Focus();
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        if (rblInvoiceType.SelectedValue == "InvoiceWise")
        {
            string sql1 = "select InvoiceNo from invInvoiceEntry where BranchCode=" + Session["BranchCode"] + " and InvoiceNo='" + txtInvoiceNo.Text.Trim() + "' and isnull(status,'')<>'Cancelled'";
            if (_oo.Duplicate(sql1))
            {
                loadInvoiceData();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Invalid Invoice No. !"), "A");
                txtVendorID.Focus();
            }
        }
        else
        {
            var studentId = hfVendorId.Value;
            if (txtVendorID.Text != string.Empty && studentId != String.Empty)
            {
                loadInvoiceData();
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
                txtVendorID.Focus();
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
    public double CheckAdvanceBalance(string vendorid)
    {
        double totalBalance = 0;
        Sql = "select top(1) Balance from AccVendorAdvancePayment where branchcode=" + Session["BranchCode"] + " and VendorId="+ vendorid + " order by id desc";
        totalBalance = double.Parse(_oo.ReturnTag(Sql, "Balance").ToString()==""?"0":_oo.ReturnTag(Sql, "Balance").ToString());
        return totalBalance;
    }


    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedIndex==0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please select payment mode!", "A");
            return;
        }
        int chkNo = 0; double totals = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label lblTotals = (Label)GridView1.Rows[i].FindControl("lblTotal");
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                chkNo = chkNo + 1;
                totals = totals + double.Parse(lblTotals.Text == "" ? "0" : lblTotals.Text);
            }
        }
        if (chkNo == 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Check atleast one from list and also fill amount > 0 in paid!", "A");
            return;
        }
        Label lblTotalPaid = (Label)GridView1.FooterRow.FindControl("lblTotalPaid");
        Label lblTotalAdvance = (Label)GridView1.FooterRow.FindControl("lblTotalAdvance");

        double TotalPaid = double.Parse(lblTotalPaid.Text == "" ? "0.00" : lblTotalPaid.Text)+ double.Parse(lblTotalAdvance.Text == "" ? "0.00" : lblTotalAdvance.Text);
        if (TotalPaid==0 && totals>0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter paid amount!", "A");
            return;
        }
        Label lblVendorId = (Label)GridView1.Rows[0].FindControl("lblVendorId");
        if ((CheckSufficeantBalance()+ CheckAdvanceBalance(lblVendorId.Text.Trim()) )< TotalPaid && ddlPaymentMode.Text.Trim()=="Cash")
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Insufficient Balance!", "A");
            return;
        }
        using (SqlCommand cmd = new SqlCommand())
        {
             int sts = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
                TextBox txtAdvance = (TextBox)GridView1.Rows[i].FindControl("txtAdvance");
                TextBox txtPaid = (TextBox)GridView1.Rows[i].FindControl("txtPaid");
                Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");

                if (chk.Checked==true && (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text)==0?1:(double.Parse(txtPaid.Text==""?"0": txtPaid.Text)+ double.Parse(txtAdvance.Text == "" ? "0" : txtAdvance.Text))) >0)
                {
                    Label InvoiceNo = (Label)GridView1.Rows[i].FindControl("InvoiceNo");
                    TextBox txtParticulars = (TextBox)GridView1.Rows[i].FindControl("txtParticulars");
                    Label amount = (Label)GridView1.Rows[i].FindControl("amount");
                    TextBox txtDiscount = (TextBox)GridView1.Rows[i].FindControl("txtDiscount");
                    Label lblBalances = (Label)GridView1.Rows[i].FindControl("lblBalances");

                    cmd.CommandText = "AccVendorPaymentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@VendorId", lblVendorId.Text.Trim());
                    cmd.Parameters.AddWithValue("@invNo", InvoiceNo.Text);
                    cmd.Parameters.AddWithValue("@ChequeNo", txtDDChequeUTRNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@BankName", txtBank.Text.Trim());
                    cmd.Parameters.AddWithValue("@paymentDate", txtDDChequeUTRDate.Text);
                    cmd.Parameters.AddWithValue("@ChequeDate", txtDDChequeUTRDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@HeadCategory", ddlHeadCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@Amount", amount.Text.Trim());
                    cmd.Parameters.AddWithValue("@Discount", (txtDiscount.Text.Trim() == "" ? "0" : txtDiscount.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Total", lblTotal.Text.Trim());
                    cmd.Parameters.AddWithValue("@Advance", (txtAdvance.Text.Trim() == "" ? "0" : txtAdvance.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Mode", ddlPaymentMode.Text.Trim());
                    cmd.Parameters.AddWithValue("@Paid", (txtPaid.Text.Trim()==""?"0": txtPaid.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Balance", lblBalances.Text.Trim());
                    cmd.Parameters.AddWithValue("@Particulars", txtParticulars.Text.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        cmd.Parameters.Clear();
                        sts = sts + 1;
                    }
                    catch (Exception ex)
                    {
                    }
                }

            }

            
            if (sts > 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                loadInvoiceData();
                return;
            }
        }
    }
    
   
    private void Reset()
    {

        hfVendorId.Value = "";
        txtInvoiceNo.Text = string.Empty;
        txtVendorID.Text = string.Empty;


        txtDDChequeUTRDate.Text = string.Empty;
        txtDDChequeUTRNo.Text = string.Empty;

        if (ddlPaymentMode.Items.Count > 0)
        {
            ddlPaymentMode.SelectedIndex = 0;
        }
        txtDDChequeUTRDate.Text = "";
        txtDDChequeUTRNo.Text = "";
        txtBank.Text = "";
        ddlPaymentMode.SelectedIndex = 0;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        divInvoice.Visible = false;
        divControls.Visible = false;

    }

    protected void txtAdvance_TextChanged(object sender, EventArgs e)
    {
        var lnk = (TextBox)sender;
        var chk = (CheckBox)lnk.NamingContainer.FindControl("chk");
        var amounts = (Label)lnk.NamingContainer.FindControl("amount");
        var txtDiscount = (TextBox)lnk.NamingContainer.FindControl("txtDiscount");
        var lblTotal = (Label)lnk.NamingContainer.FindControl("lblTotal");
        var txtPaids = (TextBox)lnk.NamingContainer.FindControl("txtPaid");
        var txtAdvances = (TextBox)lnk.NamingContainer.FindControl("txtAdvance");
        var lblBalancess = (Label)lnk.NamingContainer.FindControl("lblBalances");
        
        double AvilableBal = double.Parse(lblAvilableBal.Text == "" ? "0" : lblAvilableBal.Text);
        double AvilableAdvanceBal = double.Parse(lblAvilableAdvanceBal.Text == "" ? "0" : lblAvilableAdvanceBal.Text);
        double Tadvance = 0;
        double Tpaid = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var txtPaidsT = (TextBox)GridView1.Rows[i].FindControl("txtPaid");
            var txtAdvanceT = (TextBox)GridView1.Rows[i].FindControl("txtAdvance");
            Tpaid= Tpaid+ double.Parse(txtPaidsT.Text == "" ? "0" : txtPaidsT.Text);
            Tadvance = Tadvance + double.Parse(txtAdvanceT.Text == "" ? "0" : txtAdvanceT.Text);
        }
        if (Tpaid > AvilableBal)
        {
            txtPaids.Text = "0.00";
        }
        if (Tadvance > AvilableAdvanceBal)
        {
            txtAdvances.Text = "0.00";
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Total advance paid is greater than available advance balance!", "A");
        }
        if (double.Parse(txtDiscount.Text == "" ? "0" : txtDiscount.Text) > 0)
        {
            var total = double.Parse(amounts.Text) - double.Parse(txtDiscount.Text);
            lblTotal.Text = total.ToString("0.00");
        }
        if (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text)==0 && double.Parse(txtAdvances.Text == "" ? "0" : txtAdvances.Text) == 0)
        {
            chk.Checked = false;
            txtPaids.Text = "";
            txtAdvances.Text = "";
            lblBalancess.Text = "0.00";
        }
        else if (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text) < (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text)+ double.Parse(txtAdvances.Text == "" ? "0" : txtAdvances.Text)))
        {
            chk.Checked = false;
            txtPaids.Text = "";
            lblBalancess.Text = "0.00";
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Amount", "A");
        }
        else
        {
            chk.Checked = true;
            lblBalancess.Text = (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text) - (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text) + double.Parse(txtAdvances.Text == "" ? "0" : txtAdvances.Text))).ToString("0.00");
        }

        double desc = 0, tot=0, advnce=0, paid = 0; double Balance = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label amount = (Label)GridView1.Rows[i].FindControl("amount");
            TextBox Discount = (TextBox)GridView1.Rows[i].FindControl("txtDiscount");
            Label Total = (Label)GridView1.Rows[i].FindControl("lblTotal");
            TextBox txtAdvance = (TextBox)GridView1.Rows[i].FindControl("txtAdvance");
            TextBox txtPaid = (TextBox)GridView1.Rows[i].FindControl("txtPaid");
            Label lblBalances = (Label)GridView1.Rows[i].FindControl("lblBalances");
            desc= desc+ double.Parse(Discount.Text == "" ? "0" : Discount.Text);
            tot = tot + double.Parse(Total.Text == "" ? "0" : Total.Text);
            advnce = advnce + double.Parse(txtAdvance.Text == "" ? "0" : txtAdvance.Text);
            paid = paid + double.Parse(txtPaid.Text == "" ? "0" : txtPaid.Text);
        }
        Label lblAmount = (Label)GridView1.FooterRow.FindControl("lblAmount");
        Label lblTotalDiscount = (Label)GridView1.FooterRow.FindControl("lblTotalDiscount");
        Label lblTotalTotal = (Label)GridView1.FooterRow.FindControl("lblTotalTotal");
        Label lblTotalAdvance = (Label)GridView1.FooterRow.FindControl("lblTotalAdvance");
        Label lblTotalPaid = (Label)GridView1.FooterRow.FindControl("lblTotalPaid");
        Label lblTotalBalance = (Label)GridView1.FooterRow.FindControl("lblTotalBalance");
        lblTotalDiscount.Text = desc.ToString("0.00");
        lblTotalTotal.Text = tot.ToString("0.00");
        lblTotalAdvance.Text = advnce.ToString("0.00");
        lblTotalPaid.Text = paid.ToString("0.00");
        Balance = (double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text) - (desc + advnce + paid));
        lblTotalBalance.Text = Balance.ToString("0.00");
    }
    protected void txtPaid_TextChanged(object sender, EventArgs e)
    {
        var lnk = (TextBox)sender;
        var chk = (CheckBox)lnk.NamingContainer.FindControl("chk");
        var amounts = (Label)lnk.NamingContainer.FindControl("amount");
        var txtDiscount = (TextBox)lnk.NamingContainer.FindControl("txtDiscount");
        var lblTotal = (Label)lnk.NamingContainer.FindControl("lblTotal");
        var txtPaids = (TextBox)lnk.NamingContainer.FindControl("txtPaid");
        var txtAdvances = (TextBox)lnk.NamingContainer.FindControl("txtAdvance");
        var lblBalancess = (Label)lnk.NamingContainer.FindControl("lblBalances");

        double AvilableBal = double.Parse(lblAvilableBal.Text == "" ? "0" : lblAvilableBal.Text);
        double AvilableAdvanceBal = double.Parse(lblAvilableAdvanceBal.Text == "" ? "0" : lblAvilableAdvanceBal.Text);
        double Tadvance = 0;
        double Tpaid = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var txtPaidsT = (TextBox)GridView1.Rows[i].FindControl("txtPaid");
            var txtAdvanceT = (TextBox)GridView1.Rows[i].FindControl("txtAdvance");
            Tpaid = Tpaid + double.Parse(txtPaidsT.Text == "" ? "0" : txtPaidsT.Text);
            Tadvance = Tadvance + double.Parse(txtAdvanceT.Text == "" ? "0" : txtAdvanceT.Text);
        }
        if (Tpaid > AvilableBal)
        {
            txtPaids.Text = "0.00";
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Total paid is greater than available cash balance!", "A");
        }
        if (Tadvance > AvilableAdvanceBal)
        {
            txtAdvances.Text = "0.00";
        }
        if (double.Parse(txtDiscount.Text == "" ? "0" : txtDiscount.Text) > 0)
        {
            var total = double.Parse(amounts.Text) - double.Parse(txtDiscount.Text);
            lblTotal.Text = total.ToString("0.00");
        }
        if (double.Parse(txtDiscount.Text == "" ? "0" : txtDiscount.Text) > 0)
        {
            var total = double.Parse(amounts.Text) - double.Parse(txtDiscount.Text);
            lblTotal.Text = total.ToString("0.00");
        }
        if (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text) == 0 && double.Parse(txtAdvances.Text == "" ? "0" : txtAdvances.Text) == 0)
        {
            chk.Checked = false;
            txtPaids.Text = "";
            txtAdvances.Text = "";
            lblBalancess.Text = "0.00";
        }
        else if (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text) < (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text) + double.Parse(txtAdvances.Text == "" ? "0" : txtAdvances.Text)))
        {
            chk.Checked = false;
            txtPaids.Text = "";
            lblBalancess.Text = "0.00";
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Amount", "A");
        }
        else
        {
            chk.Checked = true;
            lblBalancess.Text = (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text) - (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text) + double.Parse(txtAdvances.Text == "" ? "0" : txtAdvances.Text))).ToString("0.00");
        }

        double desc = 0, tot = 0, advnce = 0, paid = 0; double Balance = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label amount = (Label)GridView1.Rows[i].FindControl("amount");
            TextBox Discount = (TextBox)GridView1.Rows[i].FindControl("txtDiscount");
            Label Total = (Label)GridView1.Rows[i].FindControl("lblTotal");
            TextBox txtAdvance = (TextBox)GridView1.Rows[i].FindControl("txtAdvance");
            TextBox txtPaid = (TextBox)GridView1.Rows[i].FindControl("txtPaid");
            Label lblBalances = (Label)GridView1.Rows[i].FindControl("lblBalances");
            desc = desc + double.Parse(Discount.Text == "" ? "0" : Discount.Text);
            tot = tot + double.Parse(Total.Text == "" ? "0" : Total.Text);
            advnce = advnce + double.Parse(txtAdvance.Text == "" ? "0" : txtAdvance.Text);
            paid = paid + double.Parse(txtPaid.Text == "" ? "0" : txtPaid.Text);
        }
        Label lblAmount = (Label)GridView1.FooterRow.FindControl("lblAmount");
        Label lblTotalDiscount = (Label)GridView1.FooterRow.FindControl("lblTotalDiscount");
        Label lblTotalTotal = (Label)GridView1.FooterRow.FindControl("lblTotalTotal");
        Label lblTotalAdvance = (Label)GridView1.FooterRow.FindControl("lblTotalAdvance");
        Label lblTotalPaid = (Label)GridView1.FooterRow.FindControl("lblTotalPaid");
        Label lblTotalBalance = (Label)GridView1.FooterRow.FindControl("lblTotalBalance");
        lblTotalDiscount.Text = desc.ToString("0.00");
        lblTotalTotal.Text = tot.ToString("0.00");
        lblTotalAdvance.Text = advnce.ToString("0.00");
        lblTotalPaid.Text = paid.ToString("0.00");
        Balance = (double.Parse(lblAmount.Text == "" ? "0" : lblAmount.Text) - (desc+ advnce + paid));
        lblTotalBalance.Text = Balance.ToString("0.00");
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        var lnk = (TextBox)sender;
        var chk = (CheckBox)lnk.NamingContainer.FindControl("chk");
        var amounts = (Label)lnk.NamingContainer.FindControl("amount");
        var txtDiscount = (TextBox)lnk.NamingContainer.FindControl("txtDiscount");
        var lblTotal = (Label)lnk.NamingContainer.FindControl("lblTotal");
        var txtPaids = (TextBox)lnk.NamingContainer.FindControl("txtPaid");
        var lblBalancess = (Label)lnk.NamingContainer.FindControl("lblBalances");
        if (double.Parse(txtDiscount.Text == "" ? "0" : txtDiscount.Text) > 0)
        {
            var total = double.Parse(amounts.Text) - double.Parse(txtDiscount.Text);
            lblTotal.Text = total.ToString("0.00");
        }
        if (double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text) == 0)
        {
            chk.Checked = false;
            txtPaids.Text = "";
            lblBalancess.Text = "0.00";
        }
        else if (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text) < double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text))
        {
            chk.Checked = false;
            txtPaids.Text = "";
            lblBalancess.Text = "0.00";
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Amount", "A");
        }
        else
        {
            chk.Checked = true;
            lblBalancess.Text = (double.Parse(lblTotal.Text == "" ? "0" : lblTotal.Text) - double.Parse(txtPaids.Text == "" ? "0" : txtPaids.Text)).ToString("0.00");
        }

        double desc = 0, tot = 0, paid = 0; double Balance = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label amount = (Label)GridView1.Rows[i].FindControl("amount");
            TextBox Discount = (TextBox)GridView1.Rows[i].FindControl("txtDiscount");
            Label Total = (Label)GridView1.Rows[i].FindControl("lblTotal");
            TextBox txtPaid = (TextBox)GridView1.Rows[i].FindControl("txtPaid");
            Label lblBalances = (Label)GridView1.Rows[i].FindControl("lblBalances");
            desc = desc + double.Parse(Discount.Text == "" ? "0" : Discount.Text);
            tot = tot + double.Parse(Total.Text == "" ? "0" : Total.Text);
            paid = paid + double.Parse(txtPaid.Text == "" ? "0" : txtPaid.Text);
            Balance = Balance + (double.Parse(amount.Text == "" ? "0" : amount.Text) - (double.Parse(txtPaid.Text == "" ? "0" : txtPaid.Text)+ double.Parse(Discount.Text == "" ? "0" : Discount.Text)));
        }
        Label lblTotalDiscount = (Label)GridView1.FooterRow.FindControl("lblTotalDiscount");
        Label lblTotalTotal = (Label)GridView1.FooterRow.FindControl("lblTotalTotal");
        Label lblTotalPaid = (Label)GridView1.FooterRow.FindControl("lblTotalPaid");
        Label lblTotalBalance = (Label)GridView1.FooterRow.FindControl("lblTotalBalance");
        lblTotalDiscount.Text = desc.ToString("0.00");
        lblTotalTotal.Text = tot.ToString("0.00");
        lblTotalPaid.Text = paid.ToString("0.00");
        lblTotalBalance.Text = Balance.ToString("0.00");
    }

    protected void rdoVendorAdvancePamentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoVendorAdvancePamentType.SelectedValue == "VendorAdvancePayment")
        {
            divVendorAdvancePament.Visible = true;
            divVendorPament.Visible = false;
        }
        else
        {
            divVendorAdvancePament.Visible = false;
            divVendorPament.Visible = true;
        }
    }

    protected void txtVendorIDP_TextChanged(object sender, EventArgs e)
    {
        var VendorId = hfVendorIdP.Value;
        if (txtVendorIDP.Text != string.Empty && VendorId != String.Empty)
        {
            GetAdvancePaymentReport(VendorId);
        }
        else
        {
            GetAdvancePaymentReport("0");
            Campus camp = new Campus(); camp.msgbox(this.Page, dvMSG, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtVendorIDP.Focus();
        }
    }

    protected void btnInsertP_Click(object sender, EventArgs e)
    {
        var VendorId = hfVendorIdP.Value;
        if (txtVendorIDP.Text != string.Empty && VendorId != String.Empty)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "AccVendorAdvancePaymentProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@VendorId", VendorId.Trim());
                cmd.Parameters.AddWithValue("@Date", txtDate.Text.Trim());
                cmd.Parameters.AddWithValue("@HeadType", ddlHeadTypeP.SelectedValue);
                cmd.Parameters.AddWithValue("@HeadCategory", ddlHeadCategoryP.SelectedValue);
                cmd.Parameters.AddWithValue("@Amount", txtAmountP.Text.Trim());
                cmd.Parameters.AddWithValue("@PaymentMode", ddlPaymentModeP.SelectedValue);
                cmd.Parameters.AddWithValue("@Particulars", txtDescriptionGenP.Text.Trim());
                cmd.Parameters.AddWithValue("@ChequeDate", txtDDChequeUTRDateP.Text.Trim());
                cmd.Parameters.AddWithValue("@ChequeNo", txtDDChequeUTRNoP.Text.Trim());
                cmd.Parameters.AddWithValue("@BankName", txtBankP.Text.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, dvMSG, "Submitted successfully.", "S");
                    txtAmountP.Text = "";
                    ddlPaymentModeP.SelectedIndex = 0;
                    txtDDChequeUTRNoP.Text = "";
                    txtBankP.Text = "NA";
                    txtDescriptionGenP.Text = "";
                    GetAdvancePaymentReport(VendorId);
                    ResetP();
                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvMSG, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtVendorIDP.Focus();
        }
    }

    private void ResetP()
    {
        if (ddlPaymentModeP.Items.Count > 0)
        {
            ddlPaymentModeP.SelectedIndex = 0;
        }
        txtDDChequeUTRDateP.Text = "";
        txtDDChequeUTRNoP.Text = ""; 
        txtBank.Text = "";
        txtDescriptionGenP.Text = "";
        txtAmountP.Text = "";
        ddlHeadTypeP.SelectedIndex = 0;
        ddlPaymentModeP.SelectedIndex = 0;
        ddlHeadTypeP.SelectedIndex = 0;
        _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='Expense' and branchcode=" + Session["BranchCode"] + "";
        ddlHeadCategoryP.Items.Clear();
        _oo.FillDropDown_withValue(_sql, ddlHeadCategoryP, "HeadCategory", "HeadCategory");
        ddlHeadCategoryP.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    private void GetAdvancePaymentReport(string VendorId)
    {
        Sql = "select *, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordedDate from AccVendorAdvancePayment where VendorId="+ VendorId + " and branchcode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(Sql);
        gvDayBook.DataSource = dt;
        gvDayBook.DataBind();
        if (gvDayBook.Rows.Count > 0)
        {
            double Income = 0; double Expens = 0; double Balance = 0;
            double Inc = 0; double Exp = 0; double Bal = 0;
            for (int i = gvDayBook.Rows.Count - 1; i >= 0; i--)
            {

                Label lblIncome = (Label)gvDayBook.Rows[i].FindControl("lblIncome");
                Label lblExpens = (Label)gvDayBook.Rows[i].FindControl("lblExpens");

                double.TryParse(lblIncome.Text, out Inc);
                double.TryParse(lblExpens.Text, out Exp);
                Income = Income + Inc;
                Expens = Expens + Exp;
                Balance = Balance + Bal;

            }
            Label lblTotalIncome = (Label)gvDayBook.FooterRow.FindControl("lblTotalIncome");
            Label lblTotalExpens = (Label)gvDayBook.FooterRow.FindControl("lblTotalExpens");
            Label lblTotalBalance = (Label)gvDayBook.FooterRow.FindControl("lblTotalBalance");
            lblTotalIncome.Text = Income.ToString("0.00");
            lblTotalExpens.Text = Expens.ToString("0.00");
            lblTotalBalance.Text = (Expens-Income).ToString("0.00");
        }
    }

    protected void ddlHeadTypeP_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlHeadCategoryP.Items.Clear();
        _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='"+ ddlHeadTypeP.SelectedValue+ "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHeadCategoryP, "HeadCategory", "HeadCategory");
        ddlHeadCategoryP.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
}
