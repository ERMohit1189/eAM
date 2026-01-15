using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminListofStaffInformation : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
#pragma warning disable 169
        private DataTable _dt;
#pragma warning restore 169

        public AdminListofStaffInformation()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Campus camp = new Campus(); camp.LoadLoader(loader); 

            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            _con = _oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Report", header1);
            if (!IsPostBack)
            {
                LoadEmpOfficialDetails();
                LoadEmpGenralDetails();
                LoadEmpRecord();
                LoadBoard();
                _sql = "select top(1) IsAadhaar from setting";
                if (_oo.ReturnTag(_sql, "IsAadhaar") == "")
                {
                    lblidentityNo.Text = "Identity No.";
                }
                else
                {
                    lblidentityNo.Text = _oo.ReturnTag(_sql, "IsAadhaar");
                }
            }


        }

        public void LoadEmpRecord()
        {
           

            _sql = " select Eo.MachineId,Eo.EmpCategory,EO.Ecode,EO.EmpId, (EPerAdd+', '+ CityName+', '+StateName+case when isnull(PinCode, '')='' then '' else '-'+PinCode end) addr, ";
            _sql += " EG.EFirstName +' '+ EG.EMiddleName +' '+ EG.ELastName as Name, format(EG.EDateOfBirth, 'dd-MMM-yyy') EDateOfBirth,EG.EFatherName, EG.SpouseName, EG.EMobileNo,AadharNo, PANno,EG.EPreCityId as EPreCityId,eo.Designation,Eo.DepartmentName,convert(nvarchar,EO.RegistrationDate,106) as JoiningDate";
            _sql += " ,EO.TrainingDetails, EO.TeachingSubjects,DesNameNew, EG.EGender,EG.EEmail, EO.PFNo,EO.EsicNo, EO.machinNo, EO.UAN  from EmpployeeOfficialDetails EO";
            _sql += " inner join EmpGeneralDetail EG on EO.Ecode=EG.Ecode and EO.BranchCode=EG.BranchCode";
            _sql += " inner join EmpEmployeeDetails ED on ED.Ecode=EG.Ecode and ED.BranchCode=EG.BranchCode";
            _sql += " left join StateMaster sm on sm.id=EPerStateId";
            _sql += " left join CityMaster cmm on cmm.id=EPerCityId";
            _sql += " and eo.Withdrwal is null and eo.BranchCode=" + Session["BranchCode"] + "";
            _sql += " WHERE EO.EmpId not in (SELECT Empid FROM EmpWithdrawlRecord where BranchCode=" + Session["BranchCode"].ToString() + ") and EO.BranchCode=" + Session["BranchCode"].ToString() + " ";
            _sql += (drpDepartment.SelectedIndex != 0 ? (" and EO.DepartmentName='" + drpDepartment.SelectedItem.Text + "' ") : "");
            _sql += (drpDesignation.SelectedIndex != 0 ? (" and EO.Designation='" + drpDesignation.SelectedItem.Text + "' ") : "");
            _sql += (ddlEmpDeg.SelectedIndex != 0 ? (" and EO.DesNameNew='" + ddlEmpDeg.SelectedItem.Text + "' ") : "");

            switch (RadioButtonList2.SelectedIndex)
            {
                case 0:
                    _sql += " order by EG.EFirstName";
                    break;
                case 2:
                    _sql += " order by Eo.MachineId";
                    break;
                case 3:
                    _sql += " order by Eo.Designation";
                    break;
            }

            rptStudents.DataSource = _oo.GridFill(_sql);
            rptStudents.DataBind();

            Label1.Text = "List of Staff (Department-" + (drpDepartment.SelectedIndex == 0 ? "All" : drpDepartment.SelectedItem.Text) + ") (Designation-" + (drpDesignation.SelectedIndex == 0 ? "All" : drpDesignation.SelectedItem.Text) + ") (Shift Category-" + (ddlEmpDeg.SelectedIndex == 0 ? "All" : ddlEmpDeg.SelectedItem.Text) + ")";
            if (rptStudents.Items.Count>0)
            {
                for (int i = 0; i < rptStudents.Items.Count; i++)
                {
                    Label LabelEmpId = (Label)rptStudents.Items[i].FindControl("LabelEmpId");
                    Repeater rptQualification = (Repeater)rptStudents.Items[i].FindControl("rptQualification");
                    _sql = "Select count(*) cnt from EmployeePreviousSchool where SrNo='"+ LabelEmpId.Text + "' and  BranchCode=" + Session["BranchCode"].ToString() + "";
                    string countss = _oo.ReturnTag(_sql, "cnt");
                    
                    if (countss == "0")
                    {
                        _sql = "Select '' Qualification1, '' Qualification2, '' Qualification3, '' Qualification4";
                        var ds = _oo.GridFill(_sql);
                        if (ds != null)
                        {
                            Div1.Visible = true;
                            rptQualification.DataSource = ds;
                            rptQualification.DataBind();
                            UpdatePanel11.Visible = true;

                        }
                        else
                        {
                            rptQualification.DataSource = null;
                            rptQualification.DataBind();
                            Div1.Visible = false;
                            UpdatePanel11.Visible = false;
                        }
                    }
                    else {
                        _sql = "Select Qualification +' '+ case when ISNULL(Subjects, '')='' then '' else '('+ Subjects+') from 'end +Board+ case when ISNULL(Yop, '')='' then '' else ' in '+Yop+'.' end as Quali from EmployeePreviousSchool where  SrNo='" + LabelEmpId.Text + "' and  BranchCode=" + Session["BranchCode"].ToString() + " order by id asc";
                        var ds = _oo.GridFill(_sql);
                        if (ds!=null)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Qualification1");
                            dt.Columns.Add("Qualification2");
                            dt.Columns.Add("Qualification3");
                            dt.Columns.Add("Qualification4");

                            DataRow dr= dt.NewRow();
                            dr["Qualification1"]= ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["Quali"].ToString() : "";
                            dr["Qualification2"]= ds.Tables[0].Rows.Count > 1 ? ds.Tables[0].Rows[1]["Quali"].ToString() : "";
                            dr["Qualification3"]= ds.Tables[0].Rows.Count > 2 ? ds.Tables[0].Rows[2]["Quali"].ToString() : "";
                            dr["Qualification4"]= ds.Tables[0].Rows.Count > 3 ? ds.Tables[0].Rows[3]["Quali"].ToString() : "";
                            dt.Rows.Add(dr);
                            rptQualification.DataSource = dt;
                            rptQualification.DataBind();
                        }
                    }
                }
            }
        }

       
        protected void LoadBoard()
        {
            _sql = "Select BoardName from BoardMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown(_sql, ddlboard, "BoardName");
        }
        private void LoadEmpOfficialDetails()
        {
            _sql = "Select EmpDepName,EmpDepId from EmpDepMaster where  BranchCode=" + Session["BranchCode"].ToString() + " ";
            _oo.FillDropDown_withValue(_sql, drpDepartment, "EmpDepName", "EmpDepId");
            drpDepartment.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _sql = "Select EmpDesName,EmpDesId from EmpDesMaster where  BranchCode=" + Session["BranchCode"].ToString() + " ";
            _oo.FillDropDown_withValue(_sql, drpDesignation, "EmpDesName", "EmpDesId");
            drpDesignation.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _sql = "Select DesName,DesId from DesMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown_withValue(_sql, ddlEmpDeg, "DesName", "DesId");
            ddlEmpDeg.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _sql = "select EmployeeCategoryName,EmployeeCategoryId from EmployeeCategoryMaster where  BranchCode=" + Session["BranchCode"].ToString() + "";
            _oo.FillDropDown_withValue(_sql, drpEmployeeCategory, "EmployeeCategoryName", "EmployeeCategoryId");
            drpEmployeeCategory.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }

        public void LoadEmpGenralDetails()
        {
            _sql = "Select ReligionName,ReligionId from ReligionMaster";
            _oo.FillDropDown_withValue(_sql, drpReligion, "ReligionName", "ReligionId");
            drpReligion.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _sql = "Select CasteName,CasteId from CasteMaster";
            _oo.FillDropDown_withValue(_sql, drpCategory, "CasteName", "CasteId");
            drpCategory.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            _sql = "Select BloodGroupName,BloodGroupId from BloodGroupMaster";
            _oo.FillDropDown_withValue(_sql, drpBloodGroup, "BloodGroupName", "BloodGroupId");
            drpBloodGroup.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }

        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = Div1;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            _oo.ExportToWord(Response, "ListofStaff.doc", Div1);
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            _oo.ExportDivToExcelWithFormatting(Response, "ListofStaff.xls", Div1, Server.MapPath("~/Admin/css/style.css"));
        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
            LoadEmpRecord();       
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}