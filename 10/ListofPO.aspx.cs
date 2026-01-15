using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ListofPO : Page
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
                GetPO();
            }
            catch(Exception){}
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetPO();
    }

    private void GetPO()
    {
        gvInvoice.DataSource = null;
        gvInvoice.DataBind();
        abc.Visible = true;
        divExport.Visible = true;
        sql = sql + "select format(convert(date, po.OrderDate), 'dd-MMM-yyyy') Date, PONo,po.QtnNo, OrganizationName+' ('+VendorCode+')' Vendor,  po.Subject, qt.RefNo, sum(po.Amount) Amount,sum(Tax) Tax,sum(Total)Total from invPurchaeOrder po ";
        sql = sql + " inner join AccVendor vn on vn.id=po.VendorId and vn.BranchCode=po.BranchCode  ";
        sql = sql + " inner join InvQuotation qt on qt.QtnNo=po.QtnNo and qt.VendorId=po.VendorId and vn.BranchCode=po.BranchCode  ";
        sql = sql + " where po.BranchCode=" + Session["BranchCode"] + "  and convert(date, po.OrderDate) between '" + txtFromdate.Text.Trim()+"' and '"+ txtTodate.Text.Trim() + "' ";
        sql = sql + " group by PONo, po.VendorId,OrganizationName,VendorCode, convert(date, po.OrderDate), Subject, RefNo,po.QtnNo ";
        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            heading.Text = "List of P.O.";
            lblRegister.Text ="Date : "+ DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            gvInvoice.DataSource = dt;
            gvInvoice.DataBind();
            pnlcontrols.Visible = true;
        }
        else
        {
            pnlcontrols.Visible = false;
            abc.Visible = false;
            divExport.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "ListOfPO", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "ListOfPO.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "ListOfPO", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}