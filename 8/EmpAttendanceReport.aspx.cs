using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _8
{
    public partial class AdminEmpAttendanceReport : Page
    {
        private DataTable _dt = new DataTable();
        private readonly Campus _oo = new Campus();
        private string _attendanceStatus = "";
        private string _sql = "";
        public SqlConnection con;
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter ad = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            _attendanceStatus = !string.IsNullOrEmpty(Request.QueryString["Sts"]) ? Request.QueryString["Sts"] : ddlAttendance.SelectedValue;
            con = _oo.dbGet_connection();
            con.Open();
            if (!IsPostBack)
            {
                GetEmp();
                ddlAttendance.SelectedValue = _attendanceStatus;
                txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                GetAttendanceReport();
            }

            lblHeading.Text = "Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
        }

        public void GetEmp()
        {
            
            _sql = "Select DesName,DesId from DesMaster  where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpDesignation, "DesName", "DesId");
            drpDesignation.Items.Insert(0, new ListItem("<--Select-->", "0"));
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
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetEmpAttendanceReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AttendanceType", ddlAttendance.SelectedValue);
            cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim()));
            cmd.Parameters.AddWithValue("@Designation", drpDesignation.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            ad.SelectCommand = cmd;
            ad.Fill(_dt);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                gvAttendance.DataSource = _dt;
            }
            else
            {
                gvAttendance.DataSource = null;
            }
            gvAttendance.DataBind();
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

        protected void drpDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAttendanceReport();
        }
    }
}