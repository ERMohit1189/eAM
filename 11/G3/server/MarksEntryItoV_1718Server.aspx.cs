using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G3_MarksEntryItoV_1718Server  : System.Web.UI.Page
{
    Campus oo = new Campus();
    string sql = "";
    string logintype = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            logintype = "Admin";
            //this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            logintype = "Staff";
            //this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string Evail = Request.Form["Evail"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Session = Request.Form["Session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();

        bool IsLocked = false;
        if (logintype.ToString() == "Staff")
        {
            string sql33 = "select dr.Id, ClassName, dr.EvalName, CountStartFromAdmission, format(ExamStartDate, 'dd-MMM-yyyy')ExamStartDate, format(ExamEndDate, 'dd-MMM-yyyy')ExamEndDate, format(ExamLockDate, 'dd-MMM-yyyy')ExamLockDate, CountStartFrom, format(CountStartDateDate, 'dd-MMM-yyyy')CountStartDateDate, format(dr.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordDate, dr.LoginName from SetAttendenceRange dr inner join ClassMaster cm on cm.id=dr.Classid and cm.SessionName=dr.SessionName and cm.BranchCode=dr.BranchCode Where EvalName='" + Evail.ToString().Trim() + "' and Classid=" + ClassId + " and dr.SessionName='" + Session.ToString() + "' and dr.BranchCode=" + BranchCode + "";
            string ExamLockDate = oo.ReturnTag(sql33, "ExamLockDate");
            if (!string.IsNullOrEmpty(ExamLockDate))
            {
                DateTime currentDate = DateTime.Now.Date;
                DateTime specificDate;

                bool isValidDate = DateTime.TryParseExact(ExamLockDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out specificDate);
                if (isValidDate)
                {
                    if (currentDate >= specificDate)
                    {
                        IsLocked = true;
                    }
                }
            }
        }
        string disabledAttribute = IsLocked ? "disabled" : "";
        string trClass = IsLocked ? "disabled-row" : "";

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                sql = "select SubjectType from TTSubjectMaster where SessionName='" + Session + "' and ClassId=" + ClassId + "  and id=" + SubjectId + " and SectionName='" + SectionName + "' and BranchCode=" + BranchCode + "";
                bool isoptional = (oo.ReturnTag(sql, "SubjectType").ToLower() == "optional" ? true : false);
                sql = "select   sg.srno, Name, FatherName, sg.BranchId Branch from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") SG ";
                sql +=  " where  sg.classid=" + ClassId.ToString() + "";
                sql +=  " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
                sql +=  " sg.BranchCode='" + BranchCode.ToString() + "'";
                if (isoptional)
                {
                    sql +=  " and So.srno in (select Srno from TTSubjectMaster sm inner join ICSEOptionalSubjectAllotment opt on opt.OptSubjectId=sm.id and sm.SessionName=opt.SessionName and sm.BranchCode=opt.BranchCode where sm.SessionName='" + Session + "'  and sm.BranchCode=" + BranchCode + " and sm.id=" + SubjectId + ")";
                }
                sql +=  " and sg.Withdrwal is null and isnull(Promotion,'')<>'Cancelled' order by FirstName Asc";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Parameters.Clear();
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
                        ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
                        ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
                        ss +=  " where cm.id=" + ClassId + " and bm.id=" + dt.Rows[0]["Branch"].ToString() + " and cm.BranchCode=" + BranchCode + " and cm.SessionName='" + Session + "'";
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

                        string MaxMarks1 = "", MaxMarks2 = "", MaxMarks4 = "", MaxMarks5 = "", MaxMarks6 = "", MaxMarks7="";
                        double MaxMarks1M = 0, MaxMarks2M = 0,  MaxMarks4M = 0, MaxMarks5M = 0, MaxMarks6M = 0, MaxMarks7M = 0;
                        string sql1 = "Select MaxMarks1,MaxMarks2,MaxMarks4,MaxMarks5,MaxMarks6,MaxMarks7 from SetMaxMinMarks_ItoV where Eval='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + " and SubjectActivityId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "'";
                        
                        if (Evail.ToLower() == "term1")
                        {
                            MaxMarks1 = (DisableTest1 == "hide" ? "0" : (oo.ReturnTag(sql1, "MaxMarks1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks1").ToString()));
                            MaxMarks2 = (DisableTest2 == "hide" ? "0" : (oo.ReturnTag(sql1, "MaxMarks2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks2").ToString()));
                        }
                        else
                        {
                            MaxMarks1 = (DisableTest4 == "hide" ? "0" : (oo.ReturnTag(sql1, "MaxMarks1").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks1").ToString()));
                            MaxMarks2 = (DisableTest5 == "hide" ? "0" : (oo.ReturnTag(sql1, "MaxMarks2").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks2").ToString()));
                        }
                        MaxMarks4 = (oo.ReturnTag(sql1, "MaxMarks4").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks4").ToString());
                        MaxMarks5 = (oo.ReturnTag(sql1, "MaxMarks5").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks5").ToString());
                        MaxMarks6 = (oo.ReturnTag(sql1, "MaxMarks6").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks6").ToString());
                        MaxMarks7 = (oo.ReturnTag(sql1, "MaxMarks7").ToString() == "" ? "0" : oo.ReturnTag(sql1, "MaxMarks7").ToString());

                       
                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-48 text-left' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175 text-left' scope='col'>Father's Name</th>");
                        if (Evail.ToLower() == "term1")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in " + DisableTest1 + "' scope='col'><span id=''> U.T. I </span><br><input name='' type='text' value='" + MaxMarks1.ToString() + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in " + DisableTest2 + "' scope='col'><span id=''> U.T. II </span><br><input name='' type='text' value='" + MaxMarks2.ToString() + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70 " + monthlyTest1 + "' scope='col'>Best One<br> Total</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70 " + monthlyTest1 + "' scope='col'>Conv. into<br> 10</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> S.E.</span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "5" : MaxMarks5).ToString() + "' id='MaxMarks4' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> N.B.A.</span><br><input name='' type='text' value='" + (MaxMarks4 == "" ? "5" : MaxMarks4).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");

                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col' colspan='2'  style='padding:0 !important;padding-top:5px !important;'><span> H.Y.E.</span>(80)<br>");
                            Response.Write("<table class='table table-bordered' style='width:100%; padding:0 !important; margin:0 !important; margin-top:5px !important;'><tr><th style='width:50%'><span id=''>Theory</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "80" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th><th style='width:50%'><span id=''> Practical/Oral</span><br><input name='' type='text' value='" + (MaxMarks7 == "" ? "80" : MaxMarks7).ToString() + "' id='MaxMarks7' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th></tr></table></th>");
                        }
                        if (Evail.ToLower() == "term2")
                        {
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in " + DisableTest4 + "' scope='col'><span id=''> U.T. III </span><br><input name='' type='text' value='" + MaxMarks1.ToString() + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in " + DisableTest5 + "' scope='col'><span id=''> U.T. IV </span><br><input name='' type='text' value='" + MaxMarks2.ToString() + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70 " + monthlyTest2 + "' scope='col'>Best One Total</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70 " + monthlyTest2 + "' scope='col'>Conv. in 10</th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> S.E.</span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "5" : MaxMarks5).ToString() + "' id='MaxMarks4' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> N.B.A.</span><br><input name='' type='text' value='" + (MaxMarks4 == "" ? "5" : MaxMarks4).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th>");
                            Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col' colspan='2'  style='padding:0 !important;padding-top:5px !important;'><span> A.E.</span>(80)<br>");
                            Response.Write("<table class='table table-bordered' style='width:100%; padding:0 !important; margin:0 !important; margin-top:5px !important;'><tr><th style='width:50%'><span id=''>Theory</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "80" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th><th style='width:50%'><span id=''> Practical/Oral</span><br><input name='' type='text' value='" + (MaxMarks7 == "" ? "80" : MaxMarks7).ToString() + "' id='MaxMarks7' class='form-control-blue text-center' style='width:40px;' " + disabledAttribute + "></th></tr></table></th>");
                        }
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Grade</th>");
                        Response.Write("</tr>");
                       
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Test1 = "", Test2 = "", NB = "", SE = "", SAT = "", id = "", Prac="";
                            double Test1M = 0, Test2M = 0, NBM = 0, SEM = 0, SATM = 0, PracM = 0;
                            string sql3 = "Select Id,Test1,Test2,Test3,SAT,NB,SE,Prac from CCEItoV where SRNO='" + dt.Rows[i]["srno"].ToString() + "' and Evaluation='" + Evail.ToString().Trim() + "' and SubjectId='" + SubjectId.ToString().Trim() + "'  and PaperId='" + PaperId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode + "";
                            id = oo.ReturnTag(sql3, "Id");
                            Test1 = oo.ReturnTag(sql3, "Test1");
                            Test2 = oo.ReturnTag(sql3, "Test2");

                            SAT = oo.ReturnTag(sql3, "SAT");
                            NB = oo.ReturnTag(sql3, "NB");
                            SE = oo.ReturnTag(sql3, "SE");
                            Prac = oo.ReturnTag(sql3, "Prac");

                            if (Test1.ToString().Trim().ToUpper() == "NP" || Test1.ToString().Trim().ToUpper() == "NAD" || Test1.ToString().Trim().ToUpper() == "ML" || Test1.ToString().Trim().ToUpper() == "") { }
                            else { Test1M = (Evail.ToLower() == "term1" ? (DisableTest1 == "hide" ? 0 : double.Parse(Test1.ToString().Trim())) : (DisableTest4 == "hide" ? 0 : double.Parse(Test1.ToString().Trim()))); }
                            if (Test2.ToString().Trim().ToUpper() == "NP" || Test2.ToString().Trim().ToUpper() == "NAD" || Test2.ToString().Trim().ToUpper() == "ML" || Test2.ToString().Trim().ToUpper() == "") { }
                            else { Test2M = (Evail.ToLower() == "term1" ? (DisableTest2 == "hide" ? 0 : double.Parse(Test2.ToString().Trim())) : (DisableTest5 == "hide" ? 0 : double.Parse(Test2.ToString().Trim()))); }
                            if (NB.ToString().Trim().ToUpper() == "NP" || NB.ToString().Trim().ToUpper() == "NAD" || NB.ToString().Trim().ToUpper() == "ML" || NB.ToString().Trim().ToUpper() == "") { }
                            else { NBM = double.Parse(NB.ToString().Trim()); }
                            if (SE.ToString().Trim().ToUpper() == "NP" || SE.ToString().Trim().ToUpper() == "NAD" || SE.ToString().Trim().ToUpper() == "ML" || SE.ToString().Trim().ToUpper() == "") { }
                            else { SEM = double.Parse(SE.ToString().Trim()); }
                            if (SAT.ToString().Trim().ToUpper() == "NP" || SAT.ToString().Trim().ToUpper() == "NAD" || SAT.ToString().Trim().ToUpper() == "ML" || SAT.ToString().Trim().ToUpper() == "") { }
                            else { SATM = double.Parse(SAT.ToString().Trim()); }
                            if (Prac.ToString().Trim().ToUpper() == "NP" || Prac.ToString().Trim().ToUpper() == "NAD" || Prac.ToString().Trim().ToUpper() == "ML" || Prac.ToString().Trim().ToUpper() == "") { }
                            else { PracM = double.Parse(Prac.ToString().Trim()); }

                            if (MaxMarks1.ToString().Trim().ToUpper() == "NP" || MaxMarks1.ToString().Trim().ToUpper() == "NAD" || MaxMarks1.ToString().Trim().ToUpper() == "ML" || MaxMarks1.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks1M = double.Parse(MaxMarks1.ToString().Trim()); }
                            if (MaxMarks2.ToString().Trim().ToUpper() == "NP" || MaxMarks2.ToString().Trim().ToUpper() == "NAD" || MaxMarks2.ToString().Trim().ToUpper() == "ML" || MaxMarks2.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks2M = double.Parse(MaxMarks2.ToString().Trim()); }
                            if (MaxMarks4.ToString().Trim().ToUpper() == "NP" || MaxMarks4.ToString().Trim().ToUpper() == "NAD" || MaxMarks4.ToString().Trim().ToUpper() == "ML" || MaxMarks4.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks4M = double.Parse(MaxMarks4.ToString().Trim()); }
                            if (MaxMarks5.ToString().Trim().ToUpper() == "NP" || MaxMarks5.ToString().Trim().ToUpper() == "NAD" || MaxMarks5.ToString().Trim().ToUpper() == "ML" || MaxMarks5.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks5M = double.Parse(MaxMarks5.ToString().Trim()); }
                            if (MaxMarks6.ToString().Trim().ToUpper() == "NP" || MaxMarks6.ToString().Trim().ToUpper() == "NAD" || MaxMarks6.ToString().Trim().ToUpper() == "ML" || MaxMarks6.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks6M = double.Parse(MaxMarks6.ToString().Trim()); }
                            if (MaxMarks7.ToString().Trim().ToUpper() == "NP" || MaxMarks7.ToString().Trim().ToUpper() == "NAD" || MaxMarks7.ToString().Trim().ToUpper() == "ML" || MaxMarks7.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks7M = double.Parse(MaxMarks7.ToString().Trim()); }

                            double percentle1 = 0; string ptm1 = ""; bool isaddmmconinten1 = false; string ObtMarks1 = ""; string Grade1 = "";

                            double p1 = (((Test1M * 100) / MaxMarks1M).ToString() == "NaN" ? 0 : ((Test1M * 100) / MaxMarks1M));
                                       
                            double p2 = (((Test2M * 100) / MaxMarks2M).ToString() == "NaN" ? 0 : ((Test2M * 100) / MaxMarks2M));


                            double totalmarks1 = (p1 > p2? Test1M : Test2M);
                            double totalmmmarks1 = 0;
                            totalmmmarks1 = (p1 > p2 ? MaxMarks1M : MaxMarks2M);
                            if (totalmarks1 == 0)
                            {
                                isaddmmconinten1 = true;
                            }
                            if (totalmmmarks1 == 0)
                            {
                                ptm1 = "0";
                                isaddmmconinten1 = true;
                            }
                            else
                            {
                                percentle1 = ((totalmarks1) * 10) / totalmmmarks1;
                                ptm1 = (percentle1).ToString("0.00");
                            }
                            ObtMarks1 = (double.Parse(ptm1) + NBM + SEM + SATM+ PracM).ToString("0");
                            Grade1 = Grade(double.Parse(ObtMarks1));

                            Response.Write("<tr id=" + id + ">");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n' style='text-align:left !important; width:20%;'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in " + (Evail.ToLower() == "term1" ? DisableTest1 : DisableTest4) + "'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;' " + disabledAttribute + "></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in " + (Evail.ToLower() == "term1" ? DisableTest2 : DisableTest5) + "'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-centern' name='2' style='width:40px;' " + disabledAttribute + "></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in " + (Evail.ToLower() == "term1" ? monthlyTest1 : monthlyTest2) + "'><span id=''>" + totalmarks1.ToString("0.00") + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in " + (Evail.ToLower() == "term1" ? monthlyTest1 : monthlyTest2) + "'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SE.ToString() + "' class='form-control-blue text-center' name='4' style='width:40px;' " + disabledAttribute + "></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + NB.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;' " + disabledAttribute + "></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;' " + disabledAttribute + "></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Prac.ToString() + "' class='form-control-blue text-center' name='7' style='width:40px;' " + disabledAttribute + "></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1)== 0 ? "" : Grade1.ToString()) + "</span></td>");
                            Response.Write("</tr>");
                        }
                        Response.Write("</tbody></table>");
                        if (!IsLocked)
                        {
                            Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit' /></div>");
                        }
                    }
                    catch (SqlException ex)
                    { throw ex; }
                }
            }
        }
    }

    public string Grade(double percentle)
    {
        if (percentle <= 39)
        {
            return "E";
        }
        else if (percentle >= 39.1 && percentle <= 50)
        {
            return "C2";
        }
        else if (percentle >= 50.1 && percentle <= 60)
        {
            return "C1";
        }
        else if (percentle >= 60.1 && percentle <= 70)
        {
            return "B2";
        }
        else if (percentle >= 70.1 && percentle <= 80)
        {
            return "B1";
        }
        else if (percentle >= 80.1 && percentle <= 90)
        {
            return "A2";
        }
        else if (percentle >= 90.1 && percentle <= 100)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }

}