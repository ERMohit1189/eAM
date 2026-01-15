using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SubjectTeacherAllotment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            loadclass();
            oo.fillSelectvalue(drpBranch, "<--Select-->");
            oo.fillSelectvalue(drpSection, "<--Select-->");
            oo.fillSelectvalue(drpmedium, "<--Select-->");
            oo.fillSelectvalue(drpSubject, "<--Select-->");
            oo.fillSelectvalue(drpSubjectPaper, "<--Select-->");
            div1.Visible = false;
        }
    }

    public void LoadSubject()
    {
        sql = "select SubjectName,Id from TTSubjectMaster where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and ClassID=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and ApplicableFor<>'TimeTable'";
        oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
        oo.fillSelectvalue(drpSubject, "<--Select-->");
        loadPaper();
    }

    public void loadPaper()
    {
        sql = "Select PaperName, Id from TTPaperMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and ClassID=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and SubjectID=" + drpSubject.SelectedValue + " ";
        oo.FillDropDown_withValue(sql, drpSubjectPaper, "PaperName", "Id");
        drpSubjectPaper.Items.Insert(0, "<--Select-->");
    }

    private void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
        oo.fillSelectvalue(drpBranch, "<--Select-->");
    }
    public void loadclass()
    {
        sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " Order by CIDOrder";
        oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        oo.fillSelectvalue(drpclass, "<--Select-->");
    }

    public void loadsection()
    {
        sql = "Select id, SectionName from SectionMaster";
        sql = sql + " where ClassNameId=" + drpclass.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, "<--Select-->");
    }

    public void loadmedium()
    {
        sql = "Select Medium from MediumMaster";
        sql = sql + " Where  BranchCode=" + Session["BranchCode"].ToString() + "";
        oo.FillDropDown(sql, drpmedium, "Medium");
        drpmedium.Items.Insert(0, "<--Select-->");
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadsection();
        divDuplicate.InnerHtml = "";
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmedium();
        divDuplicate.InnerHtml = "";
    }
    protected void drpmedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
        divDuplicate.InnerHtml = "";
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPaper();
        divDuplicate.InnerHtml = "";
    }
    protected void drpSubjectPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        divDuplicate.InnerHtml = "";
    }
    public void displayEmpInfo()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "Select eod.EmpId EmpId,eod.Ecode Ecode,egd.EFirstName+' '+egd.EMiddleName+' '+egd.ELastName as EmpName,egd.EFatherName FatherName,eod.Designation Designation from EmpployeeOfficialDetails eod ";
        sql = sql + " inner join EmpGeneralDetail egd on eod.Ecode=egd.Ecode and eod.EmpId=egd.EmpId where eod.Withdrwal is null ";
        sql = sql + " and eod.ECode='" + empId.Trim() + "' and eod.BranchCode=" + Session["BranchCode"].ToString() + " and egd.BranchCode=" + Session["BranchCode"].ToString() + "";
        Grd.DataSource = oo.GridFill(sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            div1.Visible = true;
        }
        else
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Sorry, No Record found!", "A");
            div1.Visible = false;
        }
    }
    public void DisplayRecord()
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        sql = "select sctm.Id,sctm.EmpId,sctm.Ecode,sctm.EmpName,' ('+scm.SectionName+')' SectionNames,cm.ClassName,case when bm.IsDisplay=1 then bm.BranchName else '' end BranchName, sctm.Medium,sm.SubjectName,pm.PaperName, cm.CidOrder from ICSESubjectTeacherAllotment sctm ";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName  and cm.BranchCode=sctm.BranchCode  ";
        sql = sql + " inner join BranchMaster bm on bm.id=sctm.BranchId and bm.Classid=sctm.ClassId and bm.SessionName=sctm.SessionName and bm.BranchCode=sctm.BranchCode ";
        sql = sql + " inner join SectionMaster scm on scm.Id=sctm.SectionId and scm.ClassNameId=sctm.ClassId and scm.SessionName=sctm.SessionName and scm.BranchCode=sctm.BranchCode ";
        sql = sql + " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.Classid=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.sessionName=sctm.sessionName  and sm.BranchCode=sctm.BranchCode ";
        sql = sql + " inner join TTPaperMaster pm on pm.id=sctm.Paperid and pm.SubjectId=sm.id and pm.Classid=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode ";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode and eod.BranchCode=sctm.BranchCode   ";
        sql = sql + " where sctm.SessionName='" + Session["SessionName"] + "' and sctm.BranchCode=" + Session["BranchCode"] + " ";
        sql = sql + " and eod.ECode='" + empId.Trim() + "' and eod.Withdrwal is null Order by CidOrder";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        displayEmpInfo();
        DisplayRecord();
    }
    protected void txtHeaderEmpId_TextChanged(object sender, EventArgs e)
    {
        displayEmpInfo();
        DisplayRecord();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var empId = Request.Form[hfEmployeeId.UniqueID];
        if (empId == string.Empty)
        {
            empId = txtHeaderEmpId.Text.Trim();
        }
        
        sql = "Select ECode from ICSESubjectTeacherAllotment where ";
        sql = sql + " Classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and SectionId=" + drpSection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and PaperId=" + drpSubjectPaper.SelectedValue + " ";
        sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
        if (oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Duplicate Record!", "A");
        }
        else
        {
            sql = "Select ECode from ICSESubjectTeacherAllotment where ";
            sql = sql + " Classid=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and SectionId=" + drpSection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and PaperId=" + drpSubjectPaper.SelectedValue + " ";
            sql = sql + " and Medium='" + drpmedium.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
            if (oo.Duplicate(sql))
            {
                string pp = "select Name from GetAllStaffRecords_UDF(1) where Ecode='" + oo.ReturnTag(sql, "Ecode") + "'";
                string ss = "select case when isdisplay=1 then BranchName else '' end BranchName from branchMaster where Classid=" + drpclass.SelectedValue + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"] + "";
                divDuplicate.InnerHtml = "<b>" + oo.ReturnTag(pp, "Name") + "</b> is already subject teacher of " + drpSubjectPaper.SelectedItem.ToString() + " in the class <b>" + drpclass.SelectedItem.ToString() + " " + oo.ReturnTag(ss, "BranchName") + " (" + drpSection.SelectedItem.ToString() + ")</b>";
            }
            else
            {

                Label lblEcode = (Label)Grd.Rows[0].FindControl("lblEcode");
                Label lblEmpId = (Label)Grd.Rows[0].FindControl("lblEmpId");
                Label lblEmpName = (Label)Grd.Rows[0].FindControl("lblEmpName");
                Label lblFName = (Label)Grd.Rows[0].FindControl("lblFName");
                Label lblDesi = (Label)Grd.Rows[0].FindControl("lblDesi");

                cmd = new SqlCommand();
                cmd.CommandText = "ICSESubjectTeacherAllotmentProc";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", lblEmpId.Text);
                cmd.Parameters.AddWithValue("@Ecode", lblEcode.Text);
                cmd.Parameters.AddWithValue("@EmpName", lblEmpName.Text);
                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@SectionId", drpSection.SelectedValue);
                cmd.Parameters.AddWithValue("@Medium", drpmedium.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@Paperid", drpSubjectPaper.SelectedValue);
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayRecord();
                Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Submitted successfully", "S");
            }
        }
    }
    protected void linkDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId3 = (Label)chk.NamingContainer.FindControl("Label37");
        string ss = lblId3.Text;
        lblvalue.Text = ss.ToString();
        Panel2_ModalPopupExtender.Show();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        sql = "Delete From ICSESubjectTeacherAllotment where SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and BranchCode=" + Session["BranchCode"].ToString() + " and Id=" + lblvalue.Text;
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        DisplayRecord();
        Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Deleted successfully", "S");
    }
}