using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _8_EmpAttendanceReportNew : System.Web.UI.Page
{
    private DataTable _dt = new DataTable();
    //  private readonly Campus _oo = new Campus();
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

      //  lblHeading.Text = "Date : " + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
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
            Link_Send.Visible = true;
            LinkButton1.Visible = true;
        }
        else
        {
            gvAttendance.DataSource = null;
            Link_Send.Visible = false;
            LinkButton1.Visible = false;
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
        smsResponse = sadpNew.Send(mess, fmobileNo, "41");
        return smsResponse;
    }
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            con = _oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Link_Send_Click(object sender, EventArgs e)
    {
        DataTable _dt1 = new DataTable();
        DataTable _dt2 = new DataTable();
        cmd = new SqlCommand();
        MakeConnection();


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
            string Message = "";
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                if (_dt.Rows[i]["InTime"].ToString() != "")
                {
                    string t1 = DateTime.Parse(_dt.Rows[i]["InTime"].ToString()).ToString("hh:mm");
                    //string t2 = DateTime.Parse(_dt.Rows[0]["Time3"].ToString()).ToString("hh:mm");
                    TimeSpan ts1 = TimeSpan.Parse(t1);
                    //TimeSpan ts2 = TimeSpan.Parse(t2);
                    Message = "Dear " + _dt.Rows[i]["empname"].ToString() + ", Your attendance has been marked at " + _dt.Rows[i]["InTime"].ToString() + " Regards: Red Rose School Lucknow";
                    //if (count==0)
                    //{
                    //    count++;
                        SendFeesSms(_dt.Rows[i]["emobileno"].ToString(), Message.ToString());
                   // }
                 
                }
                else
                {
                    
                }
            }
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "SMS send successfully.", "S");
        }
        else
        {
            gvAttendance.DataSource = null;
        }
       

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DataTable _dt1 = new DataTable();
        DataTable _dt2 = new DataTable();
        cmd = new SqlCommand();
        MakeConnection();


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
            string Message = "";
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                if (_dt.Rows[i]["OutTime"].ToString() != "")
                {
                    string t1 = DateTime.Parse(_dt.Rows[i]["OutTime"].ToString()).ToString("hh:mm");
                    //string t2 = DateTime.Parse(_dt.Rows[0]["Time3"].ToString()).ToString("hh:mm");
                    TimeSpan ts1 = TimeSpan.Parse(t1);
                    //TimeSpan ts2 = TimeSpan.Parse(t2);
                    Message = "Dear " + _dt.Rows[i]["empname"].ToString() + ", Your attendance has been marked at " + _dt.Rows[i]["OutTime"].ToString() + " Regards: Red Rose School Lucknow";
                    //if (count==0)
                    //{
                    //    count++;
                    SendFeesSms(_dt.Rows[i]["emobileno"].ToString(), Message.ToString());
                    // }

                }
                else
                {

                }
            }
            Campus camp = new Campus(); camp.msgbox(this.Page, Div1, "SMS send successfully.", "S");
        }
        else
        {
            gvAttendance.DataSource = null;
        }
    }
}