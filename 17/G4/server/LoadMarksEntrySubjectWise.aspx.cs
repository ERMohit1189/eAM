using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _17_G4_server_LoadMarksEntrySubjectWise : System.Web.UI.Page
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

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                sql = "select SubjectType from TTSubjectMaster where SessionName='" + Session + "' and ClassId=" + ClassId + "  and id=" + SubjectId + " and SectionName='" + SectionName + "' and BranchCode=" + BranchCode + "";
                bool isoptional = (oo.ReturnTag(sql, "SubjectType").ToLower() == "optional" ? true : false);
                sql = "select   sg.srno, Name, FatherName, sg.BranchId Branch from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") SG ";
                sql += " where  sg.classid=" + ClassId.ToString() + "";
                sql += " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
                sql += " sg.BranchCode='" + BranchCode.ToString() + "'";
                if (isoptional)
                {
                    sql += " and So.srno in (select Srno from TTSubjectMaster sm inner join ICSEOptionalSubjectAllotment opt on opt.OptSubjectId=sm.id and sm.SessionName=opt.SessionName and sm.BranchCode=opt.BranchCode where sm.SessionName='" + Session + "'  and sm.BranchCode=" + BranchCode + " and sm.id=" + SubjectId + ")";
                }
                sql += " and sg.Withdrwal is null and isnull(Promotion,'')<>'Cancelled' order by FirstName Asc";
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
                        string MaxMarks1 = "", MaxMarks2 = "", MaxMarks4 = "", MaxMarks5 = "";
                        double MaxMarks1M = 0, MaxMarks2M = 0, MaxMarks4M = 0, MaxMarks5M = 0;
                        string sql1 = "Select MaxMarks1,MaxMarks2,MaxMarks4,MaxMarks5 from SGSMaxMarkEntryJunior where ClassId=" + ClassId + " and SectionName='" + SectionName + "' and Evaluation='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + " and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "'";

                        MaxMarks1 = (oo.ReturnTag(sql1, "MaxMarks1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks1").ToString());
                        MaxMarks2 = (oo.ReturnTag(sql1, "MaxMarks2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks2").ToString());
                        MaxMarks4 = (oo.ReturnTag(sql1, "MaxMarks4").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks4").ToString());
                        MaxMarks5 = (oo.ReturnTag(sql1, "MaxMarks5").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks5").ToString());

                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-48 text-left' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Father's Name</th>");
                        if (Evail.ToLower() == "term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. I </span><br><input name='' type='text' value='" + MaxMarks1.ToString() + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. II </span><br><input name='' type='text' value='" + MaxMarks2.ToString() + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                        }
                        if (Evail.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. III </span><br><input name='' type='text' value='" + MaxMarks1.ToString() + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U.T. IV </span><br><input name='' type='text' value='" + MaxMarks2.ToString() + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                        }
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Average</th>");
                        if (Evail.ToLower() == "term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col' colspan='2'  style='padding:0 !important;padding-top:5px !important;'><span> H.Y.E.</span>(80)<br>");
                        }
                        if (Evail.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col' colspan='2'  style='padding:0 !important;padding-top:5px !important;'><span> A.E.</span>(80)<br>");
                        }
                        Response.Write("<table class='table table-bordered' style='width:100%; padding:0 !important; margin:0 !important; margin-top:5px !important;'><tr><th style='width:50% !important'><span id=''>Theory</span><br><input name='' type='text' value='" + (MaxMarks4 == "" ? "0" : MaxMarks4).ToString() + "' id='MaxMarks4' class='form-control-blue text-center' style='width:40px;'></th><th style='width:50% !important'><span id=''>Pra./Or./Proj.</span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "0" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th></tr></table></th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Grade</th>");
                        Response.Write("</tr>");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Test1 = "", Test2 = "", TH = "", id = "", Prac = "";
                            double Test1M = 0, Test2M = 0, THM = 0, PracM = 0;
                            string sql3 = "Select Id,Test1,Test2,TH,Prac from SGSMarkEntryJunior where  ClassId=" + ClassId + " and SectionName='" + SectionName + "' and SRNO='" + dt.Rows[i]["srno"].ToString() + "' and Evaluation='" + Evail.ToString().Trim() + "' and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode + "";
                            id = oo.ReturnTag(sql3, "Id");
                            Test1 = oo.ReturnTag(sql3, "Test1");
                            Test2 = oo.ReturnTag(sql3, "Test2");

                            TH = oo.ReturnTag(sql3, "TH");
                            Prac = oo.ReturnTag(sql3, "Prac");

                            if (Test1.ToString().Trim().ToUpper() == "NP" || Test1.ToString().Trim().ToUpper() == "NAD" || Test1.ToString().Trim().ToUpper() == "ML" || Test1.ToString().Trim().ToUpper() == "") { }
                            else { Test1M = (double.Parse(Test1.ToString().Trim())); }
                            if (Test2.ToString().Trim().ToUpper() == "NP" || Test2.ToString().Trim().ToUpper() == "NAD" || Test2.ToString().Trim().ToUpper() == "ML" || Test2.ToString().Trim().ToUpper() == "") { }
                            else { Test2M = (double.Parse(Test2.ToString().Trim())); }
                            if (TH.ToString().Trim().ToUpper() == "NP" || TH.ToString().Trim().ToUpper() == "NAD" || TH.ToString().Trim().ToUpper() == "ML" || TH.ToString().Trim().ToUpper() == "") { }
                            else { THM = double.Parse(TH.ToString().Trim()); }
                            if (Prac.ToString().Trim().ToUpper() == "NP" || Prac.ToString().Trim().ToUpper() == "NAD" || Prac.ToString().Trim().ToUpper() == "ML" || Prac.ToString().Trim().ToUpper() == "") { }
                            else { PracM = double.Parse(Prac.ToString().Trim()); }

                            if (MaxMarks1.ToString().Trim().ToUpper() == "NP" || MaxMarks1.ToString().Trim().ToUpper() == "NAD" || MaxMarks1.ToString().Trim().ToUpper() == "ML" || MaxMarks1.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks1M = double.Parse(MaxMarks1.ToString().Trim()); }
                            if (MaxMarks2.ToString().Trim().ToUpper() == "NP" || MaxMarks2.ToString().Trim().ToUpper() == "NAD" || MaxMarks2.ToString().Trim().ToUpper() == "ML" || MaxMarks2.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks2M = double.Parse(MaxMarks2.ToString().Trim()); }

                            if (MaxMarks4.ToString().Trim().ToUpper() == "NP" || MaxMarks4.ToString().Trim().ToUpper() == "NAD" || MaxMarks4.ToString().Trim().ToUpper() == "ML" || MaxMarks4.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks4M = double.Parse(MaxMarks4.ToString().Trim()); }
                            if (MaxMarks5.ToString().Trim().ToUpper() == "NP" || MaxMarks5.ToString().Trim().ToUpper() == "NAD" || MaxMarks5.ToString().Trim().ToUpper() == "ML" || MaxMarks5.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks5M = double.Parse(MaxMarks5.ToString().Trim()); }

                            string ptm1 = ""; string ObtMarks1 = ""; string Grade1 = "";

                            double avg = (Test1M + Test2M) / 2;
                            ptm1 = (avg).ToString("0.00");

                            ObtMarks1 = (double.Parse(ptm1) + THM + PracM).ToString("0");
                            Grade1 = Grade(double.Parse(ObtMarks1));

                            Response.Write("<tr id=" + id + ">");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + ptm1 + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + TH.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Prac.ToString() + "' class='form-control-blue text-center' name='7' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : Grade1.ToString()) + "</span></td>");
                            Response.Write("</tr>");
                        }
                        Response.Write("</tbody></table>");
                      //  Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit' /></div>");

                    }
                    catch (SqlException ex)
                    { throw ex; }
                }
            }
        }
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