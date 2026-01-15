using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class admin_MonthwiseEmployeeAttendenceReport_New : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            abc.Visible = false;
            fillmonth();
            loadDepartment();
            table1.Visible = false;
            table2.Visible = false;
            table3.Visible = false;
            table4.Visible = false;
        }
    }
    public void loadDepartment()
    {
        sql = "select EmpDepName,EmpDepId from EmpDepMaster";
        oo.FillDropDown_withValue(sql, drpDepartment, "EmpDepName", "EmpDepId");
        drpDepartment.Items.Insert(0, "<--Select-->");
    }
   
    public void fillmonth()
    {
        //sql = "WITH months(MonthNumber) AS(SELECT 6 UNION ALL SELECT MonthNumber+1 FROM months WHERE MonthNumber < 17)";
        //sql = sql + " SELECT Left(DATENAME(MONTH,DATEADD(MONTH,MonthNumber,GETDATE())),3) AS monthname,";
        //sql = sql + " DATEpart(MONTH,DATEADD(MONTH,MonthNumber,GETDATE())) as monthvalue FROM months";
        sql = "with months (date)";
        sql = sql + " AS (SELECT Convert(date, (Select FromDate from SessionMaster where SessionName = '" + Session["SessionName"] + "'))";
        sql = sql + " UNION ALL";
        sql = sql + " SELECT DATEADD(month, 1, date)";
        sql = sql + " from months";
        sql = sql + " where date <= Convert(date, (Select ToDate from SessionMaster where SessionName = '" + Session["SessionName"] + "')))";
        sql = sql + " select Left(Datename(month,date),3)+' '+Convert(Varchar(5),Datepart(YEAR,date)) monthname,Convert(Varchar(5),Datepart(month,date))+'-'+Convert(Varchar(5),Datepart(year,date)) monthvalue from months";

        oo.FillDropDown_withValue(sql, drpMonth, "monthname", "monthvalue");
        oo.FillDropDown_withValue(sql, drpMonth1, "monthname", "monthvalue");
        drpMonth1.Items.Insert(0, "<--Select-->");
        drpMonth1.SelectedIndex = 1;
    }
    
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        loadgrid();
    }

    public void loadgrid()
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            sql = "select EO.EmpId,(EG.EFirstName+' '+EG.EMiddleName+' '+EG.ELastName) as Name from EmpployeeOfficialDetails EO ";
            sql = sql + " left join EmpGeneralDetail EG on EO.Ecode=EG.Ecode where Eo.DepartmentName='" + drpDepartment.SelectedItem.ToString() + "'";
            sql = sql + " and Eo.Withdrwal is null and eo.BranchCode=" + Session["BranchCode"].ToString() + " order by Name asc";

        }
        else
        {
            Label lblEmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
            Label lbllblDepartment = (Label)Grd.Rows[0].FindControl("lblDepartment");
            sql = "select EO.EmpId,(EG.EFirstName+' '+EG.EMiddleName+' '+EG.ELastName) as Name from EmpployeeOfficialDetails EO ";
            sql = sql + " left join EmpGeneralDetail EG on EO.Ecode=EG.Ecode where Eo.DepartmentName='" + lbllblDepartment.Text + "'";
            sql = sql + " and Eo.Withdrwal is null and EO.EmpId='" + lblEmpId.Text.Trim() + "' and eo.BranchCode=" + Session["BranchCode"].ToString() + " order by Name asc";

        }

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            abc.Visible = true;
            //if (RadioButtonList1.SelectedIndex == 0)
            //{
            //    Label1.Text = drpMonth.SelectedItem.ToString() + " Month Employee Attendence Report";
            //}
            //else
            //{
            //    Label1.Text = drpMonth1.SelectedItem.ToString() + " Month Employee Attendence Report";
            //}
            adddayofmonth();
            name();
        }
        else
        {
            abc.Visible = false;
        }
    }

    public void adddayofmonth()
    {
        //string year;
        //string[] str = Session["SessionName"].ToString().Split('-');
        //int monthno = oo.CurrentMonthCheckValue(drpMonth.SelectedItem.Text);
        //if (monthno <= 9)
        //{
        //    year = str[0].ToString();
        //}
        //else
        //{
        //    year = str[1].ToString();
        //}
        //string date1;
        //if (RadioButtonList1.SelectedIndex == 0)
        //{
        //    date1 = year + "-" + drpMonth.SelectedValue.ToString() + "-" + "01";
        //}
        //else
        //{
        //    date1 = year + "-" + drpMonth1.SelectedValue.ToString() + "-" + "01";
        //}
        //sql = "WITH months(DATENumber) AS(SELECT 0 UNION ALL SELECT DATENumber+1 FROM months WHERE DATENumber < datediff(day, '" + date1 + "', dateadd(month, 1, '" + date1 + "'))-1)";
        //sql = sql + " Select Convert(int,DATENAME(dd,dateadd(dd,DATENumber,'" + date1 + "'))) as dayno,Left((DATENAME(DW,dateadd(DD,DATENumber,'" + date1 + "'))),3) as dayname FROM months order by dayno Asc";
        string year;
        // ReSharper disable once UnusedVariable
        string[] str = Session["SessionName"].ToString().Split('-');
        int monthno = Convert.ToInt32(drpMonth.SelectedValue.Split('-')[0]);
        year = drpMonth.SelectedItem.Text.Split(' ')[1];
        //if (monthno <= 9)
        //{           
        //    year = str[0].ToString();
        //}
        //else
        //{
        //    year = str[1].ToString();
        //}
        string date1;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            date1 = year + "-" + monthno + "-" + "01";
        }
        else
        {
            date1 = year + "-" + monthno + "-" + "01";
        }
        sql = "WITH months(DATENumber) AS(SELECT 0 UNION ALL SELECT DATENumber+1 FROM months WHERE DATENumber < datediff(day, '" + date1 + "', dateadd(month, 1, '" + date1 + "'))-1)";
        sql = sql + " Select Convert(int,DATENAME(dd,dateadd(dd,DATENumber,'" + date1 + "'))) as dayno,Left((DATENAME(DW,dateadd(DD,DATENumber,'" + date1 + "'))),3) as dayname FROM months order by dayno Asc";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GridView1.HeaderRow.Cells[i + 3].Text = dt.Rows[i][0].ToString() + " " + Environment.NewLine + dt.Rows[i][1].ToString();
                }
            }
        }
    }
    public void name()
    {
        int jj = 0;
        int jj1 = 0;
        int jj2 = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            jj = 0;
            jj1 = 0;
            jj2 = 0;
           
            Label lblEmpid = (Label)GridView1.Rows[i].FindControl("Label2");
            string month = "", departmentName = "";
            string year = drpMonth.SelectedItem.Text.Split(' ')[1];

            if (RadioButtonList1.SelectedIndex == 0)
            {
                month = drpMonth.SelectedItem.Text.Split(' ')[0];
                departmentName = drpDepartment.SelectedItem.ToString();
            }
            else
            {
                month = drpMonth1.SelectedItem.Text.Split(' ')[0];
                if (Grd.Rows.Count > 0)
                {
                    Label lblDepartment = (Label)Grd.Rows[0].FindControl("lblDepartment");
                    departmentName = lblDepartment.Text;
                }
            }
            for (int j = 3; j < GridView1.Columns.Count - 3; j++)
            {
                string[] str = GridView1.HeaderRow.Cells[j].Text.Split(' ');
                string text = addvalue(year, month, lblEmpid.Text, str[0].ToString(), "lbl" + (j - 2), i, j);
                if (text != "X")
                {
                    if (text.ToUpper() == "P" || text.ToUpper() == "LT" || text.ToUpper() == "SL"|| text.ToUpper() == "HD")
                    {
                        GridView1.Rows[i].Cells[j].BackColor = System.Drawing.ColorTranslator.FromHtml("#00c300");
                        GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Black;
                        jj += 1;
                    }
                    else if (text.ToUpper() == "A")
                    {
                        GridView1.Rows[i].Cells[j].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Black;
                        jj1 += 1;
                    }
                    jj2 = jj + jj1;

                }
                else
                {
                    GridView1.Rows[i].Cells[j].BackColor = System.Drawing.Color.Yellow;
                    GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Black;
                }
            }
            Label lblTotalPresent = (Label)GridView1.Rows[i].FindControl("lblTotalPresent");
            lblTotalPresent.Text = jj.ToString();
            Label lblTotalAbsent = (Label)GridView1.Rows[i].FindControl("lblTotalAbsent");
            lblTotalAbsent.Text = jj1.ToString();
            Label lblTotalWorkingDays = (Label)GridView1.Rows[i].FindControl("lblTotalWorkingDays");
            lblTotalWorkingDays.Text = jj2.ToString();
        }
    }


    public string addvalue(string year, string month, string lblEmpId, string attendencedate, string lblday, int i, int j)
    {
        Label lbl = (Label)GridView1.Rows[i].FindControl(lblday);
        sql = "Select ar.AttendanceValue from EmployeeAttendanceDayWise ar";
        sql = sql + " where DATEPART(yyyy,ar.AttendanceDate)='" + year + "' and ar.AttendanceMonth='" + month + "'";
        sql = sql + " and ar.EmpId='" + lblEmpId + "'";
        sql = sql + " and DATEPART(dd,ar.AttendanceDate)='" + attendencedate + "'";
        if (oo.ReturnTag(sql, "AttendanceValue") != "")
        {
            lbl.Text = oo.ReturnTag(sql, "AttendanceValue").Replace(" ", "");
        }
        else
        {
            lbl.Text = "X";
        }
        return lbl.Text;
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "MonthwiseEmployeeAttendenceReport.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel("MonthwiseEmployeeAttendenceReport.xls", GridView1);
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

   
    protected void drpMonth1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        abc.Visible = false;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            table1.Visible = false;
            table2.Visible = false;
            table3.Visible = false;
            table4.Visible = true;
            Grd.DataSource = null;
            Grd.DataBind();
            txtHeaderEmpId.Text = "";
            drpMonth1.SelectedIndex = 0;
        }
        else
        {
            table1.Visible = true;
            table4.Visible = false;
        }
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
        loadgrid();
    }

    public void displayEmpInfo()
    {
        string empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }

        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,eod.DepartmentName,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod.Empid='" + empId + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            table2.Visible = true;
            table3.Visible = true;
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Sorry, No Record found!", lnkShow);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");       

            table2.Visible = false;
            table3.Visible = false;
        }
    }


    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
        loadgrid(); 
    }
}