using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OptionalFeeAllotmentReport : Page
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (Session["LoginType"].ToString() == "Admin")
        {
            BLL.BLLInstance.LoadHeader("Report", header);
        }
        if (!IsPostBack)
        {
            abc.Visible = false;
            loadFeeCategory();
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpInstallment.Items.Insert(0, new ListItem("<-- Select-->", ""));
        }
    }
    protected void loadFeeCategory()
    {
        string sql1 = "Select FeeGroupName,Id from FeeGroupMaster";
        sql1 = sql1 + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql1, ddlFeeCategory, "FeeGroupName", "Id");
        ddlFeeCategory.Items.Insert(0, new ListItem("<-- Select-->", ""));

    }
    protected void ddlFeeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select id, ClassName from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        sql = sql + "  order by CIDOrder asc";
        oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select id, SectionName from SectionMaster ";
        sql = sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DrpClass.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        loadFeeMonth();
    }
    public void loadFeeMonth()
    {
        string sql = "select Monthid, MonthName from MonthMaster where (CardType='" + ddlFeeCategory.SelectedItem.ToString() + "' or CardType='" + ddlFeeCategory.SelectedValue.ToString() + "')";
        sql = sql + " and ClassId='" + DrpClass.SelectedValue.ToString() + "' and MOD='I' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " or monthid=0";
        sql = sql + "  order by MonthId";
        oo.FillDropDown_withValue(sql, drpInstallment, "MonthName", "Monthid");
        drpInstallment.Items.Insert(0, new ListItem("<-- Select-->", ""));
    }
    

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", Session["SessionName"]));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
        param.Add(new SqlParameter("@InstallmentId", drpInstallment.SelectedValue));
        var ds = new DLL().Sp_SelectRecord_usingExecuteDataset("GetOptionalFeeAllotment", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblInstallment.Text = "Installment : "+drpInstallment.SelectedItem.Text;
                divExport.Visible = true;
                abc.Visible = true;
                GrdDisplay.DataSource = ds.Tables[0];
                GrdDisplay.DataBind();
            }
            else
            {
                divExport.Visible = false;
                abc.Visible = false;
            }
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "FeeCollectionReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "FeeCollectionReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "FeeCollectionReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}