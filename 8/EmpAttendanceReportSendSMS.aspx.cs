using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using c4SmsNew;

namespace _8
{
    public partial class AdminEmpAttendanceReportSendSMS : Page
    {
        DataTable _dts = new DataTable();
        private DataTable _dt = new DataTable();
        private readonly Campus _oo = new Campus();
        private string _attendanceStatus = "";
        private string _sql = "";


        public SqlConnection con;
        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter ad = new SqlDataAdapter();

        public string MSG = "";
        public bool IsExists;
        Campus conCampus = new Campus();

        public static readonly DAL DALInstance = new DAL();

        public void MakeConnection()
        {
            con = new SqlConnection();
            try
            {
                con = conCampus.dbGet_connection();
                con.Open();
            }
            catch { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _attendanceStatus = !string.IsNullOrEmpty(Request.QueryString["Sts"]) ? Request.QueryString["Sts"] : ddlAttendance.SelectedValue;

            if (!IsPostBack)
            {
                GetEmp();

                txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                GetAttendanceReport();
            }

            lblHeading.Text = "Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //lblHeading.Text = "Consolidated Attendance Report ( Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy MMM dd") + " )";
        }

        public void GetEmp()
        {
            _dt = new DataTable();
            _dt = DAL.DALInstance.GetValueInTable("SELECT Z1.EmpId,Z1.EmpName,Z1.EmpName+' ('+Z1.EmpId+')' EmpNameID FROM (SELECT DISTINCT G.EmpId,G.EFirstName+ISNULL(' '+G.ELastName,'') EmpName FROM EmpGeneralDetail G JOIN EmpployeeOfficialDetails O ON O.EmpId=G.EmpId WHERE O.Withdrwal IS NULL OR O.Withdrwal='' and G.BranchCode=" + Session["BranchCode"] + " and O.BranchCode=" + Session["BranchCode"] + ") Z1 ORDER BY EmpName");
            if (_dt != null && _dt.Rows.Count > 0)
            {
                BLL.FillDropDown(ddlEmp, _dt, "EmpNameID", "EmpID", 'A');
            }

            _sql = "Select EmpDesName,EmpDesId from EmpDesMaster where BranchCode=" + Session["BranchCode"] + " ";
            _oo.FillDropDown_withValue(_sql, drpDesignation, "EmpDesName", "EmpDesId");
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
            var obj = new BAL.clsSearchAttendance
            {
                EmpID = ddlEmp.SelectedValue,
                AttendanceType = _attendanceStatus,
                Designation = drpDesignation.SelectedItem.Text.Trim(),
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim())
            };
            //_dt = null;
            cmd = new SqlCommand();
            MakeConnection();
            cmd.CommandText = "Usp_getempattendancereport_For_sendSMS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AttendanceType", obj.AttendanceType);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@Designation", obj.Designation);
            cmd.Parameters.AddWithValue("@Action", "Emp_record");
            ad.SelectCommand = cmd;
            ad.Fill(_dts);

            if (_dts != null && _dts.Rows.Count > 0)
            {
                gvAttendance.DataSource = _dts;
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
        public string SendFeesSms(string fmobileNo, string txtMessage)
        {
            string smsResponse;

            SMSAdapterNew sadpNew = new SMSAdapterNew();
            string mess;
            mess = txtMessage.Trim();
            smsResponse = sadpNew.Send(mess, fmobileNo, "");
            return smsResponse;
        }
        protected void Link_Send_Click(object sender, EventArgs e)
        {
            DataTable _dt1 = new DataTable();
            DataTable _dt2 = new DataTable();
            cmd = new SqlCommand();
            MakeConnection();

            var obj = new BAL.clsSearchAttendance
            {
                EmpID = ddlEmp.SelectedValue,
                AttendanceType = _attendanceStatus,
                Designation = drpDesignation.SelectedItem.Text.Trim(),
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim() == "" ? "1 Jan 1900" : txtFromDate.Text.Trim())
            };
            //_dt = null;
            cmd.CommandText = "Usp_getempattendancereport_For_sendSMS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@AttendanceType", obj.AttendanceType);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
            cmd.Parameters.AddWithValue("@Designation", obj.Designation);
            cmd.Parameters.AddWithValue("@Action", "Emp_record");
            
            ad.SelectCommand = cmd;
            ad.Fill(_dt1);
            cmd.Parameters.Clear();
            if (_dt1 != null && _dt1.Rows.Count > 0)
            {
                for (int i = 0; i < _dt1.Rows.Count; i++)
                {
                    string Message = "";
                    cmd.CommandText = "Usp_getempattendancereport_For_sendSMS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                    cmd.Parameters.AddWithValue("@EmpDesName", _dt1.Rows[i]["designation"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Action", "Sift_record");
                    ad.SelectCommand = cmd;
                    ad.Fill(_dt2);
                    cmd.Parameters.Clear();
                    if (_dt2.Rows.Count>0 && _dt2 != null)
                    {
                        if (_dt1.Rows[i]["InTime"].ToString() != "")
                        {
                            string t1 = DateTime.Parse(_dt1.Rows[i]["InTime"].ToString()).ToString("hh:mm");
                            string t2 = DateTime.Parse(_dt2.Rows[0]["Time3"].ToString()).ToString("hh:mm");
                            TimeSpan ts1 = TimeSpan.Parse(t1);
                            TimeSpan ts2 = TimeSpan.Parse(t2);

                            if (_dt1.Rows[i]["AttendanceValue"].ToString() == "Present")
                            {
                                if (ts1 > ts2)
                                {
                                    Message = "Dear " + _dt1.Rows[i]["empname"].ToString() + ", you are " + (ts1 - ts2).ToString() + " hours late today (" + DateTime.Parse(txtFromDate.Text).ToString("dd MMM yyyy") + ").";
                                    SendFeesSms(_dt1.Rows[i]["emobileno"].ToString(), Message.ToString());
                                }
                                else
                                {
                                    Message = "Dear " + _dt1.Rows[i]["empname"].ToString() + ", you are Present (" + _dt1.Rows[i]["intime"].ToString() + ") today (" + DateTime.Parse(txtFromDate.Text).ToString("dd MMM yyyy") + ").";
                                    SendFeesSms(_dt1.Rows[i]["emobileno"].ToString(), Message.ToString());
                                }
                            }
                        }
                        else
                        {
                            if (_dt1.Rows[i]["AttendanceValue"].ToString() == "Not Mark" || _dt1.Rows[i]["AttendanceValue"].ToString() == "Absent")
                            {
                                Message = "Dear " + _dt1.Rows[i]["empname"].ToString() + ", you are absent today (" + DateTime.Parse(txtFromDate.Text).ToString("dd MMM yyyy") + ").";
                                SendFeesSms(_dt1.Rows[i]["emobileno"].ToString(), Message.ToString());
                            }
                        }
                        _dt2.Clear();
                    }
                }

                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "SMS send successfully.", "S");
            }

        }
    }
}