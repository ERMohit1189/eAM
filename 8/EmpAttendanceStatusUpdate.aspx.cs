using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminEmpAttendanceReport : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        private DataTable _dt;
        public SqlConnection con;
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter ad = new SqlDataAdapter();
        private string _attendanceStatus = "";

        public AdminEmpAttendanceReport()
        {
            _dt = new DataTable();
            _oo = new Campus();
            _con = new SqlConnection();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _con = _oo.dbGet_connection();
            _attendanceStatus = !string.IsNullOrEmpty(Request.QueryString["Sts"]) ? Request.QueryString["Sts"] : ddlAttendance.SelectedValue;
            con = _oo.dbGet_connection();
            con.Open();
            if (!IsPostBack)
            {
                GetEmp();
                ddlAttendance.SelectedValue = _attendanceStatus;
                txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                _attendanceStatus = ddlAttendance.SelectedValue;
            }

            lblHeading.Text = "Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //lblHeading.Text = "Consolidated Attendance Report ( Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy MMM dd") + " )";
        }

        public void GetEmp()
        {
            _sql = "select EmpId,EmpName, EmpName+' ('+EmpId+')' EmpNameID from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where Withdrwal IS NULL OR Withdrwal='' ORDER BY EmpName";

            _oo.FillDropDown_withValue(_sql, ddlEmp, "EmpNameID", "EmpID");
            ddlEmp.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }


        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAttendanceReport();
        }


        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            GetAttendanceReport();
        }

        private void GetAttendanceReport()
        {
            var obj = new BAL.clsSearchAttendance
            {
                EmpID = ddlEmp.SelectedValue,
                AttendanceType = _attendanceStatus,
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim())
            };
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetEmpAttendanceReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@EmpId", ddlEmp.SelectedValue);
            cmd.Parameters.AddWithValue("@AttendanceType", ddlAttendance.SelectedValue);
            cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim()));
            cmd.Parameters.AddWithValue("@Designation", "<--Select-->");
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            ad.SelectCommand = cmd;
            ad.Fill(_dt);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                gvAttendance.DataSource = _dt;
                btnshowsubmit.Visible = true;
            }
            else
            {
                gvAttendance.DataSource = null;
                btnshowsubmit.Visible = false;
            }
            gvAttendance.DataBind();
            if (gvAttendance.Rows.Count > 0)
            {
                DropDownList drpAttendance = (DropDownList)gvAttendance.HeaderRow.FindControl("drpAttendance");
                _sql = "select AbbreviationName from  AttendanceAbbreviationMaster where BranchCode=" + Session["BranchCode"].ToString() + "";
                _oo.FillDropDownWithOutSelect(_sql, drpAttendance, "AbbreviationName");
                drpAttendance.Items.Insert(0, "Attendance");
                for (int a = 0; a < gvAttendance.Rows.Count; a++)//State Wise
                {
                    DropDownList drp = (DropDownList)gvAttendance.Rows[a].FindControl("DropDownList1");
                    _sql = "select AbbreviationName from  AttendanceAbbreviationMaster";
                    _sql = _sql + " where ValidFor='E'";
                    _oo.FillDropDownWithOutSelect(_sql, drp, "AbbreviationName");

                    Label lblempid = (Label)gvAttendance.Rows[a].FindControl("lblempid");

                    _sql = "select AttendanceValue from EmployeeAttendanceDayWise where EmpId='" + lblempid.Text + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    _sql = _sql + " and AttendanceDate='" + Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim()) + "'";

                    string ss = BAL.objBal.ReturnTag(_sql, "AttendanceValue");
                    drp.SelectedValue = ss.Trim();

                    if (drp.SelectedItem.Text.ToUpper() == "A")
                    {
                        gvAttendance.Rows[a].CssClass = "vd_bg-red form-control-blue vd_white";
                        drp.CssClass = "vd_bg-red form-control-blue vd_white";
                    }
                    else if (drp.SelectedItem.Text.ToUpper() == "L")
                    {
                        gvAttendance.Rows[a].CssClass = "vd_bg-yellow form-control-blue vd_white";
                        drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (drp.SelectedItem.Text.ToUpper() == "L")
                    {
                        gvAttendance.Rows[a].CssClass = "vd_bg-yellow form-control-blue vd_white";
                        drp.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (drp.SelectedItem.Text.ToUpper() == "LT" || drp.SelectedItem.Text.ToUpper() == "LC")
                    {
                        gvAttendance.Rows[a].CssClass = "vd_bg-blue form-control-blue vd_white";
                        drp.CssClass = "vd_bg-blue form-control-blue vd_white";
                    }
                    else
                    {
                        gvAttendance.Rows[a].CssClass = "vd_bg-green form-control-blue vd_white";
                        drp.CssClass = "vd_bg-green form-control-blue vd_white";
                    }
                }
            }
        }

        protected void ddlAttendance_SelectedIndexChanged(object sender, EventArgs e)
        {
            _attendanceStatus = "";
            _attendanceStatus = ddlAttendance.SelectedValue;
            GetAttendanceReport();
        }
  
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAttendanceReport();
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            GetAttendanceReport();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow currentrow = (GridViewRow)(((Control)(sender)).Parent).Parent;
            DropDownList DropDownList1 = (DropDownList)currentrow.FindControl("DropDownList1");

            if (DropDownList1.SelectedItem.Text.ToUpper() == "A")
            {
                currentrow.CssClass = "vd_bg-red form-control-blue vd_white";
                DropDownList1.CssClass = "vd_bg-red form-control-blue vd_white";
            }
            else if (DropDownList1.SelectedItem.Text.ToUpper() == "L")
            {
                currentrow.CssClass = "vd_bg-yellow form-control-blue vd_white";
                DropDownList1.CssClass = "vd_bg-yellow form-control-blue vd_white";
            }
            else if (DropDownList1.SelectedItem.Text.ToUpper() == "L")
            {
                currentrow.CssClass = "vd_bg-yellow form-control-blue vd_white";
                DropDownList1.CssClass = "vd_bg-yellow form-control-blue vd_white";
            }
            else if (DropDownList1.SelectedItem.Text.ToUpper() == "LT" || DropDownList1.SelectedItem.Text.ToUpper() == "LC")
            {
                currentrow.CssClass = "vd_bg-blue form-control-blue vd_white";
                DropDownList1.CssClass = "vd_bg-blue form-control-blue vd_white";
            }
            else
            {
                currentrow.CssClass = "vd_bg-green form-control-blue vd_white";
                DropDownList1.CssClass = "vd_bg-green form-control-blue vd_white";
            }
        }

        protected void drpAttendance_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpAttendance = (DropDownList)sender;
            if (drpAttendance.SelectedIndex != 0)
            {
                // ReSharper disable once LocalVariableHidesMember
                foreach (GridViewRow gvr in gvAttendance.Rows)
                {
                    DropDownList drpAtt = (DropDownList)gvr.FindControl("DropDownList1");
                    drpAtt.SelectedIndex = drpAttendance.SelectedIndex - 1;

                    if (drpAtt.SelectedItem.Text.ToUpper() == "A")
                    {
                        gvr.CssClass = "vd_bg-red form-control-blue vd_white";
                        drpAtt.CssClass = "vd_bg-red form-control-blue vd_white";
                    }
                    else if (drpAtt.SelectedItem.Text.ToUpper() == "L")
                    {
                        gvr.CssClass = "vd_bg-yellow form-control-blue vd_white";
                        drpAtt.CssClass = "vd_bg-yellow form-control-blue vd_white";
                    }
                    else if (drpAtt.SelectedItem.Text.ToUpper() == "LT" || drpAtt.SelectedItem.Text.ToUpper() == "LC")
                    {
                        gvr.CssClass = "vd_bg-blue form-control-blue vd_white";
                        drpAtt.CssClass = "vd_bg-blue form-control-blue vd_white";
                    }
                    else
                    {
                        gvr.CssClass = "vd_bg-green form-control-blue vd_white";
                        drpAtt.CssClass = "vd_bg-green form-control-blue vd_white";
                    }
                }
            }
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            DailyAttendanceRadio();
            GetAttendanceReport();
        }
        public void DailyAttendanceRadio()
        {
            var datemonth = txtFromDate.Text.Trim();
            for (var a = 0; a < gvAttendance.Rows.Count; a++)//State Wise
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "EmployeeAttendanceStatusWiseProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    Label lblempcode = (Label)gvAttendance.Rows[a].FindControl("lblempcode");
                    Label lblempid = (Label)gvAttendance.Rows[a].FindControl("lblempid");
                    Label lblempname = (Label)gvAttendance.Rows[a].FindControl("lblempname");
                    Label lbldepartment = (Label)gvAttendance.Rows[a].FindControl("lbldepartment");
                    DropDownList drp1 = (DropDownList)gvAttendance.Rows[a].FindControl("DropDownList1");

                    cmd.Parameters.AddWithValue("@CategoryWise", "Date Wise");
                    cmd.Parameters.AddWithValue("@AttendanceMonth", Convert.ToDateTime(datemonth).ToString("MMM"));
                    cmd.Parameters.AddWithValue("@AttendanceDate", datemonth);
                    cmd.Parameters.AddWithValue("@DepartmentName", lbldepartment.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ecode", lblempcode.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmpId", lblempid.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmployeeName", lblempname.Text.Trim());
                    cmd.Parameters.AddWithValue("@AttendanceValue", drp1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully.", "S");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("some reason to rethrow", ex);
                        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Record not submitted successfully.", "W");
                        // ignored
                    }
                }
            }

            _attendanceStatus = "";
            _attendanceStatus = ddlAttendance.SelectedValue;
            GetAttendanceReport();
        }
        public override void Dispose()
        {
            _dt.Dispose();
            _con.Dispose();
            _oo.Dispose();
        }

    }
}