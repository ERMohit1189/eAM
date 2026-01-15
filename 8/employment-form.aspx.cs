using System;

public partial class admin_employment_form : System.Web.UI.Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["RecieptNo"]==null)
        {
            Response.Redirect("~/default.aspx");
        }
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadSchoolDetails();
            EmpRecord();
        }
    }

    private void loadSchoolDetails()
    {
        sql = "Select SchoolNo,Phone,CollegeName,CollegeAdd1,CollegeAdd2 from CollegeMaster where Collegeid=" + Session["BranchCode"].ToString() + "";
        //lblContactNo.Text = BAL.objBal.ReturnTag(sql, "Phone").Trim();
        //lblSchoolName.Text = BAL.objBal.ReturnTag(sql, "CollegeName").Trim();
        //lblAddress1.Text = BAL.objBal.ReturnTag(sql, "CollegeAdd1").Trim();
        //lblAddress2.Text = BAL.objBal.ReturnTag(sql, "CollegeAdd2").Trim();
    }

    private void EmpRecord()
    {
        sql = "Select Distinct EmployeeFormId as Srno,EmpName,EmpFather,EmpGender,EmpContactNo,edm.DesName EmpDesName,EducationType,Convert(varchar(11),EFDate,106) as Date from Employmentform  ef";
        sql +=  " Inner join DesMaster edm on edm.DesId=EmpDesignation INNER JOIN tbl_EducationType etp ON etp.id = EmploymenttypeId and etp.BranchCode=ef.BranchCode where RecieptNo='" + Session["RecieptNo"].ToString() + "' and  ef.BranchCode=" + Session["BranchCode"].ToString() + " and edm.BranchCode=" + Session["BranchCode"].ToString() + "";
        lblDesi.Text = BAL.objBal.ReturnTag(sql, "EmpDesName").Trim()+" - "+ BAL.objBal.ReturnTag(sql, "EducationType").Trim();
        lblSrno.Text = BAL.objBal.ReturnTag(sql, "Srno").Trim();
        lblEmpName.Text = BAL.objBal.ReturnTag(sql, "EmpName").Trim();
        lblFatherName.Text = BAL.objBal.ReturnTag(sql, "EmpFather").Trim();
        lblGender.Text = BAL.objBal.ReturnTag(sql, "EmpGender").Trim();
        lblMobileNo.Text = BAL.objBal.ReturnTag(sql, "EmpContactNo").ToUpper().Trim();
        lblDate.Text = BAL.objBal.ReturnTag(sql, "Date").ToUpper().Trim();
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}