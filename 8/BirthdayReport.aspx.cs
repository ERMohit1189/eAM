using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminBirthdayReport : Page
    {
        private DataTable _dt = new DataTable();
        private readonly Campus _oo = new Campus();
        private string _perType = "";
        private string _sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            Campus camp = new Campus(); camp.LoadLoader(loader);
            _perType = !string.IsNullOrEmpty(Request.QueryString["perType"]) ? Request.QueryString["perType"] : "";

            if (!IsPostBack)
            {
                if (_perType == "S")
                {
                    GetStudentReport();
                }
                else
                {
                    GetEmployeeReport();
                }
            }
        }

        private void GetEmployeeReport()
        {
            gvStudent.DataSource = null;
            gvStudent.DataBind();
            lblNorecord.Visible = false;
            _dt = new DataTable();
            _dt = DAL.DALInstance.GetValueInTable("SELECT EFirstName+' '+EMiddleName+' '+ELastName as name,* FROM  EmpGeneralDetail inner join EmpployeeOfficialDetails on EmpployeeOfficialDetails.EmpId=EmpGeneralDetail.EmpId where EmpployeeOfficialDetails.BranchCode=" + Session["BranchCode"] + " and EmpGeneralDetail.BranchCode=" + Session["BranchCode"] + " and convert(varchar(50), DATEPART(day, EDateOfBirth)) + '-' + convert(varchar(50), DATEPART(MONTH, EDateOfBirth)) = convert(varchar(50), DATEPART(day, GETDATE())) + '-' + convert(varchar(50), DATEPART(MONTH, GETDATE()))");
            if (_dt != null && _dt.Rows.Count > 0)
            {
                gvEmployee.DataSource = _dt;
               
            }
            else
            {
                gvEmployee.DataSource = null;
                lblNorecord.Visible = true;
                lblNorecord.Text = "No Birthday(s) found!";
            }
            gvEmployee.DataBind();
        }
        private void GetStudentReport()
        {
            gvEmployee.DataSource = null;
            gvEmployee.DataBind();
            lblNorecord.Visible = false;
            _dt = new DataTable();
            _sql += " SELECT studentOfficialDetails.SrNo, FirstName+' '+MiddleName+' '+LastName as name, StudentFamilyDetails.FatherName, FamilyContactNo, Gender, (ClassName+' '+(case when IsDisplay=0 then '' else BranchName end) +' ('+SectionName+')') as class FROM StudentGenaralDetail ";
            _sql += " inner join StudentFamilyDetails on StudentFamilyDetails.SrNo = StudentGenaralDetail.SrNo and StudentFamilyDetails.SessionName = '"+ Session["SessionName"].ToString() + "' and StudentFamilyDetails.BranchCode=" + Session["BranchCode"] + " ";
            _sql += " inner join studentOfficialDetails on studentOfficialDetails.SrNo = StudentGenaralDetail.SrNo and studentOfficialDetails.SessionName = '"+ Session["SessionName"].ToString() + "' and studentOfficialDetails.BranchCode=" + Session["BranchCode"] + "";
            _sql += " inner join ClassMaster on ClassMaster.id = studentOfficialDetails.AdmissionForClassId and ClassMaster.SessionName = '"+ Session["SessionName"].ToString() + "' and ClassMaster.BranchCode=" + Session["BranchCode"] + "";
            _sql += " inner join SectionMaster on SectionMaster.id = studentOfficialDetails.SectionId and SectionMaster.SessionName = '"+ Session["SessionName"].ToString() + "' and SectionMaster.BranchCode=" + Session["BranchCode"] + "";
            _sql += " inner join BranchMaster on BranchMaster.id = studentOfficialDetails.Branch and BranchMaster.SessionName = '"+ Session["SessionName"].ToString() + "' and BranchMaster.BranchCode=" + Session["BranchCode"] + "";

            _sql += " where convert(varchar(50), DATEPART(day, DOB)) +'-' + convert(varchar(50), DATEPART(MONTH, DOB)) = convert(varchar(50), DATEPART(day, GETDATE())) + '-' + convert(varchar(50), DATEPART(MONTH, GETDATE()))";
            _sql += " and StudentGenaralDetail.SessionName = '"+ Session["SessionName"].ToString() + "' and StudentGenaralDetail.BranchCode=" + Session["BranchCode"] + "";
            _dt = DAL.DALInstance.GetValueInTable(_sql);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                gvStudent.DataSource = _dt;

            }
            else
            {
                gvStudent.DataSource = null;
                lblNorecord.Visible = true;
                lblNorecord.Text = "No Birthday(s) found!";
            }
            gvStudent.DataBind();
        }
    }
}