using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class student_registration : Page
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
        public student_registration()
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
                    drpG1State.Items.Clear(); drpG1City.Items.Clear();
                    CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored jk
                    //exception
                }
                try
                {
                    _sql = "select BranchCode, PreString, Separater from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + " and SrNoType='Manual'";
                    if (_oo.Duplicate(_sql))
                    {
                        divSrNo.Visible = true;
                        lblSrString.Text = (_oo.ReturnTag(_sql, "PreString") + (_oo.ReturnTag(_sql, "PreString") != "" ? _oo.ReturnTag(_sql, "Separater") : ""));
                        txtSr.Attributes.Add("class", "form-control-blue validatetxt validatetxtC");
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
                    EnableDisableTypeAddmission();
                    ParentQualification();
                    LoadShift();
                    LoadEducationAct();
                    LoadEntranceExamName();
                    GeneralDetailDropDown();
                    FamilyDetailsDropDown();
                    OfficialDetailDropDown();
                    var getdate = DateTime.Today.ToString("dd-MMM-yyyy");
                    txtAgeOnDate.Text = getdate;
                    TextBox100.Text = getdate;
                    Get_DocumentName();


                }
                catch (Exception ex) { _oo.MessageBox(ex.Message, Page); }

                Panel2.Visible = false;

                _sql = "select id  from StudentOfficialDetails";
                _sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
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
                AddPreviousInstitutionGridRow();
                string sqlss = "select ReceiptNocompulsory from  admissionDatePermission where ReceiptNocompulsory=1 and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.Duplicate(sqlss))
                {
                    TextBox67.Attributes.Add("class", "form-control-blue validatetxt validatetxtA");
                    TextBox67.Focus();
                }
                else
                {
                    TextBox67.Attributes.Add("class", "form-control-blue");
                }
                LoadValidation();
            }

        }
        private void LoadValidation()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    // _sql = "select * from ValidationSetting ";
                    _sql = "IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ValidationSetting') " +
          "BEGIN SELECT * FROM ValidationSetting END";
                    cmd.Connection = conn;
                    cmd.CommandText = _sql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmd.Parameters.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["ID"].ToString() == "2")
                            {
                                if (dt.Rows[i]["IsApply"].ToString() == "True")
                                {
                                    txtProspectusred.Visible = true;
                                    // txtProspectus.CssClass = "validatetxt";
                                    txtProspectus.CssClass += " validatetxt validatetxtC";
                                }
                                else
                                {
                                    txtProspectusred.Visible = false;

                                }
                            }
                            else if (dt.Rows[i]["ID"].ToString() == "1")
                            {
                                if (dt.Rows[i]["IsApply"].ToString() == "True")
                                {
                                    txtAadharNored.Visible = true;
                                    txtfaadhaarcardnored.Visible = false;
                                    txtmaadharcardnored.Visible = false;
                                    // txtAadharNo.CssClass = "validatetxt";
                                    txtAadharNo.CssClass += " validatetxt validatetxtA";
                                    //txtfaadhaarcardno.CssClass = "validatetxt";
                                    //txtmaadharcardno.CssClass = "validatetxt";

                                }
                                else
                                {
                                    txtAadharNored.Visible = false;
                                    txtfaadhaarcardnored.Visible = false;
                                    txtmaadharcardnored.Visible = false;
                                }
                            }
                            else if (dt.Rows[i]["ID"].ToString() == "3")
                            {
                                if (dt.Rows[i]["IsApply"].ToString() == "True")
                                {
                                    txtAparidred.Visible = true;
                                    txtAparid.CssClass = "validatetxt";
                                    txtAparid.CssClass += " validatetxt validatetxtC";
                                }
                                else
                                {
                                    txtAparidred.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        txtAadharNored.Visible = false;
                        txtfaadhaarcardnored.Visible = false;
                        txtmaadharcardnored.Visible = false;
                        txtProspectusred.Visible = false;
                    }
                }
            }

        }
        private void LoadShift()
        {
            _sql = "select * from StudentShiftMaster where  BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlShift, "ShiftName", "ID");
            ddlShift.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        private void ParentQualification()
        {
            _sql = "select * from ParentsQualificationMaster ";
            _oo.FillDropDown_withValue(_sql, txtqufa, "QualificationName", "ID");
            _oo.FillDropDown_withValue(_sql, txtqualimother, "QualificationName", "ID");
            txtqualimother.Items.Insert(0, new ListItem("<--Select-->", "0"));
            txtqufa.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        private void LoadEducationAct()
        {
            _sql = "select * from tblEducationAct where  BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, ddlEducationAct, "actName", "ID");
            ddlEducationAct.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        public void LoadK12()
        {
            _sql = "Select * from setting";
            string value = (_oo.ReturnTag(_sql, "isK12").ToString() == "" ? "0" : _oo.ReturnTag(_sql, "isK12").ToString());
            if (value == "True")
            {
                divStudentEmailContact.Visible = true;
                divScholarship.Visible = false;
                divAdmissiondoneat.Visible = false;
            }
            else
            {
                divStudentEmailContact.Visible = true;
                divScholarship.Visible = true;
                divAdmissiondoneat.Visible = true;
            }
            lblAadhaar.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
            lblAadhaar1.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
            lblAadhaar2.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
            lblAadhaar3.InnerText = _oo.ReturnTag(_sql, "IsAadhaar").ToString();
        }
        public void EnableDisableTypeAddmission()
        {
            SqlDataAdapter ads = new SqlDataAdapter("Select * from StudentPermissionEnabled where BranchId=" + Session["BranchCode"] + "", _con);
            DataTable dt = new DataTable();
            ads.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ControlName"].ToString() == "Type of Admission" && dt.Rows[i]["ControlValue"].ToString() == "0")
                    {
                        DrpNEWOLSAdmission.Enabled = false;
                    }
                    else
                    {
                        DrpNEWOLSAdmission.Enabled = true;
                    }
                }

            }

        }
        public void LoadEntranceExamName()
        {
            DataTable dts = new DataTable();
            dts = _oo.Fetchdata("Select ExamName,Id from EntranceExamMaster where BranchCode =" + Session["BranchCode"] + "");
            if (dts.Rows.Count > 0)
            {
                drpExamCrackedof.DataSource = dts;
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
            _sql = "Select Medium,id from MediumMaster where BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(_sql, drpMedium, "Medium", "id");
            BAL.objBal.FillDropDown_withValue(_sql, ddlMediumprevious, "Medium", "id");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Medium", drpMedium);
                    objBll.loadDefaultvalue("Medium", ddlMediumprevious);
                }
                catch
                {
                    // ignored
                }
            }

            _sql = "Select ID,ClassName from PreviousClass_tb ";
            BAL.objBal.FillDropDown_withValue(_sql, ddlPreviousClass, "ClassName", "ID");
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("ClassName", ddlPreviousClass);
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
                    objBll.loadDefaultvalue("Nationality", txtMotherNationality);
                    objBll.loadDefaultvalue("Nationality", txtFatherNationality);
                    objBll.loadDefaultvalue("Nationality", TextBox65);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void LoadDefaultHomeTown()
        {
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("HomeTown", txtHomeTown);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void LoadDefaultMotherTongue()
        {
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("MotherTongue", txtMotherTongue);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void LoadDefaultCast()
        {
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("Caste", TextBox66);
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

        protected void Get_DocumentName()
        {
            _sql = "Select DocumentType,Id from dt_CreateDocumentName where BranchCode=" + Session["BranchCode"] + "";
            Repeater1.DataSource = BAL.objBal.GridFill(_sql);
            Repeater1.DataBind();
        }
        public void NewDocumentsDetails(Control ctrl)
        {
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

                    var base64Std = hfFile.Value;
                    var fileExtention = hdfilefileExtention.Value;

                    if (base64Std != string.Empty)
                    {
                        var filePath = @"../Uploads/Docs/";
                        var fileName = Session["SrNo"].ToString() + '_' + lblDocument.Text.Trim() + fileExtention;

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
                    //string filePath = @"../Uploads/Docs/";
                    //string fileExtention = Path.GetExtension(FileUpload4.PostedFile.FileName);
                    //string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
                    //string fileName = Session["SrNo"].ToString().Replace('/', '_') + '_' + lblDocument.Text.Trim() + '_' + datetime + fileExtention;
                    //FileUpload4.SaveAs(Server.MapPath(filePath + fileName));
                    obj.DocId = lblId.Text.Trim();
                    //obj.DocName = fileName;
                    //obj.DocPath = filePath + fileName;
                    obj.SrNo = Session["SrNo"].ToString().Trim();
                    obj.StEnRCode = Session["StEnRCode"].ToString().Trim();
                    obj.Session = Session["SessionName"].ToString();
                    obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                    obj.LoginName = Session["LoginName"].ToString();

                    obj.Softcopy = chksoft.Checked ? 1 : FileUpload1.HasFile ? 1 : 0;
                    obj.Hardcopy = chkhard.Checked ? 1 : 0;
                    obj.Varified = chkVerified.Checked ? 1 : 0;
                    obj.Remark = txtRemark.Text;
                    msg = new DAL().Set_StudentDocumentRecord(obj);
                    //}
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
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@srno", Session["SrNo"].ToString()),
                    new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()),
                    new SqlParameter("@DurationofCourse", txtDuration.Text.Trim()),
                    new SqlParameter("@RegistrationNo", txtRegistration.Text.Trim()),
                    new SqlParameter("@CCNo", txtCastCerti.Text.Trim()),
                    new SqlParameter("@ICNo", txtIncomeCerti.Text.Trim()),
                    new SqlParameter("@RCNo", txtRegiCer.Text.Trim()),
                    new SqlParameter("@CurrentYearDateofAdmission", ""),
                    new SqlParameter("@CourseType", txtTypeofCourse.Text.Trim()),
                    new SqlParameter("@AdmissionType", txtTypeofAdmission.Text.Trim()),
                    new SqlParameter("@BankAccNo", txtBankAccNo.Text.Trim()),
                    new SqlParameter("@BankName", txtBankName.Text.Trim()),
                    new SqlParameter("@BranchNameofBank", txtBranchNameofBank.Text.Trim()),
                    new SqlParameter("@IFSCode", txtIfsCode.Text.Trim()),
                    new SqlParameter("@StudentNameinPassbook", txtStudentNameinPassbook.Text.Trim()),
                    new SqlParameter("@DayScholarorHostalar", txtDayScholer.Text.Trim()),
                    new SqlParameter("@YearlyNoneRefundebleFee", txtYearlynonrefund.Text.Trim()),
                    new SqlParameter("@HandyCapType", txthandycaptype.Text.Trim()),
                    new SqlParameter("@HandyCapPercentage", txthandycapPer.Text.Trim()),
                    new SqlParameter("@HandyCapCompensation", txthandycapCompe.Text.Trim()),
                    new SqlParameter("@RiciptNoofDepositFee", txtReciptNoofDepositFee.Text.Trim()),
                    new SqlParameter("@LastYearScholarshipAmount", txtLastYearScholarAmount.Text.Trim()),
                    new SqlParameter("@LastYearScholarshipDepositFee", txtLastYearScholarDepoFee.Text.Trim()),
                    new SqlParameter("@LastYearClassorCourse", txtLastClass.Text.Trim()),
                    new SqlParameter("@LastYearExamResult", txtLastYearExamResult.Text.Trim()),
                    new SqlParameter("@LastYearExamTotalMarks", txtLastYearExamTatalMarks.Text.Trim()),
                    new SqlParameter("@LastYearExamTotalObtainMarks", txtLastYearExamTotalObtainMarks.Text.Trim()),
                    new SqlParameter("@ScholarshipCompensation", txtScholarCompeAmountAccotoClass.Text.Trim()),
                    new SqlParameter("@NameofInstitute", txtNameofInstitute.Text.Trim()),
                    new SqlParameter("@isBasedonIntermediateMarks", txtIsEntrybasedonInterMarksScore.Text.Trim()),
                    new SqlParameter("@TotalMarksinIntermediate", txtTotalMarksinInter.Text.Trim()),
                    new SqlParameter("@obtainedMarksinIntermediate", txtTotalobtainedMarksinInter.Text.Trim()),
                    new SqlParameter("@StudentAdharNo", txtStudentAdharNo.Text.Trim()),
                    new SqlParameter("@TransferCertificateNo", txtTransferCertiNo.Text.Trim()),
                    new SqlParameter("@LastSchoolorCollegeName", txtLastSchoolCollegeName.Text.Trim()),
                    new SqlParameter("@IdentityProof", txtIdentityProof.Text.Trim()),
                    new SqlParameter("@IntermediateRollNo", txtIntermediateRollNo.Text.Trim()),
                    new SqlParameter("@IntermediateBoard", txtIntermediateBoard.Text.Trim()),
                    new SqlParameter("@IntermediateYearofPssing", txtIntermediateYearofPssing.Text.Trim())
                };

                var photoext = "";
                if (fpUploadPhoto.HasFile)
                {
                    photoext = Path.GetExtension(fpUploadPhoto.FileName);
                    fpUploadPhoto.SaveAs(studentPhotoDirectoryPath + Session["SrNo"].ToString().Replace("/", "_") + photoext);
                }
                param.Add(new SqlParameter("@UploadPhotoPath", fpUploadPhoto.HasFile ? studentPhotoVertualPath + Session["SrNo"].ToString().Replace("/", "_") + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@PhotoName", fpUploadPhoto.HasFile ? Session["SrNo"].ToString().Replace("/", "_") + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                var signext = "";
                if (fuUploadStudentSignature.HasFile)
                {
                    signext = Path.GetExtension(fuUploadStudentSignature.FileName);
                    fuUploadStudentSignature.SaveAs(studentSignatureDirectoryPath + Session["SrNo"].ToString().Replace("/", "_") + signext);
                }
                param.Add(new SqlParameter("@UploadStudentSignature", fuUploadStudentSignature.HasFile ? studentSignatureVertualPath + Session["SrNo"].ToString().Replace("/", "_") + signext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@StudentSignatureName", fuUploadStudentSignature.HasFile ? Session["SrNo"].ToString().Replace("/", "_") + signext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                var parentSignext = "";
                if (fuUploadFatherMotherSigThumbPrint.HasFile)
                {
                    parentSignext = Path.GetExtension(fuUploadStudentSignature.FileName);
                    fuUploadFatherMotherSigThumbPrint.SaveAs(parentSignatureDirectoryPath + Session["SrNo"].ToString().Replace("/", "_") + parentSignext);
                }
                param.Add(new SqlParameter("@UploadParentsSignature", fuUploadFatherMotherSigThumbPrint.HasFile ? parentSignatureVertualPath + Session["SrNo"].ToString().Replace("/", "_") + parentSignext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@UploadParentsSignatureName", fuUploadFatherMotherSigThumbPrint.HasFile ? Session["SrNo"].ToString().Replace("/", "_") + parentSignext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
                param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

                // ReSharper disable once UnusedVariable
                var msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOtherDetailsProc", param);
            }
        }

        protected void LoadBoard()
        {
            for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
            {
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                _sql = "Select BoardName from BoardMaster where branchcode=" + Session["BranchCode"] + "";
                _oo.FillDropDown(_sql, drpBoard, "BoardName");
                _oo.FillDropDown(_sql, drpPreviousBoard, "BoardName");
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

        protected void AddPreviousInstitutionGridRow()
        {
            var dt = AddColumn();
            if (rptPreviousEducation.Items.Count == 0)
            {
                for (int i = 0; i < 3; i++)
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
                    // ReSharper disable once LocalVariableHidesMember
                    var lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
                    var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                    var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                    var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                    var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                    var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                    // ReSharper disable once LocalVariableHidesMember
                    var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                    var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");
                    // ReSharper disable once LocalVariableHidesMember
                    var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                    // ReSharper disable once LocalVariableHidesMember
                    var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                    var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
                    var txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
                    var txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
                    var txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
                    var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                    var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                    var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

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
                    dr["maxmarks"] = txtMm.Text;
                    dr["obtained"] = txtObtained.Text;
                    dr["per"] = txtPer.Text;
                    dt.Rows.Add(dr);
                    i++;

                    if (i == rptPreviousEducation.Items.Count)
                    {
                        var dr1 = dt.NewRow();
                        dr1[0] = i + 1;
                        dt.Rows.Add(dr1);
                        _sql = "Select BoardName from BoardMaster";
                        _oo.FillDropDown(_sql, drpBoard, "BoardName");
                    }
                }
                rptPreviousEducation.DataSource = dt;
                rptPreviousEducation.DataBind();
                LoadBoard();
                SetDropdownSelectedValue(dt);
                EnableControl();
            }
        }

        protected void SetDropdownSelectedValue(DataTable dt)
        {
            for (var j = 0; j < rptPreviousEducation.Items.Count; j++)
            {
                var drpBoard = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpBoard");
                var drpResult = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpResult");
                // ReSharper disable once LocalVariableHidesMember
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
                // ReSharper disable once LocalVariableHidesMember
                var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

                // ReSharper disable once LocalVariableHidesMember
                var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                // ReSharper disable once LocalVariableHidesMember
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

        protected void lnkAddMore_Click(object sender, EventArgs e)
        {
            AddPreviousInstitutionGridRow();
        }

        protected void DeletePreviousInstitutionGridRow(int rowindex)
        {
            var dt = AddColumn();
            var i = 0;
            while (i < rptPreviousEducation.Items.Count)
            {
                var dr = dt.NewRow();
                // ReSharper disable once LocalVariableHidesMember
                var lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
                var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                // ReSharper disable once LocalVariableHidesMember
                var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");
                // ReSharper disable once LocalVariableHidesMember
                var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                // ReSharper disable once LocalVariableHidesMember
                var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
                var txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
                var txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
                var txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
                var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

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
                dr["maxmarks"] = txtMm.Text;
                dr["obtained"] = txtObtained.Text;
                dr["per"] = txtPer.Text;
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

        protected void ReIndexingofSrNo()
        {
            for (var j = 0; j < rptPreviousEducation.Items.Count; j++)
            {
                // ReSharper disable once LocalVariableHidesMember
                var lblsrno = (Label)rptPreviousEducation.Items[j].FindControl("lblsrno");
                lblsrno.Text = (j + 1).ToString();
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var lnk = (LinkButton)sender;
            var currntrow = (RepeaterItem)lnk.NamingContainer;
            var i = currntrow.ItemIndex;
            DeletePreviousInstitutionGridRow(i);

        }

        #region
        public void GeneralDetailDropDown()
        {
            try
            {
                LoaddefaultCountry(DrpPerCountry); LoaddefaultCountry(DrpPreCountry);
                LoaddefaultState(DrpPerState, DrpPerCountry); LoaddefaultState(DrpPreState, DrpPreCountry);
                LoaddefaultCity(DrpPerCity, DrpPerState); LoaddefaultCity(DrpPreCity, DrpPreState);
                LoadDefaultCasteName();
                LoadDefaultReligion();
                LoadBloodGroup();
                LoadDefaultNationality();
                LoadDefaultHomeTown();
                LoadDefaultMotherTongue();
                LoadDefaultCast();
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
        public void LoadMachineNo()
        {
            string sql = "select MachineNo from PunchMachineConfigurationStudent where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(sql, ddlMachineNo, "MachineNo", "MachineNo");
            ddlMachineNo.Items.Insert(0, new ListItem("<--Select-->", ""));
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

            LoadDefaultFeeGroup();
            LoadDefaultHouse();

            LoadDefaultTypeofAdmission();

            LoadDefaultMod();
            LoadMachineNo();
        }

        private void LoadDefaultFeeGroup()
        {
            _sql = "Select id,  FeeGroupName from FeeGroupMaster ";
            _sql += " where  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
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
            _sql = "select HouseName from HouseMaster where BranchCode=" + Session["BranchCode"] + "";
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

        private void LoadDefaultMod()
        {
            using (var objBll = new BLL())
            {
                try
                {
                    objBll.loadDefaultvalue("ModeofDeposit", drpFeeDepositMOD);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public void FamilyDetailsDropDown()
        {
            LoaddefaultCountry(drpG1Country); LoaddefaultCountry(drpG2Country);
            //LoaddefaultState(drpG1State); LoaddefaultState(drpG2State);
            //LoaddefaultCity(drpG1City, drpG1State); LoaddefaultCity(drpG2City, drpG2State);

            LoadDefaultFatherOccu();
            LoadDefaultMotherOccu();
            DrpRelationship.SelectedIndex = 1;
            drpGuardiantwoRelationship.SelectedIndex = 2;
            txtincomefa.Text = "0";
            txtincomemonthlymother.Text = "0";

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
                cmd.Parameters.AddWithValue("@MiddleName", txtMidNa.Text);
                cmd.Parameters.AddWithValue("@LastName", txtlast.Text);
                var date = txtStudentDOB.Text.Trim();
                if (date != "")
                {
                    cmd.Parameters.AddWithValue("@DOB", date);
                }
                cmd.Parameters.AddWithValue("@Gender", RadioButtonList1.SelectedValue);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@MobileNumber", txtMobile.Text);
                cmd.Parameters.AddWithValue("@SiblingCategory", "No");
                cmd.Parameters.AddWithValue("@SBSrNo", "");
                cmd.Parameters.AddWithValue("@SBStName", "");
                cmd.Parameters.AddWithValue("@SBFathersName", "");
                cmd.Parameters.AddWithValue("@SBClass", "");
                cmd.Parameters.AddWithValue("@SBSection", "");
                cmd.Parameters.AddWithValue("@PhysicallDisabledCategory", RadioButtonList8.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@PhyStName", txtPhyName.Text);
                cmd.Parameters.AddWithValue("@PhyStDetail", txtPhyDetail.Text);
                cmd.Parameters.AddWithValue("@StLocalAddress", txtPreaddress.Text);
                cmd.Parameters.AddWithValue("@StLocalCountryId", DrpPreCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@StLocalStateId", DrpPreState.SelectedValue);
                cmd.Parameters.AddWithValue("@StLocalCityId", DrpPreCity.SelectedValue);
                cmd.Parameters.AddWithValue("@StLocalZip", txtPreZip.Text);
                cmd.Parameters.AddWithValue("@StPerAddress", txtPerAdd.Text);
                cmd.Parameters.AddWithValue("@StPerCountryId", DrpPerCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@StPerStateId", DrpPerState.SelectedValue);
                cmd.Parameters.AddWithValue("@StPerCityId", DrpPerCity.SelectedValue);
                cmd.Parameters.AddWithValue("@StPerZip", txtPerZip.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                cmd.Parameters.AddWithValue("@Religion", DropDownList1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Nationality", TextBox65.Text);
                cmd.Parameters.AddWithValue("@Category", DropDownList2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Caste", TextBox66.Text);
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


                var filePath = "";
                var fileName = "";

                var base64Std = hdStPhoto.Value;
                if (base64Std != string.Empty)
                {
                    filePath = @"../Uploads/StudentPhoto/";
                    var sessionName = Session["SessionName"].ToString();
                    fileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + sessionName + ".jpg";

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

                var smsAcknowledgment = "";
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


                var emailAcknowledgment = "";
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
                cmd.Parameters.AddWithValue("@isSmsAck", chkStMobile.Checked);
                cmd.Parameters.AddWithValue("@isEmailAck", chkStEmail.Checked);
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
                cmd.Parameters.AddWithValue("@FatherDesignation", txtdesfa.Text);
                cmd.Parameters.AddWithValue("@FatherQualification", txtqufa.SelectedValue);
                cmd.Parameters.AddWithValue("@FatherIncomeMonthly", txtincomefa.Text);
                cmd.Parameters.AddWithValue("@FatherOfficeAddress", txtoffaddfa.Text);
                cmd.Parameters.AddWithValue("@FatherContactNo", txtcontfa.Text);
                cmd.Parameters.AddWithValue("@FatherEmail", txtemailfather.Text);
                cmd.Parameters.AddWithValue("@FamilyIncomeMonthly", txtfailyincome.Text);
                cmd.Parameters.AddWithValue("@FamilyRelationship", DrpRelationship.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@FamilyEmail", txtemailfamily.Text);
                cmd.Parameters.AddWithValue("@FamilyGuardianName", txtguardianname.Text);
                cmd.Parameters.AddWithValue("@FamilyContactNo", txtcontactNo.Text);
                cmd.Parameters.AddWithValue("@MotherName", txtmotherNameeee.Text);
                cmd.Parameters.AddWithValue("@MotherOccupation", drpOccupationmoth.Text);
                cmd.Parameters.AddWithValue("@MotherDesignation", txtdesmoth.Text);
                cmd.Parameters.AddWithValue("@MotherQualification", txtqualimother.SelectedValue);
                cmd.Parameters.AddWithValue("@MotherIncomeMonthly", txtincomemonthlymother.Text);
                cmd.Parameters.AddWithValue("@MotherOfficeAddress", txtofficeaddmother.Text);
                cmd.Parameters.AddWithValue("@MotherContactNo", txtmothercontact.Text);
                cmd.Parameters.AddWithValue("@MotherEmail", txtmotheremail.Text);
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                if (txtGuardiantwoIncomeMonthly.Text != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@GuardiantwoIncomeMonthly", txtGuardiantwoIncomeMonthly.Text);
                }
                cmd.Parameters.AddWithValue("@GuardiantwoName", txtGuardiantwoName.Text);
                if (txtGuardiantwoContact.Text != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@GuardiantwoContact", txtGuardiantwoContact.Text);
                }
                if (drpGuardiantwoRelationship.SelectedIndex != 0)
                {
                    cmd.Parameters.AddWithValue("@GuardiantwoRelationship", drpGuardiantwoRelationship.Text);
                }
                cmd.Parameters.AddWithValue("@GuardiantwoEmail", txtGuardiantwoEmail.Text);


                var fatherImagePath = "";
                var fatherImageFileName = "";

                var base64Father = hdfatherPhoto.Value;
                if (base64Father != string.Empty)
                {
                    fatherImagePath = @"../Uploads/FatherPhoto/";
                    var sessionName = Session["SessionName"].ToString();
                    fatherImageFileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + "FatherImage" + '_' + sessionName + ".jpg";
                    using (FileStream fs = new FileStream(Server.MapPath((fatherImagePath + fatherImageFileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var data = Convert.FromBase64String(base64Father);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                    fatherImagePath = fatherImagePath + fatherImageFileName;
                }
                cmd.Parameters.AddWithValue("@FatherPhotoPath", fatherImagePath);
                cmd.Parameters.AddWithValue("@FatherPhotoName", fatherImageFileName);

                var motherImagePath = "";
                var motherImageFileName = "";
                var base64Mother = hdmotherPhoto.Value;
                if (base64Mother != string.Empty)
                {
                    motherImagePath = @"../Uploads/MotherPhoto/";
                    var sessionName = Session["SessionName"].ToString();
                    motherImageFileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + "MotherImage" + '_' + sessionName + ".jpg";

                    using (FileStream fs = new FileStream(Server.MapPath((motherImagePath + motherImageFileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var data = Convert.FromBase64String(base64Mother);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                    motherImagePath = motherImagePath + motherImageFileName;
                }
                cmd.Parameters.AddWithValue("@MotherPhotoPath", motherImagePath);
                cmd.Parameters.AddWithValue("@MotherPhotoName", motherImageFileName);
                cmd.Parameters.AddWithValue("@FatherOfficePhoneNo", txtFatherOfficePhoneNo.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherOfficeMobileNo", txtFatherOfficeMobileNo.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherOfficeEmail", txtFatherOfficeEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherOfficePhoneNo", txtMotherOfficePhoneNo.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherOfficeMobileNo", txtMotherOfficeMobileNo.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherOfficeEmail", txtMotherOfficeEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@ParentTotalIncome", txtParentTotalIncome.Text.Trim());


                var groupImagePath = "";
                var groupImageFileName = "";
                var base64GroupPhoto = hdbase64groupPhoto.Value;
                if (base64GroupPhoto != string.Empty)
                {
                    groupImagePath = @"../Uploads/GroupPhoto/";
                    var sessionName = Session["SessionName"].ToString();
                    groupImageFileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + "GroupImage" + '_' + sessionName + ".jpg";

                    using (FileStream fs = new FileStream(Server.MapPath((groupImagePath + groupImageFileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var data = Convert.FromBase64String(base64GroupPhoto);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
                cmd.Parameters.AddWithValue("@GroupPhotoPath", groupImagePath + groupImageFileName);
                cmd.Parameters.AddWithValue("@GroupPhotoName", groupImageFileName);
                cmd.Parameters.AddWithValue("@G1Address", txtG1Address.Text.Trim());
                cmd.Parameters.AddWithValue("@G1Country", drpG1Country.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G1State", drpG1State.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G1City", drpG1City.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G1PhoneNo", txtG1PhoneNo.Text.Trim());
                cmd.Parameters.AddWithValue("@G1MobileNo", txtG1MobileNo.Text.Trim());
                cmd.Parameters.AddWithValue("@G1Pin", txtPreZip.Text.Trim());
                cmd.Parameters.AddWithValue("@G2Address", txtG2Address.Text.Trim());
                cmd.Parameters.AddWithValue("@G2Country", drpG2Country.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G2State", drpG2State.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G2City", drpG2City.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@G2PhoneNo", txtG2PhoneNo.Text.Trim());
                cmd.Parameters.AddWithValue("@G2MobileNo", txtG2MobileNo.Text.Trim());
                cmd.Parameters.AddWithValue("@G2Pin", txtG2Pin.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherNationality", txtFatherNationality.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherNationality", txtMotherNationality.Text.Trim());
                // Parents Health
                cmd.Parameters.AddWithValue("@FatherHealthAdhaarNo", txt_f_healthAdhaar.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherHealthDOB", txt_f_healthDOB.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherHealthWeight", txt_f_healthWeight.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherHealthHeight", txt_f_healthHeight.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherHealthBloodgroup", drphealth_f_BloodGroup.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherHealthAdhaarNo", txt_m_healthAdhaar.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherHealthDOB", txt_m_healthDOB.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherHealthWeight", txt_m_healthWeight.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherHealthHeight", txt_m_healthHeight.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherHealthBloodgroup", drphealth_m_BloodGroup.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@CWSN", txt_m_healthCWSN.Text.Trim());
                cmd.Parameters.AddWithValue("@FatherAadhaarCardNo", txtfaadhaarcardno.Text.Trim());
                cmd.Parameters.AddWithValue("@MotherAadhaarCardNo", txtmaadharcardno.Text.Trim());

                var smsAcknowledgment = "";
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


                var emailAcknowledgment = "";
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

                cmd.Parameters.AddWithValue("@isfaSmsAck", chkFaMobile.Checked);
                cmd.Parameters.AddWithValue("@isfaEmailAck", chkFaEmail.Checked);
                cmd.Parameters.AddWithValue("@ismoSmsAck", chkMoMobile.Checked);
                cmd.Parameters.AddWithValue("@ismoEmailAck", chkMoEmail.Checked);
                cmd.Parameters.AddWithValue("@isguaSmsAck", (txtcontfa.Text != string.Empty ? 1 : 0));
                cmd.Parameters.AddWithValue("@isguaEmailAck", (txtemailfamily.Text != string.Empty ? 1 : 0));

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
                drpGuardiantwoRelationship.SelectedIndex = 1;
                return flag;
            }
        }
        public bool PreviousSchoolDetails()
        {
            var flag = true;
            using (var cmd = new SqlCommand())
            {
                // cmd.CommandText = "StudentPreviousSchoolProc";
                cmd.CommandText = "StudentPreviousSchoolUpdateProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                for (var i = 0; i < rptPreviousEducation.Items.Count; i++)
                {
                    //var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
                    //var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
                    //var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
                    //var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
                    //var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
                    //var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
                    //var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");
                    //var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
                    //var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
                    //var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
                    //var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
                    //var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
                    //var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");
                    ////if (txtExam.Text.Trim() == "") continue;
                    //cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
                    //cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                    //cmd.Parameters.AddWithValue("@Board", drpBoard.SelectedItem.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Result", drpResult.SelectedValue.Trim());
                    //cmd.Parameters.AddWithValue("@Medium", drpMedium.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Qualification", txtExam.Text.Trim());
                    //if (txtObtained.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@Marks", txtObtained.Text.Trim());
                    //}
                    //if (txtPer.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@Percentage", txtPer.Text.Trim());
                    //}
                    //if (txtInstitute.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@SchoolName", txtInstitute.Text.Trim());
                    //}
                    //if (txtMm.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@MaxMarks", txtMm.Text.Trim());
                    //}
                    //if (txtYop.Text.Trim()!="")
                    //{
                    //    cmd.Parameters.AddWithValue("@Yop", txtYop.Text.Trim());
                    //}

                    //if (txtSubject.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@Subjects", txtSubject.Text.Trim());
                    //}
                    //if (txtRollNo.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text.Trim());
                    //}
                    //if (txtCertificateNo.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@CertificateNo", txtCertificateNo.Text.Trim());
                    //}
                    //if (txtMarksSheetNo.Text.Trim() != "")
                    //{
                    //    cmd.Parameters.AddWithValue("@MarksSheetNo", txtMarksSheetNo.Text.Trim());
                    //}
                    //cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    //cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    //cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
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

                    //if (txtExam.Text.Trim() != "")
                    //{
                    cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                    cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", txtExam.Text.Trim());
                    cmd.Parameters.AddWithValue("@Board", drpPreviousBoard.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@Result", ddlResult.SelectedValue.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SchoolName", txtPreviousInstituteName.Text.Trim() == "" ? DBNull.Value : (object)txtPreviousInstituteName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Yop", txtYop.Text.Trim() == "" ? DBNull.Value : (object)txtYop.Text.Trim());
                    cmd.Parameters.AddWithValue("@Medium", ddlMediumprevious.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@Subjects", txtSubject.Text.Trim() == "" ? DBNull.Value : (object)txtSubject.Text.Trim());

                    cmd.Parameters.AddWithValue("@RollNo", txtRollNo1.Text.Trim() == "" ? DBNull.Value : (object)txtRollNo1.Text.Trim());
                    cmd.Parameters.AddWithValue("@CertificateNo", txtCertificateNo.Text.Trim() == "" ? DBNull.Value : (object)txtCertificateNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@MarksSheetNo", txtMarksSheetNo.Text.Trim() == "" ? DBNull.Value : (object)txtMarksSheetNo.Text.Trim());

                    cmd.Parameters.AddWithValue("@MaxMarks", txtMM.Text.Trim() == "" ? DBNull.Value : (object)txtMM.Text.Trim());
                    cmd.Parameters.AddWithValue("@Marks", txtObtained.Text.Trim() == "" ? DBNull.Value : (object)txtObtained.Text.Trim());
                    cmd.Parameters.AddWithValue("@Percentage", txtPer.Text.Trim() == "" ? DBNull.Value : (object)txtPer.Text.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@isActive", true);
                    cmd.Parameters.AddWithValue("@ExamId", drpExamCrackedof.SelectedValue);
                    cmd.Parameters.AddWithValue("@Rank", txtRank.Text.Trim());
                    cmd.Parameters.AddWithValue("@CatRank", txtCategoryRank.Text.Trim());
                    cmd.Parameters.AddWithValue("@AnyOther", txtAnyOtherCategoryRank.Text.Trim());
                    cmd.Parameters.AddWithValue("@UDISECode", txtUdiaseCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNoPrevious.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PreviousClass", ddlPreviousClass.SelectedValue);
                    cmd.Parameters.AddWithValue("@Attendance", txtAttendance.Text.Trim());
                    cmd.Parameters.AddWithValue("@MarksPercentage", txtMarksPercentage.Text.Trim());
                    try
                    {

                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        flag = true;
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        flag = false; _con.Close();
                    }
                    //}
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        flag = true;
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        flag = false; _con.Close();
                    }
                    catch (Exception ex1)
                    {
                        flag = false; _con.Close();
                    }
                }


                return flag;
            }
        }
        public bool Document()
        {
            var flag = true;
            _recordNotInsertDocument = true;
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "StudentDocumentsProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
                cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
                try
                {
                    _oo.MessageBox("Only in JPEG,BMP,PNG,JPG format allowed", Page);
                }
                catch (Exception)
                {
                    // ignored
                }
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();

                }
                catch (SqlException) { _recordNotInsertDocument = false; flag = false; _con.Close(); }
                catch (Exception) { _recordNotInsertDocument = false; flag = false; _con.Close(); }
                return flag;
            }

        }
        public bool OfficialDetails()
        {
            int Rollno = 0;
            int.TryParse(txtSchoolcollegeRollno.Text, out Rollno);
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
            ss1 += 1;
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
            parameter.Add(new SqlParameter("@FileNo", txtfileno.Text));
            parameter.Add(new SqlParameter("@Reference", txtReferences.Text));
            if (ddlShift.SelectedValue != "")
            {
                parameter.Add(new SqlParameter("@ShiftId", ddlShift.SelectedValue));
            }
            if (ddlEducationAct.SelectedValue != "")
            {
                parameter.Add(new SqlParameter("@EducationActId", ddlEducationAct.SelectedValue));
            }
            parameter.Add(new SqlParameter("@Remark", txtrema.Text));
            parameter.Add(new SqlParameter("@Board", DrpBoard.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@TypeOFAdmision", DrpNEWOLSAdmission.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            parameter.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            parameter.Add(new SqlParameter("@SessionName", Session["SessionName"]));
            parameter.Add(new SqlParameter("@medium", drpMedium.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@Card", drpPanelCardType.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@CardId", drpPanelCardType.SelectedValue.ToString()));
            parameter.Add(new SqlParameter("@HostelRequired", rbHostel.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@TransportRequired", rbTransport.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@HouseName", DropDownList4.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@LibraryRequired", rbLibrary.SelectedItem.ToString()));
            parameter.Add(new SqlParameter("@Enquiry", txtEnquiryNo.Text));
            parameter.Add(new SqlParameter("@BoardUniversityRollNo", txtUniversityBoardRollNo.Text));
            parameter.Add(new SqlParameter("@InstituteRollNo", Rollno.ToString()));
            parameter.Add(new SqlParameter("@CardNo", txtCardNo.Text));
            parameter.Add(new SqlParameter("@MachineNo", ddlMachineNo.SelectedValue));
            parameter.Add(new SqlParameter("@Course", DropCourse.SelectedValue));
            parameter.Add(new SqlParameter("@Branch", DropBranch.SelectedValue));
            parameter.Add(new SqlParameter("@Scholarship", rbScholarship.SelectedItem.Text.Trim()));
            parameter.Add(new SqlParameter("@ModForHostel", drpHostalMOD.SelectedIndex != 0 ? drpHostalMOD.SelectedValue : "I"));
            parameter.Add(new SqlParameter("@ModForTransport", drpTransportMOD.SelectedIndex != 0 ? drpTransportMOD.SelectedValue : "I"));
            parameter.Add(new SqlParameter("@ModForLibrary", drpLibraryMOD.SelectedIndex != 0 ? drpLibraryMOD.SelectedValue : "I"));
            parameter.Add(new SqlParameter("@MODForFeeDeposit", drpFeeDepositMOD.SelectedValue));
            parameter.Add(new SqlParameter("@SMSAcknowledgment", drpSMSAcknowledgmentTo.SelectedValue));
            parameter.Add(new SqlParameter("@EmailAcknowledgment", drpEmailAcknowledgmentTo.SelectedValue));
            double admissionDoneAt = 0;
            double.TryParse(txtAddDoneat.Text, out admissionDoneAt);
            parameter.Add(new SqlParameter("@AdmissionDoneAt", admissionDoneAt));
            if (txtDFA.Text.Trim() != "")
            {
                parameter.Add(new SqlParameter("@DFA", txtDFA.Text.Trim()));
            }
            if (txtCFA.Text.Trim() != "")
            {
                parameter.Add(new SqlParameter("@CFA", txtCFA.Text.Trim()));
            }
            if (txtCOFA.Text.Trim() != "")
            {
                parameter.Add(new SqlParameter("@COFA", txtCOFA.Text.Trim()));
            }
            if (txtSFA.Text.Trim() != "")
            {
                parameter.Add(new SqlParameter("@SFA", txtSFA.Text.Trim()));
            }
            if (DropStream.SelectedIndex != 0)
            {
                parameter.Add(new SqlParameter("@Streamid", DropStream.SelectedValue));
            }
            if (TextBox67.Text.Trim() != "")
            {
                parameter.Add(new SqlParameter("@RecieptNo", TextBox67.Text.Trim()));
            }
            //if (txtpen.Text.Trim() != "")
            //{
            //    parameter.Add(new SqlParameter("@Pen", txtpen.Text.Trim()));
            //}
            if (RadioButtonList3.SelectedIndex == 0 || RadioButtonList3.SelectedIndex == 1)
            {
                var typeofedu = RadioButtonList3.SelectedIndex == 0 ? "R" : "P";
                parameter.Add(new SqlParameter("@TypeofEducation", typeofedu));
            }
            parameter.Add(new SqlParameter("@APAARID", txtAparid.Text.Trim()));
            parameter.Add(new SqlParameter("@BookletNo", txtProspectus.Text.Trim()));
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
            _sql = "select StudentName,MiddleName,LastName,FatherName,Class,Sex,FatherContactNo,Prospectus from AdmissionFormCollection where RecieptNo='" + TextBox67.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            txtFirstNa.Text = _oo.ReturnTag(_sql, "StudentName");
            txtMidNa.Text = _oo.ReturnTag(_sql, "MiddleName");
            txtlast.Text = _oo.ReturnTag(_sql, "LastName");
            txtfaNameee.Text = _oo.ReturnTag(_sql, "FatherName");
            txtcontfa.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            txtcontactNo.Text = _oo.ReturnTag(_sql, "FatherContactNo");
            txtguardianname.Text = _oo.ReturnTag(_sql, "FatherName");
            txtProspectus.Text = _oo.ReturnTag(_sql, "Prospectus");

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
            ss += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
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
                txtcontactNo.Enabled = false;
                txtguardianname.Enabled = false;
                DrpRelationship.Enabled = false;
                drpPanelCardType.Enabled = false;
                drpFeeDepositMOD.Enabled = false;
                DropDownList4.Enabled = false;
                txtCardNo.Enabled = false;
                txtUniversityBoardRollNo.Enabled = false;
                txtSchoolcollegeRollno.Enabled = false;
                txtfileno.Enabled = false;
                ddlShift.Enabled = false;
                ddlEducationAct.Enabled = false;
                txtFirstNa.Enabled = false;
                txtMidNa.Enabled = false;
                txtlast.Enabled = false;
                DropCourse.Enabled = false;
                DropAdmissionClass.Enabled = false;
                DrpNEWOLSAdmission.Enabled = false;
                Repeater1.Visible = false;
                RadioButtonList3.Enabled = false;
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
            drpG1State.SelectedValue = DrpPreState.SelectedValue;
            LoadCity(drpG1City, drpG1State);
            LoadCity(DrpPreCity, DrpPreState);
        }
        protected void DrpPreCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpG1City.SelectedValue = DrpPreCity.SelectedValue;
        }
        protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity(DrpPerCity, DrpPerState);
        }

        protected void RadioButtonList8_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel2.Visible = RadioButtonList8.SelectedItem.Text == "Yes";
        }
        private void LoadSection()
        {
            _sql = "select SectionName from SectionMaster where ClassNameId='" + DropAdmissionClass.SelectedValue + "'";
            _sql += "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown(_sql, drpSection, "SectionName");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        protected void DropAdmissionClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
            LoadSection();
            Get_DocumentName();
        }
        public bool Validation()
        {
            return true;
        }
        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            string fg = "select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where srno='" + (lblSrString.Text + txtSr.Text.Trim()) + "'";
            if (_oo.Duplicate(fg))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry S.R. No. (" + (lblSrString.Text + txtSr.Text.Trim()) + ") already exists!", "A");
            }
            else
            {
                _sql = "select * from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + "";
                if (!_oo.Duplicate(_sql))
                {
                    _oo.msgbox(Page, msgbox, "Please initialize S.R. No.!", "A");
                }
                else
                {

                    var sql1 = "Select SrNo from StudentOfficialDetails where SrNo='" + txtSr.Text.Trim() + "'";
                    var sql2 = "Select SrNo from StudentGenaralDetail where SrNo='" + txtSr.Text.Trim() + "'";
                    var sql3 = "Select SrNo from StudentFamilyDetails where SrNo='" + txtSr.Text.Trim() + "'";
                    var sql4 = "Select SrNo from StudentPreviousSchool where SrNo='" + txtSr.Text.Trim() + "'";
                    var sql5 = "Select SrNo from StudentDocs where SrNo='" + txtSr.Text.Trim() + "'";
                    var _sql6 = "select BranchCode from tblAutometedSRNO where BranchCode=" + Session["BranchCode"] + " and SrNoType='Manual'";
                    if ((_oo.Duplicate(sql1) || _oo.Duplicate(sql2) || _oo.Duplicate(sql3) || _oo.Duplicate(sql4) || _oo.Duplicate(sql5)) && _oo.Duplicate(_sql6))
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
                                _sql = "delete from StudentPreviousSchool where SrNo='" + Session["SrNo"] + "' and BranchCode=" + Session["BranchCode"] + "";
                                _oo.ProcedureDatabase(_sql);
                                _sql = "delete from StudentDocs where StEnRCode='" + Session["SrNo"] + "' and BranchCode=" + Session["BranchCode"] + "";
                                _oo.ProcedureDatabase(_sql);
                                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry record not inserted (Contact to Admin).", "W");
                            }
                            else
                            {
                                PreviousSchoolDetails();
                                SaveOptionalSubjectRecord();
                                NewDocumentsDetails(LinkButton14);
                                SaveStudentOtherDetails();
                                StudentPasswordGeneration();
                                GuardianPasswordGeneration();
                                SetEntranceExamRecord();
                                try
                                {
                                    Session["srNo"] = Session["SrNo"].ToString();
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
        }

        public void NewDocumentsDetails()
        {
            var studentId = txtSr.Text.Trim();

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


                    var base64Std = hfFile.Value;
                    var fileExtention = hdfilefileExtention.Value;

                    if (base64Std != string.Empty)
                    {
                        var filePath = @"../Uploads/Docs/";
                        var fileName = txtSr.Text.Trim().Replace('/', '-') + '_' + lblDocument.Text.Trim() + fileExtention;
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

                    obj.SrNo = studentId.Trim();
                    obj.StEnRCode = Session["StEnRCode"].ToString();
                    obj.Session = Session["SessionName"].ToString();
                    obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                    obj.LoginName = Session["LoginName"].ToString();

                    obj.Softcopy = chksoft.Checked ? 1 : FileUpload1.HasFile ? 1 : 0;
                    obj.Hardcopy = chkhard.Checked ? 1 : 0;
                    obj.Varified = chkVerified.Checked ? 1 : 0;
                    obj.Remark = txtRemark.Text;
                    msg = new DAL().Set_StudentDocumentRecord(obj);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (msg != string.Empty)
            {
            }
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

                    param.Add(new SqlParameter("@Srno", Session["SrNo"].ToString()));
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

        public void SendFeeSms(string mobileno, string familyConNo)
        {
            if (mobileno != "")
            {
                ComposeSMSStudent(mobileno, "10");
                ComposeSMSGuardian(familyConNo, "11");
                //SendFeesSmsStudent(mobileno, "2");
                //SendFeesSmsGuardian(familyConNo, "3");
            }
            else
            {
                ComposeSMSStudent(familyConNo, "10");
                ComposeSMSGuardian(familyConNo, "11");
                //SendFeesSmsStudent(familyConNo,"2");
                //SendFeesSmsGuardian(familyConNo,"3");
            }
        }

        public void ComposeSMSStudent(string mobileno, string pageId)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@SessionName",Session["SessionName"]),
                    new SqlParameter("@SrNo",Session["SrNo"]),
                    new SqlParameter("@BranchCode",Session["BranchCode"])
                };
                DataSet ds = _oo.ReturnDataSet("USP_StudentRegistrationTemplate", param.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    string msg = SendSms(ds, mobileno, pageId);
                }

            }
            catch
            {

            }

        }

        public void ComposeSMSGuardian(string mobileno, string pageId)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@SessionName",Session["SessionName"]),
                    new SqlParameter("@SrNo",Session["SrNo"]),
                    new SqlParameter("@BranchCode",Session["BranchCode"])
                };
                DataSet ds = _oo.ReturnDataSet("USP_GuardianRegistrationTemplate", param.ToArray());
                if (ds != null && ds.Tables.Count > 0)
                {
                    string msg = SendSms(ds, mobileno, pageId);
                }

            }
            catch
            {

            }

        }

        public string SendSms(DataSet ds, string contactno, string pageid)
        {
            string msg;
            try
            {
                DataTable data = new DataTable();
                data = ds.Tables[0];

                var fatherContactNo = contactno;

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

                new SMS().SendSms(fatherContactNo, msg, pageid, Session["BranchCode"].ToString());

            }
            catch
            {
                msg = "";
            }
            return msg;
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
                        _sql += "    and  Srno='" + Session["SrNo"].ToString().Trim() + "' order by id desc";
                        var studentName = _oo.ReturnTag(_sql, "StudentName");
                        _sql = "Select top(1) CollegeShortNa from CollegeMaster where BranchCode=" + Session["BranchCode"] + " ";
                        var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");
                        _sql = "Select top(1) UserName,Password StudentPassword from StudentLoginandPassword where";
                        _sql += " Srno='" + Session["SrNo"].ToString().Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                        var stEnRCode = _oo.ReturnTag(_sql, "UserName");
                        var studentPassword = _oo.ReturnTag(_sql, "StudentPassword");
                        mess = "Congrats! " + studentName + ", you've registered successfully with " + collegeShortNa + ". Your Username: " + stEnRCode + " and Password: " + studentPassword + " from ";
                        if (fmobileNo != "")
                        {
                            _sql = "Select SmsSent From SmsEmailMaster where Id='10' and  BranchCode=" + Session["BranchCode"] + "";
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
                        _sql += " where Srno='" + Session["SrNo"].ToString().Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                        var guardianName = _oo.ReturnTag(_sql, "GuardianName");
                        _sql = "Select top(1) CollegeShortNa from CollegeMaster where BranchCode=" + Session["BranchCode"] + " ";
                        var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");
                        _sql = "Select top(1) UserName,Password GuardianPassword from GuardianLoginandPassword where";
                        _sql += " Srno='" + Session["SrNo"].ToString().Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by id desc";
                        var stEnRCode = _oo.ReturnTag(_sql, "UserName");
                        var guardianPassword = _oo.ReturnTag(_sql, "GuardianPassword");
                        mess = "Congrats! Mr./Ms. " + guardianName + ", your ward is registered successfully with " + collegeShortNa + ". Your Username: " + stEnRCode + " and Password: " + guardianPassword + " from ";
                        if (fmobileNo != "")
                        {
                            _sql = "Select SmsSent From SmsEmailMaster where Id='11' and BranchCode=" + Session["BranchCode"] + "";
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

        protected void txtemailfather_TextChanged(object sender, EventArgs e)
        {
            txtemailfamily.Text = txtemailfather.Text;
        }
        protected void txtfaNameee_TextChanged(object sender, EventArgs e)
        {
            txtguardianname.Text = txtfaNameee.Text;
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
            mess = mess + "             " + txtFirstNa.Text + " " + txtMidNa.Text + " " + txtlast.Text + ",";
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
                _sql += " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
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
        protected void DrpRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DrpRelationship.SelectedIndex)
            {
                case 0:
                    txtguardianname.Text = txtfaNameee.Text;
                    txtemailfamily.Text = txtemailfather.Text;
                    break;
                case 1:
                    txtguardianname.Text = txtmotherNameeee.Text;
                    txtemailfamily.Text = txtmotheremail.Text;
                    break;
                default:
                    txtguardianname.Text = "";
                    txtemailfamily.Text = "";
                    txtguardianname.Focus();
                    break;
            }
        }
        protected void drpGuardiantwoRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (drpGuardiantwoRelationship.SelectedIndex)
            {
                case 0:
                    txtGuardiantwoName.Text = txtfaNameee.Text;
                    txtGuardiantwoEmail.Text = txtemailfather.Text;
                    break;
                case 1:
                    txtGuardiantwoName.Text = txtmotherNameeee.Text;
                    txtGuardiantwoEmail.Text = txtmotheremail.Text;
                    break;
                default:
                    txtGuardiantwoName.Text = "";
                    txtGuardiantwoEmail.Text = "";
                    txtGuardiantwoName.Focus();
                    break;
            }
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

        private void LoadOptionalSubject()
        {
            _sql = "Select SubjectGroup,id from SubjectGroupMaster where Classid='" + DropAdmissionClass.SelectedValue + "' and BranchCode=" + Session["BranchCode"] + " and Branchid='" + DropBranch.SelectedValue + "'  and SectionName='" + drpSection.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and IsCompulsory=0";
            rbOptionalSubject.DataSource = _oo.GridFill(_sql);
            rbOptionalSubject.DataTextField = "SubjectGroup";
            rbOptionalSubject.DataValueField = "id";
            rbOptionalSubject.DataBind();
            divopt.Visible = rbOptionalSubject.Items.Count > 0;
        }

        private void LoadClass()
        {

            if (Session["Logintype"].ToString() == "FromAdmission" && Session["RecieptNo"] != null)
            {

                _sql = "Select Id,ClassName from ClassMaster";
                _sql += " where (Course='" + DropCourse.SelectedValue + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0  order by CIDOrder asc";
                _oo.FillDropDown_withValue(_sql, DropAdmissionClass, "ClassName", "Id");
                DropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
                txtDFA.Text = txtCFA.Text = txtCOFA.Text = txtSFA.Text = "";
            }
            else
            {
                _sql = "Select Id,ClassName from ClassMaster";
                _sql += " where (Course='" + DropCourse.SelectedValue + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 order by CIDOrder asc";
                _oo.FillDropDown_withValue(_sql, DropAdmissionClass, "ClassName", "Id");
                DropAdmissionClass.Items.Insert(0, new ListItem("<--Select-->", "0"));
                txtDFA.Text = txtCFA.Text = txtCOFA.Text = txtSFA.Text = "";
            }

        }
        protected void DropCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClass();
        }
        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            LoadState(drpG2State, drpG2Country);
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
                    LoaddefaultState(drpG2State, drpG2Country);
                    LoaddefaultCity(drpG2City, drpG2State);
                }
            }
            catch (Exception ex)
            {
                BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox3);
            }
        }
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            LoadState(drpG1State, drpG1Country);
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
                    LoaddefaultState(drpG1State, drpG1Country);
                    LoaddefaultCity(drpG1City, drpG1State);
                }
            }
            catch (Exception ex)
            {
                BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox2);
            }
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

        protected void DrpPerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPerCountry.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, DrpPerState, "StateName", "id");
            if (DrpPerState.Items.Count == 0)
            {
                DrpPerState.Items.Add(new ListItem("Other", "0"));
                DrpPerCity.Items.Clear();
                DrpPerCity.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                LoadCity(DrpPerCity, DrpPerState);
            }
        }
        protected void DrpPreCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPreCountry.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, DrpPreState, "StateName", "id");
            drpG1Country.SelectedValue = DrpPreCountry.SelectedValue;
            DrpPreState.Items.Clear();
            drpG1State.Items.Clear();
            DrpPreCity.Items.Clear();
            drpG1City.Items.Clear();
            LoadState(DrpPreState, DrpPreCountry);
            LoadState(drpG1State, drpG1Country);
            LoadCity(DrpPreCity, DrpPreState);
            LoadCity(drpG1City, drpG1State);
            if (DrpPreState.Items.Count == 0)
            {
                DrpPreState.Items.Clear();
                drpG1State.Items.Clear();
                DrpPreCity.Items.Clear();
                drpG1City.Items.Clear();
                DrpPreState.Items.Add(new ListItem("Other", "0"));
                DrpPreCity.Items.Add(new ListItem("Other", "0"));
                drpG1State.Items.Add(new ListItem("Other", "0"));
                drpG1City.Items.Add(new ListItem("Other", "0"));
            }
        }
        protected void drpG1Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + drpG1Country.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpG1State, "StateName", "id");
            if (drpG1State.Items.Count == 0)
            {
                drpG1State.Items.Add(new ListItem("Other", "0"));
                drpG1City.Items.Clear();
                drpG1City.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                LoadState(drpG1State, drpG1Country);
                LoadCity(drpG1City, drpG1State);
            }
        }
        protected void drpG2Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sql = " Select StateName,Id from StateMaster where CountryId='" + drpG2Country.SelectedValue + "'";
            BAL.objBal.FillDropDown_withValue(_sql, drpG2State, "StateName", "id");
            if (drpG2State.Items.Count == 0)
            {
                drpG2State.Items.Add(new ListItem("Other", "0"));
                drpG2City.Items.Clear();
                drpG2City.Items.Add(new ListItem("Other", "0"));
            }
            else
            {
                LoadState(drpG2State, drpG2Country);
                LoadCity(drpG2City, drpG2State);
            }
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
            Response.Redirect("StudentQuickRegistration.aspx?check=student_registration");
        }
        protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
            LoadOptionalSubject();
        }
        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch();
            LoadOptionalSubject();
        }
        //Get ALL Exam Records


        public void SetEntranceExamRecord()
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@ExamId", drpExamCrackedof.SelectedValue),
                new SqlParameter("@RollNo", txtRollNo1.Text.Trim()),
                new SqlParameter("@Rank", txtRank.Text.Trim()),
                new SqlParameter("@CatRank", txtCategoryRank.Text.Trim()),
                new SqlParameter("@AnyOther", txtAnyOtherCategoryRank.Text.Trim()),
                new SqlParameter("@srno", Session["SrNo"].ToString()),
                new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()),
                new SqlParameter("@SessionName", Session["SessionName"].ToString()),
                new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
                new SqlParameter("@LoginName", Session["LoginName"].ToString())
            };

            DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentEntranceExamDetails_PROC", param);
        }
        protected void chkCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCopy.Checked == true)
            {
                txtPerAdd.Text = txtPreaddress.Text;
                txtPerZip.Text = txtPreZip.Text;
                DrpPerCountry.Items.Clear();
                LoaddefaultCountry(DrpPerCountry);
                DrpPerCountry.SelectedValue = DrpPreCountry.SelectedValue;

                DrpPerState.Items.Clear();
                LoadState(DrpPerState, DrpPerCountry);
                if (DrpPerState.Items.Count == 0)
                {
                    DrpPerState.Items.Add(new ListItem("Other", "0"));
                }
                else
                {
                    DrpPerState.SelectedValue = DrpPreState.SelectedValue;
                }


                DrpPerCity.Items.Clear();
                LoadCity(DrpPerCity, DrpPerState);
                if (DrpPerCity.Items.Count == 0)
                {
                    DrpPerCity.Items.Add(new ListItem("Other", "0"));
                }
                else
                {
                    DrpPerCity.SelectedValue = DrpPreCity.SelectedValue;
                }
            }
        }
        protected void txtemailfather_TextChanged1(object sender, EventArgs e)
        {
            txtemailfamily.Text = txtemailfather.Text;
        }
        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }



        protected void txtSr_TextChanged(object sender, EventArgs e)
        {
            txtSr.Text = txtSr.Text.Replace(" ", "");
            string ss1 = "select 1 from StudentOfficialDetails where SrNo='" + txtSr.Text.Trim() + "'";
            if (_oo.Duplicate(ss1))
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate S.R. No.- " + txtSr.Text.Trim() + "", "A");
                txtSr.Text = "";
                txtSr.Focus();
                return;
            }
        }

        protected void txtmaadharcardno_TextChanged(object sender, EventArgs e)
        {
            //if (txtmaadharcardno.Text != "" && txtmaadharcardno.Text.Length == 12)
            //{
            //    var sql = "SELECT 1 val FROM ValidationSetting where id=1 and isapply=1";
            //    if (_oo.ReturnTag(sql, "val") == "1")
            //    {
            //        _sql = "select MotherAadhaarCardNo from StudentFamilyDetails where ";
            //        _sql += "  MotherAadhaarCardNo='" + txtmaadharcardno.Text + "'";
            //        if (_oo.ReturnTag(_sql, "MotherAadhaarCardNo") != "")
            //        {
            //            LinkButton14.Enabled = false;
            //            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Aadhar No is already Used!", "A");
            //        }
            //        else
            //        {
            //            LinkButton14.Enabled = true;
            //        }
            //    }
            //}
        }

        protected void txtProspectus_TextChanged(object sender, EventArgs e)
        {
            if (txtProspectus.Text != "")
            {

                _sql = "select BookletNo from StudentOfficialDetails where ";
                _sql += "  BookletNo='" + txtProspectus.Text + "'";
                if (_oo.ReturnTag(_sql, "BookletNo") != "")
                {
                    LinkButton14.Enabled = false;
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Booklet/Form No. is already Used!", "A");
                }
                else
                {
                    LinkButton14.Enabled = true;
                }
            }

        }

        protected void txtAadharNo_TextChanged(object sender, EventArgs e)
        {
            if (txtAadharNo.Text != "" && txtAadharNo.Text.Length == 12)
            {
                var sql = "SELECT 1 val FROM ValidationSetting where id=1 and isapply=1";
                if (_oo.ReturnTag(sql, "val") == "1")
                {
                    _sql = "select AadharNo from StudentGenaralDetail where ";
                    _sql += "  AadharNo='" + txtAadharNo.Text + "'";
                    if (_oo.ReturnTag(_sql, "AadharNo") != "")
                    {
                        LinkButton14.Enabled = false;
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Aadhar No is already Used!", "A");
                    }
                    else
                    {
                        LinkButton14.Enabled = true;
                    }
                }
            }

        }

        protected void txtfaadhaarcardno_TextChanged(object sender, EventArgs e)
        {
            //if (txtfaadhaarcardno.Text != "" && txtfaadhaarcardno.Text.Length == 12)
            //{
            //    var sql = "SELECT 1 val FROM ValidationSetting where id=1 and isapply=1";
            //    if (_oo.ReturnTag(sql, "val") == "1")
            //    {
            //        _sql = "select FatherAadhaarCardNo from StudentFamilyDetails where ";
            //        _sql += " and FatherAadhaarCardNo='" + txtfaadhaarcardno.Text + "'";
            //        if (_oo.ReturnTag(_sql, "FatherAadhaarCardNo") != "")
            //        {
            //            LinkButton14.Enabled = false;
            //            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This Aadhar No is already Used!", "A");
            //        }
            //        else
            //        {
            //            LinkButton14.Enabled = true;
            //        }
            //    }
            //}

        }

        protected void txtAparid_TextChanged(object sender, EventArgs e)
        {
            if (txtAparid.Text != "")
            {
                var sql = "SELECT 1 val FROM ValidationSetting where id=3 and isapply=1";
                if (_oo.ReturnTag(sql, "val") == "1")
                {
                    _sql = "select APAARID from StudentOfficialDetails where ";
                    _sql += "  APAARID='" + txtAparid.Text + "'";
                    if (_oo.ReturnTag(_sql, "APAARID") != "")
                    {
                        LinkButton14.Enabled = false;
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "This APAAR ID No is already Used!", "A");
                    }
                    else
                    {
                        LinkButton14.Enabled = true;
                    }
                }
            }
        }
    }
}