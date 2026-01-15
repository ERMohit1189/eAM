using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

public partial class common_G2_MarkEntryNurtoPrepEmpty_1718Server : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string sql = "";
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string Evail = Request.Form["Evail"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Session = Request.Form["Session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string ActivityId = Request.Form["ActivityId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                sql = "select SubjectType from TTSubjectMaster where SessionName='" + Session + "' and ClassId=" + ClassId + "  and id=" + SubjectId + " and SectionName='" + SectionName + "' and BranchCode=" + BranchCode + "";
                bool isoptional = (oo.ReturnTag(sql, "SubjectType").ToLower() == "optional" ? true : false);
                sql = "select   sg.srno, Name, FatherName, sg.BranchId Branch from AllStudentRecord_UDF('"+ Session.ToString() + "',"+ BranchCode.ToString() + ") SG ";
                sql +=  " where  sg.classid=" + ClassId.ToString() + "";
                sql +=  " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
                sql +=  " sg.BranchCode='" + BranchCode.ToString() + "'";
                if (isoptional)
                {
                    sql +=  " and sg.srno in (select Srno from TTSubjectMaster sm inner join ICSEOptionalSubjectAllotment opt on opt.OptSubjectId=sm.id and sm.SessionName=opt.SessionName and sm.BranchCode=opt.BranchCode where sm.SessionName='" + Session + "'  and sm.BranchCode="+BranchCode +" and sm.id=" + SubjectId + ")";
                }
                sql +=  " and sg.Withdrwal is null and isnull(sg.Promotion,'')<>'Cancelled' order by FirstName Asc";
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
                        
                        string MaxMarks1 = "", MaxMarks2 = "", MaxMarks3 = "";
                        string sql1 = "Select MaxMarks1,MaxMarks2,MaxMarks3 from SetMaxMinMarks_NurtoPrep_Grade where Evaluation='" + Evail.ToString().Trim() + "' and ClassId=" + ClassId + " and BranchCode=" + BranchCode.ToString() + " and SubjectId=" + SubjectId.ToString().Trim() + "  and PaperId=" + PaperId.ToString().Trim() + " and ActivityId=" + ActivityId.ToString().Trim() + " and SessionName='" + Session.ToString() + "'";
                        MaxMarks1 = (oo.ReturnTag(sql1, "MaxMarks1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks1").ToString());
                        MaxMarks2 = (oo.ReturnTag(sql1, "MaxMarks2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks2").ToString());
                        MaxMarks3 = (oo.ReturnTag(sql1, "MaxMarks3").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks3").ToString());
                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-48 text-left' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Father's Name</th>");
                        if (Evail.ToLower()=="term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> EVAL. 1 </span><br><input name='' type='text' value='" + MaxMarks1 + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> EVAL. 2 </span><br><input name='' type='text' value='" + MaxMarks2 + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> EVAL. 3 </span><br><input name='' type='text' value='" + MaxMarks3 + "' id='MaxMarks3' class='form-control-blue text-center' style='width:40px;'></th>");
                        }
                        if (Evail.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> EVAL. 4 </span><br><input name='' type='text' value='" + MaxMarks1 + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> EVAL. 5 </span><br><input name='' type='text' value='" + MaxMarks2 + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> EVAL. 6 </span><br><input name='' type='text' value='" + MaxMarks3 + "' id='MaxMarks3' class='form-control-blue text-center' style='width:40px;'></th>");
                        }
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>TOTAL OF  BEST TWO</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>CONV. INTO 100</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");
                        Response.Write("</tr>");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Evel1 = "", Evel2 = "", Evel3= "", Best2="", Conversion="", Grade="", id = "";
                            string sql3 = "Select Id,Evel1,Evel2,Evel3,Best2,Conversion, Grade from CCENurtoPrep_Grade where SRNO='" + dt.Rows[i]["srno"].ToString() + "' and ClassId=" + ClassId + " and Evaluation='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + "  and SubjectId=" + SubjectId.ToString().Trim() + "  and PaperId=" + PaperId.ToString().Trim() + " and ActivityId=" + ActivityId.ToString().Trim() + " and SessionName='" + Session.ToString() + "' and SectionId='" + SectionId.ToString() + "' and BranchCode=" + BranchCode + "";
                            id = oo.ReturnTag(sql3, "Id");
                            Evel1 = oo.ReturnTag(sql3, "Evel1");
                            Evel2 = oo.ReturnTag(sql3, "Evel2");
                            Evel3 = oo.ReturnTag(sql3, "Evel3");
                            Best2 = oo.ReturnTag(sql3, "Best2");
                            Conversion = oo.ReturnTag(sql3, "Conversion");
                            Grade = oo.ReturnTag(sql3, "Grade");

                            Response.Write("<tr id=" + id + ">");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='width:5%;'><span id=''>" + (i + 1) + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='width:10%;'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in' style='text-align:center !important; width:7%;'><input type='text' value='" + Evel1 + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in' style='text-align:center !important; width:7%;'><input type='text' value='" + Evel2 + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in' style='text-align:center !important; width:7%;'><input type='text' value='" + Evel3 + "' class='form-control-blue text-centern' name='3' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in' style='text-align:center !important; width:8%;'><span>" + (Best2== ""?"0": double.Parse(Best2).ToString("0.0")) + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in' style='text-align:center !important; width:8%;'><span>" + (Conversion == "" ? "0" : double.Parse(Conversion).ToString("0.0"))+ "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in' style='text-align:center !important; width:8%;'><span>" + Grade + "</span></td>");
                            Response.Write("</tr>");
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