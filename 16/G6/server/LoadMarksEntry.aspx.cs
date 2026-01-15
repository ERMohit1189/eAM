using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class LoadMarksEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string sql = "";
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string Evail = Request.Form["Evail"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Session = Request.Form["Session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string StreamId = Request.Form["StreamId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string Medium = Request.Form["Medium"].ToString().Trim();
        string Status = Request.Form["Status"].ToString().Trim();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                sql = "select SubjectType from TTSubjectMaster where ClassId=" + ClassId + " and BranchId=" + BranchId + "  and id=" + SubjectId + " and Medium='" + Medium + "' and SessionName='" + Session + "' and BranchCode=" + BranchCode + "";
                bool isoptional = (oo.ReturnTag(sql, "SubjectType").ToLower() == "optional" ? true : false);
                sql = "select   sg.srno, Name, FatherName, sg.BranchId Branch from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") SG ";
                sql +=  " where  sg.classid=" + ClassId.ToString() + " and Medium='" + Medium + "'";
                sql +=  " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
                sql +=  " sg.BranchCode='" + BranchCode.ToString() + "' and sg.BranchId=" + BranchId + " and sg.StreamId =" + StreamId.ToString().Trim() + " "; 
                if (isoptional)
                {
                    sql +=  " and sg.srno in (select Srno from ICSEOptionalSubjectAllotment where SessionName='" + Session + "'  and BranchCode=" + BranchCode + " and OptSubjectId=" + SubjectId + ")";
                }
                if (Status.ToString() != "0")
                {
                    sql += "  and isnull(Withdrwal,'') = case when isnull('" + Status.ToString() + "','')='B' or isnull('" + Status.ToString() + "','')='' then isnull(Withdrwal,'') else case when isnull('" + Status.ToString() + "','')='A' then '' else 'W' end end and isnull(blocked,'') = case when isnull('" + Status.ToString() + "','')= 'W' or isnull('" + Status.ToString() + "','')= '' then isnull(blocked,'') else case when isnull('" + Status.ToString() + "','')= 'A' then '' else 'yes' end end";
                }
                sql +=  " and isnull(Promotion,'')<>'Cancelled' order by FirstName Asc";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    try
                    {

                        string UT1m = "", UT2m = "", HYTHm = "", HYPracm = "", UT3m = "", UT4m = "", UT5m="",UT6m="", AETHm = "", AEPracm = "";
                        string sql1 = "Select UT1, UT2, HYTH, HYPrac,UT3, UT4,UT6,UT5, AETH, AEPrac from UPEMXI ";
                        sql1 = sql1 + " where ClassId =" + ClassId.ToString().Trim() + " and BranchId =" + BranchId.ToString().Trim() + "  and StreamId =" + StreamId.ToString().Trim() + " and SectionId =" + SectionId.ToString().Trim() + " ";
                        sql1 = sql1 + " and BranchCode=" + BranchCode.ToString() + " and SessionName='" + Session.ToString() + "' and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and Medium='" + Medium + "'";

                        if (Evail.ToLower() == "term1")
                        {
                            UT1m = (oo.ReturnTag(sql1, "UT1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT1").ToString());
                            UT2m = (oo.ReturnTag(sql1, "UT2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT2").ToString());
                            UT3m = (oo.ReturnTag(sql1, "UT3").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT3").ToString());
                            HYTHm = (oo.ReturnTag(sql1, "HYTH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "HYTH").ToString());
                            HYPracm = (oo.ReturnTag(sql1, "HYPrac").ToString() == "" ? "0" : oo.ReturnTag(sql1, "HYPrac").ToString());
                        }
                        else
                        {
                            UT4m = (oo.ReturnTag(sql1, "UT4").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT4").ToString());
                            UT5m = (oo.ReturnTag(sql1, "UT5").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT5").ToString());
                            UT6m = (oo.ReturnTag(sql1, "UT6").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT6").ToString());
                            AETHm = (oo.ReturnTag(sql1, "AETH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "AETH").ToString());
                            AEPracm = (oo.ReturnTag(sql1, "AEPrac").ToString() == "" ? "0" : oo.ReturnTag(sql1, "AEPrac").ToString());
                        }

                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' rowspan='2' style='width:3%;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n text-left' rowspan='2' style='width:7%;' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n text-left' rowspan='2' style='width:15%;' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n text-left' rowspan='2'style='width:15%;' scope='col'>Father's Name</th>");
                        if (Evail.ToLower() == "term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:8%;' rowspan='2'><span id=''> UT1 </span><br><input name='' type='text' value='" + UT1m.ToString() + "' id='UT1m' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:8%;' rowspan='2'><span id=''> UT2 </span><br><input name='' type='text' value='" + UT2m.ToString() + "' id='UT2m' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:8%;' rowspan='2'><span id=''> UT3 </span><br><input name='' type='text' value='" + UT3m.ToString() + "' id='UT3m' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='2' style='width:24%;' scope='col'><span id=''> HYE (100)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:10%;' rowspan='2'>TOTAL (150)</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:10%;' rowspan='2'>GRADE</th>");

                            Response.Write("</tr>");
                            Response.Write("<tr>");
                            
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + HYTHm.ToString() + "' id='HYTHm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id='' style='white-space: nowrap;'> PRAC. </span><br><input name='' type='text' value='" + HYPracm.ToString() + "' id='HYPracm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("</tr>");
                        }
                        if (Evail.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:8%;' rowspan='2'><span id=''> UT4 </span><br><input name='' type='text' value='" + UT4m.ToString() + "' id='UT1m' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:8%;' rowspan='2'><span id=''> UT5 </span><br><input name='' type='text' value='" + UT5m.ToString() + "' id='UT2m' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:8%;' rowspan='2'><span id=''> UT6 </span><br><input name='' type='text' value='" + UT6m.ToString() + "' id='UT3m' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='2' style='width:24%;' scope='col'><span id=''> AE (100)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:10%;' rowspan='2'>TOTAL (150)</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:10%;' rowspan='2'>GRADE</th>");

                            Response.Write("</tr>");
                            Response.Write("<tr>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + AETHm.ToString() + "' id='HYTHm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id='' style='white-space: nowrap;'> PRAC. </span><br><input name='' type='text' value='" + AEPracm.ToString() + "' id='HYASSIm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("</tr>");
                        }


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string UT1 = "", UT2 = "", UT5 = "", UT6 = "", HYTH = "", HYPrac = "", HYTotal = "", HYGrade = "", UT3 = "", UT4 = "", AETH = "", AEPrac = "", AETotal = "", AEGrade = "";
                            string sql3 = "Select Id,UT1, UT2, HYTH, HYPrac, HYTotal, HYGrade, UT3,  UT4,UT5,UT6, AETH, AEPrac, AETotal, AEGrade from UPEXI where SRNO='" + dt.Rows[i]["srno"].ToString() + "'  and SubjectId='" + SubjectId.ToString().Trim() + "' and ClassId =" + ClassId.ToString().Trim() + " and BranchId =" + BranchId.ToString().Trim() + "  and StreamId =" + StreamId.ToString().Trim() + " and SectionId =" + SectionId.ToString().Trim() + " ";
                            sql3 = sql3 + " and BranchCode=" + BranchCode.ToString() + " and SessionName='" + Session.ToString() + "' and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and Medium='" + Medium + "'";
                            string id = oo.ReturnTag(sql3, "Id");

                            if (Evail.ToLower() == "term1")
                            {
                                UT1 = oo.ReturnTag(sql3, "UT1");
                                UT2 = oo.ReturnTag(sql3, "UT2");
                                UT3 = oo.ReturnTag(sql3, "UT3");
                                HYTH = oo.ReturnTag(sql3, "HYTH");
                                HYPrac = oo.ReturnTag(sql3, "HYPrac");
                                HYTotal = oo.ReturnTag(sql3, "HYTotal");
                                HYGrade = oo.ReturnTag(sql3, "HYGrade");

                                Response.Write("<tr id=" + id + ">");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:15%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:15%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" +UT1.Trim() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT2.Trim() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT3.Trim() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + HYTH.Trim() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + HYPrac.Trim() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span>" + HYTotal.Trim() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span>" + HYGrade.Trim() + "</span></td>");
                                Response.Write("</tr>");
                            }
                            if (Evail.ToLower() == "term2")
                            {
                                UT4 = oo.ReturnTag(sql3, "UT4");
                                UT5 = oo.ReturnTag(sql3, "UT5");
                                UT6 = oo.ReturnTag(sql3, "UT6");
                                AETH = oo.ReturnTag(sql3, "AETH");
                                AEPrac = oo.ReturnTag(sql3, "AEPrac");
                                AETotal = oo.ReturnTag(sql3, "AETotal");
                                AEGrade = oo.ReturnTag(sql3, "AEGrade");

                                Response.Write("<tr id=" + id + ">");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:15%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:15%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT4.Trim() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT5.Trim() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT6.Trim() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + AETH.Trim() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + AEPrac.Trim() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span>" + AETotal.Trim() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span>" + AEGrade.Trim() + "</span></td>");
                                Response.Write("</tr>");
                            }
                        }
                        Response.Write("</tbody></table>");
                        Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit' /></div>");

                    }
                    catch (SqlException ex)
                    { throw ex; }
                }
            }
        }
    }

}