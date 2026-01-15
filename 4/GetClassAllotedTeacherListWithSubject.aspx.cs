using System;
using System.Web.UI.WebControls;

public partial class admin_GetClassAllotedTeacherListWithSubject : System.Web.UI.Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Repeater1.Items.Count > 0)
        {
            th.Visible = true;
        }
        else
        {
            th.Visible = false;
        }

  
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        if ((string)Session["LoginName"] == "" || (string)Session["BranchCode"] == "" || (string)Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            maindiv.Visible = false;
            lblSessionName.Text = Session["SessionName"].ToString();
            loadClass();
            BAL.objBal.fillSelectvalue(drpSection, "<--Select-->");
            BAL.objBal.fillSelectvalue(drpBranch, "<--Select-->");
        }
    }

    protected void loadClass()
    {
        sql = "Select ClassName,Id from ClassMaster";
        sql = sql + "  where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " order by CIDOrder";
        BAL.objBal.FillDropDown_withValue(sql, drpClass, "ClassName", "Id");
        drpClass.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    protected void loadBranch()
    {
        sql = "Select BranchName,Id from BranchMaster where ClassId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");

        drpBranch.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    protected void loadSection()
    {
        sql = "Select SectionName,id from SectionMaster where ClassNameId=" + drpClass.SelectedValue.ToString();
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        BAL.objBal.FillDropDown_withValue(sql, drpSection, "SectionName", "id");

        drpSection.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));

    }

    private void loadClassrp()
    {
        sql = "Select Case When ISNULL(bm.IsDisplay,0)=0 then (ClassName+'-'+SectionName) Else (ClassName+'-'+SectionName+'-'+bm.BranchName) End ClassSectionBranch,bm.Id branchid,cm.Id classid,SectionName";
        sql = sql + " From ClassMaster cm";
        sql = sql + " inner join SectionMaster sm on sm.ClassNameId=cm.id and sm.SessionName=cm.SessionName";
        sql = sql + " left join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName";
        sql = sql + " where cm.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"].ToString() + "";
        if (drpClass.SelectedIndex != 0)
        {
            sql = sql + " and cm.Id='" + drpClass.SelectedValue.ToString() + "'";
        }
        if (drpSection.SelectedIndex != 0)
        {
            sql = sql + " and sm.Id='" + drpSection.SelectedValue.ToString() + "'";
        }
        if (drpBranch.SelectedIndex != 0)
        {
            sql = sql + " and bm.Id='" + drpBranch.SelectedValue.ToString() + "'";
        }

        sql = sql + " order by cm.CidOrder";

        Repeater1.DataSource = BAL.objBal.GridFill(sql);
        Repeater1.DataBind();
        if (Repeater1.Items.Count > 0)
        {
            maindiv.Visible = true;
            th.Visible = true;
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                loadSubjectTeacher(i, Repeater1);
            }
        }
        else
        {
            maindiv.Visible = false;
            th.Visible = false;
        }
    }

    private void loadSubjectTeacher(int i,Repeater rpt)
    {
        Repeater Repeater2 = (Repeater)rpt.Items[i].FindControl("Repeater2");
        Label lblclassId = (Label)rpt.Items[i].FindControl("lblclassId");
        Label lblSection = (Label)rpt.Items[i].FindControl("lblSection");
        Label lblBranchid = (Label)rpt.Items[i].FindControl("lblBranchid");

        sql = "Select Distinct gasr.EmpId EmpId,gasr.Ecode Ecode,gasr.EmpName,gasr.EFatherName FatherName,gasr.Designation,";
        sql = sql + " Case when IsClassTeacher is null then '' Else (Case When IsClassTeacher=1 then 'Class Teacher' Else '' End) End isClassTeacher";
        sql = sql + " from SubjectClassTeacherMaster sctm";
        sql = sql + " inner join GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") gasr on gasr.Ecode=sctm.Ecode and gasr.EmpId=sctm.EmpId ";
        sql = sql + " left join SectionMaster sm on sm.SectionName=sctm.SectionName and sm.ClassNameId=sctm.ClassId and sm.SessionName=sctm.SessionName";
        sql = sql + " left join ClassTeacherMaster ctm on ctm.EmpId=sctm.EmpId and ctm.SectionId=sm.Id and ctm.BranchId=sctm.BranchId";
        sql = sql + " and ctm.ClassId=sctm.ClassId and ctm.SessionName=sctm.SessionName";
        sql = sql + " where sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sctm.ClassId='" + lblclassId.Text + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and ctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SectionName='" + lblSection.Text + "' and (sctm.BranchId='" + lblBranchid.Text + "' or sctm.BranchId is null)";
        sql = sql + " order by IsClassTeacher Desc";

        Repeater2.DataSource = BAL.objBal.GridFill(sql);
        Repeater2.DataBind();

        for (int j = 0; j < Repeater2.Items.Count; j++)
        {
            loadSubjectgroupdetails(j, Repeater2, lblclassId.Text, lblSection.Text, lblBranchid.Text);
        }
    }

    private void loadSubjectgroupdetails(int j, Repeater rpt, string clssid, string sectionname, string branchid)
    {
        Repeater Repeater3 = (Repeater)rpt.Items[j].FindControl("Repeater3");
        Label lblEmpid = (Label)rpt.Items[j].FindControl("lblEmpid");

        sql = "Select Distinct sgm.SubjectGroup,sgm.id subjectgroupid from SubjectClassTeacherMaster sctm";
        sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
        sql = sql + " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId and sgm.SessionName=sctm.SessionName";
        sql = sql + " where EmpId='" + lblEmpid.Text + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and sgm.ClassId='" + clssid + "' and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.SectionName='" + sectionname + "' and (sgm.BranchId='" + branchid + "' or sgm.BranchId is null)";

        Repeater3.DataSource = BAL.objBal.GridFill(sql);
        Repeater3.DataBind();

        for (int k = 0; k < Repeater3.Items.Count; k++)
        {
            loadSubjectdetails(k, Repeater3, clssid, sectionname, branchid, lblEmpid.Text);
        }

    }

    private void loadSubjectdetails(int k, Repeater rpt, string clssid, string sectionname, string branchid, string empid)
    {
        Repeater Repeater4 = (Repeater)rpt.Items[k].FindControl("Repeater4");
        Label lblsubjectgroupid = (Label)rpt.Items[k].FindControl("lblsubjectgroupid");

        sql = "Select sm.SubjectName from SubjectClassTeacherMaster sctm";
        sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
        sql = sql + " inner join SubjectGroupMaster sgm on sgm.Id=sm.GroupId and sgm.SessionName=sctm.SessionName";
        sql = sql + " where EmpId='" + empid + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and sgm.ClassId='" + clssid + "' and sgm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sgm.SectionName='" + sectionname + "' and (sgm.BranchId='" + branchid + "' or sgm.BranchId is null) and sgm.Id='" + lblsubjectgroupid.Text + "'";

        Repeater4.DataSource = BAL.objBal.GridFill(sql);
        Repeater4.DataBind();
    }

    protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadSection();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadClassrp();
    }


    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportToWord(Response, "SubjectTeacherList.doc", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcel(Response, "SubjectTeacherList.xls", divExport);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        //BAL.objBal.ExporttolandscapePdf(Response, "SubjectTeacherList.doc", divExport);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    
    }
}