using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SaveMarksEntryItoVIII : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Term = Request.Form["Term"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
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
                bool error1 = false;
                string[] MaxMarksArr1 = MaxMarks.Split(new string[] { "##" }, StringSplitOptions.None);
                if (error1 == false)
                {
                    string[] MaxMarksArr = MaxMarks.Split(new string[] { "##" }, StringSplitOptions.None);
                    double MaxTest1M = 0, MaxTest2M = 0, MaxTest3M = 0, MaxHYAEM = 0;
                    double.TryParse(MaxMarksArr[0].Trim().ToString(), out MaxHYAEM);
                    double.TryParse(MaxMarksArr[1].Trim().ToString(), out MaxTest1M);
                    double.TryParse(MaxMarksArr[2].Trim().ToString(), out MaxTest2M);
                    double.TryParse(MaxMarksArr[3].Trim().ToString().Replace("$", ""), out MaxTest3M);

                    cmd.CommandText = "ICSEMarkEntryItoVIIIProc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Classid", ClassId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.Trim());
                    cmd.Parameters.AddWithValue("@SectionId", SectionId.Trim());
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId.Trim());
                    cmd.Parameters.AddWithValue("@PaperId", PaperId.Trim());
                    cmd.Parameters.AddWithValue("@Term", Term.Trim());
                    
                    if (Term.ToLower() == "term1")
                    {
                        cmd.Parameters.AddWithValue("@MaxTest1", MaxTest1M);
                        cmd.Parameters.AddWithValue("@MaxTest2", MaxTest2M);
                        cmd.Parameters.AddWithValue("@MaxTest3", MaxTest3M);
                        cmd.Parameters.AddWithValue("@MaxHY", MaxHYAEM);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MaxTest4", MaxTest1M);
                        cmd.Parameters.AddWithValue("@MaxTest5", MaxTest2M);
                        cmd.Parameters.AddWithValue("@MaxTest6", MaxTest3M);
                        cmd.Parameters.AddWithValue("@MaxAE", MaxHYAEM);
                    }
                    cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                    cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Action", "insert");
                    cmd.Parameters.AddWithValue("@Action2", "MaxMark");
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    string[] MarksFullArr = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                    for (int i = 0; i < MarksFullArr.Length - 1; i++)
                    {
                        string[] MarksArr = MarksFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                        cmd.CommandText = "ICSEMarkEntryItoVIIIProc";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Classid", ClassId.Trim());
                        cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@SubjectId", SubjectId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@PaperId", PaperId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Term", Term.Trim());
                        cmd.Parameters.AddWithValue("@SrNo", MarksArr[0]);
                        if (Term.ToLower() == "term1")
                        {

                            double Test1M = 0, Test2M = 0, Test3M = 0, MaxHYM = 0;
                            double.TryParse(MarksArr[1].Trim().ToString(), out MaxHYM);
                            double.TryParse(MarksArr[2].Trim().ToString(), out Test1M);
                            double.TryParse(MarksArr[3].Trim().ToString(), out Test2M);
                            double.TryParse(MarksArr[4].Trim().ToString(), out Test3M);
                            string Test1 = "", Test2 = "", Test3 = "", MaxHY = "";
                            if (MarksArr[1].Trim().ToUpper() == "NP" || MarksArr[1].Trim().ToUpper() == "NAD" || MarksArr[1].Trim().ToUpper() == "ML" || MarksArr[1].Trim().ToUpper() == "")
                            { MaxHY = MarksArr[1].Trim().ToString(); }
                            if (MaxHYM > 0 && MaxHYM < 100)
                            { MaxHY = MaxHYM.ToString("0.00"); }

                            if (MarksArr[2].Trim().ToUpper() == "NP" || MarksArr[2].Trim().ToUpper() == "NAD" || MarksArr[2].Trim().ToUpper() == "ML" || MarksArr[2].Trim().ToUpper() == "")
                            { Test1 = MarksArr[2].Trim().ToString(); }
                            if (Test1M > 0 && Test1M < 100)
                            { Test1 = Test1M.ToString("0.00"); }

                            if (MarksArr[3].Trim().ToUpper() == "NP" || MarksArr[3].Trim().ToUpper() == "NAD" || MarksArr[3].Trim().ToUpper() == "ML" || MarksArr[3].Trim().ToUpper() == "")
                            { Test2 = MarksArr[3].Trim().ToString(); }
                            if (Test2M > 0 && Test2M < 100)
                            { Test2 = Test2M.ToString("0.00"); }

                            if (MarksArr[4].Trim().ToUpper() == "NP" || MarksArr[4].Trim().ToUpper() == "NAD" || MarksArr[4].Trim().ToUpper() == "ML" || MarksArr[4].Trim().ToUpper() == "")
                            { Test3 = MarksArr[4].Trim().ToString(); }
                            if (Test3M > 0 && Test3M < 100)
                            { Test3 = Test3M.ToString("0.00"); }
                            cmd.Parameters.AddWithValue("@HY", MaxHY);
                            cmd.Parameters.AddWithValue("@Test1", Test1);
                            cmd.Parameters.AddWithValue("@Test2", Test2);
                            cmd.Parameters.AddWithValue("@Test3", Test3);
                            cmd.Parameters.AddWithValue("@BestOfT1T2T3", MarksArr[5]);
                            cmd.Parameters.AddWithValue("@AggreGateHY", MarksArr[6]);
                        }
                        else
                        {
                            double Test4M = 0, Test5M = 0, Test6M = 0, MaxAEM = 0;
                            double.TryParse(MarksArr[1].Trim().ToString(), out MaxAEM);
                            double.TryParse(MarksArr[2].Trim().ToString(), out Test4M);
                            double.TryParse(MarksArr[3].Trim().ToString(), out Test5M);
                            double.TryParse(MarksArr[4].Trim().ToString(), out Test6M);
                            string Test4 = "", Test5 = "", Test6 = "", MaxAE = "";
                            if (MarksArr[1].Trim().ToUpper() == "NP" || MarksArr[1].Trim().ToUpper() == "NAD" || MarksArr[1].Trim().ToUpper() == "ML" || MarksArr[1].Trim().ToUpper() == "")
                            { MaxAE = MarksArr[1].Trim().ToString(); }
                            if (MaxAEM > 0 && MaxAEM < 100)
                            { MaxAE = MaxAEM.ToString("0.00"); }

                            if (MarksArr[2].Trim().ToUpper() == "NP" || MarksArr[2].Trim().ToUpper() == "NAD" || MarksArr[2].Trim().ToUpper() == "ML" || MarksArr[2].Trim().ToUpper() == "")
                            { Test4 = MarksArr[2].Trim().ToString(); }
                            if (Test4M > 0 && Test4M < 100)
                            { Test4 = Test4M.ToString("0.00"); }

                            if (MarksArr[3].Trim().ToUpper() == "NP" || MarksArr[3].Trim().ToUpper() == "NAD" || MarksArr[3].Trim().ToUpper() == "ML" || MarksArr[3].Trim().ToUpper() == "")
                            { Test5 = MarksArr[3].Trim().ToString(); }
                            if (Test5M > 0 && Test5M < 100)
                            { Test5 = Test5M.ToString("0.00"); }

                            if (MarksArr[4].Trim().ToUpper() == "NP" || MarksArr[4].Trim().ToUpper() == "NAD" || MarksArr[4].Trim().ToUpper() == "ML" || MarksArr[4].Trim().ToUpper() == "")
                            { Test6 = MarksArr[4].Trim().ToString(); }
                            if (Test6M > 0 && Test6M < 100)
                            { Test6 = Test6M.ToString("0.00"); }
                            cmd.Parameters.AddWithValue("@AE", MaxAE);
                            cmd.Parameters.AddWithValue("@Test4", Test4);
                            cmd.Parameters.AddWithValue("@Test5", Test5);
                            cmd.Parameters.AddWithValue("@Test6", Test6);
                            cmd.Parameters.AddWithValue("@BestOfT4T5T6", MarksArr[5]);
                            cmd.Parameters.AddWithValue("@AggreGateAE", MarksArr[6]);
                        }
                        cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString().Trim());
                        cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Action", "insert");
                        cmd.Parameters.AddWithValue("@Action2", "Mark");
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Marks Saved Successfully.</span>");
                }
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