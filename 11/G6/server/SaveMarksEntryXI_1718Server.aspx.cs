using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G6_SaveMarksEntryXI_1718Server : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string sql = "";
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string TermName = Request.Form["TermName"].ToString().Trim();
        string Session = Request.Form["Session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string LoginName = Request.Form["LoginName"].ToString().Trim();
        string MaxMarks = Request.Form["MaxMarks"].ToString().Trim();
        string Marks = Request.Form["Marks"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            SqlTransaction transaction;

            transaction = conn.BeginTransaction("SampleTransaction");

            cmd.Connection = conn;
            cmd.Transaction = transaction;

            try
            {
                string[] MaxMarksArr = MaxMarks.Split(new string[] {"##"}, StringSplitOptions.None);
                cmd.CommandText = "SetMaxMinMarksProc_XI";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Eval", TermName.ToString().Trim());
                cmd.Parameters.AddWithValue("@SubjectActivityId", SubjectId.Trim());
                cmd.Parameters.AddWithValue("@MaxMarks1", MaxMarksArr[0]);
                cmd.Parameters.AddWithValue("@MaxMarks2", MaxMarksArr[1]);
                cmd.Parameters.AddWithValue("@MaxMarks3", MaxMarksArr[2]);
                cmd.Parameters.AddWithValue("@MaxMarks4", MaxMarksArr[3]);
                cmd.Parameters.AddWithValue("@MaxMarks5", MaxMarksArr[4]);
                cmd.Parameters.AddWithValue("@MaxMarks6", MaxMarksArr[5]);
                cmd.Parameters.AddWithValue("@MaxMarks7", MaxMarksArr[6]);
                cmd.Parameters.AddWithValue("@SessionName", Session.ToString().Trim());
                cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                string[] MarksFullArr = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                for (int i = 0; i < MarksFullArr.Length-1; i++)
                {
                    string[] MarksArr = MarksFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "USP_MarkEntry_CCEXI_1718";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Classid", ClassId.Trim());
                    cmd.Parameters.AddWithValue("@SectionName", SectionName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Evaluation", TermName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId.Trim());
                    cmd.Parameters.AddWithValue("@SrNo", MarksArr[0]);
                    cmd.Parameters.AddWithValue("@TEST1", MarksArr[1]);
                    cmd.Parameters.AddWithValue("@TEST2", MarksArr[2]);
                    cmd.Parameters.AddWithValue("@TEST3", MarksArr[3]);
                    cmd.Parameters.AddWithValue("@NB", MarksArr[4]);
                    cmd.Parameters.AddWithValue("@SE", MarksArr[5]);
                    cmd.Parameters.AddWithValue("@SAT", MarksArr[6]);
                    cmd.Parameters.AddWithValue("@Prac", MarksArr[7]);
                    cmd.Parameters.AddWithValue("@SessionName", Session.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                transaction.Commit();
                Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Marks Saved Successfully.</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>unfortunately an error occurred during mark entry. please try again !</span>");
                //Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                // Console.WriteLine("  Message: {0}", ex.Message);
                try
                {
                    transaction.Rollback();
                    conn.Close();
                }
                catch (Exception ex2)
                {
                   // Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    //Console.WriteLine("  Message: {0}", ex2.Message);
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