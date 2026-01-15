using c4SmsNew;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_EmployeeAttendanceDayWise : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt;
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
            Panel2.Visible = false;
            sql = "select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown(sql, DrpDepartment, "EmpDepName");
            oo.AddDateMonthYearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
            FindCurrentDateandSetinDropDown();
            LinkButton2.Visible = false;
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
    protected void DrpDDEmpDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkButton2.Visible = false;
        Grd.Visible = false;
        Panel2.Visible = false;

    }
    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkButton2.Visible = false;
        Grd.Visible = false;
        Panel2.Visible = false;
    }
    protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        lblDD.Text = DrpDDEmpDD.SelectedItem.Text;
        lblMM.Text = DrpDDEmpMM.SelectedItem.Text;
        lblYYYY.Text = DrpDDEmpYY.SelectedItem.Text;
        attDate.Visible = true;
        string dd = "";
        if (DrpDepartment.SelectedItem.Text == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select :<--Select-->:", "A");
            LinkButton2.Visible = false;
            Grd.Visible = false;
            Panel2.Visible = false;
        }
        else
        {

            DailyWise();
            if (DrpDDEmpDD.SelectedItem.ToString().Length == 1)
            {
                dd = "0" + DrpDDEmpDD.SelectedItem.ToString();
            }
            else
            {
                dd = DrpDDEmpDD.SelectedItem.ToString();
            }
            countpresent();
            if (Grd.Rows.Count > 0)
            {
                lblTotalstudent.Text = Grd.Rows.Count.ToString();
            }

        }


    }
    public void DailyWise()
    {
        string dd = "";
        string date = DrpDDEmpDD.SelectedItem.ToString() + "/" + DrpDDEmpMM.SelectedItem.ToString() + "/" + DrpDDEmpYY.SelectedItem.ToString();
        sql = "select Eo.DepartmentName,EO.Ecode,EO.EmpId,EO.RegistrationDate, EMobileNo,Email EEmail, Eo.Name,eo.BranchCode from GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") EO ";
        sql += " where Eo.DepartmentName='" + DrpDepartment.SelectedItem.ToString() + "' and Eo.Withdrwal is null and eo.BranchCode=" + Session["BranchCode"].ToString() + " and EO.RegistrationDate<=Convert(datetime,'" + date + "') order by Name asc";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        LinkButton2.Visible = true;
        Grd.Visible = true;
        Panel2.Visible = true;
        if (Grd.Rows.Count == 0)
        {
            LinkButton2.Visible = false;
            Grd.Visible = false;
            Panel2.Visible = false;
        }
        else
        {
            for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
            {
                if (DrpDDEmpDD.SelectedItem.ToString().Length == 1)
                {
                    dd = "0" + DrpDDEmpDD.SelectedItem.ToString();
                }
                else
                {
                    dd = DrpDDEmpDD.SelectedItem.ToString();
                }

                DropDownList Drp = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");
                Label lEcode = (Label)Grd.Rows[a].FindControl("Label5");
                Label lblType = (Label)Grd.Rows[a].FindControl("lblType");

                sql = "select AttendanceValue, CategoryWise from EmployeeAttendanceDayWise where Ecode='" + lEcode.Text + "'";
                sql += " and convert(nvarchar,AttendanceDate,106)='" + dd + " " + DrpDDEmpMM.SelectedItem.ToString() + " " + DrpDDEmpYY.SelectedItem.ToString() + "'";
                sql += " and BranchCode=" + Session["BranchCode"].ToString() + "";

                try
                {
                    Drp.SelectedValue = (oo.ReturnTag(sql, "AttendanceValue").Trim() == "" ? "<--Select-->" : oo.ReturnTag(sql, "AttendanceValue").Trim());
                    string CategoryWise = oo.ReturnTag(sql, "CategoryWise");
                    if (CategoryWise == "Date Wise")
                    {
                        lblType.Text = "Automatic";
                    }
                    if (CategoryWise == "Date Wise Manual")
                    {
                        lblType.Text = "Manual";
                    }
                }
                catch
                {

                }


            }
        }
        if (Grd.Rows.Count > 0)
        {
            LinkButton2.Visible = true;
        }


    }
    public void DailyAttendanceRadio()
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

        int chkSts = 0;
        int drpSts = 0;
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
            DropDownList DropDownList1 = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
            if (chk.Checked)
            {
                chkSts = chkSts + 1;
            }
            if (chk.Checked && DropDownList1.SelectedValue == "<--Select-->")
            {
                drpSts = drpSts + 1;
            }
        }
        if (chkSts == 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please check atleast one!", "A");
        }
        else if (drpSts > 0)
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please select one Attendance if ckecked!", "A");

        }
        else
        {
            bool flag = false;
            for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
            {
                CheckBox chk = (CheckBox)Grd.Rows[a].FindControl("chk");
                if (chk.Checked)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "EmployeeAttendanceDayWiseProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    Label lEcode = (Label)Grd.Rows[a].FindControl("Label5");
                    Label LEpId = (Label)Grd.Rows[a].FindControl("Label6");
                    Label Name = (Label)Grd.Rows[a].FindControl("Label3");
                    DropDownList Drp1 = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");
                    Label lblContactNo = (Label)Grd.Rows[a].FindControl("Label8");

                    cmd.Parameters.AddWithValue("@CategoryWise", "Date Wise Manual");
                    cmd.Parameters.AddWithValue("@AttendanceMonth", DrpDDEmpMM.SelectedItem.ToString());
                    String Date = "";
                    Date = DrpDDEmpYY.SelectedItem.ToString() + "/" + DrpDDEmpMM.SelectedItem.ToString() + "/" + DrpDDEmpDD.SelectedItem.ToString();
                    cmd.Parameters.AddWithValue("@AttendanceDate", Date);

                    cmd.Parameters.AddWithValue("@DepartmentName", DrpDepartment.Text.ToString());


                    cmd.Parameters.AddWithValue("@Ecode", lEcode.Text.ToString());
                    cmd.Parameters.AddWithValue("@EmpId", LEpId.Text.ToString());
                    cmd.Parameters.AddWithValue("@EmployeeName", Name.Text.ToString());

                    cmd.Parameters.AddWithValue("@AttendanceValue", Drp1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    try
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        flag = true;
                        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                        if (oo.ReturnTag(sql, "HitValue") != "")
                        {
                            if (oo.ReturnTag(sql, "HitValue") == "true")
                            {
                                sql = "Select SmsSent From SmsEmailMaster Where Id='25' and BranchCode=" + Session["BranchCode"] + "";
                                if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                                {
                                    if (Drp1.SelectedValue.ToUpper() == "A")
                                    {
                                        if (lblContactNo.Text != "")
                                        {
                                            SendFeesSms(lblContactNo.Text, LEpId.Text);
                                        }
                                    }
                                    if (Drp1.SelectedValue.ToUpper() == "LT")
                                    {
                                        if (lblContactNo.Text != "")
                                        {
                                            SendforLate(lblContactNo.Text, LEpId.Text);
                                        }
                                    }
                                }
                            }
                        }

                    }

                    catch (Exception) { }
                }
            }
            if (flag)
            {
                DailyWise();
                oo.ClearControls(this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

            }
        }
    }
    public void SendFeesSms(string FmobileNo, string EmpId)
    {

        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";

        sql = "Select (EFirstName+' '+EMiddleName+' '+ELastName) as EmpName from EmpGeneralDetail";
        sql += "  where  EmpId='" + EmpId + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        mess = "Dear " + oo.ReturnTag(sql, "EmpName") + ", you are absent today.";
        string sms_response = "";

        if (FmobileNo != "")
        {
            sms_response = sadpNew.Send(mess, FmobileNo, "");
        }
    }
    public void SendforLate(string FmobileNo, string EmpId)
    {


        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        string collegeTitle = "";


        sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");

        sql = "Select (EFirstName+' '+EMiddleName+' '+ELastName) as EmpName from EmpGeneralDetail";
        sql += "  where  EmpId='" + EmpId + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        mess = "Dear " + oo.ReturnTag(sql, "EmpName") + ", you are late today.";
        mess = mess + Environment.NewLine;
        mess = mess + "Regards";
        mess = mess + Environment.NewLine;
        mess = mess + "Principal";
        mess = mess + Environment.NewLine;
        mess = mess + collegeTitle;

        string sms_response = "";


        if (FmobileNo != "")
        {


            sms_response = sadpNew.Send(mess, FmobileNo, "");

        }

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        DailyAttendanceRadio();
    }



    public void countpresent()
    {
        if (Grd.Rows.Count > 0)
        {
            int p = 0, a = 0, hd = 0, sl = 0, lt = 0, swl = 0;
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                DropDownList DropDownList1 = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
                if (DropDownList1.SelectedItem.Text.ToUpper() == "P")
                {
                    p += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "A")
                {
                    a += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "HD")
                {
                    hd += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "SL")
                {
                    sl += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "LT")
                {
                    lt += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "SWL")
                {
                    swl += 1;
                }

                lblPresent.Text = p.ToString();
                lblAbsent.Text = a.ToString();
                lblHD.Text = hd.ToString();
                lblSL.Text = sl.ToString();
                lblLate.Text = lt.ToString();
                lblSWL.Text = swl.ToString();

            }
        }
    }

    protected void chkHead_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)(((Control)(sender)).Parent).Parent;
        CheckBox chkHead = (CheckBox)Grd.HeaderRow.FindControl("chkHead");

        if (chkHead.Checked)
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
                chk.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
                chk.Checked = false;
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)(((Control)(sender)).Parent).Parent;
        CheckBox chkHead = (CheckBox)Grd.HeaderRow.FindControl("chkHead");
        int sts = 0;
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)Grd.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                sts = sts + 1;
            }
        }
        chkHead.Checked = false;
        if (sts == Grd.Rows.Count)
        {
            chkHead.Checked = true;
        }
    }
    protected void ddlAbbrAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAbbrAll = (DropDownList)sender;
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            DropDownList DropDownList1 = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
            DropDownList1.SelectedValue = ddlAbbrAll.SelectedValue;
        }
    }
}
