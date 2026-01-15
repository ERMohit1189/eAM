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
    public partial class Adminstudent_registration_new : Page
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
        public Adminstudent_registration_new()
        {
            //_con = new SqlConnection();
            //_oo = new Campus();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["LoginName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            // ReSharper disable once NotAccessedVariable
            var ss = string.Empty;
            // ReSharper disable once RedundantAssignment
            var co = 0;
            //_con = _oo.dbGet_connection();
            Campus camp = new Campus(); camp.LoadLoader(loader);
            if (!IsPostBack)
            {
                try
                {
                    //CheckValueAddDeleteUpdate();
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    //LoadEntranceExamName();
                    //GeneralDetailDropDown();
                    //FamilyDetailsDropDown();
                    //OfficialDetailDropDown();
                    //var getdate = DateTime.Today.ToString("dd-MMM-yyyy");
                    //txtAgeOnDate.Text = getdate;
                    //TextBox100.Text = getdate;
                    //Get_DocumentName();
                }
                catch (Exception ex) { _oo.MessageBox(ex.Message, Page); }

                //Panel2.Visible = false;

                _sql = "select id  from StudentOfficialDetails";
                _sql = _sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                try
                {
                    co = Convert.ToInt32(_oo.ReturnTag(_sql, "id"));
                }
                catch (Exception) { co = 0; }
                co = co + 1;
                // ReSharper disable once RedundantAssignment
                //ss = IdGenerationCollege(co.ToString());

                //txtFirstNa.Focus();
                // ReSharper disable once LocalVariableHidesMember
                // ReSharper disable once UnusedVariable
                var obj = new BLL();
                //CheckTextTrnsformation();
                //AddPreviousInstitutionGridRow();
            }
        }

        //public void TextTrnsform()
        //{
        //    var param = new List<SqlParameter>
        //    {
        //        new SqlParameter("@isDo", "Select"),
        //        new SqlParameter("@value", ""),
        //        new SqlParameter("@SessionName", ""),
        //        new SqlParameter("@LoginName", "")
        //    };
        //    var para = new SqlParameter("@Msg", "")
        //    {
        //        Direction = ParameterDirection.Output,
        //        Size = 0x10
        //    };
        //    param.Add(para);
        //    var value = _dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        //    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "textTransform", "finalsubmit('" + value + "')", true);
        //}

        //private void LoaddefaultCountry(DropDownList drp)
        //{
        //    _sql = "Select CountryName,Id from CountryMaster";
        //    BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
        //    using (var objBll = new BLL())
        //    {
        //        try
        //        {
        //            objBll.loadDefaultvalue("Country", drp);
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
        //}

        //private void LoaddefaultState(DropDownList drp)
        //{
        //    _sql = "Select StateName,Id from StateMaster";
        //    BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
        //    using (var objBll = new BLL())
        //    {
        //        try
        //        {
        //            objBll.loadDefaultvalue("State", drp);
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
        //}

        //private void LoaddefaultCity(DropDownList drp, DropDownList drpValue)
        //{
        //    _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
        //    BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
        //    using (var objBll = new BLL())
        //    {
        //        try
        //        {
        //            objBll.loadDefaultvalue("City", drp);
        //        }
        //        catch
        //        {
        //            // ignored
        //        }
        //    }
        //}

        //// ReSharper disable once UnusedMember.Local
        //private void LoadCountry(DropDownList drp)
        //{
        //    _sql = "Select CountryName,Id from CountryMaster";
        //    BAL.objBal.FillDropDown_withValue(_sql, drp, "CountryName", "id");
        //}

        //private void LoadState(DropDownList drp)
        //{
        //    _sql = "Select StateName,Id from StateMaster";
        //    BAL.objBal.FillDropDown_withValue(_sql, drp, "StateName", "id");
        //}

        //private void LoadCity(DropDownList drp, DropDownList drpValue)
        //{
        //    _sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue + "'";
        //    BAL.objBal.FillDropDown_withValue(_sql, drp, "CityName", "id");
        //}

        ////private void LoadBloodGroup()
        ////{
        ////    _sql = "Select BloodGroupName,BloodGroupId from BloodGroupMaster";
        ////    BAL.objBal.FillDropDown_withValue(_sql, drpBloodGroup, "BloodGroupName", "BloodGroupId");
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Blood Group", drpBloodGroup);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultBoard()
        ////{
        ////    _sql = "Select BoardName,id from BoardMaster";
        ////    BAL.objBal.FillDropDown_withValue(_sql, DrpBoard, "BoardName", "id");
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Board", DrpBoard);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultMedium()
        ////{
        ////    _sql = "Select Medium,id from MediumMaster where SessionName='" + Session["SessionName"] + "'";
        ////    BAL.objBal.FillDropDown_withValue(_sql, drpMedium, "Medium", "id");
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Medium", drpMedium);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultNationality()
        ////{
        ////    _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Nationality'";
        ////    txtMotherNationality.Text = txtFatherNationality.Text = TextBox65.Text = _oo.ReturnTag(_sql, "defaultvalue");
        ////}

        ////private void LoadDefaultHomeTown()
        ////{
        ////    _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='HomeTown'";
        ////    txtHomeTown.Text = _oo.ReturnTag(_sql, "defaultvalue");
        ////}

        ////private void LoadDefaultMotherTongue()
        ////{
        ////    _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='MotherTongue'";
        ////    txtMotherTongue.Text = _oo.ReturnTag(_sql, "defaultvalue");
        ////}

        ////private void LoadDefaultCast()
        ////{
        ////    _sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Caste'";
        ////    TextBox66.Text = _oo.ReturnTag(_sql, "defaultvalue");
        ////}

        ////private void LoadDefaultReligion()
        ////{
        ////    _sql = "select ReligionName,ReligionId from ReligionMaster";
        ////    _oo.FillDropDown_withValue(_sql, DropDownList1, "ReligionName", "ReligionId");

        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Religion", DropDownList1);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        //protected void SwipeLabel(Control parent)
        //{
        //    foreach (Control childControl in parent.Controls)
        //    {
        //        if ((childControl.Controls.Count > 0))
        //        {
        //            SwipeLabel(childControl);
        //        }
        //        else
        //        {
        //            if (childControl is Label)
        //            {
        //                var text = ((Label)childControl).Text;
        //                _sql = "Select replace from DefaultText where replacewith='" + text + "'";
        //                if (_oo.ReturnTag(_sql, "replace") != "")
        //                {
        //                    ((Label)childControl).Text = _oo.ReturnTag(_sql, "replace");
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void CheckTextTrnsformation()
        //{
        //    var param = new List<SqlParameter>
        //    {
        //        new SqlParameter("@isDo", "Select"),
        //        new SqlParameter("@value", ""),
        //        new SqlParameter("@SessionName", ""),
        //        new SqlParameter("@LoginName", "")
        //    };
        //    var para = new SqlParameter("@Msg", "")
        //    {
        //        Direction = ParameterDirection.Output,
        //        Size = 0x10
        //    };
        //    param.Add(para);
        //    var value = _dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        //    if (value == DBNull.Value) return;
        //    switch ((string)value)
        //    {
        //        case "U":
        //            AddStyletocotrol(Page, "uppercase");
        //            break;
        //        case "L":
        //            AddStyletocotrol(Page, "lowercase");
        //            break;
        //        case "C":
        //            AddStyletocotrol(Page, "capitalize");
        //            break;
        //        default:
        //            AddStyletocotrol(Page, "none");
        //            break;
        //    }
        //}

        //public void AddStyletocotrol(Control parent, string istransform)
        //{
        //    foreach (Control childControl in parent.Controls)
        //    {
        //        if ((childControl.Controls.Count > 0))
        //        {
        //            AddStyletocotrol(childControl, istransform);
        //        }
        //        else
        //        {
        //            var box = childControl as TextBox;
        //            if (box != null)
        //            {
        //                if (box.ID != "txtSr")
        //                {
        //                    box.Style.Add("text-transform", istransform);
        //                }
        //            }
        //            else if (childControl is DropDownList)
        //            {
        //                ((DropDownList)childControl).Style.Add("text-transform", istransform);
        //            }
        //            else if (childControl is RadioButton)
        //            {
        //                ((RadioButton)childControl).Style.Add("text-transform", istransform);
        //            }
        //            else if (childControl is RadioButtonList)
        //            {
        //                ((RadioButtonList)childControl).Style.Add("text-transform", istransform);
        //            }
        //            else if (childControl is CheckBox)
        //            {
        //                ((CheckBox)childControl).Style.Add("text-transform", istransform);
        //            }
        //            else if (childControl is CheckBoxList)
        //            {
        //                ((CheckBoxList)childControl).Style.Add("text-transform", istransform);
        //            }
        //        }
        //    }
        //}

        //protected void Get_DocumentName()
        //{
        //    //_sql = "Select DocumentType,Id from dt_CreateDocumentName";
        //    //Repeater1.DataSource = BAL.objBal.GridFill(_sql);
        //    //Repeater1.DataBind();
        //}
        ////public void NewDocumentsDetails(Control ctrl)
        ////{
        ////    var msg = "";
        ////    try
        ////    {
        ////        var obj = new BAL.Set_StudentDocumentRecord();
        ////        for (var i = 0; i < Repeater1.Items.Count; i++)
        ////        {
        ////            var unused = (FileUpload)Repeater1.Items[i].FindControl("FileUpload4");
        ////            var lblId = (Label)Repeater1.Items[i].FindControl("lblId");
        ////            var lblDocument = (Label)Repeater1.Items[i].FindControl("lblDocument");
        ////            var chksoft = (CheckBox)Repeater1.Items[i].FindControl("Chksoft");
        ////            var chkhard = (CheckBox)Repeater1.Items[i].FindControl("Chkhard");
        ////            var chkVerified = (CheckBox)Repeater1.Items[i].FindControl("chkVerified");
        ////            var txtRemark = (TextBox)Repeater1.Items[i].FindControl("txtRemark");
        ////            var hfFile = (HiddenField)Repeater1.Items[i].FindControl("hfFile");
        ////            var hdfilefileExtention = (HiddenField)Repeater1.Items[i].FindControl("hdfilefileExtention");

        ////            var base64Std = hfFile.Value;
        ////            var fileExtention = hdfilefileExtention.Value;

        ////            if (base64Std != string.Empty)
        ////            {
        ////                var filePath = @"../Uploads/Docs/";
        ////                var fileName = Session["srno"].ToString() + '_' + lblDocument.Text.Trim() + fileExtention;

        ////                using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
        ////                {
        ////                    using (BinaryWriter bw = new BinaryWriter(fs))
        ////                    {
        ////                        var data = Convert.FromBase64String(base64Std);
        ////                        bw.Write(data);
        ////                        bw.Close();
        ////                    }
        ////                }

        ////                obj.DocName = fileName;
        ////                obj.DocPath = filePath + fileName;
        ////            }
        ////            else
        ////            {
        ////                obj.DocName = "";
        ////                obj.DocPath = "";
        ////            }

        ////            obj.DocId = lblId.Text.Trim();
        ////            obj.SrNo = Session["SrNo"].ToString().Trim();
        ////            obj.StEnRCode = Session["StEnRCode"].ToString().Trim();
        ////            obj.Session = Session["SessionName"].ToString();
        ////            obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
        ////            obj.LoginName = Session["LoginName"].ToString();

        ////            //obj.Softcopy = chksoft.Checked ? 1 : FileUpload1.HasFile ? 1 : 0;
        ////            obj.Hardcopy = chkhard.Checked ? 1 : 0;
        ////            obj.Varified = chkVerified.Checked ? 1 : 0;
        ////            obj.Remark = txtRemark.Text;
        ////            msg = new DAL().Set_StudentDocumentRecord(obj);
        ////            //}
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        msg = ex.Message;
        ////    }
        ////    if (msg != string.Empty)
        ////    {
        ////        _oo.MessageBoxforUpdatePanel(msg, ctrl);
        ////    }
        ////}

        ////protected void SaveStudentOtherDetails()
        ////{
        ////    if (rbScholarship.SelectedIndex == 0)
        ////    {
        ////        var studentPhotoDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "StudentPhoto" + "_" + Session["SessionName"] + "/"));
        ////        var studentPhotoVertualPath = "~/Uploads/Scholarship/" + "StudentPhoto" + "_" + Session["SessionName"] + "/";
        ////        if (!Directory.Exists(studentPhotoDirectoryPath))
        ////        {
        ////            Directory.CreateDirectory(studentPhotoDirectoryPath);
        ////        }
        ////        var studentSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "studentSignature" + "_" + Session["SessionName"] + "/"));
        ////        var studentSignatureVertualPath = "~/Uploads/Scholarship/" + "studentSignature" + "_" + Session["SessionName"] + "/";
        ////        if (!Directory.Exists(studentSignatureDirectoryPath))
        ////        {
        ////            Directory.CreateDirectory(studentSignatureDirectoryPath);
        ////        }
        ////        var parentSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "parentSignature" + "_" + Session["SessionName"] + "/"));
        ////        var parentSignatureVertualPath = "~/Uploads/Scholarship/" + "parentSignature" + "_" + Session["SessionName"] + "/";
        ////        if (!Directory.Exists(parentSignatureDirectoryPath))
        ////        {
        ////            Directory.CreateDirectory(parentSignatureDirectoryPath);
        ////        }
        ////        var param = new List<SqlParameter>
        ////        {
        ////            new SqlParameter("@srno", Session["srno"].ToString()),
        ////            new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()),
        ////            new SqlParameter("@DurationofCourse", txtDuration.Text.Trim()),
        ////            new SqlParameter("@RegistrationNo", txtRegistration.Text.Trim()),
        ////            new SqlParameter("@CCNo", txtCastCerti.Text.Trim()),
        ////            new SqlParameter("@ICNo", txtIncomeCerti.Text.Trim()),
        ////            new SqlParameter("@RCNo", txtRegiCer.Text.Trim()),
        ////            new SqlParameter("@CurrentYearDateofAdmission", ""),
        ////            new SqlParameter("@CourseType", txtTypeofCourse.Text.Trim()),
        ////            new SqlParameter("@AdmissionType", txtTypeofAdmission.Text.Trim()),
        ////            new SqlParameter("@BankAccNo", txtBankAccNo.Text.Trim()),
        ////            new SqlParameter("@BankName", txtBankName.Text.Trim()),
        ////            new SqlParameter("@BranchNameofBank", txtBranchNameofBank.Text.Trim()),
        ////            new SqlParameter("@IFSCode", txtIfsCode.Text.Trim()),
        ////            new SqlParameter("@StudentNameinPassbook", txtStudentNameinPassbook.Text.Trim()),
        ////            new SqlParameter("@DayScholarorHostalar", txtDayScholer.Text.Trim()),
        ////            new SqlParameter("@YearlyNoneRefundebleFee", txtYearlynonrefund.Text.Trim()),
        ////            new SqlParameter("@HandyCapType", txthandycaptype.Text.Trim()),
        ////            new SqlParameter("@HandyCapPercentage", txthandycapPer.Text.Trim()),
        ////            new SqlParameter("@HandyCapCompensation", txthandycapCompe.Text.Trim()),
        ////            new SqlParameter("@RiciptNoofDepositFee", txtReciptNoofDepositFee.Text.Trim()),
        ////            new SqlParameter("@LastYearScholarshipAmount", txtLastYearScholarAmount.Text.Trim()),
        ////            new SqlParameter("@LastYearScholarshipDepositFee", txtLastYearScholarDepoFee.Text.Trim()),
        ////            new SqlParameter("@LastYearClassorCourse", txtLastClass.Text.Trim()),
        ////            new SqlParameter("@LastYearExamResult", txtLastYearExamResult.Text.Trim()),
        ////            new SqlParameter("@LastYearExamTotalMarks", txtLastYearExamTatalMarks.Text.Trim()),
        ////            new SqlParameter("@LastYearExamTotalObtainMarks", txtLastYearExamTotalObtainMarks.Text.Trim()),
        ////            new SqlParameter("@ScholarshipCompensation", txtScholarCompeAmountAccotoClass.Text.Trim()),
        ////            new SqlParameter("@NameofInstitute", txtNameofInstitute.Text.Trim()),
        ////            new SqlParameter("@isBasedonIntermediateMarks", txtIsEntrybasedonInterMarksScore.Text.Trim()),
        ////            new SqlParameter("@TotalMarksinIntermediate", txtTotalMarksinInter.Text.Trim()),
        ////            new SqlParameter("@obtainedMarksinIntermediate", txtTotalobtainedMarksinInter.Text.Trim()),
        ////            new SqlParameter("@StudentAdharNo", txtStudentAdharNo.Text.Trim()),
        ////            new SqlParameter("@TransferCertificateNo", txtTransferCertiNo.Text.Trim()),
        ////            new SqlParameter("@LastSchoolorCollegeName", txtLastSchoolCollegeName.Text.Trim()),
        ////            new SqlParameter("@IdentityProof", txtIdentityProof.Text.Trim()),
        ////            new SqlParameter("@IntermediateRollNo", txtIntermediateRollNo.Text.Trim()),
        ////            new SqlParameter("@IntermediateBoard", txtIntermediateBoard.Text.Trim()),
        ////            new SqlParameter("@IntermediateYearofPssing", txtIntermediateYearofPssing.Text.Trim())
        ////        };

        ////        var photoext = "";
        ////        if (fpUploadPhoto.HasFile)
        ////        {
        ////            photoext = Path.GetExtension(fpUploadPhoto.FileName);
        ////            fpUploadPhoto.SaveAs(studentPhotoDirectoryPath + Session["srno"].ToString().Replace("/", "_") + photoext);
        ////        }
        ////        param.Add(new SqlParameter("@UploadPhotoPath", fpUploadPhoto.HasFile ? studentPhotoVertualPath + Session["srno"].ToString().Replace("/", "_") + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
        ////        param.Add(new SqlParameter("@PhotoName", fpUploadPhoto.HasFile ? Session["srno"].ToString().Replace("/", "_") + photoext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
        ////        var signext = "";
        ////        if (fuUploadStudentSignature.HasFile)
        ////        {
        ////            signext = Path.GetExtension(fuUploadStudentSignature.FileName);
        ////            fuUploadStudentSignature.SaveAs(studentSignatureDirectoryPath + Session["srno"].ToString().Replace("/", "_") + signext);
        ////        }
        ////        param.Add(new SqlParameter("@UploadStudentSignature", fuUploadStudentSignature.HasFile ? studentSignatureVertualPath + Session["srno"].ToString().Replace("/", "_") + signext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
        ////        param.Add(new SqlParameter("@StudentSignatureName", fuUploadStudentSignature.HasFile ? Session["srno"].ToString().Replace("/", "_") + signext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
        ////        var parentSignext = "";
        ////        if (fuUploadFatherMotherSigThumbPrint.HasFile)
        ////        {
        ////            parentSignext = Path.GetExtension(fuUploadStudentSignature.FileName);
        ////            fuUploadFatherMotherSigThumbPrint.SaveAs(parentSignatureDirectoryPath + Session["srno"].ToString().Replace("/", "_") + parentSignext);
        ////        }
        ////        param.Add(new SqlParameter("@UploadParentsSignature", fuUploadFatherMotherSigThumbPrint.HasFile ? parentSignatureVertualPath + Session["srno"].ToString().Replace("/", "_") + parentSignext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
        ////        param.Add(new SqlParameter("@UploadParentsSignatureName", fuUploadFatherMotherSigThumbPrint.HasFile ? Session["srno"].ToString().Replace("/", "_") + parentSignext : DBNull.Value.ToString(CultureInfo.InvariantCulture)));
        ////        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        ////        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
        ////        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        ////        // ReSharper disable once UnusedVariable
        ////        var msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOtherDetailsProc", param);
        ////    }
        ////}

        //protected void LoadBoard()
        //{
        //    for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
        //    {
        //        var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
        //        _sql = "Select BoardName from BoardMaster";
        //        _oo.FillDropDown(_sql, drpBoard, "BoardName");
        //    }
        //}

        //protected DataTable AddColumn()
        //{
        //    var dt = new DataTable();
        //    dt.Columns.Add("srno"); dt.Columns.Add("exam");
        //    dt.Columns.Add("board"); dt.Columns.Add("result"); dt.Columns.Add("institute");
        //    dt.Columns.Add("yop"); dt.Columns.Add("medium");
        //    dt.Columns.Add("subject"); dt.Columns.Add("rollno");
        //    dt.Columns.Add("certificateno"); dt.Columns.Add("marksSheetno");
        //    dt.Columns.Add("city"); dt.Columns.Add("state");
        //    dt.Columns.Add("country"); dt.Columns.Add("maxmarks");
        //    dt.Columns.Add("obtained"); dt.Columns.Add("per");

        //    return dt;
        //}

        //protected DataRow SetInitialValue(DataTable dt)
        //{
        //    var dr = dt.NewRow();
        //    dr[0] = 1;
        //    dr["exam"] = ""; dr["board"] = ""; dr["result"] = "";
        //    dr["institute"] = ""; dr["yop"] = "";
        //    dr["medium"] = ""; dr["subject"] = "";
        //    dr["rollno"] = ""; dr["certificateno"] = "";
        //    dr["marksSheetno"] = ""; dr["city"] = ""; dr["state"] = "";
        //    dr["country"] = ""; dr["maxmarks"] = "";
        //    dr["obtained"] = ""; dr["per"] = "";

        //    return dr;
        //}

        //protected void AddPreviousInstitutionGridRow()
        //{
        //    var dt = AddColumn();
        //    if (rptPreviousEducation.Items.Count == 0)
        //    {
        //        dt.Rows.Add(SetInitialValue(dt));
        //        rptPreviousEducation.DataSource = dt;
        //        rptPreviousEducation.DataBind();
        //        LoadBoard();
        //    }
        //    else
        //    {
        //        var i = 0;
        //        while (i < rptPreviousEducation.Items.Count)
        //        {
        //            var dr = dt.NewRow();
        //            // ReSharper disable once LocalVariableHidesMember
        //            var lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
        //            var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
        //            var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
        //            var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
        //            var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
        //            var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
        //            // ReSharper disable once LocalVariableHidesMember
        //            var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
        //            var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");
        //            // ReSharper disable once LocalVariableHidesMember
        //            var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
        //            // ReSharper disable once LocalVariableHidesMember
        //            var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
        //            var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
        //            var txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
        //            var txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
        //            var txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
        //            var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
        //            var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
        //            var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

        //            dr["srno"] = lblsrno.Text;
        //            dr["exam"] = txtExam.Text;
        //            dr["board"] = drpBoard.SelectedValue;
        //            dr["result"] = drpResult.SelectedValue;
        //            dr["institute"] = txtInstitute.Text;
        //            dr["yop"] = txtYop.Text;
        //            dr["medium"] = drpMedium.SelectedValue;
        //            dr["subject"] = txtSubject.Text;
        //            dr["rollno"] = txtRollNo.Text.Trim();
        //            dr["certificateno"] = txtCertificateNo.Text.Trim();
        //            dr["marksSheetno"] = txtMarksSheetNo.Text.Trim();
        //            dr["city"] = txtCity.Text;
        //            dr["state"] = txtState.Text;
        //            dr["country"] = txtCountry.Text;
        //            dr["maxmarks"] = txtMm.Text;
        //            dr["obtained"] = txtObtained.Text;
        //            dr["per"] = txtPer.Text;
        //            dt.Rows.Add(dr);
        //            i++;

        //            if (i == rptPreviousEducation.Items.Count)
        //            {
        //                var dr1 = dt.NewRow();
        //                dr1[0] = i + 1;
        //                dt.Rows.Add(dr1);
        //                _sql = "Select BoardName from BoardMaster";
        //                _oo.FillDropDown(_sql, drpBoard, "BoardName");
        //            }
        //        }
        //        rptPreviousEducation.DataSource = dt;
        //        rptPreviousEducation.DataBind();
        //        LoadBoard();
        //        SetDropdownSelectedValue(dt);
        //        EnableControl();
        //    }
        //}

        //protected void SetDropdownSelectedValue(DataTable dt)
        //{
        //    for (var j = 0; j < rptPreviousEducation.Items.Count; j++)
        //    {
        //        var drpBoard = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpBoard");
        //        var drpResult = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpResult");
        //        // ReSharper disable once LocalVariableHidesMember
        //        var drpMedium = (DropDownList)rptPreviousEducation.Items[j].FindControl("drpMedium");
        //        drpBoard.SelectedValue = dt.Rows[j]["board"].ToString();
        //        drpResult.SelectedValue = dt.Rows[j]["result"].ToString();
        //        drpMedium.SelectedValue = dt.Rows[j]["medium"].ToString();
        //    }
        //}

        //protected void EnableControl()
        //{
        //    for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
        //    {
        //        var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
        //        var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
        //        var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
        //        var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
        //        var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
        //        // ReSharper disable once LocalVariableHidesMember
        //        var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
        //        var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");

        //        // ReSharper disable once LocalVariableHidesMember
        //        var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
        //        // ReSharper disable once LocalVariableHidesMember
        //        var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
        //        var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");

        //        //TextBox txtCity = (TextBox))rptPreviousEducation.Items[i].FindControl("txtCity");
        //        //TextBox txtState = (TextBox))rptPreviousEducation.Items[i].FindControl("txtState");
        //        //TextBox txtCountry = (TextBox))rptPreviousEducation.Items[i].FindControl("txtCountry");
        //        var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
        //        var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
        //        var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");
        //        if (txtExam.Text != "")
        //        {
        //            drpBoard.Enabled = true;
        //            drpResult.Enabled = true;
        //            txtInstitute.Enabled = true;
        //            txtYop.Enabled = true;
        //            drpMedium.Enabled = true;
        //            txtSubject.Enabled = true;
        //            txtRollNo.Enabled = true;
        //            txtCertificateNo.Enabled = true;
        //            txtMarksSheetNo.Enabled = true;
        //            txtMm.Enabled = true;
        //            txtObtained.Enabled = true;
        //            txtPer.Enabled = true;
        //        }
        //    }
        //}

        //protected void lnkAddMore_Click(object sender, EventArgs e)
        //{
        //    AddPreviousInstitutionGridRow();
        //}

        //protected void DeletePreviousInstitutionGridRow(int rowindex)
        //{
        //    var dt = AddColumn();
        //    var i = 0;
        //    while (i < rptPreviousEducation.Items.Count)
        //    {
        //        var dr = dt.NewRow();
        //        // ReSharper disable once LocalVariableHidesMember
        //        var lblsrno = (Label)rptPreviousEducation.Items[i].FindControl("lblsrno");
        //        var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
        //        var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
        //        var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
        //        var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
        //        var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
        //        // ReSharper disable once LocalVariableHidesMember
        //        var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
        //        var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");
        //        // ReSharper disable once LocalVariableHidesMember
        //        var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
        //        // ReSharper disable once LocalVariableHidesMember
        //        var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
        //        var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
        //        var txtCity = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCity");
        //        var txtState = (TextBox)rptPreviousEducation.Items[i].FindControl("txtState");
        //        var txtCountry = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCountry");
        //        var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
        //        var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
        //        var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");

        //        dr["srno"] = lblsrno.Text;
        //        dr["exam"] = txtExam.Text;
        //        dr["board"] = drpBoard.SelectedValue;
        //        dr["result"] = drpResult.SelectedValue;
        //        dr["institute"] = txtInstitute.Text;
        //        dr["yop"] = txtYop.Text;
        //        dr["medium"] = drpMedium.SelectedValue;
        //        dr["subject"] = txtSubject.Text;
        //        dr["rollno"] = txtRollNo.Text.Trim();
        //        dr["certificateno"] = txtCertificateNo.Text.Trim();
        //        dr["marksSheetno"] = txtMarksSheetNo.Text.Trim();
        //        dr["city"] = txtCity.Text;
        //        dr["state"] = txtState.Text;
        //        dr["country"] = txtCountry.Text;
        //        dr["maxmarks"] = txtMm.Text;
        //        dr["obtained"] = txtObtained.Text;
        //        dr["per"] = txtPer.Text;
        //        dt.Rows.Add(dr);
        //        i++;
        //    }

        //    dt.Rows.RemoveAt(rowindex);

        //    rptPreviousEducation.DataSource = dt;
        //    rptPreviousEducation.DataBind();
        //    LoadBoard();
        //    ReIndexingofSrNo();
        //    SetDropdownSelectedValue(dt);
        //    EnableControl();

        //}

        //protected void ReIndexingofSrNo()
        //{
        //    for (var j = 0; j < rptPreviousEducation.Items.Count; j++)
        //    {
        //        // ReSharper disable once LocalVariableHidesMember
        //        var lblsrno = (Label)rptPreviousEducation.Items[j].FindControl("lblsrno");
        //        lblsrno.Text = (j + 1).ToString();
        //    }
        //}

        //protected void lnkDelete_Click(object sender, EventArgs e)
        //{
        //    var lnk = (LinkButton)sender;
        //    var currntrow = (RepeaterItem)lnk.NamingContainer;
        //    var i = currntrow.ItemIndex;
        //    DeletePreviousInstitutionGridRow(i);

        //}

        ////#region
        ////public void GeneralDetailDropDown()
        ////{
        ////    try
        ////    {
        ////        LoaddefaultCountry(DrpPerCountry); LoaddefaultCountry(DrpPreCountry);
        ////        LoaddefaultState(DrpPerState); LoaddefaultState(DrpPreState);
        ////        LoaddefaultCity(DrpPerCity, DrpPerState); LoaddefaultCity(DrpPreCity, DrpPreState);
        ////        LoadDefaultCasteName();
        ////        LoadDefaultReligion();
        ////        LoadBloodGroup();
        ////        LoadDefaultNationality();
        ////        LoadDefaultHomeTown();
        ////        LoadDefaultMotherTongue();
        ////        LoadDefaultCast();
        ////    }
        ////    catch (Exception)
        ////    {
        ////        // ignored
        ////    }

        ////}

        ////private void LoadDefaultCasteName()
        ////{
        ////    _sql = "select CasteName,CasteId from CasteMaster";
        ////    _oo.FillDropDown_withValue(_sql, DropDownList2, "CasteName", "CasteId");
        ////    DropDownList2.Items.Insert(0, new ListItem("<--Select Category-->", ""));
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Category", DropDownList2);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}
        ////public void OfficialDetailDropDown()
        ////{
        ////    LoadCourse();
        ////    LoadClass();
        ////    LoadBranch();
        ////    LoadStream();

        ////    LoadSection();
        ////    LoadDefaultMedium();
        ////    LoadDefaultBoard();

        ////    LoadDefaultFeeGroup();
        ////    LoadDefaultHouse();

        ////    LoadDefaultTypeofAdmission();

        ////    LoadDefaultMod();
        ////}

        ////private void LoadDefaultFeeGroup()
        ////{
        ////    _sql = "Select FeeGroupName from FeeGroupMaster ";
        ////    _sql = _sql + " where  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        ////    BAL.objBal.FillDropDownWithOutSelect(_sql, drpPanelCardType, "FeeGroupName");
        ////    drpPanelCardType.Items.Insert(0, new ListItem("<--Select Fee Group-->", ""));
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("FeeGroup", drpPanelCardType);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultHouse()
        ////{
        ////    _sql = "select HouseName from HouseMaster where SessionName='" + Session["SessionName"] + "'";
        ////    _oo.FillDropDownWithOutSelect(_sql, DropDownList4, "HouseName");
        ////    DropDownList4.Items.Insert(0, new ListItem("<--Select House Name-->", ""));
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("House", DropDownList4);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultTypeofAdmission()
        ////{
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("TypeofAdmission", DrpNEWOLSAdmission);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultMod()
        ////{
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("ModeofDeposit", drpFeeDepositMOD);
        ////        }
        ////        catch (Exception)
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////public void FamilyDetailsDropDown()
        ////{
        ////    LoaddefaultCountry(drpG1Country); LoaddefaultCountry(drpG2Country);
        ////    LoaddefaultState(drpG1State); LoaddefaultState(drpG2State);
        ////    LoaddefaultCity(drpG1City, drpG1State); LoaddefaultCity(drpG2City, drpG2State);

        ////    LoadDefaultFatherOccu();
        ////    LoadDefaultMotherOccu();
        ////    DrpRelationship.SelectedIndex = 1;
        ////    drpGuardiantwoRelationship.SelectedIndex = 2;
        ////    txtincomefa.Text = "0";
        ////    txtincomemonthlymother.Text = "0";

        ////}

        ////private void LoadDefaultFatherOccu()
        ////{
        ////    _sql = "Select DesignationName from GuardianDesMaster where DesignationName not like 'House%'";
        ////    _oo.FillDropDownWithOutSelect(_sql, drpOccupationfa, "DesignationName");
        ////    drpOccupationfa.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Occupation", drpOccupationfa);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}

        ////private void LoadDefaultMotherOccu()
        ////{
        ////    _sql = "Select DesignationName from GuardianDesMaster";
        ////    _oo.FillDropDownWithOutSelect(_sql, drpOccupationmoth, "DesignationName");
        ////    drpOccupationmoth.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        ////    using (var objBll = new BLL())
        ////    {
        ////        try
        ////        {
        ////            objBll.loadDefaultvalue("Occupation", drpOccupationmoth);
        ////        }
        ////        catch
        ////        {
        ////            // ignored
        ////        }
        ////    }
        ////}


        ////public bool GeneralDetails()
        ////{
        ////    var flag = true;
        ////    _recordNotInsertGeneral = true;
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        cmd.CommandText = "StudentGenaralDetailProc";
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Connection = _con;
        ////        cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
        ////        cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
        ////        cmd.Parameters.AddWithValue("@FirstName", txtFirstNa.Text);
        ////        cmd.Parameters.AddWithValue("@MiddleName", txtMidNa.Text);
        ////        cmd.Parameters.AddWithValue("@LastName", txtlast.Text);
        ////        var date = txtStudentDOB.Text.Trim();
        ////        cmd.Parameters.AddWithValue("@DOB", date);
        ////        cmd.Parameters.AddWithValue("@Gender", RadioButtonList1.SelectedValue);
        ////        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
        ////        cmd.Parameters.AddWithValue("@MobileNumber", txtMobile.Text);
        ////        cmd.Parameters.AddWithValue("@SiblingCategory", "No");
        ////        cmd.Parameters.AddWithValue("@SBSrNo", "");
        ////        cmd.Parameters.AddWithValue("@SBStName", "");
        ////        cmd.Parameters.AddWithValue("@SBFathersName", "");
        ////        cmd.Parameters.AddWithValue("@SBClass", "");
        ////        cmd.Parameters.AddWithValue("@SBSection", "");
        ////        cmd.Parameters.AddWithValue("@PhysicallDisabledCategory", RadioButtonList8.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@PhyStName", txtPhyName.Text);
        ////        cmd.Parameters.AddWithValue("@PhyStDetail", txtPhyDetail.Text);
        ////        cmd.Parameters.AddWithValue("@StLocalAddress", txtPreaddress.Text);
        ////        _sql = "Select Id from StateMaster where StateName='" + DrpPreState.SelectedItem + "'";
        ////        var dd1 = _oo.ReturnTag(_sql, "Id");
        ////        cmd.Parameters.AddWithValue("@StLocalStateId", dd1);
        ////        _sql = "Select Id from CityMaster where CityName='" + DrpPreCity.SelectedItem + "'";
        ////        var dd = _oo.ReturnTag(_sql, "Id");
        ////        cmd.Parameters.AddWithValue("@StLocalCityId", dd);
        ////        cmd.Parameters.AddWithValue("@StLocalZip", txtPreZip.Text);
        ////        cmd.Parameters.AddWithValue("@StPerAddress", txtPerAdd.Text);
        ////        _sql = "Select Id from StateMaster where StateName='" + DrpPerState.SelectedItem + "'";
        ////        var d = _oo.ReturnTag(_sql, "Id");
        ////        cmd.Parameters.AddWithValue("@StPerStateId", d);
        ////        _sql = "Select Id from CityMaster where CityName='" + DrpPerCity.SelectedItem + "'";
        ////        var sd = _oo.ReturnTag(_sql, "Id");
        ////        cmd.Parameters.AddWithValue("@StPerCityId", sd);
        ////        cmd.Parameters.AddWithValue("@StPerZip", txtPerZip.Text);
        ////        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        ////        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        ////        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        ////        cmd.Parameters.AddWithValue("@Religion", DropDownList1.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@Nationality", TextBox65.Text);
        ////        cmd.Parameters.AddWithValue("@Category", DropDownList2.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@Caste", TextBox66.Text);
        ////        cmd.Parameters.AddWithValue("@BloodGroup", drpBloodGroup.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@HouseName", "");
        ////        cmd.Parameters.AddWithValue("@Height", txtHeight.Text);
        ////        cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
        ////        cmd.Parameters.AddWithValue("@VisionL", txtVLeft.Text);
        ////        cmd.Parameters.AddWithValue("@VisionR", txtVRight.Text);
        ////        cmd.Parameters.AddWithValue("@DentalHygiene", txtDental.Text);
        ////        cmd.Parameters.AddWithValue("@OralHygiene", txtOral.Text);
        ////        cmd.Parameters.AddWithValue("@IdentificationMark", txtIMark.Text);
        ////        cmd.Parameters.AddWithValue("@SpecificAilment", txtSpeAilment.Text);

        ////        //string filePath = "";
        ////        //string fileName = "";
        ////        //if (avatarUpload.HasFile)
        ////        //{
        ////        //    filePath = @"../Uploads/StudentPhoto/";
        ////        //    string fileExtention = Path.GetExtension(avatarUpload.PostedFile.FileName);
        ////        //    string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
        ////        //    fileName = Session["SrNo"].ToString().Trim() + '_' + "Photo" + '_' + datetime + fileExtention;
        ////        //    avatarUpload.SaveAs(Server.MapPath(filePath + fileName));
        ////        //}

        ////        var filePath = "";
        ////        var fileName = "";

        ////        var base64Std = hdStPhoto.Value;
        ////        if (base64Std != string.Empty)
        ////        {
        ////            filePath = @"../Uploads/StudentPhoto/";
        ////            var sessionName = Session["SessionName"].ToString();
        ////            fileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + sessionName + ".jpg";

        ////            using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
        ////            {
        ////                using (BinaryWriter bw = new BinaryWriter(fs))
        ////                {
        ////                    var data = Convert.FromBase64String(base64Std);
        ////                    bw.Write(data);
        ////                    bw.Close();
        ////                }
        ////            }
        ////        }

        ////        cmd.Parameters.AddWithValue("@PhotoPath", filePath + fileName);
        ////        cmd.Parameters.AddWithValue("@PhotoName", fileName);
        ////        cmd.Parameters.AddWithValue("@phCertificateNo", txtCertificateNo.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@phIssuedBy", txtIssuedBy.Text.Trim());

        ////        cmd.Parameters.AddWithValue("@PrePhoneNo", txtPrePhoneNo.Text);
        ////        cmd.Parameters.AddWithValue("@PreMobileNo", txtPreMobileNo.Text);
        ////        cmd.Parameters.AddWithValue("@PerPhoneNo", txtPerPhoneNo.Text);
        ////        cmd.Parameters.AddWithValue("@PerMobileNo", txtPerMobileNo.Text);
        ////        cmd.Parameters.AddWithValue("@MotherTongue", txtMotherTongue.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@HomeTown", txtHomeTown.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@AgeOnDate", txtAgeOnDate.Text.Trim());

        ////        cmd.Parameters.AddWithValue("@AadharNo", txtAadharNo.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@AadharIssueDate", txtAadharIssueDate.Text.Trim());

        ////        var smsAcknowledgment = "";
        ////        if (chkStMobile.Checked)
        ////        {
        ////            if (txtMobile.Text != string.Empty)
        ////            {
        ////                if (smsAcknowledgment == string.Empty)
        ////                {
        ////                    smsAcknowledgment = txtMobile.Text.Trim();
        ////                }
        ////                else
        ////                {
        ////                    smsAcknowledgment = smsAcknowledgment + "," + txtMobile.Text.Trim();
        ////                }
        ////            }
        ////        }

        ////        if (smsAcknowledgment == string.Empty)
        ////        {
        ////            smsAcknowledgment = null;
        ////        }

        ////        cmd.Parameters.AddWithValue("@smsAcknowledgment", smsAcknowledgment);


        ////        var emailAcknowledgment = "";
        ////        if (chkStEmail.Checked)
        ////        {
        ////            if (txtEmail.Text != string.Empty)
        ////            {

        ////                if (emailAcknowledgment == string.Empty)
        ////                {
        ////                    emailAcknowledgment = txtEmail.Text.Trim();
        ////                }
        ////                else
        ////                {
        ////                    emailAcknowledgment = emailAcknowledgment + "," + txtEmail.Text.Trim();
        ////                }
        ////            }
        ////        }

        ////        if (emailAcknowledgment == string.Empty)
        ////        {
        ////            emailAcknowledgment = null;
        ////        }

        ////        cmd.Parameters.AddWithValue("@emailAcknowledgment", emailAcknowledgment);
        ////        cmd.Parameters.AddWithValue("@isSmsAck", chkStMobile.Checked);
        ////        cmd.Parameters.AddWithValue("@isEmailAck", chkStEmail.Checked);

        ////        try
        ////        {
        ////            _con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            _con.Close();
        ////        }
        ////        catch (SqlException ) { _recordNotInsertGeneral = false; flag = false; _con.Close(); }
        ////        catch (Exception ) { _recordNotInsertGeneral = false; flag = false; _con.Close(); }
        ////        return flag;
        ////    }
        ////}
        ////public bool FamilyDetails()
        ////{
        ////    var flag = true;
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        _redorcNotInsertfamilDetails = true;
        ////        if (drpOccupationfa.SelectedItem.Text == "<--Select-->" || drpOccupationmoth.SelectedItem.Text == "<--Select-->")
        ////        {
        ////            _oo.MessageBox("Must Select Condition :<--Select-->:", Page);
        ////            return false;
        ////        }
        ////        // ReSharper disable once RedundantIfElseBlock
        ////        else
        ////        {
        ////            cmd.CommandText = "StudentFamilyDetailsProc";
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            cmd.Connection = _con;
        ////            cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
        ////            cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
        ////            cmd.Parameters.AddWithValue("@FatherName", txtfaNameee.Text);
        ////            cmd.Parameters.AddWithValue("@FatherOccupation", drpOccupationfa.Text);
        ////            cmd.Parameters.AddWithValue("@FatherDesignation", txtdesfa.Text);
        ////            cmd.Parameters.AddWithValue("@FatherQualification", txtqufa.Text);
        ////            cmd.Parameters.AddWithValue("@FatherIncomeMonthly", txtincomefa.Text);
        ////            cmd.Parameters.AddWithValue("@FatherOfficeAddress", txtoffaddfa.Text);
        ////            cmd.Parameters.AddWithValue("@FatherContactNo", txtcontfa.Text);
        ////            cmd.Parameters.AddWithValue("@FatherEmail", txtemailfather.Text);
        ////            cmd.Parameters.AddWithValue("@FamilyIncomeMonthly", txtfailyincome.Text);
        ////            cmd.Parameters.AddWithValue("@FamilyRelationship", DrpRelationship.SelectedItem.ToString());
        ////            cmd.Parameters.AddWithValue("@FamilyEmail", txtemailfamily.Text);
        ////            cmd.Parameters.AddWithValue("@FamilyGuardianName", txtguardianname.Text);
        ////            cmd.Parameters.AddWithValue("@FamilyContactNo", txtcontactNo.Text);
        ////            cmd.Parameters.AddWithValue("@MotherName", txtmotherNameeee.Text);
        ////            cmd.Parameters.AddWithValue("@MotherOccupation", drpOccupationmoth.Text);
        ////            cmd.Parameters.AddWithValue("@MotherDesignation", txtdesmoth.Text);
        ////            cmd.Parameters.AddWithValue("@MotherQualification", txtqualimother.Text);
        ////            cmd.Parameters.AddWithValue("@MotherIncomeMonthly", txtincomemonthlymother.Text);
        ////            cmd.Parameters.AddWithValue("@MotherOfficeAddress", txtofficeaddmother.Text);
        ////            cmd.Parameters.AddWithValue("@MotherContactNo", txtmothercontact.Text);
        ////            cmd.Parameters.AddWithValue("@MotherEmail", txtmotheremail.Text);
        ////            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        ////            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        ////            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        ////            if (txtGuardiantwoIncomeMonthly.Text == string.Empty)
        ////            {
        ////                cmd.Parameters.AddWithValue("@GuardiantwoIncomeMonthly", DBNull.Value);
        ////            }
        ////            else
        ////            {
        ////                cmd.Parameters.AddWithValue("@GuardiantwoIncomeMonthly", txtGuardiantwoIncomeMonthly.Text);
        ////            }
        ////            cmd.Parameters.AddWithValue("@GuardiantwoName", txtGuardiantwoName.Text);
        ////            if (txtGuardiantwoContact.Text == string.Empty)
        ////            {
        ////                cmd.Parameters.AddWithValue("@GuardiantwoContact", DBNull.Value);
        ////            }
        ////            else
        ////            {
        ////                cmd.Parameters.AddWithValue("@GuardiantwoContact", txtGuardiantwoContact.Text);
        ////            }
        ////            if (drpGuardiantwoRelationship.SelectedIndex == 0)
        ////            {
        ////                cmd.Parameters.AddWithValue("@GuardiantwoRelationship", DBNull.Value);
        ////            }
        ////            else
        ////            {
        ////                cmd.Parameters.AddWithValue("@GuardiantwoRelationship", drpGuardiantwoRelationship.Text);
        ////            }
        ////            cmd.Parameters.AddWithValue("@GuardiantwoEmail", txtGuardiantwoEmail.Text);
        ////            //string FatherImagePath = "";
        ////            //string FatherImageFileName = "";
        ////            //if (FileUpload2.HasFile)
        ////            //{
        ////            //    FatherImagePath = @"../Uploads/FatherPhoto/";
        ////            //    string fileExtention = Path.GetExtension(FileUpload2.PostedFile.FileName);
        ////            //    string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
        ////            //    FatherImageFileName = Session["SrNo"].ToString().Trim() + '_' + "FatherImage" + '_' + datetime + fileExtention;
        ////            //    FileUpload2.SaveAs(Server.MapPath(FatherImagePath + FatherImageFileName));
        ////            //}

        ////            var fatherImagePath = "";
        ////            var fatherImageFileName = "";

        ////            var base64Father = hdfatherPhoto.Value;
        ////            if (base64Father != string.Empty)
        ////            {
        ////                fatherImagePath = @"../Uploads/FatherPhoto/";
        ////                var sessionName = Session["SessionName"].ToString();
        ////                fatherImageFileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + "FatherImage" + '_' + sessionName + ".jpg";
        ////                using (FileStream fs = new FileStream(Server.MapPath((fatherImagePath + fatherImageFileName)), FileMode.Create))
        ////                {
        ////                    using (BinaryWriter bw = new BinaryWriter(fs))
        ////                    {
        ////                        var data = Convert.FromBase64String(base64Father);
        ////                        bw.Write(data);
        ////                        bw.Close();
        ////                    }
        ////                }
        ////                fatherImagePath = fatherImagePath + fatherImageFileName;
        ////            }
        ////            cmd.Parameters.AddWithValue("@FatherPhotoPath", fatherImagePath + fatherImageFileName);
        ////            cmd.Parameters.AddWithValue("@FatherPhotoName", fatherImageFileName);
        ////            //string MotherImagePath = "";
        ////            //string MotherImageFileName = "";
        ////            //if (FileUpload3.HasFile)
        ////            //{
        ////            //    MotherImagePath = @"../Uploads/MotherPhoto/";
        ////            //    string fileExtention = Path.GetExtension(FileUpload3.PostedFile.FileName);
        ////            //    string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
        ////            //    MotherImageFileName = Session["SrNo"].ToString().Trim() + '_' + "MotherImage" + '_' + datetime + fileExtention;
        ////            //    FileUpload3.SaveAs(Server.MapPath(MotherImagePath + MotherImageFileName));
        ////            //}

        ////            var motherImagePath = "";
        ////            var motherImageFileName = "";
        ////            var base64Mother = hdmotherPhoto.Value;
        ////            if (base64Mother != string.Empty)
        ////            {
        ////                motherImagePath = @"../Uploads/MotherPhoto/";
        ////                var sessionName = Session["SessionName"].ToString();
        ////                motherImageFileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + "MotherImage" + '_' + sessionName + ".jpg";

        ////                using (FileStream fs = new FileStream(Server.MapPath((motherImagePath + motherImageFileName)), FileMode.Create))
        ////                {
        ////                    using (BinaryWriter bw = new BinaryWriter(fs))
        ////                    {
        ////                        var data = Convert.FromBase64String(base64Mother);
        ////                        bw.Write(data);
        ////                        bw.Close();
        ////                    }
        ////                }
        ////                motherImagePath = motherImagePath + motherImageFileName;
        ////            }
        ////            cmd.Parameters.AddWithValue("@MotherPhotoPath", motherImagePath + motherImageFileName);
        ////            cmd.Parameters.AddWithValue("@MotherPhotoName", motherImageFileName);
        ////            cmd.Parameters.AddWithValue("@FatherOfficePhoneNo", txtFatherOfficePhoneNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@FatherOfficeMobileNo", txtFatherOfficeMobileNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@FatherOfficeEmail", txtFatherOfficeEmail.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@MotherOfficePhoneNo", txtMotherOfficePhoneNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@MotherOfficeMobileNo", txtMotherOfficeMobileNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@MotherOfficeEmail", txtMotherOfficeEmail.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@ParentTotalIncome", txtParentTotalIncome.Text.Trim());

        ////            //string GroupImagePath = "";
        ////            //string GroupImageFileName = "";
        ////            //if (FileUpload3.HasFile)
        ////            //{
        ////            //    GroupImagePath = @"../Uploads/GroupPhoto/";
        ////            //    string fileExtention = Path.GetExtension(FileUpload3.PostedFile.FileName);
        ////            //    string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
        ////            //    GroupImageFileName = Session["SrNo"].ToString().Trim() + '_' + "GroupImage" + '_' + datetime + fileExtention;
        ////            //    FileUpload3.SaveAs(Server.MapPath(GroupImagePath + GroupImageFileName));
        ////            //}

        ////            var groupImagePath = "";
        ////            var groupImageFileName = "";
        ////            var base64GroupPhoto = hdbase64groupPhoto.Value;
        ////            if (base64GroupPhoto != string.Empty)
        ////            {
        ////                groupImagePath = @"../Uploads/GroupPhoto/";
        ////                var sessionName = Session["SessionName"].ToString();
        ////                groupImageFileName = Session["SrNo"].ToString().Replace('/', '-') + '_' + "GroupImage" + '_' + sessionName + ".jpg";

        ////                using (FileStream fs = new FileStream(Server.MapPath((groupImagePath + groupImageFileName)), FileMode.Create))
        ////                {
        ////                    using (BinaryWriter bw = new BinaryWriter(fs))
        ////                    {
        ////                        var data = Convert.FromBase64String(base64GroupPhoto);
        ////                        bw.Write(data);
        ////                        bw.Close();
        ////                    }
        ////                }
        ////            }
        ////            cmd.Parameters.AddWithValue("@GroupPhotoPath", groupImagePath + groupImageFileName);
        ////            cmd.Parameters.AddWithValue("@GroupPhotoName", groupImageFileName);
        ////            cmd.Parameters.AddWithValue("@G1Address", txtG1Address.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G1State", drpG1State.SelectedValue.Trim());
        ////            cmd.Parameters.AddWithValue("@G1City", drpG1City.SelectedValue.Trim());
        ////            cmd.Parameters.AddWithValue("@G1PhoneNo", txtG1PhoneNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G1MobileNo", txtG1MobileNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G1Pin", txtG1Pin.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G2Address", txtG2Address.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G2State", drpG2State.SelectedValue.Trim());
        ////            cmd.Parameters.AddWithValue("@G2City", drpG2City.SelectedValue.Trim());
        ////            cmd.Parameters.AddWithValue("@G2PhoneNo", txtG2PhoneNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G2MobileNo", txtG2MobileNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@G2Pin", txtG2Pin.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@FatherNationality", txtFatherNationality.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@MotherNationality", txtMotherNationality.Text.Trim());

        ////            var smsAcknowledgment = "";
        ////            if (chkFaMobile.Checked)
        ////            {
        ////                if (txtcontfa.Text != string.Empty)
        ////                {
        ////                    if (smsAcknowledgment == string.Empty)
        ////                    {
        ////                        smsAcknowledgment = txtcontfa.Text.Trim();
        ////                    }
        ////                    else
        ////                    {
        ////                        smsAcknowledgment = smsAcknowledgment + "," + txtcontfa.Text.Trim();
        ////                    }
        ////                }
        ////            }
        ////            if (chkMoMobile.Checked)
        ////            {
        ////                if (txtmothercontact.Text != string.Empty)
        ////                {
        ////                    if (txtmothercontact.Text.Trim() != txtcontfa.Text.Trim())
        ////                    {
        ////                        if (smsAcknowledgment == string.Empty)
        ////                        {
        ////                            smsAcknowledgment = txtmothercontact.Text.Trim();
        ////                        }
        ////                        else
        ////                        {
        ////                            smsAcknowledgment = smsAcknowledgment + "," + txtmothercontact.Text.Trim();
        ////                        }
        ////                    }
        ////                }
        ////            }
        ////            if (chkGuaMobile.Checked)
        ////            {
        ////                if (txtcontactNo.Text != string.Empty)
        ////                {
        ////                    if (txtcontactNo.Text.Trim() != txtcontfa.Text.Trim())
        ////                    {
        ////                        if (smsAcknowledgment == string.Empty)
        ////                        {
        ////                            smsAcknowledgment = txtcontactNo.Text.Trim();
        ////                        }
        ////                        else
        ////                        {
        ////                            smsAcknowledgment = smsAcknowledgment + "," + txtcontactNo.Text.Trim();
        ////                        }
        ////                    }
        ////                }
        ////            }

        ////            if (smsAcknowledgment == string.Empty)
        ////            {
        ////                smsAcknowledgment = txtcontactNo.Text.Trim();
        ////            }

        ////            cmd.Parameters.AddWithValue("@smsAcknowledgment", smsAcknowledgment);


        ////            var emailAcknowledgment = "";
        ////            if (chkFaEmail.Checked)
        ////            {
        ////                if (txtemailfather.Text != string.Empty)
        ////                {
        ////                    if (emailAcknowledgment == string.Empty)
        ////                    {
        ////                        emailAcknowledgment = txtemailfather.Text.Trim();
        ////                    }
        ////                    else
        ////                    {
        ////                        emailAcknowledgment = emailAcknowledgment + "," + txtemailfather.Text.Trim();
        ////                    }
        ////                }
        ////            }
        ////            if (chkMoEmail.Checked)
        ////            {
        ////                if (txtmotheremail.Text != string.Empty)
        ////                {
        ////                    if (txtemailfather.Text.Trim().ToUpper() != txtmotheremail.Text.Trim().ToUpper())
        ////                    {
        ////                        if (emailAcknowledgment == string.Empty)
        ////                        {
        ////                            emailAcknowledgment = txtmotheremail.Text.Trim();
        ////                        }
        ////                        else
        ////                        {
        ////                            emailAcknowledgment = emailAcknowledgment + "," + txtmotheremail.Text.Trim();
        ////                        }
        ////                    }
        ////                }
        ////            }
        ////            if (chkGuaEmail.Checked)
        ////            {
        ////                if (txtemailfamily.Text != string.Empty)
        ////                {
        ////                    if (txtemailfamily.Text.Trim().ToUpper() != txtemailfather.Text.Trim().ToUpper())
        ////                    {
        ////                        if (emailAcknowledgment == string.Empty)
        ////                        {
        ////                            emailAcknowledgment = txtemailfamily.Text.Trim();
        ////                        }
        ////                        else
        ////                        {
        ////                            emailAcknowledgment = emailAcknowledgment + "," + txtemailfamily.Text.Trim();
        ////                        }
        ////                    }
        ////                }
        ////            }

        ////            if (emailAcknowledgment == string.Empty)
        ////            {
        ////                emailAcknowledgment = txtemailfamily.Text.Trim();
        ////            }

        ////            cmd.Parameters.AddWithValue("@emailAcknowledgment", emailAcknowledgment);

        ////            cmd.Parameters.AddWithValue("@isfaSmsAck", chkFaMobile.Checked);
        ////            cmd.Parameters.AddWithValue("@isfaEmailAck", chkFaEmail.Checked);
        ////            cmd.Parameters.AddWithValue("@ismoSmsAck", chkMoMobile.Checked);
        ////            cmd.Parameters.AddWithValue("@ismoEmailAck", chkMoEmail.Checked);
        ////            cmd.Parameters.AddWithValue("@isguaSmsAck", txtcontfa.Text != string.Empty);
        ////            cmd.Parameters.AddWithValue("@isguaEmailAck", txtemailfamily.Text != string.Empty);

        ////            try
        ////            {
        ////                _con.Open();
        ////                cmd.ExecuteNonQuery();
        ////                _con.Close();
        ////            }
        ////            catch (SqlException) { _redorcNotInsertfamilDetails = false; flag = false; _con.Close(); }
        ////            catch (Exception) { _redorcNotInsertfamilDetails = false; flag = false; _con.Close(); }
        ////            drpGuardiantwoRelationship.SelectedIndex = 1;
        ////            return flag;
        ////        }
        ////    }
        ////}
        ////public bool PreviousSchoolDetails()
        ////{
        ////    _recordNotInsertPrevious = true;
        ////    var flag = true;
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        cmd.CommandText = "StudentPreviousSchoolProc";
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Connection = _con;
        ////        for (var i = 0; i < rptPreviousEducation.Items.Count; i++)
        ////        {
        ////            var txtExam = (TextBox)rptPreviousEducation.Items[i].FindControl("txtExam");
        ////            var drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
        ////            var drpResult = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpResult");
        ////            var txtInstitute = (TextBox)rptPreviousEducation.Items[i].FindControl("txtInstitute");
        ////            var txtYop = (TextBox)rptPreviousEducation.Items[i].FindControl("txtYop");
        ////            // ReSharper disable once LocalVariableHidesMember
        ////            var drpMedium = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpMedium");
        ////            var txtSubject = (TextBox)rptPreviousEducation.Items[i].FindControl("txtSubject");
        ////            // ReSharper disable once LocalVariableHidesMember
        ////            var txtRollNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtRollNo");
        ////            // ReSharper disable once LocalVariableHidesMember
        ////            var txtCertificateNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtCertificateNo");
        ////            var txtMarksSheetNo = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMarksSheetNo");
        ////            var txtMm = (TextBox)rptPreviousEducation.Items[i].FindControl("txtMM");
        ////            var txtObtained = (TextBox)rptPreviousEducation.Items[i].FindControl("txtObtained");
        ////            var txtPer = (TextBox)rptPreviousEducation.Items[i].FindControl("txtPer");
        ////            if (txtExam.Text.Trim() == "") continue;
        ////            cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
        ////            cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
        ////            cmd.Parameters.AddWithValue("@Qualification", txtExam.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Board", drpBoard.SelectedItem.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Result", drpResult.SelectedValue.Trim());
        ////            cmd.Parameters.AddWithValue("@SchoolName", txtInstitute.Text.Trim() == "" ? DBNull.Value : (object)txtInstitute.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Yop", txtYop.Text.Trim() == "" ? DBNull.Value : (object)txtYop.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Medium", drpMedium.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Subjects", txtSubject.Text.Trim() == "" ? DBNull.Value : (object)txtSubject.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text.Trim() == "" ? DBNull.Value : (object)txtRollNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@CertificateNo", txtCertificateNo.Text.Trim() == "" ? DBNull.Value : (object)txtCertificateNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@MarksSheetNo", txtMarksSheetNo.Text.Trim() == "" ? DBNull.Value : (object)txtMarksSheetNo.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@MaxMarks", txtMm.Text.Trim() == "" ? DBNull.Value : (object)txtMm.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Marks", txtObtained.Text.Trim() == "" ? DBNull.Value : (object)txtObtained.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@Percentage", txtPer.Text.Trim() == "" ? DBNull.Value : (object)txtPer.Text.Trim());
        ////            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        ////            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        ////            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

        ////            try
        ////            {
        ////                _con.Open();
        ////                cmd.ExecuteNonQuery();
        ////                _con.Close();
        ////            }
        ////            catch (SqlException) { _recordNotInsertPrevious = false; flag = false; _con.Close(); }
        ////            catch (Exception) { _recordNotInsertPrevious = false; flag = false; _con.Close(); }
        ////        }
        ////        return flag;
        ////    }
        ////}
        ////public bool Document()
        ////{
        ////    var flag = true;
        ////    _recordNotInsertDocument = true;
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        cmd.CommandText = "StudentDocumentsProc";
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Connection = _con;
        ////        cmd.Parameters.AddWithValue("@StEnRCode", Session["StEnRCode"].ToString());
        ////        cmd.Parameters.AddWithValue("@SrNo", Session["SrNo"].ToString());
        ////        try
        ////        {
        ////            _oo.MessageBox("Only in JPEG,BMP,PNG,JPG format allowed", Page);
        ////        }
        ////        catch (Exception)
        ////        {
        ////            // ignored
        ////        }
        ////        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        ////        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        ////        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        ////        try
        ////        {
        ////            _con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            _con.Close();

        ////        }
        ////        catch (SqlException) { _recordNotInsertDocument = false; flag = false; _con.Close(); }
        ////        catch (Exception) { _recordNotInsertDocument = false; flag = false; _con.Close(); }
        ////        return flag;
        ////    }

        ////}
        ////public bool OfficialDetails()
        ////{
        ////    // ReSharper disable once RedundantAssignment
        ////    // ReSharper disable once TooWideLocalVariableScope
        ////    var co = 0;
        ////    var flag = true;
        ////    _recordNotInsertOfficial = true;
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        cmd.CommandText = "StudentOfficialDetailsProc";
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Connection = _con;
        ////        _sql = "select max(Id) as Total from StudentOfficialDetails";
        ////        _sql = _sql + " where BranchCode=" + Session["BranchCode"] + "";
        ////        var ss = _oo.ReturnTag(_sql, "Total");
        ////        int ss1;
        ////        try
        ////        {
        ////            ss1 = Convert.ToInt32(ss.Trim());
        ////        }
        ////        catch (Exception) { ss1 = 0; }
        ////        ss1 = ss1 + 1;
        ////        Application.Lock();
        ////        Session["StEnRCode"] = IdGeneration(ss1.ToString());
        ////        Application.UnLock();
        ////        Application.Lock();
        ////        Session["SrNo"] = txtSr.Text;
        ////        Session["srno"] = txtSr.Text;
        ////        Application.UnLock();
        ////        var stEnrCode = IdGeneration(ss1.ToString());
        ////        cmd.Parameters.AddWithValue("@StEnRCode", stEnrCode);
        ////        Session["StEnRCode"] = stEnrCode;
        ////        cmd.Parameters.AddWithValue("@SrNo", txtSr.Text);
        ////        var admissionDate = TextBox100.Text.Trim();
        ////        cmd.Parameters.AddWithValue("@DateOfAdmiission", admissionDate);
        ////        _sql = "select Id from ClassMaster where ClassName='" + DropAdmissionClass.SelectedItem + "'";
        ////        _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        ////        var classId = _oo.ReturnTag(_sql, "Id");
        ////        cmd.Parameters.AddWithValue("@AdmissionForClassId", classId);
        ////        _sql = "select Id from SectionMaster where SectionName='" + drpSection.SelectedItem + "'";
        ////        _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + classId;
        ////        var sectionId = _oo.ReturnTag(_sql, "Id");
        ////        cmd.Parameters.AddWithValue("@SectionId", sectionId);
        ////        cmd.Parameters.AddWithValue("@GroupNa", DropBranch.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@FileNo", txtfileno.Text);
        ////        cmd.Parameters.AddWithValue("@Reference", txtReferences.Text);
        ////        cmd.Parameters.AddWithValue("@Remark", txtrema.Text);
        ////        cmd.Parameters.AddWithValue("@Board", DrpBoard.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@TypeOFAdmision", DrpNEWOLSAdmission.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        ////        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        ////        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        ////        cmd.Parameters.AddWithValue("@medium", drpMedium.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@Card", drpPanelCardType.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@HostelRequired", rbHostel.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@TransportRequired", rbTransport.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@HouseName", DropDownList4.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@LibraryRequired", rbLibrary.SelectedItem.ToString());
        ////        cmd.Parameters.AddWithValue("@Enquiry", txtEnquiryNo.Text);
        ////        cmd.Parameters.AddWithValue("@BoardUniversityRollNo", txtUniversityBoardRollNo.Text);
        ////        cmd.Parameters.AddWithValue("@InstituteRollNo", txtSchoolcollegeRollno.Text);
        ////        cmd.Parameters.AddWithValue("@CardNo", txtCardNo.Text);
        ////        cmd.Parameters.AddWithValue("@MachineNo", "");
        ////        cmd.Parameters.AddWithValue("@Course", DropCourse.SelectedValue);
        ////        cmd.Parameters.AddWithValue("@Branch", DropBranch.SelectedValue);
        ////        cmd.Parameters.AddWithValue("@Scholarship", rbScholarship.SelectedItem.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@ModForHostel", drpHostalMOD.SelectedIndex != 0 ? drpHostalMOD.SelectedValue : "I");
        ////        cmd.Parameters.AddWithValue("@ModForTransport", drpTransportMOD.SelectedIndex != 0 ? drpTransportMOD.SelectedValue : "I");
        ////        cmd.Parameters.AddWithValue("@ModForLibrary", drpLibraryMOD.SelectedIndex != 0 ? drpLibraryMOD.SelectedValue : "I");
        ////        cmd.Parameters.AddWithValue("@MODForFeeDeposit", drpFeeDepositMOD.SelectedValue);
        ////        cmd.Parameters.AddWithValue("@SMSAcknowledgment", drpSMSAcknowledgmentTo.SelectedValue);
        ////        cmd.Parameters.AddWithValue("@EmailAcknowledgment", drpEmailAcknowledgmentTo.SelectedValue);
        ////        double admissionDoneAt;
        ////        double.TryParse(txtAddDoneat.Text, out admissionDoneAt);
        ////        cmd.Parameters.AddWithValue("@AdmissionDoneAt", admissionDoneAt);
        ////        txtDFA.Text = _oo.ReturnTag(_sql, "DFA");
        ////        txtCFA.Text = _oo.ReturnTag(_sql, "CFA");
        ////        txtCOFA.Text = _oo.ReturnTag(_sql, "COFA");
        ////        txtSFA.Text = _oo.ReturnTag(_sql, "SFA");
        ////        if (DropStream.SelectedIndex != 0)
        ////        {
        ////            cmd.Parameters.AddWithValue("@Streamid", DropStream.SelectedValue);
        ////        }
        ////        if (RadioButtonList3.SelectedIndex == 0 || RadioButtonList3.SelectedIndex == 1)
        ////        {
        ////            var typeofedu = RadioButtonList3.SelectedIndex == 0 ? "R" : "P";
        ////            cmd.Parameters.AddWithValue("@TypeofEducation", typeofedu);
        ////        }
        ////        try
        ////        {
        ////            _con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            _con.Close();

        ////            _sql = "select id  from StudentOfficialDetails";
        ////            _sql = _sql + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        ////            try
        ////            {
        ////                co = Convert.ToInt32(_oo.ReturnTag(_sql, "id"));
        ////            }
        ////            catch (Exception) { co = 1; }
        ////            co = co + 1;
        ////            // ReSharper disable once RedundantAssignment
        ////            ss = IdGeneration(co.ToString());
        ////            rbHostel.SelectedIndex = 1;
        ////            drpHostalMOD.Style.Add("display", "none");
        ////            rbTransport.SelectedIndex = 1;
        ////            drpTransportMOD.Style.Add("display", "none");
        ////            rbLibrary.SelectedIndex = 1;
        ////            drpLibraryMOD.Style.Add("display", "none");
        ////        }
        ////        catch (SqlException ) { _recordNotInsertOfficial = false; flag = false; _con.Close(); }
        ////        catch (Exception) { _recordNotInsertOfficial = false; flag = false; _con.Close(); }
        ////        return flag;
        ////    }
        ////}
        ////protected void LinkButton1_Click(object sender, EventArgs e)
        ////{
        ////    _sql = "select StudentName,MiddleName,LastName,FatherName,Class,Sex,FatherContactNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text + "' and SessionName='" + Session["SessionName"] + "'";
        ////    txtFirstNa.Text = _oo.ReturnTag(_sql, "StudentName");
        ////    txtMidNa.Text = _oo.ReturnTag(_sql, "MiddleName");
        ////    txtlast.Text = _oo.ReturnTag(_sql, "LastName");
        ////    txtfaNameee.Text = _oo.ReturnTag(_sql, "FatherName");
        ////    txtcontfa.Text = _oo.ReturnTag(_sql, "FatherContactNo");
        ////    txtcontactNo.Text = _oo.ReturnTag(_sql, "FatherContactNo");
        ////    txtguardianname.Text = _oo.ReturnTag(_sql, "FatherName");
        ////    DropAdmissionClass.SelectedItem.Text = _oo.ReturnTag(_sql, "Class");
        ////    if (_oo.ReturnTag(_sql, "Sex").Trim().ToUpper() == "Male".ToUpper())
        ////    {
        ////        RadioButtonList1.SelectedIndex = 0;
        ////    }
        ////    else if (_oo.ReturnTag(_sql, "Sex").Trim().ToUpper() == "Female".ToUpper())
        ////    {
        ////        RadioButtonList1.SelectedIndex = 1;
        ////    }
        ////    else if (_oo.ReturnTag(_sql, "Sex").Trim().ToUpper() == "Transgender".ToUpper())
        ////    {
        ////        RadioButtonList1.SelectedIndex = 2;
        ////    }
        ////}
        ////public string IdGeneration(string x)
        ////{
        ////    var xx = "";
        ////    switch (x.Length)
        ////    {
        ////        case 1:
        ////            xx = "eAM00000" + x;
        ////            break;
        ////        case 2:
        ////            xx = "eAM0000" + x;
        ////            break;
        ////        case 3:
        ////            xx = "eAM000" + x;
        ////            break;
        ////        case 4:
        ////            xx = "eAM00" + x;
        ////            break;
        ////        case 5:
        ////            xx = xx + "eAM0" + x;
        ////            break;
        ////        default:
        ////            xx = x;
        ////            break;
        ////    }
        ////    return xx;
        ////}
        ////public string IdGenerationCollege(string x)
        ////{
        ////    var xx = "";
        ////    switch (x.Length)
        ////    {
        ////        case 1:
        ////            xx = "eAM000000" + x;
        ////            break;
        ////        case 2:
        ////            xx = "eAM00000" + x;
        ////            break;
        ////        case 3:
        ////            xx = "eAM0000" + x;
        ////            break;
        ////        case 4:
        ////            xx = "eAM000" + x;
        ////            break;
        ////        case 5:
        ////            xx = xx + "eAM00" + x;
        ////            break;
        ////        case 6:
        ////            xx = xx + "eAM0" + x;
        ////            break;
        ////        default:
        ////            xx = x;
        ////            break;
        ////    }
        ////    return xx;
        ////}

        ////protected void DrpPreState_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    LoadCity(DrpPreCity, DrpPreState);
        ////}
        ////protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    LoadCity(DrpPerCity, DrpPerState);
        ////}
        //////protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //////{
        //////    loadState(DrpPerState);
        //////    loadCity(DrpPerCity, DrpPreState);
        //////    try
        //////    {
        //////        if (CheckBox1.Items[0].Selected == true)
        //////        {
        //////            txtPerAdd.Text = txtPreaddress.Text;
        //////            DrpPerState.SelectedValue = DrpPreState.SelectedValue;
        //////            DrpPerCity.SelectedValue = DrpPreCity.SelectedValue;
        //////            txtPerZip.Text = txtPreZip.Text;
        //////            txtPerMobileNo.Text = txtPreMobileNo.Text;
        //////            txtPerPhoneNo.Text = txtPrePhoneNo.Text;
        //////        }
        //////        else
        //////        {

        //////            txtPerAdd.Text = "";
        //////            txtPerZip.Text = "";
        //////            txtPerMobileNo.Text = "";
        //////            txtPerPhoneNo.Text = "";
        //////            loaddefaultState(DrpPerState);
        //////            loaddefaultCity(DrpPerCity, DrpPerState);
        //////        }
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox1);
        //////    }

        //////}
        ////protected void RadioButtonList8_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    Panel2.Visible = RadioButtonList8.SelectedItem.Text == "Yes";
        ////}
        ////private void LoadSection()
        ////{
        ////    _sql = "select SectionName from SectionMaster where ClassNameId='" + DropAdmissionClass.SelectedValue + "'";
        ////    _sql = _sql + "  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        ////    _oo.FillDropDown(_sql, drpSection, "SectionName");
        ////}
        ////protected void DropAdmissionClass_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    LoadBranch();
        ////    LoadSection();
        ////    Get_DocumentName();
        ////}
        ////public bool Validation()
        ////{
        ////    return true;
        ////}
        ////protected void LinkButton14_Click(object sender, EventArgs e)
        ////{
        ////    // ReSharper disable once TooWideLocalVariableScope
        ////    // ReSharper disable once RedundantAssignment
        ////    var co = 0;
        ////    // ReSharper disable once NotAccessedVariable
        ////    var ss = "";
        ////    var sql1 = "Select SrNo from StudentOfficialDetails where SrNo='" + txtSr.Text + "'";
        ////    var sql2 = "Select SrNo from StudentGenaralDetail where SrNo='" + txtSr.Text + "'";
        ////    var sql3 = "Select SrNo from StudentFamilyDetails where SrNo='" + txtSr.Text + "'";
        ////    var sql4 = "Select SrNo from StudentPreviousSchool where SrNo='" + txtSr.Text + "'";
        ////    var sql5 = "Select SrNo from StudentDocs where SrNo='" + txtSr.Text + "'";
        ////    if (_oo.Duplicate(sql1) || _oo.Duplicate(sql2) || _oo.Duplicate(sql3) || _oo.Duplicate(sql4) || _oo.Duplicate(sql5))
        ////    {
        ////        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate S.R.No.!", "A");
        ////        //oo.MessageBoxforUpdatePanel("Duplicate S.R.No.!", LinkButton14);
        ////    }
        ////    else if (Validation())
        ////    {
        ////        if (txtSr.Text != string.Empty)
        ////        {
        ////            try
        ////            {
        ////                txtSr.BorderColor = ColorTranslator.FromHtml("#D5D5D5");
        ////            }
        ////            catch
        ////            {
        ////                // ignored
        ////            }
        ////            TextTrnsform();
        ////            OfficialDetails();
        ////            GeneralDetails();
        ////            FamilyDetails();
        ////            PreviousSchoolDetails();
        ////            if (_recordNotInsertGeneral == false || _recordNotInsertOfficial == false || _recordNotInsertPrevious == false || _redorcNotInsertfamilDetails == false)
        ////            {
        ////                _sql = "delete from StudentFamilyDetails where StEnRCode='" + Session["StEnRCode"] + "'";
        ////                _oo.ProcedureDatabase(_sql);
        ////                _sql = "delete from StudentGenaralDetail where StEnRCode='" + Session["StEnRCode"] + "'";
        ////                _oo.ProcedureDatabase(_sql);
        ////                _sql = "delete from StudentOfficialDetails where StEnRCode='" + Session["StEnRCode"] + "'";
        ////                _oo.ProcedureDatabase(_sql);
        ////                _sql = "delete from StudentPreviousSchool where StEnRCode='" + Session["StEnRCode"] + "'";
        ////                _oo.ProcedureDatabase(_sql);
        ////                _sql = "delete from StudentDocs where StEnRCode='" + Session["StEnRCode"] + "'";
        ////                _oo.ProcedureDatabase(_sql);
        ////                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Sorry record not inserted (Contact to Admin).", "W");
        ////                //oo.MessageBoxforUpdatePanel("Sorry record not inserted (Contact to Admin)", LinkButton14);
        ////            }
        ////            else
        ////            {                
        ////                SaveOptionalSubjectRecord();
        ////                NewDocumentsDetails(LinkButton14);
        ////                SaveStudentOtherDetails();
        ////                StudentPasswordGeneration();
        ////                GuardianPasswordGeneration();
        ////                SetEntranceExamRecord();
        ////                try
        ////                {

        ////                    _sql = "select MobileNumber from StudentGenaralDetail  where SrNo='" + txtSr.Text.Trim() + "'  and SessionName='" + Session["SessionName"] + "'";
        ////                    var mobileNumber = _oo.ReturnTag(_sql, "MobileNumber");
        ////                    _sql = "select FamilyContactNo from StudentFamilyDetails  where SrNo='" + txtSr.Text.Trim() + "'  and SessionName='" + Session["SessionName"] + "'";
        ////                    var familyContactNo = _oo.ReturnTag(_sql, "FamilyContactNo");
        ////                    SendFeeSms(mobileNumber, familyContactNo);
        ////                }
        ////                catch (Exception)
        ////                {
        ////                    // ignored
        ////                }
        ////                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
        ////                //oo.MessageBoxforUpdatePanel("Submitted successfully.", LinkButton14);
        ////                var srnoo = txtSr.Text.Trim().Replace("'", "");
        ////                Session["srNo"] = srnoo;
        ////                _oo.ClearControls(Page);
        ////                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../11/StudentRegView.aspx?print=1&id="+ srnoo + "','_blank')", true);

        ////                chkFaEmail.Checked = false;
        ////                chkFaMobile.Checked = false;
        ////                chkGuaEmail.Checked = false;
        ////                chkGuaMobile.Checked = false;
        ////                chkMoEmail.Checked = false;
        ////                chkMoMobile.Checked = false;
        ////                LoadEntranceExamName();
        ////                GeneralDetailDropDown();
        ////                FamilyDetailsDropDown();
        ////                OfficialDetailDropDown();
        ////                var getdate = DateTime.Today.ToString("dd-MMM-yyyy");
        ////                txtAgeOnDate.Text = getdate;
        ////                TextBox100.Text = getdate;
        ////                Get_DocumentName();
        ////            }

        ////            TextBox65.Text = "Indian";
        ////            _sql = "select id  from StudentOfficialDetails";
        ////            _sql = _sql + " where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        ////            try
        ////            {
        ////                co = Convert.ToInt32(_oo.ReturnTag(_sql, "id"));
        ////            }
        ////            catch (Exception) { co = 0; }
        ////            co = co + 1;
        ////            // ReSharper disable once RedundantAssignment
        ////            ss = IdGenerationCollege(co.ToString());
        ////        }
        ////    }
        ////    else
        ////    {
        ////        txtSr.Focus();
        ////        txtSr.BorderColor = Color.Red;
        ////    }
        ////}

        ////public void SaveOptionalSubjectRecord()
        ////{
        ////    try
        ////    {
        ////        if (txtSr.Text != string.Empty)
        ////        {
        ////            for (int i = 0; i < rbOptionalSubject.Items.Count; i++)
        ////            {
        ////                if (rbOptionalSubject.Items[i].Selected)
        ////                {
        ////                    var param = new List<SqlParameter>
        ////                    {
        ////                        new SqlParameter("@Srno", txtSr.Text.Trim()),
        ////                        new SqlParameter("@OptSubjectId", rbOptionalSubject.Items[i].Value),
        ////                        new SqlParameter("@SessionName", Session["SessionName"].ToString()),
        ////                        new SqlParameter("@LoginName", Session["LoginName"].ToString()),
        ////                        new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
        ////                        new SqlParameter("@QueryFor", "I")
        ////                    };
        ////                    DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOptionalSubjectProc", param);
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _oo.MessageBoxforUpdatePanel(ex.Message, Page);
        ////    }
        ////}

        ////public void SendFeeSms(string mobileno, string familyConNo)
        ////{
        ////    if (mobileno != "")
        ////    {
        ////        SendFeesSmsStudent(mobileno);
        ////        SendFeesSmsGuardian(familyConNo);
        ////    }
        ////    else
        ////    {
        ////        SendFeesSmsStudent(familyConNo);
        ////        SendFeesSmsGuardian(familyConNo);
        ////    }
        ////}
        ////public void SendFeesSmsStudent(string fmobileNo)
        ////{
        ////    _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        ////    if (_oo.ReturnTag(_sql, "HitValue") != "")
        ////    {
        ////        if (_oo.ReturnTag(_sql, "HitValue") == "true")
        ////        {
        ////            var sadpNew = new SMSAdapterNew();
        ////            // ReSharper disable once RedundantAssignment
        ////            var mess = "";
        ////            // ReSharper disable once NotAccessedVariable
        ////            var collegeTitle = "";
        ////            _sql = "Select FirstName as StudentName   from StudentGenaralDetail";
        ////            _sql = _sql + "    where  Srno='" + txtSr.Text.Trim() + "'";
        ////            var studentName = _oo.ReturnTag(_sql, "StudentName");
        ////            _sql = "Select CollegeShortNa from CollegeMaster ";
        ////            var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");
        ////            _sql = "Select UserName,Password StudentPassword from StudentLoginandPassword where";
        ////            _sql = _sql + " Srno='" + txtSr.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "'";
        ////            var stEnRCode = _oo.ReturnTag(_sql, "UserName");
        ////            var studentPassword = _oo.ReturnTag(_sql, "StudentPassword");
        ////            mess = "Congrats! " + studentName + ", you've registered successfully with " + collegeShortNa + ". Your Userid: " + stEnRCode + " and Password: " + studentPassword + "";
        ////            // ReSharper disable once InconsistentNaming
        ////            // ReSharper disable once NotAccessedVariable
        ////            var sms_response = "";
        ////            _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        ////            // ReSharper disable once RedundantAssignment
        ////            collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");
        ////            if (fmobileNo != "")
        ////            {
        ////                _sql = "Select SmsSent From SmsEmailMaster where Id='9'";
        ////                if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
        ////                {
        ////                    // ReSharper disable once RedundantAssignment
        ////                    sms_response = sadpNew.Send(mess, fmobileNo);

        ////                }
        ////            }
        ////        }
        ////    }
        ////}
        ////public void SendFeesSmsGuardian(string fmobileNo)
        ////{
        ////    _sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        ////    if (_oo.ReturnTag(_sql, "HitValue") != "")
        ////    {
        ////        if (_oo.ReturnTag(_sql, "HitValue") == "true")
        ////        {
        ////            var sadpNew = new SMSAdapterNew();
        ////            // ReSharper disable once RedundantAssignment
        ////            var mess = "";
        ////            // ReSharper disable once NotAccessedVariable
        ////            var collegeTitle = "";
        ////            _sql = "Select FamilyGuardianName as GuardianName from StudentFamilyDetails";
        ////            _sql = _sql + " where Srno='" + txtSr.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "'";
        ////            var guardianName = _oo.ReturnTag(_sql, "GuardianName");
        ////            _sql = "Select CollegeShortNa from CollegeMaster ";
        ////            var collegeShortNa = _oo.ReturnTag(_sql, "CollegeShortNa");
        ////            _sql = "Select UserName,Password GuardianPassword from GuardianLoginandPassword where";
        ////            _sql = _sql + " Srno='" + txtSr.Text.Trim() + "' and SessionName='" + Session["SessionName"] + "'";
        ////            var stEnRCode = _oo.ReturnTag(_sql, "UserName");
        ////            var guardianPassword = _oo.ReturnTag(_sql, "GuardianPassword");
        ////            mess = "Congrats! Mr./Ms. " + guardianName + ", your ward is registered successfully with " + collegeShortNa + ". Your Userid: " + stEnRCode + " and Password: " + guardianPassword + "";
        ////            // ReSharper disable once NotAccessedVariable
        ////            // ReSharper disable once InconsistentNaming
        ////            var sms_response = "";

        ////            _sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        ////            // ReSharper disable once RedundantAssignment
        ////            collegeTitle = _oo.ReturnTag(_sql, "CollegeShortNa");
        ////            if (fmobileNo != "")
        ////            {
        ////                _sql = "Select SmsSent From SmsEmailMaster where Id='9'";
        ////                if (_oo.ReturnTag(_sql, "SmsSent").Trim() == "true")
        ////                {
        ////                    // ReSharper disable once RedundantAssignment
        ////                    sms_response = sadpNew.Send(mess, fmobileNo);
        ////                }
        ////            }
        ////        }
        ////    }
        ////}

        ////protected void txtemailfather_TextChanged(object sender, EventArgs e)
        ////{
        ////    txtemailfamily.Text = txtemailfather.Text;
        ////}
        ////protected void txtfaNameee_TextChanged(object sender, EventArgs e)
        ////{
        ////    txtguardianname.Text = txtfaNameee.Text;
        ////}
        ////protected void drpOccupationmoth_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    var ss = drpOccupationmoth.SelectedItem.ToString().ToUpper();
        ////    var wf = "HOUSE WIFE";
        ////    if (ss.Trim() == wf.Trim())
        ////    {
        ////        txtdesmoth.Text = "N/A";
        ////        txtdesmoth.ReadOnly = true;
        ////        txtofficeaddmother.Text = "N/A";
        ////        txtofficeaddmother.ReadOnly = true;
        ////    }
        ////    else
        ////    {

        ////        txtdesmoth.Text = "";
        ////        txtdesmoth.ReadOnly = false;
        ////        txtofficeaddmother.Text = "";
        ////        txtofficeaddmother.ReadOnly = false;
        ////    }
        ////}

        ////public void StudentPasswordGeneration()
        ////{
        ////    var password = _oo.GetPassword();
        ////    _sql = "Select '"+ txtSr.Text.Trim() +"' UserName,(select cast((Abs(Checksum(NewId()))%10) as varchar(1)) + ";
        ////    _sql = _sql + " char(ascii('a')+(Abs(Checksum(NewId()))%25)) +";
        ////    _sql = _sql + " char(ascii('A')+(Abs(Checksum(NewId()))%25)) +";
        ////    _sql = _sql + " left(newid(),5)) Password";
        ////    var uid = _oo.ReturnTag(_sql, "UserName");
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        cmd.CommandText = "LoginTabProc";
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Connection = _con;
        ////        cmd.Parameters.AddWithValue("@LoginName", uid);
        ////        cmd.Parameters.AddWithValue("@Pass", password);
        ////        cmd.Parameters.AddWithValue("@LoginTypeId", 4);
        ////        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        ////        cmd.Parameters.AddWithValue("@Login", Session["LoginName"].ToString());
        ////        cmd.Parameters.AddWithValue("@Srno", txtSr.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@SessionId", Session["SessionID"].ToString());
        ////        cmd.Parameters.AddWithValue("@BranchId", Session["BranchCode"].ToString());
        ////        try
        ////        {
        ////            _con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            _con.Close();

        ////            SendNotificationToStudent(txtSr.Text.Trim(), uid, password);
        ////        }
        ////        catch (SqlException) { _con.Close(); }
        ////        catch (Exception) { _con.Close(); }
        ////    }
        ////}


        ////public void GuardianPasswordGeneration()
        ////{
        ////    var password = _oo.GetPassword();
        ////    _sql = "Select 'G'+'" + txtSr.Text.Trim() + "' UserName,(select cast((Abs(Checksum(NewId()))%10) as varchar(1)) + ";
        ////    _sql = _sql + " char(ascii('a')+(Abs(Checksum(NewId()))%25)) +";
        ////    _sql = _sql + " char(ascii('A')+(Abs(Checksum(NewId()))%25)) +";
        ////    _sql = _sql + " left(newid(),5)) Password";
        ////    var uid = _oo.ReturnTag(_sql, "UserName");
        ////    using (var cmd = new SqlCommand())
        ////    {
        ////        cmd.CommandText = "LoginTabProc";
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Connection = _con;
        ////        cmd.Parameters.AddWithValue("@LoginName", uid);
        ////        cmd.Parameters.AddWithValue("@Pass", password);
        ////        cmd.Parameters.AddWithValue("@Srno", txtSr.Text.Trim());
        ////        cmd.Parameters.AddWithValue("@LoginTypeId", 5);
        ////        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        ////        cmd.Parameters.AddWithValue("@Login", Session["LoginName"].ToString());
        ////        cmd.Parameters.AddWithValue("@SessionId", Session["SessionID"].ToString());
        ////        cmd.Parameters.AddWithValue("@BranchId", Session["BranchCode"].ToString());
        ////        try
        ////        {
        ////            _con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            _con.Close();
        ////            SendNotificationToGurdian(txtSr.Text.Trim(), uid, password);
        ////        }
        ////        catch (SqlException) { _con.Close(); }
        ////        catch (Exception) { _con.Close(); }
        ////    }
        ////}
        ////public void SendNotificationToStudent(string srno,string uid, string password)
        ////{
        ////    var mess = "Student Login Panel";
        ////    mess = mess + "<br>";
        ////    mess = mess + "</hr>";
        ////    mess = mess + "<br>";
        ////    mess = mess + "Dear ";
        ////    mess = mess + "             " + txtFirstNa.Text + " " + txtMidNa.Text + " " + txtlast.Text + ",";
        ////    mess = mess + "<br></hr>";
        ////    mess = mess + "Your User ID  :" + uid;
        ////    mess = mess + "<Br>";
        ////    mess = mess + "Your Password :" + password;
        ////    _sql = "select email  from StudentGenaralDetail where srno='" + srno + "'";
        ////    try
        ////    {
        ////        _oo.EmailSendingForUser(mess, "SE Education: Student Login Panel Credentials", _oo.ReturnTag(_sql, "Email"));
        ////    }
        ////    catch (Exception)
        ////    {
        ////        // ignored
        ////    }
        ////}
        ////public void SendNotificationToGurdian(string srno, string uid, string password)
        ////{
        ////    var mess = "Guardian Login Panel";
        ////    mess = mess + "<br>";
        ////    mess = mess + "</hr>";
        ////    mess = mess + "<br>";
        ////    mess = mess + "Dear ";
        ////    mess = mess + "             " + txtfaNameee.Text + ",";
        ////    mess = mess + "<br></hr>";
        ////    mess = mess + "Your User ID  :" + uid;
        ////    mess = mess + "<Br>";
        ////    mess = mess + "Your Password :" + password;
        ////    _sql = "select FamilyEmail  from StudentFamilyDetails where srno='" + srno + "'";
        ////    try
        ////    {
        ////        _oo.EmailSendingForUser(mess, "SE Education: Guardian Login Panel Credentials", _oo.ReturnTag(_sql, "FamilyEmail"));
        ////    }
        ////    catch (Exception)
        ////    {
        ////        // ignored
        ////    }
        ////}
        ////public void PermissionGrant(int add1, LinkButton ladd)
        ////{
        ////    ladd.Enabled = add1 == 1;
        ////}
        ////public void CheckValueAddDeleteUpdate()
        ////{
        ////    try
        ////    {
        ////        _sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        ////        _sql = _sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
        ////        var a = Convert.ToInt32(_oo.ReturnTag(_sql, "add1"));
        ////        // ReSharper disable once RedundantCast
        ////        PermissionGrant(a, (LinkButton)LinkButton14);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        // ignored
        ////    }
        ////}
        ////protected void LinkButton15_Click(object sender, EventArgs e)
        ////{
        ////    Response.Redirect("StudentPreview.aspx");
        ////}
        ////protected void DrpDate_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    RadioButtonList1.Focus();
        ////}
        ////protected void DrpRelationship_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    switch (DrpRelationship.SelectedIndex)
        ////    {
        ////        case 0:
        ////            txtguardianname.Text = txtfaNameee.Text;
        ////            txtemailfamily.Text = txtemailfather.Text;
        ////            break;
        ////        case 1:
        ////            txtguardianname.Text = txtmotherNameeee.Text;
        ////            txtemailfamily.Text = txtmotheremail.Text;
        ////            break;
        ////        default:
        ////            txtguardianname.Text = "";
        ////            txtemailfamily.Text = "";
        ////            txtguardianname.Focus();
        ////            break;
        ////    }
        ////}
        ////protected void drpGuardiantwoRelationship_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    switch (drpGuardiantwoRelationship.SelectedIndex)
        ////    {
        ////        case 0:
        ////            txtGuardiantwoName.Text = txtfaNameee.Text;
        ////            txtGuardiantwoEmail.Text = txtemailfather.Text;
        ////            break;
        ////        case 1:
        ////            txtGuardiantwoName.Text = txtmotherNameeee.Text;
        ////            txtGuardiantwoEmail.Text = txtmotheremail.Text;
        ////            break;
        ////        default:
        ////            txtGuardiantwoName.Text = "";
        ////            txtGuardiantwoEmail.Text = "";
        ////            txtGuardiantwoName.Focus();
        ////            break;
        ////    }
        ////}
        ////#endregion

        ////private void LoadCourse()
        ////{
        ////    _sql = "Select CourseName,Id from CourseMaster where SessionName='" + Session["SessionName"] + "'";
        ////    _oo.FillDropDown_withValue(_sql, DropCourse, "CourseName", "Id");
        ////    DropCourse.Items.Insert(0, new ListItem("<--Select Course-->", "0"));

        ////}

        ////private void LoadBranch()
        ////{
        ////    _sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"] + "' and Course='" + DropCourse.SelectedValue + "' and ClassId='" + DropAdmissionClass.SelectedValue + "'";
        ////    _oo.FillDropDown_withValue(_sql, DropBranch, "BranchName", "Id");
        ////    DropBranch.Items.Insert(0, new ListItem("<--Select Branch-->", "0"));
        ////}

        ////private void LoadStream()
        ////{
        ////    _sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"] + "' and ClassId='" + DropAdmissionClass.SelectedValue + "' and BranchId='" + DropBranch.SelectedValue + "'";
        ////    _oo.FillDropDown_withValue_withSelect(_sql, DropStream, "Stream", "Id");
        ////}

        ////private void LoadOptionalSubject()
        ////{
        ////    _sql = "Select SubjectGroup,id from SubjectGroupMaster where Classid='" + DropAdmissionClass.SelectedValue + "' and Branchid='" + DropBranch.SelectedValue + "'  and SectionName='" + drpSection.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and IsCompulsory=0";
        ////    rbOptionalSubject.DataSource = _oo.GridFill(_sql);
        ////    rbOptionalSubject.DataTextField = "SubjectGroup";
        ////    rbOptionalSubject.DataValueField = "id";
        ////    rbOptionalSubject.DataBind();
        ////    divopt.Visible = rbOptionalSubject.Items.Count > 0;
        ////}

        ////private void LoadClass()
        ////{
        ////    _sql = "Select Id,ClassName from ClassMaster";
        ////    _sql = _sql + " where (Course='" + DropCourse.SelectedValue + "' or Course is NULL) and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and CIDOrder !=0 ";
        ////    _oo.FillDropDown_withValue(_sql, DropAdmissionClass, "ClassName", "Id");
        ////    DropAdmissionClass.Items.Insert(0, new ListItem("<--Select Class-->", "0"));
        ////}
        //protected void DropCourse_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //LoadClass();
        //}

        //protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        //{
        //    //LoadState(drpG2State);
        //    //LoadCity(drpG2City, DrpPreState);
        //    //try
        //    //{
        //    //    // ReSharper disable once RedundantBoolCompare
        //    //    if (CheckBox3.Items[0].Selected == true)
        //    //    {
        //    //        txtG2Address.Text = txtPreaddress.Text;
        //    //        drpG2State.SelectedValue = DrpPreState.SelectedValue;
        //    //        drpG2City.SelectedValue = DrpPreCity.SelectedValue;
        //    //        txtG2Pin.Text = txtPreZip.Text;
        //    //        txtG2MobileNo.Text = txtPreMobileNo.Text;
        //    //        txtG2PhoneNo.Text = txtPrePhoneNo.Text;
        //    //    }
        //    //    else
        //    //    {

        //    //        txtG2Address.Text = "";
        //    //        txtG2Pin.Text = "";
        //    //        txtG2MobileNo.Text = "";
        //    //        txtG2PhoneNo.Text = "";
        //    //        LoaddefaultState(drpG2State);
        //    //        LoaddefaultCity(drpG2City, drpG2State);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox3);
        //    //}


        //}
        //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    //LoadState(drpG1State);
        //    //LoadCity(drpG1City, DrpPreState);
        //    //try
        //    //{
        //    //    // ReSharper disable once RedundantBoolCompare
        //    //    if (CheckBox2.Items[0].Selected == true)
        //    //    {
        //    //        txtG1Address.Text = txtPreaddress.Text;
        //    //        drpG1State.SelectedValue = DrpPreState.SelectedValue;
        //    //        drpG1City.SelectedValue = DrpPreCity.SelectedValue;
        //    //        txtG1Pin.Text = txtPreZip.Text;
        //    //        txtG1MobileNo.Text = txtPreMobileNo.Text;
        //    //        txtG1PhoneNo.Text = txtPrePhoneNo.Text;
        //    //    }
        //    //    else
        //    //    {

        //    //        txtG1Address.Text = "";
        //    //        txtG1Pin.Text = "";
        //    //        txtG1MobileNo.Text = "";
        //    //        txtG1PhoneNo.Text = "";
        //    //        LoaddefaultState(drpG1State);
        //    //        LoaddefaultCity(drpG1City, drpG1State);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox2);
        //    //}
        //}
        //#region WebMethod
        //[WebMethod]
        //public static List<string> GetAgeofStudent(string date1, string date2)
        //{
        //    if (date2 == "NaN/NaN/NaN")
        //    {
        //        date2 = date1;
        //    }
        //    // ReSharper disable once RedundantAssignment
        //    var con = new SqlConnection();
        //    con = BAL.objBal.dbGet_connection();
        //    var dt = new DataTable();
        //    SqlDataAdapter da;
        //    using (var cmd = new SqlCommand())
        //    {
        //        cmd.CommandText = "AgeCalculaterProc";
        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@DOB", date2);
        //        cmd.Parameters.AddWithValue("@DOB1", date1);
        //        da = new SqlDataAdapter
        //        {
        //            SelectCommand = cmd
        //        };
        //    }
        //    da.Fill(dt);
        //    // ReSharper disable once UseObjectOrCollectionInitializer
        //    var datepart = new List<string>();
        //    datepart.Add(dt.Rows[0][0].ToString());
        //    datepart.Add(dt.Rows[0][1].ToString());
        //    datepart.Add(dt.Rows[0][2].ToString());

        //    return datepart;
        //}
        //#endregion

        //protected void DrpPerCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //_sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPerCountry.SelectedValue + "'";
        //    //BAL.objBal.FillDropDown_withValue(_sql, DrpPerState, "StateName", "id");
        //    //if (DrpPerState.Items.Count == 0)
        //    //{
        //    //    DrpPerState.Items.Add(new ListItem("Other", "Other"));
        //    //    DrpPerCity.Items.Clear();
        //    //    DrpPerCity.Items.Add(new ListItem("Other", "Other"));
        //    //}
        //    //else
        //    //{
        //    //    LoadCity(DrpPerCity, DrpPerState);
        //    //}
        //}
        //protected void DrpPreCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //_sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPreCountry.SelectedValue + "'";
        //    //BAL.objBal.FillDropDown_withValue(_sql, DrpPreState, "StateName", "id");
        //    //if (DrpPreState.Items.Count == 0)
        //    //{
        //    //    DrpPreState.Items.Add(new ListItem("Other", "Other"));
        //    //    DrpPreCity.Items.Clear();
        //    //    DrpPreCity.Items.Add(new ListItem("Other", "Other"));
        //    //}
        //    //else
        //    //{
        //    //    //LoadCity(DrpPreCity, DrpPreState);
        //    //}
        //}
        //protected void drpG1Country_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //_sql = " Select StateName,Id from StateMaster where CountryId='" + drpG1Country.SelectedValue + "'";
        //    //BAL.objBal.FillDropDown_withValue(_sql, drpG1State, "StateName", "id");
        //    //if (drpG1State.Items.Count == 0)
        //    //{
        //    //    drpG1State.Items.Add(new ListItem("Other", "Other"));
        //    //    drpG1City.Items.Clear();
        //    //    drpG1City.Items.Add(new ListItem("Other", "Other"));
        //    //}
        //    //else
        //    //{
        //    //    LoadCity(drpG1City, drpG1State);
        //    //}
        //}
        //protected void drpG2Country_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //_sql = " Select StateName,Id from StateMaster where CountryId='" + drpG2Country.SelectedValue + "'";
        //    //BAL.objBal.FillDropDown_withValue(_sql, drpG2State, "StateName", "id");
        //    //if (drpG2State.Items.Count == 0)
        //    //{
        //    //    drpG2State.Items.Add(new ListItem("Other", "Other"));
        //    //    drpG2City.Items.Clear();
        //    //    drpG2City.Items.Add(new ListItem("Other", "Other"));
        //    //}
        //    //else
        //    //{
        //    //    //LoadCity(drpG2City, drpG2State);
        //    //}
        //}

        //protected void drpG1State_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //LoadCity(drpG1City, drpG1State);
        //}
        //protected void drpG2State_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //LoadCity(drpG2City, drpG2State);
        //}
        //protected void lnkQuickReg_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("StudentQuickRegistration.aspx?check=student_registration");
        //}
        //protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //LoadStream();
        //    //LoadOptionalSubject();
        //}
        //protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //LoadOptionalSubject();
        //}
        ////Get ALL Exam Records
        //public void LoadEntranceExamName()
        //{
        //    try
        //    {
        //        var param = new List<SqlParameter>();
        //        param.Add(new SqlParameter("@QueryFor", "S"));
        //        param.Add(new SqlParameter("@Id", "-1"));
        //        var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("EntranceExamMaster_Proc", param);
        //        if (ds != null)
        //        {
        //            //var dt = ds.Tables[0];
        //            //if (dt.Rows.Count > 0)
        //            //{
        //            //    drpExamCrackedof.DataSource = dt;
        //            //    drpExamCrackedof.DataTextField = "ExamName";
        //            //    drpExamCrackedof.DataValueField = "Id";
        //            //    drpExamCrackedof.DataBind();
        //            //}
        //            //else
        //            //{
        //            //    drpExamCrackedof.DataSource = null;
        //            //    drpExamCrackedof.DataBind();
        //            //}
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // ignored
        //    }
        //}

        //public void SetEntranceExamRecord()
        //{
        //    //var param = new List<SqlParameter>
        //    //{
        //    //    new SqlParameter("@ExamId", drpExamCrackedof.SelectedValue),
        //    //    new SqlParameter("@RollNo", txtRollNo.Text.Trim()),
        //    //    new SqlParameter("@Rank", txtRank.Text.Trim()),
        //    //    new SqlParameter("@CatRank", txtCategoryRank.Text.Trim()),
        //    //    new SqlParameter("@AnyOther", txtAnyOtherCategoryRank.Text.Trim()),
        //    //    new SqlParameter("@srno", Session["srno"].ToString()),
        //    //    new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()),
        //    //    new SqlParameter("@SessionName", Session["SessionName"].ToString()),
        //    //    new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
        //    //    new SqlParameter("@LoginName", Session["LoginName"].ToString())
        //    //};

        //    //DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentEntranceExamDetails_PROC", param);

        //}

        //public override void Dispose()
        //{
        //    _con.Dispose();
        //    _oo.Dispose();
        //}
    }
}