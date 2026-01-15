using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SaveMarksEntryNurtoPrep : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Term = Request.Form["Term"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string LoginName = Request.Form["LoginName"].ToString().Trim();
        string Marks = Request.Form["Marks"].ToString().Trim();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            try
            {
                string[] MarksFullArr = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                for (int i = 0; i < MarksFullArr.Length - 1; i++)
                {
                    string[] MarksArr = MarksFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "ICSEMarkEntryNurtoPrepProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Classid", ClassId.Trim());
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@PaperId", PaperId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Term", Term.Trim());
                    cmd.Parameters.AddWithValue("@SrNo", MarksArr[0]);
                    if (Term.ToLower() == "term1")
                    {
                        cmd.Parameters.AddWithValue("@FA1", MarksArr[1].ToUpper());
                        cmd.Parameters.AddWithValue("@FA2", MarksArr[2].ToUpper());
                        cmd.Parameters.AddWithValue("@EvaluationTerm1", MarksArr[3].ToUpper());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FA3", MarksArr[1].ToUpper());
                        cmd.Parameters.AddWithValue("@FA4", MarksArr[2].ToUpper());
                        cmd.Parameters.AddWithValue("@EvaluationTerm2", MarksArr[3].ToUpper());
                    }
                    cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                    cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Marks Saved Successfully.</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>unfortunately an error occurred during mark entry. please try again !</span>");
                try
                {
                    conn.Close();
                }
                catch (Exception ex2)
                {
                    conn.Close();
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}