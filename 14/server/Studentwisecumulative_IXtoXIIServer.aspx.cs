using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Studentwisecumulative_IXtoXIIServer : System.Web.UI.Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionID = Request.Form["SectionID"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string session = Request.Form["session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string status = Request.Form["status"].ToString().Trim();
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string AttendanceTyp = Request.Form["AttendanceTyp"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = conn;
                cmd.CommandText = "ICSEStuWiseCumulative_ItoVIII";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@SectionId", SectionID.Trim());
                if (SrNo != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                cmd.Parameters.AddWithValue("@status", status.Trim());
                cmd.Parameters.AddWithValue("@action", "Student");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Response.Write("<div class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:0px; padding-bottom:0px; padding-left:0px;'>");
                        Response.Write("<div id='break_div' class='term1' style='page-break-after: always;'>");
                        Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");
                        Response.Write("<div class='table-responsive table-responsive2'>");
                        Response.Write("<table class='table mp-table p-table-bordered table-bordered trBg' style='margin-bottom: 0 !important;'><tbody>");
                        Response.Write("<tr><td colspan='4' class='text-center'><b>STUDENT WISE CUMULATIVE-" + session.Trim() + "</b></td></tr><tr><td style='width: 21.6%;'><b>CLASS TEACHER NAME</b>:&nbsp;<span>" + (ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["EmpName"].ToString() : "N/A") + "</span></td><td style='width: 20%;'><b>CLASS</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["class_section"].ToString() + "</span><span></span></td><td style='width: 20%;'><b>DATE</b>:&nbsp;<span>" + DateTime.Now.ToString("dd MMM yyyy") + "</span></td></tr></tbody>");
                        Response.Write("</table>");
                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered'><tr>");

                        Response.Write("<td style='width:30%; vertical-align: top;'>");

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='trBg'><td colspan='2' class='text-center'><b>Student Details</b></td></tr>");
                        Response.Write("<tr><td style='width:30%;'>S.R. No.</td><td>" + ds.Tables[0].Rows[i]["admissionNo"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Student's Name</td><td>" + ds.Tables[0].Rows[i]["StudentName"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Student D.O.B</td><td>" + ds.Tables[0].Rows[i]["DOB"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Mother's Name</td><td>MRS. " + ds.Tables[0].Rows[i]["MotherName"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Father's Name</td><td>MR. " + ds.Tables[0].Rows[i]["FatherName"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>Contact No.</td><td>" + ds.Tables[0].Rows[i]["MobileNumber"].ToString() + "</td></tr>");
                        Response.Write("<tr><td style='width:30%;'>RES.ADD.</td><td>" + ds.Tables[0].Rows[i]["StLocalAddress"].ToString() + "</td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("<br/><table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='trBg'><td colspan='2' class='text-center'><b>Grade Scale</b></td></tr>");
                        Response.Write("<tr><td style='width:50%;'><b>Marks Range</b></td><td><b>Grade</b></td></tr>");
                        Response.Write("<tr><td style='width:50%;'>80% and above</td><td>A</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>60%-79%</td><td>B</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>50%-59%</td><td>C</td></tr>");
                        Response.Write("<tr><td style='width:50%;'>Below 49%</td><td>D</td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("</td>");

                        Response.Write("<td style='width:70%; vertical-align: top;'>");
                        cmd.CommandText = "ICSEStuWiseCumulative_IXtoXII";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@SrNo", ds.Tables[0].Rows[i]["admissionNo"].ToString());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@SectionId", SectionID.Trim());
                        cmd.Parameters.AddWithValue("@AttendanceTyp", AttendanceTyp.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dss = new DataSet();
                        das.Fill(dss);
                        cmd.Parameters.Clear();

                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th>HALF YEARLY</th><th>PROJECT/PRAC.</th><th>TOTAL 100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='3' class='text-center'><B>TERM1</B></td></tr>");
                        double Gtotal1 = 0; 
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                double HY = 0; double PracHY = 0;
                                double.TryParse(dss.Tables[0].Rows[p]["HY"].ToString(), out HY);
                                double.TryParse(dss.Tables[0].Rows[p]["PracHY"].ToString(), out PracHY);
                                Gtotal1 = Gtotal1 + (HY+ PracHY);
                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["PaperName"].ToString() + "</td><td>" + dss.Tables[0].Rows[p]["HY"].ToString() + "</td><td>" + dss.Tables[0].Rows[p]["PracHY"].ToString() + "</td><td>" + (PracHY + HY).ToString("0.0") + "</td></tr>");
                            }
                        }
                        

                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total 100</td><td></td><td></td><td>" + Gtotal1.ToString("0.0") + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Working Days</td><td>" + (dss.Tables[1].Rows.Count > 0 ? dss.Tables[1].Rows[0]["totaldays"].ToString() : "0") + "</td><td colspan='5'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Attendance</td><td>" + (dss.Tables[1].Rows.Count > 0 ? dss.Tables[1].Rows[0]["Attendence"].ToString() : "0") + "</td><td colspan='5'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th>ANNUAL</th><th>PROJECT/PRAC.</th><th>TOTAL 100</th></tr>");
                        Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='3' class='text-center'><B>TERM2</B></td></tr>");
                        double Gtotal2 = 0;
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                double AE = 0; double PracAE = 0;
                                double.TryParse(dss.Tables[0].Rows[p]["AE"].ToString(), out AE);
                                double.TryParse(dss.Tables[0].Rows[p]["PracAE"].ToString(), out PracAE);
                                Gtotal2 = Gtotal2 + (AE + PracAE);

                                Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["PaperName"].ToString() + "</td><td>" + dss.Tables[0].Rows[p]["AE"].ToString() + "</td><td>" + dss.Tables[0].Rows[p]["PracAE"].ToString() + "</td><td>" + (PracAE + AE).ToString("0.0") + "</td></tr>");
                            }
                        }


                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total 100</td><td></td><td></td><td>" + Gtotal2.ToString("0.0") + "</td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Working Days</td><td>" + (dss.Tables[2].Rows.Count > 0 ? dss.Tables[2].Rows[0]["totaldays"].ToString() : "0") + "</td><td colspan='5'></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total Attendance</td><td>" + (dss.Tables[2].Rows.Count > 0 ? dss.Tables[2].Rows[0]["Attendence"].ToString() : "0") + "</td><td colspan='5'></td></tr>");

                        Response.Write("</tbody></table>");


                        Response.Write("</td>");
                        Response.Write("</tr></table>");


                        Response.Write("</div >");

                        Response.Write("</div></div>");
                    }
                }
            }
        }
    }

    public string grade(double percentle)
    {
        if (percentle < 33)
        {
            return "E";
        }
        else if (percentle >= 33 && percentle < 41)
        {
            return "D";
        }
        else if (percentle >= 41 && percentle < 51)
        {
            return "C2";
        }
        else if (percentle >= 51 && percentle < 61)
        {
            return "C1";
        }
        else if (percentle >= 61 && percentle < 71)
        {
            return "B2";
        }
        else if (percentle >= 71 && percentle < 81)
        {
            return "B1";
        }
        else if (percentle >= 81 && percentle < 91)
        {
            return "A2";
        }
        else if (percentle >= 91 && percentle <= 100)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }
}