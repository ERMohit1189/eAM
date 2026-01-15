using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class MarksEntryNurToPrepServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Term = Request.Form["Term"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                
                cmd.Connection = conn;
                cmd.CommandText = "ICSEMarkEntryItoVIIIProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
                cmd.Parameters.AddWithValue("@SessionName", SessionName);
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                       
                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'>");
                        Response.Write("<thead style='background: #f3f3f3cc;'>");
                        Response.Write("<tr>");
                        Response.Write("<th colspan='10'><h2 style='font-size: 17px !important;margin: 5px 0 5px; font-weight:bold;'>MARK ENTRY OF CLASS <span style='color: #428bca;'>" + dt.Rows[0]["CombineClassName"].ToString().ToUpper() + "</span> IN <span style='color: #428bca;'>" + Term.ToUpper() + "</span></h2></th>");
                        Response.Write("</tr>");

                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:3%;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 7% !important;'>S.R. No.</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 15% !important; text-align: left !important;'>Student's Name</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 15% !important; text-align: left !important;'>Father's Name</th>");
                        if (Term.ToLower() == "term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'>F.A. 1</th><th class='p-tot-tit p-pad-n' scope='col'>F.A. 2</th><th class='p-tot-tit p-pad-n' scope='col'>Evaluation</th>");
                        }
                        if (Term.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'>F.A. 3</th><th class='p-tot-tit p-pad-n' scope='col'>F.A. 4</th><th class='p-tot-tit p-pad-n' scope='col'>Evaluation</th>");
                        }
                        Response.Write("</tr>");
                        Response.Write("</thead>");
                        Response.Write("<tbody>");


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string sql2 = "select FA1, FA2, EvaluationTerm1, FA3, FA4, EvaluationTerm2 from ICSEMarkEntryNurtoPrep where BranchCode=" + BranchCode.ToString() + "  and SessionName='" + SessionName.ToString() + "' ";
                            sql2 = sql2 + " and ClassId=" + ClassId.ToString() + " and BranchId=" + BranchId.ToString().Trim() + " and SectionId=" + SectionId.ToString() + " and SubjectId=" + SubjectId.ToString().Trim() + " and PaperId=" + PaperId.ToString().Trim() + "";
                            sql2 = sql2 + " and SrNo='" + dt.Rows[i]["srno"].ToString() + "'";
                           
                            if (Term.ToLower() == "term1")
                            {
                                Response.Write("<tr>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + oo.ReturnTag(sql2, "FA1").ToUpper() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + oo.ReturnTag(sql2, "FA2").ToUpper() + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + oo.ReturnTag(sql2, "EvaluationTerm1").ToUpper() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("</tr>");
                            }
                            if (Term.ToLower() == "term2")
                            {
                                Response.Write("<tr>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + oo.ReturnTag(sql2, "FA3").ToUpper() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + oo.ReturnTag(sql2, "FA4").ToUpper() + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + oo.ReturnTag(sql2, "EvaluationTerm2").ToUpper() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("</tr>");
                            }
                        }
                        Response.Write("</tbody></table>");
                        Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit'  onclick='ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp'); return validationReturn();' /></div>");

                    }
                    catch (SqlException ex)
                    { throw ex; }
                }
            }
        }
    }

}