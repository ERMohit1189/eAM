using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DiscountReport : Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;

    public DiscountReport()
    {
        con = new SqlConnection();
        oo = new Campus();
        sql = string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
                if (Session["LoginType"].ToString() == "Admin")
                {
                    DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                }
            }

            DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select id, ClassName from ClassMaster";
            sql = sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql = sql + "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex == 0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        }

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        switch (reportType.SelectedValue)
        {
            case "0":
                Consolidated();
                ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
                break;
            case "1":
                Classwise();
                ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
                break;
            case "2":
                PaidDiscount();
                ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
                break;
        }
    }
    protected void Consolidated()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        if (DrpClass.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@Classid", DrpClass.SelectedValue));
            param.Add(new SqlParameter("@ClassName", DrpClass.SelectedItem.Text));
        }
        if (drpDiscountFor.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@DiscountFor", drpDiscountFor.SelectedValue));
        }
        if (drpApplyFrom.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@ApplyFrom", drpApplyFrom.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        param.Add(new SqlParameter("@Action", "Consolidated"));
        heading.Text = "Discount Report (Consolidated) of Session " + DrpSessionName.SelectedValue + "";
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_discountReport", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";
                
                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates");
                if (DrpClass.SelectedIndex != 0)
                {
                    str = str + " | Class : "+ DrpClass.SelectedItem.Text;
                }
                lblRegister.Text = str;
                dt = ds.Tables[0];
                ConsolidatedGrid.DataSource = dt;
                ConsolidatedGrid.DataBind();
                double Amounts = 0;
                for (int i = 0; i < ConsolidatedGrid.Rows.Count; i++)
                {
                    Label Amount = (Label)ConsolidatedGrid.Rows[i].FindControl("Amount");
                    Amounts = Amounts + double.Parse(Amount.Text);
                }
                Label CAmountTotal = (Label)ConsolidatedGrid.FooterRow.FindControl("CAmountTotal");
                CAmountTotal.Text = Amounts.ToString("0.00");
            }
            else
            {
                divExports.Visible = false;
                abc.Visible = false;
            }
        }
    }
    protected void Classwise()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        if (DrpClass.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Classid", DrpClass.SelectedValue));
            param.Add(new SqlParameter("@ClassName", DrpClass.SelectedItem.Text));
        }
        if (drpDiscountFor.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@DiscountFor", drpDiscountFor.SelectedValue));
        }
        if (drpApplyFrom.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@ApplyFrom", drpApplyFrom.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        param.Add(new SqlParameter("@Action", "Studentwise"));
        heading.Text = "Discount Report (Student Wise) of Session " + DrpSessionName.SelectedValue + "";
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_discountReport", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";

                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates");
                if (DrpClass.SelectedIndex != 0)
                {
                    str = str + " | Class : " + DrpClass.SelectedItem.Text;
                }
                lblRegister.Text = str;
                dt = ds.Tables[0];
                StudentwiseGrid.DataSource = dt;
                StudentwiseGrid.DataBind();
                double Amounts = 0;
                for (int i = 0; i < StudentwiseGrid.Rows.Count; i++)
                {
                    Label Amount = (Label)StudentwiseGrid.Rows[i].FindControl("Amount");
                    Amounts = Amounts + double.Parse(Amount.Text);
                }
                Label CAmountTotal = (Label)StudentwiseGrid.FooterRow.FindControl("CAmountTotal");
                CAmountTotal.Text = Amounts.ToString("0.00");
            }
            else
            {
                divExports.Visible = false;
                abc.Visible = false;
            }
        }
    }


    protected void PaidDiscount()
    {
        List<SqlParameter> param = new List<SqlParameter>();
        if (DrpClass.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Classid", DrpClass.SelectedValue));
            param.Add(new SqlParameter("@ClassName", DrpClass.SelectedItem.Text));
        }
        if (drpDiscountFor.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@DiscountFor", drpDiscountFor.SelectedValue));
        }
        if (drpApplyFrom.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@ApplyFrom", drpApplyFrom.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        param.Add(new SqlParameter("@Action", "manual"));
        heading.Text = "Discount Report (Manual) of Session " + DrpSessionName.SelectedValue + "";
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_discountReport", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";

                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates");
                if (DrpClass.SelectedIndex != 0)
                {
                    str = str + " | Class : " + DrpClass.SelectedItem.Text;
                }
                lblRegister.Text = str;
                dt = ds.Tables[0];
                gvDiscounts.DataSource = dt;
                gvDiscounts.DataBind();
                double Amounts = 0;
                for (int i = 0; i < gvDiscounts.Rows.Count; i++)
                {
                    Label Amount = (Label)gvDiscounts.Rows[i].FindControl("Amount");
                    Amounts = Amounts + double.Parse(Amount.Text);
                }
                Label CAmountTotal = (Label)gvDiscounts.FooterRow.FindControl("CAmountTotal");
                CAmountTotal.Text = Amounts.ToString("0.00");
            }
            else
            {
                divExports.Visible = false;
                abc.Visible = false;
            }
        }
    }


    protected void reportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        abc.Visible = false;
        divExports.Visible = false;
        switch (reportType.SelectedValue)
        {
            case "0":
                ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
                DrpClass.SelectedIndex = 0;
                drpDiscountFor.SelectedIndex = 0;
                drpApplyFrom.SelectedIndex = 0;
                StudentwiseGrid.DataSource = null;
                StudentwiseGrid.DataBind();
                ConsolidatedGrid.DataSource = null;
                ConsolidatedGrid.DataBind();
                gvDiscounts.DataSource = null;
                gvDiscounts.DataBind();
                drpDiscountFor.SelectedIndex = 0;
                discForSec.Visible = true;
                drpApplyFrom.SelectedIndex = 0;
                discTypeSec.Visible = false;
                divNote.Visible = true;
                divNote2.Visible = false;
                break;
            case "1":
                ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
                DrpClass.SelectedIndex = 0;
                drpDiscountFor.SelectedIndex = 0;
                drpApplyFrom.SelectedIndex = 0;
                StudentwiseGrid.DataSource = null;
                StudentwiseGrid.DataBind();
                ConsolidatedGrid.DataSource = null;
                ConsolidatedGrid.DataBind();
                StudentwiseGrid.DataSource = null;
                StudentwiseGrid.DataBind();
                gvDiscounts.DataSource = null;
                gvDiscounts.DataBind();
                drpDiscountFor.SelectedIndex = 0;
                discForSec.Visible = true;
                drpApplyFrom.SelectedIndex = 0;
                discTypeSec.Visible = true;
                divNote.Visible = true;
                divNote2.Visible = false;
                break;

            case "2":
                ScriptManager.RegisterStartupScript(Page, GetType(), "discount", "hideSection(0)", true);
                DrpClass.SelectedIndex = 0;
                drpDiscountFor.SelectedIndex = 0;
                drpApplyFrom.SelectedIndex = 0;
                StudentwiseGrid.DataSource = null;
                StudentwiseGrid.DataBind();
                ConsolidatedGrid.DataSource = null;
                ConsolidatedGrid.DataBind();
                StudentwiseGrid.DataSource = null;
                StudentwiseGrid.DataBind();
                gvDiscounts.DataSource = null;
                gvDiscounts.DataBind();
                drpDiscountFor.SelectedIndex = 0;
                discForSec.Visible = false;
                drpApplyFrom.SelectedIndex = 0;
                discTypeSec.Visible = false;
                divNote.Visible = false;
                divNote2.Visible = true;
                break;

        }
       
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "DiscountReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "DiscountReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "DiscountReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
       server control at run time. */
    }

    public override void Dispose()
    {
        con.Dispose();
        oo.Dispose();
    }

    
}