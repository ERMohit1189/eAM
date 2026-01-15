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
    public partial class employee_registration_by_Excel : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
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
                DrpGender.SelectedValue = DrpGender.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(Gender.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DrpGender.SelectedIndex = 0;
                DrpGender.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void loadMaitalStatus(DropDownList DrpMaitalStatus, string hfMaitalStatus)
        {
            try
            {
                DrpMaitalStatus.SelectedValue = DrpMaitalStatus.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(hfMaitalStatus.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DrpMaitalStatus.SelectedIndex = 0;
                DrpMaitalStatus.ForeColor = System.Drawing.Color.Red;
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
            _sql = "select * from StateMaster where countryid=" + CountryId + "";
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
        private void LoadReligion(DropDownList drpReligion, string ReligionName)
        {
            _sql = "Select ReligionId,ReligionName from ReligionMaster";
            BAL.objBal.FillDropDown_withValue(_sql, drpReligion, "ReligionName", "ReligionId");
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
            BAL.objBal.FillDropDown_withValue(_sql, drpCategory, "CasteName", "CasteId");
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
        private void LoadBloodGroup(DropDownList DropBloodGroup, string hfBloodGroup)
        {
            _sql = "select * from  BloodGroupMaster";
            BAL.objBal.FillDropDown_withValue(_sql, DropBloodGroup, "BloodGroupName", "BloodGroupId");
            DropBloodGroup.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                DropBloodGroup.SelectedValue = DropBloodGroup.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(hfBloodGroup.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DropBloodGroup.SelectedIndex = 0;
                DropBloodGroup.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadDepartment(DropDownList DropDepartment, string hfDepartment)
        {
            _sql = "Select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.FillDropDown_withValue(_sql, DropDepartment, "EmpDepName", "EmpDepName");
            DropDepartment.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                DropDepartment.SelectedValue = DropDepartment.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(hfDepartment.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DropDepartment.SelectedIndex = 0;
                DropDepartment.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadShiftCategory(DropDownList DropShiftCategory, string hfShiftCategory)
        {

            _sql = "Select EmpDesName from EmpDesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.FillDropDown_withValue(_sql, DropShiftCategory, "EmpDesName", "EmpDesName");
            DropShiftCategory.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                DropShiftCategory.SelectedValue = DropShiftCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(hfShiftCategory.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DropShiftCategory.SelectedIndex = 0;
                DropShiftCategory.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadDesignation(DropDownList DropDesignation, string hfDesignation)
        {
            _sql = "Select DesName from DesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.FillDropDown_withValue(_sql, DropDesignation, "DesName", "DesName");
            DropDesignation.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                DropDesignation.SelectedValue = DropDesignation.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(hfDesignation.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DropDesignation.SelectedIndex = 0;
                DropDesignation.ForeColor = System.Drawing.Color.Red;
                wrongStatus = wrongStatus + 1;
            }
        }
        private void LoadEmployeeCategory(DropDownList DropEmployeeCategory, string hfEmployeeCategory)
        {
            _sql = "select Ltrim(EmployeeCategoryName) EmployeeCategoryName from EmployeeCategoryMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            BAL.objBal.FillDropDown_withValue(_sql, DropEmployeeCategory, "EmployeeCategoryName", "EmployeeCategoryName");
            DropEmployeeCategory.Items.Insert(0, new ListItem("<--Select-->", "0"));
            try
            {
                DropEmployeeCategory.SelectedValue = DropEmployeeCategory.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Trim().Equals(hfEmployeeCategory.Trim().Trim(), StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch
            {
                DropEmployeeCategory.SelectedIndex = 0;
                DropEmployeeCategory.ForeColor = System.Drawing.Color.Red;
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
                        OleDbCommand cmd = new OleDbCommand(@"select * from [" + sheetName + "] where F2<>'First Name *'", con);
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

                var dt2 = ReadExcel(filePath, getExtention, lnkShow, "Sheet1$");
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

                    DropDownList DrpMaitalStatus = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpMaitalStatus");
                    HiddenField hfMaitalStatus = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfMaitalStatus");
                    loadMaitalStatus(DrpMaitalStatus, hfMaitalStatus.Value);

                    DropDownList DropCountry = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCountry");
                    HiddenField hfCountry = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCountry");
                    LoadCountry(DropCountry, hfCountry.Value);

                    DropDownList DropState = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropState");
                    HiddenField hfState = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfState");
                    LoadState(DropState, hfState.Value, DropCountry.SelectedValue);

                    DropDownList DropCity = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCity");
                    HiddenField hfCity = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCity");
                    LoadCity(DropCity, hfCity.Value, DropState.SelectedValue);

                    DropDownList DropReligion = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropReligion");
                    HiddenField hfReligion = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfReligion");
                    LoadReligion(DropReligion, hfReligion.Value);

                    DropDownList DropCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCategory");
                    HiddenField hfCategory = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfCategory");
                    LoadCategory(DropCategory, hfCategory.Value);

                    DropDownList DropBloodGroup = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropBloodGroup");
                    HiddenField hfBloodGroup = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfBloodGroup");
                    LoadBloodGroup(DropBloodGroup, hfBloodGroup.Value.Trim());

                    DropDownList DropDepartment = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropDepartment");
                    HiddenField hfDepartment = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfDepartment");
                    LoadDepartment(DropDepartment, hfDepartment.Value.Trim());

                    DropDownList DropShiftCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropShiftCategory");
                    HiddenField hfShiftCategory = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfShiftCategory");
                    LoadShiftCategory(DropShiftCategory, hfShiftCategory.Value.Trim());

                    DropDownList DropDesignation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropDesignation");
                    HiddenField hfDesignation = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfDesignation");
                    LoadDesignation(DropDesignation, hfDesignation.Value.Trim());

                    DropDownList DropEmployeeCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropEmployeeCategory");
                    HiddenField hfEmployeeCategory = (HiddenField)gvUploadStudent.Rows[i].FindControl("hfEmployeeCategory");
                    LoadEmployeeCategory(DropEmployeeCategory, hfEmployeeCategory.Value.Trim());


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
            ShowExcel();
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

                Label lblEmployeeID = (Label)gvUploadStudent.Rows[i].FindControl("lblEmployeeID");
                Label lblFirstName = (Label)gvUploadStudent.Rows[i].FindControl("lblFirstName");
                Label lblMiddleName = (Label)gvUploadStudent.Rows[i].FindControl("lblMiddleName");
                Label lblLastName = (Label)gvUploadStudent.Rows[i].FindControl("lblLastName");
                Label lblStudentDob = (Label)gvUploadStudent.Rows[i].FindControl("lblStudentDOB");
                DropDownList DrpGender = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpGender");
                Label lblFatherName = (Label)gvUploadStudent.Rows[i].FindControl("lblFatherName");
                DropDownList DrpMaitalStatus = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpMaitalStatus");
                Label lblEmergencyContactNo = (Label)gvUploadStudent.Rows[i].FindControl("lblContactNo");
                Label lblEmail = (Label)gvUploadStudent.Rows[i].FindControl("lblEmail");
                Label lblMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblMobileNo");
                DropDownList DropCountry = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCountry");
                DropDownList DropState = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropState");
                DropDownList DropCity = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCity");
                Label lblPIN = (Label)gvUploadStudent.Rows[i].FindControl("lblPIN");
                DropDownList DropReligion = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropReligion");
                Label lblNationality = (Label)gvUploadStudent.Rows[i].FindControl("lblNationality");
                DropDownList DropCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropCategory");
                DropDownList DropBloodGroup = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropBloodGroup");
                DropDownList DropDepartment = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropDepartment");
                DropDownList DropShiftCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropShiftCategory");
                DropDownList DropDesignation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropDesignation");
                DropDownList DropEmployeeCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropEmployeeCategory");
                Label lblDateOfJoining = (Label)gvUploadStudent.Rows[i].FindControl("lblDateOfJoining");
                Label lblAddress = (Label)gvUploadStudent.Rows[i].FindControl("lblAddress");
                _sql = "select max(SrNo) as Total from EmpployeeOfficialDetails";
                var ss = oo.ReturnTag(_sql, "Total");
                int ss1;
                try
                {
                    ss1 = Convert.ToInt32(ss.Trim());
                }
                catch (Exception) { ss1 = 0; }
                ss1 = ss1 + 1;
                string Ecode= IdGeneration(ss1.ToString());
                var password = Campus.GenerateRandomNo(8);
                param.Add(new SqlParameter("@Ecode", Ecode.ToString().Trim()));
                param.Add(new SqlParameter("@EmpId", lblEmployeeID.Text.Trim()));
                param.Add(new SqlParameter("@Password", password.Trim()));
                param.Add(new SqlParameter("@EFirstName", lblFirstName.Text.Trim()));
                param.Add(new SqlParameter("@EMiddleName", lblMiddleName.Text.Trim()));
                param.Add(new SqlParameter("@ELastName", lblLastName.Text.Trim()));
                param.Add(new SqlParameter("@EDateOfBirth", lblStudentDob.Text.Trim()));
                param.Add(new SqlParameter("@EGender", DrpGender.SelectedValue.ToString()));
                param.Add(new SqlParameter("@EFatherName", lblFatherName.Text.Trim()));
                param.Add(new SqlParameter("@MaritalStatus", DrpMaitalStatus.SelectedItem.Text.Trim()));
                param.Add(new SqlParameter("@EmergencyContactNo", lblEmergencyContactNo.Text.Trim()));
                param.Add(new SqlParameter("@EEmail", lblEmail.Text.Trim()));
                param.Add(new SqlParameter("@EMobileNo", lblMobileNo.Text.Trim()));
                param.Add(new SqlParameter("@EPreStateId", DropState.SelectedValue));
                param.Add(new SqlParameter("@EPerStateId", DropState.SelectedValue));
                param.Add(new SqlParameter("@EPreCityId", DropCity.SelectedValue));
                param.Add(new SqlParameter("@EPerCityId", DropCity.SelectedValue));
                param.Add(new SqlParameter("@EPreZip", lblPIN.Text.Trim()));
                param.Add(new SqlParameter("@EPerZip", lblPIN.Text.Trim()));
                param.Add(new SqlParameter("@EReligionId", DropReligion.SelectedValue));
                param.Add(new SqlParameter("@ENationality", lblNationality.Text.Trim()));
                param.Add(new SqlParameter("@ECategoryId", DropCategory.SelectedValue));
                param.Add(new SqlParameter("@EBloodGroupId", DropBloodGroup.SelectedValue.Trim()));
                param.Add(new SqlParameter("@DepartmentName", DropDepartment.SelectedItem.ToString()));
                param.Add(new SqlParameter("@Designation", DropShiftCategory.SelectedItem.ToString()));
                param.Add(new SqlParameter("@DesNameNew", DropDesignation.SelectedItem.ToString()));
                param.Add(new SqlParameter("@EmpCategory", DropEmployeeCategory.SelectedItem.ToString()));
                param.Add(new SqlParameter("@RegistrationDate", lblDateOfJoining.Text));
                param.Add(new SqlParameter("@EPreAddress", lblAddress.Text.Trim()));
                param.Add(new SqlParameter("@EPerAdd", lblAddress.Text.Trim()));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString().Trim()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString().Trim()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString().Trim()));
                SqlParameter msgParameter = new SqlParameter("@Msg", "");
                msgParameter.Direction = ParameterDirection.Output;
                msgParameter.Size = 0x100;
                param.Add(msgParameter);
                try
                {
                    msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StaffRegByExcel", param);
                    if (msg != "S")
                    {
                        error = error + ("(" + lblEmployeeID.Text.Trim() + ") " + msg) + "<br/>";
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
                Label lblEmployeeID = (Label)gvUploadStudent.Rows[i].FindControl("lblEmployeeID");
                if (lblEmployeeID.Text == string.Empty)
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
                Label lblLastName = (Label)gvUploadStudent.Rows[i].FindControl("lblLastName");
                if (lblLastName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[4];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblStudentDOB = (Label)gvUploadStudent.Rows[i].FindControl("lblStudentDOB");
                if (lblStudentDOB.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[5];
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
                            var col = gvUploadStudent.Rows[i].Cells[5];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                        int yy = int.Parse(dDate.ToString("yyyy"));
                        if (yy < 1969)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[5];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[5];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }
                Label lblContactNo = (Label)gvUploadStudent.Rows[i].FindControl("lblContactNo");
                if (lblContactNo.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[9];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }

                Label lblEmail = (Label)gvUploadStudent.Rows[i].FindControl("lblEmail");
                if (lblEmail.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[10];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblMobileNo");
                if (lblMobileNo.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[11];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblDateOfJoining = (Label)gvUploadStudent.Rows[i].FindControl("lblDateOfJoining");
                if (lblDateOfJoining.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[24];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                else
                {
                    string inputString = lblDateOfJoining.Text.Trim();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd-MMM-yyyy}", dDate);
                        if (dDate >= DateTime.Now)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[24];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                        int yy = int.Parse(dDate.ToString("yyyy"));
                        if (yy < 1969)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[24];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[24];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }
                Label lblAddress = (Label)gvUploadStudent.Rows[i].FindControl("lblAddress");
                if (lblAddress.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[25];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
            }
            
            return flag;
        }
        public bool CheckValues()
        {
            bool flag = true;
            
            for (int i = 0; i < gvUploadStudent.Rows.Count; i++)
            {
                Label lblEmployeeID = (Label)gvUploadStudent.Rows[i].FindControl("lblEmployeeID");
                if (lblEmployeeID.Text == string.Empty)
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
                Label lblLastName = (Label)gvUploadStudent.Rows[i].FindControl("lblLastName");
                if (lblLastName.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[4];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblStudentDOB = (Label)gvUploadStudent.Rows[i].FindControl("lblStudentDOB");
                if (lblStudentDOB.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[5];
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
                            var col = gvUploadStudent.Rows[i].Cells[5];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                        int yy = int.Parse(dDate.ToString("yyyy"));
                        if (yy < 1969)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[5];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[5];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }
                Label lblContactNo = (Label)gvUploadStudent.Rows[i].FindControl("lblContactNo");
                if (lblContactNo.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[9];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }

                Label lblEmail = (Label)gvUploadStudent.Rows[i].FindControl("lblEmail");
                if (lblEmail.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[10];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblMobileNo = (Label)gvUploadStudent.Rows[i].FindControl("lblMobileNo");
                if (lblMobileNo.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[11];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                Label lblDateOfJoining = (Label)gvUploadStudent.Rows[i].FindControl("lblDateOfJoining");
                if (lblDateOfJoining.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[24];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                else
                {
                    string inputString = lblDateOfJoining.Text.Trim();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd-MMM-yyyy}", dDate);
                        if (dDate >= DateTime.Now)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[24];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                        int yy = int.Parse(dDate.ToString("yyyy"));
                        if (yy < 1969)
                        {
                            var col = gvUploadStudent.Rows[i].Cells[24];
                            col.BorderColor = System.Drawing.Color.Red;
                            flag = false;
                        }
                    }
                    else
                    {
                        var col = gvUploadStudent.Rows[i].Cells[24];
                        col.BorderColor = System.Drawing.Color.Red;
                        flag = false;
                    }
                }
                Label lblAddress = (Label)gvUploadStudent.Rows[i].FindControl("lblAddress");
                if (lblAddress.Text == string.Empty)
                {
                    var col = gvUploadStudent.Rows[i].Cells[25];
                    col.BorderColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DrpGender = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpGender");
                if (DrpGender.SelectedIndex == 0)
                {
                    DrpGender.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
                DropDownList DrpMaitalStatus = (DropDownList)gvUploadStudent.Rows[i].FindControl("DrpMaitalStatus");
                if (DrpMaitalStatus.SelectedIndex == 0)
                {
                    DrpMaitalStatus.ForeColor = System.Drawing.Color.Red;
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

                DropDownList DropBloodGroup = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropBloodGroup");
                if (DropBloodGroup.SelectedIndex == 0)
                {
                    DropBloodGroup.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList DropDepartment = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropDepartment");
                if (DropDepartment.SelectedIndex == 0)
                {
                    DropDepartment.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList DropShiftCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropShiftCategory");
                if (DropShiftCategory.SelectedIndex == 0)
                {
                    DropShiftCategory.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList DropDesignation = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropDesignation");
                if (DropDesignation.SelectedIndex == 0)
                {
                    DropDesignation.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }

                DropDownList DropEmployeeCategory = (DropDownList)gvUploadStudent.Rows[i].FindControl("DropEmployeeCategory");
                if (DropEmployeeCategory.SelectedIndex == 0)
                {
                    DropEmployeeCategory.ForeColor = System.Drawing.Color.Red;
                    flag = false;
                }
            }
            return flag;
        }
        protected void lnkDownloadExcel_Click(object sender, EventArgs e)
        {
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("uploads/Excel/Staff/StaffRegistrationByExcel.xlsx"));
            Response.WriteFile(Server.MapPath("~/uploads/Excel/Staff/StaffRegistrationByExcel.xlsx"));
            Response.End();
        }

        public string IdGeneration(string x)
        {
            var xx = "";
            if (x.Length == 1)
            {
                xx = "E-00000" + x;

            }
            else if (x.Length == 2)
            {
                xx = "E-0000" + x;
            }
            else if (x.Length == 3)
            {
                xx = "E-000" + x;

            }
            else if (x.Length == 4)
            {
                xx = "E-00" + x;
            }
            else if (x.Length == 5)
            {
                xx = xx + "E-0" + x;
            }
            else
            {
                xx = x;
            }
            return xx;
        }
    }
}