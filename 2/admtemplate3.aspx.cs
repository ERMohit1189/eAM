using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _2_admtemplate3 : System.Web.UI.Page
{
    string _sql = "";
    public static string Sex = "";
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
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" ||
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
    }
    public void dateFormate(ref string day)
    {
        if (day == "1" || day == "21" || day == "31")
        {
            day = day + "st";
        }
        else if (day == "2" || day == "22")
        {
            day = day + "nd";
        }
        else if (day == "3" || day == "23")
        {
            day = day + "rd";
        }
        else if (day == "4" || day == "5" || day == "6" || day == "7" || day == "8" || day == "9" || day == "10" || day == "11" || day == "12" || day == "13" || day == "14" || day == "15" || day == "16" || day == "17" || day == "18" || day == "19" || day == "20" || day == "24" || day == "25" || day == "26" || day == "27" || day == "28" || day == "29" || day == "30")
        {
            day = day + "th";
        }
    }
    private void StudentRecord(string admissionRecieptNo)
    {
        _sql = "select afc.Id as Srno,StudentName as FirstName,MiddleName,LastName,   Class,bm.BranchName Branch,StudentName+' '+MiddleName +' '+LastName as StudentName,Sex,SessionFor,afc.txnid,afc.AdmissionType, dob ";
        _sql += " from AdmissionFormCollection afc";
        _sql += " Inner join ClassMaster cm on cm.ClassName=afc.Class and cm.SessionName=afc.SessionName";
        _sql += " Left join BranchMaster bm on bm.BranchName=afc.Branch and bm.Classid=cm.Id and bm.SessionName=afc.SessionName and IsDisplay='1'";
        _sql += " where RecieptNo='" + admissionRecieptNo + "'";

        ViewState["txnid"] = BAL.objBal.ReturnTag(_sql, "txnid").Trim();
        lblSrno.Text = BAL.objBal.ReturnTag(_sql, "Srno").Trim();
        string branch = BAL.objBal.ReturnTag(_sql, "Branch") == "" ? "" : "( " + BAL.objBal.ReturnTag(_sql, "Branch") + " )";
       lblClass.Text = BAL.objBal.ReturnTag(_sql, "Class").Trim() + " " + branch;
       // lblStudentName.Text = BAL.objBal.ReturnTag(_sql, "StudentName").Trim();
      //  lblSessionFor.Text = BAL.objBal.ReturnTag(_sql, "SessionFor").Trim();
        Sex = BAL.objBal.ReturnTag(_sql, "Sex").ToUpper().Trim();
        string firstName = BAL.objBal.ReturnTag(_sql, "FirstName").Trim();
        foreach (char c in firstName)
        {
            // Create a new div for each character
            Literal lit = new Literal();
            lit.Text = "<div class='box'>" + c + "</div>";
            phFirstName.Controls.Add(lit);
        }
        string MiddleName = BAL.objBal.ReturnTag(_sql, "MiddleName").Trim();
        foreach (char c in MiddleName)
        {
            // Create a new div for each character
            Literal lit = new Literal();
            lit.Text = "<div class='box'>" + c + "</div>";
            PlaceHolder1.Controls.Add(lit);
        }
        string LastName = BAL.objBal.ReturnTag(_sql, "LastName").Trim();
        foreach (char c in LastName)
        {
            // Create a new div for each character
            Literal lit = new Literal();
            lit.Text = "<div class='box'>" + c + "</div>";
            PlaceHolder2.Controls.Add(lit);
        }
        if (Sex== "MALE")
        {
            chkMale.Checked = true;
        }
        else
        {
            chkFemale.Checked = true;
        }
        var dob = BAL.objBal.ReturnTag(_sql, "dob").ToUpper().Trim();
      //  lblGender.Text = Sex;
      //  lbldob.Text = dob;
        if (dob != "")
        {

            string _sqlss = "Select DatePart(dd,'" + dob + "') as date,DateName(mm,'" + dob + "') as month,DatePart(yyyy,'" + dob + "') as year";
            string day = BAL.objBal.ReturnTag(_sqlss, "date");
            dateFormate(ref day);
            string year = BAL.objBal.ReturnTag(_sqlss, "year");
            string yearpri = "";
            string yearsuf = "";
            if (Convert.ToInt16(year) < 2000)
            {
                yearpri = BAL.objBal.Date_in_Words(Convert.ToInt16(year.Substring(0, 2)));
                yearsuf = BAL.objBal.Date_in_Words(Convert.ToInt16(year.Substring(2, 2)));
                year = yearpri + " Hundred " + yearsuf;
            }
            else
            {
                year = BAL.objBal.Date_in_Words(Convert.ToInt16(BAL.objBal.ReturnTag(_sqlss, "year")));
            }
            string Date_in_Word = BAL.objBal.Date_in_Words(Convert.ToInt16(BAL.objBal.ReturnTag(_sqlss, "date"))) + " " + BAL.objBal.ReturnTag(_sqlss, "month") + " " + year;
            dobInBirrd.InnerHtml = Date_in_Word;
        }






        var admissionType = BAL.objBal.ReturnTag(_sql, "AdmissionType").Trim();
        if (admissionType == "New (Provisional)")
        {
            //lblAdmissionType.Text = admissionType.Replace("New", "");
        }
        else
        {
          //  lblAdmissionType.Text = "";
        }
    }

    //protected void lnkPrint_Click(object sender, EventArgs e)
    //{
    //    PrintHelper_New.ctrl = abc;
    //    ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    //}
    protected void lnkback_Click(object sender, EventArgs e)
    {
        lnkback.PostBackUrl = "~/2/paf.aspx";
    }
}