using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Cumulative_IXtoXServer : System.Web.UI.Page
{
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
        string TermNmae = Request.Form["TermNmae"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = conn;
                cmd.CommandText = "Cumulative_IXtoX";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@SectionID", SectionID.Trim());
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
                        Response.Write("<tr><td colspan='3' class='text-center'><b>CUMULATIVE-</b>" + session.Trim() + "&nbsp;&nbsp;&nbsp; <b>Evaluation-</b>" + TermNmae.ToUpper().Trim() + "</td></tr>");
                        Response.Write("<tr>");
                        Response.Write("<td colspan='3' class='text-left'>");

                        Response.Write("<table class='table mp-table p-table-bordered table-bordered trBg' style='margin-bottom: 0 !important;'><tbody>");
                        Response.Write("<tr><td style='width: 35.5%;'><b>CLASS TEACHER'S NAME</b>:&nbsp;<span>" + (ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["EmpName"].ToString() : "N/A") + "</span></td><td style='width: 37.2%;'><b>CLASS</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["class_section"].ToString() + "</span><span></span></td><td><b>DATE</b>:&nbsp;<span>" + DateTime.Now.ToString("dd MMM yyyy") + "</span></td></tr>");
                        Response.Write("<tr><td><b>S.R. No. :</b> " + ds.Tables[0].Rows[i]["admissionNo"].ToString() + "</td><td><b>Student's Name :</b> " + ds.Tables[0].Rows[i]["StudentName"].ToString() + "</td><td><b>Date of Birth :</b> " + ds.Tables[0].Rows[i]["DOB"].ToString() + "</td></tr>");
                        Response.Write("<tr><td><b>Mother's Name :</b> MRS. " + ds.Tables[0].Rows[i]["MotherName"].ToString() + "</td><td><b>Father's Name :</b> MR. " + ds.Tables[0].Rows[i]["FatherName"].ToString() + "</td><td><b>Contact No. :</b> " + ds.Tables[0].Rows[i]["MobileNumber"].ToString() + "</td></tr>");
                        Response.Write("<tr><td colspan='3'><b>ADDRESS :</b> " + ds.Tables[0].Rows[i]["StLocalAddress"].ToString() + "</td></tr>");
                        Response.Write("</tbody></table>");

                        Response.Write("</td>");
                        Response.Write("</tr></tbody>");
                        Response.Write("</table>");
                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered'><tr>");
                        

                        Response.Write("<td style='width:70%; vertical-align: top;'>");
                        double Test1Total = 0; double BoardPaternTotal = 0; double Test2Total = 0; double PermidTotal = 0; double PostMidTotal = 0; double MATotal = 0;
                                    double PeriodicTestTotal = 0; double BestOfMATotal = 0; double BestOfPortfolioTotal = 0; double BestOfSETotal = 0; double AETotal = 0;
                        double PortfolioTotal = 0; double SETotal = 0; double TotalTotal = 0; double MidTotal = 0; double MidConcersionTotal = 0;
                        double GrandTotalTotal = 0; double ConversionOfTest1in5Total = 0; double Test2TotalforX = 0; double AETotalforX = 0;



                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-m'><tbody>");
                        
                        
                        if (TermNmae.Trim().ToLower() == "term1")
                        {
                            cmd.CommandText = "Cumulative_IXtoX";
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
                            if (dss.Tables[0].Rows.Count > 0)
                            {
                                for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                                {
                                    double Test1 = 0; double Test2 = 0; double Test3 = 0; double NB = 0; double SE = 0; double SAT = 0; double MA = 0; double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double MaxMarks5 = 0; double MaxMarks6 = 0; double MaxMarks7 = 0;
                                    if (dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test1 = double.Parse(dss.Tables[0].Rows[p]["Test1"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test2 = double.Parse(dss.Tables[0].Rows[p]["Test2"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test3 = double.Parse(dss.Tables[0].Rows[p]["Test3"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { NB = double.Parse(dss.Tables[0].Rows[p]["NB"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { SE = double.Parse(dss.Tables[0].Rows[p]["SE"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { SAT = double.Parse(dss.Tables[0].Rows[p]["SAT"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { MA = double.Parse(dss.Tables[0].Rows[p]["MA"].ToString().Trim()); }


                                    if (dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks1 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks3 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks4 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks5 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks6 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks7 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim()); }

                                    

                                    double best1PreMid = Test1 > ((Test2 * 20) / MaxMarks2) ? (Test1 > Test3 ? Test1 : (((Test2 * 20) / MaxMarks2) > Test3 ? ((Test2 * 20) / MaxMarks2) : Test3)) : (((Test2 * 20) / MaxMarks2) > Test3 ? ((Test2 * 20) / MaxMarks2) : Test3);
                                    double best1PreMidMax = Test1 > Test2 ? (Test1 > Test3 ? MaxMarks1 : (Test2 > Test3 ? 20 : MaxMarks3)) : (Test2 > Test3 ? 20 : MaxMarks3);
                                    double convbest1PreMidin5 = (best1PreMid * 5) / best1PreMidMax;
                                    //Totals
                                    Test1Total = Test1Total + Test1;
                                    BoardPaternTotal = BoardPaternTotal + ((Test2 * 20) / MaxMarks2);
                                    Test2Total = Test2Total + Test3;
                                    PermidTotal = PermidTotal + convbest1PreMidin5;
                                    MATotal = MATotal + MA;
                                    PortfolioTotal = PortfolioTotal + NB;
                                    SETotal = SETotal + SE;
                                    TotalTotal = TotalTotal + (convbest1PreMidin5 + MA + NB + SE);
                                    MidTotal = MidTotal + SAT;
                                    MidConcersionTotal = MidConcersionTotal + ((SAT * 5) / MaxMarks6);
                                    GrandTotalTotal = GrandTotalTotal + ((convbest1PreMidin5 + MA + NB + SE) + SAT);


                                    if (p == 0)
                                    {

                                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th></th><th>May</th><th>July</th><th>August</th><th>Pre-Mid</th><th>Multiple Assessment</th><th>Portfolio</th><th>Subject Enrichment</th><th>Total</th><th>Mid</th><th>Mid Total</th><th>Grand Total</th></tr>");
                                        Response.Write("<tr class='trBg'><th>EXAM<i class='fa fa-arrow-right'></i></th><th>UT1</th><th>Board Pattern</th><th>UT2</th><th>Best Of UT1, UT2 or Board</th><th>MA</th><th>Portfolio</th><th>SE</th><th>Pre-Mid + MA + Portfolio + SE</th><th>Half Yearly</th><th>Conversion of HY in 5</th><th>20+80</th></tr>");
                                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><th style='white-space: nowrap !important;'>20 Marks</th><th>80 Marks (Conversion in 20)</th><th style='white-space: nowrap !important;'>20 Marks</th><th>Conversion in 5</th><th style='white-space: nowrap !important;'>5 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5 Marks</th><th>5+5+5+5=20</th><th style='white-space: nowrap !important;'>80 Marks</th><th>5 Marks</th><th style='white-space: nowrap !important;'>100 Marks</th></tr>");
                                    }
                                    Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; white-space: nowrap !important'>" + dss.Tables[0].Rows[p]["SubjectName"].ToString() + "</td><td>" + (Test1.ToString("0")=="0"? dss.Tables[0].Rows[p]["Test1"].ToString(): Test1.ToString("0.00")) + "</td><td>" + ((Test2 * 20) / MaxMarks2).ToString("0.00") + "</td><td>" + (Test3.ToString("0") == "0" ? dss.Tables[0].Rows[p]["Test3"].ToString() : Test3.ToString("0.00")) + "</td><td>" + convbest1PreMidin5.ToString("0.00") + "</td><td>" + (MA.ToString("0") == "0" ? dss.Tables[0].Rows[p]["MA"].ToString() : MA.ToString("0.00")) + "</td><td>" +(NB.ToString("0") == "0" ? dss.Tables[0].Rows[p]["NB"].ToString() : NB.ToString("0.00")) + "</td><td>" + (SE.ToString("0") == "0" ? dss.Tables[0].Rows[p]["SE"].ToString() : SE.ToString("0.00")) + "</td><td>" + (convbest1PreMidin5 + MA + NB + SE).ToString("0.00") + "</td><td>" + (SAT.ToString("0") == "0" ? dss.Tables[0].Rows[p]["SAT"].ToString() : SAT.ToString("0.00")) + "</td><td>" + ((SAT * 5) / MaxMarks6).ToString("0.00") + "</td><td>" + ((convbest1PreMidin5 + MA + NB + SE) + SAT).ToString("0.00") + "</td></tr>");
                                }

                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; white-space: nowrap !important'>Total 100</td><td>" + Test1Total.ToString("0.00") + "</td><td>" + BoardPaternTotal.ToString("0.00") + "</td><td>" + Test2Total.ToString("0.00") + "</td><td>" + PermidTotal.ToString("0.00") + "</td><td>" + MATotal.ToString("0.00") + "</td><td>" + PortfolioTotal.ToString("0.00") + "</td><td>" + SETotal.ToString("0.00") + "</td><td>" + TotalTotal.ToString("0.00") + "</td><td>" + MidTotal.ToString("0.00") + "</td><td>" + MidConcersionTotal.ToString("0.00") + "</td><td>" + GrandTotalTotal.ToString("0.00") + "</td></tr>");
                            }
                            else
                            {
                                Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th></th><th>May</th><th>July</th><th>August</th><th>Pre-Mid</th><th>Multiple Assessment</th><th>Portfolio</th><th>Subject Enrichment</th><th>Total</th><th>Mid</th><th>Mid Total</th><th>Grand Total</th></tr>");
                                Response.Write("<tr class='trBg'><th>EXAM<i class='fa fa-arrow-right'></i></th><th>UT1</th><th>Board Pattern</th><th>UT2</th><th>Best Of UT1, UT2 or Board</th><th>MA</th><th>Portfolio</th><th>SE</th><th>Pre-Mid + MA + Portfolio + SE</th><th>Half Yearly</th><th>Conversion of HY in 5</th><th>20+80</th></tr>");
                                Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><th style='white-space: nowrap !important;'>20 Marks</th><th>80 Marks (Conversion in 20)</th><th style='white-space: nowrap !important;'>20 Marks</th><th>Conversion in 5</th><th style='white-space: nowrap !important;'>5 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5 Marks</th><th>5+5+5+5=20</th><th style='white-space: nowrap !important;'>80 Marks</th><th>5 Marks</th><th style='white-space: nowrap !important;'>100 Marks</th></tr>");
                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:center; color:red; white-space: nowrap !important' colspan='12'>No record(s) found</td></tr>");
                            }
                        }

                        if (TermNmae.Trim().ToLower() == "term2")
                        {
                            cmd.CommandText = "Cumulative_IXtoX";
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
                            if (dss.Tables[0].Rows.Count > 0)
                            {
                                for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                                {

                                    cmd.CommandText = "Cumulative_IXtoX";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                                    cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                                    cmd.Parameters.AddWithValue("@ClassId", ClassId);
                                    cmd.Parameters.AddWithValue("@SrNo", ds.Tables[0].Rows[i]["admissionNo"].ToString());
                                    cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                                    cmd.Parameters.AddWithValue("@SubjectId", dss.Tables[0].Rows[p]["SubjectId"].ToString().Trim());
                                    cmd.Parameters.AddWithValue("@action", "details");
                                    SqlDataAdapter das1 = new SqlDataAdapter(cmd);
                                    DataSet dss1 = new DataSet();
                                    das1.Fill(dss1);
                                    cmd.Parameters.Clear();
                                    double Test1_1 = 0; double Test2_1 = 0; double Test3_1 = 0; double NB_1 = 0; double SE_1 = 0; double SAT_1 = 0; double MA_1 = 0; double MaxMarks1_1 = 0; double MaxMarks2_1 = 0; double MaxMarks3_1 = 0; double MaxMarks4_1 = 0; double MaxMarks5_1 = 0; double MaxMarks6_1 = 0; double MaxMarks7_1 = 0;
                                    double best1PreMid_1 = 0;
                                    double best1PreMidMax_1 = 0;
                                    double convbest1PreMidin5_1 = 0;
                                    if (dss1.Tables[0].Rows.Count > 0)
                                    {
                                        for (int p1 = 0; p1 < 1; p1++)
                                        {
                                            if (dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test1_1 = double.Parse(dss1.Tables[0].Rows[p1]["Test1"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test2_1 = double.Parse(dss1.Tables[0].Rows[p1]["Test2"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { Test3_1 = double.Parse(dss1.Tables[0].Rows[p1]["Test3"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["NB"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { NB_1 = double.Parse(dss1.Tables[0].Rows[p1]["NB"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["SE"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { SE_1 = double.Parse(dss1.Tables[0].Rows[p1]["SE"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { SAT_1 = double.Parse(dss1.Tables[0].Rows[p1]["SAT"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "" || dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "AB" || dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "NA" || dss1.Tables[0].Rows[p1]["MA"].ToString().Trim().ToUpper() == "M.L") { }
                                            else { MA_1 = double.Parse(dss1.Tables[0].Rows[p1]["MA"].ToString().Trim()); }


                                            if (dss1.Tables[0].Rows[p1]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks1_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks1"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks2_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks2"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks3_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks3"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks4_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks4"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MaxMarks5"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks5"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks5"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks5"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks5_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks5"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MaxMarks6"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks6"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks6"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks6"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks6_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks6"].ToString().Trim()); }
                                            if (dss1.Tables[0].Rows[p1]["MaxMarks7"].ToString().Trim().ToUpper() == "NP" || dss1.Tables[0].Rows[p1]["MaxMarks7"].ToString().Trim().ToUpper() == "NAD" || dss1.Tables[0].Rows[p1]["MaxMarks7"].ToString().Trim().ToUpper() == "ML" || dss1.Tables[0].Rows[p1]["MaxMarks7"].ToString().Trim().ToUpper() == "") { }
                                            else { MaxMarks7_1 = double.Parse(dss1.Tables[0].Rows[p1]["MaxMarks7"].ToString().Trim()); }


                                            best1PreMid_1 = Test1_1 > ((Test2_1 * 20) / MaxMarks2_1) ? (Test1_1 > Test3_1 ? Test1_1 : (((Test2_1 * 20) / MaxMarks2_1) > Test3_1 ? ((Test2_1 * 20) / MaxMarks2_1) : Test3_1)) : (((Test2_1 * 20) / MaxMarks2_1) > Test3_1 ? ((Test2_1 * 20) / MaxMarks2_1) : Test3_1);
                                            best1PreMidMax_1 = Test1_1 > Test2_1 ? (Test1_1 > Test3_1 ? MaxMarks1_1 : (Test2_1 > Test3_1 ? 20 : MaxMarks3_1)) : (Test2_1 > Test3_1 ? 20 : MaxMarks2_1);
                                            convbest1PreMidin5_1 = (best1PreMid_1 * 5) / best1PreMidMax_1; //Pre-Mid
                                        }
                                    }




                                    double Test1 = 0; double Test2 = 0; double Test3 = 0; double NB = 0; double SE = 0; double SAT = 0; double MA = 0; double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double MaxMarks5 = 0; double MaxMarks6 = 0; double MaxMarks7 = 0;
                                    if (dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test1 = double.Parse(dss.Tables[0].Rows[p]["Test1"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test2 = double.Parse(dss.Tables[0].Rows[p]["Test2"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { Test3 = double.Parse(dss.Tables[0].Rows[p]["Test3"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { NB = double.Parse(dss.Tables[0].Rows[p]["NB"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { SE = double.Parse(dss.Tables[0].Rows[p]["SE"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { SAT = double.Parse(dss.Tables[0].Rows[p]["SAT"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "M.L") { }
                                    else { MA = double.Parse(dss.Tables[0].Rows[p]["MA"].ToString().Trim()); }


                                    if (dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks1 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks3 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks4 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks5 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks6 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim()); }
                                    if (dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim().ToUpper() == "") { }
                                    else { MaxMarks7 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks7"].ToString().Trim()); }

                                    double best1PreMid = Test1 > Test2 ? Test1 : Test2;
                                    double best1PreMidMax = Test1 > Test2 ? MaxMarks1 : MaxMarks2;


                                    double PreMid = convbest1PreMidin5_1;
                                    double Mid = ((SAT_1 * 5) / MaxMarks6_1);
                                    double PostMid = (best1PreMid * 5) / best1PreMidMax;

                                    double sum1 = PreMid + Mid;
                                    double sum2 = Mid + PostMid;
                                    double sum3 = PostMid + PreMid;

                                    double PeriodicTest = sum1 > sum2 ? (sum1 > sum3 ? sum1 : (sum2 > sum3 ? sum2 : sum3)) : (sum2 > sum3 ? sum2 : sum3);
                                    PeriodicTest = (PeriodicTest) / 2;

                                    double BestOfMA = MA_1 > MA ? MA_1 : MA;
                                    double BestOfPortfolio = NB_1 > NB ? NB_1 : NB;
                                    double BestOfSE = SE_1 > SE ? SE_1 : SE;
                                    double Total = (PeriodicTest + BestOfMA + BestOfPortfolio + BestOfSE);
                                    double GTotal = Total + SAT;

                                    //Totals 
                                    Test1Total = Test1Total + Test1;
                                    Test2Total = Test2Total + Test3;
                                    PostMidTotal = PostMidTotal + PostMid;
                                    PeriodicTestTotal = PeriodicTestTotal + PeriodicTest;
                                    MATotal = MATotal + MA;
                                    PortfolioTotal = PortfolioTotal + NB;
                                    SETotal = SETotal + SE;
                                    BestOfMATotal = BestOfMATotal + BestOfMA;
                                    BestOfPortfolioTotal = BestOfPortfolioTotal + BestOfPortfolio;
                                    BestOfSETotal = BestOfSETotal + BestOfSE;
                                    TotalTotal = TotalTotal + Total;
                                    AETotal = AETotal + SAT;
                                    GrandTotalTotal = GrandTotalTotal + (SAT + Total);

                                    //For X
                                    
                                    double ConversionOfTest1in5 = ((Test1 * 5) / MaxMarks1);
                                    ConversionOfTest1in5Total = ConversionOfTest1in5Total + ConversionOfTest1in5;
                                    Test2TotalforX = Test2TotalforX + Test2;
                                    AETotalforX = AETotalforX + SAT;
                                    if (p == 0)
                                    {

                                        if (ClassName.ToUpper() == "IX")
                                        {
                                            Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th></th><th>November</th><th>January</th><th>Post-Mid</th><th>Periodic Test</th><th>Multiple Assessment</th><th>Portfolio</th><th>Subject Enrichment</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Total</th><th>Anual Exam.</th><th>Grand Total</th></tr>");
                                            Response.Write("<tr class='trBg'><th>EXAM<i class='fa fa-arrow-right'></i></th><th>UT3</th><th>UT4</th><th>Conversion in 5(Best of UT3 or UT4)</th><th>Avrage of Best 2(Pre-Mid, Mid & Post-Mid)</th><th>MA</th><th>Portfolio</th><th>SE</th><th>MA</th><th>Portfolio</th><th>SE</th><th>Periodic Test + MA + Portfolio + SE</th><th>AE</th><th>20+80</th></tr>");
                                            Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><th style='white-space: nowrap !important;'>20 Marks</th><th style='white-space: nowrap !important;'>20 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5+5+5+5=20</th><th style='white-space: nowrap !important;'>80 Marks</th><th style='white-space: nowrap !important;'>100 Marks</th></tr>");
                                        }
                                        else
                                        {
                                            Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th></th><th>November</th><th>Post-Mid</th><th>December</th><th>January</th><th>Periodic Test</th><th>Multiple Assessment</th><th>Portfolio</th><th>Subject Enrichment</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Total</th><th>Annual Exam</th><th>Grand Total</th></tr>");
                                            Response.Write("<tr class='trBg'><th>EXAM<i class='fa fa-arrow-right'></i></th><th>UT3</th><th>Conversion in 5 of UT3</th><th>Preboard I</th><th>Preboard II</th><th>Avrage of Best 2(Pre-Mid, Mid & Post-Mid)</th><th>MA</th><th>Portfolio</th><th>SE</th><th>MA</th><th>Portfolio</th><th>SE</th><th>Periodic Test + MA + Portfolio + SE</th><th>AE</th><th>20+80</th></tr>");
                                            Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><th>20 Marks</th><th>5 Marks </th><th>80 Marks</th><th>80 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5+5+5+5=20</th><th>80 Marks</th><th>100 Marks</th></tr>");
                                        }
                                    }
                                    if (ClassName.ToUpper() == "IX")
                                    {
                                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left;'>" + dss.Tables[0].Rows[p]["SubjectName"].ToString() + "</td><td>" + (Test1.ToString("0") == "0" ? dss.Tables[0].Rows[p]["Test1"].ToString() : Test1.ToString("0.00")) + "</td><td>" + (Test2.ToString("0") == "0" ? dss.Tables[0].Rows[p]["Test2"].ToString() : Test2.ToString("0.00")) + "</td><td>" + PostMid.ToString("0.000") + "</td><td>" + PeriodicTest.ToString("0.000") + "</td><td>" + (MA.ToString("0") == "0" ? dss.Tables[0].Rows[p]["MA"].ToString() : MA.ToString("0.00")) + "</td><td>" + (NB.ToString("0") == "0" ? dss.Tables[0].Rows[p]["NB"].ToString() : NB.ToString("0.00")) + "</td><td>" + (SE.ToString("0") == "0" ? dss.Tables[0].Rows[p]["SE"].ToString() : SE.ToString("0.00")) + "</td><td>" + BestOfMA.ToString("0.000") + "</td><td>" + BestOfPortfolio.ToString("0.000") + "</td><td>" + BestOfSE.ToString("0.000") + "</td><td>" + Total.ToString("0.000") + "</td><td>" + SAT.ToString("0.00") + "</td><td>" + GTotal.ToString("0.000") + "</td></tr>");
                                    }
                                    else
                                    {
                                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left;'>" + dss.Tables[0].Rows[p]["SubjectName"].ToString() + "</td><td>" + (Test1.ToString("0") == "0" ? dss.Tables[0].Rows[p]["Test1"].ToString() : Test1.ToString("0.00")) + "</td><td>" + ConversionOfTest1in5.ToString("0.000") + "</td><td>" + (Test2.ToString("0") == "0" ? dss.Tables[0].Rows[p]["Test2"].ToString() : Test2.ToString("0.00")) + "</td><td></td><td>" + PeriodicTest.ToString("0.000") + "</td><td>" + (MA.ToString("0") == "0" ? dss.Tables[0].Rows[p]["MA"].ToString() : MA.ToString("0.00")) + "</td><td>" + (NB.ToString("0") == "0" ? dss.Tables[0].Rows[p]["NB"].ToString() : NB.ToString("0.00")) + "</td><td>" + (SE.ToString("0") == "0" ? dss.Tables[0].Rows[p]["SE"].ToString() : SE.ToString("0.00")) + "</td><td>" + BestOfMA.ToString("0.000") + "</td><td>" + BestOfPortfolio.ToString("0.000") + "</td><td>" + BestOfSE.ToString("0.000") + "</td><td>" + Total.ToString("0.000") + "</td><td>" + SAT.ToString("0.00") + "</td><td>" + GTotal.ToString("0.000") + "</td></tr>");
                                    }
                                }
                                if (ClassName.ToUpper() == "IX")
                                {
                                    Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; white-space: nowrap !important'>Total 100</td><td>" + Test1Total.ToString("0.00") + "</td><td>" + Test2Total.ToString("0.00") + "</td><td>" + PostMidTotal.ToString("0.000") + "</td><td>" + PeriodicTestTotal.ToString("0.000") + "</td><td>" + MATotal.ToString("0.00") + "</td><td>" + PortfolioTotal.ToString("0.00") + "</td><td>" + SETotal.ToString("0.00") + "</td><td>" + BestOfMATotal.ToString("0.000") + "</td><td>" + BestOfPortfolioTotal.ToString("0.000") + "</td><td>" + BestOfSETotal.ToString("0.000") + "</td><td>" + TotalTotal.ToString("0.000") + "</td><td>" + AETotal.ToString("0.00") + "</td><td>" + GrandTotalTotal.ToString("0.000") + "</td></tr>");
                                }
                                else
                                {
                                    Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; white-space: nowrap !important'>Total 100</td><td>" + Test1Total.ToString("0.00") + "</td><td>" + ConversionOfTest1in5Total.ToString("0.000") + "</td><td>" + Test2TotalforX.ToString("0.00") + "</td><td></td><td>" + PeriodicTestTotal.ToString("0.000") + "</td><td>" + MATotal.ToString("0.00") + "</td><td>" + PortfolioTotal.ToString("0.00") + "</td><td>" + SETotal.ToString("0.00") + "</td><td>" + BestOfMATotal.ToString("0.000") + "</td><td>" + BestOfPortfolioTotal.ToString("0.000") + "</td><td>" + BestOfSETotal.ToString("0.000") + "</td><td>" + TotalTotal.ToString("0.000") + "</td><td>" + AETotalforX.ToString("0.00") + "</td><td>" + GrandTotalTotal.ToString("0.000") + "</td></tr>");
                                }
                            }
                            else
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th></th><th>November</th><th>January</th><th>Post-Mid</th><th>Periodic Test</th><th>Multiple Assessment</th><th>Portfolio</th><th>Subject Enrichment</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Total</th><th>Anual Exam.</th><th>Grand Total</th></tr>");
                                    Response.Write("<tr class='trBg'><th>EXAM<i class='fa fa-arrow-right'></i></th><th>UT3</th><th>UT4</th><th>Conversion in 5(Best of UT3 or UT4)</th><th>Avrage of Best 2(Pre-Mid, Mid & Post-Mid)</th><th>MA</th><th>Portfolio</th><th>SE</th><th>MA</th><th>Portfolio</th><th>SE</th><th>Periodic Test + MA + Portfolio + SE</th><th>AE</th><th>20+80</th></tr>");
                                    Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><th style='white-space: nowrap !important;'>20 Marks</th><th style='white-space: nowrap !important;'>20 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5 Marks</th><th style='white-space: nowrap !important;'>5 Marks</th><th>5+5+5+5=20</th><th style='white-space: nowrap !important;'>80 Marks</th><th style='white-space: nowrap !important;'>100 Marks</th></tr>");
                                    Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:center; color:red; white-space: nowrap !important' colspan='14'>No record(s) found</td></tr>");
                                }
                                else
                                {
                                    Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th></th><th>November</th><th>Post-Mid</th><th>December</th><th>January</th><th>Periodic Test</th><th>Multiple Assessment</th><th>Portfolio</th><th>Subject Enrichment</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Best of Term1 & Term2</th><th>Total</th><th>Annual Exam</th><th>Grand Total</th></tr>");
                                    Response.Write("<tr class='trBg'><th>EXAM<i class='fa fa-arrow-right'></i></th><th>UT3</th><th>Conversion in 5 of UT3</th><th>Preboard I</th><th>Preboard II</th><th>Avrage of Best 2(Pre-Mid, Mid & Post-Mid)</th><th>MA</th><th>Portfolio</th><th>SE</th><th>MA</th><th>Portfolio</th><th>SE</th><th>Periodic Test + MA + Portfolio + SE</th><th>AE</th><th>20+80</th></tr>");
                                    Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><th>20 Marks</th><th>5 Marks </th><th>80 Marks</th><th>80 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5 Marks</th><th>5+5+5+5=20</th><th>80 Marks</th><th>100 Marks</th></tr>");
                                    Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:center; color:red; white-space: nowrap !important' colspan='15'>No record(s) found</td></tr>");
                                }
                            }
                        }


                        Response.Write("</table>");
                        Response.Write("</td>");
                        Response.Write("</tr></table>");
                        Response.Write("</div >");
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