using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class StockReport : Page
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
                //GetStock();
            }
            catch(Exception){}
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetStock();
    }

    private void GetStock()
    {
        gvInvoice.DataSource = null;
        gvInvoice.DataBind();
        abc.Visible = true;
        divExport.Visible = true;
        sql = "select ProductType, Name, convert(varchar(50), Qty)+' ('+um.UnitName+')' Quantity from InvArticleEntry ia ";
        sql = sql + " inner join InvUnitMaster um on um.id=ia.UnitID and ia.BranchCode=um.BranchCode ";
        sql = sql + " where ia.BranchCode=" + Session["BranchCode"] + " ";
        if (ddlProductType.SelectedIndex!=0)
        {
            sql = sql + " and ia.ProductType='" + ddlProductType.SelectedValue + "' ";
        }
        var dt = _oo.Fetchdata(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            heading.Text = "Stock Report";
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
        _oo.ExportTolandscapeWord(Response, "Stockreport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "Stockreport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "Stockreport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}