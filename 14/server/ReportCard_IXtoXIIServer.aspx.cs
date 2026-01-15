using System;
using System.Data;
using System.Data.SqlClient;

public partial class ReportCard_IXtoXIIServer : System.Web.UI.Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string Term = Request.Form["Term"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string Status = Request.Form["Status"].ToString().Trim();
        string AttendanceType = Request.Form["AttendanceType"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "ICSEReportCard_IXtoXII";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", SessionName.Trim());
                if (SrNo.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                cmd.Parameters.AddWithValue("@status", Status);
                cmd.Parameters.AddWithValue("@action", "student");
                SqlDataAdapter das = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                das.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double PassingMark = 0;
                        string passm = "select PassingMark from ICSETestPermission where ClassID=" + ClassId + " and BranchId=" + BranchId + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                        double.TryParse(oo.ReturnTag(passm, "PassingMark"), out PassingMark);

                        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                        ss = ss + " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                        ss = ss + " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                        ss = ss + " where cm.id=" + ClassId + " and bm.id=" + BranchId + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='" + SessionName + "'";
                        string DisableTest1 = (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "");
                        string DisableTest2 = (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "");
                        string DisableTest3 = (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "");
                        string DisableTest4 = (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : "");
                        string DisableTest5 = (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : "");
                        string DisableTest6 = (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : "");
                        string monthlyTest1 = "", monthlyTest2 = "";
                        if (DisableTest1 == "hide" && DisableTest2 == "hide" && DisableTest3 == "hide")
                        {
                            monthlyTest1 = "hide";
                        }
                        if (DisableTest4 == "hide" && DisableTest5 == "hide" && DisableTest6 == "hide")
                        {
                            monthlyTest2 = "hide";
                        }

                        cmd.CommandText = "ICSEReportCard_IXtoXII";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", SessionName.Trim());
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@TermName", Term.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", dt.Rows[i]["ClassId"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchId", dt.Rows[i]["BranchId"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionId", dt.Rows[i]["SectionId"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@branchCode", BranchCode);
                        cmd.Parameters.AddWithValue("@status", Status);
                        cmd.Parameters.AddWithValue("@isManual", AttendanceType);
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                        try
                        {
                            Response.Write("<div  class='col-sm-12 box-border-solid-h-a3 text-uppercase' style='padding-top:0px; padding-right:5px; padding-bottom:0px; padding-left:0px;'>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<div id='break_div' class='term1 table-responsive' style='height:932px; page-break-after: always;'>");
                            }
                            else
                            {
                                Response.Write("<div id='break_div' class='term2 table-responsive' style='height:932px; page-break-after: always;'>");
                            }
                            Response.Write("<div  style='margin-top: 10px;' class='divHeader'></div>");

                            Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px; text-transform: uppercase  width:100%; border: 0 !important;'><tbody>");
                            Response.Write("<tr style='border: 0 !important;'><td class='p-pad-25' style='border: 0 !important; width:14%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>STUDENT'S NAME  </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["StudentName"].ToString().Trim() + "</span></td><td class='p-pad-25 ' style='border: 0 !important; font-weight:bold !important; width:22%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>CLASS & SECTION </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["CombineClassName"].ToString().Trim() + "</span></td></tr>");
                            Response.Write("<tr style='border: 0 !important;'><td class='p-pad-25' style='border: 0 !important; width:14%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>DATE OF BIRTH </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["dob"].ToString().Trim() + "</span></td><td class='p-pad-25 ' style='border: 0 !important; font-weight:bold !important; width:22%;'> <span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>S.R. NO. </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["admissionNo"].ToString().Trim() + "</span></td></tr>");
                            Response.Write("<tr style='border: 0 !important;'><td class='p-pad-25' style='border: 0 !important; width:14%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>FATHER'S NAME </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["Fathername"].ToString().Trim() + "</span></td><td class='p-pad-25' style='border: 0 !important; font-weight:bold !important; width:22%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>PHONE NUMBER </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["FatherContactNo"].ToString().Trim() + "</span></td></tr>");
                            Response.Write("<tr style='border: 0 !important;'><td class='p-pad-25'  style='border: 0 !important; width:14%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>MOTHER'S NAME </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["MotherName"].ToString().Trim() + "</span></td><td class='p-pad-25' style='border: 0 !important; font-weight:bold !important; width:22%;'><span  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:normal !important;'>CLASS TEACHER'S NAME </td><td style='border: 0 !important; font-weight:normal !important; width:2%;'>:</td><td  class='txt-rep-title-12-b customtext' style='border: 0 !important; font-weight:bold !important; width:30%;'>" + dt.Rows[i]["EmpName"].ToString().Trim() + "</span></td></tr>");
                            Response.Write("<tr>");
                            Response.Write("<td colspan='6' style='padding: 5px !important;'>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<table class='tables table term1 mp-table p-table-bordered table-bordered text_center' style='margin-bottom: 5px; margin-top: 5px; width:100%;'><tbody>");
                                Response.Write("<tr class='text_center'></tr>");
                                Response.Write("<tr><th class='text_center' <th style='font-size:9px !important; vertical-align: middle !important;' rowspan='3'>Subject</th><th colspan='6' style='font-size:9px !important;  vertical-align: top;'>Term-I</th></tr>");
                                Response.Write("<tr class='text_center'><th style='font-size:9px !important; vertical-align: top;' colspan='2'>THEORY</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>PROJECT/PRACTICAL</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>TOTAL</th></tr>");
                                Response.Write("<tr><th>MM</th><th>MO</th><th>MM</th><th>MO</th><th>MM</th><th>MO</th></tr>");
                            }
                            else
                            {
                                Response.Write("<table class='tables table term2 mp-table p-table-bordered table-bordered text_center' style='margin-bottom: 5px; margin-top: 5px; width:100%;'><tbody>");
                                Response.Write("<tr class='text_center'></tr>");
                                Response.Write("<tr><th class='text_center' style='font-size:9px !important;  vertical-align: middle;' rowspan='3'>Subject</th><th colspan='6' style='font-size:9px !important;  vertical-align: top;'>Term-I</th><th colspan='6' style='font-size:9px !important;  vertical-align: top;'>Term-II</th><th class='text_center' style='font-size:9px !important;  vertical-align: middle;' rowspan='3'>GRAND</br>TOTAL</br>(200)</th></tr>");
                                Response.Write("<tr class='text_center'><th style='font-size:9px !important; vertical-align: top;' colspan='2'>THEORY</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>PROJECT/PRACTICAL</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>TOTAL</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>THEORY</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>PROJECT</th><th style='font-size:9px !important; vertical-align: top;' colspan='2'>TOTAL</th></tr>");
                                Response.Write("<tr><th>MM</th><th>MO</th><th>MM</th><th>MO</th><th>MM</th><th>MO</th><th>MM</th><th>MO</th><th>MM</th><th>MO</th><th>MM</th><th>MO</th></tr>");
                            }
                            string FailPassStatus1 = ""; string FailPassStatus2 = ""; // if null then pass else fail;
                            double HYTotal = 0, HYMaxTotal = 0, AETotal = 0, AEMaxTotal = 0, GtotalTotal=0;
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    for (int m = 0; m < ds.Tables[0].Rows.Count; m++)
                                    {
                                        double HY = 0, PracHY = 0, MaxHY = 0, MaxPracHY = 0, totalMaxHY = 0, totalHY = 0;
                                        double.TryParse(ds.Tables[0].Rows[m]["HY"].ToString(), out HY);
                                        double.TryParse(ds.Tables[0].Rows[m]["PracHY"].ToString(), out PracHY);
                                        double.TryParse(ds.Tables[0].Rows[m]["MaxHY"].ToString(), out MaxHY);
                                        double.TryParse(ds.Tables[0].Rows[m]["MaxPracHY"].ToString(), out MaxPracHY);
                                        totalMaxHY = MaxHY + MaxPracHY;
                                        totalHY = HY + PracHY;
                                        HYTotal = HYTotal + totalHY;
                                        HYMaxTotal = HYMaxTotal + totalMaxHY;
                                        if (((HY * 100) / MaxHY) < PassingMark)
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        if (((PracHY * 100) / MaxPracHY) < PassingMark)
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        Response.Write("<tr><td style='font-weight: bold;'>" + ds.Tables[0].Rows[m]["PaperName"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["MaxHY"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["HY"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["MaxPracHY"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["PracHY"].ToString() + "</td><td class='text-center'>" + totalMaxHY.ToString("0.0") + "</td><td class='text-center'>" + totalHY.ToString("0.0") + "</td></tr>");
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < ds.Tables[0].Rows.Count; m++)
                                    {
                                        double HY = 0, PracHY = 0, MaxHY = 0, MaxPracHY = 0, totalMaxHY = 0, totalHY = 0, AE = 0, PracAE = 0, MaxAE = 0, MaxPracAE = 0, totalMaxAE = 0, totalAE = 0, Gtotal=0;
                                        double.TryParse(ds.Tables[0].Rows[m]["HY"].ToString(), out HY);
                                        double.TryParse(ds.Tables[0].Rows[m]["PracHY"].ToString(), out PracHY);
                                        double.TryParse(ds.Tables[0].Rows[m]["MaxHY"].ToString(), out MaxHY);
                                        double.TryParse(ds.Tables[0].Rows[m]["MaxPracHY"].ToString(), out MaxPracHY);
                                        double.TryParse(ds.Tables[0].Rows[m]["AE"].ToString(), out AE);
                                        double.TryParse(ds.Tables[0].Rows[m]["PracAE"].ToString(), out PracAE);
                                        double.TryParse(ds.Tables[0].Rows[m]["MaxAE"].ToString(), out MaxAE);
                                        double.TryParse(ds.Tables[0].Rows[m]["MaxPracAE"].ToString(), out MaxPracAE);
                                        totalMaxHY = MaxHY + MaxPracHY;
                                        totalHY = HY + PracHY;
                                        totalMaxAE = MaxAE + MaxPracAE;
                                        totalAE = AE + PracAE;
                                        Gtotal = totalHY + totalAE;
                                        HYTotal = HYTotal + totalHY;
                                        HYMaxTotal = HYMaxTotal + totalMaxHY;
                                        AETotal = AETotal + totalAE;
                                        AEMaxTotal = AEMaxTotal + totalMaxAE;
                                        GtotalTotal = HYTotal + AETotal;
                                        if (((HY * 100) / MaxHY) < PassingMark)
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        if (((PracHY * 100) / MaxPracHY) < PassingMark)
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        if (((AE * 100) / MaxAE) < PassingMark)
                                        {
                                            FailPassStatus2 = FailPassStatus2 + "FAILED";
                                        }
                                        if (((PracAE * 100) / MaxPracAE) < PassingMark)
                                        {
                                            FailPassStatus2 = FailPassStatus2 + "FAILED";
                                        }
                                        Response.Write("<tr><td style='font-weight: bold;'>" + ds.Tables[0].Rows[m]["PaperName"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["MaxHY"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["HY"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["MaxPracHY"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["PracHY"].ToString() + "</td><td class='text-center'>" + totalMaxHY.ToString("0.0") + "</td><td class='text-center'>" + totalHY.ToString("0.0") + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["MaxAE"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["AE"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["MaxPracAE"].ToString() + "</td><td class='text-center'>" + ds.Tables[0].Rows[m]["PracAE"].ToString() + "</td><td class='text-center'>" + totalMaxAE.ToString("0.0") + "</td><td class='text-center'>" + totalAE.ToString("0.0") + "</td><td class='text-center'>" + Gtotal.ToString("0.0") + "</td></tr>");
                                    }
                                }
                            }
                            double Term1Per = 0;
                            double Term2Per = 0;
                            if (HYMaxTotal > 0)
                            {
                                double.TryParse(((HYTotal * 100) / HYMaxTotal).ToString(), out Term1Per);
                            }
                            if (AEMaxTotal>0)
                            {
                                double.TryParse(((AETotal * 100) / AEMaxTotal).ToString(), out Term2Per);
                            }
                            Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'></span></td>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<td colspan='4' class='text-center'><span class='sentence-case'  style='font-weight: bold;'>Total</span></td><td class='text-center'>" + HYMaxTotal.ToString("0.0")+ "</td><td>" + HYTotal.ToString("0.0") + "</td></tr>");
                            }
                            else
                            {
                                Response.Write("<td colspan='4' class='text-center'><span class='sentence-case'  style='font-weight: bold;'>Total</span></td><td class='text-center'>" + HYMaxTotal.ToString("0.0") + "</td><td class='text-center'>" + HYTotal.ToString("0.0") + "</td><td colspan='4' class='text-center'><span class='sentence-case' >Total</span></td><td class='text-center'>" + AEMaxTotal.ToString("0.0") + "</td><td class='text-center'>" + AETotal.ToString("0.0") + "</td><td class='text-center'>" + GtotalTotal.ToString("0.0") + "</td></tr>");
                            }
                            Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Percentage</span></td>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >"+ Term1Per.ToString("0.0") + " %</span></td></tr>");
                            }
                            else
                            {
                                Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + Term1Per.ToString("0.0") + " %</span></td><td colspan='6' class='text-center'>" + Term2Per.ToString("0.0") + " %</span></td><td></td></tr>");

                            }
                            if (ds.Tables[1].Rows.Count>0)
                            {
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>SUPW (Grades)</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + ds.Tables[1].Rows[0]["Remark"].ToString().Trim() + "</span></td></tr>");
                                }
                                if (Term.ToLower() == "Term2".ToLower())
                                {
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>SUPW (Grades)</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + ds.Tables[1].Rows[0]["Remark"].ToString().Trim() + "</span></td><td colspan='6' class='text-center'>" +(ds.Tables[2].Rows.Count>0? ds.Tables[2].Rows[0]["Remark"].ToString().Trim():"") + "</span></td><td></td></tr>");
                                }
                            }
                            if (ds.Tables[7].Rows.Count>0)
                            {
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    double totaldays = 0, attendence = 0, percent = 0; string t1Att = "";
                                    totaldays = double.Parse(ds.Tables[7].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[7].Rows[0]["totaldays"].ToString().Trim());
                                    attendence = double.Parse(ds.Tables[7].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[7].Rows[0]["attendence"].ToString().Trim());
                                    if (attendence != 0 && totaldays != 0)
                                    {
                                        percent = (attendence * 100) / totaldays;
                                        t1Att = "<span >" + attendence.ToString().Trim() + "</span>/<span >" + totaldays.ToString().Trim() + "</span> (<span >" + percent.ToString("0.00") + "</span> %)";
                                    }
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Attendance %</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case'>" + t1Att + "</span></td></tr>");
                                }
                            }

                            if (ds.Tables[8].Rows.Count > 0)
                            {
                                if (Term.ToLower() == "Term2".ToLower())
                                {

                                    double totaldays1 = 0, attendence1 = 0, percent1 = 0; string t1Att = ""; string t2Att = ""; string AllAtt = "";
                                    totaldays1 = double.Parse(ds.Tables[7].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[7].Rows[0]["totaldays"].ToString().Trim());
                                    attendence1 = double.Parse(ds.Tables[7].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[7].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence1 != 0 && totaldays1 != 0)
                                    {
                                        percent1 = (attendence1 * 100) / totaldays1;
                                        t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0.00") + "</span> %)";
                                    }
                                    double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                    totaldays2 = double.Parse(ds.Tables[8].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[8].Rows[0]["totaldays"].ToString().Trim());
                                    attendence2 = double.Parse(ds.Tables[8].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[8].Rows[0]["attendence"].ToString().Trim());

                                    if (attendence2 != 0 && totaldays2 != 0)
                                    {
                                        percent2 = (attendence2 * 100) / totaldays2;
                                        t2Att = "<span >" + attendence2.ToString().Trim() + "</span>/<span >" + totaldays2.ToString().Trim() + "</span> (<span >" + percent2.ToString("0.00") + "</span> %)";
                                    }
                                    double Gtotaldays = 0, Gattendence = 0, Gpercent = 0;
                                    Gtotaldays = (totaldays1 + totaldays2);
                                    Gattendence = (attendence1 + attendence2);
                                    if (Gattendence != 0 && Gtotaldays != 0)
                                    {
                                        Gpercent = (Gattendence * 100) / Gtotaldays;
                                    }
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Attendance %</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case'>" + t1Att + "</span></td><td colspan='6' class='text-center'><span class='sentence-case'>" + t2Att + "</span></td><td></td></tr>");
                                }
                            }
                            
                            //result

                            Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Result</span></td>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' ><b>" + (FailPassStatus1 == "" ? "PASSED" : "FAILED") + "</b></span></td></tr>");
                            }
                            else
                            {
                                Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' ><b>" + (FailPassStatus1 == "" ? "PASSED" : "FAILED") + "</b></span></td><td colspan='6' class='text-center'><b>" + (FailPassStatus2 == "" ? "PASSED" : "FAILED") + "</b></span></td><td></td></tr>");

                            }
                            if (ds.Tables[3].Rows.Count>0)
                            {
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Conduct</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + ds.Tables[3].Rows[0]["Remark"].ToString().Trim() + "</span></td></tr>");
                                }
                                if (Term.ToLower() == "Term2".ToLower())
                                {
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Conduct</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + ds.Tables[3].Rows[0]["Remark"].ToString().Trim() + "</span></td><td colspan='6' class='text-center'>" +(ds.Tables[4].Rows.Count>0? ds.Tables[4].Rows[0]["Remark"].ToString().Trim():"") + "</span></td><td></td></tr>");
                                }
                            }
                            if (ds.Tables[5].Rows.Count > 0)
                            {
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Remark</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + ds.Tables[5].Rows[0]["Remark"].ToString().Trim() + "</span></td></tr>");
                                }
                                if (Term.ToLower() == "Term2".ToLower())
                                {
                                    Response.Write("<tr><td><span style='font-weight: bold;font-size: 11px;'>Remark</span></td>");
                                    Response.Write("<td colspan='6' class='text-center'><span class='sentence-case' >" + ds.Tables[5].Rows[0]["Remark"].ToString().Trim() + "</span></td><td colspan='6' class='text-center'>" + (ds.Tables[6].Rows.Count>0?ds.Tables[6].Rows[0]["Remark"].ToString().Trim():"") + "</span></td><td></td></tr>");
                                }
                            }

                            Response.Write("<table class='tables table mp-table p-table-bordered table-bordered' style='margin-bottom: 5px;border:0px !important;'><tbody>");
                            Response.Write("<tr><td style='padding-top: 5px !important;width: 100%%;border:0px !important; text-align:left;'><span style='font-weight: bold;font-size:12px !important; text-transform: initial !important'>Rules:</td></tr>");
                            Response.Write("<tr><td style='padding-top: 5px !important;width: 100%%;border:0px !important; text-align:left;'><span style='font-weight: bold;font-size:12px !important; text-transform: initial !important'>1. Student’s promotion depends on the daily performance throughout the year.<br>");
                            Response.Write("2. 80% attendance is compulsory for students to appear in the examination.<br>");
                            Response.Write("3. Report card will not be given if fee is in arrear.<br>");
                            Response.Write("4. For promotion, an average of 40% marks is must and passing in English is compulsory.<br>");
                            Response.Write("5. Promotion refused will not be reconsidered.<br>");
                            Response.Write("6. If this Report Card is lost, then a fine of Rs. 100/- will be charged for the duplicate Report Card.</td></tr>");
                            Response.Write("</tbody></table>");


                            if (Session["Logintype"].ToString() != "Guardian")
                            {
                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;border:0px !important;'><tbody><tr>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>CLASS TEACHER'S SIGNATURE</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>CHECKED BY</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>PRINCIPAL'S SIGNATURE</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>PARENT'S/GUARDIAN'S SIGNATURE</div></td>");
                                Response.Write("</tr></tbody></table>");

                                Response.Write("<table class='tables hide table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;border:0px !important;'><tbody>");
                                Response.Write("<tr><td style='padding-top: 5px !important;width: 100%%;border:0px !important; text-align:center;'><span style='font-weight: bold;font-size:9px !important;'> GRADE : &nbsp;&nbsp;A-80% and above (Outstanding); &nbsp;&nbsp;&nbsp;&nbsp;B-60% to 79% (Good); &nbsp;&nbsp;&nbsp;&nbsp;50% to 59% (Satisfactory); &nbsp;&nbsp;&nbsp;&nbsp;D-Below 49% (Need special attention)</span></td></tr>");
                                Response.Write("</tbody></table>");
                            }
                            Response.Write("</div>");

                            Response.Write("</td>");
                            Response.Write("</tr>");
                            Response.Write("</tbody></table>");
                            if (Session["Logintype"].ToString() == "Guardian")
                            {
                                Response.Write("<div class='text-center'>This is an electronically generated report card through Parent Portal.</div>");
                            }
                            Response.Write("</div></div>");
                        }
                        catch (SqlException ex)
                        {
                        }
                    }
                }
            }
        }
    }
}