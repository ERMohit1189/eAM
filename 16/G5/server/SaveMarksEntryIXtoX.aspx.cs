using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SaveMarksEntryIXtoX : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string Evaluation = Request.Form["Evaluation"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string LoginName = Request.Form["LoginName"].ToString().Trim();
        string MaxMarks = Request.Form["MaxMarks"].ToString().Trim();
        string Marks = Request.Form["Marks"].ToString().Trim();
        string Medium = Request.Form["Medium"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();


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
               // double MaxMarkschecks = double.TryParse(MaxMarksArr1[0], out mno);
             //   double Markschecks = double.Parse(MarksArr1[5] == "" ? "0" : MarksArr1[5]);
                //if (ClassName == "IX")
                //{
                if (double.TryParse(MaxMarksArr1[0], out mno) == false || double.TryParse(MaxMarksArr1[1], out mno) == false || double.TryParse(MaxMarksArr1[2], out mno) == false || double.TryParse(MaxMarksArr1[3], out mno) == false || double.TryParse(MaxMarksArr1[4], out mno) == false)
                    {
                        error1 = true;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type only Numbers in Maxsmarks Box! If Maxsmarks not available type 0.')", true);
                        return;
                    }
                    else
                    {
                        error1 = false;
                    }
               // }
                //if (ClassName == "X" && Evaluation == "TERM1")
                //{
                //    if (double.TryParse(MaxMarksArr1[0], out mno) == false || double.TryParse(MaxMarksArr1[1], out mno) == false || double.TryParse(MaxMarksArr1[2], out mno) == false || double.TryParse(MaxMarksArr1[3], out mno) == false || double.TryParse(MaxMarksArr1[4], out mno) == false || double.TryParse(MaxMarksArr1[5], out mno) == false)
                //    {
                //        error1 = true;
                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type only Numbers in Maxsmarks Box! If Maxsmarks not available type 0.')", true);
                //        return;
                //    }
                //    else
                //    {
                //        error1 = false;
                //    }
                //}
                //if (ClassName == "X" && Evaluation == "TERM2")
                //{
                //    if (double.TryParse(MaxMarksArr1[0], out mno) == false || double.TryParse(MaxMarksArr1[1], out mno) == false || double.TryParse(MaxMarksArr1[2], out mno) == false || double.TryParse(MaxMarksArr1[3], out mno) == false || double.TryParse(MaxMarksArr1[4], out mno) == false || double.TryParse(MaxMarksArr1[5], out mno) == false || double.TryParse(MaxMarksArr1[6], out mno) == false || double.TryParse(MaxMarksArr1[7], out mno) == false || double.TryParse(MaxMarksArr1[8], out mno) == false)
                //    {
                //        error1 = true;
                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type only Numbers in Maxsmarks Box! If Maxsmarks not available type 0.')", true);
                //        return;
                //    }
                //    else
                //    {
                //        error1 = false;
                //    }
                //}
                
                string[] MarksFullArr1 = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                for (int j = 0; j < MarksFullArr1.Length - j; j++)
                {

                    string[] MarksArr1 = MarksFullArr1[j].Split(new string[] { "##" }, StringSplitOptions.None);
                    double no;

                    //if (ClassName == "IX")
                    //{
                        if (MarksArr1[1].ToLower() != "" && MarksArr1[1].ToLower() != "ab" && MarksArr1[1].ToLower() != "nad" && MarksArr1[1].ToLower() != "ml")
                        {
                            if (double.TryParse(MarksArr1[1], out no) == false)
                            {
                                error1 = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
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
                        if (MarksArr1[2].ToLower() != "" && MarksArr1[2].ToLower() != "ab" && MarksArr1[2].ToLower() != "nad" && MarksArr1[2].ToLower() != "ml")
                        {
                            if (double.TryParse(MarksArr1[2], out no) == false)
                            {
                                error1 = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
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
                        if (MarksArr1[3].ToLower() != "" && MarksArr1[3].ToLower() != "ab" && MarksArr1[3].ToLower() != "nad" && MarksArr1[3].ToLower() != "ml")
                        {
                            if (double.TryParse(MarksArr1[3], out no) == false)
                            {
                                error1 = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
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
                        if (MarksArr1[4].ToLower() != "" && MarksArr1[4].ToLower() != "ab" && MarksArr1[4].ToLower() != "nad" && MarksArr1[4].ToLower() != "ml")
                        {
                            if (double.TryParse(MarksArr1[4], out no) == false)
                            {
                                error1 = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
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
                        if (MarksArr1[5].ToLower() != "" && MarksArr1[5].ToLower() != "ab" && MarksArr1[5].ToLower() != "nad" && MarksArr1[5].ToLower() != "ml")
                        {
                            double MaxMarkschecks = double.Parse(MaxMarksArr1[3] == "" ? "0" : MaxMarksArr1[3]);
                            double Markschecks = double.Parse(MarksArr1[5] == "" ? "0" : MarksArr1[5]);
                            if (double.TryParse(MarksArr1[5], out no) == false)
                            {
                                error1 = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                                break;
                            }
                            else if(MaxMarkschecks<Markschecks)
                            //else if (double.Parse(MaxMarksArr1[3] == "" ? "0" : MaxMarksArr1[3]) < double.Parse(MarksArr1[5] == "" ? "0" : MarksArr1[5]))
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
                        if (MarksArr1[6].ToLower() != "" && MarksArr1[6].ToLower() != "ab" && MarksArr1[6].ToLower() != "nad" && MarksArr1[6].ToLower() != "ml")
                        {
                            double MaxMarkschecks = double.Parse(MaxMarksArr1[3] == "" ? "0" : MaxMarksArr1[3]);
                            double Markschecks = double.Parse(MarksArr1[6] == "" ? "0" : MarksArr1[6]);
                            if (double.TryParse(MarksArr1[6], out no) == false)
                            {
                                error1 = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                                break;
                            }
                            else if (MaxMarkschecks < Markschecks)
                            //else if (double.Parse(MaxMarksArr1[5] == "" ? "0" : MaxMarksArr1[5]) < double.Parse(MarksArr1[6] == "" ? "0" : MarksArr1[6]))
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
                    //}
                    //if (ClassName == "X" && Evaluation == "TERM1")
                    //{
                    //    if (MarksArr1[1].ToLower() != "" && MarksArr1[1].ToLower() != "ab" && MarksArr1[1].ToLower() != "nad" && MarksArr1[1].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[1], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[0] == "" ? "0" : MaxMarksArr1[0]) < double.Parse(MarksArr1[1] == "" ? "0" : MarksArr1[1]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[2].ToLower() != "" && MarksArr1[2].ToLower() != "ab" && MarksArr1[2].ToLower() != "nad" && MarksArr1[2].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[2], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[1] == "" ? "0" : MaxMarksArr1[1]) < double.Parse(MarksArr1[2] == "" ? "0" : MarksArr1[2]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[3].ToLower() != "" && MarksArr1[3].ToLower() != "ab" && MarksArr1[3].ToLower() != "nad" && MarksArr1[3].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[3], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[2] == "" ? "0" : MaxMarksArr1[2]) < double.Parse(MarksArr1[3] == "" ? "0" : MarksArr1[3]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[4].ToLower() != "" && MarksArr1[4].ToLower() != "ab" && MarksArr1[4].ToLower() != "nad" && MarksArr1[4].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[4], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[3] == "" ? "0" : MaxMarksArr1[3]) < double.Parse(MarksArr1[4] == "" ? "0" : MarksArr1[4]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[5].ToLower() != "" && MarksArr1[5].ToLower() != "ab" && MarksArr1[5].ToLower() != "nad" && MarksArr1[5].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[5], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[4] == "" ? "0" : MaxMarksArr1[4]) < double.Parse(MarksArr1[5] == "" ? "0" : MarksArr1[5]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                      
                    //}

                    //if (ClassName == "X" && Evaluation == "TERM2")
                    //{
                    //    if (MarksArr1[1].ToLower() != "" && MarksArr1[1].ToLower() != "ab" && MarksArr1[1].ToLower() != "nad" && MarksArr1[1].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[1], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[0] == "" ? "0" : MaxMarksArr1[0]) < double.Parse(MarksArr1[1] == "" ? "0" : MarksArr1[1]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[2].ToLower() != "" && MarksArr1[2].ToLower() != "ab" && MarksArr1[2].ToLower() != "nad" && MarksArr1[2].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[2], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[1] == "" ? "0" : MaxMarksArr1[1]) < double.Parse(MarksArr1[2] == "" ? "0" : MarksArr1[2]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[3].ToLower() != "" && MarksArr1[3].ToLower() != "ab" && MarksArr1[3].ToLower() != "nad" && MarksArr1[3].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[3], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[2] == "" ? "0" : MaxMarksArr1[2]) < double.Parse(MarksArr1[3] == "" ? "0" : MarksArr1[3]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[4].ToLower() != "" && MarksArr1[4].ToLower() != "ab" && MarksArr1[4].ToLower() != "nad" && MarksArr1[4].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[4], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[3] == "" ? "0" : MaxMarksArr1[3]) < double.Parse(MarksArr1[4] == "" ? "0" : MarksArr1[4]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[5].ToLower() != "" && MarksArr1[5].ToLower() != "ab" && MarksArr1[5].ToLower() != "nad" && MarksArr1[5].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[5], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[4] == "" ? "0" : MaxMarksArr1[4]) < double.Parse(MarksArr1[5] == "" ? "0" : MarksArr1[5]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[6].ToLower() != "" && MarksArr1[6].ToLower() != "ab" && MarksArr1[6].ToLower() != "nad" && MarksArr1[6].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[6], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[5] == "" ? "0" : MaxMarksArr1[5]) < double.Parse(MarksArr1[6] == "" ? "0" : MarksArr1[6]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[7].ToLower() != "" && MarksArr1[7].ToLower() != "ab" && MarksArr1[7].ToLower() != "nad" && MarksArr1[7].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[7], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[6] == "" ? "0" : MaxMarksArr1[6]) < double.Parse(MarksArr1[7] == "" ? "0" : MarksArr1[7]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[8].ToLower() != "" && MarksArr1[8].ToLower() != "ab" && MarksArr1[8].ToLower() != "nad" && MarksArr1[8].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[8], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[7] == "" ? "0" : MaxMarksArr1[7]) < double.Parse(MarksArr1[8] == "" ? "0" : MarksArr1[8]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //    if (MarksArr1[9].ToLower() != "" && MarksArr1[9].ToLower() != "ab" && MarksArr1[9].ToLower() != "nad" && MarksArr1[9].ToLower() != "ml")
                    //    {
                    //        if (double.TryParse(MarksArr1[9], out no) == false)
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Type ML, NAD, AB or Numbers in Box!')", true);
                    //            break;
                    //        }
                    //        else if (double.Parse(MaxMarksArr1[8] == "" ? "0" : MaxMarksArr1[8]) < double.Parse(MarksArr1[9] == "" ? "0" : MarksArr1[9]))
                    //        {
                    //            error1 = true;
                    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Wrong Input:: Enter marks only less then max marks!')", true);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            error1 = false;
                    //        }
                    //    }
                    //}
                }
                if (error1 == false)
                {
                    string[] MaxMarksArr = MaxMarks.Split(new string[] { "##" }, StringSplitOptions.None);
                    cmd.CommandText = "Sp_UPEIXtoX";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SubjectId", SubjectId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@PaperId", PaperId.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Medium", Medium.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Evaluation", Evaluation.ToString().Trim());
                    cmd.Parameters.AddWithValue("@ClassName", ClassName.ToString().Trim());
                    if (Evaluation.ToLower() == "term1")
                    {
                        cmd.Parameters.AddWithValue("@UT1", MaxMarksArr[0]);
                        cmd.Parameters.AddWithValue("@UT2", MaxMarksArr[1]);
                        cmd.Parameters.AddWithValue("@UT3", MaxMarksArr[2]);
                        cmd.Parameters.AddWithValue("@HYTH", MaxMarksArr[3]);
                        cmd.Parameters.AddWithValue("@HYASSI", MaxMarksArr[4]);
                        cmd.Parameters.AddWithValue("@HYViva", MaxMarksArr[5]);
                    }
                    if (Evaluation.ToLower() == "term2")
                    {
                        cmd.Parameters.AddWithValue("@UT4", MaxMarksArr[0]);
                        cmd.Parameters.AddWithValue("@UT5", MaxMarksArr[1]);
                        cmd.Parameters.AddWithValue("@UT6", MaxMarksArr[2]);
                        cmd.Parameters.AddWithValue("@AETH", MaxMarksArr[3]);
                        cmd.Parameters.AddWithValue("@AEASSI", MaxMarksArr[4]);
                        cmd.Parameters.AddWithValue("@AEViva", MaxMarksArr[5]);
                    }
                    //if (ClassName == "X" && Evaluation.ToLower() == "term1")
                    //{
                    //    cmd.Parameters.AddWithValue("@MonthlyTest", MaxMarksArr[0]);
                    //    cmd.Parameters.AddWithValue("@UT1", MaxMarksArr[1]);
                    //    cmd.Parameters.AddWithValue("@UT2", MaxMarksArr[2]);
                    //    cmd.Parameters.AddWithValue("@UT3", MaxMarksArr[3]);
                    //    cmd.Parameters.AddWithValue("@HYTH", MaxMarksArr[4]);
                    //    cmd.Parameters.AddWithValue("@HYASSI", MaxMarksArr[5]);
                    //    cmd.Parameters.AddWithValue("@HYViva", MaxMarksArr[6]);
                    //}
                    //if (ClassName == "X" && Evaluation.ToLower() == "term2")
                    //{
                    //    cmd.Parameters.AddWithValue("@Pre1TH", MaxMarksArr[0]);
                    //    cmd.Parameters.AddWithValue("@Pre1ASSI", MaxMarksArr[1]);
                    //    cmd.Parameters.AddWithValue("@Pre1Viva", MaxMarksArr[2]);
                    //    cmd.Parameters.AddWithValue("@Pre2TH", MaxMarksArr[3]);
                    //    cmd.Parameters.AddWithValue("@Pre2ASSI", MaxMarksArr[4]);
                    //    cmd.Parameters.AddWithValue("@Pre2Viva", MaxMarksArr[5]);
                    //    cmd.Parameters.AddWithValue("@Pre3TH", MaxMarksArr[6]);
                    //    cmd.Parameters.AddWithValue("@Pre3ASSI", MaxMarksArr[7]);
                    //    cmd.Parameters.AddWithValue("@Pre3Viva", MaxMarksArr[8]);
                    //}
                    cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                    cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                    cmd.Parameters.AddWithValue("@ActionName", "MaxMarks");
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    string[] MarksFullArr = Marks.Split(new string[] { "$" }, StringSplitOptions.None);

                    for (int i = 0; i < MarksFullArr.Length - 1; i++)
                    {
                        string[] MarksArr = MarksFullArr[i].Split(new string[] { "##" }, StringSplitOptions.None);
                        cmd.CommandText = "Sp_UPEIXtoX";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@SubjectId", SubjectId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@PaperId", PaperId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Medium", Medium.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Evaluation", Evaluation.ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", MarksArr[0]);
                        cmd.Parameters.AddWithValue("@ClassName", ClassName.ToString().Trim());
                        if (Evaluation.ToLower() == "term1")
                        {
                            cmd.Parameters.AddWithValue("@UT1", MarksArr[1]);
                            cmd.Parameters.AddWithValue("@UT2", MarksArr[2]);
                            cmd.Parameters.AddWithValue("@UT3", MarksArr[3]);
                            cmd.Parameters.AddWithValue("@HYTH", MarksArr[4]);
                            cmd.Parameters.AddWithValue("@HYASSI", MarksArr[5]);
                            cmd.Parameters.AddWithValue("@HYViva", MarksArr[6]);
                            cmd.Parameters.AddWithValue("@HYTotal", MarksArr[7]);
                            cmd.Parameters.AddWithValue("@HYGrade", MarksArr[8]);
                        }
                        if (Evaluation.ToLower() == "term2")
                        {
                            cmd.Parameters.AddWithValue("@UT4", MarksArr[1]);
                            cmd.Parameters.AddWithValue("@UT5", MarksArr[2]);
                            cmd.Parameters.AddWithValue("@UT6", MarksArr[3]);
                            cmd.Parameters.AddWithValue("@AETH", MarksArr[4]);
                            cmd.Parameters.AddWithValue("@AEASSI", MarksArr[5]);
                            cmd.Parameters.AddWithValue("@AEViva", MarksArr[6]);
                            cmd.Parameters.AddWithValue("@AETotal", MarksArr[7]);
                            cmd.Parameters.AddWithValue("@AEGrade", MarksArr[8]);
                        }
                        //if (ClassName == "X" && Evaluation.ToLower() == "term1")
                        //{
                        //    cmd.Parameters.AddWithValue("@MonthlyTest", MarksArr[1]);
                        //    cmd.Parameters.AddWithValue("@UT1", MarksArr[2]);
                        //    cmd.Parameters.AddWithValue("@UT2", MarksArr[3]);
                        //    cmd.Parameters.AddWithValue("@HYTH", MarksArr[4]);
                        //    cmd.Parameters.AddWithValue("@HYASSI", MarksArr[5]);
                        //    cmd.Parameters.AddWithValue("@HYViva", MarksArr[6]);
                        //    cmd.Parameters.AddWithValue("@HYTotal", MarksArr[7]);
                        //    cmd.Parameters.AddWithValue("@HYGrade", MarksArr[8]);
                        //}
                        //if (ClassName == "X" && Evaluation.ToLower() == "term2")
                        //{
                        //    cmd.Parameters.AddWithValue("@Pre1TH", MarksArr[1]);
                        //    cmd.Parameters.AddWithValue("@Pre1ASSI", MarksArr[2]);
                        //    cmd.Parameters.AddWithValue("@Pre1Viva", MarksArr[3]);
                        //    cmd.Parameters.AddWithValue("@Pre2TH", MarksArr[4]);
                        //    cmd.Parameters.AddWithValue("@Pre2ASSI", MarksArr[5]);
                        //    cmd.Parameters.AddWithValue("@Pre2Viva", MarksArr[6]);
                        //    cmd.Parameters.AddWithValue("@Pre3TH", MarksArr[7]);
                        //    cmd.Parameters.AddWithValue("@Pre3ASSI", MarksArr[8]);
                        //    cmd.Parameters.AddWithValue("@Pre3Viva", MarksArr[9]);
                        //}

                        cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@LoginName", LoginName.ToString().Trim());
                        cmd.Parameters.AddWithValue("@ActionName", "Marks");
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    transaction.Commit();
                    Response.Write("<span class='label label-success' style='font-size: 100% !important;'>Marks Saved Successfully.</span>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<span class='label label-danger' style='font-size: 100% !important;'>Unfortunately an error occurred during mark entry. Please try again !</span>");
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