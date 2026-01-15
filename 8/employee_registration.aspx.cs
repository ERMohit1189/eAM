using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminEmployeeRegistration : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        private DataTable _dt;
        // ReSharper disable once ArrangeTypeMemberModifiers
#pragma warning disable 169
        // ReSharper disable once InconsistentNaming
#pragma warning disable 649
        DataTable dt;
#pragma warning restore 649
#pragma warning restore 169
        private bool _recordNotInsertEmpployeeOfficialDetails;
        private bool _recordNotInsertEmpGeneralDetail;
        private bool _recordNotInsertEmpEmployeeDetails;
        private bool _recordNotInsertEmpPreviousEmployment;
        private bool _recordNotInsertPrevious;

        public AdminEmployeeRegistration()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("Index.aspx");
            }
            _con = _oo.dbGet_connection();
            if (!IsPostBack)
            {
                _sql = "select MachineNo from PunchMachineConfiguration where BranchCode=" + Session["BranchCode"] + " ";
                _oo.FillDropDown_withValue(_sql, ddlMachineNo, "MachineNo", "MachineNo");
                ddlMachineNo.Items.Insert(0, new ListItem("<--Select-->", ""));

                try
                {
                    AddPreviousInstitutionGridRow();
                    AddPreviousEmploymentGridRow();
                    CheckValueAddDeleteUpdate();

                }
                catch (Exception)
                {
                    // ignored
                }
                LoadK12();
                TabControl();
                EmpGeneralDetailDropDown();
                EmployeeDetailDropDrop();
                //  PreviousEmploymentDropDow();
                OfficialDetailDropDown();
                Get_DocumentName(Page);
                //GetEmpShift();
                GetBankMaster(ddlBank);

                /* CReated By Hariom Dated : 12-03-21 */
                GetBankBranch(ddlBankBranch, Int32.Parse(ddlBank.SelectedValue.ToString() == "" ? "0" : ddlBank.SelectedValue.ToString()));
                _sql = "select defaultvalue from DefaultSelectedValue where defaultvalueof='MaritalStatus' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "defaultvalue") != "")
                {
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "Single")
                    {
                        DrpMaitalStatus.SelectedIndex = 0;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "Married")
                    {
                        DrpMaitalStatus.SelectedIndex = 1;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "Separated")
                    {
                        DrpMaitalStatus.SelectedIndex = 2;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "Divorced")
                    {
                        DrpMaitalStatus.SelectedIndex = 3;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "Widowed")
                    {
                        DrpMaitalStatus.SelectedIndex = 4;
                    }

                }
                _sql = "select defaultvalue from DefaultSelectedValue where defaultvalueof='Nationality' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "defaultvalue") != "")
                {
                    txtnat.Text = _oo.ReturnTag(_sql, "defaultvalue");
                }
                _sql = "select defaultvalue from DefaultSelectedValue where defaultvalueof='Blood Group' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql, "defaultvalue") != "")
                {
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "2")
                    {
                        drpblood.SelectedIndex = 1;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "3")
                    {
                        drpblood.SelectedIndex = 2;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "4")
                    {
                        drpblood.SelectedIndex = 3;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "5")
                    {
                        drpblood.SelectedIndex = 4;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "6")
                    {
                        drpblood.SelectedIndex = 5;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "7")
                    {
                        drpblood.SelectedIndex = 6;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "8")
                    {
                        drpblood.SelectedIndex = 7;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "9")
                    {
                        drpblood.SelectedIndex = 8;
                    }
                    if (_oo.ReturnTag(_sql, "defaultvalue") == "10")
                    {
                        drpblood.SelectedIndex = 9;
                    }

                }

            }
        }
        public void LoadK12()
        {
            _sql = "Select * from setting";
            lblAadhaar.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
        }
        protected void Get_DocumentName(Control ctrl)
        {
            _sql = "Select DocumentType,Id from dt_CreateStaffDocumentName where  BranchCode=" + Session["BranchCode"].ToString() + "";
            Repeater1.DataSource = BAL.objBal.GridFill(_sql);
            Repeater1.DataBind();
        }

        public void NewDocumentsDetails(Control ctrl)
        {
            var msg = "";
            try
            {
                for (var i = 0; i < Repeater1.Items.Count; i++)
                {
                    var lblId = (Label)Repeater1.Items[i].FindControl("lblId");
                    var lblDocument = (Label)Repeater1.Items[i].FindControl("lblDocument");

                    var chksoft = (CheckBox)Repeater1.Items[i].FindControl("Chksoft");
                    var chkhard = (CheckBox)Repeater1.Items[i].FindControl("Chkhard");
                    var chkVerified = (CheckBox)Repeater1.Items[i].FindControl("chkVerified");
                    var txtRemark = (TextBox)Repeater1.Items[i].FindControl("txtRemark");

                    var hfFile = (HiddenField)Repeater1.Items[i].FindControl("hfFile");
                    var hdfilefileExtention = (HiddenField)Repeater1.Items[i].FindControl("hdfilefileExtention");

                    var base64Std = hfFile.Value;
                    var fileExtention = hdfilefileExtention.Value;
                    var filePath = "";
                    var fileName = "";
                    if (base64Std != string.Empty)
                    {
                        var folderPath = Server.MapPath(@"/Uploads/StaffDocs");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        filePath = @"../Uploads/StaffDocs/";
                        fileName = Session["EmpId"].ToString() + '_' + lblDocument.Text.Trim() + fileExtention;
                        using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                var data = Convert.FromBase64String(base64Std);
                                bw.Write(data);
                                bw.Close();
                            }
                        }
                    }

                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "USP_SetEmployeeDocumentRecord";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@EmpCode", Session["Ecode"].ToString());
                        cmd.Parameters.AddWithValue("@Empid", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@DocId", lblId.Text.Trim());
                        cmd.Parameters.AddWithValue("@Softcopy", chksoft.Checked ? 1 : avatarUpload.HasFile ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Hardcopy", chkhard.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Verified", chkVerified.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Remark", txtRemark.Text.Trim());
                        cmd.Parameters.AddWithValue("@DocName", fileName != "" ? fileName : "");
                        cmd.Parameters.AddWithValue("@DocPath", filePath + fileName);
                        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                        }
                        catch (SqlException ex)
                        {
                            throw new Exception("some reason to rethrow", ex);
                            // _con.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("some reason to rethrow", ex);
                            // _con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;

            }
            if (msg != string.Empty)
            {
                _oo.MessageBoxforUpdatePanel(msg, ctrl);
            }
        }
        public void EmpGeneralDetailDropDown()
        {
            _sql = "select ltrim(rtrim(CountryName)) as CountryName, id from CountryMaster ";
            _oo.FillDropDown_withValue(_sql, DrpPreSCountry, "CountryName", "id");
            _oo.FillDropDown_withValue(_sql, DrpPerCountry, "CountryName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Country", DrpPreSCountry);
                    objBll.loadDefaultvalue("Country", DrpPerCountry);
                }
                catch
                {
                    // ignored
                }
            }

            DrpPreSta.Items.Clear();
            _sql = "Select count(*) cnt from StateMaster where countryId='" + DrpPreSCountry.SelectedValue + "'";
            if (_oo.ReturnTag(_sql, "cnt") == "0")
            {
                DrpPreSta.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                _sql = "Select StateName,Id from StateMaster where countryId='" + DrpPreSCountry.SelectedValue + "'";
                _oo.FillDropDown_withValue(_sql, DrpPreSta, "StateName", "id");
                _oo.FillDropDown_withValue(_sql, DrpPerState, "StateName", "id");
                using (var objBll = new BLL())
                {
                    try
                    {
                        objBll.loadDefaultvalue("State", DrpPreSta);
                        objBll.loadDefaultvalue("State", DrpPerState);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            DrpPresCity.Items.Clear();
            _sql = "Select count(*) cnt from CityMaster where StateId='" + DrpPreSta.SelectedValue + "'";
            if (_oo.ReturnTag(_sql, "cnt") == "0")
            {
                DrpPresCity.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                _sql = "select id, ltrim(rtrim(CityName)) as CityName from CityMaster where stateid=" + DrpPreSta.SelectedValue + "";
                _oo.FillDropDown_withValue(_sql, DrpPresCity, "CityName", "id");
                _oo.FillDropDown_withValue(_sql, DrpPerCity, "CityName", "id");
                using (var objBll = new BLL())
                {
                    try
                    {
                        objBll.loadDefaultvalue("City", DrpPresCity);
                        objBll.loadDefaultvalue("City", DrpPerCity);
                    }
                    catch
                    {   // ignored }
                    }

                    //_sql = "Select CityName,id from CityMaster where StateId='" + DrpPreSta.SelectedValue + "'";
                    //BAL.objBal.FillDropDown_withValue(_sql, DrpPresCity, "CityName", "id");
                    //BAL.objBal.FillDropDown_withValue(_sql, DrpPerCity, "CityName", "id");
                    //using (var objBll = new BLL())
                    //{
                    //    try
                    //    {
                    //        objBll.loadDefaultvalue("City", DrpPresCity);
                    //        objBll.loadDefaultvalue("City", DrpPerCity);
                    //    }
                    //    catch
                    //    {
                    //        // ignored
                    //    }
                    //}
                }

            }
        }

        public void EmployeeDetailDropDrop()
        {
            _sql = "Select ReligionName from ReligionMaster";
            _oo.FillDropDown(_sql, DrpReligion, "ReligionName");

            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Religion", DrpReligion);
                }
                catch
                {
                    // ignored
                }
            }
            _sql = "Select CasteName from CasteMaster";
            _oo.FillDropDown(_sql, DrpCategory, "CasteName");

            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("CasteName", DrpCategory);
                }
                catch
                {
                    // ignored
                }
            }
            _sql = "Select BloodGroupName from BloodGroupMaster";
            _oo.FillDropDown(_sql, drpblood, "BloodGroupName");

            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Blood Group", drpblood);
                }
                catch
                {
                    // ignored
                }
            }
        }
        public void OfficialDetailDropDown()
        {
            _oo.AddDateMonthYearDropDown(drpyearhai, drpmonthhai, drpdinhai);
            FindCurrentDateandSetinDropDownhai();
            _sql = "Select EmpDepName from EmpDepMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, txtDepartmentName, "EmpDepName");
            _sql = "Select EmpDesName from EmpDesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, drpdes, "EmpDesName");
            _sql = "Select DesName from DesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, drpEmpdes, "DesName");
            _sql = "select Ltrim(EmployeeCategoryName) EmployeeCategoryName from EmployeeCategoryMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, drpempcategory, "EmployeeCategoryName");
        }

        private void GetBankMaster(DropDownList ddl)
        {
            string ss = "SELECT ID, BankName FROM AccBankMaster";
            _oo.FillDropDown_withValue(ss, ddl, "BankName", "ID");

        }

        /* Updated By Hariom Dated : 12-03-21 */
        private void GetBankBranch(DropDownList ddl, int m02Id)
        {
            _dt = _oo.Fetchdata("SELECT id, BankId, BankBranchName, [Address], IFSC, PIN, Remark FROM AccBankBranchMaster WHERE IsActive=1 AND BankId=" + ddlBank.SelectedValue.ToString());

            if (_dt != null && _dt.Rows.Count > 0)
            {
                ddlBankBranch.DataSource = _dt;
                ddlBankBranch.DataValueField = "id";
                ddlBankBranch.DataTextField = "BankBranchName";
                ddlBankBranch.DataBind();
            }
            else
            {
                ddl.Items.Clear();
            }
        }



        public void TabControl()
        {
            //TabContainer1.ActiveTab = TabContainer1.Tabs[0];

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void DrpMaitalStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DrpMaitalStatus.SelectedValue == "2")
            {
                divhidemarr.Visible = true;
                divhideSpouse.Visible = true;
                margindiv.Style["margin-bottom"] = "136px";
            }
            else
            {
                divhidemarr.Visible = false;
                divhideSpouse.Visible = false;
                margindiv.Style["margin-bottom"] = "108px";
            }

        }
        public bool EmpGeneralDetail()
        {
            var flag = true;
            _recordNotInsertEmpGeneralDetail = true;
            var cmd = new SqlCommand
            {
                CommandText = "EmpGeneralDetailProc",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };

            cmd.Parameters.AddWithValue("@Ecode", Session["Ecode"].ToString());
            cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
            cmd.Parameters.AddWithValue("@Etitle", DrpTitle.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@EFirstName", txtFirstName.Text.Trim());
            cmd.Parameters.AddWithValue("@EMiddleName", txtmidName.Text.Trim());
            cmd.Parameters.AddWithValue("@ELastName", txtlastName.Text.Trim());
            // var date = DrpYear.SelectedItem + "/" + DrpMonth.SelectedItem + "/" + DrpDate.SelectedItem;
            cmd.Parameters.AddWithValue("@EDateOfBirth", txtStudentDOB.Text.Trim());
            cmd.Parameters.AddWithValue("@EGender", RadioButtonList1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@EFatherName", txtfathername.Text.Trim());
            cmd.Parameters.AddWithValue("@EMotherName", txtmothname.Text.Trim());
            cmd.Parameters.AddWithValue("@EEmail", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@isEmailShow", chkStEmail.Checked);

            cmd.Parameters.AddWithValue("@EMobileNo", txtmobileno.Text.Trim());
            cmd.Parameters.AddWithValue("@isMobileNoShow", chkStMobile.Checked);

            cmd.Parameters.AddWithValue("@EPreAddress", txtPreseAdd.Text.Trim());
            _sql = "Select Id from StateMaster where StateName='" + DrpPreSta.SelectedItem + "'";
            var dd1 = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@EPreStateId", dd1);
            _sql = "Select Id from CityMaster where CityName='" + DrpPresCity.SelectedItem + "'";
            var dd = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@EPreCityId", dd);
            cmd.Parameters.AddWithValue("@EPreZip", txtPerZip.Text.Trim());
            cmd.Parameters.AddWithValue("@EPerAdd", txtPermAdd.Text.Trim());
            _sql = "Select Id from StateMaster where StateName='" + DrpPerState.SelectedItem + "'";
            var d = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@EPerStateId", d);
            _sql = "Select Id from CityMaster where CityName='" + DrpPerCity.SelectedItem + "'";
            var sd = _oo.ReturnTag(_sql, "Id");
            cmd.Parameters.AddWithValue("@EPerCityId", sd);
            cmd.Parameters.AddWithValue("@EPerZip", txtPerZip.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@EmergencyContactNo", txtemergencycontactno.Text.Trim());
            cmd.Parameters.AddWithValue("@MaritalStatus", DrpMaitalStatus.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@MarriageAnniversaryDate", txtmarranniver.Text.Trim());
            cmd.Parameters.AddWithValue("@SpouseName", txtspousename.Text.Trim());
            var filePath = "";
            var fileName = "";
            if (avatarUpload.HasFile)
            {
                filePath = @"~/Uploads/StaffPhoto/";
                var fileExtention = Path.GetExtension(avatarUpload.PostedFile.FileName);
                fileName = avatarUpload.PostedFile.FileName + fileExtention;
                avatarUpload.SaveAs(Server.MapPath(filePath + fileName));
            }
            cmd.Parameters.AddWithValue("@PhotoPath", filePath + fileName);
            cmd.Parameters.AddWithValue("@PhotoName", fileName);
            cmd.Parameters.AddWithValue("@DisplayName", txtDisplay.Text.Trim());
            cmd.Parameters.AddWithValue("@AadharNo", txtAadhar.Text.Trim());
            try
            {

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                //oo.MessageBox("Submitted successfully.", this.Page);
                // oo.ClearControls(this.Page);
            }
            catch (SqlException) { _recordNotInsertEmpGeneralDetail = false; flag = false; _con.Close(); }
            catch (Exception) { _recordNotInsertEmpGeneralDetail = false; flag = false; _con.Close(); }
            return flag;

        }

        public bool EmployeeDetail()
        {

            var flag = true;
            _recordNotInsertEmpEmployeeDetails = true;
            var cmd = new SqlCommand
            {
                CommandText = "EmpEmployeeDetailsProc",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };

            cmd.Parameters.AddWithValue("@Ecode", Session["Ecode"].ToString());
            cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
            _sql = "select ReligionId from ReligionMaster where ReligionName='" + DrpReligion.SelectedItem + "'";
            var rel = _oo.ReturnTag(_sql, "ReligionId");
            cmd.Parameters.AddWithValue("@EReligionId", rel);
            _sql = "select CasteId from CasteMaster where CasteName='" + DrpCategory.SelectedItem + "'";
            var cat = _oo.ReturnTag(_sql, "CasteId");
            cmd.Parameters.AddWithValue("@ECategoryId", cat);
            cmd.Parameters.AddWithValue("@EHeight", txtheight.Text.Trim());
            cmd.Parameters.AddWithValue("@Ediseases", txtdiseas.Text.Trim());
            cmd.Parameters.AddWithValue("@EIdentificationMark", txtidentmark.Text.Trim());
            cmd.Parameters.AddWithValue("@EHostelRequired", RadioButtonList3.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@ENationality", txtnat.Text.Trim());
            cmd.Parameters.AddWithValue("@ECaste", txtcaste.Text.Trim());
            _sql = "select BloodGroupId from BloodGroupMaster where BloodGroupName='" + drpblood.SelectedItem + "'";
            var blood = _oo.ReturnTag(_sql, "BloodGroupId");
            cmd.Parameters.AddWithValue("@EBloodGroupId", blood.Trim());
            cmd.Parameters.AddWithValue("@EWeight", txtweight.Text.Trim());
            cmd.Parameters.AddWithValue("@EHobbies", txthobbies.Text.Trim());
            cmd.Parameters.AddWithValue("@ETransportRequired", RadioButtonList4.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@Languages", txtdlanguages.Text.Trim());
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();

            }
            catch (SqlException) { _recordNotInsertEmpEmployeeDetails = false; flag = false; _con.Close(); }
            catch (Exception) { _recordNotInsertEmpEmployeeDetails = false; flag = false; _con.Close(); }
            return flag;



        }
        public bool EmployeeOfficialDetails()
        {
            var flag = true;
            _recordNotInsertEmpployeeOfficialDetails = true;
            var cmd = new SqlCommand
            {
                CommandText = "EmpployeeOfficialDetailsProc",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };
            _sql = "select max(SrNo) as Total from EmpployeeOfficialDetails";

            var ss = _oo.ReturnTag(_sql, "Total");
            int ss1;
            try
            {
                ss1 = Convert.ToInt32(ss.Trim());
            }
            catch (Exception) { ss1 = 0; }
            ss1 = ss1 + 1;


            Session["Ecode"] = IdGeneration(ss1.ToString());
            Session["EmpId"] = txtEmpId.Text.Trim();

            string pass = Campus.GenerateRandomNo(8);

            cmd.Parameters.AddWithValue("@Ecode", IdGeneration(ss1.ToString()));
            cmd.Parameters.AddWithValue("@EmpId", txtEmpId.Text.Trim());

            var dates = drpyearhai.SelectedItem + "/" + drpmonthhai.SelectedItem + "/" + drpdinhai.SelectedItem;
            cmd.Parameters.AddWithValue("@RegistrationDate", dates);
            cmd.Parameters.AddWithValue("@FileNo", txtFileno.Text.Trim());
            cmd.Parameters.AddWithValue("@Reference", txtrefere.Text.Trim());
            cmd.Parameters.AddWithValue("@TrainingDetails", txtTrainingDetails.Text.Trim());
            cmd.Parameters.AddWithValue("@TeachingSubjects", txtTeachingSubjects.Text.Trim());
            cmd.Parameters.AddWithValue("@Remark", txtremak.Text.Trim());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Designation", drpdes.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@DesNameNew", drpEmpdes.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@EmpCategory", drpempcategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PFNo", txtPFNo.Text.Trim());
            cmd.Parameters.AddWithValue("@UAN", txtUAN.Text.Trim());
            cmd.Parameters.AddWithValue("@EsicNo", txtEsicNo.Text.Trim());
            cmd.Parameters.AddWithValue("@MachineId", txtMachineid.Text.Trim());
            cmd.Parameters.AddWithValue("@MachineNo", ddlMachineNo.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@Password", pass);

            cmd.Parameters.AddWithValue("@PANno", txtpanno.Text.Trim());
            cmd.Parameters.AddWithValue("@BankName", ddlBank.SelectedValue);
            cmd.Parameters.AddWithValue("@BranchName", ddlBankBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@IFSC", txtbranchifsc.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", bankbranchadd.Text.Trim());
            cmd.Parameters.AddWithValue("@PinCode", branchpincode.Text.Trim());
            cmd.Parameters.AddWithValue("@AccountNo", TextBox14.Text.Trim());
            cmd.Parameters.AddWithValue("@AccountType", ddlAccType.SelectedValue);
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            catch (SqlException) { _recordNotInsertEmpployeeOfficialDetails = false; flag = false; _con.Close(); }
            catch (Exception) { _recordNotInsertEmpployeeOfficialDetails = false; flag = false; _con.Close(); }
            return flag;
        }

        public bool Validation()
        {
            //if (txtEmpId.Text == "")
            //{
            //    TabContainer1.ActiveTab = TabContainer1.Tabs[5];

            //    oo.MessageBox("Please Fill All Required Field in Official Details!", this.Page);
            //    return false;
            //}
            //else if (txtFirstName.Text == "" || txtfathername.Text == "" || txtmothname.Text == "" || txtPreseAdd.Text == "" || DrpPreSta.SelectedItem.ToString() == "<--Select-->" || DrpPresCity.SelectedItem.ToString() == "<--Select-->" || txtPermAdd.Text == "" || DrpPerState.SelectedItem.ToString() == "<--Select-->" || DrpPerCity.SelectedItem.ToString() == "<--Select-->")
            //{
            //    oo.MessageBox("Please Fill All Required Field in General Details!", this.Page);
            //    TabContainer1.ActiveTab = TabContainer1.Tabs[0];
            //    return false;
            //}
            //else if (DrpReligion.SelectedItem.ToString() == "<--Select-->" || DrpCategory.SelectedItem.ToString() == "<--Select-->" || drpblood.SelectedItem.ToString() == "<--Select-->")
            //{
            //    oo.MessageBox("Please Fill All Required Field in Employee Details!", this.Page);

            //    TabContainer1.ActiveTab = TabContainer1.Tabs[1];
            //    return false;
            //}
            //else if (DrpDesignation.SelectedItem.ToString() == "<--Select-->" || DrpCountry.SelectedItem.ToString() == "<--Select-->" || DrpState.SelectedItem.ToString() == "<--Select-->" || DrpCity.SelectedItem.ToString() == "<--Select-->")
            //{
            //    oo.MessageBox("Please Fill All Required Field in Previous Employment!", this.Page);

            //    TabContainer1.ActiveTab = TabContainer1.Tabs[2];
            //    return false;
            //}
            return true;
        }
        public bool PreviousSchoolDetails()
        {
            _recordNotInsertPrevious = true;
            var flag = true;

            for (var i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

                var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

                if (txtExam.Text.Trim() != "")
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "USP_EmployeePreviousSchool";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@StEnRCode", Session["Ecode"].ToString());
                        cmd.Parameters.AddWithValue("@SrNo", Session["EmpId"].ToString());
                        cmd.Parameters.AddWithValue("@Qualification", txtExam.Text.Trim());
                        cmd.Parameters.AddWithValue("@Board", drpBoard.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@Result", drpResult.SelectedValue.Trim());
                        cmd.Parameters.AddWithValue("@SchoolName", txtInstitute.Text.Trim() == "" ? DBNull.Value : (object)txtInstitute.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yop", txtYop.Text.Trim() == "" ? DBNull.Value : (object)txtYop.Text.Trim());
                        cmd.Parameters.AddWithValue("@Medium", drpMedium.Text.Trim());
                        cmd.Parameters.AddWithValue("@Subjects", txtSubject.Text.Trim() == "" ? DBNull.Value : (object)txtSubject.Text.Trim());
                        cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text.Trim() == "" ? DBNull.Value : (object)txtRollNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CertificateNo", txtCertificateNo.Text.Trim() == "" ? DBNull.Value : (object)txtCertificateNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@MarksSheetNo", txtMarksSheetNo.Text.Trim() == "" ? DBNull.Value : (object)txtMarksSheetNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaxMarks", txtMm.Text.Trim() == "" ? DBNull.Value : (object)txtMm.Text.Trim());
                        cmd.Parameters.AddWithValue("@Marks", txtObtained.Text.Trim() == "" ? DBNull.Value : (object)txtObtained.Text.Trim());
                        cmd.Parameters.AddWithValue("@Percentage", txtPer.Text.Trim() == "" ? DBNull.Value : (object)txtPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

                        try
                        {
                            _con.Open();
                            cmd.ExecuteNonQuery();
                            _con.Close();
                        }
                        catch (SqlException)
                        {
                            _recordNotInsertPrevious = false;
                            flag = false;
                            _con.Close();
                        }
                        catch (Exception)
                        {
                            _recordNotInsertPrevious = false; flag = false; _con.Close();
                        }
                    }
                }
            }
            return flag;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int Machineid = 0;
            int.TryParse(txtMachineid.Text.Trim(), out Machineid);
            if (txtMachineid.Text.Trim() != "" & ddlMachineNo.SelectedValue.Trim() == "")
            {
                _oo.MessageBox("Sorry, Please select Machin No.!", Page);
                return;
            }

            if (txtMachineid.Text.Trim() == "" & ddlMachineNo.SelectedValue.Trim() != "")
            {
                _oo.MessageBox("Sorry, Please enter Machin Id.!", Page);
                return;
            }

            if (Machineid == 0 & txtMachineid.Text.Trim() != "")
            {
                _oo.MessageBox("Sorry, Please enter valid Machin Id.!", Page);
                return;
            }
            if (txtEmpId.Text != string.Empty)
            {
                var sql1 = "Select EmpId from EmpployeeOfficialDetails where EmpId='" + txtEmpId.Text.Trim() + "'";
                var sql2 = "Select EmpId from EmpGeneralDetail where EmpId='" + txtEmpId.Text.Trim() + "'";
                var sql3 = "Select EmpId from EmpEmployeeDetails where EmpId='" + txtEmpId.Text.Trim() + "'";
                var sql4 = "Select EmpId from EmpPreviousEmployment where EmpId='" + txtEmpId.Text.Trim() + "'";
                var sql5 = "Select EmpId from EmpDocuments where EmpId='" + txtEmpId.Text.Trim() + "'";
                if (_oo.Duplicate(sql1) || _oo.Duplicate(sql2) || _oo.Duplicate(sql3) || _oo.Duplicate(sql4) || _oo.Duplicate(sql5))
                {
                    _oo.MessageBox("Duplicate Employee Id!", Page);
                }
                else if (Validation())
                {
                    EmployeeOfficialDetails();
                    EmpGeneralDetail();
                    EmployeeDetail();
                    PreviousEmployment();
                    PreviousSchoolDetails();
                    if (_recordNotInsertEmpployeeOfficialDetails == false || _recordNotInsertEmpGeneralDetail == false || _recordNotInsertEmpEmployeeDetails == false || _recordNotInsertEmpPreviousEmployment == false || _recordNotInsertPrevious == false)
                    {
                        _sql = "delete from EmpployeeOfficialDetails where Ecode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _sql = "delete from EmpGeneralDetail where Ecode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _sql = "delete from EmpEmployeeDetails where Ecode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _sql = "delete from EmpPreviousEmployment where Ecode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _sql = "delete from EmpDocuments where Ecode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _sql = "delete from EmployeeDocs where Ecode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _sql = "delete from EmployeePreviousSchool where StEnRCode='" + Session["Ecode"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.ProcedureDatabase(_sql);

                        _oo.MessageBox("Sorry, Record not Inserted!", Page);
                        return;
                    }
                    else
                    {
                        NewDocumentsDetails(LinkButton1);
                        EmployeePasswordGeneration();

                        try
                        {

                            _sql = "select EMobileNo from EmpGeneralDetail  where Ecode='" + Session["Ecode"] + "'  and SessionName='" + Session["SessionName"] + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                            var mobileNumber = _oo.ReturnTag(_sql, "EMobileNo");
                            SendRegSms(mobileNumber, Session["Ecode"].ToString());
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        _oo.MessageBox("Submitted successfully.", Page);
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, new ShowMSG().MSG("I"), "S");
                        _oo.ClearControls(Page);
                    }
                    txtnat.Text = "Indian";

                    _sql = "select SrNo  from EmpployeeOfficialDetails";
                    _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";

                }
            }
            else
            {
                _oo.MessageBoxforUpdatePanel("Please, Fill all star(*) fields!", LinkButton1);
            }

        }
        public void SendRegSms(string fmobileNo, string code)
        {
            _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
            if (_oo.ReturnTag(_sql, "HitValue") == "") return;
            if (_oo.ReturnTag(_sql, "HitValue") != "true") return;
            var sadpNew = new SMSAdapterNew();

            _sql = "Select EFirstName as EmployeeName from EmpGeneralDetail where  BranchCode=" + Session["BranchCode"].ToString() + "";
            _sql = _sql + " and  Ecode='" + Session["Ecode"].ToString().Trim() + "'";

            var employeeName = "Mr./Ms. " + _oo.ReturnTag(_sql, "EmployeeName");

            _sql = "Select CollegeShortNa from CollegeMaster  where  BranchCode=" + Session["BranchCode"].ToString() + "";
            var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");

            _sql = "Select Password from EmpployeeOfficialDetails where";
            _sql = _sql + " Ecode='" + Session["Ecode"].ToString().Trim() + "' and  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"] + "'";

            var ecode = Session["Ecode"].ToString().Trim();
            var employeePassword = _oo.ReturnTag(_sql, "Password");

            var mess = "Congrats! " + employeeName + ", you've registered successfully with " + collegeShortNa + ". Your Userid: " + ecode + " and Password: " + employeePassword + "";

            if (fmobileNo == "") return;
            _sql = "Select SmsSent From SmsEmailMaster where Id='10' and  BranchCode=" + Session["BranchCode"].ToString() + "";
            if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
            {
                sadpNew.Send(mess, fmobileNo);
            }
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
        protected void DrpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpYear, DrpMonth, DrpDate);
        }
        protected void DrpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpYear, DrpMonth, DrpDate);
        }
        protected void DrpPreSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_sql = "select Id, statename from StateMaster where countryid=" + DrpPreSCountry.SelectedValue + " order by statename asc";
            //_oo.FillDropDown_withValue(_sql, DrpPreSta, "statename", "id");

            _sql = "select id, ltrim(rtrim(statename)) as statename from StateMaster where countryid=" + DrpPreSCountry.SelectedValue + " order by statename asc";
            DataTable dt = new DataTable();
            dt = _oo.Fetchdata(_sql);
            Session["states"] = dt;
            DrpPreSta.DataValueField = dt.Columns["id"].ToString();
            DrpPreSta.DataTextField = dt.Columns["StateName"].ToString();
            DrpPreSta.DataSource = dt;
            DrpPreSta.DataBind();


        }
        protected void DrpPreSta_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_sql = "Select CityName, id from CityMaster where StateId=" + DrpPreSta.SelectedValue + " order by CityName asc";
            //_oo.FillDropDown_withValue(_sql, DrpPresCity, "CityName", "id");

            _sql = "SELECT id, ltrim(rtrim(CityName)) as CityName from CityMaster where stateid=" + DrpPreSta.SelectedValue + " order by CityName asc";
            DataTable dt = new DataTable();
            dt = _oo.Fetchdata(_sql);
            Session["cities"] = dt;
            DrpPresCity.DataValueField = dt.Columns["id"].ToString();
            DrpPresCity.DataTextField = dt.Columns["CityName"].ToString();
            DrpPresCity.DataSource = dt;
            DrpPresCity.DataBind();

            if (CheckBox1.Checked)
            {
                DrpPerState.Text = DrpPreSta.Text.Trim();
                DrpPreSta.SelectedValue = DrpPerState.SelectedValue;
                DrpPerCity.DataValueField = dt.Columns["id"].ToString();
                DrpPerCity.DataTextField = dt.Columns["CityName"].ToString();
                DrpPerCity.DataSource = dt;
                DrpPerCity.DataBind();
            }

        }

        protected void DrpPerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "select Id, statename from StateMaster where countryid=" + DrpPerCountry.SelectedValue + " order by statename asc";
            _oo.FillDropDown_withValue(_sql, DrpPerState, "statename", "id");
        }
        protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = "Select CityName, id from CityMaster where StateId=" + DrpPerState.SelectedValue + " order by CityName asc";
            _oo.FillDropDown_withValue(_sql, DrpPerCity, "CityName", "id");
        }
        protected void DrpPerCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                txtPermAdd.Text = txtPreseAdd.Text.Trim();
                DrpPerCountry.Text = DrpPreSCountry.Text.Trim();

                if (Session["states"] != null)
                {
                    DataTable dt = new DataTable(); dt = (DataTable)Session["states"];
                    DrpPerState.DataValueField = dt.Columns["id"].ToString();
                    DrpPerState.DataTextField = dt.Columns["StateName"].ToString();
                    DrpPerState.DataSource = dt;
                    DrpPerState.DataBind();
                }
                DrpPerState.Text = DrpPreSta.Text.Trim();

                if (Session["cities"] != null)
                {
                    DataTable dt = new DataTable(); dt = (DataTable)Session["cities"];
                    DrpPerCity.DataValueField = dt.Columns["id"].ToString();
                    DrpPerCity.DataTextField = dt.Columns["CityName"].ToString();
                    DrpPerCity.DataSource = dt;
                    DrpPerCity.DataBind();
                }

                DrpPerCity.Text = DrpPresCity.Text.Trim();
                txtPerZip.Text = DrpPresZip.Text.Trim();
                DrpCountry.Enabled = false;
                DrpPerState.Enabled = false;
                DrpPerCity.Enabled = false;
            }
            else
            {
                txtPermAdd.Text = "";
                txtPerZip.Text = "";
                DrpCountry.Enabled = true;
                DrpPerState.Enabled = true;
                DrpPerCity.Enabled = true;
                //_sql = "select ltrim(rtrim(StateName)) as StateName from StateMaster ";
                //_oo.FillDropDown(_sql, DrpPerState, "StateName");
                //_sql = "select ltrim(rtrim(CityName)) as CityName from CityMaster ";
                //_oo.FillDropDown(_sql, DrpPerCity, "CityName");
            }
        }
        protected void DrpYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drp = sender as DropDownList;

            // ReSharper disable once PossibleNullReferenceException
            var drpYear2 = (DropDownList)drp.NamingContainer.FindControl("DrpYear2");
            var drpMonth2 = (DropDownList)drp.NamingContainer.FindControl("DrpMonth2");
            var drpDate2 = (DropDownList)drp.NamingContainer.FindControl("DrpDate2");
            _oo.YearDropDown(drpYear2, drpMonth2, drpDate2);
        }
        protected void DrpMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drp = sender as DropDownList;

            // ReSharper disable once PossibleNullReferenceException
            var drpYear2 = (DropDownList)drp.NamingContainer.FindControl("DrpYear2");
            var drpMonth2 = (DropDownList)drp.NamingContainer.FindControl("DrpMonth2");
            var drpDate2 = (DropDownList)drp.NamingContainer.FindControl("DrpDate2");
            _oo.YearDropDown(drpYear2, drpMonth2, drpDate2);
        }
        public void FindCurrentDateandSetinDropDown()
        {
            var dd = _oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
            var mm = _oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
            var yy = _oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

            DrpYear.Text = yy;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (mm)
            {
                case "1":
                    DrpMonth.Text = "Jan";
                    break;
                case "2":
                    DrpMonth.Text = "Feb";
                    break;
                case "3":
                    DrpMonth.Text = "Mar";
                    break;
                case "4":
                    DrpMonth.Text = "Apr";
                    break;
                case "5":
                    DrpMonth.Text = "May";
                    break;
                case "6":
                    DrpMonth.Text = "Jun";
                    break;
                case "7":
                    DrpMonth.Text = "Jul";
                    break;
                case "8":
                    DrpMonth.Text = "Aug";
                    break;
                case "9":
                    DrpMonth.Text = "Sep";
                    break;
                case "10":
                    DrpMonth.Text = "Oct";
                    break;
                case "11":
                    DrpMonth.Text = "Nov";
                    break;
                case "12":
                    DrpMonth.Text = "Dec";
                    break;
            }


            DrpDate.Text = dd;
        }

        protected void DrpYear3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drp = sender as DropDownList;

            // ReSharper disable once PossibleNullReferenceException
            var drpYear3 = (DropDownList)drp.NamingContainer.FindControl("DrpYear3");
            var drpMonth3 = (DropDownList)drp.NamingContainer.FindControl("DrpMonth3");
            var drpDate3 = (DropDownList)drp.NamingContainer.FindControl("DrpDate3");
            _oo.YearDropDown(drpYear3, drpMonth3, drpDate3);
        }
        protected void DrpMonth3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drp = sender as DropDownList;

            // ReSharper disable once PossibleNullReferenceException
            var drpYear3 = (DropDownList)drp.NamingContainer.FindControl("DrpYear3");
            var drpMonth3 = (DropDownList)drp.NamingContainer.FindControl("DrpMonth3");
            var drpDate3 = (DropDownList)drp.NamingContainer.FindControl("DrpDate3");
            _oo.YearDropDown(drpYear3, drpMonth3, drpDate3);
        }


        protected void DrpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                _sql = "select ltrim(rtrim(CountryName)) as CountryName,id from CountryMaster where id='" + DrpCountry.SelectedValue + "'";
                _oo.FillDropDown_withValue(_sql, drpCountry, "CountryName", "id");
                _sql = "select ltrim(rtrim(StateName)) as StateName,id from StateMaster where Countryid='" + DrpCountry.SelectedValue + "'";
                _oo.FillDropDown_withValue(_sql, drpState, "StateName", "id");
            }
        }
        protected void DrpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            //{
            //    var drpState = (DropDownList) reppreviousemployment.Items[i].FindControl("DrpState");
            //    var drpCity = (DropDownList) reppreviousemployment.Items[i].FindControl("DrpCity");

            //    _sql = "select ltrim(rtrim(CityName)) as CityName,id from CityMaster where stateid='"+ drpState .SelectedValue+ "'";
            //    _oo.FillDropDown_withValue(_sql, drpCity, "CityName", "id");
            //}
            var drp = sender as DropDownList;

            // ReSharper disable once PossibleNullReferenceException
            var drpCity = (DropDownList)drp.NamingContainer.FindControl("DrpCity");
            // ReSharper disable once RedundantAssignment
            var cc = "";
            _sql = "select Id from StateMaster where StateName='" + drp.SelectedItem + "'";
            cc = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName from CityMaster where stateid=" + cc;
            _oo.FillDropDown(_sql, drpCity, "CityName");
        }
        protected void drpyearhai_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(drpyearhai, drpmonthhai, drpdinhai);

        }
        protected void drpmonthhai_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(drpyearhai, drpmonthhai, drpdinhai);
        }
        protected void drpdinhai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void FindCurrentDateandSetinDropDownhai()
        {

            var dd = _oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
            var mm = _oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
            var yy = _oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

            drpyearhai.Text = yy;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (mm)
            {
                case "1":
                    drpmonthhai.Text = "Jan";
                    break;
                case "2":
                    drpmonthhai.Text = "Feb";
                    break;
                case "3":
                    drpmonthhai.Text = "Mar";
                    break;
                case "4":
                    drpmonthhai.Text = "Apr";
                    break;
                case "5":
                    drpmonthhai.Text = "May";
                    break;
                case "6":
                    drpmonthhai.Text = "Jun";
                    break;
                case "7":
                    drpmonthhai.Text = "Jul";
                    break;
                case "8":
                    drpmonthhai.Text = "Aug";
                    break;
                case "9":
                    drpmonthhai.Text = "Sep";
                    break;
                case "10":
                    drpmonthhai.Text = "Oct";
                    break;
                case "11":
                    drpmonthhai.Text = "Nov";
                    break;
                case "12":
                    drpmonthhai.Text = "Dec";
                    break;
            }


            drpdinhai.Text = dd;
        }

        public void ComposeSMS(string emp_code)
        {
            //  string userid = txtUserId.Text.ToString().Replace(" ", "");
            try
            {
                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@SessionName",Session["SessionName"]),
                    new SqlParameter("@BranchCode",Session["BranchCode"]),
                    new SqlParameter("@EmpCode",emp_code.ToString().Replace(" ", ""))

                };
                DataSet ds = _oo.ReturnDataSet("USP_EmployeeRegistrationTemplate", param.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    string msg = SendSms(ds);
                }

            }
            catch
            {

            }

        }

        public string SendSms(DataSet ds)
        {
            string msg;
            try
            {
                DataTable data = new DataTable();
                data = ds.Tables[0];

                var contactNo = data.Rows[0]["EmobileNo"].ToString();

                DataTable template = new DataTable();
                template = ds.Tables[1];

                msg = template.Rows[0][0].ToString();
                string[] param = template.Rows[0][1].ToString().Split(',');

                string[] daynamicVariables = msg.Split(new char[0]);
                foreach (var para in param)
                {
                    string value = data.Rows[0][para].ToString();
                    for (int i = 0; i < daynamicVariables.Count(); i++)
                    {
                        if (daynamicVariables[i].ToString() == "{{{}}}")
                        {
                            daynamicVariables[i] = value;
                            break;
                        }
                    }
                }

                msg = string.Join(" ", daynamicVariables);

                new SMS().SendSms(contactNo, msg, "12", Session["BranchCode"].ToString());

            }
            catch
            {
                msg = "";
            }
            return msg;
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
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
            try
            {
                _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
                _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
                var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
                PermissionGrant(a, LinkButton1);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void EmployeePasswordGeneration()
        {
            var password = Campus.GenerateRandomNo(8);
            _sql = "select  Ecode from EmpployeeOfficialDetails where EmpId='" + txtEmpId.Text.Trim() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
            var uid = _oo.ReturnTag(_sql, "Ecode");
            var cmd = new SqlCommand
            {
                CommandText = "LoginTabProc",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };
            cmd.Parameters.AddWithValue("@LoginName", uid);
            cmd.Parameters.AddWithValue("@UserName", uid);
            cmd.Parameters.AddWithValue("@Pass", password);
            cmd.Parameters.AddWithValue("@LoginTypeId", 3);
            cmd.Parameters.AddWithValue("@SessionId", Session["SessionID"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                _sql = "update EmpployeeOfficialDetails set Password='" + password + "' where Ecode='" + uid + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                _oo.ProcedureDatabase(_sql);
                SendNotificationToEmployee(uid, password);
                ComposeSMS(uid);

            }
            catch (SqlException ex) { _con.Close(); }
            catch (Exception ex1) { _con.Close(); }
        }
        public void SendNotificationToEmployee(string uid, string password)
        {
            var mess = "Staff Login Panel";
            mess = mess + "<br>";
            mess = mess + "</hr>";
            mess = mess + "<br>";
            mess = mess + "Dear ";
            mess = mess + "             " + txtFirstName.Text.Trim() + " " + txtmidName.Text.Trim() + " " + txtlastName.Text.Trim() + ",";
            mess = mess + "<br></hr>";
            mess = mess + "Your User ID  :" + uid;
            mess = mess + "<Br>";
            mess = mess + "Your Password :" + password;

            _sql = "select EEmail  from EmpGeneralDetail where Ecode='" + uid + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";

            EmailSending(mess, "eAM: Staff Login Credentials", _oo.ReturnTag(_sql, "EEmail"));

        }

        public bool EmailSending(string mess, string subjectParameter, string toEmailId)
        {
            var send = false;
            var mail = new MailMessage();
            mail.To.Add(toEmailId);//to ID

            mail.From = new MailAddress("donotreply@eam.co.in");
            mail.Subject = subjectParameter;



            mail.Body = mess;
            mail.IsBodyHtml = true;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("donotreply@eam.co.in", "reNply_33@9D"),
                EnableSsl = true
            };
            //Or Your SMTP Server Address
            //from id
            //Or your Smtp Email ID and Password
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

        protected void txtmobileno_TextChanged(object sender, EventArgs e)
        {

        }



        /* CREATED BY Hariom Dated : 12-03-21 */
        protected void ddlBankBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBankBranch.Items.Clear();

            if (ddlBank.SelectedIndex > 0)
            {
                string bankId = ddlBank.SelectedValue.ToString();
                DataTable dt = new DataTable();
                dt = _oo.Fetchdata("SELECT id, BankId, BankBranchName, [Address], IFSC, PIN, Remark FROM AccBankBranchMaster WHERE IsActive=1 AND BankId=" + ddlBank.SelectedValue.ToString());
                ddlBankBranch.DataSource = dt;
                ddlBankBranch.DataValueField = "id";
                ddlBankBranch.DataTextField = "BankBranchName";
                ddlBankBranch.DataBind();
            }
            ddlBank.Focus();
        }

        protected void ddlBankBranchDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBankBranch.SelectedItem.Text == "--Select--")
                {
                    txtbranchifsc.Text = bankbranchadd.Text = branchpincode.Text = String.Empty;
                }
                else
                {
                    if (ddlBankBranch.SelectedIndex <= 0) return;
                    _sql = "SELECT Address ,IFSC ,PIN FROM M03_BankBranchMaster";
                    _sql = _sql + "  WHERE IsDelete = 0 AND M02ID='" + ddlBank.SelectedValue.Trim() +
                          "' AND  BankBranchName='" + ddlBankBranch.SelectedItem.Text.Trim() + "'";
                    try
                    {
                        txtbranchifsc.Text = _oo.ReturnTag(_sql, "IFSC");
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        bankbranchadd.Text = _oo.ReturnTag(_sql, "Address");
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    try
                    {
                        branchpincode.Text = _oo.ReturnTag(_sql, "PIN");
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        protected void LoadBoard()
        {
            for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                _sql = "Select BoardName from BoardMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
                _oo.FillDropDown(_sql, drpBoard, "BoardName");
            }
        }

        protected DataTable AddColumn()
        {
            // ReSharper disable once LocalVariableHidesMember
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
        protected void ReIndexingofSrNo()
        {
            for (int j = 0; j < rptPreviousEducation.Items.Count; j++)
            {
                var lblsrno = (Label)rptPreviousEducation.Items[j].FindControl("lblsrno");
                lblsrno.Text = (j + 1).ToString();
            }
        }
        // ReSharper disable once ParameterHidesMember
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
        protected void AddPreviousInstitutionGridRow()
        {
            // ReSharper disable once LocalVariableHidesMember
            var dt = AddColumn();

            if (rptPreviousEducation.Items.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    dt.Rows.Add(SetInitialValue(dt, i));
                }
                rptPreviousEducation.DataSource = dt;
                rptPreviousEducation.DataBind();
                LoadBoard();
            }
            else
            {
                var i = 0;
                while (i < rptPreviousEducation.Items.Count)
                {
                    var dr = dt.NewRow();
                    // ReSharper disable once IdentifierTypo
                    var lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
                    var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                    var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                    var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                    var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                    var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                    var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                    var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                    var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                    var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                    var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

                    var txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
                    var txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
                    var txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
                    var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                    var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                    var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

                    dr["srno"] = lblsrno.Text.Trim();
                    dr["exam"] = txtExam.Text.Trim();
                    dr["board"] = drpBoard.SelectedValue;
                    dr["result"] = drpResult.SelectedValue;
                    dr["institute"] = txtInstitute.Text.Trim();
                    dr["yop"] = txtYop.Text.Trim();
                    dr["medium"] = drpMedium.SelectedValue;
                    dr["subject"] = txtSubject.Text.Trim();
                    dr["rollno"] = txtRollNo.Text.Trim();
                    dr["certificateno"] = txtCertificateNo.Text.Trim();
                    dr["marksSheetno"] = txtMarksSheetNo.Text.Trim();
                    dr["city"] = txtCity.Text.Trim();
                    dr["state"] = txtState.Text.Trim();
                    dr["country"] = txtCountry.Text.Trim();
                    dr["maxmarks"] = txtMm.Text.Trim();
                    dr["obtained"] = txtObtained.Text.Trim();
                    dr["per"] = txtPer.Text.Trim();
                    dt.Rows.Add(dr);
                    i++;

                    if (i == rptPreviousEducation.Items.Count)
                    {
                        var dr1 = dt.NewRow();
                        dr1[0] = i + 1;
                        dt.Rows.Add(dr1);
                        _sql = "Select BoardName from BoardMaster wher  BranchCode=" + Session["BranchCode"].ToString() + "";
                        _oo.FillDropDown(_sql, drpBoard, "BoardName");
                    }
                }
                rptPreviousEducation.DataSource = dt;
                rptPreviousEducation.DataBind();
                EnableControl();
                LoadBoard();
                SetDropdownSelectedValue(dt);
            }
        }
        // ReSharper disable once ParameterHidesMember
        protected void SetDropdownSelectedValue(DataTable dt)
        {
            for (int j = 0; j < rptPreviousEducation.Items.Count; j++)
            {
                var drpBoard = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpBoard");
                var drpResult = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpResult");
                var drpMedium = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpMedium");
                drpBoard.SelectedValue = dt.Rows[j]["board"].ToString();
                drpResult.SelectedValue = dt.Rows[j]["result"].ToString();
                drpMedium.SelectedValue = dt.Rows[j]["medium"].ToString();
            }
        }
        protected void EnableControl()
        {
            for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

                //TextBox txtCity = (TextBox))rptPreviousEducation.Items[i].FindControl("txtCity");
                //TextBox txtState = (TextBox))rptPreviousEducation.Items[i].FindControl("txtState");
                //TextBox txtCountry = (TextBox))rptPreviousEducation.Items[i].FindControl("txtCountry");
                var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");
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
                    txtMm.Enabled = true;
                    txtObtained.Enabled = true;
                    txtPer.Enabled = true;
                }
            }
        }
        protected void DeletePreviousInstitutionGridRow(int rowindex)
        {
            // ReSharper disable once LocalVariableHidesMember
            var dt = AddColumn();

            int i = 0;
            while (i < rptPreviousEducation.Items.Count)
            {
                var dr = dt.NewRow();
                var lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
                var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

                var txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
                var txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
                var txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
                var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

                dr["srno"] = lblsrno.Text.Trim();
                dr["exam"] = txtExam.Text.Trim();
                dr["board"] = drpBoard.SelectedValue;
                dr["result"] = drpResult.SelectedValue;
                dr["institute"] = txtInstitute.Text.Trim();
                dr["yop"] = txtYop.Text.Trim();
                dr["medium"] = drpMedium.SelectedValue;
                dr["subject"] = txtSubject.Text.Trim();
                dr["rollno"] = txtRollNo.Text.Trim();
                dr["certificateno"] = txtCertificateNo.Text.Trim();
                dr["marksSheetno"] = txtMarksSheetNo.Text.Trim();
                dr["city"] = txtCity.Text.Trim();
                dr["state"] = txtState.Text.Trim();
                dr["country"] = txtCountry.Text.Trim();
                dr["maxmarks"] = txtMm.Text.Trim();
                dr["obtained"] = txtObtained.Text.Trim();
                dr["per"] = txtPer.Text.Trim();
                dt.Rows.Add(dr);
                i++;
            }

            dt.Rows.RemoveAt(rowindex);
            rptPreviousEducation.DataSource = dt;
            rptPreviousEducation.DataBind();
            LoadBoard();
            ReIndexingofSrNo();
            SetDropdownSelectedValue(dt);
            EnableControl();
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            // ReSharper disable once IdentifierTypo
            var currntrow = (RepeaterItem)lnk.NamingContainer;
            var i = currntrow.ItemIndex;
            DeletePreviousInstitutionGridRow(i);
        }

        protected void lnkAddMore_OnClick(object sender, EventArgs e)
        {
            AddPreviousInstitutionGridRow();
        }
        public bool PreviousEmployment()
        {
            var flag = true;
            _recordNotInsertEmpPreviousEmployment = true;
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                var drpCity = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCity");
                var txtName0Rga = (TextBox)reppreviousemployment.Items[i].FindControl("txtName0rga");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var drptyporganizat = (TextBox)reppreviousemployment.Items[i].FindControl("drptyporganizat");
                var drpDesignation = (TextBox)reppreviousemployment.Items[i].FindControl("DrpDesignation");
                var txtProfile = (TextBox)reppreviousemployment.Items[i].FindControl("txtProfile");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var txtdealin = (TextBox)reppreviousemployment.Items[i].FindControl("txtdealin");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var txtaddres = (TextBox)reppreviousemployment.Items[i].FindControl("txtaddres");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var txtreasonresign = (TextBox)reppreviousemployment.Items[i].FindControl("txtreasonresign");
                //var txtCity = (TextBox)reppreviousemployment.Items[i].FindControl("txtCity");
                //var txtState = (TextBox)reppreviousemployment.Items[i].FindControl("txtState");

                var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");
                // ReSharper disable once IdentifierTypo
                var datee = drpYear2.SelectedItem + "/" + drpMonth2.SelectedItem + "/" + drpDate2.SelectedItem;

                var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear3");
                var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth3");
                var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate3");
                // ReSharper disable once IdentifierTypo
                var dateee = drpYear3.SelectedItem + "/" + drpMonth3.SelectedItem + "/" + drpDate3.SelectedItem;

                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = "EmpPreviousEmploymentProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    cmd.Parameters.AddWithValue("@Ecode", Session["Ecode"].ToString());
                    cmd.Parameters.AddWithValue("@EmpId", Session["EmpId"].ToString());
                    cmd.Parameters.AddWithValue("@ECountryId", drpCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@EStateId", drpState.SelectedValue);
                    cmd.Parameters.AddWithValue("@ECityId", drpCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@ETypeOfOrganization", drptyporganizat.Text.Trim());
                    cmd.Parameters.AddWithValue("@EDesignation", drpDesignation.Text.Trim());
                    cmd.Parameters.AddWithValue("@EProfile", txtProfile.Text.Trim());
                    cmd.Parameters.AddWithValue("@ENameOfOrganization", txtName0Rga.Text.Trim());
                    cmd.Parameters.AddWithValue("@EDealsIn", txtdealin.Text.Trim());
                    cmd.Parameters.AddWithValue("@EFromDate", datee);
                    cmd.Parameters.AddWithValue("@EToDate", dateee);
                    cmd.Parameters.AddWithValue("@EAddress", txtaddres.Text.Trim());
                    cmd.Parameters.AddWithValue("@EReasonOfResign", txtreasonresign.Text.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                    catch (SqlException)
                    {
                        // throw new Exception("some reason to rethrow", ex);
                        _recordNotInsertEmpPreviousEmployment = false;
                        flag = false;
                        _con.Close();
                    }
                    catch (Exception)
                    {
                        _recordNotInsertEmpPreviousEmployment = false;
                        flag = false;
                        _con.Close();
                    }
                }
            }
            return flag;
        }
        public void FindCurrentDateandSetinDropDown2()
        {
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");
                var dd = _oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
                var mm = _oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
                var yy = _oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

                drpYear2.Text = yy;
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (mm)
                {
                    case "1":
                        drpMonth2.Text = "Jan";
                        break;
                    case "2":
                        drpMonth2.Text = "Feb";
                        break;
                    case "3":
                        drpMonth2.Text = "Mar";
                        break;
                    case "4":
                        drpMonth2.Text = "Apr";
                        break;
                    case "5":
                        drpMonth2.Text = "May";
                        break;
                    case "6":
                        drpMonth2.Text = "Jun";
                        break;
                    case "7":
                        drpMonth2.Text = "Jul";
                        break;
                    case "8":
                        drpMonth2.Text = "Aug";
                        break;
                    case "9":
                        drpMonth2.Text = "Sep";
                        break;
                    case "10":
                        drpMonth2.Text = "Oct";
                        break;
                    case "11":
                        drpMonth2.Text = "Nov";
                        break;
                    case "12":
                        drpMonth2.Text = "Dec";
                        break;
                }
                drpDate2.Text = dd;
            }
        }
        public void FindCurrentDateandSetinDropDown3()
        {
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpYear3");
                var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpMonth3");
                var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpDate3");
                var dd = _oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
                var mm = _oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
                var yy = _oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

                drpYear3.Text = yy;
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (mm)
                {
                    case "1":
                        drpMonth3.Text = "Jan";
                        break;
                    case "2":
                        drpMonth3.Text = "Feb";
                        break;
                    case "3":
                        drpMonth3.Text = "Mar";
                        break;
                    case "4":
                        drpMonth3.Text = "Apr";
                        break;
                    case "5":
                        drpMonth3.Text = "May";
                        break;
                    case "6":
                        drpMonth3.Text = "Jun";
                        break;
                    case "7":
                        drpMonth3.Text = "Jul";
                        break;
                    case "8":
                        drpMonth3.Text = "Aug";
                        break;
                    case "9":
                        drpMonth3.Text = "Sep";
                        break;
                    case "10":
                        drpMonth3.Text = "Oct";
                        break;
                    case "11":
                        drpMonth3.Text = "Nov";
                        break;
                    case "12":
                        drpMonth3.Text = "Dec";
                        break;
                }
                drpDate3.Text = dd;
            }
        }
        public void PreviousEmploymentDropDow()
        {
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                var drpCity = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCity");
                _sql = "select ltrim(rtrim(CountryName)) as CountryName,id from CountryMaster";
                _oo.FillDropDown_withValue(_sql, drpCountry, "CountryName", "id");
                _sql = "select ltrim(rtrim(StateName)) as StateName,id from StateMaster where CountryId='1'";
                _oo.FillDropDown_withValue(_sql, drpState, "StateName", "id");
                _sql = "select ltrim(rtrim(CityName)) as CityName,id from CityMaster where stateid='33'";
                _oo.FillDropDown_withValue(_sql, drpCity, "CityName", "id");
                //_sql = "select EmpDesName from EmpDesMaster ";
                //_oo.FillDropDown(_sql, DrpDesignation, "EmpDesName");

                var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");

                _oo.AddDateMonthYearDropDown(drpYear2, drpMonth2, drpDate2);
                FindCurrentDateandSetinDropDown2();

                var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpYear3");
                var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpMonth3");
                var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpDate3");
                _oo.AddDateMonthYearDropDown(drpYear3, drpMonth3, drpDate3);
                FindCurrentDateandSetinDropDown3();
                drpCountry.SelectedValue = "1";
                drpState.SelectedValue = "33";
                drpCity.SelectedValue = "543";
            }
        }
        protected DataTable AddColumn1()
        {
            // ReSharper disable once LocalVariableHidesMember
            // ReSharper disable once InconsistentNaming
            var _dt1 = new DataTable();
            _dt1.Columns.Add("srno1");
            _dt1.Columns.Add("Country");
            _dt1.Columns.Add("State");
            _dt1.Columns.Add("City");
            _dt1.Columns.Add("NameofOrganization");
            _dt1.Columns.Add("TypeofOrganization");
            _dt1.Columns.Add("Designation");
            _dt1.Columns.Add("Profile");
            _dt1.Columns.Add("Details");
            _dt1.Columns.Add("Address");
            _dt1.Columns.Add("ReasonofResign");
            _dt1.Columns.Add("From");
            _dt1.Columns.Add("To");

            return _dt1;
        }
        protected void ReIndexingofSrNo1()
        {
            for (int j = 0; j < reppreviousemployment.Items.Count; j++)
            {
                var lblsrno1 = (Label)rptPreviousEducation.Items[j].FindControl("lblsrno1");
                lblsrno1.Text = (j + 1).ToString();
            }
        }
        protected DataRow SetInitialValue1(DataTable dt1)
        {
            var dr = dt1.NewRow();
            dr[0] = 1;
            dr["Country"] = ""; dr["State"] = ""; dr["City"] = "";
            dr["NameofOrganization"] = ""; dr["TypeofOrganization"] = "";
            dr["Designation"] = ""; dr["Profile"] = "";
            dr["Details"] = ""; dr["Address"] = "";
            dr["ReasonofResign"] = ""; dr["From"] = ""; dr["To"] = "";
            return dr;
        }
        protected void AddPreviousEmploymentGridRow()
        {
            // ReSharper disable once LocalVariableHidesMember
            var dt1 = AddColumn1();

            if (reppreviousemployment.Items.Count == 0)
            {
                dt1.Rows.Add(SetInitialValue1(dt1));
                reppreviousemployment.DataSource = dt1;
                reppreviousemployment.DataBind();
                PreviousEmploymentDropDow();
            }
            else
            {
                var i = 0;
                while (i < reppreviousemployment.Items.Count)
                {
                    var dr = dt1.NewRow();
                    // ReSharper disable once IdentifierTypo
                    var lblsrno1 = (Label)reppreviousemployment.Items[i].FindControl("lblsrno1");
                    var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                    var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                    var drpCity = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCity");
                    var txtName0Rga = (TextBox)reppreviousemployment.Items[i].FindControl("txtName0rga");
                    // ReSharper disable once IdentifierTypo
                    // ReSharper disable once StringLiteralTypo
                    var drptyporganizat = (TextBox)reppreviousemployment.Items[i].FindControl("drptyporganizat");
                    var drpDesignation = (TextBox)reppreviousemployment.Items[i].FindControl("DrpDesignation");
                    var txtProfile = (TextBox)reppreviousemployment.Items[i].FindControl("txtProfile");
                    // ReSharper disable once IdentifierTypo
                    // ReSharper disable once StringLiteralTypo
                    var txtdealin = (TextBox)reppreviousemployment.Items[i].FindControl("txtdealin");
                    // ReSharper disable once IdentifierTypo
                    // ReSharper disable once StringLiteralTypo
                    var txtaddres = (TextBox)reppreviousemployment.Items[i].FindControl("txtaddres");
                    // ReSharper disable once IdentifierTypo
                    // ReSharper disable once StringLiteralTypo
                    var txtreasonresign = (TextBox)reppreviousemployment.Items[i].FindControl("txtreasonresign");
                    //var txtCity = (TextBox)reppreviousemployment.Items[i].FindControl("txtCity");
                    //var txtState = (TextBox)reppreviousemployment.Items[i].FindControl("txtState");

                    var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                    var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                    var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");
                    // ReSharper disable once IdentifierTypo
                    var datee = drpYear2.SelectedItem + "/" + drpMonth2.SelectedItem + "/" + drpDate2.SelectedItem;

                    var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear3");
                    var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth3");
                    var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate3");
                    var dateee = drpYear3.SelectedItem + "/" + drpMonth3.SelectedItem + "/" + drpDate3.SelectedItem;

                    dr["srno1"] = lblsrno1.Text.Trim();
                    dr["Country"] = drpCountry.SelectedValue;
                    dr["State"] = drpState.SelectedValue;
                    dr["City"] = drpCity.SelectedValue;
                    dr["NameofOrganization"] = txtName0Rga.Text.Trim();
                    dr["TypeofOrganization"] = drptyporganizat.Text.Trim();
                    dr["Designation"] = drpDesignation.Text.Trim();
                    dr["Profile"] = txtProfile.Text.Trim();
                    dr["Details"] = txtdealin.Text.Trim();
                    dr["Address"] = txtaddres.Text.Trim();
                    dr["ReasonofResign"] = txtreasonresign.Text.Trim();
                    dr["From"] = datee;
                    dr["To"] = dateee;

                    dt1.Rows.Add(dr);
                    i++;

                    if (i == reppreviousemployment.Items.Count)
                    {
                        var dr1 = dt1.NewRow();
                        dr1[0] = i + 1;
                        dt1.Rows.Add(dr1);
                    }
                }
                reppreviousemployment.DataSource = dt1;
                reppreviousemployment.DataBind();
                PreviousEmploymentDropDow();
            }
        }
        protected void DeletePreviousInstitutionGridRow1(int rowindex)
        {

            // ReSharper disable once LocalVariableHidesMember
            var dt1 = AddColumn1();

            var i = 0;
            while (i < reppreviousemployment.Items.Count)
            {
                var dr = dt1.NewRow();
                // ReSharper disable once IdentifierTypo
                var lblsrno1 = (Label)reppreviousemployment.Items[i].FindControl("lblsrno1");
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                var drpCity = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCity");
                var txtName0Rga = (TextBox)reppreviousemployment.Items[i].FindControl("txtName0rga");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var drptyporganizat = (TextBox)reppreviousemployment.Items[i].FindControl("drptyporganizat");
                var drpDesignation = (TextBox)reppreviousemployment.Items[i].FindControl("DrpDesignation");
                var txtProfile = (TextBox)reppreviousemployment.Items[i].FindControl("txtProfile");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var txtdealin = (TextBox)reppreviousemployment.Items[i].FindControl("txtdealin");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var txtaddres = (TextBox)reppreviousemployment.Items[i].FindControl("txtaddres");
                // ReSharper disable once IdentifierTypo
                // ReSharper disable once StringLiteralTypo
                var txtreasonresign = (TextBox)reppreviousemployment.Items[i].FindControl("txtreasonresign");
                //var txtCity = (TextBox)reppreviousemployment.Items[i].FindControl("txtCity");
                //var txtState = (TextBox)reppreviousemployment.Items[i].FindControl("txtState");

                var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");
                // ReSharper disable once IdentifierTypo
                var datee = drpYear2.SelectedItem + "/" + drpMonth2.SelectedItem + "/" + drpDate2.SelectedItem;

                var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear3");
                var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth3");
                var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate3");
                var dateee = drpYear3.SelectedItem + "/" + drpMonth3.SelectedItem + "/" + drpDate3.SelectedItem;

                dr["srno1"] = lblsrno1.Text.Trim();
                dr["Country"] = drpCountry.SelectedValue;
                dr["State"] = drpState.SelectedValue;
                dr["City"] = drpCity.SelectedValue;
                dr["NameofOrganization"] = txtName0Rga.Text.Trim();
                dr["TypeofOrganization"] = drptyporganizat.Text.Trim();
                dr["Designation"] = drpDesignation.Text.Trim();
                dr["Profile"] = txtProfile.Text.Trim();
                dr["Details"] = txtdealin.Text.Trim();
                dr["Address"] = txtaddres.Text.Trim();
                dr["ReasonofResign"] = txtreasonresign.Text.Trim();
                dr["From"] = datee;
                dr["To"] = dateee;

                dt1.Rows.Add(dr);
                i++;
            }

            dt1.Rows.RemoveAt(rowindex);
            reppreviousemployment.DataSource = dt1;
            reppreviousemployment.DataBind();
            ReIndexingofSrNo1();
            PreviousEmploymentDropDow();
        }
        protected void lnkDelete1_OnClick(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            // ReSharper disable once IdentifierTypo
            var currntrow = (RepeaterItem)lnk.NamingContainer;
            var i = currntrow.ItemIndex;
            DeletePreviousInstitutionGridRow1(i);
        }
        protected void lnkAddMore1_OnClick(object sender, EventArgs e)
        {
            AddPreviousEmploymentGridRow();
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }



        protected void DrpPresCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBox1.Checked)
                {
                    if (DrpPerState.Text == DrpPreSta.Text.Trim())
                    { DrpPerCity.Text = DrpPresCity.Text.Trim(); }
                }
            }
            catch (Exception)
            { }

        }

        /* CReated By Hariom Dated : 12-03-21 */
        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetBankBranch(ddlBankBranch, Int32.Parse(ddlBank.SelectedValue.ToString()));
            }
            catch (Exception)
            { }

        }
    }
}

