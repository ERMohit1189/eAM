using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminLisOfAllStudent : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = String.Empty;

        public AdminLisOfAllStudent()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // drpSession.Focus();

            _con = _oo.dbGet_connection();
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);
            BLL.BLLInstance.LoadHeader("Report", header1);
            if (!IsPostBack)
            {
                Label1.Text = "";
                Label15.Text = "";
                if (rptType.SelectedValue == "Class wise Strength")
                {
                    TypeofAdmission.Visible = true;
                    Medium.Visible = true;
                    FeeCategory.Visible = true;
                    Shift.Visible = true;
                    EducationAct.Visible = true;
                    Status.Visible = true;
                }
                LoadpreviousEducationMaster();
                LoadPreviousBoard();
                LoadpreviousQualificationMaster();
                LoadOccuptionMaster();
                LoadDefaultMotherOccu();
                LoadFeeGroup();
                LoadSession();
                LoadCourse();
                if (Session["LoginType"].ToString().ToLower() == "staff")
                {
                    rptType.SelectedIndex = 1;
                    rptType.Visible = false;
                    //  drpSession.Enabled = false;
                    TypeofAdmission.Visible = true;
                    Medium.Visible = true;
                    FeeCategory.Visible = true;
                    Shift.Visible = true;
                    EducationAct.Visible = true;
                    Status.Visible = true;
                    Course.Visible = true;
                    Class.Visible = true;
                    Section.Visible = true;
                    Stream.Visible = true;
                    Group.Visible = true;
                    TypeofEducation.Visible = true;
                    Board.Visible = true;
                    Gender.Visible = true;
                    Category.Visible = true;
                    Religion.Visible = true;
                    Country.Visible = true;
                    State.Visible = true;
                    City.Visible = true;
                    DisplayOrder.Visible = true;
                    moreIcon.Visible = true;
                    drpCourse.Enabled = false;
                    string sql1 = "select ClassId, SectionId, BranchId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "'";
                    if (_oo.Duplicate(sql1))
                    {
                        string ss = "select course from ClassMaster where Id=" + _oo.ReturnTag(sql1, "ClassId") + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                        drpCourse.SelectedValue = _oo.ReturnTag(ss, "course");
                        string sql = "Select id, ClassName from ClassMaster  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and course=" + drpCourse.SelectedValue + "";
                        sql += " and id in (select ClassId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1) order by Id";
                        _oo.FillDropDown_withValue(sql, drpClass, "ClassName", "id");
                        drpClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
                        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
                        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
                        drpStream.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                    }
                    else
                    {
                        drpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
                        msgbox.InnerText = "Please allot class teacher first!";
                    }
                }
                else
                {
                    LoadClass();
                    drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                    drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                    drpStream.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
                }
                LoadHouse();
                LoadMedium();
                LoadCategory();
                LoadBoard();
                LoadShift();
                LoadEducationAct();
                LoadDefaultReligion();

                abc.Visible = false;

                LoadCountry(ddlCountry);
                var objBll = new BLL();
                objBll.loadDefaultvalue("Country", ddlCountry);
                LoadState(ddlState, ddlCountry);
                LoadCity(ddlCity, ddlState);
                _oo.AddDateMonthYearDropDown(DDYear, DDMonth, DDDate);
                _oo.FindCurrentDateandSetinDropDown(DDYear, DDMonth, DDDate);

                _oo.AddDateMonthYearDropDown(DDYearTo, DDMonthTo, DDDateTo);
                _oo.FindCurrentDateandSetinDropDown(DDYearTo, DDMonthTo, DDDateTo);

                _oo.AddDateMonthYearDropDown(DDYear2, DDMonth2, DDDate2);
                _oo.FindCurrentDateandSetinDropDown(DDYear2, DDMonth2, DDDate2);

                _oo.AddDateMonthYearDropDown(DDYearTo2, DDMonthTo2, DDDateTo2);
                _oo.FindCurrentDateandSetinDropDown(DDYearTo2, DDMonthTo2, DDDateTo2);
                string gtDate = "select top 1 case when convert(int, format(DateOfAdmiission,'dd'))<10 then convert(int, format(DateOfAdmiission,'dd')) else format(DateOfAdmiission,'dd') end dd, format(DateOfAdmiission, 'MMM')MMM, format(DateOfAdmiission, 'yyyy')yyyy from StudentOfficialDetails where SessionName = '" + Session["SessionName"] + "' and BranchCode = '" + Session["BranchCode"] + "' order by convert(date, DateOfAdmiission) asc";
                DDDate2.SelectedValue = _oo.ReturnTag(gtDate, "dd").ToString();
                DDMonth2.SelectedValue = _oo.ReturnTag(gtDate, "MMM").ToString();
                DDYear2.SelectedValue = _oo.ReturnTag(gtDate, "yyyy").ToString();
                if (Request.QueryString.AllKeys.Length > 0 && Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].ToString() == "1")
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        GridView1.Visible = false;
                        GridView2.Visible = true;
                        GridView1.Visible = false;
                        GridView2.Visible = true;
                        abc.Visible = true;
                        listdisplay.Visible = true;
                        Loadgrid1();
                        TextTrnsform();
                    }
                }
                else
                {
                    DDDate.SelectedValue = _oo.ReturnTag(gtDate, "dd").ToString();
                    DDMonth.SelectedValue = _oo.ReturnTag(gtDate, "MMM").ToString();
                    DDYear.SelectedValue = _oo.ReturnTag(gtDate, "yyyy").ToString();
                }
            }
            /////----------

        }

        protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYear, DDMonth, DDDate);
        }
        protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYear, DDMonth, DDDate);
        }
        protected void DDDate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DDYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DDYear2, DDMonth2, DDDate2);
        }
        protected void DDMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DDYear2, DDMonth2, DDDate2);
        }
        protected void DDDate2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            if (rptType.SelectedValue == "Class wise Strength")
            {
                searchDiv.Visible = false;
                drpSearchBy.SelectedValue = "";
                txtSearch.Text = "";
                TypeofAdmission.Visible = true;
                Medium.Visible = true;
                FeeCategory.Visible = true;
                Shift.Visible = true;
                EducationAct.Visible = true;
                Status.Visible = true;

                Course.Visible = true;
                Class.Visible = false;
                Section.Visible = false;
                Stream.Visible = false;
                Group.Visible = false;
                TypeofEducation.Visible = false;
                Board.Visible = false;
                Gender.Visible = false;
                Category.Visible = false;
                Religion.Visible = false;
                Country.Visible = false;
                State.Visible = false;
                City.Visible = false;
                DisplayOrder.Visible = false;
                moreIcon.Visible = false;

                dtFrom1.Visible = true;
                dtTo1.Visible = true;
                dtFrom2.Visible = false;
                dtTo2.Visible = false;
                PreviousEducationBoardUniversity.Visible = false;
                previousEducationMaster.Visible = false;
                FatherQualification.Visible = false;
                MotherQualification.Visible = false;
                FatherOccuption.Visible = false;
                MotherOccuption.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                searchDiv.Visible = true;
                drpSearchBy.SelectedValue = "";
                txtSearch.Text = "";
                TypeofAdmission.Visible = true;
                Medium.Visible = true;
                FeeCategory.Visible = true;
                Shift.Visible = true;
                EducationAct.Visible = true;
                Status.Visible = true;
                Course.Visible = true;
                Class.Visible = true;
                Section.Visible = true;
                Stream.Visible = true;
                Group.Visible = true;
                TypeofEducation.Visible = true;
                Board.Visible = true;
                Gender.Visible = true;
                Category.Visible = true;
                Religion.Visible = true;
                Country.Visible = true;
                State.Visible = true;
                City.Visible = true;
                DisplayOrder.Visible = true;
                moreIcon.Visible = true;
                dtFrom1.Visible = false;
                dtTo1.Visible = false;
                dtFrom2.Visible = true;
                dtTo2.Visible = true;
                PreviousEducationBoardUniversity.Visible = true;
                previousEducationMaster.Visible = true;
                FatherQualification.Visible = true;
                MotherQualification.Visible = true;
                FatherOccuption.Visible = true;
                MotherOccuption.Visible = true;
            }
            listdisplay.Visible = false;
        }
        private void LoadDefaultReligion()
        {
            _sql = "select ReligionName from ReligionMaster";
            _oo.FillDropDown_withValue(_sql, DropDownList1, "ReligionName", "ReligionName");
            DropDownList1.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        private void LoadShift()
        {
            _sql = "select * from StudentShiftMaster where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlShift, "ShiftName", "ID");
            ddlShift.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        private void LoadpreviousEducationMaster()
        {
            _sql = "Select Medium,id from MediumMaster where BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, ddlpreviousEducationMaster, "Medium", "id");
            ddlpreviousEducationMaster.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        protected void LoadPreviousBoard()
        {
            _sql = "Select BoardName from BoardMaster where branchcode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, ddlPreviousEducationBoardUniversity, "BoardName");
            ddlPreviousEducationBoardUniversity.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        private void LoadpreviousQualificationMaster()
        {
            _sql = "Select ID,QualificationName from ParentsQualificationMaster ";
            BAL.objBal.FillDropDown_withValue(_sql, ddlFatherQualification, "QualificationName", "ID");
            ddlFatherQualification.Items.Insert(0, new ListItem("<--Select-->", "0"));
            BAL.objBal.FillDropDown_withValue(_sql, ddlMotherQualification, "QualificationName", "ID");
            ddlMotherQualification.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        private void LoadOccuptionMaster()
        {
            _sql = "Select DesignationName from GuardianDesMaster where DesignationName not like 'House%'";
            _oo.FillDropDownWithOutSelect(_sql, ddlFatherOccuption, "DesignationName");
            ddlFatherOccuption.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            //_oo.FillDropDownWithOutSelect(_sql, ddlMotherOccuption, "DesignationName");
            //ddlMotherOccuption.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        private void LoadDefaultMotherOccu()
        {
            _sql = "Select DesignationName from GuardianDesMaster";
            _oo.FillDropDownWithOutSelect(_sql, ddlMotherOccuption, "DesignationName");
            ddlMotherOccuption.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            //using (var objBll = new BLL())
            //{
            //    try
            //    {
            //        objBll.loadDefaultvalue("Occupation", ddlMotherOccuption);
            //    }
            //    catch
            //    {
            //        // ignored
            //    }
            //}
        }
        private void LoadFeeGroup()
        {
            _sql = "select id, FeeGroupName from FeeGroupMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, drpFeegroup, "FeeGroupName");
            drpFeegroup.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        private void LoadEducationAct()
        {
            //  _sql = "select * from tblEducationAct where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + "";
            _sql = "select  actName,ID from tblEducationAct where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlEducationAct, "actName", "ID");
            ddlEducationAct.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        private void LoadHouse()
        {
            //  _sql = "select * from tblEducationAct where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + "";
            //_sql = "select Distinct actName,ID from tblEducationAct ";
            _sql = "select   HouseName from HouseMaster where BranchCode=" + Session["BranchCode"] + "";
            //_oo.FillDropDown_withValue(_sql, ddlEducationAct, "actName", "ID");
            //ddlEducationAct.Items.Insert(0, new ListItem("<--Select-->", "0"));
            _oo.FillDropDownWithOutSelect(_sql, ddlHouse, "HouseName");
            ddlHouse.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        private void LoadCountry(DropDownList drp)
        {
            drp.Items.Clear();
            _sql = "Select CountryName,Id from CountryMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
            drp.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        private void LoadState(DropDownList drp, DropDownList drpValue)
        {
            drp.Items.Clear();
            _sql = "Select count(*) cnt from StateMaster where countryId='" + drpValue.SelectedValue + "'";
            if (_oo.ReturnTag(_sql, "cnt") == "0")
            {
                drp.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
            else
            {
                _sql = "Select StateName,Id from StateMaster  where countryId='" + drpValue.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
                drp.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
        }
        private void LoadCity(DropDownList drp, DropDownList drpValue)
        {
            drp.Items.Clear();
            _sql = "Select count(*) cnt from CityMaster where StateId='" + drpValue.SelectedValue + "'";
            if (_oo.ReturnTag(_sql, "cnt") == "0")
            {
                drp.Items.Insert(0, new ListItem("<--Select-->", ""));
            }
            else
            {
                _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
                BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
                drp.Items.Insert(0, new ListItem("<--Select-->", ""));
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
            value = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "textTransform", "finalsubmit('" + value + "')", true);
        }

        protected void LoadBoard()
        {
            _sql = "Select BoardName from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, drpBoard, "BoardName");
            drpBoard.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

        }

        protected void LoadCourse()
        {
            _sql = "Select CourseName,Id from CourseMaster where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpCourse, "CourseName", "Id");
            drpCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            //_oo.FillDropDown_withValue(_sql, ddlCourse, "CourseName", "Id");
            //ddlCourse.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

        }

        protected void LoadSession()
        {
            _sql = "select SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + "";
            _sql += " and BranchCode=" + Session["BranchCode"] + "";
            // _oo.FillDropDown(_sql, drpSession, "SessionName");
            // drpSession.Text = Session["SessionName"].ToString();
        }

        protected void LoadClass()
        {
            _sql = "Select ClassName,Id from ClassMaster";
            _sql += "  where (Course='" + drpCourse.SelectedValue + "' or Course is null) and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _sql += " order by CIDOrder";
            _oo.FillDropDown_withValue(_sql, drpClass, "ClassName", "Id");
            drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }

        protected void LoadBranch()
        {
            if (Session["LoginType"].ToString().ToLower() == "staff")
            {
                _sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue;
                _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select BranchId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1)";
                _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "Id");
                drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            }
            else
            {
                _sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue;
                _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "Id");
                drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            }
        }

        private void LoadStream()
        {
            _sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"] + "' and ClassId='" + drpClass.SelectedValue + "' and BranchId='" + drpBranch.SelectedValue + "'";
            _oo.FillDropDown_withValue(_sql, drpStream, "Stream", "Id");
            drpStream.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }

        protected void LoadSection()
        {
            if (Session["LoginType"].ToString().ToLower() == "staff")
            {
                _sql = "Select SectionName from SectionMaster where ClassNameId=" + drpClass.SelectedValue;
                _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select SectionId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1)";
                _oo.FillDropDown(_sql, drpSection, "SectionName");
                drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            }
            else
            {
                _sql = "Select SectionName from SectionMaster where ClassNameId=" + drpClass.SelectedValue;
                _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                _oo.FillDropDown(_sql, drpSection, "SectionName");
                drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            }
        }

        protected void LoadMedium()
        {
            _sql = "select Medium from MediumMaster where BranchCode=" + Session["BranchCode"] + "";
            //_sql = "select  Medium from MediumMaster ";
            _oo.FillDropDown(_sql, drpMedium, "Medium");
            drpMedium.Items.Insert(0, new ListItem("<--Select-->"));
        }

        protected void LoadCategory()
        {
            _sql = "Select CasteName from CasteMaster";
            _oo.FillDropDown(_sql, drpCategory, "CasteName");
            drpCategory.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
            LoadSection();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (rptType.SelectedValue == "Class wise Strength")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView1.Visible = false;
                GridView2.Visible = true;
                Loadgrid1();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.Visible = false;
                GridView1.Visible = true;
                GridView2.Visible = false;
                GridView1.Visible = true;
                Loadgrid();
            }

            TextTrnsform();
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (rptType.SelectedValue == "Class wise Strength")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView1.Visible = false;
                GridView2.Visible = true;
                Loadgrid1();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.Visible = false;
                GridView1.Visible = true;
                GridView2.Visible = false;
                GridView1.Visible = true;
                Loadgrid();
            }

            TextTrnsform();
        }
        protected void Loadgrid1()
        {
            Label1.Text = "";
            Label15.Text = "";
            Label1.Text = "CLASS WISE STUDENT'S STRENGTH FOR SESSION " + Session["SessionName"].ToString();

            if (drpType.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + "Type of Admission : " + drpType.SelectedItem.Text.Trim() + "";
            }
            if (drpMedium.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + (drpType.SelectedIndex != 0 ? " | Medium : " : " Medium : ") + drpMedium.SelectedItem.Text.Trim() + "";
            }

            if (drpFeegroup.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + (drpType.SelectedIndex != 0 || drpMedium.SelectedIndex != 0 ? " | Fee Category : " : " Fee Category : ") + drpFeegroup.SelectedItem.Text.Trim() + "";
            }
            if (drpStatus.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + (drpType.SelectedIndex != 0 || drpMedium.SelectedIndex != 0 || drpFeegroup.SelectedIndex != 0 ? " | Status : " : " Status : ") + drpStatus.SelectedItem.Text.Trim() + "";
            }
            if (ddlShift.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + (drpType.SelectedIndex != 0 || drpMedium.SelectedIndex != 0 || drpFeegroup.SelectedIndex != 0 || drpStatus.SelectedIndex != 0 ? " | Shift : " : " Shift : ") + ddlShift.SelectedItem.Text.Trim() + "";
            }
            if (ddlEducationAct.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + (drpType.SelectedIndex != 0 || drpMedium.SelectedIndex != 0 || drpFeegroup.SelectedIndex != 0 || drpStatus.SelectedIndex != 0 || ddlEducationAct.SelectedIndex != 0 ? " | Education Act : " : " Education Act : ") + ddlEducationAct.SelectedItem.Text.Trim() + "";
            }
            _sql = "select getdate() as Date";
            lblPrintDate.Text = "Date : " + DateTime.Parse(_oo.ReturnTag(_sql, "Date")).ToString("dd MMM yyyy hh:mm:ss tt");
            listdisplay.Visible = true;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            if (drpType.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@TypeOfAdmission", drpType.SelectedItem.Text));
            }
            if (drpMedium.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@Medium", drpMedium.SelectedItem.Text));
            }
            if (drpFeegroup.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@FeeCategoryId", drpFeegroup.SelectedItem.Text));
            }
            if (ddlShift.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@ShiftId", ddlShift.SelectedValue));
            }
            if (ddlEducationAct.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@EducationActId", ddlEducationAct.SelectedValue));
            }
            if (drpStatus.SelectedValue != "")
            {
                param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
            }
            if (drpCourse.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@CourseID", drpCourse.SelectedValue));
            }
            if (ddlHouse.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@HouseName", ddlHouse.SelectedValue));
            }

            var fromDate = DDYear.SelectedItem + " " + DDMonth.SelectedItem + " " + DDDate.SelectedItem;
            var toDate = DDYearTo.SelectedItem + " " + DDMonthTo.SelectedItem + " " + DDDateTo.SelectedItem;
            param.Add(new SqlParameter("@fromDate", fromDate));
            param.Add(new SqlParameter("@toDate", toDate));
            DataTable dt;
            DataSet ds;
            ds = new DLL().Sp_SelectRecord_usingExecuteDataset("ListOfStrength", param);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    listdisplay.Visible = true;
                    abc.Visible = true;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    int malesC = 0, femalesC = 0, transgenderC = 0, totalC = 0;
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        Label males = (Label)GridView2.Rows[i].FindControl("males");
                        malesC = malesC + int.Parse(males.Text);
                        Label females = (Label)GridView2.Rows[i].FindControl("females");
                        femalesC = femalesC + int.Parse(females.Text);
                        Label Transgender = (Label)GridView2.Rows[i].FindControl("Transgender");
                        transgenderC = transgenderC + int.Parse(Transgender.Text);
                        Label lblTotal = (Label)GridView2.Rows[i].FindControl("lblTotal");
                        totalC = totalC + int.Parse(lblTotal.Text);
                    }
                    Label maleTotal = (Label)GridView2.FooterRow.FindControl("maleTotal");
                    Label femaleTotal = (Label)GridView2.FooterRow.FindControl("femaleTotal");
                    Label transTotal = (Label)GridView2.FooterRow.FindControl("transTotal");
                    Label GrandTotal = (Label)GridView2.FooterRow.FindControl("GrandTotal");
                    maleTotal.Text = malesC.ToString();
                    femaleTotal.Text = femalesC.ToString();
                    transTotal.Text = transgenderC.ToString();
                    GrandTotal.Text = totalC.ToString();
                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        if (malesC == 0)
                        {
                            if (i == 0)
                            {
                                GridView2.HeaderRow.Cells[2].Visible = false;
                                GridView2.FooterRow.Cells[2].Visible = false;
                            }
                            GridView2.Rows[i].Cells[2].Visible = false;
                        }
                        if (femalesC == 0)
                        {
                            if (i == 0)
                            {
                                GridView2.HeaderRow.Cells[3].Visible = false;
                                GridView2.FooterRow.Cells[3].Visible = false;
                            }
                            GridView2.Rows[i].Cells[3].Visible = false;
                        }
                        if (transgenderC == 0)
                        {
                            if (i == 0)
                            {
                                GridView2.HeaderRow.Cells[4].Visible = false;
                                GridView2.FooterRow.Cells[4].Visible = false;
                            }
                            GridView2.Rows[i].Cells[4].Visible = false;
                        }
                    }
                }
                else
                {
                    abc.Visible = false;
                    listdisplay.Visible = false;
                }
            }
            else
            {
                abc.Visible = false;
                listdisplay.Visible = false;
                _oo.MessageBoxforUpdatePanel("Sorry, No Record&apos;s found", Page);
            }
        }
        protected void Loadgrid()
        {
            Label15.Text = "";
            Label1.Text = "LIST OF STUDENTS FOR SESSION " + Session["SessionName"].ToString();

            if (drpCourse.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + "Course : " + drpCourse.SelectedItem.Text.Trim() + "";
            }
            if (drpClass.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Class : " + drpClass.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Class : " + drpClass.SelectedItem.Text.Trim() + "";
                }

            }
            if (drpBranch.SelectedIndex != 0 && drpBranch.SelectedItem.Text.Trim() != "N/A" && drpBranch.SelectedItem.Text.Trim() != "NA" && drpBranch.SelectedItem.Text.Trim() != "N")
            {
                Label15.Text = Label15.Text + "  " + drpBranch.SelectedItem.Text.Trim() + "";
            }
            if (drpSection.SelectedIndex != 0)
            {
                Label15.Text = Label15.Text + " (" + drpSection.SelectedItem.Text.Trim() + ")";
            }

            if (drpStream.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Group : " + drpStream.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Group : " + drpStream.SelectedItem.Text.Trim() + "";
                }
            }
            if (drpMedium.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Medium : " + drpMedium.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Medium : " + drpMedium.SelectedItem.Text.Trim() + "";
                }
            }
            if (drpCategory.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Category : " + drpCategory.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Category : " + drpCategory.SelectedItem.Text.Trim() + "";
                }

            }
            if (drpFeegroup.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Fee Category : " + drpFeegroup.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Fee Category : " + drpFeegroup.SelectedItem.Text.Trim() + "";
                }

            }
            if (ddlShift.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Shift : " + ddlShift.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Shift : " + ddlShift.SelectedItem.Text.Trim() + "";
                }

            }
            if (ddlEducationAct.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Education Act : " + ddlEducationAct.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Education Act : " + ddlEducationAct.SelectedItem.Text.Trim() + "";
                }

            }
            if (drpType.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Type of Admission : " + drpType.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Type of Admission : " + drpType.SelectedItem.Text.Trim() + "";
                }

            }
            if (RadioButtonList1.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Gender :  " + RadioButtonList1.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Gender :  " + RadioButtonList1.SelectedItem.Text.Trim() + "";
                }

            }
            if (drpStatus.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Status :  " + drpStatus.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Status :  " + drpStatus.SelectedItem.Text.Trim() + "";
                }

            }
            if (drpBoard.SelectedIndex != 0)
            {
                if (Label15.Text != "")
                {
                    Label15.Text = Label15.Text + " | Board :  " + drpBoard.SelectedItem.Text.Trim() + "";
                }
                else
                {
                    Label15.Text = "Board :  " + drpBoard.SelectedItem.Text.Trim() + "";
                }

            }

            _sql = "select getdate() as Date";
            lblPrintDate.Text = "Date : " + DateTime.Parse(_oo.ReturnTag(_sql, "Date")).ToString("dd MMM yyyy hh:mm:ss tt");
            listdisplay.Visible = true;

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@FeeGroup", drpFeegroup.SelectedItem.Text));
            param.Add(new SqlParameter("@CourseName", drpCourse.SelectedItem.Text));
            param.Add(new SqlParameter("@ClassName", drpClass.SelectedItem.Text));
            param.Add(new SqlParameter("@SectionName", drpSection.SelectedItem.Text));
            if (drpBranch.SelectedValue != "<--Select-->")
            {
                param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
            }
            param.Add(new SqlParameter("@Stream", drpStream.SelectedItem.Text));
            param.Add(new SqlParameter("@TypeOFAdmision", drpType.SelectedItem.Text));
            param.Add(new SqlParameter("@Medium", drpMedium.SelectedItem.Text));
            if (drpStatus.SelectedValue != "")
            {
                param.Add(new SqlParameter("@Withdrwal", drpStatus.SelectedValue));
            }
            if (drpCategory.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@Category", drpCategory.SelectedItem.Text));
            }
            param.Add(new SqlParameter("@Board", drpBoard.SelectedItem.Text));
            param.Add(new SqlParameter("@Gender", RadioButtonList1.SelectedItem.Text.ToUpper()));
            param.Add(new SqlParameter("@DisplayOrders", RadioButtonList2.SelectedValue));

            if (ddlCountry.SelectedValue != "")
            {
                param.Add(new SqlParameter("@CountryId", ddlCountry.SelectedValue));
            }
            if (ddlState.SelectedValue != "")
            {
                param.Add(new SqlParameter("@StateId", ddlState.SelectedValue));
            }
            if (ddlCity.SelectedValue != "")
            {
                param.Add(new SqlParameter("@CityId", ddlCity.SelectedValue));
            }
            if (ddlShift.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@shiftId", ddlShift.SelectedValue));
            }
            if (ddlEducationAct.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@EducationActId", ddlEducationAct.SelectedValue));
            }
            if (ddlTypeofEducation.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@TypeofEducation", ddlTypeofEducation.SelectedValue));
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@Religion", DropDownList1.SelectedValue));
            }
            if (drpSearchBy.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@SearchBy", drpSearchBy.SelectedValue));
            }
            param.Add(new SqlParameter("@SearchText", txtSearch.Text.Trim()));
            string CSVColumnList = GetCheckBoxListSelections();
            param.Add(new SqlParameter("@CSVColumnList", CSVColumnList));
            var fromDate = DDYear2.SelectedItem + " " + DDMonth2.SelectedItem + " " + DDDate2.SelectedItem;
            var toDate = DDYearTo2.SelectedItem + " " + DDMonthTo2.SelectedItem + " " + DDDateTo2.SelectedItem;
            param.Add(new SqlParameter("@fromDate", fromDate));
            param.Add(new SqlParameter("@toDate", toDate));
            if (ddlHouse.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@HouseName", ddlHouse.SelectedValue));
            }
            if (ddlpreviousEducationMaster.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@PreviousEducationMedium", ddlpreviousEducationMaster.SelectedValue));
            }
            if (ddlPreviousEducationBoardUniversity.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@PreviousEducationBoard", ddlPreviousEducationBoardUniversity.SelectedValue));
            }
            if (ddlFatherQualification.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@FatherQualification", ddlFatherQualification.SelectedValue));
            }
            if (ddlMotherQualification.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@MotherQualification", ddlMotherQualification.SelectedValue));
            }
            if (ddlFatherOccuption.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@FatherOccuption", ddlFatherOccuption.SelectedValue));
            }
            if (ddlMotherOccuption.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@MotherOccuption", ddlMotherOccuption.SelectedValue));
            }
            DataTable dt;
            DataSet ds;
            ds = new DLL().Sp_SelectRecord_usingExecuteDataset("Get_AllStudentRecord", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (GridView1.Rows.Count > 0)
                    {
                        abc.Visible = true;
                        listdisplay.Visible = true;
                        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
                        {
                            GridView1.HeaderRow.Cells[i].Text = GridView1.HeaderRow.Cells[i].Text.Replace("&amp;#39;", "'");
                        }
                        TextTrnsform();
                    }
                    else
                    {
                        abc.Visible = false;
                        listdisplay.Visible = false;
                    }

                }
                else
                {
                    abc.Visible = false;
                    listdisplay.Visible = false;
                    _oo.MessageBoxforUpdatePanel("Sorry, No Record&apos;s found", Page);
                }
            }
            else
            {
                abc.Visible = false;
                listdisplay.Visible = false;
                _oo.MessageBoxforUpdatePanel("Sorry, No Record&apos;s found", Page);
            }
        }
        private string GetCheckBoxListSelections()
        {
            string[] cblItems;
            ArrayList cblSelections = new ArrayList();
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    cblSelections.Add(item.Value.ToUpper().Replace("'", "&#39;"));
                }
            }
            foreach (ListItem item in CheckBoxList2.Items)
            {
                if (item.Selected)
                {
                    cblSelections.Add(item.Value.ToUpper().Replace("'", "&#39;"));
                }
            }
            cblItems = (string[])cblSelections.ToArray(typeof(string));
            return string.Join(", ", cblItems);
        }

        protected void ImageButton3_Click(object sender, EventArgs e)
        {
            _oo.ExporttolandscapePdf(Response, "ListOfStudents", gdv1);
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportTolandscapeWord(Response, "ListOfStudents", gdv);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            GridView1.Style.Add("text-transform", "uppercase");
            _oo.ExportDivToExcelWithFormatting(Response, "ListOfStudents.xls", gdv, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            if (rptType.SelectedValue == "Class wise Strength")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView1.Visible = false;
                GridView2.Visible = true;
                if (GridView2.Rows.Count > 0)
                {
                    GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TextTrnsform();
                }
                PrintHelper_New.ctrl = abc;
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
                Loadgrid1();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.Visible = false;
                GridView1.Visible = true;
                GridView1.Visible = true;
                GridView2.Visible = false;
                if (GridView1.Rows.Count > 0)
                {
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TextTrnsform();
                }
                PrintHelper_New.ctrl = abc;
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
                Loadgrid();

            }
        }
        protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClass();
        }
        protected void drpSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCourse();
        }
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }



        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadState(ddlState, ddlCountry);
            LoadCity(ddlCity, ddlState);
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(ddlCity, ddlState);
        }


    }
}