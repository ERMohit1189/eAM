using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;

namespace _1
{
    public partial class AdminSubjectwiseAttendance : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = string.Empty;
        private DataTable _dt;
        public AdminSubjectwiseAttendance()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;

            if (!IsPostBack)
            {
                try
                {
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }

                LoadClass();
                _oo.fillSelectvalue(drpStream, "<--Select-->");
                _oo.fillSelectvalue(DrpAttenSection, "<--Select-->");
                _oo.AddDateMonthYearDropDown(DrpSaal, DrpMahina, DrpDin);
                _oo.FindCurrentDateandSetinDropDown(DrpSaal, DrpMahina, DrpDin);
                Panel2.Visible = false;
            }
        }

        private void LoadClass()
        {
            _sql = "Select ClassName,Id from ClassMaster";
            _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            //sql = "Select ClassName,cm.Id as Id from ClassTeacherMaster ctm";
            //sql = sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName";
            //sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' and ctm.BranchCode=" + Session["BranchCode"].ToString() + "";
            //sql = sql + " and IsClassTeacher=1 Order by CIDOrder";

            _oo.FillDropDown_withValue(_sql, DrpAtteClass, "ClassName", "Id");
            _oo.fillSelectvalue(DrpAtteClass, "<--Select-->");
        } 

        private void LoadStream(string classid)
        {
            _sql = "Select BranchName,Id from BranchMaster where classId='" + classid + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

            //sql = "Select BranchName,bm.Id as Id from ClassTeacherMaster ctm";
            //sql = sql + " inner join BranchMaster bm on bm.Id=ctm.BranchId and bm.SessionName=ctm.SessionName";
            //sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' and ctm.BranchCode=" + Session["BranchCode"].ToString() + " and IsClassTeacher=1";

            _oo.FillDropDown_withValue(_sql, drpStream, "BranchName", "Id");
            _oo.fillSelectvalue(drpStream, "<--Select-->");
        }

        private void LoadSection(string classid)
        {
            _sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + classid + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            //sql = "Select SectionName,bm.Id as Id from ClassTeacherMaster ctm";
            //sql = sql + " inner join SectionMaster bm on bm.ClassNameId=ctm.ClassId and bm.id=ctm.SectionId and bm.SessionName=ctm.SessionName";
            //sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' ";
            //sql = sql + " and IsClassTeacher=1";

            _oo.FillDropDown_withValue(_sql, DrpAttenSection, "SectionName", "Id");
            _oo.fillSelectvalue(DrpAttenSection, "<--Select-->");
        }        

        protected void DrpAtteClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream(DrpAtteClass.SelectedValue);
            LoadSection(DrpAtteClass.SelectedValue);
            Panel2.Visible = false;
            lblmess.Text = "";
        }  

        protected void DrpSaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpSaal, DrpMahina, DrpDin);
        } 

        protected void DrpMahina_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpSaal, DrpMahina, DrpDin);
        }

        protected void DrpDin_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            lblmess.Text = "";
        }

        protected void DrpAttenSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmess.Text = "";
        }

        protected void Grd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (DrpAtteClass.SelectedItem.Text == "<--Select-->" || DrpAttenSection.SelectedItem.Text == "<--Select-->")
            {
                //oo.MessageBox("Please Select :<--Select-->:", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please Select :<--Select-->:", "A");
            }
            else
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                param.Add(new SqlParameter("@AttendanceDate", DrpSaal.SelectedItem.Text + "/" + DrpMahina.SelectedItem.Text + "/" + DrpDin.SelectedItem.Text));
                param.Add(new SqlParameter("@ClassName", DrpAtteClass.SelectedItem.ToString()));
                param.Add(new SqlParameter("@SectionName", DrpAttenSection.SelectedItem.ToString()));
                param.Add(new SqlParameter("@BranchName", drpStream.SelectedItem.ToString()));

                DataSet ds;
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GET_AttendanceDetailsDateWiseProc", param);
                if (ds.Tables.Count > 0)
                {
                    Grd.DataSource = ds;
                    Grd.DataBind();
                }
                if (Grd.Rows.Count > 0)
                {
                    Panel2.Visible = true;
                    for (int a = 0; a < Grd.Rows.Count; a++)//State Wise
                    {
                        DropDownList drp = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");

                        _sql = "select AbbreviationName from  AttendanceAbbreviationMaster where BranchCode=" + Session["BranchCode"] + "";
                        _oo.FillDropDownWithOutSelect(_sql, drp, "AbbreviationName");

                        Label lEnroll = (Label)Grd.Rows[a].FindControl("Label1");

                        _sql = "select AttendanceValue from AttendanceDetailsDateWise where Srno='" + lEnroll.Text + "'";
                        _sql = _sql + " and ClassName='" + DrpAtteClass.SelectedItem + "' and SectionName='" + DrpAttenSection.SelectedItem + "'  and AttendanceDate='" + DrpSaal.SelectedItem.Text + "/" + DrpMahina.SelectedItem.Text + "/" + DrpDin.SelectedItem.Text + "'";
                        _sql = _sql + " and CategoryWise='Date Wise'";
                        _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

                        string ss = _oo.ReturnTag(_sql, "AttendanceValue");
                        drp.SelectedValue = ss.Trim();

                        if (drp.SelectedItem.Text.ToUpper() == "A")
                        {
                            drp.CssClass = "vd_bg-red form-control-blue vd_white";
                        }
                        else if (drp.SelectedItem.Text.ToUpper() == "L")
                        {
                            drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                        }
                        else
                        {
                            drp.CssClass = "vd_bg-green form-control-blue vd_white";
                        }
                    }
                }
                Countpresent();
                AddInaccordian();
            }
            if (Grd.Rows.Count > 0)
            {
                lblTotalstudent.Text = Grd.Rows.Count.ToString();
            }
        }

        public void DailyAttendanceRadio()
        {
            // ReSharper disable once NotAccessedVariable
            string dd = string.Empty;

            if (DrpDin.SelectedItem.ToString().Length == 1)
            {
                // ReSharper disable once RedundantAssignment
                dd = "0" + DrpDin.SelectedItem;
            }
            else
            {
                // ReSharper disable once RedundantAssignment
                dd = DrpDin.SelectedItem.ToString();
            }
            bool flag = false;
            for (int a = 0; a < Grd.Rows.Count; a++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AttendanceDetailsDateWiseProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                Label lsrno = (Label)Grd.Rows[a].FindControl("Label1");
                Label lblsms = (Label)Grd.Rows[a].FindControl("lblsms");
                Label lEnroll = (Label)Grd.Rows[a].FindControl("Label2");
                Label firstname = (Label)Grd.Rows[a].FindControl("Label3");
                DropDownList drp1 = (DropDownList)Grd.Rows[a].FindControl("DropDownList1");
                TextBox txtHours = (TextBox)Grd.Rows[a].Cells[5].FindControl("txtHours");
                TextBox txtMinutes = (TextBox)Grd.Rows[a].Cells[5].FindControl("txtMinutes");

                cmd.Parameters.AddWithValue("@CategoryWise", "Date Wise");
                cmd.Parameters.AddWithValue("@AttendanceMonth", DrpMahina.SelectedItem.ToString());
                // ReSharper disable once RedundantAssignment
                String date = string.Empty;
                date = DrpSaal.SelectedItem + "/" + DrpMahina.SelectedItem + "/" + DrpDin.SelectedItem;
                cmd.Parameters.AddWithValue("@AttendanceDate", date);
                cmd.Parameters.AddWithValue("@ClassName", DrpAtteClass.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SectionName", DrpAttenSection.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@StEnRCode", lEnroll.Text);
                cmd.Parameters.AddWithValue("@SrNo", lsrno.Text);
                cmd.Parameters.AddWithValue("@StudentName", firstname.Text);
                cmd.Parameters.AddWithValue("@AttendanceValue", drp1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    flag = true;
                    _con.Close();
                    if (lblsms.Text != "Y")
                    {
                        _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                        if (_oo.ReturnTag(_sql, "HitValue") != "")
                        {
                            if (_oo.ReturnTag(_sql, "HitValue") == "true")
                            {
                                _sql = "Select SmsSent From SmsEmailMaster Where Id='6'";
                                if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                                {
                                    // ReSharper disable once RedundantAssignment
                                    string conta = "";
                                    _sql = "select FamilyContactNo from StudentFamilyDetails  where Srno='" + lsrno.Text + "'  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                                    conta = _oo.ReturnTag(_sql, "FamilyContactNo");
                                    if (drp1.SelectedValue.ToUpper() == "A")
                                    {
                                        SendFeesSms(conta, lsrno.Text);
                                    }
                                    if (drp1.SelectedValue.ToUpper() == "LC")
                                    {
                                        if (conta != "")
                                        {
                                            // ReSharper disable once RedundantAssignment
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
                                            // ReSharper disable once RedundantBoolCompare
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
                                                SendLatecommerssms(conta, lsrno.Text, hours, coloun, minutes);
                                            }

                                        }
                                    }
                                    if (drp1.SelectedValue.ToUpper() == "LT")
                                    {
                                        SendSms(conta, lsrno.Text);
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
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");

                _oo.ClearControls(Page);
            }

        }

        public void SendFeesSms(string fmobileNo, string srno)
        {
            SMSAdapterNew sadpNew = new SMSAdapterNew();
            // ReSharper disable once RedundantAssignment
            string mess = "";

            _sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + "    and  Srno='" + srno + "'";

            mess = "Dear Guardian, " + _oo.ReturnTag(_sql, "StudentName") + "(S.R. No.-" + srno + ") is absent today.";
            // ReSharper disable once NotAccessedVariable
            string smsResponse = "";

            if (fmobileNo != "")
            {
                // ReSharper disable once RedundantAssignment
                smsResponse = sadpNew.Send(mess, fmobileNo, "");
            }
        }

        public void SendLatecommerssms(string fmobileNo, string srno, string hours, string coloun, string minuts)
        {
            SMSAdapterNew sadpNew = new SMSAdapterNew();
            // ReSharper disable once RedundantAssignment
            string mess = "";
            // ReSharper disable once RedundantAssignment
            string collegeTitle = "";

            _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");

            _sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + "    and  Srno='" + srno + "'";

            mess = "Dear Guardian, your ward " + _oo.ReturnTag(_sql, "StudentName") + "(S.R. No.-" + srno + ") is " + hours + "" + coloun + "" + minuts + " hours late today.";
            mess = mess + Environment.NewLine;
            mess = mess + "Regards";
            mess = mess + Environment.NewLine;
            mess = mess + "Principal";
            mess = mess + Environment.NewLine;
            mess = mess + collegeTitle;

            // ReSharper disable once NotAccessedVariable
            string smsResponse = "";

            if (fmobileNo != "")
            {
                // ReSharper disable once RedundantAssignment
                smsResponse = sadpNew.Send(mess, fmobileNo, "");
            }
        }

        public void SendSms(string fmobileNo, string srno)
        {
            SMSAdapterNew sadpNew = new SMSAdapterNew();
            // ReSharper disable once RedundantAssignment
            string mess = "";

            _sql = "Select (FirstName+' '+MiddleName+' '+LastName) as StudentName from StudentGenaralDetail  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + "    and  Srno='" + srno + "'";

            mess = "Dear Guardian, your ward  " + _oo.ReturnTag(_sql, "StudentName") + "(S.R. No.-" + srno + ") is Late today.";
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once NotAccessedVariable
            string sms_response = "";

            if (fmobileNo != "")
            {
                // ReSharper disable once RedundantAssignment
                sms_response = sadpNew.Send(mess, fmobileNo, "");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DailyAttendanceRadio();
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {

        }

        public void PermissionGrant(int add1, LinkButton ladd)
        {
            if (add1 == 1)
            {
                ladd.Enabled = true;
            }
            else
            {
                ladd.Enabled = false;
            }
        }

        public void CheckValueAddDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "' and LTb.BranchId='" + Session["BranchCode"] + "";
            int a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
            // ReSharper disable once RedundantCast
            PermissionGrant(a, (LinkButton)btnSubmit);
        }

        public void SendNotificationEmail(string srno)
        {
            // ReSharper disable once RedundantAssignment
            string Mess = "";
            // ReSharper disable once RedundantAssignment
            string ss = "";
            // ReSharper disable once RedundantAssignment
            string collegeTitle = "";
            _sql = "Select Attendance  from  AttendanceEmailActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            ss = _oo.ReturnTag(_sql, "Attendance");
            if (ss == "true")
            {
                _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");
                // ReSharper disable once RedundantAssignment
                string logopath = "";
                _sql = "select  CologeLogoPath from  CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                logopath = _oo.ReturnTag(_sql, "CologeLogoPath");
                // ReSharper disable once RedundantAssignment
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
                _sql = "Select FirstName+' '+MiddleName+' '+LastName as StudentName   from StudentGenaralDetail";
                _sql = _sql + "    where  srno=" + "'" + srno + "'  and sessionname=" + Session["SessionName"] + "  and BranchCode=" + Session["BranchCode"] + "";
                Mess = Mess + "      Your ward " + _oo.ReturnTag(_sql, "StudentName") + " is absent today. ";
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

                // ReSharper disable once RedundantAssignment
                string femail, familyContactNo = "";
                _sql = "Select FamilyEmail,FamilyContactNo from StudentFamilyDetails where srno='" + srno + "'  and sessionname=" + Session["SessionName"] + "  and BranchCode=" + Session["BranchCode"] + "";
                femail = _oo.ReturnTag(_sql, "FamilyEmail");
                familyContactNo = _oo.ReturnTag(_sql, "FamilyContactNo");

                Mess = Mess + "     Note: In our record your E-mail is " + femail + " and Contact No. is " + familyContactNo + "</td>";

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
                // ReSharper disable once RedundantAssignment
                string schName = "";
                _sql = "Select CollegeName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                schName = _oo.ReturnTag(_sql, "CollegeName");

                Mess = Mess + "  " + schName + "</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "    &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "   <td>";

                // ReSharper disable once RedundantAssignment
                string cityId = "";
                _sql = "Select CityId from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                cityId = _oo.ReturnTag(_sql, "CityId");


                Mess = Mess + "   " + cityId + "</td>";
                Mess = Mess + " <td>";
                Mess = Mess + "   &nbsp;</td>";
                Mess = Mess + " </tr>";
                Mess = Mess + " <tr>";
                Mess = Mess + "    <td>";

                // ReSharper disable once RedundantAssignment
                string email = "";
                _sql = "Select Email from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";

                email = _oo.ReturnTag(_sql, "Email");
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

                _sql = "select FamilyEmail  from StudentFamilyDetails where srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and sessionName=" + Session["SessionName"] + "";


                EmailSending(Mess, "" + collegeTitle + " : Attendance Acknowledgement", _oo.ReturnTag(_sql, "FamilyEmail"));
            }


        }

        public bool EmailSending(string mess, string subjectParameter, string toEmailId)
        {
#pragma warning disable 219
            string ss = "";
#pragma warning restore 219
            // ReSharper disable once RedundantAssignment
            string kk = "";
            _sql = "Select Email,Password from EmailPanelSetting where Id=1";
            kk = _oo.ReturnTag(_sql, "Email");
            string pp = _oo.ReturnTag(_sql, "Password");
            bool send = false;
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmailId);//to ID

            mail.From = new MailAddress(kk);
            mail.Subject = subjectParameter;

            //Attachment attach = new Attachment(Server.MapPath("http://www.eam.co.in/ReceiptGenerate/123.docx"));
            //mail.Attachments.Add(attach);
            mail.Body = mess;
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
            catch (Exception)
            {
                // ignored
            }
            return send;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void Button8_Click(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)(((Control)(sender)).Parent).Parent;
            TextBox txtHours = (TextBox)currentrow.FindControl("txtHours");
            TextBox txtMinutes = (TextBox)currentrow.FindControl("txtMinutes");
            DropDownList dropDownList1 = (DropDownList)currentrow.FindControl("DropDownList1");
            if (dropDownList1.SelectedItem.Text.ToUpper() == "LC")
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
            Countpresent();
            AddInaccordian();
            if (dropDownList1.SelectedItem.Text.ToUpper() == "A")
            {
                dropDownList1.CssClass = "vd_bg-yellow form-control-blue vd_white";
            }
            else if (dropDownList1.SelectedItem.Text.ToUpper() == "LT" || dropDownList1.SelectedItem.Text.ToUpper() == "LC")
            {
                dropDownList1.CssClass = "vd_bg-blue form-control-blue vd_white";
            }
            else if (dropDownList1.SelectedItem.Text.ToUpper() == "NM" || dropDownList1.SelectedItem.Text.ToUpper() == "NAD")
            {
                dropDownList1.CssClass = "vd_bg-white form-control-blue";
            }
            else
            {
                dropDownList1.CssClass = "vd_bg-green form-control-blue vd_white";
            }
        }

        public void AddInaccordian()
        {
            GridView gv;
            _dt = new DataTable();
            _dt.Columns.Add("#");
            _dt.Columns.Add("SrNo");
            _dt.Columns.Add("StEnRCode");
            _dt.Columns.Add("StudentName");
            _dt.Columns.Add("FatherName");
            _dt.Columns.Add("ContactNo");
            _dt.Columns.Add("Attendance");
            for (int i = 0; i < Grd.Rows.Count; i++)
            {
                Label lblSrno = (Label)Grd.Rows[i].FindControl("Label1");
                Label lblStenrCode = (Label)Grd.Rows[i].FindControl("Label2");
                Label lblFName = (Label)Grd.Rows[i].FindControl("Label3");
                // ReSharper disable once UnusedVariable
                Label lblMName = (Label)Grd.Rows[i].FindControl("Label6");
                // ReSharper disable once UnusedVariable
                Label lblLName = (Label)Grd.Rows[i].FindControl("Label9");
                Label lblFaName = (Label)Grd.Rows[i].FindControl("Label5");
                Label lblContact = (Label)Grd.Rows[i].FindControl("Label11");
                DropDownList drpAttendance = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
                _dt.Rows.Add((i + 1).ToString(), lblSrno.Text, lblStenrCode.Text, lblFName.Text, lblFaName.Text, lblContact.Text, drpAttendance.SelectedItem.Text);
            }


            gv = new GridView();
            gv.CssClass = "table gw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
            DataView dv = new DataView(_dt, "Attendance='P'", "StudentName", DataViewRowState.CurrentRows);
            gv.DataSource = dv;
            gv.DataBind();

            AccordionPane1.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
            AccordionPane1.ContentContainer.Controls.Add(gv);
            //------------------------------------------------------------------------------------        
            gv = new GridView();
            gv.CssClass = "table rw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
            dv = new DataView(_dt, "Attendance='A'", "StudentName", DataViewRowState.CurrentRows);
            gv.DataSource = dv;
            gv.DataBind();
            AccordionPane2.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
            AccordionPane2.ContentContainer.Controls.Add(gv);
            //------------------------------------------------------------------------------------        
            gv = new GridView();
            gv.CssClass = "table yw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
            dv = new DataView(_dt, "Attendance='L'", "StudentName", DataViewRowState.CurrentRows);
            gv.DataSource = dv;
            gv.DataBind();
            AccordionPane3.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
            AccordionPane3.ContentContainer.Controls.Add(gv);
            //------------------------------------------------------------------------------------        
            gv = new GridView();
            gv.CssClass = "table bw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
            dv = new DataView(_dt, "Attendance='LT'", "StudentName", DataViewRowState.CurrentRows);
            gv.DataSource = dv;
            gv.DataBind();
            AccordionPane4.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
            AccordionPane4.ContentContainer.Controls.Add(gv);
            //------------------------------------------------------------------------------------        
            gv = new GridView();
            gv.CssClass = "table bw-color-table p-table p-table-bordered table-hover no-bm table-striped table-bordered";
            dv = new DataView(_dt, "Attendance='LC'", "StudentName", DataViewRowState.CurrentRows);
            gv.DataSource = dv;
            gv.DataBind();
            AccordionPane5.ContentContainer.ControlStyle.CssClass = "table-responsive  table-responsive2";
            AccordionPane5.ContentContainer.Controls.Add(gv);

            Accordion1.SelectedIndex = -1;
        }

        public void Countpresent()
        {
            if (Grd.Rows.Count > 0)
            {
                int p = 0, a = 0, l = 0, lc = 0, lt = 0;
                for (int i = 0; i < Grd.Rows.Count; i++)
                {
                    DropDownList dropDownList1 = (DropDownList)Grd.Rows[i].FindControl("DropDownList1");
                    if (dropDownList1.SelectedItem.Text.ToUpper() == "P")
                    {
                        p += 1;
                    }
                    if (dropDownList1.SelectedItem.Text.ToUpper() == "A")
                    {
                        a += 1;
                    }
                    if (dropDownList1.SelectedItem.Text.ToUpper() == "L")
                    {
                        l += 1;
                    }
                    if (dropDownList1.SelectedItem.Text.ToUpper() == "LT")
                    {
                        lt += 1;
                    }
                    if (dropDownList1.SelectedItem.Text.ToUpper() == "LC")
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

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}