using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DueDepositBalance : Page
{
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }
        if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        header.InnerHtml = "";
        Campus camp = new Campus(); camp.LoadLoader(loader);
        
        if (Session["LoginType"].ToString() == "Admin")
        {
            BLL.BLLInstance.LoadHeader("Report", header);
        }
        else
        {
            string sql = "Select Organization, Address from tbl_SocietyOrTrust";
            lblOrganization.Text = oo.ReturnTag(sql, "Organization");
            lblAddress.Text = oo.ReturnTag(sql, "Address");
        }
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
            sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
            sql = sql + "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

            if (Session["LoginType"].ToString().ToLower() == "staff")
            {
                divClass.Visible = true;
                divSection.Visible = true;
                divStream.Visible = true;
                divStatus.Visible = true;
                LinkButton2.Visible = true;

                reportType.SelectedIndex = 1;
                divtp.Visible = false;

                divClass.Visible = true;
                divSection.Visible = true;
                divStream.Visible = true;
                ClasswiseGrid.DataSource = null;
                ClasswiseGrid.DataBind();
                ConsolidatedGrid.DataSource = null;
                ConsolidatedGrid.DataBind();
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
                DrpSessionName.DataSource = dt2;
                DrpSessionName.DataTextField = "SessionName";
                DrpSessionName.DataValueField = "SessionName";
                DrpSessionName.DataBind();
                DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
                DrpSessionName.SelectedValue = Session["SessionName"].ToString();
                divBranch.Visible = false;
                divSession.Visible = false;

                sql = "Select id, ClassName from ClassMaster";
                sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
                sql = sql + "  order by Id";
                oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
                DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));

                string sql1 = "select ClassId, SectionId, BranchId from ClassTeacherMaster where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and EmpCode='"+ Session["LoginName"].ToString()+"'";
                if (oo.Duplicate(sql1))
                {
                    DrpClass.SelectedValue = oo.ReturnTag(sql1, "ClassId");
                    sql = "select id, SectionName from SectionMaster ";
                    sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassNameId=" + DrpClass.SelectedValue + "";
                    sql = sql + "  order by Id";
                    drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
                    oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");


                    drpSection.SelectedValue = oo.ReturnTag(sql1, "SectionId");
                    sqls = "select id, BranchName from BranchMaster ";
                    sqls = sqls + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassId=" + DrpClass.SelectedValue + "";
                    sqls = sqls + "  order by Id";
                    oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
                    drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                    drpBranch.SelectedValue = oo.ReturnTag(sql1, "BranchId");
                    divClass.Visible = false;
                    divSection.Visible = false;
                    divStream.Visible = false;
                }
                else
                {
                    DrpClass.SelectedValue = "";
                    drpSection.SelectedValue = "";
                    drpBranch.SelectedValue = "";
                    msgbox.InnerText = "Please allot class teacher first!";
                    divClass.Visible = false;
                    divSection.Visible = false;
                    divStream.Visible = false;
                    divStatus.Visible = false;
                    LinkButton2.Visible = false;
                }
            }
        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlBranch.SelectedIndex==0)
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
        sql = "Select id, ClassName from ClassMaster";
        sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue   + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));

    }
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string  sql = "Select id, ClassName from ClassMaster";
        sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (reportType.SelectedValue == "0")
        {
            Consolidated();
        }
        else
        {
            Classwise();
        }
    }
    protected void Consolidated()
    {
       
        DataTable table = new DataTable();
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@Type", "Consolidated"));
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        heading.Text = "Due Deposit Balance Report of Session " + DrpSessionName.SelectedValue + "";
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_DueDepositBalanceBoth", param);
        if (ds.Tables[0].Rows.Count > 0)
        {
            table = ds.Tables[0];
        }

        if (table != null)
        {
            if (table.Rows.Count > 0)
            {
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";
                
                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
                lblRegister.Text = str;
                ConsolidatedGrid.DataSource = table;
                ConsolidatedGrid.DataBind();
                double Due = 0; double Deposit = 0; double Balance = 0;
                for (int i = 0; i < ConsolidatedGrid.Rows.Count; i++)
                {
                    Label cDue = (Label)ConsolidatedGrid.Rows[i].FindControl("cDue");
                    Due = Due + double.Parse(cDue.Text);
                    Label cDeposit = (Label)ConsolidatedGrid.Rows[i].FindControl("cDeposit");
                    Deposit = Deposit + double.Parse(cDeposit.Text);
                    Label cBalance = (Label)ConsolidatedGrid.Rows[i].FindControl("cBalance");
                    Balance = Balance + double.Parse(cBalance.Text);
                }
                Label CDueTotal = (Label)ConsolidatedGrid.FooterRow.FindControl("CDueTotal");
                Label CDepositTotal = (Label)ConsolidatedGrid.FooterRow.FindControl("CDepositTotal");
                Label CBalanceTotal = (Label)ConsolidatedGrid.FooterRow.FindControl("CBalanceTotal");
                CDueTotal.Text = Due.ToString("0.00");
                CDepositTotal.Text = Deposit.ToString("0.00");
                CBalanceTotal.Text = Balance.ToString("0.00");
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
        param.Add(new SqlParameter("@Type", "Class wise"));
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        heading.Text = "Due Deposit Balance Report | Class : " + DrpClass.SelectedItem.Text;
        if (drpBranch.SelectedIndex!=0)
        {
            heading.Text= heading.Text+" " + (drpBranch.SelectedItem.Text.ToLower()=="na" || drpBranch.SelectedItem.Text.ToLower() == "n/a"?"": drpBranch.SelectedItem.Text);
        }
        if (drpSection.SelectedIndex != 0)
        {
            heading.Text = heading.Text + " (" + drpSection.SelectedItem.Text+")";
        }
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_DueDepositBalanceBoth", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";
                
                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
                lblRegister.Text = str;
                dt = ds.Tables[0];
                ClasswiseGrid.DataSource = dt;
                ClasswiseGrid.DataBind();
                double Due = 0; double Deposit = 0; double Balance = 0;
                for (int i = 0; i < ClasswiseGrid.Rows.Count; i++)
                {
                    Label ClassDue = (Label)ClasswiseGrid.Rows[i].FindControl("ClassDue");
                    Due = Due + double.Parse(ClassDue.Text);
                    Label ClassDeposit = (Label)ClasswiseGrid.Rows[i].FindControl("ClassDeposit");
                    Deposit = Deposit + double.Parse(ClassDeposit.Text);
                    Label ClassBalance = (Label)ClasswiseGrid.Rows[i].FindControl("ClassBalance");
                    Balance = Balance + double.Parse(ClassBalance.Text);
                }
                Label ClassDueTotal = (Label)ClasswiseGrid.FooterRow.FindControl("ClassDueTotal");
                Label ClassDepositTotal = (Label)ClasswiseGrid.FooterRow.FindControl("ClassDepositTotal");
                Label ClassBalanceTotal = (Label)ClasswiseGrid.FooterRow.FindControl("ClassBalanceTotal");
                ClassDueTotal.Text = Due.ToString("0.00");
                ClassDepositTotal.Text = Deposit.ToString("0.00");
                ClassBalanceTotal.Text = Balance.ToString("0.00");
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
        if (reportType.SelectedValue == "0")
        {
            divClass.Visible = false;
            divSection.Visible = false;
            divStream.Visible = false;
            ClasswiseGrid.DataSource = null;
            ClasswiseGrid.DataBind();
            ConsolidatedGrid.DataSource = null;
            ConsolidatedGrid.DataBind();

        }
        else
        {
            divClass.Visible = true;
            divSection.Visible = true;
            divStream.Visible = true;
            ClasswiseGrid.DataSource = null;
            ClasswiseGrid.DataBind();
            ConsolidatedGrid.DataSource = null;
            ConsolidatedGrid.DataBind();
        }
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string  sql = "select id, SectionName from SectionMaster ";
        sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + " and ClassId=" + DrpClass.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "DueDepositBalanceReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "ListOfStudents.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "DueDepositBalanceReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}