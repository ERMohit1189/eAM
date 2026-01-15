using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G4_ClassWiseCumulativeReport_VItoVIII_1718Server : System.Web.UI.Page
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
                cmd.CommandText = "ClasswiseCumulative_VItoVIII_2021";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@TermName", TermNmae.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@action", "Subjectd");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                    ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                    ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                    ss +=  " where cm.id=" + ClassId + " and bm.id=" + dt.Rows[0]["BranchId"].ToString() + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='" + session + "'";
                    string DisableTest1 = (TermNmae.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : ""));
                    string DisableTest2 = (TermNmae.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : ""));
                    string DisableTest3 = (TermNmae.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : ""));


                    Response.Write("<div  class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                    Response.Write("<div id='break_div' class='term1' style='page-break-after: always; overflow-x: scroll;'>");

                    Response.Write("<table id='MainTable' class='table term1 mp-table p-table-bordered text_center table-fixed' style='margin-bottom: 5px; margin-top: 5px; width:100%;'>");
                    Response.Write("<thead>");
                    Response.Write("<tr class='text_center' style='background: #eae7e7;'><th colspan='" + (dt.Rows.Count + 3) + "'><div style='margin-top: 10px;' class='divHeader'></div>");
                    Response.Write("<tr class='text_center' style='background: #eae7e7;'><th colspan='" + (dt.Rows.Count + 3) + "'><div class='text-center col-sm-12'><span><b>Class Wise Cumulative</b></span></div><div class='text-center'><span><b>Class:</b></span>&nbsp;<span>" + ClassName + "</span>&nbsp;&nbsp;<span><b>Section:</b></span>&nbsp;<span>" + SectionName+ "</span>&nbsp;&nbsp;<span><b>Eval.:</b></span>&nbsp;<span>" + TermNmae+"</span></div></th></tr>");
                    Response.Write("<tr style='background: #eae7e7;'><th  style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;' rowspan='2'>#</th><th  style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 70px !important; min-width: 70px !important; vertical-align: middle;' rowspan='2'>S.R. NO.</th><th style='font-size:11px !important; width: 115px !important; max-width: 115px !important; min-width: 115px !important; vertical-align: middle;' rowspan='2'>Students Name</th><th colspan='" + dt.Rows.Count + "' style='font-size:11px !important;  vertical-align: top;'>Subject</th></tr>");
                    Response.Write("<tr class='text_center' style='background: #eae7e7;'>");
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        Response.Write("<th style='font-size:11px !important; vertical-align: top;'>" + dt.Rows[s]["PaperName"].ToString() + "<br>");
                        Response.Write("<table class='innerTable1' style='margin-bottom: 0px !important;'>");
                        Response.Write("<tr><td class='" + DisableTest1 + "'>" + (TermNmae.Trim().ToLower() == "term1" ? "T1" : "T3") + "</td><td class='" + DisableTest2 + "'>" + (TermNmae.Trim().ToLower() == "term1" ? "T2" : "T4") + "</td><td>NB</td><td>SE</td><td>" + (TermNmae.Trim().ToLower() == "term1" ? "HY" : "AE") + "</td><td>PR.</td></tr>");
                        Response.Write("<tr><td class='" + DisableTest1 + "'>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td class='" + DisableTest2 + "'>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks4"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks7"].ToString().Trim() + "</td></tr>");
                        Response.Write("</table>");
                        Response.Write("</th>");
                    }
                    Response.Write("</tr>");
                    Response.Write("</thead>");


                    cmd.CommandText = "ClasswiseCumulative_VItoVIII_2021";
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
                            Response.Write("<tr class='text_center'>");
                            Response.Write("<td style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;'>" + (i + 1) + "</td><td style='font-size:11px !important; vertical-align: top; width: 70px !important; max-width: 70px !important; min-width: 70px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["admissionNo"].ToString() + "</td><td style='font-size:11px !important; vertical-align: top; width: 115px !important; max-width: 115px !important; min-width: 115px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["StudentName"].ToString() + "</td>");
                            for (int s2 = 0; s2 < dt.Rows.Count; s2++)
                            {
                                cmd.CommandText = "ClasswiseCumulative_VItoVIII_2021";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                                cmd.Parameters.AddWithValue("@Paperid", dt.Rows[s2]["Paperid"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SrNo", dt1.Rows[i]["admissionNo"].ToString().Trim());
                                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                                cmd.Parameters.AddWithValue("@TermName", TermNmae.Trim());
                                cmd.Parameters.AddWithValue("@action", "Marks");
                                SqlDataAdapter das2 = new SqlDataAdapter(cmd);
                                DataTable dt2 = new DataTable();
                                das1.Fill(dt2);
                                cmd.Parameters.Clear();
                                if (dt2.Rows.Count > 0)
                                {
                                    for (int m = 0; m < dt2.Rows.Count; m++)
                                    {
                                        Response.Write("<td style='font-size:11px !important; vertical-align: top;'>");
                                        Response.Write("<table class='innerTable' style='margin-bottom: 0px !important;'>");
                                        Response.Write("<tr><td class='" + DisableTest1 + "'>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td class='" + DisableTest2 + "'>" + dt2.Rows[m]["Test2"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["NB"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SE"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Prac"].ToString().Trim() + "</td></tr>");
                                        Response.Write("</table>");
                                        Response.Write("</td>");
                                    }
                                }
                            }
                            Response.Write("</tr>");
                        }
                    }
                }
                
                Response.Write("</table>");
                Response.Write("</div></div>");
            }
        }
    }
}
