using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BAL
/// </summary>
public partial class BAL
{
    public class clsCommon
    {
        public Int32 _BranchCode;
        public string _SessionName;
        public string _LoginName;
        public string _LoggedEmpID;

        public Int32 BranchCode { get { return _BranchCode; }  set{ _BranchCode = value; } }
        public string SessionName { get { return _SessionName; } set { _SessionName = value; } }
        public string LoginName { get { return _LoginName; } set { _LoginName = value; } }
        public string LoggedEmpID { get { return _LoginName; } set { _LoginName = value; } }

        public Int32 ClassID { get; set; }
        public Int32 SectionID { get; set; }
        public Int32 BranchID { get; set; }

        public decimal OpeningAmount { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public decimal Amount { get; set; }

        public string RefNo { get; set; }
        public string SrNo { get; set; }
        public string EmpID { get; set; }
        public DateTime FromDate{ get; set; }
        public DateTime ToDate{ get; set; }
        public string ORDERBY { get; set; }

        public string SQL { get; set; }
        public string Remark { get; set; }
        public Int32 IsActive { get; set; }
        public Int32 Status { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public clsCommon()
        {
            if (HttpContext.Current.Session["SessionName"] != null)
            {
                _BranchCode = Convert.ToInt32(HttpContext.Current.Session["BranchCode"].ToString());
                _SessionName = HttpContext.Current.Session["SessionName"].ToString();
                _LoginName = HttpContext.Current.Session["LoginName"].ToString();
               // _LoggedEmpID = HttpContext.Current.Session["LoggedEmpID"].ToString();
            }
            else
            {
                HttpContext.Current.Response.Redirect("../default.aspx");
            }
        }
        
    }
    public string SessioNNames()
    {
        string SessionName = HttpContext.Current.Session["SessionName"].ToString();
        return SessionName;
    }
    public string BranchCodes()
    {
        string BranchCode = HttpContext.Current.Session["BranchCode"].ToString();
        return BranchCode;
    }

    #region TimeTable

    public class clsShiftMaster : clsCommon
    {
        public Int32 A02ID { get; set; }
        public Int32 T01ID { get; set; }
        public Int32 T02ID { get; set; }
        public string ShiftName { get; set; }
        public string ShiftTime { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string AssemblyTime { get; set; }
        public string LunchFromTime { get; set; }
        public string LunchToTime { get; set; }
        public string GraceTime { get; set; }
        public string GraceTimeOut { get; set; }
        public string SlTimeHh { get; set; }
        public string SlTimeMm { get; set; }
        public string SlTimeSs { get; set; }
        public string SlTimeTt { get; set; }

        public string SlTimeHhO { get; set; }
        public string SlTimeMmO { get; set; }
        public string SlTimeSsO { get; set; }
        public string SlTimeTtO { get; set; }
        public string HdTimeHh { get; set; }
        public string HdTimeMm { get; set; }
        public string HdTimeSs { get; set; }
        public string HdTimeTt { get; set; }
        public Int32 DesID { get; set; }
    }

    public class StdShiftMaster : clsCommon
    {
        public string ShiftName { get; set; }
        public string ShiftTimeHH { get; set; }
        public string ShiftTimeMM { get; set; }
        public string ShiftTimeSS { get; set; }
        public string FromTimeHH { get; set; }
        public string FromTimeMM { get; set; }
        public string FromTimeSS { get; set; }
        public string FromTimeTT { get; set; }
        public string ToTimeHH { get; set; }
        public string ToTimeMM { get; set; }
        public string ToTimeSS { get; set; }
        public string ToTimeTT { get; set; }
        public string GraceTimeHH { get; set; }
        public string GraceTimeMM { get; set; }
        public string GraceTimeSS { get; set; }
    }
    public class clsOptionalSubjectManagement : clsCommon
    {
        public Int32 SubjectGroupMaster_ID { get; set; }
        public Int32 SubjectGroupMaster_ID_Opt { get; set; }
    }   

    public class clsGenerateTimeTable
    {
        public Int32 T01ID { get; set; }
        public string GivenWorkingDays { get; set; }
        public string LunchTimeValue { get; set; }
        public Int32 LunchAfterPeriod { get; set; }
        public string DairyTimeValue { get; set; }
        public Int32 DairyAfterPeriod { get; set; }
        public Int32 GivenDayPeriod { get; set; }
        public Int32 GivenTeacherDayPeriod { get; set; }
        public Int32 GivenContinuousPeriod { get; set; }
        public string GivenPerPeriodTime { get; set; }
        public Int32 IsFirstClassTeacher { get; set; }
        public string SessionName { get; set; }
        public string LoginName { get; set; }
        public Int32 BranchCode { get; set; }
        public Int32 IsShiftWise { get; set; }

        public string OPeriodTimeValue { get; set; }
        public Int32 OPeriodAfterPeriod { get; set; }
    }
    public class clsSubjectPaperMaster:clsCommon
    {
        public Int32 S02ID { get; set; }
        public Int32 IsForExam { get; set; }
       
        public string SubjectPaperName{ get; set; }
        public Int32 WeekPeriod { get; set; }
        public Int32 SubjectGroupID { get; set; }

        //public string SQL { get; set; }
        //public string SessionName { get; set; }
        //public string LoginName { get; set; }
        //public Int32 BranchCode { get; set; }
    }

    public class clsOPeriodSubject : clsCommon
    {
        public Int32 T07ID { get; set; }
        public Int32 S02ID { get; set; }
        public string SelectedDay { get; set; } 
    }

    public class clsManualReplacemnet : clsCommon
    {
        public Int32 PeriodNo { get; set; }
        public string SelectedDay { get; set; }
    }

    #endregion


    #region MessegeTemplate

    public class clsNotificationTemplate
    {
        public Int32 M01ID { get; set; }
        public string NotificationType { get; set; }
        public Int32 NotificationID { get; set; }
        public Int32 IsUnicode { get; set; }
        public string Template { get; set; }

        public string SQL { get; set; }
        public string SessionName { get; set; }
        public string LoginName { get; set; }
        public Int32 BranchCode { get; set; }
    }

    #endregion 


    #region Accounting

    public class clsHeadType:clsCommon
    {
        public Int32 H01ID { get; set; }
        public Int32 IsAutomatic { get; set; }
        public string HeadType { get; set; }
        public string HeadCode { get; set; }
        public string HeadMode { get; set; }
    }

    public class clsHeadMaster : clsHeadType
    {
        public Int32 H02ID { get; set; }
        public string HeadName { get; set; }
        public string HeadCategory { get; set; }
    }

    public class clsDayBook : clsHeadMaster
    {
        public Int32 H03ID { get; set; }
        //public decimal Amount { get; set; }
        public Int32 IsAdvance { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Int32 IsRecurringAmount { get; set; }

        public Int32 H05ID { get; set; } 
        //public string EmpID { get; set; }
        public Int32 SalaryMonth { get; set; }

        public Int32 H04ID { get; set; }
        public Int32 M06ID { get; set; }
        public Int32 M02ID { get; set; }
        public Int32 M09ID { get; set; }
        public string PaymentNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DDChequeUTRDate { get; set; }
        public string DDChequeUTRNo { get; set; }
        public string FromAccount { get; set; }
        public string BeneficiaryAccount { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceUrl { get; set; }
        public List<clsVendorPayment> lstVendorPayment { get; set; }
    }

    public class clsVendorPayment : clsCommon
    {
        public string VendorID { get; set; }
        public Int32 V01ID { get; set; }
        public Int32 V04ID { get; set; }

        public decimal FinalAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal TDSPer { get; set; }

        public Int32 IsQuotation { get; set; }
    }

    #endregion


    #region Master

    public class clsBankMaster:clsCommon
    {
        public Int32 M02ID { get; set; }
        public string BankName { get; set; }
        public Int32 HaveBranch { get; set; }
    }

    public class clsBankBranchMaster : clsBankMaster
    {
        public Int32 M03ID { get; set; }
        public string BankBranchName { get; set; }
        public string Address { get; set; }
        public string IFSC { get; set; }
        public string PIN { get; set; }
    }

    public class clsBankAcc : clsBankBranchMaster
    {
        public Int32 M09ID { get; set; }
        //public string AccountNo { get; set; }
        //public string AccountName { get; set; }
        //public string AccountType { get; set; }
    }

    public class clsVendorType : clsCommon
    {
        public Int32 M04ID { get; set; }
        public string VendorType { get; set; }
    }

    public class clsOrganizationType : clsCommon
    {
        public Int32 M05ID { get; set; }
        public string OrganizationType { get; set; }
    }

    public class clsCountryMaster : clsCommon
    {
        public Int32 CountryID { get; set; }
    }

    public class clsStateMaster : clsCountryMaster
    {
        public Int32 StateID { get; set; }
    }

    public class clsDistrictMaster : clsStateMaster
    {
        public Int32 DistrictID { get; set; }
    }

    public class clsPaymentMode : clsCommon
    {
        public Int32 M06ID { get; set; }
    }

    public class clsInstituteAccount : clsBankBranchMaster
    {
        public Int32 M07ID { get; set; }
    }

    #endregion 


    #region Vendor

    public class clsVendor : clsCommon
    {    
        public Int32 V01ID { get; set; }
        public Int32 M04ID { get; set; }
        public Int32 M05ID { get; set; }

        public string VendorID { get; set; }
        public string RegistrationNo { get; set; }
        public string OrganizationName { get; set; }
        public string OwnerName { get; set; }
        public string DisplayName { get; set; }
        public string DOR { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string TIN { get; set; }
        public string ServiceTaxNo { get; set; }

        public string ContactPerson { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string MailID { get; set; }
        public bool IsWhatsApp { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public Int32 CountryID { get; set; }
        public Int32 StateID { get; set; }
        public Int32 DistrictID { get; set; }
        public string PIN { get; set; }
    }

    public class clsVendorDocument : clsVendor
    {
        public Int32 V02ID { get; set; }
        public string DocumentName { get; set; }
        public Int32 IsInvoice { get; set; }
    }

    public class clsVendorBank : clsVendorDocument
    {
        public Int32 V03ID { get; set; }
        public Int32 M02ID { get; set; }
        public Int32 M03ID { get; set; }
        //public string AccountNo { get; set; }
        //public string AccountType { get; set; }
        //public string AccountName { get; set; }
        public Decimal OpeningBalanceAmount { get; set; }
    }

    public class clsVendorQuotation : clsVendor
    {
        public Int32 H06ID { get; set; }
        public Int32 V04ID { get; set; }
        public string Overview { get; set; }
        public string QuotationFor { get; set; }
        public string QuotationNo { get; set; }
        public string Reason { get; set; }
        //public decimal Amount { get; set; }
        public decimal RecurringAmount { get; set; }
        public string QRefNo { get; set; }
        public Int32 IsInvoicGenerated { get; set; }
        public Int32 IsQuotation { get; set; }
        public Int32 V04V05ID { get; set; }
        public Int32 IsPaid { get; set; }
        public Int32 HaveBalance { get; set; }
        public DateTime Date { get; set; }

        public char SubmissionType { get; set; }
        public Int32 IsDirectInvoice { get; set; }  
    }

    public class clsVendorInvoice : clsVendorQuotation
    {
        public Int32 V05ID { get; set; }
        public string InvoiceNo { get; set; }
        public string IRefNo { get; set; }
    }

    #endregion 


    #region EmpAttendance

    public class clsSearchAttendance : clsCommon
    {
        public string AttendanceType { get; set; }
        public string Designation { get; set; }
    }

    public class clsAttendanceShift : clsCommon
    {
        public Int32 A02ID { get; set; }
        public string ShiftName { get; set; }
        public string ShotName { get; set; }
        public string FromTimeShift { get; set; }
        public string ToTimeShift { get; set; }
        public string ShiftTime { get; set; }
        public string FromTimeLunch { get; set; }
        public string ToTimeLunch { get; set; }
        public string LunchTime { get; set; }
        public string NotificationTime { get; set; }
        public Int32 GraceTimeInMinute { get; set; }
        public Int32 IsEarlyPunchAllowed { get; set; }
        public Int32 IsAutoSendNotification { get; set; }
    }

    public class clsNotificationDate : clsAttendanceShift
    {
        public Int32 A03ID { get; set; }
        public DateTime DateValue { get; set; }
    }

    public class clsNotificationType : clsAttendanceShift
    {
        public string AttendanceType { get; set; }
    }

    public class clsEmpLeave : clsCommon
    {
        public Int32 A03ID { get; set; }
        //public string EmpID { get; set; }
        public string AppSubject { get; set; }
        public string AppReason { get; set; }
        public string Address { get; set; }
        public string ContactNo2 { get; set; }
        public string ContactNo1 { get; set; }
        public string ApproveEmpID { get; set; }
        public DateTime AppDate { get; set; }
        //public DateTime FromDate { get; set; }
        //public DateTime ToDate { get; set; }
        public DateTime ApproveDate { get; set; }
        public decimal LeaveDays { get; set; }
        public Int32 IsHalfDay { get; set; }
        public Int32 HalfDayType { get; set; }
    }

    #endregion


    #region PaymentAPI

    public class clsAPITransaction : clsCommon
    {
        public Int32 F01ID { get; set; }
        public string TxnID { get; set; }
        public decimal Charges { get; set; }
        public string FromTimeShift { get; set; }
        public string Mode { get; set; }
        public string PayUMoneyID { get; set; }
        public string PGType { get; set; }
        public string BankRefNo { get; set; }
        public string Error { get; set; }
    }

    public class ClsApiTransactionAdmission
    {
        public string SrNo { get; set; }
        public decimal Amount { get; set; }
        public Int32 Status { get; set; }
        public string Sql { get; set; }
        public Int32 F01Id { get; set; }
        public string TxnId { get; set; }
        public decimal Charges { get; set; }
        public string FromTimeShift { get; set; }
        public string Mode { get; set; }
        public string PayUMoneyId { get; set; }
        public string PgType { get; set; }
        public string BankRefNo { get; set; }
        public string Error { get; set; }
    }

    #endregion


    #region HR

    public class clsSalaryComponent : clsCommon
    {
        public Int32 HR01ID { get; set; }
        public string Component { get; set; }
        public string ComponentType { get; set; }      
        public Int32 IsGross { get; set; }
        public Int32 IsOther { get; set; }
        public Int32 DesignationID { get; set; }
        public Int32 IsBasic { get; set; }
    }

    public class clsComponentValue : clsSalaryComponent
    {
        public Int32 IsPer { get; set; }
        public decimal ComponentValue { get; set; }
        public string PartOf { get; set; }  
    }

    #endregion
}

public enum Default
{
    All = -1,
    Yes=1,
    No=0,
    Pending=2
}

