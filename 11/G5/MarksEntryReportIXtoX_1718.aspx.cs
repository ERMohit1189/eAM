using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class common_MarksEntryReportIXtoX_1718 : Page
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
            drpSubject.Items.Clear();
            drpSubject.Items.Insert(0, "<--Select-->");
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
        sql +=  "   and SC.SectionName='" + drpsection.SelectedItem.ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "' and sg.BranchCode=" + Session["BranchCode"] + "";
        sql +=  "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql +=  "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and  isnull(SO.Promotion, '')<>'Cancelled' order by FirstName Asc";

        if(drpEval.SelectedIndex!=0)
        {
            GridView1.DataSource = oo.GridFill(sql);
            GridView1.DataBind();
        }
        
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G5' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from ICSESubjectTeacherAllotment sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
            sql +=  " where GroupId='G5'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
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
            sql = "Select PaperName,Id from ttpapermaster where SubjectId='" + drpSubjectGroup.SelectedValue.ToString() + "' and Classid=" + drpclass.SelectedValue.ToString() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' order by id";

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
        drpSubjectGroup.Items.Clear();
        drpSubjectGroup.Items.Insert(0, "<--Select-->");
        drpSubject.Items.Clear();
        drpSubject.Items.Insert(0, "<--Select-->");
        GridView1.DataSource = null;
        GridView1.DataBind();
        divExport.Visible = false;
        loadsection();
    }
    
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpSubject.Items.Clear();
        drpSubject.Items.Insert(0, "<--Select-->");
        GridView1.DataSource = null;
        GridView1.DataBind();
        divExport.Visible = false;
        loadSubjectGroup();
    }
    private void loadSubjectMaxMarks()
    {
        sql = "Select MaxMarks1,MaxMarks2,MaxMarks3,MaxMarks4,MaxMarks5,MaxMarks6, MaxMarks7,MaxMarks8 from SetMaxMinMarks_IXtoX where Eval='" + drpEval.SelectedValue + "' and BranchCode=" + Session["BranchCode"] + " and SubjectActivityId=" + drpSubjectGroup.SelectedValue + " and paperid=" + drpSubject.SelectedValue.ToString() + " and SessionName='" + Session["SessionName"].ToString() + "'";
        string mm = oo.ReturnTag(sql, "MaxMarks");
        if (GridView1.Rows.Count > 0)
        {
            Label lblMMT1 = (Label)GridView1.HeaderRow.FindControl("lblMMT1");
            Label lblMMT2 = (Label)GridView1.HeaderRow.FindControl("lblMMT2");
            Label lblMMSE = (Label)GridView1.HeaderRow.FindControl("lblMMSE");
            Label lblMMMA = (Label)GridView1.HeaderRow.FindControl("lblMMMA");
            Label lblMMPort = (Label)GridView1.HeaderRow.FindControl("lblMMPort");
            Label lblMMHY = (Label)GridView1.HeaderRow.FindControl("lblMMHY");
            Label lblMMPrac = (Label)GridView1.HeaderRow.FindControl("lblMMPrac");

            Label lblMMT3 = (Label)GridView1.HeaderRow.FindControl("lblMMT3");
            Label lblMMT4 = (Label)GridView1.HeaderRow.FindControl("lblMMT4");
            Label lblMMSE2 = (Label)GridView1.HeaderRow.FindControl("lblMMSE2");
            Label lblMMMA2 = (Label)GridView1.HeaderRow.FindControl("lblMMMA2");
            Label lblMMPort2 = (Label)GridView1.HeaderRow.FindControl("lblMMPort2");
            Label lblMMHY2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2");
            Label lblMMHY2_2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2_2");
            Label lblMMPrac2 = (Label)GridView1.HeaderRow.FindControl("lblMMPrac2");

            string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubjectGroup.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + "";
            bool isAdditional = false;
            if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
            {
                isAdditional = true;
            }
            string ClassName = drpclass.SelectedItem.Text;
            int Evail = drpEval.SelectedIndex;
            if (Evail == 1)
            {
                if (isAdditional)
                {
                    if (ClassName.ToUpper() == "IX")
                    {
                        lblMMT1.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMT2.Text = oo.ReturnTag(sql, "MaxMarks2");
                        lblMMPrac.Text = oo.ReturnTag(sql, "MaxMarks7");
                        lblMMPort.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMHY.Text = oo.ReturnTag(sql, "MaxMarks6");
                    }
                    if (ClassName.ToUpper() == "X")
                    {
                        lblMMT1.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMT2.Text = oo.ReturnTag(sql, "MaxMarks2");
                        lblMMPrac.Text = oo.ReturnTag(sql, "MaxMarks7");
                        lblMMPort.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMHY.Text = oo.ReturnTag(sql, "MaxMarks6");
                    }

                }
                else
                {
                    if (ClassName.ToUpper() == "IX")
                    {
                        lblMMT1.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMT2.Text = oo.ReturnTag(sql, "MaxMarks2");
                        lblMMSE.Text = oo.ReturnTag(sql, "MaxMarks4");
                        lblMMMA.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMPort.Text = oo.ReturnTag(sql, "MaxMarks6");
                        lblMMHY.Text = oo.ReturnTag(sql, "MaxMarks7");
                    }
                    if (ClassName.ToUpper() == "X")
                    {
                        lblMMT1.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMT2.Text = oo.ReturnTag(sql, "MaxMarks2");
                        lblMMSE.Text = oo.ReturnTag(sql, "MaxMarks3");
                        lblMMMA.Text = oo.ReturnTag(sql, "MaxMarks4");
                        lblMMPort.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMHY.Text = oo.ReturnTag(sql, "MaxMarks6");
                    }
                }
            }

            if (Evail == 2)
            {
                if (isAdditional)
                {
                    if (ClassName.ToUpper() == "IX")
                    {
                        lblMMT3.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMT4.Text = oo.ReturnTag(sql, "MaxMarks2");
                        lblMMPrac2.Text = oo.ReturnTag(sql, "MaxMarks7");
                        lblMMPort2.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMHY2.Text = oo.ReturnTag(sql, "MaxMarks6");

                    }
                    if (ClassName.ToUpper() == "X")
                    {
                        lblMMT3.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMPrac2.Text = oo.ReturnTag(sql, "MaxMarks7");
                        lblMMPort2.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMHY2.Text = oo.ReturnTag(sql, "MaxMarks6");
                        lblMMHY2_2.Text = oo.ReturnTag(sql, "MaxMarks8");
                    }
                }
                else
                {
                    if (ClassName.ToUpper() == "IX")
                    {
                        lblMMT3.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMT4.Text = oo.ReturnTag(sql, "MaxMarks2");
                        lblMMSE2.Text = oo.ReturnTag(sql, "MaxMarks4");
                        lblMMMA2.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMPort2.Text = oo.ReturnTag(sql, "MaxMarks6");
                        lblMMHY2.Text = oo.ReturnTag(sql, "MaxMarks7");
                    }
                    if (ClassName.ToUpper() == "X")
                    {
                        lblMMT3.Text = oo.ReturnTag(sql, "MaxMarks1");
                        lblMMSE2.Text = oo.ReturnTag(sql, "MaxMarks3");
                        lblMMMA2.Text = oo.ReturnTag(sql, "MaxMarks4");
                        lblMMPort2.Text = oo.ReturnTag(sql, "MaxMarks5");
                        lblMMHY2.Text = oo.ReturnTag(sql, "MaxMarks6");
                        lblMMHY2_2.Text = oo.ReturnTag(sql, "MaxMarks8");
                    }
                }
            }
        }
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpSubject.SelectedIndex == 0)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            divExport.Visible = false;
        }
        else
        {
            loadgrid();
            divExport.Visible = true;
            createTitle();
            hideColumn();
            loadMarks();
        }
    }
    protected void loadMarks()
    {
        if (GridView1.Rows.Count > 0)
        {
            loadSubjectMaxMarks();
            string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubjectGroup.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + "";
            bool isAdditional = false;
            if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
            {
                isAdditional = true;
            }
            string ClassName = drpclass.SelectedItem.Text;
            int Evail = drpEval.SelectedIndex;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label lblsrno = (Label)GridView1.Rows[i].FindControl("Label2");
                Label lblMMT1 = (Label)GridView1.HeaderRow.FindControl("lblMMT1");
                Label lblMMT2 = (Label)GridView1.HeaderRow.FindControl("lblMMT2");
                Label lblMMSE = (Label)GridView1.HeaderRow.FindControl("lblMMSE");
                Label lblMMMA = (Label)GridView1.HeaderRow.FindControl("lblMMMA");
                Label lblMMPort = (Label)GridView1.HeaderRow.FindControl("lblMMPort");
                Label lblMMHY = (Label)GridView1.HeaderRow.FindControl("lblMMHY");
                Label lblMMPrac = (Label)GridView1.HeaderRow.FindControl("lblMMPrac");

                Label lblMMT3 = (Label)GridView1.HeaderRow.FindControl("lblMMT3");
                Label lblMMT4 = (Label)GridView1.HeaderRow.FindControl("lblMMT4");
                Label lblMMSE2 = (Label)GridView1.HeaderRow.FindControl("lblMMSE2");
                Label lblMMMA2 = (Label)GridView1.HeaderRow.FindControl("lblMMMA2");
                Label lblMMPort2 = (Label)GridView1.HeaderRow.FindControl("lblMMPort2");
                Label lblMMHY2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2");
                Label lblMMHY2_2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2_2");
                Label lblMMPrac2 = (Label)GridView1.HeaderRow.FindControl("lblMMPrac2");
                double MMT1 = 0; double MMT2 = 0; double MMSE = 0; double MMMA = 0; double MMPrac = 0; double MMPort = 0; double MMHY = 0;
                double MMT3 = 0; double MMT4 = 0; double MMSE2 = 0; double MMMA2 = 0; double MMPrac2 = 0; double MMPort2 = 0; double MMHY2 = 0; double MMHY2_2 = 0;
                double.TryParse(lblMMT1.Text, out MMT1);
                double.TryParse(lblMMT2.Text, out MMT2);
                double.TryParse(lblMMSE.Text, out MMSE);
                double.TryParse(lblMMMA.Text, out MMMA);
                double.TryParse(lblMMPrac.Text, out MMPrac);
                double.TryParse(lblMMPort.Text, out MMPort);
                double.TryParse(lblMMHY.Text, out MMHY);

                double.TryParse(lblMMT3.Text, out MMT3);
                double.TryParse(lblMMT4.Text, out MMT4);
                double.TryParse(lblMMSE2.Text, out MMSE2);
                double.TryParse(lblMMMA2.Text, out MMMA2);
                double.TryParse(lblMMPrac2.Text, out MMPrac2);
                double.TryParse(lblMMPort2.Text, out MMPort2);
                double.TryParse(lblMMHY2.Text, out MMHY2);
                double.TryParse(lblMMHY2_2.Text, out MMHY2_2);

                Label lblT1 = (Label)GridView1.Rows[i].FindControl("lblT1");
                Label lblT2 = (Label)GridView1.Rows[i].FindControl("lblT2");
                Label lblSE = (Label)GridView1.Rows[i].FindControl("lblSE");
                Label lblMA = (Label)GridView1.Rows[i].FindControl("lblMA");
                Label lblPort = (Label)GridView1.Rows[i].FindControl("lblPort");
                Label lblHY = (Label)GridView1.Rows[i].FindControl("lblHY");
                Label lblPrac = (Label)GridView1.Rows[i].FindControl("lblPrac");
                Label lblT3 = (Label)GridView1.Rows[i].FindControl("lblT3");
                Label lblT4 = (Label)GridView1.Rows[i].FindControl("lblT4");
                Label lblSE2 = (Label)GridView1.Rows[i].FindControl("lblSE2");
                Label lblMA2 = (Label)GridView1.Rows[i].FindControl("lblMA2");
                Label lblPort2 = (Label)GridView1.Rows[i].FindControl("lblPort2");
                Label lblHY2 = (Label)GridView1.Rows[i].FindControl("lblHY2");
                Label lblHY2_2 = (Label)GridView1.Rows[i].FindControl("lblHY2_2");
                Label lblPrac2 = (Label)GridView1.Rows[i].FindControl("lblPrac2");

                double Best1 = 0; double Conv10_1 = 0; double Total1 = 0;
                Label lblBest = (Label)GridView1.Rows[i].FindControl("lblBest");
                Label lblConv10 = (Label)GridView1.Rows[i].FindControl("lblConv10");
                Label lblTotal = (Label)GridView1.Rows[i].FindControl("lblTotal");

                double Best2 = 0; double Conv10_2 = 0; double Total2 = 0;
                Label lblBest2 = (Label)GridView1.Rows[i].FindControl("lblBest2");
                Label lblConv102 = (Label)GridView1.Rows[i].FindControl("lblConv102");
                Label lblTotal2 = (Label)GridView1.Rows[i].FindControl("lblTotal2");

                //double GTotal = 0;
                //Label lblGTotal = (Label)GridView1.Rows[i].FindControl("lblGTotal");

                double Test1 = 0; double Test2 = 0; double SE = 0; double MA = 0; double Port = 0; double HY = 0; double Prac = 0;
                double Test3 = 0; double Test4 = 0; double SE2 = 0; double MA2 = 0; double Port2 = 0; double AE = 0; double AE2 = 0; double Prac2 = 0;
                sql = "Select Test1,Test2,SE,MA,Port,SAT, SAT2, Prac from CCEIXtoX_1718 where SRNO='" + lblsrno.Text + "' and BranchCode=" + Session["BranchCode"] + " and Evaluation='" + drpEval.SelectedValue + "' and paperid='" + drpSubject.SelectedValue.ToString() + "'";
                if (Evail == 1)
                {
                    if (isAdditional)
                    {
                        lblT1.Text = oo.ReturnTag(sql, "Test1");
                        lblT2.Text = oo.ReturnTag(sql, "Test2");
                        lblPrac.Text = oo.ReturnTag(sql, "Prac");
                        lblPort.Text = oo.ReturnTag(sql, "Port");
                        lblHY.Text = oo.ReturnTag(sql, "SAT");
                        double.TryParse(oo.ReturnTag(sql, "Test1"), out Test1);
                        double.TryParse(oo.ReturnTag(sql, "Test2"), out Test2);
                        double.TryParse(oo.ReturnTag(sql, "Prac"), out Prac);
                        double.TryParse(oo.ReturnTag(sql, "Port"), out Port);
                        double.TryParse(oo.ReturnTag(sql, "SAT"), out HY);
                        double test11 = (Test1 * 20) / MMT1;
                        double test22 = (Test2 * 20) / MMT2;
                        Best1 = (test11 > test22 ? test11 : test22);
                        lblBest.Text = Best1.ToString("0");
                        Conv10_1 = (double.Parse(Best1.ToString("0")) * 5) / (MMT1> MMT2? MMT1: MMT2);
                        lblConv10.Text = Conv10_1.ToString("0.00");
                        Total1 = double.Parse((Conv10_1 + Port + HY + Prac).ToString("0"));
                        lblTotal.Text = Total1.ToString("0");

                    }
                    else
                    {
                        lblT1.Text = oo.ReturnTag(sql, "Test1");
                        lblT2.Text = oo.ReturnTag(sql, "Test2");
                        lblSE.Text = oo.ReturnTag(sql, "SE");
                        lblMA.Text = oo.ReturnTag(sql, "MA");
                        lblPort.Text = oo.ReturnTag(sql, "Port");
                        lblHY.Text = oo.ReturnTag(sql, "SAT");

                        double.TryParse(oo.ReturnTag(sql, "Test1"), out Test1);
                        double.TryParse(oo.ReturnTag(sql, "Test2"), out Test2);
                        double.TryParse(oo.ReturnTag(sql, "SE"), out SE);
                        double.TryParse(oo.ReturnTag(sql, "MA"), out MA);
                        double.TryParse(oo.ReturnTag(sql, "Port"), out Port);
                        double.TryParse(oo.ReturnTag(sql, "SAT"), out HY);
                        double test11 = (Test1 * 20) / MMT1;
                        double test22 = (Test2 * 20) / MMT2;
                        Best1 = (test11 > test22 ? test11 : test22);
                        lblBest.Text = Best1.ToString("0");
                        Conv10_1 = (double.Parse(Best1.ToString("0")) * 5) / (MMT1 > MMT2 ? MMT1 : MMT2);
                        lblConv10.Text = Conv10_1.ToString("0.00");
                        Total1 = double.Parse((Conv10_1 + SE + MA + Port + HY).ToString("0"));
                        lblTotal.Text = Total1.ToString("0");
                    }
                }

                if (Evail == 2)
                {
                    if (isAdditional)
                    {
                        if (ClassName.ToUpper() == "IX")
                        {
                            lblT3.Text = oo.ReturnTag(sql, "Test1");
                            lblT4.Text = oo.ReturnTag(sql, "Test2");
                            lblPort2.Text = oo.ReturnTag(sql, "Port");
                            lblHY2.Text = oo.ReturnTag(sql, "SAT");
                            lblPrac2.Text = oo.ReturnTag(sql, "Prac");
                            double.TryParse(oo.ReturnTag(sql, "Test1"), out Test3);
                            double.TryParse(oo.ReturnTag(sql, "Test2"), out Test4);
                            double.TryParse(oo.ReturnTag(sql, "Port"), out Port2);
                            double.TryParse(oo.ReturnTag(sql, "SAT"), out AE);
                            double.TryParse(oo.ReturnTag(sql, "Prac"), out Prac2);
                            double test33 = 0;
                            double test44 = 0;
                            if (MMT3 > 0)
                            {
                                double.TryParse(((Test3 * 20) / MMT3).ToString(), out test33);
                            }
                            if (MMT4 > 0)
                            {
                                double.TryParse(((Test4 * 20) / MMT4).ToString(), out test44);
                            }
                            Best2 = (test33 > test44 ? test33 : test44);
                            lblBest2.Text = Best2.ToString("0");
                            Conv10_2 = (double.Parse(Best2.ToString("0")) * 5) / (MMT3> MMT4? MMT3: MMT4);
                            lblConv102.Text = Conv10_2.ToString("0.00");
                            Total2 = double.Parse((Conv10_2 + Port2 + AE + Prac2).ToString("0"));
                            lblTotal2.Text = Total2.ToString("0");

                        }
                        if (ClassName.ToUpper() == "X")
                        {
                            lblT3.Text = oo.ReturnTag(sql, "Test1");
                            lblPrac2.Text = oo.ReturnTag(sql, "Prac");
                            lblPort2.Text = oo.ReturnTag(sql, "Port");
                            lblHY2.Text = oo.ReturnTag(sql, "SAT");
                            lblHY2_2.Text = oo.ReturnTag(sql, "SAT2");
                            double.TryParse(oo.ReturnTag(sql, "Test1"), out Test3);
                            double.TryParse(oo.ReturnTag(sql, "Prac"), out Prac2);
                            double.TryParse(oo.ReturnTag(sql, "Port"), out Port2);
                            double.TryParse(oo.ReturnTag(sql, "SAT"), out AE);
                            double.TryParse(oo.ReturnTag(sql, "SAT2"), out AE2);
                            Best2 = (Test3 * 20) / MMT3;
                            lblBest2.Text = Best2.ToString("0");
                            Conv10_2 = (double.Parse(Best2.ToString("0")) * 5) / MMT3;
                            lblConv102.Text = Conv10_2.ToString("0.00");
                            Total2 = double.Parse((Conv10_2 + Port2+ Prac2 + (AE > AE2 ? AE : AE2)).ToString("0"));
                            lblTotal2.Text = Total2.ToString("0");
                        }
                    }
                    else
                    {
                        if (ClassName.ToUpper() == "IX")
                        {
                            lblT3.Text = oo.ReturnTag(sql, "Test1");
                            lblT4.Text = oo.ReturnTag(sql, "Test2");
                            lblSE2.Text = oo.ReturnTag(sql, "SE");
                            lblMA2.Text = oo.ReturnTag(sql, "MA");
                            lblPort2.Text = oo.ReturnTag(sql, "Port");
                            lblHY2.Text = oo.ReturnTag(sql, "SAT");

                            double.TryParse(oo.ReturnTag(sql, "Test1"), out Test3);
                            double.TryParse(oo.ReturnTag(sql, "Test2"), out Test4);
                            double.TryParse(oo.ReturnTag(sql, "SE"), out SE2);
                            double.TryParse(oo.ReturnTag(sql, "MA"), out MA2);
                            double.TryParse(oo.ReturnTag(sql, "Port"), out Port2);
                            double.TryParse(oo.ReturnTag(sql, "SAT"), out AE);
                            double test33 = 0;
                            double test44 = 0;
                            if (MMT3 > 0)
                            {
                                double.TryParse(((Test3 * 20) / MMT3).ToString(), out test33);
                            }
                            if (MMT4 > 0)
                            {
                                double.TryParse(((Test4 * 20) / MMT4).ToString(), out test44);
                            }
                            Best2 = (test33 > test44 ? test33 : test44);
                            lblBest2.Text = Best2.ToString("0");
                            Conv10_2 = (double.Parse(Best2.ToString("0")) * 5) / (MMT3> MMT4? MMT3: MMT4);
                            lblConv102.Text = Conv10_2.ToString("0.00");
                            Total2 = double.Parse((Conv10_2+SE2+MA2 + Port2 + AE).ToString("0"));
                            lblTotal2.Text = Total2.ToString("0");

                        }
                        if (ClassName.ToUpper() == "X")
                        {
                            lblT3.Text = oo.ReturnTag(sql, "Test1");
                            lblSE2.Text = oo.ReturnTag(sql, "SE");
                            lblMA2.Text = oo.ReturnTag(sql, "MA");
                            lblPort2.Text = oo.ReturnTag(sql, "Port");
                            lblHY2.Text = oo.ReturnTag(sql, "SAT");
                            lblHY2_2.Text = oo.ReturnTag(sql, "SAT2");
                            double.TryParse(oo.ReturnTag(sql, "Test1"), out Test3);
                            double.TryParse(oo.ReturnTag(sql, "SE"), out SE2);
                            double.TryParse(oo.ReturnTag(sql, "MA"), out MA2);
                            double.TryParse(oo.ReturnTag(sql, "Port"), out Port2);
                            double.TryParse(oo.ReturnTag(sql, "SAT"), out AE);
                            double.TryParse(oo.ReturnTag(sql, "SAT2"), out AE2);
                            Best2 = (Test3 * 20) / MMT3;
                            lblBest2.Text = Best2.ToString("0");
                            Conv10_2 = (double.Parse(Best2.ToString("0")) * 5) / MMT3;
                            lblConv102.Text = Conv10_2.ToString("0.00");
                            Total2 = double.Parse((Conv10_2 + SE2+MA2+Port2 + (AE > AE2 ? AE : AE2)).ToString("0"));
                            lblTotal2.Text = Total2.ToString("0");
                        }
                    }
                }
            }
            createTitle();
        }

    }
    
    private void hideColumn()
    {
        string sqlAd = "select count(*) cnt from TTSubjectMaster where IsAditional=1 and Id=" + drpSubjectGroup.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and classid=" + drpclass.SelectedValue + "";
        bool isAdditional = false;
        if (int.Parse(oo.ReturnTag(sqlAd, "cnt")) > 0)
        {
            isAdditional = true;
        }
        string ClassName = drpclass.SelectedItem.Text;
        int Evail = drpEval.SelectedIndex;
        if (Evail==1)
        {
            if (isAdditional)
            {

                GridView1.Columns[8].Visible = false;
                GridView1.Columns[9].Visible = false;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                GridView1.Columns[19].Visible = false;
                GridView1.Columns[20].Visible = false;
                GridView1.Columns[21].Visible = false;
                GridView1.Columns[22].Visible = false;
                GridView1.Columns[23].Visible = false;
                GridView1.Columns[24].Visible = false;
                GridView1.Columns[25].Visible = false;
            }
            else
            {
                GridView1.Columns[12].Visible = false;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                GridView1.Columns[19].Visible = false;
                GridView1.Columns[20].Visible = false;
                GridView1.Columns[21].Visible = false;
                GridView1.Columns[22].Visible = false;
                GridView1.Columns[23].Visible = false;
                GridView1.Columns[24].Visible = false;
                GridView1.Columns[25].Visible = false;
            }

        }
        if (Evail == 2)
        {
            if (isAdditional)
            {
                if (ClassName.ToUpper() == "IX")
                {
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[5].Visible = false;
                    GridView1.Columns[6].Visible = false;
                    GridView1.Columns[7].Visible = false;
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                    GridView1.Columns[12].Visible = false;
                    GridView1.Columns[13].Visible = false;
                    GridView1.Columns[18].Visible = false;
                    GridView1.Columns[19].Visible = false;
                    GridView1.Columns[22].Visible = false;
                    GridView1.Columns[25].Visible = false;
                }
                if (ClassName.ToUpper() == "X")
                {
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[5].Visible = false;
                    GridView1.Columns[6].Visible = false;
                    GridView1.Columns[7].Visible = false;
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                    GridView1.Columns[12].Visible = false;
                    GridView1.Columns[13].Visible = false;
                    GridView1.Columns[15].Visible = false;
                    GridView1.Columns[18].Visible = false;
                    GridView1.Columns[19].Visible = false;
                    //GridView1.Columns[23].Visible = false;
                    GridView1.Columns[25].Visible = false;
                    var PreBoard1=(Label)GridView1.HeaderRow.FindControl("Labe19");
                    PreBoard1.Text = "Pre. Board 1";
                    var lblMMHY2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2");
                    lblMMHY2.Text = "50";
                    var lblMMHY2_2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2_2");
                    lblMMHY2_2.Text = "50";
                }
            }
            else
            {
                if (ClassName.ToUpper() == "IX")
                {
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[5].Visible = false;
                    GridView1.Columns[6].Visible = false;
                    GridView1.Columns[7].Visible = false;
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                    GridView1.Columns[12].Visible = false;
                    GridView1.Columns[13].Visible = false;
                    GridView1.Columns[22].Visible = false;
                    GridView1.Columns[23].Visible = false;
                    GridView1.Columns[25].Visible = false;
                }
                if (ClassName.ToUpper() == "X")
                {
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[5].Visible = false;
                    GridView1.Columns[6].Visible = false;
                    GridView1.Columns[7].Visible = false;
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                    GridView1.Columns[12].Visible = false;
                    GridView1.Columns[13].Visible = false;
                    GridView1.Columns[15].Visible = false;
                    GridView1.Columns[23].Visible = false;
                    GridView1.Columns[25].Visible = false;
                    var PreBoard1 = (Label)GridView1.HeaderRow.FindControl("Labe19");
                    PreBoard1.Text = "Pre. Board 1";
                    var lblMMHY2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2");
                    lblMMHY2.Text = "80";
                    var lblMMHY2_2 = (Label)GridView1.HeaderRow.FindControl("lblMMHY2_2");
                    lblMMHY2_2.Text = "80";
                }
            }
        }
        
    }
    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
        GridView1.DataSource = null;
        GridView1.DataBind();
        divExport.Visible = false;
    }
    
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "SubjectwiseCumlativeIXtoX", table1);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "SubjectwiseCumlativeIXtoX", table1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        GridView1.Style.Add("text-transform", "uppercase");
        oo.ExportDivToExcelWithFormatting(Response, "SubjectwiseCumlativeIXtoX.xls", table1, Server.MapPath("~/Admin/css/style.css"));
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