using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _16_G2_server_LoadMarksEntryServer : System.Web.UI.Page
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
                sql += " where  sg.classid=" + ClassId.ToString() + " and Medium='" + Medium + "'";
                sql += " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
                sql += " sg.BranchCode='" + BranchCode.ToString() + "'";
                if (isoptional)
                {
                    sql += " and sg.srno in (select Srno from ICSEOptionalSubjectAllotment where SessionName='" + Session + "'  and BranchCode=" + BranchCode + " and OptSubjectId=" + SubjectId + ")";
                }
                if (Status.ToString() != "0")
                {
                    sql += "  and isnull(Withdrwal,'') = case when isnull('" + Status.ToString() + "','')='B' or isnull('" + Status.ToString() + "','')='' then isnull(Withdrwal,'') else case when isnull('" + Status.ToString() + "','')='A' then '' else 'W' end end and isnull(blocked,'') = case when isnull('" + Status.ToString() + "','')= 'W' or isnull('" + Status.ToString() + "','')= '' then isnull(blocked,'') else case when isnull('" + Status.ToString() + "','')= 'A' then '' else 'yes' end end";
                }
                sql += "  and isnull(Promotion,'')<>'Cancelled' order by FirstName Asc";
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

                        string UT1THm = "", UT1Vivam = "", UT2THm = "", UT2Vivam = "", HYTHm = "", HYASSIm = "", HYVivam = "", UT3THm = "", UT3Vivam = "", UT4THm = "", UT4Vivam = "", AETHm = "", AEASSIm = "", AEVivam = "";
                        string sql1 = "Select UT1TH, UT1Viva, UT2TH, UT2Viva, HYTH, HYASSI, HYViva,UT3TH, UT3Viva, UT4TH, UT4Viva, AETH, AEASSI, AEViva from PrePrimaryMaxMarksEntry ";
                        sql1 = sql1 + " where ClassId =" + ClassId.ToString().Trim() + " and BranchId =" + BranchId.ToString().Trim() + " and SectionId =" + SectionId.ToString().Trim() + " ";
                        sql1 = sql1 + " and BranchCode=" + BranchCode.ToString() + " and SessionName='" + Session.ToString() + "' and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and Medium='" + Medium + "'";

                        if (Evail.ToLower() == "term1")
                        {
                            UT1THm = (oo.ReturnTag(sql1, "UT1TH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT1TH").ToString());
                            UT1Vivam = (oo.ReturnTag(sql1, "UT1Viva").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT1Viva").ToString());
                            UT2THm = (oo.ReturnTag(sql1, "UT2TH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT2TH").ToString());
                            UT2Vivam = (oo.ReturnTag(sql1, "UT2Viva").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT2Viva").ToString());
                            HYTHm = (oo.ReturnTag(sql1, "HYTH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "HYTH").ToString());
                            HYASSIm = (oo.ReturnTag(sql1, "HYASSI").ToString() == "" ? "0" : oo.ReturnTag(sql1, "HYASSI").ToString());
                            HYVivam = (oo.ReturnTag(sql1, "HYViva").ToString() == "" ? "0" : oo.ReturnTag(sql1, "HYViva").ToString());
                        }
                        else
                        {
                            UT3THm = (oo.ReturnTag(sql1, "UT3TH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT3TH").ToString());
                            UT3Vivam = (oo.ReturnTag(sql1, "UT3Viva").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT3Viva").ToString());
                            UT4THm = (oo.ReturnTag(sql1, "UT4TH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT4TH").ToString());
                            UT4Vivam = (oo.ReturnTag(sql1, "UT4Viva").ToString() == "" ? "0" : oo.ReturnTag(sql1, "UT4Viva").ToString());
                            AETHm = (oo.ReturnTag(sql1, "AETH").ToString() == "" ? "0" : oo.ReturnTag(sql1, "AETH").ToString());
                            AEASSIm = (oo.ReturnTag(sql1, "AEASSI").ToString() == "" ? "0" : oo.ReturnTag(sql1, "AEASSI").ToString());
                            AEVivam = (oo.ReturnTag(sql1, "AEViva").ToString() == "" ? "0" : oo.ReturnTag(sql1, "AEViva").ToString());
                        }

                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' rowspan='2' style='width:3%;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n text-left' rowspan='2' style='width:7%;' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n text-left' rowspan='2' style='width:17%;' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n text-left' rowspan='2'style='width:17%;' scope='col'>Father's Name</th>");
                        if (Evail.ToLower() == "term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='2' style='width:13%;' scope='col'><span id=''> UT-1 (25)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='2' style='width:13%;' scope='col'><span id=''> UT-2 (25)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='3' style='width:16%;' scope='col'><span id=''> HY (100)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:7%;' rowspan='2'>TOTAL (150)</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:7%;' rowspan='2'>GRADE</th>");

                            Response.Write("</tr>");
                            Response.Write("<tr>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + UT1THm.ToString() + "' id='UT1THm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> VIVA </span><br><input name='' type='text' value='" + UT1Vivam.ToString() + "' id='UT1Vivam' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + UT2THm.ToString() + "' id='UT2THm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> VIVA </span><br><input name='' type='text' value='" + UT2Vivam.ToString() + "' id='UT2THm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + HYTHm.ToString() + "' id='HYTHm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id='' style='white-space: nowrap;'> ASSI/ PROJ </span><br><input name='' type='text' value='" + HYASSIm.ToString() + "' id='HYASSIm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id='' style='white-space: nowrap;'> VIVA/ DICT </span><br><input name='' type='text' value='" + HYVivam.ToString() + "' id='HYVivam' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("</tr>");
                        }
                        if (Evail.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='2' style='width:13%;' scope='col'><span id=''> UT-3 (25)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='2' style='width:13%;' scope='col'><span id=''> UT-4 (25)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' colspan='3' style='width:16%;' scope='col'><span id=''> AE (100)</span></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:7%;' rowspan='2'>TOTAL (150)</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:7%;' rowspan='2'>GRADE</th>");

                            Response.Write("</tr>");
                            Response.Write("<tr>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + UT3THm.ToString() + "' id='UT1THm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> VIVA </span><br><input name='' type='text' value='" + UT3Vivam.ToString() + "' id='UT1Vivam' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + UT4THm.ToString() + "' id='UT2THm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> VIVA </span><br><input name='' type='text' value='" + UT4Vivam.ToString() + "' id='UT2THm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id=''> TH </span><br><input name='' type='text' value='" + AETHm.ToString() + "' id='HYTHm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id='' style='white-space: nowrap;'> ASSI/ PROJ </span><br><input name='' type='text' value='" + AEASSIm.ToString() + "' id='HYASSIm' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span id='' style='white-space: nowrap;'> VIVA/ DICT </span><br><input name='' type='text' value='" + AEVivam.ToString() + "' id='HYVivam' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("</tr>");
                        }


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string UT1TH = "", UT1Viva = "", UT2TH = "", UT2Viva = "", HYTH = "", HYASSI = "", HYViva = "", HYTotal = "", HYGrade = "", UT3TH = "", UT3Viva = "", UT4TH = "", UT4Viva = "", AETH = "", AEASSI = "", AEViva = "", AETotal = "", AEGrade = "", GTotal = "", GGrade = "";
                            string sql3 = "Select Id,UT1TH, UT1Viva, UT2TH, UT2Viva, HYTH, HYASSI, HYViva, HYTotal, HYGrade, UT3TH, UT3Viva, UT4TH, UT4Viva, AETH, AEASSI, AEViva, AETotal, AEGrade,GTotal,GGrade from PrePrimaryMarksEntry where SRNO='" + dt.Rows[i]["srno"].ToString() + "'  and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and Medium='" + Medium + "' and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode + "";
                            string id = oo.ReturnTag(sql3, "Id");

                            UT3TH = oo.ReturnTag(sql3, "UT3TH");
                            UT3Viva = oo.ReturnTag(sql3, "UT3Viva");
                            UT4TH = oo.ReturnTag(sql3, "UT4TH");
                            UT4Viva = oo.ReturnTag(sql3, "UT4Viva");
                            AETH = oo.ReturnTag(sql3, "AETH");
                            AEASSI = oo.ReturnTag(sql3, "AEASSI");
                            AEViva = oo.ReturnTag(sql3, "AEViva");
                            AETotal = oo.ReturnTag(sql3, "AETotal");
                            AEGrade = oo.ReturnTag(sql3, "AEGrade");
                            GTotal = oo.ReturnTag(sql3, "GTotal");
                            GGrade = oo.ReturnTag(sql3, "GGrade");
                            if (Evail.ToLower() == "term1")
                            {
                                UT1TH = oo.ReturnTag(sql3, "UT1TH");
                                UT1Viva = oo.ReturnTag(sql3, "UT1Viva");
                                UT2TH = oo.ReturnTag(sql3, "UT2TH");
                                UT2Viva = oo.ReturnTag(sql3, "UT2Viva");
                                HYTH = oo.ReturnTag(sql3, "HYTH");
                                HYASSI = oo.ReturnTag(sql3, "HYASSI");
                                HYViva = oo.ReturnTag(sql3, "HYViva");
                                HYTotal = oo.ReturnTag(sql3, "HYTotal");
                                HYGrade = oo.ReturnTag(sql3, "HYGrade");

                                Response.Write("<tr id=" + id + ">");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT1TH.Trim() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT1Viva.Trim() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT2TH.Trim() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT2Viva.Trim() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + HYTH.Trim() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + HYASSI.Trim() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + HYViva.Trim() + "' class='form-control-blue text-center' name='7' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span id='' name='8'>" + HYTotal.Trim() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span id='' name='9'>" + HYGrade.Trim() + "</span></td>");
                                Response.Write("</tr>");
                            }
                            if (Evail.ToLower() == "term2")
                            {
                                UT3TH = oo.ReturnTag(sql3, "UT3TH");
                                UT3Viva = oo.ReturnTag(sql3, "UT3Viva");
                                UT4TH = oo.ReturnTag(sql3, "UT4TH");
                                UT4Viva = oo.ReturnTag(sql3, "UT4Viva");
                                AETH = oo.ReturnTag(sql3, "AETH");
                                AEASSI = oo.ReturnTag(sql3, "AEASSI");
                                AEViva = oo.ReturnTag(sql3, "AEViva");
                                AETotal = oo.ReturnTag(sql3, "AETotal");
                                AEGrade = oo.ReturnTag(sql3, "AEGrade");

                                Response.Write("<tr id=" + id + ">");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT3TH.Trim() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT3Viva.Trim() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT4TH.Trim() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + UT4Viva.Trim() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + AETH.Trim() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + AEASSI.Trim() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + AEViva.Trim() + "' class='form-control-blue text-center' name='7' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span id='' name='8'>" + AETotal.Trim() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span id='' name='9'>" + AEGrade.Trim() + "</span></td>");
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