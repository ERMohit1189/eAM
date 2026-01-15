using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FeeCollectionReportReceiptwise : Page
{
    Campus oo = new Campus();
    string sql = "";
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
        else
        {
            string sql = "Select Organization, Address from tbl_SocietyOrTrust";
            lblOrganization.Text = oo.ReturnTag(sql, "Organization");
            lblAddress.Text = oo.ReturnTag(sql, "Address");
        }
        if (!IsPostBack)
        {
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            abc.Visible = false;
            oo.AddDateMonthYearDropDown(fromDDYears, fromDDMonths, fromDDDates);
            oo.AddDateMonthYearDropDown(toDDYears, toDDMonths, toDDDates);
            oo.FindCurrentDateandSetinDropDown(fromDDYears, fromDDMonths, fromDDDates);
            oo.FindCurrentDateandSetinDropDown(toDDYears, toDDMonths, toDDDates);
            sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

            if (Session["LoginType"].ToString() == "Admin")
            {
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();
            }
            else
            {
                ddlBranch.SelectedIndex = ddlBranch.Items.Count - 1;
            }

            string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = oo.Fetchdata(sqls);
            DrpSessionName.DataSource = dt2;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            DrpSessionName.Items.Insert(0, new ListItem("<--All-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = true;
            }
            else
            {
                divBranch.Visible = true;
                divSession.Visible = true;
            }

            
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            loadUser();
        }
    }
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "Select id, ClassName from ClassMaster";
        sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select id, SectionName from SectionMaster ";
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
    protected void fromDDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(fromDDYears, fromDDMonths, fromDDDates);
    }
    protected void fromDDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(fromDDYears, fromDDMonths, fromDDDates);

    }
    protected void toDDYearC_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(toDDYears, toDDMonths, toDDDates);
    }
    protected void toDDMonthC_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(toDDYears, toDDMonths, toDDDates);

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();
        if (ddlBranch.SelectedIndex == 0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            DrpClass.Items.Clear();
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
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
        sql = sql + "  where SessionName='" + DrpSessionName.SelectedValue + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        sql = sql + "  order by Id";
        oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
        DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    protected void loadUser()
    {
        string sql = "";
        if (Session["LoginType"].ToString() == "Admin")
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + "";
        }
        oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
        DropDownList1.Items.Insert(0, new ListItem("All", ""));
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
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

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(txtSearch.Text))
        {
            studentId = "";
        }
        List<SqlParameter> param = new List<SqlParameter>();
        string fromdate = fromDDDates.SelectedItem.Text + "-" + fromDDMonths.SelectedItem.Text + "-" + fromDDYears.SelectedItem.Text;
        string todate = toDDDates.SelectedItem.Text + "-" + toDDMonths.SelectedItem.Text + "-" + toDDYears.SelectedItem.Text;
        var days = (DateTime.Parse(todate) - DateTime.Parse(fromdate)).TotalDays;
        if (days > 370)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Date range should not more than a financial year.", "A");
        }
        else
        {
            param.Add(new SqlParameter("@FromDate", fromdate));
            param.Add(new SqlParameter("@ToDate", todate));
            if (DdlpaymentMode.SelectedItem.Text != "All")
            {
                param.Add(new SqlParameter("@PaymentMode", DdlpaymentMode.SelectedItem.Text));
            }
            if (DrpClass.SelectedIndex!=0)
            {
                param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
                param.Add(new SqlParameter("@ClassName", DrpClass.SelectedItem.Text.Trim()));
            }
            if (drpSection.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
            }
            if (drpBranch.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
                param.Add(new SqlParameter("@BranchName", drpBranch.SelectedItem.Text.Trim()));
            }
            if (drpStatus.SelectedItem.Text != "All")
            {
                param.Add(new SqlParameter("@Status", drpStatus.SelectedItem.Text));
            }
            if (DrpSessionName.SelectedIndex!=0)
            {
                param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
            }
            param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
            if (ddlPaymentFrequency.SelectedValue != "")
            {
                param.Add(new SqlParameter("@PaymentFrequency", ddlPaymentFrequency.SelectedValue));
            }
            param.Add(new SqlParameter("@IsExcludeOtherFee", (chkExclude.Checked == true ? "1" : "0")));
            heading.Text = "Fee Collection Report from " + fromdate + " to " + todate + "";
            string ss = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') genratedOn";
            string ss2 = "select name+' ('+UserId+')' GeneatedBy from NewAdminInformation where UserId='"+Session["LoginName"]+"' and BranchCode="+ ddlBranch.SelectedValue + "";
            generatedBy.Text = "Generated by "+ oo.ReturnTag(ss2, "GeneatedBy") +" on " + oo.ReturnTag(ss, "genratedOn");
            if (studentId != "")
            {
                param.Add(new SqlParameter("@SrNo", studentId));
                string sqls = "select Name +' (S.R. No.-'+ SrNo+')' name from AllStudentRecord_UDF('" + DrpSessionName.SelectedItem.Text + "', " + ddlBranch.SelectedValue + ") where srno='" + studentId + "'";
                string stuName = "Student : "+oo.ReturnTag(sqls, "name");
                lblstudens.Text = stuName;
            }
            if (DropDownList1.SelectedValue != "")
            {
                param.Add(new SqlParameter("@LoginName", DropDownList1.SelectedValue));
            }
            DataTable dt;
            DataSet ds;
            ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_FeeCollectionReport", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    divExport.Visible = true; string str = "";
                    if (DrpSessionName.SelectedIndex!=0)
                    {
                        str = str + "Session : " + DrpSessionName.SelectedItem.Text;
                    }
                    if (DrpClass.SelectedIndex != 0 && DrpSessionName.SelectedIndex != 0)
                    {
                        str = str + " | Class : " + DrpClass.SelectedItem.Text;
                    }
                    if (DrpClass.SelectedIndex != 0 && DrpSessionName.SelectedIndex == 0)
                    {
                        str = str + "Class : " + DrpClass.SelectedItem.Text;
                    }
                    if (drpSection.SelectedIndex != 0 && DrpClass.SelectedIndex != 0)
                    {
                        str = str + " | Section : " + drpSection.SelectedItem.Text;
                    }
                    if (drpSection.SelectedIndex != 0 && DrpClass.SelectedIndex == 0)
                    {
                        str = str + "Section : " + drpSection.SelectedItem.Text;
                    }
                    if (drpBranch.SelectedIndex != 0 && drpSection.SelectedIndex != 0)
                    {
                        str = str + " | Stream : " + drpBranch.SelectedItem.Text;
                    }
                    if (drpBranch.SelectedIndex != 0 && drpSection.SelectedIndex == 0)
                    {
                        str = str + "Stream : " + drpBranch.SelectedItem.Text;
                    }
                    if (drpStatus.SelectedItem.Text != "All" && drpSection.SelectedIndex!=0)
                    {
                        str = str + " | Status : " + drpStatus.SelectedItem.Text;
                    }
                    if (drpStatus.SelectedItem.Text != "All" && drpSection.SelectedIndex == 0)
                    {
                        str = str + " | Status : " + drpStatus.SelectedItem.Text;
                    }
                    

                    //if (ddlPaymentFrequency.SelectedValue != "")
                    //{
                    //    str = str + " | Payment Frequency : " + ddlPaymentFrequency.SelectedItem.Text;
                    //}
                    //if (chkExclude.Checked)
                    //{
                    //    str = str + " | Other Fee Excluded";
                    //}
                    //if (Session["Logintype"].ToString() == "SuperAdmin" && str != "")
                    //{
                    //    str = str + " | Branch : " + ddlBranch.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    str = str + "Branch : " + ddlBranch.SelectedItem.Text;
                    //}
                    if (DdlpaymentMode.SelectedItem.Text != "All" && drpStatus.SelectedIndex != 0)
                    {
                        str = str + " | Mode : " + DdlpaymentMode.SelectedItem.Text;
                    }
                    if (DdlpaymentMode.SelectedItem.Text != "All" && drpStatus.SelectedIndex == 0)
                    {
                        str = str + "Mode : " + DdlpaymentMode.SelectedItem.Text;
                    }
                    if (DropDownList1.SelectedItem.Text != "All" && DdlpaymentMode.SelectedIndex != 0)
                    {
                        str = str + " | User : " + DropDownList1.SelectedItem.Text;
                    }
                    if (DropDownList1.SelectedItem.Text != "All" && DdlpaymentMode.SelectedIndex == 0)
                    {
                        str = str + " | User : " + DropDownList1.SelectedItem.Text;
                    }
                    lblSelectdetails.Text = str;
                    dt = ds.Tables[0];
                    abc.Visible = true;
                    GrdDisplay.DataSource = dt;
                    GrdDisplay.DataBind();
                    double amt = 0;
                    for (int i = 0; i < GrdDisplay.Rows.Count; i++)
                    {
                        Label amount = (Label)GrdDisplay.Rows[i].FindControl("amount");
                        Label Status = (Label)GrdDisplay.Rows[i].FindControl("Status");
                        if (Status.Text != "Cancelled")
                        {
                            amt = amt + double.Parse(amount.Text);
                        }
                        else
                        {
                            amount.Text = "0.00";
                        }
                    }
                    Label FooterTotal = (Label)GrdDisplay.FooterRow.FindControl("FooterTotal");
                    FooterTotal.Text = amt.ToString("0.00");
                }
                else
                {
                    divExport.Visible = false;
                    abc.Visible = false;
                }
            }

        }
    }
}