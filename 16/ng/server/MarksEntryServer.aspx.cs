using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class MarksEntryServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string TermId = Request.Form["TermId"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
         {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "master_NG_MarksEntry";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString());
                cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString());
                cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString());
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId.ToString());
                cmd.Parameters.AddWithValue("@TermId", TermId.ToString());
                cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                cmd.Parameters.AddWithValue("@action", "select_markentry");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();

                try
                {
                    double Max_conutive = 0, Max_affective = 0, Max_phychomotor = 0, Max_exam = 0;
                    double maxTotalCa = 0, maxTotal = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Max_conutive = double.Parse((ds.Tables[0].Rows[0]["Max_conutive"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["Max_conutive"].ToString()));
                        Max_affective = double.Parse((ds.Tables[0].Rows[0]["Max_affective"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["Max_affective"].ToString()));
                        Max_phychomotor = double.Parse((ds.Tables[0].Rows[0]["Max_phychomotor"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["Max_phychomotor"].ToString()));
                        maxTotalCa = Max_conutive + Max_affective + Max_phychomotor;
                        Max_exam = double.Parse((ds.Tables[0].Rows[0]["Max_exam"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["Max_exam"].ToString()));
                        maxTotal = maxTotalCa + Max_exam;
                    }

                    Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                    Response.Write("<tr>");
                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n sub-m-w-48' scope='col'>S.R. No.</th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-w-175' scope='col'>Student's Name</th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-w-175' scope='col'>Father's Name</th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span> Cognitive 40% </span><br><input name='1' type='text' value='" +(Max_conutive.ToString()=="0"?"": Max_conutive.ToString("0.0")) + "' id='txtMaxCongutive' class='form-control-blue text-center' style='width:40px;'></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span> Affective 10% </span><br><input name='2' type='text' value='" + (Max_affective.ToString()=="0"?"": Max_affective.ToString("0.0")) + "' id='txtMaxAffective' class='form-control-blue text-center' style='width:40px;'></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span> Psychomotor 10% </span><br><input name='3' type='text' value='" + (Max_phychomotor.ToString()=="0"?"": Max_phychomotor.ToString("0.0")) + "' id='txtMaxPhychomotor' class='form-control-blue text-center' style='width:40px;'></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span> Total CA 60% </span><br><span id='lblMaxTotal' style='width:40px;'>" + (maxTotalCa.ToString()=="0"?"": maxTotalCa.ToString("0.0")) + "</span></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span> Exam 40%</span><br><input name='4' type='text' value='" + (Max_exam.ToString()=="0"?"": Max_exam.ToString("0.0")) + "' id='txtMaxExam' class='form-control-blue text-center' style='width:40px;'></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span> Mark Obtained 100% </span><br><span id='lblMaxMarkObtained' style='width:40px;'>" + (maxTotal.ToString()=="0"?"": maxTotal.ToString("0.0")) + "</span></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id='lblgrade' style='width:40px;'>Grade</span></th>");
                    Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id='lblTeacherRemarks' style='width:40px;'>Teacher's Remark</span></th>");
                    Response.Write("</tr>");
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            double conutive = 0, affective = 0, phychomotor = 0, exam=0; string exams = "0";
                            double MarkTotalCa = 0, MarkTotal = 0;
                            conutive = double.Parse((ds.Tables[1].Rows[i]["conutive"].ToString() == "" ? "0" : ds.Tables[1].Rows[i]["conutive"].ToString()));
                            affective = double.Parse((ds.Tables[1].Rows[i]["affective"].ToString() == "" ? "0" : ds.Tables[1].Rows[i]["affective"].ToString()));
                            phychomotor = double.Parse((ds.Tables[1].Rows[i]["phychomotor"].ToString() == "" ? "0" : ds.Tables[1].Rows[i]["phychomotor"].ToString()));
                            exam = double.Parse((ds.Tables[1].Rows[i]["exam"].ToString() == "" ? "0" : ds.Tables[1].Rows[i]["exam"].ToString()));
                            exams = double.Parse((ds.Tables[1].Rows[i]["exam"].ToString() == "" ? "0" : ds.Tables[1].Rows[i]["exam"].ToString())).ToString("0");
                            MarkTotalCa = conutive + affective + phychomotor;
                            string MarkTotalCas = (conutive + affective + phychomotor).ToString("0");
                            MarkTotal = double.Parse(MarkTotalCas) + double.Parse(exams);
                            Response.Write("<tr>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span>" + (i + 1) + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + ds.Tables[1].Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + ds.Tables[1].Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + ds.Tables[1].Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (conutive.ToString()=="0"?"":conutive.ToString("0.0")) + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (affective.ToString()=="0"?"": affective.ToString("0.0")) + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (phychomotor.ToString()=="0"?"": phychomotor.ToString("0.0")) + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><span>" + (MarkTotalCa.ToString()=="0"?"": MarkTotalCa.ToString("0.0")) + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (exam.ToString()=="0"?"": exam.ToString("0.0")) + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><span>" + (MarkTotal.ToString()=="0"?"": MarkTotal.ToString("0")) + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><span>" + Grade(MarkTotal).ToString() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><span>" + TeacherRemark(MarkTotal).ToString() + "</span></td>");
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

    public string Grade(double marks)
    {
        if (marks >= 75)
        { return "A1"; }
        else if (marks >= 70)
        { return "B2"; }
        else if (marks >= 65)
        { return "B3"; }
        else if (marks >= 60)
        { return "C4"; }
        else if (marks >= 55)
        { return "C5"; }
        else if (marks >= 50)
        { return "C6"; }
        else if (marks >= 45)
        { return "D7"; }
        else if (marks >= 40)
        { return "E8"; }
        else if(marks  <40)
        { return "F9"; }
        else
        {
            return "";
        }
    }
    public string TeacherRemark(double marks)
    {
        if (marks >= 70)
        { return "Excellent"; }
        else if (marks >= 60)
        { return "Good"; }
        else if (marks >= 50)
        { return "Average"; }
        else if (marks >= 40)
        { return "B-average"; }
        else if (marks <40)
        { return "Fail"; }
        else
        {
            return "";
        }
    }

}