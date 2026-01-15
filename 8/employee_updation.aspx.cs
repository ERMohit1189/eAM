using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _8
{
    // ReSharper disable once IdentifierTypo
    public partial class AdminEmployeeUpdation : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        private DataTable _dt;

        // ReSharper disable once IdentifierTypo
        public AdminEmployeeUpdation()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
            _con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                _sql = "select MachineNo from PunchMachineConfiguration where BranchCode=" + Session["BranchCode"] + " ";
                _oo.FillDropDown_withValue(_sql, ddlMachineNo, "MachineNo", "MachineNo");
                ddlMachineNo.Items.Insert(0, new ListItem("<--Select-->", ""));
                try
                {
                    HttpCookie cityInfo = new HttpCookie("cityInfo");
                    cityInfo["cities"] = "";
                    cityInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(cityInfo);

                    AddPreviousInstitutionGridRow();
                    AddPreviousEmploymentGridRow();
                }
                catch (Exception)
                {
                    // ignored
                }
                LoadK12();
                //TabControl();
                EmpGeneralDetailDropDown();
                EmployeeDetailDropDrop();
                PreviousEmploymentDropDow();
                OfficialDetailDropDown();
                //GetEmpShift();
                GetBankMaster(ddlBank);
            }
        }
        public void LoadK12()
        {
            _sql = "Select * from setting";
            lblAadhaar.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
        }
        protected void Get_DocumentName(Control ctrl)
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            var cmd = new SqlCommand
            {
                CommandText = "USP_GetEmployeeDocumentName",
                CommandType = CommandType.StoredProcedure,
                Connection = _con
            };
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Id", "0");
            cmd.Parameters.AddWithValue("@Empid", empId);
            using (var da = new SqlDataAdapter())
            {
                var dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
            }

        }
        public void NewDocumentsDetails(Control ctrl)
        {
            var msg = "";
            try
            {
                var empId = Request.Form[hfEmployeeId.UniqueID];
                if (empId == string.Empty)
                {
                    empId = txtHeaderEmpId.Text.Trim();
                }
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
                        fileName = empId.ToString() + '_' + lblDocument.Text.Trim() + fileExtention;
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
                        cmd.Parameters.AddWithValue("@Empid", empId);
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
            _sql = "select id, ltrim(rtrim(StateName)) as StateName from StateMaster";
            _oo.FillDropDown_withValue(_sql, DrpPreSta, "StateName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("State", DrpPreSta);
                }
                catch
                {
                    // ignored
                }
            }

            _sql = "select id, ltrim(rtrim(CityName)) as CityName from CityMaster where stateid=" + DrpPreSta.SelectedValue + "";
            _oo.FillDropDown_withValue(_sql, DrpPresCity, "CityName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("City", DrpPresCity);
                }
                catch
                {
                    // ignored
                }
            }


            _sql = "select id, ltrim(rtrim(StateName)) as StateName from StateMaster";
            _oo.FillDropDown_withValue(_sql, DrpPerState, "StateName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("State", DrpPerState);
                }
                catch
                {
                    // ignored
                }
            }
            _sql = "select id, ltrim(rtrim(CityName)) as CityName from CityMaster where  stateid=" + DrpPerState.SelectedValue + "";
            _oo.FillDropDown_withValue(_sql, DrpPerCity, "CityName", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("City", DrpPerCity);
                }
                catch
                {
                    // ignored
                }
            }
        }
        public void EmployeeDetailDropDrop()
        {
            _sql = "Select ReligionName from ReligionMaster";
            _oo.FillDropDown(_sql, DrpReligion, "ReligionName");
            _sql = "Select ltrim(rtrim(CasteName))CasteName from CasteMaster";
            _oo.FillDropDown(_sql, DrpCategory, "CasteName");
            _sql = "Select BloodGroupName from BloodGroupMaster";
            _oo.FillDropDown(_sql, drpblood, "BloodGroupName");
        }

        public void OfficialDetailDropDown()
        {
            _oo.AddDateMonthYearDropDown(drpyearhai, drpmonthhai, drpdinhai);
            _sql = "Select EmpDepName from EmpDepMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, txtDepartmentName, "EmpDepName");
            txtDepartmentName.Items.Insert(0, new ListItem("<--Select-->", ""));
            _sql = "Select EmpDesName from EmpDesMaster  where  BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, drpdes, "EmpDesName");
            drpdes.Items.Insert(0, new ListItem("<--Select-->", ""));
            _sql = "Select DesName from DesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, drpEmpdes, "DesName");
            drpEmpdes.Items.Insert(0, new ListItem("<--Select-->", ""));
            _sql = "select LTRIM(EmployeeCategoryName) EmployeeCategoryName from EmployeeCategoryMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, drpempcategory, "EmployeeCategoryName");
            drpempcategory.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        private void GetBankMaster(DropDownList ddl)
        {
            var obj = new BAL.clsBankMaster
            {

                M02ID = -1,
                IsActive = 1,
                BankName = ""
            };

            _dt = new DAL().GetBankMaster(obj);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                BLL.FillDropDown(ddl, _dt, "BankName", "M02ID", 'S');
            }
            else
            {
                ddl.Items.Clear();
            }
        }
        private void GetBankBranch(DropDownList ddl, Int32 m02Id)
        {
            var obj = new BAL.clsBankBranchMaster
            {

                M02ID = m02Id,
                M03ID = -1,
                IsActive = -1,
                BankBranchName = ""
            };

            _dt = new DAL().GetBankBranchMaster(obj);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                BLL.FillDropDown(ddl, _dt, "BankBranchName", "M03ID", 'S');
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

        public void EmpOfficialDetailDisplay()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }
            _sql = "select SrNo,Ecode,EmpId,left(convert(nvarchar,RegistrationDate,106),2) as DD,Right(left(convert(nvarchar,RegistrationDate,106),6),3) as MM , RIGHT(convert(nvarchar,RegistrationDate,106),4) as YY,FileNo,Reference,Remark,DepartmentName,Designation,PANno , BankName , BranchName , IFSC , Address , PinCode , AccountNo , AccountType,EmpCategory,SessionName,BranchCode,EmploymentType,A02ID,MachineId, TrainingDetails, TeachingSubjects,DesNameNew, PFNo, EsicNo, machinNo,UAN  from EmpployeeOfficialDetails";
            _sql = _sql + "    where EmpId='" + empId.Trim() + "'";
            _sql = _sql + "   and BranchCode=" + Session["BranchCode"] + "";

            try
            {
                txtDepartmentName.Text = _oo.ReturnTag(_sql, "DepartmentName");
            }
            catch (Exception)
            {
                // ignored
            }

            txtTrainingDetails.Text = _oo.ReturnTag(_sql, "TrainingDetails");
            txtTeachingSubjects.Text = _oo.ReturnTag(_sql, "TeachingSubjects");
            txtrefere.Text = _oo.ReturnTag(_sql, "Reference");
            txtEmpId.Text = _oo.ReturnTag(_sql, "EmpId");
            //Application["EmpId"] = txtEmpId.Text.Trim();
            txtFileno.Text = _oo.ReturnTag(_sql, "FileNo");
            txtremak.Text = _oo.ReturnTag(_sql, "Remark");
            txtMachineid.Text = _oo.ReturnTag(_sql, "MachineId");
            ddlMachineNo.SelectedValue = _oo.ReturnTag(_sql, "machinNo");
            txtPFNo.Text = _oo.ReturnTag(_sql, "PFNo");
            txtUAN.Text = _oo.ReturnTag(_sql, "UAN");
            txtEsicNo.Text = _oo.ReturnTag(_sql, "EsicNo");

            txtpanno.Text = _oo.ReturnTag(_sql, "PANno");
            // ReSharper disable once IdentifierTypo
            var bankname = _oo.ReturnTag(_sql, "BankName");
            if (ddlBank.Items.Count>0)
            {
                switch (bankname)
                {
                    case "":
                        ddlBank.SelectedValue = "-1";
                        break;
                    default:
                        ddlBank.SelectedValue = bankname;
                        GetBankBranch(ddlBankBranch, Convert.ToInt32(ddlBank.SelectedValue));
                        break;
                }
            }
            
            var branchname = _oo.ReturnTag(_sql, "BranchName");
            ddlBankBranch.SelectedValue = branchname == "" ? "-1" : branchname;
            txtbranchifsc.Text = _oo.ReturnTag(_sql, "IFSC");
            bankbranchadd.Text = _oo.ReturnTag(_sql, "Address");
            branchpincode.Text = _oo.ReturnTag(_sql, "PinCode");
            TextBox14.Text = _oo.ReturnTag(_sql, "AccountNo");
            ddlAccType.SelectedValue = _oo.ReturnTag(_sql, "AccountType");

            try
            {
                drpyearhai.Text = _oo.ReturnTag(_sql, "YY");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpmonthhai.Text = _oo.ReturnTag(_sql, "MM");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpdinhai.Text = _oo.ReturnTag(_sql, "DD");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpdes.Text = _oo.ReturnTag(_sql, "Designation");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                drpEmpdes.Text = _oo.ReturnTag(_sql, "DesNameNew");
            }
            catch (Exception)
            {
                // ignored
            }
            try
            {
                //drpempcategory.Items.FindByText(oo.ReturnTag(sql, "EmpCategory").Trim()).Selected=true;
                drpempcategory.SelectedValue = (_oo.ReturnTag(_sql, "EmpCategory").Trim());
            }
            catch (Exception)
            {
                // ignored
            }
            //try
            //{
            //    ddlEmpShift.Text = oo.ReturnTag(sql, "A02ID");
            //}
            //catch (Exception) { }
            //try
            //{
            //    rblEmploymentType.Items.FindByValue(oo.ReturnTag(sql, "EmploymentType")).Selected = true;
            //}
            //catch (Exception) { }
        }
        public void EmpGeneralDetailsDisplay()
        {
            try
            {
                var empId = Request.Form[hfEmployeeId.UniqueID];
                if (empId == string.Empty)
                {
                    empId = txtHeaderEmpId.Text.Trim();
                }

                _sql = "select AadharNo,EG.SrNo,PhotoPath,EG.Ecode,EG.EmpId,EG.Etitle,EG.EFirstName,EG.EMiddleName,EG.ELastName,EG.EGender,EG.EFatherName,EG.EMotherName,ISNULL(DisplayName,EG.EFirstName) as DisplayName,format(EG.MarriageAnniversaryDate,'yyyy MMM dd') MarriageAnniversaryDate,SpouseName,format(EG.EDateOfBirth,'yyyy MMM dd') DateOfBirth,case when MaritalStatus='Single' then 1 when MaritalStatus='Married' then 2 when MaritalStatus='Divorced' then 3 else 4 end MaritalStatus,";
                _sql = _sql + "  left(convert(nvarchar,EG.EDateOfBirth,106),2) as DD,Right(left(convert(nvarchar,EG.EDateOfBirth,106),6),3) as MM , RIGHT(convert(nvarchar,EG.EDateOfBirth,106),4) as YY,";
                _sql = _sql + "   EG.EEmail,EG.EMobileNo,EG.EPreAddress,spm.StateName,clm.CityName,EG.EPreZip,EPerAdd,slm.StateName,cpm.CityName,EG.EPerZip,EG.SessionName,EG.BranchCode,EG.EmergencyContactNo,isEmailShow,isMobileNoShow from EmpGeneralDetail EG";
                _sql = _sql + "  left join CityMaster CLM on EG.EPreCityId=CLM.Id";
                _sql = _sql + "  left join CityMaster CPM on EG.EPerCityId=CPM.Id";
                _sql = _sql + "  left join StateMaster SLM on EG.EPerStateId=SLM.Id";
                _sql = _sql + "  left join StateMaster SPM on EG.EPreStateId =SPM.Id";
                _sql = _sql + "    where EG.EmpId='" + empId + "' and EG.BranchCode=" + Session["BranchCode"] + "";

                DrpTitle.Text = _oo.ReturnTag(_sql, "Etitle");
                txtFirstName.Text = _oo.ReturnTag(_sql, "EFirstName");
                txtlastName.Text = _oo.ReturnTag(_sql, "ELastName");
                txtmidName.Text = _oo.ReturnTag(_sql, "EMiddleName");
                txtAadhar.Text = _oo.ReturnTag(_sql, "AadharNo");
                try
                {
                    string date = DateTime.Parse(_oo.ReturnTag(_sql, "DateOfBirth")).ToString("dd-MMM-yyyy");
                    txtStudentDOB.Text = date;
                }
                catch (Exception)
                {
                    txtStudentDOB.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }

                try
                {
                    drpGender.SelectedValue = _oo.ReturnTag(_sql, "EGender");
                }
                catch (Exception)
                {
                    // ignored
                }

                txtfathername.Text = _oo.ReturnTag(_sql, "EFatherName");
                txtmothname.Text = _oo.ReturnTag(_sql, "EMotherName");
                txtemail.Text = _oo.ReturnTag(_sql, "EEmail");
                // ReSharper disable once RedundantTernaryExpression
                chkStEmail.Checked = _oo.ReturnTag(_sql, "isEmailShow") == "True" ? true : false;

                txtmobileno.Text = _oo.ReturnTag(_sql, "EMobileNo");
                // ReSharper disable once RedundantTernaryExpression
                chkStMobile.Checked = _oo.ReturnTag(_sql, "isMobileNoShow") == "True" ? true : false;

                txtPreseAdd.Text = _oo.ReturnTag(_sql, "EPreAddress");
                try
                {

                    DrpPreSta.Text = _oo.ReturnTag(_sql, "StateName");
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    DrpPresCity.Text = _oo.ReturnTag(_sql, "CityName");
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    DrpPresZip.Text = _oo.ReturnTag(_sql, "EPreZip");
                }
                catch (Exception)
                {
                    // ignored
                }
                txtPermAdd.Text = _oo.ReturnTag(_sql, "EPerAdd");
                try
                {
                    DrpPerCity.Text = _oo.ReturnTag(_sql, "CityName");
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    DrpPerState.Text = _oo.ReturnTag(_sql, "StateName");
                }
                catch (Exception)
                {
                    // ignored
                }
                txtPerZip.Text = _oo.ReturnTag(_sql, "EPerZip");
                txtemergencycontactno.Text = _oo.ReturnTag(_sql, "EmergencyContactNo");
                DrpMaitalStatus.SelectedValue = _oo.ReturnTag(_sql, "MaritalStatus");
                if (DrpMaitalStatus.SelectedValue == "2")
                {
                    try
                    {
                        string date = DateTime.Parse(_oo.ReturnTag(_sql, "MarriageAnniversaryDate")).ToString("dd-MMM-yyyy");
                        txtmarranniver.Text = date;
                    }
                    catch (Exception)
                    {
                        txtmarranniver.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    txtspousename.Text = _oo.ReturnTag(_sql, "SpouseName");
                    divhideSpouse.Visible = true;
                    divhidemarr.Attributes.Add("class", "col-sm-6 half-width-50 mgbt-xs-15");
                }
                else
                {
                    divhideSpouse.Visible = false;
                    divhidemarr.Attributes.Add("class", "col-sm-6 half-width-50 mgbt-xs-15 hide");
                }

                txtDisplay.Text = _oo.ReturnTag(_sql, "DisplayName");

                var imageurl = _oo.ReturnTag(_sql, "PhotoPath");
                Avatar.ImageUrl = imageurl != string.Empty ? _oo.ReturnTag(_sql, "PhotoPath") : "../img/user-pic/student-pic.png";
            }
            catch (Exception ex)
            {
                throw new Exception("some reason to rethrow", ex);
            }
        }
        public void EmpEmployeeDetailsDisplay()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            _sql = "select ED.SrNo,ED.Ecode,ED.EmpId,RM.ReligionName,cm.CasteName as Category ,ED.EHeight,ED.Ediseases,ED.EIdentificationMark,";
            _sql = _sql + "  ED.EHostelRequired,ED.ENationality,ED.ECaste,BM.BloodGroupName,ED.EWeight,ED.EHobbies,ED.Languages,ED.ETransportRequired,ED.SessionName,ED.BranchCode from EmpEmployeeDetails ED";
            _sql = _sql + "   left join ReligionMaster RM on ED.EReligionId=RM.ReligionId";
            _sql = _sql + "   left join BloodGroupMaster BM on ED.EBloodGroupId=BM.BloodGroupId";
            _sql = _sql + "   left join CasteMaster CM on ED.ECategoryId=CM.CasteId";
            _sql = _sql + "   where ED.EmpId='" + empId + "'";
            _sql = _sql + "   and ED.BranchCode=" + Session["BranchCode"] + "";

            try
            {
                DrpReligion.Text = _oo.ReturnTag(_sql, "ReligionName");
            }
            catch
            {
                // ignored
            }
            try
            {
                DrpCategory.SelectedValue = _oo.ReturnTag(_sql, "Category").Trim();
            }
            catch
            {
                // ignored
            }
            txtheight.Text = _oo.ReturnTag(_sql, "EHeight");
            txtdiseas.Text = _oo.ReturnTag(_sql, "Ediseases");
            txtidentmark.Text = _oo.ReturnTag(_sql, "EIdentificationMark");
            try
            {
                RadioButtonList3.Text = _oo.ReturnTag(_sql, "EHostelRequired");
            }
            catch (Exception)
            {
                // ignored
            }

            txtnat.Text = _oo.ReturnTag(_sql, "ENationality");
            txtcaste.Text = _oo.ReturnTag(_sql, "ECaste");
            txtweight.Text = _oo.ReturnTag(_sql, "EWeight");
            try
            {
                drpblood.Text = _oo.ReturnTag(_sql, "BloodGroupName");
            }
            catch (Exception)
            {
                // ignored
            }
            txthobbies.Text = _oo.ReturnTag(_sql, "EHobbies");
            txtdlanguages.Text = _oo.ReturnTag(_sql, "Languages");
            try
            {
                RadioButtonList4.Text = _oo.ReturnTag(_sql, "ETransportRequired");
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void PreviousEmploymentDisplay()
        {
            string empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            _sql = "select ROW_NUMBER() OVER (ORDER BY EP.SrNo ASC) AS srno1,EP.SrNo, EP.Ecode, EP.EmpId,cm.CountryName,slm.StateName,clm.CityName,EP.ECountryId,EP.ESateId,EP.ECityId ,EP.ETypeOfOrganization as TypeofOrganization, EP.EDesignation as Designation, EP.EProfile as Profile,";
            _sql = _sql + "   left(convert(nvarchar, EP.EFromDate,106),2) as DD,Right(left(convert(nvarchar, EP.EFromDate,106),6),3) as MM , RIGHT(convert(nvarchar, EP.EFromDate,106),4) as YY,";
            _sql = _sql + "  left(convert(nvarchar, EP.EToDate,106),2) as DD1,Right(left(convert(nvarchar, EP.EToDate,106),6),3) as MM1 , RIGHT(convert(nvarchar, EP.EToDate,106),4) as YY1,";
            _sql = _sql + "   EP.ENameOfOrganization as NameofOrganization, EP.EDealsIn as Details, EP.EAddress as Address, EP.EReasonOfResign as ReasonofResign,EP.SessionName, EP.BranchCode from EmpPreviousEmployment EP";
            _sql = _sql + "  left join CityMaster CLM on EP.ECityId=CLM.Id";
            _sql = _sql + "  left join StateMaster SLM on EP.ESateId=SLM.Id";
            _sql = _sql + "  left join CountryMaster CM on EP.ECountryId=CM.Id";
            _sql = _sql + "    where EmpId='" + empId.Trim() + "'";
            _sql = _sql + "  and EP.BranchCode=" + Session["BranchCode"] + "";

            try
            {
                ViewState["Query"] = _sql;
                reppreviousemployment.DataSource = _oo.GridFill(_sql);
                reppreviousemployment.DataBind();
                PreviousEmploymentDropDow11();
                ReIndexingofSrNo1();
            }
            catch (Exception)
            {
                // ignored
            }
            DataTable ds;
            ds = _oo.Fetchdata(ViewState["Query"].ToString());
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                try
                {
                    drpCountry.SelectedValue = ds.Rows[i]["ECountryId"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                try
                {
                    drpState.SelectedValue = ds.Rows[i]["ESateId"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                var drpCity = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCity");
                try
                {
                    drpCity.SelectedValue = ds.Rows[i]["ECityId"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }

                var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");
                try
                {
                    drpYear2.SelectedValue = ds.Rows[i]["YY"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    drpMonth2.SelectedValue = ds.Rows[i]["MM"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    drpDate2.SelectedValue = ds.Rows[i]["DD"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }

                var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear3");
                var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth3");
                var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate3");
                try
                {
                    drpYear3.SelectedValue = ds.Rows[i]["YY1"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    drpMonth3.SelectedValue = ds.Rows[i]["MM1"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    drpDate3.SelectedValue = ds.Rows[i]["DD1"].ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
                //  Getstatecity();
            }
        }
        public void PreviousEmploymentDropDow11()
        {
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                var drpCity = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCity");
                _sql = "select ltrim(rtrim(CountryName)) as CountryName,id from CountryMaster";
                _oo.FillDropDown_withValue(_sql, drpCountry, "CountryName", "id");
                _sql = "select ltrim(rtrim(StateName)) as StateName,id from StateMaster ";
                _oo.FillDropDown_withValue(_sql, drpState, "StateName", "id");
                _sql = "select ltrim(rtrim(CityName)) as CityName,id from CityMaster ";
                _oo.FillDropDown_withValue(_sql, drpCity, "CityName", "id");
                //_sql = "select EmpDesName from EmpDesMaster ";
                //_oo.FillDropDown(_sql, DrpDesignation, "EmpDesName");

                var drpYear2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpYear2");
                var drpMonth2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpMonth2");
                var drpDate2 = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpDate2");

                _oo.AddDateMonthYearDropDown(drpYear2, drpMonth2, drpDate2);


                var drpYear3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpYear3");
                var drpMonth3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpMonth3");
                var drpDate3 = (DropDownList)reppreviousemployment.Items[i].FindControl("drpDate3");
                _oo.AddDateMonthYearDropDown(drpYear3, drpMonth3, drpDate3);
            }
        }
        public void EmpDocumentsDisplay()
        {
            _sql = _sql + "Select ROW_NUMBER() over(Order by cdn.Id Desc) as srno, DocumentType, cdn.Id, ISNULL(sd.DocPath, '') as DocPath, IsNULL(sd.DocName, '') as DocName, ";

            _sql = _sql + " Case when sd.DocName is null then 0 else 1 end as visible, IsNULL(Softcopy, 0) as Softcopy, IsNULL(Hardcopy, 0) as Hardcopy, IsNULL(Verified, 0) as Verified, sd.Remark";

            _sql = _sql + " from dt_CreateStaffDocumentName cdn  left join EmployeeDocs sd on sd.DocId = cdn.Id and '" + txtHeaderEmpId.Text.Trim() + "' = Case '" + txtHeaderEmpId.Text.Trim() + "' when '' then '" + txtHeaderEmpId.Text.Trim() + "' else sd.Empid End";

            _sql = _sql + " where cdn.BranchCode = " + Session["BranchCode"].ToString() + " and sd.BranchCode=" + Session["BranchCode"].ToString() + "  and cdn.Sessionname=" + Session["SessionName"] + "";


            try
            {
                //txtPhoremark.Text = oo.ReturnTag(sql, "EPhotoRemark");
                //txtrelevingremark.Text = oo.ReturnTag(sql, "ERelievingLetterRemark");
                //txtdomicileremark.Text = oo.ReturnTag(sql, "EDomicileCertificationRemark");
                //txtpancard.Text = oo.ReturnTag(sql, "EPanCardRemark");
                //txtlastremark.Text = oo.ReturnTag(sql, "ELastSalarySlip");
                //txtcastremark.Text = oo.ReturnTag(sql, "ECasteCertification");


                //HyperLink1.NavigateUrl = oo.ReturnTag(sql, "EPhotoPath");
                //HyperLink2.NavigateUrl = oo.ReturnTag(sql, "ERelievingLetterpPath");
                //HyperLink3.NavigateUrl = oo.ReturnTag(sql, "EDomicileCertificatePath");
                //HyperLink4.NavigateUrl = oo.ReturnTag(sql, "EPanCardPath");
                //HyperLink5.NavigateUrl = oo.ReturnTag(sql, "ELastSalarySlipPath");
                //HyperLink6.NavigateUrl = oo.ReturnTag(sql, "ECasteCertificatePath");

            }
            catch (Exception) { }
        }

        protected void DrpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.YearDropDown(DrpYear, DrpMonth, DrpDate);
        }
        protected void DrpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oo.MonthDropDown(DrpYear, DrpMonth, DrpDate);
        }
        protected void DrpDate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DrpPreSta_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantAssignment
            //var cCode = "";
            //_sql = "select Id from StateMaster where StateName='" + DrpPreSta.SelectedItem + "'";

            //cCode = _oo.ReturnTag(_sql, "id");
            //_sql = "Select CityName from CityMaster where StateId=" + DrpPreSta.SelectedValue;

            //_oo.FillDropDown(_sql, DrpPresCity, "CityName");
            _sql = "select id, ltrim(rtrim(CityName)) as CityName from CityMaster where stateid=" + DrpPreSta.SelectedValue + "";
            DataTable dt = new DataTable();
            dt = _oo.Fetchdata(_sql);
            Session["cities"] = dt;
            DrpPresCity.DataValueField = dt.Columns["id"].ToString();
            DrpPresCity.DataTextField = dt.Columns["CityName"].ToString(); 
            DrpPresCity.DataSource = dt;
            DrpPresCity.DataBind();

            //_oo.FillDropDown_withValue(_sql, DrpPresCity, "CityName", "id");
            //using (var objBll = new BLL())
            //{
            //    try
            //    {
            //        objBll.loadDefaultvalue("City", DrpPresCity);
            //    }
            //    catch
            //    {
            //        // ignored
            //    }
            //}
        }
        protected void DrpPresCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantAssignment

            //Session["cities"] += dr[ColumnName].ToString() + "," + dr[ColumnValue].ToString() + ",";
            var cCode = "";
            _sql = "select Id from StateMaster where StateName='" + DrpPerState.SelectedItem + "'";

            cCode = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName from CityMaster where StateId=" + cCode;

            _oo.FillDropDown(_sql, DrpPerCity, "CityName");
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            // ReSharper disable once RedundantBoolCompare
            if (CheckBox1.Checked == true)
            {
                txtPermAdd.Text = txtPreseAdd.Text.Trim();

                DrpPerState.Text = DrpPreSta.Text.Trim();
                if (Session["cities"] != null)
                {
                    DataTable dt = new DataTable();dt = (DataTable)Session["cities"];
                    DrpPerCity.DataValueField = dt.Columns["id"].ToString();
                    DrpPerCity.DataTextField = dt.Columns["CityName"].ToString();
                    DrpPerCity.DataSource = dt;
                    DrpPerCity.DataBind();
                }
                //DrpPerCity.Text = DrpPresCity.Text.Trim();
                

                //for (int i = 0; i < length; i++)
                //{

                //}
                txtPerZip.Text = DrpPresZip.Text.Trim();
                DrpPerState.Enabled = false;
                DrpPerCity.Enabled = false;
            }
            else
            {
                txtPermAdd.Text = "";
                //_sql = "select ltrim(rtrim(StateName)) from StateMaster ";
                //_oo.FillDropDown(_sql, DrpPerState, "StateName");
                //_sql = "select ltrim(rtrim(CityName)) from CityMaster ";
                //_oo.FillDropDown(_sql, DrpPerCity, "CityName");
                txtPerZip.Text = "";
                DrpPerState.Enabled = true;
                DrpPerCity.Enabled = true;
            }
        }
        protected void DrpPerCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DrpReligion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DrpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void drpblood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DrpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var drpCountry = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpCountry");
                var drpState = (DropDownList)reppreviousemployment.Items[i].FindControl("DrpState");
                // ReSharper disable once RedundantAssignment
                var cCode = "";
                _sql = "select Id from CountryMaster where CountryName='" + drpCountry.SelectedItem + "'";
                cCode = _oo.ReturnTag(_sql, "id");
                _sql = "Select StateName from StateMaster where Countryid=" + cCode;
                _oo.FillDropDown(_sql, drpState, "StateName");
            }
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
        protected void DrpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Getstatecity();
            var drp = sender as DropDownList;

            // ReSharper disable once PossibleNullReferenceException
            var drpCity = (DropDownList)drp.NamingContainer.FindControl("DrpCity");
            // ReSharper disable once RedundantAssignment
            var cc = "";
            _sql = "select Id from StateMaster where StateName='" + drp.SelectedItem + "'";
            cc = _oo.ReturnTag(_sql, "id");
            _sql = "Select CityName,id from CityMaster where stateid=" + cc;
            _oo.FillDropDown_withValue(_sql, drpCity, "CityName", "id");
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
        protected void DrpDate2_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        protected void DrpDate3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtpancard_TextChanged(object sender, EventArgs e)
        {

        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            
            string empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }
            int Machineid = 0; 
            int.TryParse(txtMachineid.Text.Trim(), out Machineid);
            
            if (txtMachineid.Text.Trim() != "" & ddlMachineNo.SelectedValue.Trim() == "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please select Machin No.!", "W");
                return;
            }

            if (txtMachineid.Text.Trim() == "" & ddlMachineNo.SelectedValue.Trim() != "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please enter Machin Id.!", "W");
                return;
            }

            
            if (Machineid == 0 & txtMachineid.Text.Trim() != "")
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry, Please enter valid Machin Id.!", "W");
                return;
            }

            _sql = "select Ecode,EmpId from EmpployeeOfficialDetails where EmpId='" + empId.Trim() + "'";
            _sql = _sql + " and BranchCode=" + Session["BranchCode"] + "";

            if (_oo.Duplicate(_sql) == false)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Employee Id", "W");
                //oo.MessageBox("",this.Page);
            }

            else
            {
                Application["EmpId"] = _oo.ReturnTag(_sql, "EmpId");
                Session["ECode"] = _oo.ReturnTag(_sql, "Ecode");
                try
                {
                    EmpOfficialDetailDisplay();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    EmpGeneralDetailsDisplay();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    EmpEmployeeDetailsDisplay();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    PreviousEmploymentDisplay();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    EmpDocumentsDisplay();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    Get_DocumentName(Page);
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    PreviousSchoolDetailsDisplay();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
            }
        }
        protected void DrpMaitalStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DrpMaitalStatus.SelectedValue == "2")
            {
                divhidemarr.Visible = true;
                divhideSpouse.Visible = true;
                margindiv.Style["margin-bottom"] = "151px";
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
            bool flag = true;
            string empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            var cmd = new SqlCommand();
            if (txtFirstName.Text == "" || txtPreseAdd.Text == "" || DrpPreSta.SelectedItem.ToString() == "<--Select-->" || DrpPresCity.SelectedItem.ToString() == "<--Select-->" || txtPermAdd.Text == "" || DrpPerState.SelectedItem.ToString() == "<--Select-->" || DrpPerCity.SelectedItem.ToString() == "<--Select-->")
            {
                //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Fill All Required Field in General Details", "A");
                _oo.MessageBoxforUpdatePanel("Please Fill All Required Field in General Details", Page);
                //TabContainer1.ActiveTab = TabContainer1.Tabs[0];
                return false;
            }
            else
            {
                cmd.CommandText = "EmpGeneralDetailUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;

                // ReSharper disable once RedundantAssignment
                var ss = "";
                _sql = "select Ecode from EmpployeeOfficialDetails where EmpId='" + empId + "' and BranchCode=" + Session["BranchCode"] + "";
                ss = _oo.ReturnTag(_sql, "Ecode");
                cmd.Parameters.AddWithValue("@Ecode", ss);
                cmd.Parameters.AddWithValue("@EOldId", empId);
                cmd.Parameters.AddWithValue("@EmpId", txtEmpId.Text.Trim());
                cmd.Parameters.AddWithValue("@Etitle", DrpTitle.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EFirstName", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@EMiddleName", txtmidName.Text.Trim());
                cmd.Parameters.AddWithValue("@ELastName", txtlastName.Text.Trim());
                // ReSharper disable once RedundantAssignment
                //var date = "";
                //date = DrpYear.SelectedItem + "/" + DrpMonth.SelectedItem + "/" + DrpDate.SelectedItem;
                cmd.Parameters.AddWithValue("@EDateOfBirth", txtStudentDOB.Text.Trim());
                cmd.Parameters.AddWithValue("@EGender", drpGender.SelectedValue);
                cmd.Parameters.AddWithValue("@EFatherName", txtfathername.Text.Trim());
                cmd.Parameters.AddWithValue("@EMotherName", txtmothname.Text.Trim());
                cmd.Parameters.AddWithValue("@EEmail", txtemail.Text.Trim());
                // ReSharper disable once RedundantTernaryExpression
                cmd.Parameters.AddWithValue("@isEmailShow", value: chkStEmail.Checked ? true : false);

                // ReSharper disable once RedundantToStringCall
                cmd.Parameters.AddWithValue("@EMobileNo", txtmobileno.Text.ToString());
                // ReSharper disable once RedundantTernaryExpression
                cmd.Parameters.AddWithValue("@isMobileNoShow", value: chkStMobile.Checked ? true : false);

                cmd.Parameters.AddWithValue("@EPreAddress", txtPreseAdd.Text.Trim());
                // ReSharper disable once RedundantAssignment
                var dd1 = "";
                _sql = "Select Id from StateMaster where StateName='" + DrpPreSta.SelectedItem + "'";
                dd1 = _oo.ReturnTag(_sql, "Id");
                cmd.Parameters.AddWithValue("@EPreStateId", dd1);
                // ReSharper disable once RedundantAssignment
                var dd = "";
                _sql = "Select Id from CityMaster where CityName='" + DrpPresCity.SelectedItem + "'";
                dd = _oo.ReturnTag(_sql, "Id");
                cmd.Parameters.AddWithValue("@EPreCityId", dd);
                cmd.Parameters.AddWithValue("@EPreZip", txtPerZip.Text.Trim());
                cmd.Parameters.AddWithValue("@EPerAdd", txtPermAdd.Text.Trim());
                // ReSharper disable once RedundantAssignment
                var d = "";
                _sql = "Select Id from StateMaster where StateName='" + DrpPerState.SelectedItem + "'";
                d = _oo.ReturnTag(_sql, "Id");
                cmd.Parameters.AddWithValue("@EPerStateId", d);
                // ReSharper disable once RedundantAssignment
                var sd = "";
                _sql = "Select Id from CityMaster where CityName='" + DrpPerCity.SelectedItem + "'";
                sd = _oo.ReturnTag(_sql, "Id");
                cmd.Parameters.AddWithValue("@EPerCityId", sd);
                cmd.Parameters.AddWithValue("@EPerZip", txtPerZip.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@EmergencyContactNo", txtemergencycontactno.Text.Trim());
                cmd.Parameters.AddWithValue("@MaritalStatus", DrpMaitalStatus.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@MarriageAnniversaryDate", txtmarranniver.Text.Trim());
                cmd.Parameters.AddWithValue("@SpouseName", txtspousename.Text.Trim());

                cmd.Parameters.AddWithValue("@Aadhar", txtAadhar.Text.Trim());
                //string filePath = "";
                //string fileName = "";
                //if (avatarUpload.HasFile)
                //{
                //    filePath = @"~/Uploads/StaffPhoto/";
                //    string fileExtention = Path.GetExtension(avatarUpload.PostedFile.FileName);
                //    fileName = ss + fileExtention;
                //    avatarUpload.SaveAs(Server.MapPath(filePath + fileName));
                //}
                var filePath = "";
                var fileName = "";


                var base64Std = hdEmpPhoto.Value;
                if (base64Std != string.Empty)
                {
                    filePath = @"../Uploads/StaffPhoto/";
                    fileName = ss + ".jpg";

                    using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64Std);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
                cmd.Parameters.AddWithValue("@PhotoPath", filePath + fileName);
                cmd.Parameters.AddWithValue("@PhotoName", fileName);
                cmd.Parameters.AddWithValue("@DisplayName", txtDisplay.Text.Trim());
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                }
                catch (SqlException) { flag = false; _con.Close(); }
                catch (Exception) { flag = false; _con.Close(); }

                EmpGeneralDetailsDisplay();
                return flag;

            }
        }
        public bool EmployeeDetail()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            var flag = true;
            var cmd = new SqlCommand();
            if (DrpReligion.SelectedItem.ToString() == "<--Select-->" || drpblood.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBoxforUpdatePanel("Please Fill All Required Field in Employee Details", Page);
                //TabContainer1.ActiveTab = TabContainer1.Tabs[1];
                return true;
            }
            if (DrpCategory.SelectedItem.ToString() == "<--Select-->")
            {
                _oo.MessageBoxforUpdatePanel("Please Select Category in Employee Details", Page);
                //TabContainer1.ActiveTab = TabContainer1.Tabs[1];
                return true;
            }
            else
            {
                // RecordNotInsertEmpEmployeeDetails = true;
                cmd.CommandText = "EmpEmployeeDetailsUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                // ReSharper disable once RedundantAssignment
                var ss = "";
                _sql = "select Ecode from EmpployeeOfficialDetails where EmpId='" + empId + "' and BranchCode=" + Session["BranchCode"] + "";
                ss = _oo.ReturnTag(_sql, "Ecode");

                cmd.Parameters.AddWithValue("@Ecode", ss);
                cmd.Parameters.AddWithValue("@EOldId", empId);
                cmd.Parameters.AddWithValue("@EmpId", txtEmpId.Text.Trim());
                // ReSharper disable once RedundantAssignment
                var rel = "";
                _sql = "select ReligionId from ReligionMaster where ReligionName='" + DrpReligion.SelectedItem + "'";
                rel = _oo.ReturnTag(_sql, "ReligionId");
                cmd.Parameters.AddWithValue("@EReligionId", rel);
                // ReSharper disable once RedundantAssignment
                var cat = "";
                _sql = "select CasteId from CasteMaster where CasteName='" + DrpCategory.SelectedItem + "'";
                cat = _oo.ReturnTag(_sql, "CasteId");
                cmd.Parameters.AddWithValue("@ECategoryId", cat);
                cmd.Parameters.AddWithValue("@EHeight", txtheight.Text.Trim());
                cmd.Parameters.AddWithValue("@Ediseases", txtdiseas.Text.Trim());
                cmd.Parameters.AddWithValue("@EIdentificationMark", txtidentmark.Text.Trim());
                cmd.Parameters.AddWithValue("@EHostelRequired", RadioButtonList3.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ENationality", txtnat.Text.Trim());
                cmd.Parameters.AddWithValue("@ECaste", txtcaste.Text.Trim());
                // ReSharper disable once RedundantAssignment
                var blood = "";
                _sql = "select BloodGroupId from BloodGroupMaster where BloodGroupName='" + drpblood.SelectedItem + "'";
                blood = _oo.ReturnTag(_sql, "BloodGroupId");
                cmd.Parameters.AddWithValue("@EBloodGroupId", blood);
                cmd.Parameters.AddWithValue("@EWeight", txtweight.Text.Trim());
                cmd.Parameters.AddWithValue("@EHobbies", txthobbies.Text.Trim());
                cmd.Parameters.AddWithValue("@Languages", txtdlanguages.Text.Trim());
                cmd.Parameters.AddWithValue("@ETransportRequired", RadioButtonList4.Text.Trim());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();


                }
                catch (SqlException) { flag = false; _con.Close(); }
                catch (Exception) { flag = false; _con.Close(); }


                return flag;

            }

        }


        public void PreviousEmployment()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }
            // ReSharper disable once RedundantAssignment

            _sql = "select Ecode,srno from EmpPreviousEmployment where EmpId='" + empId + "' and BranchCode=" + Session["BranchCode"] + "";
            var dt11 = _oo.Fetchdata(_sql);
            var ss1 = "";
            // ReSharper disable once RedundantAssignment
            for (int i = 0; i < reppreviousemployment.Items.Count; i++)
            {
                var sss1 = "";
                try
                {
                    ss1 = (string)dt11.Rows[i]["Ecode"];
                    sss1 = Convert.ToInt32(dt11.Rows[i]["srno"]).ToString();
                }
                catch (Exception)
                {
                    sss1 = "";
                }

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

                using (var cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "USP_EmpPreviousEmploymentProcUpdate";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Connection = _con;
                    cmd1.Parameters.AddWithValue("@Ecode", ss1);
                    cmd1.Parameters.AddWithValue("@EmpId", empId);
                    cmd1.Parameters.AddWithValue("@srno", sss1 != "" ? sss1 : "");
                    cmd1.Parameters.AddWithValue("@ECountryId", drpCountry.SelectedValue);
                    cmd1.Parameters.AddWithValue("@EStateId", drpState.SelectedValue);
                    cmd1.Parameters.AddWithValue("@ECityId", drpCity.SelectedValue);
                    cmd1.Parameters.AddWithValue("@ETypeOfOrganization", drptyporganizat.Text.Trim());
                    cmd1.Parameters.AddWithValue("@EDesignation", drpDesignation.Text.Trim());
                    cmd1.Parameters.AddWithValue("@EProfile", txtProfile.Text.Trim());
                    cmd1.Parameters.AddWithValue("@ENameOfOrganization", txtName0Rga.Text.Trim());
                    cmd1.Parameters.AddWithValue("@EDealsIn", txtdealin.Text.Trim());
                    cmd1.Parameters.AddWithValue("@EFromDate", datee);
                    cmd1.Parameters.AddWithValue("@EToDate", dateee);
                    cmd1.Parameters.AddWithValue("@EAddress", txtaddres.Text.Trim());
                    cmd1.Parameters.AddWithValue("@EReasonOfResign", txtreasonresign.Text.Trim());
                    cmd1.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd1.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    try
                    {
                        _con.Open();
                        cmd1.ExecuteNonQuery();
                        _con.Close();
                    }
                    catch (SqlException)
                    {
                        _con.Close();
                    }
                    catch (Exception)
                    {
                        _con.Close();
                    }
                }
            }
        }
        public bool EmployeeOfficialDetails()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

