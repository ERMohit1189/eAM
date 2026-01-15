using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Templates : System.Web.UI.Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;
    string id = "";
    public Templates()
    {
        con = new SqlConnection();
        oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        // BLL.BLLInstance.LoadHeader("Certificate", header1);
        if (!IsPostBack)
        {
            //sql = "Select AffiliationNo,SchoolNo from CollegeMaster where CollegeId=" + Session["BranchCode"] + "";
            //Label1.Text = oo.ReturnTag(sql, "AffiliationNo");

            //lblaffno.Text = oo.ReturnTag(sql, "AffiliationNo");
            //if (oo.ReturnTag(sql, "AffiliationNo") == "")
            //{
            //    lblaf.Visible = false;
            //}
            //Label2.Text = oo.ReturnTag(sql, "SchoolNo");
            loaddata();
        }
    }

    public string Date_in_Words(int date)
    {
        string[] digit = new string[32];

        digit[1] = "First";
        digit[2] = "Second";
        digit[3] = "Third";
        digit[4] = "Fourth";
        digit[5] = "Fifth";
        digit[6] = "Sixth";
        digit[7] = "Seventh";
        digit[8] = "Eighth";
        digit[9] = "Ninth";
        digit[10] = "Tenth";
        digit[11] = "Eleventh";
        digit[12] = "Twelfth";
        digit[13] = "Thirteenth";
        digit[14] = "Fourteenth";
        digit[15] = "Fifteenth";
        digit[16] = "Sixteenth";
        digit[17] = "Seventeenth";
        digit[18] = "Eighteenth";
        digit[19] = "Nineteenth";
        digit[20] = "Twentieth";
        digit[21] = "Twenty First";
        digit[22] = "Twenty Second";
        digit[23] = "Twenty Third";
        digit[24] = "Twenty Fourth";
        digit[25] = "Twenty Fifth";
        digit[26] = "Twenty Sixth";
        digit[27] = "Twenty Seventh";
        digit[28] = "Twenty Eighth";
        digit[29] = "Twenty Ninth";
        digit[30] = "Thirtieth";
        digit[31] = "Thirty First";


        return digit[date];
    }
    public void loaddata()
    {
        string sqlAddress = "Select *,(Select CityName from CityMaster where Id=CollegeMaster.CityId) as CityName from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        lblSchoolName.Text = oo.ReturnTag(sqlAddress, "CollegeName");
        string CollegeAdd1 = oo.ReturnTag(sqlAddress, "CollegeAdd1");
        string CollegeAdd2 = oo.ReturnTag(sqlAddress, "CollegeAdd2");
        string CityName = oo.ReturnTag(sqlAddress, "CityName");


        lblSchoolAddress.Text = CollegeAdd1 + " " + CollegeAdd2 + " " + CityName;
        string CologeLogoPath = oo.ReturnTag(sqlAddress, "CologeLogoPath");
        ImageUrls.ImageUrl = CologeLogoPath;


        string sql1 = "select TCIssueDate as TCIssueDate1,PreparedBy,Id,srno,bookno,DatePArt(dd,AdmissionFromDate) dd1,' '+DateNAme(MONTH,AdmissionFromDate)+' '+Convert(Varchar(4),DatePArt(YEAR,AdmissionFromDate)) as AdmissionFromDate, DatePArt(dd,DateOfStruckOff) ddStruck,' '+DateNAme(MONTH,DateOfStruckOff)+' '+Convert(Varchar(4),DatePArt(YEAR,DateOfStruckOff)) as DateOfStruckOff, DatePArt(dd,TCIssueDate) dd2,' '+DateNAme(MONTH,TCIssueDate)+' '+Convert(Varchar(4),DatePArt(YEAR,TCIssueDate)) as TCIssueDate,Concession,";
        sql1 = sql1 + " ReceivedAmount,StudentName,FatherName,Class,LoginName,BranchCode,";
        sql1 = sql1 + " Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,DatePArt(dd,RecordDate) dd3,' '+DateNAme(MONTH,RecordDate)+' '+Convert(Varchar(4),DatePArt(YEAR,RecordDate)) as RecordDate,DatePArt(dd,DateOfFAD) dd4,' '+DateNAme(MONTH,DateOfFAD)+' '+Convert(varchar(4),DatePArt(YEAR,DateOfFAD)) DateOfFAD,ClassOfFAC,";
        sql1 = sql1 + " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR,IsQualified,IsQualifiedtowhichclass, tcType, PassStatus from TCCollection where RecieptNo='" + Request.QueryString["print"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        Label31.Text = oo.ReturnTag(sql1, "srno");
        //Label17.Text = oo.ReturnTag(sql1, "Pen");

        string SQLNew = "Select * from StudentPreviousSchool where SrNo='" + Label31.Text + "'  and BranchCode=" + Session["BranchCode"] + "";
        string SchoolName = oo.ReturnTag(SQLNew, "SchoolName");
        string Schooladdress = oo.ReturnTag(SQLNew, "Schooladdress");
        Label14.Text = SchoolName + " " + Schooladdress;

        string sqlNews = "Select top 1 Pen from StudentGenaralDetail where SrNo='" + Label31.Text + "' and  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        string penNumber = oo.ReturnTag(sqlNews, "Pen");

        StringBuilder sb = new StringBuilder();
        foreach (char c in penNumber.PadRight(10)) // Ensure it always creates 10 boxes
        {
            if (char.IsWhiteSpace(c))
            {
                sb.Append("<div class='pen-box'></div>");
            }

            else
            {
                sb.Append("<div class='pen-box'>" + c + "</div>");
            }
            //sb.Append($"<div class='pen-box'>{c}</div>");

        }

        litPenBoxes.Text = sb.ToString();

        string sqlNews1 = "Select top 1 RemarkOtherActivities,ApaarID from StudentOfficialDetails where SrNo='" + Label31.Text + "' and  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        //string RemarkOtherActivities = oo.ReturnTag(sqlNews1, "RemarkOtherActivities");
        string remark = oo.ReturnTag(sqlNews1, "RemarkOtherActivities");
        string apaarid = oo.ReturnTag(sqlNews1, "ApaarID");
        sb = new StringBuilder();
        foreach (char c in apaarid.PadRight(10)) // Ensure it always creates 10 boxes
        {
            if (char.IsWhiteSpace(c))
            {
                sb.Append("<div class='pen-box'></div>");
            }

            else
            {
                sb.Append("<div class='pen-box'>" + c + "</div>");
            }
        }
        litApaarID.Text = sb.ToString();

        litRemark1.Text = remark.ToString();
        string dobs1 = oo.ReturnTag(sql1, "TCIssueDate1");
        DateTime parsedDate1 = DateTime.ParseExact(dobs1, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        string dates = parsedDate1.ToString("dd-MM-yyyy");
        //   string dates = dobs1;
        Label17.Text = dates;
        Label16.Text = dates;
        Label15.Text = dates;
        loadAttendenceData(Label31.Text);

        string SQLNeW = "Select * from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
        lblHead1.Text = oo.ReturnTag(SQLNeW, "PrincipalName");
        // lblHead2.Text = oo.ReturnTag(SQLNeW, "PrincipalName");

        string checkQuery = "SELECT COUNT(*) FROM CourseSignTCCounter WHERE SrNo = @SrNo";
        con.Open();
        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
        checkCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
        int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
        con.Close();
        if (existingCount > 0)
        {
            con.Open();
            string getExistingQuery = @"
    SELECT ID, CourseName, Class AS ClassName, DateofAdmission, DateofPromotion, 
           DateofRemoval, CauseofRemoval, Year AS AcademicYear, 
           Conduct, Work 
    FROM CourseSignTCCounter 
    WHERE SrNo = @SrNo";

            SqlCommand getCmd = new SqlCommand(getExistingQuery, con);
            getCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
            SqlDataAdapter daExist = new SqlDataAdapter(getCmd);
            DataTable dtBase = new DataTable(); // overwrite earlier
            daExist.Fill(dtBase);
            con.Close();
            var groupedData = dtBase.AsEnumerable()
         .GroupBy(r => r.Field<string>("CourseName"))
         .Select(g => new CourseGroup
         {
             CourseName = g.Key,
             ClassList = g.Select(r => new ClassInfo
             {
                 ID = r.Field<int>("ID"),
                 ClassName = r.Field<string>("ClassName"),
                 DateOfAdmission = r.Field<string>("DateofAdmission"),
                 DateOfPromotion = r.Field<string>("DateofPromotion"),
                 DateOfRemoval = r.Field<string>("DateofRemoval"),
                 CauseOfRemoval = r.Field<string>("CauseofRemoval"),
                 AcademicYear = r.Field<string>("AcademicYear"),
                 Conduct = r.Field<string>("Conduct"),
                 Work = r.Field<string>("Work")
             }).ToList()
         }).ToList();
            rptPrimarySection.DataSource = groupedData;
            rptPrimarySection.DataBind();
        }
        else
        {
            // Existing: Query 1 - ClassMaster + CourseMaster
            string query1 = @"
SELECT clm.Id, clm.ClassName, cm.CourseName
FROM ClassMaster clm
JOIN CourseMaster cm ON cm.Id = clm.Course
WHERE clm.SessionName = @SessionName
AND clm.BranchCode = @BranchCode";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            cmd1.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dtBase = new DataTable();
            da1.Fill(dtBase);

            // Existing: Query 2 - StudentOfficialDetails
            string query2 = @"
SELECT *,
       (SELECT ClassName FROM ClassMaster 
        WHERE Id = AdmissionForClassId 
        AND SessionName = st.SessionName 
        AND BranchCode = st.BranchCode) AS classID
FROM StudentOfficialDetails st
WHERE SrNo = @SrNo";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@SrNo", Label31.Text);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dtStudent = new DataTable();
            da2.Fill(dtStudent);

            // ✅ New: Query 3 - TCCollection
            string query3 = @"
SELECT top 1 Id, ClassOfFAC, remark, PassStatus, SessionName, TCIssueDate, ConductandWork 
FROM TCCollection 
WHERE SrNo = @SrNo 
ORDER BY Id DESC";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.Parameters.AddWithValue("@SrNo", Label31.Text);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dtTC = new DataTable();
            da3.Fill(dtTC);

            // Add necessary columns to dtBase
            dtBase.Columns.Add("DateOfAdmission", typeof(string));
            dtBase.Columns.Add("DateOfPromotion", typeof(string));
            dtBase.Columns.Add("DateOfRemoval", typeof(string));
            dtBase.Columns.Add("CauseOfRemoval", typeof(string));
            dtBase.Columns.Add("AcademicYear", typeof(string));
            dtBase.Columns.Add("Conduct", typeof(string));
            dtBase.Columns.Add("Work", typeof(string));

            // Loop through base class list
            foreach (DataRow baseRow in dtBase.Rows)
            {
                string className = baseRow["ClassName"].ToString();
                bool matchFound = false;

                // First check with StudentOfficialDetails
                foreach (DataRow studentRow in dtStudent.Rows)
                {
                    string admissionClassName = studentRow["classID"].ToString();
                    if (className == admissionClassName)
                    {
                        string DateOfAdmiission = studentRow["DateOfAdmiission"].ToString();
                        DateTime DateOfAdmiission1 = DateTime.ParseExact(DateOfAdmiission, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string NewDateOfAdmiission = DateOfAdmiission1.ToString("dd-MM-yyyy");

                        string PrmotionDate = studentRow["PrmotionDate"].ToString();
                        DateTime PrmotionDate1 = DateTime.ParseExact(PrmotionDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string NewPrmotionDate = PrmotionDate1.ToString("dd-MM-yyyy");

                        baseRow["DateOfAdmission"] = NewDateOfAdmiission.ToString();
                        baseRow["DateOfPromotion"] = NewPrmotionDate.ToString();
                        baseRow["AcademicYear"] = studentRow["SessionName"].ToString();
                        baseRow["Work"] = studentRow["Promotion"].ToString();
                        matchFound = true;
                        break;
                    }
                }

                // Then check with TCCollection
                bool tcMatched = false;
                foreach (DataRow tcRow in dtTC.Rows)
                {
                    string classOfFAC = tcRow["ClassOfFAC"].ToString();
                    if (className == classOfFAC)
                    {
                        baseRow["Conduct"] = tcRow["ConductandWork"].ToString();
                        baseRow["Work"] = tcRow["PassStatus"].ToString();
                        baseRow["AcademicYear"] = tcRow["SessionName"].ToString();
                        baseRow["DateOfRemoval"] = Convert.ToDateTime(tcRow["TCIssueDate"]).ToString("dd/MM/yyyy");
                        baseRow["CauseOfRemoval"] = tcRow["remark"].ToString();
                        tcMatched = true;
                        break;
                    }
                }

                // If no match in both, fill blanks
                if (!matchFound && !tcMatched)
                {
                    baseRow["DateOfAdmission"] = "";
                    baseRow["DateOfPromotion"] = "";
                    baseRow["AcademicYear"] = "";
                    baseRow["Conduct"] = "";
                    baseRow["Work"] = "";
                    baseRow["DateOfRemoval"] = "";
                    baseRow["CauseOfRemoval"] = "";
                }
            }

            foreach (DataRow row in dtBase.Rows)
            {
                con.Open();
                string insertQuery = @"
        INSERT INTO CourseSignTCCounter
        (SrNo, CourseName, Class, DateofAdmission, DateofPromotion, DateofRemoval,
         CauseofRemoval, Year, Conduct, Work, ModifiedBy, ModifiedDate)
        VALUES
        (@SrNo, @CourseName, @Class, @DOA, @DOP, @DOR, @COR, @Year, @Conduct, @Work, @ModifiedBy, @ModifiedDate)";

                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
                insertCmd.Parameters.AddWithValue("@CourseName", row["CourseName"]);
                insertCmd.Parameters.AddWithValue("@Class", row["ClassName"]);
                insertCmd.Parameters.AddWithValue("@DOA", row["DateOfAdmission"]);
                insertCmd.Parameters.AddWithValue("@DOP", row["DateOfPromotion"]);
                insertCmd.Parameters.AddWithValue("@DOR", row["DateOfRemoval"]);
                insertCmd.Parameters.AddWithValue("@COR", row["CauseOfRemoval"]);
                insertCmd.Parameters.AddWithValue("@Year", row["AcademicYear"]);
                insertCmd.Parameters.AddWithValue("@Conduct", row["Conduct"]);
                insertCmd.Parameters.AddWithValue("@Work", row["Work"]);
                insertCmd.Parameters.AddWithValue("@ModifiedBy", Session["LoginName"].ToString());
                insertCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                insertCmd.ExecuteNonQuery();
                con.Close();
            }
            con.Open();
            string getExistingQuery = @"
    SELECT ID,CourseName, Class AS ClassName, DateofAdmission, DateofPromotion, 
           DateofRemoval, CauseofRemoval, Year AS AcademicYear, 
           Conduct, Work 
    FROM CourseSignTCCounter 
    WHERE SrNo = @SrNo";

            SqlCommand getCmd = new SqlCommand(getExistingQuery, con);
            getCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
            SqlDataAdapter daExist = new SqlDataAdapter(getCmd);
            DataTable dtBase1 = new DataTable(); // overwrite earlier
            daExist.Fill(dtBase1);
            con.Close();
            var groupedData = dtBase1.AsEnumerable()
          .GroupBy(r => r.Field<string>("CourseName"))
          .Select(g => new CourseGroup
          {
              CourseName = g.Key,
              ClassList = g.Select(r => new ClassInfo
              {
                  ID = r.Field<int>("ID"),
                  ClassName = r.Field<string>("ClassName"),
                  DateOfAdmission = r.Field<string>("DateofAdmission"),
                  DateOfPromotion = r.Field<string>("DateofPromotion"),
                  DateOfRemoval = r.Field<string>("DateofRemoval"),
                  CauseOfRemoval = r.Field<string>("CauseofRemoval"),
                  AcademicYear = r.Field<string>("AcademicYear"),
                  Conduct = r.Field<string>("Conduct"),
                  Work = r.Field<string>("Work")
              }).ToList()
          }).ToList();
            rptPrimarySection.DataSource = groupedData;
            rptPrimarySection.DataBind();
            //  rptPrimarySection.DataSource = dtBase;
            // rptPrimarySection.DataBind();

        }

        sql = "select top(1) Id,srno,bookno,DatePArt(dd,AdmissionFromDate) dd1,' '+DateNAme(MONTH,AdmissionFromDate)+' '+Convert(Varchar(4),DatePArt(YEAR,AdmissionFromDate)) as AdmissionFromDate,DatePArt(dd,TCIssueDate) dd2,' '+DateNAme(MONTH,TCIssueDate)+' '+Convert(Varchar(4),DatePArt(YEAR,TCIssueDate)) as TCIssueDate,Concession,";
        sql += " ReceivedAmount,StudentName,FatherName,Class,LoginName,BranchCode,";
        sql += " Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,DatePArt(dd,RecordDate) dd3,' '+DateNAme(MONTH,RecordDate)+' '+Convert(Varchar(4),DatePArt(YEAR,RecordDate)) as RecordDate,DatePArt(dd,DateOfFAD) dd4,' '+DateNAme(MONTH,DateOfFAD)+' '+Convert(varchar(4),DatePArt(YEAR,DateOfFAD)) DateOfFAD,ClassOfFAC,";
        sql += " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR,IsQualified,IsQualifiedtowhichclass, tcType, PassStatus,Pen from TCCollection where BranchCode=" + Session["BranchCode"] + " and srno='" + Label31.Text + "' order by id asc";



        Label30.Text = oo.ReturnTag(sql1, "Id");
        id = oo.ReturnTag(sql1, "Id");

        Label32.Text = oo.ReturnTag(sql1, "bookno");
        Label12.Text = oo.ReturnTag(sql1, "PreparedBy");
        // tcCopy.Text = oo.ReturnTag(sql1, "tcType");
        string PassStatus = oo.ReturnTag(sql, "PassStatus");
        //  string cleanedDate = RemoveOrdinalSuffix(Label10.Text);


        // Parse the cleaned date string

        string day;

        //Label16.Text = oo.ReturnTag(sql, "Subject");
        //Label20.Text = oo.ReturnTag(sql, "Dues");
        //Label21.Text = oo.ReturnTag(sql, "ConcessionType");
        //Label22.Text = oo.ReturnTag(sql, "TWD");
        //Label23.Text = oo.ReturnTag(sql, "TWDP");
        //Label24.Text = oo.ReturnTag(sql, "NCC");
        //Label25.Text = oo.ReturnTag(sql, "ECA");
        //Label26.Text = oo.ReturnTag(sql, "ConductandWork");

        day = oo.ReturnTag(sql1, "ddStruck");

        dateFormate(ref day);
        //  lblpupilsname.Text = day + oo.ReturnTag(sql1, "DateOfStruckOff");


        day = oo.ReturnTag(sql1, "dd1");

        dateFormate(ref day);
        // Label27.Text = day + oo.ReturnTag(sql1, "AdmissionFromDate");

        day = oo.ReturnTag(sql1, "dd2");

        dateFormate(ref day);
        //Label28.Text = day + oo.ReturnTag(sql1, "TCIssueDate");

        //Label29.Text = oo.ReturnTag(sql, "Remark");

        //Label33.Text = oo.ReturnTag(sql, "AOR");

        //Label17.Text = oo.ReturnTag(sql, "IsQualified");
        //lbl_pen.Text = oo.ReturnTag(sql, "Pen");
        string nextclass = oo.ReturnTag(sql, "IsQualifiedtowhichclass");
        if (nextclass != string.Empty)
        {
            //changeClassName(ref nextclass);

        }
        // Label18.Text = nextclass;


        day = oo.ReturnTag(sql, "dd4");

        dateFormate(ref day);
        // Label8.Text = day + oo.ReturnTag(sql, "DateOfFAD");

        string faddinclass = oo.ReturnTag(sql, "ClassOfFAC").ToUpper();
        //changeClassName(ref faddinclass);
        // Label9.Text = faddinclass;

        string oldsessionname = "";
        string Top_sessionName = Session["Top_sessionName"].ToString();

        sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled'";
        if (oo.Duplicate(sql))
        {
            sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order by id Desc) As T1 order by id";
            oldsessionname = oo.ReturnTag(sql, "SessionName");
        }
        else
        {
            sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + "  and ISNULL(Promotion,'')<>'Cancelled' order by id Desc";
            oldsessionname = oo.ReturnTag(sql, "SessionName");
        }

        sql = "Select * from TCCollection where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and id='" + Label30.Text + "'";
        string classname = oo.ReturnTag(sql, "ClassWithResult");

        sql = "Select Religion,FatherOccupation,G1Address,Name StudentName,FatherName,MotherName,Nationality,Category,convert(nvarchar,DOB,106) as DOB,DatePart(dd,DOB) as date,DateName(mm,DOB) as month,DatePart(yyyy,DOB) as year,RIGHT('0' + CAST(DAY(DOB) AS VARCHAR), 2) + '-' + RIGHT('0' + CAST(MONTH(DOB) AS VARCHAR), 2) + '-' + CAST(YEAR(DOB) AS VARCHAR) AS FormattedDOB,StLocalStateName,StLocalCityName from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"] + ") where SrNo='" + Label31.Text + "'";
        Label3.Text = oo.ReturnTag(sql, "StudentName");
        Label4.Text = oo.ReturnTag(sql, "FatherName");
        Label5.Text = oo.ReturnTag(sql, "MotherName");
        string address = oo.ReturnTag(sql, "G1Address");
        string CityNames = oo.ReturnTag(sql, "StLocalCityName");
        string StateNames = oo.ReturnTag(sql, "StLocalStateName");
        Label8.Text = address + " " + CityNames + " " + StateNames;

        Label6.Text = oo.ReturnTag(sql, "Religion");
        Label7.Text = oo.ReturnTag(sql, "Category");
        Label9.Text = oo.ReturnTag(sql, "FatherOccupation");

        day = oo.ReturnTag(sql, "date");

        dateFormate(ref day);
        string year = "";
        string yearpri = "";
        string yearsuf = "";
        year = oo.ReturnTag(sql, "year");
        try
        {
            Label10.Text = day + " " + oo.ReturnTag(sql, "month") + " " + oo.ReturnTag(sql, "year");
            string cleanedDate = RemoveOrdinalSuffix(Label10.Text);

            string dobs = oo.ReturnTag(sql, "DOB");
            // Parse the cleaned date string
            DateTime parsedDate = DateTime.ParseExact(cleanedDate, "dd MMMM yyyy", CultureInfo.InvariantCulture);
            // DateTime parsedDate = DateTime.ParseExact(dobs, "d-MMM-yyyy", CultureInfo.InvariantCulture);

            // Format the date in "dd-MM-yyyy" format
            string formattedDate = parsedDate.ToString("dd-MM-yyyy");
            Label10.Text = formattedDate;
            year = oo.ReturnTag(sql, "year");
            if (Convert.ToInt32(year) < 2000)
            {
                yearpri = oo.Date_in_Words(Convert.ToInt16(year.Substring(0, 2)));
                yearsuf = oo.Date_in_Words(Convert.ToInt16(year.Substring(2, 2)));

                year = yearpri + " Hundred " + yearsuf;
            }
            else
            {
                year = oo.Date_in_Words(Convert.ToInt32(oo.ReturnTag(sql, "year")));
            }
        }
        catch (Exception ex)
        {

        }
        Label10.Text = (oo.ReturnTag(sql, "FormattedDOB"));
        Label11.Text = Date_in_Words(Convert.ToInt32(oo.ReturnTag(sql, "date"))) + " " + oo.ReturnTag(sql, "month") + " " + oo.Date_in_Words(Convert.ToInt32(oo.ReturnTag(sql, "year")));
        //Label18.Text = oo.ReturnTag(sql, "date") + " " + oo.ReturnTag(sql, "month") + " " + oo.ReturnTag(sql, "year");

        string status = PassStatus;
        string sqls = "Select DatePArt(dd,WithdrawalDate) dd11,' '+DateNAme(MONTH,WithdrawalDate)+' '+Convert(Varchar(4),DatePArt(YEAR,WithdrawalDate)) as WithdrawalDate From StudentWithdrawal where srno='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + "";
        day = oo.ReturnTag(sqls, "dd11");
        dateFormate(ref day);

        if (status.ToUpper() == "PASSED")
        {

            //changeClassName(ref classname);
            // Label13.Text = classname;
            // Label15.Text = "NO";
        }
        else if (status.ToUpper() == "PROMOTED")
        {

            //changeClassName(ref classname);
            // Label13.Text = classname;
            // Label15.Text = "NO";
        }
        else if (status.ToUpper() == "FAILED")
        {
            //Label15.Text = "YES";
            ////changeClassName(ref classname);
            //Label13.Text = classname;
        }
        else if (status.ToUpper() == "DETAINED")
        {
            //Label15.Text = "YES";
            ////changeClassName(ref classname);
            //Label13.Text = classname;
        }
        else if (status.ToUpper() == "COMPARTMENT")
        {
            //changeClassName(ref classname);
            //Label13.Text = classname;
            //Label15.Text = PassStatus;
        }
        else if (status.ToUpper() == "ABSENT")
        {
            //changeClassName(ref classname);
            //Label13.Text = classname;

            //Label15.Text = PassStatus;
        }
        else if (status.ToUpper() == "LEFT")
        {
            sql = "Select SessionName from";
            sql += " (Select ROW_NUMBER() Over(order by SessionId desc) srno, sessionId, sof.SessionName from StudentOfficialDetails sof";
            sql += " inner join SessionMaster sm on sm.SessionName = sof.SessionName";
            sql += " where SrNo = '" + Label31.Text + "' and sof.BranchCode=" + Session["BranchCode"] + ")T1 where srno = 1";
            string sessionname = oo.ReturnTag(sql, "SessionName");
            sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
            sql += " where SrNo='" + Label31.Text + "' and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + sessionname + "' and so.SessionName='" + sessionname + "'";
            classname = oo.ReturnTag(sql, "ClassName");
            //changeClassName(ref classname);
            //Label13.Text = classname;
            //Label15.Text = "NO";
        }
        else
        {
            string secondlastsessionid = "";
            sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                sql = "SELECT TOP 1 SessionId From(select Top 2 SessionId from SessionMaster where   BranchCode=" + Session["BranchCode"] + " ORDER BY SessionId DESC) x ORDER BY SessionId";
                secondlastsessionid = oo.ReturnTag(sql, "SessionId");
            }
            else
            {
                sql = "select Top 1 SessionId from SessionMaster where  BranchCode=" + Session["BranchCode"] + " ORDER BY SessionId DESC";
                secondlastsessionid = oo.ReturnTag(sql, "SessionId");
            }
            // sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Top_sessionName + "' and Promotion is null";
            sql = "Select *from StudentOfficialDetails where SrNo='' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Top_sessionName + "' and Promotion is null";
            if (oo.Duplicate(sql))
            {
                sql = "Select SessionName from SessionMaster where SessionId='" + secondlastsessionid + "' and BranchCode=" + Session["BranchCode"] + "";
                string sessionname = oo.ReturnTag(sql, "SessionName");
                sql = "Select ClassWithResult from TCCollection where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and id='" + Label30.Text + "'";
                classname = oo.ReturnTag(sql, "ClassWithResult");
                if (oo.ReturnTag(sql, "ClassWithResult") != "")
                {
                    classname = oo.ReturnTag(sql, "ClassWithResult");
                    //changeClassName(ref classname);
                    //  Label13.Text = classname;
                }
                else
                {
                    sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                    sql += " where SrNo='" + Label31.Text + "' and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Top_sessionName + "' and so.SessionName='" + Top_sessionName + "'";
                    classname = oo.ReturnTag(sql, "ClassName");
                    //changeClassName(ref classname);
                    //   Label13.Text = classname;
                }
            }
            else
            {
                sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                sql += " where SrNo='" + Label31.Text + "' and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Top_sessionName + "' and so.SessionName='" + Top_sessionName + "'";
                classname = oo.ReturnTag(sql, "ClassName");
                //changeClassName(ref classname);
                // Label13.Text = classname;
            }
            //Label15.Text = "NO";
        }
        string sessionnamed = "";
        sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Session["SessionName"] + "' and ISNULL(Promotion,'')<>'Cancelled'";
        if (oo.Duplicate(sql))
        {
            //sql1 = "Select top 1 SessionName from SessionMaster order by SessionId desc";
            sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Top_sessionName + "' and Promotion is null";
            if (oo.Duplicate(sql))
            {
                sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc) As T1 order by id";
                sessionnamed = oo.ReturnTag(sql, "SessionName");
            }
            else
            {
                sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc";
                sessionnamed = oo.ReturnTag(sql, "SessionName");
            }
        }
        else
        {
            if (status.ToUpper() == "LEFT")
            {
                sql = "Select SessionName from";
                sql += " (Select ROW_NUMBER() Over(order by SessionId desc) srno, sessionId, sof.SessionName from StudentOfficialDetails sof";
                sql += " inner join SessionMaster sm on sm.SessionName = sof.SessionName";
                sql += " where SrNo = '" + Label31.Text + "' and sm.BranchCode=" + Session["BranchCode"] + " and sof.BranchCode=" + Session["BranchCode"] + ")T1 where srno = 2";

                sessionnamed = oo.ReturnTag(sql, "SessionName");
            }

            else
            {
                sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order by id Desc";
                sessionnamed = oo.ReturnTag(sql, "SessionName");
            }
        }

        sql = "Select * from TCCollection where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " and id='" + Label30.Text + "'";
        classname = oo.ReturnTag(sql, "Class");
        string completeClass = ""; string changeClassNames = "";
        if (classname.ToUpper() == "I" || classname.ToUpper() == "II" || classname.ToUpper() == "II" || classname.ToUpper() == "VI" || classname.ToUpper() == "V" || classname.ToUpper() == "VI" || classname.ToUpper() == "VII" || classname.ToUpper() == "VIII" || classname.ToUpper() == "IX" || classname.ToUpper() == "X" || classname.ToUpper() == "XI" || classname.ToUpper() == "XII")
        {
            changeClassNames = classname;
            //changeClassName(ref changeClassNames);
            completeClass = "(" + changeClassNames + ") / ";
            if (classname.Contains("Nur"))
            {
                completeClass = completeClass + classname;
            }
            else
            {
                completeClass = completeClass + BAL.objBal.convertRomantostring(classname.ToUpper());
            }
            //  Label12.Text = completeClass;


        }
        else
        {
            //   Label12.Text = classname;
        }
    }
    public void dateFormate(ref string day)
    {
        if (day == "1" || day == "21" || day == "31")
        {
            day = day + "st";
        }
        else if (day == "2" || day == "22")
        {
            day = day + "nd";
        }
        else if (day == "3" || day == "23")
        {
            day = day + "rd";
        }
        else if (day == "4" || day == "5" || day == "6" || day == "7" || day == "8" || day == "9" || day == "10" || day == "11" || day == "12" || day == "13" || day == "14" || day == "15" || day == "16" || day == "17" || day == "18" || day == "19" || day == "20" || day == "24" || day == "25" || day == "26" || day == "27" || day == "28" || day == "29" || day == "30")
        {
            day = day + "th";
        }
    }
    public void loadAttendenceData(string SrNo)
    {
        string checkQuery = "SELECT COUNT(*) FROM CourseSignAttendenceTCCounter WHERE SrNo = @SrNo";
        con.Open();
        SqlCommand checkCmd = new SqlCommand(checkQuery, con);
        checkCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
        int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
        con.Close();
        if (existingCount > 0)
        {
            con.Open();
            string getExistingQuery = @"
    SELECT ID,CourseName, Class AS ClassName,Year as DateofAdmission,Subjects as DateofPromotion, 
            NoofLecturesDelivered AS AcademicYear, 
NoofLecturesAttended AS Work
         
    FROM CourseSignAttendenceTCCounter 
    WHERE SrNo = @SrNo";

            SqlCommand getCmd = new SqlCommand(getExistingQuery, con);
            getCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
            SqlDataAdapter daExist = new SqlDataAdapter(getCmd);
            DataTable dtBase = new DataTable(); // overwrite earlier
            daExist.Fill(dtBase);
            con.Close();
            Repeater1.DataSource = dtBase;
            Repeater1.DataBind();
        }
        else
        {

            // Existing: Query 1 - ClassMaster + CourseMaster
            string query1 = @"
SELECT clm.Id, clm.ClassName, cm.CourseName
FROM ClassMaster clm
JOIN CourseMaster cm ON cm.Id = clm.Course
WHERE clm.SessionName = @SessionName
AND clm.BranchCode = @BranchCode";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            cmd1.Parameters.AddWithValue("@SessionName", Session["SessionName"]);
            cmd1.Parameters.AddWithValue("@BranchCode", Session["BranchCode"]);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dtBase = new DataTable();
            da1.Fill(dtBase);

            // Existing: Query 2 - StudentOfficialDetails
            string query2 = @"
SELECT *,
       (SELECT ClassName FROM ClassMaster 
        WHERE Id = AdmissionForClassId 
        AND SessionName = st.SessionName 
        AND BranchCode = st.BranchCode) AS classID,
(Select Id from BranchMaster WHERE Id = AdmissionForClassId 
        AND SessionName = st.SessionName 
        AND BranchCode = st.BranchCode) As StreamID
FROM StudentOfficialDetails st
WHERE SrNo = @SrNo";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@SrNo", SrNo);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dtStudent = new DataTable();
            da2.Fill(dtStudent);

            // ✅ New: Query 3 - TCCollection
            string query3 = @"
SELECT top 1 Id, ClassOfFAC, remark, PassStatus, SessionName, TCIssueDate, ConductandWork 
FROM TCCollection 
WHERE SrNo = @SrNo 
ORDER BY Id DESC";
            SqlCommand cmd3 = new SqlCommand(query3, con);
            cmd3.Parameters.AddWithValue("@SrNo", SrNo);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dtTC = new DataTable();
            da3.Fill(dtTC);

            // Add necessary columns to dtBase
            dtBase.Columns.Add("DateOfAdmission", typeof(string));
            dtBase.Columns.Add("DateOfPromotion", typeof(string));
            dtBase.Columns.Add("DateOfRemoval", typeof(string));
            dtBase.Columns.Add("CauseOfRemoval", typeof(string));
            dtBase.Columns.Add("AcademicYear", typeof(string));
            dtBase.Columns.Add("Conduct", typeof(string));
            dtBase.Columns.Add("Work", typeof(string));

            // Loop through base class list
            foreach (DataRow baseRow in dtBase.Rows)
            {
                string className = baseRow["ClassName"].ToString();
                bool matchFound = false;

                // First check with StudentOfficialDetails
                foreach (DataRow studentRow in dtStudent.Rows)
                {
                    string admissionClassName = studentRow["classID"].ToString();
                    string StreamID = studentRow["StreamID"].ToString();
                    if (className == admissionClassName)
                    {
                        string query222 = @"
;WITH AllSubjects AS (
    SELECT SubjectName
    FROM TTSubjectMaster 
    WHERE SessionName = @SessionName AND Classid = @ClassID AND BranchCode = @BranchCode and BranchId=@StreamID

    UNION

    SELECT SubjectName
    FROM TTSubjectMaster 
    WHERE SessionName = @SessionName AND BranchCode = @BranchCode 
      AND id IN (
          SELECT OptSubjectId 
          FROM ICSEOptionalSubjectAllotment 
          WHERE Srno = @SrNo 
            AND SessionName = @SessionName 
            AND BranchCode = @BranchCode
      )
)
SELECT STUFF((
    SELECT ', ' + SubjectName
    FROM AllSubjects
    FOR XML PATH(''), TYPE
).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS SubjectList
";
                        string AdmissionForClassId = studentRow["AdmissionForClassId"].ToString();
                        string SessionName = studentRow["SessionName"].ToString();
                        string BranchCode = studentRow["BranchCode"].ToString();
                        SqlCommand cmd22 = new SqlCommand(query222, con);
                        cmd22.Parameters.AddWithValue("@SrNo", SrNo);
                        cmd22.Parameters.AddWithValue("@ClassID", AdmissionForClassId);
                        cmd22.Parameters.AddWithValue("@SessionName", SessionName);
                        cmd22.Parameters.AddWithValue("@BranchCode", BranchCode);
                        cmd22.Parameters.AddWithValue("@StreamID", StreamID);
                        SqlDataAdapter da21 = new SqlDataAdapter(cmd22);
                        DataTable dtStudent1 = new DataTable();
                        da21.Fill(dtStudent1);
                        if (dtStudent1.Rows.Count > 0)
                        {
                            baseRow["DateOfPromotion"] = dtStudent1.Rows[0]["SubjectList"].ToString();
                            baseRow["DateOfAdmission"] = SessionName.ToString();
                        }
                        else
                        {
                            baseRow["DateOfPromotion"] = "";
                            baseRow["DateOfAdmission"] = "";
                        }

                        string query2222 = @"Select * from Exam_AttendenceDetails where srno=@SrNo and Term='Term2'
                                             and SessionName=@SessionName and  BranchCode=@BranchCode";
                        SqlCommand cmd222 = new SqlCommand(query2222, con);
                        cmd222.Parameters.AddWithValue("@SrNo", SrNo);
                        cmd222.Parameters.AddWithValue("@SessionName", SessionName);
                        cmd222.Parameters.AddWithValue("@BranchCode", BranchCode);
                        SqlDataAdapter da211 = new SqlDataAdapter(cmd222);
                        DataTable dtStudent11 = new DataTable();
                        da211.Fill(dtStudent11);
                        if (dtStudent11.Rows.Count > 0)
                        {
                            baseRow["AcademicYear"] = dtStudent11.Rows[0]["TA"].ToString();
                            baseRow["Work"] = dtStudent11.Rows[0]["TWD"].ToString();
                        }
                        else
                        {
                            baseRow["AcademicYear"] = "";
                            baseRow["Work"] = "";
                        }


                        matchFound = true;
                        break;
                    }
                }

                //// Then check with TCCollection
                //bool tcMatched = false;
                //foreach (DataRow tcRow in dtTC.Rows)
                //{
                //    string classOfFAC = tcRow["ClassOfFAC"].ToString();
                //    if (className == classOfFAC)
                //    {
                //        baseRow["Conduct"] = tcRow["ConductandWork"].ToString();
                //        baseRow["Work"] = tcRow["PassStatus"].ToString();
                //        baseRow["AcademicYear"] = tcRow["SessionName"].ToString();
                //        baseRow["DateOfRemoval"] = Convert.ToDateTime(tcRow["TCIssueDate"]).ToString("dd/MM/yyyy");
                //        baseRow["CauseOfRemoval"] = tcRow["remark"].ToString();
                //        tcMatched = true;
                //        break;
                //    }
                //}

                // If no match in both, fill blanks
                if (!matchFound)
                {
                    baseRow["DateOfAdmission"] = "";
                    baseRow["DateOfPromotion"] = "";
                    baseRow["AcademicYear"] = "";
                    baseRow["Conduct"] = "";
                    baseRow["Work"] = "";
                    baseRow["DateOfRemoval"] = "";
                    baseRow["CauseOfRemoval"] = "";
                }
            }
            foreach (DataRow row in dtBase.Rows)
            {
                con.Open();
                string insertQuery = @"
        INSERT INTO CourseSignAttendenceTCCounter
        (SrNo, CourseName,Class,Year,Subjects,NoofLecturesDelivered,NoofLecturesAttended, ModifiedBy, ModifiedDate)
        VALUES
        (@SrNo, @CourseName, @Class, @DOA, @DOP,@Year, @Work, @ModifiedBy, @ModifiedDate)";

                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
                insertCmd.Parameters.AddWithValue("@CourseName", row["CourseName"]);
                insertCmd.Parameters.AddWithValue("@Class", row["ClassName"]);
                insertCmd.Parameters.AddWithValue("@DOA", row["DateOfAdmission"]);
                insertCmd.Parameters.AddWithValue("@DOP", row["DateOfPromotion"]);
                insertCmd.Parameters.AddWithValue("@Year", row["AcademicYear"]);
                insertCmd.Parameters.AddWithValue("@Work", row["Work"]);
                insertCmd.Parameters.AddWithValue("@ModifiedBy", Session["LoginName"].ToString());
                insertCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                insertCmd.ExecuteNonQuery();
                con.Close();
            }
            con.Open();
            string getExistingQuery = @"
    SELECT ID,CourseName, Class AS ClassName,Year as DateofAdmission,Subjects as DateofPromotion, 
            NoofLecturesDelivered AS AcademicYear, 
NoofLecturesAttended AS Work
         
    FROM CourseSignAttendenceTCCounter 
    WHERE SrNo = @SrNo";

            SqlCommand getCmd = new SqlCommand(getExistingQuery, con);
            getCmd.Parameters.AddWithValue("@SrNo", Label31.Text);
            SqlDataAdapter daExist = new SqlDataAdapter(getCmd);
            DataTable dtBase1 = new DataTable(); // overwrite earlier
            daExist.Fill(dtBase1);
            con.Close();
            Repeater1.DataSource = dtBase1;
            Repeater1.DataBind();
        }
    }
    public string RemoveOrdinalSuffix(string date)
    {
        return date
            .Replace("st", "")
            .Replace("nd", "")
            .Replace("rd", "")
            .Replace("th", "");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            //HiddenField hfID = (HiddenField)item.FindControl("IDValue");
            //string idValue = hfID.Value;
            //lblID.Text = idValue;

            string idValue = btn.CommandArgument;

            lblID.Text = idValue;

            string sql = "SELECT ID,CourseName, Class AS ClassName, DateofAdmission, DateofPromotion, DateofRemoval, CauseofRemoval, Year AS AcademicYear, Conduct, Work FROM CourseSignTCCounter where ID=" + idValue;
            TxtID.Text = oo.ReturnTag(sql, "ID");
            TextBox2.Text = oo.ReturnTag(sql, "CourseName");
            TextBox1.Text = oo.ReturnTag(sql, "ClassName");
            TextBox10.Text = oo.ReturnTag(sql, "CauseofRemoval");
            string DateofAdmission = oo.ReturnTag(sql, "DateofAdmission");
            if (!string.IsNullOrWhiteSpace(DateofAdmission))
            {
                DateTime dt = DateTime.Parse(DateofAdmission, CultureInfo.InvariantCulture);
                TextBox3.Text = dt.ToString("dd-MM-yyyy");



            }
            else
            {
                TextBox3.Text = "";
            }

            // TextBox3.Text = oo.ReturnTag(sql, "DateofAdmission");
            string DateofPromotion = oo.ReturnTag(sql, "DateofPromotion");
            if (!string.IsNullOrWhiteSpace(DateofPromotion))
            {

                DateTime dt = DateTime.Parse(DateofPromotion, CultureInfo.InvariantCulture);
                TextBox4.Text = dt.ToString("dd-MM-yyyy");

            }
            else
            {
                TextBox4.Text = "";
            }



            string DateofRemoval = oo.ReturnTag(sql, "DateofRemoval");
            if (!string.IsNullOrWhiteSpace(DateofRemoval))
            {

                DateTime dt = DateTime.Parse(DateofRemoval, CultureInfo.InvariantCulture);
                TextBox5.Text = dt.ToString("dd-MM-yyyy");
                //DateTime dt;
                //if (DateTime.TryParseExact(DateofRemoval, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                //{
                //    TextBox5.Text = dt.ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                //    TextBox5.Text = ""; // fallback to empty if parsing fails
                //}
            }
            else
            {
                TextBox5.Text = "";
            }

            TextBox6.Text = oo.ReturnTag(sql, "AcademicYear");
            TextBox7.Text = oo.ReturnTag(sql, "Conduct");
            TextBox8.Text = oo.ReturnTag(sql, "Work");

            Panel1_ModalPopupExtender.Show();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            HiddenField hfID = (HiddenField)item.FindControl("IDValue1");
            string idValue = hfID.Value;
            lblID.Text = idValue;

            string sql = "SELECT ID,CourseName, Class AS ClassName, Subjects, Year, NoofLecturesDelivered, NoofLecturesAttended FROM CourseSignAttendenceTCCounter where ID=" + idValue;
            TextBox9.Text = oo.ReturnTag(sql, "CourseName");
            TextBox11.Text = oo.ReturnTag(sql, "ClassName");
            TextBox17.Text = oo.ReturnTag(sql, "Year");
            TextBox12.Text = oo.ReturnTag(sql, "Subjects");
            TextBox13.Text = oo.ReturnTag(sql, "NoofLecturesDelivered");
            TextBox14.Text = oo.ReturnTag(sql, "NoofLecturesAttended");
            TextBox15.Text = oo.ReturnTag(sql, "ID");
            ModalPopupExtender1.Show();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        con.Open();

        string updateQuery = @"
    UPDATE CourseSignTCCounter SET 
        DateOfAdmission = @DOA,
        DateOfPromotion = @DOP,
        DateofRemoval=@DOR,
        CauseofRemoval=@COR,
        Year = @Year,
        Conduct=@Conduct,
        Work = @Work,
        ModifiedBy = @ModifiedBy, 
        ModifiedDate = @ModifiedDate
    WHERE ID = @ID"; // Make sure you have a WHERE condition to update specific row

        SqlCommand updateCmd = new SqlCommand(updateQuery, con);

        updateCmd.Parameters.AddWithValue("@DOA", TextBox3.Text);
        updateCmd.Parameters.AddWithValue("@DOP", TextBox4.Text);
        updateCmd.Parameters.AddWithValue("@DOR", TextBox5.Text);
        updateCmd.Parameters.AddWithValue("@COR", TextBox10.Text);
        updateCmd.Parameters.AddWithValue("@Year", TextBox6.Text);
        updateCmd.Parameters.AddWithValue("@Conduct", TextBox7.Text);
        updateCmd.Parameters.AddWithValue("@Work", TextBox8.Text);
        updateCmd.Parameters.AddWithValue("@ModifiedBy", Session["LoginName"].ToString());
        updateCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

        // Add ID parameter to identify which row to update
        updateCmd.Parameters.AddWithValue("@ID", TxtID.Text);

        updateCmd.ExecuteNonQuery();
        con.Close();
        loaddata();

    }

    protected void Button6_Click(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        con.Open();

        string updateQuery = @"
    UPDATE CourseSignAttendenceTCCounter SET 
        Subjects = @DOA,
        NoofLecturesDelivered = @DOP,
        NoofLecturesAttended=@DOR,
        Year = @Year,
        ModifiedBy = @ModifiedBy, 
        ModifiedDate = @ModifiedDate
    WHERE ID = @ID"; // Make sure you have a WHERE condition to update specific row

        SqlCommand updateCmd = new SqlCommand(updateQuery, con);

        updateCmd.Parameters.AddWithValue("@DOA", TextBox12.Text);
        updateCmd.Parameters.AddWithValue("@DOP", TextBox13.Text);
        updateCmd.Parameters.AddWithValue("@DOR", TextBox14.Text);
        updateCmd.Parameters.AddWithValue("@Year", TextBox17.Text);
        updateCmd.Parameters.AddWithValue("@ModifiedBy", Session["LoginName"].ToString());
        updateCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

        // Add ID parameter to identify which row to update
        updateCmd.Parameters.AddWithValue("@ID", TextBox15.Text);

        updateCmd.ExecuteNonQuery();
        con.Close();
        loaddata();
    }
    public class ClassInfo
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string DateOfAdmission { get; set; }
        public string DateOfPromotion { get; set; }
        public string DateOfRemoval { get; set; }
        public string CauseOfRemoval { get; set; }
        public string AcademicYear { get; set; }
        public string Conduct { get; set; }
        public string Work { get; set; }
    }

    public class CourseGroup
    {
        public string CourseName { get; set; }
        public List<ClassInfo> ClassList { get; set; }
    }


    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintDiv", "PrintTC();", true);
    }

    protected void LinkButton2_Click1(object sender, EventArgs e)
    {
        string combinedRemarks = string.Join(Environment.NewLine, new string[]
        {
        litRemark1.Text.Trim()

        });

        // Set the value in the textbox
        TextBox16.Text = combinedRemarks;
        ModalPopupExtender2.Show();
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        con.Open();

        string sqlNews1 = "Select top 1 RemarkOtherActivities from StudentOfficialDetails where SrNo='" + Label31.Text + "' and  BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        string updateQuery = @"
    UPDATE StudentOfficialDetails SET 
        RemarkOtherActivities = @DOA
    WHERE SrNo = @ID
and BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'"; // Make sure you have a WHERE condition to update specific row
        SqlCommand updateCmd = new SqlCommand(updateQuery, con);
        updateCmd.Parameters.AddWithValue("@DOA", TextBox16.Text);
        updateCmd.Parameters.AddWithValue("@ID", Label31.Text);

        updateCmd.ExecuteNonQuery();
        con.Close();
        loaddata();
    }

    protected void Button9_Click(object sender, EventArgs e)
    {

    }
}