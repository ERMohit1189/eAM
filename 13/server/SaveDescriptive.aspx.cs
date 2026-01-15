using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SaveDescriptive : System.Web.UI.Page
{
    private SqlConnection _con = new SqlConnection();
    private readonly Campus _oo = new Campus();
    private string _sql, _sql1 = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        _con = _oo.dbGet_connection();
        string ExamID = Request.Form["ExamID"].ToString().Trim();
        string QuestionId = Request.Form["QuestionId"].ToString().Trim();
        string ChooseOption = Request.Form["ChooseOption"].ToString().Trim();

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "OT_ExamAnswerResultProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamID", ExamID.Trim());
            cmd.Parameters.AddWithValue("@QuestionId", QuestionId.Trim());
            cmd.Parameters.AddWithValue("@SrNO", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@ChooseOption", ChooseOption.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@Loginname", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "saveOptionsDescriptive");
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