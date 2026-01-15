using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_MonthwiseStudentAttendenceReport_Newoo : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        BLL.BLLInstance.LoadHeader("Report", header);
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            abc.Visible = false;
            loadClass();
            fillmonth();
            oo.fillSelectvalue(drpStream, "<--Select-->");
            oo.fillSelectvalue(drpSection, "<--Select-->");
            table1.Visible = false;
            table2.Visible = false;
            table3.Visible = false;
        }
    }
    private void loadClass()
    {

        sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassTeacherMaster ctm";
        sql = sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName";
        sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' and ctm.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and IsClassTeacher=1 Order by CIDOrder";

        oo.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        oo.fillSelectvalue(drpClass, "<--Select-->");

    }
    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStream(drpClass.SelectedValue.ToString());
        loadSection(drpClass.SelectedValue.ToString());
    }
    private void loadStream(string classid)
    {
       
        sql = "Select BranchName,bm.Id as Id from ClassTeacherMaster ctm";
        sql = sql + " inner join BranchMaster bm on bm.Id=ctm.BranchId and bm.SessionName=ctm.SessionName";
        sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' and ctm.BranchCode=" + Session["BranchCode"].ToString() + " and IsClassTeacher=1";

        oo.FillDropDown_withValue(sql, drpStream, "BranchName", "Id");
        oo.fillSelectvalue(drpStream, "<--Select-->");

    }
    private void loadSection(string classid)
    {
        sql = "Select SectionName,bm.Id as Id from ClassTeacherMaster ctm";
        sql = sql + " inner join SectionMaster bm on bm.ClassNameId=ctm.ClassId and bm.id=ctm.SectionId and bm.SessionName=ctm.SessionName";
        sql = sql + " where ctm.SessionName='" + Session["SessionName"].ToString() + "' and EmpCode='" + Session["LoginName"].ToString() + "' ";
        sql = sql + " and IsClassTeacher=1";

        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "Id");
        oo.fillSelectvalue(drpSection, "<--Select-->");
    }
    public void fillmonth()
    {
        sql = "with months (date)";
        sql = sql + " AS (SELECT Convert(date, (Select FromDate from SessionMaster where SessionName = '" + Session["SessionName"].ToString() + "'))";
        sql = sql + " UNION ALL";
        sql = sql + " SELECT DATEADD(month, 1, date)";
        sql = sql + " from months";
        sql = sql + " where date <= Convert(date, (Select ToDate from SessionMaster where SessionName = '" + Session["SessionName"].ToString() + "')))";
        sql = sql + " select Left(Datename(month,date),3)+' '+Convert(Varchar(5),Datepart(YEAR,date)) monthname,Convert(Varchar(5),Datepart(month,date))+'-'+Convert(Varchar(5),Datepart(year,date)) monthvalue from months";

        oo.FillDropDown_withValue(sql, drpMonth, "monthname", "monthvalue");
        oo.FillDropDown_withValue(sql, drpMonth1, "monthname", "monthvalue");
        drpMonth1.Items.Insert(0, "<--Select-->");
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        //loadgrid();
    }
    //public void loadgrid()
    //{
    //    string branchid = drpStream.SelectedValue.ToString();

    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //        sql = "select   so.srno  as srno,(FirstName+''+MiddleName+''+LastName) as Name from StudentOfficialDetails SO ";
    //        sql = sql + "   left join StudentGenaralDetail sg on sg.srno=so.srno";
    //        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
    //        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
    //        sql = sql + "   where cm.ClassName='" + drpClass.SelectedItem.ToString() + "'  and sc.SectionName='" + drpSection.SelectedItem.ToString() + "'  and (So.Branch='" + drpStream.SelectedValue.ToString() + "' or So.Branch is null) and";
    //        sql = sql + "   sg.SessionName='" + Session["SessionName"].ToString() + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
    //        sql = sql + "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
    //        sql = sql + "   so.BranchCode=" + Session["BranchCode"].ToString() + "";
    //        sql = sql + "   and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled') order by FirstName";
    //    }
    //    else
    //    {
    //        Label lblsrno = (Label)Grd.Rows[0].FindControl("Label1");
    //        Label lblclass = (Label)Grd.Rows[0].FindControl("Label5");
    //        Label lblsection = (Label)Grd.Rows[0].FindControl("Label6");
    //        Label lblBranch = (Label)Grd.Rows[0].FindControl("Label29");
    //        sql = "select   so.srno  as srno,(FirstName+''+MiddleName+''+LastName) as Name,bm.id from StudentOfficialDetails SO ";
    //        sql = sql + "   left join StudentGenaralDetail sg on sg.srno=so.srno";
    //        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
    //        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
    //        sql = sql + "   left join BranchMaster bm on bm.Id=So.Branch and bm.SessionName=so.SessionName";
    //        sql = sql + "   where so.srno='" + lblsrno.Text + "' and cm.ClassName='" + lblclass.Text + "'  and sc.SectionName='" + lblsection.Text + "' and (bm.BranchName='" + lblBranch.Text.ToString() + "' or bm.BranchName is null)";
    //        sql = sql + "   and sg.SessionName='" + Session["SessionName"].ToString() + "' and so.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
    //        sql = sql + "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
    //        sql = sql + "   so.BranchCode=" + Session["BranchCode"].ToString() + "";
    //        sql = sql + "   and SO.Withdrwal is null and (SO.Promotion is null or SO.Promotion<>'Cancelled') order by FirstName";
    //        branchid = oo.ReturnTag(sql, "id");
    //    }

    //    GridView1.DataSource = oo.GridFill(sql);
    //    GridView1.DataBind();
    //    if (GridView1.Rows.Count > 0)
    //    {
    //        abc.Visible = true;
    //        if (RadioButtonList1.SelectedIndex == 0)
    //        {
    //            Label1.Text = drpMonth.SelectedItem.Text + " Month Student's Attendence Report " + "| Class :" + drpClass.SelectedItem.Text.ToString() + "-" + drpSection.SelectedItem.Text.ToString() + (drpStream.SelectedItem.Text.ToString().ToUpper() == "OTHER" ? "" : (drpStream.SelectedItem.Text.ToString().ToUpper() == "NA") ? "" : "-" + drpStream.SelectedItem.Text.ToString());
    //        }
    //        else
    //        {
    //            Label1.Text = drpMonth1.SelectedItem.ToString() + " Month Student's Attendence Report " + "| Class :" + drpClass.SelectedItem.Text.ToString() + "-" + drpSection.SelectedItem.Text.ToString() + (drpStream.SelectedItem.Text.ToString().ToUpper() == "OTHER" ? "" : (drpStream.SelectedItem.Text.ToString().ToUpper() == "NA") ? "" : "-" + drpStream.SelectedItem.Text.ToString());
    //        }
    //        adddayofmonth();
    //        name(branchid);
    //    }
    //    else
    //    {
    //        abc.Visible = false;
    //    }
    //}
    //public void adddayofmonth()
    //{
    //    string year;
    //    string[] str = Session["SessionName"].ToString().Split('-');
    //    int monthno = Convert.ToInt32(drpMonth.SelectedValue.Split('-')[0]);
    //    year = drpMonth.SelectedItem.Text.Split(' ')[1];
    //    //if (monthno <= 9)
    //    //{           
    //    //    year = str[0].ToString();
    //    //}
    //    //else
    //    //{
    //    //    year = str[1].ToString();
    //    //}
    //    string date1;
    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //        date1 = year + "-" + monthno + "-" + "01";
    //    }
    //    else
    //    {
    //        date1 = year + "-" + monthno + "-" + "01";
    //    }
    //    sql = "WITH months(DATENumber) AS(SELECT 0 UNION ALL SELECT DATENumber+1 FROM months WHERE DATENumber < datediff(day, '" + date1 + "', dateadd(month, 1, '" + date1 + "'))-1)";
    //    sql = sql + " Select Convert(int,DATENAME(dd,dateadd(dd,DATENumber,'" + date1 + "'))) as dayno,Left((DATENAME(DW,dateadd(DD,DATENumber,'" + date1 + "'))),3) as dayname FROM months order by dayno Asc";
    //    SqlDataAdapter da = new SqlDataAdapter(sql, con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        if (GridView1.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                GridView1.HeaderRow.Cells[i + 3].Text = dt.Rows[i][0].ToString() + " " + Environment.NewLine + dt.Rows[i][1].ToString();
    //            }
    //        }
    //    }
    //}
    //public void name(string branchid)
    //{
    //    int jj = 0;
    //    int jj1 = 0;
    //    int jj2 = 0;
    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        jj = 0;
    //        jj1 = 0;
    //        jj2 = 0;
    //        Label lblsrno = (Label)GridView1.Rows[i].FindControl("Label2");
    //        string month = "", classname = "", sectioname = "";
    //        string year = drpMonth.SelectedItem.Text.Split(' ')[1];

    //        if (RadioButtonList1.SelectedIndex == 0)
    //        {
    //            month = drpMonth.SelectedItem.Text.Split(' ')[0];
    //            classname = drpClass.SelectedItem.ToString();
    //            sectioname = drpSection.SelectedItem.ToString();
    //        }
    //        else
    //        {
    //            month = drpMonth1.SelectedItem.Text.Split(' ')[0];
    //            if (Grd.Rows.Count > 0)
    //            {
    //                Label lblclass = (Label)Grd.Rows[0].FindControl("Label5");
    //                Label lblsection = (Label)Grd.Rows[0].FindControl("Label6");
    //                classname = lblclass.Text;
    //                sectioname = lblsection.Text;
    //            }
    //        }
    //        for (int j = 3; j < GridView1.Columns.Count - 3; j++)
    //        {
    //            string[] str = GridView1.HeaderRow.Cells[j].Text.Split(' ');
    //            string text = addvalue(year, month, classname, sectioname, branchid, lblsrno.Text, str[0].ToString(), "lbl" + (j - 2), i, j);
    //            if (text != "X")
    //            {
    //                if (text == "P" || text == "LC" || text == "LT")
    //                {
    //                    GridView1.Rows[i].Cells[j].CssClass = "vd_bg-green vd_white";
    //                    //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.White;
    //                    jj += 1;
    //                }
    //                else if (text == "A")
    //                {
    //                    GridView1.Rows[i].Cells[j].CssClass = "vd_bg-red vd_white";
    //                    //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.White;
    //                    jj1 += 1;
    //                }
    //                else if (text == "L")
    //                {
    //                    GridView1.Rows[i].Cells[j].CssClass = "vd_bg-yellow vd_white";
    //                    //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.White;
    //                    jj1 += 1;
    //                }
    //                jj2 = jj + jj1;

    //            }
    //            else
    //            {
    //                GridView1.Rows[i].Cells[j].CssClass = "vd_bg-yellow-l vd_black";
    //                //GridView1.Rows[i].Cells[j].ForeColor = System.Drawing.Color.Black;
    //            }
    //        }
    //        Label lblTotalPresent = (Label)GridView1.Rows[i].FindControl("lblTotalPresent");
    //        lblTotalPresent.Text = jj.ToString();
    //        Label lblTotalAbsent = (Label)GridView1.Rows[i].FindControl("lblTotalAbsent");
    //        lblTotalAbsent.Text = jj1.ToString();
    //        Label lblTotalWorkingDays = (Label)GridView1.Rows[i].FindControl("lblTotalWorkingDays");
    //        lblTotalWorkingDays.Text = jj2.ToString();
    //    }
    //}
    //public string addvalue(string year, string month, string classname, string section, string branchid, string lblsrno, string attendencedate, string lblday, int i, int j)
    //{
    //    Label lbl = (Label)GridView1.Rows[i].FindControl(lblday);
    //    sql = "Select ar.AttendanceValue from AttendanceDetailsDateWise ar left join StudentOfficialDetails SO on So.StEnRCode=ar.StEnRCode and so.sessionname=ar.sessionname";
    //    sql = sql + " where DATEPART(yyyy,ar.AttendanceDate)='" + year + "' and ar.AttendanceMonth='" + month + "' and ar.ClassName='" + classname + "'";
    //    sql = sql + " and ar.SrNo='" + lblsrno + "' and ar.SessionName='" + Session["SessionName"].ToString() + "'";
    //    sql = sql + " and DATEPART(dd,ar.AttendanceDate)='" + attendencedate + "'";
    //    if (oo.ReturnTag(sql, "AttendanceValue") != "")
    //    {
    //        lbl.Text = oo.ReturnTag(sql, "AttendanceValue").Replace(" ", "");
    //    }
    //    else
    //    {
    //        lbl.Text = "X";
    //    }
    //    return lbl.Text;
    //}
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "MonthwiseStudentAttendenceReport.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel("MonthwiseStudentAttendenceReport.xls", GridView1);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
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

        sql = "Select  SectionName,Card,Medium,ClassName,BranchName,convert(nvarchar,DateOfAdmiission,106) as DateOfAdmiission,";
        sql = sql + "  FatherName,MotherName,Name as StudentName,StEnRCode,srno,";
        sql = sql + "  case  when TransportRequired='Yes' then 'Yes' else 'No' end as TransportRequired ";
        sql = sql + "  from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where " + DrpEnter.SelectedValue.ToString() + "=" + "'" + TxtEnter.Text.Trim() + "'";
        sql = sql + "  and Withdrwal is null and (Promotion is null or Promotion<>'Cancelled')";


        Grd.DataSource = oo.GridFill(sql);
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
            //Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Sorry, No record(s) found!", "A");

        }
    }
    protected void drpMonth1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadgrid();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {        
        abc.Visible = false;
        if (RadioButtonList1.SelectedIndex == 0)
        {
            table1.Visible = false;
            table2.Visible = false;
            table3.Visible = false;
            //table4.Visible = true;
            Grd.DataSource = null;
            Grd.DataBind();
            TxtEnter.Text = "";
            drpMonth1.SelectedIndex = 0;
        }
        else
        {
            table1.Visible = true;
            //table4.Visible = false;
        }
    }
    public override void Dispose()
    {
        con.Dispose();
        oo.Dispose();
    }
}