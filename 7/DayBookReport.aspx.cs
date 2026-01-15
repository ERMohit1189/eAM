using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class DayBookReport : System.Web.UI.Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            try
            {
                ddlHead.Items.Insert(0, new ListItem("<--Select-->", ">"));
                ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
            }
            if (Request.QueryString.AllKeys.Length > 0 && Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"].ToString() == "1")
                {
                    ddlPaymentMode.SelectedValue = "";
                    GetTodayReport();
                }
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void GetOpeningBalance()
    {
        string Sdate = "";
        string Opdate = "";
        try
        {
            Sdate=DateTime.Parse(Sdate = txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
        }
        catch (Exception)
        {
            Sdate = "";
        }
        if (Sdate=="")
        {
            return;
        }
        
        GetOpeningClosing(Sdate);

    }
    private void GetHeadMaster()
    {
        ddlHead.Items.Clear();
        _sql = "select Id, HeadName from AccHeadMaster where HeadType='" + ddlHeadType.SelectedValue + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHead, "HeadName", "id");
        ddlHead.Items.Insert(0, new ListItem("<--Select-->", ">"));
        ddlHeadCategory.Items.Clear();
        _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='" + ddlHeadType.SelectedValue + "' and branchcode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(_sql, ddlHeadCategory, "HeadCategory", "HeadCategory");
        ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
        if (ddlHeadType.SelectedValue == "Income")
        {
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Composite Fee", "Composite Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("TC Fee", "TC Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("CC Fee", "CC Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Other Fee", "Other Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Additional Fee", "Additional Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Product Fee", "Product Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Admission Form Fee", "Admission Form Fee"));
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Library Fee", "Library Fee"));
        }
        if (ddlHeadType.SelectedValue == "Expense")
        {
            ddlHeadCategory.Items.Insert(ddlHeadCategory.Items.Count, new ListItem("Bulk Salary paid", "Bulk Salary paid"));
        }
    }

    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetHeadMaster();
    }
    private void GetTodayReport()
    {
        GetOpeningBalance();
        string str = "";
        heading.Text = "Day Book Report of " + txtDate.Text + "";
        Sql = "select *, format(RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordedDate from AccDayBook where convert(date, RecordDate)=convert(date, '" + txtDate.Text+"') and branchcode=" + Session["BranchCode"] + " ";
        if (ddlHeadType.SelectedIndex!=0)
        {
            str = str + " Head Type : " + ddlHeadType.SelectedItem.Text;
            Sql = Sql + " and HeadType='" + ddlHeadType.SelectedItem.Text + "'";
        }
        if (ddlHead.SelectedIndex != 0)
        {
            str = str + " |  Head : " + ddlHead.SelectedItem.Text;
            Sql = Sql + " and HeadName='" + ddlHead.SelectedItem.Text + "'";
        }
        if (ddlHeadCategory.SelectedIndex != 0)
        {
            str = str + " |  Head Category : " + ddlHeadCategory.SelectedItem.Text;
            Sql = Sql + " and HeadCategory='" + ddlHeadCategory.SelectedItem.Text + "'";
        }
        if (ddlPaymentMode.SelectedIndex != 0 && ddlHeadType.SelectedIndex != 0)
        {
            str = str + " | Mode : " + ddlPaymentMode.SelectedItem.Text;
            Sql = Sql + " and PaymentMode='" + ddlPaymentMode.SelectedItem.Text + "'";
        }
        if (ddlPaymentMode.SelectedIndex != 0 && ddlHeadType.SelectedIndex == 0)
        {
            str = str + " Mode : " + ddlPaymentMode.SelectedItem.Text;
            Sql = Sql + " and PaymentMode='" + ddlPaymentMode.SelectedItem.Text + "'";
        }
        Sql = Sql+ " order by id asc";
        if (ddlPaymentMode.SelectedIndex == 0 && ddlHeadType.SelectedIndex == 0)
        {
            str = str + " Print by : " + Session["LoginName"].ToString();
        }
        else
        {
            str = str + " | Print by : " + Session["LoginName"].ToString();
        }
        lblRegister.Text = str;
        var dt = _oo.Fetchdata(Sql);
        gvDayBook.DataSource = dt;
        gvDayBook.DataBind();
        if (gvDayBook.Rows.Count > 0)
        {
            abc.Visible = true;
            divExport.Visible = true;
            double Income = 0; double Expens = 0; double Balance = 0;
            double Inc = 0; double Exp = 0; double Bal = 0;
            for (int i = 0; i < gvDayBook.Rows.Count; i++)
            {


                if (i == 0)
                {
                    Bal = Bal + double.Parse(lblOpening.Text);
                }
                Label lblIncome = (Label)gvDayBook.Rows[i].FindControl("lblIncome");
                Label lblExpens = (Label)gvDayBook.Rows[i].FindControl("lblExpens");
                Label lblBalances = (Label)gvDayBook.Rows[i].FindControl("lblBalances");
				Label lblMode = (Label)gvDayBook.Rows[i].FindControl("lblMode");

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
        else
        {
            abc.Visible = false;
            divExport.Visible = false;
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "DayBookReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "DayBookReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "DayBookReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        GetTodayReport();
    }

    private void GetOpeningClosing(string dat)
    {
        var Sql = "";
        string openingBalae = "0.00";
        string closingBalae = "0.00";
        Sql = " declare @RecordDate date ";
        Sql = Sql + " select top(1) @RecordDate=convert(date, RecordDate) from AccDayBook ";
        Sql = Sql + " where convert(date, RecordDate)<convert(date, '"+ dat + "') and BranchCode=" + Session["BranchCode"] + " ";
        Sql = Sql + " order by id desc ";
        Sql = Sql + " select top(1)  Balance from AccDayBook  ";
        Sql = Sql + " where convert(date, RecordDate)=convert(date, @RecordDate) and BranchCode=" + Session["BranchCode"] + " ";
        Sql = Sql + "  and PaymentMode='Cash' order by id desc ";
        if (_oo.Duplicate(Sql))
        {
            lblOpening.Text = _oo.ReturnTag(Sql, "Balance");
        }
        else
        {
            lblOpening.Text = openingBalae;
        }

        Sql = " select top(1)  Balance from AccDayBook  ";
        Sql = Sql + " where convert(date, RecordDate)=convert(date, '" + dat + "') and BranchCode=" + Session["BranchCode"] + " and PaymentMode='Cash' ";
        Sql = Sql + " order by id desc ";
        if (_oo.Duplicate(Sql))
        {
            lblClosing.Text = _oo.ReturnTag(Sql, "Balance");
        }
        else
        {
            lblClosing.Text = closingBalae;
        }
    }

}