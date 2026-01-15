using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _11_G3_server_ClasswiseAllSubjectPrePrimaryServer : System.Web.UI.Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionID = Request.Form["SectionID"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string TermNmae = Request.Form["TermNmae"].ToString().Trim();
        string session = Request.Form["session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string status = Request.Form["status"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = conn;
                cmd.CommandText = "ClasswiseCumulative_ItoV_2021";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@TermName", TermNmae.Trim());
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                cmd.Parameters.AddWithValue("@action", "Subjectd");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                    ss += " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                    ss += " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                    ss += " where cm.id=" + ClassId + " and bm.id=" + dt.Rows[0]["BranchId"].ToString() + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='" + session + "'";
                    string DisableTest1 = (TermNmae.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : ""));
                    string DisableTest2 = (TermNmae.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : ""));
                    string DisableTest3 = (TermNmae.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : ""));

                    Response.Write("<div  class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                    Response.Write("<div id='break_div' class='term1' style='page-break-after: always; overflow-x: scroll;'>");

                    Response.Write("<table id='MainTable' class='table term1 mp-table p-table-bordered text_center table-fixed' style='margin-bottom: 5px; margin-top: 5px; width:100%;'>");
                    Response.Write("<thead>");
                    Response.Write("<tr class='text_center' style='background: #eae7e7;'><th colspan='" + (dt.Rows.Count + 3) + "'><div style='margin-top: 10px;' class='divHeader'></div></th></tr>");
                    Response.Write("<tr class='text_center' style='background: #eae7e7;'><th colspan='" + (dt.Rows.Count + 3) + "'><div class='text-center col-sm-12'><span><b>Class Wise Cumulative</b></span></div><div class='text-center'><span><b>Class:</b></span>&nbsp;<span>" + ClassName + "</span>&nbsp;&nbsp;<span><b>Section:</b></span>&nbsp;<span>" + SectionName + "</span>&nbsp;&nbsp;<span><b>Eval:</b></span>&nbsp;<span>" + TermNmae + "</span></div></th></tr>");
                    Response.Write("<tr style='background: #eae7e7;'><th  style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;' rowspan='2'>#</th><th  style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 70px !important; min-width: 70px !important; vertical-align: middle;' rowspan='2'>S.R. NO.</th><th style='font-size:11px !important; width: 115px !important; max-width: 115px !important; min-width: 115px !important; vertical-align: middle;' rowspan='2'>Students Name</th><th colspan='" + dt.Rows.Count + "' style='font-size:11px !important;  vertical-align: top;'>Subject</th></tr>");
                    Response.Write("<tr class='text_center' style='background: #eae7e7;'>");
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        Response.Write("<th style='font-size:11px !important; vertical-align: top;'>" + dt.Rows[s]["PaperName"].ToString() + "<br>");
                        Response.Write("<table class='innerTable1' style='margin-bottom: 0px !important;'>");
                        Response.Write("<tr><td class='" + DisableTest1 + "'>" + (TermNmae.Trim().ToLower() == "term1" ? "UT1" : "UT3") + "</td><td class='" + DisableTest2 + "'>" + (TermNmae.Trim().ToLower() == "term1" ? "UT2" : "UT4") + "</td><td>" + (TermNmae.Trim().ToLower() == "term1" ? "HY" : "AE") + "</td><td>PRAC</td></tr>");
                        //Response.Write("<tr><td class='" + DisableTest1 + "'>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td class='" + DisableTest2 + "'>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks4"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks7"].ToString().Trim() + "</td></tr>");
                        Response.Write("<tr><td class='" + DisableTest1 + "'>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td class='" + DisableTest2 + "'>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks4"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td></tr>");
                        Response.Write("</table>");
                        Response.Write("</th>");
                    }
                    Response.Write("</tr>");
                    Response.Write("</thead>");

                    cmd.CommandText = "ClasswiseCumulative_ItoV_2021";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", ClassId);
                    cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                    cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                    cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@action", "Student");

                    SqlDataAdapter das1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    das1.Fill(dt1);
                    cmd.Parameters.Clear();
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            Response.Write("<tr class='text_center NewRowmain'>");
                            Response.Write("<td style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;'>" + (i + 1) + "</td><td style='font-size:11px !important; vertical-align: top; width: 70px !important; max-width: 70px !important; min-width: 70px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["admissionNo"].ToString() + "</td><td style='font-size:11px !important; vertical-align: top; width: 115px !important; max-width: 115px !important; min-width: 115px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["StudentName"].ToString() + "</td>");
                            for (int s2 = 0; s2 < dt.Rows.Count; s2++)
                            {
                                cmd.CommandText = "ClasswiseCumulative_ItoV_2021";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                                cmd.Parameters.AddWithValue("@Paperid", dt.Rows[s2]["Paperid"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SrNo", dt1.Rows[i]["admissionNo"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                                cmd.Parameters.AddWithValue("@TermName", TermNmae.Trim());
                                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                                cmd.Parameters.AddWithValue("@action", "Marks");
                                SqlDataAdapter das2 = new SqlDataAdapter(cmd);
                                DataTable dt2 = new DataTable();
                                das1.Fill(dt2);
                                cmd.Parameters.Clear();
                              //  Response.Write("<tr class='text_center NewRowmain'>");
                              //  Response.Write("<td style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;'>" + (i + 1) + "</td><td style='font-size:11px !important; vertical-align: top; width: 70px !important; max-width: 70px !important; min-width: 70px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["admissionNo"].ToString() + "</td><td style='font-size:11px !important; vertical-align: top; width: 115px !important; max-width: 115px !important; min-width: 115px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["StudentName"].ToString() + "</td>");
                                if (dt2.Rows.Count > 0)
                                {
                                    for (int m = 0; m < dt2.Rows.Count; m++)
                                    {
                                        Response.Write("<td style='font-size:11px !important; vertical-align: top;'>");
                                        Response.Write("<table class='innerTable' style='margin-bottom: 0px !important;'>");
                                        Response.Write("<tr><td class='" + DisableTest1 + "'>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td class='" + DisableTest2 + "'>" + dt2.Rows[m]["Test2"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["TH"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Prac"].ToString().Trim() + "</td></tr>");
                                        Response.Write("</table>");
                                        Response.Write("</td>");
                                    }
                                }
                                //Response.Write("</tr>");
                            }
                            Response.Write("</tr>");
                        }
                    }
                }

                Response.Write("</table>");
                Response.Write("</div></div>");
            }
        }



        // Campus oo = new Campus();
        // string sql = "";
        //// string ClassName = Request.Form["ClassName"].ToString().Trim();
        //// string SectionName = Request.Form["SectionName"].ToString().Trim();
        // string Evail = Request.Form["TermNmae"].ToString().Trim();
        // //string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        //// string PaperId = Request.Form["PaperId"].ToString().Trim();
        // string Session = Request.Form["Session"].ToString().Trim();
        //// string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        //// string ClassId = Request.Form["ClassId"].ToString().Trim();

        // using (SqlConnection conn = new SqlConnection())
        // {
        //     conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
        //     using (SqlCommand cmd = new SqlCommand())
        //     {
        //         sql = "select SubjectType from TTSubjectMaster where SessionName='" + Session + "' and ClassId=" + ClassId + "  and SectionName='" + SectionName + "' and BranchCode=" + BranchCode + "";
        //         bool isoptional = (oo.ReturnTag(sql, "SubjectType").ToLower() == "optional" ? true : false);
        //         sql = "select   sg.srno, Name, FatherName, sg.BranchId Branch from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") SG ";
        //         sql += " where  sg.classid=" + ClassId.ToString() + "";
        //         sql += " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
        //         sql += " sg.BranchCode='" + BranchCode.ToString() + "'";
        //         if (isoptional)
        //         {
        //             sql += " and So.srno in (select Srno from TTSubjectMaster sm inner join ICSEOptionalSubjectAllotment opt on opt.OptSubjectId=sm.id and sm.SessionName=opt.SessionName and sm.BranchCode=opt.BranchCode where sm.SessionName='" + Session + "'  and sm.BranchCode=" + BranchCode + " )";
        //         }
        //         sql += " and sg.Withdrwal is null and isnull(Promotion,'')<>'Cancelled' order by FirstName Asc";
        //         cmd.Connection = conn;
        //         cmd.CommandText = sql;
        //         cmd.CommandType = CommandType.Text;

        //         SqlDataAdapter da = new SqlDataAdapter(cmd);
        //         DataTable dt = new DataTable();
        //         da.Fill(dt);
        //         cmd.Parameters.Clear();
        //         if (dt.Rows.Count > 0)
        //         {
        //             try
        //             {

        //                 string MaxMarks1 = "", MaxMarks2 = "", MaxMarks4 = "", MaxMarks5 = "";
        //                 double MaxMarks1M = 0, MaxMarks2M = 0, MaxMarks4M = 0, MaxMarks5M = 0;
        //                 string sql1 = "Select MaxMarks1,MaxMarks2,MaxMarks4,MaxMarks5 from SGSMaxMarkEntryPrimary where ClassId=" + ClassId + " and SectionName='" + SectionName + "' and Evaluation='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + "  and SessionName='" + Session.ToString() + "'";

        //                 MaxMarks1 = (oo.ReturnTag(sql1, "MaxMarks1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks1").ToString());
        //                 MaxMarks2 = (oo.ReturnTag(sql1, "MaxMarks2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks2").ToString());
        //                 MaxMarks4 = (oo.ReturnTag(sql1, "MaxMarks4").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks4").ToString());
        //                 MaxMarks5 = (oo.ReturnTag(sql1, "MaxMarks5").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks5").ToString());

        //                 Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
        //                 Response.Write("<tr>");
        //                 Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
        //                 Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-48 text-left' scope='col'>S.R. No.</th>");
        //                 Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Student's Name</th>");
        //                 Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Father's Name</th>");
        //                 if (Evail.ToLower() == "term1")
        //                 {
        //                     Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. I </span><br>" + MaxMarks1.ToString() + "</th>");
        //                     Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. II </span><br>" + MaxMarks2.ToString() + "</th>");
        //                 }
        //                 if (Evail.ToLower() == "term2")
        //                 {
        //                     Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. III </span><br>" + MaxMarks1.ToString() + "</th>");
        //                     Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. IV </span><br>" + MaxMarks2.ToString() + "</th>");
        //                 }
        //                 Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Average</th>");
        //                 if (Evail.ToLower() == "term1")
        //                 {
        //                     Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col' colspan='2'  style='padding:0 !important;padding-top:5px !important;'><span> H.Y.E.</span>(80)<br>");
        //                 }
        //                 if (Evail.ToLower() == "term2")
        //                 {
        //                     Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col' colspan='2'  style='padding:0 !important;padding-top:5px !important;'><span> A.E.</span>(80)<br>");
        //                 }
        //                 Response.Write("<table class='table table-bordered' style='width:100%; padding:0 !important; margin:0 !important; margin-top:5px !important;'><tr><th style='width:50% !important'><span id=''>Theory</span><br>" + (MaxMarks4 == "" ? "0" : MaxMarks4).ToString() + "</th><th style='width:50% !important'><span id=''>Pra./Or./Proj.</span><br>" + (MaxMarks5 == "" ? "0" : MaxMarks5).ToString() + "</th></tr></table></th>");
        //                 Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Grade</th>");
        //                 Response.Write("</tr>");

        //                 for (int i = 0; i < dt.Rows.Count; i++)
        //                 {
        //                     string Test1 = "", Test2 = "", TH = "", id = "", Prac = "";
        //                     double Test1M = 0, Test2M = 0, THM = 0, PracM = 0;
        //                     string sql3 = "Select Id,Test1,Test2,TH,Prac from SGSMarkEntryPrimary where  ClassId=" + ClassId + " and SectionName='" + SectionName + "' and SRNO='" + dt.Rows[i]["srno"].ToString() + "' and Evaluation='" + Evail.ToString().Trim() + "'  and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode + "";
        //                     id = oo.ReturnTag(sql3, "Id");
        //                     Test1 = oo.ReturnTag(sql3, "Test1");
        //                     Test2 = oo.ReturnTag(sql3, "Test2");

        //                     TH = oo.ReturnTag(sql3, "TH");
        //                     Prac = oo.ReturnTag(sql3, "Prac");

        //                     if (Test1.ToString().Trim().ToUpper() == "NP" || Test1.ToString().Trim().ToUpper() == "NAD" || Test1.ToString().Trim().ToUpper() == "ML" || Test1.ToString().Trim().ToUpper() == "") { }
        //                     else { Test1M = (double.Parse(Test1.ToString().Trim())); }
        //                     if (Test2.ToString().Trim().ToUpper() == "NP" || Test2.ToString().Trim().ToUpper() == "NAD" || Test2.ToString().Trim().ToUpper() == "ML" || Test2.ToString().Trim().ToUpper() == "") { }
        //                     else { Test2M = (double.Parse(Test2.ToString().Trim())); }
        //                     if (TH.ToString().Trim().ToUpper() == "NP" || TH.ToString().Trim().ToUpper() == "NAD" || TH.ToString().Trim().ToUpper() == "ML" || TH.ToString().Trim().ToUpper() == "") { }
        //                     else { THM = double.Parse(TH.ToString().Trim()); }
        //                     if (Prac.ToString().Trim().ToUpper() == "NP" || Prac.ToString().Trim().ToUpper() == "NAD" || Prac.ToString().Trim().ToUpper() == "ML" || Prac.ToString().Trim().ToUpper() == "") { }
        //                     else { PracM = double.Parse(Prac.ToString().Trim()); }

        //                     if (MaxMarks1.ToString().Trim().ToUpper() == "NP" || MaxMarks1.ToString().Trim().ToUpper() == "NAD" || MaxMarks1.ToString().Trim().ToUpper() == "ML" || MaxMarks1.ToString().Trim().ToUpper() == "") { }
        //                     else { MaxMarks1M = double.Parse(MaxMarks1.ToString().Trim()); }
        //                     if (MaxMarks2.ToString().Trim().ToUpper() == "NP" || MaxMarks2.ToString().Trim().ToUpper() == "NAD" || MaxMarks2.ToString().Trim().ToUpper() == "ML" || MaxMarks2.ToString().Trim().ToUpper() == "") { }
        //                     else { MaxMarks2M = double.Parse(MaxMarks2.ToString().Trim()); }

        //                     if (MaxMarks4.ToString().Trim().ToUpper() == "NP" || MaxMarks4.ToString().Trim().ToUpper() == "NAD" || MaxMarks4.ToString().Trim().ToUpper() == "ML" || MaxMarks4.ToString().Trim().ToUpper() == "") { }
        //                     else { MaxMarks4M = double.Parse(MaxMarks4.ToString().Trim()); }
        //                     if (MaxMarks5.ToString().Trim().ToUpper() == "NP" || MaxMarks5.ToString().Trim().ToUpper() == "NAD" || MaxMarks5.ToString().Trim().ToUpper() == "ML" || MaxMarks5.ToString().Trim().ToUpper() == "") { }
        //                     else { MaxMarks5M = double.Parse(MaxMarks5.ToString().Trim()); }

        //                     string ptm1 = ""; string ObtMarks1 = ""; string Grade1 = "";

        //                     double avg = (Test1M + Test2M) / 2;
        //                     ptm1 = (avg).ToString("0.00");

        //                     ObtMarks1 = (double.Parse(ptm1) + THM + PracM).ToString("0");
        //                     Grade1 = Grade(double.Parse(ObtMarks1));

        //                     Response.Write("<tr id=" + id + ">");
        //                     Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
        //                     Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
        //                     Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
        //                     Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'>" + Test1.ToString() + "</td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'>" + Test2.ToString() + "</td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + ptm1 + "</span></td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'>" + TH.ToString() + "</td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'>" + Prac.ToString() + "</td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString() + "</span></td>");
        //                     Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : Grade1.ToString()) + "</span></td>");
        //                     Response.Write("</tr>");
        //                 }
        //                 Response.Write("</tbody></table>");
        //              //   Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit' /></div>");

        //             }
        //             catch (SqlException ex)
        //             { throw ex; }
        //         }
        //     }
        // }
    }
    public string Grade(double percentle)
    {
        if (percentle <= 39)
        {
            return "E";
        }
        else if (percentle >= 39.1 && percentle <= 50)
        {
            return "C2";
        }
        else if (percentle >= 50.1 && percentle <= 60)
        {
            return "C1";
        }
        else if (percentle >= 60.1 && percentle <= 70)
        {
            return "B2";
        }
        else if (percentle >= 70.1 && percentle <= 80)
        {
            return "B1";
        }
        else if (percentle >= 80.1 && percentle <= 90)
        {
            return "A2";
        }
        else if (percentle >= 90.1 && percentle <= 100)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }
}