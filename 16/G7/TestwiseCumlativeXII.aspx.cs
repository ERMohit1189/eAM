using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using c4SmsNew;
using System.Threading;

public partial class TestwiseCumlativeXII : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    DataTable dtSubject;
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
            drptest.Items.Insert(0, new ListItem("<--Select-->", "0"));
           // drptest.Items.Insert(0, new ListItem("MonthlyTest", "MonthlyTest"));
            drptest.Items.Insert(1, new ListItem("UT-1", "Test1"));
            drptest.Items.Insert(2, new ListItem("UT-2", "Test2"));
            drptest.Items.Insert(3, new ListItem("UT-3", "Test3"));
            drptest.Items.Insert(4, new ListItem("HY", "HY"));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpStream.Items.Insert(0, new ListItem("<--Select-->", "0"));
            drpMedium.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }
    protected void LoadTests()
    {
        drptest.Items.Clear();
        if (drpEval.SelectedIndex == 0)
        {
            drptest.Items.Insert(0, new ListItem("<--Select-->", "0"));
            //drptest.Items.Insert(1, new ListItem("Monthly Test", "MonthlyTest"));
            drptest.Items.Insert(1, new ListItem("UT-1", "Test1"));
            drptest.Items.Insert(2, new ListItem("UT-2", "Test2"));
            drptest.Items.Insert(3, new ListItem("UT-3", "Test3"));
            drptest.Items.Insert(4, new ListItem("HY", "HY"));
        }
        else
        {
            drptest.Items.Insert(0, new ListItem("<--Select-->", "0"));
            //drptest.Items.Insert(1, new ListItem("Pre-1", "Pre1"));
            //drptest.Items.Insert(2, new ListItem("Pre-2", "Pre2"));
            //drptest.Items.Insert(3, new ListItem("Pre-3", "Pre3"));
            drptest.Items.Insert(1, new ListItem("UT-4", "Test4"));
            drptest.Items.Insert(2, new ListItem("UT-5", "Test5"));
            drptest.Items.Insert(3, new ListItem("UT-6", "Test6"));
            drptest.Items.Insert(4, new ListItem("AE", "AE"));
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and t1.SessionName=cm.SessionName  and cm.BranchCode=t1.BranchCode";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and GroupId='G7' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {

            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId and cm.SessionName=sctm.SessionName and cm.BranchCode=sctm.BranchCode";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId and t1.SessionName=sctm.SessionName and t1.BranchCode=sctm.BranchCode";
            sql +=  " where GroupId='G7'  and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.BranchCode=" + Session["BranchCode"] + " and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
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
    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and  BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        else
        {
            sql = "Select Distinct bm.BranchName,bm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join BranchMaster bm on bm.Id=sctm.Branchid and bm.ClassId=sctm.ClassId and bm.SessionName=sctm.SessionName  and bm.BranchCode=sctm.BranchCode";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and bm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "' order by bm.id";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }

    private void loadStream()
    {
        sql = "Select Stream,Id from StreamMaster where ClassId=" + drpclass.SelectedValue.ToString() + " and  branchid=" + drpBranch.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        oo.FillDropDown_withValue(sql, drpStream, "Stream", "Id");
        drpStream.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    private void loadMedium()
    {
        sql = "Select Medium,Id from MediumMaster where BranchCode=" + Session["BranchCode"].ToString() + " ";
        oo.FillDropDown_withValue(sql, drpMedium, "Medium", "Id");
        drpMedium.Items.Insert(0, new ListItem("<--Select-->", "0"));
    }
    public void setTitle()
    {
        lblClass.Text = (drpclass.SelectedItem.Text.Trim() + " " + drpBranch.SelectedItem.Text.Trim() + " (" + drpsection.SelectedItem.Text.Trim() + ")");
        lblEval.Text = drpEval.SelectedItem.Text.Trim();
        lblTest.Text = drptest.SelectedItem.Text.Trim();
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadBranch();
    }

    public void loadSubjects()
    {
        sql = "Select pm.id Paperid, SubjectId, papername SubjectName,'' marks,IsAditional from TTSubjectMaster  sm";
        sql +=  " inner join ttPapermaster pm on pm.subjectid = sm.id and pm.Classid=sm.Classid  and pm.BranchId=sm.BranchId and pm.SessionName=sm.SessionName  and pm.BranchCode=sm.BranchCode";
        sql +=  " and sm.Medium=pm.Medium where sm.ClassId = '" + drpclass.SelectedValue.ToString() + "'  and sm.branchid='" + drpBranch.SelectedValue.ToString() + "' and sm.SessionName='" + Session["SessionName"] + "'  and sm.BranchCode=" + Session["BranchCode"] + "";
        sql +=  " and sm.Medium='" + drpMedium.SelectedItem.Text + "' and sm.ApplicableFor<>'TimeTable'  order by pm.id Asc";



        dtSubject = BAL.objBal.Fetchdata(sql);
        rptSubject.DataSource = dtSubject;
        rptSubject.DataBind();
    }
    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStudents();
        loadSubjects();
        SetMarks();
    }
    public void loadStudents()
    {

        sql = "select name StudentName,srno, FamilyContactNo contactno, FirstName,DateOfAdmiission,InstituteRollNo  from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") ";
        sql +=  "   where SectionId='" + drpsection.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   BranchCode='" + Session["BranchCode"].ToString() + "' and BranchId='" + drpBranch.SelectedValue.ToString() + "'";
        if (drpStatus.SelectedValue != "0")
        {
            sql += "  and isnull(Withdrwal,'') = case when isnull('" + drpStatus.SelectedValue + "','')='B' or isnull('" + drpStatus.SelectedValue + "','')='' then isnull(Withdrwal,'') else case when isnull('" + drpStatus.SelectedValue + "','')='A' then '' else 'W' end end and isnull(blocked,'') = case when isnull('" + drpStatus.SelectedValue + "','')= 'W' or isnull('" + drpStatus.SelectedValue + "','')= '' then isnull(blocked,'') else case when isnull('" + drpStatus.SelectedValue + "','')= 'A' then '' else 'yes' end end";
        }
        sql +=  "    and isnull(Promotion, '')<>'Cancelled' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Medium='" + drpMedium.SelectedItem.Text + "'";

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
            sql = "Select pm.id Paperid, SubjectId, papername SubjectName,'' marks,IsAditional from TTSubjectMaster  sm";
            sql +=  " inner join ttPapermaster pm on pm.subjectid = sm.id and pm.Classid=sm.Classid  and pm.BranchId=sm.BranchId and pm.SessionName=sm.SessionName  and pm.BranchCode=sm.BranchCode";
            sql +=  " and sm.Medium=pm.Medium where sm.ClassId = '" + drpclass.SelectedValue.ToString() + "'  and sm.branchid='" + drpBranch.SelectedValue.ToString() + "' and sm.SessionName='" + Session["SessionName"] + "'  and sm.BranchCode=" + Session["BranchCode"] + "";
            sql +=  " and sm.Medium='" + drpMedium.SelectedItem.Text + "' and sm.ApplicableFor<>'TimeTable' order by pm.id Asc";

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
        loadBranch();
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

            for (int j = 0; j < rptStudents.Items.Count; j++)
            {
                for (int i = 0; i < dtSubject.Rows.Count; i++)
                {
                    Label lblSubjectId = (Label)rptSubject.Items[i].FindControl("lblSubjectId");
                    Label lblpaperid = (Label)rptSubject.Items[i].FindControl("lblpaperid");
                    Label IsAdditional = (Label)rptSubject.Items[i].FindControl("IsAdditional");
                    Label lblMM1 = (Label)rptSubject.Items[i].FindControl("lblMM1");
                    if (j == 0)
                    {
                        string mmmarks;
                        mmmarks = GetMMarks(lblSubjectId.Text.Trim(), lblpaperid.Text.Trim());
                        // Label lblMM1 = (Label)rptSubject.Items[i].FindControl("lblMM1");
                        // lblMM1.Text = mmmarks;
                        if (IsAdditional.Text.Trim() == "True")
                        {
                            if (mmmarks == null && mmmarks == "")
                            {
                                // lblMM1.Visible = false;
                            }
                        }
                        else
                        {
                            lblMM1.Text = "(" + mmmarks + ")";
                        }
                    }
                    Label lblsrno = (Label)rptStudents.Items[j].FindControl("lblsrno");
                    Repeater rptMarks = (Repeater)rptStudents.Items[j].FindControl("rptMarks");
                    string testmarks;
                    testmarks = GetTestMarks(lblsrno.Text.Trim(), lblSubjectId.Text.Trim(), lblpaperid.Text.Trim(), IsAdditional.Text.Trim());
                    Label lblM1 = (Label)rptMarks.Items[i].FindControl("lblM1");
                    lblM1.Text = testmarks.ToString();
                }

            }
        }
        catch (Exception)
        {
        }
    }

    public string GetMMarks(string subjectid, string paperid)
    {
        string mmt1 = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue));
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
            if (drpStream.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@StreamId", drpStream.SelectedValue));
            }
            param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue));
            param.Add(new SqlParameter("@Medium", drpMedium.SelectedItem.Text));
            param.Add(new SqlParameter("@Subjectid", subjectid));
            param.Add(new SqlParameter("@PaperId", paperid));
            param.Add(new SqlParameter("@Test", drptest.SelectedValue.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("TestwiseCumlativeUPEMXII", param);
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
    public string GetTestMarks(string srno, string subjectid, string paperid, string IsAdditional)
    {
        string obtmarks = "";
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Srno", srno));
            param.Add(new SqlParameter("@ClassId", drpclass.SelectedValue));
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
            if (drpStream.SelectedIndex != 0)
            {
                param.Add(new SqlParameter("@StreamId", drpStream.SelectedValue));
            }
            param.Add(new SqlParameter("@SectionId", drpsection.SelectedValue));
            param.Add(new SqlParameter("@Medium", drpMedium.SelectedItem.Text));
            param.Add(new SqlParameter("@Subjectid", subjectid));
            param.Add(new SqlParameter("@PaperId", paperid));
            param.Add(new SqlParameter("@Test", drptest.SelectedValue.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));
            param.Add(new SqlParameter("@IsAdditional", IsAdditional.ToString()));
            DataSet ds = new DataSet();

            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("TestwiseCumlativeUPEXIINew", param);

            if (ds != null)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    obtmarks = dt.Rows[0][0].ToString();
                    if (obtmarks == "-1")
                    {
                        obtmarks = "AB";
                    }
                    if (obtmarks == "-2")
                    {
                        obtmarks = "ML";
                    }
                    if (obtmarks == "-3")
                    {
                        obtmarks = "NAD";
                    }
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
        BAL.objBal.ExportTolandscapeWord(Response, "ClassCumulativeXII" + ".doc", divExport);
    }
    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "ClassCumulativeXII", divExport, Server.MapPath("~/css/theme.min.css"));
    }
    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "ClassCumulativeXII", divExport);
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


    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTests();
        loadMedium();
        loadStream();
    }
    protected void drpStream_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTests();
        loadMedium();
    }
    protected void drpMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTests();
    }
    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjects();
        loadStudents();
        SetMarks();
    }
    protected void drptest_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjects();
        loadStudents();
        SetMarks();
    }
}