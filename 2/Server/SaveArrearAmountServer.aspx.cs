using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SaveArrearAmountServer : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string OldArrearAmt = Request.Form["OldArrearAmt"].ToString().Trim();
        string TutionfeeArrearAmt = Request.Form["TutionfeeArrearAmt"].ToString().Trim();
        string TransportArrearAmt = Request.Form["TransportArrearAmt"].ToString().Trim();
        string HostelArrearAmt = Request.Form["HostelArrearAmt"].ToString().Trim();
        string Discount = Request.Form["Discount"].ToString().Trim();
        string Fine = Request.Form["Fine"].ToString().Trim();
        string TotalAmount = Request.Form["TotalAmount"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.Connection = conn;
            try
            {
                cmd.CommandText = "SetArriarAmountMasterProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Srno", SrNo);
                cmd.Parameters.AddWithValue("@OldArrearAmt", OldArrearAmt);
                cmd.Parameters.AddWithValue("@TutionfeeArrearAmt", TutionfeeArrearAmt);
                cmd.Parameters.AddWithValue("@TransportArrearAmt", TransportArrearAmt);
                cmd.Parameters.AddWithValue("@HostelArrearAmt", HostelArrearAmt);
                cmd.Parameters.AddWithValue("@Discount", Discount);
                cmd.Parameters.AddWithValue("@Fine", Fine);
                cmd.Parameters.AddWithValue("@TotalArrearAmt", TotalAmount);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                int x = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
}