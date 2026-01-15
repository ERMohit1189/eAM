using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Master_traitsEntryServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string TraitsType = Request.Form["TraitsType"].ToString().Trim();
        string TermId = Request.Form["TermId"].ToString().Trim();
        string srno = Request.Form["srno"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "master_NG_TraitsEntry";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString());
                cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString());
                cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString());
                cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                if (srno!="")
                {
                    cmd.Parameters.AddWithValue("@srNo", srno.ToString());
                }
                cmd.Parameters.AddWithValue("@action", "select_traitsentry");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        if (TraitsType == "PSYCHOMOTIVE TRAITS")
                        {
                            Response.Write("<tr>");
                            Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n sub-m-w-48' scope='col'>S.R. No.</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-w-175' scope='col'>Student's Name</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-w-175' scope='col'>Father's Name</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Spirit of team work</span></th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Willingness to learn</span></th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Participation in game</span></th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Verbal fluency</span></th>");
                            Response.Write("</tr>");
                        }
                        if (TraitsType == "AFFECTIVE TRAITS")
                        {
                            Response.Write("<tr>");
                            Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n sub-m-w-48' scope='col'>S.R. No.</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-w-175' scope='col'>Student's Name</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-sub-tit p-pad-n sub-w-175' scope='col'>Father's Name</th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Mental alertness</span></th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Politeness or respect</span></th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Neatness</span></th>");
                            Response.Write("<th style='white-space: nowrap;' class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span>Relationship with others</span></th>");
                            Response.Write("</tr>");
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "master_NG_TraitsEntry";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ClassId", ClassId.ToString());
                            cmd.Parameters.AddWithValue("@SectionId", SectionId.ToString());
                            cmd.Parameters.AddWithValue("@BranchId", BranchId.ToString());
                            cmd.Parameters.AddWithValue("@TermId", TermId.ToString());
                            cmd.Parameters.AddWithValue("@srNo", dt.Rows[i]["SrNo"].ToString());
                            cmd.Parameters.AddWithValue("@SessionName", SessionName.ToString());
                            cmd.Parameters.AddWithValue("@tradsType", TraitsType.ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
                            cmd.Parameters.AddWithValue("@action", "select_Traits");
                            SqlDataAdapter das = new SqlDataAdapter(cmd);
                            DataTable dts = new DataTable();
                            da.Fill(dts);
                            cmd.Parameters.Clear();
                            if (dts.Rows.Count > 0)
                            {
                                if (TraitsType == "PSYCHOMOTIVE TRAITS")
                                {
                                    double Spiritofteamwork = 0, Willingnesstolearn = 0, Participationingame = 0, Verbalfluency = 0;
                                    Response.Write("<tr>");
                                    for (int j = 0; j < dts.Rows.Count; j++)
                                    {
                                        Spiritofteamwork = double.Parse((dts.Rows[j]["Traits1"].ToString() == "" ? "0" : dts.Rows[j]["Traits1"].ToString()));
                                        Willingnesstolearn = double.Parse((dts.Rows[j]["Traits2"].ToString() == "" ? "0" : dts.Rows[j]["Traits2"].ToString()));
                                        Participationingame = double.Parse((dts.Rows[j]["Traits3"].ToString() == "" ? "0" : dts.Rows[j]["Traits3"].ToString()));
                                        Verbalfluency = double.Parse((dts.Rows[j]["Traits4"].ToString() == "" ? "0" : dts.Rows[j]["Traits4"].ToString()));
                                        Response.Write("<td class='p-tot-tit p-pad-n'><span>" + (i + 1) + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dts.Rows[j]["srno"].ToString().ToUpper() + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dts.Rows[j]["Name"].ToString().ToUpper() + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dts.Rows[j]["FatherName"].ToString().ToUpper() + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Spiritofteamwork.ToString() == "0" ? "" : Spiritofteamwork.ToString("0.0")) + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Willingnesstolearn.ToString() == "0" ? "" : Willingnesstolearn.ToString("0.0")) + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Participationingame.ToString() == "0" ? "" : Participationingame.ToString("0.0")) + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Verbalfluency.ToString() == "0" ? "" : Verbalfluency.ToString("0.0")) + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                    }
                                    Response.Write("</tr>");
                                }
                                if (TraitsType == "AFFECTIVE TRAITS")
                                {
                                    double Mentalalertness = 0, Politeness_or_respect = 0, Neatness = 0, Relationshipwithothers = 0;
                                    Response.Write("<tr>");
                                    for (int k = 0; k < dts.Rows.Count; k++)
                                    {
                                        Mentalalertness = double.Parse((dts.Rows[k]["Traits1"].ToString() == "" ? "0" : dts.Rows[k]["Traits1"].ToString()));
                                        Politeness_or_respect = double.Parse((dts.Rows[k]["Traits2"].ToString() == "" ? "0" : dts.Rows[k]["Traits2"].ToString()));
                                        Neatness = double.Parse((dts.Rows[k]["Traits3"].ToString() == "" ? "0" : dts.Rows[k]["Traits3"].ToString()));
                                        Relationshipwithothers = double.Parse((dts.Rows[k]["Traits4"].ToString() == "" ? "0" : dts.Rows[k]["Traits4"].ToString()));
                                        Response.Write("<td class='p-tot-tit p-pad-n'><span>" + (i + 1) + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dts.Rows[k]["srno"].ToString().ToUpper() + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dts.Rows[k]["Name"].ToString().ToUpper() + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-tot-tit p-pad-n'><span>" + dts.Rows[k]["FatherName"].ToString().ToUpper() + "</span></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Mentalalertness.ToString() == "0" ? "" : Mentalalertness.ToString("0.0")) + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Politeness_or_respect.ToString() == "0" ? "" : Politeness_or_respect.ToString("0.0")) + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Neatness.ToString() == "0" ? "" : Neatness.ToString("0.0")) + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                        Response.Write("<td style='white-space: nowrap;' class='p-pad-n text-center tab-in'><input type='text' value='" + (Relationshipwithothers.ToString() == "0" ? "" : Relationshipwithothers.ToString("0.0")) + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                        Response.Write("</tr>");
                                    }
                                }
                            }
                        }
                        Response.Write("</tbody></table>");
                        Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit' /></div>");
                    }
                }
                catch (SqlException ex)
                { throw ex; }
            }
        }
    }
}