using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class AditionalGradeEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt = new DataTable();
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
            loadEval();
            loadclass();
            loadMedium();


            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void loadEval()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Eval");
        DataRow dr1 = dt.NewRow();
        dr1["Eval"] = "TERM1";
        dt.Rows.Add(dr1);
        DataRow dr2 = dt.NewRow();
        dr2["Eval"] = "TERM2";
        dt.Rows.Add(dr2);

        drpEval.DataSource = dt;
        drpEval.DataTextField = "Eval";
        drpEval.DataValueField = "Eval";
        drpEval.DataBind();
    }
    public void loadMedium()
    {
        drpMedium.Items.Clear();
        sql = "Select Distinct Medium from MediumMaster  where  BranchCode=" + Session["BranchCode"] + "";
        oo.FillDropDown_withValue(sql, drpMedium, "Medium", "Medium");
        drpMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    public void loadclass()
    {
        drpclass.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            drpclass.Items.Clear();
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and cm.SessionName=t1.SessionName";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G7' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select Distinct ClassName,cm.Id,CIDOrder from ICSESubjectTeacherAllotment ctm";
            sql +=  " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
            sql +=  " where ECode='" + Session["LoginName"].ToString() + "' ";
            sql +=  " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
            sql +=  " and cm.id in(select ClassId from dt_ClassGroupMaster where GroupId='G7' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
            sql +=  " order by CIDOrder asc ";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpBranch.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName, id from BranchMaster where ClassId='" + drpclass.SelectedValue + "'";
            sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = " Select Distinct sm.BranchName,sm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join BranchMaster sm on sm.ClassId=sctm.ClassId and sm.Id=sctm.BranchId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName = '" + Session["SessionName"] + "' and sctm.BranchCode = " + Session["BranchCode"] + " and sctm.ClassId='" + drpclass.SelectedValue + "' ";
            sql +=  " and ECode='" + Session["LoginName"].ToString() + "' ";
            BAL.objBal.FillDropDown_withValue(sql, drpBranch, "BranchName", "id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        drpStream.Items.Clear();
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpsection.Items.Clear();
        drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpSubject.Items.Clear();
        drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        GridView1.DataSource = null;
        GridView1.DataBind();
        lnkSubmit.Visible = false;
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpsection.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName, id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select  Distinct cm.SectionName, cm.id from ICSESubjectTeacherAllotment T1";
            sql +=  " inner join SectionMaster cm on cm.ClassNameId=T1.ClassId and cm.id=T1.SectionId and cm.SessionName=t1.SessionName and cm.BranchCode=t1.BranchCode";
            sql +=  " where  T1.ClassId=" + drpclass.SelectedValue + " and ECode='" + Session["LoginName"].ToString() + "' and T1.BranchCode=" + Session["BranchCode"] + " and T1.SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        sql = "Select Stream,id from StreamMaster where ClassId='" + drpclass.SelectedValue + "' and BranchCode=" + Session["BranchCode"].ToString() + " and BranchId='" + drpBranch.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "'  order by id";
        BAL.objBal.FillDropDown_withValue(sql, drpStream, "Stream", "id");
        drpStream.Items.Insert(0, new ListItem("<--Select-->", ""));

        drpSubject.Items.Clear();
        drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        GridView1.DataSource = null;
        GridView1.DataBind();
        lnkSubmit.Visible = false;
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSubject.Items.Clear();
        drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        GridView1.DataSource = null;
        GridView1.DataBind();
        lnkSubmit.Visible = false;
        drpMedium.SelectedIndex = 0;
    }
    protected void drpMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        drpSubject.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from TTSubjectMaster where ClassId=" + drpclass.SelectedValue + " and BranchId=" + drpBranch.SelectedValue + " and Medium='" + drpMedium.SelectedItem.Text + "' ";
            sql +=  " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ApplicableFor<>'TimeTable' and IsAditional=1";
            BAL.objBal.FillDropDown_withValue(sql, drpSubject, "SubjectName", "id");
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue + " and sctm.sectionid=" + drpsection.SelectedValue + " ";
            sql +=  " and sm.BranchId=" + drpBranch.SelectedValue + " and sm.Medium='" + drpMedium.SelectedItem.Text + "'  and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and IsAditional=1";
            BAL.objBal.FillDropDown_withValue(sql, drpSubject, "SubjectName", "id");
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        GridView1.DataSource = null;
        GridView1.DataBind();
        lnkSubmit.Visible = false;
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        GridView1.DataSource = null;
        GridView1.DataBind();
        lnkSubmit.Visible = false;
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpPaper.Items.Clear();
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,Id from TTPaperMaster where ClassId=" + drpclass.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " ";
            sql +=  " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpPaper, "PaperName", "id");
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select  Distinct PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTPaperMaster pm on pm.Id=sctm.PaperId and pm.Subjectid=sctm.Subjectid and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId=" + drpclass.SelectedValue + " and sctm.SubjectId=" + drpSubject.SelectedValue + " ";
            sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            BAL.objBal.FillDropDown_withValue(sql, drpPaper, "PaperName", "id");
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }

    protected void drpPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }

    public void loadgrid()
    {
        sql = "select * from (   ";
        sql +=  "  select isnull(g.SubjectId,0) SubjectId, isnull(g.PaperId,0) PaperId, g.id, asr.SrNo, Name, FatherName,MonthlyTest,UT1, UT2,UT3, HY  from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr    ";
        sql +=  "  left join UPEAditionalEntryXII g on g.ClassId=asr.ClassId and g.BranchId=asr.BranchId and g.SectionId=asr.SectionID and g.BranchCode=asr.BranchCode and g.SessionName=asr.SessionName and g.SrNo=asr.SrNo and SubjectId=" + drpSubject.SelectedValue + " and PaperId=" + drpPaper.SelectedValue + "";
        sql +=  "  where asr.ClassId=" + drpclass.SelectedValue + " and asr.BranchId=" + drpBranch.SelectedValue + " and asr.SectionID=" + drpsection.SelectedValue + " ";
        sql +=  " and asr.SessionName='" + Session["SessionName"].ToString() + "' and asr.BranchCode=" + Session["BranchCode"].ToString() + " and asr.Medium='" + drpMedium.SelectedValue + "' ";
        sql +=  "  and  isnull(Promotion,'')<>'Cancelled' and '" + drpEval.SelectedValue.ToLower() + "'='term1' and asr.StreamId=" + drpStream.SelectedValue + " ";
        if (drpStatus.SelectedValue != "0")
        {
            sql += "  and isnull(Withdrwal,'') = case when isnull('" + drpStatus.SelectedValue + "','')='B' or isnull('" + drpStatus.SelectedValue + "','')='' then isnull(Withdrwal,'') else case when isnull('" + drpStatus.SelectedValue + "','')='A' then '' else 'W' end end and isnull(blocked,'') = case when isnull('" + drpStatus.SelectedValue + "','')= 'W' or isnull('" + drpStatus.SelectedValue + "','')= '' then isnull(blocked,'') else case when isnull('" + drpStatus.SelectedValue + "','')= 'A' then '' else 'yes' end end";
        
            }
        sql +=  "  union all ";
        sql += "  select isnull(g.SubjectId,0) SubjectId,isnull(g.PaperId,0) PaperId, g.id, asr.SrNo, Name, FatherName,'' MonthlyTest, UT4 as UT1, UT5 as UT2,UT6 as UT3, AE as HY  from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "', " + Session["BranchCode"].ToString() + ") asr ";
        sql +=  "  left join UPEAditionalEntryXII g on g.ClassId=asr.ClassId and g.BranchId=asr.BranchId and g.SectionId=asr.SectionID and g.BranchCode=asr.BranchCode and g.SessionName=asr.SessionName and g.SrNo=asr.SrNo  and SubjectId=" + drpSubject.SelectedValue + " and PaperId=" + drpPaper.SelectedValue + "";
        sql +=  " where asr.ClassId=" + drpclass.SelectedValue + " and asr.BranchId=" + drpBranch.SelectedValue + " and asr.SectionID=" + drpsection.SelectedValue + "  and asr.Medium='" + drpMedium.SelectedValue + "'";
        sql +=  " and asr.SessionName='" + Session["SessionName"].ToString() + "' and asr.BranchCode=" + Session["BranchCode"].ToString() + " ";
        sql +=  " and isnull(Promotion,'')<>'Cancelled' and '" + drpEval.SelectedValue.ToLower() + "'='term2' and asr.StreamId=" + drpStream.SelectedValue + " ";
        if (drpStatus.SelectedValue != "0")
        {
            sql += "  and isnull(Withdrwal,'') = case when isnull('" + drpStatus.SelectedValue + "','')='B' or isnull('" + drpStatus.SelectedValue + "','')='' then isnull(Withdrwal,'') else case when isnull('" + drpStatus.SelectedValue + "','')='A' then '' else 'W' end end and isnull(blocked,'') = case when isnull('" + drpStatus.SelectedValue + "','')= 'W' or isnull('" + drpStatus.SelectedValue + "','')= '' then isnull(blocked,'') else case when isnull('" + drpStatus.SelectedValue + "','')= 'A' then '' else 'yes' end end";

        }
        sql +=  " )T1 order by t1.Name asc ";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            if (drpEval.SelectedValue.ToLower() == "term2")
            {
                //GridView1.Columns[4].Visible = false;
                //Label ut1 = (Label)GridView1.HeaderRow.FindControl("ut1");
                //ut1.Text = "Pre. 1";
                //Label ut2 = (Label)GridView1.HeaderRow.FindControl("ut2");
                //ut2.Text = "Pre. 2";
                //Label hy = (Label)GridView1.HeaderRow.FindControl("hy");
                //hy.Text = "Pre. 3";
                Label ut1 = (Label)GridView1.HeaderRow.FindControl("ut1");
                ut1.Text = "UT-4";
                Label ut2 = (Label)GridView1.HeaderRow.FindControl("ut2");
                ut2.Text = "UT-5";
                Label ut3 = (Label)GridView1.HeaderRow.FindControl("ut3");
                ut3.Text = "UT-6";
                Label hy = (Label)GridView1.HeaderRow.FindControl("hy");
                hy.Text = "AE";
            }
            else
            {
               // GridView1.Columns[4].Visible = true;
            }
            lnkSubmit.Visible = true;
        }
        else
        {
            lnkSubmit.Visible = false;
        }
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            int row = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                Label lblid = (Label)gvr.FindControl("lblid");
                Label SrNo = (Label)gvr.FindControl("SrNo");
                TextBox txtut1 = (TextBox)gvr.FindControl("txtut1");
                TextBox txtut2 = (TextBox)gvr.FindControl("txtut2");
                TextBox txtut3 = (TextBox)gvr.FindControl("txtut3");
                TextBox txthy = (TextBox)gvr.FindControl("txthy");
                TextBox monthlytest = (TextBox)gvr.FindControl("txtmonthlytest");

                cmd = new SqlCommand();
                cmd.CommandText = "sp_UPEAditionalEntryXII";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Id", lblid.Text);
                cmd.Parameters.AddWithValue("@ClassId", drpclass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", drpBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@StreamId", drpStream.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SectionId", drpsection.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SubjectId", drpSubject.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@PaperId", drpPaper.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Medium", drpMedium.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Evaluation", drpEval.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SrNo", SrNo.Text);
                if (drpEval.SelectedValue.ToLower() == "term1")
                {
                    cmd.Parameters.AddWithValue("@monthlytest", txtut1.Text.Trim());
                    cmd.Parameters.AddWithValue("@UT1", txtut1.Text.Trim());
                    cmd.Parameters.AddWithValue("@UT2", txtut2.Text.Trim());
                    cmd.Parameters.AddWithValue("@UT3", txtut3.Text.Trim());
                    cmd.Parameters.AddWithValue("@HY", txthy.Text.Trim());
                }
                else
                {
                    //cmd.Parameters.AddWithValue("@Pre1", txtut1.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Pre2", txtut2.Text.Trim());
                    //cmd.Parameters.AddWithValue("@UT3", txtut3.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Pre3", txthy.Text.Trim());
                    cmd.Parameters.AddWithValue("@UT1", txtut1.Text.Trim());
                    cmd.Parameters.AddWithValue("@UT2", txtut2.Text.Trim());
                    cmd.Parameters.AddWithValue("@UT3", txtut3.Text.Trim());
                    cmd.Parameters.AddWithValue("@HY", txthy.Text.Trim());
                }

                cmd.Parameters.AddWithValue("@ClassName", drpclass.SelectedItem.Text);
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
                loadgrid();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
            }
        }
    }
}