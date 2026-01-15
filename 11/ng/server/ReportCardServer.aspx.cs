using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class ReportCardServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string srno = Request.Form["srno"].ToString().Trim();
        string TermId = Request.Form["TermId"].ToString().Trim();
        string TermName = Request.Form["TermName"].ToString().Trim();
        string AttendanceType = Request.Form["AttendanceType"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string Currdate = Request.Form["Currdate"].ToString().Trim();


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "master_NG_Reportcard";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                cmd.Parameters.AddWithValue("@SectionID", SectionId.Trim());
                cmd.Parameters.AddWithValue("@BranchId", BranchId.Trim());
                cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                if (srno.Trim()!="")
                {
                    cmd.Parameters.AddWithValue("@SrNo", srno.Trim());
                }
                cmd.Parameters.AddWithValue("@action", "students");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "master_NG_Reportcard";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                        cmd.Parameters.AddWithValue("@SectionID", SectionId.Trim());
                        cmd.Parameters.AddWithValue("@BranchId", BranchId.Trim());
                        cmd.Parameters.AddWithValue("@TermId", TermId.Trim());
                        cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType.Trim());
                        cmd.Parameters.AddWithValue("@TermName", TermName.Trim());
                        cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["SrNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cmd.Parameters.Clear();

                        try
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int totaldays = 0, present = 0, absent = 0;
                                totaldays= int.Parse(ds.Tables[3].Rows[0]["totaldays"].ToString() == "" ? "0" : ds.Tables[3].Rows[0]["totaldays"].ToString().Trim());
                                present  = int.Parse(ds.Tables[3].Rows[0]["attendence"].ToString() == "" ? "0" : ds.Tables[3].Rows[0]["attendence"].ToString().Trim());
                                absent = (totaldays - present);

                                Response.Write("<div class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                                Response.Write("<div id='break_div' class='term1' style='height:1032px; page-break-after: always;'>");

                                Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                                Response.Write("<div  style='padding-top: 99px;'>");

                                Response.Write("<table class='table term1 mp-table p-table-bordered table-bordered text-uppercase term1' style='margin-bottom: 5px; text-transform: uppercase  width:100%;'><tbody>");
                                Response.Write("<tr><td class='p-pad-25'> <span  class='txt-rep-title-12-b customtext'>STUDENT'S NAME&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + ds.Tables[0].Rows[0]["Name"].ToString().Trim() + "</span></td><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>Date of Birth&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + (ds.Tables[0].Rows[0]["DOB"].ToString()==""?"": DateTime.Parse(ds.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd-MMM-yyyy")) + "</span></td><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>Class&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + ds.Tables[0].Rows[0]["class"].ToString().Trim() + "</span></td></tr>");
                                Response.Write("<tr><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>Term&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + TermName.ToString().Trim() + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>Weight&nbsp;:&nbsp;</ span>&nbsp;<span  class='txt-rep-title-12-b customtext'>" + ds.Tables[0].Rows[0]["Weight"].ToString().Trim() + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>Height&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + ds.Tables[0].Rows[0]["Height"].ToString().Trim() + "</span></td></tr>");
                                Response.Write("<tr><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>No. of Students&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + ds.Tables[1].Rows[0]["noofStu"].ToString().Trim() + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>Next term begins on&nbsp;:&nbsp;</ span>&nbsp;<span  class='txt-rep-title-12-b customtext'>" + (ds.Tables[2].Rows[0]["NextTermBeginDate"].ToString()==""?"":DateTime.Parse(ds.Tables[2].Rows[0]["NextTermBeginDate"].ToString()).ToString("dd-MMM-yyyy")) + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>Date&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + DateTime.Parse(Currdate).ToString("dd-MMM-yyyy") + "</span></td></tr>");
                                Response.Write("<tr><td class='p-pad-25'><span  class='txt-rep-title-12-b customtext'>No. of times school opened&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + totaldays.ToString() + "</span></td><td class='p-pad-25 '><span  class='txt-rep-title-12-b customtext'>No. of times Present&nbsp;:&nbsp;</span>&nbsp;<span  class='txt-rep-title-12-b customtext'>" + present.ToString() + "</span></td><td class='p-pad-25 '><span class='txt-rep-title-12-b customtext'>No. of times Absent&nbsp;:&nbsp;</span><span  class='txt-rep-title-12-b customtext'>" + absent.ToString() + "</span></td></tr>");
                                Response.Write("<tr>");
                                Response.Write("<td colspan='3'>");

                                Response.Write("<table class='table term1 mp-table p-table-bordered table-bordered text_center' style='margin-bottom: 5px; margin-top: 5px; width:100%;'><tbody>");
                                Response.Write("<tr></tr>");
                                Response.Write("<tr class='text-center' style='height: 141px;'><th style='width:20%; text-align:center !important;'><div>SUBJECT</div></th><th style='width:7%;'><div class='vertical-text'>COGNITIVE 40%</div></th><th style='width:7%;'><div class='vertical-text'>AFFECTIVE 10%</div></th><th style='width:7%;'><div class='vertical-text'>PSYCHOMOTOR 10%</div></th><th class='th_tdDesign' style='width:7%;'><div class='vertical-text'>TOTAL CA 60%</div></th><th style='width:7%;'><div class='vertical-text'>EXAMS 40%</div></th><th class='th_tdDesign' style='width:7%;'><div class='vertical-text'>Mark Obtained 100%</div></th><th style='width:7%;'><div class='vertical-text'>Grade</div></th><th style='width:7%;'><div class='vertical-text'>Highest in class</div></th><th style='width:7%;'><div class='vertical-text'>lowest in class</div></th><th class='th_tdDesign' style='width:17%;'><div class='vertical-text'>Teacher's remark</div></th></tr>");
                                double avg = 0.0;
                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    
                                    for (int j = 0; j < ds.Tables[4].Rows.Count; j++)
                                    {
                                        Response.Write("<tr class='text_center'>");
                                        Response.Write("<td class='tdDesign' style='width:20%; text-align:left !important;'>" + ds.Tables[4].Rows[j]["subjectName"].ToString() + "</td>");
                                        Response.Write("<td class='tdDesign'>" + (ds.Tables[4].Rows[j]["conutive"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["conutive"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='tdDesign'>" + (ds.Tables[4].Rows[j]["affective"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["affective"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='tdDesign'>" + (ds.Tables[4].Rows[j]["phychomotor"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["phychomotor"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='th_tdDesign tdDesign'>" + (ds.Tables[4].Rows[j]["totalCa"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["totalCa"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='tdDesign'>" + (ds.Tables[4].Rows[j]["exam"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["exam"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='th_tdDesign tdDesign'>" + (ds.Tables[4].Rows[j]["obtainedMark"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["obtainedMark"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='tdDesign'>" + ds.Tables[4].Rows[j]["gade"].ToString() + "</td>");
                                        Response.Write("<td class='tdDesign'>" + (ds.Tables[4].Rows[j]["highestMark"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["highestMark"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td class='tdDesign'>" + (ds.Tables[4].Rows[j]["lowestMark"].ToString() == "" ? "" : double.Parse(ds.Tables[4].Rows[j]["lowestMark"].ToString()).ToString("0")) + "</td>");
                                        Response.Write("<td  class='th_tdDesign tdDesign' style='text-align:left !important;'>" + ds.Tables[4].Rows[j]["TeachersRemark"].ToString() + "</td></tr>");
                                    }
                                    Response.Write("<tr><td class='tdDesign2' colspan='11' style='border: 0 !important;'></td></tr>");
                                    Response.Write("<tr><td class='tdDesign2' colspan='11' style='border: 0 !important;'>");
                                    Response.Write("<tr><th class='tdDesign2 text-left' colspan='3'>TOTAL MARKS OBTAINED :</th><td class='tdDesign2 text-right'>" + double.Parse(ds.Tables[7].Rows[0]["obtMrk"].ToString()).ToString("0.00") + "</td><td class='tdDesign2' colspan='7' style='border: 0 !important;'></td></tr>");
                                    
                                    if (ds.Tables[7].Rows[0]["obtMrk"].ToString() != "")
                                    {
                                        avg = (double.Parse(ds.Tables[7].Rows[0]["obtMrk"].ToString())/ds.Tables[4].Rows.Count);
                                    }
                                    
                                    Response.Write("<tr><th class='tdDesign2 text-left' colspan='3'>Average score :</th><td class='tdDesign2 text-right'>" + avg.ToString("0.00") + "</td><td class='tdDesign2' colspan='7' style='border: 0 !important;'></td></tr>");
                                    Response.Write("<tr><td class='tdDesign2' colspan='11' style='height: 10px;border: 0 !important;'></td></tr>");
                                    Response.Write("</td></tr>");
                                }

                                Response.Write("<tr>");
                                Response.Write("<td class='tdDesign2' colspan='6' style='border-right: 0 !important;'>");
                                double Spiritofteamwork = 0, Willingnesstolearn = 0, Participationingame = 0, Verbalfluency = 0;
                                double Mentalalertness = 0, Politeness_or_respect = 0, Neatness = 0, Relationshipwithothers = 0;
                                if (ds.Tables[5].Rows.Count > 0)
                                {
                                    for (int m = 0; m < ds.Tables[5].Rows.Count; m++)
                                    {
                                        if (ds.Tables[5].Rows[m]["tradsType"].ToString() == "PSYCHOMOTIVE TRAITS")
                                        {
                                            Spiritofteamwork = double.Parse((ds.Tables[5].Rows[m]["Traits1"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits1"].ToString()));
                                            Willingnesstolearn = double.Parse((ds.Tables[5].Rows[m]["Traits2"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits2"].ToString()));
                                            Participationingame = double.Parse((ds.Tables[5].Rows[m]["Traits3"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits3"].ToString()));
                                            Verbalfluency = double.Parse((ds.Tables[5].Rows[m]["Traits4"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits4"].ToString()));
                                        }
                                        if (ds.Tables[5].Rows[m]["tradsType"].ToString() == "AFFECTIVE TRAITS")
                                        {
                                            Mentalalertness = double.Parse((ds.Tables[5].Rows[m]["Traits1"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits1"].ToString()));
                                            Politeness_or_respect = double.Parse((ds.Tables[5].Rows[m]["Traits2"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits2"].ToString()));
                                            Neatness = double.Parse((ds.Tables[5].Rows[m]["Traits3"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits3"].ToString()));
                                            Relationshipwithothers = double.Parse((ds.Tables[5].Rows[m]["Traits4"].ToString() == "" ? "0" : ds.Tables[5].Rows[m]["Traits4"].ToString()));
                                        }
                                    }
                                }

                                Response.Write("<table class='table mp-table p-table-bordered table-bordered text_left' style='margin: 0;'>");
                                Response.Write("<tr><th colspan='2' style='padding: 2px 10px;'>PSYCHOMOTIVE TRAITS</th></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Spirit of team work</td><td style='width:20%;'>" + (Spiritofteamwork == 0 ? "" : Spiritofteamwork.ToString("0")) + "</td></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Willingness to learn</td><td style='width:20%;'>" + (Willingnesstolearn == 0 ? "" : Willingnesstolearn.ToString("0")) + "</td></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Participation in game</td><td style='width:20%;'>" + (Participationingame == 0 ? "" : Participationingame.ToString("0")) + "</td></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Verbal fluency</td><td style='width:20%;'>" + (Verbalfluency == 0 ? "" : Verbalfluency.ToString("0")) + "</td></tr>");
                                Response.Write("</table>");

                                Response.Write("<table class='table mp-table p-table-bordered table-bordered text_left' style='margin: 0; margin-top:5px;'>");
                                Response.Write("<tr><th colspan='2' style='padding: 2px 10px;'>AFFECTIVE TRAITS</th></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Mental alertness</td><td style='width:20%;'>" + (Mentalalertness == 0 ? "" : Mentalalertness.ToString("0")) + "</td></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Politeness/respect</td><td style='width:20%;'>" + (Politeness_or_respect == 0 ? "" : Politeness_or_respect.ToString("0")) + "</td></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Neatness</td><td style='width:20%;'>" + (Neatness == 0 ? "" : Neatness.ToString("0")) + "</td></tr>");
                                Response.Write("<tr><td style='width:80%; padding: 2px 10px;'>Relationship with others</td><td style='width:20%;'>" + (Relationshipwithothers == 0 ? "" : Relationshipwithothers.ToString("0")) + "</td></tr>");
                                Response.Write("</table>");

                                Response.Write("</td>");
                                Response.Write("<td class='tdDesign2' colspan='2' style='border-right: 0 !important; border-left: 0 !important;'>");
                                Response.Write("</td>");
                                Response.Write("<td class='tdDesign2 text-center' colspan='3' style='border-left: 0 !important;'>");
                                Response.Write("<table class='scalTable'>");
                                Response.Write("<tr><th class='scal'>SCALE</th><th class='keys'>KEY</th></tr>");
                                Response.Write("<tr><td class='scal'>5-Excellent</td><td class='keys'>70 and above: Excellent</td></tr>");
                                Response.Write("<tr><td class='scal'>4-Good</td><td class='keys'>60-69: Good</td></tr>");
                                Response.Write("<tr><td class='scal'>3-Fair</td><td class='keys'>50-59: Average</td></tr>");
                                Response.Write("<tr><td class='scal'>2-Poor</td><td class='keys'>40-49: B/average</td></tr>");
                                Response.Write("<tr><td class='scal'>1-Very poor</td><td class='keys'>0-39: Fail</td></tr>");
                                Response.Write("<tr><td class='scal'></td><td class='keys'>F=Fail</td></tr>");
                                Response.Write("</table>");

                                Response.Write("</td>");
                                Response.Write("</tr> ");

                                Response.Write("<tr><td class='tdDesign2' colspan='11'>CLASS TEACHER'S COMMENT : &nbsp; <i>" + (ds.Tables[6].Rows.Count > 0 ? ds.Tables[6].Rows[0]["comment"].ToString() : "") + "</i></td></tr>");
                                //Response.Write("<tr><td class='tdDesign2' colspan='11'>PRINCIPAL'S COMMENT : &nbsp; <i>" + teacherRemarks(double.Parse(ds.Tables[7].Rows[0]["percents"].ToString() == "" ? "0" : ds.Tables[7].Rows[0]["percents"].ToString())) + "</i></td></tr>");
                                Response.Write("<tr><td class='tdDesign2' colspan='11'>PRINCIPAL'S COMMENT : &nbsp; <i>" + teacherRemarks(avg) + "</i></td></tr>");
                                Response.Write("<tr><td class='tdDesign2' colspan='6' style='height: 50px; text-align: left !important; vertical-align:bottom; padding-left:100px !important;'><img src='../../img/teacherSign.jpg' style='height:50px;'></td><td class='tdDesign2' colspan='6' style='height: 50px; text-align: right !important; vertical-align:bottom; padding-right:100px !important;'><img src='../../img/PrincipalSign.jpg' style='height:50px;'></td></tr>");

                                Response.Write("</tbody></table>");

                                Response.Write("</td>");
                                Response.Write("</tr>");
                                Response.Write("</tbody></table>");
                                Response.Write("</div></div></div>");
                            }
                        }
                        catch (SqlException ex)
                        { throw ex; }
                    }
                }
            }
        }
    }

    public string teacherRemarks(double totalPercent)
    {
        if (totalPercent >= 70) { return "An excellent performance."; }
        else if (totalPercent >= 60) { return "Good performance."; }
        else if (totalPercent >= 50) { return "An average performance"; }
        else if (totalPercent >= 40) { return "Below average performance"; }
        else if (totalPercent < 40)  { return "Very poor performance."; }
        else { return ""; }
    }
}
