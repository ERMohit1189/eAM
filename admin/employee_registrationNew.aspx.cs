using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Net.Mail;
using c4SmsNew;
using System.IO;
public partial class admin_employee_registration : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    DataTable dt;
    BAL.Staff_Document obj = new BAL.Staff_Document();
    

    bool RecordNotInsertEmpployeeOfficialDetails;
    bool RecordNotInsertEmpGeneralDetail;
    bool RecordNotInsertEmpEmployeeDetails;
    bool RecordNotInsertEmpPreviousEmployment;
#pragma warning disable 414
    bool RedorcNotInsertEmpDocuments;
#pragma warning restore 414

    public admin_employee_registration()
    {
        RecordNotInsertEmpployeeOfficialDetails = false;
        RecordNotInsertEmpGeneralDetail = false;
        RecordNotInsertEmpEmployeeDetails = false;
        RecordNotInsertEmpPreviousEmployment = false;
        RedorcNotInsertEmpDocuments = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            try
            {
                CheckValueADDDeleteUpdate();
            }
            catch (Exception) { }

            TabControl();
            EmpGeneralDetailDropDown();
            EmployeeDetailDropDrop();
            PreviousEmploymentDropDow();
            OfficialDetailDropDown();
            Get_DocumentName(this.Page);
            GetEmpShift();

            GetEarningComponent(-1);
            GetDeductionsComponent(-1);
        }
    }

    private void GetEarningComponent(Int32 DesID)
    {
        BAL.clsComponentValue obj = new BAL.clsComponentValue();

        obj.HR01ID = -1;
        obj.IsActive = 1;
        obj.ComponentType = "E";
        obj.DesignationID = DesID;
        obj.IsOther = -1;

        dt = new DataTable();
        dt = DAL.DALInstance.GetSalaryComponent(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            repEarnings.DataSource = dt;
        }
        else
        {
            repEarnings.DataSource = null;
        }

        repEarnings.DataBind();
    }

    private void GetDeductionsComponent(Int32 DesID)
    {
        BAL.clsComponentValue obj = new BAL.clsComponentValue();

        obj.HR01ID = -1;
        obj.IsActive = 1;
        obj.ComponentType = "D";
        obj.DesignationID = DesID;
        obj.IsOther = -1;

        dt = new DataTable();
        dt = DAL.DALInstance.GetSalaryComponent(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            repDeductions.DataSource = dt;
        }
        else
        {
            repDeductions.DataSource = null;
        }

        repDeductions.DataBind();
    }


    protected void Get_DocumentName(Control ctrl)
    {
    }

    public void NewDocumentsDetails(Control ctrl)
    {

    }

    public void EmpGeneralDetailDropDown()
    {
        sql = "select ltrim(rtrim(StateName)) as StateName from StateMaster ";
        oo.FillDropDown(sql, DrpPreSta, "StateName");
        sql = "select ltrim(rtrim(CityName)) as CityName from CityMaster ";
        oo.FillDropDown(sql, DrpPresCity, "CityName");
        sql = "select ltrim(rtrim(StateName)) as StateName from StateMaster ";
        oo.FillDropDown(sql, DrpPerState, "StateName");
        sql = "select ltrim(rtrim(CityName)) as CityName from CityMaster ";
        oo.FillDropDown(sql, DrpPerCity, "CityName");
        oo.AddDateMonthYearDropDown(DrpYear, DrpMonth, DrpDate);
        FindCurrentDateandSetinDropDown();

        DrpPreSta.Text = "Uttar Pradesh";
        DrpPresCity.Text = "Lucknow";
        DrpPerState.Text = "Uttar Pradesh";
        DrpPerCity.SelectedIndex = 0;
    }

    public void EmployeeDetailDropDrop()
    {
        sql = "Select ReligionName from ReligionMaster";
        oo.FillDropDown(sql, DrpReligion, "ReligionName");
        sql = "Select CasteName from CasteMaster";
        oo.FillDropDown(sql, DrpCategory, "CasteName");
        sql = "Select BloodGroupName from BloodGroupMaster";
        oo.FillDropDown(sql, drpblood, "BloodGroupName");
    }

    public void PreviousEmploymentDropDow()
    {
        sql = "select ltrim(rtrim(CountryName)) as CountryName from CountryMaster";
        oo.FillDropDown(sql, DrpCountry, "CountryName");
        sql = "select ltrim(rtrim(StateName)) as StateName from StateMaster ";
        oo.FillDropDown(sql, DrpState, "StateName");
        sql = "select ltrim(rtrim(CityName)) as CityName from CityMaster ";
        oo.FillDropDown(sql, DrpCity, "CityName");
        sql = "select EmpDesName from EmpDesMaster ";
        oo.FillDropDown(sql, DrpDesignation, "EmpDesName");
        oo.AddDateMonthYearDropDown(DrpYear2, DrpMonth2, DrpDate2);
        FindCurrentDateandSetinDropDown2();
        oo.AddDateMonthYearDropDown(DrpYear3, DrpMonth3, DrpDate3);
        FindCurrentDateandSetinDropDown3();
        DrpCountry.Text = "India";
        DrpState.Text = "Uttar Pradesh";
        DrpCity.Text = "Lucknow";
    }

    public void OfficialDetailDropDown()
    {
        oo.AddDateMonthYearDropDown(drpyearhai, drpmonthhai, drpdinhai);
        FindCurrentDateandSetinDropDownhai();
        sql = "Select EmpDepName from EmpDepMaster where BranchCode="+Session["BranchCode"] +" ";
        oo.FillDropDown(sql, txtDepartmentName, "EmpDepName");
        sql = "Select EmpDesName from EmpDesMaster  where BranchCode=" + Session["BranchCode"] + " ";
        oo.FillDropDown(sql, drpdes, "EmpDesName");
        sql = "select Ltrim(EmployeeCategoryName) EmployeeCategoryName from EmployeeCategoryMaster  where BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown(sql, drpempcategory, "EmployeeCategoryName");
    }


    
    public void TabControl()
    {
        //TabContainer1.ActiveTab = TabContainer1.Tabs[0];

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }


    public bool EmpGeneralDetail()
    {
        bool flag = true;
        RecordNotInsertEmpGeneralDetail = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "EmpGeneralDetailProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        cmd.Parameters.AddWithValue("@Ecode", Application["Ecode"].ToString());
        cmd.Parameters.AddWithValue("@EmpId", Application["EmpId"].ToString());
        cmd.Parameters.AddWithValue("@Etitle", DrpTitle.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@EFirstName", txtFirstName.Text.ToString());
        cmd.Parameters.AddWithValue("@EMiddleName", txtmidName.Text.ToString());
        cmd.Parameters.AddWithValue("@ELastName", txtlastName.Text.ToString());
        String Date = "";
        Date = DrpYear2.SelectedItem.ToString() + "/" + DrpMonth2.SelectedItem.ToString() + "/" + DrpDate2.SelectedItem.ToString();
        cmd.Parameters.AddWithValue("@EDateOfBirth", Date);
        cmd.Parameters.AddWithValue("@EGender", RadioButtonList1.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@EFatherName", txtfathername.Text.ToString());
        cmd.Parameters.AddWithValue("@EMotherName", txtmothname.Text.ToString());
        cmd.Parameters.AddWithValue("@EEmail", txtemail.Text.ToString());
        cmd.Parameters.AddWithValue("@EMobileNo", txtmobileno.Text.ToString());
        cmd.Parameters.AddWithValue("@EPreAddress", txtPreseAdd.Text.ToString());
        string DD1 = "";
        sql = "Select Id from StateMaster where StateName='" + DrpPreSta.SelectedItem.ToString() + "'";
        DD1 = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@EPreStateId", DD1);
        string DD = "";
        sql = "Select Id from CityMaster where CityName='" + DrpPresCity.SelectedItem.ToString() + "'";
        DD = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@EPreCityId", DD);
        cmd.Parameters.AddWithValue("@EPreZip", txtPerZip.Text.ToString());
        cmd.Parameters.AddWithValue("@EPerAdd", txtPermAdd.Text.ToString());
        string D = "";
        sql = "Select Id from StateMaster where StateName='" + DrpPerState.SelectedItem.ToString() + "'";
        D = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@EPerStateId", D);
        string SD = "";
        sql = "Select Id from CityMaster where CityName='" + DrpPerCity.SelectedItem.ToString() + "'";
        SD = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@EPerCityId", SD);
        cmd.Parameters.AddWithValue("@EPerZip", txtPerZip.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@EmergencyContactNo", txtemergencycontactno.Text.ToString());
        cmd.Parameters.AddWithValue("@MaritalStatus", DrpMaitalStatus.SelectedItem.ToString());
        string filePath = "";
        string fileName = "";
        if (avatarUpload.HasFile)
        {
            filePath = @"~/Uploads/StaffPhoto/";
            string fileExtention = Path.GetExtension(avatarUpload.PostedFile.FileName);
            fileName = avatarUpload.PostedFile.FileName + fileExtention;
            avatarUpload.SaveAs(Server.MapPath(filePath + fileName));
        }
        cmd.Parameters.AddWithValue("@PhotoPath", filePath + fileName);
        cmd.Parameters.AddWithValue("@PhotoName", fileName);
        cmd.Parameters.AddWithValue("@DisplayName", txtDisplay.Text.Trim());
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Submitted successfully.", this.Page);
           // oo.ClearControls(this.Page);
        }
        catch (SqlException) { RecordNotInsertEmpGeneralDetail = false; flag = false; con.Close(); }
        catch (Exception) { RecordNotInsertEmpGeneralDetail = false; flag = false; con.Close(); }
        return flag;

    }

    public bool EmployeeDetail()
    {

            bool flag = true;
            RecordNotInsertEmpEmployeeDetails = true;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EmpEmployeeDetailsProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@Ecode", Application["Ecode"].ToString());
            cmd.Parameters.AddWithValue("@EmpId", Application["EmpId"].ToString());
            string rel = "";
            sql = "select ReligionId from ReligionMaster where ReligionName='" + DrpReligion.SelectedItem.ToString() + "'";
            rel = oo.ReturnTag(sql, "ReligionId");
            cmd.Parameters.AddWithValue("@EReligionId", rel);
            string Cat = "";
            sql = "select CasteId from CasteMaster where CasteName='" + DrpCategory.SelectedItem.ToString() + "'";
            Cat = oo.ReturnTag(sql, "CasteId");
            cmd.Parameters.AddWithValue("@ECategoryId", Cat);
            cmd.Parameters.AddWithValue("@EHeight", txtheight.Text.ToString());
            cmd.Parameters.AddWithValue("@Ediseases", txtdiseas.Text.ToString());
            cmd.Parameters.AddWithValue("@EIdentificationMark", txtidentmark.Text.ToString());
            cmd.Parameters.AddWithValue("@EHostelRequired", RadioButtonList3.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@ENationality", txtnat.Text.ToString());
            cmd.Parameters.AddWithValue("@ECaste", txtcaste.Text.ToString());
            string Blood = "";
            sql = "select BloodGroupId from BloodGroupMaster where BloodGroupName='" + drpblood.SelectedItem.ToString() + "'";
            Blood = oo.ReturnTag(sql, "BloodGroupId");
            cmd.Parameters.AddWithValue("@EBloodGroupId", Blood);
            cmd.Parameters.AddWithValue("@EWeight", txtweight.Text.ToString());
            cmd.Parameters.AddWithValue("@EHobbies", txthobbies.Text.ToString());
            cmd.Parameters.AddWithValue("@ETransportRequired", RadioButtonList4.Text.ToString());
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (SqlException) { RecordNotInsertEmpEmployeeDetails = false; flag = false; con.Close(); }
            catch (Exception) { RecordNotInsertEmpEmployeeDetails = false; flag = false; con.Close(); }
            return flag;



        }
  
    public bool PreviousEmployment()
    {

        bool flag = true;
        RecordNotInsertEmpPreviousEmployment = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "EmpPreviousEmploymentProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        //string ss = "";
        //sql = "select Count(*) as Total from EmpPreviousEmployment";
        //ss = oo.ReturnTag(sql, "Total");
        //int ss1 = Convert.ToInt32(ss.Trim());
        //ss1 = ss1 + 1;
        cmd.Parameters.AddWithValue("@Ecode",  Application["Ecode"].ToString());

        cmd.Parameters.AddWithValue("@EmpId", Application["EmpId"].ToString());
         string DD2 = "";
        sql = "Select Id from CountryMaster where CountryName='" + DrpCountry.SelectedItem.ToString() + "'";
        DD2 = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@ECountryId", DD2);
        string DD1 = "";
        sql = "Select Id from StateMaster where StateName='" + DrpState.SelectedItem.ToString() + "'";
        DD1 = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@EStateId", DD1);
         string SD = "";
        sql = "Select Id from CityMaster where CityName='" + DrpCity.SelectedItem.ToString() + "'";
        SD = oo.ReturnTag(sql, "Id");
        cmd.Parameters.AddWithValue("@ECityId", SD);
        cmd.Parameters.AddWithValue("@ETypeOfOrganization", drptyporganizat.Text.ToString());
        cmd.Parameters.AddWithValue("@EDesignation", DrpDesignation.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@EProfile", txtProfile.Text.ToString());
        cmd.Parameters.AddWithValue("@ENameOfOrganization", txtName0rga.Text.ToString());
        cmd.Parameters.AddWithValue("@EDealsIn", txtdealin.Text.ToString());

        String Datee = "";
        Datee = DrpYear2.SelectedItem.ToString() + "/" + DrpMonth2.SelectedItem.ToString() + "/" + DrpDate2.SelectedItem.ToString();

        cmd.Parameters.AddWithValue("@EFromDate", Datee);

        String Dateee = "";
        Dateee = DrpYear3.SelectedItem.ToString() + "/" + DrpMonth3.SelectedItem.ToString() + "/" + DrpDate3.SelectedItem.ToString();

        cmd.Parameters.AddWithValue("@EToDate", Dateee);
        cmd.Parameters.AddWithValue("@EAddress", txtaddres.Text.ToString());
        cmd.Parameters.AddWithValue("@EReasonOfResign", txtreasonresign.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        try
        {

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Submitted successfully.", this.Page);
            //oo.ClearControls(this.Page);

        }
        catch (SqlException) { RecordNotInsertEmpPreviousEmployment = false; flag = false; con.Close(); }
        catch (Exception) { RecordNotInsertEmpPreviousEmployment = false; flag = false; con.Close(); }
        return flag;

    }
    public bool EmployeeDocuments()
    {
//        //@Ecode nvarchar(50),@EPhotoPath nvarchar(50),@ERelievingLetterpPath nvarchar(50),
////////@EDomicileCertificatePath nvarchar(50),@EPanCardPath nvarchar(50),
////////@ELastSalarySlipPath nvarchar(50),@ECasteCertificatePath nvarchar(50),@BranchCode nvarchar(50),@LoginName nvarchar(50),
////////@SessionName nvarchar(50))
//        RedorcNotInsertEmpDocuments = true;
        bool flag = true;
//         SqlCommand cmd = new SqlCommand();
//         cmd.CommandText = "EmpDocumentsProc";
//        cmd.CommandType = CommandType.StoredProcedure;
//        cmd.Connection = con;
//        //string ss = "";
//        //sql = "select Count(*) as Total from EmpDocuments";
//        //ss = oo.ReturnTag(sql, "Total");
//        //int ss1 = Convert.ToInt32(ss.Trim());
//        //ss1 = ss1 + 1;
//        cmd.Parameters.AddWithValue("@Ecode",  Application["Ecode"].ToString());
//        cmd.Parameters.AddWithValue("@EmpId",  Application["EmpId"].ToString());


//        try
//        {
//            if (Photoupload.FileName == "")
//            {
//                cmd.Parameters.AddWithValue("@EPhotoPath", "~/e_StaffP/" + "Blank.jpg");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@EPhotoPath", "~/e_StaffP/" + Photoupload.FileName.ToString());
//                Photoupload.SaveAs(Server.MapPath("~/e_StaffP/" + Photoupload.FileName));
//            }
//        }
//        catch (Exception) { }


//        try
//        {
//            if(RelivingUpload.FileName=="")
//            {
//                cmd.Parameters.AddWithValue("@ERelievingLetterpPath", "~/e_StaffRL/" + "Blank.jpg");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@ERelievingLetterpPath", "~/e_StaffRL/" + RelivingUpload.FileName.ToString());
//                RelivingUpload.SaveAs(Server.MapPath("~/e_StaffRL/" + RelivingUpload.FileName));
//            }
//        }
//        catch (Exception) { }



//        try
//        {
//            if( DomicilCerti.FileName=="")
//            {
//                cmd.Parameters.AddWithValue("@EDomicileCertificatePath", "~/e_StaffDC/" + "Blank.jpg");
//            }
//              else
//            {
//                cmd.Parameters.AddWithValue("@EDomicileCertificatePath", "~/e_StaffDC/" + DomicilCerti.FileName.ToString());
//                DomicilCerti.SaveAs(Server.MapPath("~/e_StaffDC/" + DomicilCerti.FileName));
//            }

//        }
//        catch (Exception) { }





//        try
//        {
//            if (PanCard.FileName == "")
//            {
//                cmd.Parameters.AddWithValue("@EPanCardPath", "~/e_StaffPAN/" + "Blank.jpg");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@EPanCardPath", "~/e_StaffPAN/" + PanCard.FileName.ToString());
//                PanCard.SaveAs(Server.MapPath("~/e_StaffPAN/" + PanCard.FileName));
//            }

//        }
//        catch (Exception) { }


//        try
//        {
//            if (LastSalary.FileName == "")
//            {
//                cmd.Parameters.AddWithValue("@ELastSalarySlipPath", "~/e_StaffLSS/" + "Blank.jpg");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@ELastSalarySlipPath", "~/e_StaffLSS/" + LastSalary.FileName.ToString());
//                LastSalary.SaveAs(Server.MapPath("~/e_StaffLSS/" + LastSalary.FileName));
//            }

//        }
//        catch (Exception) { }


//        try
//        {
//            if (CasteCertificate.FileName == "")
//            {
//                cmd.Parameters.AddWithValue("@ECasteCertificatePath", "~/e_StaffO/" + "Blank.jpg");
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue("@ECasteCertificatePath", "~/e_StaffO/" + CasteCertificate.FileName.ToString());
//                CasteCertificate.SaveAs(Server.MapPath("~/e_StaffO/" + CasteCertificate.FileName));
//            }

//        }
//        catch (Exception) { }
       
  
//        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
//        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
//        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
//        cmd.Parameters.AddWithValue("@EPhotoRemark", txtphotoRemark.Text.ToString());
//        cmd.Parameters.AddWithValue("@ERelievingLetterRemark", txtRelievingRemark.Text.ToString());
//        cmd.Parameters.AddWithValue("@EDomicileCertificationRemark", txtdomicilecertificateRemark.Text.ToString());
//        cmd.Parameters.AddWithValue("@EPanCardRemark", TxtPanCardRemark.Text.ToString());
//        cmd.Parameters.AddWithValue("@ELastSalarySlip", TxtLastSalarySlipRemark.Text.ToString());
//        cmd.Parameters.AddWithValue("@ECasteCertification", txtXasteCertificateRemark.Text.ToString());


//        try
//        {

//            con.Open();
//            cmd.ExecuteNonQuery();
//            con.Close();
//            //oo.MessageBox("Submitted successfully.", this.Page);
//          //  oo.ClearControls(this.Page);

//        }
//        catch (SqlException ee) { RedorcNotInsertEmpDocuments = false; flag = false; con.Close(); }
//        catch (Exception aa) { RedorcNotInsertEmpDocuments = false; flag = false; con.Close(); }
        return flag;


    }
    public bool EmployeeCardDetails()
    {     
        bool flag = true;
        //SqlCommand cmd = new SqlCommand();
        //cmd.CommandText = "EmployeeCardDetailsProc";
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Connection = con;
        //cmd.Parameters.AddWithValue("@Ecode", Application["Ecode"].ToString());
        //cmd.Parameters.AddWithValue("@EmpId", Application["EmpId"].ToString());
        //cmd.Parameters.AddWithValue("@UserId",txtuserId.Text.ToString());
        //cmd.Parameters.AddWithValue("@CardNo",txtCardnumber.Text.ToString());
        //cmd.Parameters.AddWithValue("@PinNumber",txtValidUpto.Text.ToString());
        //cmd.Parameters.AddWithValue("ValidUpTo",txtValidUpto.Text.ToString());
        //cmd.Parameters.AddWithValue("Status", TxtStatus.Text.ToString());
        //cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        //cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        //cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);

        //try
        //{

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    //oo.MessageBox("Submitted successfully.", this.Page);
        //   // oo.ClearControls(this.Page);

        //}
        //catch (SqlException ee) { flag = false; Label1.Text = ee.Message.ToString(); con.Close(); }
        return flag;
    }

    public bool EmployeeOfficialDetails()
    {
        int co = 0;
        bool flag = true;
        RecordNotInsertEmpployeeOfficialDetails = true;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "EmpployeeOfficialDetailsProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        string ss = "";
        sql = "select max(SrNo) as Total from EmpployeeOfficialDetails";
        
        ss = oo.ReturnTag(sql, "Total");
        int ss1 =0;
        try
        {
            ss1 = Convert.ToInt32(ss.Trim());
        }    
        catch (Exception) { ss1 = 0; }
        ss1 = ss1 + 1;

        Application.Lock();
        Application["Ecode"] = IDGeneration(ss1.ToString());
        Application.UnLock();

        Application.Lock();
        Application["EmpId"] = txtEmpId.Text.ToString();
        Application.UnLock();

        cmd.Parameters.AddWithValue("@Ecode", IDGeneration(ss1.ToString()));
        cmd.Parameters.AddWithValue("@EmpId", txtEmpId.Text.ToString());

        String Dates = "";
        Dates = drpyearhai.SelectedItem.ToString() + "/" + drpmonthhai.SelectedItem.ToString() + "/" + drpdinhai.SelectedItem.ToString();
        cmd.Parameters.AddWithValue("@RegistrationDate", Dates);
        cmd.Parameters.AddWithValue("@FileNo", txtFileno.Text.ToString());
        cmd.Parameters.AddWithValue("@Reference", txtrefere.Text.ToString());
        cmd.Parameters.AddWithValue("@Remark", txtremak.Text.ToString());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());//Session["Login"].ToString());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
        cmd.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@Designation", drpdes.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@EmpCategory", drpempcategory.SelectedItem.ToString());

        cmd.Parameters.AddWithValue("@EmploymentType", rblEmploymentType.SelectedValue);
        cmd.Parameters.AddWithValue("@A02ID", ddlEmpShift.SelectedValue); 
         
        try 
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Submitted successfully.", this.Page);
            // oo.ClearControls(this.Page);
            sql = "select max(SrNo)  from EmpployeeOfficialDetails";
            sql = sql + "  where BranchCode=" + Session["BranchCode"].ToString() + "";
            try
            {
                co = Convert.ToInt32(oo.ReturnTag(sql, "SrNo"));
            }
            catch (Exception) { co = 1; }
            co = co + 1;
            ss = IDGeneration(co.ToString());
            //  txtSr.Text = ss;
        }

        catch (SqlException) { RecordNotInsertEmpployeeOfficialDetails = false; flag = false; con.Close(); }
        catch (Exception) { RecordNotInsertEmpployeeOfficialDetails = false; flag = false; con.Close(); }
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (txtEmpId.Text != string.Empty)
        {
            int co = 0;
            string Sql1 = "";
            Sql1 = "Select EmpId from EmpployeeOfficialDetails where EmpId='" + txtEmpId.Text.ToString() + "'";
            string Sql2 = "";
            Sql2 = "Select EmpId from EmpGeneralDetail where EmpId='" + txtEmpId.Text.ToString() + "'";
            string Sql3 = "";
            Sql3 = "Select EmpId from EmpEmployeeDetails where EmpId='" + txtEmpId.Text.ToString() + "'";
            string Sql4 = "";
            Sql4 = "Select EmpId from EmpPreviousEmployment where EmpId='" + txtEmpId.Text.ToString() + "'";
            string Sql5 = "";
            Sql5 = "Select EmpId from EmpDocuments where EmpId='" + txtEmpId.Text.ToString() + "'";
            if (oo.Duplicate(Sql1) || oo.Duplicate(Sql2) || oo.Duplicate(Sql3) || oo.Duplicate(Sql4) || oo.Duplicate(Sql5))
            {
                oo.MessageBox("Duplicate Employee Id!", this.Page);
            }
            else if (Validation())
            {
                EmployeeOfficialDetails();
                EmpGeneralDetail();
                EmployeeDetail();
                PreviousEmployment();

                if (RecordNotInsertEmpployeeOfficialDetails == false || RecordNotInsertEmpGeneralDetail == false || RecordNotInsertEmpEmployeeDetails == false || RecordNotInsertEmpPreviousEmployment == false)
                {
                    sql = "delete from EmpployeeOfficialDetails where Ecode='" + Application["Ecode"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from EmpGeneralDetail where Ecode='" + Application["Ecode"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from EmpEmployeeDetails where Ecode='" + Application["Ecode"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from EmpPreviousEmployment where Ecode='" + Application["Ecode"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from EmpDocuments where Ecode='" + Application["Ecode"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.ProcedureDatabase(sql);

                    sql = "delete from EmployeeDocs where Ecode='" + Application["Ecode"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    oo.ProcedureDatabase(sql);

                    oo.MessageBox("Sorry, Record not Inserted!", this.Page);
                }
                else
                {
                    NewDocumentsDetails(LinkButton1);
                    EmployeePasswordGeneration();

                    try
                    {
                        sql = "select EMobileNo from EmpGeneralDetail  where Ecode='" + Application["Ecode"].ToString() + "'  and SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                        string MobileNumber = txtmobileno.Text;
                        MobileNumber = oo.ReturnTag(sql, "EMobileNo");
                        SendRegSms(MobileNumber, Application["Ecode"].ToString());
                    }
                    catch (Exception) { }
                    oo.MessageBox("Submitted successfully.", this.Page);
                    oo.ClearControls(this.Page);
                }
                txtnat.Text = "Indian";

                sql = "select SrNo  from EmpployeeOfficialDetails";
                sql = sql + " where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                try
                {
                    co = Convert.ToInt32(oo.ReturnTag(sql, "SrNo"));
                }
                catch (Exception) { co = 0; }
                co = co + 1;

            }
        }
        else
        {
            oo.MessageBoxforUpdatePanel("Please, Fill all star(*) fields!", LinkButton1);
        }

    }
    public void SendRegSms(string FmobileNo,string code)
    {
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql, "HitValue") != "")
        {
            if (oo.ReturnTag(sql, "HitValue") == "true")
            {
                SMSAdapterNew sadpNew = new SMSAdapterNew();
                string mess = "";
                string collegeTitle = "";

                sql = "Select EFirstName as EmployeeName from EmpGeneralDetail";
                sql = sql + " where  Ecode='" + Application["Ecode"].ToString().Trim() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";

                string EmployeeName = DrpTitle.SelectedItem.ToString()+" "+oo.ReturnTag(sql, "EmployeeName");

                sql = "Select CollegeShortNa from CollegeMaster  where  BranchCode=" + Session["BranchCode"].ToString() + "";
                string CollegeShortNa = oo.ReturnTag(sql, "CollegeShortNa");

                sql = "Select Password from EmpployeeOfficialDetails where";
                sql = sql + " Ecode='" + Application["Ecode"].ToString().Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";

                string Ecode = Application["Ecode"].ToString().Trim();
                string EmployeePassword = oo.ReturnTag(sql, "Password");

                mess = "Congrats! " + EmployeeName + ", you've registered successfully with " + CollegeShortNa + ". Your Userid: " + Ecode + " and Password: " + EmployeePassword + "";

                string sms_response = "";

                string user = "", Pass = "";
                sql = "Select UserId,Password from SmsPanelSetting where BranchCode=" + Session["BranchCode"] + "";
                user = oo.ReturnTag(sql, "UserId");
                Pass = oo.ReturnTag(sql, "Password");

                sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                collegeTitle = oo.ReturnTag(sql, "CollegeShortNa");

                if (FmobileNo != "")
                {
                    sql = "Select SmsSent From SmsEmailMaster where Id='10' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                    {
                            sms_response = sadpNew.Send(mess, FmobileNo, "");
                    }
                }
            }
        }
    }
    public string IDGeneration(string x)
    {
        string xx = "";
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
        oo.YearDropDown(DrpYear, DrpMonth, DrpDate);
    }
    protected void DrpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear, DrpMonth, DrpDate);
    }
    public void FindCurrentDateandSetinDropDown2()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear2.Text = yy;
        if (mm == "1")
        {
            DrpMonth2.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth2.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth2.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth2.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth2.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth2.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth2.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth2.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth2.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth2.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth2.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth2.Text = "Dec";
        }

        DrpDate2.Text = dd;
    }
    protected void DrpPreSta_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + DrpPreSta.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, DrpPresCity, "CityName");
    }
    protected void DrpPresCity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpPerState_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from StateMaster where StateName='" + DrpPerState.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where StateId=" + CCode;

        oo.FillDropDown(sql, DrpPerCity, "CityName");
    }
    protected void DrpPerCity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            txtPermAdd.Text = txtPreseAdd.Text;
            DrpPerCity.Text = DrpPresCity.Text;
            DrpPerState.Text = DrpPreSta.Text;
            txtPerZip.Text = DrpPresZip.Text;
        }
        else
        {
            txtPermAdd.Text = "";
            sql = "select ltrim(rtrim(StateName)) as StateName from StateMaster ";
            oo.FillDropDown(sql, DrpPerState, "StateName");
            sql = "select ltrim(rtrim(CityName)) as CityName from CityMaster ";
            oo.FillDropDown(sql, DrpPerCity, "CityName");
            txtPerZip.Text = "";
        }
    }
    protected void DrpYear2_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYear2, DrpMonth2, DrpDate2);
    }
    protected void DrpMonth2_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear2, DrpMonth2, DrpDate2);
    }
    public void FindCurrentDateandSetinDropDown()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear.Text = yy;
        if (mm == "1")
        {
            DrpMonth.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth.Text = "Dec";
        }


        DrpDate.Text = dd;
    }

    protected void DrpYear3_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpYear3, DrpMonth3, DrpDate3);
    }
    protected void DrpMonth3_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpYear3, DrpMonth3, DrpDate3);
    }
    public void FindCurrentDateandSetinDropDown3()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        DrpYear3.Text = yy;
        if (mm == "1")
        {
            DrpMonth3.Text = "Jan";
        }
        else if (mm == "2")
        {
            DrpMonth3.Text = "Feb";
        }
        else if (mm == "3")
        {
            DrpMonth3.Text = "Mar";
        }
        else if (mm == "4")
        {
            DrpMonth3.Text = "Apr";
        }
        else if (mm == "5")
        {
            DrpMonth3.Text = "May";
        }
        else if (mm == "6")
        {
            DrpMonth3.Text = "Jun";

        }
        else if (mm == "7")
        {
            DrpMonth3.Text = "Jul";
        }
        else if (mm == "8")
        {
            DrpMonth3.Text = "Aug";
        }
        else if (mm == "9")
        {
            DrpMonth3.Text = "Sep";
        }
        else if (mm == "10")
        {
            DrpMonth3.Text = "Oct";
        }
        else if (mm == "11")
        {
            DrpMonth3.Text = "Nov";
        }
        else if (mm == "12")
        {
            DrpMonth3.Text = "Dec";
        }


        DrpDate3.Text = dd;
    }
    protected void DrpDate2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpDate3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CCode = "";
        sql = "select Id from CountryMaster where CountryName='" + DrpCountry.SelectedItem.ToString() + "'";

        CCode = oo.ReturnTag(sql, "id");
        sql = "Select StateName from StateMaster where Countryid=" + CCode;

        oo.FillDropDown(sql, DrpState, "StateName");
    }
    protected void DrpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CC = "";
        sql = "select Id from StateMaster where StateName='" + DrpState.SelectedItem.ToString() + "'";
        CC = oo.ReturnTag(sql, "id");
        sql = "Select CityName from CityMaster where Id=" + CC;
        oo.FillDropDown(sql, DrpCity, "CityName");
    }
    protected void drpyearhai_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpyearhai, drpmonthhai, drpdinhai);

    }
    protected void drpmonthhai_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpyearhai, drpmonthhai, drpdinhai);
    }
    protected void drpdinhai_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public void FindCurrentDateandSetinDropDownhai()
    {
        string dd = "", mm = "", yy = "";


        dd = oo.ReturnTag("Select day(getdate()) as DateDD", "DateDD");
        mm = oo.ReturnTag("Select Month(getdate())as MonthMM", "MonthMM");
        yy = oo.ReturnTag("Select Year(getdate()) as YearYY ", "YearYY");

        drpyearhai.Text = yy;
        if (mm == "1")
        {
            drpmonthhai.Text = "Jan";
        }
        else if (mm == "2")
        {
            drpmonthhai.Text = "Feb";
        }
        else if (mm == "3")
        {
            drpmonthhai.Text = "Mar";
        }
        else if (mm == "4")
        {
            drpmonthhai.Text = "Apr";
        }
        else if (mm == "5")
        {
            drpmonthhai.Text = "May";
        }
        else if (mm == "6")
        {
            drpmonthhai.Text = "Jun";

        }
        else if (mm == "7")
        {
            drpmonthhai.Text = "Jul";
        }
        else if (mm == "8")
        {
            drpmonthhai.Text = "Aug";
        }
        else if (mm == "9")
        {
            drpmonthhai.Text = "Sep";
        }
        else if (mm == "10")
        {
            drpmonthhai.Text = "Oct";
        }
        else if (mm == "11")
        {
            drpmonthhai.Text = "Nov";
        }
        else if (mm == "12")
        {
            drpmonthhai.Text = "Dec";
        }


        drpdinhai.Text = dd;
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpCity_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    

        PermissionGrant(a,(LinkButton)LinkButton1);
    }


    public void EmployeePasswordGeneration()
    {
        string UID = "";
        string Password = oo.GetPassword();
        sql = "select  Ecode from EmpployeeOfficialDetails where EmpId='" + txtEmpId.Text + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
        UID = oo.ReturnTag(sql, "Ecode");
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "LoginTabProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@LoginName", UID);
        cmd.Parameters.AddWithValue("@Pass", Password);
        cmd.Parameters.AddWithValue("@LoginTypeId", 3);
        cmd.Parameters.AddWithValue("@SessionId", Session["SessionID"].ToString());
        cmd.Parameters.AddWithValue("@BranchId", Session["BranchCode"].ToString());
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            sql = "update EmpployeeOfficialDetails set Password='" + Password + "' where Ecode='" + UID + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
            oo.ProcedureDatabase(sql);
            SendNotificationToEmployee(UID, Password);
           
        }
        catch (SqlException) { con.Close(); }
        catch (Exception) { con.Close(); }      
    }
    public void SendNotificationToEmployee(string UID, string Password)
    {
        string Mess = "";
        Mess = "Staff Login Panel";
        Mess = Mess + "<br>";
        Mess = Mess + "</hr>";
        Mess = Mess + "<br>";
        Mess = Mess + "Dear ";        
        Mess = Mess + "             " + txtFirstName.Text + " " + txtmidName.Text + " " + txtlastName.Text+",";
        Mess = Mess + "<br></hr>";
        Mess = Mess + "Your User ID  :" + UID;
        Mess = Mess + "<Br>";
        Mess = Mess + "Your Password :" + Password;

        sql = "select EEmail  from EmpGeneralDetail where Ecode='" + UID + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";

        EmailSending(Mess, "eAM: Staff Login Credentials", oo.ReturnTag(sql, "EEmail"));

    }

    public bool EmailSending(string Mess, string subjectParameter, string TOEmailID)
    {
        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add(TOEmailID);//to ID

        mail.From = new MailAddress("donotreply@eam.co.in");
        mail.Subject = subjectParameter;



        mail.Body = Mess;
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential("donotreply@eam.co.in", "reNply_33@9D");//from id
        //Or your Smtp Email ID and Password
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(mail);
            send = true;
        }
        catch (Exception) { }
        return send;
    }

    protected void txtmobileno_TextChanged(object sender, EventArgs e)
    {

    }

    public void GetEmpShift()
    {
        ddlEmpShift.Items.Clear();
        dt = new DataTable();
        dt = new DAL().GetShiftMaster(-1, "", -1, true);
        if (dt != null && dt.Rows.Count > 0)
        {
            BLL.FillDropDown(ddlEmpShift, dt, "ShiftName", "A02ID", 'S');
        }
    }

    protected void ddlEmpShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gvShiftDetail.DataSource = null;
            gvShiftDetail.DataBind();

            if (ddlEmpShift.SelectedIndex > 0)
            {
                dt = new DataTable();
                dt = new DAL().GetShiftMaster(Convert.ToInt32(ddlEmpShift.SelectedValue), "", -1, true);

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvShiftDetail.DataSource = dt;
                    gvShiftDetail.DataBind();
                }
            }
        }
        catch(Exception)
        {

        }
    }

    protected void txtCTC_TextChanged(object sender, EventArgs e)
    {
        SetSalaryComponent();
    }

    public void SetSalaryComponent()
    {
        ResetSalaryComponent();

        if(txtEmpId.Text!="")
        {
            if(drpdes.SelectedIndex>0)
            {
                decimal CTC = 0;
                decimal.TryParse(txtCTC.Text.Trim(), out CTC);

                if (CTC > 0)
                {
                    string df = txtEmpId.Text.Trim();
                    string DesID = Campus.CampusInstance.ReturnTag("SELECT EmpDesId FROM EmpDesMaster WHERE EmpDesName='" + drpdes.SelectedValue + "' and  BranchCode=" + Session["BranchCode"].ToString() + "", "EmpDesId");
                    DAL.DALInstance.CalculateSalaryComponent(CTC, Convert.ToInt32(DesID), txtEmpId.Text.Trim());

                    if (repEarnings.Items.Count > 0)
                    {
                        FillComponentValue(repEarnings);
                    }

                    if (repDeductions.Items.Count > 0)
                    {
                        FillComponentValue(repDeductions);
                    }
                }
            }
        }     
    }

    public void ResetSalaryComponent()
    {
        if (DrpDesignation.SelectedValue == "--Select--")
        {
            GetEarningComponent(-1);
            GetDeductionsComponent(-1);
        }
        else
        {
            string DesID = Campus.CampusInstance.ReturnTag("SELECT EmpDesId FROM EmpDesMaster WHERE EmpDesName='" + drpdes.SelectedValue + "' and  BranchCode=" + Session["BranchCode"].ToString() + "", "EmpDesId");
            GetEarningComponent(Convert.ToInt32(DesID));
            GetDeductionsComponent(Convert.ToInt32(DesID));
        }
    }

    public void FillComponentValue(Repeater rep)
    {
        for (int i = 0; i < rep.Items.Count; i++)
        {
            RepeaterItem repItm = rep.Items[i];

            Int32 ID = Convert.ToInt32((repItm.FindControl("lblHR01ID") as Label).Text.Trim());
            TextBox txt = repItm.FindControl("txt") as TextBox;

            string DesID = Campus.CampusInstance.ReturnTag("SELECT EmpDesId FROM EmpDesMaster WHERE EmpDesName='" + drpdes.SelectedValue + "' and  BranchCode=" + Session["BranchCode"].ToString() + "", "EmpDesId");
            string Value = Campus.CampusInstance.ReturnTag("SELECT HR03.Value FROM HR01_SalaryComponent HR01 JOIN HR03_EmployeeSalary HR03 ON HR01.HR01ID=HR03.HR01ID WHERE EmpID='" + txtEmpId.Text.Trim() + "' AND HR01.HR01ID='" + ID + "' AND HR01.DesignationID='" + DesID + "'", "Value");

            decimal temp = 0;
            decimal.TryParse(Value, out temp);

            if (temp > 0)
            {
                txt.Text = temp.ToString();
            }
            else
            {
                txt.Text = "";
            }
        }
    }

    protected void repEarnings_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
    }

    protected void repDeductions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void DrpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(drpdes.SelectedValue=="--Select--")
        {
            GetEarningComponent(-1);
            GetDeductionsComponent(-1);
        }
        else
        {
            string DesID = Campus.CampusInstance.ReturnTag("SELECT EmpDesId FROM EmpDesMaster WHERE EmpDesName='" + drpdes.SelectedValue + "' and  BranchCode=" + Session["BranchCode"].ToString() + "", "EmpDesId");
            GetEarningComponent(Convert.ToInt32(DesID));
            GetDeductionsComponent(Convert.ToInt32(DesID));
        }
    }

    public bool InsertEarningDeduction()
    {
        bool flag = true;

        for (int i = 0; i < repEarnings.Items.Count; i++)
        {
            string ID = (repEarnings.Items[i].FindControl("lblHR01ID_"+i.ToString()) as Label).Text.Trim();
            string temp = (repEarnings.Items[i].FindControl("txt_"+i.ToString()) as TextBox).Text.Trim();

            DAL.DALInstance.SetEmpSalaryComponent(txtEmpId.Text.Trim(), Convert.ToInt32(ID), Convert.ToDecimal(temp), "I");
        }

        for (int i = 0; i < repDeductions.Items.Count; i++)
        {
            string ID = (repDeductions.Items[i].FindControl("lblHR01ID_" + i.ToString()) as Label).Text.Trim();
            string temp = (repDeductions.Items[i].FindControl("txt_" + i.ToString()) as TextBox).Text.Trim();

            DAL.DALInstance.SetEmpSalaryComponent(txtEmpId.Text.Trim(), Convert.ToInt32(ID), Convert.ToDecimal(temp), "I");
        }

        return flag;
    }
}

