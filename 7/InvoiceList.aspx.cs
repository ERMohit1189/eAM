using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_InvoiceList : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;
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
                GetInvoice();
            }
            catch(Exception){}
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void txtSearchBy_TextChanged(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {
            GetInvoice();
            pnlcontrols.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            pnlcontrols.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetInvoice();
    }

    private void GetInvoice()
    {
        var VendorId = hfVendorId.Value;
        gvInvoice.DataSource = null;
        gvInvoice.DataBind();
        abc.Visible = true;
        divExport.Visible = true;
        sql = sql + "select subject+' ('+InvoiceNo+')'InvoiceNo, inv.VendorId, sum(inv.Amount) Amount,sum(Tax) Tax,sum(inv.Total)Total, sum(isnull(vp.discount,0))discount, (sum(inv.Total)-sum(isnull(vp.discount,0)))GrandTotal, sum(isnull(vp.Paid,0))Paid, (sum(inv.Total)-sum(isnull(vp.discount,0)))-sum(isnull(vp.Paid,0))Balance, OrganizationName+' ('+VendorCode+')' Vendor, FilePath, format(convert(date, inv.RecordedDate), 'dd-MMM-yyyy') Date, isnull(inv.Status, 'Pending') Status from invInvoiceEntry inv ";
        sql = sql + " inner join AccVendor vn on vn.id=inv.VendorId and vn.BranchCode=inv.BranchCode ";
        sql = sql + " left join accVendorPayment vp on vp.VendorId=inv.VendorId and vp.invNo=inv.InvoiceNo and vn.BranchCode=inv.BranchCode ";
        sql = sql + " where inv.BranchCode=" + Session["BranchCode"] + "  and convert(date, inv.RecordedDate) between '"+txtFromdate.Text.Trim()+"' and '"+ txtTodate.Text.Trim() + "' ";
        if (VendorId!="")
        {
            sql = sql + " and inv.VendorId='" + VendorId.Trim() + "' ";
        }
        if (ddlStatus.SelectedIndex!=0)
        {
            sql = sql + " and isnull(inv.Status, 'Pending')='" + ddlStatus.SelectedValue + "' ";
        }
        sql = sql + " group by InvoiceNo, inv.VendorId,OrganizationName,VendorCode, FilePath, subject, convert(date, inv.RecordedDate), isnull(inv.Status, 'Pending') ";
        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            heading.Text = "List of Invoices";
            lblRegister.Text ="Date : "+ DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            gvInvoice.DataSource = dt;
            gvInvoice.DataBind();
            pnlcontrols.Visible = true;
        }
        else
        {
            txtSearchBy.Text = "";
            hfVendorId.Value = "";
            pnlcontrols.Visible = false;
            abc.Visible = false;
            divExport.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "ListOfInvoice", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "ListOfInvoice.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "ListOfInvoice", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        var lbtnDonwload = (HyperLink)chk.NamingContainer.FindControl("lbtnDonwload");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "print", "window.open('" + lbtnDonwload.NavigateUrl.Replace("~/", "../../") + "','_blank');", true);
    }
}