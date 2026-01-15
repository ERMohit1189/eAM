using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class MarksEntryIXtoXIIServer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string ClassId = Request.Form["ClassId"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string StreamId = Request.Form["StreamId"].ToString().Trim();
        string SectionId = Request.Form["SectionId"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Term = Request.Form["Term"].ToString().Trim();
        string SessionName = Request.Form["SessionName"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                ss = ss + " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                ss = ss + " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                ss = ss + " where cm.id="+ClassId+ " and bm.id=" + BranchId + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='"+SessionName+"'";
                string DisableTest1 = (oo.ReturnTag(ss, "Test1")=="No"?"hide":"");
                string DisableTest2 = (oo.ReturnTag(ss, "Test2")=="No"?"hide":"");
                string DisableTest3 = (oo.ReturnTag(ss, "Test3")=="No"?"hide":"");
                string DisableTest4 = (oo.ReturnTag(ss, "Test4")=="No"?"hide":"");
                string DisableTest5 = (oo.ReturnTag(ss, "Test5")=="No"?"hide":"");
                string DisableTest6 = (oo.ReturnTag(ss, "Test6")=="No"?"hide":"");
                string monthlyTest1 = "", monthlyTest2 = "";
                if (DisableTest1== "hide" && DisableTest2 == "hide" && DisableTest3 == "hide")
                {
                    monthlyTest1 = "hide";
                }
                if (DisableTest4 == "hide" && DisableTest5 == "hide" && DisableTest6 == "hide")
                {
                    monthlyTest2 = "hide";
                }


                cmd.Connection = conn;
                cmd.CommandText = "ICSEMarkEntryIXtoXIIProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                if (StreamId!="")
                {
                    cmd.Parameters.AddWithValue("@StreamId", StreamId);
                }
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                cmd.Parameters.AddWithValue("@PaperId", PaperId);
                cmd.Parameters.AddWithValue("@BranchCode", BranchCode);
                cmd.Parameters.AddWithValue("@SessionName", SessionName);
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        string MaxTest1 = "", MaxTest2 = "", MaxTest3 = "", MaxHY = "", MaxPracHY="", MaxTest4 = "", MaxTest5 = "", MaxTest6 = "", MaxAE = "", MaxPracAE = "";
                        string sql1 = "select MaxTest1, MaxTest2, MaxTest3, MaxHY,MaxPracHY, MaxTest4, MaxTest5, MaxTest6, MaxAE, MaxPracAE from ICSEMaxMarkEntryIXtoXII where BranchCode=" + BranchCode.ToString() + "  and SessionName='" + SessionName.ToString() + "' ";
                        sql1 = sql1+ " and ClassId=" + ClassId.ToString() + " and BranchId=" + BranchId.ToString().Trim() + " and SectionId=" + SectionId.ToString() + " and SubjectId=" + SubjectId.ToString().Trim() + " and PaperId=" + PaperId.ToString().Trim() + "";

                        double MaxHYM = 0, MaxTest1M = 0, MaxTest2M = 0, MaxTest3M = 0, MaxAEM = 0, MaxTest4M = 0, MaxTest5M = 0, MaxTest6M = 0;
                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'>");
                        Response.Write("<thead style='background: #f3f3f3cc;'>");
                        Response.Write("<tr>");
                        Response.Write("<th colspan='11'><h2 style='font-size: 17px !important;margin: 5px 0 5px; font-weight:bold;'>MARK ENTRY OF CLASS <span style='color: #428bca;'>" + dt.Rows[0]["CombineClassName"].ToString().ToUpper() + "</span> IN <span style='color: #428bca;'>" + Term.ToUpper() + "</span></h2></th>");
                        Response.Write("</tr>");

                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width:3%;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 7% !important;'>S.R. No.</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 14% !important; text-align: left !important;'>Student's Name</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 14% !important; text-align: left !important;'>Father's Name</th>");
                        if (Term.ToLower() == "term1")
                        {
                            MaxPracHY = oo.ReturnTag(sql1, "MaxPracHY").ToString();
                            MaxHY = oo.ReturnTag(sql1, "MaxHY").ToString();
                            MaxTest1 = (oo.ReturnTag(sql1, "MaxTest1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxTest1").ToString());
                            MaxTest2 = (oo.ReturnTag(sql1, "MaxTest2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxTest2").ToString());
                            MaxTest3 = (oo.ReturnTag(sql1, "MaxTest3").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxTest3").ToString());
                            
                            Response.Write("<th class='p-tot-tit p-pad-n " + DisableTest1 + "' scope='col'style='width: 9% !important;'><span> MONTHLY TEST1 </span><br><input name='' type='text' value='" + MaxTest1.ToString() + "' id='MaxTest1' class='form-control-blue validatetxt text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n "+ DisableTest2+"' scope='col'style='width: 9% !important;'><span> MONTHLY TEST2 </span><br><input name='' type='text' value='" + MaxTest2.ToString() + "' id='MaxTest2' class='form-control-blue validatetxt text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n " + DisableTest3 + "' scope='col'style='width: 9% !important;'><span> MONTHLY TEST3 </span><br><input name='' type='text' value='" + MaxTest3.ToString() + "' id='MaxTest3' class='form-control-blue validatetxt text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n " + monthlyTest1 + "' scope='col' style='width: 9% !important;'>MONTHLY TEST<br>(Max Marks 20)</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'style='width: 8% !important;'><span> THEORY</span><br><input name='' type='text' value='" + MaxHY.ToString() + "' id='MaxHY' class='form-control-blue validatetxt text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'style='width: 9% !important;'><span> PROJECT/PRAC.</span><br><input name='' type='text' value='" + MaxPracHY.ToString() + "' id='MaxPracHY' class='form-control-blue validatetxt text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col' style='width: 9% !important;'>TOTAL<br>(Max Marks 100)</th>");

                            if (MaxHY.ToString().Trim().ToUpper() == "NP" || MaxHY.ToString().Trim().ToUpper() == "NAD" || MaxHY.ToString().Trim().ToUpper() == "ML" || MaxHY.ToString().Trim().ToUpper() == "") { }
                            else { MaxHYM = double.Parse(MaxTest1.ToString().Trim()); }
                            if (monthlyTest1!="hide")
                            {
                                if (MaxTest1.ToString().Trim().ToUpper() == "NP" || MaxTest1.ToString().Trim().ToUpper() == "NAD" || MaxTest1.ToString().Trim().ToUpper() == "ML" || MaxTest1.ToString().Trim().ToUpper() == "" || DisableTest1=="hide") { }
                                else { MaxTest1M = double.Parse(MaxTest1.ToString().Trim()); }
                                if (MaxTest2.ToString().Trim().ToUpper() == "NP" || MaxTest2.ToString().Trim().ToUpper() == "NAD" || MaxTest2.ToString().Trim().ToUpper() == "ML" || MaxTest2.ToString().Trim().ToUpper() == "" || DisableTest2 == "hide") { }
                                else { MaxTest2M = double.Parse(MaxTest2.ToString().Trim()); }
                                if (MaxTest3.ToString().Trim().ToUpper() == "NP" || MaxTest3.ToString().Trim().ToUpper() == "NAD" || MaxTest3.ToString().Trim().ToUpper() == "ML" || MaxTest3.ToString().Trim().ToUpper() == "" || DisableTest2 == "hide") { }
                                else { MaxTest3M = double.Parse(MaxTest3.ToString().Trim()); }
                            }
                            
                        }
                        if (Term.ToLower() == "term2")
                        {
                            MaxPracAE = oo.ReturnTag(sql1, "MaxPracAE").ToString();
                            MaxAE = oo.ReturnTag(sql1, "MaxAE").ToString();
                            MaxTest4 = (oo.ReturnTag(sql1, "MaxTest4").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxTest4").ToString());
                            MaxTest5 = (oo.ReturnTag(sql1, "MaxTest5").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxTest5").ToString());
                            MaxTest6 = (oo.ReturnTag(sql1, "MaxTest6").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxTest6").ToString());

                            
                            Response.Write("<th class='p-tot-tit p-pad-n "+ DisableTest4+"' scope='col'><span> MONTHLY TEST4 </span><br><input name='' type='text' value='" + MaxTest4.ToString() + "' id='MaxTest4' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n "+ DisableTest5+"' scope='col'><span> MONTHLY TEST5 </span><br><input name='' type='text' value='" + MaxTest5.ToString() + "' id='MaxTest5' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n " + DisableTest6 + "' scope='col'><span> MONTHLY TEST6 </span><br><input name='' type='text' value='" + MaxTest6.ToString() + "' id='MaxTest6' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n " + monthlyTest2 + "' scope='col'>MONTHLY TEST<br>(Max Marks 20)</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span> THEORY</span><br><input name='' type='text' value='" + MaxAE.ToString() + "' id='MaxAE' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'><span> PROJECT/PRAC.</span><br><input name='' type='text' value='" + MaxPracAE.ToString() + "' id='MaxPracAE' class='form-control-blue text-center' style='width:40px;'></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n' scope='col'>TOTAL<br>(Max Marks 100)</th>");

                            if (MaxAE.ToString().Trim().ToUpper() == "NP" || MaxAE.ToString().Trim().ToUpper() == "NAD" || MaxAE.ToString().Trim().ToUpper() == "ML" || MaxAE.ToString().Trim().ToUpper() == "") { }
                            else { MaxAEM = double.Parse(MaxTest4.ToString().Trim()); }
                            if (monthlyTest2 != "hide")
                            {
                                if (MaxTest4.ToString().Trim().ToUpper() == "NP" || MaxTest4.ToString().Trim().ToUpper() == "NAD" || MaxTest4.ToString().Trim().ToUpper() == "ML" || MaxTest4.ToString().Trim().ToUpper() == "" || DisableTest4=="hide") { }
                                else { MaxTest4M = double.Parse(MaxTest4.ToString().Trim()); }
                                if (MaxTest5.ToString().Trim().ToUpper() == "NP" || MaxTest5.ToString().Trim().ToUpper() == "NAD" || MaxTest5.ToString().Trim().ToUpper() == "ML" || MaxTest5.ToString().Trim().ToUpper() == "" || DisableTest5 == "hide") { }
                                else { MaxTest5M = double.Parse(MaxTest5.ToString().Trim()); }
                                if (MaxTest6.ToString().Trim().ToUpper() == "NP" || MaxTest6.ToString().Trim().ToUpper() == "NAD" || MaxTest6.ToString().Trim().ToUpper() == "ML" || MaxTest6.ToString().Trim().ToUpper() == "" || DisableTest6 == "hide") { }
                                else { MaxTest6M = double.Parse(MaxTest6.ToString().Trim()); }
                            }
                        }
                       Response.Write("</tr>");
                       Response.Write("</thead>");
                        Response.Write("<tbody>");
                        
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string sql2 = "select Test1, Test2, Test3, BestOfT1T2T3,HY, PracHY, TotalHY, Test4, Test5, Test6, BestOfT4T5T6, AE, PracAE, TotalAE from ICSEMarkEntryIXtoXII where BranchCode=" + BranchCode.ToString() + "  and SessionName='" + SessionName.ToString() + "' ";
                            sql2 = sql2 + " and ClassId=" + ClassId.ToString() + " and BranchId=" + BranchId.ToString().Trim() + " and SectionId=" + SectionId.ToString() + " and SubjectId=" + SubjectId.ToString().Trim() + " and PaperId=" + PaperId.ToString().Trim() + "";
                            sql2 = sql2 + " and SrNo='" + dt.Rows[i]["srno"].ToString() + "'";
                            var dt2=oo.Fetchdata(sql2);
                            string HY ="", PracHY = "", Test1 = "", Test2 = "", Test3 = "", AE = "", PracAE = "", Test4 = "", Test5 = "", Test6="";
                            double HYM=0, PracHYM = 0, Test1M = 0, Test2M = 0, Test3M = 0, AEM = 0, PracAEM = 0, Test4M = 0, Test5M = 0, Test6M = 0;
                            

                            if (Term.ToLower() == "term1")
                            {
                                double MonthlyTestAY = 0; double aggregateHY = 0;
                                if (dt2.Rows.Count > 0)
                                {

                                    PracHY = dt2.Rows[0]["PracHY"].ToString();
                                    HY = dt2.Rows[0]["HY"].ToString();
                                    Test1 = dt2.Rows[0]["Test1"].ToString();
                                    Test2 = dt2.Rows[0]["Test2"].ToString();
                                    Test3 = dt2.Rows[0]["Test3"].ToString();
                                    if (Test1.ToString().Trim().ToUpper() == "NP" || Test1.ToString().Trim().ToUpper() == "NAD" || Test1.ToString().Trim().ToUpper() == "ML" || Test1.ToString().Trim().ToUpper() == "") { }
                                    else { Test1M = double.Parse(Test1.ToString().Trim()); }
                                    if (Test2.ToString().Trim().ToUpper() == "NP" || Test2.ToString().Trim().ToUpper() == "NAD" || Test2.ToString().Trim().ToUpper() == "ML" || Test2.ToString().Trim().ToUpper() == "") { }
                                    else { Test2M = double.Parse(Test2.ToString().Trim()); }
                                    if (Test3.ToString().Trim().ToUpper() == "NP" || Test3.ToString().Trim().ToUpper() == "NAD" || Test3.ToString().Trim().ToUpper() == "ML" || Test3.ToString().Trim().ToUpper() == "") { }
                                    else { Test3M = double.Parse(Test3.ToString().Trim()); }

                                    if (PracHY.ToString().Trim().ToUpper() == "NP" || PracHY.ToString().Trim().ToUpper() == "NAD" || PracHY.ToString().Trim().ToUpper() == "ML" || PracHY.ToString().Trim().ToUpper() == "") { }
                                    else { PracHYM = double.Parse(PracHY.ToString().Trim()); }
                                    if (HY.ToString().Trim().ToUpper() == "NP" || HY.ToString().Trim().ToUpper() == "NAD" || HY.ToString().Trim().ToUpper() == "ML" || HY.ToString().Trim().ToUpper() == "") { }
                                    else { HYM = double.Parse(HY.ToString().Trim()); }
                                    if (monthlyTest1 == "hide")
                                    {
                                        Test1M = 0;
                                        Test2M = 0;
                                        Test3M = 0;
                                        MaxTest1M = 0;
                                        MaxTest2M = 0;
                                        MaxTest3M = 0;
                                    }



                                    double p1 = (((Test1M * 100) / MaxTest1M).ToString() == "NaN" ? 0 : ((Test1M * 100) / MaxTest1M));
                                    double p2 = (((Test2M * 100) / MaxTest2M).ToString() == "NaN" ? 0 : ((Test2M * 100) / MaxTest2M));
                                    double p3 = (((Test3M * 100) / MaxTest3M).ToString() == "NaN" ? 0 : ((Test3M * 100) / MaxTest3M));
                                    
                                    MonthlyTestAY = Math.Round(p1 > p2 ? (p1 > p3 ? Test1M : (p2 > p3 ? Test2M : Test3M)) : (p2 > p3 ? Test2M : Test3M));
                                    aggregateHY = Math.Round(MonthlyTestAY + HYM+ PracHYM);
                                }

                                Response.Write("<tr>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in "+ DisableTest1+"'><input type='text' value='" + (Test1M == 0 ? Test1 : Test1M.ToString("0.0")) + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in "+ DisableTest2+"'><input type='text' value='" + (Test2M == 0 ? Test2 : Test2M.ToString("0.0")) + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in " + DisableTest3 + "'><input type='text' value='" + (Test3M == 0 ? Test3 : Test3M.ToString("0.0")) + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in " + monthlyTest1 + "'><span>" + (MonthlyTestAY > 0 ? MonthlyTestAY.ToString("0.0") : "") + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + (HYM == 0 ? HY : HYM.ToString("0.0")) + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + (PracHYM==0? PracHY:PracHYM.ToString("0.0")) + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span>" + (aggregateHY > 0 ? aggregateHY.ToString("0.0") : "") + "</span></td>");
                                Response.Write("</tr>");
                            }
                            if (Term.ToLower() == "term2")
                            {
                                double MonthlyTestAE = 0; double aggregateAE = 0;
                                if (dt2.Rows.Count > 0)
                                {
                                    PracAE = dt2.Rows[0]["PracAE"].ToString();
                                    AE = dt2.Rows[0]["AE"].ToString();
                                    Test4 = dt2.Rows[0]["Test4"].ToString();
                                    Test5 = dt2.Rows[0]["Test5"].ToString();
                                    Test6 = dt2.Rows[0]["Test6"].ToString();
                                    if (Test4.ToString().Trim().ToUpper() == "NP" || Test4.ToString().Trim().ToUpper() == "NAD" || Test4.ToString().Trim().ToUpper() == "ML" || Test4.ToString().Trim().ToUpper() == "") { }
                                    else { Test4M = double.Parse(Test4.ToString().Trim()); }
                                    if (Test5.ToString().Trim().ToUpper() == "NP" || Test5.ToString().Trim().ToUpper() == "NAD" || Test5.ToString().Trim().ToUpper() == "ML" || Test5.ToString().Trim().ToUpper() == "") { }
                                    else { Test5M = double.Parse(Test5.ToString().Trim()); }
                                    if (Test6.ToString().Trim().ToUpper() == "NP" || Test6.ToString().Trim().ToUpper() == "NAD" || Test6.ToString().Trim().ToUpper() == "ML" || Test6.ToString().Trim().ToUpper() == "") { }
                                    else { Test6M = double.Parse(Test6.ToString().Trim()); }
                                    if (PracAE.ToString().Trim().ToUpper() == "NP" || PracAE.ToString().Trim().ToUpper() == "NAD" || PracAE.ToString().Trim().ToUpper() == "ML" || PracAE.ToString().Trim().ToUpper() == "") { }
                                    else { PracAEM = double.Parse(PracAE.ToString().Trim()); }
                                    if (AE.ToString().Trim().ToUpper() == "NP" || AE.ToString().Trim().ToUpper() == "NAD" || AE.ToString().Trim().ToUpper() == "ML" || AE.ToString().Trim().ToUpper() == "") { }
                                    else { AEM = double.Parse(AE.ToString().Trim()); }
                                    if (monthlyTest2 == "hide")
                                    {
                                        Test4M = 0;
                                        Test5M = 0;
                                        Test6M = 0;
                                        MaxTest4M = 0;
                                        MaxTest5M = 0;
                                        MaxTest6M = 0;
                                    }

                                    

                                    double p1 = (((Test4M * 100) / MaxTest4M).ToString() == "NaN" ? 0 : ((Test4M * 100) / MaxTest4M));
                                    double p2 = (((Test5M * 100) / MaxTest5M).ToString() == "NaN" ? 0 : ((Test5M * 100) / MaxTest5M));
                                    double p3 = (((Test6M * 100) / MaxTest6M).ToString() == "NaN" ? 0 : ((Test6M * 100) / MaxTest6M));
                                    
                                    MonthlyTestAE = Math.Round(p1 > p2 ? (p1 > p3 ? Test4M : (p2 > p3 ? Test5M : Test6M)) : (p2 > p3 ? Test5M : Test6M));
                                    aggregateAE = Math.Round(MonthlyTestAE + AEM + PracAEM);
                                }

                                Response.Write("<tr>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-tot-tit p-pad-n' style='text-align: left !important;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in "+ DisableTest4+"'><input type='text' value='" + (Test4M == 0 ? Test4 : Test4M.ToString("0.0")) + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in "+ DisableTest5+"'><input type='text' value='" + (Test5M == 0 ? Test5 : Test5M.ToString("0.0")) + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in " + DisableTest6 + "'><input type='text' value='" + (Test6M == 0 ? Test6 : Test6M.ToString("0.0")) + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in " + monthlyTest2 + "'><span>" + (MonthlyTestAE > 0 ? MonthlyTestAE.ToString("0.0") : "") + "</span></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + (AEM==0?AE: AEM.ToString("0.0")) + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + (PracAEM==0? PracAE:PracAEM.ToString("0.0")) + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                Response.Write("<td class='p-pad-n text-center tab-in'><span>" + (aggregateAE>0? aggregateAE.ToString("0.0"):"") + "</span></td>");
                                Response.Write("</tr>");
                            }
                        }
                        Response.Write("</tbody></table>");
                        Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit'  onclick='ValidateTextBox('.validatetxt');ValidateDropdown('.validatedrp'); return validationReturn();' /></div>");

                    }
                    catch (SqlException ex)
                    { }
                }
            }
        }
    }
    
}