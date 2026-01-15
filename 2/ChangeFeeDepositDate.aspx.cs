using System;
using System.Data.SqlClient;
using System.Globalization;

public partial class ChangeFeeDepositDate : System.Web.UI.Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql, sql1 = String.Empty;

    public ChangeFeeDepositDate()
    {
        con = new SqlConnection();
        oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
            oo.AddDateMonthYearDropDown(DDYearC, DDMonthC, DDDateC);
            oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);
            oo.FindCurrentDateandSetinDropDown(DDYearC, DDMonthC, DDDateC);
            btnUpate.Enabled = false;
        }
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYear, DDMonth, DDDate);
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYear, DDMonth, DDDate);

    }
    protected void DDYearC_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYearC, DDMonthC, DDDateC);
    }
    protected void DDMonthC_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYearC, DDMonthC, DDDateC);

    }
    protected void Show_Click(object sender, EventArgs e)
    {
        loadData();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void loadData()
    {
        if (TextBox1.Text != "")
        {
            TextBox1.Text = TextBox1.Text.Replace(" ", string.Empty).Trim();
        }
        sql = "select top 1 srno, left(convert(nvarchar,DepositDate,106),2) as DD,Right(left(convert(nvarchar,DepositDate,106),6),3) as MM, RIGHT(convert(nvarchar,DepositDate,106),4) as YY";
        sql = sql + " from CompositFeeDeposit where ReceiptNo='" + TextBox1.Text + "' and receiptStatus='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,DepositDate,106),2) as DD,Right(left(convert(nvarchar,DepositDate,106),6),3) as MM, RIGHT(convert(nvarchar,DepositDate,106),4) as YY ";
        sql = sql + " from OtherFeeDeposit where Receipt_no='" + TextBox1.Text + "' and PaymentSatus='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,FeeDepositeDate,106),2) as DD,Right(left(convert(nvarchar,FeeDepositeDate,106),6),3) as MM, RIGHT(convert(nvarchar,FeeDepositeDate,106),4) as YY ";
        sql = sql + " from Other_fee_collection_1 where Receipt_no='" + TextBox1.Text + "' and Status='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,AdmissionFromDate,106),2) as DD,Right(left(convert(nvarchar,AdmissionFromDate,106),6),3) as MM, RIGHT(convert(nvarchar,AdmissionFromDate,106),4) as YY ";
        sql = sql + " from AdmissionFormCollection where RecieptNo='" + TextBox1.Text + "' and Status='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,DepositDate,106),2) as DD,Right(left(convert(nvarchar,DepositDate,106),6),3) as MM, RIGHT(convert(nvarchar,DepositDate,106),4) as YY ";
        sql = sql + " from UniformFeeDeposit where Receipt_no='" + TextBox1.Text + "' and PaymentSatus='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,AdmissionFromDate,106),2) as DD,Right(left(convert(nvarchar,AdmissionFromDate,106),6),3) as MM, RIGHT(convert(nvarchar,AdmissionFromDate,106),4) as YY ";
        sql = sql + " from AdmissionFormCollection where RecieptNo='" + TextBox1.Text + "' and Status='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,TCIssueDate,106),2) as DD,Right(left(convert(nvarchar,TCIssueDate,106),6),3) as MM, RIGHT(convert(nvarchar,TCIssueDate,106),4) as YY ";
        sql = sql + " from TCCollection where RecieptNo='" + TextBox1.Text + "' and Status='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        sql = sql + " union all";
        sql = sql + " select srno, left(convert(nvarchar,CCissuedate,106),2) as DD,Right(left(convert(nvarchar,CCissuedate,106),6),3) as MM, RIGHT(convert(nvarchar,CCissuedate,106),4) as YY ";
        sql = sql + " from CCCollection where RecieptNo='" + TextBox1.Text + "' and Status='Paid' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";

        if (oo.Duplicate(sql))
        {
            divDate.Visible = true;
            btnUpate.Enabled = true;
            DDYear.Text = oo.ReturnTag(sql, "YY");
            DDMonth.Text = oo.ReturnTag(sql, "MM");
            DDDate.Text = oo.ReadDD(oo.ReturnTag(sql, "DD"));
        }
        else
        {
            divDate.Visible = false;
            btnUpate.Enabled = false;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Reciept No!", "A");
        }
    }
    protected void btnUpate_Click(object sender, EventArgs e)
    {
        string fd = "";
        fd = DDYearC.SelectedItem + "/" + DDMonthC.SelectedItem + "/" + DDDateC.SelectedItem;
        sql = "update CompositFeeDeposit set DepositDate='" + fd + "' where ReceiptNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update OtherFeeDeposit set DepositDate='" + fd + "' where Receipt_no='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update Other_fee_collection_1 set FeeDepositeDate='" + fd + "' where Receipt_no='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update AdmissionFormCollection set AdmissionFromDate='" + fd + "' where RecieptNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update UniformFeeDeposit set DepositDate='" + fd + "' where Receipt_no='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update AdmissionFormCollection set AdmissionFromDate='" + fd + "' where RecieptNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update TCCollection set TCIssueDate='" + fd + "' where RecieptNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        sql = sql + " update CCCollection set CCissuedate='" + fd + "' where RecieptNo='" + TextBox1.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
        oo.ProcedureDatabase(sql);
        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
        loadData();
    }
    public override void Dispose()
    {
        con.Dispose();
        oo.Dispose();
    }
}