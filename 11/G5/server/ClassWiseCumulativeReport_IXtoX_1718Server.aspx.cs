using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G5_ClassWiseCumulativeReport_IXtoX_1718Server : System.Web.UI.Page
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
                string html = "";
                cmd.Connection = conn;
                cmd.CommandText = "ClasswiseCumulative_IXtoX_2021";
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
                    html=html+" <div  class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>";
                    html=html+" <div id='break_div' class='term1' style='page-break-after: always; overflow-x: scroll;'>";

                    html=html+" <table id='MainTable' class='table term1 mp-table p-table-bordered text_center table-fixed' style='margin-bottom: 5px; margin-top: 5px; width:100%;'>";
                    html=html+" <thead>";
                    html=html+" <tr class='text_center' style='background: #eae7e7;'><th colspan='" + (dt.Rows.Count + 3) + "'><div style='margin-top: 10px;' class='divHeader'></div>";
                    html=html+" <tr class='text_center' style='background: #eae7e7;'><th colspan='" + (dt.Rows.Count + 3) + "'><div class='text-center col-sm-12'><span><b>Class Wise Cumulative</b></span></div><div class='text-center'><span><b>Class:</b></span>&nbsp;<span>" + ClassName + "</span>&nbsp;&nbsp;<span><b>Section:</b></span>&nbsp;<span>" + SectionName+ "</span>&nbsp;&nbsp;<span><b>Eval.:</b></span>&nbsp;<span>" + TermNmae+"</span></div></th></tr>";
                    html=html+" <tr style='background: #eae7e7;'><th  style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;' rowspan='2'>#</th><th  style='font-size:11px !important; padding: 5px; width: 70px !important; max-width: 70px !important; min-width: 70px !important; vertical-align: middle;' rowspan='2'>S.R. No.</th><th style='font-size:11px !important; width: 115px !important; max-width: 115px !important; min-width: 115px !important; vertical-align: middle;' rowspan='2'>Students Name</th><th colspan='" + dt.Rows.Count + "' style='font-size:11px !important;  vertical-align: top;'>Subject</th></tr>";
                    html=html+" <tr class='text_center' style='background: #eae7e7;'>";
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        html=html+" <th style='font-size:11px !important; vertical-align: top;'>" + dt.Rows[s]["PaperName"].ToString() + "<br>";
                        html=html+" <table class='innerTable1' style='margin-bottom: 0px !important;'>";
                        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + dt.Rows[s]["SubjectId"].ToString() + " and SessionName='" + session.ToString() + "' and BranchCode=" + BranchCode.ToString() + " and classid=" + ClassId.ToString() + "";
                        bool isAdditional = false;
                        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
                        {
                            isAdditional = true;
                        }

                        if (TermNmae.ToLower() == "term1")
                        {
                            if (isAdditional)
                            {
                                html=html+" <tr><td>T1</td><td>T2</td><td>PRAC</td><td>PORT</td><td>HY</td></tr>";
                                html=html+" <tr><td>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks7"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td></tr>";
                            }
                            else
                            {
                                    html=html+" <tr><td>T1</td><td>T2</td><td>SE</td><td>MA</td><td>PORT</td><td>HY</td></tr>";
                                    html=html+" <tr><td>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks3"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks4"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td></tr>";
                            }
                        }
                        if (TermNmae.ToLower() == "term2")
                        {
                            if (isAdditional)
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    html=html+" <tr><td>T1</td><td>T2</td><td>PRAC</td><td>PORT</td><td>AE</td></tr>";
                                    html=html+" <tr><td>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks7"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td></tr>";
                                }
                                if (ClassName.ToUpper() == "X")
                                {
                                    html=html+" <tr><td>T1</td><td>PRAC</td><td>PORT</td><td>AE</td><td>AE 2</td></tr>";
                                    html=html+" <tr><td>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks7"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks8"].ToString().Trim() + "</td></tr>";
                                }
                            }
                            else
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    html=html+" <tr><td>T1</td><td>T2</td><td>SE</td><td>MA</td><td>PORT</td><td>AE</td></tr>";
                                    html=html+" <tr><td>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks2"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks3"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks4"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td></tr>";
                                }
                                if (ClassName.ToUpper() == "X")
                                {
                                    html=html+" <tr><td>T1</td><td>SE</td><td>MA</td><td>PORT</td><td>AE</td><td>AE 2</td></tr>";
                                    html=html+" <tr><td>" + dt.Rows[s]["MaxMarks1"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks3"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks4"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks5"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks6"].ToString().Trim() + "</td><td>" + dt.Rows[s]["MaxMarks8"].ToString().Trim() + "</td></tr>";
                                }
                            }
                        }
                        html=html+" </table>";
                        html=html+" </th>";
                    }
                    html=html+" </tr>";
                    html=html+" </thead>";


                    cmd.CommandText = "ClasswiseCumulative_IXtoX_2021";
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
                            html=html+" <tr class='text_center'>";
                            html=html+" <td style='font-size:11px !important; padding: 5px; width: 37px !important; max-width: 37px !important; min-width: 37px !important; vertical-align: middle;'>" + (i + 1) + "</td><td style='font-size:11px !important; vertical-align: top; width: 70px !important; max-width: 70px !important; min-width: 70px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["admissionNo"].ToString() + "</td><td style='font-size:11px !important; vertical-align: top; width: 115px !important; max-width: 115px !important; min-width: 115px !important; word-wrap: break-word !important;'>" + dt1.Rows[i]["StudentName"].ToString() + "</td>";
                            for (int s2 = 0; s2 < dt.Rows.Count; s2++)
                            {
                                cmd.CommandText = "ClasswiseCumulative_IXtoX_2021";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                                cmd.Parameters.AddWithValue("@PaperId", dt.Rows[s2]["PaperId"].ToString().Trim());
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
                                        string sqlAd = "select count(1) cnt from TTSubjectMaster where IsAditional=1 and Id=" + dt2.Rows[m]["SubjectId"].ToString() + " and SessionName='" + session.ToString() + "' and BranchCode=" + BranchCode.ToString() + " and classid=" + ClassId.ToString() + "";
                                        bool isAdditional = false;
                                        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
                                        {
                                            isAdditional = true;
                                        }
                                        html=html+" <td style='font-size:11px !important; vertical-align: top;'>";
                                        html=html+" <table class='innerTable' style='margin-bottom: 0px !important;'>";
                                        if (TermNmae.ToLower() == "term1")
                                        {
                                            if (isAdditional)
                                            {
                                                html=html+" <tr><td>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Test2"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Prac"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Port"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td></tr>";
                                            }
                                            else
                                            {
                                                html=html+" <tr><td>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Test2"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SE"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["MA"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Port"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td></tr>";
                                            }
                                        }
                                        if (TermNmae.ToLower() == "term2")
                                        {
                                            if (isAdditional)
                                            {
                                                if (ClassName.ToUpper() == "IX")
                                                {
                                                    html=html+" <tr><td>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Test2"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Prac"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Port"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td></tr>";
                                                }
                                                if (ClassName.ToUpper() == "X")
                                                {
                                                    html=html+" <tr><td>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Prac"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Port"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT2"].ToString().Trim() + "</td></tr>";
                                                }
                                            }
                                            else
                                            {
                                                if (ClassName.ToUpper() == "IX")
                                                {
                                                    html=html+" <tr><td>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Test2"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SE"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["MA"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Port"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td></tr>";
                                                }
                                                if (ClassName.ToUpper() == "X")
                                                {
                                                    html=html+" <tr><td>" + dt2.Rows[m]["Test1"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SE"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["MA"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["Port"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT"].ToString().Trim() + "</td><td>" + dt2.Rows[m]["SAT2"].ToString().Trim() + "</td></tr>";
                                                }
                                            }
                                        }
                                        html=html+" </table>";
                                        html=html+" </td>";
                                    }
                                }
                            }
                            html=html+" </tr>";
                        }
                    }
                }
                
                html=html+" </table>";
                html=html+" </div></div>";
                Response.Write(html);
            }
        }
    }
}
