using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for BAL
/// </summary>


public partial class BAL : Campus
    {
        public static readonly BAL objBal = new BAL();

        public class GenralInfo : Message
        {
            public string SessionName { get; set; }
            public int BranchCode { get; set; }
            public string LoginName { get; set; }
            public int ClassId { get; set; }
            public string ClassName { get; set; }
            public int SectionId { get; set; }
            public string SectionName { get; set; }
            public string Medium { get; set; }
            public string CardType { get; set; }
            public bool IsActive { get; set; }
            public string SrNo { get; set; }
            public string StEnRCode { get; set; }
            public string Queryfor { get; set; }
            public int id { get; set; }
            public string Remark { get; set; }
        }

        public class GET_ClassGroupMaster : GenralInfo
        {
            public string GroupId { get; set; }
        }

        public class SET_ClassGroupMaster : GenralInfo
        {
            public string GroupId { get; set; }
        }

        public class DepositedUnclearedChequeorDD
        {
            public string Session { get; set; }
            public int BranchCode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
        }

        public class Set_Headertemplate
        {
            public int Id { get; set; }
            public string Template { get; set; }
            public string TemplateFor { get; set; }
            public int IsAvtive { get; set; }
            public string Session { get; set; }
            public int BranchCode { get; set; }
            public string LoginName { get; set; }
            public bool Isdisplaylogo { get; set; }
        }

        public class Get_Headertemplate
        {
            public string TemplateFor { get; set; }
            public string Session { get; set; }
            public int BranchCode { get; set; }
            public bool Isdisplaylogo { get; set; }
        }

        public class Get_Header
        {
            public string TemplateFor { get; set; }
        }

        public class Set_DocumentName
        {
            public string Type { get; set; }
            public string Srno { get; set; }
            public int Id { get; set; }
            public string DocumentType { get; set; }
            public int ClassId { get; set; }
            public string ClassName { get; set; }
            public string Session { get; set; }
            public int BranchCode { get; set; }
            public string LoginName { get; set; }
        }

        public class Staff_Document
        {
            public string Type { get; set; }
            public string Empid { get; set; }
            public int Id { get; set; }
            public string DocumentType { get; set; }
            public string Session { get; set; }
            public int BranchCode { get; set; }
            public string LoginName { get; set; }
            public string DocName { get; set; }
            public string DocId { get; set; }
            public string DocPath { get; set; }
            public string EmpCode { get; set; }

        }

        public class textBoxList
        {
            public string txt1 { get; set; }
            public string txt2 { get; set; }
            public string txt3 { get; set; }
            public string txt4 { get; set; }
            public string txt5 { get; set; }
            public int Noofnoncleartxt { get; set; }
        }

        public class Set_StudentDocumentRecord
        {
            public string DocId { get; set; }
            public string DocName { get; set; }
            public string DocPath { get; set; }
            public string SrNo { get; set; }
            public string StEnRCode { get; set; }
            public string Session { get; set; }
            public int BranchCode { get; set; }
            public string LoginName { get; set; }
            public int Softcopy { get; set; }
            public int Hardcopy { get; set; }
            public string Remark { get; set; }
            public int Varified { get; set; }
        }

        public class Get_StudentDataForPromotionCancelation
        {
            public string Srno { get; set; }
            public string SessionName { get; set; }
            public string ClassName { get; set; }
        }

        public class Set_SetDefaultName : GenralInfo
        {
            public string replacewidth { get; set; }
            public string replace { get; set; }
        }

        public class Set_DefaultSelectedValue : GenralInfo
        {
            public string defaultvalueof { get; set; }
            public string defaultvalue { get; set; }
        }

        public class Set_RulesForSibling : GenralInfo
        {
            public string rule1 { get; set; }
            public string rule2 { get; set; }
            public int noofSibling { get; set; }
            public string discountType { get; set; }
            public int discountValue { get; set; }
        }

        public class Set_DiscountHead : GenralInfo
        {
            public string ParentHeadvalue { get; set; }
            public string HeadName { get; set; }
            //public string id { get; set; }
            public int NoofSibling { get; set; }
        }

        public class RulesForDiscount : GenralInfo
        {
            public int DiscountHeadId { get; set; }
            public string GenderValue { get; set; }
            public string CategoryValue { get; set; }
            public string Installment { get; set; }
            public double Amount{get; set;}
        }

        public class FeeHeadCategoryMaster : GenralInfo
        {
            public string FeeHeadCategory { get; set; }
            //public string Remark { get; set; }
        }

        public class Employmentform : GenralInfo
        {
            public string RecieptNo { get; set; }
            public string EmpName { get; set; }
            public string EmpFather { get; set; }
            public string EmpGender { get; set; }
            public string EmpContactNo { get; set; }
            public int EmpDesignation { get; set; }
            public int EmpAmount { get; set; }
            public int EmployeeFormId { get; set; }
            public string HuabandName { get; set; }
            public int EmploymenttypeId { get; set; }
            public string SubjectIds { get; set; }
            public string Email { get; set; }
            public string IsCancel { get; set; }
            public string EFDate { get; set; }
        }

        public class EducationType : GenralInfo
        {
            public string EducationTypes { get; set; }
            public int FromClass { get; set; }
            public int ToClass { get; set; }
        }

        public class SubjectForEmploymentForm : GenralInfo
        {
            public int EducationTypes { get; set; }
            public string Subject { get; set; }
        }

        public class AssociateOrganizatinDeails : GenralInfo
        {
            //public int id { get; set; }
            public string OrganizationName { get; set; }
            //public string Remark { get; set; }
        }

    
    }


