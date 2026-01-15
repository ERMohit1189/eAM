using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SaveOtherFeeHeadServer : Page
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

            cmd.Connection = conn;
            try
            {
                int isSuccess = 0; int isExists = 0;
                string[] HeadFullArr = HeadData.Split(new string[] { "$" }, StringSplitOptions.None);
                for (int i = 0; i < HeadFullArr.Length - 1; i++)
                {
                    string[] HeadArr = HeadFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "USP_OtherFeeHeadMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", HeadArr[0].Trim());
                    cmd.Parameters.AddWithValue("@Gender", HeadArr[1].Trim());
                    cmd.Parameters.AddWithValue("@IsSingleHead", HeadArr[2].Trim());
                    cmd.Parameters.AddWithValue("@HeadName", HeadArr[3].Trim());
                    cmd.Parameters.AddWithValue("@Amount", HeadArr[4].Trim());
                    cmd.Parameters.AddWithValue("@Remark", HeadArr[5].Trim());
                    cmd.Parameters.AddWithValue("@SessionName", HeadArr[6].Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", HeadArr[7].Trim());
                    cmd.Parameters.AddWithValue("@LoginName", HeadArr[8].Trim());
                    cmd.Parameters.AddWithValue("@action", "save");
                    int x= cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (x == 1)
                    {
                        isSuccess=isSuccess+1;
                    }
                    else
                    {
                        isExists= isExists+ 1;
                    }
                }
                if (isSuccess>0 && isExists>0)
                {
                    Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>Some record(s) submitted successfully.!</span>");
                }
                if (isSuccess > 0 && isExists == 0)
                {
                    Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Submitted successfully.</span>");
                }
                if (isSuccess == 0 && isExists > 0)
                {
                    Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>Record(s) already exists!</span>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>Unfortunately an error occurred during Other Fee head entry. Please try again!</span>");
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