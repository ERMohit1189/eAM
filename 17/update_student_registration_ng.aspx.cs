using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _11
{
    public partial class update_student_registration_ng : Page
    {
        private SqlConnection _con;
        readonly Campus _oo;
        string _sql = "";
        private readonly BAL.Set_DocumentName _obj = new BAL.Set_DocumentName();
        private readonly DLL _dllobj = new DLL();

        private DataTable _dt = new DataTable();

        public update_student_registration_ng()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
            // ReSharper disable once PossibleNullReferenceException
            switch (Session["Logintype"].ToString())
            {
                case "Admin":
                    MasterPageFile = "~/Master/admin_root-manager.master";
                    break;
                case "Staff":
                    MasterPageFile = "~/Staff/staff_root-manager.master";
                    break;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            var getdate = DateTime.Today.ToString("dd-MMM-yyyy");
            txtAgeOnDate.Text = getdate;
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                try
                {
                    CheckValueADDDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }

                {
                    loadEntranceExamName();
                    GeneralDetailDropDown();
                    FamilyDetailsDropDown();
                    PreviousSchoolDropDown();
                    OfficialDetailDropDown();
                    Panel2.Visible = false;
                }
                CheckTextTrnsformation();
                DropBranch.Enabled = false;

                if (Session["Logintype"].ToString() == "Staff")
                {
                    div1.Visible = true;
                    div2.Visible = false;
                    DropCourse.Enabled = false;
                    DropAdmissionClass.Enabled = false;
                    DropBranch.Enabled = false;
                    DropSection.Enabled = false;
                    drpMedium.Enabled = false;
                    DrpNEWOLSAdmission.Enabled = false;
                    rbHostel.Enabled = false;
                    rbLibrary.Enabled = false;
                    rbTransport.Enabled = false;
                    rbScholarship.Enabled = false;
                    drpFeeDepositMOD.Enabled = false;

                    loadClass(drpClassforStaff);
                    BAL.objBal.fillSelectvalue(drpSectionforStaff, "<--Select-->", "-1");
                    BAL.objBal.fillSelectvalue(drpBranchforStaff, "<--Select-->", "-1");
                    BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
                }
                else
                {
                    div2.Visible = true;
                    div1.Visible = false;
                    DropCourse.Enabled = false;
                    DropAdmissionClass.Enabled = false;
                    DropBranch.Enabled = false;
                    DropSection.Enabled = false;
                    drpMedium.Enabled = false;
                    DrpNEWOLSAdmission.Enabled = true;
                    rbHostel.Enabled = true;
                    rbLibrary.Enabled = true;
                    rbTransport.Enabled = true;
                    rbScholarship.Enabled = true;
                    drpFeeDepositMOD.Enabled = true;
                }
            }    
        }

        private void LoaddefaultState(DropDownList drp)
        {
            _sql = "Select StateName,Id from StateMaster";
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

        private void LoaddefaultCity(DropDownList drp, DropDownList drpValue)
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

        private void LoadState(DropDownList drp)
        {
            _sql = "Select StateName,Id from StateMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
        }

        private void LoadCity(DropDownList drp, DropDownList drpValue)
        {
            _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
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

        protected void LinkButton15_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentPreview.aspx");
        }

        protected void DrpPreCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPreCountry.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, DrpPreState, "StateName", "id");
            if (DrpPreState.Items.Count == 0)
            {
                DrpPreState.Items.Add(new ListItem("Other", "Other"));
                DrpPreCity.Items.Clear();
                DrpPreCity.Items.Add(new ListItem("Other", "Other"));
            }
            else
            {
                LoadCity(DrpPreCity, DrpPreState);
            }
        }

        protected void DrpPerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPerCountry.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, DrpPerState, "StateName", "id");
            if (DrpPerState.Items.Count == 0)
            {
                DrpPerState.Items.Add(new ListItem("Other", "Other"));
                DrpPerCity.Items.Clear();
                DrpPerCity.Items.Add(new ListItem("Other", "Other"));
            }
            else
            {
                LoadCity(DrpPerCity, DrpPerState);
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            LoadState(drpG1State);
            LoadCity(drpG1City, DrpPreState);
            try
            {
                // ReSharper disable once RedundantBoolCompare
                if (CheckBox2.Items[0].Selected == true)
                {
                    txtG1Address.Text = txtPreaddress.Text;
                    drpG1State.SelectedValue = DrpPreState.SelectedValue;
                    drpG1City.SelectedValue = DrpPreCity.SelectedValue;
                    txtG1Pin.Text = txtPreZip.Text;
                    txtG1MobileNo.Text = txtPreMobileNo.Text;
                    txtG1PhoneNo.Text = txtPrePhoneNo.Text;
                }
                else
                {
                    txtG1Address.Text = "";
                    txtG1Pin.Text = "";
                    txtG1MobileNo.Text = "";
                    txtG1PhoneNo.Text = "";
                    LoaddefaultState(drpG1State);
                    LoaddefaultCity(drpG1City, drpG1State);
                }
            }
            catch (Exception ex)
            {
                BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox2);
            }
        }

        protected void drpG1Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + drpG1Country.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpG1State, "StateName", "id");
            if (drpG1State.Items.Count == 0)
            {
                drpG1State.Items.Add(new ListItem("Other", "Other"));
                drpG1City.Items.Clear();
                drpG1City.Items.Add(new ListItem("Other", "Other"));
            }
            else
            {
                LoadCity(drpG1City, drpG1State);
            }
        }
        protected void drpG2Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + drpG2Country.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpG2State, "StateName", "id");
            if (drpG2State.Items.Count == 0)
            {
                drpG2State.Items.Add(new ListItem("Other", "Other"));
                drpG2City.Items.Clear();
                drpG2City.Items.Add(new ListItem("Other", "Other"));
            }
            else
            {
                LoadCity(drpG2City, drpG2State);
            }
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            LoadState(drpG2State);
            LoadCity(drpG2City, DrpPreState);
            try
            {
                // ReSharper disable once RedundantBoolCompare
                if (CheckBox3.Items[0].Selected == true)
                {
                    txtG2Address.Text = txtPreaddress.Text;
                    drpG2State.SelectedValue = DrpPreState.SelectedValue;
                    drpG2City.SelectedValue = DrpPreCity.SelectedValue;
                    txtG2Pin.Text = txtPreZip.Text;
                    txtG2MobileNo.Text = txtPreMobileNo.Text;
                    txtG2PhoneNo.Text = txtPrePhoneNo.Text;
                }
                else
                {
                    txtG2Address.Text = "";
                    txtG2Pin.Text = "";
                    txtG2MobileNo.Text = "";
                    txtG2PhoneNo.Text = "";
                    LoaddefaultState(drpG2State);
                    LoaddefaultCity(drpG2City, drpG2State);
                }
            }
            catch (Exception ex)
            {
                BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox3);
            }
        }

        protected void drpOccupationmoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ss = drpOccupationmoth.SelectedItem.ToString().ToUpper();
            var wf = "HOUSE WIFE";
            if (ss.Trim() == wf.Trim())
            {
                txtdesmoth.Text = "N/A";
                txtdesmoth.ReadOnly = true;
                txtofficeaddmother.Text = "N/A";
                txtofficeaddmother.ReadOnly = true;
            }
            else
            {
                txtdesmoth.Text = "";
                txtdesmoth.ReadOnly = false;
                txtofficeaddmother.Text = "";
                txtofficeaddmother.ReadOnly = false;
            }
        }

        #region
        protected void CheckTextTrnsformation()
        {
            object value;
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@isDo", "Select"));
            param.Add(new SqlParameter("@value", ""));
            param.Add(new SqlParameter("@SessionName", ""));
            param.Add(new SqlParameter("@LoginName", ""));
            var para = new SqlParameter("@Msg", "")
            {
                Direction = ParameterDirection.Output,
                Size = 0x10
            };
            param.Add(para);
            value = _dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
            if (value != DBNull.Value)
            {
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

                    if (childControl is TextBox)
                    {
                        if (((TextBox)childControl).ID != "txtSr")
                        {
                            ((TextBox)childControl).Style.Add("text-transform", istransform);
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
        private void LoadDefaultMedium()
        {
            _sql = "Select Medium,id from MediumMaster where SessionName='" + Session["SessionName"] + "'";
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
        private void LoadBloodGroup()
        {
            _sql = "Select BloodGroupName,BloodGroupId from BloodGroupMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drpBloodGroup, "BloodGroupName", "BloodGroupId");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Blood Group", drpBloodGroup);
                }
                catch
                {
                    // ignored
                }
            }
        }

        public void GeneralDetailDropDown()
        {
            LoaddefaultCountry(DrpPerCountry); LoaddefaultCountry(DrpPreCountry);
            LoaddefaultState(DrpPerState); LoaddefaultState(DrpPreState);
            LoaddefaultCity(DrpPerCity, DrpPerState); LoaddefaultCity(DrpPreCity, DrpPreState);
            LoadDefaultCasteName();
            LoadDefaultMotherTongue();
            LoadBloodGroup();
            LoadDefaultNationality();
            LoadDefaultFeeGroup();
            LoadDefaultHouse();
            LoadDefaultCast();
            LoadDefaultHomeTown();
            LoadDefaultHomeTown();
            LoadDefaultMotherOccu();
            LoadDefaultFatherOccu();
        }
        private void LoadDefaultNationality()
        {
            _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Nationality'";
            txtMotherNationality.Text = txtFatherNationality.Text = TextBox65.Text = _oo.ReturnTag(_sql, "defaultvalue");
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
        private void LoadDefaultHomeTown()
        {
            _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='HomeTown'";
            txtHomeTown.Text = _oo.ReturnTag(_sql, "defaultvalue");
        }

        private void LoadDefaultMotherTongue()
        {
            _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='MotherTongue'";
            txtMotherTongue.Text = _oo.ReturnTag(_sql, "defaultvalue");
        }

        private void LoadDefaultCast()
        {
            _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Caste'";
            TextBox66.Text = _oo.ReturnTag(_sql, "defaultvalue");
        }
        private void LoadDefaultHouse()
        {
            _sql = "select HouseName from HouseMaster where SessionName='" + Session["SessionName"] + "'";
            _oo.FillDropDownWithOutSelect(_sql, DropDownList4, "HouseName");
            DropDownList4.Items.Insert(0, new ListItem("<--Select House Name-->", ""));
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("House", DropDownList4);
                }
                catch
                {
                    // ignored
                }
            }
        }
        private void LoadDefaultFeeGroup()
        {
            _sql = "Select FeeGroupName from FeeGroupMaster ";
            _sql +=  " where  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDownWithOutSelect(_sql, drpPanelCardType, "FeeGroupName");
            drpPanelCardType.Items.Insert(0, new ListItem("<--Select Fee Category-->", ""));
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
        private void LoadDefaultCasteName()
        {
            _sql = "select CasteName,CasteId from CasteMaster";
            _oo.FillDropDown_withValue(_sql, DropDownList2, "CasteName", "CasteId");
            DropDownList2.Items.Insert(0, new ListItem("<--Select Category-->", ""));
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
        private void LoadDefaultBoard()
        {
            _sql = "Select BoardName,id from BoardMaster";
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

        public void PreviousSchoolDropDown()
        {
            loadCourse();
            loadClass();
            loadBranch();
            loadSection();
            LoadDefaultMedium();
            LoadDefaultBoard();
            LoadDefaultReligion();
        }
        public void OfficialDetailDropDown()
        {
            loadCourse();
            loadClass();
            loadBranch();
            loadStream();
            loadSection();
            LoadDefaultMedium();
            LoadDefaultBoard();
        }
        public void FamilyDetailsDropDown()
        {
            LoaddefaultCountry(drpG1Country); LoaddefaultCountry(drpG2Country);
            LoaddefaultState(drpG1State); LoaddefaultState(drpG2State);
            LoaddefaultCity(drpG1City, drpG1State); LoaddefaultCity(drpG2City, drpG2State);
            DrpRelationship.SelectedIndex = 1;
            drpGuardiantwoRelationship.SelectedIndex = 2;
            txtincomefa.Text = "0";
            txtincomemonthlymother.Text = "0";
        }
        protected void Get_DocumentName()
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId) )
            {
                studentId = TxtEnter.Text.Trim() == string.Empty ? drpSrno.SelectedValue : TxtEnter.Text.Trim();
            }
            _obj.Srno = studentId;
            //obj.Session = Session["SessionName"].ToString();
            _obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
            //obj.ClassId = Convert.ToInt16(DropAdmissionClass.SelectedValue.ToString());
            _obj.Id = 0;
            var dv = new DataView(new DAL().Get_DocumentName(_obj).Item1);
            dv.Sort = "Id Asc";
            Repeater1.DataSource = dv;
            Repeater1.DataBind();
        }


        private void LoadStudentOtherDetails()
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId) )
            {
                studentId = TxtEnter.Text.Trim() == string.Empty ? drpSrno.SelectedValue : TxtEnter.Text.Trim();
            }

            _sql = @"Select DurationofCourse,RegistrationNo,CCNo,DATEPART(DAY,CCIssueDate) as CCD,Left(DateName(MONTH,CCIssueDate),3) as CCM,DATEPART(YEAR,CCIssueDate) as CCY ,
                ICNo,DATEPART(DAY,ICIssueDate) as ICD,Left(DateName(MONTH,ICIssueDate),3) as ICM,DATEPART(YEAR,ICIssueDate) as ICY ,
                RCNo,DATEPART(DAY,RCIssueDate) as RCD,Left(DateName(MONTH,RCIssueDate),3) as RCM,DATEPART(YEAR,RCIssueDate) as RCY ,
                DATEPART(DAY,FirstYearDateofAdmission) as FYDAD,Left(DateName(MONTH,FirstYearDateofAdmission),3) as FYDAM,DATEPART(YEAR,FirstYearDateofAdmission) as FYDAY ,
                DATEPART(DAY,CurrentYearDateofAdmission) as CYDAD,Left(DateName(MONTH,CurrentYearDateofAdmission),3) as CYDAM,DATEPART(YEAR,CurrentYearDateofAdmission) as CYDAY ,
                CourseType,AdmissionType,BankAccNo,BankName,BranchNameofBank,IFSCode,StudentNameinPassbook,DayScholarorHostalar,YearlyNoneRefundebleFee,HandyCapType ,
                HandyCapPercentage,HandyCapCompensation,RiciptNoofDepositFee ,
                DATEPART(DAY,DepositFeeDate) as DFDD,Left(DateName(MONTH,DepositFeeDate),3) as DFDM,DATEPART(YEAR,DepositFeeDate) as DFDY ,
                LastYearScholarshipAmount,LastYearScholarshipDepositFee,LastYearClassorCourse,LastYearExamResult,LastYearExamTotalMarks,LastYearExamTotalObtainMarks,ScholarshipCompensation ,
                NameofInstitute,isBasedonIntermediateMarks,TotalMarksinIntermediate,obtainedMarksinIntermediate,StudentAdharNo ,
                TransferCertificateNo,DATEPART(DAY,TransferCertificateDate) as TCDD,Left(DateName(MONTH,TransferCertificateDate),3) as TCDM,DATEPART(YEAR,TransferCertificateDate) as TCDY ,
                LastSchoolorCollegeName,IdentityProof,IntermediateRollNo,IntermediateBoard,IntermediateYearofPssing,UploadPhotoPath ,UploadStudentSignature,UploadParentsSignature  ";
            _sql +=  " from StudentOtherDetails where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "' and SessionName='" + Session["SessionName"] + "'";

            txtDuration.Text = _oo.ReturnTag(_sql, "DurationofCourse");
            txtRegistration.Text = _oo.ReturnTag(_sql, "RegistrationNo");
            txtCastCerti.Text = _oo.ReturnTag(_sql, "CCNo");
            txtIncomeCerti.Text = _oo.ReturnTag(_sql, "ICNo");
            txtRegiCer.Text = _oo.ReturnTag(_sql, "RCNo");
            txtTypeofCourse.Text = _oo.ReturnTag(_sql, "CourseType");
            txtTypeofAdmission.Text = _oo.ReturnTag(_sql, "AdmissionType");
            txtBankAccNo.Text = _oo.ReturnTag(_sql, "BankAccNo");
            txtBankName.Text = _oo.ReturnTag(_sql, "BankName");
            txtBranchNameofBank.Text = _oo.ReturnTag(_sql, "BranchNameofBank");
            txtIfsCode.Text = _oo.ReturnTag(_sql, "IFSCode");
            txtStudentNameinPassbook.Text = _oo.ReturnTag(_sql, "StudentNameinPassbook");
            txtDayScholer.Text = _oo.ReturnTag(_sql, "DayScholarorHostalar");
            txtYearlynonrefund.Text = _oo.ReturnTag(_sql, "YearlyNoneRefundebleFee");
            txthandycaptype.Text = _oo.ReturnTag(_sql, "HandyCapType");
            txthandycapPer.Text = _oo.ReturnTag(_sql, "HandyCapPercentage");
            txthandycapCompe.Text = _oo.ReturnTag(_sql, "HandyCapCompensation");
            txtReciptNoofDepositFee.Text = _oo.ReturnTag(_sql, "RiciptNoofDepositFee");
            txtLastYearScholarAmount.Text = _oo.ReturnTag(_sql, "LastYearScholarshipAmount");
            txtLastYearScholarDepoFee.Text = _oo.ReturnTag(_sql, "LastYearScholarshipDepositFee");
            txtLastClass.Text = _oo.ReturnTag(_sql, "LastYearClassorCourse");
            txtLastYearExamResult.Text = _oo.ReturnTag(_sql, "LastYearExamResult");
            txtLastYearExamTatalMarks.Text = _oo.ReturnTag(_sql, "LastYearExamTotalMarks");
            txtLastYearExamTotalObtainMarks.Text = _oo.ReturnTag(_sql, "LastYearExamTotalObtainMarks");
            txtScholarCompeAmountAccotoClass.Text = _oo.ReturnTag(_sql, "ScholarshipCompensation");
            txtNameofInstitute.Text = _oo.ReturnTag(_sql, "NameofInstitute");
            txtIsEntrybasedonInterMarksScore.Text = _oo.ReturnTag(_sql, "isBasedonIntermediateMarks");
            txtTotalMarksinInter.Text = _oo.ReturnTag(_sql, "TotalMarksinIntermediate");
            txtTotalobtainedMarksinInter.Text = _oo.ReturnTag(_sql, "obtainedMarksinIntermediate");
            txtStudentAdharNo.Text = _oo.ReturnTag(_sql, "StudentAdharNo");
            txtTransferCertiNo.Text = _oo.ReturnTag(_sql, "TransferCertificateNo");
            txtLastSchoolCollegeName.Text = _oo.ReturnTag(_sql, "LastSchoolorCollegeName");
            txtIdentityProof.Text = _oo.ReturnTag(_sql, "IdentityProof");
            txtIntermediateRollNo.Text = _oo.ReturnTag(_sql, "IntermediateRollNo");
            txtIntermediateBoard.Text = _oo.ReturnTag(_sql, "IntermediateBoard");
            txtIntermediateYearofPssing.Text = _oo.ReturnTag(_sql, "IntermediateYearofPssing");
        }

        public void NewDocumentsDetails()
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }
            var msg = "";
            try
            {
                var obj = new BAL.Set_StudentDocumentRecord();
                for (var i = 0; i < Repeater1.Items.Count; i++)
                {
                    var unused = (FileUpload)Repeater1.Items[i].FindControl("FileUpload4");
                    var lblId = (Label)Repeater1.Items[i].FindControl("lblId");
                    var lblDocument = (Label)Repeater1.Items[i].FindControl("lblDocument");
                    var chksoft = (CheckBox)Repeater1.Items[i].FindControl("Chksoft");
                    var chkhard = (CheckBox)Repeater1.Items[i].FindControl("Chkhard");
                    var chkVerified = (CheckBox)Repeater1.Items[i].FindControl("chkVerified");
                    var txtRemark = (TextBox)Repeater1.Items[i].FindControl("txtRemark");
                    var hfFile = (HiddenField)Repeater1.Items[i].FindControl("hfFile");
                    var hdfilefileExtention = (HiddenField)Repeater1.Items[i].FindControl("hdfilefileExtention");

                    //if (FileUpload4.HasFile)
                    //{
                    //    string filePath = @"../Uploads/Docs/";
                    //    string fileExtention = Path.GetExtension(FileUpload4.PostedFile.FileName);
                    //    string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
                    //    string fileName = TxtEnter.Text.Trim().Replace('/', '_') + '_' + lblDocument.Text.Trim() + '_' + datetime + fileExtention;
                    //    FileUpload4.SaveAs(Server.MapPath(filePath + fileName));
                    //    obj.DocName = fileName;
                    //    obj.DocPath = filePath + fileName;

                    //}
                    //else
                    //{
                    //    obj.DocName = null;
                    //    obj.DocPath = null;
                    //}
                    
                    var base64Std = hfFile.Value;
                    var fileExtention = hdfilefileExtention.Value;

                    if (base64Std != string.Empty)
                    {
                        var filePath = @"../Uploads/Docs/";
                        var fileName = studentId.Replace('/', '-') + '_' + lblDocument.Text.Trim() + fileExtention;
                        using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                var data = Convert.FromBase64String(base64Std);
                                bw.Write(data);
                                bw.Close();
                            }
                        }
                        obj.DocName = fileName;
                        obj.DocPath = filePath + fileName;
                    }
                    else
                    {
                        obj.DocName = "";
                        obj.DocPath = "";
                    }

                    obj.DocId = lblId.Text.Trim();
                    studentId = Request.Form[hfStudentId.UniqueID];
                    if (string.IsNullOrEmpty(studentId))
                    {
                        studentId = TxtEnter.Text.Trim();
                    }
                    obj.SrNo = studentId.Trim();
                    obj.StEnRCode = Session["StEnRCode"].ToString();
                    obj.Session = Session["SessionName"].ToString();
                    obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                    obj.LoginName = Session["LoginName"].ToString();

                    obj.Softcopy = chksoft.Checked? 1 : FileUpload1.HasFile ? 1 : 0;
                    obj.Hardcopy = chkhard.Checked ? 1 : 0;
                    obj.Varified = chkVerified.Checked ? 1 : 0;
                    obj.Remark = txtRemark.Text;
                    msg = new DAL().Set_StudentDocumentRecord(obj);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //oo.MessageBoxforUpdatePanel(msg + " (Document Upload)", ctrl);
            }
            if (msg != string.Empty)
            {
                //oo.MessageBoxforUpdatePanel(msg+" (Document Upload)", ctrl);
            }
        }

        protected void SaveStudentOtherDetails()
        {
            if (rbScholarship.SelectedIndex == 0)
            {
                var studentPhotoDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "StudentPhoto" + "_" + Session["SessionName"] + "/"));
                var studentPhotoVertualPath = "~/Uploads/Scholarship/" + "StudentPhoto" + "_" + Session["SessionName"] + "/";
                if (!Directory.Exists(studentPhotoDirectoryPath))
                {
                    Directory.CreateDirectory(studentPhotoDirectoryPath);
                }
                var studentSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "studentSignature" + "_" + Session["SessionName"] + "/"));
                var studentSignatureVertualPath = "~/Uploads/Scholarship/" + "studentSignature" + "_" + Session["SessionName"] + "/";
                if (!Directory.Exists(studentSignatureDirectoryPath))
                {
                    Directory.CreateDirectory(studentSignatureDirectoryPath);
                }
                var parentSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "parentSignature" + "_" + Session["SessionName"] + "/"));
                var parentSignatureVertualPath = "~/Uploads/Scholarship/" + "parentSignature" + "_" + Session["SessionName"] + "/";
                if (!Directory.Exists(parentSignatureDirectoryPath))
                {
                    Directory.CreateDirectory(parentSignatureDirectoryPath);
                }
                var param = new List<SqlParameter>();

                param.Add(new SqlParameter("@srno", Session["srno"].ToString()));
                param.Add(new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()));
                param.Add(new SqlParameter("@DurationofCourse", txtDuration.Text.Trim()));
                param.Add(new SqlParameter("@RegistrationNo", txtRegistration.Text.Trim()));
                param.Add(new SqlParameter("@CCNo", txtCastCerti.Text.Trim()));
                param.Add(new SqlParameter("@CCIssueDate", ""));
                param.Add(new SqlParameter("@ICNo", txtIncomeCerti.Text.Trim()));
                param.Add(new SqlParameter("@ICIssueDate", ""));
                param.Add(new SqlParameter("@RCNo", txtRegiCer.Text.Trim()));
                param.Add(new SqlParameter("@RCIssueDate", ""));
                param.Add(new SqlParameter("@FirstYearDateofAdmission", ""));
                param.Add(new SqlParameter("@CurrentYearDateofAdmission", ""));
                param.Add(new SqlParameter("@CourseType", txtTypeofCourse.Text.Trim()));
                param.Add(new SqlParameter("@AdmissionType", txtTypeofAdmission.Text.Trim()));
                param.Add(new SqlParameter("@BankAccNo", txtBankAccNo.Text.Trim()));
                param.Add(new SqlParameter("@BankName", txtBankName.Text.Trim()));
                param.Add(new SqlParameter("@BranchNameofBank", txtBranchNameofBank.Text.Trim()));
                param.Add(new SqlParameter("@IFSCode", txtIfsCode.Text.Trim()));
                param.Add(new SqlParameter("@StudentNameinPassbook", txtStudentNameinPassbook.Text.Trim()));
                param.Add(new SqlParameter("@DayScholarorHostalar", txtDayScholer.Text.Trim()));
                param.Add(new SqlParameter("@YearlyNoneRefundebleFee", txtYearlynonrefund.Text.Trim()));
                param.Add(new SqlParameter("@HandyCapType", txthandycaptype.Text.Trim()));
                param.Add(new SqlParameter("@HandyCapPercentage", txthandycapPer.Text.Trim()));
                param.Add(new SqlParameter("@HandyCapCompensation", txthandycapCompe.Text.Trim()));
                param.Add(new SqlParameter("@RiciptNoofDepositFee", txtReciptNoofDepositFee.Text.Trim()));
                param.Add(new SqlParameter("@DepositFeeDate", ""));
                param.Add(new SqlParameter("@LastYearScholarshipAmount", txtLastYearScholarAmount.Text.Trim()));
                param.Add(new SqlParameter("@LastYearScholarshipDepositFee", txtLastYearScholarDepoFee.Text.Trim()));
                param.Add(new SqlParameter("@LastYearClassorCourse", txtLastClass.Text.Trim()));
                param.Add(new SqlParameter("@LastYearExamResult", txtLastYearExamResult.Text.Trim()));
                param.Add(new SqlParameter("@LastYearExamTotalMarks", txtLastYearExamTatalMarks.Text.Trim()));
                param.Add(new SqlParameter("@LastYearExamTotalObtainMarks", txtLastYearExamTotalObtainMarks.Text.Trim()));
                param.Add(new SqlParameter("@ScholarshipCompensation", txtScholarCompeAmountAccotoClass.Text.Trim()));
                param.Add(new SqlParameter("@NameofInstitute", txtNameofInstitute.Text.Trim()));
                param.Add(new SqlParameter("@isBasedonIntermediateMarks", txtIsEntrybasedonInterMarksScore.Text.Trim()));
                param.Add(new SqlParameter("@TotalMarksinIntermediate", txtTotalMarksinInter.Text.Trim()));
                param.Add(new SqlParameter("@obtainedMarksinIntermediate", txtTotalobtainedMarksinInter.Text.Trim()));
                param.Add(new SqlParameter("@StudentAdharNo", txtStudentAdharNo.Text.Trim()));
                param.Add(new SqlParameter("@TransferCertificateNo", txtTransferCertiNo.Text.Trim()));
                param.Add(new SqlParameter("@TransferCertificateDate", ""));
                param.Add(new SqlParameter("@LastSchoolorCollegeName", txtLastSchoolCollegeName.Text.Trim()));
                param.Add(new SqlParameter("@IdentityProof", txtIdentityProof.Text.Trim()));
                param.Add(new SqlParameter("@IntermediateRollNo", txtIntermediateRollNo.Text.Trim()));
                param.Add(new SqlParameter("@IntermediateBoard", txtIntermediateBoard.Text.Trim()));
                param.Add(new SqlParameter("@IntermediateYearofPssing", txtIntermediateYearofPssing.Text.Trim()));
                var photoext = "";
                if (fpUploadPhoto.HasFile)
                {
                    photoext = Path.GetExtension(fpUploadPhoto.FileName);
                    fpUploadPhoto.SaveAs(studentPhotoDirectoryPath + Session["srno"].ToString().Replace("/", "_") + photoext);
                }
                param.Add(new SqlParameter("@UploadPhotoPath", fpUploadPhoto.HasFile ? studentPhotoVertualPath + Session["srno"].ToString().Replace("/", "_") + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@PhotoName", fpUploadPhoto.HasFile ? Session["srno"].ToString().Replace("/", "_") + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                var signext = "";
                if (fuUploadStudentSignature.HasFile)
                {
                    signext = Path.GetExtension(fuUploadStudentSignature.FileName);
                    fuUploadStudentSignature.SaveAs(studentSignatureDirectoryPath + Session["srno"].ToString().Replace("/", "_") + signext);
                }
                param.Add(new SqlParameter("@UploadStudentSignature", fuUploadStudentSignature.HasFile ? studentSignatureVertualPath + Session["srno"].ToString().Replace("/", "_") + signext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@StudentSignatureName", fuUploadStudentSignature.HasFile ? Session["srno"].ToString().Replace("/", "_") + signext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                var parentSignext = "";
                if (fuUploadFatherMotherSigThumbPrint.HasFile)
                {
                    parentSignext = Path.GetExtension(fuUploadStudentSignature.FileName);
                    fuUploadFatherMotherSigThumbPrint.SaveAs(parentSignatureDirectoryPath + Session["srno"].ToString().Replace("/", "_") + parentSignext);
                }
                param.Add(new SqlParameter("@UploadParentsSignature", fuUploadFatherMotherSigThumbPrint.HasFile ? parentSignatureVertualPath + Session["srno"].ToString().Replace("/", "_") + parentSignext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@UploadParentsSignatureName", fuUploadFatherMotherSigThumbPrint.HasFile ? Session["srno"].ToString().Replace("/", "_") + parentSignext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                // ReSharper disable once UnusedVariable
                var msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOtherDetailsProc", param);
            }
        }
        public void GeneralDetailsDisplay()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == null || studentId==string.Empty )
            {
                if (TxtEnter.Text.Trim() == string.Empty)
                {
                    studentId = drpSrno.SelectedValue;
                }
                else
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }
            _sql = "select StEnRCode ,SrNo,FirstName,MiddleName,LastName,left(convert(nvarchar,DOB,106),2) as DD,Right(left(convert(nvarchar,DOB,106),6),3) as MM ,";
            _sql +=  "   RIGHT(convert(nvarchar,DOB,106),4) as YY ,Gender,Email,convert(nvarchar,DOB,106) as DOB,convert(nvarchar,AgeOnDate,106) as AgeOnDate,";
            _sql +=  "   MobileNumber,SiblingCategory,SBSrNo,SBStName,SBFathersName,SBClass,SBSection,PhysicallDisabledCategory,PhyStName,";
            _sql +=  "   PhyStDetail,StLocalAddress,Height,Weight,VisionL,VisionR,DentalHygiene,OralHygiene,IdentificationMark,SpecificAilment,StLocalZip,StPerAddress,SPM.StateName,CPM.CityName,StPerZip,";
            _sql +=  "   Religion,Nationality,Category,Caste,BloodGroup,HouseName,PhotoPath,PhCertificateNo,PhCertificateIssuedBy,PrePhoneNo,";
            _sql +=  "   PreMobileNo,PerPhoneNo,PerMobileNo,MotherTongue,HomeTown,AadharNo,convert(nvarchar,AadharIssueDate,106) as AadharIssueDate,";
            _sql +=  "   isSmsAck,isEmailAck from StudentGenaralDetail sg";
            _sql +=  "   left join CityMaster CLM on sg.StLocalCityId=CLM.Id";
            _sql +=  "   left join CityMaster CPM on sg.StPerCityId=CPM.Id";
            _sql +=  "   left join StateMaster SLM on sg.StLocalStateId=SLM.Id";
            _sql +=  "   left join StateMaster SPM on sg.StPerStateId=SPM.Id";
            _sql +=  "   Where Case When Left('" + studentId + "',3)='eAM' Then SG.StEnRCode Else SG.SrNo End ='" + studentId + "'";
            _sql +=  "   and sg.SessionName='" + Session["SessionName"] + "' and sg.BranchCode=" + Session["BranchCode"] + "";

            string date = "";



            try
            {
                try
                {
                    string imageurl = _oo.ReturnTag(_sql, "PhotoPath");
                    Avatar.ImageUrl = imageurl + "?" + DateTime.Now.Ticks.ToString();
                }
                catch
                {
                    Avatar.ImageUrl = "";
                }

                txtPreaddress.Text = _oo.ReturnTag(_sql, "StLocalAddress");
                txtPreZip.Text = _oo.ReturnTag(_sql, "StLocalZip");
                txtPerAdd.Text = _oo.ReturnTag(_sql, "StPerAddress");

                txtPhyName.Text = _oo.ReturnTag(_sql, "PhyStName");
                txtPhyDetail.Text = _oo.ReturnTag(_sql, "PhyStDetail");

                txtCertificateNo.Text = _oo.ReturnTag(_sql, "PhCertificateNo");
                txtIssuedBy.Text = _oo.ReturnTag(_sql, "PhCertificateIssuedBy");



                txtPerZip.Text = _oo.ReturnTag(_sql, "StPerZip");
                txtHeight.Text = _oo.ReturnTag(_sql, "Height");
                txtWeight.Text = _oo.ReturnTag(_sql, "Weight");
                txtVLeft.Text = _oo.ReturnTag(_sql, "VisionL");
                txtVRight.Text = _oo.ReturnTag(_sql, "VisionR");
                txtDental.Text = _oo.ReturnTag(_sql, "DentalHygiene");
                txtOral.Text = _oo.ReturnTag(_sql, "OralHygiene");
                txtIMark.Text = _oo.ReturnTag(_sql, "IdentificationMark");
                txtSpeAilment.Text = _oo.ReturnTag(_sql, "SpecificAilment");
                txtPerPhoneNo.Text = _oo.ReturnTag(_sql, "PerPhoneNo");
                txtPerMobileNo.Text = _oo.ReturnTag(_sql, "PerMobileNo");
                txtPrePhoneNo.Text = _oo.ReturnTag(_sql, "PrePhoneNo");
                txtPreMobileNo.Text = _oo.ReturnTag(_sql, "PreMobileNo");

                txtFirstNa.Text = _oo.ReturnTag(_sql, "FirstName");
                txtMidNa.Text = _oo.ReturnTag(_sql, "MiddleName");
                txtlast.Text = _oo.ReturnTag(_sql, "LastName");

                txtEmail.Text = _oo.ReturnTag(_sql, "Email");

                txtMobile.Text = _oo.ReturnTag(_sql, "MobileNumber");

                string Nationality = _oo.ReturnTag(_sql, "Nationality");
                TextBox65.Text = Nationality == string.Empty ? "INDIAN" : Nationality;

                TextBox66.Text = _oo.ReturnTag(_sql, "Caste");

                string mothertounge = _oo.ReturnTag(_sql, "MotherTongue");
                txtMotherTongue.Text = mothertounge == string.Empty ? "HINDI" : mothertounge;

                string homeTown = _oo.ReturnTag(_sql, "HomeTown");
                txtHomeTown.Text = homeTown == string.Empty ? "LUCKNOW" : homeTown;


                txtAadharNo.Text = _oo.ReturnTag(_sql, "AadharNo");
                date = Convert.ToDateTime(_oo.ReturnTag(_sql, "DOB")).ToString("dd-MMM-yyyy");
                if (date.ToUpper() == "1900 JAN 01")
                {
                    txtStudentDOB.Text = "";
            
                }
                else
                {
                    txtStudentDOB.Text = date;
                }

                if (_oo.ReturnTag(_sql, "AgeOnDate") != "")
                {
                    try
                    {
                        txtAgeOnDate.Text = Convert.ToDateTime(_oo.ReturnTag(_sql, "AgeOnDate")).ToString("dd-MMM-yyyy");
                    }
                    catch
                    {
                    }
                }

    
                if (_oo.ReturnTag(_sql, "Gender") != DBNull.Value.ToString())
                {
                    try
                    {
                        RadioButtonList1.SelectedValue = _oo.ReturnTag(_sql, "Gender").Trim();
                    }
                    catch { }
                }

        
                if (_oo.ReturnTag(_sql, "Religion") != DBNull.Value.ToString())
                {
                    DropDownList1.SelectedValue = DropDownList1.Items.FindByText(_oo.ReturnTag(_sql, "Religion")).Value;
                }
                else
                {
                    DropDownList1.SelectedIndex = 0;
                }
        
                try
                {
                    if (_oo.ReturnTag(_sql, "Category") != DBNull.Value.ToString())
                    {
                        DropDownList2.SelectedValue = DropDownList2.Items.FindByText(_oo.ReturnTag(_sql, "Category")).Value;
                    }
                    else
                    {
                        DropDownList2.SelectedValue = DropDownList2.Items.FindByText("Others").Value;
                    }
                }
                catch
                {
                    DropDownList2.SelectedIndex = 1;
                }

       
                try
                {
                    drpBloodGroup.SelectedValue = drpBloodGroup.Items.FindByText(_oo.ReturnTag(_sql, "BloodGroup")).Value;
                }
                catch
                {
                }
                try
                {
                    if (_oo.ReturnTag(_sql, "PhysicallDisabledCategory") != "" && _oo.ReturnTag(_sql, "PhysicallDisabledCategory") != DBNull.Value.ToString())
                    {
                        RadioButtonList8.SelectedValue = RadioButtonList8.Items.FindByText(_oo.ReturnTag(_sql, "PhysicallDisabledCategory")).Value;
                    }
                    else
                    {
                        RadioButtonList8.SelectedIndex = 1;
                    }
                }
                catch { }
                if (RadioButtonList8.SelectedItem.Text == "Yes")
                {
                    Panel2.Visible = true;
                }
                else
                {
                    Panel2.Visible = false;
                }
                   
                //txtAadharIssueDate.Text = oo.ReturnTag(sql, "AadharissueDate");

                try
                {
                    date = Convert.ToDateTime(_oo.ReturnTag(_sql, "AadharissueDate")).ToString("dd-MMM-yyyy");
                    txtAadharIssueDate.Text = date;
                }
                catch
                {
                }

                chkStMobile.Checked = _oo.ReturnTag(_sql, "isSmsAck").ToString() == "True" ? true : false;
                chkStEmail.Checked = _oo.ReturnTag(_sql, "isEmailAck").ToString() == "True" ? true : false;

                if (_oo.ReturnTag(_sql, "StateName") != DBNull.Value.ToString())
                {
                    try
                    {
                        DrpPerState.SelectedValue = DrpPerState.Items.FindByText(_oo.ReturnTag(_sql, "StateName")).Value;
                    }
                    catch { }
                }
                string sqlnew = _sql;
                LoadCity(DrpPerCity, DrpPerState);
                try
                {
                    if (_oo.ReturnTag(sqlnew, "CityName") != DBNull.Value.ToString())
                    {
                        DrpPerCity.SelectedValue = DrpPerCity.Items.FindByText(_oo.ReturnTag(sqlnew, "CityName")).Value;
                    }
                    else
                    {
                        DrpPerCity.SelectedValue = DrpPerCity.Items.FindByText("Lucknow").Value;
                    }
                }
                catch
                {
                    //DrpPerCity.SelectedValue = DrpPerCity.Items.FindByText("Lucknow").Value;
                }


                try
                {
                    DrpPreState.SelectedValue = DrpPreState.Items.FindByText(_oo.ReturnTag(sqlnew, "StateName")).Value;
                }
                catch (Exception) { }

                LoadCity(DrpPreCity, DrpPreState);

                try
                {
                    if (_oo.ReturnTag(sqlnew, "CityName") != DBNull.Value.ToString())
                    {
                        DrpPreCity.SelectedValue = DrpPreCity.Items.FindByText(_oo.ReturnTag(sqlnew, "CityName")).Value;
                    }
                    else
                    {
                        DrpPreCity.SelectedValue = DrpPreCity.Items.FindByText("Lucknow").Value;
                    }
                }
                catch (Exception) { 
                    //DrpPreCity.SelectedValue = DrpPreCity.Items.FindByText("Lucknow").Value;
                }




            }
            catch (Exception) { }

        }
        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using (_con = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = _con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        public void FamilyDetailsDisplay()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == null || studentId==string.Empty )
            {
                if (TxtEnter.Text.Trim() == string.Empty)
                {
                    studentId = drpSrno.SelectedValue;
                }
                else
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }

            _sql = "select StEnRCode,SrNo,FatherOccupation,FatherDesignation,FatherQualification,FatherIncomeMonthly,";
            _sql +=  "   FatherOfficeAddress,FatherContactNo,FatherEmail,FamilyIncomeMonthly,FamilyRelationship,FamilyEmail,FamilyGuardianName,FamilyContactNo,";
            _sql +=  "   MotherOccupation,MotherDesignation,MotherQualification,MotherIncomeMonthly,MotherOfficeAddress,MotherContactNo,MotherEmail,";
            _sql +=  "    BranchCode,LoginName,SessionName,RecordDate,FatherName,MotherName,GuardiantwoIncomeMonthly,GuardiantwoName,";
            _sql +=  "    GuardiantwoContact,GuardiantwoRelationship,GuardiantwoEmail,FatherPhotoPath,FatherPhotoName,MotherPhotoPath,MotherPhotoName,";
            _sql +=  "  FatherOfficePhoneNo,FatherOfficeMobileNo,FatherOfficeEmail,MotherOfficePhoneNo,MotherOfficeMobileNo,MotherOfficeEmail,ParentTotalIncome,GroupPhotoName,GroupPhotoPath,";
            _sql +=  " G1Address,G1State,G1City,G1PhoneNo,G1MobileNo,G1Pin,G2Address,G2State,G2City,G2PhoneNo,G2MobileNo,G2Pin,FatherNationality,MotherNationality,isfaSmsAck , isfaEmailAck ,ismoSmsAck ,ismoEmailAck ,isguaSmsAck , isguaEmailAck from StudentFamilyDetails";
            _sql +=  "    where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            try
            {
                txtfaNameee.Text = _oo.ReturnTag(_sql, "FatherName");

                string facontact = _oo.ReturnTag(_sql, "FatherContactNo");
                txtcontfa.Text = facontact;

                string familycontact = _oo.ReturnTag(_sql, "FamilyContactNo");
                txtcontactNo.Text = familycontact == string.Empty ? facontact : familycontact;

                string mocontact = _oo.ReturnTag(_sql, "MotherContactNo");
                txtmothercontact.Text = mocontact == string.Empty ? facontact : mocontact;

                string fanationality = _oo.ReturnTag(_sql, "FatherNationality");
                txtFatherNationality.Text = fanationality == string.Empty ? "INDIAN" : fanationality;

                string monationality = _oo.ReturnTag(_sql, "MotherNationality");
                txtMotherNationality.Text = monationality == string.Empty ? "INDIAN" : monationality;

                string guardianName = _oo.ReturnTag(_sql, "FamilyGuardianName");
                txtguardianname.Text = guardianName == string.Empty ? txtfaNameee.Text.Trim() : guardianName;

                try
                {
                    drpOccupationfa.Text = _oo.ReturnTag(_sql, "FatherOccupation");
                }
                catch (Exception) 
                {
                    drpOccupationfa.Text = _oo.ReturnTag(_sql, "FatherOccupation").ToUpper();
                }
           
                txtdesfa.Text = _oo.ReturnTag(_sql, "FatherDesignation");
                txtqufa.Text = _oo.ReturnTag(_sql, "FatherQualification");
                txtincomefa.Text = _oo.ReturnTag(_sql, "FatherIncomeMonthly");
                txtoffaddfa.Text = _oo.ReturnTag(_sql, "FatherOfficeAddress");
      
                txtemailfather.Text = _oo.ReturnTag(_sql, "FatherEmail");
                txtfailyincome.Text = _oo.ReturnTag(_sql, "FamilyIncomeMonthly");
                DrpRelationship.Text = _oo.ReturnTag(_sql, "FamilyRelationship");
                txtemailfamily.Text = _oo.ReturnTag(_sql, "FamilyEmail");
          
          
                txtmotherNameeee.Text = _oo.ReturnTag(_sql, "MotherName");
              
                try
                {
                    drpOccupationmoth.Text = _oo.ReturnTag(_sql, "MotherOccupation");
                }
                catch (Exception)
                {
                    drpOccupationfa.Text = _oo.ReturnTag(_sql, "MotherOccupation").ToUpper();
                }


                txtdesmoth.Text = _oo.ReturnTag(_sql, "MotherDesignation");
                txtqualimother.Text = _oo.ReturnTag(_sql, "MotherQualification");
                txtincomemonthlymother.Text = _oo.ReturnTag(_sql, "MotherIncomeMonthly");
                txtofficeaddmother.Text = _oo.ReturnTag(_sql, "MotherOfficeAddress");



                txtmotheremail.Text = _oo.ReturnTag(_sql, "MotherEmail");
                if (_oo.ReturnTag(_sql, "GuardiantwoIncomeMonthly") == string.Empty)
                {
                    txtGuardiantwoIncomeMonthly.Text = "0";
                }
                else
                {
                    txtGuardiantwoIncomeMonthly.Text = _oo.ReturnTag(_sql, "GuardiantwoIncomeMonthly");
                }
            
                txtGuardiantwoName.Text = _oo.ReturnTag(_sql, "GuardiantwoName");
                txtGuardiantwoContact.Text = _oo.ReturnTag(_sql, "GuardiantwoContact");
                if (_oo.ReturnTag(_sql, "GuardiantwoRelationship") == string.Empty)
                {
                    drpGuardiantwoRelationship.SelectedIndex = 2;
                }
                else
                {
                    drpGuardiantwoRelationship.SelectedValue = _oo.ReturnTag(_sql, "GuardiantwoRelationship");
                }
                txtGuardiantwoEmail.Text = _oo.ReturnTag(_sql, "GuardiantwoEmail");

                txtFatherOfficePhoneNo.Text = _oo.ReturnTag(_sql, "FatherOfficePhoneNo");
                txtFatherOfficeMobileNo.Text = _oo.ReturnTag(_sql, "FatherOfficeMobileNo");
                txtFatherOfficeEmail.Text = _oo.ReturnTag(_sql, "FatherOfficeEmail");
                txtMotherOfficePhoneNo.Text = _oo.ReturnTag(_sql, "MotherOfficePhoneNo");
                txtMotherOfficeMobileNo.Text = _oo.ReturnTag(_sql, "MotherOfficeMobileNo");
                txtMotherOfficeEmail.Text = _oo.ReturnTag(_sql, "MotherOfficeEmail");
                txtParentTotalIncome.Text = _oo.ReturnTag(_sql, "ParentTotalIncome");

                txtG1Address.Text=  _oo.ReturnTag(_sql, "G1Address");
          
           
            
                string _FatherPhotoPath = lblFatherImageUrl.Text = _oo.ReturnTag(_sql, "FatherPhotoPath");
                lblFatherImageName.Text = _oo.ReturnTag(_sql, "FatherPhotoName");
                if (_FatherPhotoPath != string.Empty)
                {
                    imgFather.ImageUrl = _oo.ReturnTag(_sql, "FatherPhotoPath");
                }
                else
                {
                    imgFather.ImageUrl = @"~\admin\images\dummy.png";
                }
                string _MotherPhotoPath = lblMotherImageUrl.Text = _oo.ReturnTag(_sql, "MotherPhotoPath");
                lblMotherImageName.Text = _oo.ReturnTag(_sql, "MotherPhotoName");
                if (_MotherPhotoPath != string.Empty)
                {
                    imgMother.ImageUrl = _oo.ReturnTag(_sql, "MotherPhotoPath");
                }
                else
                {
                    imgMother.ImageUrl = @"~\admin\images\dummy.png";
                }

                string _GroupPhotoPath = lblGroupImageUrl.Text = _oo.ReturnTag(_sql, "GroupPhotoPath");
                lblGroupImageName.Text = _oo.ReturnTag(_sql, "GroupPhotoName");
                if (_GroupPhotoPath != string.Empty)
                {
                    imgGroupPhoto.ImageUrl = _oo.ReturnTag(_sql, "GroupPhotoPath");
                }
                else
                {
                    imgGroupPhoto.ImageUrl = @"~\admin\images\dummy.png";
                }
                txtG1PhoneNo.Text = _oo.ReturnTag(_sql, "G1PhoneNo");
                txtG1MobileNo.Text = _oo.ReturnTag(_sql, "G1MobileNo");
                txtG1Pin.Text = _oo.ReturnTag(_sql, "G1Pin");
                txtG2Address.Text = _oo.ReturnTag(_sql, "G2Address");
                txtG2PhoneNo.Text = _oo.ReturnTag(_sql, "G2PhoneNo");
                txtG2MobileNo.Text = _oo.ReturnTag(_sql, "G2MobileNo");
                txtG2Pin.Text = _oo.ReturnTag(_sql, "G2Pin");

                drpG1State.SelectedValue = _oo.ReturnTag(_sql, "G1State");
                drpG2State.SelectedValue = _oo.ReturnTag(_sql, "G2State");



                chkFaMobile.Checked = _oo.ReturnTag(_sql, "isfaSmsAck").ToString() == "True" ? true : false;
                chkFaEmail.Checked = _oo.ReturnTag(_sql, "isfaEmailAck").ToString() == "True" ? true : false;

                chkMoMobile.Checked = _oo.ReturnTag(_sql, "ismoSmsAck").ToString() == "True" ? true : false;
                chkMoEmail.Checked = _oo.ReturnTag(_sql, "ismoEmailAck").ToString() == "True" ? true : false;

                chkGuaMobile.Checked = _oo.ReturnTag(_sql, "isguaSmsAck").ToString() == "True" ? true : false;
                chkGuaEmail.Checked = _oo.ReturnTag(_sql, "isguaEmailAck").ToString() == "True" ? true : false;



                string sqldup = _sql;
                LoadCity(drpG1City, drpG1State);
                drpG1City.SelectedValue = _oo.ReturnTag(sqldup, "G1City");
                LoadCity(drpG2City, drpG2State);
                drpG2City.SelectedValue = _oo.ReturnTag(sqldup, "G2City");

           

            }
            catch (Exception) { }
        }
        public void PreviousSchoolDetailsDisplay()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == null || studentId==string.Empty )
            {
                if (TxtEnter.Text.Trim() == string.Empty)
                {
                    studentId = drpSrno.SelectedValue.ToString();
                }
                else
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }


            _sql = "Select ROW_NUMBER() Over (Order by Id) as srno,Board,Result,Medium,";
            _sql +=  " (Case when (Qualification='' or Qualification is null) then 'N/A' else Qualification End) as Exam,Marks as Obtained,";
            _sql +=  " Percentage as Per,SchoolName as Institute,MaxMarks,Yop,Subjects as Subject,RollNo,CertificateNo,MarksSheetNo";
            _sql +=  " from StudentPreviousSchool where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "' ";
            _sql +=  " and BranchCode=" + Session["BranchCode"].ToString() + " and isActive=1";
            try
            {
                rptPreviousEducation.DataSource = _oo.GridFill(_sql);
                rptPreviousEducation.DataBind();
            
                loadBoard();
                setDropdownValue();
                EnableControl();
            }
            catch (Exception) { }
        }

        protected void setDropdownValue()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }

            _sql = "Select Board,Result,Medium from StudentPreviousSchool where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and BranchCode=" + Session["BranchCode"].ToString() + " and isActive=1";

            DataTable dt = _oo.Fetchdata(_sql);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
                {
                    DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                    DropDownList drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                    DropDownList drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");

                    drpBoard.SelectedValue = dt.Rows[i]["Board"].ToString();
                    drpResult.SelectedValue = dt.Rows[i]["Result"].ToString();
                    drpMedium.SelectedValue = dt.Rows[i]["Medium"].ToString();
                }
            }
        }
        public void OfficialDetailDisplay()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == null || studentId==string.Empty )
            {
                if (TxtEnter.Text.Trim() == string.Empty)
                {
                    studentId = drpSrno.SelectedValue.ToString();
                }
                else
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }

            _sql = "select SO.StEnRCode,AdmissionDoneAt,SO.SrNo,convert(nvarchar,SO.DateOfAdmiission,106) as DateOfAdmiission,TypeofEducation,left(convert(nvarchar,SO.DateOfAdmiission,106),2) as DD,Right(left(convert(nvarchar,SO.DateOfAdmiission,106),6),3) as MM ,";
            _sql +=  "  RIGHT(convert(nvarchar,SO.DateOfAdmiission,106),4) as YY,cm.Id as ClassId,sc.SectionName,SO.GroupNa,SO.FileNo,So.Board,So.Card,So.TypeOFAdmision,SO.Reference,SO.Remark,";
            _sql +=  "  SO.BranchCode ,SO.LoginName,so.CardNo ,SO.SessionName,so.Enquiry,DFA ,CFA ,COFA ,SFA ,so.BoardUniversityRollNo,so.InstituteRollNo,SO.RecordDate,so.Medium as Medium,SO.HostelRequired as HostelRequired,SO.TransportRequired as TransportRequired,SO.HouseName as HouseName,SO.LibraryRequired as LibraryRequired,";
            _sql +=  "  So.Course,Branch,Streamid,Scholarship,ModForHostel,ModForTransport,ModForLibrary,MODForFeeDeposit,SMSAcknowledgment,EmailAcknowledgment from StudentOfficialDetails SO left join ClassMaster CM on SO.AdmissionForClassId=CM.Id and cm.SessionName=so.SessionName";
            _sql +=  "  left join SectionMaster SC on SO.SectionId=SC.Id and sc.SessionName=so.SessionName";
            _sql +=  "  where Case When Left('" + studentId + "',3)='eAM' Then So.StEnRCode Else So.SrNo End ='" + studentId + "'";
            _sql +=  "  and so.SessionName='" + Session["SessionName"].ToString() + "' and so.BranchCode=" + Session["BranchCode"].ToString() + "";

            string sqlforsection = _sql;
            try
            {
                TextBox100.Text = Convert.ToDateTime(_oo.ReturnTag(_sql, "DateOfAdmiission")).ToString("dd-MMM-yyyy"); 
                txtSr.Text = _oo.ReturnTag(_sql, "SrNo");
                Session["StEnRcode"] = _oo.ReturnTag(_sql, "StEnRCode");
                txtfileno.Text = _oo.ReturnTag(_sql, "FileNo");
                txtReferences.Text = _oo.ReturnTag(_sql, "Reference");
                txtrema.Text = _oo.ReturnTag(_sql, "Remark");
                txtDFA.Text = _oo.ReturnTag(_sql, "DFA");
                txtCFA.Text = _oo.ReturnTag(_sql, "CFA");
                txtCOFA.Text = _oo.ReturnTag(_sql, "COFA");
                txtSFA.Text = _oo.ReturnTag(_sql, "SFA");
                txtAddDoneat.Text = _oo.ReturnTag(_sql, "AdmissionDoneAt") == string.Empty ? "0.00" : _oo.ReturnTag(_sql, "AdmissionDoneAt");
                try
                {
                    drpMedium.SelectedValue = drpMedium.Items.FindByText(_oo.ReturnTag(_sql, "Medium")).Value;
                }
                catch (Exception) { }
                try
                {
                    DrpBoard.SelectedValue= DrpBoard.Items.FindByText(_oo.ReturnTag(_sql, "Board")).Value;
                }
                catch (Exception) { }
                try
                {
                    DrpNEWOLSAdmission.SelectedValue = _oo.ReturnTag(_sql, "TypeOFAdmision");
                }
                catch (Exception) { }
                try
                {
                    drpPanelCardType.SelectedValue = drpPanelCardType.Items.FindByText(_oo.ReturnTag(_sql, "Card").Trim()).Value;
                }
                catch (Exception) { }

                try
                {
                    rbHostel.SelectedValue = _oo.ReturnTag(_sql, "HostelRequired");
                }
                catch (Exception) { }

                try
                {
                    rbTransport.SelectedValue = _oo.ReturnTag(_sql, "TransportRequired");
                    //ScriptManager.RegisterClientScriptBlock(lnkShow, this.GetType(), "unload", "document.onunload=;", true);
                }
                catch (Exception) { }

                try
                {
                    DropDownList4.Text = _oo.ReturnTag(_sql, "HouseName");
                }
                catch (Exception) {
                    DropDownList4.SelectedIndex = DropDownList4.Items.Count - 1;
                }

                try
                {
                    rbLibrary.SelectedValue = _oo.ReturnTag(_sql, "LibraryRequired");
                }
                catch (Exception) { }
                try
                {
                    DropBranch.Text = _oo.ReturnTag(_sql, "GroupNa");
                }
                catch (Exception) {
                    DropBranch.SelectedIndex = DropBranch.Items.Count-1;
                }
                try
                {
                    txtEnquiryNo.Text = _oo.ReturnTag(_sql, "Enquiry");
                }
                catch (Exception) { }
                try
                {
                    txtSchoolcollegeRollno.Text = _oo.ReturnTag(_sql, "InstituteRollNo");
                }
                catch (Exception) { }
                try
                {
                    txtUniversityBoardRollNo.Text = _oo.ReturnTag(_sql, "BoardUniversityRollNo");
                }
                catch (Exception) { }

                rbScholarship.SelectedValue = _oo.ReturnTag(_sql, "Scholarship") != "" ? _oo.ReturnTag(_sql, "Scholarship") : "No";

                txtCardNo.Text = _oo.ReturnTag(_sql, "CardNo");

                DropCourse.SelectedValue = _oo.ReturnTag(_sql, "Course") != "" ? _oo.ReturnTag(_sql, "Course") : "0";
                if (_oo.ReturnTag(_sql, "ModForHostel") != string.Empty)
                {
                    try
                    {
                        drpHostalMOD.SelectedValue = _oo.ReturnTag(_sql, "ModForHostel");
                    }
                    catch { }
                }
                if (_oo.ReturnTag(_sql, "ModForTransport") != string.Empty)
                { try
                    {
                        drpTransportMOD.SelectedValue = _oo.ReturnTag(_sql, "ModForTransport");
                    }
                    catch { }
                }
                if (_oo.ReturnTag(_sql, "ModForLibrary") != string.Empty)
                { try
                    {
                        drpLibraryMOD.SelectedValue = _oo.ReturnTag(_sql, "ModForLibrary");
                    }
                    catch { }
                }
                if (_oo.ReturnTag(_sql, "MODForFeeDeposit") != string.Empty)
                {
                    try
                    {
                        drpFeeDepositMOD.SelectedValue = _oo.ReturnTag(_sql, "MODForFeeDeposit");
                    }
                    catch { }
                }

                try
                {
                    drpSMSAcknowledgmentTo.SelectedValue = drpSMSAcknowledgmentTo.Items.FindByText(_oo.ReturnTag(_sql, "SMSAcknowledgment")).Value;
                }
                catch
                {
                    drpSMSAcknowledgmentTo.SelectedIndex = 1;
                }
                try
                {
                    drpEmailAcknowledgmentTo.SelectedValue = drpEmailAcknowledgmentTo.Items.FindByText(_oo.ReturnTag(_sql, "EmailAcknowledgment")).Value;
                }
                catch { drpEmailAcknowledgmentTo.SelectedIndex = 1; }


                try
                {
                    RadioButtonList3.SelectedIndex = _oo.ReturnTag(_sql, "TypeofEducation") == "P" ? 1 : 0;
                }
                catch
                {
                }

                try
                {
                    loadClass();
                
                }
                catch
                {
                }

                try
                {
                    DropAdmissionClass.SelectedValue = _oo.ReturnTag(sqlforsection, "ClassId");
                }
                catch
                {
                }

                try
                {
                    loadBranch();

                }
                catch
                {
                }

                try
                {
                    DropBranch.SelectedValue = _oo.ReturnTag(sqlforsection, "Branch") != "" ? _oo.ReturnTag(sqlforsection, "Branch") : "0";
                }
                catch
                {
                }

                try
                {
                    loadStream();

                }
                catch
                {
                }

                try
                {
                    DropStream.SelectedValue = _oo.ReturnTag(sqlforsection, "Streamid") != "" ? _oo.ReturnTag(sqlforsection, "Streamid") : "0";
                }
                catch
                {
                }


                try
                {
                    loadSection();

                }
                catch
                {
                }

                try
                {
                    string section = _oo.ReturnTag(sqlforsection, "SectionName");
                    DropSection.Text = _oo.ReturnTag(sqlforsection, "SectionName");

                }
                catch
                {
                }

                try
                {
                    loadOptionalSubject();

                }
                catch
                {
                }

                try
                {
                    for (int i = 0; i < rbOptionalSubject.Items.Count; i++)
                    {
                        if (GetOptionalSubjectRecordbySrno(rbOptionalSubject.Items[i].Value))
                        {
                            rbOptionalSubject.Items[i].Selected = true;
                        }
                        else
                        {
                            rbOptionalSubject.Items[i].Selected = false;
                        }
                    }
                }
                catch
                {
                }                                         
            }
            catch (Exception) { }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            OfficialDetailDisplay();
            GeneralDetailsDisplay();
            FamilyDetailsDisplay();
            PreviousSchoolDetailsDisplay();
        }

        public bool Not_Change_sr()
        {
            _sql = "select COUNT(*) as Counter from FeeDeposite where SrNo='" + Label4.Text + "'";
            bool return_value=false;
            if (_oo.ReturnTag(_sql, "Counter") != "0")
            {           
                return_value=true;
            }

            return return_value;
       
        }

        private void loadSection()
        {
            _sql = "select SectionName from SectionMaster where ClassNameId='" + DropAdmissionClass.SelectedValue.ToString() + "'";
            _sql +=  "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, DropSection, "SectionName");
        }
  
        protected void DropAdmissionClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string values = DropSection.SelectedValue;
            loadBranch();
            loadSection();
            try
            {
                DropSection.SelectedValue = values;
            }
            catch
            {
                DropSection.SelectedIndex = 0;
            }
            Get_DocumentName();
        }
   
        protected void DrpPreState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CCode = "";
            _sql = "select Id from StateMaster where StateName='" + DrpPreState.SelectedItem.ToString() + "'";

            CCode = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName from CityMaster where StateId=" + CCode;

            _oo.FillDropDown(_sql, DrpPreCity, "CityName");

        }
  
        protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CCode = "";
            _sql = "select Id from StateMaster where StateName='" + DrpPerState.SelectedItem.ToString() + "'";

            CCode = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName from CityMaster where StateId=" + CCode;

            _oo.FillDropDown(_sql, DrpPerCity, "CityName");
        }
        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                try
                {
                    string studentId = Request.Form[hfStudentId.UniqueID];
                    if (string.IsNullOrEmpty(studentId))
                    {
                        studentId = TxtEnter.Text.Trim();
                    }

                    _sql = "Select Distinct(sod.Card) as Card,Class from StudentOfficialDetails sod inner join ";
                    _sql +=  "FeeDeposite fd on fd.SrNo=sod.SrNo and fd.StEnRCode=sod.StEnRCode ";
                    _sql +=  "where sod.SessionName='" + Session["SessionName"].ToString() + "' and fd.SessionName='" + Session["SessionName"].ToString() + "' and";
                    _sql +=  "sod.BranchCode=" + Session["BranchCode"].ToString() + " and Case When Left('" + studentId + "',3)='eAM' Then Sod.StEnRCode Else Sod.SrNo End ='" + studentId + "' and fd.Cancel is null";

                    string cardtype = _oo.ReturnTag(_sql, "Card");
                    string Class = _oo.ReturnTag(_sql, "Class");

                    if (cardtype == "")
                    {
                        updatestudent();
                    }
                    else if (cardtype.ToUpper() == drpPanelCardType.SelectedItem.ToString().ToUpper())
                    {
                        updatestudent();

                    }
                    else
                    {
                        _oo.MessageBoxforUpdatePanel("Please cancel all fee receipts, after that you can change Group/Class!", this.Page);
                    }
                }
                catch
                {
                    _oo.MessageBoxforUpdatePanel("Not Update Successfuly", this.Page);
                }
            }
            else
            {
                _oo.MessageBoxforUpdatePanel("Please fill all * fields!", this.Page);
            }
        }

        public void updatestudent()
        {
            bool flag1=true, flag2=true, flag3=true;

            int i = 0;
            int j = 0;
            int k = 0;

            if (txtFirstNa.Text == "" || txtPreaddress.Text == "" || DrpPerState.SelectedItem.ToString() == "<--Select-->" || DrpPerCity.SelectedItem.ToString() == "<--Select-->" || txtPerAdd.Text == "" || DrpPreCity.SelectedItem.ToString() == "<--Select-->" || DrpPerState.SelectedItem.ToString() == "<--Select-->" || DropDownList1.SelectedItem.ToString() == "<--Select-->" || DropDownList2.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBoxforUpdatePanel("Please Fill All Required Field in GeneralDetails", this.Page);
                flag1 = false;
            }
            if (txtfaNameee.Text == "" || drpOccupationfa.SelectedItem.ToString() == "<--Select-->" || drpOccupationmoth.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBoxforUpdatePanel("Please Fill All Required Field in Family Details", this.Page);
                flag2 = false;
            }
            if (txtSr.Text == "" || DropAdmissionClass.SelectedItem.ToString() == "<--Select-->" || DropSection.SelectedItem.ToString() == "<--Select-->" || DrpBoard.SelectedItem.ToString() == "<--Select-->" || DropDownList4.SelectedItem.ToString() == "<--Select-->" || drpMedium.SelectedItem.ToString() == "<--Select-->" || DropBranch.SelectedItem.ToString() == "<--Select-->" || drpPanelCardType.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBoxforUpdatePanel("Please Fill All Required Field in Official Details", this.Page);
                flag3 = false;
            }

            if (flag1 == false)
            {
                i = 1;
            }
            if (flag2 == false)
            {
                j = 1;
            }
            if (flag3 == false)
            {
                k = 1;
            }

            if (i == 0 && j == 0 && k == 0)
            {
                TextTrnsform();

                FamilyDetails();
                GeneralDetails();
                PreviousSchoolDetails();
                OfficialDetails();
            
                NewDocumentsDetails();
                Get_DocumentName();
                SaveStudentOtherDetails();
                setEntranceExamRecord();
                Campus camp = new Campus(); camp.msgbox(this.Page, divmsg, "Updated successfully", "S");
                //oo.MessageBoxforUpdatePanel("Updated successfully", this.Page);
                ScriptManager.RegisterClientScriptBlock(LinkButton14, GetType(), "redirect", "window.location='update_student_registration.aspx';", true);
                //Session["srNo."] = TxtEnter.Text.Trim().Replace("'", "");
                //ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "redirect", "window.open('../11/StudentRegView.aspx?print=1','_blank')", true);
            }
        }

        public void TextTrnsform()
        {
            object value;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@isDo", "Select"));
            param.Add(new SqlParameter("@value", ""));
            param.Add(new SqlParameter("@SessionName", ""));
            param.Add(new SqlParameter("@LoginName", ""));
            SqlParameter para = new SqlParameter("@Msg", "");
            para.Direction = ParameterDirection.Output;
            para.Size = 0x10;
            param.Add(para);
            value = _dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "textTransform", "finalsubmit('" + value + "')", true);
        }

        public void SaveOptionalSubjectRecord()
        {
            try
            {
                if (txtSr.Text != string.Empty)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("OptSubjectId");
                    for (int i = 0; i < rbOptionalSubject.Items.Count; i++)
                    {
                        if (rbOptionalSubject.Items[i].Selected)
                        {
                            DataRow dr = dt.NewRow();
                            dr["OptSubjectId"] = rbOptionalSubject.Items[i].Value.ToString();
                            dt.Rows.Add(dr);
                        }
                    }

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@Srno", txtSr.Text.Trim()));
                    param.Add(new SqlParameter("@StudentOptionalSubject", dt));
                    param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                    param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                    param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                    param.Add(new SqlParameter("@QueryFor", "I"));

                    DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOptionalSubjectProc", param);
                }
            }
            catch (Exception ex)
            {
                _oo.MessageBoxforUpdatePanel(ex.Message, this.Page);
            }
        }

        public bool GetOptionalSubjectRecordbySrno(string optionalsubjectid)
        {
            bool flag = false;
            string value = "0";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Srno", txtSr.Text.Trim()));
            param.Add(new SqlParameter("@OptSubjectId", optionalsubjectid));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@QueryFor", "S"));

            value=Convert.ToString(DLL.objDll.Sp_SelectRecord_usingExecuteScalar("StudentOptionalSubjectProc", param));

            if (value == "1")
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        protected void RadioButtonList8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList8.SelectedItem.Text == "Yes")
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
        }
        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    loadState(DrpPerState);
        //    loadCity(DrpPerCity, DrpPreState);
        //    try
        //    {
        //        if (CheckBox1.Items[0].Selected == true)
        //        {
        //            txtPerAdd.Text = txtPreaddress.Text;
        //            DrpPerState.SelectedValue = DrpPreState.SelectedValue;
        //            DrpPerCity.SelectedValue = DrpPreCity.SelectedValue;
        //            txtPerZip.Text = txtPreZip.Text;
        //            txtPerMobileNo.Text = txtPreMobileNo.Text;
        //            txtPerPhoneNo.Text = txtPrePhoneNo.Text;
        //        }
        //        else
        //        {

        //            txtPerAdd.Text = "";
        //            txtPerZip.Text = "";
        //            txtPerMobileNo.Text = "";
        //            txtPerPhoneNo.Text = "";
        //            loaddefaultState(DrpPerState);
        //            loaddefaultCity(DrpPerCity, DrpPerState);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox1);
        //    }
        //}
    

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void ShowDetails()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (studentId == null || studentId==string.Empty )
            {
                if (TxtEnter.Text.Trim() == string.Empty)
                {
                    studentId = drpSrno.SelectedValue.ToString();
                }
                else
                {
                    studentId = TxtEnter.Text.Trim();
                }
            }

            _sql = "select Withdrwal from StudentOfficialDetails where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (Promotion is null or Promotion<>'Cancelled')";
            if (_oo.ReturnTag(_sql, "Withdrwal") != "")
            {
                LinkButton14.Enabled = false;
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Student is already Withdrawn!", "A");
            }
            else
            {
                LinkButton14.Enabled = true;
            }
            Label4.Text = studentId;
            _sql = "select COUNT(*) as Counter from FeeDeposite where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "' and Cancel is NULL";
            if (Convert.ToInt32(_oo.ReturnTag(_sql, "Counter")) != 0)
            {
                txtSr.ReadOnly = true;
            }
            else
            {
                txtSr.ReadOnly = false;
            }
            _sql = "select srno,StEnRCode from StudentOfficialDetails where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and (Promotion is null or Promotion<>'Cancelled')";
            if (DrpEnter.SelectedValue.ToString() == "<--Select-->")
            {
                //oo.MessageBoxforUpdatePanel("Please <--Select--> Condition", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please <--Select--> Condition", "A");
            }

            else if (_oo.Duplicate(_sql) == false)
            {
                //oo.MessageBoxforUpdatePanel("Invalid " + DrpEnter.SelectedItem.ToString() + "!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Invalid " + DrpEnter.SelectedItem.ToString() + "!", "A");
            }

            else
            {
                string srno = _oo.ReturnTag(_sql, "srno");
                OfficialDetailDisplay();
                GeneralDetailsDisplay();
                FamilyDetailsDisplay();
                PreviousSchoolDetailsDisplay();
                Get_DocumentName();
                LoadStudentOtherDetails();
                Session["srno"] = srno;
                getEntranceExamRecord();
                TextTrnsform();
            }
        
       
        }

        public void GeneralDetails()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "StudentGenaralDetailUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;

            string encode = "", srcode = "";

            _sql = "select srno,stenrcode from StudentGenaralDetail where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            Session["StEnRCode"] = encode = _oo.ReturnTag(_sql, "stenrcode");
            Session["srno"] = srcode = _oo.ReturnTag(_sql, "srno");

            cmd.Parameters.AddWithValue("@SRno", srcode);
            cmd.Parameters.AddWithValue("@StEnRCode", encode);
            cmd.Parameters.AddWithValue("@FirstName", txtFirstNa.Text.ToString());
            cmd.Parameters.AddWithValue("@MiddleName", txtMidNa.Text.ToString());
            cmd.Parameters.AddWithValue("@LastName", txtlast.Text.ToString());
            String Date = "";
            Date = txtStudentDOB.Text.Trim();
            cmd.Parameters.AddWithValue("@DOB", Date);
            cmd.Parameters.AddWithValue("@Gender", RadioButtonList1.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
            cmd.Parameters.AddWithValue("@MobileNumber", txtMobile.Text.ToString());
            cmd.Parameters.AddWithValue("@SiblingCategory", "No");
            cmd.Parameters.AddWithValue("@SBSrNo", "");
            cmd.Parameters.AddWithValue("@SBStName", "");
            cmd.Parameters.AddWithValue("@SBFathersName", "");
            cmd.Parameters.AddWithValue("@SBClass", "");
            cmd.Parameters.AddWithValue("@SBSection", "");
            cmd.Parameters.AddWithValue("@PhysicallDisabledCategory", RadioButtonList8.Text.ToString());

            cmd.Parameters.AddWithValue("@PhyStName", txtPhyName.Text.ToString());
            cmd.Parameters.AddWithValue("@PhyStDetail", txtPhyDetail.Text.ToString());
            cmd.Parameters.AddWithValue("@StLocalAddress", txtPreaddress.Text.ToString());
            string DD1 = "";
            _sql = "Select Id from StateMaster where StateName='" + DrpPreState.SelectedItem.ToString() + "'";
            DD1 = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@StLocalStateId", DD1);
            string DD = "";
            _sql = "Select Id from CityMaster where CityName='" + DrpPreCity.SelectedItem.ToString() + "'";
            DD = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@StLocalCityId", DD);
            cmd.Parameters.AddWithValue("@StLocalZip", txtPreZip.Text.ToString());
            cmd.Parameters.AddWithValue("@StPerAddress", txtPerAdd.Text.ToString());
            string D = "";
            _sql = "Select Id from StateMaster where StateName='" + DrpPerState.SelectedItem.ToString() + "'";
            D = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@StPerStateId", D);

            string SD = "";
            _sql = "Select Id from CityMaster where CityName='" + DrpPerCity.SelectedItem.ToString() + "'";
            SD = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@StPerCityId", SD);
            cmd.Parameters.AddWithValue("@StPerZip", txtPerZip.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Religion", DropDownList1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Nationality", TextBox65.Text.ToString());
            cmd.Parameters.AddWithValue("@Category", DropDownList2.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Caste", TextBox66.Text.ToString());
            cmd.Parameters.AddWithValue("@BloodGroup", drpBloodGroup.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@HouseName", "");
            cmd.Parameters.AddWithValue("@Height", txtHeight.Text);
            cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
            cmd.Parameters.AddWithValue("@VisionL", txtVLeft.Text);
            cmd.Parameters.AddWithValue("@VisionR", txtVRight.Text);
            cmd.Parameters.AddWithValue("@DentalHygiene", txtDental.Text);
            cmd.Parameters.AddWithValue("@OralHygiene", txtOral.Text);
            cmd.Parameters.AddWithValue("@IdentificationMark", txtIMark.Text);
            cmd.Parameters.AddWithValue("@SpecificAilment", txtSpeAilment.Text);

            try
            {
                string filePath = "";
                string fileName = "";

                string base64std = hdStPhoto.Value;
                if (base64std != string.Empty)
                {
                    filePath = @"../Uploads/StudentPhoto/";
                    string sessionName = Session["SessionName"].ToString();
                    fileName = srcode.Replace('/','-') + '_' + sessionName + ".jpg";

                    using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }

                cmd.Parameters.AddWithValue("@PhotoPath", filePath + fileName);
                cmd.Parameters.AddWithValue("@PhotoName", fileName);
                cmd.Parameters.AddWithValue("@phCertificateNo", txtCertificateNo.Text.Trim());
                cmd.Parameters.AddWithValue("@phIssuedBy", txtIssuedBy.Text.Trim());

                cmd.Parameters.AddWithValue("@PrePhoneNo", txtPrePhoneNo.Text);
                cmd.Parameters.AddWithValue("@PreMobileNo", txtPreMobileNo.Text);
                cmd.Parameters.AddWithValue("@PerPhoneNo", txtPerPhoneNo.Text);
                cmd.Parameters.AddWithValue("@PerMobileNo", txtPerMobileNo.Text);

                cmd.Parameters.AddWithValue("@MotherTongue", txtMotherTongue.Text.Trim());
                cmd.Parameters.AddWithValue("@HomeTown", txtHomeTown.Text.Trim());
                cmd.Parameters.AddWithValue("@AgeOnDate", txtAgeOnDate.Text.Trim());

                cmd.Parameters.AddWithValue("@AadharNo", txtAadharNo.Text.Trim());
                cmd.Parameters.AddWithValue("@AadharIssueDate", txtAadharIssueDate.Text.Trim());


                string smsAcknowledgment = "";
                if (chkStMobile.Checked)
                {
                    if (txtMobile.Text != string.Empty)
                    {
                        if (smsAcknowledgment == string.Empty)
                        {
                            smsAcknowledgment = txtMobile.Text.Trim();
                        }
                        else
                        {
                            smsAcknowledgment = smsAcknowledgment + "," + txtMobile.Text.Trim();
                        }
                    }
                }

                if (smsAcknowledgment == string.Empty)
                {
                    smsAcknowledgment = null;
                }

                cmd.Parameters.AddWithValue("@smsAcknowledgment", smsAcknowledgment);


                string emailAcknowledgment = "";
                if (chkStEmail.Checked)
                {
                    if (txtEmail.Text != string.Empty)
                    {
                         
                        if (emailAcknowledgment == string.Empty)
                        {
                            emailAcknowledgment = txtEmail.Text.Trim();
                        }
                        else
                        {
                            emailAcknowledgment = emailAcknowledgment + "," + txtEmail.Text.Trim();
                        }
                         
                    }
                }

                if (emailAcknowledgment == string.Empty)
                {
                    emailAcknowledgment = null;
                }

                cmd.Parameters.AddWithValue("@emailAcknowledgment", emailAcknowledgment);

                cmd.Parameters.AddWithValue("@isSmsAck", chkStMobile.Checked ? true : false);
                cmd.Parameters.AddWithValue("@isEmailAck", chkStEmail.Checked ? true : false);



                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                GeneralDetailsDisplay();

            }
            catch (Exception ex)
            {
                _oo.MessageBoxforUpdatePanel(ex.Message, LinkButton14);

                _con.Close();
            }
        }

        public void FamilyDetails()
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "StudentFamilyDetailsUpdateProc";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;

            string encode = "", srcode = "";

            _sql = "select srno,stenrcode from StudentFamilyDetails where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            encode = _oo.ReturnTag(_sql, "stenrcode");
            srcode = _oo.ReturnTag(_sql, "srno");

            cmd.Parameters.AddWithValue("@SRno", srcode);
            cmd.Parameters.AddWithValue("@StEnRCode", encode);
            cmd.Parameters.AddWithValue("@FatherName", txtfaNameee.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherOccupation", drpOccupationfa.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherDesignation", txtdesfa.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherQualification", txtqufa.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherIncomeMonthly", txtincomefa.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherOfficeAddress", txtoffaddfa.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherContactNo", txtcontfa.Text.ToString());
            cmd.Parameters.AddWithValue("@FatherEmail", txtemailfather.Text.ToString());
            cmd.Parameters.AddWithValue("@FamilyIncomeMonthly", txtfailyincome.Text.ToString());
            cmd.Parameters.AddWithValue("@FamilyRelationship", DrpRelationship.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@FamilyEmail", txtemailfamily.Text.ToString());
            cmd.Parameters.AddWithValue("@FamilyGuardianName", txtguardianname.Text.ToString());
            cmd.Parameters.AddWithValue("@FamilyContactNo", txtcontactNo.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherName", txtmotherNameeee.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherOccupation", drpOccupationmoth.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherDesignation", txtdesmoth.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherQualification", txtqualimother.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherIncomeMonthly", txtincomemonthlymother.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherOfficeAddress", txtofficeaddmother.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherContactNo", txtmothercontact.Text.ToString());
            cmd.Parameters.AddWithValue("@MotherEmail", txtmotheremail.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            if (txtGuardiantwoIncomeMonthly.Text.ToString() == string.Empty)
            {
                cmd.Parameters.AddWithValue("@GuardiantwoIncomeMonthly", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GuardiantwoIncomeMonthly", txtGuardiantwoIncomeMonthly.Text.ToString());
            }
            cmd.Parameters.AddWithValue("@GuardiantwoName", txtGuardiantwoName.Text.ToString());
            if (txtGuardiantwoContact.Text == string.Empty)
            {
                cmd.Parameters.AddWithValue("@GuardiantwoContact", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GuardiantwoContact", txtGuardiantwoContact.Text.ToString());
            }
            if (drpGuardiantwoRelationship.SelectedIndex == 0)
            {
                cmd.Parameters.AddWithValue("@GuardiantwoRelationship", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GuardiantwoRelationship", drpGuardiantwoRelationship.Text.ToString());
            }
            cmd.Parameters.AddWithValue("@GuardiantwoEmail", txtGuardiantwoEmail.Text.ToString());

            string FatherImagePath = "";
            string FatherImageFileName = "";


            string base64father = hdfatherPhoto.Value;
            if (base64father != string.Empty)
            {
                FatherImagePath = @"../Uploads/FatherPhoto/";
                string sessionName = Session["SessionName"].ToString();
                FatherImageFileName = srcode.Replace('/','-') + '_' + "FatherImage" + '_' + sessionName + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath((FatherImagePath + FatherImageFileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64father);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                FatherImagePath = FatherImagePath + FatherImageFileName;
            }
            else
            {
                FatherImageFileName = lblFatherImageName.Text.Trim();
                FatherImagePath = lblFatherImageUrl.Text.Trim();
            }

            cmd.Parameters.AddWithValue("@FatherPhotoPath", (FatherImagePath));
            cmd.Parameters.AddWithValue("@FatherPhotoName", FatherImageFileName);

            string MotherImagePath = "";
            string MotherImageFileName = "";


            string base64mother = hdmotherPhoto.Value;
            if (base64mother != string.Empty)
            {
                MotherImagePath = @"../Uploads/MotherPhoto/";
                string sessionName = Session["SessionName"].ToString();
                MotherImageFileName = srcode.Replace('/', '-') + '_' + "MotherImage" + '_' + sessionName + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath((MotherImagePath + MotherImageFileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64mother);
                        bw.Write(data);
                        bw.Close();
                    }
                }

                MotherImagePath = MotherImagePath + MotherImageFileName;
            }
            else
            {
                MotherImageFileName = lblMotherImageName.Text.Trim();
                MotherImagePath = lblMotherImageUrl.Text.Trim();
            }


            cmd.Parameters.AddWithValue("@MotherPhotoPath", (MotherImagePath));
            cmd.Parameters.AddWithValue("@MotherPhotoName", MotherImageFileName);

            cmd.Parameters.AddWithValue("@FatherOfficePhoneNo", txtFatherOfficePhoneNo.Text.Trim());
            cmd.Parameters.AddWithValue("@FatherOfficeMobileNo", txtFatherOfficeMobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@FatherOfficeEmail", txtFatherOfficeEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@MotherOfficePhoneNo", txtMotherOfficePhoneNo.Text.Trim());
            cmd.Parameters.AddWithValue("@MotherOfficeMobileNo", txtMotherOfficeMobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@MotherOfficeEmail", txtMotherOfficeEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@ParentTotalIncome", txtParentTotalIncome.Text.Trim());

            string GroupImagePath = "";
            string GroupImageFileName = "";

            string base64groupPhoto = hdbase64groupPhoto.Value;
            if (base64groupPhoto != string.Empty)
            {
                GroupImagePath = @"../Uploads/GroupPhoto/";
                string sessionName = Session["SessionName"].ToString();
                GroupImageFileName = srcode.Replace('/', '-') + '_' + "GroupImage" + '_' + sessionName + ".jpg";

                using (FileStream fs = new FileStream(Server.MapPath((GroupImagePath + GroupImageFileName)), FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(base64groupPhoto);
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }
            else
            {
                GroupImageFileName = lblGroupImageName.Text.Trim();
                GroupImagePath = lblGroupImageUrl.Text.Trim();
            }

            cmd.Parameters.AddWithValue("@GroupPhotoPath", (GroupImagePath + GroupImageFileName));
            cmd.Parameters.AddWithValue("@GroupPhotoName", GroupImageFileName);

            cmd.Parameters.AddWithValue("@G1Address", txtG1Address.Text.Trim());
            cmd.Parameters.AddWithValue("@G1State", drpG1State.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@G1City", drpG1City.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@G1PhoneNo", txtG1PhoneNo.Text.Trim());
            cmd.Parameters.AddWithValue("@G1MobileNo", txtG1MobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@G1Pin", txtG1Pin.Text.Trim());
            cmd.Parameters.AddWithValue("@G2Address", txtG2Address.Text.Trim());
            cmd.Parameters.AddWithValue("@G2State", drpG2State.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@G2City", drpG2City.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@G2PhoneNo", txtG2PhoneNo.Text.Trim());
            cmd.Parameters.AddWithValue("@G2MobileNo", txtG2MobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@G2Pin", txtG2Pin.Text.Trim());
            cmd.Parameters.AddWithValue("@FatherNationality", txtFatherNationality.Text.Trim());
            cmd.Parameters.AddWithValue("@MotherNationality", txtMotherNationality.Text.Trim());

            string smsAcknowledgment = "";
            if (chkFaMobile.Checked)
            {
                if (txtcontfa.Text != string.Empty)
                {
                    if (smsAcknowledgment == string.Empty)
                    {
                        smsAcknowledgment = txtcontfa.Text.Trim();
                    }
                    else
                    {
                        smsAcknowledgment = smsAcknowledgment + "," + txtcontfa.Text.Trim();
                    }
                }
            }
            if (chkMoMobile.Checked)
            {
                if (txtmothercontact.Text != string.Empty)
                {
                    if (txtmothercontact.Text.Trim() != txtcontfa.Text.Trim())
                    {
                        if (smsAcknowledgment == string.Empty)
                        {
                            smsAcknowledgment = txtmothercontact.Text.Trim();
                        }
                        else
                        {
                            smsAcknowledgment = smsAcknowledgment + "," + txtmothercontact.Text.Trim();
                        }
                    }
                }
            }
            if (chkGuaMobile.Checked)
            {
                if (txtcontactNo.Text != string.Empty)
                {
                    if (txtcontactNo.Text.Trim() != txtcontfa.Text.Trim())
                    {
                        if (smsAcknowledgment == string.Empty)
                        {
                            smsAcknowledgment = txtcontactNo.Text.Trim();
                        }
                        else
                        {
                            smsAcknowledgment = smsAcknowledgment + "," + txtcontactNo.Text.Trim();
                        }
                    }
                }
            }

            if (smsAcknowledgment == string.Empty)
            {
                smsAcknowledgment = txtcontactNo.Text.Trim();
            }

            cmd.Parameters.AddWithValue("@smsAcknowledgment", smsAcknowledgment);


            string emailAcknowledgment = "";
            if (chkFaEmail.Checked)
            {
                if (txtemailfather.Text != string.Empty)
                {
                    if (emailAcknowledgment == string.Empty)
                    {
                        emailAcknowledgment = txtemailfather.Text.Trim();
                    }
                    else
                    {
                        emailAcknowledgment = emailAcknowledgment + "," + txtemailfather.Text.Trim();
                    }
                }
            }
            if (chkMoEmail.Checked)
            {
                if (txtmotheremail.Text != string.Empty)
                {
                    if (txtemailfather.Text.Trim().ToUpper() != txtmotheremail.Text.Trim().ToUpper())
                    {
                        if (emailAcknowledgment == string.Empty)
                        {
                            emailAcknowledgment = txtmotheremail.Text.Trim();
                        }
                        else
                        {
                            emailAcknowledgment = emailAcknowledgment + "," + txtmotheremail.Text.Trim();
                        }
                    }
                }
            }
            if (chkGuaEmail.Checked)
            {
                if (txtemailfamily.Text != string.Empty)
                {
                    if (txtemailfamily.Text.Trim().ToUpper() != txtemailfather.Text.Trim().ToUpper())
                    {
                        if (emailAcknowledgment == string.Empty)
                        {
                            emailAcknowledgment = txtemailfamily.Text.Trim();
                        }
                        else
                        {
                            emailAcknowledgment = emailAcknowledgment + "," + txtemailfamily.Text.Trim();
                        }
                    }
                }
            }

            if (emailAcknowledgment == string.Empty)
            {
                emailAcknowledgment = txtemailfamily.Text.Trim();
            }

            cmd.Parameters.AddWithValue("@emailAcknowledgment", emailAcknowledgment);

            cmd.Parameters.AddWithValue("@isfaSmsAck", chkFaMobile.Checked ? true : false);
            cmd.Parameters.AddWithValue("@isfaEmailAck", chkFaEmail.Checked ? true : false);
            cmd.Parameters.AddWithValue("@ismoSmsAck", chkMoMobile.Checked ? true : false);
            cmd.Parameters.AddWithValue("@ismoEmailAck", chkMoEmail.Checked ? true : false);
            if (txtcontfa.Text != string.Empty)
            {
                cmd.Parameters.AddWithValue("@isguaSmsAck", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@isguaSmsAck", false);
            }
            if (txtemailfamily.Text != string.Empty)
            {
                cmd.Parameters.AddWithValue("@isguaEmailAck", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@isguaEmailAck", false);
            }


            try
            {

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (Exception ex)
            {
                _oo.MessageBoxforUpdatePanel(ex.Message, LinkButton14);
            }
            FamilyDetailsDisplay();

        }


        public void PreviousSchoolDetails()
        {

            if ( rptPreviousEducation.Items.Count > 0)
            {
                _sql = "Delete From StudentPreviousSchool where SrNo='" + Session["SrNo"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                _oo.ProcedureDatabase(_sql);

                for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "StudentPreviousSchoolUpdateProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;

                    TextBox txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                    DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                    DropDownList drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                    TextBox txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                    TextBox txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                    DropDownList drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                    TextBox txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                    TextBox txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                    TextBox txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                    TextBox txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
          
                    TextBox txtMM = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                    TextBox txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                    TextBox txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

                    if (txtExam.Text.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                        cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
                        cmd.Parameters.AddWithValue("@Qualification", txtExam.Text.Trim());
                        cmd.Parameters.AddWithValue("@Board", drpBoard.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@Result", drpResult.SelectedValue.ToString().Trim());
                        cmd.Parameters.AddWithValue("@SchoolName", txtInstitute.Text.Trim() == "" ? DBNull.Value : (object)txtInstitute.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yop", txtYop.Text.Trim() == "" ? DBNull.Value : (object)txtYop.Text.Trim());
                        cmd.Parameters.AddWithValue("@Medium", drpMedium.Text.Trim());
                        cmd.Parameters.AddWithValue("@Subjects", txtSubject.Text.Trim() == "" ? DBNull.Value : (object)txtSubject.Text.Trim());

                        cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text.Trim() == "" ? DBNull.Value : (object)txtRollNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CertificateNo", txtCertificateNo.Text.Trim() == "" ? DBNull.Value : (object)txtCertificateNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@MarksSheetNo", txtMarksSheetNo.Text.Trim() == "" ? DBNull.Value : (object)txtMarksSheetNo.Text.Trim());

                        cmd.Parameters.AddWithValue("@MaxMarks", txtMM.Text.Trim() == "" ? DBNull.Value : (object)txtMM.Text.Trim());
                        cmd.Parameters.AddWithValue("@Marks", txtObtained.Text.Trim() == "" ? DBNull.Value : (object)txtObtained.Text.Trim());
                        cmd.Parameters.AddWithValue("@Percentage", txtPer.Text.Trim() == "" ? DBNull.Value : (object)txtPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                        cmd.Parameters.AddWithValue("@isActive", true);
                        try
                        {

                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                        }
                        catch (Exception ex)
                        {
                            _oo.MessageBoxforUpdatePanel(ex.Message, LinkButton14);
                        }
                    }

                }

            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "StudentPreviousSchoolUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;

                cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());

                cmd.Parameters.AddWithValue("@Qualification", "");
                cmd.Parameters.AddWithValue("@Board", "");
                cmd.Parameters.AddWithValue("@Result", "");
                cmd.Parameters.AddWithValue("@SchoolName", "");
                cmd.Parameters.AddWithValue("@Yop", "");
                cmd.Parameters.AddWithValue("@Medium", "");
                cmd.Parameters.AddWithValue("@Subjects", "");

                cmd.Parameters.AddWithValue("@RollNo", "");
                cmd.Parameters.AddWithValue("@CertificateNo", "");
                cmd.Parameters.AddWithValue("@MarksSheetNo", "");

                cmd.Parameters.AddWithValue("@MaxMarks", "");
                cmd.Parameters.AddWithValue("@Marks", "");
                cmd.Parameters.AddWithValue("@Percentage", "");
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@isActive", false);

                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                }
                catch (Exception ex)
                {
                    _oo.MessageBoxforUpdatePanel(ex.Message, LinkButton14);
                }

            }     
        }
        public void OfficialDetails()
        {

            string studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }
#pragma warning disable 219
            int co = 0;
#pragma warning restore 219

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "StudentOfficialDetailsUpdateProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;


            string encode = "", srcode = "";

            _sql = "select srno,stenrcode from StudentOfficialDetails where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            encode = _oo.ReturnTag(_sql, "stenrcode");
            srcode = _oo.ReturnTag(_sql, "srno");

            cmd.Parameters.AddWithValue("@SRno", srcode);
            cmd.Parameters.AddWithValue("@StEnRCode", encode);
            Session["StEnRCode"] = encode;
            Session["Srno"] = srcode;
            string AdmissionDate = "";
            AdmissionDate = TextBox100.Text.Trim();
            cmd.Parameters.AddWithValue("@DateOfAdmiission", AdmissionDate);
            string ClassId = "";
            _sql = "select Id from ClassMaster where ClassName='" + DropAdmissionClass.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            ClassId = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@AdmissionForClassId", ClassId);
            string sectionId = "";
            _sql = "select Id from SectionMaster where SectionName='" + DropSection.SelectedItem.ToString() + "' and ClassNameId='" + ClassId + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            sectionId = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@GroupNa", DropBranch.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@FileNo", txtfileno.Text.ToString());
            cmd.Parameters.AddWithValue("@Reference", txtReferences.Text.ToString());
            cmd.Parameters.AddWithValue("@Remark", txtrema.Text.ToString());
            cmd.Parameters.AddWithValue("@Board", DrpBoard.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@TypeOFAdmision", DrpNEWOLSAdmission.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Card", drpPanelCardType.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@medium", drpMedium.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@HostelRequired", rbHostel.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@TransportRequired", rbTransport.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@HouseName", DropDownList4.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@LibraryRequired", rbLibrary.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Enquiry", txtEnquiryNo.Text.ToString());
            cmd.Parameters.AddWithValue("@BoardUniversityRollNo", txtUniversityBoardRollNo.Text.ToString());
            cmd.Parameters.AddWithValue("@InstituteRollNo", txtSchoolcollegeRollno.Text.ToString());
            cmd.Parameters.AddWithValue("@CardNo", txtCardNo.Text.ToString());
            cmd.Parameters.AddWithValue("@MachineNo", "null");
            cmd.Parameters.AddWithValue("@Course", DropCourse.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Branch", DropBranch.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Scholarship", rbScholarship.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@ModForHostel", drpHostalMOD.SelectedValue);
            cmd.Parameters.AddWithValue("@ModForTransport", drpTransportMOD.SelectedValue);
            cmd.Parameters.AddWithValue("@ModForLibrary", drpLibraryMOD.SelectedValue);

            cmd.Parameters.AddWithValue("@MODForFeeDeposit", drpFeeDepositMOD.SelectedValue);
            cmd.Parameters.AddWithValue("@SMSAcknowledgment", drpSMSAcknowledgmentTo.Text);
            cmd.Parameters.AddWithValue("@EmailAcknowledgment", drpEmailAcknowledgmentTo.Text);

            cmd.Parameters.AddWithValue("@DFA", txtDFA.Text.Trim());
            cmd.Parameters.AddWithValue("@CFA", txtCFA.Text.Trim());
            cmd.Parameters.AddWithValue("@COFA", txtCOFA.Text.Trim());
            cmd.Parameters.AddWithValue("@SFA", txtSFA.Text.Trim());
            if (DropStream.SelectedIndex != 0)
            {
                cmd.Parameters.AddWithValue("@Streamid", DropStream.SelectedValue.ToString());
            }
            if (RadioButtonList3.SelectedIndex == 0 || RadioButtonList3.SelectedIndex == 1)
            {
                string typeofedu = RadioButtonList3.SelectedIndex == 0 ? "R" : "P";
                cmd.Parameters.AddWithValue("@TypeofEducation", typeofedu);
            }
            cmd.Parameters.AddWithValue("@AdmissionDoneAt", txtAddDoneat.Text.Trim());

            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (SqlException) { _con.Close(); }
            SaveOptionalSubjectRecord();
            OfficialDetailDisplay();
        }
        public void Document()
        {
       
        }

        protected void LinkGneralDetails_Click(object sender, EventArgs e)
        {
            GeneralDetails();
        }
        protected void lnkfamilydetail_Click(object sender, EventArgs e)
        {
            FamilyDetails();
        }
        protected void lnkpreviousschooldetails_Click(object sender, EventArgs e)
        {
            PreviousSchoolDetails();
        }
        protected void lnkofficialdetail_Click(object sender, EventArgs e)
        {
            string studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }

            OfficialDetails();

            string encode = "";

            _sql = "select srno,stenrcode from StudentGenaralDetail where Case When Left('" + studentId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + studentId + "'";
            _sql +=  " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            encode = _oo.ReturnTag(_sql, "stenrcode");

            _sql = " update StudentFamilyDetails set SrNo='" + txtSr.Text + "'  where StEnRCode='" + encode + "'";
            _oo.ProcedureDatabase(_sql);
            _sql = " update StudentGenaralDetail set SrNo='" + txtSr.Text + "'  where StEnRCode='" + encode + "'";
            _oo.ProcedureDatabase(_sql);
            _sql = " update StudentPreviousSchool set SrNo='" + txtSr.Text + "'  where StEnRCode='" + encode + "'";
            _oo.ProcedureDatabase(_sql);
            _sql = " update StudentDocuments set SrNo='" + txtSr.Text + "'  where StEnRCode='" + encode + "'";
            _oo.ProcedureDatabase(_sql);
        }
        protected void lnkDocument_Click(object sender, EventArgs e)
        {
            Document();
        }
        public void PermissionGrant(int xx, LinkButton God)//Formal Arguments
        {


            if (xx == 1)
            {
          
                God.Enabled = true;

            }
            else
            {
           
                God.Enabled = false;
           
            }

        }
        public void CheckValueADDDeleteUpdate()
        {
            _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
            _sql +=  " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
            int a, u, d;
#pragma warning restore 168
            a = Convert.ToInt32(_oo.ReturnTag(_sql, "update1"));
            PermissionGrant(a,LinkButton14);
        }
        protected void lnkPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentPreview.aspx?print=1");
        }
        protected void txtcontfa_TextChanged(object sender, EventArgs e)
        {
       
        }
        protected void txtemailfather_TextChanged(object sender, EventArgs e)
        {
       
        }
        protected void txtcontfa_TextChanged1(object sender, EventArgs e)
        {
            txtcontactNo.Text = txtcontfa.Text;
        }
        protected void txtemailfather_TextChanged1(object sender, EventArgs e)
        {
            txtemailfamily.Text = txtemailfather.Text;
        }

        protected void DrpRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DrpRelationship.SelectedIndex == 0)
            {
                txtguardianname.Text = txtfaNameee.Text;
                txtcontactNo.Text = txtcontfa.Text;
                txtemailfamily.Text = txtemailfather.Text;
                txtfailyincome.Text = txtincomefa.Text;
            }
            else if (DrpRelationship.SelectedIndex == 1)
            {
                txtguardianname.Text = txtmotherNameeee.Text;
                txtcontactNo.Text = txtmothercontact.Text;
                txtemailfamily.Text = txtmotheremail.Text;
                txtfailyincome.Text = txtincomemonthlymother.Text;
            }
            else
            {
                txtguardianname.Text = "";
                txtcontactNo.Text = "";
                txtemailfamily.Text = "";
                txtfailyincome.Text = "0";
                txtguardianname.Focus();
            }
        }

        protected void drpGuardiantwoRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpGuardiantwoRelationship.SelectedIndex == 0)
            {
                txtGuardiantwoName.Text = txtfaNameee.Text;
                txtGuardiantwoContact.Text = txtcontfa.Text;
                txtGuardiantwoEmail.Text = txtemailfather.Text;
                txtGuardiantwoIncomeMonthly.Text = txtincomefa.Text;
            }
            else if (drpGuardiantwoRelationship.SelectedIndex == 1)
            {
                txtGuardiantwoName.Text = txtmotherNameeee.Text;
                txtGuardiantwoContact.Text = txtmothercontact.Text;
                txtGuardiantwoEmail.Text = txtmotheremail.Text;
                txtGuardiantwoIncomeMonthly.Text = txtincomemonthlymother.Text;
            }
            else
            {
                txtGuardiantwoName.Text = "";
                txtGuardiantwoContact.Text = "";
                txtGuardiantwoEmail.Text = "";
                txtGuardiantwoIncomeMonthly.Text = "0";
                txtGuardiantwoName.Focus();
            }

        }

        protected void txtfaNameee_TextChanged(object sender, EventArgs e)
        {
            txtguardianname.Text = txtfaNameee.Text;
        }
    
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Session["aa"] = lnk.Text;
            Response.Redirect(Session["aa"].ToString());
        }
        # endregion

        #region
        protected void loadBoard()
        {
            for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                _sql = "Select BoardName from BoardMaster";
                _oo.FillDropDown(_sql, drpBoard, "BoardName");
            }
        }

        protected DataTable AddColumn()
        {
            DataTable dt = new DataTable();
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

        protected DataRow setInitialValue(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr["exam"] = ""; dr["board"] = ""; dr["result"] = "";
            dr["institute"] = ""; dr["yop"] = "";
            dr["medium"] = ""; dr["subject"] = "";
            dr["rollno"] = ""; dr["certificateno"] = "";
            dr["marksSheetno"] = ""; dr["city"] = ""; dr["state"] = "";
            dr["country"] = ""; dr["maxmarks"] = "";
            dr["obtained"] = ""; dr["per"] = "";

            return dr;
        }

        protected void AddPreviousInstitutionGridRow()
        {
            DataTable dt = AddColumn();

            if (rptPreviousEducation.Items.Count == 0)
            {
                dt.Rows.Add(setInitialValue(dt));
                rptPreviousEducation.DataSource = dt;
                rptPreviousEducation.DataBind();
                loadBoard();
            }
            else
            {
                int i = 0;
                while (i < rptPreviousEducation.Items.Count)
                {
                    DataRow dr = dt.NewRow();
                    Label lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
                    TextBox txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                    DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                    DropDownList drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                    TextBox txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                    TextBox txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                    DropDownList drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                    TextBox txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                    TextBox txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                    TextBox txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                    TextBox txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

                    TextBox txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
                    TextBox txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
                    TextBox txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
                    TextBox txtMM = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                    TextBox txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                    TextBox txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

                    dr["srno"] = lblsrno.Text;
                    dr["exam"] = txtExam.Text;
                    dr["board"] = drpBoard.SelectedValue;
                    dr["result"] = drpResult.SelectedValue;
                    dr["institute"] = txtInstitute.Text;
                    dr["yop"] = txtYop.Text;
                    dr["medium"] = drpMedium.SelectedValue;
                    dr["subject"] = txtSubject.Text;
                    dr["rollno"] = txtRollNo.Text.Trim();
                    dr["certificateno"] = txtCertificateNo.Text.Trim();
                    dr["marksSheetno"] = txtMarksSheetNo.Text.Trim();
                    dr["city"] = txtCity.Text;
                    dr["state"] = txtState.Text;
                    dr["country"] = txtCountry.Text;
                    dr["maxmarks"] = txtMM.Text;
                    dr["obtained"] = txtObtained.Text;
                    dr["per"] = txtPer.Text;
                    dt.Rows.Add(dr);
                    i++;

                    if (i == rptPreviousEducation.Items.Count)
                    {
                        DataRow dr1 = dt.NewRow();
                        dr1[0] = i + 1;
                        dt.Rows.Add(dr1);
                        _sql = "Select BoardName from BoardMaster";
                        _oo.FillDropDown(_sql, drpBoard, "BoardName");
                    }
                }
                rptPreviousEducation.DataSource = dt;
                rptPreviousEducation.DataBind();
                loadBoard();
                setDropdownSelectedValue(dt);
                EnableControl();
            }
        }

        protected void setDropdownSelectedValue(DataTable dt)
        {
            for (int j = 0; j < rptPreviousEducation.Items.Count; j++)
            {
                DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpBoard");
                DropDownList drpResult = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpResult");
                DropDownList drpMedium = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpMedium");
                drpBoard.SelectedValue = dt.Rows[j]["board"].ToString();
                drpResult.SelectedValue = dt.Rows[j]["result"].ToString();
                drpMedium.SelectedValue = dt.Rows[j]["medium"].ToString();
            }
        }

        protected void EnableControl()
        {
            for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                TextBox txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                DropDownList drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                TextBox txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                TextBox txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                DropDownList drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                TextBox txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                TextBox txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                TextBox txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                TextBox txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
                TextBox txtMM = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                TextBox txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                TextBox txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");
                if (txtExam.Text != "")
                {
                    drpBoard.Enabled = true;
                    drpResult.Enabled = true;
                    txtInstitute.Enabled = true;
                    txtYop.Enabled = true;
                    drpMedium.Enabled = true;
                    txtSubject.Enabled = true;
                    txtRollNo.Enabled = true;
                    txtCertificateNo.Enabled = true;
                    txtMarksSheetNo.Enabled = true;
                    txtMM.Enabled = true;
                    txtObtained.Enabled = true;
                    txtPer.Enabled = true;
                }
            }
        }

        protected void lnkAddMore_Click(object sender, EventArgs e)
        {
            AddPreviousInstitutionGridRow();
        }

        protected void DeletePreviousInstitutionGridRow(int rowindex)
        {
            DataTable dt = AddColumn();

            int i = 0;
            while (i < rptPreviousEducation.Items.Count)
            {
                DataRow dr = dt.NewRow();
                Label lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
                TextBox txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                DropDownList drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                TextBox txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                TextBox txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                DropDownList drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                TextBox txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                TextBox txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                TextBox txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                TextBox txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

                TextBox txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
                TextBox txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
                TextBox txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
                TextBox txtMM = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                TextBox txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                TextBox txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

                dr["srno"] = lblsrno.Text;
                dr["exam"] = txtExam.Text;
                dr["board"] = drpBoard.SelectedValue;
                dr["result"] = drpResult.SelectedValue;
                dr["institute"] = txtInstitute.Text;
                dr["yop"] = txtYop.Text;
                dr["medium"] = drpMedium.SelectedValue;
                dr["subject"] = txtSubject.Text;
                dr["rollno"] = txtRollNo.Text.Trim();
                dr["certificateno"] = txtCertificateNo.Text.Trim();
                dr["marksSheetno"] = txtMarksSheetNo.Text.Trim();
                dr["city"] = txtCity.Text;
                dr["state"] = txtState.Text;
                dr["country"] = txtCountry.Text;
                dr["maxmarks"] = txtMM.Text;
                dr["obtained"] = txtObtained.Text;
                dr["per"] = txtPer.Text;
                dt.Rows.Add(dr);
                i++;
            }

            dt.Rows.RemoveAt(rowindex);

            rptPreviousEducation.DataSource = dt;
            rptPreviousEducation.DataBind();
            loadBoard();
            reIndexingofSrNo();
            setDropdownSelectedValue(dt);
            EnableControl();
        }

        protected void reIndexingofSrNo()
        {
            for (int j = 0; j < rptPreviousEducation.Items.Count; j++)
            {
                Label lblsrno = (Label)rptPreviousEducation.Items[j].FindControl("lblsrno");
                lblsrno.Text = (j + 1).ToString();
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem currntrow = (RepeaterItem)lnk.NamingContainer;

            int i = currntrow.ItemIndex;

            DeletePreviousInstitutionGridRow(i);

        }

        private void loadCourse()
        {
            _sql = "Select CourseName,Id from CourseMaster where SessionName='" + Session["SessionName"].ToString() + "'";
            _oo.FillDropDown_withValue(_sql, DropCourse, "CourseName", "Id");
            DropCourse.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }

        private void loadBranch()
        {
            _sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"].ToString() + "' and Course='" + DropCourse.SelectedValue.ToString() + "' and ClassId='" + DropAdmissionClass.SelectedValue.ToString() + "'";
            _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
            DropBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

        private void loadStream()
        {
            _sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + DropAdmissionClass.SelectedValue.ToString() + "' and BranchId='" + DropBranch.SelectedValue.ToString() + "'";
            _oo.FillDropDown_withValue(_sql, DropStream, "Stream", "Id");
            DropStream.Items.Insert(0, new ListItem("<--Select Stream-->", "0"));
        }

        private void loadOptionalSubject()
        {
            _sql = "Select SubjectGroup,id from SubjectGroupMaster where Classid='" + DropAdmissionClass.SelectedValue.ToString() + "' and Branchid='" + DropBranch.SelectedValue.ToString() + "'  and SectionName='" + DropSection.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsCompulsory=0";
            rbOptionalSubject.DataSource = _oo.GridFill(_sql);
            rbOptionalSubject.DataTextField = "SubjectGroup";
            rbOptionalSubject.DataValueField = "id";
            rbOptionalSubject.DataBind();
            if (rbOptionalSubject.Items.Count > 0)
            {
                divopt.Visible = true;
            }
            else
            {
                divopt.Visible = false;
            }
        }

        private void loadClass()
        {
            _sql = "Select Id,ClassName from ClassMaster";
            _sql +=  " where (Course='" + DropCourse.SelectedValue.ToString() + "' or Course is NULL) and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CIDOrder !=0 ";
            _oo.FillDropDown_withValue(_sql, DropAdmissionClass, "ClassName", "Id");
            DropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }

        protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string values = DropAdmissionClass.SelectedValue;
            loadClass();
            DropAdmissionClass.SelectedValue = values;
            loadStream();
            loadOptionalSubject();

        }
        protected void DropCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClass();
        }
        #endregion
 
        [WebMethod]
        public static List<string> getAgeofStudent(string date1, string date2)
        {
            SqlConnection con = new SqlConnection();
            con = BAL.objBal.dbGet_connection();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AgeCalculaterProc";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            List<string> datepart = new List<string>();

            if (date1 != string.Empty)
            {
                date1 = "1990/01/01";
            }

            if (date2 != string.Empty)
            {
                date2 = "1990/01/01";
            }

            cmd.Parameters.AddWithValue("@DOB", date2);
            cmd.Parameters.AddWithValue("@DOB1", date1);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);

            datepart.Add(dt.Rows[0][0].ToString());
            datepart.Add(dt.Rows[0][1].ToString());
            datepart.Add(dt.Rows[0][2].ToString());
        
            return datepart;
        }
   
        protected void drpG1State_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(drpG1City, drpG1State);
        }
        protected void drpG2State_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(drpG2City, drpG2State);
        }
        protected void lnkQuickReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/StudentQuickRegistrationUpdation.aspx?check=common/update_student_registration");
        }
        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadOptionalSubject();
        }

        //Get ALL Exam Records
        public void loadEntranceExamName()
        {
            DataSet ds = new DataSet();

            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@QueryFor", "S"));
            param.Add(new SqlParameter("@Id", "-1"));

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EntranceExamMaster_Proc", param);

            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    drpExamCrackedof.DataSource = dt;
                    drpExamCrackedof.DataTextField = "ExamName";
                    drpExamCrackedof.DataValueField = "Id";
                    drpExamCrackedof.DataBind();
                }
                else
                {
                    drpExamCrackedof.DataSource = null;
                    drpExamCrackedof.DataBind();
                }
            }
        }

        public void setEntranceExamRecord()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ExamId", drpExamCrackedof.SelectedValue.ToString()));
            param.Add(new SqlParameter("@RollNo", txtRollNo.Text.Trim()));
            param.Add(new SqlParameter("@Rank", txtRank.Text.Trim()));
            param.Add(new SqlParameter("@CatRank", txtCategoryRank.Text.Trim()));
            param.Add(new SqlParameter("@AnyOther", txtAnyOtherCategoryRank.Text.Trim()));
            param.Add(new SqlParameter("@srno", Session["srno"].ToString()));
            param.Add(new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));

            DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentEntranceExamDetails_PROC", param);

        }

        public void getEntranceExamRecord()
        {
            DataSet ds = new DataSet();
        
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QueryFor","S"));
            param.Add(new SqlParameter("@srno", Session["srno"].ToString()));

            ds=DLL.objDll.Sp_SelectRecord_usingExecuteDataset("StudentEntranceExamDetails_PROC", param);

            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        drpExamCrackedof.SelectedValue = dt.Rows[0][0].ToString();
                        txtRollNo.Text = dt.Rows[0][1].ToString();
                        txtRank.Text = dt.Rows[0][2].ToString();
                        txtCategoryRank.Text = dt.Rows[0][3].ToString();
                        txtAnyOtherCategoryRank.Text = dt.Rows[0][4].ToString();
                    }
                    catch
                    {
                    }
                }

            }
        }

        private void loadClass(DropDownList drpclassforStaff)
        {
            if (Session["logintype"].ToString() == "Admin")
            {
                _sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
                _sql +=  " where cm.SessionName='" + Session["SessionName"] + "' order by cidorder asc ";
                _dt = DAL.DALInstance.GetValueInTable(_sql);
                drpClassforStaff.DataSource = _dt = DAL.DALInstance.GetValueInTable(_sql);
                drpClassforStaff.DataTextField = "ClassName";
                drpClassforStaff.DataValueField = "Id";
                drpClassforStaff.DataBind();
                drpClassforStaff.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
            else
            {
                _sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1 ";
                _sql +=  "inner join ClassMaster cm on cm.Id = T1.ClassId and cm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"] + "' ";
                _sql +=  "and cm.SessionName = T1.SessionName and cm.SessionName = '" + Session["SessionName"] + "' and EmpCode = '" + Session["LoginName"].ToString() + "' ";
                _sql +=  " where cm.SessionName='" + Session["SessionName"] + "'  order by cidorder asc  ";

                drpClassforStaff.DataSource = _dt = DAL.DALInstance.GetValueInTable(_sql);
                drpClassforStaff.DataTextField = "ClassName";
                drpClassforStaff.DataValueField = "Id";
                drpClassforStaff.DataBind();
                drpClassforStaff.Items.Insert(0, new ListItem("<--Select-->", "0"));  
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
                _sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
                _sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
                _sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
                _sql +=  " and t1.Classid=" + drpclass.SelectedValue.ToString() + "";
                BAL.objBal.FillDropDown_withValue(_sql, drpsection, "SectionName", "Id");
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
                _sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
                _sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
                _sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
                _sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drpbranch, "BranchName", "Id");
            }
        }

        protected void drpClassforStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSection(drpSectionforStaff, drpClassforStaff);
            loadBranch(drpBranchforStaff, drpClassforStaff);

            LoadDropDown();
            //LoadGid();
        }

        protected void drpSectionforStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDropDown();
            //LoadGid();
        }

        protected void drpBranchforStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDropDown();
            //LoadGid();
        }

        protected void drpSrno_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtEnter.Text = drpSrno.SelectedValue.ToString();
            ShowDetails();
            if (drpSrno.SelectedIndex == 0)
            {
                TxtEnter.Text = "";
            }
        }

        private DataTable getStudentsRecordinDropDown()
        {
            DataTable dt = null;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ClassName", drpClassforStaff.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@SectionName", drpSectionforStaff.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@BranchName", drpBranchforStaff.SelectedItem.Text.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds = new DataSet();

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);

            if (ds != null)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dt = dv.ToTable(true, "S.R.No.", "Name", "Father's Name", "Class", "Section", "Branch", "Medium", "Status", "SRNOWITHNAME");

                dt.Columns["S.R.No."].ColumnName = "srno";
                dt.Columns["Father's Name"].ColumnName = "FatherName";
                //dt.Columns["Transport Facility"].ColumnName = "TransportFacility";
            }

            return dt;
        }

        private void LoadDropDown()
        {
            drpSrno.DataSource = getStudentsRecordinDropDown();
            drpSrno.DataTextField = "SRNOWITHNAME";
            drpSrno.DataValueField = "srno";
            drpSrno.DataBind();
            BAL.objBal.fillSelectvalue(drpSrno, "<--Select-->", "<--Select-->");
            drpSrno.SelectedIndex = 0;
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            ShowDetails();
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}