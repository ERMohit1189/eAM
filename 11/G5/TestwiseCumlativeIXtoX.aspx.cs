using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using c4SmsNew;
using System.Threading;

public partial class TestwiseCumlativeIXtoX : Page
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
        else if (Session["Logintype"].ToString() != "Staff")
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
        Campus oo = new Campus(); oo.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Examination", header);
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            drptest.Items.Insert(0, new ListItem("U. T. I", "Test1"));
            drptest.Items.Insert(1, new ListItem("U. T. II", "Test2"));
            drptest.Items.Insert(2, new ListItem("H. Y. E.(Theory)", "HY"));
            drptest.Items.Insert(3, new ListItem("H. Y. E.(Prac)", "Prac"));
        }
    }
    protected void LoadTests()
    {
        drptest.Items.Clear();
        if (drpEval.SelectedIndex == 0 && drpclass.SelectedItem.Text.ToUpper() == "IX")
        {
            drptest.Items.Insert(0, new ListItem("U. T. I", "Test1"));
            drptest.Items.Insert(1, new ListItem("U. T. II", "Test2"));
            drptest.Items.Insert(2, new ListItem("H. Y. E.", "HY"));
            drptest.Items.Insert(3, new ListItem("PRACTICAL", "Prac"));
        }
        else if (drpEval.SelectedIndex == 1 && drpclass.SelectedItem.Text.ToUpper() == "IX")
        {
            drptest.Items.Insert(0, new ListItem("U. T. III", "Test3"));
            drptest.Items.Insert(1, new ListItem("U. T. IV", "Test4"));
            drptest.Items.Insert(2, new ListItem("A. E.", "AE"));
        }
        else if (drpEval.SelectedIndex == 0 && drpclass.SelectedItem.Text.ToUpper() == "X")
        {
            drptest.Items.Insert(0, new ListItem("U. T. I", "Test1"));
            drptest.Items.Insert(1, new ListItem("U. T. II", "Test2"));
            drptest.Items.Insert(2, new ListItem("H. Y. E.", "HY"));
            drptest.Items.Insert(3, new ListItem("PRACTICAL", "Prac"));
        }
        else if (drpEval.SelectedIndex == 1 && drpclass.SelectedItem.Text.ToUpper() == "X")
        {
            drptest.Items.Insert(0, new ListItem("Prelims.", "Test3"));
            drptest.Items.Insert(1, new ListItem("PREBOARD-1", "AE"));
            drptest.Items.Insert(2, new ListItem("PREBOARD-1", "AE2"));
            drptest.Items.Insert(3, new ListItem("PRACTICAL", "Prac"));
        }
    }

    protected void BindMonth()
    {
        sql = "Select Month,Convert(Varchar(11),id,106) StartingDate From(Select Distinct Left(DateName(MONTH, StartingDate), 3) Month,StartingDate Id from MonthMaster Where SessionName = '" + Session["SessionName"].ToString() + "')T1 order by Id Asc";

        BAL.objBal.FillDropDown_withValue(sql, drpMonth, "Month", "StartingDate");

        drpMonth.Items.Insert(0, new ListItem("<--Select-->", "<--Select-->"));
    }

    public void setTitle()
    {
        lblClass.Text = drpclass.SelectedItem.Text.Trim();
        lblSection.Text = drpsection.SelectedItem.Text.Trim();
        lblEval.Text = drpEval.SelectedItem.Text.Trim();
        lblTest.Text = drptest.SelectedItem.Text.Trim();
        lblMonthss.Text = drpMonth.SelectedItem.Text.Trim();
    }


    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName  and cm.BranchCode=t1.BranchCode";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G5' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {

            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName and cm.BranchCode=sctm.BranchCode";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId and t1.SessionName=sctm.SessionName and t1.BranchCode=sctm.BranchCode";
            sql +=  " where GroupId='G5'  and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }
    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        else
        {
            sql = "Select Distinct sm.SectionName,sm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.id=sctm.sectionid and sm.SessionName=sctm.SessionName   and sm.BranchCode=sctm.BranchCode ";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTests();
        loadsection();
    }

    public void loadSubjects()
    {
        sql = "Select sm.id Paperid, SubjectId, PaperName SubjectName,'' marks from ttpaperMaster  sm";
        sql +=  " where sm.ClassId = '" + drpclass.SelectedValue.ToString() + "' and sm.SessionName='" + Session["SessionName"] + "'  and sm.BranchCode=" + Session["BranchCode"] + "";
        sql +=  " order by sm.id Asc";

        DataTable dt;
        dt = BAL.objBal.Fetchdata(sql);

        rptSubject.DataSource = dt;
        rptSubject.DataBind();
    }

    public void loadStudents()
    {
        sql = "select name StudentName,srno, FamilyContactNo contactno, FirstName,DateOfAdmiission,InstituteRollNo  from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") ";
        sql +=  "   where SectionId='" + drpsection.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   BranchCode='" + Session["BranchCode"].ToString() + "'";
        sql +=  "   and Withdrwal is null and isnull(Promotion, '')<>'Cancelled' and ClassId='" + drpclass.SelectedValue.ToString() + "' ";

        if (ddlOrder.SelectedValue == "Alphabetical")
        {
            sql +=  "  order by FirstName";
        }
        if (ddlOrder.SelectedValue == "Sequential")
        {
            sql +=  "  order by DateOfAdmiission asc";
        }
        if (ddlOrder.SelectedValue == "RollNoWise")
        {
            sql +=  "  order by InstituteRollNo asc";
        }
        rptStudents.DataSource = BAL.objBal.GridFill(sql);
        rptStudents.DataBind();

        for (int i = 0; i < rptStudents.Items.Count; i++)
        {
            Repeater rptMarks = (Repeater)rptStudents.Items[i].FindControl("rptMarks");
            Label lblMonth = (Label)rptStudents.Items[i].FindControl("lblMonth");
            lblMonth.Text = drpMonth.SelectedValue;
            sql = "Select sm.id Paperid, SubjectId, PaperName SubjectName,'' marks from ttpaperMaster  sm";
            sql +=  " where sm.ClassId = '" + drpclass.SelectedValue.ToString() + "' and sm.SessionName='" + Session["SessionName"] + "'  and sm.BranchCode=" + Session["BranchCode"] + "";
            sql +=  "  order by sm.id Asc";

            DataTable dt;
            dt = BAL.objBal.Fetchdata(sql);
            if (dt.Rows.Count > 0)
            {
                divList.Visible = true;
            }
            else
            {
                divList.Visible = false;
            }
            rptMarks.DataSource = dt;
            rptMarks.DataBind();
        }
    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTests();
        loadSubjects();
        loadStudents();
        SetMarks();
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTests();
        SetMarks();
    }

    public void SetMarks()
    {
        try
        {


            if (rptSubject.Items.Count > 0)
            {
                setTitle();
            }
            for (int i = 0; i < rptSubject.Items.Count; i++)
            {
                Label lblSubjectId = (Label)rptSubject.Items[i].FindControl("lblSubjectId");
                Label lblpaperid = (Label)rptSubject.Items[i].FindControl("lblpaperid");
                string mmmarks;
                mmmarks = GetMMarks(drpEval.SelectedValue.ToString(), lblSubjectId.Text.Trim(), lblpaperid.Text.Trim());

                Label lblMM1 = (Label)rptSubject.Items[i].FindControl("lblMM1");

                lblMM1.Text = mmmarks;

                for (int j = 0; j < rptStudents.Items.Count; j++)
                {
                    string testmarks;

                    Label lblsrno = (Label)rptStudents.Items[j].FindControl("lblsrno");
                    Repeater rptMarks = (Repeater)rptStudents.Items[j].FindControl("rptMarks");

                    testmarks = GetTestMarks(drpEval.SelectedValue.ToString(), lblSubjectId.Text.Trim(), lblsrno.Text.Trim(), lblpaperid.Text.Trim());

                    Label lblM1 = (Label)rptMarks.Items[i].FindControl("lblM1");
                    lblM1.Text = testmarks.ToString();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public string GetMMarks(string Eval, string subjectid, string paperid)
    {
        string mmt1 = "";
        try
        {


            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@Subjectid", subjectid));
            param.Add(new SqlParameter("@PaperId", paperid));
            param.Add(new SqlParameter("@ClassName", drpclass.SelectedItem.Text));
            param.Add(new SqlParameter("@Evaluation", Eval));
            param.Add(new SqlParameter("@Test", drptest.SelectedValue.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds = new DataSet();

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("TestwiseCumlativeMMMarksIXToX_2021", param);

            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    mmt1 = dt.Rows[0][0].ToString();
                }
            }
        }
        catch (Exception)
        {
        }

        return mmt1;
    }


    public string GetTestMarks(string Eval, string subjectid, string srno, string paperid)
    {
        string obtmarks = "";
        try
        {


            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@Classid", drpclass.SelectedValue.ToString()));
            param.Add(new SqlParameter("@Subjectid", subjectid));
            param.Add(new SqlParameter("@PaperId", paperid));
            param.Add(new SqlParameter("@ClassName", drpclass.SelectedItem.Text));
            param.Add(new SqlParameter("@SectionName", drpsection.SelectedItem.Text));
            param.Add(new SqlParameter("@Evaluation", Eval));
            param.Add(new SqlParameter("@Test", drptest.SelectedValue.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@Srno", srno));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("TestwiseCumlativeMarksIXToX_2021", param);

            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    obtmarks = dt.Rows[0][0].ToString();
                }
            }
        }
        catch (Exception)
        {
        }
        return obtmarks;
    }
    protected void lnkWord_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "ClassCumulative" + ".doc", divExport);
    }
    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "ClassCumulative", divExport, Server.MapPath("~/css/theme.min.css"));
    }
    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "ClassCumulative", divExport);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void drptest_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMarks();
    }

    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMarks();
    }
    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjects();
        loadStudents();
        SetMarks();
    }
}