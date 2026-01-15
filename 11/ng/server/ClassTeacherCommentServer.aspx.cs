using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class ClassTeacherCommentServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string srNO = Request.Form["srNO"].ToString().Trim();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "master_NG_ClassTeacherComment";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString());
                cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString());
                cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString());
                cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                if (srNO!="")
                {
                    cmd.Parameters.AddWithValue("@srNo", srNO.ToString());
                }
                cmd.Parameters.AddWithValue("@action", "select_Students");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                        Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n sub-m-w-48' scope='col'>S.R. No.</th>");
                        Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-m-w-48 text-center' scope='col'>Student's Name</th>");
                        Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-m-w-48 text-center' scope='col'>Father's Name</th>");
                        Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n sub-w-200 tab-in' scope='col'>Comment</th>");
                        Response.Write("</tr>");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Response.Write("<tr>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span>" + (i + 1) + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='"+ dt.Rows[i]["Comment"].ToString() + "'></td>");
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