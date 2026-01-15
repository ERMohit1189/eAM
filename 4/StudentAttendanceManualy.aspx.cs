using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class _4_StudentAttendanceManualy : System.Web.UI.Page
{
    string sql = "";
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
        loadBranch(drpBranch, drpClass);
        loadSection(drpSection, drpClass);
      //  SelectStudentAttendance();
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
            param.Add(new SqlParameter("@EmpCode", Session["LoginName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

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
            sql = sql + " inner join SectionMaster sm on sm.Id=T1.SectionId and sm.SessionName=t1.SessionName";
            sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and T1.SessionName='" + Session["SessionName"] + "' and bm.BranchCode=" + Session["BranchCode"] + " and T1.BranchCode=" + Session["BranchCode"] + "";
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
            sql = sql + "   inner join BranchMaster bm on bm.Id=T1.BranchId and bm.SessionName=t1.SessionName";
            sql = sql + "   where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1 and";
            sql = sql + "   T1.SessionName='" + Session["SessionName"] + "' and T1.Classid='" + drpclass.SelectedValue.ToString() + "' and bm.BranchCode=" + Session["BranchCode"] + " and  T1.BranchCode=" + Session["BranchCode"] + "";
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
            new SqlParameter("@SessionName",Session["SessionName"].ToString()),
            new SqlParameter("@LoginName",Session["LoginName"].ToString()),
            new SqlParameter("@BranchCode", Session["BranchCode"].ToString())
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
            new SqlParameter("@status",DropDownList1.SelectedValue),
            new SqlParameter("@displayorder",DropDownList2.SelectedValue),
            new SqlParameter("@SessionName",Session["SessionName"].ToString()),
            new SqlParameter("@BranchCode", Session["BranchCode"].ToString()),
            new SqlParameter("@Queryfor","S"),
        };
        //grdStudentList.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Exam_AttendenceDetails", param);
       var ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Exam_AttendenceDetails", param);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdStudentList.DataSource = ds;
            grdStudentList.DataBind();
            abc11.Visible = true;
        }
        else
        {
            grdStudentList.DataSource = ds;
            grdStudentList.DataBind();
            abc11.Visible = false;
        }
       // grdStudentList.DataBind();
       
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
        //SelectStudentAttendance();
    }
}