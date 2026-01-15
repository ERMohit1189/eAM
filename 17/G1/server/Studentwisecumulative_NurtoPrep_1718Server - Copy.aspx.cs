using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G2_Studentwisecumulative_NurtoPrep_1718Server : System.Web.UI.Page
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

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "StudentWiseCumulative_NurtoPrep_2021";
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
                    string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                    ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                    ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                    ss +=  " where cm.id=" + ClassId + " and bm.id=" + ds.Tables[0].Rows[0]["BranchId"].ToString() + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='" + session + "'";
                    string DisableTest1 = (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "");
                    string DisableTest2 = (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "");
                    string DisableTest3 = (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "");
                    string DisableTest4 = (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : "");
                    string DisableTest5 = (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : "");
                    string DisableTest6 = (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : "");
                    string monthlyTest1 = "", monthlyTest2 = "";
                    if (DisableTest1 == "hide" && DisableTest2 == "hide" && DisableTest3 == "hide")
                    {
                        monthlyTest1 = "hide";
                    }
                    if (DisableTest4 == "hide" && DisableTest5 == "hide" && DisableTest6 == "hide")
                    {
                        monthlyTest2 = "hide";
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Response.Write("<div class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                        Response.Write("<div id='break_div' class='term1' style='page-break-after: always;'>");
                        Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                        Response.Write("<div class='table-responsive table-responsive2'>");
                        Response.Write("<table class='table mp-table p-table-bordered table-bordered trBg' style='margin-bottom: 0 !important;'><tbody>");
                        Response.Write("<tr><td colspan='4' class='text-center'><b>STUDENT WISE CUMULATIVE-" + session.Trim() + "</b></td></tr><tr><td style='width: 21.6%;'><b>CLASS TEACHER NAME</b>:&nbsp;<span>" + (ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["EmpName"].ToString() : "N/A") + "</span></td><td style='width: 20%;'><b>CLASS</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["class_section"].ToString() + "</span><span></span></td><td style='width: 20%;'><b>DATE</b>:&nbsp;<span>" + DateTime.Now.ToString("dd MMM yyyy") + "</span></td></tr></tbody>");
                        Response.Write("</table>");
                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered'><tr>");

                        Response.Write("<td style='width:30%; vertical-align: top;'>");

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='trBg'><td colspan='2' class='text-center'><b>Student Details</b></td></tr>");
                        Response.Write("<tr><td style='width:30%;'>S.R. No.</td><td>" + ds.Tables[0].Rows[i]["admissionNo"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Student's Name</td><td>" + ds.Tables[0].Rows[i]["StudentName"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Student D.O.B</td><td>" + ds.Tables[0].Rows[i]["DOB"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Mother's Name</td><td>MRS. " + ds.Tables[0].Rows[i]["MotherName"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Father's Name</td><td>MR. " + ds.Tables[0].Rows[i]["FatherName"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Contact No.</td><td>" + ds.Tables[0].Rows[i]["MobileNumber"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>RES.ADD.</td><td>" + ds.Tables[0].Rows[i]["StLocalAddress"].ToString() + "</td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("<br/><table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='trBg'><td colspan='2' class='text-center'><b>Grade Scale</b></td></tr>");
                        Response.Write("<tr><td style='width:50%;'><b>Marks Range</b></td><td><b>Grade</b></td></tr>");
                        Response.Write("<tr><td style='width:50%;'>91-100</td><td>A1</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>81-90</td><td>A2</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>71-80</td><td>B1</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>61-70</td><td>B2</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>51-60</td><td>C1</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>41-50</td><td>C2</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>33-40</td><td>D</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>32- & below</td><td>E</td></tr>");
                        Response.Write("<tr><td colspan='2'><b>Co-Scholastic Activities : 3-point grading scale</b></td></tr>");
                        Response.Write("<tr><td colspan='2'>A=Outstanding,B=Very Good & C= Fair</td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("</td>");

                        Response.Write("<td style='width:70%; vertical-align: top;'>");
                        cmd.CommandText = "StudentWiseCumulative_NurtoPrep_2021";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@SrNo", ds.Tables[0].Rows[i]["admissionNo"].ToString());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@AttendanceTyp", AttendanceTyp.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dss = new DataSet();
                        das.Fill(dss);
                        cmd.Parameters.Clear();

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th class='"+ monthlyTest1 + "'>Periodic Test 10</th><th>Note Book 5</th><th>Subject Enrichment</th><th>Half Yearly Exam (80)</th><th>Total 100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='"+( monthlyTest1=="hide"?"4":"6")+"' class='text-center'><B>TERM1</B></td></tr>");
                        double Gpercentle1 = 0; double GNB1 = 0; double GSE1 = 0; double GSAT1 = 0; double Gtotal1 = 0;
                        string percentleGrade1 = ""; string NBGrade1 = ""; string SEGrade1 = ""; string SATGrade1 = ""; string totalGrade1 = "";
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                double Test1 = 0; double Test2 = 0; double Test3 = 0; double NB = 0; double SE = 0; double SAT = 0; double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double MaxMarks5 = 0; double MaxMarks6 = 0;
                                if (dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test1 =(DisableTest1=="hide"?0: double.Parse(dss.Tables[0].Rows[p]["Test1"].ToString().Trim())); }
                                if (dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test2 = (DisableTest2 == "hide" ? 0 : double.Parse(dss.Tables[0].Rows[p]["Test2"].ToString().Trim())); }
                                if (dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test3 = (DisableTest3 == "hide" ? 0 : double.Parse(dss.Tables[0].Rows[p]["Test3"].ToString().Trim())); }
                                if (dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["NB"].ToString().Trim().ToUpper() == "M.L") { }
                                else { NB = double.Parse(dss.Tables[0].Rows[p]["NB"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "M.L") { }
                                else { SE = double.Parse(dss.Tables[0].Rows[p]["SE"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                else { SAT = double.Parse(dss.Tables[0].Rows[p]["SAT"].ToString().Trim()); }

                                if (dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks1 = (DisableTest1 == "hide" ? 0 : double.Parse(dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim())); }
                                if (dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks2 = (DisableTest2 == "hide" ? 0 : double.Parse(dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim())); }
                                if (dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks3 = (DisableTest3 == "hide" ? 0 : double.Parse(dss.Tables[0].Rows[p]["MaxMarks3"].ToString().Trim())); }
                                if (dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks4 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks4"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks5 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks5"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks6 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks6"].ToString().Trim()); }

                                double percentle1 = 0; string ptm1 = ""; bool isaddmmconinten1 = false; double ObtMarks1 = 0; string grade1 = "";

                                double sum1 = Test1 + Test2;
                                double maxsum1 = MaxMarks1 + MaxMarks2;
                                double sum2 = Test1 + Test3;
                                double maxsum2 = MaxMarks2 + MaxMarks3;
                                double sum3 = Test2 + Test3;
                                double maxsum3 = MaxMarks2 + MaxMarks3;
                                double totalmarks1 = sum1 > sum2 ? (sum1 > sum3 ? sum1 : (sum2 > sum3 ? sum2 : sum3)) : (sum2 > sum3 ? sum2 : sum3);
                                double totalmmmarks1 = sum1 > sum2 ? (sum1 > sum3 ? maxsum1 : (sum2 > sum3 ? maxsum2 : maxsum3)) : (sum2 > sum3 ? maxsum2 : maxsum3);

                                if (totalmarks1 == 0)
                                {
                                    isaddmmconinten1 = true;
                                }
                                if (totalmmmarks1 == 0)
                                {
                                    ptm1 = "0";
                                    isaddmmconinten1 = true;
                                }
                                else
                                {
                                    percentle1 = ((totalmarks1) * 10) / totalmmmarks1;
                                    ObtMarks1 = (percentle1 + NB + SE + SAT);

                                    Gpercentle1 = Gpercentle1 + double.Parse(percentle1.ToString("0"));
                                    GNB1 = GNB1 + NB;
                                    GSE1 = GSE1 + SE;
                                    GSAT1 = GSAT1 + SAT;
                                    Gtotal1 = Gtotal1 + ObtMarks1;
                                }

                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["SubjectName"].ToString() + "</td><td class='" + monthlyTest1 + "'>" + percentle1.ToString("0") + "</td><td>" + NB.ToString("0") + "</td><td>" + SE.ToString("0") + "</td><td>" + SAT.ToString("0") + "</td><td>" + ObtMarks1.ToString("0") + "</td></tr>");
                            }
                        }
                        percentleGrade1 = grade(Gpercentle1 / dss.Tables[0].Rows.Count);
                        NBGrade1 = grade(GNB1 / dss.Tables[0].Rows.Count);
                        SEGrade1 = grade(GSE1 / dss.Tables[0].Rows.Count);
                        SATGrade1 = grade(GSAT1 / dss.Tables[0].Rows.Count);
                        totalGrade1 = grade(Gtotal1 / dss.Tables[0].Rows.Count);

                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total 100</td><td class='" + monthlyTest1 + "'>" + Gpercentle1.ToString("0") + "</td><td>" + GNB1.ToString("0") + "</td><td>" + GSE1.ToString("0") + "</td><td>" + GSAT1.ToString("0") + "</td><td>" + Gtotal1.ToString("0") + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Grade</td><td class='" + monthlyTest1 + "'></td><td></td><td></td><td></td><td>" + totalGrade1 + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Working Days</td><td>" + (dss.Tables[2].Rows.Count > 0 ? dss.Tables[2].Rows[0]["totaldays"].ToString() : "0") + "</td><td colspan='" + (monthlyTest1 == "hide" ? "3" : "4") + "'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Attendance</td><td>" + (dss.Tables[2].Rows.Count > 0 ? dss.Tables[2].Rows[0]["Attendence"].ToString() : "0") + "</td><td colspan='" + (monthlyTest1 == "hide" ? "3" : "4") + "'></td></tr>");

                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th class='" + monthlyTest2 + "'>Periodic Test 10</th><th>Note Book 5</th><th>Subject Enrichment</th><th>Annual Exam (80)</th><th>Total 100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='" + (monthlyTest2 == "hide" ? "4" : "6") + "' class='text-center'><B>TERM2</B></td></tr>");

                        double Gpercentle2 = 0; double GNB2 = 0; double GSE2 = 0; double GSAT2 = 0; double Gtotal2 = 0;
                        string percentleGrade2 = ""; string NBGrade2 = ""; string SEGrade2 = ""; string SATGrade2 = ""; string totalGrade2 = "";
                        if (dss.Tables[1].Rows.Count > 0)
                        {
                            for (int q = 0; q < dss.Tables[1].Rows.Count; q++)
                            {
                                double Test1_2 = 0; double Test2_2 = 0; double Test3_2 = 0; double NB_2 = 0; double SE_2 = 0; double SAT_2 = 0; double MaxMarks1_2 = 0; double MaxMarks2_2 = 0; double MaxMarks3_2 = 0; double MaxMarks4_2 = 0; double MaxMarks5_2 = 0; double MaxMarks6_2 = 0;
                                if (dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[q]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test1_2 = (DisableTest4 == "hide" ? 0 : double.Parse(dss.Tables[1].Rows[q]["Test1"].ToString().Trim())); }
                                if (dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[q]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test2_2 = (DisableTest5 == "hide" ? 0 : double.Parse(dss.Tables[1].Rows[q]["Test2"].ToString().Trim())); }
                                if (dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[q]["Test3"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test3_2 = (DisableTest6 == "hide" ? 0 : double.Parse(dss.Tables[1].Rows[q]["Test3"].ToString().Trim())); }
                                if (dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[q]["NB"].ToString().Trim().ToUpper() == "M.L") { }
                                else { NB_2 = double.Parse(dss.Tables[1].Rows[q]["NB"].ToString().Trim()); }
                                if (dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[q]["SE"].ToString().Trim().ToUpper() == "M.L") { }
                                else { SE_2 = double.Parse(dss.Tables[1].Rows[q]["SE"].ToString().Trim()); }
                                if (dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[q]["SAT"].ToString().Trim().ToUpper() == "M.L") { }
                                else { SAT_2 = double.Parse(dss.Tables[1].Rows[q]["SAT"].ToString().Trim()); }

                                if (dss.Tables[1].Rows[q]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks1_2 = (DisableTest4 == "hide" ? 0 : double.Parse(dss.Tables[1].Rows[q]["MaxMarks1"].ToString().Trim())); }
                                if (dss.Tables[1].Rows[q]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks2_2 = (DisableTest5 == "hide" ? 0 : double.Parse(dss.Tables[1].Rows[q]["MaxMarks2"].ToString().Trim())); }
                                if (dss.Tables[1].Rows[q]["MaxMarks3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["MaxMarks3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["MaxMarks3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["MaxMarks3"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks3_2 = (DisableTest6 == "hide" ? 0 : double.Parse(dss.Tables[1].Rows[q]["MaxMarks3"].ToString().Trim())); }
                                if (dss.Tables[1].Rows[q]["MaxMarks4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["MaxMarks4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["MaxMarks4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["MaxMarks4"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks4_2 = double.Parse(dss.Tables[1].Rows[q]["MaxMarks4"].ToString().Trim()); }
                                if (dss.Tables[1].Rows[q]["MaxMarks5"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["MaxMarks5"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["MaxMarks5"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["MaxMarks5"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks5_2 = double.Parse(dss.Tables[1].Rows[q]["MaxMarks5"].ToString().Trim()); }
                                if (dss.Tables[1].Rows[q]["MaxMarks6"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[q]["MaxMarks6"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[q]["MaxMarks6"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[q]["MaxMarks6"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks6_2 = double.Parse(dss.Tables[1].Rows[q]["MaxMarks6"].ToString().Trim()); }

                                double percentle_2 = 0; string ptm_2 = ""; bool isaddmmconinten_2 = false; double ObtMarks_2 = 0; string grade_2 = "";

                                double sum1_2 = Test1_2 + Test2_2;
                                double maxsum1_2 = MaxMarks1_2 + MaxMarks2_2;
                                double sum2_2 = Test1_2 + Test3_2;
                                double maxsum2_2 = MaxMarks2_2 + MaxMarks3_2;
                                double sum3_2 = Test2_2 + Test3_2;
                                double maxsum3_2 = MaxMarks2_2 + MaxMarks3_2;
                                double totalmarks_2 = sum1_2 > sum2_2 ? (sum1_2 > sum3_2 ? sum1_2 : (sum2_2 > sum3_2 ? sum2_2 : sum3_2)) : (sum2_2 > sum3_2 ? sum2_2 : sum3_2);
                                double totalmmmarks_2 = sum1_2 > sum2_2 ? (sum1_2 > sum3_2 ? maxsum1_2 : (sum2_2 > sum3_2 ? maxsum2_2 : maxsum3_2)) : (sum2_2 > sum3_2 ? maxsum2_2 : maxsum3_2);

                                if (totalmarks_2 == 0)
                                {
                                    isaddmmconinten_2 = true;
                                }
                                if (totalmmmarks_2 == 0)
                                {
                                    ptm_2 = "0";
                                    isaddmmconinten_2 = true;
                                }
                                else
                                {
                                    percentle_2 = ((totalmarks_2) * 10) / totalmmmarks_2;
                                    ObtMarks_2 = (percentle_2 + NB_2 + SE_2 + SAT_2);

                                    Gpercentle2 = Gpercentle2 + double.Parse(percentle_2.ToString("0"));
                                    GNB2 = GNB2 + NB_2;
                                    GSE2 = GSE2 + SE_2;
                                    GSAT2 = GSAT2 + SAT_2;
                                    Gtotal2 = Gtotal2 + ObtMarks_2;
                                }

                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>" + dss.Tables[1].Rows[q]["SubjectName"].ToString() + "</td><td class='" + monthlyTest2 + "'>" + percentle_2.ToString("0") + "</td><td>" + NB_2.ToString("0") + "</td><td>" + SE_2.ToString("0") + "</td><td>" + SAT_2.ToString("0") + "</td><td>" + ObtMarks_2.ToString("0") + "</td></tr>");
                            }
                        }
                        percentleGrade2 = grade(Gpercentle2 / dss.Tables[1].Rows.Count);
                        NBGrade2 = grade(GNB2 / dss.Tables[1].Rows.Count);
                        SEGrade2 = grade(GSE2 / dss.Tables[1].Rows.Count);
                        SATGrade2 = grade(GSAT2 / dss.Tables[1].Rows.Count);
                        totalGrade2 = grade(Gtotal2 / dss.Tables[1].Rows.Count);
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total 100</td><td class='" + monthlyTest2 + "'>" + Gpercentle2.ToString("0") + "</td><td>" + GNB2.ToString("0") + "</td><td>" + GSE2.ToString("0") + "</td><td>" + GSAT2.ToString("0") + "</td><td>" + Gtotal2.ToString("0") + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Grade</td><td class='" + monthlyTest2 + "'></td><td></td><td></td><td></td><td>" + totalGrade2 + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Grand Total 200</td><td class='" + monthlyTest2 + "'></td><td></td><td></td><td></td><td>" + grade(((Gtotal1 / dss.Tables[0].Rows.Count) + (Gtotal2 / dss.Tables[1].Rows.Count)) / 2) + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Working Days</td><td>" + (dss.Tables[3].Rows.Count > 0 ? dss.Tables[3].Rows[0]["totaldays"].ToString() : "0") + "</td><td colspan='" + (monthlyTest2 == "hide" ? "3" : "4") + "'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Attendance</td><td>" + (dss.Tables[3].Rows.Count > 0 ? dss.Tables[3].Rows[0]["Attendence"].ToString() : "0") + "</td><td colspan='" + (monthlyTest2 == "hide" ? "3" : "4") + "'></td></tr>");
                        Response.Write("</tbody></table>");

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='trBg'><th colspan='3' style='text-align:left;'>Co-Scholastic Areas: 3-point (A-C) grading scale</th></tr>");
                        if (dss.Tables[4].Rows.Count > 0)
                        {
                            for (int j = 0; j < dss.Tables[4].Rows.Count; j++)
                            {
                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='width: 45%; text-align:left;'>" + dss.Tables[4].Rows[j]["CoscholasticName_1"].ToString() + "</td><td>" + dss.Tables[4].Rows[j]["Grade_1"].ToString() + "</td><td>" + dss.Tables[4].Rows[j]["Grade_2"].ToString() + "</td></tr>");
                            }
                        }
                        else
                        {
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='width: 45%; text-align:left;'></td><td></td><td></td></tr>");
                        }
                        Response.Write("<tr class='trBg'><th style='text-align:left;'>Discipline</th><th></th><th></th></tr>");

                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='width: 45%; text-align:left;'>" + (dss.Tables[5].Rows.Count > 0 ? dss.Tables[5].Rows[0]["CoscholasticName_1"].ToString() : "N/A") + "</td><td>" + (dss.Tables[5].Rows.Count > 0 ? dss.Tables[5].Rows[0]["Grade_1"].ToString() : "0") + "</td><td>" + (dss.Tables[5].Rows.Count > 0 ? dss.Tables[5].Rows[0]["Grade_2"].ToString() : "") + "</td></tr>");
                        Response.Write("</tbody></table>");

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