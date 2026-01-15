using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class common_StudenceAttendanceDayWiseReport : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
   
        con = oo.dbGet_connection();
        BLL.BLLInstance.LoadHeader("Report", header);
  Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
  
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        if (!IsPostBack)
        {

            loadClass(drpClass);
            oo.fillSelectvalue(drpBranch, "<--Select-->", "-1");
            oo.fillSelectvalue(drpSection, "<--Select-->", "-1");
  

            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);


            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);

            ImageButton1.Visible = false;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
            ImageButton4.Visible = false;


            IsAttendanceDateBeetweenSessionDate();

        }
    }

    protected void IsAttendanceDateBeetweenSessionDate()
    {
        string fromdate = FromYY.SelectedItem.Text + " " + FromMM.SelectedItem.Text + " " + FromDD.SelectedItem.Text;
        string todate = ToYY.SelectedItem.Text + " " + ToMM.SelectedItem.Text + " " + ToDD.SelectedItem.Text;
        sql = "Select '1' flag from SessionMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and ";
        sql +=  " FromDate <='" + fromdate + "' and ToDate>='" + fromdate + "' and";
        sql +=  " FromDate <= '" + todate + "' and ToDate>= '" + todate + "'";
        string value = BAL.objBal.ReturnTag(sql, "flag");
        if (value == "1")
        {
            LinkButton5.Visible = true;

        }
        else
        {
            LinkButton5.Visible = false;
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Selected FromDate and ToDate must between session date!", "A");
        }
    }

    private void loadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadClass(drpclass, Session["SessionName"].ToString());
        }
        else
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString().Trim()));

            drpclass.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetClassTeacherClassName_Proc", param);
            drpclass.DataTextField = "ClassName";
            drpclass.DataValueField = "Id";
            drpclass.DataBind();
            drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    private void loadSection(DropDownList drpsection, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadSection(drpsection, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select SectionName,sm.Id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName and sm.BranchCode=t1.BranchCode";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "'";
            sql +=  " and t1.Classid=" + drpclass.SelectedValue.ToString() + " and T1.BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, "<--Select-->");
        }
    }

    private void loadBranch(DropDownList drpbranch, DropDownList drpclass)
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            BLL.BLLInstance.loadBranch(drpbranch, Session["SessionName"].ToString(), drpclass.SelectedValue.ToString());
        }
        else
        {
            sql = "Select BranchName,bm.Id from ClassTeacherMaster T1";
            sql +=  "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName and bm.BranchCode=t1.BranchCode";
            sql +=  "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql +=  "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "' and T1.BranchCode=" + Session["BranchCode"] + "";
            BAL.objBal.FillDropDown_withValue(sql, drpbranch, "BranchName", "Id");
            drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch(drpBranch,drpClass);
        loadSection(drpSection,drpClass);
    }

    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
        IsAttendanceDateBeetweenSessionDate();
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
        IsAttendanceDateBeetweenSessionDate();
    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
        IsAttendanceDateBeetweenSessionDate();
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
        IsAttendanceDateBeetweenSessionDate();
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        string todate = "", fromdate = "";
        todate = ToYY.SelectedItem.ToString() + "/" + ToMM.SelectedItem.ToString() + "/" + ToDD.SelectedItem.ToString();
        fromdate = FromYY.SelectedItem.ToString() + "/" + FromMM.SelectedItem.ToString() + "/" + FromDD.SelectedItem.ToString();

        if (drpClass.SelectedItem.ToString() == "<--Select>" || drpSection.SelectedItem.ToString() == "<--Select-->")
        {
            //oo.MessageBox("Please Select Condition", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Please Select Condition", "A");

        }
        else
        {

            sql = "select  SG.SectionName,SG.ClassName,ad.AttendanceValue as AttendanceValue,";
            sql +=  "  SG.FatherName,  SG.MotherName,SG.Name,sg.StEnRCode as StEnRCode,sg.srno  as srno ,ad.CategoryWise as CategoryWise ,";
            sql +=  "  convert(nvarchar,ad.AttendanceDate,106) as AttendanceDate from AllStudentRecord_UDF(''," + Session["BranchCode"].ToString() + ") SG";
            sql +=  "  inner join AttendanceDetailsDateWise ad on SG.SrNo=ad.SrNo  and SG.SessionName=ad.SessionName   and SG.BranchCode=ad.BranchCode ";
            sql +=  "  where ad.ClassName='" + drpClass.SelectedItem.ToString() + "' and  SG.SectionName='" + drpSection.SelectedItem.ToString() + "' and (SG.BranchId='" + drpBranch.SelectedValue.ToString() + "' or SG.BranchId is null)  and ";
            sql +=  "  ad.AttendanceDate between '" + fromdate + "'   and   '" + fromdate + "'    and ad.CategoryWise='Date Wise' and ";
            sql +=  "  (SG.Promotion is null or SG.Promotion<>'Cancelled') ";
            sql +=  "  Order by SG.Name Asc,AttendanceValue desc";

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
            ImageButton1.Visible = true;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            ImageButton4.Visible = true;

            if (GridView1.Rows.Count == 0)
            {
                //oo.MessageBox("Sorry, No Record(s) found!", this.Page);
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record(s) found!", "A");
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                ImageButton4.Visible = false;
                Label14.Text = "";
            }
            else
            {
                Label14.Text = "Student's Attendance Report | "+ DateTime.Parse(fromdate).ToString("dd MMM yyyy") + "| Class: " + drpClass.SelectedItem.Text.ToString() + " " + (drpBranch.SelectedItem.Text.ToString().ToUpper() == "OTHER" ? "" : (drpBranch.SelectedItem.Text.ToString().ToUpper() == "NA") ? "" : " " + drpBranch.SelectedItem.Text.ToString()) +" ("+ drpSection.SelectedItem.Text.ToString()+")";
            
            }
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportToWord(Response, "StudenceAttendanceDayWiseReport.doc", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportToExcel("StudenceAttendanceDayWiseReport.xls", GridView1);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        if (GridView1.Rows.Count > 0)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        IsAttendanceDateBeetweenSessionDate();
    }

    protected void ToDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        IsAttendanceDateBeetweenSessionDate();
    }
}