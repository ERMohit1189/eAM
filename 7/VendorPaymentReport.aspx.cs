using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VendorPaymentReport : System.Web.UI.Page
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
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                ddlHeadCategory.Items.Clear();
                _sql = "select HeadCategory from AccHeadCategoryMaster where HeadType='Expense' and branchcode=" + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue(_sql, ddlHeadCategory, "HeadCategory", "HeadCategory");
                ddlHeadCategory.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
            catch (Exception)
            {
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void txtVendorID_TextChanged(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtVendorID.Text == string.Empty && studentId == String.Empty)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtVendorID.Focus();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GetTodayReport();
    }
    private void GetTodayReport()
    {
        string str = "";
        heading.Text = "Vendor Payment Report (from " + txtDate.Text + " to " + txtToDate.Text + ")";
        Sql = "select v.OrganizationName Vendor, vi.InvoiceDate, vi.InvoiceNo, isnull(VP.HeadCategory, 'NA') HeadCategory, CASE WHEN isnull(VP.HeadCategory, 'NA')='na' AND isnull(VP.Amount,0)=0 THEN vi.GrandTotal ELSE isnull(VP.Amount,0) END Amount, isnull(Paid,0) Paid, isnull(discount,0) discount, CASE WHEN isnull(VP.HeadCategory, 'NA') = 'na' AND isnull(VP.Amount, 0)= 0 THEN vi.GrandTotal ELSE isnull(Balance, 0) END Balance from AccVendor v  ";
        Sql = Sql + " inner join   ";
        Sql = Sql + " (  ";
        Sql = Sql + " select VendorId, format(convert(date,InvoiceDate),'dd-MMM-yyyy') InvoiceDate, isnull(InvoiceNo, 'NA') InvoiceNo, GrandTotal,BranchCode  ";
        Sql = Sql + " from invInvoiceEntry where convert(date, InvoiceDate) between convert(date, '" + txtDate.Text + "') and convert(date, '" + txtToDate.Text + "') AND BranchCode=" + Session["BranchCode"] + "  ";
        Sql = Sql + " GROUP BY VendorId, format(convert(date,InvoiceDate),'dd-MMM-yyyy'), isnull(InvoiceNo, 'NA'), GrandTotal,BranchCode  ";
        Sql = Sql + " )vi on vi.VendorID=v.id  and v.BranchCode=vi.BranchCode   ";
        Sql = Sql + " left join   ";
        Sql = Sql + " (  ";
        Sql = Sql + " select VendorId, HeadCategory, invNo, sum(isnull(Amount,0)) Amount, sum(isnull(Paid,0)) Paid, Sum(isnull(Balance,0)) Balance, Sum(isnull(discount,0)) discount,BranchCode  ";
        Sql = Sql + " from accVendorPayment where BranchCode=" + Session["BranchCode"] + " group by VendorId, HeadCategory, invNo,BranchCode, Paid, Balance, discount,Amount  ";
        Sql = Sql + " ) vp on vp.VendorID=v.id and v.BranchCode=vp.BranchCode and vp.invNo=vi.InvoiceNo   ";
        Sql = Sql + " where convert(date, vi.InvoiceDate) between convert(date, '" + txtDate.Text + "') and convert(date, '" + txtToDate.Text + "')  and v.BranchCode=" + Session["BranchCode"] + "   ";
        if (!String.IsNullOrEmpty(hfVendorId.Value) && !String.IsNullOrEmpty(txtVendorID.Text.Trim()))
        {
            Sql = Sql + " and v.id=" + hfVendorId.Value + " ";
        }
        if (String.IsNullOrEmpty(hfVendorId.Value) && !String.IsNullOrEmpty(txtVendorID.Text.Trim()))
        {
            Sql = Sql + " and (v.VendorCode='" + txtVendorID.Text.Trim() + "' or v.OrganizationName='" + txtVendorID.Text.Trim() + "') ";
        }
        if (ddlHeadCategory.SelectedIndex > 0)
        {
            Sql = Sql + " and isnull(VP.HeadCategory, 'NA')='" + ddlHeadCategory.SelectedValue + "' ";
        }

        var dt = _oo.Fetchdata(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (ddlHeadCategory.SelectedIndex != 0)
            {
                str = str + "Head Category : " + ddlHeadCategory.SelectedItem.Text;
            }
            lblRegister.Text = str;

            gvDayBook.DataSource = dt;
            gvDayBook.DataBind();
            abc.Visible = true;
            divExport.Visible = true;
            double amount = 0; double dicount = 0; double paid = 0; double Balance = 0;
            double amt = 0; double dis = 0; double pad = 0; double Bal = 0;
            for (int i = 0; i < gvDayBook.Rows.Count; i++)
            {
                Label lblAmount = (Label)gvDayBook.Rows[i].FindControl("lblAmount");
                Label lblDiscount = (Label)gvDayBook.Rows[i].FindControl("lblDiscount");
                Label lblPaid = (Label)gvDayBook.Rows[i].FindControl("lblPaid");
                Label lblBalance = (Label)gvDayBook.Rows[i].FindControl("lblBalance");
                double.TryParse(lblAmount.Text, out amt);
                double.TryParse(lblPaid.Text, out dis);
                double.TryParse(lblPaid.Text, out pad);
                double.TryParse(lblBalance.Text, out Bal);
               
                amount = amount + amt;
                dicount = dicount + dis;
                paid = paid + pad;
                Balance = Balance + Bal;
            }
            Label lblTotalAmount = (Label)gvDayBook.FooterRow.FindControl("lblTotalAmount");
            Label lblTotalDiscount = (Label)gvDayBook.FooterRow.FindControl("lblTotalDiscount");
            Label lblTotalPaid = (Label)gvDayBook.FooterRow.FindControl("lblTotalPaid");
            Label lblTotalBalance = (Label)gvDayBook.FooterRow.FindControl("lblTotalBalance");
            lblTotalAmount.Text = amount.ToString("0.00");
            lblTotalDiscount.Text = dicount.ToString("0.00");
            lblTotalPaid.Text = paid.ToString("0.00");
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
        _oo.ExportTolandscapeWord(Response, "VendorPaymentReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "VendorPaymentReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "VendorPaymentReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

}