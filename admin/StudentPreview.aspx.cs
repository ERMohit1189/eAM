using System.Web.UI;
using System.Data.SqlClient;
using System;

public partial class admin_StudentPreview : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            Panel1.Visible = false;
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        //sql = "Select Row_Number() over (order by SG.Id Asc) as SNo ,SG.Id, SC.SectionName,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,sg.gender,so.TypeOFAdmision as TypeOFAdmision,sg.StLocalAddress, ";
        //sql = sql + " so.Enquiry,so.BoardUniversityRollNo,so.InstituteRollNo,so.SrNo as sn,so.LibraryRequired,so.FileNo,so.Reference,so.Remark,so.GroupNa,SO.SectionId,SO.Card,SO.Medium,sf.FatherOccupation,sf.FatherDesignation,sf.FatherQualification,sf.FatherIncomeMonthly,";
        //sql = sql + " sf.FatherOfficeAddress,sf.FatherContactNo,sf.FatherEmail,sf.MotherOccupation,sf.MotherDesignation,sf.MotherQualification,";
        //sql = sql + " sf.MotherIncomeMonthly,sf.MotherOfficeAddress,sf.MotherContactNo,sf.MotherEmail,sf.FamilyIncomeMonthly,sf.FamilyGuardianName,";
        //sql = sql + " sf.FamilyRelationship,sf.FamilyContactNo,sf.FamilyEmail,Sf.FatherName,SF.MotherName,";
        //sql = sql + " sg.PhyStName,sg.PhyStDetail,sg.SBSrNo,sg.SBStName,sg.SBFathersName,sg.SBClass,sg.SBSection,sg.BloodGroup,sg.Nationality,sg.Religion,sg.Category,sg.Caste,sg.MobileNumber,sg.Email,sg.SiblingCategory,";
        //sql = sql + " sg.PhysicallDisabledCategory,SG.FirstName,SG.MiddleName,convert(nvarchar,SG.DOB,106) as DOB,SG.LastName,sg.StEnRCode as StEnRCode,";
        //sql = sql + " sg.srno as srno,sg.SessionName,SG.StLocalZip,SG.StPerZip,cml.CityName as LocalCity,cmp.CityName  as PerMamentCity,";
        //sql = sql + " sml.StateName as LocalSatateName,smp.StateName as PermanentState,SG.StPerAddress , ";
        //sql = sql + " sp.Board,sp.Medium as med,sp.Marks,sp.Percentage,sp.SchoolName AS PreviousSchoolName,sp.Medium  as PreviousMedium ,sp.Schooladdress as PreviousSchoolAdd,sp.Board as PreviousBoard ,convert(nvarchar,sp.FromDate,106) as FDate,convert(nvarchar,SP.ToDate,106) as TDate ,SP.Class  as PreviousClass, ";
        //sql = sql + " coun.CountryName,cmpPre.CityName  as PreviousCityName ,smprev.StateName as PrevStateName ,case when SO.TransportRequired IS null or SO.TransportRequired='No'  then 'No' else 'Yes' end  as TransportRequired,case when  SO.HostelRequired IS null or SO.HostelRequired='No' then 'No' else 'Yes' end  as  HostelRequired, ";
        //sql = sql + " so.HouseName as HouseName,so.Board as Board1,sd.PhotoPath as PhotoPath ";
        //sql=  sql + " from StudentGenaralDetail SG ";
        //sql = sql + " left join StudentFamilyDetails SF on SG.StEnRCode=SF.StEnRCode";
        //sql = sql + " left join StudentOfficialDetails SO on SG.StEnRCode=SO.StEnRCode";
        //sql = sql + " left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        //sql = sql + " left join StudentDocuments sd on Sg.StEnRCode=sd.StEnRCode";
        //sql = sql + " left join StudentPreviousSchool sp on Sg.StEnRCode=sp.StEnRCode";
        //sql = sql + " left join SectionMaster SC on SO.SectionId=SC.Id";
        //sql = sql + " left join CityMaster cml on SG.StLocalCityId =CMl.Id  ";
        //sql = sql + " left join CountryMaster coun on Sp.CountryId =coun.Id  ";
        //sql=  sql + " left join CityMaster cmp on SG.StPerCityId =CMp.Id  ";
        //sql=  sql + " left join StateMaster  sml on SG.StLocalStateId=sml.Id  ";
        //sql=  sql + " left join StateMaster  smp on SG.StPerStateId =smp.Id  ";
        //sql=  sql + " left join CityMaster cmpPre on sp.CityId =cmpPre.Id  ";
        //sql = sql + " left join StateMaster smprev on sp.StateId=smprev.id   ";  
        //sql = sql + " where SO.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and Sf.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and Sg.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and Sc.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and sd.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and sp.SessionName='" + Session["SessionName"].ToString() + "'";
        //sql = sql + " and So.Srno='" + txtSrNo.Text + "' and (So.Promotion is null or So.Promotion<>'Cancelled')";


        sql = "Select *from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + txtSrNo.Text + "' and (Promotion is null or Promotion<>'Cancelled')";


        if (oo.Duplicate(sql))
        {
            lblStuName.Text = oo.ReturnTag(sql, "Name");
            lblFaName.Text = oo.ReturnTag(sql, "FatherName");
            lblMothName.Text = oo.ReturnTag(sql, "MotherName");
            lblGender.Text = oo.ReturnTag(sql, "Gender");
            lblDob.Text = oo.ReturnTag(sql, "DOB");
            //lblAdmissionType.Text = oo.ReturnTag(sql, "TypeOFAdmision");
            //lblFeeType.Text = oo.ReturnTag(sql, "Card");
            lblBloodGroup.Text = oo.ReturnTag(sql, "BloodGroup");
            lblNationality.Text = oo.ReturnTag(sql, "Nationality");
            lblReligion.Text = oo.ReturnTag(sql, "Religion");
            lblCategeory.Text = oo.ReturnTag(sql, "Category");
            lblCaste.Text = oo.ReturnTag(sql, "stCaste");
            lblMobile.Text = oo.ReturnTag(sql, "MobileNumber");
            lblmail.Text = oo.ReturnTag(sql, "stEmail");
            //--------------------------------------------------------------------
            lblFatherOccu.Text = oo.ReturnTag(sql, "FatherOccupation");
            lblFatherDesi.Text = oo.ReturnTag(sql, "FatherDesignation");
            lblFatherQuali.Text = oo.ReturnTag(sql, "FatherQualification");
            lblFatherInc.Text = oo.ReturnTag(sql, "FatherIncomeMonthly");
            lblFatherOffAdd.Text = oo.ReturnTag(sql, "FatherOfficeAddress");
            lblFatherConNo.Text = oo.ReturnTag(sql, "FatherContactNo");
            lblFathermail.Text = oo.ReturnTag(sql, "FatherEmail");
            lblMotherOccu.Text = oo.ReturnTag(sql, "MotherOccupation");
            lblMotherDesi.Text = oo.ReturnTag(sql, "MotherDesignation");
            lblMotherQuali.Text = oo.ReturnTag(sql, "MotherQualification");
            lblMotherInc.Text = oo.ReturnTag(sql, "MotherIncomeMonthly");
            lblMotherOffAdd.Text = oo.ReturnTag(sql, "MotherOfficeAddress");
            lblMotherConNo.Text = oo.ReturnTag(sql, "MotherContactNo");
            lblMothermail.Text = oo.ReturnTag(sql, "MotherEmail");
            lblFamilyIncMon.Text = oo.ReturnTag(sql, "FamilyIncomeMonthly");
            lblGuardianname.Text = oo.ReturnTag(sql, "FamilyGuardianName");
            lblGuardianRel.Text = oo.ReturnTag(sql, "FamilyRelationship");
            lblGuardianmail.Text = oo.ReturnTag(sql, "FamilyEmail");
            lblGuardianContactNo.Text = oo.ReturnTag(sql, "FamilyContactNo");
            //--------------------------------------------------------------------
            lblStuSrNo.Text = oo.ReturnTag(sql, "sn");
            Label1.Text = oo.ReturnTag(sql, "CountryName");
            Label2.Text = oo.ReturnTag(sql, "Board");
            Label3.Text = oo.ReturnTag(sql, "med");
            Label4.Text = oo.ReturnTag(sql, "Marks");
            Label5.Text = oo.ReturnTag(sql, "Percentage");

            Label6.Text = oo.ReturnTag(sql, "LibraryRequired");
            Label10.Text = oo.ReturnTag(sql, "GroupNa");
            Label12.Text = oo.ReturnTag(sql, "FileNo");
            Label13.Text = oo.ReturnTag(sql, "Reference");
            Label14.Text = oo.ReturnTag(sql, "Remark");

            Label11.Text = oo.ReturnTag(sql, "InstituteRollNo");
            Label7.Text = oo.ReturnTag(sql, "BoardUniversityRollNo");
            Label9.Text = oo.ReturnTag(sql, "Enquiry");
            //---------------------------------------------------------------------
            Label8.Text = oo.ReturnTag(sql, "SiblingCategory");
            lblSiblingSrNo.Text = oo.ReturnTag(sql, "SBSrNo");
            lblSiblingName.Text = oo.ReturnTag(sql, "SBStName");
            lblSiblingFName.Text = oo.ReturnTag(sql, "SBFathersName");
            lblSiblingClass.Text = oo.ReturnTag(sql, "SBClass");
            lblSiblingSection.Text = oo.ReturnTag(sql, "SBSection");
            //---------------------------------------------------------------------
            lblPhysicallyDisabled.Text = oo.ReturnTag(sql, "PhysicallDisabledCategory");
            lblPhysicallyName.Text = oo.ReturnTag(sql, "PhyStName");
            lblPhysicallyDetails.Text = oo.ReturnTag(sql, "PhyStDetail");
            //----------------------------------------------------------------------
            //lblClass.Text = oo.ReturnTag(sql, "ClassName");
            lblStuTempAdd.Text = oo.ReturnTag(sql, "StLocalAddress");
            lblStuPermanentAdd.Text = oo.ReturnTag(sql, "StPerAddress");
            lblStateTemp.Text = oo.ReturnTag(sql, "LocalSatateName");
            lblStatePermanent.Text = oo.ReturnTag(sql, "PermanentState");
            lblCityTemp.Text = oo.ReturnTag(sql, "LocalCity");
            lblPin.Text = oo.ReturnTag(sql, "StLocalZip");
            lblPin0.Text = oo.ReturnTag(sql, "StPerZip");
            lblCityPermanent.Text = oo.ReturnTag(sql, "PerMamentCity");
            lblSchoolName.Text = oo.ReturnTag(sql, "PreviousSchoolName");
            lblAddPrev.Text = oo.ReturnTag(sql, "PreviousSchoolAdd");
            lblSchoolStatePrev.Text = oo.ReturnTag(sql, "PrevStateName");
            lblSchoolCityPrev.Text = oo.ReturnTag(sql, "PreviousCityName");
            lblFormDD.Text = oo.ReturnTag(sql, "FDate");
            lblToDD.Text = oo.ReturnTag(sql, "tdate");
            lblStuLastClass.Text = oo.ReturnTag(sql, "PreviousClass");
            lblSrno.Text = oo.ReturnTag(sql, "srno");
            lblStuClass.Text = oo.ReturnTag(sql, "ClassName");
            lblDate.Text = lblStuAdmission.Text = oo.ReturnTag(sql, "DateOfAdmiission");
            lblSection.Text = oo.ReturnTag(sql, "SectionName");
            lblStuGroup.Text = oo.ReturnTag(sql, "Card");
            lblStuMedium.Text = oo.ReturnTag(sql, "Medium");
            lblTransport.Text = oo.ReturnTag(sql, "TransportRequired");
            lblStuHostel.Text = oo.ReturnTag(sql, "HostelRequired");
            lblHouse.Text = oo.ReturnTag(sql, "HouseName");
            lblAddTypeStu.Text = oo.ReturnTag(sql, "TypeOFAdmision");
            lblStuBoard.Text = oo.ReturnTag(sql, "Board1");
            Image1.ImageUrl = oo.ReturnTag(sql, "PhotoPath");
            Panel1.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
            oo.MessageBox("Invalid S.R. No.!", this.Page);
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //if (GridView2.Rows.Count > 0)
        //{
        //    oo.ExportToWord(Response, "Other Fee Collection.doc", Panel1);
        //}
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        //if (GridView2.Rows.Count > 0)
        //{
        //    oo.ExportToExcel("AdmisionFormCollection.xls", GridView2);
        //}
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
}