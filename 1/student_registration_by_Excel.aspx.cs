using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminUploadStudentsExcel : Page
    {
        Campus oo = new Campus(); //comman object
        private string _sql = String.Empty;
        int wrongStatus = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            oo.LoadLoader(loader);
            if (!IsPostBack)
            {
            }
        }

        private void loadGender(DropDownList DrpGender, string Gender)
        {
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                DrpGender.SelectedValue = DrpGender.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(Gender.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DrpGender.SelectedIndex = 0;
                DrpGender.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadReligion(DropDownList drpReligion, string ReligionName)
        {
            _sql = "select * from ReligionMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drpReligion, "ReligionName", "ReligionName");
            drpReligion.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                drpReligion.SelectedValue = drpReligion.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(ReligionName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpReligion.SelectedIndex = 0;
                drpReligion.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadCategory(DropDownList drpCategory, string CategoryName)
        {
            _sql = "select * from  CasteMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drpCategory, "CasteName", "CasteName");
            drpCategory.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                drpCategory.SelectedValue = drpCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(CategoryName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpCategory.SelectedIndex = 0;
                drpCategory.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadOccupation(DropDownList drpOccupation, string Occupation)
        {
            _sql = "select * from GuardianDesMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drpOccupation, "DesignationName", "DesignationName");
            drpOccupation.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpOccupation.SelectedValue = drpOccupation.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(Occupation.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpOccupation.SelectedIndex = 0;
                drpOccupation.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadCountry(DropDownList drpCountry, string CountryName)
        {
            _sql = "select * from CountryMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drpCountry, "CountryName", "id");
            drpCountry.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpCountry.SelectedValue = drpCountry.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(CountryName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpCountry.SelectedIndex = 0;
                drpCountry.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadState(DropDownList drpState, string StateName, string CountryId)
        {
            _sql = "select * from StateMaster where countryid="+ CountryId + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpState, "StateName", "id");
            drpState.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpState.SelectedValue = drpState.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(StateName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpState.SelectedIndex = 0;
                drpState.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadCity(DropDownList drpCity, string CityName, string StateId)
        {
            _sql = "select * from CityMaster where Stateid=" + StateId + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpCity, "CityName", "id");
            drpCity.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpCity.SelectedValue = drpCity.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(CityName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpCity.SelectedIndex = 0;
                drpCity.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadMedium(DropDownList drpMedium, string mediumName)
        {
            _sql = "Select Medium,id from MediumMaster where  BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpMedium, "Medium", "Medium");
            drpMedium.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpMedium.SelectedValue = drpMedium.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(mediumName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpMedium.SelectedIndex = 0;
                drpMedium.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadBoard(DropDownList drpBoard, string BoardName)
        {
            _sql = "Select * from BoardMaster where BranchCode=" + Session["BranchCode"]+"";
            BAL.objBal.FillDropDown_withValue(_sql, drpBoard, "BoardName", "BoardName");
            drpBoard.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpBoard.SelectedValue = drpBoard.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(BoardName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpBoard.SelectedIndex = 0;
                drpBoard.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadFeeCategory(DropDownList drpFeeCategory, string FeeCategoryName)
        {
            _sql = "select * from FeeGroupMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpFeeCategory, "FeeGroupName", "Id");
            drpFeeCategory.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpFeeCategory.SelectedValue = drpFeeCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(FeeCategoryName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpFeeCategory.SelectedIndex = 0;
                drpFeeCategory.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadShift(DropDownList drpShift, string ShiftName)
        {
            _sql = "select * from StdShiftMaster where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpShift, "ShiftName", "Id");
            drpShift.Items.Insert(0, new ListItem("<--Select-->", "0"));
            if (drpShift.Items.Count>1)
            {
                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    drpShift.SelectedValue = drpShift.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(ShiftName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
                }
                catch
                {
                    drpShift.SelectedIndex = 0;
                    drpShift.ForeColor = System.Drawing.Color.Yellow;
                    //wrongStatus = wrongStatus + 1;
                }
            }
        }
        private void LoadEducationAct(DropDownList drpEducationAct, string ActName)
        {
            _sql = "select * from tblEducationAct where BranchCode=" + Session["BranchCode"] + " ";
            BAL.objBal.FillDropDown_withValue(_sql, drpEducationAct, "ActName", "Id");
            drpEducationAct.Items.Insert(0, new ListItem("<--Select-->", "0"));
            if (drpEducationAct.Items.Count>1)
            {
                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    drpEducationAct.SelectedValue = drpEducationAct.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(ActName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
                }
                catch
                {
                    drpEducationAct.SelectedIndex = 0;
                    drpEducationAct.ForeColor = System.Drawing.Color.Yellow;
                    //wrongStatus = wrongStatus + 1;
                }
            }
        }
        private void LoadCourse(DropDownList dropCourse, string courseName)
        {
            _sql = "Select CourseName,Id from CourseMaster where  BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, dropCourse, "CourseName", "Id");
            dropCourse.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                dropCourse.SelectedValue = dropCourse.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(courseName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                dropCourse.SelectedIndex = 0;
                dropCourse.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;

            }
        }

        private void LoadClass(DropDownList dropCourse, DropDownList dropAdmissionClass, string className)
        {
            _sql = "Select Id,ClassName from ClassMaster";
            _sql +=  " where (Course='" + dropCourse.SelectedValue + "' or Course is NULL) and SessionName='" + Session["SessionName"] + "'";
            _sql +=  " and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 ";
            BAL.objBal.FillDropDown_withValue(_sql, dropAdmissionClass, "ClassName", "Id");
            dropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                dropAdmissionClass.SelectedValue = dropAdmissionClass.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(className.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                dropAdmissionClass.SelectedIndex = 0;
                dropAdmissionClass.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }

        private void LoadStream(DropDownList dropCourse, DropDownList dropAdmissionClass, DropDownList dropLoadStream, string LoadStreamName)
        {
            _sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"] + " and ";
            _sql +=  "Course='" + dropCourse.SelectedValue + "' and ClassId='" + dropAdmissionClass.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, dropLoadStream, "BranchName", "Id");
            dropLoadStream.Items.Insert(0, new ListItem("<--Select-->", "0"));

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                dropLoadStream.SelectedValue = dropLoadStream.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(LoadStreamName.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                dropLoadStream.SelectedIndex = 0;
                dropLoadStream.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }

        private void LoadSection(DropDownList dropSection, DropDownList dropAdmissionClass, string sectionName)
        {
            _sql = "select SectionName,Id from SectionMaster where ClassNameId='" + dropAdmissionClass.SelectedValue + "'";
            _sql +=  "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, dropSection, "SectionName", "Id");
            dropSection.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                dropSection.SelectedValue = dropSection.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(sectionName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                dropSection.SelectedIndex = 0;
                dropSection.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }

        }
        private void LoadGroup(DropDownList dropGroup, DropDownList dropAdmissionClass, DropDownList dropStream, string GroupName)
        {
            _sql = "select SectionName,Id from StreamMaster where ClassId=" + dropAdmissionClass.SelectedValue + " and BranchId=" + dropStream.SelectedValue + "";
            _sql +=  "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, dropGroup, "Stream", "Id");
            dropGroup.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (dropGroup.Items.Count>1)
            {
                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    dropGroup.SelectedValue = dropGroup.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(GroupName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
                }
                catch
                {
                    dropGroup.SelectedIndex = 0;
                    dropGroup.ForeColor = System.Drawing.Color.Yellow;
                    //wrongStatus = wrongStatus + 1;
                }
            }
        }
        private void LoadHouse(DropDownList drophouse, string houseName)
        {
            _sql = "select HouseName from HouseMaster where  BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDownWithOutSelect(_sql, drophouse, "HouseName");
            drophouse.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drophouse.SelectedValue = drophouse.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(houseName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drophouse.SelectedIndex = 0;
                drophouse.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }

        private void LoadCard(DropDownList dropcard, string cardName)
        {
            _sql = "Select FeeGroupName from FeeGroupMaster ";
            _sql +=  " where  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDownWithOutSelect(_sql, dropcard, "FeeGroupName");
            dropcard.Items.Insert(0, new ListItem("<--Select Fee Category-->", "0"));
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                dropcard.SelectedValue = dropcard.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(cardName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                dropcard.SelectedIndex = 0;
                dropcard.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }

        }

        private void loadDefaultTypeofAdmission(DropDownList drpTypeofAdmission, string admissionType)
        {
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpTypeofAdmission.SelectedValue = drpTypeofAdmission.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(admissionType.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpTypeofAdmission.SelectedIndex = 0;
                drpTypeofAdmission.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }

        private void loadTransport(DropDownList drpTransport, string transportRequird)
        {
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                drpTransport.SelectedValue = drpTransport.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(transportRequird.Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                drpTransport.SelectedIndex = 0;
                drpTransport.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }

        public DataTable ReadExcel(string fileName, string fileExt, Control ctrl, string sheetName)
        {
            try
            {
                var conn = string.Empty;
                var dtexcel = new DataTable();
                if (fileExt.CompareTo(".xls") == 0)
                {
                    conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007 
                }
                else
                {
                    conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007 
                }
                using (var con = new OleDbConnection(conn))
                {
                    try
                    {
                        OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "] where F3<>'First Name *'", con);
                        //OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "]", con);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        adapter.Fill(dtexcel);
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(ctrl, GetType(), "alert", "window.alert('" + ex.Message + "')", true);
                    }
                }
                return dtexcel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ShowExcel()
        {
            lblWorning.Visible = false;
            gvUploadStudent.DataSource = null;
            gvUploadStudent.DataBind();
            if (fu1.HasFile)
            {
                string getExtention = Path.GetExtension(fu1.FileName);
                if (getExtention!= ".xlsx")
                {
                    lblWorning.Visible = true;
                    lnkSubmit.Visible = false;
                    oo.MessageBox("Please select excel file with .xlsx Extension!", Page);
                    return;
                }
                DateTime date = DateTime.Now;

                string time = date.ToString("HH_mm_ss");

                string filePath = String.Format(Server.MapPath("~/uploads/UploadExcel/stdlist") + "{0}" + getExtention, time);

                fu1.SaveAs(filePath);

                var dt2 = ReadExcel(filePath, getExtention, lnkShow, "Student_Detail$");
                if (dt2.Rows.Count==0)
                {
                    lblWorning.Visible = true;
                    lnkSubmit.Visible = false;
                    oo.MessageBox("Please select valid excel file!", Page);
                    return;
                }
                gvUploadStudent.DataSource = dt2;
                gvUploadStudent.DataBind();

                for (int i = 0; i < gvUploadStudent.Rows.Count; i++)
                {
                    DropDownList DrpGender = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpGender");
                    HiddenField hfGender = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfGender");
                    loadGender(DrpGender, hfGender.Value);
                    DropDownList DropReligion = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropReligion");
                    HiddenField hfReligion = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfReligion");
                    LoadReligion(DropReligion, hfReligion.Value);
                    DropDownList DropCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCategory");
                    HiddenField hfCategory = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCategory");
                    LoadCategory(DropCategory, hfCategory.Value);
                    DropDownList DropFatherOccupation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropFatherOccupation");
                    HiddenField hfFatherOccupation = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfFatherOccupation");
                    LoadOccupation(DropFatherOccupation, hfFatherOccupation.Value);
                    DropDownList DropMotherrOccupation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropMotherrOccupation");
                    HiddenField hfMotherOccupation = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfMotherOccupation");
                    LoadOccupation(DropMotherrOccupation, hfMotherOccupation.Value);
                    DropDownList DropCountry = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCountry");
                    HiddenField hfCountry = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCountry");
                    LoadCountry(DropCountry, hfCountry.Value);
                    DropDownList DropState = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropState");
                    HiddenField hfState = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfState");
                    LoadState(DropState, hfState.Value, DropCountry.SelectedValue);
                    DropDownList DropCity = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCity");
                    HiddenField hfCity = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCity");
                    LoadCity(DropCity, hfCity.Value, DropState.SelectedValue);
                    DropDownList DrpTypeofAdmission = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpTypeofAdmission");
                    HiddenField hfAdmissionType = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfAdmissionType");
                    loadDefaultTypeofAdmission(DrpTypeofAdmission, hfAdmissionType.Value);
                    DropDownList drpMedium = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpMedium");
                    HiddenField hfMedium = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfMedium");
                    LoadMedium(drpMedium, hfMedium.Value);
                    DropDownList drpBoard = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpBoard");
                    HiddenField hfBoard = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfBoard");
                    LoadBoard(drpBoard, hfBoard.Value);
                    DropDownList drpFeeCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpFeeCategory");
                    HiddenField hfFeeCategory = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfFeeCategory");
                    LoadFeeCategory(drpFeeCategory, hfFeeCategory.Value.Trim());
                    DropDownList drpShift = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpShift");
                    HiddenField hfShift = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfShift");
                    LoadShift(drpShift, hfShift.Value);
                    DropDownList drpEducationAct = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpEducationAct");
                    HiddenField hfEducationAct = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfEducationAct");
                    LoadEducationAct(drpEducationAct, hfEducationAct.Value);
                    DropDownList dropCourse = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCourse");
                    HiddenField hfCourse = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCourse");
                    LoadCourse(dropCourse, hfCourse.Value);
                    DropDownList dropAdmissionClass = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropAdmissionClass");
                    HiddenField hfClass = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfClass");
                    LoadClass(dropCourse, dropAdmissionClass, hfClass.Value);
                    
                    DropDownList DropStream = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropStream");
                    HiddenField hfStream = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfStream");
                    LoadStream(dropCourse, dropAdmissionClass, DropStream, hfStream.Value.Trim());
                    DropDownList dropSection = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropSection");
                    HiddenField hfSection = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfSection");
                    LoadSection(dropSection, dropAdmissionClass, hfSection.Value.Trim());
                    DropDownList DropGroup = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropGroup");
                    HiddenField hfGroup = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfGroup");
                    LoadGroup(DropGroup, dropAdmissionClass, DropStream, hfGroup.Value.Trim());
                    DropDownList Drophouse = (DropDownList)gvUploadStudent.Rows[i].FindControl("Drophouse");
                    HiddenField hfHouse = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfHouse");
                    LoadHouse(Drophouse, hfHouse.Value.Trim());


                }
                File.Delete(filePath);
                if (!CheckValuesFirst())
                {
                    oo.MessageBox("Mandatory columns should have their values!", Page);
                    lnkSubmit.Visible = false;
                }
                else
                {
                    if (wrongStatus == 0)
                    {
                        oo.MessageBox("Data verified successfully.", Page);
                        lnkSubmit.Visible = true;
                    }
                    else
                    {
                        oo.MessageBox("Mandatory columns should have their values!", Page);
                        lnkSubmit.Visible = false;
                    }
                }
            }
            else
            {
                oo.MessageBox("Please select excel file!", Page);
            }
        }

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            _sql = "select * from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + "";
            if (!oo.Duplicate(_sql))
            {
                oo.MessageBox("Please initialize S.R. No.!", Page);
            }
            else
            {
                ShowExcel();
            }
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            if (CheckValues())
            {
                SaveData();
            }
            else
            {
                oo.MessageBox("Mandatory columns should have their values!", Page);
            }
        }

        private void SaveData()
        {
            string msg = ""; string error = "";
            for (int i = 0; i < gvUploadStudent.Rows.Count; i++)
            {
                List<SqlParameter> param = new List<SqlParameter>();

                Label lblsrno = (Label)gvUploadStudent.Rows[i].FindControl("lblsrno");
                Label lblFirstName = (Label)gvUploadStudent.Rows[i].FindControl("lblFirstName");
                Label lblMiddleName = (Label)gvUploadStudent.Rows[i].FindControl("lblMiddleName");
                Label lblLastName = (Label)gvUploadStudent.Rows[i].FindControl("lblLastName");
                DropDownList DrpGender = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpGender");
                Label lblStudentDob = (Label)gvUploadStudent.Rows[i].FindControl("lblStudentDOB");
                DropDownList DropReligion = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropReligion");
                DropDownList DropCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCategory");
                Label lblNationality = (Label)gvUploadStudent.Rows[i].FindControl("lblNationality");
                Label lblAadhaarNo = (Label)gvUploadStudent.Rows[i].FindControl("lblAadhaarNo");
                Label lblFatherName = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherName");
                DropDownList DropFatherOccupation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropFatherOccupation");
                Label lblFatherMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherMobileNo");
                Label lblFatherEmail = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherEmail");
                Label lblMotherName = (Label)gvUploadStudent.Rows[i].FindControl("lblMotherName");
                DropDownList DropMotherrOccupation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropMotherrOccupation");
                Label lblMotherMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblMotherMobileNo");
                Label lblAddress = (Label)gvUploadStudent.Rows[i].FindControl("lblAddress");
                DropDownList DropCountry = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCountry");
                DropDownList DropState = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropState");
                DropDownList DropCity = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCity");
                Label lblDateofAdmission = (Label)gvUploadStudent.Rows[i].FindControl("lblDateofAdmission");
                DropDownList DrpTypeofAdmission = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpTypeofAdmission");
                DropDownList drpMedium = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpMedium");
                DropDownList drpBoard = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpBoard");
                DropDownList drpFeeCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpFeeCategory");
                DropDownList drpShift = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpShift");
                DropDownList drpEducationAct = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpEducationAct");
                DropDownList DropCourse = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCourse");
                DropDownList DropAdmissionClass = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropAdmissionClass");
                DropDownList DropStream = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropStream");
                DropDownList DropSection = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropSection");
                DropDownList DropGroup = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropGroup");
                DropDownList Drophouse = (DropDownList)gvUploadStudent.Rows[i].FindControl("Drophouse");

                lblsrno.Text = lblsrno.Text.Replace(" ", "");
                param.Add(new SqlParameter("@Srno", lblsrno.Text.Trim()));
                param.Add(new SqlParameter("@FirstName", lblFirstName.Text.Trim()));
                param.Add(new SqlParameter("@MiddleName", lblMiddleName.Text.Trim()));
                param.Add(new SqlParameter("@LastName", lblLastName.Text.Trim()));
                param.Add(new SqlParameter("@Gender", DrpGender.SelectedValue.Trim()));
                param.Add(new SqlParameter("@DOB", DateTime.Parse(lblStudentDob.Text.Trim()).ToString("dd-MMM-yyyy")));
                param.Add(new SqlParameter("@Religion", DropReligion.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Category", DropCategory.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Nationality", lblNationality.Text.Trim()));
                param.Add(new SqlParameter("@AadhaarNo", lblAadhaarNo.Text.Trim()));
                param.Add(new SqlParameter("@FatherName", lblFatherName.Text.Trim()));
                param.Add(new SqlParameter("@FatherOccupation", DropFatherOccupation.SelectedValue.Trim()));
                param.Add(new SqlParameter("@FatherMobileNo", lblFatherMobileNo.Text.Trim()));
                param.Add(new SqlParameter("@FatherEmail", lblFatherEmail.Text.Trim()));
                param.Add(new SqlParameter("@MotherName", lblMotherName.Text.Trim()));
                param.Add(new SqlParameter("@MotherOccupation", DropMotherrOccupation.SelectedValue.Trim()));
                param.Add(new SqlParameter("@MotherMobileNo", lblMotherMobileNo.Text.Trim()));
                param.Add(new SqlParameter("@Address", lblAddress.Text.Trim()));
                param.Add(new SqlParameter("@CountryId", DropCountry.SelectedValue.Trim()));
                param.Add(new SqlParameter("@StateId", DropState.SelectedValue.Trim()));
                param.Add(new SqlParameter("@CityId", DropCity.SelectedValue.Trim()));
                param.Add(new SqlParameter("@DateofAdmission", DateTime.Parse(lblDateofAdmission.Text.Trim()).ToString("dd-MMM-yyyy")));
                param.Add(new SqlParameter("@TypeofAdmission", DrpTypeofAdmission.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Medium", drpMedium.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Board", drpBoard.SelectedValue.Trim()));
                param.Add(new SqlParameter("@FeeCategoryId", drpFeeCategory.SelectedValue.Trim()));
                param.Add(new SqlParameter("@FeeCategoryName", drpFeeCategory.SelectedItem.Text.Trim()));
                param.Add(new SqlParameter("@Shift", drpShift.SelectedValue.Trim()));
                param.Add(new SqlParameter("@EducationAct", drpEducationAct.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Course", DropCourse.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Class", DropAdmissionClass.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Branch", DropStream.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Section", DropSection.SelectedValue.Trim()));
                param.Add(new SqlParameter("@Stream", DropGroup.SelectedValue.Trim()));
                param.Add(new SqlParameter("@HouseName", Drophouse.SelectedValue.Trim()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString().Trim()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString().Trim()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString().Trim()));
                param.Add(new SqlParameter("@MODForFeeDeposit", "I"));
                SqlParameter msgParameter = new SqlParameter("@Msg", "");
                msgParameter.Direction = ParameterDirection.Output;
                msgParameter.Size = 0x100;
                param.Add(msgParameter);
                try
                {
                    msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentRegByExcel", param);
                    if (msg != "S")
                    {
                        error = error + ("(" + lblsrno.Text.Trim() + ") " + msg) + "<br/>";
                        gvUploadStudent.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            if (error.Trim() == "")
            {
                gvUploadStudent.DataSource = null;
                gvUploadStudent.DataBind();
                lnkSubmit.Visible = false;
                oo.msgbox(Page, msgbox, "Record(s) submitted successfully.", "S");
            }
            else
            {
                oo.msgbox(Page, msgbox, "Duplicate record(s) found!", "A");
                errors.InnerHtml = error.ToString();
                errors.Focus();
            }
        }
        public bool CheckValuesFirst()
        {
            bool flag = true;

            for (int i = 0; i < gvUploadStudent.Rows.Count; i++)
            {
                Label lblsrno = (Label)gvUploadStudent.Rows[i].FindControl("lblsrno");
                if (lblsrno.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[1];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblFirstName = (Label)gvUploadStudent.Rows[i].FindControl("lblFirstName");
                if (lblFirstName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[2];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblStudentDOB = (Label)gvUploadStudent.Rows[i].FindControl("lblStudentDOB");
                if (lblStudentDOB.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[6];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                else
                {
                    string inputString = lblStudentDOB.Text.Trim();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd-MMM-yyyy}", dDate);
                        if (dDate >= DateTime.Now)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[6];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                        int yy = int.Parse(dDate.ToString("yyyy"));
                        if (yy< 1969)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[6];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[6];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }

                Label lblFatherName = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherName");
                if (lblFatherName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[11];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblFatherMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherMobileNo");
                if (lblFatherMobileNo.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[13];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblMotherName = (Label)gvUploadStudent.Rows[i].FindControl("lblMotherName");
                if (lblMotherName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[15];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblMotherMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblMotherMobileNo");
                if (lblMotherName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[17];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblAddress = (Label)gvUploadStudent.Rows[i].FindControl("lblAddress");
                if (lblAddress.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[18];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }

                Label lblDateofAdmission = (Label)gvUploadStudent.Rows[i].FindControl("lblDateofAdmission");
                if (lblDateofAdmission.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[22];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                else
                {
                    string inputString = lblDateofAdmission.Text.Trim();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd-MMM-yyyy}", dDate);
                        if (dDate > DateTime.Now)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[22];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                        int yy = int.Parse(dDate.ToString("yyyy"));
                        if (yy < 1969)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[22];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[22];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }
            }
            return flag;
        }
        public bool CheckValues()
        {
            bool flag = true;
            
            for (int i = 0; i < gvUploadStudent.Rows.Count; i++)
            {
                Label lblsrno = (Label)gvUploadStudent.Rows[i].FindControl("lblsrno");
                if (lblsrno.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[1];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblFirstName = (Label)gvUploadStudent.Rows[i].FindControl("lblFirstName");
                if (lblFirstName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[2];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblStudentDOB = (Label)gvUploadStudent.Rows[i].FindControl("lblStudentDOB");
                if (lblStudentDOB.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[6];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                else
                {
                    string inputString = lblStudentDOB.Text.Trim();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd-MMM-yyyy}", dDate);
                        if (dDate>=DateTime.Now)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[6];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[6];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }

                Label lblFatherName = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherName");
                if (lblFatherName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[11];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblFatherMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherMobileNo");
                if (lblFatherMobileNo.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[13];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblMotherName = (Label)gvUploadStudent.Rows[i].FindControl("lblMotherName");
                if (lblMotherName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[15];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblMotherMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblMotherMobileNo");
                if (lblMotherName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[17];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblAddress = (Label)gvUploadStudent.Rows[i].FindControl("lblAddress");
                if (lblAddress.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[18];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }

                Label lblDateofAdmission = (Label)gvUploadStudent.Rows[i].FindControl("lblDateofAdmission");
                if (lblDateofAdmission.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[22];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                else
                {
                    string inputString = lblDateofAdmission.Text.Trim();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd-MMM-yyyy}", dDate);
                        if (dDate > DateTime.Now)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[22];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[22];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }
                DropDownList DrpGender = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpGender");
                if (DrpGender.SelectedIndex == 0)
                {
                    DrpGender.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropReligion = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropReligion");
                if (DropReligion.SelectedIndex == 0)
                {
                    DropReligion.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCategory");
                if (DropCategory.SelectedIndex == 0)
                {
                    DropCategory.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropFatherOccupation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropFatherOccupation");
                if (DropFatherOccupation.SelectedIndex == 0)
                {
                    DropFatherOccupation.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropMotherrOccupation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropMotherrOccupation");
                if (DropMotherrOccupation.SelectedIndex == 0)
                {
                    DropMotherrOccupation.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropCountry = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCountry");
                if (DropCountry.SelectedIndex == 0)
                {
                    DropCountry.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropState = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropState");
                if (DropState.SelectedIndex == 0)
                {
                    DropState.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DropCity = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCity");
                if (DropCity.SelectedIndex == 0)
                {
                    DropCity.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DrpTypeofAdmission = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpTypeofAdmission");
                if (DrpTypeofAdmission.SelectedIndex == 0)
                {
                    DrpTypeofAdmission.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList drpMedium = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpMedium");
                if (drpMedium.SelectedIndex == 0)
                {
                    drpMedium.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList drpBoard = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpBoard");
                if (drpBoard.SelectedIndex == 0)
                {
                    drpBoard.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList drpFeeCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpFeeCategory");
                if (drpFeeCategory.SelectedIndex == 0)
                {
                    drpFeeCategory.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList drpShift = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpShift");
                if (drpShift.Items.Count>1)
                {
                    if (drpShift.SelectedIndex == 0)
                    {
                        drpShift.ForeColor = System.Drawing.Color.Yellow;
                        //flag = false;
                    }
                }
                
                DropDownList drpEducationAct = (DropDownList)gvUploadStudent.Rows[i].FindControl("drpEducationAct");
                if (drpEducationAct.Items.Count>1)
                {
                    if (drpEducationAct.SelectedIndex == 0)
                    {
                        drpEducationAct.ForeColor = System.Drawing.Color.Yellow;
                        //flag = false;
                    }
                }

                DropDownList dropCourse = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCourse");
                if (dropCourse.SelectedIndex == 0)
                {
                    dropCourse.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList dropAdmissionClass = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropAdmissionClass");
                if (dropAdmissionClass.SelectedIndex == 0)
                {
                    dropAdmissionClass.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList DropStream = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropStream");
                if (DropStream.SelectedIndex == 0)
                {
                    DropStream.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                
                DropDownList dropSection = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropSection");
                if (dropSection.SelectedIndex == 0)
                {
                    dropSection.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList DropGroup = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropGroup");
                if (DropGroup.Items.Count > 1)
                {
                    if (DropGroup.SelectedIndex == 0)
                    {
                        DropGroup.ForeColor = System.Drawing.Color.Yellow;
                        //flag = false;
                    }
                }
                
                DropDownList drophouse = (DropDownList)gvUploadStudent.Rows[i].FindControl("Drophouse");
                if (drophouse.SelectedIndex == 0)
                {
                    drophouse.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
            }
            return flag;
        }
        protected void lnkDownloadExcel_Click(object sender, EventArgs e)
        {
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("uploads/Excel/Student/student_registration_by_Excel.xlsx"));
            Response.WriteFile(Server.MapPath("~/uploads/Excel/Student/student_registration_by_Excel.xlsx"));
            Response.End();
        }
    }
}