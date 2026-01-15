using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G5_Studentwisecumulative_IXtoX_1718Server : System.Web.UI.Page
{
    Campus oo = new Campus();
    string sql = "";
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

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = conn;
                cmd.CommandText = "StudentWiseCumulative_IXtoX_2021";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@SectionID", SectionID.Trim());
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
                        Response.Write("<tr><td colspan='3' class='text-center'><b>STUDENT WISE CUMULATIVE " + session.Trim() + "</b></td></tr>");
                        Response.Write("<tr><td style='width: 21.6%;'><b>Student's Name</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["StudentName"].ToString() + "(" + ds.Tables[0].Rows[i]["admissionNo"].ToString() + ")</span></td><td style='width: 20%;'><b>Father's Name</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["FatherName"].ToString() + "</span><span></span></td><td style='width: 20%;'><b>D.O.B</b>:&nbsp;<span>" + ds.Tables[0].Rows[i]["DOB"].ToString() + "</span></td></tr>");
                        Response.Write("<tr><td colspan='3' class='text-center'>");

                        cmd.CommandText = "StudentWiseCumulative_IXtoX_2021";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", session.Trim());
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@SrNo", ds.Tables[0].Rows[i]["admissionNo"].ToString());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dss = new DataSet();
                        das.Fill(dss);
                        cmd.Parameters.Clear();
                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        if (ClassName == "IX")
                        {
                            Response.Write("<tr class='p-pad-3 trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th>U.T. I<br>(20)</th><th>U.T. II<br>(20)</th><th>Conv. into<br>(5)</th><th>S.E.<br>(5)</th><th>M.A.<br>(5)</th><th>Port.<br>(5)</th><th>H.Y.E.<br>(80)</th><th>Total<br>(100)</th><th>Grade</th><th>U.T. III<br>(20)</th><th>U.T. IV<br>(20)</th><th>Conv. into<br>(5)</th><th>S.E.<br>(5)</th><th>M.A.<br>(5)</th><th>Port.<br>(5)</th><th>A.E.<br>(80)</th><th>Total<br>(100)</th><th>Grade</th><th>G. TOTAL<br>(200)</th><th>Grade</th></tr>");
                            Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='9' class='text-center'><B>TERM 1</B></td><td colspan='9' class='text-center'><B>TERM 2</B></td><td colspan='2'></td></tr>");
                        }
                        if (ClassName=="X")
                        {
                            Response.Write("<tr class='p-pad-3 trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th>U.T. I<br>(20)</th><th>Conv. into<br>(20)</th><th>U.T. II<br>(80)</th><th>Conv. into<br>(20)</th><th>P.T.<br>(5)</th><th>S.E.<br>(5)</th><th>M.A.<br>(5)</th><th>Port.<br>(5)</th><th>H.Y.E.<br>(80)</th><th>Total<br>(100)</th><th>Grade</th><th>Prelims.<br>(80)</th><th>P.T.<br>(5)</th><th>S.E.<br>(5)</th><th>M.A.<br>(5)</th><th>Port.<br>(5)</th><th>Pre. Board 1<br>(80)</th><th>Pre. Board 2<br>(80)</th><th>Total<br>(100)</th><th>Grade</th><th>G. TOTAL<br>(200)</th><th>Grade</th></tr>");
                            Response.Write("<tr class='trBg'><th>SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='11' class='text-center'><B>TERM 1</B></td><td colspan='9' class='text-center'><B>TERM 2</B></td><td colspan='2'></td></tr>");
                        }
                        double Gpercentle = 0; double GTest1 = 0; double GTest2 = 0; double GMA = 0; double GPort = 0; double GSE = 0; double GSAT = 0; double GPrac = 0; double Gtotal = 0;
                        double Gpercentle_2 = 0; double GTest1_2 = 0; double GTest2_2 = 0; double GMA_2 = 0; double GPort_2 = 0; double GSE_2 = 0; double GSAT_2 = 0; double GPrac_2 = 0; double Gtotal_2 = 0;
                        
                        double totalmarks1 = 0; double totalmmmarks1 = 0; double bestofAE = 0; double bestofAE2 = 0;
                        double totalmarks_2 = 0; double totalmmmarks_2 = 0;
                        double percentle1 = 0; double ObtMarks1 = 0;
                        double percentle_2 = 0; double ObtMarks_2 = 0;
                        double Test1convin20 = 0; double Test2convin20 = 0;
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            double Test1 = 0; double Test2 = 0; double MA = 0; double SE = 0; double SAT = 0; double Prac = 0; double Port = 0; double MaxMarks1 = 0; double MaxMarks2 = 0; double MaxMarks3 = 0; double MaxMarks4 = 0; double MaxMarks5 = 0; double MaxMarks6 = 0; double MaxMarks7 = 0;
                            double Test1_2 = 0; double Test2_2 = 0; double MA_2 = 0; double SE_2 = 0; double SAT_2 = 0; double SAT2_2 = 0; double Prac_2 = 0; double Port_2 = 0; double MaxMarks1_2 = 0; double MaxMarks2_2 = 0; double MaxMarks3_2 = 0; double MaxMarks4_2 = 0; double MaxMarks5_2 = 0; double MaxMarks6_2 = 0; double MaxMarks7_2 = 0;
                            for (int p = 0; p < dss.Tables[0].Rows.Count; p++)
                            {
                                Response.Write("<tr class='p-pad-3' style='text-align:center !important;'>");
                                if (dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { Test1 = 0; }
                                else { Test1 = double.Parse(dss.Tables[0].Rows[p]["Test1"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { Test2 = 0; }
                                else { Test2 = double.Parse(dss.Tables[0].Rows[p]["Test2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SE"].ToString().Trim().ToUpper() == "M.L") { SE = 0; }
                                else { SE = double.Parse(dss.Tables[0].Rows[p]["SE"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["MA"].ToString().Trim().ToUpper() == "M.L") { MA =0; }
                                else { MA = double.Parse(dss.Tables[0].Rows[p]["MA"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Port"].ToString().Trim().ToUpper() == "M.L") { Port = 0; }
                                else { Port = double.Parse(dss.Tables[0].Rows[p]["Port"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { SAT = 0; }
                                else { SAT = double.Parse(dss.Tables[0].Rows[p]["SAT"].ToString().Trim()); }


                                if (dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks1 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks1"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks2"].ToString().Trim()); }

                                totalmarks1 = Test1 > Test2 ? Test1 : Test2;
                                totalmmmarks1 = Test1 > Test2 ? MaxMarks1 : MaxMarks2;
                                if (ClassName == "X")
                                {
                                    if (MaxMarks1 > 0)
                                    {
                                        Test1convin20 = (Test1 * 20) / MaxMarks1;
                                    }
                                    if (MaxMarks1 > 0)
                                    {
                                        Test2convin20 = (Test2 * 20) / MaxMarks2;
                                    }
                                    totalmarks1 = (Test1convin20 > Test2convin20 ? Test1convin20 : Test2convin20);
                                    totalmmmarks1 = 20;
                                }
                                if (totalmmmarks1 > 0)
                                {
                                    percentle1 = ((totalmarks1) * 5) / 20;
                                    ObtMarks1 = (double.Parse(percentle1.ToString("0")) + double.Parse(MA.ToString("0")) + double.Parse(SE.ToString("0")) + double.Parse(SAT.ToString("0")) + double.Parse(Port.ToString("0")));
                                    ObtMarks1 = double.Parse(ObtMarks1.ToString("0"));
                                    GTest1 = GTest1 + Test1;
                                    GTest2 = GTest2 + Test2;
                                    Gpercentle = Gpercentle + double.Parse(percentle1.ToString("0"));
                                    GSE = GSE + SE;
                                    GMA = GMA + MA;
                                    GPort = GPort + Port;
                                    GSAT = GSAT + GSAT;
                                    Gtotal = Gtotal + ObtMarks1;
                                }


                                Response.Write("<td style='text-align:left; width:23%; !important'>" + dss.Tables[0].Rows[p]["SubjectName"].ToString() + "</td>");
                                if (ClassName == "IX")
                                {
                                    Response.Write("<td>" + Test1.ToString() + "</td><td>" + Test2.ToString() + "</td>");
                                }
                                if (ClassName == "X")
                                {
                                    Response.Write("<td>" + Test1.ToString() + "</td><td>" + Test1convin20.ToString() + "</td><td>" + Test2.ToString() + "</td><td>" + Test2convin20.ToString() + "</td>");
                                }
                                Response.Write("<td>" + percentle1.ToString() + "</td><td>" + SE.ToString() + "</td><td>" + MA.ToString() + "</td>");
                                Response.Write("<td>" + Port.ToString() + "</td><td>" + SAT.ToString() + "</td><td>" + ObtMarks1.ToString() + "</td>");
                                Response.Write("<td>" + grade(ObtMarks1) + "</td>");

                                if (dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { Test1_2 = 0; }
                                else { Test1_2 = double.Parse(dss.Tables[0].Rows[p]["Test1_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { Test2_2 = 0; }
                                else { Test2_2 = double.Parse(dss.Tables[0].Rows[p]["Test2_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SE_2"].ToString().Trim().ToUpper() == "M.L") { SE_2 = 0; }
                                else { SE_2 = double.Parse(dss.Tables[0].Rows[p]["SE_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["MA_2"].ToString().Trim().ToUpper() == "M.L") { MA_2 = 0; }
                                else { MA_2 = double.Parse(dss.Tables[0].Rows[p]["MA_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "M.L") { Port_2 = 0; }
                                else { Port_2 = double.Parse(dss.Tables[0].Rows[p]["Port_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { SAT_2 = 0; }
                                else { SAT_2 = double.Parse(dss.Tables[0].Rows[p]["SAT_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "" || dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "M.L") { SAT2_2 = 0; }
                                else { SAT2_2 = double.Parse(dss.Tables[0].Rows[p]["SAT2_2"].ToString().Trim()); }


                                if (dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks1_2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks1_2"].ToString().Trim()); }
                                if (dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "") { }
                                else { MaxMarks2_2 = double.Parse(dss.Tables[0].Rows[p]["MaxMarks2_2"].ToString().Trim()); }
                                totalmarks_2 = Test1_2 > Test2_2 ? Test1_2 : Test2_2;
                                totalmmmarks_2 = Test1_2 > Test2_2 ? MaxMarks1_2 : MaxMarks2_2;
                                if (ClassName == "IX")
                                {
                                    if (totalmmmarks_2 > 0)
                                    {
                                        percentle_2 = ((totalmarks_2) * 5) / totalmmmarks_2;
                                    }
                                    ObtMarks_2 = (double.Parse(percentle_2.ToString("0")) + double.Parse(MA_2.ToString("0")) + double.Parse(SE_2.ToString("0")) + double.Parse(SAT_2.ToString("0")) + double.Parse(Port_2.ToString("0")));
                                    ObtMarks_2 = double.Parse(ObtMarks_2.ToString("0"));
                                    GTest1_2 = GTest1_2 + Test1_2;
                                    GTest2_2 = GTest2_2 + Test1_2;
                                    Gpercentle_2 = Gpercentle_2 + double.Parse(percentle_2.ToString());
                                    GSE_2 = GSE_2 + SE_2;
                                    GMA_2 = GMA_2 + MA_2;
                                    GPort_2 = GPort_2 + Port_2;
                                    GSAT_2 = GSAT_2 + SAT_2;
                                    Gtotal_2 = Gtotal_2 + ObtMarks_2;
                                    Response.Write("<td>" + Test1_2.ToString() + "</td><td>" + Test2_2.ToString() + "</td>");
                                    Response.Write("<td>" + percentle_2.ToString() + "</td><td>" + SE_2.ToString() + "</td><td>" + MA_2.ToString() + "</td>");
                                    Response.Write("<td>" + Port_2.ToString() + "</td><td>" + SAT_2.ToString() + "</td><td>" + ObtMarks_2.ToString() + "</td>");
                                    Response.Write("<td>" + grade(ObtMarks_2) + "</td>");
                                    Response.Write("<td>" + (ObtMarks1 + ObtMarks_2).ToString() + "</td>");
                                    Response.Write("<td>" + grade((ObtMarks1 + ObtMarks_2) / 2) + "</td>");
                                    Response.Write("</tr>");
                                }
                                if (ClassName == "X")
                                {
                                    if (Test1_2 > 0 && MaxMarks1_2 > 0)
                                    {
                                        percentle_2 = ((Test1_2) * 5) / MaxMarks1_2;
                                    }
                                        bestofAE = (SAT_2 > SAT2_2 ? SAT_2 : SAT2_2);
                                        ObtMarks_2 = (double.Parse(percentle_2.ToString("0")) + double.Parse(MA_2.ToString("0")) + double.Parse(SE_2.ToString("0")) + double.Parse(bestofAE.ToString("0")) + double.Parse(Port_2.ToString("0")));
                                        ObtMarks_2 = double.Parse(ObtMarks_2.ToString("0"));
                                        GTest1_2 = GTest1_2 + Test1_2;
                                        GTest2_2 = GTest2_2 + Test1_2;
                                        Gpercentle_2 = Gpercentle_2 + double.Parse(percentle_2.ToString());
                                        GSE_2 = GSE_2 + SE_2;
                                        GMA_2 = GMA_2 + MA_2;
                                        GPort_2 = GPort_2 + Port_2;
                                        GSAT_2 = GSAT_2 + bestofAE2;
                                        Gtotal_2 = Gtotal_2 + ObtMarks_2;
                                    Response.Write("<td>" + Test1_2.ToString() + "</td><td>" + percentle_2.ToString() + "</td>");
                                    Response.Write("<td>" + SE_2.ToString() + "</td><td>" + MA_2.ToString() + "</td>");
                                    Response.Write("<td>" + Port_2.ToString() + "</td><td>" + SAT_2.ToString() + "</td><td>" + SAT2_2.ToString() + "</td>");
                                    Response.Write("<td>" + ObtMarks_2.ToString() + "</td><td>" + grade(ObtMarks_2) + "</td>");
                                    Response.Write("<td>" + (ObtMarks1 + ObtMarks_2).ToString() + "</td>");
                                    Response.Write("<td>" + grade((ObtMarks1 + ObtMarks_2) / 2) + "</td>");
                                    Response.Write("</tr>");
                                }
                            }
                            if (dss.Tables[1].Rows.Count > 0)
                            {
                                if (ClassName == "IX")
                                {
                                    Response.Write("<tr class='p-pad-3 trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th>U.T. I<br>(20)</th><th>U.T. II<br>(20)</th><th>Conv. into<br>(5)</th><th>PRACTICAL<br>(30)</th><th colspan='2'>Project Work/Field Visit <br>/ Portfolio </span>(15)</th><th>H.Y.E.<br>(50)</th><th>Total<br>(100)</th><th>Grade</th><th>U.T. III<br>(20)</th><th>U.T. IV<br>(20)</th><th>P.T.<br>(5)</th><th>PRACTICAL<br>(30)</th><th colspan='2'>Project Work/Field Visit<br> / Portfolio </span>(15)</th><th>A.E.<br>(50)</th><th>Total<br>(100)</th><th>Grade</th><th>G. TOTAL<br>(200)</th><th>Grade</th></tr>");
                                    Response.Write("<tr class='trBg'><th>ADDITIONAL SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='9' class='text-center'><B>TERM 1</B></td><td colspan='9' class='text-center'><B>TERM 2</B></td><td colspan='2'></td></tr>");
                                }
                                if (ClassName == "X")
                                {
                                    Response.Write("<tr class='p-pad-3 trBg'><th style='width:23%; !important'>EXAM<i class='fa fa-arrow-right'></i></th><th>U.T. I<br>(20)</th><th>Conv. into<br>(20)</th><th>U.T. II<br>(80)</th><th>Conv. into<br>(20)</th><th>P.T.<br>(5)</th><th>PRACTICAL<br>(30)</th><th colspan='2'>Project Work/Field Visit <br>/ Portfolio </span>(15)</th><th>H.Y.E.<br>(50)</th><th>Total<br>(100)</th><th>Grade</th><th>PRELIMS.(50)</th><th>Conv. into<br>(5)</th><th>PRACTICAL<br>(30)</th><th colspan='2'>Project Work/Field Visit<br> / Portfolio </span>(15)</th><th>Pre. Board 1<br>(50)</th><th>Pre. Board 2<br>(50)</th><th>Total<br>(100)</th><th>Grade</th><th>G. TOTAL<br>(200)</th><th>Grade</th></tr>");
                                    Response.Write("<tr class='trBg'><th>ADDITIONAL SUBJECT<i class='fa fa-arrow-down'></i></th><td colspan='11' class='text-center'><B>TERM 1</B></td><td colspan='9' class='text-center'><B>TERM 2</B></td><td colspan='2'></td></tr>");
                                }
                                for (int p = 0; p < dss.Tables[1].Rows.Count; p++)
                                {
                                    Response.Write("<tr class='p-pad-3' style='text-align:center !important;'>");
                                    if (dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test1"].ToString().Trim().ToUpper() == "M.L") { Test1 = 0; }
                                    else { Test1 = double.Parse(dss.Tables[1].Rows[p]["Test1"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test2"].ToString().Trim().ToUpper() == "M.L") { Test2 = 0; }
                                    else { Test2 = double.Parse(dss.Tables[1].Rows[p]["Test2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Prac"].ToString().Trim().ToUpper() == "M.L") { Prac = 0; }
                                    else { Prac = double.Parse(dss.Tables[1].Rows[p]["Prac"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Port"].ToString().Trim().ToUpper() == "M.L") { Port = 0; }
                                    else { Port = double.Parse(dss.Tables[1].Rows[p]["Port"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["SAT"].ToString().Trim().ToUpper() == "M.L") { SAT = 0; }
                                    else { SAT = double.Parse(dss.Tables[1].Rows[p]["SAT"].ToString().Trim()); }


                                    if (dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim().ToUpper() == "") { MaxMarks1 = 0; }
                                    else { MaxMarks1 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks1"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim().ToUpper() == "") { MaxMarks2 = 0; }
                                    else { MaxMarks2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks2"].ToString().Trim()); }
                                    
                                    totalmarks1 = Test1 > Test2 ? Test1 : Test2;
                                    totalmmmarks1 = Test1 > Test2 ? MaxMarks1 : MaxMarks2;
                                    
                                    if (ClassName=="X")
                                    {
                                        if (MaxMarks1 > 0)
                                        {
                                            Test1convin20 = (Test1 * 20) / MaxMarks1;
                                        }
                                        if (MaxMarks1 > 0)
                                        {
                                            Test2convin20 = (Test2 * 20) / MaxMarks2;
                                        }
                                        totalmarks1 = (Test1convin20 > Test2convin20 ? Test1convin20 : Test2convin20);
                                        totalmmmarks1 = 20;
                                    }

                                    if (totalmmmarks1 > 0)
                                    {
                                        percentle1 = ((totalmarks1) * 5) / totalmmmarks1;
                                        ObtMarks1 = (double.Parse(percentle1.ToString("0")) + double.Parse(MA.ToString("0")) + double.Parse(SE.ToString("0")) + double.Parse(SAT.ToString("0")) + double.Parse(Port.ToString("0")) + double.Parse(Prac.ToString("0")));

                                        GTest1 = GTest1 + Test1;
                                        GTest2 = GTest2 + Test2;
                                        Gpercentle = Gpercentle + double.Parse(percentle1.ToString());
                                        GPrac = GPrac + Prac;
                                        GPort = GPort + Port;
                                        GSAT = GSAT + SAT;
                                        Gtotal = Gtotal + ObtMarks1;
                                    }


                                    Response.Write("<td style='text-align:left; width:23%; !important'>" + dss.Tables[1].Rows[p]["SubjectName"].ToString() + "</td>");
                                    if (ClassName == "IX")
                                    {
                                        Response.Write("<td>" + Test1.ToString() + "</td><td>" + Test2.ToString() + "</td>");
                                    }
                                    if (ClassName == "X")
                                    {
                                        Response.Write("<td>" + Test1.ToString() + "</td><td>" + Test1convin20.ToString() + "</td><td>" + Test2.ToString() + "</td><td>" + Test2convin20.ToString() + "</td>");
                                    }
                                    Response.Write("<td>" + percentle1.ToString() + "</td><td>" + Prac.ToString() + "</td><td colspan='2'>" + Port.ToString() + "</td>");
                                    Response.Write("<td>" + SAT.ToString() + "</td><td>" + ObtMarks1.ToString() + "</td>");
                                    Response.Write("<td>" + grade(ObtMarks1) + "</td>");

                                    if (dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim().ToUpper() == "M.L") { Test1_2 = 0; }
                                    else { Test1_2 = double.Parse(dss.Tables[1].Rows[p]["Test1_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim().ToUpper() == "M.L") { Test2_2 = 0; }
                                    else { Test2_2 = double.Parse(dss.Tables[1].Rows[p]["Test2_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim().ToUpper() == "M.L") { Prac_2 = 0; }
                                    else { Prac_2 = double.Parse(dss.Tables[1].Rows[p]["Prac_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["Port_2"].ToString().Trim().ToUpper() == "M.L") { Port_2 = 0; }
                                    else { Port_2 = double.Parse(dss.Tables[1].Rows[p]["Port_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim().ToUpper() == "M.L") { SAT_2 = 0; }
                                    else { SAT_2 = double.Parse(dss.Tables[1].Rows[p]["SAT_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "" || dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "AB" || dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "NA" || dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim().ToUpper() == "M.L") { SAT2_2 = 0; }
                                    else { SAT2_2 = double.Parse(dss.Tables[1].Rows[p]["SAT2_2"].ToString().Trim()); }


                                    if (dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim().ToUpper() == "") { MaxMarks1_2 = 0; }
                                    else { MaxMarks1_2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks1_2"].ToString().Trim()); }
                                    if (dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NP" || dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "NAD" || dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "ML" || dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim().ToUpper() == "") { MaxMarks2_2 = 0; }
                                    else { MaxMarks2_2 = double.Parse(dss.Tables[1].Rows[p]["MaxMarks2_2"].ToString().Trim()); }
                                    if (ClassName == "IX")
                                    {
                                        totalmarks_2 = Test1_2 > Test2_2 ? Test1_2 : Test2_2;
                                        totalmmmarks_2 = Test1_2 > Test2_2 ? MaxMarks1_2 : MaxMarks2_2;
                                        if (totalmmmarks_2 > 0)
                                        {
                                            percentle_2 = ((totalmarks_2) * 5) / totalmmmarks_2;
                                            ObtMarks_2 = (double.Parse(percentle_2.ToString("0")) + double.Parse(Prac_2.ToString("0")) + double.Parse(SAT_2.ToString("0")) + double.Parse(Port_2.ToString("0")) + double.Parse(Prac_2.ToString("0")));
                                            GTest1_2 = GTest1_2 + Test1_2;
                                            GTest2_2 = GTest2_2 + Test1_2;
                                            Gpercentle_2 = Gpercentle_2 + double.Parse(percentle_2.ToString());
                                            GPrac_2 = GPrac_2 + Prac_2;
                                            GPort_2 = GPort_2 + Port_2;
                                            GSAT_2 = GSAT_2 + SAT_2;
                                            Gtotal_2 = Gtotal_2 + ObtMarks_2;
                                        }
                                        Response.Write("<td>" + Test1_2.ToString() + "</td><td>" + Test2_2.ToString() + "</td>");
                                        Response.Write("<td>" + percentle_2.ToString() + "</td><td>" + Prac_2.ToString() + "</td><td colspan='2'>" + Port_2.ToString() + "</td>");
                                        Response.Write("<td>" + SAT_2.ToString() + "</td><td>" + ObtMarks_2.ToString() + "</td>");
                                        Response.Write("<td>" + grade(ObtMarks1) + "</td>");
                                        Response.Write("<td>" + (ObtMarks1 + ObtMarks_2).ToString() + "</td>");
                                        Response.Write("<td>" + grade((ObtMarks1 + ObtMarks_2) / 2) + "</td>");
                                        Response.Write("</tr>");
                                    }
                                    if (ClassName == "X")
                                    {
                                        if (Test1_2 > 0 && MaxMarks1_2 > 0)
                                        {
                                            percentle_2 = ((Test1_2) * 5) / MaxMarks1_2;
                                        }
                                        bestofAE2 = (SAT_2 > SAT2_2 ? SAT_2 : SAT2_2);
                                        ObtMarks_2 = (double.Parse(percentle_2.ToString("0")) + double.Parse(Prac_2.ToString("0")) + double.Parse(bestofAE2.ToString("0")) + double.Parse(Port_2.ToString("0")));
                                        ObtMarks_2 = (percentle_2 + Prac_2 + bestofAE2 + Port_2);

                                            GTest1_2 = GTest1_2 + Test1_2;
                                            GTest2_2 = GTest2_2 + Test1_2;
                                            Gpercentle_2 = Gpercentle_2 + double.Parse(percentle_2.ToString());
                                            GPrac_2 = GPrac_2 + Prac_2;
                                            GPort_2 = GPort_2 + Port_2;
                                            GSAT_2 = GSAT_2 + SAT_2;
                                            Gtotal_2 = Gtotal_2 + ObtMarks_2;
                                        Response.Write("<td>" + Test1_2.ToString() + "</td><td>" + percentle_2.ToString() + "</td>");
                                        Response.Write("<td>" + Prac_2.ToString() + "</td><td colspan='2'>" + Port_2.ToString() + "</td>");
                                        Response.Write("<td>" + SAT_2.ToString() + "</td><td>" + SAT2_2.ToString() + "</td>");
                                        Response.Write("<td>" + ObtMarks_2.ToString() + "</td><td>" + grade(ObtMarks1) + "</td>");
                                        Response.Write("<td>" + (ObtMarks1 + ObtMarks_2).ToString() + "</td>");
                                        Response.Write("<td>" + grade((ObtMarks1 + ObtMarks_2) / 2) + "</td>");
                                        Response.Write("</tr>");
                                    }
                                }
                            }
                        }
                        if (ClassName == "IX")
                        {
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal.ToString("0") + "</td><td></td>");
                            Response.Write("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal_2.ToString("0") + "</td><td></td><td>" + (Gtotal + Gtotal_2).ToString("0") + "</td><td></td></tr>");
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Percentage</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ((Gtotal * 100) / (100 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ((Gtotal_2 * 100) / (100 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td><td>" + (((Gtotal + Gtotal_2) * 100) / (200 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td></tr>");
                        }
                        if (ClassName == "X")
                        {
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Total</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal.ToString("0") + "</td><td></td>");
                            Response.Write("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + Gtotal_2.ToString("0") + "</td><td></td><td>" + (Gtotal + Gtotal_2).ToString("0") + "</td><td></td></tr>");
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:23%; !important'>Percentage</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ((Gtotal * 100) / (100 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + ((Gtotal_2 * 100) / (100 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td><td>" + (((Gtotal + Gtotal_2) * 100) / (200 * dss.Tables[0].Rows.Count)).ToString("0.00") + "</td><td></td></tr>");
                        }
                        Response.Write("</tbody></table>");
                        Response.Write("</td></tr>");

                        

                        Response.Write("<tr><td colspan='21' class='text-center'>");
                        Response.Write("<br><table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        sql = "select MonthName from MonthMaster where ClassId=" + ClassId + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and CardType=" + ds.Tables[0].Rows[i]["CardId"].ToString() + " and MOD='I' and typeofAdd='Regular' and ForMonth=1";
                        var monthDt = oo.Fetchdata(sql);
                        string months = ""; string monthsblanks = "";
                        if (monthDt.Rows.Count > 0)
                        {
                            for (int n = 0; n < monthDt.Rows.Count; n++)
                            {
                                months = months + "<td>" + monthDt.Rows[n]["MonthName"].ToString() + "</td>";
                                monthsblanks = monthsblanks + "<td></td>";
                            }
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:19% !important'>Months</td><td>Arrear</td>" + months + "</tr>");
                            Response.Write("<tr class='p-pad-3 p-tot-tit'><td style='text-align:left; width:19% !important'>Fee Report</td><td></td>" + monthsblanks + "</tr>");
                        }

                        Response.Write("</tbody></table>");
                        Response.Write("</td></tr>");
                        Response.Write("<tr><td colspan='21' class='text-center'>");
                        Response.Write("<h3 style='text-align:center; width:100%; margin-top:15px !important;font-weight: bold; font-size: 13px !important;'>Activity Record</h3>");
                        Response.Write("<table class='table no-bm mp-table p-table-bordered table-bordered v-align-t'><tbody>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><th style='text-align:center; width:15%; !important'>Date</th><th style='text-align:center; width:50%; !important'>Activity</th><th style='text-align:center; width:15%; !important'>Grade</th></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td><br><br></td><td></td><td></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td><br><br></td><td></td><td></td></tr>");
                        Response.Write("<tr class='p-pad-3 p-tot-tit'><td><br><br></td><td></td><td></td></tr>");
                        Response.Write("</tbody></table>");
                        Response.Write("</td></tr>");

                        Response.Write("<tr><td colspan='21' class='text-center'>");
                        Response.Write("<div class='p-pad-3 p-tot-tit'>");
                        Response.Write("<ul style='list-style:none !important;text-align:left !important;'>");
                        Response.Write("<li>Note - In both Half Yearly and Annual Examination, marks of best Unit Test will be taken and converterd into 5 marks.</ li>");
                        Response.Write("</ul>");
                        Response.Write("</div>");
                        Response.Write("<div class='p-pad-3 p-tot-tit'>U.T.-Unit Test, * P.B.-Pre-Board, * L.A./P.-Internal Assisment/Practical, * M.M.-Maximum Marks, * M.O.-Mark Obtained, * O.P- Optional</div>");
                        Response.Write("</td></tr>");
                        Response.Write("</table>");
                        Response.Write("</div>");
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