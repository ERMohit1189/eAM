using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_MarksEntryReportNurtoPrep_1718 : Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
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
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        //BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            loadSubjectGroup();
            drpActivityId.Items.Insert(0, "<--Select-->");

        }
    }

    public string getSubjectTeacherName()
    {
        sql = "Select Name EmpName from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where EmpId=(Select EmpId from ICSESubjectTeacherAllotment where classid=" + drpclass.SelectedValue.ToString() + " and Sectionid=" + drpsection.SelectedValue.ToString() + " and Subjectid=" + drpSubjectGroup.SelectedValue.ToString() + " and paperid=" + drpSubject.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "')";
        return BAL.objBal.ReturnTag(sql, "EmpName");
    }
    public void createTitle()
    {
        LblSession.Text = Session["SessionName"].ToString();
        lblClass.Text = drpclass.SelectedItem.Text.ToString() + " (" + drpsection.SelectedItem.Text.ToString() + ")";
        lblSubjectTeacherName.Text = getSubjectTeacherName();
        lblSubject.Text = drpSubjectGroup.SelectedItem.Text.ToString();
        lblPaper.Text = drpSubject.SelectedItem.Text.ToString();
        lblActivity.Text = drpActivityId.SelectedItem.Text.ToString();
        lblDate.Text = BAL.objBal.CurrentDate();
    }
    public void loadgrid()
    {
        sql = "select mm.MaxMarks1, mm.MaxMarks2, mm.MaxMarks3, mm2.MaxMarks1 MaxMarks1_2, mm2.MaxMarks2 MaxMarks2_2, mm2.MaxMarks3 MaxMarks3_2 ";
        sql +=  " from SetMaxMinMarks_NurtoPrep_Grade mm ";
        sql +=  " left join SetMaxMinMarks_NurtoPrep_Grade mm2 on mm.ClassId=mm2.ClassId and mm2.SubjectId=mm.SubjectId and mm2.PaperId=mm.PaperId and mm2.ActivityId=mm.ActivityId ";
        sql +=  " and mm.BranchCode=mm2.BranchCode and mm.SessionName=mm2.SessionName and mm.Evaluation='Term1' and mm2.Evaluation='Term2'";
        sql +=  " where mm.ClassId=" + drpclass.SelectedValue + " and mm.BranchCode=" + Session["BranchCode"] + " and mm.SessionName='" + Session["SessionName"] + "' ";
        sql +=  " and mm.SubjectId=" + drpSubjectGroup.SelectedValue + " and mm.PaperId=" + drpSubject.SelectedValue + " ";
        sql +=  "  and mm.ActivityId=" + drpActivityId.SelectedValue + " and mm.Evaluation='Term1' ";


        string sql1 = "select asr.SrNo, Name, FatherName, mm.Evel1, mm.Evel2, mm.Evel3, mm.Best2, mm.Conversion, mm.Grade, ";
        sql1 = sql1 + " mm2.Evel1 Evel1_2, mm2.Evel2 Evel2_2, mm2.Evel3 Evel3_2, mm2.Best2 Best2_2, mm2.Conversion Conversion_2, mm2.Grade Grade_2 ";
        sql1 = sql1 + " from AllStudentRecord_UDF('', 1) asr ";
        sql1 = sql1 + " left join CCENurtoPrep_Grade mm on mm.SrNo=asr.SrNo and asr.ClassId=mm.ClassId and mm.SectionId=asr.SectionID ";
        sql1 = sql1 + " and asr.BranchCode=mm.BranchCode and asr.SessionName=mm.SessionName and mm.Evaluation='Term1' ";
        sql1 = sql1 + " left join CCENurtoPrep_Grade mm2 on mm2.SrNo=asr.SrNo and asr.ClassId=mm2.ClassId and mm2.SectionId=asr.SectionID ";
        sql1 = sql1 + " and asr.BranchCode=mm2.BranchCode and asr.SessionName=mm2.SessionName and mm2.Evaluation='Term2' ";
        sql1 = sql1 + " where asr.ClassId=" + drpclass.SelectedValue + " and asr.SectionID=" + drpsection.SelectedValue + " ";
        sql1 = sql1 + " and asr.BranchCode=" + Session["BranchCode"] + " and asr.SessionName='" + Session["SessionName"] + "' ";
        sql1 = sql1 + " and mm.SubjectId=" + drpSubjectGroup.SelectedValue + " and mm.PaperId=" + drpSubject.SelectedValue + " ";
        sql1 = sql1 + "  and mm.ActivityId=" + drpActivityId.SelectedValue + " and Withdrwal is null  order by Name Asc ";
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (oo.GridFill(sql1) != null & oo.GridFill(sql1).Tables[0].Rows.Count > 0)
            {
                createTitle();
                divExport.Visible = true;
                GridView1.DataSource = oo.GridFill(sql1);
                GridView1.DataBind();
                Label lblMmEvel1 = (Label)GridView1.HeaderRow.FindControl("lblMmEvel1_1");
                Label lblMmEvel2 = (Label)GridView1.HeaderRow.FindControl("lblMmEvel2_1");
                Label lblMmEvel3 = (Label)GridView1.HeaderRow.FindControl("lblMmEvel3_1");
                Label lblMmEvel4 = (Label)GridView1.HeaderRow.FindControl("lblMmEvel4_2");
                Label lblMmEvel5 = (Label)GridView1.HeaderRow.FindControl("lblMmEvel5_2");
                Label lblMmEvel6 = (Label)GridView1.HeaderRow.FindControl("lblMmEvel6_2");
                lblMmEvel1.Text = oo.ReturnTag(sql, "MaxMarks1");
                lblMmEvel2.Text = oo.ReturnTag(sql, "MaxMarks2");
                lblMmEvel3.Text = oo.ReturnTag(sql, "MaxMarks3");
                lblMmEvel4.Text = oo.ReturnTag(sql, "MaxMarks1_2");
                lblMmEvel5.Text = oo.ReturnTag(sql, "MaxMarks2_2");
                lblMmEvel6.Text = oo.ReturnTag(sql, "MaxMarks3_2");
            }
            else
            {
                divExport.Visible = false;
            }
        }
        catch (Exception)
        {
            divExport.Visible = false;
        }
        
        if (drpEval.SelectedIndex==0)
        {
            GridView1.Columns[10].Visible = false;
            GridView1.Columns[11].Visible = false;
            GridView1.Columns[12].Visible = false;
            GridView1.Columns[13].Visible = false;
            GridView1.Columns[14].Visible = false;
            GridView1.Columns[15].Visible = false;
        }
        if (drpEval.SelectedIndex == 1)
        {
            GridView1.Columns[10].Visible = true;
            GridView1.Columns[11].Visible = true;
            GridView1.Columns[12].Visible = true;
            GridView1.Columns[13].Visible = true;
            GridView1.Columns[14].Visible = true;
            GridView1.Columns[15].Visible = true;
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G1' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
            sql +=  " where GroupId='G1'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }
    public void loadsection()
    {
        
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        else
        {
            sql = "Select Distinct sm.SectionName,sm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.id=sctm.SectionId ";
            sql +=  " and sm.SectionName=sctm.SectionName";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sm.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }
    private void loadSubjectGroup()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from ttSubjectMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and ApplicableFor<>'TimeTable'";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectName", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " ";
            sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            
            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectName", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadSubject()
    {

        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,Id from ttpapermaster where SubjectId='" + drpSubjectGroup.SelectedValue.ToString() + "' and Classid="+ drpclass.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id";

            oo.FillDropDown_withValue(sql, drpSubject, "PaperName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct pm.PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql +=  " inner join ttpapermaster pm on pm.Id=sm.Subjectid and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " and pm.SubjectId='" + drpSubjectGroup.SelectedValue.ToString() + " ";
            sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            
            oo.FillDropDown_withValue(sql, drpSubject, "PaperName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadActivity()
    {

        sql = "Select ActivityName,Id from ttActivitymaster where SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + " and PaperId=" + drpSubject.SelectedValue + " and Classid=" + drpclass.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id";
        oo.FillDropDown_withValue(sql, drpActivityId, "ActivityName", "Id");
        drpActivityId.Items.Insert(0, "<--Select-->");
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadActivity();
    }
    protected void drpActivityId_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadSubject();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
        loadSubject();
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSubjectGroup.SelectedIndex=0;
        drpSubject.Items.Clear();
        drpSubject.Items.Insert(0, "<--Select-->");
        drpActivityId.Items.Clear();
        drpActivityId.Items.Insert(0, "<--Select-->");
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "SubjectwiseCumlativeNurtoPrep", table1);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "SubjectwiseCumlativeNurtoPrep", table1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        GridView1.Style.Add("text-transform", "uppercase");
        oo.ExportDivToExcelWithFormatting(Response, "SubjectwiseCumlativeNurtoPrep.xls", table1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {

        PrintHelper_New.ctrl = table1;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    
}