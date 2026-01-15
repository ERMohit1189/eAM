using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaidSalaryReport : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadHeader("Report", header);
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            sql = "select DesId, DesName from DesMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpDesignation, "DesName", "DesName");
            drpDesignation.Items.Insert(0, new ListItem("<--Select All-->", ""));
            loadYear();
            selectedMonth();
        }
    }

    protected void loadYear()
    {
        sql = "Select years from(Select DatePart(YEAR,Getdate())-1 years Union Select DatePart(YEAR,Getdate()) years Union Select DatePart(YEAR,Getdate()) years)T1";
        BAL.objBal.FillDropDown_withValue(sql, drpYear, "years", "years");
        drpYear.SelectedIndex = 1;
    }

    public void selectedMonth()
    {
        sql = "Select format(getdate(),'yyyy') yearvalue";
        drpYear.SelectedValue = BAL.objBal.ReturnTag(sql, "yearvalue");
        sql = "Select format(getdate(),'MMM') monthvalue";
        drpMonth.SelectedValue = BAL.objBal.ReturnTag(sql, "monthvalue");
    }

    public void loadGrid()
    {
        sql = "select *,GeneratedBy, '('+format(GeneratedDate,'dd-MMM-yyyy hh:mm:ss tt')+')' GeneratedDates,PaidBy, '('+format(PaidDate,'dd-MMM-yyyy hh:mm:ss tt')+')' PaidDates  from SalaryGeneratedAndPaid where BranchCode=" + Session["BranchCode"] + " and MonthName='" + drpMonth.SelectedValue.ToString() + "' and YearName='" + drpYear.SelectedValue.ToString() + "' ";
        sql = sql + " and EmpID in (select EmpID from EmpployeeOfficialDetails where BranchCode=" + Session["BranchCode"] + " and DesNameNew=case when '"+ drpDesignation.SelectedIndex + "'='0' then DesNameNew else '" + drpDesignation.SelectedValue + "' end) ";
        if (ddlStatus.SelectedIndex!=0)
        {
            sql = sql + " and Status='" + ddlStatus.SelectedValue + "' ";
        }
        if (ddlMode.SelectedIndex != 0)
        {
            sql = sql + " and Mode='"+ ddlMode.SelectedValue + "' ";
        }
        sql = sql + " order by  Name asc";
        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count > 0)
        {
            divExport.Visible = true;
            rptEmp.DataSource = dt;
            rptEmp.DataBind();
            double ctc_v = 0; double Earning_v = 0; double Deduction_v = 0; double total_v = 0; double AdvanceSalary_v = 0; double NetPay_v = 0;
            for (int i = 0; i < rptEmp.Items.Count; i++)
            {
                Label EmpId = (Label)rptEmp.Items[i].FindControl("EmpId");
                string sql1 = "select * from SalaryGeneratedAndPaidComponent where BranchCode=" + Session["BranchCode"] + " and MonthName='" + drpMonth.SelectedValue.ToString() + "' and YearName='" + drpYear.SelectedValue.ToString() + "' and EmpId='" + EmpId.Text.Trim() + "' ";
                var dt1 = oo.Fetchdata(sql1);
                Repeater rptHead = (Repeater)rptEmp.Items[i].FindControl("rptHead");
                if (dt1.Rows.Count > 0)
                {
                    rptHead.DataSource = dt1;
                    rptHead.DataBind();
                }
                Label CTC = (Label)rptEmp.Items[i].FindControl("CTC");
                ctc_v = ctc_v + double.Parse(CTC.Text==""?"0": CTC.Text);
                Label Earning = (Label)rptEmp.Items[i].FindControl("Earning");
                Earning_v = Earning_v + double.Parse(Earning.Text == "" ? "0" : Earning.Text);
                Label Deduction = (Label)rptEmp.Items[i].FindControl("Deduction");
                Deduction_v = Deduction_v + double.Parse(Deduction.Text == "" ? "0" : Deduction.Text);
                Label total = (Label)rptEmp.Items[i].FindControl("total");
                total_v = total_v + double.Parse(total.Text == "" ? "0" : total.Text);
                Label AdvanceSalary = (Label)rptEmp.Items[i].FindControl("AdvanceSalary");
                AdvanceSalary_v = AdvanceSalary_v + double.Parse(AdvanceSalary.Text == "" ? "0" : AdvanceSalary.Text);
                Label NetPay = (Label)rptEmp.Items[i].FindControl("NetPay");
                NetPay_v = NetPay_v + double.Parse(NetPay.Text == "" ? "0" : NetPay.Text);
            }
            lbltotalCTC.Text = ctc_v.ToString("0.00");
            lbltotalEarning.Text = Earning_v.ToString("0.00");
            lbltotalDeduction.Text = Deduction_v.ToString("0.00");
            lbltotalTotal.Text = total_v.ToString("0.00");
            lbltotalAdvance.Text = AdvanceSalary_v.ToString("0.00");
            lbltotalNetpay.Text = NetPay_v.ToString("0.00");
            lblRegister.Text = " (" + drpMonth.SelectedValue.ToString() + "-" + drpYear.SelectedValue.ToString() + ")";
            div1.Visible = true;
            divGeading.Visible = true;
        }
        else
        {
            div1.Visible = false;
            divGeading.Visible = false;
            rptEmp.DataSource = null;
            rptEmp.DataBind();
            divExport.Visible = false;
            Campus camp = new Campus(); camp.msgbox(Page, divMsg, "Records not found!", "A");
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        loadGrid();
    }


    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptEmp.DataSource = null;
        rptEmp.DataBind();
        divExport.Visible = false;
    }

    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptEmp.DataSource = null;
        rptEmp.DataBind();
        divExport.Visible = false;
    }

    protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptEmp.DataSource = null;
        rptEmp.DataBind();
        divExport.Visible = false;
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptEmp.DataSource = null;
        rptEmp.DataBind();
        divExport.Visible = false;
    }
    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptEmp.DataSource = null;
        rptEmp.DataBind();
        divExport.Visible = false;
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "SalaryReport", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "SalaryReport.xls", divExport, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "SalaryReport", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}