#pragma warning disable 219
            var co = 0;
#pragma warning restore 219
            var flag = true;

            using (var cmd = new SqlCommand())
            {
                if (txtEmpId.Text == "")
                {
                    //TabContainer1.ActiveTab = TabContainer1.Tabs[5];

                    _oo.MessageBoxforUpdatePanel("Please Fill All Required Field in Official Details", Page);
                    return false;
                }
                else
                {
                    cmd.CommandText = "EmpployeeOfficialDetailsUpdateProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    // ReSharper disable once RedundantAssignment
                    var ss = "";
                    _sql = "select Ecode from EmpployeeOfficialDetails where EmpId='" + empId + "' and BranchCode=" + Session["BranchCode"] + "";
                    ss = _oo.ReturnTag(_sql, "Ecode");

                    Application.Lock();
                    Application["EmpId"] = txtEmpId.Text.Trim();
                    Application.UnLock();
                    cmd.Parameters.AddWithValue("@Ecode", ss);
                    cmd.Parameters.AddWithValue("@EmpId", txtEmpId.Text.Trim());
                    cmd.Parameters.AddWithValue("@EOldId", empId);
                    // ReSharper disable once RedundantAssignment
                    var dates = "";
                    dates = drpyearhai.SelectedItem + "/" + drpmonthhai.SelectedItem + "/" + drpdinhai.SelectedItem;
                    cmd.Parameters.AddWithValue("@RegistrationDate", dates);
                    cmd.Parameters.AddWithValue("@FileNo", txtFileno.Text.Trim());
                    cmd.Parameters.AddWithValue("@Reference", txtrefere.Text.Trim());
                    cmd.Parameters.AddWithValue("@TrainingDetails", txtTrainingDetails.Text.Trim());
                    cmd.Parameters.AddWithValue("@TeachingSubjects", txtTeachingSubjects.Text.Trim());
                    cmd.Parameters.AddWithValue("Remark", txtremak.Text.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
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
                    catch (SqlException ex) { flag = false; _con.Close(); }
                    catch (Exception) { flag = false; _con.Close(); }


                    return flag;

                }
            }

        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            EmployeeOfficialDetails();
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            //EmployeeDocuments();
        }
        //protected void LinkButton6_Click(object sender, EventArgs e)
        //{
        //    PreviousEmployment();
        //}
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            EmployeeDetail();
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            EmpGeneralDetail();
        }
        protected void txtPhoremark_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtrelevingremark_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtdomicileremark_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtlastremark_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txtcastremark_TextChanged(object sender, EventArgs e)
        {

        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void PermissionGrant(int update1, LinkButton lupdate3, LinkButton lupdate4, LinkButton lupdate5, LinkButton lupdate6, LinkButton lupdate7, LinkButton lupdate8)
        {

            if (update1 == 1)
            {
                lupdate3.Enabled = true;
                lupdate4.Enabled = true;
                lupdate5.Enabled = true;
                lupdate6.Enabled = true;
                lupdate7.Enabled = true;
                lupdate8.Enabled = true;
            }
            else
            {
                lupdate3.Enabled = false;
                lupdate4.Enabled = false;
                lupdate5.Enabled = false;
                lupdate6.Enabled = false;
                lupdate7.Enabled = false;
                lupdate8.Enabled = false;
            }


        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    EmpGeneralDetail();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    EmployeeDetail();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    PreviousEmployment();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    NewDocumentsDetails(lnkSubmit);
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    EmployeeOfficialDetails();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                try
                {
                    PreviousSchoolDetails();
                }
                catch (Exception ex)
                {
                    throw new Exception("some reason to rethrow", ex);
                }
                //oo.MessageBox("Updated successfully.", this.Page);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Updated successfully.", "S");
                reppreviousemployment.DataSource = null;
                reppreviousemployment.DataBind();
                rptPreviousEducation.DataSource = null;
                rptPreviousEducation.DataBind();
                _oo.ClearControls(Page);
            }
            catch (Exception ex)
            {
                throw new Exception("some reason to rethrow", ex);
            }
        }
        protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            _sql = "select Ecode,EmpId from EmpployeeOfficialDetails where EmpId='" + empId.Trim() + "'";
            _sql = _sql + " and BranchCode=" + Session["BranchCode"] + "";

            if (_oo.Duplicate(_sql) == false)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid Employee Id", "W");
                //oo.MessageBox("",this.Page);
            }

            else
            {
                Application["EmpId"] = _oo.ReturnTag(_sql, "EmpId");
                Session["ECode"] = _oo.ReturnTag(_sql, "Ecode");
                EmpOfficialDetailDisplay();
                EmpGeneralDetailsDisplay();
                EmpEmployeeDetailsDisplay();
                PreviousEmploymentDisplay();
                EmpDocumentsDisplay();
                Get_DocumentName(Page);
            }
        }
        
        protected void ddlBankBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBankBranch.Items.Clear();

            if (ddlBank.SelectedIndex > 0)
                GetBankBranch(ddlBankBranch, Convert.ToInt32(ddlBank.SelectedValue));
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
        public void PreviousSchoolDetailsDisplay()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            _sql = "Select convert(varchar, ROW_NUMBER() Over (Order by Id)) as srno, convert(varchar, Id) id, StEnRCode, Board,Result,Medium,";
            _sql = _sql + " (Case when (Qualification='' or Qualification is null) then 'N/A' else Qualification End) as Exam,Marks as Obtained,";
            _sql = _sql + " Percentage as Per,SchoolName as Institute,MaxMarks,Yop,Subjects as Subject,RollNo,CertificateNo,MarksSheetNo";
            _sql = _sql + " from EmployeePreviousSchool where Case When Left('" + empId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + empId + "' ";
            _sql = _sql + " and BranchCode=" + Session["BranchCode"] + " and isActive=1";
            try
            {
                var ds = _oo.GridFill(_sql);
                int n = 4 - ds.Tables[0].Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    ds.Tables[0].Rows.Add();
                }


                rptPreviousEducation.DataSource = ds.Tables[0];
                rptPreviousEducation.DataBind();
                ReIndexingofSrNo();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var lblstIDs = (Label)rptPreviousEducation.Items[i].FindControl("lblstIDs");
                    var lblStEnRCodes = (Label)rptPreviousEducation.Items[i].FindControl("lblStEnRCodes");
                    lblstIDs.Text = ds.Tables[0].Rows[i]["id"].ToString();
                    lblStEnRCodes.Text = ds.Tables[0].Rows[i]["StEnRCode"].ToString();
                }
                LoadBoard();
                SetDropdownValue();
                EnableControl();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void PreviousSchoolDetails()
        {
            var empId = Request.Form[hfEmployeeId.UniqueID];
            if (empId == string.Empty)
            {
                empId = txtHeaderEmpId.Text.Trim();
            }
            _sql = "select Ecode from EmpployeeOfficialDetails where EmpId='" + empId + "' and Withdrwal is null and BranchCode=" + Session["BranchCode"] + "";
            var ss = _oo.ReturnTag(_sql, "Ecode");
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
                var lblstID = (Label)rptPreviousEducation.Items[i].FindControl("lblstIDs");
                var lblStEnRCodes = (Label)rptPreviousEducation.Items[i].FindControl("lblStEnRCodes");

                if (txtExam.Text.Trim() != "")
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.CommandText = "USP_EmployeePreviousSchoolUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = _con;
                        cmd.Parameters.AddWithValue("@StEnRCode", lblStEnRCodes.Text == "" ? ss : lblStEnRCodes.Text.Trim());
                        cmd.Parameters.AddWithValue("@SrNo", empId);
                        cmd.Parameters.AddWithValue("@id", lblstID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qualification", txtExam.Text.Trim());
                        cmd.Parameters.AddWithValue("@Board", drpBoard.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@Result", drpResult.SelectedValue.Trim());
                        cmd.Parameters.AddWithValue("@SchoolName",
                            txtInstitute.Text.Trim() == "" ? DBNull.Value : (object)txtInstitute.Text.Trim());
                        cmd.Parameters.AddWithValue("@Yop",
                            txtYop.Text.Trim() == "" ? DBNull.Value : (object)txtYop.Text.Trim());
                        cmd.Parameters.AddWithValue("@Medium", drpMedium.Text.Trim());
                        cmd.Parameters.AddWithValue("@Subjects",
                            txtSubject.Text.Trim() == "" ? DBNull.Value : (object)txtSubject.Text.Trim());
                        cmd.Parameters.AddWithValue("@RollNo",
                            txtRollNo.Text.Trim() == "" ? DBNull.Value : (object)txtRollNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CertificateNo",
                            txtCertificateNo.Text.Trim() == ""
                                ? DBNull.Value
                                : (object)txtCertificateNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@MarksSheetNo",
                            txtMarksSheetNo.Text.Trim() == ""
                                ? DBNull.Value
                                : (object)txtMarksSheetNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaxMarks",
                            txtMm.Text.Trim() == "" ? DBNull.Value : (object)txtMm.Text.Trim());
                        cmd.Parameters.AddWithValue("@Marks",
                            txtObtained.Text.Trim() == "" ? DBNull.Value : (object)txtObtained.Text.Trim());
                        cmd.Parameters.AddWithValue("@Percentage",
                            txtPer.Text.Trim() == "" ? DBNull.Value : (object)txtPer.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoginName",
                            Session["LoginName"].ToString()); //Session["Login"].ToString());
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
                            _con.Close();
                        }
                        catch (Exception)
                        {
                            _con.Close();
                        }
                    }
                }
            }
        }

        protected void SetDropdownValue()
        {
            string empId = Request.Form[hfEmployeeId.UniqueID];
            if (string.IsNullOrEmpty(empId))
            {
                empId = txtHeaderEmpId.Text.Trim();
            }

            _sql = "Select Board,Result,Medium from EmployeePreviousSchool where Case When Left('" + empId + "',3)='eAM' Then StEnRCode Else SrNo End ='" + empId + "'";
            _sql = _sql + " and BranchCode=" + Session["BranchCode"] + " and isActive=1";

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
        protected void LoadBoard()
        {
            for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                _sql = "Select BoardName from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
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
        protected DataRow SetInitialValue(DataTable dt)
        {
            var dr = dt.NewRow();
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
            // ReSharper disable once LocalVariableHidesMember
            var dt = AddColumn();

            if (rptPreviousEducation.Items.Count == 0)
            {
                dt.Rows.Add(SetInitialValue(dt));
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
                        _sql = "Select BoardName from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
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
                if (txtExam.Text == "")
                {
                    txtExam.Enabled = true;
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
            try
            {
                for (int j = 0; j < reppreviousemployment.Items.Count; j++)
                {
                    var lblsrno1 = (Label)rptPreviousEducation.Items[j].FindControl("lblsrno1");
                    if (lblsrno1!=null)
                    {   lblsrno1.Text = (j + 1).ToString(); }
                }
            }
            catch (NullReferenceException ex)
            { }
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
    }
}
