using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using c4SmsNew;

public partial class _1_AttendanceAlert : System.Web.UI.Page
{
    public static string SessionName="", BranchCode="";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            SessionName = Session["SessionName"].ToString();
            BranchCode = Session["BranchCode"].ToString();
        }
    }

    [System.Web.Services.WebMethod()]
    public static List<StudentAttendance> GetStudentAttendance(string FromDate,string AttendanceValue)
    {
        var objStudentList = new List<StudentAttendance>();
        DataTable dt = new DataTable();
        var param = new List<SqlParameter> {
            new SqlParameter("@FromDate", FromDate),
            new SqlParameter("@Attendancevalue",AttendanceValue),
            new SqlParameter("@SessionName", SessionName),
            new SqlParameter("@BranchCode",BranchCode)
        };

        DataSet ds=DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_GetStudentAttendenceRecord", param);
        if(ds!=null)
        {
            if(ds.Tables[0].Rows.Count>0)
            {
                dt = ds.Tables[0];
            }
        }

        if(dt.Rows.Count>0)
        {
            foreach(DataRow dr in dt.Rows)
            {
                objStudentList.Add(new StudentAttendance(dr["srno"].ToString(), dr["StudentName"].ToString(), dr["FatherName"].ToString(), dr["Mobileno"].ToString(), dr["AttendanceValue"].ToString(), dr["AttendanceDate"].ToString(), dr["Intime"].ToString(), dr["Outtime"].ToString(), dr["AttendanceName"].ToString() ));
            }
        }

        return objStudentList;
    }

    [System.Web.Services.WebMethod()]
    public static string SendSMS(string msg,string contactno)
    {
        string respo = "";
        SMSAdapterNew sa = new SMSAdapterNew();
        respo=sa.Send(msg, contactno,"");
        if(respo == "Sorry, SMS Panel is Deactivated!")
        {
            respo = "PDA";
        }
        return respo;
    }

}