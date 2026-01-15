using System.Web.UI;
using System.Data.SqlClient;
using System;

public partial class admin_EmployeeAttendanceDayWiseReport : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
     
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 

        if (!IsPostBack)
        {
            sql = "select EmpDepName from EmpDepMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, DrpDepartment, "EmpDepName");
            oo.AddDateMonthYearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
            FindCurrentDateandSetinDropDown();
        }
        
    }

   
    protected void DrpDDEmpYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
    }
    protected void DrpDDEmpMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpDDEmpYY.Text = yy;
        if (mm == "1")
        {
            DrpDDEmpMM.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpDDEmpMM.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpDDEmpMM.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpDDEmpMM.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpDDEmpMM.Text = "May";
        }
        else if (mm == "6")
        {
            DrpDDEmpMM.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpDDEmpMM.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpDDEmpMM.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpDDEmpMM.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpDDEmpMM.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpDDEmpMM.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpDDEmpMM.Text = "Dec";
        }


        DrpDDEmpDD.Text = dd;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string dd = "";
        if (DrpDDEmpDD.SelectedItem.ToString().Length == 1)
        {
            dd = "0" + DrpDDEmpDD.SelectedItem.ToString();
        }
        else
        {
            dd = DrpDDEmpDD.SelectedItem.ToString();
        }
        sql = "select ad.Id,ad.CategoryWise,ad.AttendanceMonth ,ad.AttendanceDate,ad.DepartmentName,ad.Ecode ,ad.EmpId ,EG.EMobileNo,EG.EFirstName,EG.EMiddleName,EG.ELastName, ";
        sql = sql + "  ad.EmployeeName,ad.AttendanceValue,ad.BranchCode,ad.LoginName,ad.SessionName,ad.RecordDate from EmployeeAttendanceDayWise  ad";
        sql = sql + "  left join EmpGeneralDetail EG   on ad.Ecode=EG.Ecode ";
        sql = sql + "  left join EmpployeeOfficialDetails EO  on ad.Ecode=EO.Ecode ";
        sql = sql + "  where convert(date,ad.AttendanceDate)=convert(date,'" + dd + " " + DrpDDEmpMM.SelectedItem.ToString() + " " + DrpDDEmpYY.SelectedItem.ToString() + "')";
        sql = sql + "  and ad.CategoryWise='Date Wise'";
        if (DrpDepartment.SelectedIndex != 0)
        {
            sql = sql + "  and Eo.DepartmentName='"+ DrpDepartment.SelectedItem.Text.Trim() +"'";
        }
        sql = sql + "  and ad.BranchCode=" + Session["BranchCode"].ToString() + " and eo.BranchCode=" + Session["BranchCode"].ToString() + " and eg.BranchCode=" + Session["BranchCode"].ToString() + " Order by EG.EFirstName";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();

        if (Grd1.Rows.Count == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No record found!", "A");       

        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (Grd1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "EmployeeAttendanceDayWiseReport.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (Grd1.Rows.Count > 0)
        {
            oo.ExportToExcel("EmployeeAttendanceDayWiseReport.xls", Grd1);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}