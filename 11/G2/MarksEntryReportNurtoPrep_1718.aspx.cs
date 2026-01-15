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
        lblClass.Text = drpclass.SelectedItem.Text.ToString();
        lblSection.Text = drpsection.SelectedItem.Text.ToString();
        lblSubjectTeacherName.Text = getSubjectTeacherName();
        lblSubject.Text = drpSubject.SelectedItem.Text.ToString();
        lblDate.Text = BAL.objBal.CurrentDate();
    }
    public void loadgrid()
    {
        sql = "select   So.srno,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name,sf.FatherName from StudentGenaralDetail SG ";
        sql +=  "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "   where  CM.ClassName='" + drpclass.SelectedItem.ToString() + "'";
        sql +=  "   and SC.SectionName='" + drpsection.SelectedItem.ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   so.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and isnull(SO.Promotion, '')<>'Cancelled' order by FirstName Asc";

        if(drpEval.SelectedIndex!=0)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                Label lblgrd1MMT1 = (Label)GridView1.HeaderRow.FindControl("lblMMT1");
                Label lblgrd1MMT2 = (Label)GridView1.HeaderRow.FindControl("lblMMT2");
                Label lblgrd1MMNB = (Label)GridView1.HeaderRow.FindControl("lblMMNB");
                Label lblgrd1MMSE = (Label)GridView1.HeaderRow.FindControl("lblMMSE");
                Label lblgrd1MMHY = (Label)GridView1.HeaderRow.FindControl("lblMMHY");

                lblgrd1MMT1.Text = string.Empty;
                lblgrd1MMT2.Text = string.Empty;

                lblgrd1MMNB.Text = string.Empty;
                lblgrd1MMSE.Text = string.Empty;
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
                Label lblMMNB = (Label)GridView2.HeaderRow.FindControl("lblMMNB");
                Label lblMMSE = (Label)GridView2.HeaderRow.FindControl("lblMMSE");
                Label lblMMHY = (Label)GridView2.HeaderRow.FindControl("lblMMHY");

                Label lblMMT4 = (Label)GridView2.HeaderRow.FindControl("lblMMT4");
                Label lblMMT5 = (Label)GridView2.HeaderRow.FindControl("lblMMT5");
                Label lblMMNB2 = (Label)GridView2.HeaderRow.FindControl("lblMMNB2");
                Label lblMMSE2 = (Label)GridView2.HeaderRow.FindControl("lblMMSE2");
                Label lblMMHY2 = (Label)GridView2.HeaderRow.FindControl("lblMMHY2");

                lblMMT4.Text = string.Empty;
                lblMMT5.Text = string.Empty;

                lblMMNB2.Text = string.Empty;
                lblMMSE2.Text = string.Empty;
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
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G2' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
            sql +=  " where GroupId='G2'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
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
    private void loadSubjectMaxMarks(GridView grd, string Eval,string mmt1,string mmt2, string mmt3,string mmnb,string mmse,string mmfinal)
    {
        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
        ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss +=  " where cm.id=" + drpclass.SelectedValue + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
        string DisableTest1 = (Eval.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : ""));
        string DisableTest2 = (Eval.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : ""));
        string DisableTest3 = (Eval.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : ""));
        sql = "Select MaxMarks1,MaxMarks2,MaxMarks4,MaxMarks5,MaxMarks6 from SetMaxMinMarks_NurtoPrep where Eval='" + Eval + "' and BranchCode=" + Session["BranchCode"] + " and paperid='" + drpSubject.SelectedValue.ToString().Trim() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
        string mm = oo.ReturnTag(sql, "MaxMarks");
        if (grd.Rows.Count > 0)
        {
            Label lblMMT1 = (Label)grd.HeaderRow.FindControl(mmt1);
            Label lblMMT2 = (Label)grd.HeaderRow.FindControl(mmt2);
            Label lblMMT3 = (Label)grd.HeaderRow.FindControl(mmt3);
            Label lblMMNB = (Label)grd.HeaderRow.FindControl(mmnb);
            Label lblMMSE = (Label)grd.HeaderRow.FindControl(mmse);
            Label lblMMHY = (Label)grd.HeaderRow.FindControl(mmfinal);

            lblMMT1.Text = (DisableTest1 == "hide" ? "0" : oo.ReturnTag(sql, "MaxMarks1"));
            lblMMT2.Text = (DisableTest1 == "hide" ? "0" : oo.ReturnTag(sql, "MaxMarks2"));

            lblMMNB.Text = oo.ReturnTag(sql, "MaxMarks4");
            lblMMSE.Text = oo.ReturnTag(sql, "MaxMarks5");
            lblMMHY.Text = oo.ReturnTag(sql, "MaxMarks6");
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
                loadMarks(GridView1, drpEval.SelectedValue.ToString(), "lblT1", "lblT2", "lblT3", "lblNB", "lblSE", "lblHY", "Label2", "Label3", "lblTotal", "lblGrade", "lblMMT1", "lblMMT2", "lblMMT3", "lblMMNB", "lblMMSE", "lblMMHY");
            }
            else
            {
                GridView1.Visible = false;
                GridView2.Visible = true;
                loadMarks(GridView2, "TERM1", "lblT1", "lblT2", "lblT3", "lblNB", "lblSE", "lblHY", "Label2", "Label3", "lblTotal", "lblGrade", "lblMMT1", "lblMMT2", "lblMMT3", "lblMMNB", "lblMMSE", "lblMMHY");
                loadMarks(GridView2, "TERM2", "lblT4", "lblT5", "lblT6", "lblNB2", "lblSE2", "lblHY2", "lblBestofTwo", "lblConinten", "lblTotal2", "lblGrade2", "lblMMT4", "lblMMT5", "lblMMT6", "lblMMNB2", "lblMMSE2", "lblMMHY2");
            }
        }
    }
    protected void loadMarks(GridView grd,string Eval,string test1,string test2,string test3,string nb,string se,string final,string total,string tenper, string finaltotal, string grademarks, string mmt1, string mmt2, string mmt3, string mmnb, string mmse, string mmfinal)
    {
        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
        ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss +=  " where cm.id=" + drpclass.SelectedValue + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
        string DisableTest1 = (Eval.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : ""));
        string DisableTest2 = (Eval.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : ""));
        string DisableTest3 = (Eval.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "") : (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : ""));
        if (grd.Rows.Count > 0)
        {
            loadSubjectMaxMarks(grd, Eval, mmt1, mmt2, mmt3, mmnb, mmse, mmfinal);
            foreach (GridViewRow gvr in grd.Rows)
            {
                Label lblsrno = (Label)gvr.FindControl("Label16");

                Label lblT1 = (Label)gvr.FindControl(test1);
                Label lblT2 = (Label)gvr.FindControl(test2);
                Label lblT3 = (Label)gvr.FindControl(test3);
                Label lblNB = (Label)gvr.FindControl(nb);
                Label lblSE = (Label)gvr.FindControl(se);
                Label lblHY = (Label)gvr.FindControl(final);

                sql = "Select Test1,Test2,Test3,SAT,NB,SE from CCENurtoPrep_1718 where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and Evaluation='" + Eval + "' and Paperid='" + drpSubject.SelectedValue.ToString() + "'";


                lblT1.Text = (DisableTest1 == "hide" ? "0" : oo.ReturnTag(sql, "Test1"));
                lblT2.Text = (DisableTest1 == "hide" ? "0" : oo.ReturnTag(sql, "Test2"));
                lblT3.Text = (DisableTest1 == "hide" ? "0" : oo.ReturnTag(sql, "Test3"));

                lblNB.Text = oo.ReturnTag(sql, "NB");
                lblSE.Text = oo.ReturnTag(sql, "SE");
                lblHY.Text = oo.ReturnTag(sql, "SAT");
            }
            refresh(grd, Eval, test1, test2, test3, nb, se, final, mmt1, mmt2, mmt3, mmnb, mmse, mmfinal, total, tenper, finaltotal, grademarks);
            createTitle();
        }

    }
    private void refresh(GridView grd, string Eval, string test1, string test2, string test3, string nb, string se, string final,string mmt1, string mmt2, string mmt3, string mmnb, string mmse, string mmfinal, string total, string tenper, string finaltotal, string grademarks)
    {
        if (grd.Rows.Count > 0)
        {
            Label lblMMT1 = (Label)grd.HeaderRow.FindControl(mmt1);
            Label lblMMT2 = (Label)grd.HeaderRow.FindControl(mmt2);
            Label lblMMT3 = (Label)grd.HeaderRow.FindControl(mmt3);
            Label lblMMNB = (Label)grd.HeaderRow.FindControl(mmnb);
            Label lblMMSE = (Label)grd.HeaderRow.FindControl(mmse);
            Label lblMMHY = (Label)grd.HeaderRow.FindControl(mmfinal);
       
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                int mm1 = 0; int mm2 = 0; int mm3 = 0; int maxmark;
                int mm4 = 0; int mm5 = 0; int mm6 = 0;

                Label Label2 = (Label)grd.Rows[i].FindControl(total);
                Label Label3 = (Label)grd.Rows[i].FindControl(tenper);
                Label lblGrade = (Label)grd.Rows[i].FindControl(grademarks);

                Label lblGTotal = (Label)grd.Rows[i].FindControl("lnlGTotal");
                Label lblGGrade = (Label)grd.Rows[i].FindControl("lblGGrade");

                Label lblT1 = (Label)grd.Rows[i].FindControl(test1);
                Label lblT2 = (Label)grd.Rows[i].FindControl(test2);
                Label lblT3 = (Label)grd.Rows[i].FindControl(test3);
                Label lblNB = (Label)grd.Rows[i].FindControl(nb);
                Label lblSE = (Label)grd.Rows[i].FindControl(se);
                Label lblHY = (Label)grd.Rows[i].FindControl(final);

                double num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0, num6 = 0;
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

                if (lblT3.Text.ToUpper() == "NAD" || lblT3.Text.ToUpper() == "ML" || lblT3.Text == "")
                {

                }
                else if (lblT3.Text != "")
                {
                    result = double.TryParse(lblT3.Text.Trim(), out num3);
                    if (result == true)
                    {
                        num3 = Convert.ToDouble(lblT3.Text.Trim());
                    }
                    else
                    {

                        num3 = 0;

                    }
                    mm3 = int.TryParse(lblMMT3.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num3 = 0;
                    mm3 = int.TryParse(lblMMT3.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                if (lblNB.Text.ToUpper() == "NAD" || lblNB.Text.ToUpper() == "ML" || lblNB.Text == "")
                {

                }
                else if (lblNB.Text != "")
                {
                    result = double.TryParse(lblNB.Text.Trim(), out num4);
                    if (result == true)
                    {
                        num4 = Convert.ToDouble(lblNB.Text.Trim());
                    }
                    else
                    {
                        num4 = 0;
                    }
                    mm4 = int.TryParse(lblMMNB.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num4 = 0;
                    mm4 = int.TryParse(lblMMNB.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                if (lblSE.Text.ToUpper() == "NAD" || lblSE.Text.ToUpper() == "ML" || lblSE.Text == "")
                {

                }
                else if (lblSE.Text != "")
                {
                    result = double.TryParse(lblSE.Text.Trim(), out num5);
                    if (result == true)
                    {
                        num5 = Convert.ToDouble(lblSE.Text.Trim());
                    }
                    else
                    {
                        num5 = 0;
                    }
                    mm5 = int.TryParse(lblMMSE.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num5 = 0;
                    mm5 = int.TryParse(lblMMSE.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                if (lblHY.Text.ToUpper() == "NAD" || lblHY.Text.ToUpper() == "ML" || lblHY.Text == "")
                {

                }
                else if (lblHY.Text != "")
                {
                    result = double.TryParse(lblHY.Text.Trim(), out num6);
                    if (result == true)
                    {
                        num6 = Convert.ToDouble(lblHY.Text.Trim());
                    }
                    else
                    {
                        num6 = 0;
                    }
                    mm6 = int.TryParse(lblMMHY.Text.Trim(), out maxmark) ? maxmark : 0;
                }
                else
                {
                    num6 = 0;
                    mm6 = int.TryParse(lblMMHY.Text.Trim(), out maxmark) ? maxmark : 0;
                }

                double percentle = 0;
                bool isaddmmconinten = false;
                if ((lblT1.Text.ToUpper() == "NAD" || lblT1.Text.ToUpper() == "ML" || lblT1.Text == "") && (lblT2.Text.ToUpper() == "NAD" || lblT2.Text.ToUpper() == "ML" || lblT2.Text == "") && (lblT3.Text.ToUpper() == "NAD" || lblT3.Text.ToUpper() == "ML" || lblT3.Text == ""))
                {
                    if (lblT1.Text == "" && lblT2.Text == "" && lblT3.Text == "")
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

                    double totalmarks = num1 > num2 ? num1 : num2;
                    double totalmmmarks = num1 > num2 ?mm1 : mm2;
                    Label2.Text = (totalmarks).ToString(CultureInfo.InvariantCulture);
                    if (totalmarks == 0)
                    {
                        isaddmmconinten = true;
                    }
                    if (totalmmmarks == 0)
                    {
                        Label3.Text = "0";
                        isaddmmconinten = true;
                    }
                    else
                    {
                        percentle = ((totalmarks) * 5) / totalmmmarks;
                        Label3.Text = (Math.Round(percentle, 1)).ToString(CultureInfo.InvariantCulture);
                    }

                }


                Label lblTotal = (Label)grd.Rows[i].FindControl(finaltotal);

                if ((Label3.Text.ToUpper() == "NP" || Label3.Text.ToUpper() == "NAD" || Label3.Text.ToUpper() == "ML" || Label3.Text == "") && (lblNB.Text.ToUpper() == "NAD" || lblNB.Text.ToUpper() == "ML" || lblNB.Text == "") && (lblSE.Text.ToUpper() == "NAD" || lblSE.Text.ToUpper() == "ML" || lblSE.Text == "") && (lblHY.Text.ToUpper() == "NAD" || lblHY.Text.ToUpper() == "ML" || lblHY.Text == ""))
                {
                    if (Label2.Text == "" && lblNB.Text == "" && lblSE.Text == "" && lblHY.Text == "")
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
                    string ptm = (Math.Round(percentle, 0)).ToString("0");
                    percentle = double.Parse((Math.Round(double.Parse(ptm) + num4 + num5 + num6)).ToString(CultureInfo.CurrentCulture));
                    //percentle = Math.Round(((double.Parse(ptm) + num4 + num5 + num6) * 100) / (mm4 + mm5 + mm6 + (percentle > 0 ? 10 : isaddmmconinten ? 10 : 0)), 1);

                    lblTotal.Text = percentle.ToString(CultureInfo.CurrentCulture);

                    lblGrade.Text = grade(percentle);

                    double gtotal = 0;
                    double.TryParse(lblGTotal.Text, out gtotal);
                    lblGTotal.Text = (gtotal + percentle).ToString(CultureInfo.CurrentCulture);
                    double.TryParse(lblGTotal.Text, out gtotal);
                    lblGGrade.Text = lblGTotal.Text;// grade(Math.Round((gtotal / 2)));
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
        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
        ss +=  " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss +=  " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss +=  " where cm.id=" + drpclass.SelectedValue + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
        bool DisableTest1 = (drpEval.SelectedValue.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test1") == "No" ? false : true) : (oo.ReturnTag(ss, "Test4") == "No" ? false : true));
        bool DisableTest2 = (drpEval.SelectedValue.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test2") == "No" ? false : true) : (oo.ReturnTag(ss, "Test5") == "No" ? false : true));
        bool DisableTest3 = (drpEval.SelectedValue.Trim().ToLower() == "term1" ? (oo.ReturnTag(ss, "Test3") == "No" ? false : true) : (oo.ReturnTag(ss, "Test6") == "No" ? false : true));
        bool DisableTest1A=false;
        bool DisableTest2A=false;
        bool DisableTest3A=false;
        bool DisableTest4A=false;
        bool DisableTest5A=false;
        bool DisableTest6A = false;
        if (drpEval.SelectedValue.Trim().ToLower() == "all")
        {
             DisableTest1A = (oo.ReturnTag(ss, "Test1") == "No" ? false : true);
             DisableTest2A = (oo.ReturnTag(ss, "Test2") == "No" ? false : true);
             DisableTest3A = (oo.ReturnTag(ss, "Test3") == "No" ? false : true);
             DisableTest4A = (oo.ReturnTag(ss, "Test4") == "No" ? false : true);
             DisableTest5A = (oo.ReturnTag(ss, "Test5") == "No" ? false : true);
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
            }
            else if (drpEval.SelectedIndex == 2)
            {
                Label lblHY = (Label)GridView1.HeaderRow.FindControl("lblHY");
                Label lblTest1 = (Label)GridView1.HeaderRow.FindControl("Label5");
                Label lblTest2 = (Label)GridView1.HeaderRow.FindControl("Label7");

                lblHY.Text = "A.E.";
                lblTest1.Text = "TEST3";
                lblTest2.Text = "TEST4";
                GridView1.Columns[4].Visible = DisableTest1;
                GridView1.Columns[5].Visible = DisableTest2;
            }
        }
        else
        {
            GridView2.Columns[4].Visible = DisableTest1A;
            GridView2.Columns[5].Visible = DisableTest2A;
            GridView2.Columns[14].Visible = DisableTest4A;
            GridView2.Columns[15].Visible = DisableTest5A;
            GridView2.Columns[16].Visible = DisableTest6A;
        }
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
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
            AddMergedCells(objgridviewrow, objtablecell, 13, drpEval.SelectedItem.Text.Trim(), System.Drawing.Color.LightGray.Name);
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
            AddMergedCells(objgridviewrow, objtablecell, 10, "TERM 1", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 11, "TERM 2", System.Drawing.Color.LightGray.Name);
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
}