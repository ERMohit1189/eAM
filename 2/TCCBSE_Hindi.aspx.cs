using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace _2
{
    public partial class TCCBSE_Hindi : Page
    {
        private SqlConnection con;
        private readonly Campus oo;
        private string sql;
        string id = "";
        public TCCBSE_Hindi()
        {
            con = new SqlConnection();
            oo = new Campus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            con = oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Certificate", header1);
            if (!IsPostBack)
            {
                sql = "Select AffiliationNo,SchoolNo from CollegeMaster where CollegeId=" + Session["BranchCode"] + "";
                Label1.Text = oo.ReturnTag(sql, "AffiliationNo");

                lblaffno.Text = oo.ReturnTag(sql, "AffiliationNo");
                if (oo.ReturnTag(sql, "AffiliationNo") == "")
                {
                    lblaf.Visible = false;
                }
                Label2.Text = oo.ReturnTag(sql, "SchoolNo");
                loaddata();
                setMargin();
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
            string sql1 = "select Id,srno,bookno,DatePArt(dd,AdmissionFromDate) dd1,' '+DateNAme(MONTH,AdmissionFromDate)+' '+Convert(Varchar(4),DatePArt(YEAR,AdmissionFromDate)) as AdmissionFromDate,DatePArt(dd,DateOfStruckOff) ddStruck,' '+DateNAme(MONTH,DateOfStruckOff)+' '+Convert(Varchar(4),DatePArt(YEAR,DateOfStruckOff)) as DateOfStruckOff,DatePArt(dd,TCIssueDate) dd2,' '+DateNAme(MONTH,TCIssueDate)+' '+Convert(Varchar(4),DatePArt(YEAR,TCIssueDate)) as TCIssueDate,Concession,";
            sql1 = sql1 + " ReceivedAmount,StudentName,FatherName,Class,LoginName,BranchCode,";
            sql1 = sql1 + " Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,DatePArt(dd,RecordDate) dd3,' '+DateNAme(MONTH,RecordDate)+' '+Convert(Varchar(4),DatePArt(YEAR,RecordDate)) as RecordDate,DatePArt(dd,DateOfFAD) dd4,' '+DateNAme(MONTH,DateOfFAD)+' '+Convert(varchar(4),DatePArt(YEAR,DateOfFAD)) DateOfFAD,ClassOfFAC,";
            sql1 = sql1 + " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR,IsQualified,IsQualifiedtowhichclass, tcType, PassStatus from TCCollection where RecieptNo='" + Request.QueryString["print"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
            Label31.Text = oo.ReturnTag(sql1, "srno");
            string srno = oo.ReturnTag(sql1, "srno");
            sql = "select Id,srno,bookno,DatePArt(dd,AdmissionFromDate) dd1,' '+DateNAme(MONTH,AdmissionFromDate)+' '+Convert(Varchar(4),DatePArt(YEAR,AdmissionFromDate)) as AdmissionFromDate,DatePArt(dd,TCIssueDate) dd2,' '+DateNAme(MONTH,TCIssueDate)+' '+Convert(Varchar(4),DatePArt(YEAR,TCIssueDate)) as TCIssueDate,Concession,";
            sql = sql + " ReceivedAmount,StudentName,FatherName,Class,LoginName,BranchCode,";
            sql = sql + " Subject,Dues,ConcessionType,TWD,TWDP,NCC,ECA,MotherName,DatePArt(dd,RecordDate) dd3,' '+DateNAme(MONTH,RecordDate)+' '+Convert(Varchar(4),DatePArt(YEAR,RecordDate)) as RecordDate,DatePArt(dd,DateOfFAD) dd4,' '+DateNAme(MONTH,DateOfFAD)+' '+Convert(varchar(4),DatePArt(YEAR,DateOfFAD)) DateOfFAD,ClassOfFAC,";
            sql = sql + " Sex,FatherContactNo,Amount,Remark,ConductandWork,AOR,IsQualified,IsQualifiedtowhichclass, tcType, PassStatus from TCCollection where BranchCode=" + Session["BranchCode"] + " and srno='" + srno + "' and RecieptNo='" + Request.QueryString["print"].ToString() + "' order by id asc";



            Label30.Text = oo.ReturnTag(sql1, "Id");
            id = oo.ReturnTag(sql1, "Id");

            Label32.Text = oo.ReturnTag(sql1, "bookno");
            tcCopy.Text = oo.ReturnTag(sql1, "tcType");
            string PassStatus = oo.ReturnTag(sql, "PassStatus"); ;
            Label14.Text = (PassStatus == "" ? "" : "(" + PassStatus + ")");

            string day;

            Label16.Text = oo.ReturnTag(sql, "Subject");
            Label20.Text = oo.ReturnTag(sql, "Dues");
            Label21.Text = oo.ReturnTag(sql, "ConcessionType");
            Label22.Text = oo.ReturnTag(sql, "TWD");
            Label23.Text = oo.ReturnTag(sql, "TWDP");
            Label24.Text = oo.ReturnTag(sql, "NCC");
            Label25.Text = oo.ReturnTag(sql, "ECA");
            Label26.Text = oo.ReturnTag(sql, "ConductandWork");

            day = oo.ReturnTag(sql1, "ddStruck");

            dateFormate(ref day);
            lblpupilsname.Text = day + oo.ReturnTag(sql1, "DateOfStruckOff");

            day = oo.ReturnTag(sql1, "dd1");

            dateFormate(ref day);
            Label27.Text = day + oo.ReturnTag(sql1, "AdmissionFromDate");

            day = oo.ReturnTag(sql1, "dd2");

            dateFormate(ref day);
            Label28.Text = day + oo.ReturnTag(sql1, "TCIssueDate");

            Label29.Text = oo.ReturnTag(sql, "Remark");

            Label33.Text = oo.ReturnTag(sql, "AOR");

            Label17.Text = oo.ReturnTag(sql, "IsQualified");

            string nextclass = oo.ReturnTag(sql, "IsQualifiedtowhichclass");
            if (nextclass != string.Empty)
            {
                changeClassName(ref nextclass);

            }
            Label18.Text = nextclass;


            day = oo.ReturnTag(sql, "dd4");

            dateFormate(ref day);
            Label8.Text = day + oo.ReturnTag(sql, "DateOfFAD");

            string faddinclass = oo.ReturnTag(sql, "ClassOfFAC").ToUpper();
            changeClassName(ref faddinclass);
            Label9.Text = faddinclass;

            string oldsessionname = "";
            string Top_sessionName = Session["Top_sessionName"].ToString();

            sql = "Select *from StudentOfficialDetails where SrNo='" + srno + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled'";
            if (oo.Duplicate(sql))
            {
                sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order by id Desc) As T1 order by id";
                oldsessionname = oo.ReturnTag(sql, "SessionName");
            }
            else
            {
                sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + "  and ISNULL(Promotion,'')<>'Cancelled' order by id Desc";
                oldsessionname = oo.ReturnTag(sql, "SessionName");
            }

            sql = "Select * from TCCollection where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and id='" + Label30.Text + "'";
            string classname = oo.ReturnTag(sql, "ClassWithResult");

            sql = "Select Name StudentName,FatherName,MotherName,Nationality,Category,convert(nvarchar,DOB,106) as DOB,DatePart(dd,DOB) as date,DateName(mm,DOB) as month,DatePart(yyyy,DOB) as year,RIGHT('0' + CAST(DAY(DOB) AS VARCHAR), 2) + '-' + RIGHT('0' + CAST(MONTH(DOB) AS VARCHAR), 2) + '-' + CAST(YEAR(DOB) AS VARCHAR) AS FormattedDOB,Pen,ApaarID from AllStudentRecord_UDF('" + oldsessionname + "'," + Session["BranchCode"] + ") where SrNo='" + srno + "'";
            Label3.Text = oo.ReturnTag(sql, "StudentName");
            Label4.Text = oo.ReturnTag(sql, "FatherName");
            Label5.Text = oo.ReturnTag(sql, "MotherName");

            Label6.Text = oo.ReturnTag(sql, "Nationality");
            Label7.Text = oo.ReturnTag(sql, "Category");
            lblPen.Text = oo.ReturnTag(sql, "Pen");
            lblApaarID.Text = oo.ReturnTag(sql, "ApaarID");

            day = oo.ReturnTag(sql, "date");

            dateFormate(ref day);

            Label10.Text = (oo.ReturnTag(sql, "FormattedDOB"));
            string year = oo.ReturnTag(sql, "year");
            string yearpri = "";
            string yearsuf = "";
            if (Convert.ToInt16(year) < 2000)
            {
                yearpri = oo.Date_in_Words(Convert.ToInt16(year.Substring(0, 2)));
                yearsuf = oo.Date_in_Words(Convert.ToInt16(year.Substring(2, 2)));

                year = yearpri + " Hundred " + yearsuf;
            }
            else
            {
                year = oo.Date_in_Words(Convert.ToInt16(oo.ReturnTag(sql, "year")));
            }

            Label11.Text = Date_in_Words(Convert.ToInt16(oo.ReturnTag(sql, "date"))) + " " + oo.ReturnTag(sql, "month") + " " + year;

            string status = PassStatus;
            string sqls = "Select DatePArt(dd,WithdrawalDate) dd11,' '+DateNAme(MONTH,WithdrawalDate)+' '+Convert(Varchar(4),DatePArt(YEAR,WithdrawalDate)) as WithdrawalDate From StudentWithdrawal where srno='" + srno + "' and BranchCode=" + Session["BranchCode"] + "";
            day = oo.ReturnTag(sqls, "dd11");
            dateFormate(ref day);

            if (status.ToUpper() == "PASSED")
            {

                changeClassName(ref classname);
                Label13.Text = classname;
                Label15.Text = "NO";
            }
            else if (status.ToUpper() == "PROMOTED")
            {

                changeClassName(ref classname);
                Label13.Text = classname;
                Label15.Text = "NO";
            }
            else if (status.ToUpper() == "FAILED")
            {
                Label15.Text = "YES";
                changeClassName(ref classname);
                Label13.Text = classname;
            }
            else if (status.ToUpper() == "COMPARTMENT")
            {
                changeClassName(ref classname);
                Label13.Text = classname;
                Label15.Text = PassStatus;
            }
            else if (status.ToUpper() == "ABSENT")
            {
                changeClassName(ref classname);
                Label13.Text = classname;

                Label15.Text = PassStatus;
            }
            else if (status.ToUpper() == "LEFT")
            {
                sql = "Select SessionName from";
                sql = sql + " (Select ROW_NUMBER() Over(order by SessionId desc) srno, sessionId, sof.SessionName from StudentOfficialDetails sof";
                sql = sql + " inner join SessionMaster sm on sm.SessionName = sof.SessionName";
                sql = sql + " where SrNo = '" + srno + "' and sof.BranchCode=" + Session["BranchCode"] + ")T1 where srno = 1";
                string sessionname = oo.ReturnTag(sql, "SessionName");
                sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                sql = sql + " where SrNo='" + srno + "' and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + sessionname + "' and so.SessionName='" + sessionname + "'";
                classname = oo.ReturnTag(sql, "ClassName");
                changeClassName(ref classname);
                Label13.Text = classname;
                Label15.Text = "NO";
            }
            else
            {
                string secondlastsessionid = "";
                sql = "Select *from StudentOfficialDetails where SrNo='" + srno + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
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
                sql = "Select *from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Top_sessionName + "' and Promotion is null";
                if (oo.Duplicate(sql))
                {
                    sql = "Select SessionName from SessionMaster where SessionId='" + secondlastsessionid + "' and BranchCode=" + Session["BranchCode"] + "";
                    string sessionname = oo.ReturnTag(sql, "SessionName");
                    sql = "Select ClassWithResult from TCCollection where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and id='" + Label30.Text + "'";
                    classname = oo.ReturnTag(sql, "ClassWithResult");
                    if (oo.ReturnTag(sql, "ClassWithResult") != "")
                    {
                        classname = oo.ReturnTag(sql, "ClassWithResult");
                        changeClassName(ref classname);
                        Label13.Text = classname;
                    }
                    else
                    {
                        sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                        sql = sql + " where SrNo='" + srno + "' and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Top_sessionName + "' and so.SessionName='" + Top_sessionName + "'";
                        classname = oo.ReturnTag(sql, "ClassName");
                        changeClassName(ref classname);
                        Label13.Text = classname;
                    }
                }
                else
                {
                    sql = "Select ClassName from StudentOfficialDetails so inner join ClassMaster cm on cm.Id=so.AdmissionForClassId";
                    sql = sql + " where SrNo='" + srno + "' and so.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Top_sessionName + "' and so.SessionName='" + Top_sessionName + "'";
                    classname = oo.ReturnTag(sql, "ClassName");
                    changeClassName(ref classname);
                    Label13.Text = classname;
                }
                Label15.Text = "NO";
            }
            string sessionnamed = "";
            sql = "Select *from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Session["SessionName"] + "' and ISNULL(Promotion,'')<>'Cancelled'";
            if (oo.Duplicate(sql))
            {
                //sql1 = "Select top 1 SessionName from SessionMaster order by SessionId desc";
                sql = "Select *from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and SessionNAme='" + Top_sessionName + "' and Promotion is null";
                if (oo.Duplicate(sql))
                {
                    sql = "Select Top 1 SessionName from (Select Top 2 SessionName,id from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc) As T1 order by id";
                    sessionnamed = oo.ReturnTag(sql, "SessionName");
                }
                else
                {
                    sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc";
                    sessionnamed = oo.ReturnTag(sql, "SessionName");
                }
            }
            else
            {
                if (status.ToUpper() == "LEFT")
                {
                    sql = "Select SessionName from";
                    sql = sql + " (Select ROW_NUMBER() Over(order by SessionId desc) srno, sessionId, sof.SessionName from StudentOfficialDetails sof";
                    sql = sql + " inner join SessionMaster sm on sm.SessionName = sof.SessionName";
                    sql = sql + " where SrNo = '" + Label31.Text + "' and sm.BranchCode=" + Session["BranchCode"] + " and sof.BranchCode=" + Session["BranchCode"] + ")T1 where srno = 2";

                    sessionnamed = oo.ReturnTag(sql, "SessionName");
                }

                else
                {
                    sql = "Select Top 1 SessionName from StudentOfficialDetails where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and ISNULL(Promotion,'')<>'Cancelled' order by id Desc";
                    sessionnamed = oo.ReturnTag(sql, "SessionName");
                }
            }

            sql = "Select * from TCCollection where SrNo='" + srno + "' and BranchCode=" + Session["BranchCode"] + " and id='" + Label30.Text + "'";
            classname = oo.ReturnTag(sql, "Class");
            string completeClass = ""; string changeClassNames = "";
            if (classname.ToUpper() == "I" || classname.ToUpper() == "II" || classname.ToUpper() == "II" || classname.ToUpper() == "VI" || classname.ToUpper() == "V" || classname.ToUpper() == "VI" || classname.ToUpper() == "VII" || classname.ToUpper() == "VIII" || classname.ToUpper() == "IX" || classname.ToUpper() == "X" || classname.ToUpper() == "XI" || classname.ToUpper() == "XII")
            {
                changeClassNames = classname;
                changeClassName(ref changeClassNames);
                completeClass = "(" + changeClassNames + ") / ";
                if (classname.Contains("Nur"))
                {
                    completeClass = completeClass + classname;
                }
                else
                {
                    completeClass = completeClass + BAL.objBal.convertRomantostring(classname.ToUpper());
                }
                Label12.Text = completeClass;


            }
            else
            {
                Label12.Text = classname;
            }
        }



        public void changeClassName(ref string classname)
        {
            string[] changeclassname = classname.Split(' ');
            if (changeclassname[0].ToUpper() == "I")
            {
                classname = changeclassname[0].ToUpper() + "st" + (changeclassname.Length > 1 ? " " + changeclassname[1] : "");
            }
            else if (changeclassname[0].ToUpper() == "II")
            {
                classname = changeclassname[0].ToUpper() + "nd" + (changeclassname.Length > 1 ? " " + changeclassname[1] : "");
            }
            else if (changeclassname[0].ToUpper() == "III")
            {
                classname = changeclassname[0].ToUpper() + "rd" + (changeclassname.Length > 1 ? " " + changeclassname[1] : "");
            }
            else if (changeclassname[0].ToUpper() == "IV" || changeclassname[0].ToUpper() == "V" || changeclassname[0].ToUpper() == "VI" || changeclassname[0].ToUpper() == "VII" || changeclassname[0].ToUpper() == "VIII" || changeclassname[0].ToUpper() == "IX" || changeclassname[0].ToUpper() == "X" || changeclassname[0].ToUpper() == "XI" || changeclassname[0].ToUpper() == "XII")
            {
                classname = changeclassname[0].ToUpper() + "th" + (changeclassname.Length > 1 ? " " + changeclassname[1] : "");
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


        private void loadNextClass()
        {
            string sessionnamed = "";
            string classid = "";
            sql = "Select *from StudentOfficialDetails where SrNo='" + Label31.Text + "' and SessionNAme='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                sql = "Select Top 1 SessionName,AdmissionForClassId from (Select Top 2 SessionName,id,AdmissionForClassId from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc) As T1 order by id";
                sessionnamed = oo.ReturnTag(sql, "SessionName");
                classid = oo.ReturnTag(sql, "AdmissionForClassId");
            }
            else
            {
                sql = "Select Top 1 SessionName,AdmissionForClassId from StudentOfficialDetails where SrNo='" + Label31.Text + "' and BranchCode=" + Session["BranchCode"] + " order by id Desc";
                sessionnamed = oo.ReturnTag(sql, "SessionName");
                classid = oo.ReturnTag(sql, "AdmissionForClassId");
            }


            sql = "Select Top 1 ClassName from ClassMaster where CIDOrder=(Select CIDOrder+1 from ClassMaster where SessionName='" + sessionnamed + "' and BranchCode=" + Session["BranchCode"] + " and id='" + classid + "') and SessionName='" + sessionnamed + "' and BranchCode=" + Session["BranchCode"] + "";

            if (oo.ReturnTag(sql, "ClassName") != string.Empty)
            {
                string nextclass = oo.ReturnTag(sql, "ClassName");
                if (nextclass != string.Empty)
                {
                    changeClassName(ref nextclass);

                }
                Label18.Text = nextclass;
            }
            else
            {
                Label18.Text = "HIGHER STUDIES";
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void lnkExcel_Click(object sender, EventArgs e)
        {
            oo.ExportDivToExcel(Response, "TC.xls", divexport);
        }

        protected void lnkPdf_Click(object sender, EventArgs e)
        {
            oo.ExporttoPdf(Response, "TC", divexport);
        }

        protected void lnkWord_Click(object sender, EventArgs e)
        {
            oo.ExportToWord(Response, "TC", divexport);
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        public Tuple<string, string, string, string, string> getDocSetting()
        {
            string mrgTop = "0px", mrgBott = "0px", mrgLeft = "0px", mrgRight = "0px";
            string font = "12px";

            try
            {
                DataSet ds = new DataSet();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@QueryFor", "S"));
                param.Add(new SqlParameter("@DocCategory", "Certificate"));
                param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
                ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("DocSetting_Proc", param);
                if (ds != null)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //drpFontsize.SelectedValue = dt.Rows[0]["FontSize"].ToString();
                        font = dt.Rows[0]["FontSize"] + "px";

                        mrgTop = (Convert.ToDouble(dt.Rows[0]["MarginTop"].ToString()) * 96) + "px";
                        //margin. = mrgTop;
                        mrgBott = (Convert.ToDouble(dt.Rows[0]["MarginBottom"].ToString()) * 96) + "px";
                        //margin.Add(mrgBott);
                        mrgLeft = (Convert.ToDouble(dt.Rows[0]["MarginLeft"].ToString()) * 96) + "px";
                        //margin.Add(mrgLeft);
                        mrgRight = (Convert.ToDouble(dt.Rows[0]["MarginRight"].ToString()) * 96) + "px";
                        //margin.Add(mrgRight);
                    }
                }
            }
            catch
            {
                // ignored
            }

            return new Tuple<string, string, string, string, string>(mrgTop, mrgRight, mrgBott, mrgLeft, font);
        }

        public void setMargin()
        {
            Table1.Style.Add("margin-top", getDocSetting().Item1);
            Table1.Style.Add("margin-right", getDocSetting().Item2);
            Table1.Style.Add("margin-bottom", getDocSetting().Item3);
            Table1.Style.Add("margin-left", getDocSetting().Item4);

            ScriptManager.RegisterStartupScript(Page, GetType(), "font", "setFont('" + getDocSetting().Item5 + "');", true);
        }

        public override void Dispose()
        {
            con.Dispose();
            oo.Dispose();
        }

        protected void Label12_TextChanged(object sender, EventArgs e)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "update TCCollection set Class='" + Label12.Text + "' where RecieptNo='" + Request.QueryString["print"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("TCForCbseBoard.aspx?print=1");
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }
    }
}