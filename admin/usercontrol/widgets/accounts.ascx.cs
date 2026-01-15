using System;

public partial class admin_usercontrol_widgets_Wid2 : System.Web.UI.UserControl
{
    readonly Campus _oo = new Campus();
    string Sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {

                Label1.Text = GetOpeningBalance();
                Label2.Text = GetIncome();
                Label3.Text = GetExpens();
                Label4.Text = GetCloasingBalance();
                Label5.Text = GetIncomeOther();

                lblDiscount.Text = GetDiscount();
            }
        }
    }
    public string GetOpeningBalance()
    {
        string openingBalae = "0.00";
        Sql = " declare @RecordDate date ";
        Sql += " select top(1) @RecordDate=convert(date, RecordDate) from AccDayBook ";
        Sql += " where convert(date, RecordDate)<convert(date, getdate()) and BranchCode=" + Session["BranchCode"] + " and Balance is not null";
        Sql += " order by id desc ";
        Sql += " select top(1)  Balance from AccDayBook   ";
        Sql += " where convert(date, RecordDate)=convert(date, @RecordDate) and BranchCode=" + Session["BranchCode"] + " and PaymentMode='Cash' and Balance is not null";
        Sql += " order by id desc ";
        if (_oo.Duplicate(Sql))
        {
            openingBalae = _oo.ReturnTag(Sql, "Balance");
        }
        return openingBalae;

    }
    public string GetCloasingBalance()
    {
        string CloasingBalance = "0.00";
        Sql = " select top(1)  Balance from AccDayBook  ";
        Sql += " where balance is not null and convert(date, RecordDate)<=convert(date, getdate()) and BranchCode=" + Session["BranchCode"] + " and PaymentMode='Cash' and Balance is not null";
        Sql += " order by id desc ";
        if (_oo.Duplicate(Sql))
        {
            CloasingBalance = _oo.ReturnTag(Sql, "Balance");
        }
        return CloasingBalance;
    }
    public string GetIncome()
    {
        string Income = "0.00";
        Sql = "select HeadType from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode='Cash' and convert(date, RecordDate)=convert(date, getdate())";
        if (_oo.Duplicate(Sql))
        {
            Sql = "select sum(Income) Income from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode='Cash' and convert(date, RecordDate)=convert(date, getdate())";
            Income = _oo.ReturnTag(Sql, "Income");
        }
        return Income;

    }

    public string GetIncomeOther()
    {
        string Income = "0.00";
        Sql = "select HeadType from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode!='Cash' and convert(date, RecordDate)=convert(date, getdate())";
        if (_oo.Duplicate(Sql))
        {
            Sql = "select sum(Income) Income from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode!='Cash' and convert(date, RecordDate)=convert(date, getdate())";
            Income = _oo.ReturnTag(Sql, "Income");
        }
        return Income;

    }
    public string GetExpens()
    {
        string Expens = "0.00";
        Sql = "select HeadType from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode='Cash' and convert(date, RecordDate)=convert(date, getdate())";
        if (_oo.Duplicate(Sql))
        {
            Sql = "select sum(Expens) Expens from AccDayBook where branchcode=" + Session["BranchCode"] + " and PaymentMode='Cash' and convert(date, RecordDate)=convert(date, getdate())";
            Expens = _oo.ReturnTag(Sql, "Expens");
        }
        return Expens;

    }

    /* Hari Om Thapa Dated : 16-March-2021 */
    public string GetDiscount()
    {
        string Discounts = "0.00";
        Sql = "EXEC GetAllDiscountByDate @SessionName='" + Session["SessionName"] + "', @BranchCode=" + Session["BranchCode"] + "";
        var dt = _oo.Fetchdata(Sql);
        if (dt.Rows.Count > 0)
        {
            Discounts = dt.Rows[0]["Total"].ToString();
        }
        return Discounts;

    }

}