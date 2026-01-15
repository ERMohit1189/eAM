using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G6_Studentwisecumulative_XI_1718Server : System.Web.UI.Page
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionID = Request.Form["SectionID"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string session = Request.Form["session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string status = Request.Form["status"].ToString().Trim();
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string AttendanceTyp = Request.Form["AttendanceTyp"].ToString().Trim();
        string BranchID = Request.Form["BranchID"].ToString().Trim();


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = conn;
                cmd.CommandText = "StudentWiseCumulative_XI_2021";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@SectionID", SectionID.Trim());
                cmd.Parameters.AddWithValue("@BranchID", BranchID.Trim());
                if (SrNo != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                cmd.Parameters.AddWithValue("@status", status.Trim());
                cmd.Parameters.AddWithValue("@action", "Student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Response.Write("<div class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                        Response.Write("<div id='break_div' class='term1' style='page-break-after: always;'>");
                        Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                        Response.Write("<div class='table-responsive table-responsive2'>");
                        Response.Write("<table class='table mp-table p-table-bordered table-bordered trBg' style='margin-bottom: 0 !important;'><tbody>");
                        Response.Write("<tr><td colspan='3' class='text-center'><b>STUDENT WISE CUMULATIVE " + session.Trim() + "</b></td></tr><tr><td style='width: 21.6%;'><b>Student's Name</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["StudentName"].ToString() + "(" + ds.Tables[0].Rows[i]["admissionNo"].ToString() + ")</span></td><td style='width: 20%;'><b>Father's Name</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["FatherName"].ToString() + "</span><span></span></td><td style='width: 20%;'><b>D.O.B</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["DOB"].ToString() + "</span></td></tr></tbody>");
                        Response.Write("<tr><td colspan='3' class='text-center'>");

                        cmd.CommandText = "StudentWiseCumulative_XI_2021";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@SrNo", ds.Tables[0].Rows[i]["admissionNo"].ToString());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dss = new DataSet();
                        das.Fill(dss);
                        cmd.Parameters.Clear();

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important' rowspan='2'>EXAM<i class='fa fa-arrow-right'></i></th><th colspan='2'>U.T 1</th><th colspan='2'>U.T 2</th><th rowspan='2'>Conversion <br>into (10)<br>(A)</th><th colspan='2'>Practical/I.A</th><th colspan='2'>Term-1</th><th rowspan='2'>Conversion<br>(A+B) into <br>Board Marks<br></th><th>Total</th><th rowspan='2'>Grade</th><th colspan='2'>U.T 3</th><th colspan='2'>U.T 4</th><th rowspan='2'>Conversion <br>into (10)<br>(C)</th><th colspan='2'>Practical/I.A</th><th colspan='2'>Term-2</th><th rowspan='2'>Conversion<br>(C+D) into <br>Board Marks</th><th>Total</th><th rowspan='2'>Grade</th><th>Grand Total</th><th>Conversion into</th></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.(B)</th><th>100</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.(D)</th><th>100</th><th>200</th><th>100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='13' class='text-center'><B>TERM 1</B></td><td colspan='13' class='text-center'><B>TERM 2</B></td></tr>");
                        double Gpercentle = 0; double GTest1 = 0; double GTest2 = 0; double GSAT = 0; double GPrac = 0; double Gtotal = 0;
                        double Gpercentle_2 = 0; double GTest1_2 = 0; double GTest2_2 = 0; double GSAT_2 = 0; double GPrac_2 = 0; double Gtotal_2 = 0;
                       
                        double totalmarks1 = 0; double totalmmmarks1 = 0;
                        double totalmarks_2 = 0; double totalmmmarks_2 = 0;
                        double percentle1 = 0; double ObtMarks1 = 0;
                        double percentle_2 = 0; double ObtMarks_2 = 0;
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                double Test1 = 0; double Test2 = 0; double SAT = 0; double Prac = 0; double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0;
                                double Test1_2 = 0; double Test2_2 = 0; double MA_2 = 0; double SE_2 = 0; double SAT_2 = 0; double Prac_2 = 0; double MaxMarks1_2 = 0; double MaxMarks2_2 = 0; double MaxMarks3_2 = 0; double MaxMarks4_2 = 0;

                                if (dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test1 = double.Parse(dss.Tables[0].Rows[p]["Test1"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                else {
                                    Test2 = double.Parse(dss.Tables[0].Rows[p]["Test2"].ToString().Trim());
                                }
                                if (dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                else { SAT = double.Parse(dss.Tables[0].Rows[p]["SAT"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Prac"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Prac = double.Parse(dss.Tables[0].Rows[p]["Prac"].ToString().Trim()); }

                                if (dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks1 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks3 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks4 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim()); }

                                totalmarks1 = Test1 > Test2 ? Test1 : Test2;
                                totalmmmarks1 = Test1 > Test2 ? MaxMarks1 : MaxMarks2;
                                double convintoboard = 0;
                                if (totalmmmarks1 > 0)
                                {
                                    percentle1 = (((totalmarks1) * 10) / totalmmmarks1);
                                    convintoboard= (((double.Parse(percentle1.ToString("0")) + SAT) / (10 + MaxMarks3)) * MaxMarks3);
                                    ObtMarks1 = (double.Parse(convintoboard.ToString("0")) + double.Parse(Prac.ToString("0")));
                                    ObtMarks1 = double.Parse(ObtMarks1.ToString("0"));
                                    GTest1 = GTest1 + Test1;
                                    GTest2 = GTest2 + Test2;
                                    Gpercentle = Gpercentle + double.Parse(percentle1.ToString());
                                    GPrac = GPrac + Prac;
                                    GSAT = GSAT + SAT;
                                    Gtotal = Gtotal + ObtMarks1;
                                }
                                Response.Write("<td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["SubjectName"].ToString() + "</td>");
                                Response.Write("<td>" + MaxMarks1.ToString() + "</td><td>" + Test1.ToString() + "</td><td>" + MaxMarks2.ToString() + "</td><td>" + Test2.ToString() + "</td>");
                                Response.Write("<td>" + percentle1.ToString() + "</td>");
                                Response.Write("<td>" + MaxMarks4.ToString() + "</td><td>" + Prac.ToString() + "</td><td>" + MaxMarks3.ToString() + "</td><td>" + SAT.ToString() + "</td>");
                                Response.Write("<td>" + convintoboard.ToString("0.0") + "</td><td>" + ObtMarks1.ToString("0") + "</td>");
                                Response.Write("<td>" + grade(double.Parse(ObtMarks1.ToString("0"))) + "</td>");

                                if (dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test1_2 = double.Parse(dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test2_2 = double.Parse(dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Prac_2 = double.Parse(dss.Tables[0].Rows[p]["Prac_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { SAT_2 = double.Parse(dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim()); }


                                if (dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks1_2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks2_2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks3_2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks3_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks4_2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks4_2"].ToString().Trim()); }
                                totalmarks_2 = Test1_2 > Test2_2 ? Test1_2 : Test2_2;
                                totalmmmarks_2 = Test1_2 > Test2_2 ? MaxMarks1_2 : MaxMarks2_2;
                                double convintoboard2 = 0;
                                if (totalmmmarks_2 > 0 )
                                {
                                    percentle_2 = double.Parse((((totalmarks_2) * 10) / totalmmmarks_2).ToString("0.00"));
                                    convintoboard2 = (((double.Parse(percentle_2.ToString("0")) + SAT_2) / (10 + MaxMarks3_2)) * MaxMarks3_2);
                                    ObtMarks_2 = (double.Parse(convintoboard2.ToString("0")) + double.Parse(Prac_2.ToString("0")));
                                    ObtMarks_2 = double.Parse(ObtMarks_2.ToString("0"));
                                    GTest1_2 = GTest1_2 + Test1_2;
                                    GTest2_2 = GTest2_2 + Test1_2;
                                    Gpercentle_2 = Gpercentle_2 + double.Parse(percentle_2.ToString());
                                    GPrac_2 = GPrac_2 + Prac_2;
                                    GSAT_2 = GSAT_2 + SAT_2;
                                    Gtotal_2 = Gtotal_2 + ObtMarks_2;
                                }
                                Response.Write("<td>" + MaxMarks1_2.ToString() + "</td><td>" + Test1_2.ToString() + "</td><td>" + MaxMarks2_2.ToString() + "</td><td>" + Test2_2.ToString() + "</td>");
                                Response.Write("<td>" + percentle_2.ToString() + "</td>");
                                Response.Write("<td>" + MaxMarks4_2.ToString() + "</td><td>" + Prac_2.ToString() + "</td><td>" + MaxMarks3_2.ToString() + "</td><td>" + SAT_2.ToString() + "</td>");
                                Response.Write("<td>" + convintoboard2.ToString("0.0") + "</td><td>" + ObtMarks_2.ToString() + "</td>");
                                Response.Write("<td>" + grade(double.Parse(ObtMarks_2.ToString("0"))) + "</td>");
                                Response.Write("<td>" + (double.Parse(ObtMarks1.ToString("0")) + double.Parse(ObtMarks_2.ToString("0"))).ToString("0") + "</td>");
                                Response.Write("<td>" + ((ObtMarks1 + ObtMarks_2) / 2).ToString("0") + "</td>");
                                Response.Write("</tr>");
                            }
                            if (dss.Tables[1].Rows.Count > 0)
                            {
                                double Test1 = 0; double Test2 = 0; double SAT = 0; double Prac = 0; double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0;
                                double Test1_2 = 0; double Test2_2 = 0; double MA_2 = 0; double SE_2 = 0; double SAT_2 = 0; double Prac_2 = 0; double MaxMarks1_2 = 0; double MaxMarks2_2 = 0; double MaxMarks3_2 = 0; double MaxMarks4_2 = 0;

                                Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important' rowspan='2'>EXAM<i class='fa fa-arrow-right'></i></th><th colspan='2'>U.T 1</th><th colspan='2'>U.T 2</th><th rowspan='2'>Conversion <br>into (5)<br>(A)</th><th colspan='2'>Practical/I.A</th><th colspan='2'>Term-1</th><th rowspan='2'>Conversion<br>(A+B) into <br>Board Marks<br></th><th>Total</th><th rowspan='2'>Grade</th><th colspan='2'>U.T 3</th><th colspan='2'>U.T 4</th><th rowspan='2'>Conversion <br>into (5)<br>(C)</th><th colspan='2'>Practical/I.A</th><th colspan='2'>Term-2</th><th rowspan='2'>Conversion<br>(C+D) into <br>Board Marks</th><th>Total</th><th rowspan='2'>Grade</th><th>Grand Total</th><th>Conversion into</th></tr>");
                                Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.(B)</th><th>100</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.</th><th>M.M</th><th>M.O.(D)</th><th>100</th><th>200</th><th>100</th></tr>");
                                Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='13' class='text-center'><B>TERM 1</B></td><td colspan='13' class='text-center'><B>TERM 2</B></td></tr>");
                                for (int p = 0; p < dss.Tables[1].Rows.Count; p++)
                                {
                                    if (dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test1 = double.Parse(dss.Tables[1].Rows[p]["Test1"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test2 = double.Parse(dss.Tables[1].Rows[p]["Test2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { SAT = double.Parse(dss.Tables[1].Rows[p]["SAT"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Prac = double.Parse(dss.Tables[1].Rows[p]["Prac"].ToString().Trim()); }

                                    if (dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks1 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks3 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks3"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks4 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks4"].ToString().Trim()); }

                                    totalmarks1 = Test1 > Test2 ? Test1 : Test2;
                                    totalmmmarks1 = Test1 > Test2 ? MaxMarks1 : MaxMarks2;
                                    double convintoboard = 0;
                                    if (totalmmmarks1 > 0)
                                    {
                                        percentle1 = double.Parse((((totalmarks1) * 10) / totalmmmarks1).ToString("0.00"));
                                        convintoboard = (((double.Parse(percentle1.ToString("0")) + SAT) / (10 + MaxMarks3)) * MaxMarks3);
                                        ObtMarks1 = (double.Parse(convintoboard.ToString("0")) + double.Parse(Prac.ToString("0")));
                                        ObtMarks1 = double.Parse(ObtMarks1.ToString("0"));
                                        //GTest1 = GTest1 + Test1;
                                        //GTest2 = GTest2 + Test2;
                                        //Gpercentle = Gpercentle + double.Parse(percentle1.ToString());
                                        //GPrac = GPrac + Prac;
                                        //GSAT = GSAT + SAT;
                                        //Gtotal = Gtotal + ObtMarks1;
                                    }
                                    Response.Write("<td style='text-align:left; width:23%; !important'>" + dss.Tables[1].Rows[p]["SubjectName"].ToString() + "</td>");
                                    Response.Write("<td>" + MaxMarks1.ToString() + "</td><td>" + Test1.ToString() + "</td><td>" + MaxMarks2.ToString() + "</td><td>" + Test2.ToString() + "</td>");
                                    Response.Write("<td>" + percentle1.ToString() + "</td>");
                                    Response.Write("<td>" + MaxMarks4.ToString() + "</td><td>" + Prac.ToString() + "</td><td>" + MaxMarks3.ToString() + "</td><td>" + SAT.ToString() + "</td>");
                                    Response.Write("<td>" + convintoboard.ToString("0.0") + "</td><td>" + ObtMarks1.ToString() + "</td>");
                                    Response.Write("<td>" + grade(ObtMarks1) + "</td>");

                                    if (dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test1_2 = double.Parse(dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test2_2 = double.Parse(dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Prac_2 = double.Parse(dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { SAT_2 = double.Parse(dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim()); }


                                    if (dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks1_2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks2_2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks3_2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks3_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks4_2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks4_2"].ToString().Trim()); }
                                    totalmarks_2 = Test1_2 > Test2_2 ? Test1_2 : Test2_2;
                                    totalmmmarks_2 = Test1_2 > Test2_2 ? MaxMarks1_2 : MaxMarks2_2;
                                    double convintoboard2 = 0;
                                    if (totalmmmarks_2 > 0)
                                    {
                                        percentle_2 = double.Parse((((totalmarks_2) * 5) / totalmmmarks_2).ToString("0.00"));
                                        convintoboard2 = (((double.Parse(percentle_2.ToString("0")) + SAT_2) / (10 + MaxMarks3_2)) * MaxMarks3_2);
                                        ObtMarks_2 = (double.Parse(convintoboard2.ToString("0")) + double.Parse(Prac_2.ToString("0")));
                                        ObtMarks_2 = double.Parse(ObtMarks_2.ToString("0"));
                                        //GTest1_2 = GTest1_2 + Test1_2;
                                        //GTest2_2 = GTest2_2 + Test1_2;
                                        //Gpercentle_2 = Gpercentle_2 + double.Parse(percentle_2.ToString());
                                        //GPrac_2 = GPrac_2 + Prac_2;
                                        //GSAT_2 = GSAT_2 + SAT_2;
                                        //Gtotal_2 = Gtotal_2 + ObtMarks_2;
                                    }
                                    Response.Write("<td>" + MaxMarks1_2.ToString() + "</td><td>" + Test1_2.ToString() + "</td><td>" + MaxMarks2_2.ToString() + "</td><td>" + Test2_2.ToString() + "</td>");
                                    Response.Write("<td>" + percentle_2.ToString() + "</td>");
                                    Response.Write("<td>" + MaxMarks4_2.ToString() + "</td><td>" + Prac_2.ToString() + "</td><td>" + MaxMarks3_2.ToString() + "</td><td>" + SAT_2.ToString() + "</td>");
                                    Response.Write("<td>" + convintoboard2.ToString("0.0") + "</td><td>" + ObtMarks_2.ToString() + "</td>");
                                    Response.Write("<td>" + grade(ObtMarks_2) + "</td>");
                                    Response.Write("<td>" + (ObtMarks1 + ObtMarks_2).ToString() + "</td>");
                                    Response.Write("<td>" + ((ObtMarks1 + ObtMarks_2) / 2).ToString("0") + "</td>");
                                    Response.Write("</tr>");
                                }
                            }
                        }
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal.ToString("0") + "</td><td></td>");
                        Response.Write("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal_2.ToString("0") + "</td><td></td><td>" + (Gtotal + Gtotal_2).ToString("0") + "</td><td></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Percentage</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ((Gtotal * 100) / (100 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ((Gtotal_2 * 100) / (100 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td><td>" + (((Gtotal + Gtotal_2) * 100) / (200 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("</td></tr>");



                        Response.Write("<tr><td colspan='21' class='text-center'>");
                        Response.Write("<br><table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        sql = "select MonthName from MonthMaster where ClassId=" + ClassId + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and CardType=" + ds.Tables[0].Rows[i]["CardId"].ToString() + " and MOD='I' and typeofAdd='Regular' and ForMonth=1";
                        var monthDt = oo.Fetchdata(sql);
                        string months = ""; string monthsblanks = "";
                        if (monthDt.Rows.Count > 0)
                        {
                            for (int n = 0; n < monthDt.Rows.Count; n++)
                            {
                                months = months + "<td>" + monthDt.Rows[n]["MonthName"].ToString() + "</td>";
                                monthsblanks = monthsblanks + "<td></td>";
                            }
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:19% !important'>Months</td><td>Arrear</td>" + months + "</tr>");
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:19% !important'>Fee Report</td><td></td>" + monthsblanks + "</tr>");
                        }

                        Response.Write("</tbody></table>");
                        Response.Write("</td></tr>");
                        Response.Write("<tr><td colspan='21' class='text-center'>");
                        Response.Write("<h3 style='text-align:center; width:100%; margin-top:15px !important;font-weight: bold; font-size: 13px !important;'>Activity Record</h3>");
                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><th style='text-align:center; width:15%; !important'>Date</th><th style='text-align:center; width:50%; !important'>Activity</th><th style='text-align:center; width:15%; !important'>Grade</th></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td><br><br></td><td></td><td></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td><br><br></td><td></td><td></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td><br><br></td><td></td><td></td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("</td></tr>");

                        Response.Write("<tr><td colspan='21' class='text-center'>");
                        Response.Write("<div class='p-pad-3 p-tot-tit'>");
                        Response.Write("<ul style='list-style:none !important;text-align:left !important;'>");
                        Response.Write("<li>Note - In both Half Yearly and Annual Examination, marks of best Unit Test will be taken and converterd into 5 marks.</ li>");
                        Response.Write("</ul>");
                        Response.Write("</div>");
                        Response.Write("<div class='p-pad-3 p-tot-tit'>U.T.-Unit Test, * P.B.-Pre-Board, * L.A./P.-Internal Assisment/Practical, * M.M.-Maximum Marks, * M.O.-Mark Obtained, * O.P- Optional</div>");
                        Response.Write("</td></tr>");
                        Response.Write("</table>");
                        Response.Write("</div>");
                        Response.Write("</div></div>");
                    }
                }
            }
        }
    }

    public string grade(double percentle)
    {
        if (percentle < 33)
        {
            return "E";
        }
        else if (percentle >= 33 && percentle < 41)
        {
            return "D";
        }
        else if (percentle >= 41 && percentle < 51)
        {
            return "C2";
        }
        else if (percentle >= 51 && percentle < 61)
        {
            return "C1";
        }
        else if (percentle >= 61 && percentle < 71)
        {
            return "B2";
        }
        else if (percentle >= 71 && percentle < 81)
        {
            return "B1";
        }
        else if (percentle >= 81 && percentle < 91)
        {
            return "A2";
        }
        else if (percentle >= 91 && percentle <= 100)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }
}