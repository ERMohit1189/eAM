using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class OtherFeeHeadListServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FromClassName = Request.Form["FromClassName"].ToString().Trim();
        string ToClassName = Request.Form["ToClassName"].ToString().Trim();

        string FromClassId = Request.Form["FromClassId"].ToString().Trim();
        string ToClassId = Request.Form["ToClassId"].ToString().Trim();
        string Gender = Request.Form["Gender"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "USP_OtherFeeHeadMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromClassid", FromClassId.Trim());
                cmd.Parameters.AddWithValue("@ToClassId", ToClassId.Trim());
                cmd.Parameters.AddWithValue("@Gender", Gender.Trim());
                cmd.Parameters.AddWithValue("@SessionName", SessionName.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@action", "select");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                
                if (dt.Rows.Count > 0)
                {
                    Response.Write("<div class='col -sm-12  no-padding' id='icons' style='margin-bottom: 20px; padding-bottom: 5px !important;'>");
                             Response.Write("<div style = 'float: right; font-size: 19px;'>");
                    Response.Write("<a onclick='PrintDiv();' class='btn btn-sm'><i class='fa fa-print text-primary'></i>Print</a>");
                    Response.Write("</div>");
                    Response.Write("</div>");
                    Response.Write("<div id='printList' class='table - responsive'>");
                    Response.Write("<h3 style='text-align: center; padding: 5px; font-size: 16px !important; font-weight: bold !important;'>Other Fee Heads ("+(FromClassName==ToClassName? FromClassName: FromClassName + " - " + ToClassName) + ")<h3>");
                    Response.Write("<table class='table table-striped table-hover no-bm no-head-border table-bordered' style='text-align: center;'><thead>");
                    Response.Write("<tr>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center'>#</th>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center'>Head Name</th>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center'>Amount</th>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center'>Class</th>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center'>Gender</th>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center no-print' style='width:50px;'>Edit</th>");
                    Response.Write("<th class='vd_bg-blue vd_white text-center no-print' style='width:50px;'>Delete</th>");
                    Response.Write("</tr>");
                    Response.Write("</thead>");
                    Response.Write("<tbody>");
                    int sno = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Response.Write("<tr valdata='" + dt.Rows[i]["id"].ToString() + "##" + dt.Rows[i]["ClassId"].ToString() + "##" + dt.Rows[i]["IsSingleHead"].ToString() + "##" + dt.Rows[i]["GenderId"].ToString() + "##" + dt.Rows[i]["HeadName"].ToString() + "##" + dt.Rows[i]["Amount"].ToString() + "##" + dt.Rows[i]["Remark"].ToString() + "'>");
                        Response.Write("<td>"+(sno+1) +"</td>");
                        Response.Write("<td>"+ dt.Rows[i]["HeadName"].ToString() + "</td>");
                        Response.Write("<td>"+ dt.Rows[i]["Amount"].ToString() + "</td>");
                        Response.Write("<td>" + dt.Rows[i]["ClassName"].ToString() + "</td>");
                        Response.Write("<td>" + dt.Rows[i]["Gender"].ToString() + "</td>");
                        Response.Write("<td class='no-print'><span class='btn menu-icon vd_bd-yellow vd_yellow' style='padding: 1px 5px !important;' onclick='editHead(this)'><i class='fa fa-pencil'></i></span>");
                        Response.Write("<td class='no-print'><span class='btn menu-icon vd_bd-red vd_red' style='padding: 1px 5px !important;' onclick='deleteHead(this)'><i class='glyphicon glyphicon-trash'></i></span></td>");
                        Response.Write("</tr>");
                        sno = sno + 1;
                    }
                    Response.Write("</tbody></table>");
                    Response.Write("</div>");
                }
                
            }
        }
    }
    
}