using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class GetDurationServer : System.Web.UI.Page
{
    private SqlConnection _con=new SqlConnection();
    private readonly Campus _oo=new Campus();
    private string _sql, _sql1=String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string ExamID = Request.Form["ExamID"].ToString().Trim();
        _sql = "select Duration from OT_ExamAllotment where ExamID=" + ExamID + " and SrNO='" + Session["LoginName"] + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        string Duration = _oo.ReturnTag(_sql, "Duration");
        Response.Write(Duration);
    }
}