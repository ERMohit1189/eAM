using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class StudentsPhotoReportServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string session = Request.Form["session"].ToString().Trim();
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string Section = Request.Form["Section"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string status = Request.Form["status"].ToString().Trim();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "USP_StudentsPhotoReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                if (SrNo.Trim()!="")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                if (Section !="<--Select-->")
                {
                    cmd.Parameters.AddWithValue("@SectionName", Section.Trim());
                }
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                if (status!="")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                }
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                    Response.Write("<div class='col-sm-12' style='margin-top: 0px;'>");
                    Response.Write("<h3 class='text-center' style='    text-transform: uppercase;'><b><span class='span'>PHOTO REPORT OF CLASS " + ClassName + " "+(Section != "<--Select-->"?"("+Section+")":"") +" FOR <span>"+session+"</span></span></b></h3>");
                    Response.Write("</div>");
                    Response.Write("<table class='table term1 mp-table p-table-bordered table-bordered text_center' style='margin-bottom: 5px; margin-top: 10px !important; width:100%;'><tbody>");
                    Response.Write("<tr>");
                    Response.Write("<th class='text-center'>#</th>");
                    Response.Write("<th class='text-center'>S.R. No.</th>");
                    Response.Write("<th class='text-center'>Student's Name</th>");
                    Response.Write("<th class='text-center'>Father's Name</th>");
                    Response.Write("<th class='text-center'>Student's Photo</th>");
                    Response.Write("<th class='text-center'>Father's Photo</th>");
                    Response.Write("<th class='text-center'>Mother's Photo</th>");
                    Response.Write("<th class='text-center'>Group Photo</th>");
                    Response.Write("</tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "USP_StudentsPhotoReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        das.Fill(ds);
                        cmd.Parameters.Clear();
                        try
                        {
                            Response.Write("<tr>");
                            Response.Write("<td class='text-center'>" + (i + 1) + "</td>");
                            Response.Write("<td class='text-center'>" + dt.Rows[i]["admissionNo"].ToString().Trim() + "</td>");
                            Response.Write("<td class='text-center'>" + dt.Rows[i]["StudentName"].ToString().Trim() + "</td>");
                            Response.Write("<td class='text-center'>" + dt.Rows[i]["FatherName"].ToString().Trim() + "</td>");
                            Response.Write("<td class='text-center'>" + (ds.Tables[0].Rows[0]["PhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:60px;' />") +"</td>");// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>");
                            Response.Write("<td class='text-center'>" + (ds.Tables[1].Rows[0]["FatherPhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[1].Rows[0]["FatherPhotoPath"].ToString() + " style='width:60px;' />") +"</td>");// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>");
                            Response.Write("<td class='text-center'>" + (ds.Tables[2].Rows[0]["MotherPhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[2].Rows[0]["MotherPhotoPath"].ToString() + " style='width:60px;' />") +"</td>");// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>");
                            Response.Write("<td class='text-center'>" + (ds.Tables[3].Rows[0]["GroupPhotoPath"].ToString() == "" ? "<img src='../uploads/pics/Student.ico' style='width:60px;' />" : "<img class='zoom' src=" + ds.Tables[3].Rows[0]["GroupPhotoPath"].ToString() + " style='width:60px;' />") +"</td>");// "<img class='zoom' src='../uploads/StudentPhoto/blank.jpg' style='width:40px;' />" : '<img class='zoom' src="+ ds.Tables[0].Rows[0]["PhotoPath"].ToString() + " style='width:40px;'/>'+)+"</td>");
                            Response.Write("</tr>");
                            
                        }
                        catch (SqlException ex)
                        { throw ex; }
                    }
                    Response.Write("</tbody></table>");
                }
            }
        }
    }
}