using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;

public partial class admin_SmsForLatecomers : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = "select EmpDepName from EmpDepMaster";
            sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.FillDropDown(sql, DrpDepartment, "EmpDepName");
            oo.AddDateMonthYearDropDown(DrpDDEmpYY, DrpDDEmpMM, DrpDDEmpDD);
            FindCurrentDateandSetinDropDown();
            table1.Visible = false;
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
        sql = "select Row_Number() over (order by Eo.SrNo Asc) as SNo,Eo.DepartmentName,EO.Ecode,EO.EmpId,EO.RegistrationDate,EG.EMobileNo,EG.EFirstName,EG.EMiddleName,EG.ELastName,EG.EFatherName as FName from EmpployeeOfficialDetails EO ";
        sql = sql + "  inner join EmpGeneralDetail EG on EO.Ecode=EG.Ecode where Eo.DepartmentName='" + DrpDepartment.SelectedItem.ToString() + "'";
        sql = sql + " and eo.Withdrwal is null and eg.BranchCode=" + Session["BranchCode"].ToString() + " and eo.BranchCode=" + Session["BranchCode"].ToString() + "";

        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();

        if (Grd1.Rows.Count > 0)
        {
            TextBox txt1 = (TextBox)Grd1.Rows[0].FindControl("txtHours");
            txt1.Focus();
            table1.Visible = true;
           
        }
        else
        {
            //oo.MessageBoxforUpdatePanel("Sorry, No record found!", LinkButton1);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sorry, No Record(s) found!", "A");

        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
       if(Grd1.Rows.Count>0)
       {
           foreach (GridViewRow gvr in Grd1.Rows)
           {
               TextBox txtHours = (TextBox)gvr.FindControl("txtHours");
               TextBox txtMinutes = (TextBox)gvr.FindControl("txtMinutes");
               Label Label8 = (Label)gvr.FindControl("Label8");
               Label Label6 = (Label)gvr.FindControl("Label6");
               CheckBox chk = (CheckBox)gvr.FindControl("Chk");
               if (chk.Checked)
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
                       sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                       if (oo.ReturnTag(sql, "HitValue") != "")
                       {
                           if (oo.ReturnTag(sql, "HitValue") == "true")
                           {
                               sql = "Select SmsSent From SmsEmailMaster Where Id='26' and SmsSent=1";
                               if (oo.Duplicate(sql))
                               {
                                   SendFeesSms(Label8.Text, Label6.Text, hours, coloun, minutes);
                                   //oo.MessageBoxforUpdatePanel("Message sent successfully", LinkButton2);
                                   Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Message sent successfully", "S");

                               }
                               else
                               {
                                   //oo.MessageBoxforUpdatePanel("Permission not granted!", LinkButton2);
                                   Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not granted1!", "A");

                               }

                           }
                           else
                           {
                               //oo.MessageBoxforUpdatePanel("Permission not granted!", LinkButton2);
                               Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not granted2!", "A");

                           }
                       }
                       else
                       {
                           //oo.MessageBoxforUpdatePanel("Permission not granted!", LinkButton2);
                           Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Permission not granted3!", "A");

                       }
                   }
               }
           }
       }
    }
    public void SendFeesSms(string FmobileNo, string EmpId, string hours, string coloun, string minuts)
    {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                string collegeTitle = "";

                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");

                sql = "Select (EFirstName+' '+EMiddleName+' '+ELastName) as EmpName from EmpGeneralDetail";
                sql = sql + "  where  EmpId='" + EmpId + "'";               

                mess = "Dear " + oo.ReturnTag(sql, "EmpName") + ", you are " + hours + "" + coloun + "" + minuts + " hours late today.";
                mess = mess + Environment.NewLine;
                mess=  mess+"Regards";
                mess = mess + Environment.NewLine;
                mess=  mess+"Principal";
                mess = mess + Environment.NewLine;
                mess=  mess+collegeTitle;

                string sms_response = "";

                if (FmobileNo != "")
                {

                 sms_response = sadpNew.Send(mess, FmobileNo, "");
                    
                }
           
    }


    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkall = (CheckBox)Grd1.HeaderRow.FindControl("ChkAll");
        if (chkall.Checked)
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in Grd1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("Chk");
                chk.Checked = false;
            }
        }
    }


    protected void DrpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}