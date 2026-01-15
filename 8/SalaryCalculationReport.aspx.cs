using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SalaryCalculationReport : Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadYear();
            loadEmpOfficialDetails();
            selectedMonth();
            BindEmpwithdrp();
        }
    }

    public void selectedMonth()
    {
        sql = "Select DatePart(mm,GETDATE()) monthvalue,DatePart(yyyy,GETDATE()) yearvalue";
        drpMonth.SelectedValue = BAL.objBal.ReturnTag(sql, "monthvalue");
        drpYear.SelectedValue = BAL.objBal.ReturnTag(sql, "yearvalue");
    }

    private void loadEmpOfficialDetails()
    {
        sql = "Select EmpDepName,EmpDepId from EmpDepMaster where BranchCode=" + Session["BranchCode"] + "";
        BAL.objBal.FillDropDown_withValue(sql, drpDepartment, "EmpDepName", "EmpDepId");
        drpDepartment.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        sql = "Select EmpDesName,EmpDesId from EmpDesMaster where BranchCode=" + Session["BranchCode"] + " ";
        BAL.objBal.FillDropDown_withValue(sql, drpDesignation, "EmpDesName", "EmpDesId");
        drpDesignation.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }
    public void loadEmp()
    {
        List<SqlParameter> param= new List<SqlParameter >();
        param.Add(new SqlParameter("@Empid", drpEmp.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Department", drpDepartment.SelectedItem.ToString()));
        param.Add(new SqlParameter("@Designation", drpDesignation.SelectedItem.ToString()));
        param.Add(new SqlParameter("@MonthsName", drpMonth.SelectedItem.ToString()));
        param.Add(new SqlParameter("@YearsName", drpYear.SelectedValue.ToString()));
        param.Add(new SqlParameter("@Sql", "S")); 
        rptEmp.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_SalaryCalculation", param);
        rptEmp.DataBind();
        
        lblTitle.Text = "Salary calculation for " + drpMonth.SelectedItem.Text.ToString() + " " + drpYear.SelectedItem.Text.ToString();
    }

    protected void loadYear()
    {
        sql = "Select years from(Select DatePart(YEAR,Getdate())-1 years Union Select DatePart(YEAR,Getdate()) years Union Select DatePart(YEAR,Getdate())+1 years)T1";
        BAL.objBal.FillDropDown_withValue(sql, drpYear, "years", "years");
        drpYear.SelectedIndex = 1;
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        loadEmp();
    }
    protected void drpEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadEmp();
    }

    public void BindEmpwithdrp()
    {
        string SQL = "";

        SQL = "Select eod.EmpId,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName Name";
        SQL = SQL + " from EmpployeeOfficialDetails eod inner";
        SQL = SQL + " join EmpGeneralDetail egd on egd.EmpId = eod.EmpId";
        SQL = SQL + " where eod.Withdrwal is null";
        if (drpDepartment.SelectedIndex != 0)
        {
            SQL = SQL + " and DepartmentName= '" + drpDepartment.SelectedItem.Text + "'";
        }
        if (drpDesignation.SelectedIndex != 0)
        {
            SQL = SQL + " and Designation = '" + drpDesignation.SelectedItem.Text + "'";
        }
        SQL = SQL + " and egd.BranchCode=" + Session["BranchCode"] + " and eod.BranchCode=" + Session["BranchCode"] + " order by EFirstName";

        DataTable dt = new DataTable();
        dt = DAL.DALInstance.GetValueInTable(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(drpEmp, dt, "Name", "EmpId", 'A');
        }
    }

    protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmpwithdrp();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ImageButton1_Click1(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "Salary", divExport);
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "Salary.xls", divExport);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "Salary", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void rptEmp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            calculateTotal(e);
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalNetBasicSalary = (Label)e.Item.FindControl("lblTotalNetBasicSalary");
            Label lblTotalNetOtherSalary = (Label)e.Item.FindControl("lblTotalNetOtherSalary");

            Label lblTotalNetSalary = (Label)e.Item.FindControl("lblTotalNetSalary");
            Label lblTotalGrossSalary = (Label)e.Item.FindControl("lblTotalGrossSalary");

            Label lblTotalPF = (Label)e.Item.FindControl("lblTotalPF");

            Label lblTotalDeduction = (Label)e.Item.FindControl("lblTotalDeduction");
            Label lblTotalBonus = (Label)e.Item.FindControl("lblTotalBonus");

            Label lblTotalSalEncash = (Label)e.Item.FindControl("lblTotalSalEncash");

            Label lblTotalSalFooter = (Label)e.Item.FindControl("lblTotalSalFooter");
            Label lblTotalCTC = (Label)e.Item.FindControl("lblTotalCTC");

            lblTotalNetBasicSalary.Text = totalnetbasicsalary.ToString(CultureInfo.InvariantCulture);
            lblTotalNetOtherSalary.Text = totalnetothersalary.ToString(CultureInfo.InvariantCulture);

            lblTotalNetSalary.Text = totalnetsalary.ToString(CultureInfo.InvariantCulture);
            lblTotalGrossSalary.Text = totalgrosssalary.ToString(CultureInfo.InvariantCulture);
            lblTotalPF.Text = totalpf.ToString(CultureInfo.InvariantCulture);

            lblTotalDeduction.Text = totaldeduction.ToString(CultureInfo.InvariantCulture);
            lblTotalBonus.Text = totalbonus.ToString(CultureInfo.InvariantCulture);


            lblTotalSalEncash.Text = totalclincash.ToString(CultureInfo.InvariantCulture);
            lblTotalSalFooter.Text = totalsalfooter.ToString(CultureInfo.InvariantCulture);

            lblTotalCTC.Text = totalctc.ToString(CultureInfo.InvariantCulture);
        }
    }

    decimal totalnetbasicsalary = 0;
    decimal totalnetothersalary = 0;
    decimal totalnetsalary = 0;
    decimal totalgrosssalary = 0;
    decimal totalpf = 0;
    decimal totaldeduction = 0;
    decimal totalbonus = 0;
    decimal totalclincash = 0;
    decimal totalsalfooter = 0;
    decimal totalctc = 0;
    public void calculateTotal(RepeaterItemEventArgs e)
    {
        Label lblBasicSalary = (Label)e.Item.FindControl("lblNetBasicSalary");
        decimal basicsalary = 0;
        decimal.TryParse(lblBasicSalary.Text, out basicsalary);
        totalnetbasicsalary = totalnetbasicsalary + basicsalary;

        Label lblOtherSalary = (Label)e.Item.FindControl("lblNetOtherSalary");
        decimal othersalary = 0;
        decimal.TryParse(lblOtherSalary.Text, out othersalary);
        totalnetothersalary = totalnetothersalary + othersalary;

        Label lblNetSalary = (Label)e.Item.FindControl("lblNetSalary");
        decimal netsalary = 0;
        decimal.TryParse(lblNetSalary.Text, out netsalary);
        totalnetsalary = totalnetsalary + netsalary;

        Label lblGrossSalary = (Label)e.Item.FindControl("lblGrossSalary");
        decimal grosssalary = 0;
        decimal.TryParse(lblGrossSalary.Text, out grosssalary);
        totalgrosssalary = totalgrosssalary + grosssalary;

        Label lblpf = (Label)e.Item.FindControl("lblPf");
        decimal pf = 0;
        decimal.TryParse(lblpf.Text, out pf);
        totalpf = totalpf + pf;

        Label lblDeduction = (Label)e.Item.FindControl("lblDeduction");
        decimal deduction = 0;
        decimal.TryParse(lblDeduction.Text, out deduction);
        totaldeduction = totaldeduction + deduction;

        Label lblBonus = (Label)e.Item.FindControl("lblBonus");
        decimal bonus = 0;
        decimal.TryParse(lblBonus.Text, out bonus);
        totalbonus = totalbonus + bonus;

        Label lblCLinCash = (Label)e.Item.FindControl("lblCLinCash");
        decimal clincash = 0;
        decimal.TryParse(lblCLinCash.Text, out clincash);
        totalclincash = totalclincash + clincash;

        totalsalfooter = totalsalfooter + clincash + grosssalary;

        Label lblCTC = (Label)e.Item.FindControl("lblCTC");
        decimal ctc = 0;
        decimal.TryParse(lblCTC.Text, out ctc);
        totalctc = totalctc + ctc;
        
    }


    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmpwithdrp();
    }
}