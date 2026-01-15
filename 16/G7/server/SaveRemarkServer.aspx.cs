using System;
using System.Data;
using System.Data.SqlClient;

public partial class SaveRemarkServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string Eval = Request.Form["Eval"].ToString().Trim();
        string Remark = Request.Form["Remark"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;

            try
            {
                cmd.CommandText = "SaveXIIRemarkAndResult";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Srno", SrNo.Trim());
                cmd.Parameters.AddWithValue("@Eval", Eval.Trim());
                if (Eval == "Term1")
                {
                    cmd.Parameters.AddWithValue("@SA1Remark", Remark);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SA2Remark", Remark);
                }
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                int pp = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                string xx = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
}