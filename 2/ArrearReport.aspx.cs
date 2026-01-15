using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArrearReport : Page
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

                divClass.Visible = true;
                divSection.Visible = true;
                divStream.Visible = true;
                ClasswiseGrid.DataSource = null;
                ClasswiseGrid.DataBind();
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Consolidated();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Classwise();
    }
    protected void Consolidated()
    {

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        heading.Text = "Arrear Report | Consolidated";
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("GetConsolidatedArrear", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ConsolidatedGrid.Visible = true;
                ClasswiseGrid.Visible = false;
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";
                
                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
                lblRegister.Text = str;
                dt = ds.Tables[0];
                ConsolidatedGrid.DataSource = dt;
                ConsolidatedGrid.DataBind();
                double Arrear = 0; double Discount = 0; double Total = 0; double PaidAmount = 0; double BalanceAmount = 0;
                for (int i = 0; i < ConsolidatedGrid.Rows.Count; i++)
                {
                    Label Arrearl = (Label)ConsolidatedGrid.Rows[i].FindControl("Arrear");
                    Arrear = Arrear + double.Parse(Arrearl.Text);
                    Label Discountl = (Label)ConsolidatedGrid.Rows[i].FindControl("Discount");
                    Discount = Discount + double.Parse(Discountl.Text);
                    Label Totall = (Label)ConsolidatedGrid.Rows[i].FindControl("Total");
                    Total = Total + double.Parse(Totall.Text);
                    Label PaidAmountl = (Label)ConsolidatedGrid.Rows[i].FindControl("PaidAmount");
                    PaidAmount = PaidAmount + double.Parse(PaidAmountl.Text);
                    Label BalanceAmountl = (Label)ConsolidatedGrid.Rows[i].FindControl("BalanceAmount");
                    BalanceAmount = BalanceAmount + double.Parse(BalanceAmountl.Text);
                }
                Label ClassArrear = (Label)ConsolidatedGrid.FooterRow.FindControl("ClassArrear");
                Label ClassDiscount = (Label)ConsolidatedGrid.FooterRow.FindControl("ClassDiscount");
                Label ClassTotal = (Label)ConsolidatedGrid.FooterRow.FindControl("ClassTotal");
                Label ClassPaidAmount = (Label)ConsolidatedGrid.FooterRow.FindControl("ClassPaidAmount");
                Label ClassBalanceAmount = (Label)ConsolidatedGrid.FooterRow.FindControl("ClassBalanceAmount");
                ClassArrear.Text = Arrear.ToString("0.00");
                ClassDiscount.Text = Discount.ToString("0.00");
                ClassTotal.Text = Total.ToString("0.00");
                ClassPaidAmount.Text = PaidAmount.ToString("0.00");
                ClassBalanceAmount.Text = BalanceAmount.ToString("0.00");
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
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        heading.Text = "Arrear Report | Class : " + DrpClass.SelectedItem.Text;
        if (drpBranch.SelectedIndex != 0)
        {
            heading.Text = heading.Text + " " + (drpBranch.SelectedItem.Text.ToLower() == "na" || drpBranch.SelectedItem.Text.ToLower() == "n/a" ? "" : drpBranch.SelectedItem.Text);
        }
        if (drpSection.SelectedIndex != 0)
        {
            heading.Text = heading.Text + " (" + drpSection.SelectedItem.Text + ")";
        }
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("GetDescriptiveArrear", param);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ConsolidatedGrid.Visible = false;
                ClasswiseGrid.Visible = true;
                divExports.Visible = true;
                abc.Visible = true;
                string str = "";

                string sqls = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
                str = str + "Date : " + oo.ReturnTag(sqls, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
                lblRegister.Text = str;
                dt = ds.Tables[0];
                ClasswiseGrid.DataSource = dt;
                ClasswiseGrid.DataBind();
                double Arrear = 0; double Discount = 0; double Total = 0; double PaidAmount = 0; double BalanceAmount = 0;
                for (int i = 0; i < ClasswiseGrid.Rows.Count; i++)
                {
                    Label Arrearl = (Label)ClasswiseGrid.Rows[i].FindControl("Arrear");
                    Arrear = Arrear + double.Parse(Arrearl.Text);
                    Label Discountl = (Label)ClasswiseGrid.Rows[i].FindControl("Discount");
                    Discount = Discount + double.Parse(Discountl.Text);
                    Label Totall = (Label)ClasswiseGrid.Rows[i].FindControl("Total");
                    Total = Total + double.Parse(Totall.Text);
                    Label PaidAmountl = (Label)ClasswiseGrid.Rows[i].FindControl("PaidAmount");
                    PaidAmount = PaidAmount + double.Parse(PaidAmountl.Text);
                    Label BalanceAmountl = (Label)ClasswiseGrid.Rows[i].FindControl("BalanceAmount");
                    BalanceAmount = BalanceAmount + double.Parse(BalanceAmountl.Text);
                }
                Label ClassArrear = (Label)ClasswiseGrid.FooterRow.FindControl("ClassArrear");
                Label ClassDiscount = (Label)ClasswiseGrid.FooterRow.FindControl("ClassDiscount");
                Label ClassTotal = (Label)ClasswiseGrid.FooterRow.FindControl("ClassTotal");
                Label ClassPaidAmount = (Label)ClasswiseGrid.FooterRow.FindControl("ClassPaidAmount");
                Label ClassBalanceAmount = (Label)ClasswiseGrid.FooterRow.FindControl("ClassBalanceAmount");
                ClassArrear.Text = Arrear.ToString("0.00");
                ClassDiscount.Text = Discount.ToString("0.00");
                ClassTotal.Text = Total.ToString("0.00");
                ClassPaidAmount.Text = PaidAmount.ToString("0.00");
                ClassBalanceAmount.Text = BalanceAmount.ToString("0.00");
            }
            else
            {
                divExports.Visible = false;
                abc.Visible = false;
            }
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
    protected void reportType_SelectedIndexChanged(object sender, EventArgs e)
    {

        abc.Visible = false;
        divExports.Visible = false;
        if (reportType.SelectedValue == "0")
        {
            LinkButton1.Visible = true;
            LinkButton2.Visible = false;

            divClass.Visible = false;
            divSection.Visible = false;
            divStream.Visible = false;
            ConsolidatedGrid.DataSource = null;
            ConsolidatedGrid.DataBind();
            ClasswiseGrid.DataSource = null;
            ClasswiseGrid.DataBind();
            ConsolidatedGrid.Visible = false;
            ConsolidatedGrid.Visible = false;
        }
        else
        {
            LinkButton1.Visible = false;
            LinkButton2.Visible = true;
            divClass.Visible = true;
            divSection.Visible = true;
            divStream.Visible = true;
            ConsolidatedGrid.DataSource = null;
            ConsolidatedGrid.DataBind();
            ClasswiseGrid.DataSource = null;
            ClasswiseGrid.DataBind();
            ConsolidatedGrid.Visible = false;
            ConsolidatedGrid.Visible = false;
        }
    }
}