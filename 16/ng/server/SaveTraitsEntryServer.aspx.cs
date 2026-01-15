using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SaveTraitsEntryServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string TermId = Request.Form["TermId"].ToString().Trim();
        string TraitsType = Request.Form["TraitsType"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string LoginName = Request.Form["LoginName"].ToString().Trim();
        string Traits = Request.Form["Traits"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            try
            {
                string[] TraitsFullArr = Traits.Split(new string[] { "$" }, StringSplitOptions.None);
                for (int i = 0; i < TraitsFullArr.Length-1; i++)
                {
                    string[] TraitsArr = TraitsFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "master_NG_TraitsEntry";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                    cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@termid", TermId.ToString());
                    cmd.Parameters.AddWithValue("@tradsType", TraitsType.ToString());
                    cmd.Parameters.AddWithValue("@srNo", TraitsArr[0]);
                    if (TraitsArr[1] != "")
                    {
                        cmd.Parameters.AddWithValue("@Traits1", TraitsArr[1]);
                    }
                    if (TraitsArr[2] != "")
                    {
                        cmd.Parameters.AddWithValue("@Traits2", TraitsArr[2]);
                    }
                    if (TraitsArr[3] != "")
                    {
                        cmd.Parameters.AddWithValue("@Traits3", TraitsArr[3]);
                    }
                    if (TraitsArr[4] != "")
                    {
                        cmd.Parameters.AddWithValue("@Traits4", TraitsArr[4]);
                    }
                    cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString());
                    cmd.Parameters.AddWithValue("@action", "save_Traits");
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Traits Saved Successfully.</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>unfortunately an error occurred during mark entry. please try again !</span>");
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
}