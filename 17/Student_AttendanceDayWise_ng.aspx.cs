using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Net.Mail;
using System.Collections.Generic;

public partial class comman_Student_AttendanceDayWise_ng : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt;
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }

            loadClass(DrpAtteClass);
            oo.fillSelectvalue(drpBranch, "<--Select-->");
            oo.fillSelectvalue(DrpAttenSection, "<--Select-->");
            oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
            oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);
            IsAttendanceDateBeetweenSessionDate();
            Panel2.Visible = false;

        }
    }

    protected void IsAttendanceDateBeetweenSessionDate()
    {
        string date = DrpSaal.SelectedItem.Text +" "+ DrpMahina.SelectedItem.Text +" "+ DrpDin.SelectedItem.Text;
        sql = "Select '1' flag from SessionMaster where SessionName='"+Session["SessionName"].ToString()+ "' and BranchCode=" + Session["BranchCode"].ToString() + " and FromDate<='" + date + "' and ToDate>='"+ date + "'";
        string value = BAL.objBal.ReturnTag(sql, "flag");
        if(value=="1")
        {
            btnShow.Visible = true;
            
        }
        else
        {
            btnShow.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Selected date must between session date!", "A");
        }
    }
    private void loadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"].ToString() + " order by cidorder asc ";
            dt = DAL.DALInstance.GetValueInTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                drpclass.DataSource = dt;
                drpclass.DataTextField = "ClassName";
                drpclass.DataValueField = "Id";
                drpclass.DataBind();
                drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
        else
        {
            sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1 ";
            sql +=  "inner join ClassMaster cm on cm.Id = T1.ClassId and cm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"] + "' ";
            sql +=  " and cm.BranchCode=" + Session["BranchCode"].ToString() + " and cm.SessionName = T1.SessionName and cm.SessionName = '" + Session["SessionName"] + "' and T1.BranchCode=" + Session["BranchCode"].ToString() + " and EmpCode = '" + Session["LoginName"].ToString() + "' ";
            sql +=  "order by cidorder asc  ";
            dt = DAL.DALInstance.GetValueInTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                drpclass.DataSource = dt;
                drpclass.DataTextField = "ClassName";
                drpclass.DataValueField = "Id";
                drpclass.DataBind();
                drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
    }

    private void loadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            sql +=  " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and t1.BranchCode=" + Session["BranchCode"].ToString() + " and t1.Classid=" + drpclass.SelectedValue.ToString() + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, "<--Select-->");
        }
    }

    private void loadBranch(DropDownList drpbranch, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql +=  "  and bm.BranchCode=" + Session["BranchCode"].ToString() + "  and t1.BranchCode=" + Session["BranchCode"].ToString() + " T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpbranch, "BranchName", "Id");
            drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch(drpBranch, DrpAtteClass);

        loadSection(DrpAttenSection, DrpAtteClass);
         
        Panel2.Visible = false;
        lblmess.Text = "";
      
    }

    protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
        IsAttendanceDateBeetweenSessionDate();

    }

    protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
    {
       oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
        IsAttendanceDateBeetweenSessionDate();

    }

    protected void DrpDin_SelectedIndexChanged(object sender, EventArgs e)
    {     
        Panel2.Visible = false;             
        lblmess.Text = "";
        IsAttendanceDateBeetweenSessionDate();
    }

    protected void DrpAttenSection_SelectedIndexChanged(object sender, EventArgs e)
    {             
        lblmess.Text = "";
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (DrpAtteClass.SelectedItem.Text == "<--Select-->" || DrpAttenSection.SelectedItem.Text == "<--Select-->")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select :<--Select-->:", "A");
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@AttendanceDate", DrpSaal.SelectedItem.Text.ToString() + "/" + DrpMahina.SelectedItem.Text.ToString() + "/" + DrpDin.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@ClassName", DrpAtteClass.SelectedItem.ToString()));
            param.Add(new SqlParameter("@SectionName", DrpAttenSection.SelectedItem.ToString()));
            param.Add(new SqlParameter("@BranchName", drpBranch.SelectedItem.ToString()));
            param.Add(new SqlParameter("@DisplayOrder", RadioButtonList2.SelectedValue.ToString()));

            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GET_AttendanceDetailsDateWiseProc", param);
            if (ds.Tables.Count > 0)
            {
                Grd.DataSource = ds;
                Grd.DataBind();
            }

            if (Grd.Rows.Count > 0)
            {
                Panel2.Visible = true;
                DropDownList drpAttendance = (DropDownList)Grd.HeaderRow.FindControl("drpAttendance");
                sql = "select AbbreviationName from  AttendanceAbbreviationMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
                oo.FillDropDownWithOutSelect(sql, drpAttendance, "AbbreviationName");
                drpAttendance.Items.Insert(0, "Attendance");
                for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
                {
                    DropDownList Drp = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");

                    sql = "select AbbreviationName from  AttendanceAbbreviationMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.FillDropDownWithOutSelect(sql, Drp, "AbbreviationName");

                    Label lEnroll = (Label)Grd.Rows[a].FindControl("Label1");

                    sql = "select AttendanceValue from AttendanceDetailsDateWise where Srno='" + lEnroll.Text + "'";
                    sql +=  " and ClassName='" + DrpAtteClass.SelectedItem.ToString() + "' and SectionName='" + DrpAttenSection.SelectedItem.ToString() + "'";
                    sql +=  " and AttendanceDate='" + DrpSaal.SelectedItem.Text.ToString() + "/" + DrpMahina.SelectedItem.Text.ToString() + "/" + DrpDin.SelectedItem.Text.ToString() + "'";
                    sql +=  " and CategoryWise='Date Wise'";
                    sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    string ss = oo.ReturnTag(sql, "AttendanceValue");
                    Drp.SelectedValue = ss.Trim();
                    if (ss != "")
                    {
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
                            Grd.Rows[a].CssClass = "vd_bg-blue form-control-blue vd_white";
                            Drp.CssClass = "vd_bg-blue form-control-blue vd_white";
                        }
                        else
                        {
                            Grd.Rows[a].CssClass = "vd_bg-green form-control-blue vd_white";
                            Drp.CssClass = "vd_bg-green form-control-blue vd_white";
                        }
                    }
                }
            }
            countpresent();
            addInaccordian();            
        }
        
        if (Grd.Rows.Count > 0)
        {
            lblTotalstudent.Text = Grd.Rows.Count.ToString();
        }
    }
   
    public void DailyAttendanceRadio()
    {
        string dd = "";

        if (DrpDin.SelectedItem.ToString().Length == 1)
        {
            dd = "0" + DrpDin.SelectedItem.ToString();
        }
        else
        {
            dd = DrpDin.SelectedItem.ToString();
        }
            bool flag = false;
            for (int a = 0; a < Grd.Rows.Count; a++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AttendanceDetailsDateWiseProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                Label Lsrno = (Label)Grd.Rows[a].FindControl("Label1");
                Label lblsms = (Label)Grd.Rows[a].FindControl("lblsms");
                Label lEnroll = (Label)Grd.Rows[a].FindControl("Label2");                
                Label Firstname = (Label)Grd.Rows[a].FindControl("Label3");
                DropDownList Drp1 = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");
                TextBox txtHours = (TextBox)Grd.Rows[a].Cells[5].FindControl("txtHours");
                TextBox txtMinutes = (TextBox)Grd.Rows[a].Cells[5].FindControl("txtMinutes");

                cmd.Parameters.AddWithValue("@CategoryWise", "Date Wise");
                cmd.Parameters.AddWithValue("@AttendanceMonth", DrpMahina.SelectedItem.ToString());
                String Date = "";
                Date = DrpSaal.SelectedItem.ToString() + "/" + DrpMahina.SelectedItem.ToString() + "/" + DrpDin.SelectedItem.ToString();
                cmd.Parameters.AddWithValue("@AttendanceDate", Date);

                cmd.Parameters.AddWithValue("@ClassName", DrpAtteClass.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SectionName", DrpAttenSection.SelectedItem.ToString());

                cmd.Parameters.AddWithValue("@StEnRCode", lEnroll.Text.ToString());
                cmd.Parameters.AddWithValue("@SrNo", Lsrno.Text.ToString());
                cmd.Parameters.AddWithValue("@StudentName", Firstname.Text.ToString());

                cmd.Parameters.AddWithValue("@AttendanceValue", Drp1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    flag = true;
                    con.Close();
                    if (lblsms.Text != "Y")
                    {
                        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                        if (oo.ReturnTag(sql, "HitValue") != "")
                        {
                            if (oo.ReturnTag(sql, "HitValue") == "true")
                            {
                                sql = "Select SmsSent From SmsEmailMaster Where Id='6'";
                                if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                                {
                                    string Conta = "";
                                    sql = "select FamilyContactNo from StudentFamilyDetails  where Srno='" + Lsrno.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "  and SessionName='" + Session["SessionName"].ToString() + "'";
                                    Conta = oo.ReturnTag(sql, "FamilyContactNo");

                                    //sql = "Select Convert(Nvarchar(11),Getdate(),106) Date";
                                    string date = Date;

                                    if (Drp1.SelectedValue.ToUpper() == "A")
                                    {
                                        SendFeesSms(Conta, Lsrno.Text, date);
                                    }
                                    if (Drp1.SelectedValue.ToUpper() == "LC")
                                    {
                                        if (Conta != "")
                                        {
                                            bool send = false;
                                            if (txtHours.Text != "" && txtMinutes.Text != "")
                                            {
                                                send = true;
                                            }
                                            else if (txtHours.Text == "" && txtMinutes.Text != "")
                                            {
                                                send = true;
                                            }
                                            else if (txtHours.Text != "" && txtMinutes.Text == "")
                                            {
                                                send = true;
                                            }
                                            else
                                            {
                                                send = false;
                                            }
                                            if (txtHours.Text == "0" && txtMinutes.Text == "0")
                                            {
                                                send = false;
                                            }
                                            if (txtHours.Text == "00" && txtMinutes.Text == "00")
                                            {
                                                send = false;
                                            }
                                            if (txtHours.Text == "0" && txtMinutes.Text == "")
                                            {
                                                send = false;
                                            }
                                            if (txtHours.Text == "" && txtMinutes.Text == "0")
                                            {
                                                send = false;
                                            }
                                            if (txtHours.Text == "00" && txtMinutes.Text == "")
                                            {
                                                send = false;
                                            }
                                            if (txtHours.Text == "" && txtMinutes.Text == "00")
                                            {
                                                send = false;
                                            }
                                            if (send == true)
                                            {
                                                string hours;
                                                string minutes;
                                                string coloun;
                                                coloun = ":";
                                                if (txtHours.Text.Length == 0)
                                                {
                                                    hours = "00";
                                                }
                                                else if (txtHours.Text.Length == 1)
                                                {
                                                    hours = "0" + txtHours.Text;
                                                }
                                                else
                                                {
                                                    hours = txtHours.Text;
                                                }
                                                if (txtMinutes.Text.Length == 0)
                                                {
                                                    minutes = "00";
                                                }
                                                else if (txtMinutes.Text.Length == 1)
                                                {
                                                    minutes = "0" + txtMinutes.Text;
                                                }
                                                else
                                                {
                                                    minutes = txtMinutes.Text;
                                                }
                                                SendLatecommerssms(Conta, Lsrno.Text, hours, coloun, minutes, date);
                                            }

                                        }
                                    }
                                    if (Drp1.SelectedValue.ToUpper() == "LT")
                                    {
                                        SendSms(Conta, Lsrno.Text, date);
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    flag = false;
                }
            }
            if (flag)
            {
                //oo.MessageBoxforUpdatePanel("Submitted successfully.", btnSubmit);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");

                oo.ClearControls(this.Page);
            }
        
    }

    public void SendFeesSms(string FmobileNo, string Srno, string date)
    {

        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";

        sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "    and  Srno='" + Srno + "'";

        mess = "Dear Guardian, " + oo.ReturnTag(sql, "StudentName") + "(S.R. No.-" + Srno + ") is absent on " + date + ".";
        string sms_response = "";

        if (FmobileNo != "")
        {
            sms_response = sadpNew.Send(mess, FmobileNo, "");
        }
    }

    public void SendLatecommerssms(string FmobileNo, string Srno, string hours, string coloun, string minuts, string date)
    {
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        string collegeTitle = "";

        sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");

        sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "    and  Srno='" + Srno + "'";

        mess = "Dear Guardian, your ward " + oo.ReturnTag(sql, "StudentName") + "(S.R. No.-" + Srno + ") is " + hours + "" + coloun + "" + minuts + " hours late on " + date + ".";
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

    public void SendSms(string FmobileNo, string Srno, string date)
    {
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        
        sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "    and  Srno='" + Srno + "'";

        mess = "Dear Guardian, your ward  " + oo.ReturnTag(sql, "StudentName") + "(S.R. No.-" + Srno + ") is Late on " + date + ".";
        string sms_response = "";
       
        if (FmobileNo != "")
        {          
           sms_response = sadpNew.Send(mess, FmobileNo, "");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
        sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));
        PermissionGrant(a, (LinkButton)btnSubmit);
    }  

    public void SendNotificationEmail(string Srno)
    {
        string Mess = "";
        string ss = "";
        string collegeTitle = "";


        sql = "Select Attendance  from  AttendanceEmailActivateDeactivate where BranchCode=" + Session["BranchCode"].ToString() + "";

        ss = oo.ReturnTag(sql, "Attendance");

        if (ss == "true")
        {
            sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");
            string logopath = "";
            sql = "select  CologeLogoPath from  CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            logopath = oo.ReturnTag(sql, "CologeLogoPath");
            int l = 0;
            l = Convert.ToInt32(logopath.Length.ToString());

            Mess = "  <table > ";
            Mess = Mess + "<tr>";
            Mess = Mess + "<td colspan='2'>" + "<img src='www.eam.co.in/" + logopath.Substring(1, l - 1) + "'  height='88' width='80' >";
            Mess = Mess + "</tr>";

            Mess = Mess + " <tr>";
            Mess = Mess + "  <td>";
            Mess = Mess + "  &nbsp;</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "    &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
            Mess = Mess + "   <hr/></td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td>";
            Mess = Mess + " This message was sent from a notification-only E-mail address.</td>";
            Mess = Mess + " <td>";
            Mess = Mess + " &nbsp;</td>";
            Mess = Mess + "</tr>";
            Mess = Mess + "<tr>";
            Mess = Mess + "   <td>";
            Mess = Mess + " Please do not reply to this message.</td>";
            Mess = Mess + "  <td>";
            Mess = Mess + "     &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
            Mess = Mess + "  <hr/></td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
            Mess = Mess + "   Dear Sir/Madam,</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "  <td colspan='2'>";
            Mess = Mess + "  Greetings from " + collegeTitle + "!</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
            sql = "Select FirstName+' '+MiddleName+' '+LastName as StudentName   from StudentGenaralDetail";
            sql +=  "    where  srno=" + "'" + Srno + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            Mess = Mess + "      Your ward " + oo.ReturnTag(sql, "StudentName") + " is absent today. ";

            Mess = Mess + " </td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
          
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " </tr>";
            //Mess = Mess + " <tr>";
            //Mess = Mess + "  <td colspan='2'>";
            //Mess = Mess + "    Please find attaced file of Fee Receipt.</td>";
            //Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "  <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "  <td colspan='2'>";

            string femail = "", FamilyContactNo = "";
            sql = "Select FamilyEmail,FamilyContactNo from StudentFamilyDetails where srno='" + Srno + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            femail = oo.ReturnTag(sql, "FamilyEmail");
            FamilyContactNo = oo.ReturnTag(sql, "FamilyContactNo");

            Mess = Mess + "     Note: In our record your E-mail is " + femail + " and Contact No. is " + FamilyContactNo + "</td>";

            Mess = Mess + "  </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td colspan='2'>";
            Mess = Mess + " <hr/></td>";
            Mess = Mess + "   </tr>";
            Mess = Mess + "  <tr>";
            Mess = Mess + "     <td colspan='2'>";
            Mess = Mess + "        Warm Regards,&nbsp;";
            Mess = Mess + "   </td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "   <td>";
            string schName = "";
            sql = "Select CollegeName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            schName = oo.ReturnTag(sql, "CollegeName");

            Mess = Mess + "  " + schName + "</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "    &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "   <td>";

            string CityId = "";
            sql = "Select CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            CityId = oo.ReturnTag(sql, "CityId");


            Mess = Mess + "   " + CityId + "</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "   &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + "    <td>";

            string email = "";
            sql = "Select Email from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";

            email = oo.ReturnTag(sql, "Email");
            Mess = Mess + "   " + email + "</td>";
            Mess = Mess + "  <td>";
            Mess = Mess + "    &nbsp;</td>";
            Mess = Mess + " </tr>";
            Mess = Mess + " <tr>";
            Mess = Mess + " <td>";
            Mess = Mess + "  &nbsp;</td>";
            Mess = Mess + " <td>";
            Mess = Mess + "  &nbsp;</td>";
            Mess = Mess + "  </tr>";
            Mess = Mess + " </table>";

            sql = "select FamilyEmail  from StudentFamilyDetails where srno='" + Srno + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

         
            EmailSending(Mess, "" + collegeTitle + " : Attendance Acknowledgement", oo.ReturnTag(sql, "FamilyEmail"));
        }


    }

    public bool EmailSending(string Mess, string subjectParameter, string TOEmailID)
    {
#pragma warning disable 219
        string ss = "";
#pragma warning restore 219
        string kk = "";
        sql = "Select Email,Password from EmailPanelSetting where  BranchCode=" + Session["BranchCode"].ToString() + "";
        kk = oo.ReturnTag(sql, "Email");
        string pp = oo.ReturnTag(sql, "Password");


        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add(TOEmailID);//to ID

        mail.From = new MailAddress(kk);
        mail.Subject = subjectParameter;

        mail.Body = Mess;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential(kk, pp);//from id
        //Or your Smtp Email ID and Password
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);
            send = true;
        }
        catch (Exception) { }
        return send;
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
            currentrow.CssClass = "vd_bg-blue form-control-blue vd_white";
            DropDownList1.CssClass = "vd_bg-blue form-control-blue vd_white";
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
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StEnRCode");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("FatherName");
        dt.Columns.Add("ContactNo");
        dt.Columns.Add("Attendance");
        for (int i = 0; i < Grd.Rows.Count; i++)
        {
            Label lblSrno = (Label)Grd.Rows[i].FindControl("Label1");
            Label lblStenrCode = (Label)Grd.Rows[i].FindControl("Label2");
            Label lblFName = (Label)Grd.Rows[i].FindControl("Label3");
            Label lblMName = (Label)Grd.Rows[i].FindControl("Label6");
            Label lblLName = (Label)Grd.Rows[i].FindControl("Label9");
            Label lblFAName = (Label)Grd.Rows[i].FindControl("Label5");
            Label lblContact = (Label)Grd.Rows[i].FindControl("Label11");
            DropDownList drpAttendance = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
            dt.Rows.Add((i+1).ToString(),lblSrno.Text, lblStenrCode.Text, lblFName.Text, lblFAName.Text, lblContact.Text, drpAttendance.SelectedItem.Text);
        }
        
       
        gv = new GridView();
        gv.CssClass = "table gw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
        DataView dv = new DataView(dt, "Attendance='P'", "StudentName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
     
        AccordionPane1.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
        AccordionPane1.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table rw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";      
        dv = new DataView(dt, "Attendance='A'", "StudentName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane2.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
        AccordionPane2.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table yw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered"; 
        dv = new DataView(dt, "Attendance='L'", "StudentName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane3.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
        AccordionPane3.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table bw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered"; 
        dv = new DataView(dt, "Attendance='LT'", "StudentName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane4.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
        AccordionPane4.ContentContainer.Controls.Add(gv);
        //------------------------------------------------------------------------------------        
        gv = new GridView();
        gv.CssClass = "table bw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered"; 
        dv = new DataView(dt, "Attendance='LC'", "StudentName", DataViewRowState.CurrentRows);
        gv.DataSource = dv;
        gv.DataBind();
        AccordionPane5.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
        AccordionPane5.ContentContainer.Controls.Add(gv);

        Accordion1.SelectedIndex = -1;
    }

    public void countpresent()
    {
        if (Grd.Rows.Count > 0)
        {
            int p=0,a=0,l=0,lc=0,lt=0;
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
                lblPresent.Text = p.ToString();
                lblAbsent.Text = a.ToString();
                lblLate.Text = lt.ToString();
                lblLatecomers.Text = lc.ToString();
                lblLeave.Text = l.ToString();
            }
        }
    }
    protected void drpAttendance_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpAttendance = (DropDownList)sender;
        if (drpAttendance.SelectedIndex != 0)
        {
            foreach (GridViewRow gvr in Grd.Rows)
            {
                DropDownList drpAtt = (DropDownList)gvr.FindControl("DropDownList1");
                drpAtt.SelectedIndex = drpAttendance.SelectedIndex - 1;

                if (drpAtt.SelectedItem.Text.ToUpper() == "A")
                {
                    gvr.CssClass = "vd_bg-red form-control-blue vd_white";
                    drpAtt.CssClass = "vd_bg-red form-control-blue vd_white";
                }
                else if (drpAtt.SelectedItem.Text.ToUpper() == "L")
                {
                    gvr.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    drpAtt.CssClass = "vd_bg-yellow form-control-blue vd_white";
                }
                else if (drpAtt.SelectedItem.Text.ToUpper() == "LT" || drpAtt.SelectedItem.Text.ToUpper() == "LC")
                {
                    gvr.CssClass = "vd_bg-blue form-control-blue vd_white";
                    drpAtt.CssClass = "vd_bg-blue form-control-blue vd_white";
                }
                else
                {
                    gvr.CssClass = "vd_bg-green form-control-blue vd_white";
                    drpAtt.CssClass = "vd_bg-green form-control-blue vd_white";
                }
            }
        }
        //foreach (GridViewRow gvr in Grd.Rows)
        //{
        //    DropDownList drpAtt = (DropDownList)gvr.FindControl("DropDownList1");
        //    drpAtt.SelectedValue=
        //}
    }

    protected void DrpAttenSection_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}

