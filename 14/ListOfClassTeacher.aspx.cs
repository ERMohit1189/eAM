using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListOfClassTeacher : System.Web.UI.Page
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
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadclass();
            loadmedium();
            oo.fillSelectvalue(drpBranch, "<--Select-->");
            oo.fillSelectvalue(drpSection, "<--Select-->");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

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
        sql = sql + " Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
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
        
        divDuplicate.InnerHtml = "";
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        divDuplicate.InnerHtml = "";
    }
    
    public void DisplayRecord()
    {
        sql = "select sctm.Id,sctm.EmpId,sctm.Ecode,sctm.EmpName,' ('+scm.SectionName+')' SectionName,cm.ClassName,case when bm.IsDisplay=1 then bm.BranchName else '' end BranchName, sctm.Medium, cm.CidOrder, sctm.LoginName, format(sctm.RecordDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordedDT from ICSEClassTeacherAllotment sctm ";
        sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName  and cm.BranchCode=sctm.BranchCode  ";
        sql = sql + " inner join BranchMaster bm on bm.id=sctm.BranchId and bm.Classid=sctm.ClassId and bm.SessionName=sctm.SessionName and bm.BranchCode=sctm.BranchCode ";
        sql = sql + " inner join SectionMaster scm on scm.Id=sctm.SectionId and scm.ClassNameId=sctm.ClassId and scm.SessionName=sctm.SessionName and scm.BranchCode=sctm.BranchCode ";
        sql = sql + " inner join EmpployeeOfficialDetails eod on eod.EmpId=sctm.Empid and eod.Ecode=sctm.Ecode and eod.BranchCode=sctm.BranchCode   ";
        sql = sql + " where sctm.SessionName='" + Session["SessionName"] + "' and sctm.BranchCode=" + Session["BranchCode"] + " ";
        if (drpclass.SelectedIndex!=0)
        {
            sql = sql + " and cm.id=" + drpclass.SelectedValue + " ";
        }
        if (drpBranch.SelectedIndex != 0)
        {
            sql = sql + " and bm.id=" + drpBranch.SelectedValue + " ";
        }
        if (drpSection.SelectedIndex != 0)
        {
            sql = sql + " and scm.id=" + drpSection.SelectedValue + " ";
        }
        sql = sql + " and sctm.Medium='"+drpmedium.SelectedValue+"' and  eod.Withdrwal is null Order by CidOrder";
        Grd1.DataSource = oo.GridFill(sql);
        Grd1.DataBind();
        if (Grd1.Rows.Count > 0)
        {
            gdv1.Visible = true;
            divExport.Visible = true;
        }
        else
        {
            gdv1.Visible = false;
            divExport.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "ListOfClassTeachers", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "ListOfClassTeachers.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "ListOfClassTeachers", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        DisplayRecord();
    }
    
    
}