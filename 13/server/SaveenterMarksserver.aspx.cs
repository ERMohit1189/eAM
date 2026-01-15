using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SaveenterMarksserver : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql, _sql1 = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        string ExamID = Request.Form["ExamID"].ToString().Trim();
        string QuestionId = Request.Form["QuestionId"].ToString().Trim();
        string Marks = Request.Form["Marks"].ToString().Trim();
        string Remark = Request.Form["Remark"].ToString().Trim();
        string SrNO = Request.Form["SrNO"].ToString().Trim();
        if (Marks == "")
        {
            Marks = "0";
        }
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamAnswerResultProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamId", ExamID.Trim());
            cmd.Parameters.AddWithValue("@QuestionId", QuestionId.Trim());
            cmd.Parameters.AddWithValue("@SrNO", SrNO.ToString());
            cmd.Parameters.AddWithValue("@Marks", Marks.Trim());
            cmd.Parameters.AddWithValue("@Remark", Remark.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "saveMarksByTeacher");
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}