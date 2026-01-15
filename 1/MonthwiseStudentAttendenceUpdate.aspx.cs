using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace _1
{
    public partial class MonthwiseStudentAttendenceUpdate : Page
    {
        public static SqlConnection _con;
        private readonly Campus _oo;
        private string _sql = "";
        public static string session = "", BranchCode = "", LoginName=""; 
        public MonthwiseStudentAttendenceUpdate()
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
     
          
            if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            {
                Response.Redirect("default.aspx");
            }

            session = Session["SessionName"].ToString();
            BranchCode = Session["BranchCode"].ToString();
            LoginName = Session["LoginName"].ToString();
            _con = _oo.dbGet_connection();
            BLL.BLLInstance.LoadHeader("Report", header);
            Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

            if (!IsPostBack)
            {
                //Image1.ImageUrl = "DisplayImage.ashx?UserLoginID=" + 1;
                //sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
                //lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
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
            _sql = "Select ClassName,Id from ClassMaster where SessionName='" + Session["SessionName"] + "'";
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
            _sql = "Select SectionName,Id from SectionMaster where ClassNameId='"+drpClass.SelectedValue +"'  and SessionName='" + Session["SessionName"] + "'";
            _oo.FillDropDown_withValue(_sql, drpSection, "SectionName", "Id");
            _oo.fillSelectvalue(drpSection, "<--Select-->");
        }
        public void Fillmonth()
        {
            //sql = "WITH months(MonthNumber) AS(SELECT 12 UNION ALL SELECT MonthNumber+1 FROM months WHERE MonthNumber < 23)";
            //sql = sql + " SELECT Left(DATENAME(MONTH,DATEADD(MONTH,MonthNumber,GETDATE())),3) AS monthname,";
            //sql = sql + " DATEpart(MONTH,DATEADD(MONTH,MonthNumber,GETDATE())) as monthvalue FROM months";

            _sql = "with months (date)";
            _sql = _sql + " AS (SELECT Convert(date, (Select FromDate from SessionMaster where SessionName = '" + Session["SessionName"] + "'))";
            _sql = _sql + " UNION ALL";
            _sql = _sql + " SELECT DATEADD(month, 1, date)";
            _sql = _sql + " from months";
            _sql = _sql + " where date <= Convert(date, (Select ToDate from SessionMaster where SessionName = '" + Session["SessionName"] + "')))";
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
            hdnClass.Value = drpClass.SelectedItem.Text;
            hdnSection.Value = drpSection.SelectedItem.Text;
            hdnMonthYearTex.Value = drpMonth.SelectedItem.Text;
            hdnMonthYearVal.Value = drpMonth.SelectedValue;
            Loadgrid();
        }

        public void Loadgrid()
        {
            string branchid = drpStream.SelectedValue;

            if (RadioButtonList1.SelectedIndex == 0)
            {
                _sql = "select   so.srno  as srno,(FirstName+''+MiddleName+''+LastName) as Name from StudentOfficialDetails SO ";
                _sql = _sql + "   left join StudentGenaralDetail sg on sg.srno=so.srno";
                _sql = _sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                _sql = _sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                _sql = _sql + "   where cm.ClassName='" + drpClass.SelectedItem + "'  and sc.SectionName='" + drpSection.SelectedItem + "'  and (So.Branch='" + drpStream.SelectedValue + "' or So.Branch is null) and";
                _sql = _sql + "   sg.SessionName='" + Session["SessionName"] + "' and so.SessionName='" + Session["SessionName"] + "' and cm.SessionName='" + Session["SessionName"] + "'";
                _sql = _sql + "   and SC.SessionName='" + Session["SessionName"] + "'  and";
                _sql = _sql + "   so.BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "   and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled') order by FirstName";
            }
            else
            {
                Label lblsrno = (Label)Grd.Rows[0].FindControl("Label1");
                Label lblclass = (Label)Grd.Rows[0].FindControl("Label5");
                Label lblsection = (Label)Grd.Rows[0].FindControl("Label6");
                Label lblBranch = (Label)Grd.Rows[0].FindControl("Label29");
                _sql = "select   so.srno  as srno,(FirstName+''+MiddleName+''+LastName) as Name,bm.id from StudentOfficialDetails SO ";
                _sql = _sql + "   left join StudentGenaralDetail sg on sg.srno=so.srno";
                _sql = _sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
                _sql = _sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
                _sql = _sql + "   left join BranchMaster bm on bm.Id=So.Branch and bm.SessionName=so.SessionName";
                _sql = _sql + "   where so.srno='" + lblsrno.Text + "' and cm.ClassName='" + lblclass.Text + "'  and sc.SectionName='" + lblsection.Text + "' and (bm.BranchName='" + lblBranch.Text + "' or bm.BranchName is null)";
                _sql = _sql + "   and sg.SessionName='" + Session["SessionName"] + "' and so.SessionName='" + Session["SessionName"] + "' and cm.SessionName='" + Session["SessionName"] + "'";
                _sql = _sql + "   and SC.SessionName='" + Session["SessionName"] + "'  and";
                _sql = _sql + "   so.BranchCode=" + Session["BranchCode"] + "";
                _sql = _sql + "   and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled') order by FirstName";
                branchid = _oo.ReturnTag(_sql, "id");
            }

            GridView1.DataSource = _oo.GridFill(_sql);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                abc.Visible = true;
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    Label1.Text = drpMonth.SelectedItem.Text + " Month Student's Attendence Report " + "| Class :" + drpClass.SelectedItem.Text + "-" + drpSection.SelectedItem.Text + (drpStream.SelectedItem.Text.ToUpper() == "OTHER" ? "" : (drpStream.SelectedItem.Text.ToUpper() == "NA") ? "" : "-" + drpStream.SelectedItem.Text);
                }
                else
                {
                    Label1.Text = drpMonth1.SelectedItem + " Month Student's Attendence Report " + "| Class :" + drpClass.SelectedItem.Text + "-" + drpSection.SelectedItem.Text + (drpStream.SelectedItem.Text.ToUpper() == "OTHER" ? "" : (drpStream.SelectedItem.Text.ToUpper() == "NA") ? "" : "-" + drpStream.SelectedItem.Text);
                }
                Adddayofmonth();
                Name(branchid);
            }
            else
            {
                abc.Visible = false;
            }
        }

        public void Adddayofmonth()
        {
            string year;
            // ReSharper disable once UnusedVariable
            string[] str = Session["SessionName"].ToString().Split('-');
            int monthno = Convert.ToInt32(drpMonth.SelectedValue.Split('-')[0]);
            year = drpMonth.SelectedItem.Text.Split(' ')[1];
            //if (monthno <= 9)
            //{           
            //    year = str[0].ToString();
            //}
            //else
            //{
            //    year = str[1].ToString();
            //}
            string date1;
            if (RadioButtonList1.SelectedIndex == 0)
            {
                date1 = year + "-" + monthno + "-" + "01";
            }
            else
            {
                date1 = year + "-" + monthno + "-" + "01";
            }
            _sql = "WITH months(DATENumber) AS(SELECT 0 UNION ALL SELECT DATENumber+1 FROM months WHERE DATENumber < datediff(day, '" + date1 + "', dateadd(month, 1, '" + date1 + "'))-1)";
            _sql = _sql + " Select Convert(int,DATENAME(dd,dateadd(dd,DATENumber,'" + date1 + "'))) as dayno,Left((DATENAME(DW,dateadd(DD,DATENumber,'" + date1 + "'))),3) as dayname FROM months order by dayno Asc";
            SqlDataAdapter da = new SqlDataAdapter(_sql, _con);
            DataTable dt = new DataTable();
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
            int jj;
            int jj1;
            int jj2;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                jj = 0;
                jj1 = 0;
                jj2 = 0;
                Label lblsrno = (Label)GridView1.Rows[i].FindControl("Label2");
                string month, classname="", sectioname = "" ;

                string year = drpMonth.SelectedItem.Text.Split(' ')[1];

                if (RadioButtonList1.SelectedIndex == 0)
                {
                    month = drpMonth.SelectedItem.Text.Split(' ')[0];
                    classname = drpClass.SelectedItem.ToString();
                    sectioname = drpSection.SelectedItem.ToString();
                }
                else
                {
                    month = drpMonth1.SelectedItem.Text.Split(' ')[0];
                    if (Grd.Rows.Count > 0)
                    {
                        Label lblclass = (Label)Grd.Rows[0].FindControl("Label5");
                        Label lblsection = (Label)Grd.Rows[0].FindControl("Label6");
                        classname = lblclass.Text;
                        sectioname = lblsection.Text;
                    }
                }
                for (int j = 3; j < GridView1.Columns.Count - 3; j++)
                {
                    string[] str = GridView1.HeaderRow.Cells[j].Text.Split(' ');
                    string text = Addvalue(year ,month, classname, sectioname, branchid,lblsrno.Text, str[0], "lbl" + (j - 2), i, j);
                    if (text != "X")
                    {
                        if (text == "P" || text == "LC" || text == "LT")
                        {
                            GridView1.Rows[i].Cells[j].CssClass = "vd_bg-green vd_white";
                            //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.White;
                            jj += 1;
                        }
                        else if (text == "A")
                        {
                            GridView1.Rows[i].Cells[j].CssClass = "vd_bg-red vd_white";
                            //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.White;
                            jj1 += 1;
                        }
                        else if (text == "L")
                        {
                            GridView1.Rows[i].Cells[j].CssClass = "vd_bg-yellow vd_white";
                            //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.White;
                            jj1 += 1;
                        }
                        jj2 = jj + jj1;

                    }
                    else
                    {
                        GridView1.Rows[i].Cells[j].CssClass = "vd_bg-yellow-l vd_black";
                        //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Black;
                    }
                }
                Label lblTotalPresent = (Label)GridView1.Rows[i].FindControl("lblTotalPresent");
                lblTotalPresent.Text = jj.ToString();
                Label lblTotalAbsent = (Label)GridView1.Rows[i].FindControl("lblTotalAbsent");
                lblTotalAbsent.Text = jj1.ToString();
                Label lblTotalWorkingDays = (Label)GridView1.Rows[i].FindControl("lblTotalWorkingDays");
                lblTotalWorkingDays.Text = jj2.ToString();
            }
        }


        public string Addvalue(string year, string month, string classname, string section, string branchid, string lblsrno, string attendencedate, string lblday, int i, int j)
        {
            //Label lbl = (Label)GridView1.Rows[i].FindControl(lblday);
            TextBox lbl = (TextBox)GridView1.Rows[i].FindControl(lblday);
            _sql = "Select ar.AttendanceValue from AttendanceDetailsDateWise ar left join StudentOfficialDetails SO on So.StEnRCode=ar.StEnRCode and so.sessionname=ar.sessionname and so.BranchCode=ar.BranchCode";
            _sql = _sql + " where DATEPART(yyyy,ar.AttendanceDate)='" + year + "' and ar.AttendanceMonth='" + month + "' and ar.ClassName='" + classname + "'";
            _sql = _sql + " and ar.SrNo='" + lblsrno + "' and ar.SessionName='" + Session["SessionName"] + "' and ar.BranchCode=" + Session["BranchCode"] + "";
            _sql = _sql + " and DATEPART(dd,ar.AttendanceDate)='" + attendencedate + "'";
            if (_oo.ReturnTag(_sql, "AttendanceValue") != "")
            {
                lbl.Text = _oo.ReturnTag(_sql, "AttendanceValue").Replace(" ", "");
                lbl.ToolTip = (j-2).ToString();
            }
            else
            {
                lbl.Text = "X";
                lbl.ToolTip= (j-2).ToString();
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
            //sql = "select SG.Id, SC.SectionName,SO.Card,SO.Medium as Medium,CM.ClassName,convert(nvarchar,So.DateOfAdmiission,106) as DateOfAdmiission ,SO.SectionId,Sf.FatherName,SF.MotherName,SG.FirstName,SG.MiddleName,SG.LastName,so.StEnRCode as StEnRCode,sg.srno  as srno,case  when so.TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired,so.wayamount as wayamount from StudentGenaralDetail SG ";
            //sql = sql + "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
            //sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
            //sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
            //sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
            //sql = sql + "   where  SG." + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
            //sql = sql + "   and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
            //sql = sql + "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            //sql = sql + "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
            //sql = sql + "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
            //sql = sql + "   and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled')";
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

            _sql = "Select  SectionName,Card,Medium,ClassName,BranchName,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,";
            _sql = _sql + "  FatherName,MotherName,Name as StudentName,StEnRCode,srno,";
            _sql = _sql + "  case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired ";
            _sql = _sql + "  from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") where srno=" + "'" + studentId.Trim() + "'";
            _sql = _sql + "  and Withdrwal is null and (Promotion is null or Promotion<>'Cancelled')";


            Grd.DataSource = _oo.GridFill(_sql);
            Grd.DataBind();

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