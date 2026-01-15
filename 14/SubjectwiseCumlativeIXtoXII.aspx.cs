using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubjectwiseCumlativeIXtoXII : Page
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
        if (!IsPostBack)
        {
            loadclass();
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
    }

    public string getSubjectTeacherName()
    {
        sql = "Select Name EmpName from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where EmpId=(Select EmpId from ICSESubjectTeacherAllotment where Subjectid='" + drpPaper.SelectedValue.ToString() + "' and classid="+drpclass.SelectedValue+ " and branchid=" + drpBranch.SelectedValue + " and sectionid=" + drpsection.SelectedValue + " and BranchCode=" + Session["BranchCode"] + ")";

        return BAL.objBal.ReturnTag(sql, "EmpName");
    }
    public void createTitle()
    {
        LblSession.Text = Session["SessionName"].ToString();
        lblClass.Text = drpclass.SelectedItem.Text.ToString();
        lblSection.Text = drpsection.SelectedItem.Text.ToString();
        lblSubjectTeacherName.Text = getSubjectTeacherName();
        lblSubject.Text = drpPaper.SelectedItem.Text.ToString();
        lblDate.Text = BAL.objBal.CurrentDate();
    }
    public void loadgrid1()
    {
        sql = "select   srno,Name,FatherName from VW_AllStudentRecord ";
        sql = sql + "   where Classid='" + drpclass.SelectedValue.ToString() + "' and branchid=" + drpBranch.SelectedValue.ToString() + "";
        sql = sql + "   and Sectionid='" + drpsection.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + "   and BranchCode=" + Session["BranchCode"] + " ";
        sql = sql + "   and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' order by name Asc";


        GridView2.Visible = false;
        GridView1.Visible = true;
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            divExport.Visible = true;
            string sqls = "select MaxHY, MaxPracHY, MaxAE, MaxPracAE from ICSEMaxMarkEntryIXtoXII where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' ";
            sqls = sqls + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";

            Label lblMMTHEORYhy = (Label)GridView1.HeaderRow.FindControl("lblMMTHEORYhy");
            Label lblMMPRAChy = (Label)GridView1.HeaderRow.FindControl("lblMMPRAChy");

            lblMMTHEORYhy.Text = oo.ReturnTag(sqls, "MaxHY");
            lblMMPRAChy.Text = oo.ReturnTag(sqls, "MaxPracHY");

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblsrno = (Label)GridView1.Rows[i].FindControl("lblsrnos");
                sql = "Select HY,PracHY,AE, PracAE from ICSEMarkEntryIXtoXII where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                sql = sql + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";
                Label lblTHEORYhy = (Label)GridView1.Rows[i].FindControl("lblTHEORYhy");
                Label lblPRAChy = (Label)GridView1.Rows[i].FindControl("lblPRAChy");
                Label lblTotalhy = (Label)GridView1.Rows[i].FindControl("lblTotalhy");
                

                lblTHEORYhy.Text = oo.ReturnTag(sql, "HY");
                lblPRAChy.Text = oo.ReturnTag(sql, "PracHY");
                double HY = 0; double Prac = 0;
                double.TryParse(oo.ReturnTag(sql, "HY"), out HY);
                double.TryParse(oo.ReturnTag(sql, "PracHY"), out Prac);
                lblTotalhy.Text = (HY + Prac).ToString("0.0");
            }
        }
        else
        {
            divExport.Visible = false;
        }
    }

    public void loadgrid2()
    {
        sql = "select   srno,Name,FatherName from VW_AllStudentRecord ";
        sql = sql + "   where Classid='" + drpclass.SelectedValue.ToString() + "' and branchid=" + drpBranch.SelectedValue.ToString() + "";
        sql = sql + "   and Sectionid='" + drpsection.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + "   and BranchCode=" + Session["BranchCode"] + " ";
        sql = sql + "   and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' order by name Asc";


        GridView2.Visible = true;
        GridView1.Visible = false;
        GridView2.DataSource = oo.GridFill(sql);
        GridView2.DataBind();
        if (GridView2.Rows.Count > 0)
        {
            divExport.Visible = true;
            string sqls = "select MaxHY, MaxPracHY, MaxAE, MaxPracAE from ICSEMaxMarkEntryIXtoXII where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' ";
            sqls = sqls + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";

            Label lblMMTHEORYhy = (Label)GridView2.HeaderRow.FindControl("lblMMTHEORYhy");
            Label lblMMPRAChy = (Label)GridView2.HeaderRow.FindControl("lblMMPRAChy");

            lblMMTHEORYhy.Text = oo.ReturnTag(sqls, "MaxHY");
            lblMMPRAChy.Text = oo.ReturnTag(sqls, "MaxPracHY");

            Label lblMMTHEORYae = (Label)GridView2.HeaderRow.FindControl("lblMMTHEORYae");
            Label lblMMPRACae = (Label)GridView2.HeaderRow.FindControl("lblMMPRACae");

            lblMMTHEORYae.Text = oo.ReturnTag(sqls, "MaxAE");
            lblMMPRACae.Text = oo.ReturnTag(sqls, "MaxPracAE");

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblsrno = (Label)GridView2.Rows[i].FindControl("lblsrno");
                sql = "Select HY,PracHY,AE, PracAE from ICSEMarkEntryIXtoXII where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                sql = sql + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";
                Label lblTHEORYhy = (Label)GridView2.Rows[i].FindControl("lblTHEORYhy");
                Label lblPRAChy = (Label)GridView2.Rows[i].FindControl("lblPRAChy");
                Label lblTotalhy = (Label)GridView2.Rows[i].FindControl("lblTotalhy");


                lblTHEORYhy.Text = oo.ReturnTag(sql, "HY");
                lblPRAChy.Text = oo.ReturnTag(sql, "PracHY");
                double HY = 0; double Prac = 0;
                double.TryParse(oo.ReturnTag(sql, "HY"), out HY);
                double.TryParse(oo.ReturnTag(sql, "PracHY"), out Prac);
                lblTotalhy.Text = (HY + Prac).ToString("0.0");

                Label lblTHEORYae = (Label)GridView2.Rows[i].FindControl("lblTHEORYae");
                Label lblPRACae = (Label)GridView2.Rows[i].FindControl("lblPRACae");
                Label lblTotalae = (Label)GridView2.Rows[i].FindControl("lblTotalae");


                lblTHEORYae.Text = oo.ReturnTag(sql, "AE");
                lblPRACae.Text = oo.ReturnTag(sql, "PracAE");
                double AE = 0; double PracAE = 0;
                double.TryParse(oo.ReturnTag(sql, "AE"), out AE);
                double.TryParse(oo.ReturnTag(sql, "PracAE"), out PracAE);
                lblTotalae.Text = (AE + PracAE).ToString("0.0");

                Label lnlGTotal = (Label)GridView2.Rows[i].FindControl("lnlGTotal");
                lnlGTotal.Text= (HY + Prac+AE + PracAE).ToString("0.0");

            }

        }
        else
        {
            divExport.Visible = true;
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Id,ClassName,CidOrder from ClassMaster Where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " and id in(select classid from ICSEClassGroupMaster where GroupName='G3' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")  Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select Distinct ClassName,cm.Id,CIDOrder from ICSEClassTeacherAllotment ctm";
            sql = sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' ";
            sql = sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
            sql = sql + " and cm.id in(select classid from ICSEClassGroupMaster where GroupName='G3' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
            sql = sql + " order by CIDOrder asc ";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,id from BranchMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode = " + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
        else
        {
            sql = " Select Distinct sm.BranchName,sm.id from ICSEClassTeacherAllotment sctm";
            sql = sql + " inner join BranchMaster sm on sm.ClassId=sctm.ClassId and sm.Id=sctm.BranchId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            sql = sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode = " + Session["BranchCode"] + " and ClassId='" + drpclass.SelectedValue.ToString() + "' ";
            sql = sql + " and Ecode='" + Session["LoginName"].ToString() + "' ";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void loadsection()
    {
       

        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and  BranchCode = " + Session["BranchCode"] + "";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
        else
        {
            sql = " Select Distinct sm.SectionName,sm.id from ICSEClassTeacherAllotment sctm";
            sql = sql + " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.Id=sctm.SectionId and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode ";
            sql = sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.BranchCode = " + Session["BranchCode"] + " and ClassId='" + drpclass.SelectedValue.ToString() + "' ";
            sql = sql + " and Ecode='" + Session["LoginName"].ToString() + "' ";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    private void loadSubject()
    {

        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from TTSubjectMaster where ClassId=" + drpclass.SelectedValue.ToString() + " ";
            sql = sql + " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and ApplicableFor<>'TimeTable'";
            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            
        }
        else
        {
            sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql = sql + " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " ";
            sql = sql + " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void loadPaper()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,Id from TTPaperMaster where ClassId=" + drpclass.SelectedValue.ToString() + "  and SubjectId=" + drpSubject.SelectedValue.ToString() + " ";
            sql = sql + " and  BranchCode = " + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select  Distinct PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            sql = sql + " inner join TTPaperMaster pm on pm.Id=sctm.PaperId and pm.Subjectid=sctm.Subjectid and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " and sctm.SubjectId=" + drpSubject.SelectedValue.ToString() + " ";
            sql = sql + " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpPaper, "PaperName", "Id");
            drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        divExport.Visible = false;
        loadBranch();
        loadsection();
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divExport.Visible = false;
        loadSubject();
        loadPaper();
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        divExport.Visible = false;
        loadPaper();
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
    }
    protected void drpPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        divExport.Visible = false;
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        divExport.Visible = false;
        createTitle();
         if (drpEval.SelectedIndex == 0)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.Visible = false;
            GridView1.Visible = true;
            loadgrid1();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            GridView2.Visible = true;
            loadgrid2();
        }
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        divExport.Visible = false;
        loadSubject();
        drpPaper.Items.Clear();
        drpPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        GridView2.DataSource = null;
        GridView2.DataBind();
        GridView2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
    }
   
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "SubjectwiseCumlativeItoV", table1);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "SubjectwiseCumlativeItoV", table1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "SubjectwiseCumlativeItoV.xls", table1, Server.MapPath("~/Admin/css/style.css"));
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