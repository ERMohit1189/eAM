using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class admin_SalaryCalculation : Page
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
        string _sql = "select count(*)cnt from SalaryMonthlyHoliday where BranchCode=" + Session["BranchCode"] + " and format(MonthDate, '-MMM-yyyy')='-" + drpMonth.SelectedValue + "-" + drpYear.SelectedValue + "'";
        if (oo.ReturnTag(_sql, "cnt") == "0")
        {
            Campus camp = new Campus(); camp.msgbox(Page, divMsg, "Please create holiday master first!", "A");
            divExport.Visible = false;
            return;
        }
        string _sql1 = "select count(*)cnt from RulesForSalaryCalculation where BranchCode=" + Session["BranchCode"] + " and DesId=(select distinct DesId from DesMaster where BranchCode=" + Session["BranchCode"] + " and DesName='" + drpDesignation.SelectedItem + "')";
        if (oo.ReturnTag(_sql1, "cnt") == "0")
        {
            Campus camp = new Campus(); camp.msgbox(Page, divMsg, "Please create rules master first!", "A");
            divExport.Visible = false;
            return;
        }

        if (ddlSalary.SelectedItem.Text == "Not Calculated")
        {
            sql = "select distinct o.EmpId, EFirstName+' '+EMiddleName+' '+ELastName name, EMobileNo, RegistrationDate from EmpployeeOfficialDetails o inner join EmpGeneralDetail g on g.EmpId=o.EmpId and g.BranchCode=o.BranchCode where o.BranchCode=" + Session["BranchCode"] + " and o.DesNameNew='" + drpDesignation.SelectedValue + "' and isnull(o.Withdrwal,'')='' and o.empid not in (select EmpID from SalaryGeneratedAndPaid where BranchCode=" + Session["BranchCode"] + " and MonthName='" + drpMonth.SelectedValue.ToString() + "' and YearName='" + drpYear.SelectedValue.ToString() + "') order by  (EFirstName+' '+EMiddleName+' '+ELastName) asc";
        }
        else if (ddlSalary.SelectedItem.Text == "Calculated")
        {
            sql = "select distinct o.EmpId, EFirstName+' '+EMiddleName+' '+ELastName name, EMobileNo, RegistrationDate from EmpployeeOfficialDetails o inner join EmpGeneralDetail g on g.EmpId=o.EmpId and g.BranchCode=o.BranchCode where o.BranchCode=" + Session["BranchCode"] + " and o.DesNameNew='" + drpDesignation.SelectedValue + "' and isnull(o.Withdrwal,'')='' and o.empid in (select EmpID from SalaryGeneratedAndPaid where BranchCode=" + Session["BranchCode"] + " and MonthName='" + drpMonth.SelectedValue.ToString() + "' and YearName='" + drpYear.SelectedValue.ToString() + "') order by  (EFirstName+' '+EMiddleName+' '+ELastName) asc";
        }
        else
        {
            sql = "select distinct o.EmpId, EFirstName+' '+EMiddleName+' '+ELastName name, EMobileNo, RegistrationDate from EmpployeeOfficialDetails o inner join EmpGeneralDetail g on g.EmpId=o.EmpId and g.BranchCode=o.BranchCode where o.BranchCode=" + Session["BranchCode"] + " and o.DesNameNew='" + drpDesignation.SelectedValue + "' and isnull(o.Withdrwal,'')='' order by  (EFirstName+' '+EMiddleName+' '+ELastName) asc";
        }

        var dt = oo.Fetchdata(sql);
        if (dt.Rows.Count > 0)
        {
            rptEmp.DataSource = dt;
            rptEmp.DataBind();
            int sts = 0;
            for (int i = 0; i < rptEmp.Items.Count; i++)
            {
                Label EmpId = (Label)rptEmp.Items[i].FindControl("EmpId");
                List<SqlParameter> param = new List<SqlParameter>();
                if (EmpId.Text.Trim() == "41")
                {

                }
                param.Add(new SqlParameter("@EmpId", EmpId.Text.Trim()));
                param.Add(new SqlParameter("@Designation", drpDesignation.SelectedItem.Text));
                param.Add(new SqlParameter("@month", drpMonth.SelectedValue.ToString()));
                param.Add(new SqlParameter("@year", drpYear.SelectedValue.ToString()));
                param.Add(new SqlParameter("@JoiningDate", Convert.ToDateTime(dt.Rows[i]["RegistrationDate"].ToString() == "" ? DateTime.Now.ToString() : dt.Rows[i]["RegistrationDate"].ToString()).ToString("dd-MMM-yyyy")));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                try
                {
                    DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("SalaryCalculationProc", param);
                    if (ds != null)
                    {
                        if (rptEmp.Items.Count > 0)
                        {
                            Label lblcolor = (Label)rptEmp.Items[i].FindControl("lblcolor");
                            lblcolor.CssClass = ds.Tables[0].Rows[0]["isdifferNetpay"].ToString();
                            //HtmlTableRow feeheadrow = (HtmlTableRow)rptEmp.Items[i].FindControl("feeheadrow");
                            //feeheadrow.Attributes.Add("class", ds.Tables[0].Rows[0]["isdifferNetpay"].ToString());
                            //if (int.Parse(ds.Tables[0].Rows[i]["TotalDaysOfMonth"].ToString()) != int.Parse(ds.Tables[2].Rows[0]["attDayCount"].ToString()))
                            //{
                            //    rptEmp.Items[i].Visible = false;
                            //}
                            //else
                            //{
                            Label TotalDaysOfMonth = (Label)rptEmp.Items[i].FindControl("TotalDaysOfMonth");
                            TotalDaysOfMonth.Text = ds.Tables[0].Rows[0]["TotalDaysOfMonth"].ToString();
                            Label DeductedDaysOfMonth = (Label)rptEmp.Items[i].FindControl("DeductedDaysOfMonth");
                            DeductedDaysOfMonth.Text = ds.Tables[0].Rows[0]["DeductedDaysOfMonth"].ToString();
                            Label TotalAttendance = (Label)rptEmp.Items[i].FindControl("TotalAttendance");
                            TotalAttendance.Text = ds.Tables[0].Rows[0]["TotalAttendance"].ToString();
                            Label CTC = (Label)rptEmp.Items[i].FindControl("CTC");
                            CTC.Text = ds.Tables[0].Rows[0]["CTC"].ToString();
                            Label Earning = (Label)rptEmp.Items[i].FindControl("Earning");
                            Earning.Text = ds.Tables[0].Rows[0]["Earning"].ToString();
                            Label Deduction = (Label)rptEmp.Items[i].FindControl("Deduction");
                            Deduction.Text = ds.Tables[0].Rows[0]["Deduction"].ToString();
                            Label Total = (Label)rptEmp.Items[i].FindControl("Total");
                            Total.Text = ds.Tables[0].Rows[0]["Total"].ToString();
                            Label Netpay = (Label)rptEmp.Items[i].FindControl("Netpay");
                            Netpay.Text = ds.Tables[0].Rows[0]["NetPay"].ToString();
                            Label AdvanceSalary = (Label)rptEmp.Items[i].FindControl("AdvanceSalary");
                            AdvanceSalary.Text = ds.Tables[0].Rows[0]["AdvanceSalary"].ToString();
                            TextBox txtAdvanceSalary = (TextBox)rptEmp.Items[i].FindControl("txtAdvanceSalary");
                            txtAdvanceSalary.Text = ds.Tables[0].Rows[0]["advGenerate"].ToString();
                            double advance = 0;
                            double.TryParse(AdvanceSalary.Text, out advance);
                            if (advance > 0)
                            {
                                txtAdvanceSalary.Visible = true;
                            }
                            Repeater rptHead = (Repeater)rptEmp.Items[i].FindControl("rptHead");
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                rptHead.DataSource = ds.Tables[1];
                                rptHead.DataBind();
                            }
                            sts = sts + 1;
                            string _sql11 = "select count(*)cnt from SalarySetMaster where BranchCode=" + Session["BranchCode"] + " and EmpID='" + dt.Rows[i]["EmpID"].ToString() + "'";
                            if (oo.ReturnTag(_sql11, "cnt") == "0")
                            {
                                CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
                                rptEmp.Items[i].Controls.Remove(chk);
                            }
                            // }
                        }
                        else
                        {
                            string _sql11 = "select count(*)cnt from SalarySetMaster where BranchCode=" + Session["BranchCode"] + " and EmpID='" + dt.Rows[i]["EmpID"].ToString() + "'";
                            if (oo.ReturnTag(_sql11, "cnt") == "0")
                            {
                                CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
                                rptEmp.Items[i].Controls.Remove(chk);
                            }
                        }
                    }
                    else
                    {
                        string _sql11 = "select count(*)cnt from SalarySetMaster where BranchCode=" + Session["BranchCode"] + " and EmpID='" + dt.Rows[i]["EmpID"].ToString() + "'";
                        if (oo.ReturnTag(_sql11, "cnt") == "0")
                        {
                            CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
                            rptEmp.Items[i].Controls.Remove(chk);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
                    rptEmp.Items[i].Controls.Remove(chk);
                }
            }
            if (sts == 0)
            {
                divExport.Visible = false;
            }
            else
            {
                divExport.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "setColor();", true);
            }
        }
        else
        {
            rptEmp.DataSource = null;
            rptEmp.DataBind();
            divExport.Visible = false;
            Campus camp = new Campus();
            if (ddlSalary.SelectedItem.Text == "Not Calculated")
            {
                camp.msgbox(Page, divMsg, "Salary of " + drpMonth.SelectedValue + " " + drpYear.SelectedValue + " has been paid!", "A");
            }
        }
    }



    protected void lnkCalculated_Click(object sender, EventArgs e)
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
        int sts = 0;
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            Repeater rptHead = (Repeater)rptEmp.Items[i].FindControl("rptHead");
            CheckBox chk = (CheckBox)rptEmp.Items[i].FindControl("chk");
            if (rptHead.Items.Count > 0 && chk.Checked)
            {
                Label EmpId = (Label)rptEmp.Items[i].FindControl("EmpId");
                Label Name = (Label)rptEmp.Items[i].FindControl("Name");
                Label EMobileNo = (Label)rptEmp.Items[i].FindControl("EMobileNo");
                Label TotalDaysOfMonth = (Label)rptEmp.Items[i].FindControl("TotalDaysOfMonth");
                Label DeductedDaysOfMonth = (Label)rptEmp.Items[i].FindControl("DeductedDaysOfMonth");
                Label TotalAttendance = (Label)rptEmp.Items[i].FindControl("TotalAttendance");

                DataTable dtComponent = new DataTable();
                dtComponent.Columns.Add("EmpID", typeof(string));
                dtComponent.Columns.Add("ComponentId", typeof(string));
                dtComponent.Columns.Add("ComponentName", typeof(string));
                dtComponent.Columns.Add("ComponentValue", typeof(string));
                dtComponent.Columns.Add("MonthName", typeof(string));
                dtComponent.Columns.Add("YearName", typeof(string));
                for (int j = 0; j < rptHead.Items.Count; j++)
                {
                    Label ComponentId = (Label)rptHead.Items[j].FindControl("ComponentId");
                    Label ComponentName = (Label)rptHead.Items[j].FindControl("ComponentName");
                    Label Value = (Label)rptHead.Items[j].FindControl("Value");
                    DataRow row = dtComponent.NewRow();
                    row["EmpID"] = EmpId.Text.Trim();
                    row["ComponentId"] = ComponentId.Text.Trim();
                    row["ComponentName"] = ComponentName.Text.Trim();
                    row["ComponentValue"] = (Value.Text.Trim() == "" ? "0" : Value.Text.Trim());
                    row["MonthName"] = drpMonth.SelectedValue;
                    row["YearName"] = drpYear.SelectedValue;
                    dtComponent.Rows.Add(row);
                }
                Label CTC = (Label)rptEmp.Items[i].FindControl("CTC");
                Label Earning = (Label)rptEmp.Items[i].FindControl("Earning");
                Label Deduction = (Label)rptEmp.Items[i].FindControl("Deduction");
                Label Total = (Label)rptEmp.Items[i].FindControl("Total");
                TextBox txtAdvanceSalary = (TextBox)rptEmp.Items[i].FindControl("txtAdvanceSalary");
                Label NetPay = (Label)rptEmp.Items[i].FindControl("Netpay");


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SalaryGenerateAndPaidProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@EmpID", EmpId.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMobileNo", EMobileNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@MonthName", drpMonth.SelectedValue);
                    cmd.Parameters.AddWithValue("@YearName", drpYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@TotalDaysOfMonth", (TotalDaysOfMonth.Text.Trim() == "" ? "0" : TotalDaysOfMonth.Text.Trim()));
                    cmd.Parameters.AddWithValue("@DeductedDaysOfMonth", (DeductedDaysOfMonth.Text.Trim() == "" ? "0" : DeductedDaysOfMonth.Text.Trim()));
                    cmd.Parameters.AddWithValue("@TotalAttendance", (TotalAttendance.Text.Trim() == "" ? "0" : TotalAttendance.Text.Trim()));
                    cmd.Parameters.AddWithValue("@SalaryGeneratedAndPaidComponent_table_type", dtComponent);

                    cmd.Parameters.AddWithValue("@CTC", (CTC.Text.Trim() == "" ? "0" : CTC.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Earning", (Earning.Text.Trim() == "" ? "0" : Earning.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Deduction", (Deduction.Text.Trim() == "" ? "0" : Deduction.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Total", (Total.Text.Trim() == "" ? "0" : Total.Text.Trim()));
                    cmd.Parameters.AddWithValue("@AdvanceSalary", (txtAdvanceSalary.Text.Trim() == "" ? "0" : txtAdvanceSalary.Text.Trim()));
                    cmd.Parameters.AddWithValue("@NetPay", (double.Parse(Total.Text.Trim() == "" ? "0" : Total.Text.Trim()) - double.Parse(txtAdvanceSalary.Text.Trim() == "" ? "0" : txtAdvanceSalary.Text.Trim())).ToString("0.00"));

                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    cmd.Parameters.AddWithValue("@GeneratedBy", Session["LoginName"]);
                    cmd.Parameters.AddWithValue("@Action", "generate");
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
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        if (sts > 0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, divMsg2, "Submit successfully", "S");
            loadGrid();
        }
    }

    protected void ddlSalary_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}