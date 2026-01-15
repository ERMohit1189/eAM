using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class common_G6_MarksEntryXI_1718Server_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus oo = new Campus();
        string sql = "";
        string ClassName = Request.Form["ClassName"].ToString().Trim();
        string SectionName = Request.Form["SectionName"].ToString().Trim();
        string Evail = Request.Form["Evail"].ToString().Trim();
        string SubjectId = Request.Form["SubjectId"].ToString().Trim();
        string Session = Request.Form["Session"].ToString().Trim();
        string BranchCode = Request.Form["BranchCode"].ToString().Trim();
        string BranchId = Request.Form["BranchId"].ToString().Trim();
        string Stream = Request.Form["Stream"].ToString().Trim();


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                sql = "select   sg.srno, Name, FatherName, sg.BranchId Branch from AllStudentRecord_UDF('" + Session.ToString() + "'," + BranchCode.ToString() + ") SG ";
                sql = sql + " where  sg.ClassName=" + ClassName.ToString() + " and sg.BranchId = '" + BranchId + "' ";
                sql = sql + " and sg.SectionName='" + SectionName.ToString() + "' and sg.SessionName='" + Session.ToString() + "' and ";
                sql = sql + " sg.BranchCode='" + BranchCode.ToString() + "'";
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
                        string MaxMarks1 = "", MaxMarks2 = "", MaxMarks3 = "", MaxMarks6 = "", MaxMarks7 = "", commanMax = "";
                        double MaxMarks1M = 0, MaxMarks2M = 0, MaxMarks3M = 0, MaxMarks6M = 0, MaxMarks7M = 0;
                        string sql1 = "Select MaxMarks1,MaxMarks2,MaxMarks3,MaxMarks4,MaxMarks5,MaxMarks6, MaxMarks7 from SetMaxMinMarks_XI where Eval='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + " and SubjectActivityId='" + SubjectId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "'";
                        string sql2 = "Select MaxMarks from SubjectMaster where Id='" + SubjectId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "' and BranchCode=" + BranchCode.ToString() + " and SectionName='" + SectionName.ToString() + "'";
                        commanMax = oo.ReturnTag(sql2, "MaxMarks");

                        MaxMarks1 = oo.ReturnTag(sql1, "MaxMarks1").ToString();
                        MaxMarks2 = oo.ReturnTag(sql1, "MaxMarks2").ToString();
                        MaxMarks3 = oo.ReturnTag(sql1, "MaxMarks3").ToString();

                        if (MaxMarks1 == "") { MaxMarks1 = commanMax; }
                        if (MaxMarks2 == "") { MaxMarks2 = commanMax; }
                        if (MaxMarks3 == "") { MaxMarks3 = commanMax; }

                        MaxMarks6 = oo.ReturnTag(sql1, "MaxMarks6");
                        MaxMarks7 = oo.ReturnTag(sql1, "MaxMarks7");

                        Response.Write("<table cellspacing='0' rules='all' class='table mp-table p-table-bordered table-bordered' style='border-collapse:collapse;'><tbody>");
                        Response.Write("<tr>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-35' scope='col' style='width:40px;'>#</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-48' scope='col'>S.R. No.</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175' scope='col'>Student's Name</th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175' scope='col'>Father's Name</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> TEST1 </span><br><input name='' type='text' value='" + MaxMarks1.ToString() + "' id='MaxMarks1' class='form-control-blue text-center' style='width:40px;'></th>");
                        Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> TEST2 </span><br><input name='' type='text' value='" + MaxMarks2.ToString() + "' id='MaxMarks2' class='form-control-blue text-center' style='width:40px;'></th>");
                        Response.Write("<th class='p-tot-tit p-pad-n tab-b-15 tab-in' scope='col'><span id=''> TEST3 </span><br><input name='' type='text' value='" + MaxMarks3.ToString() + "' id='MaxMarks3' class='form-control-blue text-center' style='width:40px;'></th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Best Two Test Total</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n sub-m-w-70' scope='col'>Con. in 10</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id='spnExamTrem'>"+(Evail=="TERM1"? "Half-Yearly" : "Annual")+"</span><br><input name='' type='text' value='" + (MaxMarks6 == "" ? "50" : MaxMarks6).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175' scope='col'>Conv. in Board Pattern</th>");
                        Response.Write("<th class='p-tot-tit p-pad-n tab-b-15  tab-in' scope='col'><span id=''> Prac./Asl</span><br><input name='' type='text' value='" + (MaxMarks7 == "" ? "50" : MaxMarks7).ToString() + "' id='MaxMarks6' class='form-control-blue text-center' style='width:40px;'></th>");
                        Response.Write("<th class='p-sub-tit p-pad-n sub-w-175' scope='col'>Conv + Prac</th>");


                        Response.Write("</tr>");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Test1 = "", Test2 = "", Test3 = "", SAT = "", id = "", Prac="";
                            double Test1M = 0, Test2M = 0, Test3M = 0, SATM = 0, PracM = 0;
                            string sql3 = "Select Id,Test1,Test2,Test3,SAT,NB,SE, Prac from CCEXI_1718 where SRNO='" + dt.Rows[i]["srno"].ToString() + "' and Evaluation='" + Evail.ToString().Trim() + "' and BranchCode=" + BranchCode.ToString() + " and SubjectId='" + SubjectId.ToString().Trim() + "' and SessionName='" + Session.ToString() + "' and SectionName='" + SectionName.ToString() + "'";
                            id = oo.ReturnTag(sql3, "Id");
                            Test1 = oo.ReturnTag(sql3, "Test1");
                            Test2 = oo.ReturnTag(sql3, "Test2");
                            Test3 = oo.ReturnTag(sql3, "Test3");

                            SAT = oo.ReturnTag(sql3, "SAT");
                            Prac = oo.ReturnTag(sql3, "Prac");

                            if (Test1.ToString().Trim().ToUpper() == "NP" || Test1.ToString().Trim().ToUpper() == "NAD" || Test1.ToString().Trim().ToUpper() == "ML" || Test1.ToString().Trim().ToUpper() == "") { }
                            else { Test1M = double.Parse(Test1.ToString().Trim()); }
                            if (Test2.ToString().Trim().ToUpper() == "NP" || Test2.ToString().Trim().ToUpper() == "NAD" || Test2.ToString().Trim().ToUpper() == "ML" || Test2.ToString().Trim().ToUpper() == "") { }
                            else { Test2M = double.Parse(Test2.ToString().Trim()); }
                            if (Test3.ToString().Trim().ToUpper() == "NP" || Test3.ToString().Trim().ToUpper() == "NAD" || Test3.ToString().Trim().ToUpper() == "ML" || Test3.ToString().Trim().ToUpper() == "") { }
                            else { Test3M = double.Parse(Test3.ToString().Trim()); }
                            if (SAT.ToString().Trim().ToUpper() == "NP" || SAT.ToString().Trim().ToUpper() == "NAD" || SAT.ToString().Trim().ToUpper() == "ML" || SAT.ToString().Trim().ToUpper() == "") { }
                            else { SATM = double.Parse(SAT.ToString().Trim()); }
                            if (Prac.ToString().Trim().ToUpper() == "NP" || Prac.ToString().Trim().ToUpper() == "NAD" || Prac.ToString().Trim().ToUpper() == "ML" || Prac.ToString().Trim().ToUpper() == "") { }
                            else { PracM = double.Parse(Prac.ToString().Trim()); }


                            if (MaxMarks1.ToString().Trim().ToUpper() == "NP" || MaxMarks1.ToString().Trim().ToUpper() == "NAD" || MaxMarks1.ToString().Trim().ToUpper() == "ML" || MaxMarks1.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks1M = double.Parse(MaxMarks1.ToString().Trim()); }
                            if (MaxMarks2.ToString().Trim().ToUpper() == "NP" || MaxMarks2.ToString().Trim().ToUpper() == "NAD" || MaxMarks2.ToString().Trim().ToUpper() == "ML" || MaxMarks2.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks2M = double.Parse(MaxMarks2.ToString().Trim()); }
                            if (MaxMarks3.ToString().Trim().ToUpper() == "NP" || MaxMarks3.ToString().Trim().ToUpper() == "NAD" || MaxMarks3.ToString().Trim().ToUpper() == "ML" || MaxMarks3.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks3M = double.Parse(MaxMarks3.ToString().Trim()); }
                            if (MaxMarks6.ToString().Trim().ToUpper() == "NP" || MaxMarks6.ToString().Trim().ToUpper() == "NAD" || MaxMarks6.ToString().Trim().ToUpper() == "ML" || MaxMarks6.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks6M = double.Parse(MaxMarks6.ToString().Trim()); }
                            if (MaxMarks7.ToString().Trim().ToUpper() == "NP" || MaxMarks7.ToString().Trim().ToUpper() == "NAD" || MaxMarks7.ToString().Trim().ToUpper() == "ML" || MaxMarks7.ToString().Trim().ToUpper() == "") { }
                            else { MaxMarks7M = double.Parse(MaxMarks7.ToString().Trim()); }

                            double percentle1 = 0; string ptm1 = ""; bool isaddmmconinten1 = false; string ObtMarks1 = ""; string grade1 = "";

                            double p1 = (((Test1M * 100) / MaxMarks1M).ToString() == "NaN" ? 0 : ((Test1M * 100) / MaxMarks1M))
                                        + (((Test2M * 100) / MaxMarks2M).ToString() == "NaN" ? 0 : ((Test2M * 100) / MaxMarks2M));
                            double p2 = (((Test2M * 100) / MaxMarks2M).ToString() == "NaN" ? 0 : ((Test2M * 100) / MaxMarks2M))
                                + (((Test3M * 100) / MaxMarks3M).ToString() == "NaN" ? 0 : ((Test3M * 100) / MaxMarks3M));
                            double p3 = (((Test3M * 100) / MaxMarks3M).ToString() == "NaN" ? 0 : ((Test3M * 100) / MaxMarks3M))
                                + (((Test1M * 100) / MaxMarks1M).ToString() == "NaN" ? 0 : ((Test1M * 100) / MaxMarks1M));

                            double sum1 = Test1M + Test2M;
                            double maxsum1 = MaxMarks1M + MaxMarks2M;
                            double sum2 = Test2M + Test3M;
                            double maxsum2 = MaxMarks2M + MaxMarks3M;
                            double sum3 = Test3M + Test1M;
                            double maxsum3 = MaxMarks3M + MaxMarks1M;
                            double totalmarks1 = p1 > p2 ? (p1 > p3 ? sum1 : (p2 > p3 ? sum2 : sum3)) : (p2 > p3 ? sum2 : sum3);
                            double totalmmmarks1 = 0;
                            if (MaxMarks1M == 0 && MaxMarks2M > 0 && MaxMarks3M > 0)
                            {
                                totalmmmarks1 = MaxMarks2M + MaxMarks3M;
                            }
                            if (MaxMarks1M > 0 && MaxMarks2M == 0 && MaxMarks3M > 0)
                            {
                                totalmmmarks1 = MaxMarks1M + MaxMarks3M;
                            }
                            if (MaxMarks1M > 0 && MaxMarks2M > 0 && MaxMarks3M == 0)
                            {
                                totalmmmarks1 = MaxMarks1M + MaxMarks2M;
                            }
                            if (MaxMarks1M > 0 && MaxMarks2M > 0 && MaxMarks3M > 0)
                            {
                                totalmmmarks1 = p1 > p2 ? (p1 > p3 ? maxsum1 : (p2 > p3 ? maxsum2 : maxsum3)) : (p2 > p3 ? maxsum2 : maxsum3);
                            }
                            if (totalmarks1!=0 && totalmmmarks1!=0)
                            {
                                percentle1 = ((totalmarks1) * 10) / totalmmmarks1;
                                ObtMarks1 = (Math.Round(percentle1 + SATM)).ToString(CultureInfo.CurrentCulture);
                                grade1 = grade(Math.Round(double.Parse(ObtMarks1), 1));
                            }

                            Response.Write("<tr id=" + id + ">");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + (i + 1) + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["srno"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["Name"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-tot-tit p-pad-n'><span id=''>" + dt.Rows[i]["FatherName"].ToString().ToUpper() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test1.ToString() + "' class='form-control-blue text-center' name='1' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test2.ToString() + "' class='form-control-blue text-centern' name='2' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + Test3.ToString() + "' class='form-control-blue text-center' name='3' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + totalmarks1.ToString() + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + percentle1.ToString("0.00") + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + SAT.ToString() + "' class='form-control-blue text-center' name='4' style='width:40px;'></td>");
                            double totals = (SATM);
                            double pTotal = 0;
                            if (totals > 0)
                            {
                                pTotal = pTotal + 10;
                            }
                            double bordPercentile = 0;
                            if (totals!=0 && MaxMarks6M!=0)
                            {
                                bordPercentile = (totals * MaxMarks6M) / (MaxMarks6M + pTotal);
                            }
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + bordPercentile.ToString("0.00") + "</span></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><input type='text' value='" + PracM.ToString() + "' class='form-control-blue text-center' name='5' style='width:40px;'></td>");
                            Response.Write("<td class='p-pad-n text-center tab-in'><span id=''>" + (bordPercentile + PracM).ToString("0.00") + "</span></td>");
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