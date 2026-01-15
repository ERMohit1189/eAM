using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public partial class _17_CumulativeReport : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    private string _sql = String.Empty;

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["SessionName"].ToString() == "" || Session["LoginName"].ToString() == "" || Session["BranchCode"].ToString() == "")
        {
            Response.Redirect("~/default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus();
        camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            BindClassDropdown();
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            //  BuildDynamicTable();
        }

    }

    protected void LoadBranch()
    {
        if (Session["LoginType"].ToString().ToLower() == "staff")
        {
            _sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpclass.SelectedValue;
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select BranchId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1)";
            oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        else
        {
            _sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpclass.SelectedValue;
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(_sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
    }
    private void BindClassDropdown()
    {
        string sql = "SELECT Id, ClassName FROM ClassMaster WHERE SessionName = '" + Session["SessionName"].ToString() + "' AND BranchCode = '" + Session["BranchCode"].ToString() + "'";
        var dt = oo.Fetchdata(sql);
        drpclass.DataSource = dt;
        drpclass.DataTextField = "ClassName";
        drpclass.DataValueField = "Id";
        drpclass.DataBind();
        drpclass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }
    //private void BuildDynamicTable()
    //{
    //    List<string> subjects = new List<string> { "ENGLISH", "HINDI" };

    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("SrNo");
    //    dt.Columns.Add("StudentName");
    //    dt.Columns.Add("Term");

    //    foreach (var sub in subjects)
    //    {
    //        dt.Columns.Add(sub + "_UT1", typeof(double));
    //        dt.Columns.Add(sub + "_UT2", typeof(double));
    //        dt.Columns.Add(sub + "_AVG", typeof(double));
    //        dt.Columns.Add(sub + "_HYAE", typeof(double));
    //        dt.Columns.Add(sub + "_TOTAL", typeof(double));
    //    }

    //    dt.Columns.Add("GrandTotal", typeof(double));
    //    dt.Columns.Add("Result");
    //    dt.Columns.Add("AttendancePresent", typeof(int));
    //    dt.Columns.Add("AttendanceTotal", typeof(int));

    //    // Sample Data
    //    dt.Rows.Add("1", "A", "I", 9, 16, 12.5, 57, 70, 16, 17, 16.5, 36, 53, 123, "Pass", 200, 250);
    //    dt.Rows.Add("1", "A", "II", 12, 18, 15, 52, 67, 19, 13, 16, 36, 52, 119, "Pass", 190, 250);
    //    dt.Rows.Add("2", "B", "I", 10, 15, 12.5, 50, 62.5, 18, 14, 16, 38, 54, 116.5, "Pass", 198, 250);
    //    dt.Rows.Add("2", "B", "II", 11, 17, 14, 58, 72, 20, 16, 18, 40, 58, 130, "Pass", 210, 250);

    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<table>");
    //    sb.Append("<tr>");
    //    sb.Append("<th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

    //    foreach (string sub in subjects)
    //        sb.Append(string.Format("<th colspan='5'>{0}</th>", sub));

    //    sb.Append("<th>Grand Total</th><th>Result</th><th colspan='2'>Attendance</th>");
    //    sb.Append("</tr>");

    //    // Subheader row
    //    sb.Append("<tr><td></td><td></td><td></td>");
    //    foreach (string sub in subjects)
    //        sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
    //    sb.Append("<td></td><td></td><th>Present</th><th>Total</th></tr>");

    //    // Group by student
    //    var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

    //    foreach (var studentGroup in studentGroups)
    //    {
    //        string srNo = studentGroup.First()["SrNo"].ToString();
    //        string studentName = studentGroup.Key;
    //        double grandTotalSum = 0;
    //        int presentSum = 0;
    //        int totalSum = 0;

    //        foreach (var row in studentGroup)
    //        {
    //            sb.Append("<tr>");
    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td><td>{2}</td>", srNo, studentName, row["Term"]));

    //            foreach (string sub in subjects)
    //            {
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_UT1"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_UT2"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_AVG"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_HYAE"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_TOTAL"]));
    //            }

    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td>", row["GrandTotal"], row["Result"]));
    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td>", row["AttendancePresent"], row["AttendanceTotal"]));
    //            sb.Append("</tr>");

    //            grandTotalSum += Convert.ToDouble(row["GrandTotal"]);
    //            presentSum += Convert.ToInt32(row["AttendancePresent"]);
    //            totalSum += Convert.ToInt32(row["AttendanceTotal"]);
    //        }

    //        // Total row per student
    //        sb.Append("<tr class='section-title'>");
    //        sb.Append(string.Format("<td></td><td>{0}</td><td>Total</td>", studentName));

    //        foreach (string sub in subjects)
    //        {
    //            double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
    //            double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
    //            double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
    //            double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
    //            double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>",
    //                totalUT1, totalUT2, totalAVG, totalHYAE, totalSubTotal));
    //        }

    //        sb.Append(string.Format("<td>{0}</td><td>Pass</td><td>{1}</td><td>{2}</td>",
    //            grandTotalSum, presentSum, totalSum));
    //        sb.Append("</tr>");
    //    }




    //    sb.Append("</table>");
    //    phTable.Controls.Add(new Literal { Text = sb.ToString() });
    //}


    //private void BuildDynamicTable()
    //{
    //    List<string> subjects = new List<string> { "ENGLISH", "HINDI" };

    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("SrNo");
    //    dt.Columns.Add("StudentName");
    //    dt.Columns.Add("Term");

    //    foreach (var sub in subjects)
    //    {
    //        dt.Columns.Add(sub + "_UT1", typeof(double));
    //        dt.Columns.Add(sub + "_UT2", typeof(double));
    //        dt.Columns.Add(sub + "_AVG", typeof(double));
    //        dt.Columns.Add(sub + "_HYAE", typeof(double));
    //        dt.Columns.Add(sub + "_TOTAL", typeof(double));
    //    }

    //    dt.Columns.Add("GrandTotal", typeof(double));
    //    dt.Columns.Add("Result");
    //    dt.Columns.Add("AttendancePresent", typeof(int));
    //    dt.Columns.Add("AttendanceTotal", typeof(int));

    //    // Sample Data
    //    dt.Rows.Add("1", "A", "I", 9, 16, 12.5, 57, 70, 16, 17, 16.5, 36, 53, 123, "Pass", 200, 250);
    //    dt.Rows.Add("1", "A", "II", 12, 18, 15, 52, 67, 19, 13, 16, 36, 52, 119, "Pass", 190, 250);
    //    dt.Rows.Add("2", "B", "I", 10, 15, 12.5, 50, 62.5, 18, 14, 16, 38, 54, 116.5, "Pass", 198, 250);
    //    dt.Rows.Add("2", "B", "II", 11, 17, 14, 58, 72, 20, 16, 18, 40, 58, 130, "Pass", 210, 250);

    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<table>");
    //    sb.Append("<tr>");
    //    sb.Append("<th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

    //    foreach (string sub in subjects)
    //        sb.Append(string.Format("<th colspan='5'>{0}</th>", sub));

    //    sb.Append("<th>Grand Total</th><th>Result</th><th colspan='2'>Attendance</th>");
    //    sb.Append("</tr>");

    //    // Subheader row
    //    sb.Append("<tr><td></td><td></td><td></td>");
    //    foreach (string sub in subjects)
    //        sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
    //    sb.Append("<td></td><td></td><th>Present</th><th>Total</th></tr>");

    //    // Group by student
    //    var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

    //    foreach (var studentGroup in studentGroups)
    //    {
    //        string srNo = studentGroup.First()["SrNo"].ToString();
    //        string studentName = studentGroup.Key;
    //        double grandTotalSum = 0;
    //        int presentSum = 0;
    //        int totalSum = 0;
    //        int rowIndex = 0;
    //        int termCount = studentGroup.Count();

    //        foreach (var row in studentGroup)
    //        {
    //            sb.Append("<tr>");

    //            if (rowIndex == 0)
    //            {
    //                sb.Append(string.Format("<td rowspan='{0}'>{1}</td>", termCount, srNo));
    //                sb.Append(string.Format("<td rowspan='{0}'>{1}</td>", termCount, studentName));
    //            }

    //            sb.Append(string.Format("<td>{0}</td>", row["Term"]));

    //            foreach (string sub in subjects)
    //            {
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_UT1"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_UT2"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_AVG"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_HYAE"]));
    //                sb.Append(string.Format("<td>{0}</td>", row[sub + "_TOTAL"]));
    //            }

    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td>", row["GrandTotal"], row["Result"]));
    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td>", row["AttendancePresent"], row["AttendanceTotal"]));
    //            sb.Append("</tr>");

    //            grandTotalSum += Convert.ToDouble(row["GrandTotal"]);
    //            presentSum += Convert.ToInt32(row["AttendancePresent"]);
    //            totalSum += Convert.ToInt32(row["AttendanceTotal"]);

    //            rowIndex++;
    //        }

    //        // Total row per student
    //        sb.Append("<tr class='section-title'>");
    //        sb.Append(string.Format("<td></td><td></td><td>Total</td>"));

    //        foreach (string sub in subjects)
    //        {
    //            double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
    //            double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
    //            double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
    //            double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
    //            double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

    //            sb.Append(string.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>",
    //                totalUT1, totalUT2, totalAVG, totalHYAE, totalSubTotal));
    //        }

    //        sb.Append(string.Format("<td>{0}</td><td>Pass</td><td>{1}</td><td>{2}</td>",
    //            grandTotalSum, presentSum, totalSum));
    //        sb.Append("</tr>");
    //    }

    //    sb.Append("</table>");
    //    phTable.Controls.Add(new Literal { Text = sb.ToString() });
    //}


    private void BuildDynamicTable1()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        // 1. Get subjects
        DataTable dtSubjects = oo.GridFill1(@"
        SELECT SubjectName FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable()
                                          .Select(r => r["SubjectName"].ToString())
                                          .ToList();

        // 2. Get students
        DataTable dtStudents = oo.GridFill1(@"
        SELECT SrNo, Name 
        FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        WHERE ClassId = " + classId + @" 
        AND SectionName = '" + sectionName + @"' 
        AND Withdrwal IS NULL 
        ORDER BY FirstName");

        // 3. Create dynamic data table
        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Result");
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        // 4. Populate dummy marks per student (for Term I & II)
        foreach (DataRow stu in dtStudents.Rows)
        {
            for (int term = 1; term <= 2; term++)
            {
                DataRow dr = dt.NewRow();
                dr["SrNo"] = stu["SrNo"];
                dr["StudentName"] = stu["Name"];
                dr["Term"] = term == 1 ? "I" : "II";

                foreach (string sub in subjects)
                {
                    dr[sub + "_UT1"] = 10 + term;
                    dr[sub + "_UT2"] = 12 + term;
                    dr[sub + "_AVG"] = 11 + term;
                    dr[sub + "_HYAE"] = 40 + term;
                    dr[sub + "_TOTAL"] = 60 + term;
                }

                dr["GrandTotal"] = 120 + term;
                dr["Result"] = "Pass";
                dr["AttendancePresent"] = 190 + (term * 5);
                dr["AttendanceTotal"] = 250;
                dt.Rows.Add(dr);
            }
        }

        // 5. Build HTML
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr>");
        sb.Append("<th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

        foreach (string sub in subjects)
            sb.Append("<th colspan='5'>" + sub + "</th>");

        sb.Append("<th>Grand Total</th><th>Result</th><th colspan='2'>Attendance</th>");
        sb.Append("</tr>");

        // Subheader
        sb.Append("<tr><td></td><td></td><td></td>");
        foreach (string sub in subjects)
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        sb.Append("<td></td><td></td><th>Present</th><th>Total</th></tr>");

        // Group by student
        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            double grandTotalSum = 0;
            int presentSum = 0;
            int totalSum = 0;
            int rowIndex = 0;
            int termCount = studentGroup.Count();

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");

                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }

                sb.Append("<td>" + row["Term"] + "</td>");

                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }

                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Result"] + "</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                grandTotalSum += Convert.ToDouble(row["GrandTotal"]);
                presentSum += Convert.ToInt32(row["AttendancePresent"]);
                totalSum += Convert.ToInt32(row["AttendanceTotal"]);
                rowIndex++;
            }

            // Student Total Row
            sb.Append("<tr class='section-title'>");
            sb.Append("<td></td><td>" + studentName + "</td><td>Total</td>");

            foreach (string sub in subjects)
            {
                double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
                double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
                double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
                double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
                double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

                sb.Append("<td>" + totalUT1 + "</td><td>" + totalUT2 + "</td><td>" + totalAVG + "</td><td>" + totalHYAE + "</td><td>" + totalSubTotal + "</td>");
            }

            sb.Append("<td>" + grandTotalSum + "</td><td>Pass</td><td>" + presentSum + "</td><td>" + totalSum + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
    }

    private void BuildDynamicTable2()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        // 1. Get Subjects
        DataTable dtSubjects = oo.GridFill1(@"
        SELECT SubjectName, Id 
        FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable()
                                          .Select(r => r["SubjectName"].ToString())
                                          .ToList();

        Dictionary<string, string> subjectIdMap = dtSubjects.AsEnumerable()
            .ToDictionary(r => r["SubjectName"].ToString(), r => r["Id"].ToString());

        // 2. Get Students
        DataTable dtStudents = oo.GridFill1(@"
        SELECT SrNo, Name 
        FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        WHERE ClassId = " + classId + @" 
        AND SectionName = '" + sectionName + @"' 
        AND Withdrwal IS NULL 
        ORDER BY FirstName");

        // 3. Get All Marks
        DataTable dtMarks = oo.GridFill1(@"
        SELECT 
            SRNO,
            Evaluation,
            SubjectId,
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
            (
                ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
                ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
            ) / 2 AS AVG,
            ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE,
            ISNULL(TRY_CAST(NULLIF(Prac, '') AS FLOAT), 0) AS TOTAL
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode);

        // 4. Prepare Dynamic DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Result");
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        // 5. Populate Marks for each student
        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;

                foreach (string sub in subjects)
                {
                    string subId = subjectIdMap[sub];
                    var marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subId + "'");

                    if (marks.Length > 0)
                    {
                        var m = marks[0];
                        dr[sub + "_UT1"] = m["UT1"];
                        dr[sub + "_UT2"] = m["UT2"];
                        dr[sub + "_AVG"] = m["AVG"];
                        dr[sub + "_HYAE"] = m["HYAE"];
                        dr[sub + "_TOTAL"] = m["TOTAL"];
                        grandTotal += Convert.ToDouble(m["TOTAL"]);
                    }
                    else
                    {
                        dr[sub + "_UT1"] = 0;
                        dr[sub + "_UT2"] = 0;
                        dr[sub + "_AVG"] = 0;
                        dr[sub + "_HYAE"] = 0;
                        dr[sub + "_TOTAL"] = 0;
                    }
                }

                dr["GrandTotal"] = grandTotal;
                dr["Result"] = "Pass"; // Add logic if needed
                dr["AttendancePresent"] = 200 + term * 5;
                dr["AttendanceTotal"] = 250;

                dt.Rows.Add(dr);
            }
        }

        // 6. Render HTML Table
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

        foreach (string sub in subjects)
            sb.Append("<th colspan='5'>" + sub + "</th>");

        sb.Append("<th>Grand Total</th><th>Result</th><th colspan='2'>Attendance</th></tr>");
        sb.Append("<tr><td></td><td></td><td></td>");
        foreach (string sub in subjects)
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        sb.Append("<td></td><td></td><th>Present</th><th>Total</th></tr>");

        // Group by student
        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            double grandTotalSum = 0;
            int presentSum = 0;
            int totalSum = 0;
            int rowIndex = 0;
            int termCount = studentGroup.Count();

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");

                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }

                sb.Append("<td>" + row["Term"] + "</td>");

                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }

                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Result"] + "</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                grandTotalSum += Convert.ToDouble(row["GrandTotal"]);
                presentSum += Convert.ToInt32(row["AttendancePresent"]);
                totalSum += Convert.ToInt32(row["AttendanceTotal"]);
                rowIndex++;
            }

            // Total row per student
            sb.Append("<tr class='section-title'>");
            sb.Append("<td></td><td>" + studentName + "</td><td>Total</td>");

            foreach (string sub in subjects)
            {
                double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
                double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
                double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
                double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
                double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

                sb.Append("<td>" + totalUT1 + "</td><td>" + totalUT2 + "</td><td>" + totalAVG + "</td><td>" + totalHYAE + "</td><td>" + totalSubTotal + "</td>");
            }

            sb.Append("<td>" + grandTotalSum + "</td><td>Pass</td><td>" + presentSum + "</td><td>" + totalSum + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
    }

    private void BuildDynamicTable3()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        // 1. Get Subjects
        DataTable dtSubjects = oo.GridFill1(@"
        SELECT SubjectName, Id 
        FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable()
                                          .Select(r => r["SubjectName"].ToString())
                                          .ToList();

        Dictionary<string, string> subjectIdMap = dtSubjects.AsEnumerable()
            .ToDictionary(r => r["SubjectName"].ToString(), r => r["Id"].ToString());

        // 2. Get Students
        DataTable dtStudents = oo.GridFill1(@"
        SELECT SrNo, Name 
        FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        WHERE ClassId = " + classId + @" 
        AND SectionName = '" + sectionName + @"' 
        AND Withdrwal IS NULL 
        ORDER BY FirstName");

        // 3. Get All Marks
        DataTable dtMarks = oo.GridFill1(@"
        SELECT 
            SRNO,
            Evaluation,
            SubjectId,
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
            (
                ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
                ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
            ) / 2 AS AVG,
            ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode);

        // 4. Prepare Dynamic DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Result");
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        // 5. Populate Rows Per Student Per Term
        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;

                foreach (string sub in subjects)
                {
                    string subId = subjectIdMap[sub];
                    var marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subId + "'");

                    if (marks.Length > 0)
                    {
                        var m = marks[0];
                        double ut1 = Convert.ToDouble(m["UT1"]);
                        double ut2 = Convert.ToDouble(m["UT2"]);
                        double avg = Convert.ToDouble(m["AVG"]);
                        double hyae = Convert.ToDouble(m["HYAE"]);
                        // double total = avg + hyae;
                        double total = Math.Ceiling(avg + hyae);


                        dr[sub + "_UT1"] = ut1;
                        dr[sub + "_UT2"] = ut2;
                        dr[sub + "_AVG"] = avg;
                        dr[sub + "_HYAE"] = hyae;
                        dr[sub + "_TOTAL"] = total;

                        grandTotal += total;
                    }
                    else
                    {
                        dr[sub + "_UT1"] = 0;
                        dr[sub + "_UT2"] = 0;
                        dr[sub + "_AVG"] = 0;
                        dr[sub + "_HYAE"] = 0;
                        dr[sub + "_TOTAL"] = 0;
                    }
                }

                dr["GrandTotal"] = grandTotal;
                dr["Result"] = "Pass"; // logic can be added
                dr["AttendancePresent"] = 200 + term * 5;
                dr["AttendanceTotal"] = 250;
                dt.Rows.Add(dr);
            }
        }

        // 6. Render HTML
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

        foreach (string sub in subjects)
            sb.Append("<th colspan='5'>" + sub + "</th>");

        sb.Append("<th>Grand Total</th><th>Result</th><th colspan='2'>Attendance</th></tr>");
        sb.Append("<tr><td></td><td></td><td></td>");
        foreach (string sub in subjects)
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        sb.Append("<td></td><td></td><th>Present</th><th>Total</th></tr>");

        // Group by student
        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            double grandTotalSum = 0;
            int presentSum = 0;
            int totalSum = 0;
            int rowIndex = 0;
            int termCount = studentGroup.Count();

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }

                sb.Append("<td>" + row["Term"] + "</td>");

                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }

                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Result"] + "</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                grandTotalSum += Convert.ToDouble(row["GrandTotal"]);
                presentSum += Convert.ToInt32(row["AttendancePresent"]);
                totalSum += Convert.ToInt32(row["AttendanceTotal"]);
                rowIndex++;
            }

            // Student Total Row
            sb.Append("<tr class='section-title'>");
            sb.Append("<td></td><td>" + studentName + "</td><td>Total</td>");

            foreach (string sub in subjects)
            {
                double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
                double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
                double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
                double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
                double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

                sb.Append("<td>" + totalUT1 + "</td><td>" + totalUT2 + "</td><td>" + totalAVG + "</td><td>" + totalHYAE + "</td><td>" + totalSubTotal + "</td>");
            }

            sb.Append("<td>" + grandTotalSum + "</td><td>Pass</td><td>" + presentSum + "</td><td>" + totalSum + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
    }




    private void BuildDynamicTable4()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        // 1. Get Subjects
        DataTable dtSubjects = oo.GridFill1(@"
        SELECT SubjectName, Id 
        FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable()
                                          .Select(r => r["SubjectName"].ToString())
                                          .ToList();

        Dictionary<string, string> subjectIdMap = dtSubjects.AsEnumerable()
            .ToDictionary(r => r["SubjectName"].ToString(), r => r["Id"].ToString());

        // 2. Get Students
        DataTable dtStudents = oo.GridFill1(@"
        SELECT SrNo, Name 
        FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        WHERE ClassId = " + classId + @" 
        AND SectionName = '" + sectionName + @"' 
        AND Withdrwal IS NULL 
        ORDER BY FirstName");

        // 3. Get All Marks
        DataTable dtMarks = oo.GridFill1(@"
        SELECT 
            SRNO,
            Evaluation,
            SubjectId,
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
            (
                ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
                ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
            ) / 2 AS AVG,
            ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode);

        // 4. Prepare Dynamic DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Percentage", typeof(double));
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        int totalMarks = subjects.Count * 100;

        // 5. Populate Rows Per Student Per Term
        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;

                foreach (string sub in subjects)
                {
                    string subId = subjectIdMap[sub];
                    var marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subId + "'");

                    if (marks.Length > 0)
                    {
                        var m = marks[0];
                        double ut1 = Convert.ToDouble(m["UT1"]);
                        double ut2 = Convert.ToDouble(m["UT2"]);
                        double avg = Convert.ToDouble(m["AVG"]);
                        double hyae = Convert.ToDouble(m["HYAE"]);
                        double total = Math.Ceiling(avg + hyae);

                        dr[sub + "_UT1"] = ut1;
                        dr[sub + "_UT2"] = ut2;
                        dr[sub + "_AVG"] = avg;
                        dr[sub + "_HYAE"] = hyae;
                        dr[sub + "_TOTAL"] = total;

                        grandTotal += total;
                    }
                    else
                    {
                        dr[sub + "_UT1"] = 0;
                        dr[sub + "_UT2"] = 0;
                        dr[sub + "_AVG"] = 0;
                        dr[sub + "_HYAE"] = 0;
                        dr[sub + "_TOTAL"] = 0;
                    }
                }

                // Percentage Calculation
                double percentage = (totalMarks > 0) ? (grandTotal / totalMarks) * 100 : 0;

                dr["GrandTotal"] = grandTotal;
                dr["Percentage"] = Math.Round(percentage, 2);

                // Fetch Attendance
                string currentTerm = term == 1 ? "Term1" : "Term2";

                DataTable dtAttendance = oo.GridFill1(@"
                SELECT TA, TWD
                FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") asr
                LEFT JOIN Exam_AttendenceDetails ead 
                    ON ead.srno = asr.SrNo 
                    AND ead.SessionName = asr.SessionName 
                    AND ead.BranchCode = asr.BranchCode 
                WHERE ead.Term = '" + currentTerm + @"'
                    AND asr.SrNo = '" + srno + @"'");

                if (dtAttendance.Rows.Count > 0)
                {
                    dr["AttendanceTotal"] = Convert.ToInt32(dtAttendance.Rows[0]["TA"]);
                    dr["AttendancePresent"] = Convert.ToInt32(dtAttendance.Rows[0]["TWD"]);
                }
                else
                {
                    dr["AttendanceTotal"] = 0;
                    dr["AttendancePresent"] = 0;
                }

                dt.Rows.Add(dr);
            }
        }

        // 6. Render HTML
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

        foreach (string sub in subjects)
            sb.Append("<th colspan='5'>" + sub + "</th>");

        sb.Append("<th>Grand Total</th><th>Percentage</th><th colspan='2'>Attendance</th></tr>");
        sb.Append("<tr><td></td><td></td><td></td>");
        foreach (string sub in subjects)
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        sb.Append("<td></td><td></td><th>Present</th><th>Total</th></tr>");

        // Group by student
        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            int rowIndex = 0;
            int termCount = studentGroup.Count();

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }

                sb.Append("<td>" + row["Term"] + "</td>");

                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }

                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Percentage"] + "%</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                rowIndex++;
            }
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
    }

    private void BuildDynamicTableLast()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        // 1. Get Subjects with IsAdditional
        DataTable dtSubjects = oo.GridFill1(@"
    SELECT SubjectName, Id, IsAditional
    FROM TTSubjectMaster 
    WHERE ClassId = " + classId + @"
    AND BranchCode = " + branchCode + @"
    AND SessionName = '" + sessionName + @"'
    AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable()
                                          .Select(r => r["SubjectName"].ToString())
                                          .ToList();

        //Dictionary<string, (string Id, bool IsAdditional)> subjectInfoMap = dtSubjects.AsEnumerable()
        //    .ToDictionary(
        //        r => r["SubjectName"].ToString(),
        //        r => (r["Id"].ToString(), Convert.ToBoolean(r["IsAdditional"]))
        //    );


        Dictionary<string, SubjectInfo> subjectInfoMap = dtSubjects.AsEnumerable()
    .ToDictionary(
        r => r["SubjectName"].ToString(),
        r => new SubjectInfo
        {
            Id = r["Id"].ToString(),
            IsAdditional = Convert.ToBoolean(r["IsAditional"])
        }
    );
        // 2. Get Students
        DataTable dtStudents = oo.GridFill1(@"
    SELECT SrNo, Name 
    FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
    WHERE ClassId = " + classId + @" 
    AND SectionName = '" + sectionName + @"' 
    AND Withdrwal IS NULL 
    ORDER BY FirstName");

        // 3. Get All Marks
        DataTable dtMarks = oo.GridFill1(@"
    SELECT 
        SRNO,
        Evaluation,
        SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
        ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE
    FROM SGSMarkEntryJunior
    WHERE ClassId = " + classId + @"
    AND SessionName = '" + sessionName + @"'
    AND BranchCode = " + branchCode);

        // 4. Prepare Dynamic DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Percentage", typeof(double));
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        int totalMaxMarks = subjectInfoMap.Count(s => !s.Value.IsAdditional) * 100;

        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;
                double percentageBaseTotal = 0;

                foreach (string sub in subjects)
                {
                    //  var (subId, isAdditional) = subjectInfoMap[sub];

                    var subjectInfo = subjectInfoMap[sub];
                    string subId = subjectInfo.Id;
                    bool isAdditional = subjectInfo.IsAdditional;
                    var marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subId + "'");

                    if (marks.Length > 0)
                    {
                        var m = marks[0];
                        double ut1 = Convert.ToDouble(m["UT1"]);
                        double ut2 = Convert.ToDouble(m["UT2"]);
                        double avg = Convert.ToDouble(m["AVG"]);
                        double hyae = Convert.ToDouble(m["HYAE"]);
                        double total = Math.Ceiling(avg + hyae);

                        dr[sub + "_UT1"] = ut1;
                        dr[sub + "_UT2"] = ut2;
                        dr[sub + "_AVG"] = avg;
                        dr[sub + "_HYAE"] = hyae;
                        dr[sub + "_TOTAL"] = total;

                        grandTotal += total;
                        if (!isAdditional)
                        {
                            percentageBaseTotal += total;
                        }
                    }
                    else
                    {
                        dr[sub + "_UT1"] = 0;
                        dr[sub + "_UT2"] = 0;
                        dr[sub + "_AVG"] = 0;
                        dr[sub + "_HYAE"] = 0;
                        dr[sub + "_TOTAL"] = 0;
                    }
                }

                double percentage = totalMaxMarks > 0 ? (percentageBaseTotal / totalMaxMarks) * 100 : 0;
                dr["GrandTotal"] = grandTotal;
                dr["Percentage"] = Math.Round(percentage, 2);

                // Attendance
                DataTable dtAttendance = oo.GridFill1(@"
            SELECT TA, TWD
            FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") asr
            LEFT JOIN Exam_AttendenceDetails ead 
                ON ead.srno = asr.SrNo 
                AND ead.SessionName = asr.SessionName 
                AND ead.BranchCode = asr.BranchCode 
            WHERE ead.Term = '" + evaluation + @"'
                AND asr.SrNo = '" + srno + @"'");

                if (dtAttendance.Rows.Count > 0)
                {
                    dr["AttendanceTotal"] = Convert.ToInt32(dtAttendance.Rows[0]["TA"]);
                    dr["AttendancePresent"] = Convert.ToInt32(dtAttendance.Rows[0]["TWD"]);
                }
                else
                {
                    dr["AttendanceTotal"] = 0;
                    dr["AttendancePresent"] = 0;
                }

                dt.Rows.Add(dr);
            }
        }

        // 6. Render HTML with Total Row
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");
        foreach (string sub in subjects)
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        sb.Append("<th>Grand Total</th><th>Percentage</th><th>Present</th><th>Total</th></tr>");

        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            int rowIndex = 0;
            int termCount = studentGroup.Count();
            double studentGrandTotal = 0;
            double studentPercentageTotal = 0;
            int studentPresentTotal = 0, studentAttendanceTotal = 0;

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }
                sb.Append("<td>" + row["Term"] + "</td>");
                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }
                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Percentage"] + "%</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                studentGrandTotal += Convert.ToDouble(row["GrandTotal"]);
                studentPercentageTotal += Convert.ToDouble(row["Percentage"]);
                studentPresentTotal += Convert.ToInt32(row["AttendancePresent"]);
                studentAttendanceTotal += Convert.ToInt32(row["AttendanceTotal"]);
                rowIndex++;
            }

            // Total Row
            sb.Append("<tr class='section-title'>");
            sb.Append("<td></td><td>" + studentName + "</td><td>Total</td>");
            foreach (string sub in subjects)
            {
                double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
                double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
                double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
                double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
                double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

                sb.Append("<td>" + totalUT1 + "</td><td>" + totalUT2 + "</td><td>" + totalAVG + "</td><td>" + totalHYAE + "</td><td>" + totalSubTotal + "</td>");
            }
            double overallPercentage = termCount > 0 ? studentPercentageTotal / termCount : 0;

            sb.Append("<td>" + studentGrandTotal + "</td>");
            sb.Append("<td>" + Math.Round(overallPercentage, 2) + "%</td>");
            sb.Append("<td>" + studentPresentTotal + "</td>");
            sb.Append("<td>" + studentAttendanceTotal + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
        icons.Visible = true;
    }


    private void BuildDynamicTableue()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        DataTable dtSubjects = oo.GridFill1(@"
        SELECT SubjectName, Id, IsAditional 
        FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = new List<string>();
        Dictionary<string, SubjectInfo> subjectInfoMap = new Dictionary<string, SubjectInfo>();

        foreach (DataRow r in dtSubjects.Rows)
        {
            string subjectName = r["SubjectName"].ToString();
            subjects.Add(subjectName);
            subjectInfoMap[subjectName] = new SubjectInfo
            {
                Id = r["Id"].ToString(),
                IsAdditional = Convert.ToBoolean(r["IsAditional"])
            };
        }

        DataTable dtStudents = oo.GridFill1(@"
        SELECT SrNo, Name 
        FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        WHERE ClassId = " + classId + @" 
        AND SectionName = '" + sectionName + @"' 
        AND Withdrwal IS NULL 
        ORDER BY FirstName");

        DataTable dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId, 
               ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
               ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
               (ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) + ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)) / 2 AS AVG,
               ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode);

        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (string sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Percentage", typeof(double));
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        int totalMaxMarks = 0;
        foreach (var si in subjectInfoMap.Values)
        {
            if (!si.IsAdditional) totalMaxMarks += 100;
        }

        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;
                double percentageBaseTotal = 0;

                foreach (string sub in subjects)
                {
                    SubjectInfo subjectInfo = subjectInfoMap[sub];
                    DataRow[] marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subjectInfo.Id + "'");

                    double ut1 = 0, ut2 = 0, avg = 0, hyae = 0, total = 0;
                    if (marks.Length > 0)
                    {
                        DataRow m = marks[0];
                        ut1 = Convert.ToDouble(m["UT1"]);
                        ut2 = Convert.ToDouble(m["UT2"]);
                        avg = Convert.ToDouble(m["AVG"]);
                        hyae = Convert.ToDouble(m["HYAE"]);
                        total = Math.Ceiling(avg + hyae);
                    }

                    dr[sub + "_UT1"] = ut1;
                    dr[sub + "_UT2"] = ut2;
                    dr[sub + "_AVG"] = avg;
                    dr[sub + "_HYAE"] = hyae;
                    dr[sub + "_TOTAL"] = total;

                    grandTotal += total;
                    if (!subjectInfo.IsAdditional)
                        percentageBaseTotal += total;
                }

                dr["GrandTotal"] = grandTotal;
                dr["Percentage"] = totalMaxMarks > 0 ? Math.Round((percentageBaseTotal / totalMaxMarks) * 100, 2) : 0;

                DataTable dtAttendance = oo.GridFill1(@"
                SELECT TA, TWD
                FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") asr
                LEFT JOIN Exam_AttendenceDetails ead 
                    ON ead.srno = asr.SrNo 
                    AND ead.SessionName = asr.SessionName 
                    AND ead.BranchCode = asr.BranchCode 
                WHERE ead.Term = '" + evaluation + @"'
                AND asr.SrNo = '" + srno + @"'");

                dr["AttendanceTotal"] = dtAttendance.Rows.Count > 0 ? Convert.ToInt32(dtAttendance.Rows[0]["TA"]) : 0;
                dr["AttendancePresent"] = dtAttendance.Rows.Count > 0 ? Convert.ToInt32(dtAttendance.Rows[0]["TWD"]) : 0;

                dt.Rows.Add(dr);
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>#</th><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");

        foreach (string sub in subjects)
        {
            sb.Append("<th colspan='5'>" + sub + "</th>");
        }

        sb.Append("<th>Grand Total</th><th>Percentage</th><th>Present</th><th>Total</th></tr>");

        sb.Append("<tr><td></td><td></td><td></td><td></td>");
        foreach (string sub in subjects)
        {
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        }
        sb.Append("<td></td><td></td><td></td><td></td></tr>");

        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));
        int serialNo = 1;

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            int rowIndex = 0;
            int termCount = studentGroup.Count();

            foreach (DataRow row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + serialNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }
                sb.Append("<td>" + row["Term"] + "</td>");
                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }
                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Percentage"] + "%</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");
                rowIndex++;
            }
            serialNo++;
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
        icons.Visible = true;
    }

    private void BuildDynamicTable()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        int branchId = Convert.ToInt32(drpBranch.SelectedValue);
        string sectionName = drpSection.SelectedValue;
        string displayOrder = DropDownList1.SelectedValue;
        string status = drpStatus.SelectedValue;

        DataTable dtClassID = oo.GridFill1(@"
     Select *,(Select top 1 CourseName from CourseMaster where id=ClassMaster.Course)CourseName from ClassMaster where Id = " + classId + @"
        AND BranchCode = " + branchCode + @"");

        List<string> Course = dtClassID.AsEnumerable().Select(r => r["CourseName"].ToString()).ToList();




        DataTable dtSubjects = oo.GridFill1(@"
        SELECT DISTINCT  SubjectName, Id, IsAditional
        FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        and BranchId=" + branchId + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable().Select(r => r["SubjectName"].ToString()).ToList();

        Dictionary<string, SubjectInfo> subjectInfoMap = dtSubjects.AsEnumerable()
            .ToDictionary(
                r => r["SubjectName"].ToString(),
                r => new SubjectInfo
                {
                    Id = r["Id"].ToString(),
                    IsAdditional = Convert.ToBoolean(r["IsAditional"])
                }
            );


        //DataTable dtStudents = oo.GridFill1(@"
        //SELECT SrNo, Name 
        //FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        //WHERE ClassId = " + classId + @" 
        //AND SectionName = '" + sectionName + @"' 
        //AND Withdrwal IS NULL 
        //ORDER BY FirstName");


        string sql = @"
DECLARE @displayorder NVARCHAR(50) = '" + displayOrder + @"';
DECLARE @status NVARCHAR(10) = '" + status + @"';

SELECT SrNo, Name 
FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") as asr
WHERE ClassId = " + classId + @"
AND SectionName = '" + sectionName + @"'
AND 
(
      (@status = 'A' 
        AND ISNULL(Withdrwal, '') = '' 
        AND ISNULL(blocked, '') = '' 
        AND ISNULL(promotion, '') <> 'Cancelled'
    )
    OR
    (@status = 'AB' 
        AND (
            ISNULL(Withdrwal, '') = '' 
            OR ISNULL(blocked, '') = 'Yes'
        )
        AND ISNULL(promotion, '') <> 'Cancelled'
    )
    OR
    (@status = 'W' 
     
        AND  ISNULL(Withdrwal, '') = 'Yes' 
        AND ISNULL(promotion, '') <> 'Cancelled'
    )
    OR
    (@status = 'B' 
        AND ISNULL(blocked, '') = 'Yes' 
        AND ISNULL(promotion, '') <> 'Cancelled'
    )
    OR
    (@status NOT IN ('A','AB','W','B')  
        AND ISNULL(promotion, '') <> 'Cancelled'
    )
)
ORDER BY   
    CASE   
        WHEN @displayorder = 'InstituteRollNo' THEN CAST(asr.InstituteRollNo AS NVARCHAR(50))  
        WHEN @displayorder = 'Name' THEN asr.Name  
        WHEN @displayorder = 'Id' THEN CAST(asr.SrNo AS NVARCHAR(50))  
        WHEN @displayorder = 'doa' THEN CONVERT(NVARCHAR(50), asr.DOB, 103)   
        ELSE asr.Name  
    END
";

        DataTable dtStudents = oo.GridFill1(sql);


        DataTable dtMarks = new DataTable();
        if (Course[0].ToString() == "PRE PRIMARY")
        {
            dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
       CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryPrePrimary
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
            AND BranchCode = " + branchCode + @"
    UNION ALL
    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode

        );
        }
        else if (Course[0].ToString() == "PRIMARY")
        {
            dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
        CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryPrimary
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
          AND BranchCode = " + branchCode + @"
    UNION ALL
    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode

        );
        }
        else if (Course[0].ToString() == "JUNIOR")
        {
            dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
       CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode + @"

    UNION ALL

    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode

        );
        }
        else if (Course[0].ToString() == "SECONDARY")
        {
            dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
        CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
          AND BranchCode = " + branchCode + @"
    UNION ALL
    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode
        );
        }
        else if (Course[0].ToString() == "SENIOR SECONDARY")
        {
            if (drpclass.SelectedItem.Text == "XI")
            {
                dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
       CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryXI
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
           AND BranchCode = " + branchCode + @"
    UNION ALL
    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode


        );
            }
            if (drpclass.SelectedItem.Text == "XII")
            {
                dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
       CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryXII
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
            AND BranchCode = " + branchCode + @"
    UNION ALL
    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode


        );
            }

        }
        else
        {
            dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
        CAST(ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS NVARCHAR(10)) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
           AND BranchCode = " + branchCode + @"
    UNION ALL
    SELECT 
        SRNO, 
        Evaluation, 
        SubjectId,
        '' AS UT1,
        '' AS UT2,
        0 AS AVG,
        Grade AS HYAE
    FROM AdditionalGradesEntry
    WHERE 
       ClassId = " + classId + @"
       AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode

        );
        }




        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(string));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Percentage", typeof(double));
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        int totalMaxMarks = subjectInfoMap.Count(s => !s.Value.IsAdditional) * 100;

        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                string evaluation1 = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0, percentageBaseTotal = 0;

                foreach (string sub in subjects)
                {
                    var subjectInfo = subjectInfoMap[sub];
                    string subId = subjectInfo.Id;
                    bool isAdditional = subjectInfo.IsAdditional;
                    if (isAdditional)
                    {
                        if (evaluation1 == "Term1")
                        {
                            evaluation1 = "Term 1";
                        }
                        if (evaluation1 == "Term2")
                        {
                            evaluation1 = "Term 2";
                        }
                    }

                    var marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation1 + "' AND SubjectId = '" + subId + "'");
                    if (marks.Length > 0)
                    {
                        if (!isAdditional)
                        {
                            var m = marks[0];
                            double ut1 = Convert.ToDouble(m["UT1"]);
                            double ut2 = Convert.ToDouble(m["UT2"]);
                            double avg = Convert.ToDouble(m["AVG"]);
                            double hyae = Convert.ToDouble(m["HYAE"]);
                            double total = Math.Ceiling(avg + hyae);

                            dr[sub + "_UT1"] = ut1;
                            dr[sub + "_UT2"] = ut2;
                            dr[sub + "_AVG"] = avg;
                            dr[sub + "_HYAE"] = hyae;
                            dr[sub + "_TOTAL"] = total;
                            grandTotal += total;
                            if (!isAdditional) percentageBaseTotal += total;
                        }
                        else
                        {
                            var m = marks[0];
                            string ut1 = "";
                            string ut2 = "";
                            string avg = "";
                            string hyae = m["HYAE"].ToString();
                            double total = 0;

                            dr[sub + "_UT1"] = 0;
                            dr[sub + "_UT2"] = 0;
                            dr[sub + "_AVG"] = 0;
                            dr[sub + "_HYAE"] = hyae;
                            dr[sub + "_TOTAL"] = total;
                            grandTotal += total;
                            if (!isAdditional) percentageBaseTotal += total;
                        }


                    }
                    else
                    {
                        dr[sub + "_UT1"] = 0;
                        dr[sub + "_UT2"] = 0;
                        dr[sub + "_AVG"] = 0;
                        dr[sub + "_HYAE"] = 0;
                        dr[sub + "_TOTAL"] = 0;
                    }
                }

                double percentage = totalMaxMarks > 0 ? (percentageBaseTotal / totalMaxMarks) * 100 : 0;
                dr["GrandTotal"] = grandTotal;
                dr["Percentage"] = Math.Round(percentage, 2);

                DataTable dtAttendance = oo.GridFill1(@"
                SELECT TA, TWD
                FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") asr
                LEFT JOIN Exam_AttendenceDetails ead 
                    ON ead.srno = asr.SrNo 
                    AND ead.SessionName = asr.SessionName 
                    AND ead.BranchCode = asr.BranchCode 
                WHERE ead.Term = '" + evaluation + @"'
                AND asr.SrNo = '" + srno + @"'");

                if (dtAttendance.Rows.Count > 0)
                {
                    dr["AttendanceTotal"] = dtAttendance.Rows[0]["TA"] != DBNull.Value && !string.IsNullOrWhiteSpace(dtAttendance.Rows[0]["TA"].ToString())
    ? Convert.ToInt32(dtAttendance.Rows[0]["TA"])
    : 0;

                    dr["AttendancePresent"] = dtAttendance.Rows[0]["TWD"] != DBNull.Value && !string.IsNullOrWhiteSpace(dtAttendance.Rows[0]["TWD"].ToString())
    ? Convert.ToInt32(dtAttendance.Rows[0]["TWD"])
    : 0;
                }
                else
                {
                    dr["AttendanceTotal"] = 0;
                    dr["AttendancePresent"] = 0;
                }

                dt.Rows.Add(dr);
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>#</th><th>SrNo</th><th>Student's Name</th><th>Term</th>");
        foreach (string sub in subjects)
        {
            sb.Append("<th colspan='5'>" + sub + "</th>");
        }
        sb.Append("<th>Grand Total</th><th>Percentage</th><th colspan='2'>Attendence</th></tr>");

        sb.Append("<tr><td></td><td></td><td></td><td></td>");
        foreach (string sub in subjects)
        {
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        }
        sb.Append("<td></td><td></td><td>Present</td><td>Total</td></tr>");

        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));
        int serialNo = 1;

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            int rowIndex = 0, termCount = studentGroup.Count();
            double studentGrandTotal = 0, studentPercentageTotal = 0;
            int studentPresentTotal = 0, studentAttendanceTotal = 0;

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + serialNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }

                sb.Append("<td>" + row["Term"] + "</td>");
                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }

                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Percentage"] + "%</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                studentGrandTotal += row["GrandTotal"] != DBNull.Value ? Convert.ToDouble(row["GrandTotal"]) : 0;
                studentPercentageTotal += row["Percentage"] != DBNull.Value ? Convert.ToDouble(row["Percentage"]) : 0;
                studentPresentTotal += row["AttendancePresent"] != DBNull.Value ? Convert.ToInt32(row["AttendancePresent"]) : 0;
                studentAttendanceTotal += row["AttendanceTotal"] != DBNull.Value ? Convert.ToInt32(row["AttendanceTotal"]) : 0;

                rowIndex++;
            }

            // Total row
            sb.Append("<tr style='font-weight:bold;background-color:#f0f0f0;'>");
            sb.Append("<td class='text-right' colspan='4'>Total</td>");
            foreach (string sub in subjects)
            {
                double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
                double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
                double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
                //double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));

                double totalHYAE = studentGroup.Sum(r =>
                {
                    double val;
                    return double.TryParse(r[sub + "_HYAE"].ToString(), out val) ? val : 0;
                });

                double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

                sb.Append("<td>" + totalUT1 + "</td>");
                sb.Append("<td>" + totalUT2 + "</td>");
                sb.Append("<td>" + totalAVG + "</td>");
                sb.Append("<td>" + totalHYAE + "</td>");
                sb.Append("<td>" + totalSubTotal + "</td>");
            }
            double overallPercentage = termCount > 0 ? (studentPercentageTotal / termCount) : 0;
            sb.Append("<td>" + studentGrandTotal + "</td>");
            sb.Append("<td>" + Math.Round(overallPercentage, 2) + "%</td>");
            sb.Append("<td>" + studentPresentTotal + "</td>");
            sb.Append("<td>" + studentAttendanceTotal + "</td>");
            sb.Append("</tr>");

            serialNo++;
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
        icons.Visible = true;
    }

    private void BuildDynamicTableNEw()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        DataTable dtSubjects = oo.GridFill1(@"
        SELECT SubjectName, Id, IsAditional 
        FROM TTSubjectMaster 
        WHERE ClassId = " + classId + @"
        AND BranchCode = " + branchCode + @"
        AND SessionName = '" + sessionName + @"'
        AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = new List<string>();
        Dictionary<string, SubjectInfo> subjectInfoMap = new Dictionary<string, SubjectInfo>();

        foreach (DataRow r in dtSubjects.Rows)
        {
            string subjectName = r["SubjectName"].ToString();
            subjects.Add(subjectName);
            subjectInfoMap[subjectName] = new SubjectInfo
            {
                Id = r["Id"].ToString(),
                IsAdditional = Convert.ToBoolean(r["IsAditional"])
            };
        }

        DataTable dtStudents = oo.GridFill1(@"
        SELECT SrNo, Name 
        FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
        WHERE ClassId = " + classId + @" 
        AND SectionName = '" + sectionName + @"' 
        AND Withdrwal IS NULL 
        ORDER BY FirstName");

        DataTable dtMarks = oo.GridFill1(@"
        SELECT SRNO, Evaluation, SubjectId, 
               ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
               ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
               (ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) + ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)) / 2 AS AVG,
               ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE
        FROM SGSMarkEntryJunior
        WHERE ClassId = " + classId + @"
        AND SessionName = '" + sessionName + @"'
        AND BranchCode = " + branchCode);

        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (string sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Percentage", typeof(double));
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        int totalMaxMarks = 0;
        foreach (var si in subjectInfoMap.Values)
        {
            if (!si.IsAdditional) totalMaxMarks += 100;
        }

        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;
                double percentageBaseTotal = 0;

                foreach (string sub in subjects)
                {
                    SubjectInfo subjectInfo = subjectInfoMap[sub];
                    DataRow[] marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subjectInfo.Id + "'");

                    double ut1 = 0, ut2 = 0, avg = 0, hyae = 0, total = 0;
                    if (marks.Length > 0)
                    {
                        DataRow m = marks[0];
                        ut1 = Convert.ToDouble(m["UT1"]);
                        ut2 = Convert.ToDouble(m["UT2"]);
                        avg = Convert.ToDouble(m["AVG"]);
                        hyae = Convert.ToDouble(m["HYAE"]);
                        total = Math.Ceiling(avg + hyae);
                    }

                    dr[sub + "_UT1"] = ut1;
                    dr[sub + "_UT2"] = ut2;
                    dr[sub + "_AVG"] = avg;
                    dr[sub + "_HYAE"] = hyae;
                    dr[sub + "_TOTAL"] = total;

                    grandTotal += total;
                    if (!subjectInfo.IsAdditional)
                        percentageBaseTotal += total;
                }

                dr["GrandTotal"] = grandTotal;
                dr["Percentage"] = totalMaxMarks > 0 ? Math.Round((percentageBaseTotal / totalMaxMarks) * 100, 2) : 0;

                DataTable dtAttendance = oo.GridFill1(@"
                SELECT TA, TWD
                FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") asr
                LEFT JOIN Exam_AttendenceDetails ead 
                    ON ead.srno = asr.SrNo 
                    AND ead.SessionName = asr.SessionName 
                    AND ead.BranchCode = asr.BranchCode 
                WHERE ead.Term = '" + evaluation + @"'
                AND asr.SrNo = '" + srno + @"'");

                dr["AttendanceTotal"] = dtAttendance.Rows.Count > 0 ? Convert.ToInt32(dtAttendance.Rows[0]["TA"]) : 0;
                dr["AttendancePresent"] = dtAttendance.Rows.Count > 0 ? Convert.ToInt32(dtAttendance.Rows[0]["TWD"]) : 0;

                dt.Rows.Add(dr);
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>#</th><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");
        foreach (string sub in subjects)
        {
            sb.Append("<th colspan='5'>" + sub + "</th>");
        }
        sb.Append("<th>Grand Total</th><th>Percentage</th><th>Present</th><th>Total</th></tr>");

        sb.Append("<tr><td></td><td></td><td></td><td></td>");
        foreach (string sub in subjects)
        {
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        }
        sb.Append("<td></td><td></td><td></td><td></td></tr>");

        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));
        int serialNo = 1;

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            int rowIndex = 0;
            int termCount = studentGroup.Count();
            double studentGrandTotal = 0;
            double studentPercentageTotal = 0;
            int studentPresentTotal = 0, studentAttendanceTotal = 0;

            foreach (DataRow row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + (termCount + 1) + "'>" + serialNo + "</td>");
                    sb.Append("<td rowspan='" + (termCount + 1) + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + (termCount + 1) + "'>" + studentName + "</td>");
                }
                sb.Append("<td>" + row["Term"] + "</td>");
                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }
                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Percentage"] + "%</td>");
                sb.Append("<td>" + row["AttendancePresent"] + "</td>");
                sb.Append("<td>" + row["AttendanceTotal"] + "</td>");
                sb.Append("</tr>");

                studentGrandTotal += Convert.ToDouble(row["GrandTotal"]);
                studentPercentageTotal += Convert.ToDouble(row["Percentage"]);
                studentPresentTotal += Convert.ToInt32(row["AttendancePresent"]);
                studentAttendanceTotal += Convert.ToInt32(row["AttendanceTotal"]);
                rowIndex++;
            }

            double overallPercentage = termCount > 0 ? (studentPercentageTotal / termCount) : 0;

            // TOTAL Row
            sb.Append("<tr><td class='text-right' colspan='4'><strong>Total</strong></td>");
            foreach (string sub in subjects)
            {
                double totalUT1 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT1"]));
                double totalUT2 = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_UT2"]));
                double totalAVG = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_AVG"]));
                double totalHYAE = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_HYAE"]));
                double totalSubTotal = studentGroup.Sum(r => Convert.ToDouble(r[sub + "_TOTAL"]));

                sb.Append("<td>" + totalUT1 + "</td><td>" + totalUT2 + "</td><td>" + totalAVG + "</td><td>" + totalHYAE + "</td><td>" + totalSubTotal + "</td>");
            }
            sb.Append("<td>" + studentGrandTotal + "</td>");
            sb.Append("<td>" + Math.Round(overallPercentage, 2) + "%</td>");
            sb.Append("<td>" + studentPresentTotal + "</td>");
            sb.Append("<td>" + studentAttendanceTotal + "</td>");
            sb.Append("</tr>");

            serialNo++;
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
        icons.Visible = true;
    }



    private void BuildDynamicTableWithSubjectName()
    {
        string sessionName = Session["SessionName"].ToString();
        int branchCode = Convert.ToInt32(Session["BranchCode"]);
        int classId = Convert.ToInt32(drpclass.SelectedValue);
        string sectionName = drpSection.SelectedValue;

        DataTable dtSubjects = oo.GridFill1(@"
    SELECT SubjectName, Id, IsAditional
    FROM TTSubjectMaster 
    WHERE ClassId = " + classId + @"
    AND BranchCode = " + branchCode + @"
    AND SessionName = '" + sessionName + @"'
    AND ApplicableFor <> 'TimeTable'");

        List<string> subjects = dtSubjects.AsEnumerable()
                                          .Select(r => r["SubjectName"].ToString())
                                          .ToList();

        Dictionary<string, SubjectInfo> subjectInfoMap = dtSubjects.AsEnumerable()
            .ToDictionary(
                r => r["SubjectName"].ToString(),
                r => new SubjectInfo
                {
                    Id = r["Id"].ToString(),
                    IsAdditional = Convert.ToBoolean(r["IsAditional"])
                }
            );

        DataTable dtStudents = oo.GridFill1(@"
    SELECT SrNo, Name 
    FROM AllStudentRecord_UDF('" + sessionName + "', " + branchCode + @") 
    WHERE ClassId = " + classId + @" 
    AND SectionName = '" + sectionName + @"' 
    AND Withdrwal IS NULL 
    ORDER BY FirstName");

        DataTable dtMarks = oo.GridFill1(@"
    SELECT 
        SRNO,
        Evaluation,
        SubjectId,
        ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) AS UT1,
        ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0) AS UT2,
        (
            ISNULL(TRY_CAST(NULLIF(Test1, '') AS FLOAT), 0) +
            ISNULL(TRY_CAST(NULLIF(Test2, '') AS FLOAT), 0)
        ) / 2 AS AVG,
        ISNULL(TRY_CAST(NULLIF(TH, '') AS FLOAT), 0) AS HYAE
    FROM SGSMarkEntryJunior
    WHERE ClassId = " + classId + @"
    AND SessionName = '" + sessionName + @"'
    AND BranchCode = " + branchCode);

        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Term");

        foreach (var sub in subjects)
        {
            dt.Columns.Add(sub + "_UT1", typeof(double));
            dt.Columns.Add(sub + "_UT2", typeof(double));
            dt.Columns.Add(sub + "_AVG", typeof(double));
            dt.Columns.Add(sub + "_HYAE", typeof(double));
            dt.Columns.Add(sub + "_TOTAL", typeof(double));
        }

        dt.Columns.Add("GrandTotal", typeof(double));
        dt.Columns.Add("Percentage", typeof(double));
        dt.Columns.Add("AttendancePresent", typeof(int));
        dt.Columns.Add("AttendanceTotal", typeof(int));

        int totalMaxMarks = subjectInfoMap.Count(s => !s.Value.IsAdditional) * 100;

        foreach (DataRow stu in dtStudents.Rows)
        {
            string srno = stu["SrNo"].ToString();
            string studentName = stu["Name"].ToString();

            for (int term = 1; term <= 2; term++)
            {
                string evaluation = term == 1 ? "Term1" : "Term2";
                DataRow dr = dt.NewRow();
                dr["SrNo"] = srno;
                dr["StudentName"] = studentName;
                dr["Term"] = term == 1 ? "I" : "II";

                double grandTotal = 0;
                double percentageBaseTotal = 0;

                foreach (string sub in subjects)
                {
                    var subjectInfo = subjectInfoMap[sub];
                    string subId = subjectInfo.Id;
                    bool isAdditional = subjectInfo.IsAdditional;

                    var marks = dtMarks.Select("SRNO = '" + srno + "' AND Evaluation = '" + evaluation + "' AND SubjectId = '" + subId + "'");

                    if (marks.Length > 0)
                    {
                        var m = marks[0];
                        double ut1 = Convert.ToDouble(m["UT1"]);
                        double ut2 = Convert.ToDouble(m["UT2"]);
                        double avg = Convert.ToDouble(m["AVG"]);
                        double hyae = Convert.ToDouble(m["HYAE"]);
                        double total = Math.Ceiling(avg + hyae);

                        dr[sub + "_UT1"] = ut1;
                        dr[sub + "_UT2"] = ut2;
                        dr[sub + "_AVG"] = avg;
                        dr[sub + "_HYAE"] = hyae;
                        dr[sub + "_TOTAL"] = total;

                        grandTotal += total;
                        if (!isAdditional)
                        {
                            percentageBaseTotal += total;
                        }
                    }
                    else
                    {
                        dr[sub + "_UT1"] = 0;
                        dr[sub + "_UT2"] = 0;
                        dr[sub + "_AVG"] = 0;
                        dr[sub + "_HYAE"] = 0;
                        dr[sub + "_TOTAL"] = 0;
                    }
                }

                double percentage = totalMaxMarks > 0 ? (percentageBaseTotal / totalMaxMarks) * 100 : 0;
                dr["GrandTotal"] = grandTotal;
                dr["Percentage"] = Math.Round(percentage, 2);

                dt.Rows.Add(dr);
            }
        }

        // Render HTML with #
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='table table-striped no-bm table-hover no-head-border table-bordered' border='1'>");
        sb.Append("<tr><th>#</th><th>S.R. No.</th><th>Student's Name</th><th>Term</th>");
        foreach (string sub in subjects)
            sb.Append("<th>UT1</th><th>UT2</th><th>AVG</th><th>HY/AE</th><th>TOTAL</th>");
        sb.Append("<th>Grand Total</th><th>Percentage</th></tr>");

        var studentGroups = dt.AsEnumerable().GroupBy(r => r.Field<string>("StudentName"));
        int serialNo = 1;

        foreach (var studentGroup in studentGroups)
        {
            string srNo = studentGroup.First()["SrNo"].ToString();
            string studentName = studentGroup.Key;
            int rowIndex = 0;
            int termCount = studentGroup.Count();

            foreach (var row in studentGroup)
            {
                sb.Append("<tr>");
                if (rowIndex == 0)
                {
                    sb.Append("<td rowspan='" + termCount + "'>" + serialNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + srNo + "</td>");
                    sb.Append("<td rowspan='" + termCount + "'>" + studentName + "</td>");
                }

                sb.Append("<td>" + row["Term"] + "</td>");

                foreach (string sub in subjects)
                {
                    sb.Append("<td>" + row[sub + "_UT1"] + "</td>");
                    sb.Append("<td>" + row[sub + "_UT2"] + "</td>");
                    sb.Append("<td>" + row[sub + "_AVG"] + "</td>");
                    sb.Append("<td>" + row[sub + "_HYAE"] + "</td>");
                    sb.Append("<td>" + row[sub + "_TOTAL"] + "</td>");
                }

                sb.Append("<td>" + row["GrandTotal"] + "</td>");
                sb.Append("<td>" + row["Percentage"] + "%</td>");
                sb.Append("</tr>");

                rowIndex++;
            }

            serialNo++;
        }

        sb.Append("</table>");
        phTable.Controls.Add(new Literal { Text = sb.ToString() });
        icons.Visible = true;
    }

    protected void LoadSection()
    {
        if (Session["LoginType"].ToString().ToLower() == "staff")
        {
            _sql = "Select SectionName from SectionMaster where ClassNameId=" + drpclass.SelectedValue;
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select SectionId from ClassTeacherMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1)";
            oo.FillDropDown(_sql, drpSection, "SectionName");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
        else
        {
            _sql = "Select SectionName from SectionMaster where ClassNameId=" + drpclass.SelectedValue;
            _sql += " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            oo.FillDropDown(_sql, drpSection, "SectionName");
            drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
        LoadSection();
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BuildDynamicTable();
    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBranch.SelectedValue != "<--Select-->")
        {
            BuildDynamicTable();
        }

    }
    public class SubjectInfo
    {
        public string Id { get; set; }
        public bool IsAdditional { get; set; }
    }

    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BuildDynamicTable();
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (phTable.Controls.Count == 0)
        {
            BuildDynamicTable();  // Ensure it's built
        }
        oo.ExportControlToExcel(Response, "CumulativeReport.xls", phTable);
        //oo.ExportControlToExcel(Response, "CumulativeReport.xls", phTable);
    }
}
