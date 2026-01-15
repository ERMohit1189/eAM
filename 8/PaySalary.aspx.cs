using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaySalary : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            sql = "select DesId, DesName from DesMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpDesignation, "DesName", "DesName");
            drpDesignation.Items.Insert(0, new ListItem("<--Select-->", ""));
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
        sql = "select *,GeneratedBy, '('+format(GeneratedDate,'dd-MMM-yyyy hh:mm:ss tt')+')' GeneratedDates  from SalaryGeneratedAndPaid where BranchCode=" + Session["BranchCode"] + " and MonthName='" + drpMonth.SelectedValue.ToString() + "' and YearName='" + drpYear.SelectedValue.ToString() + "' ";
        sql = sql + " and EmpID in (select EmpID from EmpployeeOfficialDetails where BranchCode=" + Session["BranchCode"] + " and DesNameNew='" + drpDesignation.SelectedValue + "') ";
        sql = sql + " and Status='Pending' order by  Name asc";
        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count > 0)
        {
            divExport.Visible = true;
            rptEmp.DataSource = dt;
            rptEmp.DataBind();
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
            }
        }
        else
        {
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



    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtType = new DataTable();
        dtType.Columns.Add("EmpID", typeof(string));
        dtType.Columns.Add("AdvanceSalary", typeof(string));
        dtType.Columns.Add("NetPay", typeof(string));
        int sts = 0;
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
            if (chk.Checked == true)
            {

                Repeater rptHead = (Repeater)rptEmp.Items[i].FindControl("rptHead");
                if (rptHead.Items.Count > 0)
                {
                    Label EmpId = (Label)rptEmp.Items[i].FindControl("EmpId");
                    Label AdvanceSalary = (Label)rptEmp.Items[i].FindControl("AdvanceSalary");
                    Label NetPay = (Label)rptEmp.Items[i].FindControl("NetPay");
                    DataRow row = dtType.NewRow();
                    row["EmpID"] = EmpId.Text.Trim();
                    row["AdvanceSalary"] = AdvanceSalary.Text.Trim();
                    row["NetPay"] = NetPay.Text.Trim();
                    dtType.Rows.Add(row);
                    if (dtType.Rows.Count > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "SalaryGenerateAndPaidProc";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Mode", ddlMode.SelectedValue.Trim());
                            cmd.Parameters.AddWithValue("@InstumentNo", txtInstrument.Text.Trim());
                            cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
                            cmd.Parameters.AddWithValue("@MonthName", drpMonth.SelectedValue);
                            cmd.Parameters.AddWithValue("@YearName", drpYear.SelectedValue);
                            cmd.Parameters.AddWithValue("@SalaryGeneratedAndPaid_type", dtType);
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                            cmd.Parameters.AddWithValue("@PaidBy", Session["LoginName"]);
                            cmd.Parameters.AddWithValue("@Action", "paid");
                            try
                            {
                                con.Open();
                                int RowsEffected = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                con.Close();
                                if (RowsEffected > 0)
                                {
                                    sts = sts + 1;
                                }
                            }
                            catch (SqlException ex)
                            {
                                Campus camp = new Campus(); camp.msgbox(Page, divMsg2, ex.Message, "A");
                            }
                            catch (Exception ex1)
                            {
                                Campus camp = new Campus(); camp.msgbox(Page, divMsg2, ex1.Message, "A");
                            }
                        }
                    }
                }
            }
            

        }
        if (sts > 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, divMsg2, "Submitted successfully", "S");
            loadGrid();
        }
    }
}