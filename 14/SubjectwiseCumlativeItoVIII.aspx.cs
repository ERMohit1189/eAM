using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubjectwiseCumlativeItoVIII : Page
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
        sql = "Select Name EmpName from GetAllStaffRecords_UDF(" + Session["BranchCode"].ToString() + ") where EmpId=(Select EmpId from ICSESubjectTeacherAllotment where Subjectid='" + drpPaper.SelectedValue.ToString() + "' and classid=" + drpclass.SelectedValue + " and branchid=" + drpBranch.SelectedValue + " and sectionid=" + drpsection.SelectedValue + " and BranchCode=" + Session["BranchCode"] + ")";

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
        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
        ss = ss + " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss = ss + " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss = ss + " where cm.id=" + drpclass.SelectedValue + " and bm.id=" + drpBranch.SelectedValue + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
        string DisableTest1 = (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "");
        string DisableTest2 = (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "");
        string DisableTest3 = (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "");
        string DisableTest4 = (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : "");
        string DisableTest5 = (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : "");
        string DisableTest6 = (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : "");
        string monthlyTest1 = "", monthlyTest2 = "";
        if (DisableTest1 == "hide" && DisableTest2 == "hide" && DisableTest3 == "hide")
        {
            monthlyTest1 = "hide";
        }
        if (DisableTest4 == "hide" && DisableTest5 == "hide" && DisableTest6 == "hide")
        {
            monthlyTest2 = "hide";
        }

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
            string sqls = "select MaxTest1, MaxTest2, MaxTest3, MaxHY, MaxTest4, MaxTest5, MaxTest6, MaxAE from ICSEMaxMarkEntryItoVIII where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' ";
            sqls = sqls + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";

            Label lblMMT1 = (Label)GridView1.HeaderRow.FindControl("lblMMT1s");
            Label lblMMT2 = (Label)GridView1.HeaderRow.FindControl("lblMMT2s");
            Label lblMMT3 = (Label)GridView1.HeaderRow.FindControl("lblMMT3s");
            Label lblMMHY = (Label)GridView1.HeaderRow.FindControl("lblMMHYs");
            lblMMT1.Text = oo.ReturnTag(sqls, "MaxTest1");
            lblMMT2.Text = oo.ReturnTag(sqls, "MaxTest2");
            lblMMT3.Text = oo.ReturnTag(sqls, "MaxTest3");
            lblMMHY.Text = oo.ReturnTag(sqls, "MaxHY");
            if (monthlyTest1 == "hide")
            {
                GridView1.HeaderRow.Cells[4].Visible = false;
                GridView1.HeaderRow.Cells[5].Visible = false;
                GridView1.HeaderRow.Cells[6].Visible = false;
                GridView1.HeaderRow.Cells[7].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[4].Visible = (DisableTest1 == "hide" ? false : true);
                GridView1.HeaderRow.Cells[5].Visible = (DisableTest2 == "hide" ? false : true);
                GridView1.HeaderRow.Cells[6].Visible = (DisableTest3 == "hide" ? false : true);
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblsrno = (Label)GridView1.Rows[i].FindControl("lblsrnos");
                sql = "Select Test1,Test2,Test3,HY,Test4,Test5,Test6,AE from ICSEMarkEntryItoVIII where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                sql = sql + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";
                Label lblT1 = (Label)GridView1.Rows[i].FindControl("lblT1s");
                Label lblT2 = (Label)GridView1.Rows[i].FindControl("lblT2s");
                Label lblT3 = (Label)GridView1.Rows[i].FindControl("lblT3s");
                Label lblHY = (Label)GridView1.Rows[i].FindControl("lblHYs");
                Label lblBestOneHY = (Label)GridView1.Rows[i].FindControl("lblBestOneHYs");

                Label lnlGTotal = (Label)GridView1.Rows[i].FindControl("lnlGTotals");
                double Test1 = 0, Test2 = 0, Test3 = 0, BestOneHY = 0, MaxTest1 = 0, MaxTest2 = 0, MaxTest3 = 0;
                double.TryParse(oo.ReturnTag(sql, "Test1"), out Test1);
                double.TryParse(oo.ReturnTag(sql, "Test2"), out Test2);
                double.TryParse(oo.ReturnTag(sql, "Test3"), out Test3);
                double p1 = (((Test1 * 100) / MaxTest1).ToString() == "NaN" ? 0 : ((Test1 * 100) / MaxTest1));
                double p2 = (((Test2 * 100) / MaxTest2).ToString() == "NaN" ? 0 : ((Test2 * 100) / MaxTest2));
                double p3 = (((Test3 * 100) / MaxTest3).ToString() == "NaN" ? 0 : ((Test3 * 100) / MaxTest3));
                p1=(DisableTest1 == "hide" ? 0 : p1);
                p2=(DisableTest2 == "hide" ? 0 : p2);
                p3=(DisableTest3 == "hide" ? 0 : p3);
                BestOneHY = Math.Round(p1 > p2 ? (p1 > p3 ? Test1 : (p2 > p3 ? Test2 : Test3)) : (p2 > p3 ? Test2 : Test3));
                if (monthlyTest1 == "hide")
                {
                    BestOneHY = 0;
                }
                

                lblT1.Text = oo.ReturnTag(sql, "Test1");
                lblT2.Text = oo.ReturnTag(sql, "Test2");
                lblT3.Text = oo.ReturnTag(sql, "Test3");
                lblBestOneHY.Text = BestOneHY.ToString("0.0");
                lblHY.Text = oo.ReturnTag(sql, "HY");
                double HY = 0;
                double.TryParse(oo.ReturnTag(sql, "HY"), out HY);
                lnlGTotal.Text = (BestOneHY + HY).ToString("0.0");
                if (monthlyTest1 == "hide")
                {
                    GridView1.Rows[i].Cells[4].Visible = false;
                    GridView1.Rows[i].Cells[5].Visible = false;
                    GridView1.Rows[i].Cells[6].Visible = false;
                    GridView1.Rows[i].Cells[7].Visible = false;
                }
                else
                {
                    GridView1.Rows[i].Cells[4].Visible = (DisableTest1 == "hide" ? false : true);
                    GridView1.Rows[i].Cells[5].Visible = (DisableTest2 == "hide" ? false : true);
                    GridView1.Rows[i].Cells[6].Visible = (DisableTest3 == "hide" ? false : true);
                }
            }
        }
        else
        {
            divExport.Visible = false;
        }
    }

    public void loadgrid2()
    {
        string ss = "select isnull(Test1, 'Yes')Test1, isnull(Test2, 'Yes')Test2, isnull(Test3, 'Yes')Test3, isnull(Test4, 'Yes')Test4, isnull(Test5, 'Yes')Test5, isnull(Test6, 'Yes')Test6 ";
        ss = ss + " from ClassMaster cm inner join BranchMaster bm on bm.Classid=cm.Id and bm.SessionName=cm.SessionName and bm.BranchCode=cm.BranchCode ";
        ss = ss + " left join ICSETestPermission pm on pm.Classid=cm.Id and pm.BranchId=bm.Id and pm.SessionName=cm.SessionName and pm.BranchCode=cm.BranchCode";
        ss = ss + " where cm.id=" + drpclass.SelectedValue + " and bm.id=" + drpBranch.SelectedValue + " and cm.BranchCode=" + Session["BranchCode"] + " and cm.SessionName='" + Session["SessionName"] + "'";
        string DisableTest1 = (oo.ReturnTag(ss, "Test1") == "No" ? "hide" : "");
        string DisableTest2 = (oo.ReturnTag(ss, "Test2") == "No" ? "hide" : "");
        string DisableTest3 = (oo.ReturnTag(ss, "Test3") == "No" ? "hide" : "");
        string DisableTest4 = (oo.ReturnTag(ss, "Test4") == "No" ? "hide" : "");
        string DisableTest5 = (oo.ReturnTag(ss, "Test5") == "No" ? "hide" : "");
        string DisableTest6 = (oo.ReturnTag(ss, "Test6") == "No" ? "hide" : "");
        string monthlyTest1 = "", monthlyTest2 = "";
        if (DisableTest1 == "hide" && DisableTest2 == "hide" && DisableTest3 == "hide")
        {
            monthlyTest1 = "hide";
        }
        if (DisableTest4 == "hide" && DisableTest5 == "hide" && DisableTest6 == "hide")
        {
            monthlyTest2 = "hide";
        }

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
            string sqls = "select MaxTest1, MaxTest2, MaxTest3, MaxHY, MaxTest4, MaxTest5, MaxTest6, MaxAE from ICSEMaxMarkEntryItoVIII where BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "' ";
            sqls = sqls + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";

            Label lblMMT1 = (Label)GridView2.HeaderRow.FindControl("lblMMT1");
            Label lblMMT2 = (Label)GridView2.HeaderRow.FindControl("lblMMT2");
            Label lblMMT3 = (Label)GridView2.HeaderRow.FindControl("lblMMT3");
            Label lblMMHY = (Label)GridView2.HeaderRow.FindControl("lblMMHY");

            Label lblMMT4 = (Label)GridView2.HeaderRow.FindControl("lblMMT4");
            Label lblMMT5 = (Label)GridView2.HeaderRow.FindControl("lblMMT5");
            Label lblMMT6 = (Label)GridView2.HeaderRow.FindControl("lblMMT6");
            Label lblMMAE = (Label)GridView2.HeaderRow.FindControl("lblMMAE");
            lblMMT1.Text = oo.ReturnTag(sqls, "MaxTest1");
            lblMMT2.Text = oo.ReturnTag(sqls, "MaxTest2");
            lblMMT3.Text = oo.ReturnTag(sqls, "MaxTest3");
            lblMMHY.Text = oo.ReturnTag(sqls, "MaxHY");

            lblMMT4.Text = oo.ReturnTag(sqls, "MaxTest4");
            lblMMT5.Text = oo.ReturnTag(sqls, "MaxTest5");
            lblMMT6.Text = oo.ReturnTag(sqls, "MaxTest6");
            lblMMAE.Text = oo.ReturnTag(sqls, "MaxAE");

            if (monthlyTest1 == "hide")
            {
                GridView2.HeaderRow.Cells[4].Visible = false;
                GridView2.HeaderRow.Cells[5].Visible = false;
                GridView2.HeaderRow.Cells[6].Visible = false;
                GridView2.HeaderRow.Cells[7].Visible = false;
            }
            else
            {
                GridView2.HeaderRow.Cells[4].Visible = (DisableTest1 == "hide" ? false : true);
                GridView2.HeaderRow.Cells[5].Visible = (DisableTest2 == "hide" ? false : true);
                GridView2.HeaderRow.Cells[6].Visible = (DisableTest3 == "hide" ? false : true);
            }
            if (monthlyTest2 == "hide")
            {
                GridView2.HeaderRow.Cells[8].Visible = false;
                GridView2.HeaderRow.Cells[9].Visible = false;
                GridView2.HeaderRow.Cells[10].Visible = false;
                GridView2.HeaderRow.Cells[11].Visible = false;
            }
            else
            {
                GridView2.HeaderRow.Cells[8].Visible = (DisableTest4 == "hide" ? false : true);
                GridView2.HeaderRow.Cells[9].Visible = (DisableTest5 == "hide" ? false : true);
                GridView2.HeaderRow.Cells[10].Visible = (DisableTest6 == "hide" ? false : true);
            }
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                Label lblsrno = (Label)GridView2.Rows[i].FindControl("lblsrno");
                sql = "Select Test1,Test2,Test3,HY,Test4,Test5,Test6,AE from ICSEMarkEntryItoVIII where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                sql = sql + " and Classid=" + drpclass.SelectedValue + " and BranchId=" + drpclass.SelectedValue + " and SectionId=" + drpsection.SelectedValue + " and SubjectId=" + drpSubject.SelectedValue + " and paperid=" + drpPaper.SelectedValue + "";
                Label lblT1 = (Label)GridView2.Rows[i].FindControl("lblT1");
                Label lblT2 = (Label)GridView2.Rows[i].FindControl("lblT2");
                Label lblT3 = (Label)GridView2.Rows[i].FindControl("lblT3");
                Label lblHY = (Label)GridView2.Rows[i].FindControl("lblHY");
                Label lblBestOneHY = (Label)GridView2.Rows[i].FindControl("lblBestOneHY");

                Label lblT4 = (Label)GridView2.Rows[i].FindControl("lblT4");
                Label lblT5 = (Label)GridView2.Rows[i].FindControl("lblT5");
                Label lblT6 = (Label)GridView2.Rows[i].FindControl("lblT6");
                Label lblBestOneAE = (Label)GridView2.Rows[i].FindControl("lblBestOneAE");
                Label lblAE = (Label)GridView2.Rows[i].FindControl("lblAE");
                Label lnlGTotal = (Label)GridView2.Rows[i].FindControl("lnlGTotal");
                double Test1 = 0, Test2 = 0, Test3 = 0, BestOneHY = 0, MaxTest1 = 0, MaxTest2 = 0, MaxTest3 = 0;
                double.TryParse(oo.ReturnTag(sql, "Test1"), out Test1);
                double.TryParse(oo.ReturnTag(sql, "Test2"), out Test2);
                double.TryParse(oo.ReturnTag(sql, "Test3"), out Test3);
                double p1 = (((Test1 * 100) / MaxTest1).ToString() == "NaN" ? 0 : ((Test1 * 100) / MaxTest1));
                double p2 = (((Test2 * 100) / MaxTest2).ToString() == "NaN" ? 0 : ((Test2 * 100) / MaxTest2));
                double p3 = (((Test3 * 100) / MaxTest3).ToString() == "NaN" ? 0 : ((Test3 * 100) / MaxTest3));
                p1 = (DisableTest1 == "hide" ? 0 : p1);
                p2 = (DisableTest2 == "hide" ? 0 : p2);
                p3 = (DisableTest3 == "hide" ? 0 : p3);
                BestOneHY = Math.Round(p1 > p2 ? (p1 > p3 ? Test1 : (p2 > p3 ? Test2 : Test3)) : (p2 > p3 ? Test2 : Test3));
                if (monthlyTest1 == "hide")
                {
                    BestOneHY = 0;
                }
                


                lblT1.Text = oo.ReturnTag(sql, "Test1");
                lblT2.Text = oo.ReturnTag(sql, "Test2");
                lblT3.Text = oo.ReturnTag(sql, "Test3");
                lblBestOneHY.Text = BestOneHY.ToString("0.0");
                lblHY.Text = oo.ReturnTag(sql, "HY");


                double Test4 = 0, Test5 = 0, Test6 = 0, BestOneAE = 0, MaxTest4 = 0, MaxTest5 = 0, MaxTest6 = 0;
                double.TryParse(oo.ReturnTag(sql, "Test4"), out Test4);
                double.TryParse(oo.ReturnTag(sql, "Test5"), out Test5);
                double.TryParse(oo.ReturnTag(sql, "Test6"), out Test6);
                double p4 = (((Test4 * 100) / MaxTest4).ToString() == "NaN" ? 0 : ((Test4 * 100) / MaxTest4));
                double p5 = (((Test5 * 100) / MaxTest5).ToString() == "NaN" ? 0 : ((Test5 * 100) / MaxTest5));
                double p6 = (((Test6 * 100) / MaxTest6).ToString() == "NaN" ? 0 : ((Test6 * 100) / MaxTest6));
                p4 = (DisableTest4 == "hide" ? 0 : p4);
                p5 = (DisableTest5 == "hide" ? 0 : p5);
                p6 = (DisableTest6 == "hide" ? 0 : p6);
                BestOneAE = Math.Round(p4 > p5 ? (p4 > p6 ? Test4 : (p5 > p6 ? Test5 : Test6)) : (p5 > p6 ? Test5 : Test6));
                if (monthlyTest2 == "hide")
                {
                    BestOneAE = 0;
                }
                
                lblT4.Text = oo.ReturnTag(sql, "Test4");
                lblT5.Text = oo.ReturnTag(sql, "Test4");
                lblT6.Text = oo.ReturnTag(sql, "Test6");
                lblBestOneAE.Text = BestOneAE.ToString("0.0");
                lblAE.Text = oo.ReturnTag(sql, "AE");

                double HY = 0, AE = 0;
                double.TryParse(oo.ReturnTag(sql, "HY"), out HY);
                double.TryParse(oo.ReturnTag(sql, "AE"), out AE);
                lnlGTotal.Text = (BestOneHY + BestOneAE + HY + AE).ToString("0.0");
                if (monthlyTest1 == "hide")
                {
                    GridView2.Rows[i].Cells[4].Visible = false;
                    GridView2.Rows[i].Cells[5].Visible = false;
                    GridView2.Rows[i].Cells[6].Visible = false;
                    GridView2.Rows[i].Cells[7].Visible = false;
                }
                else
                {
                    GridView2.Rows[i].Cells[4].Visible = (DisableTest1 == "hide" ? false : true);
                    GridView2.Rows[i].Cells[5].Visible = (DisableTest2 == "hide" ? false : true);
                    GridView2.Rows[i].Cells[6].Visible = (DisableTest3 == "hide" ? false : true);
                }
                if (monthlyTest2 == "hide")
                {
                    GridView2.Rows[i].Cells[9].Visible = false;
                    GridView2.Rows[i].Cells[10].Visible = false;
                    GridView2.Rows[i].Cells[11].Visible = false;
                    GridView2.Rows[i].Cells[12].Visible = false;
                }
                else
                {
                    GridView2.Rows[i].Cells[9].Visible = (DisableTest4 == "hide" ? false : true);
                    GridView2.Rows[i].Cells[10].Visible = (DisableTest5 == "hide" ? false : true);
                    GridView2.Rows[i].Cells[11].Visible = (DisableTest6 == "hide" ? false : true);
                }
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
            sql = sql + " and id in(select classid from ICSEClassGroupMaster where GroupName='G2' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")  Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            drpclass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            sql = "Select Distinct ClassName,cm.Id,CIDOrder from ICSEClassTeacherAllotment ctm";
            sql = sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName and cm.BranchCode=ctm.BranchCode";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' ";
            sql = sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and ctm.BranchCode = " + Session["BranchCode"] + " ";
            sql = sql + " and cm.id in(select classid from ICSEClassGroupMaster where GroupName='G2' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ")";
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