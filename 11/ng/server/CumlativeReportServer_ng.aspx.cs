using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class CumlativeReportServer_ng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string srno = Request.Form["srno"].ToString().Trim();
        string TermId = Request.Form["TermId"].ToString().Trim();
        string TermName = Request.Form["TermName"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "CumlativeReport_ng";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                if (SectionId!="")
                {
                    cmd.Parameters.AddWithValue("@SectionID", SectionId.Trim());
                }
                if (BranchId!="")
                {
                    cmd.Parameters.AddWithValue("@BranchId", BranchId.Trim());
                }
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
                    bool buildHeader = false;
                    Response.Write("<div class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;  overflow: auto;'>");
                    Response.Write("<div id='break_div' class='term1' style='height:1032px; page-break-after: always;'>");

                    Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                    Response.Write("<div  style='padding-top: 99px;'>");

                    Response.Write("<table class='table term1 mp-table p-table-bordered table-bordered text_center' style='margin-bottom: 5px; margin-top: 5px; width:100%;'><tbody>");
                    Response.Write("<tr></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = "CumlativeReport_ng";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClassId", ClassId.Trim());
                        cmd.Parameters.AddWithValue("@SectionID", SectionId.Trim());
                        cmd.Parameters.AddWithValue("@BranchId", BranchId.Trim());
                        cmd.Parameters.AddWithValue("@TermId", TermId.Trim());
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
                                if (ds.Tables[0].Rows.Count > 0 && buildHeader==false)
                                {
                                    Response.Write("<tr class='text-center' style='height: 40px;'>");
                                    Response.Write("<th style='text-align:left !important; white-space: nowrap;'>#</th><th style='text-align:left !important; white-space: nowrap;'>S.R. NO.</th><th style='text-align:left !important; white-space: nowrap;'>Student's Name</th>");
                                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                                    {
                                        Response.Write("<th style='width:20%; text-align:center !important; white-space: nowrap;'>" + ds.Tables[0].Rows[k]["subjectName"].ToString() + "<br/>" + (ds.Tables[0].Rows[k]["Maxtotal"].ToString() == "" ? "" : double.Parse(ds.Tables[0].Rows[k]["Maxtotal"].ToString()).ToString("0")) + "</th>");
                                        buildHeader = true;
                                    }
                                    Response.Write("<th style='text-align:left !important; white-space: nowrap;'>Total</th>");
                                    Response.Write("</tr>");
                                }
                                
                                
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    double total = 0;
                                    Response.Write("<tr class='text_center'>");
                                    Response.Write("<td class='tdDesign' style='text-align:left !important; white-space: nowrap;'>" + (i+1) + "</td><td class='tdDesign' style='text-align:left !important; white-space: nowrap;'>" + dt.Rows[i]["SrNo"].ToString().Trim() + "</td><td class='tdDesign' style='text-align:left !important; white-space: nowrap;'>" + dt.Rows[i]["name"].ToString() + "</td>");
                                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                    {
                                        Response.Write("<td class='tdDesign' style='text-align:left !important; white-space: nowrap;'>" + (ds.Tables[1].Rows[j]["obtainedMark"].ToString() == "" ? "" : double.Parse(ds.Tables[1].Rows[j]["obtainedMark"].ToString()).ToString("0")) + "</td>");
                                        total = total + double.Parse((ds.Tables[1].Rows[j]["obtainedMark"].ToString()==""?"0": ds.Tables[1].Rows[j]["obtainedMark"].ToString()));
                                    }
                                    Response.Write("<td class='tdDesign' style='text-align:right !important; white-space: nowrap;'>" + total.ToString("0") + "</td>");
                                    Response.Write("</tr>");
                                }
                                
                            }
                        }
                        catch (SqlException ex)
                        { throw ex; }
                    }
                    
                    Response.Write("</tbody></table>");
                    Response.Write("</div></div></div>");
                }
            }
        }
    }
}
