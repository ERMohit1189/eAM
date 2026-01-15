using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Studentwisecumulative_ItoVIIIServer : System.Web.UI.Page
{
    Campus oo = new Campus();
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
                cmd.CommandText = "ICSEStuWiseCumulative_ItoVIII";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@SectionId", SectionID.Trim());
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
                    ss = ss + " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                    ss = ss + " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                    ss = ss + " where cm.id=" + ClassId + " and bm.id=" + ds.Tables[0].Rows[0]["BranchId"].ToString() + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
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
                        Response.Write("<tr><td style='width:50%;'>80% and above</td><td>A</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>60%-79%</td><td>B</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>50%-59%</td><td>C</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>Below 49%</td><td>D</td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("</td>");

                        Response.Write("<td style='width:70%; vertical-align: top;'>");
                        cmd.CommandText = "ICSEStuWiseCumulative_ItoVIII";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@SrNo", ds.Tables[0].Rows[i]["admissionNo"].ToString());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@SectionId", SectionID.Trim());
                        cmd.Parameters.AddWithValue("@AttendanceTyp", AttendanceTyp.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dss = new DataSet();
                        das.Fill(dss);
                        cmd.Parameters.Clear();

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th class='"+ DisableTest1 + "'>TEST 1</th><th class='" + DisableTest2 + "'>TEST 2</th><th class='" + DisableTest3 + "'>TEST 3</th><th  class='" + monthlyTest1 + "'>MONTHLY TEST</th><th>HALF YEARLY/ANNUAL</th><th>TOTAL 100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='7' class='text-center'><B>TERM1</B></td></tr>");
                        double Gtotal1 = 0; 
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                double Test1 = 0; double Test2 = 0; double Test3 = 0; double HY = 0;
                                if (dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test1 = double.Parse(dss.Tables[0].Rows[p]["Test1"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test2 = double.Parse(dss.Tables[0].Rows[p]["Test2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test3"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test3 = double.Parse(dss.Tables[0].Rows[p]["Test3"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["HY"].ToString().Trim().ToUpper() == "M.L") { }
                                else { HY = double.Parse(dss.Tables[0].Rows[p]["HY"].ToString().Trim()); }
                                if (DisableTest1=="hide")
                                {
                                    Test1 = 0;
                                }
                                if (DisableTest2 == "hide")
                                {
                                    Test2 = 0;
                                }
                                if (DisableTest3 == "hide")
                                {
                                    Test3 = 0;
                                }
                                double totalmarks1 = Test1 > Test2 ? (Test1 > Test3 ? Test1 : (Test2 > Test3 ? Test2 : Test3)) : (Test2 > Test3 ? Test2 : Test3);
                                if (monthlyTest1 == "hide")
                                {
                                    totalmarks1 = 0;
                                }
                                Gtotal1 = Gtotal1 + (totalmarks1+ HY);

                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["PaperName"].ToString() + "</td><td class='" + DisableTest1 + "'>" + Test1.ToString("0.0") + "</td><td class='" + DisableTest2 + "'>" + Test2.ToString("0.0") + "</td><td class='" + DisableTest3 + "'>" + Test3.ToString("0.0") + "</td><td class='" + monthlyTest1 + "'>" + totalmarks1.ToString("0.0") + "</td><td>" + HY.ToString("0.0") + "</td><td>" + ((monthlyTest1=="hide"?0:totalmarks1) + HY).ToString("0.0") + "</td></tr>");
                            }
                        }
                        

                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total 100</td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal1.ToString("0.0") + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Working Days</td><td>" + (dss.Tables[1].Rows.Count > 0 ? dss.Tables[1].Rows[0]["totaldays"].ToString() : "0") + "</td><td colspan='5'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Attendance</td><td>" + (dss.Tables[1].Rows.Count > 0 ? dss.Tables[1].Rows[0]["Attendence"].ToString() : "0") + "</td><td colspan='5'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th class='" + DisableTest4 + "'>TEST 1</th><th class='" + DisableTest5 + "'>TEST 2</th><th class='" + DisableTest6 + "'>TEST 3</th><th class='" + monthlyTest2 + "'>MONTHLY TEST</th><th>HALF YEARLY/ANNUAL</th><th>TOTAL 100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='7' class='text-center'><B>TERM2</B></td></tr>");
                        double Gtotal2 = 0;
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                double Test4 = 0; double Test5 = 0; double Test6 = 0; double AE = 0;
                                if (dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test4"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test4 = double.Parse(dss.Tables[0].Rows[p]["Test4"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test5"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test5 = double.Parse(dss.Tables[0].Rows[p]["Test5"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test6"].ToString().Trim().ToUpper() == "M.L") { }
                                else { Test6 = double.Parse(dss.Tables[0].Rows[p]["Test6"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["AE"].ToString().Trim().ToUpper() == "M.L") { }
                                else { AE = double.Parse(dss.Tables[0].Rows[p]["AE"].ToString().Trim()); }
                                if (DisableTest4 == "hide")
                                {
                                    Test4 = 0;
                                }
                                if (DisableTest5 == "hide")
                                {
                                    Test5 = 0;
                                }
                                if (DisableTest6 == "hide")
                                {
                                    Test6 = 0;
                                }
                                double totalmarks2 = Test4 > Test5 ? (Test4 > Test6 ? Test4 : (Test5 > Test6 ? Test5 : Test6)) : (Test5 > Test6 ? Test5 : Test6);
                                if (monthlyTest2 == "hide")
                                {
                                    totalmarks2 = 0;
                                }
                                Gtotal2 = Gtotal2 + (totalmarks2 + AE);

                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["PaperName"].ToString() + "</td><td class='" + DisableTest4 + "'>" + Test4.ToString("0.0") + "</td><td class='" + DisableTest5 + "'>" + Test5.ToString("0.0") + "</td><td class='" + DisableTest6 + "'>" + Test6.ToString("0.0") + "</td><td class='" + monthlyTest2 + "'>" + totalmarks2.ToString("0.0") + "</td><td>" + AE.ToString("0.0") + "</td><td>" + ((monthlyTest1 == "hide" ? 0 : totalmarks2) + AE).ToString("0.0") + "</td></tr>");
                            }
                        }


                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total 100</td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal2.ToString("0.0") + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Working Days</td><td>" + (dss.Tables[2].Rows.Count > 0 ? dss.Tables[2].Rows[0]["totaldays"].ToString() : "0") + "</td><td colspan='5'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Attendance</td><td>" + (dss.Tables[2].Rows.Count > 0 ? dss.Tables[2].Rows[0]["Attendence"].ToString() : "0") + "</td><td colspan='5'></td></tr>");

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