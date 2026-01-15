using System;
using System.Data;
using System.Data.SqlClient;

public partial class ReportCard_ItoVIIIServer : System.Web.UI.Page
{
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        string SrNo = Request.Form["SrNo"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
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
                cmd.CommandText = "ICSEReportCard_ItoVIII";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionName", SessionName.Trim());
                if (SrNo.Trim() != "")
                {
                    cmd.Parameters.AddWithValue("@SrNo", SrNo.Trim());
                }
                cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
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
                        string passm = "select PassingMark from ICSETestPermission where ClassID=" + ClassId + " and BranchId=" + dt.Rows[i]["BranchId"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                        double.TryParse(oo.ReturnTag(passm, "PassingMark"), out PassingMark);

                        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                        ss = ss + " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                        ss = ss + " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                        ss = ss + " where cm.id=" + ClassId + " and bm.id=" + dt.Rows[i]["BranchId"].ToString() + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='" + SessionName + "'";
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

                        cmd.CommandText = "ICSEReportCard_ItoVIII";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", SessionName.Trim());
                        cmd.Parameters.AddWithValue("@SrNo", dt.Rows[i]["admissionNo"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SectionName", SectionName.Trim());
                        cmd.Parameters.AddWithValue("@TermName", Term.Trim());
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
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
                                Response.Write("<tr><th class='text_center' <th style='font-size:9px !important; vertical-align: middle !important;' rowspan='2'>Subject</th><th colspan='" + (monthlyTest1 == "hide" ? 2 : 3) + "' style='font-size:9px !important;  vertical-align: top;'>Term-1(100 Marks)</th></tr>");
                                Response.Write("<tr class='text_center'><th style='font-size:9px !important; vertical-align: top;'>HALF YEARLY EXAM<br>(Max Marks 80)</th><th style='font-size:9px !important; vertical-align: top;' class='" + monthlyTest1 + "'>MONTHLY TEST<br>(Max Marks 20)</th><th style='font-size:9px !important; vertical-align: top;'>AGGREGATE<br>(Max Marks 100)</th></tr>");
                            }
                            else
                            {
                                Response.Write("<table class='tables table term2 mp-table p-table-bordered table-bordered text_center' style='margin-bottom: 5px; margin-top: 5px; width:100%;'><tbody>");
                                Response.Write("<tr class='text_center'></tr>");
                                Response.Write("<tr><th class='text_center' style='font-size:9px !important;  vertical-align: middle;' rowspan='2'>Subject</th><th colspan='" + (monthlyTest1 == "hide" ? 2 : 3) + "' style='font-size:9px !important;  vertical-align: top;'>Term-1(100 Marks)</th><th colspan='" + (monthlyTest2 == "hide" ? 3 : 4) + "' style='font-size:9px !important;  vertical-align: top;'>Term-2(100 Marks)</th></tr>");
                                Response.Write("<tr class='text_center'><th style='font-size:9px !important; vertical-align: top;'>HALF YEARLY EXAM<br>(Max Marks 80)</th><th style='font-size:9px !important; vertical-align: top;' class='" + monthlyTest1 + "'>MONTHLY TEST<br>(Max Marks 20)</th><th style='font-size:9px !important; vertical-align: top;'>AGGREGATE<br>(Max Marks 100)</th><th style='font-size:9px !important; vertical-align: top;'>ANNUAL EXAM<br>(Max Marks 80)</th><th style='font-size:9px !important; vertical-align: top;' class='" + monthlyTest2 + "'>MONTHLY TEST<br>(Max Marks 20)</th><th style='font-size:9px !important; vertical-align: top;'>AGGREGATE<br>(Max Marks 100)</th><th style='font-size:9px !important; vertical-align: top;'>TOTAL<br>(Max Marks 200)</th></tr>");
                            }

                            string sql2 = "select id, SubjectName from ( ";
                            sql2 = sql2 + " SELECT Id, SubjectName, SubjectType FROM TTSubjectMaster where applicablefor<>'TimeTable' and SubjectType='Compulsory' ";
                            sql2 = sql2 + " and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "' ";
                            sql2 = sql2 + " union all ";
                            sql2 = sql2 + " SELECT sm.Id, SubjectName, SubjectType FROM TTSubjectMaster sm  ";
                            sql2 = sql2 + " inner join ICSEOptionalSubjectAllotment me on me.OptSubjectId=sm.id and me.SessionName=sm.SessionName and me.BranchCode=sm.BranchCode ";
                            sql2 = sql2 + " where applicablefor<>'TimeTable' and SubjectType='Optional' and sm.classid=" + ClassId + " and sm.branchid=" + dt.Rows[i]["branchid"].ToString() + "  ";
                            sql2 = sql2 + " and sm.BranchCode=" + BranchCode + " and sm.SessionName='" + SessionName + "' and srNo='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "' ";
                            sql2 = sql2 + " )T1 ";
                            var dtSubject = oo.Fetchdata(sql2);
                            string FailPassStatus1 = ""; string FailPassStatus2 = ""; // if null then pass else fail;
                            double totalHYs = 0, totalTestHYs = 0, totalAggHYs= 0, totalAEs = 0, totalTestAEs = 0, totalAggAEs = 0, totalGtotals = 0;
                            double Term1TotalMax = 0, Term2TotalMax = 0;
                            for (int s = 0; s < dtSubject.Rows.Count; s++)
                            {
                                double HY = 0, BestOfT1T2T3 = 0, AggreGateHY = 0, AE = 0, BestOfT4T5T6 = 0, AggreGateAE = 0;
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    string sql3 = "SELECT Id, PaperName FROM TTPaperMaster where subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                    var dtPaper = oo.Fetchdata(sql3);
                                    if (dtPaper.Rows.Count > 1)
                                    {
                                        Response.Write("<tr class='text_center'><td><span >" + dtSubject.Rows[s]["SubjectName"].ToString().Trim() + "</span></td><td></td><td class='"+ monthlyTest1 + "'></td><td></td></tr>");
                                        string totalStr = ""; double totalHY = 0, TotalBestOfT1T2T3 = 0, TotalAggreGateHY = 0;
                                        for (int p = 0; p < dtPaper.Rows.Count; p++)
                                        {
                                            string sql = "SELECT HY, BestOfT1T2T3, AggreGateHY FROM ICSEMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                            sql = sql + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[p]["Id"].ToString() + " and srno='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "' and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                            var dtMark = oo.Fetchdata(sql);
                                            if (dtMark.Rows.Count > 0)
                                            {
                                                double.TryParse(dtMark.Rows[0]["HY"].ToString(), out HY);
                                                
                                                double.TryParse(dtMark.Rows[0]["AggreGateHY"].ToString(), out AggreGateHY);
                                            }

                                            if (dtMark.Rows.Count > 0)
                                            {
                                                totalHY = totalHY + HY;
                                                TotalAggreGateHY = TotalAggreGateHY + HY;
                                                Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>&nbsp;(" + (p + 1) + ") " + dtPaper.Rows[p]["PaperName"].ToString().Trim() + "</span></td><td style='text-align: center;'><span>" + (dtMark.Rows[0]["HY"].ToString() == "" ? "" : HY.ToString("0.0")) + "</span></td><td colspan='" + (monthlyTest1=="hide"?1:2) + "' style='border-top: 0px !important; border-bottom: 0px !important;'></td></tr>");
                                            }
                                            else
                                            {
                                                Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>&nbsp;(" + (p + 1) + ") " + dtPaper.Rows[p]["PaperName"].ToString().Trim() + "</span></td><td></td><td colspan='" + (monthlyTest1 == "hide" ? 1 : 2) + "' style='border-top: 0px !important; border-bottom: 0px !important;'></td></tr>");
                                            }
                                            if (p != dtPaper.Rows.Count - 1)
                                            {
                                                totalStr = totalStr + "(" + (p + 1) + ")+";
                                            }
                                            else
                                            {
                                                totalStr = totalStr + "(" + (p + 1) + ")";
                                            }
                                            string sqlss = "SELECT MaxHY, MaxAE FROM ICSEMaxMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                            sqlss = sqlss + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[p]["Id"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                            double MaxHY = 0;
                                            double.TryParse(oo.ReturnTag(sqlss, "MaxHY"), out MaxHY);
                                            if (((HY*100)/MaxHY)<PassingMark)
                                            {
                                                FailPassStatus1 = FailPassStatus1 +"Fail";
                                            }
                                            
                                        }
                                        Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>Total " + totalStr + "</span></td><td style='text-align: center'><span >" + totalHY.ToString("0.0") + "</span></td><td style='text-align: center' class='" + monthlyTest1 + "'><span ></span></td><td style='text-align: center'><span >" + TotalAggreGateHY.ToString("0.0") + "</span></td></tr>");
                                        totalHYs = totalHYs + totalHY; totalAggHYs = totalAggHYs + TotalAggreGateHY;
                                        Term1TotalMax = Term1TotalMax + 100;
                                    }
                                    if (dtPaper.Rows.Count == 1)
                                    {
                                        string sql = "SELECT HY, BestOfT1T2T3, AggreGateHY FROM ICSEMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                        sql = sql + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[0]["Id"].ToString() + " and srno='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "' and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                        var dtMark = oo.Fetchdata(sql);
                                        if (dtMark.Rows.Count > 0)
                                        {
                                            double.TryParse(dtMark.Rows[0]["HY"].ToString(), out HY);
                                            if (monthlyTest1 != "hide")
                                            {
                                                double.TryParse(dtMark.Rows[0]["BestOfT1T2T3"].ToString(), out BestOfT1T2T3);
                                            }
                                            if (monthlyTest1 != "hide")
                                            {
                                                double.TryParse(dtMark.Rows[0]["AggreGateHY"].ToString(), out AggreGateHY);
                                            }
                                            else
                                            {
                                                double.TryParse(dtMark.Rows[0]["HY"].ToString(), out AggreGateHY);
                                            }
                                        }
                                        if (dtMark.Rows.Count > 0)
                                        {
                                            totalHYs = totalHYs + HY; totalTestHYs = totalTestHYs + BestOfT1T2T3; totalAggHYs = totalAggHYs + AggreGateHY;
                                            Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>" + dtPaper.Rows[0]["PaperName"].ToString().Trim() + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["HY"].ToString() == "" ? "" : HY.ToString("0.0")) + "</span></td><td style='text-align: center' class='" + monthlyTest1 + "'><span >" + (dtMark.Rows[0]["BestOfT1T2T3"].ToString() == "" ? "" : BestOfT1T2T3.ToString("0.0")) + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["AggreGateHY"].ToString() == "" ? "" : AggreGateHY.ToString("0.0")) + "</span></td></tr>");
                                        }
                                        else
                                        {
                                            Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>" + dtPaper.Rows[0]["PaperName"].ToString().Trim() + "</span></td><td></td><td class='" + monthlyTest1 + "'></td><td></td></tr>");
                                        }
                                        string sqlss = "SELECT MaxHY, MaxAE FROM ICSEMaxMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                        sqlss = sqlss + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[0]["Id"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                        double MaxTestHY = 0, MaxHY = 0;
                                        double.TryParse(oo.ReturnTag(sqlss, "MaxHY"), out MaxHY);
                                        MaxTestHY = (100 - MaxHY) / dtPaper.Rows.Count;
                                        if (((BestOfT1T2T3 * 100) / MaxTestHY) < PassingMark && monthlyTest1 != "hide")
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        if (((HY * 100) / MaxHY) < PassingMark)
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        
                                    }
                                    Term1TotalMax = Term1TotalMax + 100;
                                }
                                if (Term.ToLower() == "Term2".ToLower())
                                {
                                    string sql3 = "SELECT Id, PaperName FROM TTPaperMaster where subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and classid=" + ClassId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                    var dtPaper = oo.Fetchdata(sql3);
                                    if (dtPaper.Rows.Count > 1)
                                    {
                                        Response.Write("<tr class='text_center'><td><span >" + dtSubject.Rows[s]["SubjectName"].ToString().Trim() + "</span></td><td></td><td class='" + monthlyTest1 + "'></td><td></td><td class='" + monthlyTest2 + "'></td><td></td><td></td><td></td></tr>");
                                        string totalStr = ""; double totalHY = 0, TotalBestOfT1T2T3 = 0, TotalAggreGateHY = 0, totalAE = 0, TotalBestOfT4T5T6 = 0, TotalAggreGateAE = 0;
                                        double TotalstrTotal = 0;
                                        for (int p = 0; p < dtPaper.Rows.Count; p++)
                                        {
                                            string sql = "SELECT HY, BestOfT1T2T3, AggreGateHY, AE, BestOfT4T5T6, AggreGateAE FROM ICSEMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                            sql = sql + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[p]["Id"].ToString() + " and srno='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "' and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                            var dtMark = oo.Fetchdata(sql);
                                            if (dtMark.Rows.Count > 0)
                                            {
                                                double.TryParse(dtMark.Rows[0]["HY"].ToString(), out HY);
                                                
                                                double.TryParse(dtMark.Rows[0]["AggreGateHY"].ToString(), out AggreGateHY);
                                                double.TryParse(dtMark.Rows[0]["AE"].ToString(), out AE);
                                                
                                                double.TryParse(dtMark.Rows[0]["AggreGateAE"].ToString(), out AggreGateAE);
                                            }

                                            if (dtMark.Rows.Count > 0)
                                            {
                                                totalHY = totalHY + HY; TotalBestOfT1T2T3 = TotalBestOfT1T2T3 + BestOfT1T2T3; TotalAggreGateHY = TotalAggreGateHY + HY;
                                                totalAE = totalAE + AE; TotalBestOfT4T5T6 = TotalBestOfT4T5T6 + BestOfT4T5T6; TotalAggreGateAE = TotalAggreGateAE + AE;
                                                Response.Write("<tr class='text_center'><td><span  style='font-weight: bold;'>&nbsp;(" + (p + 1) + ") " + dtPaper.Rows[p]["PaperName"].ToString().Trim() + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["HY"].ToString() == "" ? "" : HY.ToString("0.0")) + "</span></td><td colspan='"+ (monthlyTest1=="hide"?1:2) + "' style='border-top: 0px !important; border-bottom: 0px !important;'></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["AE"].ToString() == "" ? "" : AE.ToString("0.0")) + "</span></td><td colspan='"+ (monthlyTest2 == "hide" ? 1 : 2) + "' style='border-top: 0px !important; border-bottom: 0px !important;'></td><td></td></tr>");
                                            }
                                            else
                                            {
                                                Response.Write("<tr class='text_center'><td><span  style='font-weight: bold;'>&nbsp;(" + (p + 1) + ") " + dtPaper.Rows[p]["PaperName"].ToString().Trim() + "</span></td><td></td><td colspan='" + (monthlyTest1 == "hide" ? 1 : 2) + "' style='border-top: 0px !important; border-bottom: 0px !important;'></td><td></td><td colspan='" + (monthlyTest2 == "hide" ? 1 : 2) + "' style='border-top: 0px !important; border-bottom: 0px !important;'></td><td></td></tr>");
                                            }
                                            if (p != dtPaper.Rows.Count - 1)
                                            {
                                                totalStr = totalStr + "(" + (p + 1) + ")+";
                                            }
                                            else
                                            {
                                                totalStr = totalStr + "(" + (p + 1) + ")";
                                            }

                                            string sqlss = "SELECT MaxHY, MaxAE FROM ICSEMaxMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                            sqlss = sqlss + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[p]["Id"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                            double MaxHY = 0, MaxAE = 0;
                                            double.TryParse(oo.ReturnTag(sqlss, "MaxHY"), out MaxHY);
                                            double.TryParse(oo.ReturnTag(sqlss, "MaxAE"), out MaxAE);
                                            if (((HY * 100) / MaxHY) < PassingMark)
                                            {
                                                FailPassStatus1 = FailPassStatus1 + "FAILED";
                                            }
                                            if (((AE * 100) / MaxAE) < PassingMark)
                                            {
                                                FailPassStatus2 = FailPassStatus2 + "FAILED";
                                            }
                                        }
                                        TotalstrTotal = TotalstrTotal + (TotalAggreGateHY + TotalAggreGateAE);
                                        Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>" + totalStr + "</span></td><td style='text-align: center'><span >" + totalHY.ToString("0.0") + "</span></td><td style='text-align: center' class='" + monthlyTest1 + "'><span ></span></td><td style='text-align: center'><span >" + TotalAggreGateHY.ToString("0.0") + "</span></td><td style='text-align: center'><span >" + totalAE.ToString("0.0") + "</span></td><td style='text-align: center' class='" + monthlyTest2 + "'><span ></span></td><td style='text-align: center'><span >" + TotalAggreGateAE.ToString("0.0") + "</span></td><td style='text-align: center'><span >" + TotalstrTotal.ToString("0.0") + "</span></td></tr>");
                                        totalHYs = totalHYs + totalHY; totalAggHYs = totalAggHYs + TotalAggreGateHY; totalAEs = totalAEs + totalAE; totalAggAEs = totalAggAEs + TotalAggreGateAE; totalGtotals = totalGtotals + TotalstrTotal;
                                        Term1TotalMax = Term1TotalMax + 100; Term2TotalMax = Term2TotalMax + 100;
                                    }
                                    if (dtPaper.Rows.Count == 1)
                                    {
                                        string sql = "SELECT HY, BestOfT1T2T3, AggreGateHY, AE, BestOfT4T5T6, AggreGateAE  FROM ICSEMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                        sql = sql + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[0]["Id"].ToString() + " and srno='" + dt.Rows[i]["admissionNo"].ToString().Trim() + "' and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                        var dtMark = oo.Fetchdata(sql);
                                        if (dtMark.Rows.Count > 0)
                                        {
                                            double.TryParse(dtMark.Rows[0]["HY"].ToString(), out HY);
                                            if (monthlyTest1 != "hide")
                                            {
                                                double.TryParse(dtMark.Rows[0]["BestOfT1T2T3"].ToString(), out BestOfT1T2T3);
                                            }
                                            if (monthlyTest1 != "hide")
                                            {
                                                double.TryParse(dtMark.Rows[0]["AggreGateHY"].ToString(), out AggreGateHY);
                                            }
                                            else
                                            {
                                                double.TryParse(dtMark.Rows[0]["HY"].ToString(), out AggreGateHY);
                                            }
                                            double.TryParse(dtMark.Rows[0]["AE"].ToString(), out AE);
                                            if (monthlyTest2 != "hide")
                                            {
                                                double.TryParse(dtMark.Rows[0]["BestOfT4T5T6"].ToString(), out BestOfT4T5T6);
                                            }
                                            if (monthlyTest2 != "hide")
                                            {
                                                double.TryParse(dtMark.Rows[0]["AggreGateAE"].ToString(), out AggreGateAE);
                                            }
                                            else
                                            {
                                                double.TryParse(dtMark.Rows[0]["AE"].ToString(), out AggreGateAE);
                                            }
                                            
                                        }
                                        if (dtMark.Rows.Count > 0)
                                        {
                                            totalHYs = totalHYs + HY; totalTestHYs = totalTestHYs + BestOfT1T2T3; totalAggHYs = totalAggHYs + AggreGateHY;
                                            totalAEs = totalAEs + AE; totalTestAEs = totalTestAEs + BestOfT4T5T6; totalAggAEs = totalAggAEs + AggreGateAE;
                                            string Total = (AggreGateHY + AggreGateAE).ToString("0.00");
                                            totalGtotals = totalGtotals + (AggreGateHY + AggreGateAE);
                                            Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>" + dtPaper.Rows[0]["PaperName"].ToString().Trim() + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["HY"].ToString() == "" ? "" : HY.ToString("0.0")) + "</span></td><td style='text-align: center' class='" + monthlyTest1 + "'><span >" + (dtMark.Rows[0]["BestOfT1T2T3"].ToString() == "" ? "" : BestOfT1T2T3.ToString("0.0")) + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["AggreGateHY"].ToString() == "" ? "" : AggreGateHY.ToString("0.0")) + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["AE"].ToString() == "" ? "" : AE.ToString("0.0")) + "</span></td><td style='text-align: center' class='" + monthlyTest2 + "'><span >" + (dtMark.Rows[0]["BestOfT4T5T6"].ToString() == "" ? "" : BestOfT4T5T6.ToString("0.0")) + "</span></td><td style='text-align: center'><span >" + (dtMark.Rows[0]["AggreGateAE"].ToString() == "" ? "" : AggreGateAE.ToString("0.0")) + "</span></td><td style='text-align: center'><span >" + (Total) + "</span></td></tr>");
                                        }
                                        else
                                        {
                                            Response.Write("<tr class='text_center'><td><span style='font-weight: bold;'>" + dtPaper.Rows[0]["PaperName"].ToString().Trim() + "</span></td><td></td><td  class='" + monthlyTest1 + "'></td><td></td><td  class='" + monthlyTest2 + "'></td><td></td></tr>");
                                        }
                                        Term1TotalMax = Term1TotalMax + 100; Term2TotalMax = Term2TotalMax + 100;

                                        string sqlss = "SELECT MaxHY, MaxAE FROM ICSEMaxMarkEntryItoVIII where classid=" + ClassId + " and sectionId=" + SectionId + " and branchid=" + dt.Rows[i]["branchid"].ToString() + "";
                                        sqlss = sqlss + " and subjectId=" + dtSubject.Rows[s]["id"].ToString() + " and paperId=" + dtPaper.Rows[0]["Id"].ToString() + " and BranchCode=" + BranchCode + " and SessionName='" + SessionName + "'";
                                        double MaxTestHY = 0, MaxHY = 0, MaxTestAE = 0, MaxAE = 0;
                                        double.TryParse(oo.ReturnTag(sqlss, "MaxHY"), out MaxHY);
                                        MaxTestHY = (100 - MaxHY) / dtPaper.Rows.Count;
                                        double.TryParse(oo.ReturnTag(sqlss, "MaxAE"), out MaxAE);
                                        MaxTestAE = (100 - MaxAE) / dtPaper.Rows.Count;
                                        if (((BestOfT1T2T3 * 100) / MaxTestHY) < PassingMark && monthlyTest1 != "hide")
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        if (((HY * 100) / MaxHY) < PassingMark)
                                        {
                                            FailPassStatus1 = FailPassStatus1 + "FAILED";
                                        }
                                        if (((BestOfT4T5T6 * 100) / MaxTestAE) < PassingMark && monthlyTest2 != "hide")
                                        {
                                            FailPassStatus2 = FailPassStatus2 + "FAILED";
                                        }
                                        if (((AE * 100) / MaxAE) < PassingMark)
                                        {
                                            FailPassStatus2 = FailPassStatus2 + "FAILED";
                                        }
                                    }
                                }
                                
                            }
                            double term1Percent = 0; double term2Percent = 0; double term12Percent = 0;
                            term1Percent = (totalAggHYs * 100) / Term1TotalMax;
                            term2Percent = (totalAggAEs * 100) / Term2TotalMax;
                            term12Percent = (term1Percent + term2Percent) / 2;
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                Response.Write("<tr class='text_center' style='background: #f3f3f3;'><td><b>TOTAL</b></td><td style='text-align: center'><b>"+totalHYs.ToString("0.0")+ "</b></td><td style='text-align: center' class='" + monthlyTest1 + "'><b>" + totalTestHYs.ToString("0.0") + "</b></td><td style='text-align: center'><b>" + totalAggHYs.ToString("0.0") + "</b></td></tr>");
                                Response.Write("<tr class='text_center'><td></td><td style='text-align: left;' colspan='"+(monthlyTest1=="hide"?1:2) +"'><b>PERCENTAGE</b></td><td style='text-align: center'><b>"+ term1Percent.ToString("0.0") + " %</b></td></tr>");
                                Response.Write("<tr class='text_center'><td></td><td style='text-align: left; padding: 5px 0 !important;'><b>RESULT</b></td><td style='text-align: center;border:0px !important;' colspan='2'><b>" + (FailPassStatus1==""?"PASSED": "FAILED") +"</b></td></tr>");
                            }
                            if (Term.ToLower() == "Term2".ToLower())
                            {
                                Response.Write("<tr class='text_center' style='background: #f3f3f3;'><td><b>TOTAL</b></td><td style='text-align: center'><b>" + totalHYs.ToString("0.0") + "</b></td><td style='text-align: center'class='" + monthlyTest1 + "'><b>" + totalTestHYs.ToString("0.0") + "</b></td><td style='text-align: center'><b>" + totalAggHYs.ToString("0.0") + "</b></td><td style='text-align: center'><b>" + totalAEs.ToString("0.0") + "</b></td><td style='text-align: center'class='" + monthlyTest2 + "'><b>" + totalTestAEs.ToString("0.0") + "</b></td><td style='text-align: center'><b>" + totalAggAEs.ToString("0.0") + "</b></td><td style='text-align: center'><b>" + totalGtotals.ToString("0.0") + "</b></td></tr>");
                                Response.Write("<tr class='text_center'><td ></td><td style='text-align: center;'><b>PERCENTAGE</b></td><td class='"+ monthlyTest1 + "'></td><td style='text-align: center'><b>" + term1Percent.ToString("0.0") + " %</b></td><td colspan='" + (monthlyTest2 == "hide" ? 1 : 2) + "'></td><td style='text-align: center'><b>" + term2Percent.ToString("0.0") + " %</b></td><td style='text-align: center'><b>" + term12Percent.ToString("0.0") + " %</b></td></tr>");
                                Response.Write("<tr class='text_center'><td ></td><td style='text-align: center;padding: 5px 0 !important;'><b>RESULT</b></td><td style='text-align: center;' colspan='" + (monthlyTest1 == "hide" ? 1 : 2) + "'><b>" + (FailPassStatus1 == "" ? "Passed" : "FAILED") + "</b></td><td style='text-align: center;' colspan='" + (monthlyTest2 == "hide" ? 2 : 3) + "'><b>" + (FailPassStatus2 == "" ? "PASSED" : "FAILED") + "</b></td><td></td></tr>");
                            }                                                                             

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;border: 0px !important;'><tbody>");
                                Response.Write("<tr><td style='padding-top: 10px !important;text-align:center;border: 0px !important; ' colspan='" + ds.Tables[0].Rows.Count + "'><div style='font-weight: bold; font-size:11px; border-top: 1px solid #000 !important; width:100%;'>CLASS TEACHER'S EVALUATION (GRADES)</span></td>");
                                for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                                {
                                    if (Term.ToLower() == "Term1".ToLower())
                                    {
                                        Response.Write("<tr><td style='padding: 3px !important;width: 40%;'><span style='font-weight: bold'>" + ds.Tables[0].Rows[l]["EvaluationHead"].ToString().Trim() + "</span></td>");
                                        Response.Write("<td style='padding: 3px !important;width: 60%; text-align:center;'><span class='sentence-case' >" + ds.Tables[0].Rows[l]["Term1Evaluation"].ToString().Trim() + "</span></td></tr>");
                                    }
                                    if (Term.ToLower() == "Term2".ToLower())
                                    {
                                        Response.Write("<tr><td style='padding: 3px !important;width: 40%;'><span style='font-weight: bold'>" + ds.Tables[0].Rows[l]["EvaluationHead"].ToString().Trim() + "</span></td>");
                                        Response.Write("<td style='padding: 3px !important;width: 30%;text-align:center;'><span class='sentence-case' >" + ds.Tables[0].Rows[l]["Term1Evaluation"].ToString().Trim() + "</span></td><td style='padding: 3px !important;width: 30%;text-align:center;'><span class='sentence-case' >" + ds.Tables[0].Rows[l]["Term2Evaluation"].ToString().Trim() + "</span></td></tr>");
                                    }
                                }
                                Response.Write("</tbody></table>");
                            }

                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;'><tbody>");
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    Response.Write("<tr><td style='padding: 1px  !important;width: 40%;'><span style='font-weight: bold;font-size: 11px;'>CLASS TEACHER'S REMARKS &nbsp;</span></td>");
                                    Response.Write("<td style='padding: 1px  !important;width: 60%'><span class='sentence-case' >" + ds.Tables[1].Rows[0]["Term1Remark"].ToString().Trim() + "</span></td></tr>");
                                }
                                if (Term.ToLower() == "Term2".ToLower())
                                {
                                    Response.Write("<tr><td style='padding: 1px  !important;width: 40%;'><span style='font-weight: bold;font-size: 11px;'>CLASS TEACHER'S REMARKS &nbsp;</span></td>");
                                    Response.Write("<td style='padding: 1px  !important;width: 30%;'><span class='sentence-case' >" + ds.Tables[1].Rows[0]["Term1Remark"].ToString().Trim() + "</span></td><td style='padding: 1px  !important;width: 30%;'><span class='sentence-case' >" + ds.Tables[1].Rows[0]["Term2Remark"].ToString().Trim() + "</span></td></tr>");
                                }
                                Response.Write("</tbody></table>");
                            }

                            Response.Write("<table class='tables table term1 mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;'><tbody>");
                            if (Term.ToLower() == "Term1".ToLower())
                            {
                                double totaldays = 0, attendence = 0, percent = 0; string t1Att = "";
                                totaldays = double.Parse(ds.Tables[2].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["totaldays"].ToString().Trim());
                                attendence = double.Parse(ds.Tables[2].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["attendence"].ToString().Trim());
                                if (attendence != 0 && totaldays != 0)
                                {
                                    percent = (attendence * 100) / totaldays;
                                    t1Att = "<span >" + attendence.ToString().Trim() + "</span>/<span >" + totaldays.ToString().Trim() + "</span> (<span >" + percent.ToString("0.00") + "</span> %)";
                                }
                                Response.Write("<tr><td style='padding: 1px  !important;width: 40%; font-weight: bold;font-size: 11px !important;'>STUDENT'S ATTENDANCE<br><div style='font-size:7px !important;'>Aminimum of 75% attendance is compulsory as per the council rules.</div></td><td style='width:60%' class='text-center'>" + t1Att + "</td></tr>");
                            }
                            if (Term.ToLower() == "Term2".ToLower())
                            {
                                Response.Write("<table class='tables table term2 mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;'><tbody>");

                                double totaldays1 = 0, attendence1 = 0, percent1 = 0; string t1Att = ""; string t2Att = ""; string AllAtt = "";
                                totaldays1 = double.Parse(ds.Tables[2].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["totaldays"].ToString().Trim());
                                attendence1 = double.Parse(ds.Tables[2].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[2].Rows[0]["attendence"].ToString().Trim());

                                if (attendence1 != 0 && totaldays1 != 0)
                                {
                                    percent1 = (attendence1 * 100) / totaldays1;
                                    t1Att = "<span >" + attendence1.ToString().Trim() + "</span>/<span >" + totaldays1.ToString().Trim() + "</span> (<span >" + percent1.ToString("0.00") + "</span> %)";
                                }
                                double totaldays2 = 0, attendence2 = 0, percent2 = 0;
                                totaldays2 = double.Parse(ds.Tables[3].Rows[0]["totaldays"].ToString().Trim() == "" ? "0" : ds.Tables[3].Rows[0]["totaldays"].ToString().Trim());
                                attendence2 = double.Parse(ds.Tables[3].Rows[0]["attendence"].ToString().Trim() == "" ? "0" : ds.Tables[3].Rows[0]["attendence"].ToString().Trim());

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
                                Response.Write("<tr><td style='padding: 1px  !important;width: 40%; font-weight: bold;font-size:11px !important;'>STUDENT'S ATTENDANCE<br><div style='font-size:7px !important;'>Aminimum of 75% attendance is compulsory as per the council rules.</div></td><td style='padding: 1px  !important;width:30%;' class='text-center'>" + t1Att + "</td><td style='width: 30%;' class='text-center'>" + t2Att + "</td></tr>");
                            }
                            Response.Write("</tbody></table>");

                            for (int l = 0; l < ds.Tables[4].Rows.Count; l++)
                            {
                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;'><tbody>");
                                if (Term.ToLower() == "Term1".ToLower())
                                {
                                    Response.Write("<tr><td style='padding: 1px  !important;width: 40%;'><span style='font-weight: bold;font-size:11px'>ACADEMIC SESSION ASSESSMENT &nbsp;</span></td>");
                                    Response.Write("<td style='padding: 1px  !important;width: 60%'><span class='sentence-case' >" + ds.Tables[4].Rows[0]["Term1assessment"].ToString().Trim() + "</span></td></tr>");
                                }
                                if (Term.ToLower() == "Term2".ToLower())
                                {
                                    Response.Write("<tr><td style='padding: 1px  !important;width: 40%;'><span style='font-weight: bold;font-size:11px'>ACADEMIC SESSION ASSESSMENT&nbsp;</span></td>");
                                    Response.Write("<td style='padding: 1px  !important;width: 30%;'><span class='sentence-case' >" + ds.Tables[4].Rows[0]["Term1assessment"].ToString().Trim() + "</span></td><td style='padding: 2px !important;width: 30%;'><span class='sentence-case' >" + ds.Tables[4].Rows[0]["Term2assessment"].ToString().Trim() + "</span></td></tr>");
                                }
                                Response.Write("</tbody></table>");
                            }
                            if (Session["Logintype"].ToString() != "Guardian")
                            {
                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered text-uppercase' style='margin-bottom: 5px;border:0px !important;'><tbody><tr>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>CLASS TEACHER'S SIGNATURE</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>CHECKED BY</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>PRINCIPAL'S SIGNATURE</div></td>");
                                Response.Write("<td style='padding-top: 40px !important;width: 100%%; border:0px !important; text-align:center; font-size:12px !important;'><div style='font-weight: bold; width:100%; border-top:1px solid #000;padding-top: 6px !important;'>PARENT'S/GUARDIAN'S SIGNATURE</div></td>");
                                Response.Write("</tr></tbody></table>");

                                Response.Write("<table class='tables table mp-table p-table-bordered table-bordered' style='margin-bottom: 5px;border:0px !important;'><tbody>");
                                Response.Write("<tr><td style='padding-top: 5px !important;width: 100%%;border:0px !important; text-align:center;'><span style='font-weight: bold;font-size:10PX !important; text-transform: initial !important;'> GRADES : &nbsp;&nbsp;A-80% and above (Outstanding), &nbsp;&nbsp;&nbsp;&nbsp;B-60% to 79% (Good), &nbsp;&nbsp;&nbsp;&nbsp;C-50% to 59% (Satisfactory), &nbsp;&nbsp;&nbsp;&nbsp;D-Below 49% (Need special attention)</span></td></tr>");
                                Response.Write("<tr><td style='padding-top: 5px !important;width: 100%%;border:0px !important; text-align:left;'><span style='font-weight: bold;font-size:12PX !important; text-transform: initial !important'>Rules:</td></tr>");
                                Response.Write("<tr><td style='padding-top: 5px !important;width: 100%%;border:0px !important; text-align:left;'><span style='font-weight: bold;font-size:12PX !important; text-transform: initial !important'>1. Student’s promotion depends on the daily performance throughout the year.<br>");
                                Response.Write("2. 80% attendance is compulsory for students to appear in the examination.<br>");
                                Response.Write("3. Report card will not be given if fee is in arrear.<br>");
                                Response.Write("4. For promotion, an average of 40% marks is must and passing in English is compulsory.<br>");
                                Response.Write("5. Promotion refused will not be reconsidered.<br>");
                                Response.Write("6. If this Report Card is lost, then a fine of Rs. 100/- will be charged for the duplicate Report Card.</td></tr>");
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