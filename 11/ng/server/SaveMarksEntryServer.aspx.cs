using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SaveMarksEntryServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string TermId = Request.Form["TermId"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string LoginName = Request.Form["LoginName"].ToString().Trim();
        string MaxMarks = Request.Form["MaxMarks"].ToString().Trim();
        string Marks = Request.Form["Marks"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            try
            {
                string[] MaxMarksArr = MaxMarks.Split(new string[] {"##"}, StringSplitOptions.None);
                cmd.CommandText = "master_NG_MarksEntry";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId.Trim());
                cmd.Parameters.AddWithValue("@termid", TermId.ToString());
                if (MaxMarksArr[0]!="")
                {
                    cmd.Parameters.AddWithValue("@Max_conutive", MaxMarksArr[0]);
                }
                if (MaxMarksArr[1] != "")
                {
                    cmd.Parameters.AddWithValue("@Max_affective", MaxMarksArr[1]);
                }
                if (MaxMarksArr[2] != "")
                {
                    cmd.Parameters.AddWithValue("@Max_phychomotor", MaxMarksArr[2]);
                }
                if (MaxMarksArr[3] != "")
                {
                    cmd.Parameters.AddWithValue("@MaxtotalCa", MaxMarksArr[3]);
                }
                if (MaxMarksArr[4] != "")
                {
                    cmd.Parameters.AddWithValue("@Max_exam", MaxMarksArr[4]);
                }
                if (MaxMarksArr[5] != "")
                {
                    cmd.Parameters.AddWithValue("@Maxtotal", MaxMarksArr[5]);
                }
                cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString());
                cmd.Parameters.AddWithValue("@action", "save_markentry");
                cmd.Parameters.AddWithValue("@action2", "save_maxmark");
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                string[] MarksFullArr = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                for (int i = 0; i < MarksFullArr.Length-1; i++)
                {
                    string[] MarksArr = MarksFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "master_NG_MarksEntry";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                    cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId.Trim());
                    cmd.Parameters.AddWithValue("@termid", TermId.ToString());
                    cmd.Parameters.AddWithValue("@srNo", MarksArr[0]);
                    if (MarksArr[1]!="")
                    {
                        cmd.Parameters.AddWithValue("@conutive", MarksArr[1]);
                    }
                    if (MarksArr[1] != "")
                    {
                    }
                    if (MarksArr[2] != "")
                    {
                        cmd.Parameters.AddWithValue("@affective", MarksArr[2]);
                    }
                    if (MarksArr[2] != "")
                    {
                        cmd.Parameters.AddWithValue("@phychomotor", MarksArr[3]);
                    }
                    if (MarksArr[4] != "")
                    {
                        cmd.Parameters.AddWithValue("@totalCa", MarksArr[4]);
                    }
                    if (MarksArr[5] != "")
                    {
                        cmd.Parameters.AddWithValue("@exam", MarksArr[5]);
                    }
                    if (MarksArr[6] != "")
                    {
                        cmd.Parameters.AddWithValue("@obtainedMark", MarksArr[6]);
                    }
                    if (MarksArr[7] != "")
                    {
                        cmd.Parameters.AddWithValue("@gade", MarksArr[7]);
                    }
                    if (MarksArr[8] != "")
                    {
                        cmd.Parameters.AddWithValue("@TeachersRemark", MarksArr[8]);
                    }
                    cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString());
                    cmd.Parameters.AddWithValue("@action", "save_markentry");
                    cmd.Parameters.AddWithValue("@action2", "save_mark");
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