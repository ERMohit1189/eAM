using System;
using System.Data;
using System.Data.SqlClient;

public partial class ReportCard_NurtoPrepServer : System.Web.UI.Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string Term = Request.Form["Term"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string Status = Request.Form["Status"].ToString().Trim();
        string AttendanceType = Request.Form["AttendanceType"].ToString().Trim();
        string Promotedtoclass = Request.Form["Promotedtoclass"].ToString().Trim();
        string SchoolReopenon = Request.Form["SchoolReopenon"].ToString().Trim();
        string curDate = Request.Form["curDate"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ICSEReportCard_NurtoPrep";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", SessionName.Trim());
                if (SrNo.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                cmd.Parameters.AddWithValue("@status", Status);
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        cmd.CommandText = "ICSEReportCard_NurtoPrep";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", SessionName.Trim());
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@TermName", Term.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                        cmd.Parameters.AddWithValue("@status", Status);
                        cmd.Parameters.AddWithValue("@isManual", AttendanceType);
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                        try
                        {
                            Response.Write("<div  class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<div id='break_div' class='term1 table-responsive' style='height:932px; page-break-after: always;'>");
                            }
                            else
                            {
                                Response.Write("<div id='break_div' class='term2 table-responsive' style='height:932px; page-break-after: always;'>");
                            }
                            Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");

                            Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; text-transform: uppercase  width:100%;'><tbody>");
                            Response.Write("<tr><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>STUDENT'S NAME : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["StudentName"].ToString().Trim() + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>CLASS & SECTION : </span>&nbsp;<span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["CombineClassName"].ToString().Trim() + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>DATE OF BIRTH : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["dob"].ToString().Trim() + "</span></td></tr>");
                            Response.Write("<tr><td class='p-pad-25 '> <span  class='txt-rep-title-12-b customtext'>S.R. NO. : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["admissionNo"].ToString().Trim() + "</span></td><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>FATHER'S NAME : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["Fathername"].ToString().Trim() + "</span></td><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>PHONE NUMBER : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["FatherContactNo"].ToString().Trim() + "</span></td></tr>");
                            Response.Write("<tr><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>MOTHER'S NAME : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["MotherName"].ToString().Trim() + "</span></td><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>CLASS TEACHER'S NAME : </span><span  class='txt-rep-title-12-b customtext'>" + dt.Rows[i]["EmpName"].ToString().Trim() + "</span></td><td></td></tr>");
                            Response.Write("<tr>");
                            Response.Write("<td colspan='2' style='padding: 5px !important; width:70%;vertical-align: top;'>");
                            Response.Write("<table class='tables table term1 mp-table p-table-bordered table-bordered text-center' style='margin-bottom: 5px; margin-top: 5px; width:100%;'><tbody>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<tr class='text-center'></tr>");
                                Response.Write("<tr><th class='text-center' <th style='font-size:9px !important; vertical-align: middle !important;' rowspan='2'>Subject</th><th colspan='3' style='font-size:9px !important;  vertical-align: top;'>FIRST TERM</th></tr>");
                                Response.Write("<tr class='text-center'><th style='font-size:9px !important; vertical-align: top;'>F.A.1</th><th style='font-size:9px !important; vertical-align: top;'>F.A.2</th><th style='font-size:9px !important; vertical-align: top;'>EVALUATION</th></tr>");
                            }
                            else
                            {
                                Response.Write("<tr class='text-center'></tr>");
                                Response.Write("<tr><th class='text-center' <th style='font-size:9px !important; vertical-align: middle !important;' rowspan='2'>Subject</th><th colspan='3' style='font-size:9px !important;  vertical-align: top;'>FIRST TERM</th><th colspan='3' style='font-size:9px !important;  vertical-align: top;'>SECOND TERM</th></tr>");
                                Response.Write("<tr class='text-center'><th style='font-size:9px !important; vertical-align: top;'>F.A.1</th><th style='font-size:9px !important; vertical-align: top;'>F.A.2</th><th style='font-size:9px !important; vertical-align: top;'>EVALUATION</th><th style='font-size:9px !important; vertical-align: top;'>F.A.3</th><th style='font-size:9px !important; vertical-align: top;'>F.A.4</th><th style='font-size:9px !important; vertical-align: top;'>EVALUATION</th></tr>");
                            }

                            string sql2 = "select id, SubjectName from ( ";
                            sql2 = sql2 + " SELECT Id, SubjectName, SubjectType FROM TTSubjectMaster where applicablefor<>'TimeTable' and SubjectType='Compulsory' ";
                            sql2 = sql2 + " and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' ";
                            sql2 = sql2 + " union all ";
                            sql2 = sql2 + " SELECT sm.Id, SubjectName, SubjectType FROM TTSubjectMaster sm  ";
                            sql2 = sql2 + " inner join ICSEOptionalSubjectAllotment me on me.OptSubjectId=sm.id and me.SessionName=sm.SessionName and me.BranchCode=sm.BranchCode ";
                            sql2 = sql2 + " where applicablefor<>'TimeTable' and SubjectType='Optional' and sm.classid=" + ClassId + " and sm.branchid=" + dt.Rows[i]["branchid"].ToString() + "  ";
                            sql2 = sql2 + " and sm.BranchCode=" + BranchCode + " and sm.SessionName='" + SessionName + "' and srNo='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "' ";
                            sql2 = sql2 + " )T1 order by id asc";
                            var dtSubject = oo.Fetchdata(sql2);
                            if (dtSubject.Rows.Count > 0)
                            {
                                for (int s = 0; s < dtSubject.Rows.Count; s++)
                                {
                                    string sql3 = "SELECT Id, PaperName FROM TTPaperMaster where subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' order by id asc";
                                    var dtPaper = oo.Fetchdata(sql3);
                                    if (dtPaper.Rows.Count > 0)
                                    {
                                        for (int p = 0; p < dtPaper.Rows.Count; p++)
                                        {
                                            string sqlm = "select FA1, FA2, EvaluationTerm1, FA3, FA4, EvaluationTerm2 from ICSEMarkEntryNurtoPrep where subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' and SectionId=" + SectionId + " and PaperId=" + dtPaper.Rows[p]["id"].ToString() + " and SrNo='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "'";
                                            var dtMark = oo.Fetchdata(sqlm);
                                            if (dtMark.Rows.Count > 0)
                                            {
                                                if (Term.ToLower() == "Term1".ToLower())
                                                {
                                                    Response.Write("<tr><td class='text-left' style='width:40% !important;'><span>" + dtPaper.Rows[p]["PaperName"].ToString().Trim() + "</span></td><td class='text-center' style='width:20% !important;'>" + dtMark.Rows[0]["FA1"].ToString().Trim() + "</td><td class='text-center' style='width:20% !important;'>" + dtMark.Rows[0]["FA2"].ToString().Trim() + "</td><td class='text-center' style='width:20% !important;'>" + dtMark.Rows[0]["EvaluationTerm1"].ToString().Trim() + "</td></tr>");
                                                }
                                                if (Term.ToLower() == "Term2".ToLower())
                                                {
                                                    Response.Write("<tr><td class='text-left' style='width:40% !important;'><span>" + dtPaper.Rows[p]["PaperName"].ToString().Trim() + "</span></td><td class='text-center' style='width:10% !important;'>" + dtMark.Rows[0]["FA1"].ToString().Trim() + "</td><td class='text-center' style='width:10% !important;'>" + dtMark.Rows[0]["FA2"].ToString().Trim() + "</td><td class='text-center' style='width:10% !important;'>" + dtMark.Rows[0]["EvaluationTerm1"].ToString().Trim() + "</td><td class='text-center' style='width:10% !important;'>" + dtMark.Rows[0]["FA3"].ToString().Trim() + "</td><td class='text-center' style='width:10% !important;'>" + dtMark.Rows[0]["FA4"].ToString().Trim() + "</td><td class='text-center' style='width:10% !important;'>" + dtMark.Rows[0]["EvaluationTerm2"].ToString().Trim() + "</td></tr>");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                double totaldays = 0, attendence = 0, percent = 0; string t1Att = "";
                                totaldays = double.Parse(ds.Tables[0].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["totaldays"].ToString().Trim());
                                attendence = double.Parse(ds.Tables[0].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["attendence"].ToString().Trim());
                                if (attendence != 0 && totaldays != 0)
                                {
                                    percent = (attendence * 100) / totaldays;
                                    t1Att = "<span >" + attendence.ToString().Trim() + "</span>/<span >" + totaldays.ToString().Trim() + "</span> (<span >" + percent.ToString("0.00") + "</span>%)";
                                }
                                Response.Write("<tr class='text-center'><td style='text-align: center;'><b>ATTENDANCE</b></td><td style='text-align: center' colspan='3'><b>" + t1Att + "</b></td></tr>");
                            }
                            if (Term.ToLower() == "Term2".ToLower())
                            {

                                double totaldays1 = 0, attendence1 = 0, percent1 = 0; string t1Att = ""; string t2Att = ""; string AllAtt = "";
                                totaldays1 = double.Parse(ds.Tables[0].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["totaldays"].ToString().Trim());
                                attendence1 = double.Parse(ds.Tables[0].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[0].Rows[0]["attendence"].ToString().Trim());

                                if (attendence1 != 0 && totaldays1 != 0)
                                {
                                    percent1 = (attendence1 * 100) / totaldays1;
                                    t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0.00") + "</span>%)";
                                }
                                double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                totaldays2 = double.Parse(ds.Tables[1].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["totaldays"].ToString().Trim());
                                attendence2 = double.Parse(ds.Tables[1].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[1].Rows[0]["attendence"].ToString().Trim());

                                if (attendence2 != 0 && totaldays2 != 0)
                                {
                                    percent2 = (attendence2 * 100) / totaldays2;
                                    t2Att = "<span >" + attendence2.ToString().Trim() + "</span>/<span >" + totaldays2.ToString().Trim() + "</span> (<span >" + percent2.ToString("0.00") + "</span>%)";
                                }
                                double Gtotaldays = 0, Gattendence = 0, Gpercent = 0;
                                Gtotaldays = (totaldays1 + totaldays2);
                                Gattendence = (attendence1 + attendence2);
                                if (Gattendence != 0 && Gtotaldays != 0)
                                {
                                    Gpercent = (Gattendence * 100) / Gtotaldays;
                                }
                                Response.Write("<tr class='text-center'><td style='text-align: center;'><b>ATTENDANCE</b></td><td style='text-align: center' colspan='3'><b>" + t1Att + "</b></td><td style='text-align: center' colspan='3'><b>" + t2Att + "</b></td></tr>");
                            }
                            


                            if (Session["Logintype"].ToString() != "Guardian")
                            {
                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;border:0px !important;'><tbody><tr>");
                                Response.Write("<tr>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>CLASS TEACHER'S SIGNATURE</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>PRINCIPAL'S SIGNATURE</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>PARENT'S/GUARDIAN'S SIGNATURE</div></td>");
                                Response.Write("</tr></tbody></table>");
                            }
                            Response.Write("</div>");

                            Response.Write("</td>");


                            Response.Write("<td style='padding: 5px !important; width:30%;vertical-align: top;'>");
                            Response.Write("<table class='tables table term1 mp-table p-table-bordered table-bordered text-center' style='margin-bottum:0px;'>");
                            
                            string ss = "select id, RemarkHead from ICSERemarkHeadsNurToPrep where classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' order by id asc";
                            var dtRemarkHead = oo.Fetchdata(ss);
                            if (dtRemarkHead.Rows.Count > 0)
                            {
                                Response.Write("<tr><tr><th>CLASS TEACHER'S REMARK</th><th> H.Y.</th><th class='" + (Term.ToLower() == "term1" ? "hide" : "") + "'>Annual</th></tr>");
                                for (int r = 0; r < dtRemarkHead.Rows.Count; r++)
                                {
                                    if (Term.ToLower() == "Term1".ToLower())
                                    {
                                        string ssr = "select Remark from ICSERemarkNurToPrep where RemarkHeadId=" + dtRemarkHead.Rows[r]["id"].ToString() + " and term='Term1' and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' order by id asc";
                                        Response.Write("<tr><td style='width:80% !important; text-align:left;'>" + dtRemarkHead.Rows[r]["RemarkHead"].ToString() + "</td><td style='width:20% !important;'>" + oo.ReturnTag(ssr, "Remark") + "</td></tr>");
                                    }
                                    else
                                    {
                                        string ssr = "select Remark from ICSERemarkNurToPrep where RemarkHeadId=" + dtRemarkHead.Rows[r]["id"].ToString() + " and term='Term1' and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' order by id asc";
                                        string ssr2 = "select Remark from ICSERemarkNurToPrep where RemarkHeadId=" + dtRemarkHead.Rows[r]["id"].ToString() + " and term='Term2' and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' order by id asc";
                                        Response.Write("<tr><td style='width:70% !important; text-align:left;'>" + dtRemarkHead.Rows[r]["RemarkHead"].ToString() + "</td><td style='width:15% !important;'>" + oo.ReturnTag(ssr, "Remark") + "</td><td style='width:15% !important;'>" + oo.ReturnTag(ssr2, "Remark") + "</td></tr>");
                                    }
                                }
                            }
                            Response.Write("</table>");

                            Response.Write("<table class='tables table term1 mp-table p-table-bordered table-bordered' style='margin-bottum:0px;'>");
                            Response.Write("<tr><tr><tH colspan='2'>GRADING SCALE</tH></tr>");
                            Response.Write("<tr><tr><td>A+ EXCELLENT</td><td class='text-center'>90-100</td></tr>");
                            Response.Write("<tr><tr><td>A VERY GOOD</td><td class='text-center'>80-90</td></tr>");
                            Response.Write("<tr><tr><td>B+ GOOD</td><td class='text-center'>60-80</td></tr>");
                            Response.Write("<tr><tr><td>B AVERAGE</td><td class='text-center'>50-60</td></tr>");
                            Response.Write("<tr><tr><td>C SATISFACTORY</td><td class='text-center'>40-50</td></tr>");
                            if (Session["Logintype"].ToString() != "Guardian")
                            {
                                Response.Write("<tr><tr><td>PROMOTION GRANTED TO CLASS</td><td class='text-center promotedToClass'>"+ Promotedtoclass + "</td></tr>");
                                Response.Write("<tr><tr><td>SCHOOL REOPEN ON DATE</td><td class='text-center reOpenOn'>"+ SchoolReopenon + "</td></tr>");
                                Response.Write("<tr><tr><td>DATE</td><td class='text-center currReportDate'>"+curDate+"</td></tr>");
                            }
                            Response.Write("</table>");


                            Response.Write("</td>");
                            Response.Write("</tr>");
                            Response.Write("</tbody></table>");
                            if (Session["Logintype"].ToString() == "Guardian")
                            {
                                Response.Write("<div class='text-center'>This is an electronically generated report card through Parent Portal.</div>");
                            }
                            Response.Write("</div></div>");
                        }
                        catch (SqlException ex)
                        {
                        }
                    }
                }
            }
        }
    }
}