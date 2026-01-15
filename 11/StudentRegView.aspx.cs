using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace _11
{
    public partial class AdminStudentRegView : System.Web.UI.Page
    {
        SqlConnection _con;
        readonly Campus _oo;
        private string _sql = "";
        // ReSharper disable once ArrangeTypeMemberModifiers
#pragma warning disable 169
        DataTable _dt;
#pragma warning restore 169
        private static string _studentId = String.Empty;

        public AdminStudentRegView()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        string SRNumber = "";
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            // ReSharper disable once PossibleNullReferenceException
            if (Session["Logintype"].ToString() == "Admin")
            {
                MasterPageFile = "~/Master/admin_root-manager.master";
            }
            else if (Session["Logintype"].ToString() == "Staff")
            {
                MasterPageFile = "~/Staff/staff_root-manager.master";
            }
            if (Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
            {
                MasterPageFile = "~/ap/admin_root-manager.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
            {
                show1.Visible = false;
                show2.Visible = false;
                show3.Visible = false;
            }
                _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                if (Session["srNo"] != null)
                {
                    SRNumber = Session["srNo"].ToString();
                }
                else
                {
                    Session["srNo"] = null;
                }
                
                LoadK12();
                if (Request.QueryString.Keys.Count > 0)
                {
                    if (Session["srNo"] == null)
                    {
                        _studentId = Request.QueryString["id"];
                        if (_studentId != "")
                        {
                            Session["srNo"] = _studentId;
                        }
                    }
                       
                    
                    
                    _sql = "select srno from StudentGenaralDetail where (stenrcode='"+ Session["srNo"].ToString() + "' or srno='" + Session["srNo"].ToString() + "') and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() +"'";
                    Session["srNo"] = _oo.ReturnTag(_sql, "srno");
                    //Bindstate();
                    Getstudentdetails();
                    Getstudentfatherdetails();
                    Getofficialdetails();
                    show1.Visible = false;
                    show2.Visible = false;
                    show3.Attributes.Add("class", "col-sm-12 text-right");
                }

                if (Session["srNo"] != null && Session["SessionName"]!=null && Session["BranchCode"] != null)
                {
                    try
                    {
                        //Bindstate();
                        lblG1Country11.Text = "India";
                        Getstudentdetails();
                        Getstudentfatherdetails();
                        Getofficialdetails();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                
            }
        }
        public void LoadK12()
        {
            _sql = "Select * from setting";
            string value = (_oo.ReturnTag(_sql, "isK12").ToString() == "" ? "0" : _oo.ReturnTag(_sql, "isK12").ToString());
            if (value == "True")
            {
                K12EmailDiv.Visible = false;
                Admissiondoneat.Visible = false;
                ScholarshipDiv.Visible = false;
                K12MobileDiv.Visible = false;
            }
            else
            {
                K12EmailDiv.Visible = true;
                Admissiondoneat.Visible = true;
                ScholarshipDiv.Visible = true;
                K12MobileDiv.Visible = true;

            }
            lblAadhaar.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
        }
        public void Getstudentdetails()
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>
                {
                    new SqlParameter("@Srno",Session["srNo"].ToString()),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString())

                };
                DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentInformation", param);
                var dt = new DataTable();
                if(ds!=null)
                {
                    if(ds.Tables.Count>0)
                    {
                        if(ds.Tables[0].Rows.Count>0)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }

                string date;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "USP_StudentsPhotoReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", Session["srNo"].ToString().ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dsPhoto = new DataSet();
                        das.Fill(dsPhoto);
                        cmd.Parameters.Clear();

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                        {
                            Image1.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/student-pic.png" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgshow.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/student-pic.png" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgFather.ImageUrl = dsPhoto.Tables[1].Rows[0]["FatherPhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[1].Rows[0]["FatherPhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/student-pic.png" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgFathernew.ImageUrl = dsPhoto.Tables[1].Rows[0]["FatherPhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[1].Rows[0]["FatherPhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/student-pic.png" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgMother.ImageUrl = dsPhoto.Tables[2].Rows[0]["MotherPhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[2].Rows[0]["MotherPhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/student-f-pic.png" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgMothernew.ImageUrl = dsPhoto.Tables[2].Rows[0]["MotherPhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[2].Rows[0]["MotherPhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/student-f-pic.png" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgGroupPhoto.ImageUrl = dsPhoto.Tables[3].Rows[0]["GroupPhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[3].Rows[0]["GroupPhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/group-photo.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            imgGroupPhotonew.ImageUrl = dsPhoto.Tables[3].Rows[0]["GroupPhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[3].Rows[0]["GroupPhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/group-photo.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        }
                    }
                }

                lblSrno.Text = dt.Rows[0]["Srno"].ToString();
                lblFirstNa.Text = dt.Rows[0]["FirstName"].ToString();
                lblMidNa.Text = dt.Rows[0]["MiddleName"].ToString();
                lbllast.Text = dt.Rows[0]["LastName"].ToString();

                lblEmail.Text = dt.Rows[0]["Email"].ToString();

                lblMobile.Text = dt.Rows[0]["MobileNumber"].ToString();

                string nationality = dt.Rows[0]["Nationality"].ToString();
                lblNationality.Text = nationality == string.Empty ? "INDIAN" : nationality;

                lblCaste.Text = dt.Rows[0]["Caste"].ToString();

                string mothertounge = dt.Rows[0]["MotherTongue"].ToString();
                lblMotherTongue.Text = mothertounge == string.Empty ? "HINDI" : mothertounge;

                string homeTown = dt.Rows[0]["HomeTown"].ToString();
                lblHomeTown.Text = homeTown == string.Empty ? "LUCKNOW" : homeTown;

                lblAadharNo.Text = dt.Rows[0]["AadharNo"].ToString();

                date = dt.Rows[0]["DOB"].ToString();
                if (date!="")
                {

                    _sql = "Select DatePart(dd,'"+ date + "') as date,DateName(mm,'" + date + "') as month,DatePart(yyyy,'" + date + "') as year";
                    string day = _oo.ReturnTag(_sql, "date");
                    dateFormate(ref day);
                    string year = _oo.ReturnTag(_sql, "year");
                    string yearpri = "";
                    string yearsuf = "";
                    if (Convert.ToInt16(year) < 2000)
                    {
                        yearpri = _oo.Date_in_Words(Convert.ToInt16(year.Substring(0, 2)));
                        yearsuf = _oo.Date_in_Words(Convert.ToInt16(year.Substring(2, 2)));
                        year = yearpri + " Hundred " + yearsuf;
                    }
                    else
                    {
                        year = _oo.Date_in_Words(Convert.ToInt16(_oo.ReturnTag(_sql, "year")));
                    }
                    string Date_in_Word = Date_in_Words(Convert.ToInt16(_oo.ReturnTag(_sql, "date"))) + " " + _oo.ReturnTag(_sql, "month") + " " + year;
                    lblStudentDOB.Text = date+ " &nbsp;&nbsp;(" + Date_in_Word + ")";
                }


               // lblAgeOnDate.Text = dt.Rows[0]["AgeOnDate"].ToString();

                lblGender.Text = dt.Rows[0]["Gender"].ToString();

                lblReligion.Text = dt.Rows[0]["Religion"].ToString();

                lblBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();

                lblVLeft.Text = dt.Rows[0]["VisionL"].ToString();
                lblVLeft1.Text = dt.Rows[0]["VisionL"].ToString();
                lblVRight.Text = dt.Rows[0]["VisionR"].ToString();
                lblVRight1.Text = dt.Rows[0]["VisionR"].ToString();
                lblHeight.Text = dt.Rows[0]["Height"].ToString();
                lblWeight.Text = dt.Rows[0]["Weight"].ToString();
                lblDental.Text = dt.Rows[0]["DentalHygiene"].ToString();
                lblOral.Text = dt.Rows[0]["OralHygiene"].ToString();
                lblIMark.Text = dt.Rows[0]["IdentificationMark"].ToString();
                lblSpeAilment.Text = dt.Rows[0]["SpecificAilment"].ToString();
                lblCategory.Text = dt.Rows[0]["Category"].ToString();
                try
                {
                    if (dt.Rows[0]["PhysicallDisabledCategory"].ToString() != "" && dt.Rows[0]["PhysicallDisabledCategory"].ToString() != DBNull.Value.ToString(CultureInfo.InvariantCulture))
                    {
                        lblPhysicallyDisabled.Text =dt.Rows[0]["PhysicallDisabledCategory"].ToString();
                        if (lblPhysicallyDisabled.Text == "Yes")
                        {
                            Panel2.Visible = true;

                            lblPhyName.Text = dt.Rows[0]["PhyStName"].ToString();
                            lblPhyDetail.Text = dt.Rows[0]["PhyStDetail"].ToString();

                            lblCertificateNo.Text = dt.Rows[0]["PhCertificateNo"].ToString();
                            lblIssuedBy.Text = dt.Rows[0]["PhCertificateIssuedBy"].ToString();
                        }
                        else
                        {
                            Panel2.Visible = false;
                        }
                    }
                }
                catch
                {
                    // ignored
                }

                lblPerPhoneNo.Text = dt.Rows[0]["PerPhoneNo"].ToString();
                lblPerMobileNo.Text = dt.Rows[0]["PerMobileNo"].ToString();
                lblPrePhoneNo.Text = dt.Rows[0]["PrePhoneNo"].ToString();
                lblPreMobileNo.Text = dt.Rows[0]["PreMobileNo"].ToString();
                lbl_pen.Text= dt.Rows[0]["Pen"].ToString();

            }
            catch (Exception ex)
            {
                _oo.MessageBox(ex.Message, Page);
            }
            finally
            {
                if(_con.State == ConnectionState.Open) { _con.Close(); }
            }
        }
        public void dateFormate(ref string day)
        {
            if (day == "1" || day == "21" || day == "31")
            {
                day = day + "st";
            }
            else if (day == "2" || day == "22")
            {
                day = day + "nd";
            }
            else if (day == "3" || day == "23")
            {
                day = day + "rd";
            }
            else if (day == "4" || day == "5" || day == "6" || day == "7" || day == "8" || day == "9" || day == "10" || day == "11" || day == "12" || day == "13" || day == "14" || day == "15" || day == "16" || day == "17" || day == "18" || day == "19" || day == "20" || day == "24" || day == "25" || day == "26" || day == "27" || day == "28" || day == "29" || day == "30")
            {
                day = day + "th";
            }
        }
        public string Date_in_Words(int date)
        {
            string[] digit = new string[32];

            digit[1] = "First";
            digit[2] = "Second";
            digit[3] = "Third";
            digit[4] = "Fourth";
            digit[5] = "Fifth";
            digit[6] = "Sixth";
            digit[7] = "Seventh";
            digit[8] = "Eighth";
            digit[9] = "Ninth";
            digit[10] = "Tenth";
            digit[11] = "Eleventh";
            digit[12] = "Twelfth";
            digit[13] = "Thirteenth";
            digit[14] = "Fourteenth";
            digit[15] = "Fifteenth";
            digit[16] = "Sixteenth";
            digit[17] = "Seventeenth";
            digit[18] = "Eighteenth";
            digit[19] = "Nineteenth";
            digit[20] = "Twentieth";
            digit[21] = "Twenty First";
            digit[22] = "Twenty Second";
            digit[23] = "Twenty Third";
            digit[24] = "Twenty Fourth";
            digit[25] = "Twenty Fifth";
            digit[26] = "Twenty Sixth";
            digit[27] = "Twenty Seventh";
            digit[28] = "Twenty Eighth";
            digit[29] = "Twenty Ninth";
            digit[30] = "Thirtieth";
            digit[31] = "Thirty First";


            return digit[date];
        }
        public void Getstudentfatherdetails()
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>
                {
                    new SqlParameter("@Srno",Session["srNo"].ToString()),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString())

                };
                DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentInformation", param);
                var dt = new DataTable();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }

                try
                {
                    lblfaNameee.Text = dt.Rows[0]["FatherName"].ToString();

                    string facontact = dt.Rows[0]["FatherContactNo"].ToString();
                    lblcontfa.Text = facontact;

                    string familycontact = dt.Rows[0]["FamilyContactNo"].ToString();
                    lblcontactNo.Text = familycontact == string.Empty ? facontact : familycontact;

                    string mocontact = dt.Rows[0]["MotherContactNo"].ToString();
                    lblmothercontact.Text = mocontact == string.Empty ? facontact : mocontact;

                    string fanationality = dt.Rows[0]["FatherNationality"].ToString();
                    lblFatherNationality.Text = fanationality == string.Empty ? "INDIAN" : fanationality;

                    string monationality = dt.Rows[0]["MotherNationality"].ToString();
                    lblMotherNationality.Text = monationality == string.Empty ? "INDIAN" : monationality;

                    string guardianName = dt.Rows[0]["FamilyGuardianName"].ToString();
                    lblguardianname.Text = guardianName == string.Empty ? lblfaNameee.Text.Trim() : guardianName;

                    lblOccupationfa.Text = dt.Rows[0]["FatherOccupation"].ToString();

                    lbldesfa.Text = dt.Rows[0]["FatherDesignation"].ToString();
                    lblqufa.Text = dt.Rows[0]["FatherQualification"].ToString();
                    lblincomefa.Text = dt.Rows[0]["FatherIncomeMonthly"].ToString();
                    lbloffaddfa.Text = dt.Rows[0]["FatherOfficeAddress"].ToString();

                    lblemailfather.Text = dt.Rows[0]["FatherEmail"].ToString();
                    lblRelationship.Text = dt.Rows[0]["FamilyRelationship"].ToString();
                    lblemailfamily.Text = dt.Rows[0]["FamilyEmail"].ToString();

                    lblcontfaAltername.Text = dt.Rows[0]["PreMobileNo"].ToString();
                    lblmotherNameeee.Text = dt.Rows[0]["MotherName"].ToString();

                    lblOccupationmoth.Text = dt.Rows[0]["MotherOccupation"].ToString();

                    lblParentTotalIncome.Text = dt.Rows[0]["ParentTotalIncome"].ToString();

                    lbldesmoth.Text = dt.Rows[0]["MotherDesignation"].ToString();
                    lblqualimother.Text = dt.Rows[0]["MotherQualification"].ToString();
                    lblincomemonthlymother.Text = dt.Rows[0]["MotherIncomeMonthly"].ToString();
                    lblofficeaddmother.Text = dt.Rows[0]["MotherOfficeAddress"].ToString();
                
                    lblmotheremail.Text = dt.Rows[0]["MotherEmail"].ToString();
              
                    lblG1Address.Text = dt.Rows[0]["G1Address"].ToString();
                    lblG1City11.Text = dt.Rows[0]["G1City"].ToString() == "" ? "Other" : dt.Rows[0]["G1City"].ToString();
                    lblG1State11.Text = dt.Rows[0]["G1State"].ToString() == "" ? "Other" : dt.Rows[0]["G1State"].ToString();
                    lblG1Country11.Text = dt.Rows[0]["G1Country"].ToString();
                    lblG1Pin.Text = dt.Rows[0]["G1Pin"].ToString();



                    //lblPreaddress.Text = dt.Rows[0]["StLocalAddress"].ToString();
                    //lblPreCity.Text = dt.Rows[0]["StlocalCity"].ToString() == "" ? "Other" : dt.Rows[0]["StlocalCity"].ToString();
                    //lblPreState.Text = dt.Rows[0]["StlocalState"].ToString() == "" ? "Other" : dt.Rows[0]["StlocalState"].ToString();
                    //lblPreCountry.Text = dt.Rows[0]["StlocalCountry"].ToString();
                    //lblPerZip.Text = dt.Rows[0]["StLocalZip"].ToString();
                    string StLocalAddress = dt.Rows[0]["StLocalAddress"].ToString();
                    string StlocalCity = dt.Rows[0]["StlocalCity"].ToString() == "" ? "Other" : dt.Rows[0]["StlocalCity"].ToString();
                    string StlocalState = dt.Rows[0]["StlocalState"].ToString() == "" ? "Other" : dt.Rows[0]["StlocalState"].ToString();
                    string StLocalZip = dt.Rows[0]["StLocalZip"].ToString();
                    string StlocalCountry = dt.Rows[0]["StlocalCountry"].ToString();

                    lblPersendtAddress.Text = StLocalAddress+" "+ StlocalCity+" "+ StlocalState+" "+ StLocalZip+" "+ StlocalCountry;


                    //lblPerAdd.Text = dt.Rows[0]["StPerAddress"].ToString();
                    //lblPerCity.Text = dt.Rows[0]["StpreCity"].ToString() == "" ? "Other" : dt.Rows[0]["StpreCity"].ToString();
                    //lblPerState.Text = dt.Rows[0]["StpreState"].ToString() == "" ? "Other" : dt.Rows[0]["StpreState"].ToString();
                    //lblPerCountry.Text = dt.Rows[0]["StpreCountry"].ToString();
                    //lblPreZip.Text = dt.Rows[0]["StPerZip"].ToString();
                    string StPerAddress = dt.Rows[0]["StPerAddress"].ToString();
                    string StpreCity = dt.Rows[0]["StpreCity"].ToString() == "" ? "Other" : dt.Rows[0]["StpreCity"].ToString();
                    string StpreState = dt.Rows[0]["StpreState"].ToString() == "" ? "Other" : dt.Rows[0]["StpreState"].ToString();
                    string StPerZip = dt.Rows[0]["StPerZip"].ToString();
                    string StpreCountry = dt.Rows[0]["StpreCountry"].ToString();
                    lblPermanentAddress.Text = StPerAddress + " " + StpreCity + " " + StpreState + " " + StPerZip + " " + StpreCountry;

                }
                catch (Exception)
                {
                    // ignored
                }
            }
            catch (Exception ex)
            {
                _oo.MessageBox(ex.Message, Page);
            }
            finally
            {
                if (_con.State == ConnectionState.Open) { _con.Close(); }
            }
        }

        public void Getofficialdetails()
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>
                {
                    new SqlParameter("@Srno",Session["srNo"].ToString()),
                    new SqlParameter("@SessionName",Session["SessionName"].ToString()),
                    new SqlParameter("@BranchCode",Session["BranchCode"].ToString())

                };
                DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStudentInformation", param);
                var dt = new DataTable();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dt = ds.Tables[0];
                        }
                    }
                }

                string sqlforsection = _sql;
                try
                {
                    lblSession.Text = Session["SessionName"].ToString();
                    lblDOA.Text = dt.Rows[0]["DateOfAdmiission"].ToString();
                    lblSr.Text = dt.Rows[0]["SrNo"].ToString();
                    Session["StEnRcode"] = dt.Rows[0]["StEnRCode"].ToString();
                    lblfileno.Text = dt.Rows[0]["FileNo"].ToString();
               
                    lblrema.Text = dt.Rows[0]["Remark"].ToString();
                    lblDFA.Text = dt.Rows[0]["DFA"].ToString();
                    lblCFA.Text = dt.Rows[0]["CFA"].ToString();
                    lblCOFA.Text = dt.Rows[0]["COFA"].ToString();
                    lblSFA.Text = dt.Rows[0]["SFA"].ToString();
                    lblAddDoneat.Text = dt.Rows[0]["AdmissionDoneAt"].ToString() == string.Empty ? "0.00" : dt.Rows[0]["AdmissionDoneAt"].ToString();
                    try
                    {
                        lblMedium.Text = dt.Rows[0]["Medium"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        lblBoard.Text = dt.Rows[0]["Board"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        lblNEWOLSAdmission.Text = dt.Rows[0]["TypeOFAdmision"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        lblPanelCardType.Text = dt.Rows[0]["Card"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        rbHostel.Text = dt.Rows[0]["HostelRequired"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        rbTransport.Text = dt.Rows[0]["TransportRequired"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        lblHouseName.Text = dt.Rows[0]["HouseName"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        rbLibrary.Text = dt.Rows[0]["LibraryRequired"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        lblBranch.Text = dt.Rows[0]["GroupNa"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        lblSchoolcollegeRollno.Text = dt.Rows[0]["InstituteRollNo"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        lblUniversityBoardRollNo.Text = dt.Rows[0]["BoardUniversityRollNo"].ToString();
                        lblUniversityBoardRollNo1.Text = dt.Rows[0]["BoardUniversityRollNo"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        lblApparID.Text = dt.Rows[0]["APAARID"].ToString();
                        lbleducationAct.Text = dt.Rows[0]["EducationAct"].ToString();
                        lblShift.Text = dt.Rows[0]["ShiftName"].ToString();
                        lblMachineNo.Text = dt.Rows[0]["MachineNo"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    rbScholarship.Text = dt.Rows[0]["Scholarship"].ToString() != "" ? dt.Rows[0]["Scholarship"].ToString() : "No";

                    lblCardNo.Text = dt.Rows[0]["CardNo"].ToString();

                    lblCourse.Text = dt.Rows[0]["CourseName"].ToString() != "" ? dt.Rows[0]["CourseName"].ToString() : "0";

                    if (dt.Rows[0]["MODForFeeDeposit"].ToString() != string.Empty)
                    {
                        try
                        {
                            lblFeeDepositMOD.Text = dt.Rows[0]["MODForFeeDeposit"].ToString();
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    try
                    {
                        lblTypeofEducation.Text = dt.Rows[0]["TypeofEducation"].ToString() == "R" ? "Regular" : "Private";
                    }
                    catch
                    {
                        // ignored
                    }
                    lblClass.Text = dt.Rows[0]["ClassName"].ToString();
                    lblBranch.Text = dt.Rows[0]["BranchName"].ToString();
                    lblStream.Text = dt.Rows[0]["Stream"].ToString();
                    lblSection.Text = dt.Rows[0]["SectionName"].ToString();
                    lbl_pen.Text=dt.Rows[0]["Pen"].ToString();

                    try
                    {
                        lblboardPre.Text = dt.Rows[0]["Board"].ToString();
                        lblPreviousInstite.Text = dt.Rows[0]["Institute"].ToString();
                        lbludisecodePre.Text = dt.Rows[0]["UDISECode"].ToString();
                        lblContactPre.Text = dt.Rows[0]["ContactNo"].ToString();
                        lblPreviousClass.Text = dt.Rows[0]["PreviousClass"].ToString();
                        lblAddressPre.Text = dt.Rows[0]["Schooladdress"].ToString();
                        lblMediumPre.Text = dt.Rows[0]["Medium"].ToString();
                        lblAttendancePre.Text = dt.Rows[0]["Attendance"].ToString();
                        lblResultPre.Text = dt.Rows[0]["Result"].ToString();
                        lblMarksPrecentage.Text = dt.Rows[0]["MarksPercentage"].ToString();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                }
                catch
                {
                    // ignored
                }
            }
            catch (Exception ex)
            {
                _oo.MessageBox(ex.Message, Page);
            }
            finally
            {
                if (_con.State == ConnectionState.Open) { _con.Close(); }
            }
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            if (lblEnter.Text != "")
            {
                _studentId = lblEnter.Text = lblEnter.Text.Replace(" ", string.Empty).Replace("'","").Trim();

                //Bindstate();
                //FamilyDetailsDropDown();
                lblG1Country11.Text = "India";
                Getstudentdetails();
                Getstudentfatherdetails();
                Getofficialdetails();
            }
        }
    
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}