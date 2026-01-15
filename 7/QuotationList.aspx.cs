using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_QuotationList : Page
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
                txtQtnFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtQtnToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                GetQtnList();
            }
            catch (Exception ex)
            {
            }
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
            GetQtnList();
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, dvSearch, BLL.BLLInstance.FetchMSG("Enter Vendor ID !"), "A");
            txtSearchBy.Focus();
        }
    }

    protected void lbtnSearchBy_Click(object sender, EventArgs e)
    {
        GetQtnList();
    }

    private void GetQtnList()
    {
       
        var studentId = hfVendorId.Value;
        sql = sql + "select qt.*, qt.Id ids, format(qt.QtnDate, 'dd-MMM-yyyy') QtnEnterDate, OrganizationName+' ('+VendorCode+')' VendorName, case when qt.status='Approve' then 'Approved' else case when qt.status='Reject' then 'Rejected' else qt.status end end Statuss, case when qt.status='Pending' then qt.remark else qt.RemarkApprove end Remarks  from InvQuotation qt ";
        sql = sql + " inner join AccVendor v on v.id=qt.VendorId and v.BranchCode=qt.BranchCode and convert(date, qt.QtnDate) between '" + txtQtnFromDate.Text.Trim() + "' and '" + txtQtnToDate.Text.Trim() + "'";
        sql = sql + " where qt.BranchCode=" + Session["BranchCode"] + " ";
        if (studentId != "")
        {
            sql = sql + " and qt.Vendorid=" + studentId + " ";
        }
        if (txtQtnNo.Text.Trim() != "")
        {
            sql = sql + " and qt.QtnNo='" + txtQtnNo.Text.Trim() + "' ";
        }
        if (ddlStatus.SelectedIndex!=0)
        {
            sql = sql + " and qt.Status='" + ddlStatus.SelectedValue + "' ";
        }
        var dt = _oo.Fetchdata(sql);
        if (dt.Rows.Count > 0)
        {
            abc.Visible = true;
            divExport.Visible = true;
            gvBankBranchList.DataSource = dt;
            gvBankBranchList.DataBind();
            heading.Text = "List of Quotations";
            lblRegister.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        }
        else
        {
            abc.Visible = false;
            divExport.Visible = false;
        }
        
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "QuotationList", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "QuotationList.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "QuotationList", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void Dispose()
    {
        _oo.Dispose();
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        var lbtnDonwload = (HyperLink)chk.NamingContainer.FindControl("lbtnDonwload");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "print", "window.open('" + lbtnDonwload.NavigateUrl.Replace("~/","../../") + "','_blank');", true);
    }
}