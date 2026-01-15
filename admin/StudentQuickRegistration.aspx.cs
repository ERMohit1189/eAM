using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using c4SmsNew;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Web.Services;

public partial class admin_student_registration_new : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    BAL.Set_DocumentName obj = new BAL.Set_DocumentName();
    bool RecordNotInsertOfficial;
    bool RecordNotInsertGeneral;
    bool RecordNotInsertPrevious;
    bool RedorcNotInsertfamilDetails;
#pragma warning disable 414
    bool RecordNotInsertDocument;
#pragma warning restore 414
    DLL dllobj = new DLL();
    public admin_student_registration_new()
    {
        RecordNotInsertOfficial = false;
        RecordNotInsertGeneral = false;
        RecordNotInsertPrevious = false;
        RedorcNotInsertfamilDetails = false;
        RecordNotInsertDocument = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        string ss = "";
        int co = 0;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
          
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }
            try
            {
                loadEntranceExamName();
                GeneralDetailDropDown();
                FamilyDetailsDropDown();
                OfficialDetailDropDown();
                string getdate = DateTime.Today.ToString("dd-MMM-yyyy");
                txtAgeOnDate.Text = getdate;
                TextBox100.Text = getdate;
                Get_DocumentName(this.Page);
            }
            catch (Exception ex) { oo.MessageBox(ex.Message, this.Page); }
       
            Panel2.Visible = false;

            sql = "select id  from StudentOfficialDetails";
            sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            try
            {
                co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
            }
            catch (Exception) { co = 0; }
            co = co + 1;
            ss = IDGenerationCollege(co.ToString());
        
            txtFirstNa.Focus();
            BLL obj = new BLL();
            CheckTextTrnsformation();
            AddPreviousInstitutionGridRow();
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
        value = dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "textTransform", "finalsubmit('" + value + "')", true);
    }

    private void loaddefaultCountry(DropDownList drp)
    {
        sql = "Select CountryName,Id from CountryMaster";
        BAL.objBal.FillDropDown_withValue(sql, drp, "CountryName", "id");
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Country", drp);
        }
        catch
        {
        }
    }

    private void loaddefaultState(DropDownList drp)
    {
        sql = "Select StateName,Id from StateMaster";
        BAL.objBal.FillDropDown_withValue(sql, drp, "StateName", "id");
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("State", drp);
        }
        catch
        {
        }
    }

    private void loaddefaultCity(DropDownList drp, DropDownList drpValue)
    {
        sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drp, "CityName", "id");
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("City", drp);
        }
        catch
        {
        }
    }

    private void loadCountry(DropDownList drp)
    {
        sql = "Select CountryName,Id from CountryMaster";
        BAL.objBal.FillDropDown_withValue(sql, drp, "CountryName", "id");
    }

    private void loadState(DropDownList drp)
    {
        sql = "Select StateName,Id from StateMaster";
        BAL.objBal.FillDropDown_withValue(sql, drp, "StateName", "id");
    }

    private void loadCity(DropDownList drp, DropDownList drpValue)
    {
        sql = "Select CityName,id from CityMaster where StateId='" + drpValue.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drp, "CityName", "id");
    }

    private void loadBloodGroup()
    {
        sql = "Select BloodGroupName,BloodGroupId from BloodGroupMaster";
        BAL.objBal.FillDropDown_withValue(sql, drpBloodGroup, "BloodGroupName", "BloodGroupId");
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Blood Group", drpBloodGroup);
        }
        catch
        {
        }
    }

    private void loadDefaultBoard()
    {
        sql = "Select BoardName,id from BoardMaster";
        BAL.objBal.FillDropDown_withValue(sql, DrpBoard, "BoardName", "id");
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Board", DrpBoard);
        }
        catch
        {
        }
    }

    private void loadDefaultMedium()
    {
        sql = "Select Medium,id from MediumMaster where SessionName='" + Session["SessionName"].ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpMedium, "Medium", "id");
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Medium", drpMedium);
        }
        catch
        {
        }
    }

    private void loadDefaultNationality()
    {
        sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Nationality'";
        txtMotherNationality.Text = txtFatherNationality.Text = TextBox65.Text = oo.ReturnTag(sql, "defaultvalue");
    }

    private void loadDefaultHomeTown()
    {
        sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='HomeTown'";
        txtHomeTown.Text = oo.ReturnTag(sql, "defaultvalue");
    }

    private void loadDefaultMotherTongue()
    {
        sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='MotherTongue'";
        txtMotherTongue.Text = oo.ReturnTag(sql, "defaultvalue");
    }

    private void loadDefaultCast()
    {
        sql = "Select defaultvalue from DefaultSelectedValue where defaultvalueof='Caste'";
        TextBox66.Text = oo.ReturnTag(sql, "defaultvalue");
    }

    private void loadDefaultReligion()
    {
        sql = "select ReligionName,ReligionId from ReligionMaster";
        oo.FillDropDown_withValue(sql, DropDownList1, "ReligionName", "ReligionId");

        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Religion", DropDownList1);
        }
        catch
        {
        }
    }

    protected void swipeLabel(Control parent)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                swipeLabel(_ChildControl);
            }
            else
            {
                if (_ChildControl is Label)
                {
                    string text = ((Label)_ChildControl).Text;
                    sql = "Select replace from DefaultText where replacewith='" + text + "'";
                    if (oo.ReturnTag(sql, "replace") != "")
                    {
                        ((Label)_ChildControl).Text = oo.ReturnTag(sql, "replace");
                    }
                }
            }
        }
    }

    protected void CheckTextTrnsformation()
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
        value = dllobj.Sp_SelectRecord_usingExecuteScalar("SetandGet_texttransformdata", param);
        if (value != DBNull.Value)
        {
            switch ((string)value)
            {
                case "U":
                    AddStyletocotrol(this.Page, "uppercase");
                    break;
                case "L":
                    AddStyletocotrol(this.Page, "lowercase");
                    break;
                case "C":
                    AddStyletocotrol(this.Page, "capitalize");
                    break;
                default:
                    AddStyletocotrol(this.Page, "none");
                    break;
            }
        }
    }

    public void AddStyletocotrol(Control parent, string istransform)
    {
        foreach (Control _ChildControl in parent.Controls)
        {
            if ((_ChildControl.Controls.Count > 0))
            {
                AddStyletocotrol(_ChildControl, istransform);
            }
            else
            {

                if (_ChildControl is TextBox)
                {
                    if (((TextBox)_ChildControl).ID != "txtSr")
                    {
                        ((TextBox)_ChildControl).Style.Add("text-transform", istransform);
                    }
                }
                else if (_ChildControl is DropDownList)
                {
                    ((DropDownList)_ChildControl).Style.Add("text-transform", istransform);
                }
                else if (_ChildControl is RadioButton)
                {
                    ((RadioButton)_ChildControl).Style.Add("text-transform", istransform);
                }
                else if (_ChildControl is RadioButtonList)
                {
                    ((RadioButtonList)_ChildControl).Style.Add("text-transform", istransform);
                }
                else if (_ChildControl is CheckBox)
                {
                    ((CheckBox)_ChildControl).Style.Add("text-transform", istransform);
                }
                else if (_ChildControl is CheckBoxList)
                {
                    ((CheckBoxList)_ChildControl).Style.Add("text-transform", istransform);
                }
            }
        }
    }

    protected void Get_DocumentName(Control ctrl)
    {
        sql = "Select DocumentType,Id from dt_CreateDocumentName where BranchCode="+Session["BranchCode"] +"";
        Repeater1.DataSource = BAL.objBal.GridFill(sql);
        Repeater1.DataBind();
    }
    public void NewDocumentsDetails(Control ctrl)
    {
        string msg = "";
        try
        {
            BAL.Set_StudentDocumentRecord obj = new BAL.Set_StudentDocumentRecord();
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                FileUpload FileUpload4 = (FileUpload)Repeater1.Items[i].FindControl("FileUpload4");
                Label lblId = (Label)Repeater1.Items[i].FindControl("lblId");
                Label lblDocument = (Label)Repeater1.Items[i].FindControl("lblDocument");

                CheckBox Chksoft = (CheckBox)Repeater1.Items[i].FindControl("Chksoft");
                CheckBox Chkhard = (CheckBox)Repeater1.Items[i].FindControl("Chkhard");
                CheckBox chkVerified = (CheckBox)Repeater1.Items[i].FindControl("chkVerified");
                TextBox txtRemark = (TextBox)Repeater1.Items[i].FindControl("txtRemark");

                HiddenField hfFile = (HiddenField)Repeater1.Items[i].FindControl("hfFile");
                HiddenField hdfilefileExtention = (HiddenField)Repeater1.Items[i].FindControl("hdfilefileExtention");
                //if (FileUpload4.HasFile)
                //{

                string base64std = hfFile.Value;
                string fileExtention = hdfilefileExtention.Value;

                if (base64std != string.Empty)
                {
                    string filePath = "";
                    string fileName = "";

                    filePath = @"../Uploads/Docs/";
                    string sessionName = Session["SessionName"].ToString();
                    fileName = Application["srno"].ToString() + '_' + lblDocument.Text.Trim() + fileExtention;

                    using (FileStream fs = new FileStream(Server.MapPath((filePath + fileName)), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64std);
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
                    //string fileName = Application["SrNo"].ToString().Replace('/', '_') + '_' + lblDocument.Text.Trim() + '_' + datetime + fileExtention;
                    //FileUpload4.SaveAs(Server.MapPath(filePath + fileName));
                    obj.DocId = lblId.Text.Trim();
                    //obj.DocName = fileName;
                    //obj.DocPath = filePath + fileName;
                    obj.SrNo = Application["SrNo"].ToString().Trim();
                    obj.StEnRCode = Application["StEnRCode"].ToString().Trim();
                    obj.Session = Session["SessionName"].ToString();
                    obj.BranchCode = Convert.ToInt16(Session["BranchCode"].ToString());
                    obj.LoginName = Session["LoginName"].ToString();

                    obj.Softcopy = Chksoft.Checked ? 1 : FileUpload1.HasFile ? 1 : 0;
                    obj.Hardcopy = Chkhard.Checked ? 1 : 0;
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
            oo.MessageBoxforUpdatePanel(msg, ctrl);
        }
    }

    protected void SaveStudentOtherDetails()
    {
        if (rbScholarship.SelectedIndex == 0)
        {
            string studentPhotoDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "StudentPhoto" + "_" + Session["SessionName"].ToString() + "/"));
            string studentPhotoVertualPath = "~/Uploads/Scholarship/" + "StudentPhoto" + "_" + Session["SessionName"].ToString() + "/";
            if (!Directory.Exists(studentPhotoDirectoryPath))
            {
                Directory.CreateDirectory(studentPhotoDirectoryPath);
            }
            string studentSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "studentSignature" + "_" + Session["SessionName"].ToString() + "/"));
            string studentSignatureVertualPath = "~/Uploads/Scholarship/" + "studentSignature" + "_" + Session["SessionName"].ToString() + "/";
            if (!Directory.Exists(studentSignatureDirectoryPath))
            {
                Directory.CreateDirectory(studentSignatureDirectoryPath);
            }
            string parentSignatureDirectoryPath = Server.MapPath(string.Format("~/Uploads/Scholarship/" + "parentSignature" + "_" + Session["SessionName"].ToString() + "/"));
            string parentSignatureVertualPath = "~/Uploads/Scholarship/" + "parentSignature" + "_" + Session["SessionName"].ToString() + "/";
            if (!Directory.Exists(parentSignatureDirectoryPath))
            {
                Directory.CreateDirectory(parentSignatureDirectoryPath);
            }
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@srno", Session["srno"].ToString()));
            param.Add(new SqlParameter("@StEnRCode", Session["StEnRCode"].ToString()));
            param.Add(new SqlParameter("@DurationofCourse", txtDuration.Text.Trim()));
            param.Add(new SqlParameter("@RegistrationNo", txtRegistration.Text.Trim()));
            param.Add(new SqlParameter("@CCNo", txtCastCerti.Text.Trim()));
            param.Add(new SqlParameter("@ICNo", txtIncomeCerti.Text.Trim()));
            param.Add(new SqlParameter("@RCNo", txtRegiCer.Text.Trim()));
            string cydadate = "";
            param.Add(new SqlParameter("@CurrentYearDateofAdmission", cydadate));
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
            param.Add(new SqlParameter("@LastSchoolorCollegeName", txtLastSchoolCollegeName.Text.Trim()));
            param.Add(new SqlParameter("@IdentityProof", txtIdentityProof.Text.Trim()));
            param.Add(new SqlParameter("@IntermediateRollNo", txtIntermediateRollNo.Text.Trim()));
            param.Add(new SqlParameter("@IntermediateBoard", txtIntermediateBoard.Text.Trim()));
            param.Add(new SqlParameter("@IntermediateYearofPssing", txtIntermediateYearofPssing.Text.Trim()));
            string Photoext = "";
            if (fpUploadPhoto.HasFile)
            {
                Photoext = Path.GetExtension(fpUploadPhoto.FileName);
                fpUploadPhoto.SaveAs(studentPhotoDirectoryPath + Session["srno"].ToString().Replace("/", "_") + Photoext);
            }
            param.Add(new SqlParameter("@UploadPhotoPath", fpUploadPhoto.HasFile ? studentPhotoVertualPath + Session["srno"].ToString().Replace("/", "_") + Photoext : DBNull.Value.ToString()));
            param.Add(new SqlParameter("@PhotoName", fpUploadPhoto.HasFile ? Session["srno"].ToString().Replace("/", "_") + Photoext : DBNull.Value.ToString()));
            string Signext = "";
            if (fuUploadStudentSignature.HasFile)
            {
                Signext = Path.GetExtension(fuUploadStudentSignature.FileName);
                fuUploadStudentSignature.SaveAs(studentSignatureDirectoryPath + Session["srno"].ToString().Replace("/", "_") + Signext);
            }
            param.Add(new SqlParameter("@UploadStudentSignature", fuUploadStudentSignature.HasFile ? studentSignatureVertualPath + Session["srno"].ToString().Replace("/", "_") + Signext : DBNull.Value.ToString()));
            param.Add(new SqlParameter("@StudentSignatureName", fuUploadStudentSignature.HasFile ? Session["srno"].ToString().Replace("/", "_") + Signext : DBNull.Value.ToString()));
            string ParentSignext = "";
            if (fuUploadFatherMotherSigThumbPrint.HasFile)
            {
                ParentSignext = Path.GetExtension(fuUploadStudentSignature.FileName);
                fuUploadFatherMotherSigThumbPrint.SaveAs(parentSignatureDirectoryPath + Session["srno"].ToString().Replace("/", "_") + ParentSignext);
            }
            param.Add(new SqlParameter("@UploadParentsSignature", fuUploadFatherMotherSigThumbPrint.HasFile ? parentSignatureVertualPath + Session["srno"].ToString().Replace("/", "_") + ParentSignext : DBNull.Value.ToString()));
            param.Add(new SqlParameter("@UploadParentsSignatureName", fuUploadFatherMotherSigThumbPrint.HasFile ? Session["srno"].ToString().Replace("/", "_") + ParentSignext : DBNull.Value.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            string msg = new DLL().Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOtherDetailsProc", param);
        }
    }

    protected void loadBoard()
    {
        for (int i = 0; i < rptPreviousEducation.Items.Count; i++)
        {
            DropDownList drpBoard = (DropDownList)rptPreviousEducation.Items[i].FindControl("drpBoard");
            sql = "Select BoardName from BoardMaster where BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown(sql, drpBoard, "BoardName");
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
                    sql = "Select BoardName from BoardMaster where  BranchCode=" + Session["BranchCode"] + "";
                    oo.FillDropDown(sql, drpBoard, "BoardName");
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

            //TextBox txtCity = (TextBox))rptPreviousEducation.Items[i].FindControl("txtCity");
            //TextBox txtState = (TextBox))rptPreviousEducation.Items[i].FindControl("txtState");
            //TextBox txtCountry = (TextBox))rptPreviousEducation.Items[i].FindControl("txtCountry");
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

    #region
    public void GeneralDetailDropDown()
    {
        loaddefaultCountry(DrpPerCountry); loaddefaultCountry(DrpPreCountry);
        loaddefaultState(DrpPerState); loaddefaultState(DrpPreState);
        loaddefaultCity(DrpPerCity, DrpPerState); loaddefaultCity(DrpPreCity, DrpPreState);

        loadDefaultCasteName();
        loadDefaultReligion();
        loadBloodGroup();
        loadDefaultNationality();
        loadDefaultHomeTown();
        loadDefaultMotherTongue();
        loadDefaultCast();
    }

    private void loadDefaultCasteName()
    {
        sql = "select CasteName,CasteId from CasteMaster";
        oo.FillDropDown_withValue(sql, DropDownList2, "CasteName", "CasteId");
        DropDownList2.Items.Insert(0, new ListItem("<--Select Category-->", ""));
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Category", DropDownList2);
        }
        catch
        {
        }
    }
    public void OfficialDetailDropDown()
    {
        loadCourse();
        loadClass();
        loadBranch();
        loadStream();

        loadSection();
        loadDefaultMedium();
        loadDefaultBoard();

        loadDefaultFeeGroup();
        loadDefaultHouse();

        loadDefaultTypeofAdmission();

        loadDefaultMOD();
    }

    private void loadDefaultFeeGroup()
    {
        sql = "Select FeeGroupName from FeeGroupMaster ";
        sql = sql + " where  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        BAL.objBal.FillDropDownWithOutSelect(sql, drpPanelCardType, "FeeGroupName");
        drpPanelCardType.Items.Insert(0, new ListItem("<--Select Fee Category-->", ""));
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("FeeGroup", drpPanelCardType);
        }
        catch
        {
        }
    }

    private void loadDefaultHouse()
    {
        sql = "select HouseName from HouseMaster where SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDownWithOutSelect(sql, DropDownList4, "HouseName");
        DropDownList4.Items.Insert(0, new ListItem("<--Select House Name-->", ""));
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("House", DropDownList4);
        }
        catch
        {
        }
    }

    private void loadDefaultTypeofAdmission()
    {
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("TypeofAdmission", DrpNEWOLSAdmission);
        }
        catch
        {
        }
    }

    private void loadDefaultMOD()
    {
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("ModeofDeposit", drpFeeDepositMOD);
        }
        catch
        {
        }
    }

    public void FamilyDetailsDropDown()
    {
        loaddefaultCountry(drpG1Country); loaddefaultCountry(drpG2Country);
        loaddefaultState(drpG1State); loaddefaultState(drpG2State);
        loaddefaultCity(drpG1City, drpG1State); loaddefaultCity(drpG2City, drpG2State);

        loadDefaultFatherOccu();
        loadDefaultMotherOccu();
        DrpRelationship.SelectedIndex = 1;
        drpGuardiantwoRelationship.SelectedIndex = 2;
        txtincomefa.Text = "0";
        txtincomemonthlymother.Text = "0";

    }

    private void loadDefaultFatherOccu()
    {
        sql = "Select DesignationName from GuardianDesMaster where DesignationName not like 'House%'";
        oo.FillDropDownWithOutSelect(sql, drpOccupationfa, "DesignationName");
        drpOccupationfa.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Occupation", drpOccupationfa);
        }
        catch
        {
        }
    }

    private void loadDefaultMotherOccu()
    {
        sql = "Select DesignationName from GuardianDesMaster";
        oo.FillDropDownWithOutSelect(sql, drpOccupationmoth, "DesignationName");
        drpOccupationmoth.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        try
        {
            BLL objBll = new BLL();
            objBll.loadDefaultvalue("Occupation", drpOccupationmoth);
        }
        catch
        {
        }
    }


    public bool GeneralDetails()
    {
        bool flag = true;
        RecordNotInsertGeneral = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "StudentGenaralDetailProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@StEnRCode", Application["StEnRCode"].ToString());
        cmd.Parameters.AddWithValue("@SrNo", Application["SrNo"].ToString());
        cmd.Parameters.AddWithValue("@FirstName", txtFirstNa.Text.ToString());
        cmd.Parameters.AddWithValue("@MiddleName", txtMidNa.Text.ToString());
        cmd.Parameters.AddWithValue("@LastName", txtlast.Text.ToString());
        string Date = txtStudentDOB.Text.Trim();
        cmd.Parameters.AddWithValue("@DOB", Date);
        cmd.Parameters.AddWithValue("@Gender", RadioButtonList1.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
        cmd.Parameters.AddWithValue("@MobileNumber", txtMobile.Text.ToString());
        cmd.Parameters.AddWithValue("@SiblingCategory", "No");
        cmd.Parameters.AddWithValue("@SBSrNo", "");
        cmd.Parameters.AddWithValue("@SBStName", "");
        cmd.Parameters.AddWithValue("@SBFathersName", "");
        cmd.Parameters.AddWithValue("@SBClass", "");
        cmd.Parameters.AddWithValue("@SBSection", "");
        cmd.Parameters.AddWithValue("@PhysicallDisabledCategory", RadioButtonList8.SelectedItem.ToString());

        cmd.Parameters.AddWithValue("@PhyStName", txtPhyName.Text.ToString());
        cmd.Parameters.AddWithValue("@PhyStDetail", txtPhyDetail.Text.ToString());
        cmd.Parameters.AddWithValue("@StLocalAddress", txtPreaddress.Text.ToString());
        string DD1 = "";
        sql = "Select Id from StateMaster where StateName='" + DrpPreState.SelectedItem.ToString() + "'";
        DD1 = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@StLocalStateId", DD1);
        string DD = "";
        sql = "Select Id from CityMaster where CityName='" + DrpPreCity.SelectedItem.ToString() + "'";
        DD = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@StLocalCityId", DD);
        cmd.Parameters.AddWithValue("@StLocalZip", txtPreZip.Text.ToString());
        cmd.Parameters.AddWithValue("@StPerAddress", txtPerAdd.Text.ToString());
        string D = "";
        sql = "Select Id from StateMaster where StateName='" + DrpPerState.SelectedItem.ToString() + "'";
        D = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@StPerStateId", D);

        string SD = "";
        sql = "Select Id from CityMaster where CityName='" + DrpPerCity.SelectedItem.ToString() + "'";
        SD = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@StPerCityId", SD);
        cmd.Parameters.AddWithValue("@StPerZip", txtPerZip.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
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

        string filePath = "";
        string fileName = "";
        if (avatarUpload.HasFile)
        {
            filePath = @"../Uploads/StudentPhoto/";
            string fileExtention = Path.GetExtension(avatarUpload.PostedFile.FileName);
            string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
            fileName = Application["SrNo"].ToString().Trim() + '_' + "Photo" + '_' + datetime + fileExtention;
            avatarUpload.SaveAs(Server.MapPath(filePath + fileName));
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



        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (SqlException) { RecordNotInsertGeneral = false; flag = false; con.Close(); }
        catch (Exception) { RecordNotInsertGeneral = false; flag = false; con.Close(); }
        return flag;
    }
    public bool FamilyDetails()
    {
        bool flag = true;
        SqlCommand cmd = new SqlCommand();
        RedorcNotInsertfamilDetails = true;
        if (drpOccupationfa.SelectedItem.Text == "<--Select-->" || drpOccupationmoth.SelectedItem.Text == "<--Select-->")
        {
            oo.MessageBox("Must Select Condition :<--Select-->:", this.Page);
            return false;
        }
        else
        {


            cmd.CommandText = "StudentFamilyDetailsProc";

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@StEnRCode", Application["StEnRCode"].ToString());
            cmd.Parameters.AddWithValue("@SrNo", Application["SrNo"].ToString());
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
            if (FileUpload2.HasFile)
            {
                FatherImagePath = @"../Uploads/FatherPhoto/";
                string fileExtention = Path.GetExtension(FileUpload2.PostedFile.FileName);
                string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
                FatherImageFileName = Application["SrNo"].ToString().Trim() + '_' + "FatherImage" + '_' + datetime + fileExtention;
                FileUpload2.SaveAs(Server.MapPath(FatherImagePath + FatherImageFileName));
            }
            cmd.Parameters.AddWithValue("@FatherPhotoPath", FatherImagePath + FatherImageFileName);
            cmd.Parameters.AddWithValue("@FatherPhotoName", FatherImageFileName);
            string MotherImagePath = "";
            string MotherImageFileName = "";
            if (FileUpload3.HasFile)
            {
                MotherImagePath = @"../Uploads/MotherPhoto/";
                string fileExtention = Path.GetExtension(FileUpload3.PostedFile.FileName);
                string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
                MotherImageFileName = Application["SrNo"].ToString().Trim() + '_' + "MotherImage" + '_' + datetime + fileExtention;
                FileUpload3.SaveAs(Server.MapPath(MotherImagePath + MotherImageFileName));
            }
            cmd.Parameters.AddWithValue("@MotherPhotoPath", MotherImagePath + MotherImageFileName);
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
            if (FileUpload3.HasFile)
            {
                GroupImagePath = @"../Uploads/GroupPhoto/";
                string fileExtention = Path.GetExtension(FileUpload3.PostedFile.FileName);
                string datetime = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt");
                GroupImageFileName = Application["SrNo"].ToString().Trim() + '_' + "GroupImage" + '_' + datetime + fileExtention;
                FileUpload3.SaveAs(Server.MapPath(GroupImagePath + GroupImageFileName));
            }

            cmd.Parameters.AddWithValue("@GroupPhotoPath", GroupImagePath + GroupImageFileName);
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
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { RedorcNotInsertfamilDetails = false; flag = false; con.Close(); }
            catch (Exception) { RedorcNotInsertfamilDetails = false; flag = false; con.Close(); }
            drpGuardiantwoRelationship.SelectedIndex = 1;
            return flag;

        }
    }
    public bool PreviousSchoolDetails()
    {
        RecordNotInsertPrevious = true;
        bool flag = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "StudentPreviousSchoolProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
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

            if (txtExam.Text.Trim() != "")
            {
                cmd.Parameters.AddWithValue("@StEnRCode", Application["StEnRCode"].ToString());
                cmd.Parameters.AddWithValue("@SrNo", Application["SrNo"].ToString());
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
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);


                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (SqlException) { RecordNotInsertPrevious = false; flag = false; con.Close(); }
                catch (Exception) { RecordNotInsertPrevious = false; flag = false; con.Close(); }
            }
        }
        return flag;
    }
    public bool Document()
    {
        bool flag = true;

        RecordNotInsertDocument = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "StudentDocumentsProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@StEnRCode", Application["StEnRCode"].ToString());
        cmd.Parameters.AddWithValue("@SrNo", Application["SrNo"].ToString());
        try
        {
            oo.MessageBox("Only in JPEG,BMP,PNG,JPG format allowed", this.Page);
        }
        catch (Exception) { }

        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (SqlException) { RecordNotInsertDocument = false; flag = false; con.Close(); }
        catch (Exception) { RecordNotInsertDocument = false; flag = false; con.Close(); }
        return flag;

    }
    public bool OfficialDetails()
    {

        int co = 0;

        bool flag = true;
        RecordNotInsertOfficial = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "StudentOfficialDetailsProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        string ss = "";
        sql = "select max(Id) as Total from StudentOfficialDetails";
        
        ss = oo.ReturnTag(sql, "Total");
        int ss1 = 0;
        try
        {

            ss1 = Convert.ToInt32(ss.Trim());
        }
        catch (Exception) { ss1 = 0; }
        ss1 = ss1 + 1;

        Application.Lock();
        Application["StEnRCode"] = IDGeneration(ss1.ToString());
        Application.UnLock();


        Application.Lock();
        Application["SrNo"] = txtSr.Text.ToString();
        Session["srno"] = txtSr.Text.ToString();
 
        Application.UnLock();
        string stEnrCode = IDGeneration(ss1.ToString());
        cmd.Parameters.AddWithValue("@StEnRCode", stEnrCode);

        Session["StEnRCode"] = stEnrCode;
        cmd.Parameters.AddWithValue("@SrNo", txtSr.Text.ToString());


        string AdmissionDate = TextBox100.Text.Trim();
        cmd.Parameters.AddWithValue("@DateOfAdmiission", AdmissionDate);
        string ClassId = "";

        sql = "select Id from ClassMaster where ClassName='" + DropAdmissionClass.SelectedItem.ToString() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        ClassId = oo.ReturnTag(sql, "Id");

        cmd.Parameters.AddWithValue("@AdmissionForClassId", ClassId);

        string sectionId = "";
        sql = "select Id from SectionMaster where SectionName='" + drpSection.SelectedItem.ToString() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ClassNameId=" + ClassId;

        sectionId = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@SectionId", sectionId);
        cmd.Parameters.AddWithValue("@GroupNa", DropBranch.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@FileNo", txtfileno.Text.ToString());
        cmd.Parameters.AddWithValue("@Reference", txtReferences.Text.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtrema.Text.ToString());
        cmd.Parameters.AddWithValue("@Board", DrpBoard.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@TypeOFAdmision", DrpNEWOLSAdmission.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@medium", drpMedium.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Card", drpPanelCardType.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@HostelRequired", rbHostel.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@TransportRequired", rbTransport.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@HouseName", DropDownList4.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@LibraryRequired", rbLibrary.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Enquiry", txtEnquiryNo.Text.ToString());
        cmd.Parameters.AddWithValue("@BoardUniversityRollNo", txtUniversityBoardRollNo.Text.ToString());
        cmd.Parameters.AddWithValue("@InstituteRollNo", txtSchoolcollegeRollno.Text.ToString());
        cmd.Parameters.AddWithValue("@CardNo", txtCardNo.Text.ToString());
        cmd.Parameters.AddWithValue("@MachineNo", "");
        cmd.Parameters.AddWithValue("@Course", DropCourse.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Branch", DropBranch.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Scholarship", rbScholarship.SelectedItem.Text.Trim());

        cmd.Parameters.AddWithValue("@ModForHostel", drpHostalMOD.SelectedIndex!=0?drpHostalMOD.SelectedValue:"I");
        cmd.Parameters.AddWithValue("@ModForTransport", drpTransportMOD.SelectedIndex != 0 ? drpTransportMOD.SelectedValue : "I");
        cmd.Parameters.AddWithValue("@ModForLibrary", drpLibraryMOD.SelectedIndex != 0 ? drpLibraryMOD.SelectedValue : "I");

        cmd.Parameters.AddWithValue("@MODForFeeDeposit", drpFeeDepositMOD.SelectedValue);
        cmd.Parameters.AddWithValue("@SMSAcknowledgment", drpSMSAcknowledgmentTo.SelectedValue);
        cmd.Parameters.AddWithValue("@EmailAcknowledgment", drpEmailAcknowledgmentTo.SelectedValue);

        double admissionDoneAt = 0;
        double.TryParse(txtAddDoneat.Text,out admissionDoneAt);
        cmd.Parameters.AddWithValue("@AdmissionDoneAt", admissionDoneAt);

        txtDFA.Text = oo.ReturnTag(sql, "DFA");
        txtCFA.Text = oo.ReturnTag(sql, "CFA");
        txtCOFA.Text = oo.ReturnTag(sql, "COFA");
        txtSFA.Text = oo.ReturnTag(sql, "SFA");
      
        if (DropStream.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Streamid", DropStream.SelectedValue.ToString());
        }
        if (RadioButtonList3.SelectedIndex == 0 || RadioButtonList3.SelectedIndex == 1)
        {
            string typeofedu = RadioButtonList3.SelectedIndex == 0 ? "R" : "P";
            cmd.Parameters.AddWithValue("@TypeofEducation", typeofedu);
        }
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            sql = "select id  from StudentOfficialDetails";
            sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            try
            {
                co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
            }
            catch (Exception) { co = 1; }
            co = co + 1;
            ss = IDGeneration(co.ToString());

            rbHostel.SelectedIndex = 1;
            drpHostalMOD.Style.Add("display", "none");

            rbTransport.SelectedIndex = 1;
            drpTransportMOD.Style.Add("display", "none");

            rbLibrary.SelectedIndex = 1;
            drpLibraryMOD.Style.Add("display", "none");

        }
        catch (SqlException) { RecordNotInsertOfficial = false; flag = false; con.Close(); }
        catch (Exception) { RecordNotInsertOfficial = false; flag = false; con.Close(); }

        return flag;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select StudentName,MiddleName,LastName,FatherName,Class,Sex,FatherContactNo from AdmissionFormCollection where RecieptNo='" + TextBox67.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        txtFirstNa.Text = oo.ReturnTag(sql, "StudentName");
        txtMidNa.Text = oo.ReturnTag(sql, "MiddleName");
        txtlast.Text = oo.ReturnTag(sql, "LastName");
        txtfaNameee.Text = oo.ReturnTag(sql, "FatherName");
        txtcontfa.Text = oo.ReturnTag(sql, "FatherContactNo");
        txtcontactNo.Text = oo.ReturnTag(sql, "FatherContactNo");
        txtguardianname.Text = oo.ReturnTag(sql, "FatherName");
        DropAdmissionClass.SelectedItem.Text = oo.ReturnTag(sql, "Class");
        if (oo.ReturnTag(sql, "Sex").ToString().Trim().ToUpper() == "Male".ToUpper())
        {
            RadioButtonList1.SelectedIndex = 0;
        }
        else if (oo.ReturnTag(sql, "Sex").ToString().Trim().ToUpper() == "Female".ToUpper())
        {
            RadioButtonList1.SelectedIndex = 1;
        }
        else if (oo.ReturnTag(sql, "Sex").ToString().Trim().ToUpper() == "Transgender".ToUpper())
        {
            RadioButtonList1.SelectedIndex = 2;
        }
    }
    public string IDGeneration(string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = "eAM00000" + x;

        }
        else if (x.Length == 2)
        {
            xx = "eAM0000" + x;
        }
        else if (x.Length == 3)
        {
            xx = "eAM000" + x;

        }
        else if (x.Length == 4)
        {
            xx = "eAM00" + x;
        }
        else if (x.Length == 5)
        {
            xx = xx + "eAM0" + x;
        }
        else
        {
            xx = x;
        }
        return xx;
    }
    public string IDGenerationCollege(string x)
    {
        string xx = "";
        if (x.Length == 1)
        {
            xx = "eAM000000" + x;

        }
        else if (x.Length == 2)
        {
            xx = "eAM00000" + x;
        }
        else if (x.Length == 3)
        {
            xx = "eAM0000" + x;

        }
        else if (x.Length == 4)
        {
            xx = "eAM000" + x;
        }
        else if (x.Length == 5)
        {
            xx = xx + "eAM00" + x;
        }
        else if (x.Length == 6)
        {
            xx = xx + "eAM0" + x;
        }
        else
        {
            xx = x;
        }
        return xx;
    }
    
    protected void DrpPreState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity(DrpPreCity, DrpPreState);
    }
    protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity(DrpPerCity, DrpPerState);
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        loadState(DrpPerState);
        loadCity(DrpPerCity, DrpPreState);
        try
        {
            if (CheckBox1.Items[0].Selected == true)
            {
                txtPerAdd.Text = txtPreaddress.Text;
                DrpPerState.SelectedValue = DrpPreState.SelectedValue;
                DrpPerCity.SelectedValue = DrpPreCity.SelectedValue;
                txtPerZip.Text = txtPreZip.Text;
                txtPerMobileNo.Text = txtPreMobileNo.Text;
                txtPerPhoneNo.Text = txtPrePhoneNo.Text;
            }
            else
            {

                txtPerAdd.Text = "";
                txtPerZip.Text = "";
                txtPerMobileNo.Text = "";
                txtPerPhoneNo.Text = "";
                loaddefaultState(DrpPerState);
                loaddefaultCity(DrpPerCity, DrpPerState);
            }
        }
        catch (Exception ex)
        {
            BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox1);
        }

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
    private void loadSection()
    {
        sql = "select SectionName from SectionMaster where ClassNameId='" + DropAdmissionClass.SelectedValue.ToString() + "'";
        sql = sql + "  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpSection, "SectionName");
    }

    protected void DropAdmissionClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
        Get_DocumentName(DropAdmissionClass);
    }
    public bool Validation()
    {
        return true;
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        int co = 0;
        string ss = "";
        string Sql1 = "";
        Sql1 = "Select SrNo from StudentOfficialDetails where SrNo='" + txtSr.Text.ToString() + "'";
        string Sql2 = "";
        Sql2 = "Select SrNo from StudentGenaralDetail where SrNo='" + txtSr.Text.ToString() + "'";
        string Sql3 = "";
        Sql3 = "Select SrNo from StudentFamilyDetails where SrNo='" + txtSr.Text.ToString() + "'";
        string Sql4 = "";
        Sql4 = "Select SrNo from StudentPreviousSchool where SrNo='" + txtSr.Text.ToString() + "'";
        string Sql5 = "";
        Sql5 = "Select SrNo from StudentDocs where SrNo='" + txtSr.Text.ToString() + "'";
        if (oo.Duplicate(Sql1) || oo.Duplicate(Sql2) || oo.Duplicate(Sql3) || oo.Duplicate(Sql4) || oo.Duplicate(Sql5))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate S.R.No.!", "A");
            //oo.MessageBoxforUpdatePanel("Duplicate S.R.No.!", LinkButton14);
        }
        else if (Validation())
        {
            if (txtSr.Text != string.Empty)
            {
                try
                {
                    txtSr.BorderColor = ColorTranslator.FromHtml("#D5D5D5");
                }
                catch
                {
                }
                TextTrnsform();
                OfficialDetails();
                GeneralDetails();
                FamilyDetails();
                PreviousSchoolDetails();
                if (RecordNotInsertGeneral == false || RecordNotInsertOfficial == false || RecordNotInsertPrevious == false || RedorcNotInsertfamilDetails == false)
                {
                    sql = "delete from StudentFamilyDetails where StEnRCode='" + Application["StEnRCode"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from StudentGenaralDetail where StEnRCode='" + Application["StEnRCode"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from StudentOfficialDetails where StEnRCode='" + Application["StEnRCode"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from StudentPreviousSchool where StEnRCode='" + Application["StEnRCode"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from StudentDocs where StEnRCode='" + Application["StEnRCode"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                    oo.ProcedureDatabase(sql);

                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry record not inserted (Contact to Admin).", "W");
                    //oo.MessageBoxforUpdatePanel("Sorry record not inserted (Contact to Admin)", LinkButton14);
                }
                else
                {                
                    SaveOptionalSubjectRecord();
                    NewDocumentsDetails(LinkButton14);
                    SaveStudentOtherDetails();
                    StudentPasswordGeneration();
                    GuardianPasswordGeneration();
                    setEntranceExamRecord();
                    try
                    {

                        sql = "select MobileNumber from StudentGenaralDetail  where SrNo='" + txtSr.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "  and SessionName='" + Session["SessionName"].ToString() + "'";
                        string MobileNumber = "";
                        MobileNumber = oo.ReturnTag(sql, "MobileNumber");

                        sql = "select FamilyContactNo from StudentFamilyDetails  where SrNo='" + txtSr.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "  and SessionName='" + Session["SessionName"].ToString() + "'";
                        string FamilyContactNo = "";
                        FamilyContactNo = oo.ReturnTag(sql, "FamilyContactNo");
                        SendFeeSms(MobileNumber, FamilyContactNo);

                    }
                    catch (Exception) { }
                    Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    //oo.MessageBoxforUpdatePanel("Submitted successfully.", LinkButton14);
                    oo.ClearControls(this.Page);

                    chkFaEmail.Checked = false;
                    chkFaMobile.Checked = false;
                    chkGuaEmail.Checked = false;
                    chkGuaMobile.Checked = false;
                    chkMoEmail.Checked = false;
                    chkMoMobile.Checked = false;

                    loadEntranceExamName();
                    GeneralDetailDropDown();
                    FamilyDetailsDropDown();
                    OfficialDetailDropDown();
                    string getdate = DateTime.Today.ToString("dd-MMM-yyyy");
                    txtAgeOnDate.Text = getdate;
                    TextBox100.Text = getdate;
                    Get_DocumentName(this.Page);

                }
                
                TextBox65.Text = "Indian";
                sql = "select id  from StudentOfficialDetails";
                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                try
                {
                    co = Convert.ToInt32(oo.ReturnTag(sql, "id"));
                }
                catch (Exception) { co = 0; }
                co = co + 1;
                ss = IDGenerationCollege(co.ToString());
            }
        }
        else
        {
            txtSr.Focus();
            txtSr.BorderColor = Color.Red;
        }

    }

    public void SaveOptionalSubjectRecord()
    {
        try
        {
            if (txtSr.Text != string.Empty)
            {
                for (int i = 0; i < rbOptionalSubject.Items.Count; i++)
                {
                    if (rbOptionalSubject.Items[i].Selected)
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@Srno", txtSr.Text.Trim()));
                        param.Add(new SqlParameter("@OptSubjectId", rbOptionalSubject.Items[i].Value));
                        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
                        param.Add(new SqlParameter("@LoginName", Session["LoginName"].ToString()));
                        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
                        param.Add(new SqlParameter("@QueryFor", "I"));

                        DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("StudentOptionalSubjectProc", param);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            oo.MessageBoxforUpdatePanel(ex.Message, this.Page);
        }
    }

    public void SendFeeSms(string Mobileno, string FamilyConNo)
    {
        if (Mobileno != "")
        {
            SendFeesSmsStudent(Mobileno);
            SendFeesSmsGuardian(FamilyConNo);
        }
        else
        {
            SendFeesSmsStudent(FamilyConNo);
            SendFeesSmsGuardian(FamilyConNo);
        }
    }
    public void SendFeesSmsStudent(string FmobileNo)
    {
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "HitValue") != "")
        {
            if (oo.ReturnTag(sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                string collegeTitle = "";

                sql = "Select FirstName as StudentName   from StudentGenaralDetail";
                sql = sql + "    where  Srno='" + txtSr.Text.Trim() + "' and BranchCode=" + Session["BranchCode"] + "";

                string StudentName = oo.ReturnTag(sql, "StudentName");

                sql = "Select CollegeShortNa from CollegeMaster  where BranchCode=" + Session["BranchCode"] + "";
                string CollegeShortNa = oo.ReturnTag(sql, "CollegeShortNa");

                sql = "Select UserName,Password StudentPassword from StudentLoginandPassword where";
                sql = sql + " Srno='" + txtSr.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";

                string StEnRCode = oo.ReturnTag(sql, "UserName");
                string StudentPassword = oo.ReturnTag(sql, "StudentPassword");

                mess = "Congrats! " + StudentName + ", you've registered successfully with " + CollegeShortNa + ". Your Userid: " + StEnRCode + " and Password: " + StudentPassword + "";

                string sms_response = "";

                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");
                if (FmobileNo != "")
                {

                    sql = "Select SmsSent From SmsEmailMaster where Id='9' and BranchCode=" + Session["BranchCode"] + "";
                    if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                    {

                        sms_response = sadpNew.Send(mess, FmobileNo, "");

                    }
                }
            }
        }
    }
    public void SendFeesSmsGuardian(string FmobileNo)
    {
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "HitValue") != "")
        {
            if (oo.ReturnTag(sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                string collegeTitle = "";

                sql = "Select FamilyGuardianName as GuardianName from StudentFamilyDetails";
                sql = sql + " where Srno='" + txtSr.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";

                string GuardianName = oo.ReturnTag(sql, "GuardianName");

                sql = "Select CollegeShortNa from CollegeMaster  where BranchCode=" + Session["BranchCode"] + "";
                string CollegeShortNa = oo.ReturnTag(sql, "CollegeShortNa");

                sql = "Select UserName,Password GuardianPassword from GuardianLoginandPassword where";
                sql = sql + " Srno='" + txtSr.Text.Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";

                string StEnRCode = oo.ReturnTag(sql, "UserName");
                string GuardianPassword = oo.ReturnTag(sql, "GuardianPassword");

                mess = "Congrats! Mr./Ms. " + GuardianName + ", your ward is registered successfully with " + CollegeShortNa + ". Your Userid: " + StEnRCode + " and Password: " + GuardianPassword + "";

                string sms_response = "";

                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");
                if (FmobileNo != "")
                {

                    sql = "Select SmsSent From SmsEmailMaster where Id='9' and BranchCode=" + Session["BranchCode"] + "";
                    if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                    {

                        sms_response = sadpNew.Send(mess, FmobileNo, "");

                    }
                }
            }
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
        string ss , wf , pf = string.Empty;
        ss = drpOccupationmoth.SelectedItem.ToString().ToUpper();
        wf = "HOUSE WIFE";
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
        string UID = "";
        string Password = oo.GetPassword();
        sql = "Select '"+ txtSr.Text.Trim().ToString() +"' UserName,(select cast((Abs(Checksum(NewId()))%10) as varchar(1)) + ";
        sql = sql + " char(ascii('a')+(Abs(Checksum(NewId()))%25)) +";
        sql = sql + " char(ascii('A')+(Abs(Checksum(NewId()))%25)) +";
        sql = sql + " left(newid(),5)) Password";
        UID = oo.ReturnTag(sql, "UserName");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "LoginTabProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@LoginName", UID);
        cmd.Parameters.AddWithValue("@Pass", Password);
        cmd.Parameters.AddWithValue("@LoginTypeId", 4);
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@Login", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@Srno", txtSr.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@SessionId", Session["SessionID"].ToString());
        cmd.Parameters.AddWithValue("@BranchId", Session["BranchCode"].ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            SendNotificationToStudent(txtSr.Text.Trim().ToString(), UID, Password);
        }
        catch (SqlException) { con.Close(); }
        catch (Exception) { con.Close(); }


    }


    public void GuardianPasswordGeneration()
    {
        string UID = "";
        string Password = oo.GetPassword();
        sql = "Select 'G'+'" + txtSr.Text.Trim().ToString() + "' UserName,(select cast((Abs(Checksum(NewId()))%10) as varchar(1)) + ";
        sql = sql + " char(ascii('a')+(Abs(Checksum(NewId()))%25)) +";
        sql = sql + " char(ascii('A')+(Abs(Checksum(NewId()))%25)) +";
        sql = sql + " left(newid(),5)) Password";
        UID = oo.ReturnTag(sql, "UserName");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "LoginTabProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@LoginName", UID);
        cmd.Parameters.AddWithValue("@Pass", Password);
        cmd.Parameters.AddWithValue("@Srno", txtSr.Text.Trim().ToString());
        cmd.Parameters.AddWithValue("@LoginTypeId", 5);
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
        cmd.Parameters.AddWithValue("@Login", Session["LoginName"].ToString());
        cmd.Parameters.AddWithValue("@SessionId", Session["SessionID"].ToString());
        cmd.Parameters.AddWithValue("@BranchId", Session["BranchCode"].ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            SendNotificationToGurdian(txtSr.Text.Trim().ToString(),UID, Password);

        }
        catch (SqlException) { con.Close(); }
        catch (Exception) { con.Close(); }
    }



    public void SendNotificationToStudent(string srno,string UID, string Password)
    {
        string Mess = "";
        Mess = "Student Login Panel";
        Mess = Mess + "<br>";
        Mess = Mess + "</hr>";
        Mess = Mess + "<br>";
        Mess = Mess + "Dear ";
        Mess = Mess + "             " + txtFirstNa.Text + " " + txtMidNa.Text + " " + txtlast.Text + ",";
        Mess = Mess + "<br></hr>";
        Mess = Mess + "Your User ID  :" + UID;
        Mess = Mess + "<Br>";
        Mess = Mess + "Your Password :" + Password;

        sql = "select email  from StudentGenaralDetail where srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + "";
        try
        {
            oo.EmailSendingForUser(Mess, "SE Education: Student Login Panel Credentials", oo.ReturnTag(sql, "Email"));
        }
        catch (Exception) { }

    }

    public void SendNotificationToGurdian(string srno, string UID, string Password)
    {
        string Mess = "";
        Mess = "Guardian Login Panel";
        Mess = Mess + "<br>";
        Mess = Mess + "</hr>";
        Mess = Mess + "<br>";
        Mess = Mess + "Dear ";
        Mess = Mess + "             " + txtfaNameee.Text + ",";
        Mess = Mess + "<br></hr>";
        Mess = Mess + "Your User ID  :" + UID;
        Mess = Mess + "<Br>";
        Mess = Mess + "Your Password :" + Password;

        sql = "select FamilyEmail  from StudentFamilyDetails where srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + "";
        try
        {
            oo.EmailSendingForUser(Mess, "SE Education: Guardian Login Panel Credentials", oo.ReturnTag(sql, "FamilyEmail"));
        }
        catch (Exception) { }
    }




    public void PermissionGrant(int add1, LinkButton Ladd)
    {


        if (add1 == 1)
        {
            Ladd.Enabled = true;
        }
        else
        {
            Ladd.Enabled = false;
        }

    }
    public void CheckValueADDDeleteUpdate()
    {
        sql = " select LoginId,LoginName,Pass,SessionId,BranchId,LT.LoginTypeName,ltb.add1 as add1,ltb.delete1 as delete1,ltb.update1 as update1 from LoginTab LTb";
        sql = sql + " left join LoginType Lt on LTb.LoginTypeId=Lt.LoginTypeId where LT.LoginTypeName='" + Session["Logintype"] + "' and LTb.LoginName='" + Session["LoginName"] + "'";
#pragma warning disable 168
        int a, u, d;
#pragma warning restore 168
        a = Convert.ToInt32(oo.ReturnTag(sql, "add1"));


        PermissionGrant(a, (LinkButton)LinkButton14);
    }





    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentPreview.aspx");
    }


    protected void DrpDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList1.Focus();
    }
    protected void DrpRelationship_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpRelationship.SelectedIndex == 0)
        {
            txtguardianname.Text = txtfaNameee.Text;
            txtemailfamily.Text = txtemailfather.Text;
        }
        else if (DrpRelationship.SelectedIndex == 1)
        {
            txtguardianname.Text = txtmotherNameeee.Text;
            txtemailfamily.Text = txtmotheremail.Text;
        }
        else
        {
            txtguardianname.Text = "";
            txtemailfamily.Text = "";
            txtguardianname.Focus();
        }
    }
    protected void drpGuardiantwoRelationship_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpGuardiantwoRelationship.SelectedIndex == 0)
        {
            txtGuardiantwoName.Text = txtfaNameee.Text;
            txtGuardiantwoEmail.Text = txtemailfather.Text;
        }
        else if (drpGuardiantwoRelationship.SelectedIndex == 1)
        {
            txtGuardiantwoName.Text = txtmotherNameeee.Text;
            txtGuardiantwoEmail.Text = txtmotheremail.Text;
        }
        else
        {
            txtGuardiantwoName.Text = "";
            txtGuardiantwoEmail.Text = "";
            txtGuardiantwoName.Focus();
        }

    }
    #endregion

    private void loadCourse()
    {
        sql = "Select CourseName,Id from CourseMaster where SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, DropCourse, "CourseName", "Id");
        DropCourse.Items.Insert(0, new ListItem("<--Select Course-->", "0"));

    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Course='" + DropCourse.SelectedValue.ToString() + "' and ClassId='" + DropAdmissionClass.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue(sql, DropBranch, "BranchName", "Id");
        DropBranch.Items.Insert(0, new ListItem("<--Select Branch-->", "0"));
    }

    private void loadStream()
    {
        sql = "Select Stream,Id from StreamMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and ClassId='" + DropAdmissionClass.SelectedValue.ToString() + "' and BranchId='" + DropBranch.SelectedValue.ToString() + "'";
        oo.FillDropDown_withValue_withSelect(sql, DropStream, "Stream", "Id");
    }

    private void loadOptionalSubject()
    {
        sql = "Select SubjectGroup,id from SubjectGroupMaster where Classid='" + DropAdmissionClass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and Branchid='" + DropBranch.SelectedValue.ToString() + "'  and SectionName='" + drpSection.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsCompulsory=0";
        rbOptionalSubject.DataSource = oo.GridFill(sql);
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
        sql = "Select Id,ClassName from ClassMaster";
        sql = sql + " where (Course='" + DropCourse.SelectedValue.ToString() + "' or Course is NULL) and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and CIDOrder !=0 ";
        oo.FillDropDown_withValue(sql, DropAdmissionClass, "ClassName", "Id");
        DropAdmissionClass.Items.Insert(0, new ListItem("<--Select Class-->", "0"));
    }
    protected void DropCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadClass();
    }

    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        loadState(drpG2State);
        loadCity(drpG2City, DrpPreState);
        try
        {
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
                loaddefaultState(drpG2State);
                loaddefaultCity(drpG2City, drpG2State);
            }
        }
        catch (Exception ex)
        {
            BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox3);
        }


    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        loadState(drpG1State);
        loadCity(drpG1City, DrpPreState);
        try
        {
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
                loaddefaultState(drpG1State);
                loaddefaultCity(drpG1City, drpG1State);
            }
        }
        catch (Exception ex)
        {
            BAL.objBal.MessageBoxforUpdatePanel(ex.Message, CheckBox2);
        }
    }
    #region WebMethod
    [WebMethod]
    public static List<string> getAgeofStudent(string date1,string date2)
    {
        SqlConnection con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "AgeCalculaterProc";
        cmd.Connection = con;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DOB", date2);
        cmd.Parameters.AddWithValue("@DOB1", date1);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
            da.Fill(dt);
            List<string> datepart = new List<string>();
            datepart.Add(dt.Rows[0][0].ToString());
            datepart.Add(dt.Rows[0][1].ToString());
            datepart.Add(dt.Rows[0][2].ToString());
       
        return datepart;
    }
    #endregion

    protected void DrpPerCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " Select StateName,Id from StateMaster where CountryId='"+DrpPerCountry.SelectedValue.ToString()+"'";
        BAL.objBal.FillDropDown_withValue(sql, DrpPerState, "StateName", "id");
        if (DrpPerState.Items.Count == 0)
        {
            DrpPerState.Items.Add(new ListItem("Other", "Other"));
            DrpPerCity.Items.Clear();
            DrpPerCity.Items.Add(new ListItem("Other", "Other"));
        }
        else
        {
            loadCity(DrpPerCity, DrpPerState);
        }
    }
    protected void DrpPreCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " Select StateName,Id from StateMaster where CountryId='" + DrpPreCountry.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, DrpPreState, "StateName", "id");
        if (DrpPreState.Items.Count == 0)
        {
            DrpPreState.Items.Add(new ListItem("Other", "Other"));
            DrpPreCity.Items.Clear();
            DrpPreCity.Items.Add(new ListItem("Other", "Other"));
        }
        else
        {
            loadCity(DrpPreCity, DrpPreState);
        }
    }
    protected void drpG1Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " Select StateName,Id from StateMaster where CountryId='" + drpG1Country.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpG1State, "StateName", "id");
        if (drpG1State.Items.Count == 0)
        {
            drpG1State.Items.Add(new ListItem("Other", "Other"));
            drpG1City.Items.Clear();
            drpG1City.Items.Add(new ListItem("Other", "Other"));
        }
        else
        {
            loadCity(drpG1City, drpG1State);
        }
    }
    protected void drpG2Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " Select StateName,Id from StateMaster where CountryId='" + drpG2Country.SelectedValue.ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpG2State, "StateName", "id");
        if (drpG2State.Items.Count == 0)
        {
            drpG2State.Items.Add(new ListItem("Other", "Other"));
            drpG2City.Items.Clear();
            drpG2City.Items.Add(new ListItem("Other", "Other"));
        }
        else
        {
            loadCity(drpG2City, drpG2State);
        }
    }

    protected void drpG1State_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity(drpG1City, drpG1State);
    }
    protected void drpG2State_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity(drpG2City, drpG2State);
    }
    protected void lnkQuickReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("student_registration.aspx");
    }
    protected void DropBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStream();
        loadOptionalSubject();
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
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
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
}