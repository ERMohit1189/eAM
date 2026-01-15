using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class _4_StudentAttendanceManualy_ng : System.Web.UI.Page
{
    string sql = "";
    private SqlConnection _con;
    readonly Campus _oo;
    DataTable dt;

    public _4_StudentAttendanceManualy_ng()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        // ReSharper disable once PossibleNullReferenceException
        switch (Session["Logintype"].ToString())
        {
            case "Admin":
                MasterPageFile = "~/Master/admin_root-manager.master";
                break;
            case "Staff":
                MasterPageFile = "~/Staff/staff_root-manager.master";
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            loadClass(drpClass);
        }
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSection(drpSection, drpClass);
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch(drpBranch, drpClass);
        SelectStudentAttendance();
        SelectTerms(drpTerm);
    }
    private void SelectTerms(DropDownList drpTerm)
    {
        sql = "select term, id from master_examterms where SessionName='" + Session["SessionName"] + "' and BranchCode='" + Session["BranchCode"] + "' order by id asc ";
        dt = DAL.DALInstance.GetValueInTable(sql);

        if (dt != null && dt.Rows.Count > 0)
        {
            drpTerm.DataSource = dt;
            drpTerm.DataTextField = "term";
            drpTerm.DataValueField = "Id";
            drpTerm.DataBind();
            drpTerm.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }
    private void loadClass(DropDownList drpclass)
    {
        if (Session["logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode='" + Session["BranchCode"] + "' order by cidorder asc ";
            dt = DAL.DALInstance.GetValueInTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                drpclass.DataSource = dt;
                drpclass.DataTextField = "ClassName";
                drpclass.DataValueField = "Id";
                drpclass.DataBind();
                drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
        else
        {
            sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1 ";
            sql = sql + "inner join ClassMaster cm on cm.Id = T1.ClassId and cm.SessionName = t1.SessionName and T1.SessionName = '" + Session["SessionName"] + "'  and Cm.BranchCode='" + Session["BranchCode"] + "' ";
            sql = sql + "and cm.SessionName = T1.SessionName and cm.SessionName = '" + Session["SessionName"] + "' and EmpCode = '" + Session["LoginName"].ToString() + "' and T1.BranchCode='" + Session["BranchCode"] + "' ";
            sql = sql + "order by cidorder asc  ";
            dt = DAL.DALInstance.GetValueInTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                drpclass.DataSource = dt;
                drpclass.DataTextField = "ClassName";
                drpclass.DataValueField = "Id";
                drpclass.DataBind();
                drpclass.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
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
            sql = sql + " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName  and sm.BranchCode='" + Session["BranchCode"] + "' ";
            sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' and T1.BranchCode='" + Session["BranchCode"] + "' ";
            sql = sql + " and t1.Classid=" + drpclass.SelectedValue.ToString() + "";
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
            sql = sql + "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName and bm.BranchCode=t1.BranchCode  inner join SectionMaster sm on sm.Id = T1.sectionid and sm.SessionName = t1.SessionName and sm.BranchCode = t1.BranchCode    ";
            sql = sql + "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql = sql + "   T1.SessionName='" + Session["SessionName"] + "' and T1.BranchCode='" + Session["BranchCode"] + "'  and Bm.BranchCode='" + Session["BranchCode"] + "'  and T1.Classid='" + drpclass.SelectedValue.ToString() + "' and T1.SectionId="+ drpSection.SelectedValue + "";
            BAL.objBal.FillDropDown_withValue(sql, drpbranch, "BranchName", "Id");
            drpbranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        SelectStudentAttendance();
    }
    public void InsertStudentAttendance(string srno,string ta,string twa)
    {
        List<SqlParameter> param = new List<SqlParameter> {
            new SqlParameter("@Srno",srno),
            new SqlParameter("@TA",ta),
            new SqlParameter("@TWD",twa),
            new SqlParameter("@Term",drpTerm.SelectedValue),
            new SqlParameter("@SessionName",Session["SessionName"]),
            new SqlParameter("@LoginName",Session["LoginName"]),
            new SqlParameter("@BranchCode",Session["BranchCode"])
        };

        SqlParameter para = new SqlParameter("@Msg", "");
        para.Direction = System.Data.ParameterDirection.Output;
        param.Add(para);

        string msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("USP_Exam_AttendenceDetails", param);
        if(msg=="S")
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Submitted successfully.", "S");
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox1, "Record not Submitted successfully!", "A");
        }
    }

    public void SelectStudentAttendance()
    {
        List<SqlParameter> param = new List<SqlParameter> {
            new SqlParameter("@Term",drpTerm.SelectedValue),
            new SqlParameter("@Clasid",drpClass.SelectedValue),
            new SqlParameter("@BranchId",drpBranch.SelectedValue),
            new SqlParameter("@Sectionid",drpSection.SelectedValue),
            new SqlParameter("@SessionName",Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode",Session["BranchCode"].ToString()),
            new SqlParameter("@Queryfor","S"),
        };
        grdStudentList.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Exam_AttendenceDetails", param);
        grdStudentList.DataBind();
        abc11.Visible = true;
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        for(int i=0;i<grdStudentList.Rows.Count;i++)
        {
            Label lblSrno = (Label)grdStudentList.Rows[i].FindControl("lblSrno");
            TextBox txtTA = (TextBox)grdStudentList.Rows[i].FindControl("txtTA");
            TextBox txtTWD = (TextBox)grdStudentList.Rows[i].FindControl("txtTWD");
            InsertStudentAttendance(lblSrno.Text, txtTA.Text, txtTWD.Text);
        } 
    }

    protected void drpTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectStudentAttendance();
    }
}