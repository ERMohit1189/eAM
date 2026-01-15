using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SaveMarksEntryIXtoXII : Page
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
                    double MaxTest1M = 0, MaxTest2M = 0, MaxTest3M = 0, MaxHYAEM = 0, MaxPracHYAEM = 0;
                    double.TryParse(MaxMarksArr[0].Trim().ToString(), out MaxTest1M);
                    double.TryParse(MaxMarksArr[1].Trim().ToString(), out MaxTest2M);
                    double.TryParse(MaxMarksArr[2].Trim().ToString(), out MaxTest3M);
                    double.TryParse(MaxMarksArr[3].Trim().ToString(), out MaxHYAEM);
                    double.TryParse(MaxMarksArr[4].Trim().ToString().Replace("$", ""), out MaxPracHYAEM);

                    cmd.CommandText = "ICSEMarkEntryIXtoXIIProc";
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
                        cmd.Parameters.AddWithValue("@MaxPracHY", MaxPracHYAEM);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MaxTest4", MaxTest1M);
                        cmd.Parameters.AddWithValue("@MaxTest5", MaxTest2M);
                        cmd.Parameters.AddWithValue("@MaxTest6", MaxTest3M);
                        cmd.Parameters.AddWithValue("@MaxAE", MaxHYAEM);
                        cmd.Parameters.AddWithValue("@MaxPracAE", MaxPracHYAEM);
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
                        cmd.CommandText = "ICSEMarkEntryIXtoXIIProc";
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

                            double Test1M = 0, Test2M = 0, Test3M = 0, MaxHYM = 0, MaxPracHYM = 0;
                            
                            double.TryParse(MarksArr[1].Trim().ToString(), out Test1M);
                            double.TryParse(MarksArr[2].Trim().ToString(), out Test2M);
                            double.TryParse(MarksArr[3].Trim().ToString(), out Test3M);
                            double.TryParse(MarksArr[5].Trim().ToString(), out MaxHYM);
                            double.TryParse(MarksArr[6].Trim().ToString(), out MaxPracHYM);
                            string Test1 = "", Test2 = "", Test3 = "", MaxHY = "", MaxPracHY = "";
                            if (MarksArr[5].Trim().ToUpper() == "NP" || MarksArr[5].Trim().ToUpper() == "NAD" || MarksArr[5].Trim().ToUpper() == "ML" || MarksArr[5].Trim().ToUpper() == "")
                            { MaxHY = MarksArr[5].Trim().ToString(); }
                            if (MaxHYM > 0 && MaxHYM < 100)
                            { MaxHY = MaxHYM.ToString("0.00"); }

                            if (MarksArr[6].Trim().ToUpper() == "NP" || MarksArr[6].Trim().ToUpper() == "NAD" || MarksArr[6].Trim().ToUpper() == "ML" || MarksArr[6].Trim().ToUpper() == "")
                            { MaxPracHY = MarksArr[6].Trim().ToString(); }
                            if (MaxPracHYM > 0 && MaxPracHYM < 100)
                            { MaxPracHY = MaxPracHYM.ToString("0.00"); }

                            if (MarksArr[1].Trim().ToUpper() == "NP" || MarksArr[1].Trim().ToUpper() == "NAD" || MarksArr[1].Trim().ToUpper() == "ML" || MarksArr[1].Trim().ToUpper() == "")
                            { Test1 = MarksArr[1].Trim().ToString(); }
                            if (Test1M > 0 && Test1M < 100)
                            { Test1 = Test1M.ToString("0.00"); }

                            if (MarksArr[2].Trim().ToUpper() == "NP" || MarksArr[2].Trim().ToUpper() == "NAD" || MarksArr[2].Trim().ToUpper() == "ML" || MarksArr[2].Trim().ToUpper() == "")
                            { Test2 = MarksArr[2].Trim().ToString(); }
                            if (Test2M > 0 && Test2M < 100)
                            { Test2 = Test2M.ToString("0.00"); }

                            if (MarksArr[3].Trim().ToUpper() == "NP" || MarksArr[3].Trim().ToUpper() == "NAD" || MarksArr[3].Trim().ToUpper() == "ML" || MarksArr[3].Trim().ToUpper() == "")
                            { Test3 = MarksArr[3].Trim().ToString(); }
                            if (Test3M > 0 && Test3M < 100)
                            { Test3 = Test3M.ToString("0.00"); }
                            cmd.Parameters.AddWithValue("@PracHY", MaxPracHY);
                            cmd.Parameters.AddWithValue("@HY", MaxHY);
                            cmd.Parameters.AddWithValue("@Test1", Test1);
                            cmd.Parameters.AddWithValue("@Test2", Test2);
                            cmd.Parameters.AddWithValue("@Test3", Test3);
                            cmd.Parameters.AddWithValue("@BestOfT1T2T3", MarksArr[4]);
                            cmd.Parameters.AddWithValue("@TotalHY", MarksArr[7]);
                        }
                        else
                        {
                            double Test1M = 0, Test2M = 0, Test3M = 0, MaxAEM = 0, MaxPracAEM = 0;

                            double.TryParse(MarksArr[1].Trim().ToString(), out Test1M);
                            double.TryParse(MarksArr[2].Trim().ToString(), out Test2M);
                            double.TryParse(MarksArr[3].Trim().ToString(), out Test3M);
                            double.TryParse(MarksArr[5].Trim().ToString(), out MaxAEM);
                            double.TryParse(MarksArr[6].Trim().ToString(), out MaxPracAEM);
                            string Test1 = "", Test2 = "", Test3 = "", MaxAE = "", MaxPracAE = "";
                            if (MarksArr[5].Trim().ToUpper() == "NP" || MarksArr[5].Trim().ToUpper() == "NAD" || MarksArr[5].Trim().ToUpper() == "ML" || MarksArr[5].Trim().ToUpper() == "")
                            { MaxAE = MarksArr[5].Trim().ToString(); }
                            if (MaxAEM > 0 && MaxAEM < 100)
                            { MaxAE = MaxAEM.ToString("0.00"); }

                            if (MarksArr[6].Trim().ToUpper() == "NP" || MarksArr[6].Trim().ToUpper() == "NAD" || MarksArr[6].Trim().ToUpper() == "ML" || MarksArr[6].Trim().ToUpper() == "")
                            { MaxPracAE = MarksArr[6].Trim().ToString(); }
                            if (MaxPracAEM > 0 && MaxPracAEM < 100)
                            { MaxPracAE = MaxPracAEM.ToString("0.00"); }

                            if (MarksArr[1].Trim().ToUpper() == "NP" || MarksArr[1].Trim().ToUpper() == "NAD" || MarksArr[1].Trim().ToUpper() == "ML" || MarksArr[1].Trim().ToUpper() == "")
                            { Test1 = MarksArr[1].Trim().ToString(); }
                            if (Test1M > 0 && Test1M < 100)
                            { Test1 = Test1M.ToString("0.00"); }

                            if (MarksArr[2].Trim().ToUpper() == "NP" || MarksArr[2].Trim().ToUpper() == "NAD" || MarksArr[2].Trim().ToUpper() == "ML" || MarksArr[2].Trim().ToUpper() == "")
                            { Test2 = MarksArr[2].Trim().ToString(); }
                            if (Test2M > 0 && Test2M < 100)
                            { Test2 = Test2M.ToString("0.00"); }

                            if (MarksArr[3].Trim().ToUpper() == "NP" || MarksArr[3].Trim().ToUpper() == "NAD" || MarksArr[3].Trim().ToUpper() == "ML" || MarksArr[3].Trim().ToUpper() == "")
                            { Test3 = MarksArr[3].Trim().ToString(); }
                            if (Test3M > 0 && Test3M < 100)
                            { Test3 = Test3M.ToString("0.00"); }
                            cmd.Parameters.AddWithValue("@PracAE", MaxPracAE);
                            cmd.Parameters.AddWithValue("@AE", MaxAE);
                            cmd.Parameters.AddWithValue("@Test4", Test1);
                            cmd.Parameters.AddWithValue("@Test5", Test2);
                            cmd.Parameters.AddWithValue("@Test6", Test3);
                            cmd.Parameters.AddWithValue("@BestOfT4T5T6", MarksArr[4]);
                            cmd.Parameters.AddWithValue("@TotalAE", MarksArr[7]);
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