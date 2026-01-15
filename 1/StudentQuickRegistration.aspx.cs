using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;

namespace _1
{
    public partial class StudentQuickRegistration : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = string.Empty;
        private bool _recordNotInsertOfficial;
        private bool _recordNotInsertGeneral;
        private bool _recordNotInsertPrevious;
        private bool _redorcNotInsertfamilDetails;
#pragma warning disable 414
        private bool _recordNotInsertDocument;
#pragma warning restore 414
        private readonly DLL _dllobj = new DLL();
        public StudentQuickRegistration()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
            {
                MasterPageFile = "~/ap/admin_root-manager";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((string)Session["LoginName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            var ss = string.Empty;
            var co = 0;
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                if (TextBox67.Text.Trim() != "")
                {
                    loadByAdmission();
                }


                _sql = "select * from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + "";
                if (!_oo.Duplicate(_sql))
                {
                    btnDiv.Visible = false;
                    _oo.MessageBox("Please initialize S.R. No.!", Page);
                }
                else
                {
                    btnDiv.Visible = true;
                }
                LoadK12();
                
                try
                {
                    _sql = "select BranchCode, PreString, Separater from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + " and SrNoType='Manual'";
                    if (_oo.Duplicate(_sql))
                    {
                        divSrNo.Visible = true;
                        lblSrString.Text = (_oo.ReturnTag(_sql, "PreString") + (_oo.ReturnTag(_sql, "PreString") != "" ? _oo.ReturnTag(_sql, "Separater") : ""));
                        txtSr.Attributes.Add("class", "form-control-blue validatetxt");
                    }
                    else
                    {
                        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
                        {
                            TextBox67.Text = Session["RecieptNo"].ToString();
                            if (TextBox67.Text.Trim() != "")
                            {
                                loadByAdmission();
                            }
                            divReceipForAddmission.Visible = false;
                        }
                    }
                    GeneralDetailDropDown();
                    OfficialDetailDropDown();
                    var getdate = DateTime.Today.ToString("dd-MMM-yyyy");
                    txtAgeOnDate.Text = getdate;
                    TextBox100.Text = getdate;


                }
                catch (Exception ex) { _oo.MessageBox(ex.Message, Page); }


                _sql = "select id  from StudentOfficialDetails";
                _sql +=  "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                try
                {
                    co = Convert.ToInt32(_oo.ReturnTag(_sql, "id"));
                }
                catch (Exception) { co = 0; }
                co = co + 1;
                ss = IdGenerationCollege(co.ToString());

                txtFirstNa.Focus();
                var obj = new BLL();
                //CheckTextTrnsformation();
                string sqlss = "select ReceiptNocompulsory from  admissionDatePermission where ReceiptNocompulsory=1 and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(sqlss))
                {
                    TextBox67.Attributes.Add("class", "form-control-blue validatetxt");
                    TextBox67.Focus();
                }
                else
                {
                    TextBox67.Attributes.Add("class", "form-control-blue");
                }
            }

        }
       
        public void LoadK12()
        {
            _sql = "Select * from setting";
            
            lblAadhaar2.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
        }
        public void TextTrnsform()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@isDo", "Select"),
                new SqlParameter("@value", ""),
                new SqlParameter("@SessionName", ""),
                new SqlParameter("@LoginName", "")
            };
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x10
            };
            param.Add(para);
            var value = _dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "textTransform", "finalsubmit('" + value + "')", true);
        }
        private void LoaddefaultCountry(DropDownList drp)
        {
            _sql = "Select CountryName,Id from CountryMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Country", drp);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void LoaddefaultState(DropDownList drp, DropDownList drpValue)
        {
            drp.Items.Clear();
            _sql = "Select count(*) cnt from StateMaster where countryId='" + drpValue.SelectedValue + "'";
            if (_oo.ReturnTag(_sql, "cnt") == "0")
            {
                drp.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                _sql = "Select StateName,Id from StateMaster where countryId='" + drpValue.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
                using (var objBll = new BLL())
                {
                    try
                    {
                        objBll.loadDefaultvalue("State", drp);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        private void LoaddefaultCity(DropDownList drp, DropDownList drpValue)
        {
            drp.Items.Clear();
            _sql = "Select count(*) cnt from CityMaster where StateId='" + drpValue.SelectedValue + "'";
            if (_oo.ReturnTag(_sql, "cnt") == "0")
            {
                drp.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
                using (var objBll = new BLL())
                {
                    try
                    {
                        objBll.loadDefaultvalue("City", drp);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        // ReSharper disable once UnusedMember.Local
        private void LoadCountry(DropDownList drp)
        {
            _sql = "Select CountryName,Id from CountryMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
        }

        private void LoadState(DropDownList drp, DropDownList drpValue)
        {
            _sql = "Select StateName,Id from StateMaster where CountryId='" + drpValue.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
        }

        private void LoadCity(DropDownList drp, DropDownList drpValue)
        {
            _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
        }
        

        private void LoadDefaultBoard()
        {
            _sql = "Select BoardName,id from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, DrpBoard, "BoardName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Board", DrpBoard);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void LoadDefaultMedium()
        {
            _sql = "Select Medium,id from MediumMaster where  BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpMedium, "Medium", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Medium", drpMedium);
                }
                catch
                {
                    // ignored
                }
            }
        }
        private void LoadDefaultNationality()
        {
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Nationality", TextBox65);
                }
                catch
                {
                    // ignored
                }
            }
        }
        private void LoadDefaultReligion()
        {
            _sql = "select ReligionName,ReligionId from ReligionMaster";
            _oo.FillDropDown_withValue(_sql, DropDownList1, "ReligionName", "ReligionId");

            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Religion", DropDownList1);
                }
                catch
                {
                    // ignored
                }
            }
        }

        protected void SwipeLabel(Control parent)
        {
            foreach (Control childControl in parent.Controls)
            {
                if ((childControl.Controls.Count > 0))
                {
                    SwipeLabel(childControl);
                }
                else
                {
                    if (childControl is Label)
                    {
                        var text = ((Label)childControl).Text;
                        _sql = "Select replace from DefaultText where replacewith='" + text + "'";
                        if (_oo.ReturnTag(_sql, "replace") != "")
                        {
                            ((Label)childControl).Text = _oo.ReturnTag(_sql, "replace");
                        }
                    }
                }
            }
        }

        protected void CheckTextTrnsformation()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@isDo", "Select"),
                new SqlParameter("@value", ""),
                new SqlParameter("@SessionName", ""),
                new SqlParameter("@LoginName", "")
            };
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x10
            };
            param.Add(para);
            var value = _dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
            if (value == DBNull.Value) return;
            switch ((string)value)
            {
                case "U":
                    AddStyletocotrol(Page, "uppercase");
                    break;
                case "L":
                    AddStyletocotrol(Page, "lowercase");
                    break;
                case "C":
                    AddStyletocotrol(Page, "capitalize");
                    break;
                default:
                    AddStyletocotrol(Page, "none");
                    break;
            }
        }

        public void AddStyletocotrol(Control parent, string istransform)
        {
            foreach (Control childControl in parent.Controls)
            {
                if ((childControl.Controls.Count > 0))
                {
                    AddStyletocotrol(childControl, istransform);
                }
                else
                {
                    var box = childControl as TextBox;
                    if (box != null)
                    {
                        if (box.ID != "txtSr")
                        {
                            box.Style.Add("text-transform", istransform);
                        }
                    }
                    else if (childControl is DropDownList)
                    {
                        ((DropDownList)childControl).Style.Add("text-transform", istransform);
                    }
                    else if (childControl is RadioButton)
                    {
                        ((RadioButton)childControl).Style.Add("text-transform", istransform);
                    }
                    else if (childControl is RadioButtonList)
                    {
                        ((RadioButtonList)childControl).Style.Add("text-transform", istransform);
                    }
                    else if (childControl is CheckBox)
                    {
                        ((CheckBox)childControl).Style.Add("text-transform", istransform);
                    }
                    else if (childControl is CheckBoxList)
                    {
                        ((CheckBoxList)childControl).Style.Add("text-transform", istransform);
                    }
                }
            }
        }


        protected DataTable AddColumn()
        {
            var dt = new DataTable();
            dt.Columns.Add("srno"); dt.Columns.Add("exam");
            dt.Columns.Add("board"); dt.Columns.Add("result"); dt.Columns.Add("institute");
            dt.Columns.Add("yop"); dt.Columns.Add("medium");
            dt.Columns.Add("subject"); dt.Columns.Add("rollno");
            dt.Columns.Add("certificateno"); dt.Columns.Add("marksSheetno");
            dt.Columns.Add("city"); dt.Columns.Add("state");
            dt.Columns.Add("country"); dt.Columns.Add("maxmarks");
            dt.Columns.Add("obtained"); dt.Columns.Add("per");

            return dt;
        }

        protected DataRow SetInitialValue(DataTable dt, int i)
        {
            var dr = dt.NewRow();
            dr[0] = (i + 1);
            dr["exam"] = ""; dr["board"] = ""; dr["result"] = "";
            dr["institute"] = ""; dr["yop"] = "";
            dr["medium"] = ""; dr["subject"] = "";
            dr["rollno"] = ""; dr["certificateno"] = "";
            dr["marksSheetno"] = ""; dr["city"] = ""; dr["state"] = "";
            dr["country"] = ""; dr["maxmarks"] = "";
            dr["obtained"] = ""; dr["per"] = "";
            return dr;
        }


        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var currntrow = (RepeaterItem)lnk.NamingContainer;
            var i = currntrow.ItemIndex;

        }

        #region
        public void GeneralDetailDropDown()
        {
            try
            {
                LoaddefaultCountry(DrpPreCountry);
                LoaddefaultState(DrpPreState, DrpPreCountry);
                LoaddefaultCity(DrpPreCity, DrpPreState);
                LoadDefaultCasteName();
                LoadDefaultReligion();
            }
            catch (Exception)
            {
                // ignored
            }

        }

        private void LoadDefaultCasteName()
        {
            _sql = "select CasteName,CasteId from CasteMaster";
            _oo.FillDropDown_withValue(_sql, DropDownList2, "CasteName", "CasteId");
            DropDownList2.Items.Insert(0, new ListItem("<--Select-->", ""));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Category", DropDownList2);
                }
                catch
                {
                    // ignored
                }
            }
        }
        public void OfficialDetailDropDown()
        {
            LoadCourse();
            LoadClass();
            LoadBranch();
            LoadStream();

            LoadSection();
            LoadDefaultMedium();
            LoadDefaultBoard();
            LoadDefaultNationality();
            LoadDefaultHouse();
            LoadDefaultFeeGroup();
            LoadDefaultTypeofAdmission();
            LoadDefaultFatherOccu();
            LoadDefaultMotherOccu();

        }
        private void LoadDefaultFeeGroup()
        {
            _sql = "Select id,  FeeGroupName from FeeGroupMaster ";
            _sql +=  " where  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpPanelCardType, "FeeGroupName", "id");
            drpPanelCardType.Items.Insert(0, new ListItem("<--Select-->", ""));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("FeeGroup", drpPanelCardType);
                }
                catch
                {
                    // ignored
                }
            }
        }
        private void LoadDefaultHouse()
        {
            _sql = "select HouseName from HouseMaster where  BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDownWithOutSelect(_sql, DropDownList4, "HouseName");
            DropDownList4.Items.Insert(0, new ListItem("<--Select-->", ""));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("HouseName", DropDownList4);
                }
                catch
                {
                    // ignored
                }
            }
        }


        private void LoadDefaultTypeofAdmission()
        {
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("TypeofAdmission", DrpNEWOLSAdmission);
                }
                catch
                {
                    // ignored
                }
            }
        }
        
        private void LoadDefaultFatherOccu()
        {
            _sql = "Select DesignationName from GuardianDesMaster where DesignationName not like 'House%'";
            _oo.FillDropDownWithOutSelect(_sql, drpOccupationfa, "DesignationName");
            drpOccupationfa.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Occupation", drpOccupationfa);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void LoadDefaultMotherOccu()
        {
            _sql = "Select DesignationName from GuardianDesMaster";
            _oo.FillDropDownWithOutSelect(_sql, drpOccupationmoth, "DesignationName");
            drpOccupationmoth.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Occupation", drpOccupationmoth);
                }
                catch
                {
                    // ignored
                }
            }
        }
        
        public bool GeneralDetails()
        {
            var flag = true;
            using (var cmd = new SqlCommand())
            {


                cmd.CommandText = "StudentGenaralDetailProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;

                cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
                cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                cmd.Parameters.AddWithValue("@FirstName", txtFirstNa.Text);
                cmd.Parameters.AddWithValue("@MiddleName", "");
                cmd.Parameters.AddWithValue("@LastName", "");
                var date = txtStudentDOB.Text.Trim();
                if (date != "")
                {
                    cmd.Parameters.AddWithValue("@DOB", date);
                }
                cmd.Parameters.AddWithValue("@Gender", RadioButtonList1.SelectedValue);
                cmd.Parameters.AddWithValue("@Email", "");
                cmd.Parameters.AddWithValue("@MobileNumber", "");
                cmd.Parameters.AddWithValue("@SiblingCategory", "No");
                cmd.Parameters.AddWithValue("@SBSrNo", "");
                cmd.Parameters.AddWithValue("@SBStName", "");
                cmd.Parameters.AddWithValue("@SBFathersName", "");
                cmd.Parameters.AddWithValue("@SBClass", "");
                cmd.Parameters.AddWithValue("@SBSection", "");
                cmd.Parameters.AddWithValue("@PhysicallDisabledCategory", "No");
                cmd.Parameters.AddWithValue("@PhyStName", "");
                cmd.Parameters.AddWithValue("@PhyStDetail", "");
                cmd.Parameters.AddWithValue("@StLocalAddress", txtPreaddress.Text);
                cmd.Parameters.AddWithValue("@StLocalCountryId", DrpPreCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@StLocalStateId", DrpPreState.SelectedValue);
                cmd.Parameters.AddWithValue("@StLocalCityId", DrpPreCity.SelectedValue);
                cmd.Parameters.AddWithValue("@StLocalZip", txtPreZip.Text);
                cmd.Parameters.AddWithValue("@StPerAddress", txtPreaddress.Text);
                cmd.Parameters.AddWithValue("@StPerCountryId", DrpPreCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@StPerStateId", DrpPreState.SelectedValue);
                cmd.Parameters.AddWithValue("@StPerCityId", DrpPreCity.SelectedValue);
                cmd.Parameters.AddWithValue("@StPerZip", txtPreZip.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@Religion", DropDownList1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Nationality", TextBox65.Text.Trim());
                cmd.Parameters.AddWithValue("@Category", DropDownList2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Caste", "");
                cmd.Parameters.AddWithValue("@BloodGroup", "NA");
                cmd.Parameters.AddWithValue("@HouseName", DropDownList4.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Height", "");
                cmd.Parameters.AddWithValue("@Weight", "");
                cmd.Parameters.AddWithValue("@VisionL", "");
                cmd.Parameters.AddWithValue("@VisionR", "");
                cmd.Parameters.AddWithValue("@DentalHygiene", "");
                cmd.Parameters.AddWithValue("@OralHygiene", "");
                cmd.Parameters.AddWithValue("@IdentificationMark", "");
                cmd.Parameters.AddWithValue("@SpecificAilment", "");
                
                cmd.Parameters.AddWithValue("@PhotoPath", "../img/user-pic/student-pic.png");
                cmd.Parameters.AddWithValue("@PhotoName", "");
                cmd.Parameters.AddWithValue("@phCertificateNo", "");
                cmd.Parameters.AddWithValue("@phIssuedBy", "");

                cmd.Parameters.AddWithValue("@PrePhoneNo", "");
                cmd.Parameters.AddWithValue("@PreMobileNo", "");
                cmd.Parameters.AddWithValue("@PerPhoneNo", "");
                cmd.Parameters.AddWithValue("@PerMobileNo", "");
                cmd.Parameters.AddWithValue("@MotherTongue", "");
                cmd.Parameters.AddWithValue("@HomeTown", "");
                cmd.Parameters.AddWithValue("@AgeOnDate", txtAgeOnDate.Text.Trim());

                cmd.Parameters.AddWithValue("@AadharNo", txtAadharNo.Text.Trim());
                cmd.Parameters.AddWithValue("@AadharIssueDate", "");
                cmd.Parameters.AddWithValue("@smsAcknowledgment", "");
                cmd.Parameters.AddWithValue("@emailAcknowledgment", "");
                cmd.Parameters.AddWithValue("@isSmsAck", 0);
                cmd.Parameters.AddWithValue("@isEmailAck", 0);
                cmd.Parameters.AddWithValue("@Pen", txtpen.Text);
                try
                {
                    _con.Open();
                    int x = cmd.ExecuteNonQuery();
                    _con.Close();
                    flag = true;
                    cmd.Parameters.Clear();
                }
                catch (SqlException EX)
                {
                    flag = false; _con.Close();
                }
                catch (Exception)
                {
                    flag = false; _con.Close();
                }
                return flag;
            }
        }
        public bool FamilyDetails()
        {
            var flag = true;
            using (var cmd = new SqlCommand())
            {
                if (drpOccupationfa.SelectedItem.Text == "<--Select-->" || drpOccupationmoth.SelectedItem.Text == "<--Select-->")
                {
                    drpOccupationfa.SelectedIndex = 1;
                }
                cmd.CommandText = "StudentFamilyDetailsProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
                cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                cmd.Parameters.AddWithValue("@FatherName", txtfaNameee.Text);
                cmd.Parameters.AddWithValue("@FatherOccupation", drpOccupationfa.Text);
                cmd.Parameters.AddWithValue("@FatherDesignation", "");
                cmd.Parameters.AddWithValue("@FatherQualification", "");
                cmd.Parameters.AddWithValue("@FatherIncomeMonthly", "0.00");
                cmd.Parameters.AddWithValue("@FatherOfficeAddress", "");
                cmd.Parameters.AddWithValue("@FatherContactNo", txtcontfa.Text);
                cmd.Parameters.AddWithValue("@FatherEmail", "");
                cmd.Parameters.AddWithValue("@FamilyIncomeMonthly", "");
                cmd.Parameters.AddWithValue("@FamilyRelationship", "Father");
                cmd.Parameters.AddWithValue("@FamilyEmail", "");
                cmd.Parameters.AddWithValue("@FamilyGuardianName", txtfaNameee.Text);
                cmd.Parameters.AddWithValue("@FamilyContactNo", txtcontfa.Text);
                cmd.Parameters.AddWithValue("@MotherName", txtmotherNameeee.Text);
                cmd.Parameters.AddWithValue("@MotherOccupation", drpOccupationmoth.Text);
                cmd.Parameters.AddWithValue("@MotherDesignation", "");
                cmd.Parameters.AddWithValue("@MotherQualification", "");
                cmd.Parameters.AddWithValue("@MotherIncomeMonthly", "0.00");
                cmd.Parameters.AddWithValue("@MotherOfficeAddress", "");
                cmd.Parameters.AddWithValue("@MotherContactNo", txtmothercontact.Text);
                cmd.Parameters.AddWithValue("@MotherEmail", "");
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@GuardiantwoIncomeMonthly", "0.00");
                cmd.Parameters.AddWithValue("@GuardiantwoName", txtfaNameee.Text);
                cmd.Parameters.AddWithValue("@GuardiantwoContact", txtcontfa.Text);
                cmd.Parameters.AddWithValue("@GuardiantwoRelationship", "Father");
                cmd.Parameters.AddWithValue("@GuardiantwoEmail", "");
                cmd.Parameters.AddWithValue("@FatherPhotoPath", "../img/user-pic/student-pic.png");
                cmd.Parameters.AddWithValue("@FatherPhotoName", "");
                cmd.Parameters.AddWithValue("@MotherPhotoPath", "../img/user-pic/student-f-pic.png");
                cmd.Parameters.AddWithValue("@MotherPhotoName", "");
                cmd.Parameters.AddWithValue("@FatherOfficePhoneNo", "");
                cmd.Parameters.AddWithValue("@FatherOfficeMobileNo", "");
                cmd.Parameters.AddWithValue("@FatherOfficeEmail", "");
                cmd.Parameters.AddWithValue("@MotherOfficePhoneNo", "");
                cmd.Parameters.AddWithValue("@MotherOfficeMobileNo", "");
                cmd.Parameters.AddWithValue("@MotherOfficeEmail", "");
                cmd.Parameters.AddWithValue("@ParentTotalIncome", "0.00");
                
                cmd.Parameters.AddWithValue("@GroupPhotoPath", "../img/user-pic/group-photo.jpg");
                cmd.Parameters.AddWithValue("@GroupPhotoName", "");
                cmd.Parameters.AddWithValue("@G1Address", txtPreaddress.Text);
                cmd.Parameters.AddWithValue("@G1Country", DrpPreCountry.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G1State", DrpPreState.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G1City", DrpPreCity.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G1PhoneNo", "");
                cmd.Parameters.AddWithValue("@G1MobileNo", "");
                cmd.Parameters.AddWithValue("@G1Pin", txtPreZip.Text.Trim());
                cmd.Parameters.AddWithValue("@G2Address", txtPreaddress.Text);
                cmd.Parameters.AddWithValue("@G2Country", DrpPreCountry.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G2State", DrpPreState.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G2City", DrpPreCity.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G2PhoneNo", "");
                cmd.Parameters.AddWithValue("@G2MobileNo", "");
                cmd.Parameters.AddWithValue("@G2Pin", txtPreZip.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherNationality", TextBox65.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherNationality", TextBox65.Text.Trim());
                // Parents Health
                cmd.Parameters.AddWithValue("@FatherHealthAdhaarNo", "");
                cmd.Parameters.AddWithValue("@FatherHealthDOB", "");
                cmd.Parameters.AddWithValue("@FatherHealthWeight", "");
                cmd.Parameters.AddWithValue("@FatherHealthHeight", "");
                cmd.Parameters.AddWithValue("@FatherHealthBloodgroup", "Other");
                cmd.Parameters.AddWithValue("@MotherHealthAdhaarNo", "");
                cmd.Parameters.AddWithValue("@MotherHealthDOB", "");
                cmd.Parameters.AddWithValue("@MotherHealthWeight", "");
                cmd.Parameters.AddWithValue("@MotherHealthHeight", "");
                cmd.Parameters.AddWithValue("@MotherHealthBloodgroup", "Other");
                cmd.Parameters.AddWithValue("@CWSN", "");
                
                cmd.Parameters.AddWithValue("@smsAcknowledgment", "");
                
                cmd.Parameters.AddWithValue("@emailAcknowledgment", "");

                cmd.Parameters.AddWithValue("@isfaSmsAck", "");
                cmd.Parameters.AddWithValue("@isfaEmailAck", 0);
                cmd.Parameters.AddWithValue("@ismoSmsAck", 0);
                cmd.Parameters.AddWithValue("@ismoEmailAck", 0);
                cmd.Parameters.AddWithValue("@isguaSmsAck", 0);
                cmd.Parameters.AddWithValue("@isguaEmailAck", 0);

                try
                {
                    _con.Open();
                    int x = cmd.ExecuteNonQuery();
                    _con.Close();
                    flag = true;
                    cmd.Parameters.Clear();
                }
                catch (SqlException)
                {
                    flag = false; _con.Close();
                }
                catch (Exception)
                {
                    flag = false; _con.Close();
                }
                return flag;
            }
        }

        public bool OfficialDetails()
        {
            var flag = true;
            string msg = "";
            _sql = "select max(Id) as Total from StudentOfficialDetails";
            var ss = _oo.ReturnTag(_sql, "Total");
            int ss1;
            try
            {
                ss1 = Convert.ToInt32(ss.Trim());
            }
            catch (Exception) { ss1 = 0; }
            ss1 +=  1;
            Application.Lock();
            Session["StEnRCode"] = IdGeneration(ss1.ToString());
            Application.UnLock();
            Application.Lock();
            Session["SrNo"] = (lblSrString.Text + txtSr.Text.Trim());
            Application.UnLock();
            var stEnrCode = Session["StEnRCode"].ToString();

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@StEnRCode", stEnrCode));
            parameter.Add(new SqlParameter("@SrNo", Session["SrNo"].ToString()));
            var admissionDate = TextBox100.Text.Trim();
            parameter.Add(new SqlParameter("@DateOfAdmiission", admissionDate));
            string _sql1 = "select Id from ClassMaster where ltrim(rtrim(ClassName))='" + DropAdmissionClass.SelectedItem.Text.Trim() + "'";
            _sql1 = _sql1 + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            var classId = _oo.ReturnTag(_sql1, "Id");
            parameter.Add(new SqlParameter("@AdmissionForClassId", classId));
            string _sql2 = "select Id from SectionMaster where ltrim(rtrim(SectionName))='" + drpSection.SelectedItem.Text.Trim() + "'";
            _sql2 = _sql2 + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + classId;
            var sectionId = _oo.ReturnTag(_sql2, "Id");
            parameter.Add(new SqlParameter("@SectionId", sectionId));
            parameter.Add(new SqlParameter("@GroupNa", DropBranch.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@FileNo", ""));
            parameter.Add(new SqlParameter("@Reference", ""));
            parameter.Add(new SqlParameter("@ShiftId", "0"));
            parameter.Add(new SqlParameter("@EducationActId", "0"));
            parameter.Add(new SqlParameter("@Remark", ""));
            parameter.Add(new SqlParameter("@Board", DrpBoard.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@TypeOFAdmision", DrpNEWOLSAdmission.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            parameter.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            parameter.Add(new SqlParameter("@SessionName", Session["SessionName"]));
            parameter.Add(new SqlParameter("@medium", drpMedium.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@Card", drpPanelCardType.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@CardId", drpPanelCardType.SelectedValue.ToString()));
            parameter.Add(new SqlParameter("@HostelRequired", "No"));
            parameter.Add(new SqlParameter("@TransportRequired", "No"));
            parameter.Add(new SqlParameter("@HouseName", DropDownList4.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@LibraryRequired", "No"));
            parameter.Add(new SqlParameter("@Enquiry", txtEnquiryNo.Text));
            parameter.Add(new SqlParameter("@BoardUniversityRollNo", ""));
            parameter.Add(new SqlParameter("@InstituteRollNo", ""));
            parameter.Add(new SqlParameter("@CardNo", ""));
            parameter.Add(new SqlParameter("@MachineNo", ""));
            parameter.Add(new SqlParameter("@Course", DropCourse.SelectedValue));
            parameter.Add(new SqlParameter("@Branch", DropBranch.SelectedValue));
            parameter.Add(new SqlParameter("@Scholarship", "No"));
            parameter.Add(new SqlParameter("@ModForHostel", "I"));
            parameter.Add(new SqlParameter("@ModForTransport", "I"));
            parameter.Add(new SqlParameter("@ModForLibrary", "I"));
            parameter.Add(new SqlParameter("@MODForFeeDeposit", "I"));
            parameter.Add(new SqlParameter("@SMSAcknowledgment", "Gaurdian 1"));
            parameter.Add(new SqlParameter("@EmailAcknowledgment", "Gaurdian 1"));
            parameter.Add(new SqlParameter("@AdmissionDoneAt", 0));
            if (DropStream.SelectedIndex != 0)
            {
                parameter.Add(new SqlParameter("@Streamid", DropStream.SelectedValue));
            }
            if (TextBox67.Text.Trim() != "")
            {
                parameter.Add(new SqlParameter("@RecieptNo", TextBox67.Text.Trim()));
            }
            parameter.Add(new SqlParameter("@TypeofEducation", "R"));
            SqlParameter paramsg = new SqlParameter("@MSG", "");
            paramsg.Direction = ParameterDirection.Output;
            paramsg.Size = 0x1000;
            parameter.Add(paramsg);
            try
            {
                msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOfficialDetailsProc", parameter);
                string RValue = msg.ToString();
                _sql = "select BranchCode from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + " and SrNoType<>'Manual'";
                if (_oo.Duplicate(_sql))
                {
                    string[] data = RValue.Split(new string[] { "##" }, StringSplitOptions.None);
                    if (data[0].ToString() == "Already")
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate S.R.No.!", "A");
                    }
                    Session["StEnRCode"] = data[0].ToString();
                    Session["SrNo"] = data[1].ToString();
                }
            }
            catch (SqlException ex)
            {
                flag = false;
                _con.Close();
            }
            catch (Exception ex1)
            {
                flag = false;
                _con.Close();
            }
            return flag;
        }
        protected void loadByAdmission()
        {
            _sql = "select StudentName,MiddleName,LastName,FatherName,Class,Sex,FatherContactNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            txtFirstNa.Text = _oo.ReturnTag(_sql, "StudentName");
            txtfaNameee.Text = _oo.ReturnTag(_sql, "FatherName");
            txtcontfa.Text = _oo.ReturnTag(_sql, "FatherContactNo");

            string tt = "select id, Course from ClassMaster where ClassName='" + _oo.ReturnTag(_sql, "Class") + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            string classid = _oo.ReturnTag(tt, "id");
            string Courseid = _oo.ReturnTag(tt, "Course");

            DropCourse.SelectedValue = Courseid;

            string sql2 = "Select Id,ClassName from ClassMaster";
            sql2 = sql2 + " where (Course='" + DropCourse.SelectedValue + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and id=" + classid + " and CIDOrder !=0 ";
            _oo.FillDropDown_withValue(sql2, DropAdmissionClass, "ClassName", "Id");
            DropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));

            DropAdmissionClass.SelectedValue = classid;

            string ss = "select SectionName from SectionMaster where ClassNameId='" + classid + "'";
            ss +=  "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(ss, drpSection, "SectionName");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            DropDownList4.SelectedIndex = 1;
            drpPanelCardType.SelectedIndex = 1;


            if (_oo.ReturnTag(_sql, "Sex").Trim().ToUpper() == "Male".ToUpper())
            {
                RadioButtonList1.SelectedIndex = 0;
            }
            else if (_oo.ReturnTag(_sql, "Sex").Trim().ToUpper() == "Female".ToUpper())
            {
                RadioButtonList1.SelectedIndex = 1;
            }
            else if (_oo.ReturnTag(_sql, "Sex").Trim().ToUpper() == "Transgender".ToUpper())
            {
                RadioButtonList1.SelectedIndex = 2;
            }
            if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
            {
                RadioButtonList1.Enabled = false;
                txtfaNameee.Enabled = false;
                txtcontfa.Enabled = false;
                drpPanelCardType.Enabled = false;
                DropDownList4.Enabled = false;
                txtFirstNa.Enabled = false;
                DropCourse.Enabled = false;
                DropAdmissionClass.Enabled = false;
                DrpNEWOLSAdmission.Enabled = false;
            }
        }
        protected void TextBox67_TextChanged(object sender, EventArgs e)
        {
            if (TextBox67.Text.Trim() != "")
            {
                string sql = "select RecieptNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text.Trim() + "' and isnull(AdmissionStatus, 0)=1 and BranchCode=" + Session["BranchCode"] + "";
                string sq2 = "select RecieptNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(sql))
                {
                    _oo.msgbox(Page, msgbox, "This registration already completed!", "A");
                    TextBox67.Text = "";
                    return;
                }
                else if (!_oo.Duplicate(sq2))
                {
                    _oo.msgbox(Page, msgbox, "Invalid Receipt No.!", "A");
                    TextBox67.Text = "";
                    return;
                }
                else
                {
                    loadByAdmission();
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (TextBox67.Text.Trim() != "")
            {
                string sql = "select RecieptNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text.Trim() + "' and isnull(AdmissionStatus, 0)=1 and BranchCode=" + Session["BranchCode"] + "";
                string sq2 = "select RecieptNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(sql))
                {
                    _oo.msgbox(Page, msgbox, "This registration already completed!", "A");
                    TextBox67.Text = "";
                    return;
                }
                else if (!_oo.Duplicate(sq2))
                {
                    _oo.msgbox(Page, msgbox, "Invalid Receipt No.!", "A");
                    TextBox67.Text = "";
                    return;
                }
                else
                {
                    loadByAdmission();
                }
            }
        }
        public string IdGeneration(string x)
        {
            var xx = "";
            switch (x.Length)
            {
                case 1:
                    xx = "eAM00000" + x;
                    break;
                case 2:
                    xx = "eAM0000" + x;
                    break;
                case 3:
                    xx = "eAM000" + x;
                    break;
                case 4:
                    xx = "eAM00" + x;
                    break;
                case 5:
                    xx = xx + "eAM0" + x;
                    break;
                default:
                    xx = x;
                    break;
            }
            return xx;
        }
        public string IdGenerationCollege(string x)
        {
            var xx = "";
            switch (x.Length)
            {
                case 1:
                    xx = "eAM000000" + x;
                    break;
                case 2:
                    xx = "eAM00000" + x;
                    break;
                case 3:
                    xx = "eAM0000" + x;
                    break;
                case 4:
                    xx = "eAM000" + x;
                    break;
                case 5:
                    xx = xx + "eAM00" + x;
                    break;
                case 6:
                    xx = xx + "eAM0" + x;
                    break;
                default:
                    xx = x;
                    break;
            }
            return xx;
        }
        protected void DrpPreState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(DrpPreCity, DrpPreState);
        }
        private void LoadSection()
        {
            _sql = "select SectionName from SectionMaster where ClassNameId='" + DropAdmissionClass.SelectedValue + "'";
            _sql +=  "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, drpSection, "SectionName");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        protected void DropAdmissionClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
            LoadSection();
        }
        public bool Validation()
        {
            return true;
        }
        protected void LinkButton14_Click(object sender, EventArgs e)
        {

            _sql = "select * from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + "";
            if (!_oo.Duplicate(_sql))
            {
                _oo.msgbox(Page, msgbox, "Please initialize S.R. No.!", "A");
            }
            else
            {

                if (txtAadharNo.Text.Trim() != "")
                {
                    var sql = "Select count(*) AadharNoCount from StudentGenaralDetail where LTRIM(RTRIM(isnull(AadharNo, '')))='" + txtAadharNo.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";
                    string AadharNoCount = _oo.ReturnTag(sql, "AadharNoCount");
                    if (AadharNoCount != "0")
                    {
                        _sql = "Select top(1) * from setting";
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate " + _oo.ReturnTag(_sql, "IsAadhaar").ToString() + "!", "A");
                        return;
                    }
                }

                var sql1 = "Select SrNo from StudentOfficialDetails where SrNo='" + txtSr.Text.Trim() + "'";
                var sql2 = "Select SrNo from StudentGenaralDetail where SrNo='" + txtSr.Text.Trim() + "'";
                var sql3 = "Select SrNo from StudentFamilyDetails where SrNo='" + txtSr.Text.Trim() + "'";
                var _sql6 = "select BranchCode from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + " and SrNoType='Manual'";
                if ((_oo.Duplicate(sql1) || _oo.Duplicate(sql2) || _oo.Duplicate(sql3)))
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate S.R.No.!", "A");
                }
                else
                {
                    if (txtSr.Text.Trim() == string.Empty && _oo.Duplicate(_sql6))
                    {
                        txtSr.Focus();
                        txtSr.BorderColor = Color.Red;
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please enter S.R.No.!", "A");
                    }
                    else
                    {
                        try
                        {
                            txtSr.BorderColor = ColorTranslator.FromHtml("#D5D5D5");
                        }
                        catch
                        {
                        }
                        TextTrnsform();
                        //OfficialDetails();
                        //GeneralDetails();
                        //FamilyDetails();

                        if (OfficialDetails() == false || GeneralDetails() == false || FamilyDetails() == false)
                        {
                            _sql = "delete from StudentFamilyDetails where SrNo='" + Session["SrNo"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            _oo.ProcedureDatabase(_sql);
                            _sql = "delete from StudentGenaralDetail where SrNo='" + Session["SrNo"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            _oo.ProcedureDatabase(_sql);
                            _sql = "delete from StudentOfficialDetails where SrNo='" + Session["SrNo"] + "' and BranchCode=" + Session["BranchCode"] + "";
                            _oo.ProcedureDatabase(_sql);
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry record not inserted (Contact to Admin).", "W");
                        }
                        else
                        {
                            StudentPasswordGeneration();
                            GuardianPasswordGeneration();
                            try
                            {

                                _sql = "select top(1) MobileNumber from StudentGenaralDetail  where SrNo='" + Session["SrNo"].ToString().Trim() + "'  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                                var mobileNumber = _oo.ReturnTag(_sql, "MobileNumber");
                                _sql = "select top(1) FamilyContactNo from StudentFamilyDetails  where SrNo='" + Session["SrNo"].ToString().Trim() + "'  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                                var familyContactNo = _oo.ReturnTag(_sql, "FamilyContactNo");
                                SendFeeSms(mobileNumber, familyContactNo);
                            }
                            catch (Exception)
                            {
                            }
                            var srnoo = Session["SrNo"];
                            _oo.ClearControls(Page);
                            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../11/StudentRegView.aspx?id=" + srnoo + "','_self')", true);
                        }
                    }
                }
            }
        }
        public void SendFeeSms(string mobileno, string familyConNo)
        {
            if (mobileno != "")
            {
                SendFeesSmsStudent(mobileno, "2");
                SendFeesSmsGuardian(familyConNo, "3");
            }
            else
            {
                SendFeesSmsStudent(familyConNo, "2");
                SendFeesSmsGuardian(familyConNo, "3");
            }
        }
        public void SendFeesSmsStudent(string fmobileNo, string title)
        {
            try
            {
                _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "HitValue") != "")
                {
                    if (_oo.ReturnTag(_sql, "HitValue") == "true")
                    {
                        var sadpNew = new SMSAdapterNew();
                        var mess = "";
                        _sql = "Select top(1) FirstName as StudentName   from StudentGenaralDetail where  SessionName='" + Session["SessionName"] + "'  and BranchCode=" + Session["BranchCode"] + "";
                        _sql +=  "    and  Srno='" + Session["SrNo"].ToString().Trim() + "' order by id desc";
                        var studentName = _oo.ReturnTag(_sql, "StudentName");
                        _sql = "Select top(1) CollegeShortNa from CollegeMaster where BranchCode=" + Session["BranchCode"] + " ";
                        var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");
                        _sql = "Select top(1) UserName,Password StudentPassword from StudentLoginandPassword where";
                        _sql +=  " Srno='" + Session["SrNo"].ToString().Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                        var stEnRCode = _oo.ReturnTag(_sql, "UserName");
                        var studentPassword = _oo.ReturnTag(_sql, "StudentPassword");
                        mess = "Congrats! " + studentName + ", you've registered successfully with " + collegeShortNa + ". Your Username: " + stEnRCode + " and Password: " + studentPassword + " from ";
                        if (fmobileNo != "")
                        {
                            _sql = "Select SmsSent From SmsEmailMaster where Id='2'";
                            if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                            {
                                sadpNew.Send(mess, fmobileNo, title);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public void SendFeesSmsGuardian(string fmobileNo, string title)
        {
            try
            {


                _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "HitValue") != "")
                {
                    if (_oo.ReturnTag(_sql, "HitValue") == "true")
                    {
                        var sadpNew = new SMSAdapterNew();
                        var mess = "";
                        _sql = "Select top(1) FamilyGuardianName as GuardianName from StudentFamilyDetails";
                        _sql +=  " where Srno='" + Session["SrNo"].ToString().Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                        var guardianName = _oo.ReturnTag(_sql, "GuardianName");
                        _sql = "Select top(1) CollegeShortNa from CollegeMaster where BranchCode=" + Session["BranchCode"] + " ";
                        var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");
                        _sql = "Select top(1) UserName,Password GuardianPassword from GuardianLoginandPassword where";
                        _sql +=  " Srno='" + Session["SrNo"].ToString().Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                        var stEnRCode = _oo.ReturnTag(_sql, "UserName");
                        var guardianPassword = _oo.ReturnTag(_sql, "GuardianPassword");
                        mess = "Congrats! Mr./Ms. " + guardianName + ", your ward is registered successfully with " + collegeShortNa + ". Your Username: " + stEnRCode + " and Password: " + guardianPassword + " from ";
                        if (fmobileNo != "")
                        {
                            _sql = "Select SmsSent From SmsEmailMaster where Id='3'";
                            if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
                            { sadpNew.Send(mess, fmobileNo, title); }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        
        public void StudentPasswordGeneration()
        {
            var password = Campus.GenerateRandomNo(8);
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "LoginTabProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Srno", Session["SrNo"].ToString());
                cmd.Parameters.AddWithValue("@UserName", Session["SrNo"].ToString());
                cmd.Parameters.AddWithValue("@Pass", password);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginTypeId", "4");

                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();

                    SendNotificationToStudent(txtSr.Text.Trim(), txtSr.Text.Trim(), password);
                }
                catch (SqlException) { _con.Close(); }
                catch (Exception) { _con.Close(); }
            }
        }


        public void GuardianPasswordGeneration()
        {
            var password = Campus.GenerateRandomNo(8);
            var uid = "G" + Session["SrNo"].ToString();
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "LoginTabProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Srno", Session["SrNo"].ToString());
                cmd.Parameters.AddWithValue("@UserName", uid.Trim());
                cmd.Parameters.AddWithValue("@Pass", password);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginTypeId", "5");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    SendNotificationToGurdian(txtSr.Text.Trim(), uid, password);
                }
                catch (SqlException) { _con.Close(); }
                catch (Exception) { _con.Close(); }
            }
        }
        public void SendNotificationToStudent(string srno, string uid, string password)
        {
            var mess = "Student Login Panel";
            mess = mess + "<br>";
            mess = mess + "</hr>";
            mess = mess + "<br>";
            mess = mess + "Dear ";
            mess = mess + "             " + txtFirstNa.Text +",";
            mess = mess + "<br></hr>";
            mess = mess + "Your Username  :" + uid;
            mess = mess + "<Br>";
            mess = mess + "Your Password :" + password;
            _sql = "select email  from StudentGenaralDetail where srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            try
            {
                _oo.EmailSendingForUser(mess, "SE Education: Student Login Panel Credentials", _oo.ReturnTag(_sql, "Email"));
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void SendNotificationToGurdian(string srno, string uid, string password)
        {
            var mess = "Guardian Login Panel";
            mess = mess + "<br>";
            mess = mess + "</hr>";
            mess = mess + "<br>";
            mess = mess + "Dear ";
            mess = mess + "             " + txtfaNameee.Text + ",";
            mess = mess + "<br></hr>";
            mess = mess + "Your Username  :" + uid;
            mess = mess + "<Br>";
            mess = mess + "Your Password :" + password;
            _sql = "select FamilyEmail  from StudentFamilyDetails where srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            try
            {
                _oo.EmailSendingForUser(mess, "SE Education: Guardian Login Panel Credentials", _oo.ReturnTag(_sql, "FamilyEmail"));
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void PermissionGrant(int add1, LinkButton ladd)
        {
            ladd.Enabled = add1 == 1;
        }
        public void CheckValueAddDeleteUpdate()
        {
            try
            {
                _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
                _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
                var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
                // ReSharper disable once RedundantCast
                PermissionGrant(a, (LinkButton)LinkButton14);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected void DrpDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.Focus();
        }
        
        #endregion

        private void LoadCourse()
        {
            _sql = "Select CourseName,Id from CourseMaster where  BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, DropCourse, "CourseName", "Id");
            DropCourse.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

        private void LoadBranch()
        {
            _sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DropAdmissionClass.SelectedValue + "";
            _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
            DropBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

        private void LoadStream()
        {
            string tt = "select id from classmaster where classname='" + DropAdmissionClass.SelectedItem.Text + "'";
            string ss = _oo.ReturnTag(tt, "id");
            _sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + ss + "' and BranchId='" + DropBranch.SelectedValue + "'";
            _oo.FillDropDown_withValue_withSelect(_sql, DropStream, "Stream", "Id");
        }
        
        private void LoadClass()
        {

            if (Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
            {

                _sql = "Select Id,ClassName from ClassMaster";
                _sql +=  " where (Course='" + DropCourse.SelectedValue + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 ";
                _oo.FillDropDown_withValue(_sql, DropAdmissionClass, "ClassName", "Id");
                DropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
            else
            {
                _sql = "Select Id,ClassName from ClassMaster";
                _sql +=  " where (Course='" + DropCourse.SelectedValue + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 ";
                _oo.FillDropDown_withValue(_sql, DropAdmissionClass, "ClassName", "Id");
                DropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }

        }
        protected void DropCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClass();
        }
        #region WebMethod
        [WebMethod]
        public static List<string> GetAgeofStudent(string date1, string date2)
        {
            var datepart = new List<string>();
            if (date2 == "NaN/NaN/NaN")
            {
                date2 = date1;
            }
            if (date1 == "NaN/NaN/NaN")
            {
                date1 = date2;
            }
            if (date1 != "NaN/NaN/NaN" & date2 != "NaN/NaN/NaN")
            {


                // ReSharper disable once RedundantAssignment
                var con = new SqlConnection();
                con = BAL.objBal.dbGet_connection();
                var dt = new DataTable();
                SqlDataAdapter da;
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = "AgeCalculaterProc";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DOB", date2);
                    cmd.Parameters.AddWithValue("@DOB1", date1);
                    da = new SqlDataAdapter
                    {
                        SelectCommand = cmd
                    };
                }
                da.Fill(dt);
                // ReSharper disable once UseObjectOrCollectionInitializer

                datepart.Add(dt.Rows[0][0].ToString());
                datepart.Add(dt.Rows[0][1].ToString());
                datepart.Add(dt.Rows[0][2].ToString());
            }
            else
            {
                datepart.Add("0 YEAR");
                datepart.Add("0 MONTH");
                datepart.Add("0 DAY");
            }
            return datepart;
        }
        #endregion
        
        protected void lnkQuickReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentQuickRegistration.aspx?check=student_registration");
        }
        protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }
        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
        }
        
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }



        protected void txtSr_TextChanged(object sender, EventArgs e)
        {
            txtSr.Text = txtSr.Text.Replace(" ", "");
        }
    }
}