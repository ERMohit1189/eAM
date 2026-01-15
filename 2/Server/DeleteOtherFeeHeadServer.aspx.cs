using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class DeleteOtherFeeHeadServer : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string id = Request.Form["id"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();

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
                cmd.CommandText = "USP_OtherFeeHeadMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id.Trim());
                cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                cmd.Parameters.AddWithValue("@action", "delete");
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                transaction.Commit();
                Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Other fee head deleted duccessfully.</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>unfortunately an error occurred during Other Fee head entry. please try again !</span>");
                try
                {
                    transaction.Rollback();
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