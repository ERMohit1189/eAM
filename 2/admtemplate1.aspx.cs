using System;
using System.Web.UI;

namespace _2
{
    public partial class admtemplate1 : Page
    {
        string _sql = "";
        public static string Sex="";
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["rid"]))
            {
                if (Session["AdmissionRecieptNo"] == null)
                {
                    Response.Redirect("AdmissionForm.aspx");
                }
                if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
                {
                    Response.Redirect("default.aspx");
                }
            }
            else
            {
                MasterPageFile = "~/ap/main-ap.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["rid"]))
            {
                if ((string) Session["LoginName"] == "" || (string) Session["BranchCode"] == "" || (string) Session["SessionName"] == "" ||
                    Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
            }

            BLL.BLLInstance.LoadHeader("Report", header);
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["rid"]))
                {
                    LoadSchoolDetails();
                    StudentRecord((string)Session["AdmissionRecieptNo"]);
                }
                else
                {
                    StudentRecord(Request.QueryString["rid"]);
                }
            }
        }
    
        private void LoadSchoolDetails()
        {
            _sql = "Select SchoolNo,AffiliationNo,CollegeName,CollegeAdd1,CollegeAdd2 from CollegeMaster where Collegeid=" + Session["BranchCode"] + "";
            //lblAffiliationNo.Text = BAL.objBal.ReturnTag(sql, "AffiliationNo").Trim();
            //lblSchoolCodeNo.Text = BAL.objBal.ReturnTag(sql, "SchoolNo").Trim();
            //lblSchoolName.Text = BAL.objBal.ReturnTag(sql, "CollegeName").Trim();
            //lblAddress1.Text = BAL.objBal.ReturnTag(sql, "CollegeAdd1").Trim();
            //lblAddress2.Text = BAL.objBal.ReturnTag(sql, "CollegeAdd2").Trim();
        }

        private void StudentRecord(string admissionRecieptNo)
        {
            //sql = "Select Id as Srno,Class,Branch,(StudentName+case when MiddleName=' ' then ' ' else ' '+MiddleName+' ' End+LastName) as StudentName,Sex,SessionFor";
            //sql = sql + " from AdmissionFormCollection where RecieptNo='" + Session["AdmissionRecieptNo"].ToString() + "'";

            _sql = "select afc.Id as Srno,Class,bm.BranchName Branch,StudentName+' '+MiddleName +' '+LastName as StudentName,Sex,SessionFor,afc.txnid,afc.AdmissionType ";
            _sql = _sql + " from AdmissionFormCollection afc";
            _sql = _sql + " Inner join ClassMaster cm on cm.ClassName=afc.Class and cm.SessionName=afc.SessionName";
            _sql = _sql + " Left join BranchMaster bm on bm.BranchName=afc.Branch and bm.Classid=cm.Id and bm.SessionName=afc.SessionName and IsDisplay='1'";
            _sql = _sql + " where RecieptNo='" + admissionRecieptNo + "'";

            ViewState["txnid"] = BAL.objBal.ReturnTag(_sql, "txnid").Trim();
            lblSrno.Text = BAL.objBal.ReturnTag(_sql, "Srno").Trim();
            string branch = BAL.objBal.ReturnTag(_sql, "Branch") == "" ? "" : "( " + BAL.objBal.ReturnTag(_sql, "Branch") + " )";
            lblClass.Text = BAL.objBal.ReturnTag(_sql, "Class").Trim() +" "+ branch;
            lblStudentName.Text = BAL.objBal.ReturnTag(_sql, "StudentName").Trim();
            lblSessionFor.Text = BAL.objBal.ReturnTag(_sql, "SessionFor").Trim();
            Sex = BAL.objBal.ReturnTag(_sql, "Sex").ToUpper().Trim();
            if (Sex == "MALE")
            {
                male.Attributes.Add("class", "icon-checkmark");
                female.Attributes.Add("class", "sex-check-box-blank");
                span1.Attributes.Add("class", "sex-check-box5 male");
                span2.Attributes.Add("class", "sex-check-box10 female");
            }
            else if (Sex == "FEMALE")
            {
                male.Attributes.Add("class", "sex-check-box-blank");
                female.Attributes.Add("class", "icon-checkmark");
                span1.Attributes.Add("class", "sex-check-box10 male");
                span2.Attributes.Add("class", "sex-check-box5 female");
            }
            var admissionType = BAL.objBal.ReturnTag(_sql, "AdmissionType").Trim();
            if (admissionType == "New (Provisional)")
            {
                lblAdmissionType.Text = admissionType.Replace("New", "");
            }
            else
            {
                lblAdmissionType.Text = "";
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void lnkback_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["rid"]))
            {
                lnkback.PostBackUrl = "~/2/paf.aspx";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "redirect", "window.open('../ap/Admission_Details.aspx?txtno=" +
                                                                                     ViewState["txnid"] + "','_self')", true);
            }
        }
    }
}