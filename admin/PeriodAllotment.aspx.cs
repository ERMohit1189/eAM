using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_AllotPeriod : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    string PeriodName;
                                                
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            div1.Visible = false;
        }
    }

    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
        loadmedium();
        DisplayRecord();
        foreach (ListItem li in RadioButtonList1.Items)
        {
            if (li.Selected)
            {
                li.Selected = false;
                GridView1.Visible = false;
            }
        }
    }

    public void displayEmpInfo()
    {
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+egd.EMiddleName+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and egd.SessionName='" + Session["SessionName"].ToString() + "'";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            div1.Visible = true;
        }
        else
        {
            oo.MessageBoxforUpdatePanel("Sorry, No Record found!", lnkShow);
            div1.Visible = false;
        }
    }

    public void displayperiod()
    {
        sql = @"Select PeriodName,Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as PeriodTime
        from PeriodMaster where SessionName='" + Session["SessionName"].ToString() + "' and Branchcode=" + Session["BranchCode"].ToString() + " and type='" + RadioButtonList1.SelectedValue.ToString() + "' order by Id";
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            GridView2.Visible = true;
            GenrateTimeTable();
        }
        
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayperiod();
        //loadclass();
        //checkperiods();      
        GridView1.Visible = true;
    }

    public void loadclass()
    {
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                DropDownList drp2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                DropDownList drp3 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList3");
                DropDownList drp4 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList4");
                DropDownList drp5 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList5");
                DropDownList drp6 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList6");
                DropDownList drp7 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList7");
                DropDownList drp8 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList8");
                DropDownList drp9 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList9");
                DropDownList drp10 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList10");
                DropDownList drp11 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList11");
                DropDownList drp12 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList12");
                DropDownList drp13 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList13");
                DropDownList drp14 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList14");
                DropDownList drp15 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList15");
                DropDownList drp16 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList16");
                DropDownList drp17 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList17");
                DropDownList drp18 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList18");

                if (drpMedium.SelectedValue=="Both")
                {
                    sql = "select Distinct cm.ClassName,cm.id as CId, cm.CidOrder from SubjectClassteacherMaster sctm ";
                    sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                    sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                    sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                    sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                }
                else
                {
                    sql = "select Distinct cm.ClassName,cm.id as CId, cm.CidOrder from SubjectClassteacherMaster sctm ";
                    sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                    sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                    sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                    sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                    sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null and sctm.Medium='" + drpMedium.SelectedItem.ToString() + "' Order by CidOrder";
                }
                oo.FillDropDown_withValue(sql, drp7, "ClassName", "CId");
                drp1.Items.Insert(0, "None");
                drp13.Items.Insert(0, "None");
                drp7.Items.Insert(0, "Select");
               
                oo.FillDropDown_withValue(sql, drp8, "ClassName", "CId");
                drp2.Items.Insert(0, "None");
                drp14.Items.Insert(0, "None");
                drp8.Items.Insert(0, "Select");
               
                oo.FillDropDown_withValue(sql, drp9, "ClassName", "CId");
                drp3.Items.Insert(0, "None");
                drp15.Items.Insert(0, "None");
                drp9.Items.Insert(0, "Select");
               
                oo.FillDropDown_withValue(sql, drp10, "ClassName", "CId");
                drp4.Items.Insert(0, "None");
                drp16.Items.Insert(0, "None");
                drp10.Items.Insert(0, "Select");
               
                oo.FillDropDown_withValue(sql, drp11, "ClassName", "CId");
                drp5.Items.Insert(0, "None");
                drp17.Items.Insert(0, "None");
                drp11.Items.Insert(0, "Select");
               
                oo.FillDropDown_withValue(sql, drp12, "ClassName", "CId");
                drp6.Items.Insert(0, "None");
                drp18.Items.Insert(0, "None");
                drp12.Items.Insert(0, "Select");

            }
        }
    }

    public void loadmedium()
    {
                sql = "select Distinct sctm.Medium from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null";
                oo.FillDropDown(sql,drpMedium,"Medium");
                drpMedium.Items.Add("Both");
    }

    protected void DropDownList13_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {


            GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
            DropDownList drp1 = (DropDownList)currentrow.FindControl("DropDownList1");
            DropDownList drp13 = (DropDownList)currentrow.FindControl("DropDownList13");
            DropDownList drp7 = (DropDownList)currentrow.FindControl("DropDownList7");

            sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
            sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
            sql = sql + " where scm.SectionName='"+drp13.SelectedItem.ToString()+"' and sm.classId='" + drp7.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

            oo.FillDropDown_withValue(sql, drp1, "SubjectName", "Id");

            drp1.Items.Add("None");


        }
    }

    protected void DropDownList14_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {


            GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
            DropDownList drp2 = (DropDownList)currentrow.FindControl("DropDownList2");
            DropDownList drp14 = (DropDownList)currentrow.FindControl("DropDownList14");
            DropDownList drp8 = (DropDownList)currentrow.FindControl("DropDownList8");

            sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
            sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
            sql = sql + " where scm.SectionName='" + drp14.SelectedItem.ToString() + "' and sm.classId='" + drp8.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

            oo.FillDropDown_withValue(sql, drp2, "SubjectName", "Id");

            drp2.Items.Add("None");



        }
    }

    protected void DropDownList15_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {


            GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
            DropDownList drp3 = (DropDownList)currentrow.FindControl("DropDownList3");
            DropDownList drp9 = (DropDownList)currentrow.FindControl("DropDownList9");
            DropDownList drp15 = (DropDownList)currentrow.FindControl("DropDownList15");

            sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
            sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
            sql = sql + " where scm.SectionName='" + drp15.SelectedItem.ToString() + "' and sm.classId='" + drp9.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

            oo.FillDropDown_withValue(sql, drp3, "SubjectName", "Id");

            drp3.Items.Add("None");



        }
    }

    protected void DropDownList16_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {


            GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
            DropDownList drp4 = (DropDownList)currentrow.FindControl("DropDownList4");
            DropDownList drp10 = (DropDownList)currentrow.FindControl("DropDownList10");
            DropDownList drp16 = (DropDownList)currentrow.FindControl("DropDownList16");

            sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
            sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
            sql = sql + " where scm.SectionName='" + drp16.SelectedItem.ToString() + "' and sm.classId='" + drp10.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

            oo.FillDropDown_withValue(sql, drp4, "SubjectName", "Id");

            drp4.Items.Add("None");



        }
    }

    protected void DropDownList17_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {


            GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
            DropDownList drp5 = (DropDownList)currentrow.FindControl("DropDownList5");
            DropDownList drp17 = (DropDownList)currentrow.FindControl("DropDownList17");
            DropDownList drp11 = (DropDownList)currentrow.FindControl("DropDownList11");

            sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
            sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
            sql = sql + " where scm.SectionName='" + drp17.SelectedItem.ToString() + "' and sm.classId='" + drp11.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

            oo.FillDropDown_withValue(sql, drp5, "SubjectName", "Id");

            drp5.Items.Add("None");



        }
    }

    protected void DropDownList18_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {


            GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
            DropDownList drp6 = (DropDownList)currentrow.FindControl("DropDownList6");
            DropDownList drp18 = (DropDownList)currentrow.FindControl("DropDownList18");
            DropDownList drp12 = (DropDownList)currentrow.FindControl("DropDownList12");

            sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
            sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
            sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
            sql = sql + " where scm.SectionName='" + drp18.SelectedItem.ToString() + "' and sm.classId='" + drp12.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

            oo.FillDropDown_withValue(sql, drp6, "SubjectName", "Id");

            drp6.Items.Add("None");



        }
    }
  
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {

         
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
                DropDownList drp13 = (DropDownList)currentrow.FindControl("DropDownList13");
                DropDownList drp7 = (DropDownList)currentrow.FindControl("DropDownList7");

                sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where sm.classId='" + drp7.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                oo.FillDropDownWithOutSelect(sql, drp13, "SectionName");

                drp13.Items.Insert(0,"Select");
           
            
        }
    }

    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {

           
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
       
                DropDownList drp14 = (DropDownList)currentrow.FindControl("DropDownList14");
                DropDownList drp8 = (DropDownList)currentrow.FindControl("DropDownList8");

                sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where sm.classId='" + drp8.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                oo.FillDropDownWithOutSelect(sql, drp14, "SectionName");

                drp14.Items.Insert(0, "Select");

          
        }
    }

    protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
               
                DropDownList drp9 = (DropDownList)currentrow.FindControl("DropDownList9");
                DropDownList drp15 = (DropDownList)currentrow.FindControl("DropDownList15");

                sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where sm.classId='" + drp9.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                oo.FillDropDownWithOutSelect(sql, drp15, "SectionName");

                drp15.Items.Insert(0, "Select");
            
        }
    }

    protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
           
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
              
                DropDownList drp10 = (DropDownList)currentrow.FindControl("DropDownList10");
                DropDownList drp16 = (DropDownList)currentrow.FindControl("DropDownList16");

                sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where sm.classId='" + drp10.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                oo.FillDropDownWithOutSelect(sql, drp16, "SectionName");

                drp16.Items.Insert(0, "Select");
            
        }
    }

    protected void DropDownList11_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
                DropDownList drp5 = (DropDownList)currentrow.FindControl("DropDownList5");
                DropDownList drp11 = (DropDownList)currentrow.FindControl("DropDownList11");
                DropDownList drp17 = (DropDownList)currentrow.FindControl("DropDownList17");

                sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where sm.classId='" + drp11.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                oo.FillDropDownWithOutSelect(sql, drp17, "SectionName");

                drp17.Items.Insert(0, "Select");            
        }
    }

    protected void DropDownList12_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;

                DropDownList drp12 = (DropDownList)currentrow.FindControl("DropDownList12");
                DropDownList drp18 = (DropDownList)currentrow.FindControl("DropDownList18");

                sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " where sm.classId='" + drp12.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
                oo.FillDropDownWithOutSelect(sql, drp18, "SectionName");

                drp18.Items.Insert(0, "Select");
            
        }
    }

    public void deletedata()
    {
        sql = "Delete from PeriodAllocationMaster where " + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "and Type='" + RadioButtonList1.SelectedItem.ToString() + "'";
        cmd = new SqlCommand(sql, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        sql = "Delete from PeriodAllocationMasterforRecord where " + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and Convert(nvarchar(11),RecordDate,111)=Convert(nvarchar(11),GetDate(),111)";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "and Type='" + RadioButtonList1.SelectedItem.ToString() + "'";
        cmd = new SqlCommand(sql, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

    }

    protected void ChkAll_OnCheckedChanged(object sender, EventArgs e)
    {
       
        CheckBox ChkAll=(CheckBox)sender;
        EnableDisableDropDownForAll();
        if (GridView1.Rows.Count > 0)
        {
            if (ChkAll.Checked)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chk");
                    if (chk.Checked == false)
                    {
                        chk.Checked = true;
                    }
                    if (GridView1.Rows.Count > 3)
                    {
                        oo.MessageBoxforUpdatePanel("Already alloted continuasly three period in day,Do you want to allot more period!", ChkAll);
                    }
                }
            }
            else
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chk");
                    if (chk.Checked == true)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
    }

    public void EnableDisableDropDownForAll()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)GridView1.HeaderRow.FindControl("ChkAll");
                DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                DropDownList drp2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                DropDownList drp3 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList3");
                DropDownList drp4 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList4");
                DropDownList drp5 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList5");
                DropDownList drp6 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList6");
                DropDownList drp7 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList7");
                DropDownList drp8 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList8");
                DropDownList drp9 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList9");
                DropDownList drp10 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList10");
                DropDownList drp11 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList11");
                DropDownList drp12 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList12");
                DropDownList drp13 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList13");
                DropDownList drp14 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList14");
                DropDownList drp15 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList15");
                DropDownList drp16 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList16");
                DropDownList drp17 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList17");
                DropDownList drp18 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList18");
                if (chkall.Checked)
                {
                    drp1.Enabled = true; drp2.Enabled = true; drp3.Enabled = true; drp4.Enabled = true; drp5.Enabled = true;
                    drp6.Enabled = true; drp7.Enabled = true; drp8.Enabled = true; drp9.Enabled = true; drp10.Enabled = true;
                    drp11.Enabled = true; drp12.Enabled = true; drp13.Enabled = true; drp14.Enabled = true; drp15.Enabled = true;
                    drp16.Enabled = true; drp17.Enabled = true; drp18.Enabled = true;  
                }
                else
                {
                   
                    drp1.Enabled = false; drp2.Enabled = false; drp3.Enabled = false; drp4.Enabled = false; drp5.Enabled = false;
                    drp6.Enabled = false; drp7.Enabled = false; drp8.Enabled = false; drp9.Enabled = false; drp10.Enabled = false;
                    drp11.Enabled = false; drp12.Enabled = false; drp13.Enabled = false; drp14.Enabled = false; drp15.Enabled = false;
                    drp16.Enabled = false; drp17.Enabled = false; drp18.Enabled = false;
                }
            }


        }
    }

    protected void Chk_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)sender;
        if (ch.Checked)
        {          
            if (GridView1.Rows.Count > 0)
            {
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
                int rowindex = currentrow.RowIndex;
                Session["indexno"] = rowindex;
                enableDopdown();
                if (rowindex == 0)
                {
                    try
                    {
                        int j = 0;
                        for (int i = 1; i <= rowindex + 3; i++)
                        {
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");

                            if (chk.Checked)
                            {
                                j = j + 1;
                            }

                        }
                        if (j > 2)
                        {
                            Panel2_ModalPopupExtender.Show();
                        }
                    }
                    catch
                    {
                    }

                }
                else if (rowindex == 1)
                {
                    if (GridView1.Rows.Count > 3)
                    {
                        try
                        {
                            int j = 0;
                            for (int i = 0; i <= rowindex + 2; i++)
                            {
                                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                                if (chk.Checked)
                                {

                                    j = j + 1;
                                }

                            }
                            if (j > 3)
                            {
                                Panel2_ModalPopupExtender.Show();
                            }
                        }
                        catch
                        {
                        }
                    }

                }
                else if (rowindex == GridView1.Rows.Count - 1)
                {
                    try
                    {
                        int j = 0;
                        for (int i = rowindex - 3; i <= rowindex; i++)
                        {
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                            if (chk.Checked)
                            {

                                j = j + 1;
                            }
                        }
                        if (j > 3)
                        {
                            Panel2_ModalPopupExtender.Show();
                        }
                    }
                    catch
                    {
                    }
                }

                else
                {
                    if (rowindex == 2)
                    {
                        int j = 0;
                        for (int i = rowindex - 2; i <= rowindex + 1; i++)
                        {
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                            if (chk.Checked)
                            {

                                j = j + 1;
                            }
                        }

                        if (j > 3)
                        {
                            Panel2_ModalPopupExtender.Show();
                        }

                    }
                    else
                    {
                        int j = 0;
                        for (int i = rowindex - 3; i <= rowindex; i++)
                        {
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                            if (chk.Checked)
                            {

                                j = j + 1;
                            }
                        }

                        if (j > 3)
                        {
                            Panel2_ModalPopupExtender.Show();
                        }
                        else
                        {
                            j = 0;
                            for (int i = rowindex - 2; i <= rowindex + 1; i++)
                            {
                                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                                if (chk.Checked)
                                {

                                    j = j + 1;
                                }
                            }

                            if (j > 3)
                            {
                                Panel2_ModalPopupExtender.Show();
                            }
                        }
                    }
                }

            }
        }
        else
        {
            if (GridView1.Rows.Count > 0)
            {
                GridViewRow currentrow = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
                int rowindex = currentrow.RowIndex;
                Session["indexno"] = rowindex;
                DisableDopdown();
            }
        }
    }

    public void enableDopdown()
    {
        if (Grd.Rows.Count > 0)
        {
            int indexno = Convert.ToInt32(Session["indexno"].ToString());
            CheckBox chk = (CheckBox)GridView1.Rows[indexno].FindControl("Chk");
            DropDownList drp1 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList1");
            DropDownList drp2 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList2");
            DropDownList drp3 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList3");
            DropDownList drp4 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList4");
            DropDownList drp5 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList5");
            DropDownList drp6 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList6");
            DropDownList drp7 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList7");
            DropDownList drp8 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList8");
            DropDownList drp9 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList9");
            DropDownList drp10 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList10");
            DropDownList drp11 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList11");
            DropDownList drp12 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList12");
            DropDownList drp13 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList13");
            DropDownList drp14 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList14");
            DropDownList drp15 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList15");
            DropDownList drp16 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList16");
            DropDownList drp17 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList17");
            DropDownList drp18 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList18");
            drp1.Enabled = true; drp2.Enabled = true; drp3.Enabled = true; drp4.Enabled = true; drp5.Enabled = true;
            drp6.Enabled = true; drp7.Enabled = true; drp8.Enabled = true; drp9.Enabled = true; drp10.Enabled = true;
            drp11.Enabled = true; drp12.Enabled = true; drp13.Enabled = true; drp14.Enabled = true; drp15.Enabled = true;
            drp16.Enabled = true; drp17.Enabled = true; drp18.Enabled = true;            
        }
    }

    public void DisableDopdown()
    {
        if (Grd.Rows.Count > 0)
        {
            int indexno = Convert.ToInt32(Session["indexno"].ToString());
           
            CheckBox chk = (CheckBox)GridView1.Rows[indexno].FindControl("Chk");
          
                DropDownList drp1 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList1");
                DropDownList drp2 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList2");
                DropDownList drp3 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList3");
                DropDownList drp4 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList4");
                DropDownList drp5 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList5");
                DropDownList drp6 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList6");
                DropDownList drp7 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList7");
                DropDownList drp8 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList8");
                DropDownList drp9 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList9");
                DropDownList drp10 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList10");
                DropDownList drp11 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList11");
                DropDownList drp12 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList12");
                DropDownList drp13 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList13");
                DropDownList drp14 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList14");
                DropDownList drp15 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList15");
                DropDownList drp16 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList16");
                DropDownList drp17 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList17");
                DropDownList drp18 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList18");
                drp1.Enabled = false; drp2.Enabled = false; drp3.Enabled = false; drp4.Enabled = false; drp5.Enabled = false;
                drp6.Enabled = false; drp7.Enabled = false; drp8.Enabled = false; drp9.Enabled = false; drp10.Enabled = false;
                drp11.Enabled = false; drp12.Enabled = false; drp13.Enabled = false; drp14.Enabled = false; drp15.Enabled = false;
                drp16.Enabled = false; drp17.Enabled = false; drp18.Enabled = false; 

            
        }
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        int indexno = Convert.ToInt32(Session["indexno"].ToString());
        CheckBox chk = (CheckBox)GridView1.Rows[indexno].FindControl("Chk");
        DropDownList drp1 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList1");
        DropDownList drp2 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList2");
        DropDownList drp3 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList3");
        DropDownList drp4 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList4");
        DropDownList drp5 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList5");
        DropDownList drp6 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList6");
        DropDownList drp7 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList7");
        DropDownList drp8 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList8");
        DropDownList drp9 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList9");
        DropDownList drp10 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList10");
        DropDownList drp11 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList11");
        DropDownList drp12 = (DropDownList)GridView1.Rows[indexno].FindControl("DropDownList12");
        drp1.Enabled = false; drp2.Enabled = false; drp3.Enabled = false; drp4.Enabled = false; drp5.Enabled = false;
        drp6.Enabled = false; drp7.Enabled = false; drp8.Enabled = false; drp9.Enabled = false; drp10.Enabled = false;
        drp11.Enabled = false; drp12.Enabled = false;
        chk.Checked = false;
    }

    public void savedata(string EmpId,string Ecode,string lblPeriodName, string periodtime,string drpClassId,string SectionName,string drpSubjectId,string Day)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "PeriodAllocationMasterProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        cmd.Parameters.AddWithValue("@ECode", Ecode);
        cmd.Parameters.AddWithValue("@Type", RadioButtonList1.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@PeriodName", lblPeriodName);
        cmd.Parameters.AddWithValue("@PeriodTime", periodtime);
        cmd.Parameters.AddWithValue("@ClassId", drpClassId);
        cmd.Parameters.AddWithValue("@SectionName", SectionName);
        cmd.Parameters.AddWithValue("@SubjectId", drpSubjectId);
        cmd.Parameters.AddWithValue("@Day", Day);
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString().Trim());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        cmd = new SqlCommand();
        cmd.CommandText = "PeriodAllocationMasterforRecordProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@EmpId", EmpId);
        cmd.Parameters.AddWithValue("@ECode", Ecode);
        cmd.Parameters.AddWithValue("@Type", RadioButtonList1.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@PeriodName", lblPeriodName);
        cmd.Parameters.AddWithValue("@PeriodTime", periodtime);
        cmd.Parameters.AddWithValue("@ClassId", drpClassId);
        cmd.Parameters.AddWithValue("@SectionName", SectionName);
        cmd.Parameters.AddWithValue("@SubjectId", drpSubjectId);
        cmd.Parameters.AddWithValue("@Day", Day);
        cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString().Trim());
        cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString().Trim());
        cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString().Trim());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    //protected void lnkSubmit_Click(object sender, EventArgs e)
    //{
    //    if (Grd.Rows.Count > 0)
    //    {
    //        if (GridView1.Rows.Count > 0)
    //        {
    //            deletedata();
    //            for (int i = 0; i < GridView1.Rows.Count; i++)
    //            {
    //                Label EmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
    //                Label Ecode = (Label)Grd.Rows[0].FindControl("lblEcode");
    //                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chk");
    //                Label lblPeriodName = (Label)GridView1.Rows[i].FindControl("lblPeriodName");

    //                if (chk.Checked)
    //                {
    //                    DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
    //                    DropDownList drp2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
    //                    DropDownList drp3 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList3");
    //                    DropDownList drp4 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList4");
    //                    DropDownList drp5 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList5");
    //                    DropDownList drp6 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList6");
    //                    DropDownList drp7 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList7");
    //                    DropDownList drp8 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList8");
    //                    DropDownList drp9 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList9");
    //                    DropDownList drp10 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList10");
    //                    DropDownList drp11 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList11");
    //                    DropDownList drp12 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList12");
    //                    DropDownList drp13 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList13");
    //                    DropDownList drp14 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList14");
    //                    DropDownList drp15 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList15");
    //                    DropDownList drp16 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList16");
    //                    DropDownList drp17 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList17");
    //                    DropDownList drp18 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList18");

    //                    if (drp7.SelectedIndex != 0 && drp13.SelectedIndex != 0)
    //                        {
    //                            if (drp1.SelectedIndex != drp1.Items.Count)
    //                            {
    //                                string SectionName = drp13.SelectedItem.ToString();
    //                                savedata(EmpId.Text.Trim(),Ecode.Text.Trim(),lblPeriodName.Text.Trim(),drp7.SelectedValue.ToString(),SectionName,drp1.SelectedValue.Trim(),GridView1.Columns[2].HeaderText.ToString().Substring(0,1));             
    //                            }
                               

    //                        }
    //                    if (drp8.SelectedIndex != 0 && drp14.SelectedIndex != 0)
    //                        {
    //                            if (drp2.SelectedIndex != drp1.Items.Count)
    //                            {

    //                                string SectionName = drp14.SelectedItem.ToString();
    //                                savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), drp8.SelectedValue.ToString(), SectionName, drp2.SelectedValue.Trim(), GridView1.Columns[3].HeaderText.ToString().Substring(0, 1));             
                                   

    //                            }


    //                        }
    //                    if (drp9.SelectedIndex != 0 && drp15.SelectedIndex != 0)
    //                        {
    //                            if (drp3.SelectedIndex != drp1.Items.Count)
    //                            {
    //                                string SectionName = drp15.SelectedItem.ToString();
    //                                savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), drp9.SelectedValue.ToString(), SectionName, drp3.SelectedValue.Trim(), GridView1.Columns[4].HeaderText.ToString().Substring(0, 1));             
                                   

    //                            }


    //                        }
    //                    if (drp10.SelectedIndex != 0 && drp16.SelectedIndex != 0)
    //                        {
    //                            if (drp4.SelectedIndex != drp1.Items.Count)
    //                            {
    //                                string SectionName = drp16.SelectedItem.ToString();
    //                                savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), drp10.SelectedValue.ToString(), SectionName, drp4.SelectedValue.Trim(), GridView1.Columns[5].HeaderText.ToString().Substring(0, 2));             
                                   

    //                            }


    //                        }
    //                        if (drp11.SelectedIndex != 0 && drp17.SelectedIndex != 0)
    //                        {
    //                            if (drp5.SelectedIndex != drp1.Items.Count)
    //                            {
    //                                string SectionName = drp17.SelectedItem.ToString();
    //                                savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), drp11.SelectedValue.ToString(), SectionName, drp5.SelectedValue.Trim(), GridView1.Columns[6].HeaderText.ToString().Substring(0, 1));             
                                   

    //                            }


    //                        }
    //                        if (drp12.SelectedIndex != 0 && drp18.SelectedIndex != 0)
    //                        {
    //                            if (drp6.SelectedIndex != drp1.Items.Count)
    //                            {
    //                                string SectionName = drp18.SelectedItem.ToString();
    //                                savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), drp12.SelectedValue.ToString(), SectionName, drp6.SelectedValue.Trim(), GridView1.Columns[7].HeaderText.ToString().Substring(0, 1));             
                                   

    //                            }


    //                        }
                                                      
    //                }
                   
    //            }
    //            DisplayRecord();
    //            oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
    //        }
           
    //    }
       
    //}

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (Grd.Rows.Count > 0)
        {
            if (GridView2.Rows.Count > 0)
            {
                deletedata();
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    Label EmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
                    Label Ecode = (Label)Grd.Rows[0].FindControl("lblEcode");
                    Label lblPeriodName = (Label)GridView2.Rows[i].FindControl("lblPeriodName");
                    Label lblPeriodTime = (Label)GridView2.Rows[i].FindControl("lblPeriodTime");
                    Label Label2 = (Label)GridView2.Rows[i].FindControl("Label2");
                    Label Label5 = (Label)GridView2.Rows[i].FindControl("Label5");
                    Label Label6 = (Label)GridView2.Rows[i].FindControl("Label6");
                    Label Label7 = (Label)GridView2.Rows[i].FindControl("Label7");
                    Label Label8 = (Label)GridView2.Rows[i].FindControl("Label8");
                    Label Label9 = (Label)GridView2.Rows[i].FindControl("Label9");
                    Label Label10 = (Label)GridView2.Rows[i].FindControl("Label10");
                    Label Label11 = (Label)GridView2.Rows[i].FindControl("Label11");
                    Label Label12 = (Label)GridView2.Rows[i].FindControl("Label12");
                    Label Label13 = (Label)GridView2.Rows[i].FindControl("Label13");
                    Label Label14 = (Label)GridView2.Rows[i].FindControl("Label14");
                    Label Label15 = (Label)GridView2.Rows[i].FindControl("Label15");
                    Label Label16 = (Label)GridView2.Rows[i].FindControl("Label16");
                    Label Label17 = (Label)GridView2.Rows[i].FindControl("Label17");
                    Label Label18 = (Label)GridView2.Rows[i].FindControl("Label18");
                    Label Label19 = (Label)GridView2.Rows[i].FindControl("Label19");
                    Label Label20 = (Label)GridView2.Rows[i].FindControl("Label20");
                    Label Label21 = (Label)GridView2.Rows[i].FindControl("Label21");
                    Label Label22 = (Label)GridView2.Rows[i].FindControl("Label22");
                    Label Label23 = (Label)GridView2.Rows[i].FindControl("Label23");
                    Label Label24 = (Label)GridView2.Rows[i].FindControl("Label24");
                    Label Label25 = (Label)GridView2.Rows[i].FindControl("Label25");
                    Label Label26 = (Label)GridView2.Rows[i].FindControl("Label26");
                    Label Label27 = (Label)GridView2.Rows[i].FindControl("Label27");
                    Label Label28 = (Label)GridView2.Rows[i].FindControl("Label28");
                    Label Label29 = (Label)GridView2.Rows[i].FindControl("Label29");
                    Label Label30 = (Label)GridView2.Rows[i].FindControl("Label30");
                    Label Label31 = (Label)GridView2.Rows[i].FindControl("Label31");
                    Label Label32 = (Label)GridView2.Rows[i].FindControl("Label32");
                    Label Label33 = (Label)GridView2.Rows[i].FindControl("Label33");

                    savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), lblPeriodTime.Text.Trim(), Label22.Text, Label5.Text, Label28.Text, GridView2.Columns[1].HeaderText.ToString().Substring(0, 1));

                    savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), lblPeriodTime.Text.Trim(), Label23.Text, Label7.Text, Label29.Text, GridView2.Columns[2].HeaderText.ToString().Substring(0, 1));

                    savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), lblPeriodTime.Text.Trim(), Label24.Text, Label9.Text, Label30.Text, GridView2.Columns[3].HeaderText.ToString().Substring(0, 1));

                    savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), lblPeriodTime.Text.Trim(), Label25.Text, Label11.Text, Label31.Text, GridView2.Columns[4].HeaderText.ToString().Substring(0, 2));

                    savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), lblPeriodTime.Text.Trim(), Label26.Text, Label13.Text, Label32.Text, GridView2.Columns[5].HeaderText.ToString().Substring(0, 1));

                    savedata(EmpId.Text.Trim(), Ecode.Text.Trim(), lblPeriodName.Text.Trim(), lblPeriodTime.Text.Trim(), Label27.Text, Label15.Text, Label33.Text, GridView2.Columns[6].HeaderText.ToString().Substring(0, 1));
                      
                           
                }
            }
            DisplayRecord();
            oo.MessageBoxforUpdatePanel("Submitted successfully", lnkSubmit);
            drpMedium.SelectedIndex = 0;
            GridView2.Visible = false;
        }

    }

    public void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];
            for (int i = 0; i < row.Cells.Count-1; i++)
            {
                if (((Label)(row.Cells[i].Controls[1])).Text == ((Label)(previousRow.Cells[i].Controls[1])).Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
                }
            }
        }

    }

    protected void Grd1_PreRender(object sender, EventArgs e)
    {
        //MergeRows(Grd1);
    }

    public void DisplayRecord()
    {
        sql = "select Distinct pm.id,pam.EmpId,pam.Ecode,sctm.EmpName,sctm.SectionName,sctm.Medium,cm.ClassName,sm.SubjectName,sm.SessionName,pam.Type,pam.PeriodName,sctm.Classteacher,";
        sql = sql + " Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time,Convert(nvarchar(50),DATEDIFF(MINUTE,FromTime,ToTime))+' min.' as Duration from PeriodAllocationMaster pam ";
        sql = sql + " inner join PeriodMaster pm on pm.PeriodName=pam.PeriodName and pm.Type=pam.Type";
        sql = sql + " inner join SubjectClassteacherMaster sctm on pam.EmpId=sctm.EmpId and pam.Ecode=sctm.Ecode and (pam.SubjectId=sctm.Subjectid or pam.SubjectId=0) and pam.ClassId=sctm.ClassId and pam.SectionName=sctm.SectionName";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
        sql = sql + " where pam.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by pm.id";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
        foreach (GridViewRow gvr in Grd1.Rows)
        {
            Label lblEmpId1 = (Label)gvr.FindControl("lblEmpId1");
            Label lblPeriod = (Label)gvr.FindControl("lblPeriod");
            Label lblClassName = (Label)gvr.FindControl("lblClassName");
            Label lblSectionName = (Label)gvr.FindControl("lblSectionName");

            sql = " declare @days nvarchar(50)";
            sql = sql + " select @days=Coalesce(@days+'.','')+Substring(pam.Day,1,2) from PeriodAllocationMaster pam";
            sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=pam.Empid and eod.Ecode=pam.Ecode";
            sql = sql + " inner join ClassMaster cm on cm.Id=pam.ClassId";
            sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and pam.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' ";
            sql = sql + " and pam.SectionName='" + lblSectionName.Text + "' and cm.ClassName='" + lblClassName.Text + "' and eod.EmpId='" + lblEmpId1.Text + "' and pam.PeriodName='" + lblPeriod.Text + "' and eod.Withdrwal is null";
            sql = sql + " SELECT day = @days";
            Label lblDays = (Label)gvr.FindControl("lblDays");
            lblDays.Text = oo.ReturnTag(sql,"day");
            
        }
    }

    //public bool checkvalue()
    //{
    //    sql = "Select Distinct PeriodName from PeriodAllocationMaster where " + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text + "'";
    //    SqlDataAdapter da1 = new SqlDataAdapter(sql, con);
    //    DataTable dt1 = new DataTable();
    //    da1.Fill(dt1);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
    //        {
    //            sql = "Select Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time from PeriodMaster where PeriodName='" + dt1.Rows[i1][0].ToString() + "'";
    //            string time1 = oo.ReturnTag(sql, "Time");

    //            sql = "Select Replace(Convert(varchar(15),FromTime,100),RIGHT(CONVERT(VARCHAR(30), FromTime, 9),2),' ') + '- '+Replace(Convert(varchar(15),ToTime,100),RIGHT(CONVERT(VARCHAR(30), ToTime, 9),2),'') as Time from PeriodMaster where PeriodName='" + lblPeriodName.Text.Trim() + "'";
    //            string time2 = oo.ReturnTag(sql, "Time");
    //            if (time1 == time2)
    //            {
    //                break;

    //            }
    //            else
    //            {
    //                display(dt, countperiod, k, j, "Label2", "Label5", "Label16", "Label22", "Label28");
    //                countperiod = display(dt, countperiod, k, j, "Label2", "Label5", "Label16", "Label22", "Label28");

    //            }
    //        }
    //    }
    //}

    public void GenrateTimeTable()
    {
        sql = "Select cm.ClassName,sctm.SectionName,sctm.ClassId,sm.SubjectName,sctm.SubjectId,sctm.ClassTeacher from SubjectClassTeacherMaster sctm";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId";
        sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
        sql = sql + " where sctm." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text + "' and ";
        sql = sql + " sctm.StudentType='"+RadioButtonList1.SelectedItem.ToString()+"' and sm.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " order by sctm.ClassTeacher Desc";

        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        int j=0;
        int countperiod = 0;
        if (dt.Rows.Count > 0)
        {
            while (j < dt.Rows.Count)
            {

                for (int k = 0; k < GridView2.Rows.Count; k++)
                {
                    Label lblPeriodName = (Label)GridView2.Rows[k].FindControl("lblPeriodName");
                    Label lblPeriodTime = (Label)GridView2.Rows[k].FindControl("lblPeriodTime");
                    int l;
                    if (k != 0)
                    {
                        l = k % 3;
                    }
                    else
                    {
                        l = 1;
                    }
                    if (lblPeriodName.Text != "Lunch")
                    {
                        if (lblPeriodName.Text != "Dairy")
                        {
                            if (l != 0)
                            {
                                PeriodName = lblPeriodName.Text;
                                if (GridView2.HeaderRow.Cells[1].Text == "MONDAY")
                                {
                                    if (countperiod < 9)
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='M'";
                                        if (oo.Duplicate(sql) == false)
                                        {
                                            countperiod = display(dt, countperiod, k, j, "Label2", "Label5", "Label16", "Label22", "Label28");
                                        }
                                    }
                                    else
                                    {
                                       
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='M'";
                                        if (oo.Duplicate(sql) == false)
                                        {
                                            j = j + 1; 
                                            countperiod = display(dt, countperiod, k, j, "Label2", "Label5", "Label16", "Label22", "Label28");
                                        }
                                    }

                                }
                                if (GridView2.HeaderRow.Cells[2].Text == "TUESDAY")
                                {
                                    if (countperiod < 9)
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='T'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              countperiod = display(dt, countperiod, k, j, "Label6", "Label7", "Label17", "Label23", "Label29");
                                          }
                                    }
                                    else
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='T'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              j = j + 1;
                                              countperiod = display(dt, countperiod, k, j, "Label6", "Label7", "Label17", "Label23", "Label29");
                                          }
                                    }

                                }

                                if (GridView2.HeaderRow.Cells[3].Text == "WEDNESDAY")
                                {
                                    if (countperiod < 9)
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='W'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              countperiod = display(dt, countperiod, k, j, "Label8", "Label9", "Label18", "Label24", "Label30");
                                          }
                                    }
                                    else
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='W'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              j = j + 1;
                                              countperiod = display(dt, countperiod, k, j, "Label8", "Label9", "Label18", "Label24", "Label30");
                                          }
                                    }

                                }

                                if (GridView2.HeaderRow.Cells[4].Text == "THURSDAY")
                                {
                                    if (countperiod < 9)
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='TH'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              countperiod = display(dt, countperiod, k, j, "Label10", "Label11", "Label19", "Label25", "Label31");
                                          }
                                    }
                                    else
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='TH'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              j = j + 1;
                                              countperiod = display(dt, countperiod, k, j, "Label10", "Label11", "Label19", "Label25", "Label31");
                                          }
                                    }

                                }

                                if (GridView2.HeaderRow.Cells[5].Text == "FRIDAY")
                                {
                                    if (countperiod < 9)
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='F'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              countperiod = display(dt, countperiod, k, j, "Label12", "Label13", "Label20", "Label26", "Label32");
                                          }
                                    }
                                    else
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='F'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              j = j + 1;
                                              countperiod = display(dt, countperiod, k, j, "Label12", "Label13", "Label20", "Label26", "Label32");
                                          }
                                    }

                                }

                                if (GridView2.HeaderRow.Cells[6].Text == "SATURDAY")
                                {
                                    if (countperiod < 9)
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='S'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              countperiod = display(dt, countperiod, k, j, "Label14", "Label15", "Label21", "Label27", "Label33");
                                          }
                                    }
                                    else
                                    {
                                        sql = "Select *from PeriodAllocationMaster where PeriodTime='" + lblPeriodTime.Text.Trim() + "' and ClassId<>0 and Day='S'";
                                          if (oo.Duplicate(sql) == false)
                                          {
                                              j = j + 1;
                                              countperiod = display(dt, countperiod, k, j, "Label14", "Label15", "Label21", "Label27", "Label33");
                                          }
                                    }
                                }
                            }
                            else
                            {
                                if (k == 3)
                                {
                                    display(dt, countperiod, k, j, "Label10", "Label11", "Label19", "Label25", "Label31");
                                    display(dt, countperiod, k, j, "Label12", "Label13", "Label20", "Label26", "Label32");
                                    display(dt, countperiod, k, j, "Label14", "Label15", "Label21", "Label27", "Label33");
                                }
                                if (k == 6)
                                {
                                    display(dt, countperiod, k, j, "Label2", "Label5", "Label16", "Label22", "Label28");
                                    display(dt, countperiod, k, j, "Label6", "Label7", "Label17", "Label23", "Label29");
                                    display(dt, countperiod, k, j, "Label8", "Label9", "Label18", "Label24", "Label30");

                                }
                            }
                    }
                    else
                    {
                        displayforLunchandDairy(dt, k, "Label2", "Label5", "Label22");
                        displayforLunchandDairy(dt, k, "Label6", "Label7", "Label23");
                        displayforLunchandDairy(dt, k, "Label8", "Label9", "Label24");
                        displayforLunchandDairy(dt, k, "Label10", "Label11", "Label25");
                        displayforLunchandDairy(dt, k, "Label12", "Label13", "Label26");
                        displayforLunchandDairy(dt, k, "Label14", "Label15", "Label27");
                    }
                    }
                    else
                    {
                        displayforLunchandDairy(dt, k, "Label2", "Label5", "Label22");
                        displayforLunchandDairy(dt, k, "Label6", "Label7", "Label23");
                        displayforLunchandDairy(dt, k, "Label8", "Label9", "Label24");
                        displayforLunchandDairy(dt, k, "Label10", "Label11", "Label25");
                        displayforLunchandDairy(dt, k, "Label12", "Label13", "Label26");
                        displayforLunchandDairy(dt, k, "Label14", "Label15", "Label27");
                    }
                }
            }
        }
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            if (i == 1)
            {
                donotDisplay(i, "Label10", "Label11", "Label19", "Label25", "Label31");
                donotDisplay(i, "Label12", "Label13", "Label20", "Label26", "Label32");
                donotDisplay(i, "Label14", "Label15", "Label21", "Label27", "Label33");   
            }
            if (i == 7)
            {
                donotDisplay(i, "Label2", "Label5", "Label16", "Label22", "Label28");
                donotDisplay(i, "Label6", "Label7", "Label17", "Label23", "Label29");
                donotDisplay(i, "Label8", "Label9", "Label18", "Label24", "Label30");
            }
        }
    }

    public void donotDisplay(int i, string str1, string str2, string str3,string str4,string str5)
    {
        Label Label1 = (Label)GridView2.Rows[i].FindControl(str1);
        Label Label2 = (Label)GridView2.Rows[i].FindControl(str2);
        Label Label3 = (Label)GridView2.Rows[i].FindControl(str3);
        Label Label4 = (Label)GridView2.Rows[i].FindControl(str4);
        Label Label5 = (Label)GridView2.Rows[i].FindControl(str5);
    
        Label1.Text = "";
        Label2.Text = "";
        Label3.Text = "";
        Label4.Text = "";
        Label5.Text = "";
      

    }

    public void displayforLunchandDairy(DataTable dt, int k, string str1, string str2, string str3)
    {
        if (dt.Rows[0]["ClassTeacher"].ToString() == "Yes")
        {
            Label Label1 = (Label)GridView2.Rows[k].FindControl(str1);
            Label Label2 = (Label)GridView2.Rows[k].FindControl(str2);
            Label Label3 = (Label)GridView2.Rows[k].FindControl(str3);
            Label1.Text = dt.Rows[0]["ClassName"].ToString();
            Label2.Text = dt.Rows[0]["SectionName"].ToString();
            Label3.Text = dt.Rows[0]["ClassId"].ToString();
        }
       
    }

    public int display(DataTable dt, int countperiod,int k,int j,string str1,string str2,string str3,string str4, string str5)
    {
        Label Label1 = (Label)GridView2.Rows[k].FindControl(str1);
        Label Label2 = (Label)GridView2.Rows[k].FindControl(str2);
        Label Label3 = (Label)GridView2.Rows[k].FindControl(str3);
        Label Label4 = (Label)GridView2.Rows[k].FindControl(str4);
        Label Label5 = (Label)GridView2.Rows[k].FindControl(str5);
          
        if (countperiod < 9)
        {
            if (j < dt.Rows.Count)
            {
           
                        Label1.Text = dt.Rows[j]["ClassName"].ToString();
                        Label2.Text = dt.Rows[j]["SectionName"].ToString();
                        Label3.Text = dt.Rows[j]["SubjectName"].ToString();
                        Label4.Text = dt.Rows[j]["ClassId"].ToString();
                        Label5.Text = dt.Rows[j]["SubjectId"].ToString();
                   
                    countperiod = countperiod + 1;         
            }

        }
        else
        {
            if (j < dt.Rows.Count)
            {
 
                        Label1.Text = dt.Rows[j]["ClassName"].ToString();
                        Label2.Text = dt.Rows[j]["SectionName"].ToString();
                        Label3.Text = dt.Rows[j]["SubjectName"].ToString();
                        Label4.Text = dt.Rows[j]["ClassId"].ToString();
                        Label5.Text = dt.Rows[j]["SubjectId"].ToString();
                   
                    countperiod = 1;
          
            }
        }
        return countperiod;
    }

    public void checkperiods()
    {
        sql = "Select PeriodName,ClassId,SubjectId,Day,SectionName from PeriodAllocationMaster where " + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "'";
        sql = sql + " and Type='"+RadioButtonList1.SelectedItem.ToString()+"' and SessionName='"+Session["SessionName"].ToString()+"' order by Id";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt=new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    Label lblPeriodName = (Label)GridView1.Rows[j].FindControl("lblPeriodName");
                    CheckBox chk = (CheckBox)GridView1.Rows[j].FindControl("chk");
                 
                    if (dt.Rows[i][0].ToString() == lblPeriodName.Text.Trim())
                    {
                        chk.Checked = true;
                       
                    }
                }
            }
            loadclass();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    Label lblPeriodName = (Label)GridView1.Rows[j].FindControl("lblPeriodName");
                    CheckBox chk = (CheckBox)GridView1.Rows[j].FindControl("chk");
                    DropDownList drp1 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList1");
                    DropDownList drp2 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList2");
                    DropDownList drp3 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList3");
                    DropDownList drp4 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList4");
                    DropDownList drp5 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList5");
                    DropDownList drp6 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList6");
                    DropDownList drp7 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList7");
                    DropDownList drp8 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList8");
                    DropDownList drp9 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList9");
                    DropDownList drp10 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList10");
                    DropDownList drp11 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList11");
                    DropDownList drp12 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList12");
                    DropDownList drp13 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList13");
                    DropDownList drp14 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList14");
                    DropDownList drp15 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList15");
                    DropDownList drp16 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList16");
                    DropDownList drp17 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList17");
                    DropDownList drp18 = (DropDownList)GridView1.Rows[j].FindControl("DropDownList18");
                    if (dt.Rows[i][0].ToString() == lblPeriodName.Text.Trim())
                        {
                            for (int k = 0; k < GridView1.Columns.Count; k++)
                            {
                                if (dt.Rows[i][3].ToString() == "M")
                                {
                                    drp7.SelectedValue = dt.Rows[i][1].ToString();
                                    loadSection(j, 7, 13);
                                    drp13.SelectedValue = dt.Rows[i][4].ToString();
                                    loadsubject(j,1,7,13);
                                    drp1.SelectedValue = dt.Rows[i][2].ToString();
                                   
                                }
                                if (dt.Rows[i][3].ToString() == "T")
                                {
                                    drp8.SelectedValue = dt.Rows[i]["ClassId"].ToString();
                                    loadSection(j, 8, 14);
                                    drp14.SelectedValue = dt.Rows[i][4].ToString();
                                    loadsubject(j,2,8,14);
                                    drp2.SelectedValue = dt.Rows[i]["SubjectId"].ToString();
                                   
                                }
                                if (dt.Rows[i][3].ToString() == "W")
                                {
                                    drp9.SelectedValue = dt.Rows[i][1].ToString();
                                    loadSection(j, 9, 15);
                                    drp15.SelectedValue = dt.Rows[i][4].ToString();
                                    loadsubject(j,3,9,15);
                                    drp3.SelectedValue = dt.Rows[i][2].ToString();
                                   
                                }
                                if (dt.Rows[i][3].ToString() == "TH")
                                {
                                    drp10.SelectedValue = dt.Rows[i][1].ToString();
                                    loadSection(j, 10, 16);
                                    drp16.SelectedValue = dt.Rows[i][4].ToString();
                                    loadsubject(j,4,10,16);
                                    drp4.SelectedValue = dt.Rows[i][2].ToString();
                                  
                                }
                                if (dt.Rows[i][3].ToString() == "F")
                                {
                                    drp11.SelectedValue = dt.Rows[i][1].ToString();
                                    loadSection(j, 11, 17);
                                    drp17.SelectedValue = dt.Rows[i][4].ToString();
                                    loadsubject(j,5,11,17);
                                    drp5.SelectedValue = dt.Rows[i][2].ToString();
                                    
                                }
                                if (dt.Rows[i][3].ToString() == "S")
                                {
                                    drp12.SelectedValue = dt.Rows[i][1].ToString();
                                    loadSection(j, 12, 18);
                                    drp18.SelectedValue = dt.Rows[i][4].ToString();
                                    loadsubject(j,6,12,18);
                                    drp6.SelectedValue = dt.Rows[i][2].ToString();
                                    
                                }
                            }
                        }
                    
                }
            }
            EnableDisableDropDown();
            
        }
    }

    public void EnableDisableDropDown()
    {
        if (Grd.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                if (chk.Checked)
                {
                    DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                    DropDownList drp2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                    DropDownList drp3 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList3");
                    DropDownList drp4 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList4");
                    DropDownList drp5 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList5");
                    DropDownList drp6 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList6");
                    DropDownList drp7 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList7");
                    DropDownList drp8 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList8");
                    DropDownList drp9 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList9");
                    DropDownList drp10 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList10");
                    DropDownList drp11 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList11");
                    DropDownList drp12 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList12");
                    drp1.Enabled = true; drp2.Enabled = true; drp3.Enabled = true; drp4.Enabled = true; drp5.Enabled = true;
                    drp6.Enabled = true; drp7.Enabled = true; drp8.Enabled = true; drp9.Enabled = true; drp10.Enabled = true;
                    drp11.Enabled = true; drp12.Enabled = true;
                }
                else
                {
                    DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList1");
                    DropDownList drp2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList2");
                    DropDownList drp3 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList3");
                    DropDownList drp4 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList4");
                    DropDownList drp5 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList5");
                    DropDownList drp6 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList6");
                    DropDownList drp7 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList7");
                    DropDownList drp8 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList8");
                    DropDownList drp9 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList9");
                    DropDownList drp10 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList10");
                    DropDownList drp11 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList11");
                    DropDownList drp12 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList12");
                    drp1.Enabled = false; drp2.Enabled = false; drp3.Enabled = false; drp4.Enabled = false; drp5.Enabled = false;
                    drp6.Enabled = false; drp7.Enabled = false; drp8.Enabled = false; drp9.Enabled = false; drp10.Enabled = false;
                    drp11.Enabled = false; drp12.Enabled = false;
                }
            }


        }
    }

    public void loadsubject(int i, int j, int k, int l)
    {
      
                DropDownList drp0 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList" + j.ToString());
                DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList" + k.ToString());
                DropDownList drp2 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList" + l.ToString());

                sql = "select sm.SubjectName as SubjectName,sm.Id,cm.CidOrder from SubjectClassteacherMaster sctm ";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
                sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
                sql = sql + " inner join SectionMaster scm on scm.SectionName=sctm.SectionName and scm.ClassNameId=sctm.ClassId";
                sql = sql + " where scm.SectionName='" + drp2.SelectedItem.ToString() + "' and sm.classId='" + drp1.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
                sql = sql + " and scm.SessionName='" + Session["SessionName"].ToString() + "' and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
                sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";

                oo.FillDropDown_withValue(sql, drp0, "SubjectName", "Id");
                drp0.Items.Add("None");

    }

    public void loadSection(int i, int j, int k)
    {

        DropDownList drp0 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList" + j.ToString());
        DropDownList drp1 = (DropDownList)GridView1.Rows[i].FindControl("DropDownList" + k.ToString());

        sql = "select Distinct sctm.SectionName as SectionName,cm.CidOrder from SubjectClassteacherMaster sctm ";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode";
        sql = sql + " where sm.classId='" + drp0.SelectedValue.ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and eod.SessionName='" + Session["SessionName"].ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and eod." + drpEnter.SelectedItem.ToString() + "='" + txtEnter.Text.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
        oo.FillDropDownWithOutSelect(sql, drp1, "SectionName");

        drp1.Items.Insert(0, "Select");

    }

   
}