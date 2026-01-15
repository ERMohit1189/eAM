using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListOfpublisher : System.Web.UI.Page
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
                sql = "Select CategoryName from PublisherCategoryMaster where  BranchCode=" + Session["BranchCode"] + "";
                oo.FillDropDown(sql, drppublisherCategory, "CategoryName");
                drppublisherCategory.Items.Insert(0, new ListItem("<--All-->", "<--All-->"));
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
        sql = " Select  pm.CategoryName, p.PublisherName,p.Address+' '+p.city+', '+ p.State +' -'+p.Zip+', '+p.Country Address,p.Phone1,p.Mobile, p.Email from PublisherInfoEntry p  ";
        sql = sql + " inner join PublisherCategoryMaster pm on p.PublisherCategory=pm.CategoryCode  and  pm.BranchCode=" + Session["BranchCode"] + "  and  p.BranchCode=" + Session["BranchCode"] + "";
        if (drppublisherCategory.SelectedIndex!=0)
        {
            sql = sql + " and  pm.CategoryName='"+ drppublisherCategory.SelectedItem.Text + "' ";
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
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "ListofPublishers", abc);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "ListofPublishers", abc);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "ListofPublishers", abc);
    }


    
}
