using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class MarksEntryIXtoX_1920Server : System.Web.UI.Page
{
    public string checkIsCompulsory(string SubjectId, string classid)
    {
        string SubjectType = "";
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string sqlAd = "select SubjectType from TTSubjectMaster where Id=" + SubjectId + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + classid + "";
                cmd.Connection = conn;
                cmd.CommandText = sqlAd;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SubjectType=dt.Rows[0]["SubjectType"].ToString();
            }
        }
        return SubjectType;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string sql = "";
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string Evail = Request.Form["Evail"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string PaperId = Request.Form["PaperId"].ToString().Trim();
        string Session = Request.Form["Session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string ClassId = Request.Form["ClassId"].ToString().Trim();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string subType = checkIsCompulsory(SubjectId, ClassId);
                if (subType == "Compulsory")
                {
                    sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") asr ";
                    sql = sql + " where asr.ClassId='" + ClassId.ToString() + "' ";
                    sql = sql + " and asr.SectionName='" + SectionName.ToString() + "'  and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' order by Name Asc";
                }
                else if (subType == "Optional")
                {
                    sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") asr";
                    sql = sql + " inner join ICSEOptionalSubjectAllotment sos on sos.Srno=asr.SrNo and sos.SessionName=asr.SessionName and sos.BranchCode=asr.BranchCode";
                    sql = sql + " where asr.ClassId='" + ClassId + "' and asr.SectionName='" + SectionName + "' and asr.SessionName='" + Session.ToString() + "'  and asr.BranchCode=" + BranchCode + "";
                    sql = sql + "  and sos.OptSubjectId='" + SubjectId + "' and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' ";
                    sql = sql + " order by Name Asc";
                }
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
                        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + SubjectId.ToString().Trim() + " and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode.ToString() + " and classid=" + ClassId.ToString() + "";
                        string isAdditional = "";
                        if (int.Parse(oo.ReturnTag(sqlAd, "cnt"))>0)
                        {
                            isAdditional = "hide";
                        }

                        string MaxMarks1 = "", MaxMarks2 = "", MaxMarks3 = "", MaxMarks4 = "", MaxMarks5 = "", MaxMarks6 = "", MaxMarks7 = "", MaxMarks8 = "", commanMax = "";
                        double MaxMarks1M = 0, MaxMarks2M = 0, MaxMarks3M = 0, MaxMarks4M = 0, MaxMarks5M = 0, MaxMarks6M = 0, MaxMarks7M = 0, MaxMarks8M = 0; 
                        string sql1 = "Select MaxMarks1,MaxMarks2,MaxMarks3,MaxMarks4,MaxMarks5,MaxMarks6,MaxMarks7,MaxMarks8 from SetMaxMinMarks_IXtoX where Eval='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + " and SubjectActivityId='" + SubjectId.ToString().Trim() + "' and Paperid='" + PaperId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "'";
                        string sql2 = "Select MaxMarks from SubjectMaster where Id='" + SubjectId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode.ToString() + " and SectionName='" + SectionName.ToString() + "'";
                        commanMax = oo.ReturnTag(sql2, "MaxMarks");

                        MaxMarks1 = oo.ReturnTag(sql1, "MaxMarks1").ToString();
                        MaxMarks2 = oo.ReturnTag(sql1, "MaxMarks2").ToString();
                        MaxMarks3 = oo.ReturnTag(sql1, "MaxMarks3").ToString();
                        if (MaxMarks1 == "") { MaxMarks1 = commanMax; }
                        if (MaxMarks2 == "") { MaxMarks2 = commanMax; }
                        if (MaxMarks3 == "") { MaxMarks3 = commanMax; }

                        MaxMarks4 = oo.ReturnTag(sql, "MaxMarks4");
                        MaxMarks5 = oo.ReturnTag(sql, "MaxMarks5");
                        MaxMarks6 = oo.ReturnTag(sql, "MaxMarks6");
                        MaxMarks7 = oo.ReturnTag(sql, "MaxMarks7");
                        MaxMarks8 = oo.ReturnTag(sql, "MaxMarks8");

                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#<input type='hidden' id='hdnAditional' value='"+ (isAdditional == "hide" ? "1":"0") + "' /></th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-48' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175' scope='col'>Father's Name</th>");
                        if (Evail.ToLower() == "term1")
                        {
                            if (isAdditional == "hide")
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>U. T. I </span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "20" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>U. T. II </span><br><input name='' type='text' value='" + (MaxMarks2 == "" ? "20" : MaxMarks2.ToString()) + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                                }
                                if (ClassName.ToUpper() == "X")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>U. T. I </span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "20" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>Conv. <br>into 20 </th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>U. T. II </span><br><input name='' type='text' value='" + (MaxMarks2 == "" ? "80" : MaxMarks2.ToString()) + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>Conv. <br>into 20</th>");
                                }
                                Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>P.T.<br>5</th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> PRACTICAL</span><br><input name='' type='text' value='" + (MaxMarks7 == "" ? "30" : MaxMarks7).ToString() + "' id='MaxMarks7' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> Project Work/Field Visit / Portfolio </span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "15" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> H.Y.E.</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "50" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");

                            }
                            else
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. I </span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "20" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. II </span><br><input name='' type='text' value='" + (MaxMarks2 == "" ? "20" : MaxMarks2.ToString()) + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                                }
                                if (ClassName.ToUpper() == "X")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. I </span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "20" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>Conv. <br>into 20 </th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. II </span><br><input name='' type='text' value='" + (MaxMarks2 == "" ? "80" : MaxMarks2.ToString()) + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>Conv. <br>into 20 </th>");
                                }
                                Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>P.T.<br>5</th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> S.E.</span><br><input name='' type='text' value='" + (MaxMarks3 == "" ? "5" : MaxMarks3).ToString() + "' id='MaxMarks3' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''>M.A.</span><br><input name='' type='text' value='" + (MaxMarks4 == "" ? "5" : MaxMarks4).ToString() + "' id='MaxMarks4' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> PORT.</span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "5" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> H.Y.E.</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "80" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                                Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");
                            }

                        }
                        if (Evail.ToLower() == "term2")
                        {
                            if (isAdditional == "hide")
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. III </span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "20" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. IV </span><br><input name='' type='text' value='" + (MaxMarks2 == "" ? "20" : MaxMarks2.ToString()) + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>P.T.<br>5</th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> PRACTICAL</span><br><input name='' type='text' value='" + (MaxMarks7 == "" ? "30" : MaxMarks7).ToString() + "' id='MaxMarks7' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> Project Work/Field Visit / Portfolio </span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "15" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> A.E.</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "50" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");

                                }
                                if (ClassName.ToUpper() == "X")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>Prelims.</span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "50" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>P.T.<br>5</th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> PRACTICAL</span><br><input name='' type='text' value='" + (MaxMarks7 == "" ? "30" : MaxMarks7).ToString() + "' id='MaxMarks7' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> Project Work/Field Visit / Portfolio </span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "15" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> A.E.</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "50" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> A.E.</span><br><input name='' type='text' value='" + (MaxMarks8 == "" ? "50" : MaxMarks8).ToString() + "' id='MaxMark8' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");
                                }
                            }
                            else
                            {
                                if (ClassName.ToUpper() == "IX")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. III </span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "20" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> U. T. IV </span><br><input name='' type='text' value='" + (MaxMarks2 == "" ? "20" : MaxMarks2.ToString()) + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>P.T.<br>5</th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> S.E.</span><br><input name='' type='text' value='" + (MaxMarks3 == "" ? "5" : MaxMarks3).ToString() + "' id='MaxMarks3' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''>M.A.</span><br><input name='' type='text' value='" + (MaxMarks4 == "" ? "5" : MaxMarks4).ToString() + "' id='MaxMarks4' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> PORT.</span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "5" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> A.E.</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "80" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");
                                }
                                if (ClassName.ToUpper() == "X")
                                {
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''>Prelims.</span><br><input name='' type='text' value='" + (MaxMarks1 == "" ? "80" : MaxMarks1.ToString()) + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>P.T.<br>5</th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> S.E.</span><br><input name='' type='text' value='" + (MaxMarks3 == "" ? "5" : MaxMarks3).ToString() + "' id='MaxMarks3' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''>M.A.</span><br><input name='' type='text' value='" + (MaxMarks4 == "" ? "5" : MaxMarks4).ToString() + "' id='MaxMarks4' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> PORT.</span><br><input name='' type='text' value='" + (MaxMarks5 == "" ? "5" : MaxMarks5).ToString() + "' id='MaxMarks5' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> Pre. <br>Board-1</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "80" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> Pre. <br>Board-1</span><br><input name='' type='text' value='" + (MaxMarks8 == "" ? "80" : MaxMarks8).ToString() + "' id='MaxMarks8' class='form-control-blue text-center' style='width:40px;'></th>");
                                    Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'><span id=''> TOTAL </span><br><span id=''>100</span></th><th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>GRADE</th>");
                                }
                            }
                        }
                        
                        Response.Write("</tr>");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Test1 = "0", Test2 = "0", Port = "0", SE = "0", SAT = "0", SAT2 = "0", MA = "0", Prac = "0", id="0";
                            double Test1M = 0, Test2M = 0,  PortM = 0, SEM = 0, SATM = 0, SAT2M = 0, MAM = 0, PracM=0;
                            string sql3 = "Select Id,Test1,Test2,Port,SAT, SAT2,Prac,SE,MA from CCEIXtoX_1718 where SRNO='" + dt.Rows[i]["srno"].ToString() + "' and Evaluation='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + " and SubjectId='" + SubjectId.ToString().Trim() + "' and Paperid='" + PaperId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "' and SectionName='" + SectionName.ToString() + "'";
                            id = oo.ReturnTag(sql3, "Id");
                            Test1 = oo.ReturnTag(sql3, "Test1");
                            Test2 = oo.ReturnTag(sql3, "Test2");
                            SE = oo.ReturnTag(sql3, "SE");
                            MA = oo.ReturnTag(sql3, "MA");
                            Port = oo.ReturnTag(sql3, "Port");
                            SAT = oo.ReturnTag(sql3, "SAT");
                            SAT2 = oo.ReturnTag(sql3, "SAT2");
                            Prac = oo.ReturnTag(sql3, "Prac");

                            if (Test1.ToString().Trim().ToUpper() == "NP" || Test1.ToString().Trim().ToUpper() == "NAD" || Test1.ToString().Trim().ToUpper() == "ML" || Test1.ToString().Trim().ToUpper() == "") { }
                            else { Test1M = double.Parse(Test1.ToString().Trim()); }
                            if (Test2.ToString().Trim().ToUpper() == "NP" || Test2.ToString().Trim().ToUpper() == "NAD" || Test2.ToString().Trim().ToUpper() == "ML" || Test2.ToString().Trim().ToUpper() == "") { }
                            else { Test2M = double.Parse(Test2.ToString().Trim()); }

                            if (SE.ToString().Trim().ToUpper() == "NP" || SE.ToString().Trim().ToUpper() == "NAD" || SE.ToString().Trim().ToUpper() == "ML" || SE.ToString().Trim().ToUpper() == "") { }
                            else { SEM = double.Parse(SE.ToString().Trim()); }
                            if (MA.ToString().Trim().ToUpper() == "NP" || MA.ToString().Trim().ToUpper() == "NAD" || MA.ToString().Trim().ToUpper() == "ML" || MA.ToString().Trim().ToUpper() == "") { }
                            else { MAM = double.Parse(MA.ToString().Trim()); }
                            if (Port.ToString().Trim().ToUpper() == "NP" || Port.ToString().Trim().ToUpper() == "NAD" || Port.ToString().Trim().ToUpper() == "ML" || Port.ToString().Trim().ToUpper() == "") { }
                            else { PortM = double.Parse(Port.ToString().Trim()); }
                            if (SAT.ToString().Trim().ToUpper() == "NP" || SAT.ToString().Trim().ToUpper() == "NAD" || SAT.ToString().Trim().ToUpper() == "ML" || SAT.ToString().Trim().ToUpper() == "") { }
                            else { SATM = double.Parse(SAT.ToString().Trim()); }
                            if (Prac.ToString().Trim().ToUpper() == "NP" || Prac.ToString().Trim().ToUpper() == "NAD" || Prac.ToString().Trim().ToUpper() == "ML" || Prac.ToString().Trim().ToUpper() == "") { }
                            else { PracM = double.Parse(Prac.ToString().Trim()); }
                            if (SAT2.ToString().Trim().ToUpper() == "NP" || SAT2.ToString().Trim().ToUpper() == "NAD" || SAT2.ToString().Trim().ToUpper() == "ML" || SAT2.ToString().Trim().ToUpper() == "") { }
                            else { SAT2M = double.Parse(SAT2.ToString().Trim()); }

                            if (MaxMarks1.ToString().Trim().ToUpper() == "NP" || MaxMarks1.ToString().Trim().ToUpper() == "NAD" || MaxMarks1.ToString().Trim().ToUpper() == "ML" || MaxMarks1.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks1M = double.Parse(MaxMarks1.ToString().Trim()); }
                            if (MaxMarks2.ToString().Trim().ToUpper() == "NP" || MaxMarks2.ToString().Trim().ToUpper() == "NAD" || MaxMarks2.ToString().Trim().ToUpper() == "ML" || MaxMarks2.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks2M = double.Parse(MaxMarks2.ToString().Trim()); }
                            if (MaxMarks3.ToString().Trim().ToUpper() == "NP" || MaxMarks3.ToString().Trim().ToUpper() == "NAD" || MaxMarks3.ToString().Trim().ToUpper() == "ML" || MaxMarks3.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks3M = double.Parse(MaxMarks3.ToString().Trim()); }
                            if (MaxMarks4.ToString().Trim().ToUpper() == "NP" || MaxMarks4.ToString().Trim().ToUpper() == "NAD" || MaxMarks4.ToString().Trim().ToUpper() == "ML" || MaxMarks4.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks4M = double.Parse(MaxMarks4.ToString().Trim()); }
                            if (MaxMarks5.ToString().Trim().ToUpper() == "NP" || MaxMarks5.ToString().Trim().ToUpper() == "NAD" || MaxMarks5.ToString().Trim().ToUpper() == "ML" || MaxMarks5.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks5M = double.Parse(MaxMarks5.ToString().Trim()); }
                            if (MaxMarks6.ToString().Trim().ToUpper() == "NP" || MaxMarks6.ToString().Trim().ToUpper() == "NAD" || MaxMarks6.ToString().Trim().ToUpper() == "ML" || MaxMarks6.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks6M = double.Parse(MaxMarks6.ToString().Trim()); }
                            if (MaxMarks7.ToString().Trim().ToUpper() == "NP" || MaxMarks7.ToString().Trim().ToUpper() == "NAD" || MaxMarks7.ToString().Trim().ToUpper() == "ML" || MaxMarks7.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks7M = double.Parse(MaxMarks7.ToString().Trim()); }
                            if (MaxMarks8.ToString().Trim().ToUpper() == "NP" || MaxMarks8.ToString().Trim().ToUpper() == "NAD" || MaxMarks8.ToString().Trim().ToUpper() == "ML" || MaxMarks8.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks8M = double.Parse(MaxMarks8.ToString().Trim()); }

                            double percentle1 = 0; string ptm1 = ""; bool isaddmmconinten1 = false; string ObtMarks1 = ""; string grade1 = "";

                            double p1 = (((Test1M * 100) / MaxMarks1M).ToString() == "NaN" ? 0 : ((Test1M * 100) / MaxMarks1M));
                            double p2 = (((Test2M * 100) / MaxMarks2M).ToString() == "NaN" ? 0 : ((Test2M * 100) / MaxMarks2M));
                            double Test1Conv = (Test1M * 20) / MaxMarks1M;
                            double Test2Conv = (Test2M * 20) / MaxMarks2M;
                            double ConvPreboard = 0;
                            double totalmarks1 = 0;
                            double totalmmmarks1 = 0;
                            if (ClassName.ToUpper() == "IX")
                            {
                                totalmmmarks1 = p1 > p2 ? MaxMarks1M : MaxMarks2M;
                                totalmarks1 = (p1 > p2 ? Test1M : Test2M);
                            }
                            if (ClassName.ToUpper() == "X" && Evail.ToLower() == "term1")
                            {
                                totalmmmarks1 = 20;
                                totalmarks1 = (p1 > p2 ? Test1Conv : Test2Conv);
                            }
                            if (ClassName.ToUpper() == "X" && Evail.ToLower() == "term2")
                            {
                                totalmmmarks1 = MaxMarks1M;
                                totalmarks1 = Test1M;
                                ConvPreboard = (SATM > SAT2M ? SATM : SAT2M);
                            }
                            if (totalmarks1 == 0)
                            {
                                isaddmmconinten1 = true;
                            }
                            else
                            {
                                percentle1 = ((totalmarks1) * 5) / totalmmmarks1;
                            }
                            if (ClassName.ToUpper() == "IX")
                            {
                                if (isAdditional == "hide")
                                {
                                    ObtMarks1 = (percentle1 + SATM + PortM + PracM).ToString(CultureInfo.CurrentCulture);
                                }
                                else
                                {
                                    ObtMarks1 = (percentle1 + PortM + SEM + SATM + MAM).ToString(CultureInfo.CurrentCulture);
                                }
                            }
                            if (ClassName.ToUpper() == "X" && Evail.ToLower() == "term1")
                            {
                                if (isAdditional == "hide")
                                {
                                    ObtMarks1 = (percentle1 + SATM + PortM + PracM).ToString(CultureInfo.CurrentCulture);
                                }
                                else
                                {
                                    ObtMarks1 = (percentle1 + PortM + SEM + SATM + MAM).ToString(CultureInfo.CurrentCulture);
                                }
                            }
                            if (ClassName.ToUpper() == "X" && Evail.ToLower() == "term2")
                            {
                                if (isAdditional == "hide")
                                {
                                    ObtMarks1 = (percentle1 + PracM + PortM + ConvPreboard).ToString(CultureInfo.CurrentCulture);
                                }
                                else
                                {
                                    ObtMarks1 = (percentle1 + SEM +  MAM + PortM + ConvPreboard).ToString(CultureInfo.CurrentCulture);
                                }
                            }
                            grade1 = grade(double.Parse(double.Parse(ObtMarks1).ToString("0")));
                            

                            Response.Write("<tr id=" + id + ">");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-pad-n' style='font-weight: 600 !important;'>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</td>");
                            Response.Write("<td class='p-pad-n' style='font-weight: 600 !important;'>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</td>");
                            if (Evail.ToLower() == "term1")
                            {
                                if (isAdditional == "hide")
                                {
                                    if (ClassName.ToUpper() == "IX")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                    }
                                    if (ClassName.ToUpper() == "X")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + Test1Conv.ToString("0.00") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + Test2Conv.ToString("0.00") + "</span></td>");
                                    }
                                    Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Prac.ToString() + "' class='form-control-blue text-center' name='7' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Port.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString("0") + "</span></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : grade1.ToString()) + "</span></td>");
                                }
                                else
                                {
                                    if (ClassName.ToUpper() == "IX")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                        
                                    }
                                    if (ClassName.ToUpper() == "X")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + Test1Conv.ToString("0.00") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + Test2Conv.ToString("0.00") + "</span></td>");
                                    }
                                    Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SE.ToString() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + MA.ToString() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Port.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString("0") + "</span></td>");
                                    Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : grade1.ToString()) + "</span></td>");
                                }
                            }

                            if (Evail.ToLower() == "term2")
                            {
                                if (isAdditional == "hide")
                                {
                                    if (ClassName.ToUpper() == "IX")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Prac.ToString() + "' class='form-control-blue text-center' name='7' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Port.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString("0") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : grade1.ToString()) + "</span></td>");
                                    }
                                    if (ClassName.ToUpper() == "X")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Prac.ToString() + "' class='form-control-blue text-center' name='7' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Port.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT2.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString("0") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : grade1.ToString()) + "</span></td>");
                                    }
                                    
                                }
                                else
                                {
                                    if (ClassName.ToUpper() == "IX")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-center' name='2' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SE.ToString() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + MA.ToString() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Port.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString("0") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : grade1.ToString()) + "</span></td>");

                                    }
                                    if (ClassName.ToUpper() == "X")
                                    {
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SE.ToString() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + MA.ToString() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Port.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT2.ToString() + "' class='form-control-blue text-center' name='6' style='width:40px;'></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + double.Parse(ObtMarks1).ToString("0") + "</span></td>");
                                        Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (double.Parse(ObtMarks1) == 0 ? "" : grade1.ToString()) + "</span></td>");
                                    }
                                }
                            }

                            Response.Write("</tr>");
                        }
                        Response.Write("</tbody></table>");
                        Response.Write("<div class='col -sm-12  text-center'><input type='button' id='lnkSubmit' class='button form-control-blue hide' value='Submit' /></div>");

                    }
                    catch (SqlException ex)
                    { throw ex; }
                }
            }
        }
    }

    public string grade(double percentle)
    {
        if (percentle <= 32)
        {
            return "E";
        }
        else if (percentle >= 33 && percentle <= 40)
        {
            return "D";
        }
        else if (percentle >= 41 && percentle <= 50)
        {
            return "C2";
        }
        else if (percentle >= 50 && percentle <= 60)
        {
            return "C1";
        }
        else if (percentle >= 61 && percentle <= 70)
        {
            return "B2";
        }
        else if (percentle >= 71 && percentle <= 80)
        {
            return "B1";
        }
        else if (percentle >= 81 && percentle <= 90)
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