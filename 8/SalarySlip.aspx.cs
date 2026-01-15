using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _8_SalarySlip : System.Web.UI.Page
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
        Campus camp = new Campus();
        //camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            
            loadYear();
            selectedMonth();
            LoadEmployee();
            //BindPaySlipData();
        }
    }
    public void selectedMonth()
    {
        sql = "Select format(getdate(),'yyyy') yearvalue";
        drpYear.SelectedValue = BAL.objBal.ReturnTag(sql, "yearvalue");
        sql = "Select format(getdate(),'MMM') monthvalue";
        drpMonth.SelectedValue = BAL.objBal.ReturnTag(sql, "monthvalue");
    }
    public void LoadEmployee()
    {
        if (Session["Logintype"].ToString() == "Staff")
        {
            sql = "select distinct o.EmpId, EFirstName+' '+EMiddleName+' '+ELastName name, EMobileNo, RegistrationDate from EmpployeeOfficialDetails o inner join EmpGeneralDetail g on g.EmpId=o.EmpId and g.BranchCode=o.BranchCode where o.BranchCode=" + Session["BranchCode"] + " and o.Ecode='" + Session["LoginName"].ToString() + "'  and isnull(o.Withdrwal,'')='' order by  (EFirstName+' '+EMiddleName+' '+ELastName) asc";
        }
        else
        {
            sql = "select distinct o.EmpId, EFirstName+' '+EMiddleName+' '+ELastName name, EMobileNo, RegistrationDate from EmpployeeOfficialDetails o inner join EmpGeneralDetail g on g.EmpId=o.EmpId and g.BranchCode=o.BranchCode where o.BranchCode=" + Session["BranchCode"] + "  and isnull(o.Withdrwal,'')='' order by  (EFirstName+' '+EMiddleName+' '+ELastName) asc";
        }
        // ddlSalary.SelectedValue = BAL.objBal.ReturnTag(sql, "EmpId");
        BAL.objBal.FillDropDown_withValue(sql, ddlSalary, "name", "EmpId");
        ddlSalary.Items.Insert(0, new ListItem("<--Select-->", "0"));
        ddlSalary.SelectedIndex = 0;
    }

    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void loadYear()
    {
        sql = "Select years from(Select DatePart(YEAR,Getdate())-1 years Union Select DatePart(YEAR,Getdate()) years Union Select DatePart(YEAR,Getdate()) years)T1";
        BAL.objBal.FillDropDown_withValue(sql, drpYear, "years", "years");
        drpYear.SelectedIndex = 1;
    }

    protected void lnkCalculated_Click(object sender, EventArgs e)
    {
        if(ddlSalary.SelectedValue!="0")
        {
            BindPaySlipData();
        }
      
    }
    private void BindPaySlipData()
    {
        string sqldetail = "Select *,(Select CityName from CityMaster where Id=clg.CityId)CityName from CollegeMaster clg where BranchCode=" + Session["BranchCode"] + "";
        lblSchoolName.Text = BAL.objBal.ReturnTag(sqldetail, "CollegeName");
        string CollegeAdd1 = BAL.objBal.ReturnTag(sqldetail, "CollegeAdd1");
        string CollegeAdd2 = BAL.objBal.ReturnTag(sqldetail, "CollegeAdd2");
        string CityName = BAL.objBal.ReturnTag(sqldetail, "CityName");
        lblSchoolAddress.Text = CollegeAdd1+" "+ CollegeAdd2+" "+ CityName;

        string shortMonth = drpMonth.SelectedValue;  // yeh aapko DB ya dropdown se milega
        DateTime monthDate = DateTime.ParseExact(shortMonth, "MMM", System.Globalization.CultureInfo.InvariantCulture);
        string fullMonthName = monthDate.ToString("MMMM");  // => "January"

        Label1.Text = "Pay Slip for the month of "+ fullMonthName+" "+ drpYear.SelectedValue;


        string sql = "SELECT *, GeneratedBy, '('+FORMAT(GeneratedDate,'dd-MMM-yyyy hh:mm:ss tt')+')' AS GeneratedDates " +
             "FROM SalaryGeneratedAndPaid as sal join EmpployeeOfficialDetails emp on emp.EmpId = sal.EmpID" +
             " WHERE sal.BranchCode = " + Session["BranchCode"] +
             " AND MonthName = '" + drpMonth.SelectedValue + "'" +
             " AND YearName = '" + drpYear.SelectedValue + "'" +
             " AND sal.EmpID IN (SELECT EmpID FROM EmpployeeOfficialDetails WHERE BranchCode = " + Session["BranchCode"] + ")" +
             " AND sal.EmpID = '" + ddlSalary.SelectedValue + "' AND Status = 'Paid' ORDER BY Name ASC";

        DataTable dt = oo.Fetchdata(sql);

        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow row1 = dt.Rows[0];

            lblEmpID.Text = row1["Ecode"].ToString();
            lblAccountNo.Text = row1["AccountNo"].ToString();
            lblName.Text = row1["Name"].ToString();
            lblBranchIFSC.Text = row1["IFSC"].ToString(); // Make sure this column exists or combine
            lblDesignation.Text = row1["Designation"].ToString();
            lblBankName.Text = row1["BankName"].ToString();
            lblDOJ.Text = Convert.ToDateTime(row1["RegistrationDate"]).ToString("dd MMM yyyy");
            lblPayableDays.Text = row1["TotalAttendance"].ToString();
            lblPFNo.Text = row1["PFNo"].ToString();
            lblPAN.Text = row1["PANno"].ToString();
            lblESIC.Text = row1["EsicNo"].ToString();
            lblUAN.Text = row1["UAN"].ToString();

            string sql1 = "select '' as Arrear, *,(Select ComponentType from SalaryComponent where ID = comp.ComponentId)ComponentType,(Select CTC from SalarySetMaster where EmpId = comp.EmpId and ComponentId = comp.ComponentId) CTC,(Select ComponentValue from SalarySetMaster where EmpId = comp.EmpId and ComponentId = comp.ComponentId) ReteComponentValue from SalaryGeneratedAndPaidComponent as comp where BranchCode=" + Session["BranchCode"] + " and MonthName='" + drpMonth.SelectedValue.ToString() + "' and YearName='" + drpYear.SelectedValue.ToString() + "' and EmpId='" + ddlSalary.SelectedValue.Trim() + "' ";
            var dt1 = oo.Fetchdata(sql1);
            List<SalaryComponent> earnings = new List<SalaryComponent>();
            List<SalaryComponent> deductions = new List<SalaryComponent>();
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    var component = new SalaryComponent
                    {
                        Description = row["ComponentName"].ToString(), // Or use: row["Name"] if column is named that way
                        Rate = row["ReteComponentValue"].ToString(),
                        Monthly = row["ComponentValue"].ToString(),
                        Arrear = row["Arrear"].ToString(),
                        Total = row["ComponentValue"].ToString()
                    };

                    string type = row["ComponentType"].ToString().Trim().ToLower();

                    if (type == "earning")
                    {
                        earnings.Add(component);
                    }
                    else if (type == "deduction")
                    {
                        deductions.Add(component);
                    }
                }

                decimal grossPayRate = 0, grossPayMonthly = 0, grossPayArrear = 0, grossPayTotal = 0;

                foreach (var item in earnings)
                {
                    decimal rate, monthly, arrear, total;

                    if (decimal.TryParse(item.Rate, out rate)) grossPayRate += rate;
                    if (decimal.TryParse(item.Monthly, out monthly)) grossPayMonthly += monthly;
                    if (decimal.TryParse(item.Arrear, out arrear)) grossPayArrear += arrear;
                    if (decimal.TryParse(item.Total, out total)) grossPayTotal += total;
                }

                earnings.Add(new SalaryComponent
                {
                    Description = "Gross Pay",
                    Rate = grossPayRate.ToString("0.##"),
                    Monthly = grossPayMonthly.ToString("0.##"),
                    Arrear = "",
                    Total = grossPayTotal.ToString("0.##")
                });

                decimal grossDedRate = 0, grossDedMonthly = 0, grossDedArrear = 0, grossDedTotal = 0;

                foreach (var item in deductions)
                {
                    decimal rate, monthly, arrear, total;

                    if (decimal.TryParse(item.Rate, out rate)) grossDedRate += rate;
                    if (decimal.TryParse(item.Monthly, out monthly)) grossDedMonthly += monthly;
                    if (decimal.TryParse(item.Arrear, out arrear)) grossDedArrear += arrear;
                    if (decimal.TryParse(item.Total, out total)) grossDedTotal += total;
                }

                // Add summary row
                deductions.Add(new SalaryComponent
                {
                    Description = "Gross Deduction",
                    Rate = grossDedRate.ToString("0.##"),
                    Monthly = grossDedMonthly.ToString("0.##"),
                    Arrear = "",
                    Total = grossDedTotal.ToString("0.##")
                });

                decimal grossPay = 0;

                foreach (var item in earnings)
                {
                    decimal total;
                    if (decimal.TryParse(item.Total, out total))
                    {
                        grossPay += total;
                    }
                }
                // Now bind the repeater/gridview here
                rptEarnings.DataSource = earnings;
                rptEarnings.DataBind();
               // lblGrossPay.Text = grossPay.ToString("0.##");

                decimal grossDeduction = 0;

                foreach (var item in deductions)
                {
                    decimal total;
                    if (decimal.TryParse(item.Total, out total))
                    {
                        grossDeduction += total;
                    }
                }
                //lblGrossDeduction.Text = grossDeduction.ToString("0.##");
                rptDeductions.DataSource = deductions;
                rptDeductions.DataBind();
                decimal netPay = grossPay - grossDeduction;

                // Show amount + in words
                int netPayInt = (int)Math.Round(netPay); // Round and convert to int
                string netPayWords = NumberToWords(netPayInt);

                lblNetPay.Text = netPayInt.ToString() + " (" + netPayWords + " Only)";
                btndivNew.Visible = true;
                divExport.Visible = true;
            }
            else
            {
                btndivNew.Visible = false;
                divExport.Visible = false;
            }
        }
        else
        {
            btndivNew.Visible = false;
            divExport.Visible = false;
        }


    }
    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "Zero";

        if (number < 0)
            return "Minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 10000000) > 0)
        {
            words += NumberToWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 100000) > 0)
        {
            words += NumberToWords(number / 100000) + " Lakh ";
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "") words += "and ";

            string[] unitsMap = new string[]
            {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six",
            "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
            "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen",
            "Eighteen", "Nineteen"
            };
            string[] tensMap = new string[]
            {
            "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty",
            "Sixty", "Seventy", "Eighty", "Ninety"
            };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words.Trim();
    }


    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "SalarySlip.doc", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
       // oo.ExportToExcel("ListofAllDealers.xls", divExport);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}
public class SalaryComponent
{
    public string Description { get; set; }
    public string Rate { get; set; }
    public string Monthly { get; set; }
    public string Arrear { get; set; }
    public string Total { get; set; }
}
