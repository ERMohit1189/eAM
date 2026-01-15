using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SetExamStatus : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql, _sql1 = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        string ExamID = Request.Form["ExamID"].ToString().Trim();

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamAllotmentProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamID", ExamID.Trim());
            cmd.Parameters.AddWithValue("@SrNO", Session["LoginName"]);
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "SetStatus");
            cmd.Connection = _con;
            try
            {
                _con.Open();
               string res=  cmd.ExecuteScalar().ToString();
                _con.Close();
                if (res == "r")
                {
                    Response.Redirect("~/result.aspx?p=" + ExamID.Trim());
                }
                else
                {
                    Response.Redirect("~/dashboard-student.aspx");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}