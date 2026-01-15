using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_EmployeeAttendanceDayWiseUpdate : Page
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
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }


            Panel2.Visible = false;

            sql = "select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
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
        lbmrrr.Text = "";

    }
    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkButton2.Visible = false;
        Grd.Visible = false;
        Panel2.Visible = false;
        lbmrrr.Text = "";
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

            addInaccordian();
        }


    }
    public void DailyWise()
    {
        string dd = "";

        string date = DrpDDEmpDD.SelectedItem.ToString() + "/" + DrpDDEmpMM.SelectedItem.ToString() + "/" + DrpDDEmpYY.SelectedItem.ToString();

        sql = "select Row_Number() over (order by Eo.SrNo Asc) as SNo,Eo.DepartmentName,EO.Ecode,EO.EmpId,EO.RegistrationDate,EG.EMobileNo,EG.EEmail,EG.EFirstName,EG.EMiddleName,EG.ELastName from EmpployeeOfficialDetails EO ";
        sql += " inner join EmpGeneralDetail EG on EO.EmpId=EG.EmpId  where Eo.DepartmentName='" + DrpDepartment.SelectedItem.ToString() + "'";
        sql += " and Eo.Withdrwal is null and eg.BranchCode=" + Session["BranchCode"].ToString() + " and eo.BranchCode=" + Session["BranchCode"].ToString() + " and EO.RegistrationDate<=Convert(datetime,'" + date + "') order by EFirstName asc";

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

                DropDownList Drp = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
                Label lEcode = (Label)Grd.Rows[a].Cells[0].FindControl("Label5");

                sql = "select AbbreviationName from  AttendanceAbbreviationMaster";
                sql += " where ValidFor='E'";
                oo.FillDropDownWithOutSelect(sql, Drp, "AbbreviationName");

                sql = "select AttendanceValue from EmployeeAttendanceDayWise where Ecode='" + lEcode.Text + "'";
                sql += " and convert(nvarchar,AttendanceDate,106)='" + dd + " " + DrpDDEmpMM.SelectedItem.ToString() + " " + DrpDDEmpYY.SelectedItem.ToString() + "'";
                sql += " and CategoryWise='Date Wise'";
                sql += " and BranchCode=" + Session["BranchCode"].ToString() + "";

                try
                {
                    string ss = oo.ReturnTag(sql, "AttendanceValue");
                    Drp.Text = ss.Trim();

                    if (Drp.SelectedItem.Text.ToUpper() == "A")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-red form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-red form-control-blue vd_white";
                    }
                    else if (Drp.SelectedItem.Text.ToUpper() == "L")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-yellow form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (Drp.SelectedItem.Text.ToUpper() == "L")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-yellow form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (Drp.SelectedItem.Text.ToUpper() == "LT" || Drp.SelectedItem.Text.ToUpper() == "LC")
                    {
                        Grd.Rows[a].CssClass = "vd_bg-blue-new form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-blue-new form-control-blue vd_white";
                    }
                    else
                    {
                        Grd.Rows[a].CssClass = "vd_bg-green form-control-blue vd_white";
                        Drp.CssClass = "vd_bg-green form-control-blue vd_white";
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
    public void ReadDailyGrid()
    {
        string Date = "";
        Campus oo = new Campus();



        Date = DrpDDEmpDD.SelectedItem.ToString() + "/" + DrpDDEmpMM.SelectedItem.ToString() + "/" + DrpDDEmpYY.SelectedItem.ToString();
        string dd = "";
        if (DrpDDEmpDD.SelectedItem.ToString().Length == 1)
        {
            dd = "0" + DrpDDEmpDD.SelectedItem.ToString();
        }
        else
        {
            dd = DrpDDEmpDD.SelectedItem.ToString();
        }


        sql = "select ad.Id,ad.CategoryWise ,ad.AttendanceMonth ,ad.AttendanceDate ,ad.DepartmentName,ad.Ecode ,ad.EmpId ,EG.EMobileNo,eg.EEmail,EG.EFirstName,EG.EMiddleName,EG.ELastName  , ";
        sql += "  ad.EmployeeName , ad.AttendanceValue , ad.BranchCode,ad.LoginName,ad.SessionName,ad.RecordDate from EmployeeAttendanceDayWise   ad";
        sql += "  inner join EmpGeneralDetail EG on ad.EmpId=EG.EmpId ";
        sql += "  where ad.DepartmentName='" + DrpDepartment.SelectedValue.ToString() + "'   and convert(nvarchar,ad.AttendanceDate,106)='" + dd + " " + DrpDDEmpMM.SelectedItem.ToString() + " " + DrpDDEmpYY.SelectedItem.ToString() + "'";
        sql += "  and ad.CategoryWise='Date Wise'";
        sql += "  and ad.BranchCode=" + Session["BranchCode"].ToString() + " and eg.BranchCode=" + Session["BranchCode"].ToString() + " order by EFirstName Asc";

        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        Grd.Visible = true;
        Panel2.Visible = true;
        LinkButton2.Visible = true;
        if (Grd.Rows.Count == 0)
        {

            Grd.Visible = false;
            Panel2.Visible = false;
            LinkButton2.Visible = false;

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
                DropDownList Drp = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
                Label lEcode = (Label)Grd.Rows[a].Cells[0].FindControl("Label5");
                sql = "select AbbreviationName from  AttendanceAbbreviationMaster";
                oo.FillDropDownWithOutSelect(sql, Drp, "AbbreviationName");


                sql = "select AttendanceValue from EmployeeAttendanceDayWise where Ecode='" + lEcode.Text + "'";
                sql += "    and DepartmentName='" + DrpDepartment.SelectedValue.ToString() + "'   and convert(nvarchar,AttendanceDate,106)='" + dd + " " + DrpDDEmpMM.SelectedItem.ToString() + " " + DrpDDEmpYY.SelectedItem.ToString() + "'";
                sql += "  and CategoryWise='Date Wise'";
                sql += " and BranchCode=" + Session["BranchCode"].ToString() + "";
                string ss = oo.ReturnTag(sql, "AttendanceValue");
                Drp.Text = ss.Trim();

            }
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

        bool flag = false;
        for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EmployeeAttendanceDayWiseProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            Label lEcode = (Label)Grd.Rows[a].Cells[0].FindControl("Label5");
            Label LEpId = (Label)Grd.Rows[a].Cells[1].FindControl("Label6");
            Label Firstname = (Label)Grd.Rows[a].Cells[2].FindControl("Label3");
            Label MiddleName = (Label)Grd.Rows[a].Cells[2].FindControl("Label4");
            Label Lastname = (Label)Grd.Rows[a].Cells[2].FindControl("Label7");
            DropDownList Drp1 = (DropDownList)Grd.Rows[a].Cells[3].FindControl("DropDownList1");
            Label lblContactNo = (Label)Grd.Rows[a].Cells[2].FindControl("Label8");
            TextBox txtHours = (TextBox)Grd.Rows[a].Cells[5].FindControl("txtHours");
            TextBox txtMinutes = (TextBox)Grd.Rows[a].Cells[5].FindControl("txtMinutes");

            cmd.Parameters.AddWithValue("@CategoryWise", "Date Wise");
            cmd.Parameters.AddWithValue("@AttendanceMonth", DrpDDEmpMM.SelectedItem.ToString());
            String Date = "";
            Date = DrpDDEmpYY.SelectedItem.ToString() + "/" + DrpDDEmpMM.SelectedItem.ToString() + "/" + DrpDDEmpDD.SelectedItem.ToString();
            cmd.Parameters.AddWithValue("@AttendanceDate", Date);

            cmd.Parameters.AddWithValue("@DepartmentName", DrpDepartment.Text.ToString());


            cmd.Parameters.AddWithValue("@Ecode", lEcode.Text.ToString());
            cmd.Parameters.AddWithValue("@EmpId", LEpId.Text.ToString());
            cmd.Parameters.AddWithValue("@EmployeeName", Firstname.Text.ToString() + " " + MiddleName.Text.ToString() + " " + Lastname.Text.ToString());

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

            }
            catch (Exception) { }
        }
        if (flag)
        {
            oo.ClearControls(this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        DailyAttendanceRadio();
    }
    public void PermissionGrant(int add1, LinkButton Ladd)
    {
        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }
    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql += " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));


        PermissionGrant(a, (LinkButton)LinkButton2);
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow currentrow = (GridViewRow)(((Control)(sender)).Parent).Parent;
        TextBox txtHours = (TextBox)currentrow.FindControl("txtHours");
        TextBox txtMinutes = (TextBox)currentrow.FindControl("txtMinutes");
        DropDownList DropDownList1 = (DropDownList)currentrow.FindControl("DropDownList1");
        if (DropDownList1.SelectedItem.Text.ToUpper() == "LC")
        {
            txtHours.Enabled = true;
            txtMinutes.Enabled = true;
            txtHours.CssClass = "vd_bg-blue form-control-blue vd_white text-center";
            txtMinutes.CssClass = "vd_bg-blue form-control-blue vd_white text-center";
            txtHours.Focus();
        }
        else
        {
            txtHours.Enabled = false;
            txtMinutes.Enabled = false;
        }
        countpresent();
        addInaccordian();
        if (DropDownList1.SelectedItem.Text.ToUpper() == "A")
        {
            currentrow.CssClass = "vd_bg-red form-control-blue vd_white";
            DropDownList1.CssClass = "vd_bg-red form-control-blue vd_white";
        }
        else if (DropDownList1.SelectedItem.Text.ToUpper() == "L")
        {
            currentrow.CssClass = "vd_bg-yellow form-control-blue vd_white";
            DropDownList1.CssClass = "vd_bg-yellow form-control-blue vd_white";
        }
        else if (DropDownList1.SelectedItem.Text.ToUpper() == "L")
        {
            currentrow.CssClass = "vd_bg-yellow form-control-blue vd_white";
            DropDownList1.CssClass = "vd_bg-yellow form-control-blue vd_white";
        }
        else if (DropDownList1.SelectedItem.Text.ToUpper() == "LT" || DropDownList1.SelectedItem.Text.ToUpper() == "LC")
        {
            currentrow.CssClass = "vd_bg-blue-new form-control-blue vd_white";
            DropDownList1.CssClass = "vd_bg-blue-new form-control-blue vd_white";
        }
        else
        {
            currentrow.CssClass = "vd_bg-green form-control-blue vd_white";
            DropDownList1.CssClass = "vd_bg-green form-control-blue vd_white";
        }
    }
    public void addInaccordian()
    {
        GridView gv = new GridView();
        dt = new DataTable();
        dt.Columns.Add("#");
        dt.Columns.Add("Ecode");
        dt.Columns.Add("EmpId");
        dt.Columns.Add("EmployeeName");
        dt.Columns.Add("EMobileNo");
        dt.Columns.Add("Attendance");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblEcode = (Label)Grd.Rows[i].FindControl("Label5");
            Label lblEmpId = (Label)Grd.Rows[i].FindControl("Label6");
            Label lblEFName = (Label)Grd.Rows[i].FindControl("Label3");
            Label lblEMName = (Label)Grd.Rows[i].FindControl("Label4");
            Label lblELName = (Label)Grd.Rows[i].FindControl("Label7");
            Label lblEMobileNo = (Label)Grd.Rows[i].FindControl("Label8");
            DropDownList drpAttendance = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
            dt.Rows.Add((i + 1).ToString(), lblEcode.Text, lblEmpId.Text, lblEFName.Text + lblEMName.Text + lblELName.Text, lblEMobileNo.Text, drpAttendance.SelectedItem.Text);
        }

        gv = new GridView();
        gv.CssClass = "table gw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
        DataView dv = new DataView(dt, "Attendance='P'", "EmployeeName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane1.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table rw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
        dv = new DataView(dt, "Attendance='A'", "EmployeeName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane2.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table yw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
        dv = new DataView(dt, "Attendance='L'", "EmployeeName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane3.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table bw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
        dv = new DataView(dt, "Attendance='LT'", "EmployeeName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane4.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table bw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
        dv = new DataView(dt, "Attendance='LC'", "EmployeeName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane5.ContentContainer.Controls.Add(gv);

        Accordion1.SelectedIndex = -1;
    }
    public void countpresent()
    {
        if (Grd.Rows.Count > 0)
        {
            int p = 0, a = 0, l = 0, lc = 0, lt = 0, swl = 0;
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
                if (DropDownList1.SelectedItem.Text.ToUpper() == "L")
                {
                    l += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "LT")
                {
                    lt += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "LC")
                {
                    lc += 1;
                }
                if (DropDownList1.SelectedItem.Text.ToUpper() == "SWL")
                {
                    swl += 1;
                }
                lblPresent.Text = p.ToString();
                lblAbsent.Text = a.ToString();
                lblLate.Text = lt.ToString();
                lblLatecomers.Text = lc.ToString();
                lblLeave.Text = l.ToString();
                lblSWL.Text = swl.ToString();
            }
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