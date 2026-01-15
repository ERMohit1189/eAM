using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SaveOptionalSubjectAllotmentServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
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

                for (int i = 0; i < MarksFullArr.Length-1; i++)
                {
                    string[] MarksArr = MarksFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "master_NG_OptionalSubjectAllotment";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId.Trim());
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.Trim());
                    cmd.Parameters.AddWithValue("@srNo", MarksArr[0]);
                    cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString());
                    cmd.Parameters.AddWithValue("@action", "save_OptionalSubjectentry");
                    cmd.Parameters.AddWithValue("@status", MarksArr[1]);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Subject alloted Successfully.</span>");
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