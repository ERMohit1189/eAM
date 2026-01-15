using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_MarksEntryReportXII_1718 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    Campus oo = new Campus();
    string sql = "";
    private decimal term1Avg = 0;
    private decimal term2Avg = 0;
    int totalStudents = 0;
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
            loadBranch();
            loadsection();
            loadSubjectGroup();

        }
    }

    public string getSubjectTeacherName()
    {
        sql = "Select Name EmpName from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where EmpId=(Select EmpId from ICSESubjectTeacherAllotment where classid=" + drpclass.SelectedValue.ToString() + " and branchid=" + drpBranch.SelectedValue.ToString() + " and Sectionid=" + drpsection.SelectedValue.ToString() + " and Subjectid=" + drpSubjectGroup.SelectedValue.ToString() + " and paperid=" + drpSubject.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "')";
        return BAL.objBal.ReturnTag(sql, "EmpName");
    }
    public void createTitle()
    {
        LblSession.Text = Session["SessionName"].ToString();
        lblClass.Text = drpclass.SelectedItem.Text.ToString();
        lblSection.Text = drpsection.SelectedItem.Text.ToString();
        lblSubjectTeacherName.Text = getSubjectTeacherName();
        lblSubject.Text = drpSubject.SelectedItem.Text.ToString();
        lblDate.Text = BAL.objBal.CurrentDate();
    }

    public bool checkIsCompulsory()
    {
        bool flag;
        sql = "Select ISNULL(SubjectType,'')  IsCompulsory from TTSubjectMaster where id='" + drpSubjectGroup.SelectedValue + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        flag = oo.ReturnTag(sql, "IsCompulsory") == "Compulsory" ? true : false;
        return flag;
    }
    public void loadgrid()
    {

        sql = " Select asr.SrNo,Name,FatherName from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") asr ";
        sql +=  " where asr.ClassId='" + drpclass.SelectedValue.ToString() + "'  and asr.BranchId='" + drpBranch.SelectedValue.ToString() + "'";
        sql +=  " and asr.SectionId='" + drpsection.SelectedValue.ToString() + "' and Withdrwal is null and isnull(asr.Promotion, '')<>'Cancelled' order by Name Asc";

        if (drpEval.SelectedIndex != 0)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                Label lblgrd1MMT1 = (Label)GridView1.HeaderRow.FindControl("lblMMT1");
                Label lblgrd1MMT2 = (Label)GridView1.HeaderRow.FindControl("lblMMT2");
                Label lblgrd1MMHY = (Label)GridView1.HeaderRow.FindControl("lblMMHY");

                lblgrd1MMT1.Text = string.Empty;
                lblgrd1MMT2.Text = string.Empty;

                lblgrd1MMHY.Text = string.Empty;
            }
        }
        else
        {
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();

            GridView1.DataSource = null;
            GridView1.DataBind();

            if (GridView2.Rows.Count > 0)
            {
                Label lblMMT1 = (Label)GridView2.HeaderRow.FindControl("lblMMT1");
                Label lblMMT2 = (Label)GridView2.HeaderRow.FindControl("lblMMT2");
                Label lblMMHY = (Label)GridView2.HeaderRow.FindControl("lblMMHY");

                Label lblMMT4 = (Label)GridView2.HeaderRow.FindControl("lblMMT4");
                Label lblMMT5 = (Label)GridView2.HeaderRow.FindControl("lblMMT5");
                Label lblMMHY2 = (Label)GridView2.HeaderRow.FindControl("lblMMHY2");

                lblMMT4.Text = string.Empty;
                lblMMT5.Text = string.Empty;

                lblMMHY2.Text = string.Empty;
            }
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G7' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
            sql +=  " where GroupId='G7'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }
    private void loadBranch()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select BranchName,Id from BranchMaster where ClassId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and   BranchCode=" + Session["BranchCode"].ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
        else
        {
            sql = "Select Distinct bm.BranchName,bm.id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join BranchMaster bm on bm.Id=sctm.Branchid and bm.ClassId=sctm.ClassId and bm.SessionName=sctm.SessionName";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and bm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "' and bm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "' order by bm.id";
            oo.FillDropDown_withValue(sql, drpBranch, "BranchName", "Id");
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", "0"));
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
            sql +=  " and sm.SessionName=sctm.SessionName and sm.BranchCode=sctm.BranchCode";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
        }
    }


    private void loadSubjectGroup()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from ttSubjectMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and BranchId=" + drpBranch.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' and ApplicableFor<>'TimeTable'";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectName", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct SubjectName,sm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId  and sm.BranchId=sctm.BranchId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " and sctm.BranchId=" + drpBranch.SelectedValue.ToString() + " and sctm.SectionId=" + drpsection.SelectedValue.ToString() + " ";
            sql +=  " and sctm.BranchId=" + drpBranch.SelectedValue.ToString() + " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectName", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select PaperName,Id from ttpapermaster where SubjectId='" + drpSubjectGroup.SelectedValue.ToString() + "' and Classid=" + drpclass.SelectedValue.ToString() + " and BranchId=" + drpBranch.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id";

            oo.FillDropDown_withValue(sql, drpSubject, "PaperName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct pm.PaperName,pm.Id from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join TTSubjectMaster sm on sm.Id=sctm.Subjectid and sm.ClassId=sctm.ClassId and sm.BranchId=sctm.BranchId and sm.SessionName=sctm.SessionName  and sm.BranchCode=sctm.BranchCode";
            sql +=  " inner join ttpapermaster pm on pm.SubjectId=sm.id and pm.ClassId=sctm.ClassId and pm.BranchId=sctm.BranchId and pm.SessionName=sctm.SessionName  and pm.BranchCode=sctm.BranchCode";
            sql +=  " where Ecode='" + Session["LoginName"].ToString() + "'  and ApplicableFor<>'TimeTable' and sctm.ClassId=" + drpclass.SelectedValue.ToString() + " and sctm.BranchId=" + drpBranch.SelectedValue.ToString() + " and sctm.SectionId=" + drpsection.SelectedValue.ToString() + " and pm.SubjectId='" + drpSubjectGroup.SelectedValue.ToString() + "' ";
            sql +=  " and  sctm.BranchCode = " + Session["BranchCode"] + " and sctm.SessionName='" + Session["SessionName"].ToString() + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "PaperName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadSubject();
    }
    public string grade(double percentle)
    {
        if (percentle < 33)
        {
            return "E";
        }
        else if (percentle >= 33 && percentle < 41)
        {
            return "D";
        }
        else if (percentle >= 41 && percentle < 51)
        {
            return "C2";
        }
        else if (percentle >= 51 && percentle < 61)
        {
            return "C1";
        }
        else if (percentle >= 61 && percentle < 71)
        {
            return "B2";
        }
        else if (percentle >= 71 && percentle < 81)
        {
            return "B1";
        }
        else if (percentle >= 81 && percentle < 91)
        {
            return "A2";
        }
        else if (percentle >= 91 && percentle <= 100)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
        loadSubject();
    }
    private void loadSubjectMaxMarks(GridView grd, string Eval, string mmt1, string mmt2, string mmfinal, string mmprac)
    {
        sql = "Select MaxMarks1,MaxMarks2,MaxMarks3,MaxMarks4 from SetMaxMinMarks_XII where Eval='" + Eval + "' and BranchCode=" + Session["BranchCode"] + " and paperid='" + drpSubject.SelectedValue.ToString().Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string mm = oo.ReturnTag(sql, "MaxMarks");
        if (grd.Rows.Count > 0)
        {
            Label lblMMT1 = (Label)grd.HeaderRow.FindControl(mmt1);
            Label lblMMT2 = (Label)grd.HeaderRow.FindControl(mmt2);
            Label lblMMHY = (Label)grd.HeaderRow.FindControl(mmfinal);

            Label lblMMPrac = (Label)grd.HeaderRow.FindControl(mmprac);

            lblMMT1.Text = oo.ReturnTag(sql, "MaxMarks1");
            lblMMT2.Text = oo.ReturnTag(sql, "MaxMarks2");

            lblMMHY.Text = oo.ReturnTag(sql, "MaxMarks3");
            lblMMPrac.Text = oo.ReturnTag(sql, "MaxMarks4");
        }
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpSubject.SelectedIndex == 0)
        {
            divExport.Visible = false;
            GridView2.Visible = false;
            GridView1.Visible = false;

        }
        else
        {
            divExport.Visible = true;
            createTitle();
            loadgrid();
            hideColumn();
            if (drpEval.SelectedIndex != 0)
            {
                GridView2.Visible = false;
                GridView1.Visible = true;
                loadMarks(GridView1, drpEval.SelectedValue.ToString(), "lblT1", "lblT2", "lblHY", "lblHY2", "lblPrac", "Label2", "Label3", "lblTotal", "lblGrade", "lblMMT1", "lblMMT2", "lblMMHY", "lblMMHY2", "lblMMPrac", "lblConinBoardMarks", "lblGTotal1");
            }
            else
            {
                GridView1.Visible = false;
                GridView2.Visible = true;
                loadMarks(GridView2, "TERM1", "lblT1", "lblT2", "lblHY", "lblHY22", "lblPracMarks", "Label2", "Label3", "lblTotal", "lblGrade", "lblMMT1", "lblMMT2", "lblMMHY","", "lblPracMM", "lblConinBoardMarks", "lblGTotal1");
                loadMarks(GridView2, "TERM2", "lblT4", "lblT5", "lblHY2", "lblHY22", "lblPracMarks2", "lblBestofTwo", "lblConinten", "lblTotal2", "lblGrade2", "lblMMT4", "lblMMT5", "lblMMHY2", "lblMMHY22", "lblPracMM2", "lblConinBoardMarks2", "lblGTotal2");
            }
        }

    }
    protected void loadMarks(GridView grd, string Eval, string test1, string test2, string hy, string hy2, string prac, string total, string tenper, string finaltotal, string grademarks, string mmt1, string mmt2, string mmhy, string mmhy2, string mmprac, string coninboard, string GTotal1)
    {
        if (grd.Rows.Count > 0)
        {
            loadSubjectMaxMarks(grd, Eval, mmt1, mmt2, mmhy, mmprac);
            foreach (GridViewRow gvr in grd.Rows)
            {
                Label lblsrno = (Label)gvr.FindControl("Label16");

                Label lblT1 = (Label)gvr.FindControl(test1);
                Label lblT2 = (Label)gvr.FindControl(test2);
                Label lblPrac = (Label)gvr.FindControl(prac);
                Label lblHY = (Label)gvr.FindControl(hy);
                Label lblHY2 = (Label)gvr.FindControl(hy2);


                sql = "Select Test1,Test2,SAT,SAT2,Prac from CCEXII_1718 where SRNO='" + lblsrno.Text + "' and Evaluation='" + Eval + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' and paperid='" + drpSubject.SelectedValue.ToString() + "'";


                lblT1.Text = oo.ReturnTag(sql, "Test1").ToString();
                lblT2.Text = oo.ReturnTag(sql, "Test2").ToString();
                lblHY.Text = oo.ReturnTag(sql, "SAT").ToString();
                lblHY2.Text = oo.ReturnTag(sql, "SAT2").ToString();
                lblPrac.Text = oo.ReturnTag(sql, "Prac").ToString();

            }
            refresh(grd, Eval, test1, test2, hy, hy2, prac, mmt1, mmt2, mmhy, mmhy2, mmprac, total, tenper, finaltotal, grademarks, coninboard, GTotal1);
            createTitle();
        }

    }
    private void refresh(GridView grd, string Eval, string test1, string test2, string hy, string hy2, string prac, string mmt1, string mmt2, string mmhy, string mmhy2, string mmprac, string total, string tenper, string finaltotal, string grademarks, string coninboard, string GTotal1)
    {
        if (grd.Rows.Count > 0)
        {
            Label lblMMT1 = (Label)grd.HeaderRow.FindControl(mmt1);
            Label lblMMT2 = (Label)grd.HeaderRow.FindControl(mmt2);
            Label lblMMHY = (Label)grd.HeaderRow.FindControl(mmhy);
            Label lblMMHY2 = (Label)grd.HeaderRow.FindControl(mmhy2);

            Label lblMMPrac = (Label)grd.HeaderRow.FindControl(mmprac);


            for (int i = 0; i < grd.Rows.Count; i++)
            {
                int mm1 = 0; int mm2 = 0; int maxmark;
                int mm5 = 0; int mm6 = 0; int mm7 = 0;

                Label Label2 = (Label)grd.Rows[i].FindControl(total);
                Label Label3 = (Label)grd.Rows[i].FindControl(tenper);
                Label lblGrade = (Label)grd.Rows[i].FindControl(grademarks);

                Label lblGTotal = (Label)grd.Rows[i].FindControl("lnlGTotal");
                Label lblGGrade = (Label)grd.Rows[i].FindControl("lblGGrade");

                Label lblT1 = (Label)grd.Rows[i].FindControl(test1);
                Label lblT2 = (Label)grd.Rows[i].FindControl(test2);
                Label lblHY = (Label)grd.Rows[i].FindControl(hy);
                Label lblHY2 = (Label)grd.Rows[i].FindControl(hy2);
                Label lblPracMarks = (Label)grd.Rows[i].FindControl(prac);

                Label lblConvinBoard = (Label)grd.Rows[i].FindControl(coninboard);
                Label lblGTotal1 = (Label)grd.Rows[i].FindControl(GTotal1);

                double num1 = 0, num2 = 0, num5 = 0, num6 = 0, num7 = 0;
                bool result;
                if (lblT1.Text.ToUpper() == "NAD" || lblT1.Text.ToUpper() == "ML" || lblT1.Text == "")
                {

                }
                else if (lblT1.Text != "")
                {
                    result = double.TryParse(lblT1.Text.Trim(), out num1);
                    if (result == true)
                    {
                        num1 = Convert.ToDouble(lblT1.Text.Trim());
                    }
                    else
                    {

                        num1 = 0;

                    }
                    mm1 = int.TryParse(lblMMT1.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num1 = 0;
                    mm1 = int.TryParse(lblMMT1.Text.Trim(), out maxmark) ? maxmark : 0;
                }


                if (lblT2.Text.ToUpper() == "NAD" || lblT2.Text.ToUpper() == "ML" || lblT2.Text == "")
                {

                }
                else if (lblT2.Text != "")
                {
                    result = double.TryParse(lblT2.Text.Trim(), out num2);
                    if (result == true)
                    {
                        num2 = Convert.ToDouble(lblT2.Text.Trim());
                    }
                    else
                    {

                        num2 = 0;

                    }
                    mm2 = int.TryParse(lblMMT2.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num2 = 0;
                    mm2 = int.TryParse(lblMMT2.Text.Trim(), out maxmark) ? maxmark : 0;
                }


                if (lblHY.Text.ToUpper() == "NAD" || lblHY.Text.ToUpper() == "ML" || lblHY.Text == "")
                {

                }
                else if (lblHY.Text != "")
                {
                    result = double.TryParse(lblHY.Text.Trim(), out num5);
                    if (result == true)
                    {
                        num5 = Convert.ToDouble(lblHY.Text.Trim());
                    }
                    else
                    {
                        num5 = 0;
                    }
                    mm5 = int.TryParse(lblMMHY.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num5 = 0;
                    mm5 = int.TryParse(lblMMHY.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                if (lblHY2.Text.ToUpper() == "NAD" || lblHY2.Text.ToUpper() == "ML" || lblHY2.Text == "")
                {

                }
                else if (lblHY2.Text != "")
                {
                    result = double.TryParse(lblHY2.Text.Trim(), out num6);
                    if (result == true)
                    {
                        num6 = Convert.ToDouble(lblHY2.Text.Trim());
                    }
                    else
                    {
                        num6 = 0;
                    }
                    mm6 = int.TryParse(lblMMHY2.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num6 = 0;
                    mm6 = int.TryParse(lblMMHY2.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                if (lblPracMarks.Text.ToUpper() == "NAD" || lblPracMarks.Text.ToUpper() == "ML" || lblPracMarks.Text == "")
                {

                }
                else if (lblPracMarks.Text != "")
                {
                    result = double.TryParse(lblPracMarks.Text.Trim(), out num7);
                    if (result == true)
                    {
                        num7 = Convert.ToDouble(lblPracMarks.Text.Trim());
                    }
                    else
                    {
                        num7 = 0;
                    }
                    mm7 = int.TryParse(lblMMPrac.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num7 = 0;
                    mm7 = int.TryParse(lblMMPrac.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                double percentle = 0;
                if ((lblT1.Text.ToUpper() == "NAD" || lblT1.Text.ToUpper() == "ML" || lblT1.Text == "") && (lblT2.Text.ToUpper() == "NAD" || lblT2.Text.ToUpper() == "ML" || lblT2.Text == ""))
                {
                    if (lblT1.Text == "" && lblT2.Text == "")
                    {
                        Label3.Text = "";
                        Label2.Text = "";
                        lblGGrade.Text = "";
                    }
                    else
                    {
                        Label3.Text = "NP";
                        Label2.Text = "NP";
                        lblGGrade.Text = "NP";
                    }
                }
                else
                {
                    double sum1 = 0;
                    double maxsum1 = 0;
                    double sum2 = 0;
                    double maxsum2 = 0;
                    double totalmarks = 0;
                    double totalmmmarks = 0;
                    if (Eval == "term1")
                    {
                       sum1 = num1;
                       maxsum1 = mm1;
                       sum2 = num2;
                       maxsum2 = mm2;
                       totalmarks = 0;
                       totalmmmarks = 0;
                        totalmarks = sum1 > sum2 ? sum1 : sum2;
                        totalmmmarks = sum1 > sum2 ? maxsum1 : maxsum2;
                    }
                    else
                    {
                        sum1 = num1;
                        maxsum1 = mm1;
                        totalmarks = 0;
                        totalmmmarks = 0;
                        totalmarks = sum1 ;
                        totalmmmarks = maxsum1;
                    }

                    Label2.Text = (totalmarks).ToString();

                    if (totalmmmarks == 0)
                    {
                        Label3.Text = "0";
                    }
                    else
                    {
                        percentle = ((totalmarks) * 10) / totalmmmarks;
                        Label3.Text = (Math.Round(percentle, 1)).ToString();
                    }

                }


                Label lblTotal = (Label)grd.Rows[i].FindControl(finaltotal);

                if ((Label3.Text.ToUpper() == "NP" || Label3.Text.ToUpper() == "NAD" || Label3.Text.ToUpper() == "ML" || Label3.Text == "") && (lblHY.Text.ToUpper() == "NAD" || lblHY.Text.ToUpper() == "ML" || lblHY.Text == ""))
                {
                    if (Label2.Text == "" && lblHY.Text == "")
                    {
                        lblTotal.Text = "";
                        lblGrade.Text = "";
                    }
                    else
                    {
                        lblTotal.Text = "NP";
                        lblGrade.Text = "NP";
                    }
                }
                else
                {
                    double nums = 0;
                    double mms = 0;
                    if (Eval == "term2")
                    {
                        nums = num5 > num6 ? num5 : num6;
                        mms = num5 > num6 ? mm5 : mm6;
                    }
                    else
                    {
                        nums = num5;
                        mms = mm5;
                    }
                    if ((Label3.Text.ToUpper() == "NP" || Label3.Text == "") && Eval == "term1")
                    {
                        lblTotal.Text = (double.Parse(percentle.ToString("0")) + num6).ToString("0");
                        lblGrade.Text = grade(percentle + num6);
                        double ConvinBoard = 0;
                        ConvinBoard = (((double.Parse(percentle.ToString("0")) + num6) / (10 + mm6)) * mm6);
                        lblConvinBoard.Text = ConvinBoard.ToString("0");
                        lblPracMarks.Text = num7.ToString("0");
                        lblGTotal1.Text = (double.Parse(ConvinBoard.ToString("0")) + double.Parse(num7.ToString("0"))).ToString("0");
                    }
                    else
                    {
                        lblTotal.Text = (double.Parse(percentle.ToString("0")) + nums).ToString("0");
                        lblGrade.Text = grade(percentle + nums);
                        double ConvinBoard = 0;
                        ConvinBoard = (((double.Parse(percentle.ToString("0")) + nums) / (10 + mms)) * mms);
                        lblConvinBoard.Text = ConvinBoard.ToString("0");
                        lblPracMarks.Text = num7.ToString("0");
                        lblGTotal1.Text = (double.Parse(ConvinBoard.ToString("0")) + double.Parse(num7.ToString("0"))).ToString("0");
                    }

                    double gtotal = 0;
                    double.TryParse(lblGTotal1.Text, out gtotal);
                    lblGGrade.Text = grade(gtotal / 2);
                }
            }
        }
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpEval.SelectedIndex != 0)
        {
            GridView2.Visible = false;
            GridView1.Visible = true;
        }
        else
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
        }
    }
    private void hideColumn()
    {
        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4 ";
        ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss +=  " where cm.id=" + drpclass.SelectedValue + " and bm.id=" + drpBranch.SelectedValue + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
        bool DisableTest1 = (drpEval.SelectedValue.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test1") == "No" ? false : true) : (oo.ReturnTag(ss, "Test4") == "No" ? false : true));
        bool DisableTest2 = (drpEval.SelectedValue.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test2") == "No" ? false : true) : (oo.ReturnTag(ss, "Test5") == "No" ? false : true));
        bool DisableTest1A = false;
        bool DisableTest2A = false;
        bool DisableTest3A = false;
        bool DisableTest4A = false;
        if (drpEval.SelectedValue.Trim().ToLower() == "all")
        {
            DisableTest1A = (oo.ReturnTag(ss, "Test1") == "No" ? false : true);
            DisableTest2A = (oo.ReturnTag(ss, "Test2") == "No" ? false : true);
            DisableTest3A = (oo.ReturnTag(ss, "Test3") == "No" ? false : true);
            DisableTest4A = (oo.ReturnTag(ss, "Test4") == "No" ? false : true);
        }

        if (drpEval.SelectedIndex != 0)
        {
            if (drpEval.SelectedIndex == 1)
            {
                Label lblHY = (Label)GridView1.HeaderRow.FindControl("lblHY");
                Label lblTest1 = (Label)GridView1.HeaderRow.FindControl("Label5");
                Label lblTest2 = (Label)GridView1.HeaderRow.FindControl("Label7");
                lblHY.Text = "H.Y.";
                lblTest1.Text = "TEST1";
                lblTest2.Text = "TEST2";
                GridView1.Columns[4].Visible = DisableTest1;
                GridView1.Columns[5].Visible = DisableTest2;
                GridView1.Columns[9].Visible = false;

            }
            else if (drpEval.SelectedIndex == 2)
            {
                Label lblHY = (Label)GridView1.HeaderRow.FindControl("lblHY");
                Label lblHY2 = (Label)GridView1.HeaderRow.FindControl("lblHY2");
                Label lblTest1 = (Label)GridView1.HeaderRow.FindControl("Label5");
                Label lblTest2 = (Label)GridView1.HeaderRow.FindControl("Label7");

                lblHY.Text = "Pre Board1";
                lblHY2.Text = "Pre Board2";
                lblTest1.Text = "TEST3";
                lblTest2.Text = "TEST4";
                GridView1.Columns[4].Visible = DisableTest1;
                GridView1.Columns[5].Visible = DisableTest2;
                GridView1.Columns[9].Visible = true;
            }
        }
        else
        {
            GridView2.Columns[4].Visible = DisableTest1A;
            GridView2.Columns[5].Visible = DisableTest2A;
            GridView2.Columns[6].Visible = DisableTest3A;
            GridView2.Columns[16].Visible = DisableTest4A;
        }
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        string totalStu = "select count(*) totalStu ";
        totalStu = totalStu + " from CCEXII_1718 m inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        totalStu = totalStu + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        totalStu = totalStu + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
        totalStu = totalStu + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
        totalStu = totalStu + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
        totalStu = totalStu + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
        totalStu = totalStu + " and isnull(Withdrwal,'') = '' ";
        totalStu = totalStu + " and isnull(blocked,'') = '') ";

        // double avgMarks = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));


        string optStu = "select count(*) optStu from ICSEOptionalSubjectAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and OptSubjectId=" + drpSubjectGroup.SelectedValue.ToString() + " and Srno in (select Srno from StudentOfficialDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId=" + drpsection.SelectedValue + " and Branch=" + drpBranch.SelectedValue + " and isnull(Withdrwal,'')='')";
        int optCount = int.Parse(oo.ReturnTag(optStu, "optStu"));
        if (optCount == 0)
        {
            optCount = int.Parse(oo.ReturnTag(totalStu, "totalStu").ToString());
        }

        totalStudents = optCount;

        string avg = "select isnull(TRY_CONVERT(decimal(10,0),T1.avgMarks),0) avgMarks from ( ";
        avg = avg + " select sum((((isnull(TRY_CONVERT(decimal(10,0),  (case when (isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1)> ";
        avg = avg + " (isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks2),1)  then (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20   ";
        avg = avg + " else  (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20 end)),0)  +isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)) ";
        avg = avg + " /(10+isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0))) ";
        avg = avg + " *isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0)) ";
        avg = avg + " + isnull(TRY_CONVERT(decimal(10,0),m.Prac),0)) avgMarks ";
        avg = avg + " from CCEXII_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode ";
        avg = avg + " inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        avg = avg + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        avg = avg + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "')T1 ";

    
        //double avgMarks = double.Parse((oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : oo.ReturnTag(avg, "avgMarks")));
        //term1Avg = Convert.ToDecimal(avgMarks / totalStudents);

        double avgMarks;
        if (!double.TryParse(oo.ReturnTag(avg, "avgMarks"), out avgMarks))
        {
            avgMarks = 0;
        }

        if (totalStudents > 0)
        {
            // decimal term1Avg = Convert.ToDecimal(avgMarks / totalStudents);
            term1Avg = (decimal)avgMarks / (decimal)totalStudents;
        }
        else
        {
            term1Avg = 0;
        }

        string avg1 = "select isnull(TRY_CONVERT(decimal(10,0),T1.avgMarks),0) avgMarks from ( ";
        avg1 = avg1 + " select isnull(sum(Ts.avgMarks1),0) avgMarks  from ( ";
        avg1 = avg1 + " select sum((((isnull(TRY_CONVERT(decimal(10,0),  (case when (isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1)> ";
        avg1 = avg1 + " (isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks2),1)  then (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20   ";
        avg1 = avg1 + " else  (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20 end)),0)  +isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)) ";
        avg1 = avg1 + " /(10+isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0))) ";
        avg1 = avg1 + " *isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0)) ";
        avg1 = avg1 + " + isnull(TRY_CONVERT(decimal(10,0),m.Prac),0)) avgMarks1 ";
        avg1 = avg1 + " from CCEXII_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode ";
        avg1 = avg1 + " inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        avg1 = avg1 + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        avg1 = avg1 + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' and m.Evaluation='TERM1' ";
        avg1 = avg1 + " union all ";
        avg1 = avg1 + " select sum((((((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*10)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1)) +(case when isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)>isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) then isnull(TRY_CONVERT(decimal(10,0),m.SAT),0) else isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) end)) ";
        avg1 = avg1 + " /(10+case when isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)>isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) then isnull(TRY_CONVERT(decimal(10,0), MaxMarks4),0) else isnull(TRY_CONVERT(decimal(10,0), MaxMarks5),0) end)) ";
        avg1 = avg1 + " *(case when isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)>isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) then isnull(TRY_CONVERT(decimal(10,0), MaxMarks4),0) else isnull(TRY_CONVERT(decimal(10,0), MaxMarks5),0) end)) ";
        avg1 = avg1 + " +isnull(TRY_CONVERT(decimal(10,0),m.Prac),0)) avgMarks1 ";
        avg1 = avg1 + " from CCEXII_1718 m  ";
        avg1 = avg1 + " inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode ";
        avg1 = avg1 + " inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName  ";
        avg1 = avg1 + " and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        avg1 = avg1 + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        avg1 = avg1 + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' and m.Evaluation='TERM2' ";
        avg1 = avg1 + " )Ts ";
        avg1 = avg1 + " )T1 ";


        double avgMarks2;
        if (!double.TryParse(oo.ReturnTag(avg1, "avgMarks"), out avgMarks2))
        {
            avgMarks2 = 0;
        }

        if (totalStudents > 0)
        {
            // decimal term2Avg = Convert.ToDecimal(avgMarks2 / totalStudents);
            term2Avg = (decimal)avgMarks2 / (decimal)totalStudents;
        }
        else
        {
            term2Avg = 0;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 5, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            //AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "Father's Name", System.Drawing.Color.LightGray.Name);
           // AddMergedCells(objgridviewrow, objtablecell, 16, drpEval.SelectedItem.Text.Trim(), System.Drawing.Color.LightGray.Name);
            if (drpEval.SelectedItem.Text.Trim() == "TERM 1")
            {
                AddMergedCells(objgridviewrow, objtablecell, 16, "TERM 1 (Average Mark: " + term1Avg.ToString("0.00") + ")", System.Drawing.Color.LightGray.Name);
            }
            else
            {
                AddMergedCells(objgridviewrow, objtablecell, 16, "TERM 2 (Average Mark: " + term2Avg.ToString("0.00") + ")", System.Drawing.Color.LightGray.Name);
            }
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 5, "EVALUATION2", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 5, "EVALUATION3", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "G.TOTAL", System.Drawing.Color.LightGray.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        string totalStu = "select count(*) totalStu ";
        totalStu = totalStu + " from CCEXII_1718 m inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        totalStu = totalStu + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        totalStu = totalStu + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' ";
        totalStu = totalStu + " and SrNo in (select SrNo from AllStudentRecord_UDF('" + Session["SessionName"] + "'," + Session["BranchCode"] + ") stu ";
        totalStu = totalStu + " where stu.sessionName='" + Session["SessionName"] + "' and isnull(stu.Promotion, '')<>'Cancelled' ";
        totalStu = totalStu + " and stu.ClassId=" + drpclass.SelectedValue + " and stu.SectionName='" + drpsection.SelectedItem.Text + "'  and BranchId=" + drpBranch.SelectedValue + " ";
        totalStu = totalStu + " and isnull(Withdrwal,'') = '' ";
        totalStu = totalStu + " and isnull(blocked,'') = '') ";

        // double avgMarks = double.Parse((_oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : _oo.ReturnTag(avg, "avgMarks")));


        string optStu = "select count(*) optStu from ICSEOptionalSubjectAllotment where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "  and OptSubjectId=" + drpSubjectGroup.SelectedValue.ToString() + " and Srno in (select Srno from StudentOfficialDetails where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and SectionId=" + drpsection.SelectedValue + " and Branch=" + drpBranch.SelectedValue + " and isnull(Withdrwal,'')='')";
        int optCount = int.Parse(oo.ReturnTag(optStu, "optStu"));
        if (optCount == 0)
        {
            optCount = int.Parse(oo.ReturnTag(totalStu, "totalStu").ToString());
        }

        totalStudents = optCount;

        string avg = "select isnull(TRY_CONVERT(decimal(10,0),T1.avgMarks),0) avgMarks from ( ";
        avg = avg + " select sum((((isnull(TRY_CONVERT(decimal(10,0),  (case when (isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1)> ";
        avg = avg + " (isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks2),1)  then (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20   ";
        avg = avg + " else  (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20 end)),0)  +isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)) ";
        avg = avg + " /(10+isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0))) ";
        avg = avg + " *isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0)) ";
        avg = avg + " + isnull(TRY_CONVERT(decimal(10,0),m.Prac),0)) avgMarks ";
        avg = avg + " from CCEXII_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode ";
        avg = avg + " inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        avg = avg + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.Evaluation='TERM1' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        avg = avg + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "')T1 ";


        //double avgMarks = double.Parse((oo.ReturnTag(avg, "avgMarks").ToString() == "" ? "0" : oo.ReturnTag(avg, "avgMarks")));
        //term1Avg = Convert.ToDecimal(avgMarks / totalStudents);

        double avgMarks;
        if (!double.TryParse(oo.ReturnTag(avg, "avgMarks"), out avgMarks))
        {
            avgMarks = 0;
        }

        if (totalStudents > 0)
        {
            // decimal term1Avg = Convert.ToDecimal(avgMarks / totalStudents);
            term1Avg = (decimal)avgMarks / (decimal)totalStudents;
        }
        else
        {
            term1Avg = 0;
        }

        string avg1 = "select isnull(TRY_CONVERT(decimal(10,0),T1.avgMarks),0) avgMarks from ( ";
        avg1 = avg1 + " select isnull(sum(Ts.avgMarks1),0) avgMarks  from ( ";
        avg1 = avg1 + " select sum((((isnull(TRY_CONVERT(decimal(10,0),  (case when (isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1)> ";
        avg1 = avg1 + " (isnull(TRY_CONVERT(decimal(10,0),m.Test2),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks2),1)  then (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20   ";
        avg1 = avg1 + " else  (((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*20)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1))*10)/20 end)),0)  +isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)) ";
        avg1 = avg1 + " /(10+isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0))) ";
        avg1 = avg1 + " *isnull(TRY_CONVERT(decimal(10,0), MaxMarks3),0)) ";
        avg1 = avg1 + " + isnull(TRY_CONVERT(decimal(10,0),m.Prac),0)) avgMarks1 ";
        avg1 = avg1 + " from CCEXII_1718 m inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode ";
        avg1 = avg1 + " inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        avg1 = avg1 + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        avg1 = avg1 + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' and m.Evaluation='TERM1' ";
        avg1 = avg1 + " union all ";
        avg1 = avg1 + " select sum((((((isnull(TRY_CONVERT(decimal(10,0),m.Test1),0)*10)/isnull(TRY_CONVERT(decimal(10,0),mm.MaxMarks1),1)) +(case when isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)>isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) then isnull(TRY_CONVERT(decimal(10,0),m.SAT),0) else isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) end)) ";
        avg1 = avg1 + " /(10+case when isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)>isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) then isnull(TRY_CONVERT(decimal(10,0), MaxMarks4),0) else isnull(TRY_CONVERT(decimal(10,0), MaxMarks5),0) end)) ";
        avg1 = avg1 + " *(case when isnull(TRY_CONVERT(decimal(10,0),m.SAT),0)>isnull(TRY_CONVERT(decimal(10,0),m.SAT2),0) then isnull(TRY_CONVERT(decimal(10,0), MaxMarks4),0) else isnull(TRY_CONVERT(decimal(10,0), MaxMarks5),0) end)) ";
        avg1 = avg1 + " +isnull(TRY_CONVERT(decimal(10,0),m.Prac),0)) avgMarks1 ";
        avg1 = avg1 + " from CCEXII_1718 m  ";
        avg1 = avg1 + " inner join TTSubjectMaster sm on sm.Id=m.SubjectId and sm.Classid=m.ClassId and sm.SessionName=m.SessionName and sm.BranchCode=m.BranchCode ";
        avg1 = avg1 + " inner join SetMaxMinMarks_XII mm on mm.SubjectActivityId=m.SubjectId and m.PaperId=mm.PaperId and m.SessionName=mm.SessionName  ";
        avg1 = avg1 + " and m.BranchCode=mm.BranchCode and m.Evaluation=mm.Eval ";
        avg1 = avg1 + " where m.ClassId=" + drpclass.SelectedValue + " and m.SectionName='" + drpsection.SelectedItem.Text + "' and m.SubjectId=" + drpSubjectGroup.SelectedValue.ToString() + "  ";
        avg1 = avg1 + " and m.BranchCode=" + Session["BranchCode"] + " and m.SessionName='" + Session["SessionName"] + "' and m.Evaluation='TERM2' ";
        avg1 = avg1 + " )Ts ";
        avg1 = avg1 + " )T1 ";


        double avgMarks2;
        if (!double.TryParse(oo.ReturnTag(avg1, "avgMarks"), out avgMarks2))
        {
            avgMarks2 = 0;
        }

        if (totalStudents > 0)
        {
            // decimal term2Avg = Convert.ToDecimal(avgMarks2 / totalStudents);
            term2Avg = (decimal)avgMarks2 / (decimal)totalStudents;
        }
        else
        {
            term2Avg = 0;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 5, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 4, "", System.Drawing.Color.LightGray.Name);
          //  AddMergedCells(objgridviewrow, objtablecell, 12, "TERM 1", System.Drawing.Color.LightGray.Name);
           // AddMergedCells(objgridviewrow, objtablecell, 15, "TERM 2", System.Drawing.Color.LightGray.Name);

            AddMergedCells(objgridviewrow, objtablecell, 12, "TERM 1 (Average Mark: " + term1Avg.ToString("0.00") + ")", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 15, "TERM 2 (Average Mark: " + term2Avg.ToString("0.00") + ")", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 5, "EVALUATION2", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 5, "EVALUATION3", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "G.TOTAL", System.Drawing.Color.LightGray.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.CssClass = "vd_bg-blue vd_white text-center";
        //objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "SubjectwiseCumlativeXIII", table1);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "SubjectwiseCumlativeXIII", table1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        GridView1.Style.Add("text-transform", "uppercase");
        oo.ExportDivToExcelWithFormatting(Response, "SubjectwiseCumlativeXIII.xls", table1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {

        //if (GridView1.Rows.Count > 0)
        //{
        //    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}
        PrintHelper_New.ctrl = table1;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
    }
}