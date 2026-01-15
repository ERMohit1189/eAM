using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;


public partial class _1_StudentDocsReportServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string drpClassId = Request.Form["drpClassId"].ToString();
        string drpClassName = Request.Form["drpClassName"].ToString();
        string drpSection = Request.Form["drpSection"].ToString();
        string drpAdmissioncType = Request.Form["drpAdmissioncType"].ToString();
        string Hardcopy = Request.Form["Hardcopy"].ToString();
        string Softcopy = Request.Form["Softcopy"].ToString();
        string BranchCode = Request.Form["BranchCode"].ToString();
        string session = Request.Form["session"].ToString();
        var chksName = Request.Form["chksName"].ToString();
        var chksNamedata = Request.Form["chksNamedata"].ToString();
        string[] chkNameLength = chksNamedata.Split(new string[] { "##" }, StringSplitOptions.None);

        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(int));
        dt.Columns.Add("DocumentType", typeof(string));
        DataRow dr;

        for (int q = 0; q < chkNameLength.Length; q++)
        {
            string[] chksValueLength = chkNameLength[q].Split(new string[] { "#" }, StringSplitOptions.None);
            dr = dt.NewRow();
            dr["id"] = chksValueLength[0];
            dr["DocumentType"] = chksValueLength[1];
            dt.Rows.Add(dr);

        }



        var ddllistOf = Request.Form["ddllistOf"].ToString();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "Get_StudentDocsRecord_new";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                if (drpClassName != "")
                {cmd.Parameters.AddWithValue("@ClassName", drpClassName.Trim()); }
                if (drpSection != "")
                { cmd.Parameters.AddWithValue("@SectionName", drpSection.Trim()); }
                cmd.Parameters.AddWithValue("@Hardcopy", Hardcopy.Trim());
                cmd.Parameters.AddWithValue("@Softcopy", Softcopy.Trim());
                if (drpAdmissioncType != "")
                { cmd.Parameters.AddWithValue("@TypeOFAdmision", drpAdmissioncType.Trim()); }
                cmd.Parameters.AddWithValue("@docTypes", chksName);
                cmd.Parameters.AddWithValue("@SubmitTypes", ddllistOf);
                cmd.Parameters.AddWithValue("@QueryFor", "Student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                try
                {
                    conn.Open();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<table id='example' class='table no-bm  table-striped table-hover no-head-border table-bordered'>");
                        Response.Write("<thead>");
                        Response.Write("<tr><td class='text-center' colspan='"+(5+ dt.Rows.Count) + "'><b>Document Report</b></td></tr>");
                        Response.Write("<tr>");
                        Response.Write("<th align = 'center' valign = 'middle' scope = 'col' style = 'width: 40px; '>#</th> <th scope='col'>S.R. No.</th> <th scope='col'>Student's Name</th> <th scope='col'>Father's Name</th> <th scope='col'>Class</th>");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Response.Write("<th  scope='col'>" + ds.Tables[0].Rows[i]["DocumentType"].ToString() + "</th>");

                        }
                        Response.Write("</tr></thead>");
                        Response.Write("<tbody>");

                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            Response.Write("<tr>");
                            Response.Write("<td>" + (j + 1) + "</td>");
                            Response.Write("<td>" + ds.Tables[1].Rows[j]["Srno"].ToString() + "</td>");
                            Response.Write("<td>" + ds.Tables[1].Rows[j]["Name"].ToString() + "</td>");
                            Response.Write("<td>" + ds.Tables[1].Rows[j]["FatherName"].ToString() + "</td>");
                            Response.Write("<td>" + ds.Tables[1].Rows[j]["ClassName"].ToString() + " "+(drpSection.ToString()!=""?"("+drpSection.ToString()+")":"")+")</td>");

                            cmd.Connection = conn;
                            cmd.CommandText = "Get_StudentDocsRecord_new";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                            cmd.Parameters.AddWithValue("@Srno", ds.Tables[1].Rows[j]["Srno"].ToString());
                            cmd.Parameters.AddWithValue("@BranchCode", BranchCode.Trim());
                            cmd.Parameters.AddWithValue("@docTypes", chksName);
                            cmd.Parameters.AddWithValue("@QueryFor", "Doc");
                            SqlDataAdapter das = new SqlDataAdapter(cmd);
                            DataTable dts = new DataTable();
                            das.Fill(dts);
                            cmd.Parameters.Clear();

                            if (dts.Rows.Count>0)
                            {
                                for (int k = 0; k < dts.Rows.Count; k++)
                                {
                                    string Softcopys = ""; string Hardcopys = "";
                                    if (dts.Rows[k]["Softcopy"].ToString() == "0")
                                    {
                                        Softcopys = "[S]<i class='text-false-color txt-bold' style='font-size:15px'>X</i>";
                                    }
                                    else
                                    {
                                        Softcopys = "[S]<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                                    }
                                    if (dts.Rows[k]["Hardcopy"].ToString() == "0")
                                    {
                                        Hardcopys = "[H]<i class='text-false-color txt-bold' style='font-size:15px'>X</i>";
                                    }
                                    else
                                    {
                                        Hardcopys = "[H]<i class='text-true-color txt-bold' style='font-size:15px'>✔</i>";
                                    }
                                    if (Hardcopy=="1" && Softcopy=="1")
                                    {
                                        Response.Write("<td>" + Softcopys + "&nbsp;|&nbsp;" + Hardcopys + "</td>");

                                    }
                                    if (Hardcopy != "1" && Softcopy == "1")
                                    {
                                        Response.Write("<td>" + Softcopys +"</td>");

                                    }
                                    if (Hardcopy == "1" && Softcopy != "1")
                                    {
                                        Response.Write("<td>" + Hardcopys + "</td>");

                                    }
                                }
                            }
                            
                            Response.Write("</tr>");
                        }
                        Response.Write("</tbody>");
                        Response.Write("</table>");
                    }
                    else
                    {
                        Response.Write("");
                    }
                    conn.Close();
                }
                catch (SqlException ex)
                {  throw ex; }
            }
        }
    }
}