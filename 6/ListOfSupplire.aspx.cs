using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListOfSupplire : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
         con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);

        if (!IsPostBack)
        {
            try
            {
                sql = "Select CategoryName from SupplierCategoryMaster where  BranchCode=" + Session["BranchCode"] + "";
                oo.FillDropDown(sql, drpSupplierCategory, "CategoryName");
                drpSupplierCategory.Items.Insert(0, new ListItem("<--All-->", "<--All-->"));
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadData();
    }
    protected void loadData()
    {
        sql = " Select SupplierCategory,SupplierName,Address+' '+city+', '+ State +' -'+Zip+', '+Country Address,Phone1,Mobile,Email from SupplierInfoEntry  ";
        sql = sql + " where BranchCode=" + Session["BranchCode"] + "";
        if (drpSupplierCategory.SelectedIndex != 0)
        {
            sql = sql + " and SupplierCategory='" + drpSupplierCategory.SelectedItem.Text + "' ";
        }
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            divPrinttools.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "No record(s) found!", "A");
            divPrinttools.Visible = false;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
       server control at run time. */
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "ListofSuppliers", abc);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "ListofSuppliers", abc);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "ListofSuppliers", abc);
    }

}
