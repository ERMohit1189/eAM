using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class EmployeeAttendanceMonthWise : System.Web.UI.Page
{
    string sql = "";
    static DataTable dt = new DataTable();
    static DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, DrpDepartment, "EmpDepName", "EmpDepName");
            //DrpDepartment.Items.Insert(0, new ListItem("Select", "Select"));

            BAL.objBal.AddDateMonthYearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
            BAL.objBal.FindCurrentDateandSetinDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);

            sql = "select AbbreviationName from  AttendanceAbbreviationMaster ";
            sql += " where ValidFor='E'";

            dt = BAL.objBal.Fetchdata(sql);

            drpAttendancealldate.DataSource = dt;
            drpAttendancealldate.DataTextField = "AbbreviationName";
            drpAttendancealldate.DataValueField = "AbbreviationName";
            drpAttendancealldate.DataBind();

            divemp4.Visible = false;
        }
    }

    public void loadheader(Repeater rpt)
    {
        //List<SqlParameter> param = new List<SqlParameter>();
        //param.Add(new SqlParameter("@month", (DrpDDEmpMM.SelectedIndex + 1)));
        //param.Add(new SqlParameter("@year", DrpDDEmpYY.SelectedValue.ToString()));

        rpt.DataSource = ds;
        rpt.DataBind();
    }
    public void loadEmp()
    {
        string empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }

        sql = "select Row_Number() over (order by Eo.SrNo Asc) as SNo,Eo.DepartmentName,EO.Ecode,EO.EmpId,EO.RegistrationDate,EG.EMobileNo,EG.EEmail,EG.EFirstName+' '+EG.EMiddleName+' '+EG.ELastName EmpName from EmpployeeOfficialDetails EO ";
        sql += " inner join EmpGeneralDetail EG on EO.EmpId=EG.EmpId";
        sql += " where Eo.Withdrwal is null and eg.BranchCode=" + Session["BranchCode"].ToString() + " and eo.BranchCode=" + Session["BranchCode"].ToString() + "";
        if (!string.IsNullOrEmpty(empId))
        {
            sql += " and eo.empid='" + empId + "'";
            sql += " order by EFirstName asc";
        }
        else
        {
            sql += " and  Eo.DepartmentName='" + DrpDepartment.SelectedItem.ToString() + "'";
            sql += " order by EFirstName asc";
        }
        //else if (DrpDepartment.SelectedIndex != 0)
        //{
        //sql += " and  Eo.DepartmentName='" + DrpDepartment.SelectedItem.ToString() + "'";
        //sql += " order by EFirstName asc";
        //}
        //else
        //{
        //    sql += " order by EFirstName asc";
        //}




        rptEmp.DataSource = BAL.objBal.GridFill(sql);
        rptEmp.DataBind();

        if (rptEmp.Items.Count > 0)
        {
            lnkSubmit.Visible = true;
        }
        else
        {
            lnkSubmit.Visible = false;
        }
    }

    protected void rptEmp_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpt = (Repeater)e.Item.FindControl("rptDays");
            loadheader(rpt);
        }
    }

    protected void rptDays_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList drpAttendancevalue = (DropDownList)e.Item.FindControl("drpAttendancevalue");
            drpAttendancevalue.DataSource = dt;
            drpAttendancevalue.DataTextField = "AbbreviationName";
            drpAttendancevalue.DataValueField = "AbbreviationName";
            drpAttendancevalue.DataBind();

            //BAL.objBal.FillDropDownWithOutSelect(sql, drpAttendancevalue, "AbbreviationName");
        }
    }

    public void BindAttendancevalue()
    {
        divemp4.Visible = true;
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            Label lblempid = (Label)rptEmp.Items[i].FindControl("lblempid");
            Repeater rptDays = (Repeater)rptEmp.Items[i].FindControl("rptDays");


            for (int j = 0; j < rptDays.Items.Count; j++)
            {
                DropDownList drpAttendancevalue = (DropDownList)rptDays.Items[j].FindControl("drpAttendancevalue");
                Label lblMonthDate = (Label)rptDays.Items[j].FindControl("lblMonthDate");
                Label lblIN = (Label)rptDays.Items[j].FindControl("lblIN");
                Label lblOUT = (Label)rptDays.Items[j].FindControl("lblOUT");
                HtmlControl div_inout = (HtmlControl)rptDays.Items[j].FindControl("div_inout");
                Label lbldayname = (Label)rptHeader.Items[j].FindControl("lbldayname");

                sql = "select 1 istrue from EmpployeeOfficialDetails EO  ";
                sql += " where convert(nvarchar,EO.RegistrationDate,106)<=convert(datetime,'" + lblMonthDate.Text.Trim() + "')";
                sql += " and Empid='" + lblempid.Text.Trim() + "' and  eo.BranchCode=" + Session["BranchCode"].ToString() + "";

                string istrue = BAL.objBal.ReturnTag(sql, "istrue");

                if (istrue == "1")
                {
                    //sql = "select AttendanceValue,AttendanceValueIn,AttendanceValueOut from EmployeeAttendanceDayWise where Empid='" + lblempid.Text + "'";
                    //sql += " and convert(nvarchar,AttendanceDate,106)='" + lblMonthDate.Text.Trim() + "'";
                    //sql += " and BranchCode=" + Session["BranchCode"].ToString() + "";

                    sql = "select AttendanceValue, Format(Intime, 'HH:mm') Intime,Format(Outtime, 'HH:mm') Outtime from EmployeeAttendanceDayWise eadw";
                    sql += " inner join EmpployeeOfficialDetails eod on eod.EmpId = eadw.EmpId and eod.BranchCode = " + Session["BranchCode"].ToString() + "";
                    sql += " left join DownloadedPunch dp on dp.BranchCode = eod.BranchCode and dp.EmployeeMachineId = eod.MachineId and convert(date, dp.Intime)= convert(date, AttendanceDate)";
                    sql += " where eadw.Empid = '" + lblempid.Text + "' and convert(date, AttendanceDate)= '" + lblMonthDate.Text.Trim() + "' and eadw.BranchCode = " + Session["BranchCode"].ToString() + "";

                    try
                    {
                        string ss = BAL.objBal.ReturnTag(sql, "AttendanceValue");
                        string txtIN = BAL.objBal.ReturnTag(sql, "Intime");
                        string txtOUT = BAL.objBal.ReturnTag(sql, "Outtime");

                        lblIN.Text = txtIN;
                        lblOUT.Text = txtOUT;
                        if (lblIN.Text != "" || lblOUT.Text != "")
                        {
                            div_inout.Visible = true;
                        }
                        if (ss != string.Empty)
                        {
                            drpAttendancevalue.SelectedValue = ss.Trim();
                        }
                        if (lbldayname.Text == "Sun")
                        {
                            drpAttendancevalue.Items.Insert(0, "--");
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    drpAttendancevalue.Visible = false;
                }

            }
        }
    }

    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@month", (DrpDDEmpMM.SelectedIndex + 1)));
        param.Add(new SqlParameter("@year", DrpDDEmpYY.SelectedValue.ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetMonthAllDays", param);

        hfEmployeeId.Value = string.Empty;
        txtHeaderEmpId.Text = string.Empty;
        loadheader(rptHeader);
        loadEmp();
        BindAttendancevalue();
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@month", (DrpDDEmpMM.SelectedIndex + 1)));
        param.Add(new SqlParameter("@year", DrpDDEmpYY.SelectedValue.ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetMonthAllDays", param);

        loadheader(rptHeader);
        loadEmp();
        BindAttendancevalue();
    }

    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@month", (DrpDDEmpMM.SelectedIndex + 1)));
        param.Add(new SqlParameter("@year", DrpDDEmpYY.SelectedValue.ToString()));

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetMonthAllDays", param);

        loadheader(rptHeader);
        loadEmp();
        BindAttendancevalue();
    }

    protected void rbDepartmentwise_SelectedIndexChanged(object sender, EventArgs e)
    {
        lnkSubmit.Visible = false;
        divemp4.Visible = false;

        rptHeader.DataSource = null;
        rptHeader.DataBind();

        rptEmp.DataSource = null;
        rptEmp.DataBind();

        if (rbDepartmentwise.SelectedIndex == 0)
        {
            divdepartment.Visible = true;
            //divemp1.Visible = false;
            divemp2.Visible = false;
        }
        else
        {
            divdepartment.Visible = false;
            //divemp1.Visible = true;
            divemp2.Visible = true;
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            Label lblempid = (Label)rptEmp.Items[i].FindControl("lblempid");
            Repeater rptDays = (Repeater)rptEmp.Items[i].FindControl("rptDays");

            for (int j = 0; j < rptDays.Items.Count; j++)
            {
                DropDownList drpAttendancevalue = (DropDownList)rptDays.Items[j].FindControl("drpAttendancevalue");
                Label lblMonthDate = (Label)rptDays.Items[j].FindControl("lblMonthDate");
                if (drpAttendancevalue.Visible == true)
                {
                    DailyAttendanceRadio(lblMonthDate.Text.Trim(), lblempid.Text.Trim(), drpAttendancevalue.SelectedItem.Text.Trim());
                }
            }
        }

        Campus camp = new Campus(); camp.msgbox(this.Page, divmsgbox, "Submitted successfully.", "S");
    }

    public void DailyAttendanceRadio(string date, string empid, string attendancevalue)
    {
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@CategoryWise", "Date Wise"));
        param.Add(new SqlParameter("@AttendanceMonth", DrpDDEmpMM.SelectedItem.ToString()));
        param.Add(new SqlParameter("@AttendanceDate", date));
        param.Add(new SqlParameter("@EmpId", empid));
        param.Add(new SqlParameter("@AttendanceValue", attendancevalue));
        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

        DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_EmployeeAttendanceMonthWise", param);
    }

    protected void rptHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList drpAttendanceall = (DropDownList)e.Item.FindControl("drpAttendanceall");
            Label lbldayname = (Label)e.Item.FindControl("lbldayname");
            drpAttendanceall.DataSource = dt;
            drpAttendanceall.DataTextField = "AbbreviationName";
            drpAttendanceall.DataValueField = "AbbreviationName";
            drpAttendanceall.DataBind();
            if (lbldayname.Text == "Sun")
            {
                drpAttendanceall.Items.Insert(0, "--");
            }

            if (rbDepartmentwise.SelectedItem.Text == "Departmentwise")
                drpAttendanceall.Visible = true;
            else
                drpAttendanceall.Visible = false;
            //BAL.objBal.FillDropDownWithOutSelect(sql, drpAttendancevalue, "AbbreviationName");
        }
    }

    protected void drpAttendanceall_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpAttendanceall = sender as DropDownList;
        RepeaterItem item = (RepeaterItem)drpAttendanceall.NamingContainer;
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            Repeater rptDays = (Repeater)rptEmp.Items[i].FindControl("rptDays");
            DropDownList drpAttendancevalue = (DropDownList)rptDays.Items[item.ItemIndex].FindControl("drpAttendancevalue");
            drpAttendancevalue.SelectedValue = drpAttendanceall.SelectedValue;
        }
    }

    protected void drpAttendancealldate_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < rptEmp.Items.Count; i++)
        {
            Repeater rptDays = (Repeater)rptEmp.Items[i].FindControl("rptDays");
            for (int j = 0; j < rptDays.Items.Count; j++)
            {
                DropDownList drpAttendancevalue = (DropDownList)rptDays.Items[j].FindControl("drpAttendancevalue");
                Label lbldayname = (Label)rptHeader.Items[j].FindControl("lbldayname");
                if (lbldayname.Text == "Sun")
                {
                    if (drpAttendancevalue.SelectedIndex != 0)
                    {
                        drpAttendancevalue.SelectedValue = drpAttendancealldate.SelectedValue;
                    }
                }
                else
                {
                    drpAttendancevalue.SelectedValue = drpAttendancealldate.SelectedValue;
                }
            }
        }
    }
}