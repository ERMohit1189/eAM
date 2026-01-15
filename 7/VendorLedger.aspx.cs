using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;

public partial class admin_VendorLedger : Page
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
            catch (Exception) { }
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
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Please enter Vendor Name or Code !"), "A");
            pnlcontrols.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var studentId = hfVendorId.Value;
        if (txtSearchBy.Text != string.Empty && studentId != String.Empty)
        {
            GetInvoice();
            pnlcontrols.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, BLL.BLLInstance.FetchMSG("Please enter Vendor Name or Code !"), "A");
            pnlcontrols.Visible = false;
        }
    }

    private void GetInvoice()
    {
        sql = "";
        var VendorId = hfVendorId.Value;
        gvInvoice.DataSource = null;
        gvInvoice.DataBind();
        abc.Visible = true;
        divExport.Visible = true;
        sql = sql + "declare @tables as table(FilePath nvarchar(200), Date datetime, subject nvarchar(100), InvoiceNo nvarchar(50), VendorId int, Amount decimal(10, 2), Tax decimal(10, 2),total decimal(10, 2)) insert into @tables ";
        sql = sql + " select FilePath, convert(date,RecordedDate), Subject, InvoiceNo, VendorId, sum(Amount)Amount, sum(Tax)Tax, sum(total)total from invInvoiceEntry where VendorId=" + VendorId.Trim() + " and BranchCode=" + Session["BranchCode"] + "  group by InvoiceNo, VendorId,convert(date,RecordedDate), Subject, FilePath ";
        sql = sql + " declare @tables2 as table(FilePath nvarchar(200),Date datetime, subject nvarchar(100), InvoiceNo nvarchar(50), VendorId int, Amount decimal(10, 2), Tax decimal(10, 2),total decimal(10, 2), disc decimal(10, 2), gTotal decimal(10,2), Paids decimal(10, 2), Bal decimal(10, 2)) ";
        sql = sql + " insert into @tables2 select t.FilePath, t.Date, (t.subject+' ('+vp.invNo+')'), vp.invNo, vp.VendorId, t.Amount, t.Tax, t.total, sum(vp.discount)disc, (t.total-sum(vp.discount))gTotal, sum(Paid)Paids, (t.total-sum(vp.discount))-sum(Paid) Bal  from accVendorPayment vp inner join @tables t on t.VendorId=vp.VendorId and t.InvoiceNo=vp.invNo ";
        sql = sql + " where vp.VendorId=" + VendorId.Trim() + " and vp.BranchCode=" + Session["BranchCode"] + " group by vp.invNo, vp.VendorId, t.Amount,t.Tax, t.total,t.Date, t.subject,t.FilePath ";
        sql = sql + " select format(Date, 'dd-MMM-yyyy') datess, * from @tables2 where VendorId=" + VendorId.Trim() + " ";
        if (ddlStatus.SelectedValue == "Balance")
        {
            sql = sql + " and Bal>0";
        }
        if (ddlStatus.SelectedValue == "Complete")
        {
            sql = sql + " and Bal=0";
        }
        sql = sql + " order by Date,InvoiceNo asc";

        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            heading.Text = txtSearchBy.Text+" | Ledger";
            lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            gvInvoice.DataSource = dt;
            gvInvoice.DataBind();
            header.Visible = true;
            heading.Visible = true;
            lblRegister.Visible = true;
            pnlcontrols.Visible = true;
        }
        else
        {
            pnlcontrols.Visible = true;
            DataTable dt2 = new DataTable();
            gvInvoice.DataSource = dt2;
            gvInvoice.DataBind();
            abc.Visible = true;
            header.Visible = false;
            heading.Visible = false;
            lblRegister.Visible = false;
            divExport.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "VendorLedger", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "VendorLedger.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "VendorLedger", abc);
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
