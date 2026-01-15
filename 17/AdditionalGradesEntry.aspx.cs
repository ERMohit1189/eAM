using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class AdditionalGradesEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() != "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null || Session["SessionName"].ToString() == "" || Session["LoginName"].ToString() == "" || Session["BranchCode"].ToString() == "")
        {
            Response.Redirect("~/default.aspx");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            loadclass();
        }
    }
    public void loadgrid()
    {
        sql = "select  srno, Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") ";
        sql +=  "   where  ClassId='" + drpclass.SelectedValue + "' and BranchId='" + drpBranch.SelectedValue + "'";
        sql +=  "   and SectionID='" + drpsection.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   BranchCode=" + Session["BranchCode"].ToString() + " and Withdrwal is null and  isnull(Promotion,'')<>'Cancelled' order by Name Asc";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                Label lblsrno = (Label)gvr.FindControl("Label16");
                sql = "Select Grade from AdditionalGradesEntry where SRNO='" + lblsrno.Text + "' and classid=" + drpclass.SelectedValue + "";
                sql += " and Branchid=" + drpBranch.SelectedValue + " and Sectionid=" + drpsection.SelectedValue + " and BranchCode=" + Session["BranchCode"] + "";
                sql += " and Evaluation='" + drpEval.SelectedItem.Text + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "' and PaperId='" + drpPaper.SelectedValue.ToString() + "'";
                Text1.Text = oo.ReturnTag(sql, "Grade");
            }
            lnkSubmit.Visible = true;
        }
        else
        {
            lnkSubmit.Visible = false;
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and cm.SessionName=t1.SessionName";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select  Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster T1";
            sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName and cm.BranchCode=t1.BranchCode";
            sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName and T1.BranchCode=t2.BranchCode";
            sql +=  " where EmpCode='" + Session["LoginName"].ToString() + "' and T1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        
    }
    public void loadSubjects()
    {
        drpSubject.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from TTSubjectMaster where ClassId=" + drpclass.SelectedValue.ToString() + " and IsAditional=1 ";
            sql += " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ApplicableFor<>'TimeTable'";
        }
        else
        {
            sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql += " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql += " where Ecode='" + Session["LoginName"].ToString() + "' and sm.IsAditional=1 and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.ToString() + " and sctm.sectionid=" + drpsection.SelectedValue + " ";
            sql += " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        }
        oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
        drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public void loadPapers()
    {
        drpPaper.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,Id from TTPaperMaster where ClassId=" + drpclass.SelectedValue.ToString() + " and SubjectId=" + drpSubject.SelectedValue.ToString() + " ";
            sql += " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        }
        else
        {
            sql = "Select  Distinct PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            sql += " inner join TTPaperMaster pm on pm.Id=sctm.PaperId and pm.Subjectid=sctm.Subjectid and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode";
            sql += " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " and sctm.SubjectId=" + drpSubject.SelectedValue.ToString() + " ";
            sql += " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
        }
        oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPapers();
    }
    public void loadsection()
    {
        drpsection.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName, id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        }
        else
        {
            sql = "Select  Distinct cm.SectionName, cm.id from ClassTeacherMaster T1";
            sql +=  " inner join SectionMaster cm on cm.ClassNameId=T1.ClassId and cm.id=T1.SectionId and cm.SessionName=t1.SessionName and cm.BranchCode=t1.BranchCode";
            sql +=  " where  T1.ClassId=" + drpclass.SelectedValue + " and EmpCode='" + Session["LoginName"].ToString() + "' and T1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "'";
        }
        oo.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
        drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public void loadBranch()
    {
        drpBranch.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster Where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        }
        else
        {
            sql = "Select Distinct t1.BranchName, t1.Id  from ICSESubjectTeacherAllotment ctm";
            sql += " inner join BranchMaster t1 on t1.ClassId=ctm.ClassId and t1.id=ctm.BranchId  and t1.SessionName=ctm.SessionName and t1.BranchCode=ctm.BranchCode";
            sql += " where ECode='" + Session["LoginName"].ToString() + "'";
            sql += " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + "  and ctm.classid=" + drpclass.SelectedValue + "";
        }
        oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBranch();
        loadsection();
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            int row = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                Label Label16 = (Label)gvr.FindControl("Label16");
                TextBox Text1 = (TextBox)gvr.FindControl("TextBox1");
                cmd = new SqlCommand();
                cmd.CommandText = "AdditionalGradesEntryProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Classid", drpclass.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Branchid", drpBranch.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Sectionid", drpsection.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SrNo", Label16.Text);
                cmd.Parameters.AddWithValue("@Grade", Text1.Text.Trim());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                row = row + 1;
                con.Close();
            }
            if (row > 0)
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                oo.ClearControls(table1);
                drpPaper.SelectedIndex = 0;
            }
        }

    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjects();
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjects();
    }
    protected void ddrpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPapers();
    }
    protected void drpPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        
    }
}