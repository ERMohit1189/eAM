using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class staff_userControl_EmpAttendanceAtGlance : System.Web.UI.UserControl
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    public SqlDataAdapter ad = new SqlDataAdapter();
    string sql = "";
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetTodaysAttendance();
        }
    }

    private void GetTodaysAttendance()
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@EmpId", Session["LoginName"]),
            new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
            new SqlParameter("@DateWise", true)
        };
        var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetStaffAttendence", param);

        lblTodayDate.Text = "Date: " + DateTime.Now.ToString("dd/MMM/yyyy");
        if (ds == null || ds.Tables[0].Rows.Count <= 0)
        {
            lblTodayIn.Visible = false;
            lblTodayOut.Visible = false;
            lblTodayAttendance.Visible = false;
        }
        else
        {
            lblTodayIn.Text = "Punch In Time: " + ds.Tables[0].Rows[0]["PunchInTime"].ToString();
            lblTodayOut.Text = "Punch Out Time: " + ds.Tables[0].Rows[0]["PunchOutTime"].ToString();
            lblTodayAttendance.Text = "Attendance: " + ds.Tables[0].Rows[0]["SavedAttendence"].ToString();
        }
    }
}