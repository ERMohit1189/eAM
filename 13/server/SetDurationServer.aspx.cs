using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SetDurationServer : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql, _sql1 = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        string ExamID = Request.Form["ExamID"].ToString().Trim();
        string MIN = Request.Form["MIN"].ToString().Trim();

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamAllotmentProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamID", ExamID.Trim());
            cmd.Parameters.AddWithValue("@SrNO", Session["LoginName"]);
            cmd.Parameters.AddWithValue("@Duration", MIN);
            cmd.Parameters.AddWithValue("@Action", "SetDuration");
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}