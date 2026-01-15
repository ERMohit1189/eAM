using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1
{
    public partial class AdminMonthwiseStudentAttendenceReportNew : Page
    {
        private SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        public AdminMonthwiseStudentAttendenceReportNew()
        {
            _con = new SqlConnection();
            _oo = new Campus();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Grd.Rows.Count > 0)
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
     
            if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }
            
            _con = _oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Report", header);
            Campus camp = new Campus(); camp.LoadLoader(loader);

            if (!IsPostBack)
            {
                abc.Visible = false;
                Fillmonth();
                LoadClass();
                _oo.fillSelectvalue(drpStream, "<--Select-->");
                _oo.fillSelectvalue(drpSection, "<--Select-->");
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
            }
        }
        public void LoadClass()
        {
            _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql,drpClass, "ClassName", "Id");
            _oo.fillSelectvalue(drpClass, "<--Select-->");
        }
        private void LoadStream(string classid)
        {
            _sql = "Select BranchName,Id from BranchMaster where classId='" + classid + "'";
            _sql = _sql + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpStream, "BranchName", "Id");
            _oo.fillSelectvalue(drpStream, "<--Select-->");
        }
        public void LoadSection()
        {
            _sql = "Select SectionName,Id from SectionMaster where ClassNameId='"+drpClass.SelectedValue +"'  and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(_sql, drpSection, "SectionName", "Id");
            _oo.fillSelectvalue(drpSection, "<--Select-->");
        }
        public void Fillmonth()
        {

            _sql = "with months (date)";
            _sql = _sql + " AS (SELECT Convert(date, (Select FromDate from SessionMaster where SessionName = '" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "))";
            _sql = _sql + " UNION ALL";
            _sql = _sql + " SELECT DATEADD(month, 1, date)";
            _sql = _sql + " from months";
            _sql = _sql + " where date <= Convert(date, (Select ToDate from SessionMaster where SessionName = '" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")))";
            _sql = _sql + " select Left(Datename(month,date),3)+' '+Convert(Varchar(5),Datepart(YEAR,date)) monthname,Convert(Varchar(5),Datepart(month,date))+'-'+Convert(Varchar(5),Datepart(year,date)) monthvalue from months";
            _oo.FillDropDown_withValue(_sql, drpMonth, "monthname", "monthvalue");
            _oo.FillDropDown_withValue(_sql, drpMonth1, "monthname", "monthvalue");
            drpMonth1.Items.Insert(0, "<--Select-->");
        }
        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream(drpClass.SelectedValue);
            LoadSection();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            Loadgrid();
        }

        public void Loadgrid()
        {
            Panel2.Visible = true;
            string branchid = drpStream.SelectedValue;
            string month = drpMonth.SelectedItem.Text.Split(' ')[0];
            string year = drpMonth.SelectedItem.Text.Split(' ')[1];

            if (RadioButtonList1.SelectedIndex == 0)
            {
                month = drpMonth.SelectedItem.Text.Split(' ')[0];
                year = drpMonth.SelectedItem.Text.Split(' ')[1];
                _sql = "select   srno, Name from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") ";
                _sql = _sql + "   where classid='" + drpClass.SelectedValue.Trim() + "'  and SectionName='" + drpSection.SelectedItem.Text + "'  and (BranchId='" + drpStream.SelectedValue + "' or BranchId is null) and";
                _sql = _sql + "   Withdrwal is null and (Promotion is null or Promotion<>'Cancelled')";
                _sql = _sql + "   and convert(date, DateOfAdmiission)< convert(date, DATEADD(s, -1, DATEADD(mm, DATEDIFF(m, 0, '28-"+ month + "-"+ year + "') + 1, 0))) order by Name";
            }
            else
            {
                month = drpMonth1.SelectedItem.Text.Split(' ')[0];
                year = drpMonth1.SelectedItem.Text.Split(' ')[1];
                Label lblsrno = (Label)Grd.Rows[0].FindControl("Label1");
                Label lblclass = (Label)Grd.Rows[0].FindControl("lblClass");
                Label lblsection = (Label)Grd.Rows[0].FindControl("lblSection");
                Label lblBranch = (Label)Grd.Rows[0].FindControl("lblBranch");
                _sql = "select   srno, Name from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") ";
                _sql = _sql + "   where ClassName='" + lblclass.Text + "'  and SectionName='" + lblsection.Text + "'  and (BranchName='" + lblBranch.Text + "' or BranchName is null) ";
                _sql = _sql + "   and Withdrwal is null and (Promotion is null or Promotion<>'Cancelled') and srno='"+ lblsrno.Text + "'";
                _sql = _sql + "   and convert(date, DateOfAdmiission)< convert(date, DATEADD(s, -1, DATEADD(mm, DATEDIFF(m, 0, '28-" + month + "-" + year + "') + 1, 0))) order by Name";
                branchid = _oo.ReturnTag(_sql, "id");
            }

            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                Label1.Text = "";
                abc.Visible = true;
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    Label1.Text = "Student's Attendance Report | Month: " + drpMonth.SelectedItem.Text + "| Class: " + drpClass.SelectedItem.Text.Trim() + " " + drpSection.SelectedItem.Text + (drpStream.SelectedItem.Text.ToUpper() == "OTHER" ? "" : (drpStream.SelectedItem.Text.ToUpper() == "NA") ? "" : "-" + drpStream.SelectedItem.Text);
                }
                else
                {
                    Label combineClass = (Label)Grd.Rows[0].FindControl("combineClass");
                    Label1.Text = "Student's Attendance Report | Month: " + drpMonth1.SelectedItem + "| Class: " + combineClass.Text.Trim();
                }
                Adddayofmonth();
                Name(branchid);
            }
            else
            {
                abc.Visible = false;
            }
        }
        DataTable dt = new DataTable();
        public void Adddayofmonth()
        {
            string year;
            // ReSharper disable once UnusedVariable
            string[] str = Session["SessionName"].ToString().Split('-');
            int monthno = Convert.ToInt32(drpMonth.SelectedValue.Split('-')[0]);
            year = drpMonth.SelectedItem.Text.Split(' ')[1];
            
            string date1;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                date1 = year + "-" + monthno + "-" + "01";
            }
            else
            {
                monthno = Convert.ToInt32(drpMonth1.SelectedValue.Split('-')[0]);
                year = drpMonth1.SelectedItem.Text.Split(' ')[1];
                date1 = year + "-" + monthno + "-" + "01";
            }
            _sql = "WITH months(DATENumber) AS(SELECT 0 UNION ALL SELECT DATENumber+1 FROM months WHERE DATENumber < datediff(day, '" + date1 + "', dateadd(month, 1, '" + date1 + "'))-1)";
            _sql = _sql + " Select Convert(int,DATENAME(dd,dateadd(dd,DATENumber,'" + date1 + "'))) as dayno,Left((DATENAME(DW,dateadd(DD,DATENumber,'" + date1 + "'))),3) as dayname FROM months order by dayno Asc";
            SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
            
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (GridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView1.HeaderRow.Cells[i + 3].Text = dt.Rows[i][0] +" " + Environment.NewLine + dt.Rows[i][1];
                    }
                }
            }
        }
        public void Name(string branchid)
        {
            int P;
            int A;
            int W;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                P = 0;
                A = 0;
                W = 0;

                Label lblsrno = (Label)GridView1.Rows[i].FindControl("Label2");
                string month, classname="", sectioname = "" ;

                string year = drpMonth.SelectedItem.Text.Split(' ')[1];

                if (RadioButtonList1.SelectedIndex == 0)
                {
                    month = drpMonth.SelectedItem.Text.Split(' ')[0];
                    classname = drpClass.SelectedItem.ToString().Trim();
                    sectioname = drpSection.SelectedItem.ToString();
                }
                else
                {
                    year = drpMonth1.SelectedItem.Text.Split(' ')[1];
                    month = drpMonth1.SelectedItem.Text.Split(' ')[0];
                    if (Grd.Rows.Count > 0)
                    {
                        Label lblclass = (Label)Grd.Rows[0].FindControl("lblClass");
                        Label lblsection = (Label)Grd.Rows[0].FindControl("lblSection");
                        classname = lblclass.Text.Trim();
                        sectioname = lblsection.Text;
                    }
                }
                int cnt = 0;
                if (dt.Rows.Count == 28)
                {
                    cnt = GridView1.Columns.Count - 6;
                }
                if (dt.Rows.Count==29)
                {
                    cnt = GridView1.Columns.Count - 5;
                }
                if (dt.Rows.Count == 30)
                {
                    cnt = GridView1.Columns.Count - 4;
                }
                if (dt.Rows.Count == 31)
                {
                    cnt = GridView1.Columns.Count - 3;
                }
                for (int j = 3; j < cnt; j++)
                {
                    string[] str = GridView1.HeaderRow.Cells[j].Text.Split(' ');
                    string text = Addvalue(year, month, classname, sectioname, branchid,lblsrno.Text, str[0].Trim(), "lbl" + (j - 2), i, j);
                    if (text != "X")
                    {
                        if (text == "P" || text == "LC" || text == "LT" || text == "HD" || text == "SL")
                        {
                            GridView1.Rows[i].Cells[j].CssClass = "vd_bg-green vd_white";
                            P += 1;
                            W += 1;
                        }
                        else if (text == "A" || text == "L" || text == "RS" || text == "ML")
                        {
                            GridView1.Rows[i].Cells[j].CssClass = "vd_bg-red vd_white";
                            A += 1;
                            W += 1;
                        }
                        else if(text == "NA" || text=="NE" || text == "NAD")
                        {
                            GridView1.Rows[i].Cells[j].CssClass = "vd_bg-yellow vd_white";
                        }
                    }
                    else
                    {
                        GridView1.Rows[i].Cells[j].CssClass = "vd_bg-yellow-l vd_black";
                    }
                }
                string years = drpMonth.SelectedItem.Text.Split(' ')[1];



                string tdays = " SELECT COUNT(DISTINCT AttendanceDate)cnt FROM AttendanceDetailsDateWise ar ";
                tdays= tdays+ "INNER JOIN AttendanceAbbreviationMaster am ON am.AbbreviationName = ar.AttendanceValue ";
                tdays = tdays + "WHERE ar.BranchCode = " + Session["BranchCode"] + " and ar.SessionName = '" + Session["SessionName"] + "' and SectionName = '" + sectioname + "' and SrNo in  ";
                tdays = tdays + "(select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "', " + Session["BranchCode"] + ") where ClassName = '"+ classname + "' and SectionName = '"+sectioname+"') ";
                tdays = tdays + "AND DATEPART(yyyy,AttendanceDate)='" + year + "' and ltrim(rtrim(AttendanceMonth))='" + month + "' AND ar.AttendanceValue IN ('P', 'LT', 'LC', 'HD', 'SL') ";


                Label lblTotalWorkingDays = (Label)GridView1.Rows[i].FindControl("lblTotalWorkingDays");
                lblTotalWorkingDays.Text = _oo.ReturnTag(tdays, "cnt");
                Label lblTotalPresent = (Label)GridView1.Rows[i].FindControl("lblTotalPresent");
                lblTotalPresent.Text = P.ToString();
                Label lblTotalAbsent = (Label)GridView1.Rows[i].FindControl("lblTotalAbsent");
                lblTotalAbsent.Text = A.ToString(); 
            }
        }


        public string Addvalue(string year, string month, string classname, string section, string branchid, string lblsrno, string attendencedate, string lblday, int i, int j)
        {
            string dayAtt = attendencedate;
            dayAtt = (int.Parse(dayAtt) < 10 ? "0" + dayAtt : dayAtt);
            Label lbl = (Label)GridView1.Rows[i].FindControl(lblday);
            _sql = "Select AttendanceValue, convert(date, AttendanceDate) AttendanceDate from AttendanceDetailsDateWise ";
            _sql = _sql + " where DATEPART(yyyy,AttendanceDate)='" + year + "' and ltrim(rtrim(AttendanceMonth))='" + month + "' and ltrim(rtrim(ClassName))='" + classname + "'";
            _sql = _sql + " and SrNo='" + lblsrno + "' and SessionName='" + Session["SessionName"] + "' and BranchCode = " + Session["BranchCode"] + "";
            _sql = _sql + " and DATEPART(dd,AttendanceDate)='" + dayAtt + "' and SectionName='"+ section + "'";
            attendencedate = (int.Parse(attendencedate) < 10 ? "0" + attendencedate : attendencedate) + "-" + month + "-" + year;
            string sql2 = "";
            sql2 = "select * from AttendanceDetailsDateWise where SessionName = '" + Session["SessionName"] + "' and SrNo = '" + lblsrno + "' and convert(date, AttendanceDate)='" + attendencedate + "' and BranchCode = " + Session["BranchCode"] + "  and SectionName='" + section + "'";
            string sql3 = "";
            sql3 = "select * from AttendanceDetailsDateWise where SessionName = '" + Session["SessionName"] + "' and convert(date, AttendanceDate)='" + attendencedate + "' and BranchCode = " + Session["BranchCode"] + "  and SectionName='" + section + "'";
            if(_oo.ReturnTag(sql2, "cnt")!="0" && _oo.ReturnTag(sql3, "cnt") != "0")
            {
                lbl.Text = _oo.ReturnTag(_sql, "AttendanceValue").Replace(" ", "");
            }
            else if (_oo.ReturnTag(sql2, "cnt") == "0" && _oo.ReturnTag(sql3, "cnt") != "0")
            {
                lbl.Text = "A";
            }
            else if (_oo.ReturnTag(sql3, "cnt") == "0")
            {
                lbl.Text = "X";
            }
            if (attendencedate!= "&nbsp;")
            {
                string monthf = drpMonth.SelectedValue.Split('-')[0];
                string yearf = drpMonth.SelectedValue.Split('-')[1];
                if (RadioButtonList1.SelectedIndex == 1)
                {
                    monthf = drpMonth1.SelectedValue.Split('-')[0];
                    yearf = drpMonth1.SelectedValue.Split('-')[1];
                }
                if (int.Parse(monthf) < 10)
                {
                    monthf = ("0" + monthf);
                }
                 string AttendanceDate = (yearf + "-"+ monthf+"-"+ dayAtt).ToString();
                string _sql2 = "select  format(DateOfAdmiission, 'dd') day, format(DateOfAdmiission, 'MMM') mon,format(DateOfAdmiission, 'yyyy') yer, convert(date, DateOfAdmiission) DateOfAdmiission from StudentOfficialDetails where SrNo='" + lblsrno.Trim() + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
                if (_oo.ReturnTag(_sql2, "day").ToString() == dayAtt && _oo.ReturnTag(_sql2, "mon").ToString() == month && _oo.ReturnTag(_sql2, "yer").ToString() == year)
                {
                    lbl.Text = "NAD";
                }
                if (DateTime.Parse(_oo.ReturnTag(_sql2, "DateOfAdmiission").ToString()) >= DateTime.Parse(AttendanceDate))
                {
                    lbl.Text = "NAD";
                }
            }
            return lbl.Text;
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
            {
                _oo.ExportToWord(Response, "MonthwiseStudentAttendenceReport.doc", divExport);
            }
        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
            {
                _oo.ExportToExcel("MonthwiseStudentAttendenceReport.xls", GridView1);
            }
        }
        protected void ImageButton3_Click(object sender, EventArgs e)
        {

        }
        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            PrintHelper_New.ctrl = abc;
            ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            SinglestudentRecord(); 
        }
        protected void drpMonth1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadgrid();
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {        
            abc.Visible = false;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = true;
                Grd.DataSource = null;
                Grd.DataBind();
                TxtEnter.Text = "";
                drpMonth1.SelectedIndex = 0;
            }
            else
            {
                table1.Visible = true;
                table4.Visible = false;
            }
        }

        protected void TxtEnter_TextChanged(object sender, EventArgs e)
        {
            SinglestudentRecord();
        }

        public void SinglestudentRecord()
        {
            var studentId = Request.Form[hfStudentId.UniqueID];
            if (string.IsNullOrEmpty(studentId))
            {
                studentId = TxtEnter.Text.Trim();
            }

            _sql = "Select id,SrNo,StEnRCode,CombineClassName,Name as StudentName,FatherName,ClassName,SectionName,Medium,Card,Convert(varchar(11),DateOfAdmiission) as DateOfAdmiission,CourseName,BranchName,FamilyContactNo,PhotoPath";
            _sql = _sql + " from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") asr where srno='" + studentId + "' and Withdrwal is null";
            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();
            DataSet ds;
            ds = _oo.GridFill(_sql);
            // ReSharper disable once UseNullPropagation
            if (ds != null && Grd.Rows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = BAL.objBal.dbGet_connection().ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "USP_StudentsPhotoReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sessionName", Session["SessionName"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@SrNo", studentId.ToString().Trim());
                        cmd.Parameters.AddWithValue("@action", "details");
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        DataSet dsPhoto = new DataSet();
                        das.Fill(dsPhoto);
                        cmd.Parameters.Clear();


                        grdshow.Visible = true;
                        if (dsPhoto.Tables[0].Rows.Count > 0)
                        {
                            img.ImageUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            studentImg.NavigateUrl = dsPhoto.Tables[0].Rows[0]["PhotoPath"].ToString() != string.Empty ? dsPhoto.Tables[0].Rows[0]["PhotoPath"] + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : "../img/user-pic/user-pic.jpg" + "?value=" + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                            hylinkmoredetails.NavigateUrl = "../11/StudentRegView.aspx?print=1&id=" + ds.Tables[0].Rows[0]["stenrcode"];
                        }
                    }
                }
            }

            if (Grd.Rows.Count > 0)
            {
                table2.Visible = true;
                table3.Visible = true;
            }
            else
            {
                table2.Visible = false;
                table3.Visible = false;
                //oo.MessageBoxforUpdatePanel("Sorry, No record(s) found!", LinkButton2);
                Campus camp = new Campus(); camp.msgbox(Page, msgbox1, "Sorry, No record(s) found!", "A");

            }
        }

        public override void Dispose()
        {
            _con.Dispose();
            _oo.Dispose();
        }
    }
}