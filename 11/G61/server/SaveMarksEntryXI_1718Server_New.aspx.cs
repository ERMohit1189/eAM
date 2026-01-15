using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class common_G6_SaveMarksEntryXI_1718Server_New : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
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
                bool error1 = false;
                string[] MaxMarksArr1 = MaxMarks.Split(new string[] { "##" }, StringSplitOptions.None);
                double mno;
                bool successs = double.TryParse(MaxMarksArr1[0], out mno);
                if (double.TryParse(MaxMarksArr1[0], out mno) == false || double.TryParse(MaxMarksArr1[1], out mno) == false || double.TryParse(MaxMarksArr1[2], out mno) == false || double.TryParse(MaxMarksArr1[3], out mno) == false || double.TryParse(MaxMarksArr1[4], out mno) == false)
                {
                    error1 = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type only Numbers in Maxsmarks Box!')", true);
                    return;
                }
                else
                {
                    error1 = false;
                }
                string[] MarksFullArr1 = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                for (int j = 0; j < MarksFullArr1.Length - j; j++)
                {

                    string[] MarksArr1 = MarksFullArr1[j].Split(new string[] { "##" }, StringSplitOptions.None);
                    double no;


                    if (MarksArr1[1].ToLower() != "" && MarksArr1[1].ToLower() != "np" && MarksArr1[1].ToLower() != "nad" && MarksArr1[1].ToLower() != "ml")
                    {
                        if (double.TryParse(MarksArr1[1], out no) == false)
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, NP or Numbers in Box!')", true);
                            break;
                        }
                        else if (double.Parse(MaxMarksArr1[0] == "" ? "0" : MaxMarksArr1[0]) < double.Parse(MarksArr1[1] == "" ? "0" : MarksArr1[1]))
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                            break;
                        }
                        else
                        {
                            error1 = false;
                        }
                    }
                    if (MarksArr1[2].ToLower() != "" && MarksArr1[2].ToLower() != "np" && MarksArr1[2].ToLower() != "nad" && MarksArr1[2].ToLower() != "ml")
                    {
                        if (double.TryParse(MarksArr1[2], out no) == false)
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, NP or Numbers in Box!')", true);
                            break;
                        }
                        else if (double.Parse(MaxMarksArr1[1] == "" ? "0" : MaxMarksArr1[1]) < double.Parse(MarksArr1[2] == "" ? "0" : MarksArr1[2]))
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                            break;
                        }
                        else
                        {
                            error1 = false;
                        }
                    }
                    if (MarksArr1[3].ToLower() != "" && MarksArr1[3].ToLower() != "np" && MarksArr1[3].ToLower() != "nad" && MarksArr1[3].ToLower() != "ml")
                    {
                        if (double.TryParse(MarksArr1[3], out no) == false)
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, NP or Numbers in Box!')", true);
                            break;
                        }
                        else if (double.Parse(MaxMarksArr1[2] == "" ? "0" : MaxMarksArr1[2]) < double.Parse(MarksArr1[3] == "" ? "0" : MarksArr1[3]))
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                            break;
                        }
                        else
                        {
                            error1 = false;
                        }
                    }
                    if (MarksArr1[4].ToLower() != "" && MarksArr1[4].ToLower() != "np" && MarksArr1[4].ToLower() != "nad" && MarksArr1[4].ToLower() != "ml")
                    {
                        if (double.TryParse(MarksArr1[4], out no) == false)
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, NP or Numbers in Box!')", true);
                            break;
                        }
                        else if (double.Parse(MaxMarksArr1[3] == "" ? "0" : MaxMarksArr1[3]) < double.Parse(MarksArr1[4] == "" ? "0" : MarksArr1[4]))
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                            break;
                        }
                        else
                        {
                            error1 = false;
                        }
                    }
                    if (MarksArr1[5].ToLower() != "" && MarksArr1[5].ToLower() != "np" && MarksArr1[5].ToLower() != "nad" && MarksArr1[5].ToLower() != "ml")
                    {
                        if (double.TryParse(MarksArr1[5], out no) == false)
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, NP or Numbers in Box!')", true);
                            break;
                        }
                        else if (double.Parse(MaxMarksArr1[4] == "" ? "0" : MaxMarksArr1[4]) < double.Parse(MarksArr1[5] == "" ? "0" : MarksArr1[5]))
                        {
                            error1 = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                            break;
                        }
                        else
                        {
                            error1 = false;
                        }
                    }
                    
                }
                if (error1 == false)
                {
                    string[] MaxMarksArr = MaxMarks.Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "SetMaxMinMarksProc_XI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Eval", TermName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SubjectActivityId", SubjectId.Trim());
                    cmd.Parameters.AddWithValue("@MaxMarks1", MaxMarksArr[0]);
                    cmd.Parameters.AddWithValue("@MaxMarks2", MaxMarksArr[1]);
                    cmd.Parameters.AddWithValue("@MaxMarks3", MaxMarksArr[2]);
                    cmd.Parameters.AddWithValue("@MaxMarks6", MaxMarksArr[3]);
                    cmd.Parameters.AddWithValue("@MaxMarks7", MaxMarksArr[4]);
                    cmd.Parameters.AddWithValue("@SessionName", Session.ToString().Trim());
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    string[] MarksFullArr = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                    for (int i = 0; i < MarksFullArr.Length - 1; i++)
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
                        cmd.Parameters.AddWithValue("@SAT", MarksArr[4]);
                        cmd.Parameters.AddWithValue("@Prac", MarksArr[5]);
                        cmd.Parameters.AddWithValue("@SessionName", Session.ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    transaction.Commit();
                    Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Marks Saved Successfully.</span>");
                }
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