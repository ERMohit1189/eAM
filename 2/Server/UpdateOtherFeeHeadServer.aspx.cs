using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class UpdateOtherFeeHeadServer : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string HeadData = Request.Form["HeadData"].ToString().Trim();

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
                string[] HeadFullArr = HeadData.Split(new string[] { "$" }, StringSplitOptions.None);
                string[] HeadArr = HeadFullArr[0].Split(new string[] { "##" }, StringSplitOptions.None);
                cmd.CommandText = "USP_OtherFeeHeadMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", HeadArr[0].Trim());
                cmd.Parameters.AddWithValue("@ClassId", HeadArr[1].Trim());
                cmd.Parameters.AddWithValue("@IsSingleHead", HeadArr[2].Trim());
                cmd.Parameters.AddWithValue("@Amount", HeadArr[3].Trim());
                cmd.Parameters.AddWithValue("@Remark", HeadArr[4].Trim());
                cmd.Parameters.AddWithValue("@SessionName", HeadArr[5].Trim());
                cmd.Parameters.AddWithValue("@BranchCode", HeadArr[6].Trim());
                cmd.Parameters.AddWithValue("@action", "update");
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                transaction.Commit();
                Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Updated successfully.</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>Unfortunately an error occurred during Other Fee head entry. Please try again!</span>");
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