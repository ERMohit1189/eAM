using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

public partial class staff_MarkEntryReport : Page
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
        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            Response.Redirect("MarksEntryReportVItoVIII_1718.aspx?check=MarkEntryReport_VItoVIII");
            loadclass();
            loadsection();
            loadSubject();
            string sql = "";
            Image1.ImageUrl = ResolveClientUrl("~/DisplayImage.ashx?UserLoginID=" + 1);
            sql = "Select CollegeName from CollegeMaster where CollegeId=" + 1;
            lblCollegeName.Text = oo.ReturnTag(sql, "CollegeName");
            lblSessionName.Text = Session["SessionName"].ToString();
        }
    }
    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G4' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
            sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
    }

    //--For Admin------------------------------------------------------------------------------------------------------------
    //public void loadclass()
    //{
    //    sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
    //    sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
    //    sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and (GroupId='G4' or GroupId='G5') Order by CIDOrder";
    //    oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
    //}

    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SubjectName,Id from SubjectMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and  SectionName='" + drpsection.SelectedItem.ToString() + "' ";
            sql +=  " and (IsForExam is null or IsForExam ='1') and SessionName='" + Session["SessionName"].ToString() + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select Distinct sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
            sql +=  " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql +=  " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID";
            sql +=  " where sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
            sql +=  " and sctm.SectionName='" + drpsection.SelectedItem.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and (IsForExam is null or IsForExam ='1') and sctm.Ecode='" + Session["LoginName"].ToString() + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }
    public void loadsection()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown(sql, drpsection, "SectionName");

        }
        else
        {
            sql = "Select sm.SectionName from SubjectClassTeacherMaster sctm";
            sql +=  " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.SessionName=sctm.SessionName ";
            sql +=  " and sm.SectionName=sctm.SectionName";
            sql +=  " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";

            oo.FillDropDown(sql, drpsection, "SectionName");
        }
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadSubject();
        loadgrid();
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubject();
        loadgrid();
    }

    public void loadgrid()
    {
        try
        {
            sql = "Select SG.SrNo,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name from StudentGenaralDetail SG";
            sql +=  " inner join StudentOfficialDetails SO on SG.SrNo=SO.SrNo";
            sql +=  " inner join ClassMaster cm on cm.Id=SO.AdmissionForClassId";
            sql +=  " inner join SectionMaster scm on scm.Id=SO.SectionId";
            sql +=  " where SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' and SO.SectionId='" + drpsection.SelectedValue.ToString() + "' and scm.ClassNameId='" + drpclass.SelectedValue.ToString() + "'";
            sql +=  " and scm.SessionName='" + Session["SessionName"].ToString() + "' and SO.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and SG.SessionName='" + Session["SessionName"].ToString() + "'";
            sql +=  " and SO.Withdrwal is null  order by Name Asc";
            if (drpEval.SelectedValue.ToUpper() == "ALL")
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView4.DataSource = null;
                GridView4.DataBind();
                GridView1.DataSource = oo.GridFill(sql);
                GridView1.DataBind();
            }
            else if (drpEval.SelectedValue == "FA1")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView4.DataSource = null;
                GridView4.DataBind();
                GridView2.DataSource = oo.GridFill(sql);
                GridView2.DataBind();

            }
            else if (drpEval.SelectedValue == "FA2" || drpEval.SelectedValue == "FA3" || drpEval.SelectedValue == "FA4")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView4.DataSource = null;
                GridView4.DataBind();
                GridView3.DataSource = oo.GridFill(sql);
                GridView3.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView4.DataSource = oo.GridFill(sql);
                GridView4.DataBind();
            }

            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    double tenPercentsa1sa2 = 0;

                    double tenPercentfa1 = 0;
                    double tenPercentfa2 = 0;
                    double tenPercentsa1 = 0;
                    double tenPercentfasa1 = 0;

                    double tenPercentfa3 = 0;
                    double tenPercentfa4 = 0;
                    double tenPercentsa2 = 0;
                    double tenPercentfasa2 = 0;



                    double tenPercentfasa = 0;

                    Label SubjectId = (Label)GridView1.Rows[i].FindControl("Label87");

                    Label lblgrandTotalPer = (Label)GridView1.Rows[i].FindControl("lblgrandTotalPer");
                    Label lblgrandTotal = (Label)GridView1.Rows[i].FindControl("lblgrandTotal");

                    Label lblFASA1Per = (Label)GridView1.Rows[i].FindControl("lblFASA1Per");

                    Label lblFASA1Grade = (Label)GridView1.Rows[i].FindControl("lblFASA1");

                    Label lblFASA2Per = (Label)GridView1.Rows[i].FindControl("lblFASA2Per");
                    Label lblFASA2Grade = (Label)GridView1.Rows[i].FindControl("lblFASA2");

                    Label lblTotalFASAPer = (Label)GridView1.Rows[i].FindControl("lblTotalFASAPer");
                    Label lblTotalFASA = (Label)GridView1.Rows[i].FindControl("lblTotalFASA");

                    Label lblTotalAllFaPer = (Label)GridView1.Rows[i].FindControl("lblTotalFaPer");
                    Label lblTotalAllFa = (Label)GridView1.Rows[i].FindControl("lblTotalFa");


                    tenPercentfa1 = loadFA1Data(i, "FA1", "Label2", "Label6", "Label9", "Label12", "Label15", "Label18", "Label19", "Label20", "Label21");
                    tenPercentfa2 = loadFA1Data2(i, "FA2", "Label2", "Label23", "Label24", "Label25", "Label87", "Label88", "Label89", "Label28", "Label31", "Label34", "Label37", "Label38", "Label39", "Label40");
                    tenPercentsa1 = loadSAData(GridView1, i, "SA1", "Label2", "Label99", "Label100", "Label101");

                    tenPercentfasa1 = tenPercentfa1 + tenPercentfa2 + tenPercentsa1;

                    lblFASA1Per.Text = tenPercentfasa1.ToString(CultureInfo.CurrentCulture);
                    lblFASA1Grade.Text = grade(tenPercentfasa1 / 5);

                    tenPercentfa3=loadFA1Data2(i, "FA3", "Label2", "Label44", "Label45", "Label46", "Label92", "Label91", "Label90", "Label49", "Label52", "Label55", "Label58", "Label59", "Label60", "Label61");
                    tenPercentfa4=loadFA1Data2(i, "FA4", "Label2", "Label63", "Label64", "Label65", "Label93", "Label94", "Label95", "Label68", "Label71", "Label74", "Label77", "Label78", "Label79", "Label80");

                    tenPercentsa2 = loadSAData(GridView1, i, "SA2", "Label2", "Label102", "Label103", "Label104");

                    tenPercentfasa2 = tenPercentfa3 + tenPercentfa4 + tenPercentsa2;

                    tenPercentsa1sa2 = tenPercentsa1 + tenPercentsa2;
                    lblFASA2Per.Text = tenPercentfasa2.ToString(CultureInfo.CurrentCulture);
                    lblFASA2Grade.Text = grade(tenPercentfasa2 / 5);

                    tenPercentfasa = tenPercentfasa1 + tenPercentfasa2;

                    lblgrandTotalPer.Text = tenPercentsa1sa2.ToString(CultureInfo.CurrentCulture);
                    lblgrandTotal.Text = grade(tenPercentsa1sa2 / (2 * 3));

                    lblTotalAllFaPer.Text = (tenPercentfa1 + tenPercentfa2 + tenPercentfa3 + tenPercentfa4).ToString(CultureInfo.CurrentCulture);
                    lblTotalAllFa.Text = grade((tenPercentfa1 + tenPercentfa2 + tenPercentfa3 + tenPercentfa4) / 4);

                    lblTotalFASAPer.Text = tenPercentfasa.ToString(CultureInfo.CurrentCulture);
                    lblTotalFASA.Text = grade(tenPercentfasa / (2 * 5));
                }
            }
            else if (GridView2.Rows.Count > 0)
            {
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    loadFA1Data1(i, drpEval.SelectedItem.ToString().ToUpper(), "Label2", "Label6", "Label9", "Label12", "Label15", "Label18", "Label19", "Label20", "Label21");
                }
            }
            else if (GridView3.Rows.Count > 0)
            {
                for (int i = 0; i < GridView3.Rows.Count; i++)
                {
                    loadFA1Data3(i, drpEval.SelectedItem.ToString().ToUpper(), "Label2", "Label4", "Label5", "Label6", "Label96", "Label97", "Label98", "Label9", "Label12", "Label15", "Label18", "Label19", "Label20", "Label21");
                }
            }
            else if (GridView4.Rows.Count > 0)
            {
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {
                    loadSAData(GridView4,i, drpEval.SelectedItem.ToString().ToUpper(), "Label2", "Label6", "Label20", "Label21");
                }
            }
        }
        catch
        {
        }
        if (drpEval.SelectedIndex != 0)
        {
            lblEval.Text = drpEval.SelectedItem.Text.Trim();
        }
        else { lblEval.Text = ""; }
    }

    public string grade(double percentle)
    {
        if (percentle >= 0 && percentle <= 1.9)
        {
            return "E2";
        }
        else if (percentle > 1.9 && percentle <= 3.2)
        {
            return "E1";
        }
        else if (percentle > 3.2 && percentle <= 4)
        {
            return "D";
        }
        else if (percentle > 4 && percentle <= 5)
        {
            return "C2";
        }
        else if (percentle > 5 && percentle <= 6)
        {
            return "C1";
        }
        else if (percentle > 6 && percentle <= 7)
        {
            return "B2";
        }
        else if (percentle > 7 && percentle <= 8)
        {
            return "B1";
        }
        else if (percentle > 8 && percentle <= 9)
        {
            return "A2";
        }
        else if (percentle > 9 && percentle <= 10)
        {
            return "A1";
        }
        else
        {
            return "";
        }
    }

    public double loadFA1Data(int i, string Eval, string Srno, string ut, string act1, string act2, string hw_cw, string att, string total, string percent, string grade)
    {
        Label srno = (Label)GridView1.Rows[i].FindControl(Srno);
        Label UT = (Label)GridView1.Rows[i].FindControl(ut);
        Label ACT1 = (Label)GridView1.Rows[i].FindControl(act1);
        Label ACT2 = (Label)GridView1.Rows[i].FindControl(act2);
        Label HW_CW = (Label)GridView1.Rows[i].FindControl(hw_cw);
        Label ATT = (Label)GridView1.Rows[i].FindControl(att);
        Label Total = (Label)GridView1.Rows[i].FindControl(total);
        Label TenPercent = (Label)GridView1.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView1.Rows[i].FindControl(grade);

        sql = "Select UT,ACT1,ACT2,HW_CW,ATT,Total,TenPercent,Grade from CCE where SrNo='"+srno.Text+"' and Evaluation='"+Eval+"' and ClassId='"+drpclass.SelectedValue.ToString()+"' and SessionName='"+Session["SessionName"].ToString()+"' and SubjectId='"+drpSubject.SelectedValue.ToString()+"'";

        UT.Text = oo.ReturnTag(sql, "UT").ToString();
        ACT1.Text = oo.ReturnTag(sql, "ACT1").ToString();
        ACT2.Text = oo.ReturnTag(sql, "ACT2").ToString();
        HW_CW.Text = oo.ReturnTag(sql, "HW_CW").ToString();
        ATT.Text = oo.ReturnTag(sql, "ATT").ToString();
        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        TenPercent.Text = oo.ReturnTag(sql, "TenPercent").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

        double tenPercent = 0;
        if (oo.ReturnTag(sql, "TenPercent") != string.Empty)
        {
            tenPercent = Convert.ToDouble(oo.ReturnTag(sql, "TenPercent").ToString());
        }
        return tenPercent;
    }

    public double loadSAData(GridView Grd,int i, string Eval, string Srno, string ut, string percent, string grade)
    {
        Label srno = (Label)Grd.Rows[i].FindControl(Srno);
        Label UT = (Label)Grd.Rows[i].FindControl(ut);
        Label TenPercent = (Label)Grd.Rows[i].FindControl(percent);
        Label Grade = (Label)Grd.Rows[i].FindControl(grade);
        double tenPercent = 0;
        sql = "Select UT,TenPercent,Grade from CCE where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";

        UT.Text = oo.ReturnTag(sql, "UT").ToString();
        TenPercent.Text = oo.ReturnTag(sql, "TenPercent").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

        if (oo.ReturnTag(sql, "TenPercent") != string.Empty)
        {
            tenPercent = Convert.ToDouble(oo.ReturnTag(sql, "TenPercent").ToString());
        }

        return tenPercent;

    }

    public void loadFA1Data1(int i, string Eval, string Srno, string ut, string act1, string act2, string hw_cw, string att, string total, string percent, string grade)
    {
        Label srno = (Label)GridView2.Rows[i].FindControl(Srno);
        Label UT = (Label)GridView2.Rows[i].FindControl(ut);
        Label ACT1 = (Label)GridView2.Rows[i].FindControl(act1);
        Label ACT2 = (Label)GridView2.Rows[i].FindControl(act2);
        Label HW_CW = (Label)GridView2.Rows[i].FindControl(hw_cw);
        Label ATT = (Label)GridView2.Rows[i].FindControl(att);
        Label Total = (Label)GridView2.Rows[i].FindControl(total);
        Label TenPercent = (Label)GridView2.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView2.Rows[i].FindControl(grade);

        sql = "Select UT,ACT1,ACT2,HW_CW,ATT,Total,TenPercent,Grade from CCE where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";

        UT.Text = oo.ReturnTag(sql, "UT").ToString();
        ACT1.Text = oo.ReturnTag(sql, "ACT1").ToString();
        ACT2.Text = oo.ReturnTag(sql, "ACT2").ToString();
        HW_CW.Text = oo.ReturnTag(sql, "HW_CW").ToString();
        ATT.Text = oo.ReturnTag(sql, "ATT").ToString();
        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        TenPercent.Text = oo.ReturnTag(sql, "TenPercent").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

    }

    public double loadFA1Data2(int i, string Eval, string Srno, string test1,string test2,string test3,string test4,string test5,string test6, string act1, string act2, string hw_cw, string att, string total, string percent, string grade)
    {
        Label srno = (Label)GridView1.Rows[i].FindControl(Srno);
        Label Test1 = (Label)GridView1.Rows[i].FindControl(test1);
        Label Test2 = (Label)GridView1.Rows[i].FindControl(test2);
        Label Test3 = (Label)GridView1.Rows[i].FindControl(test3);
        Label Test4 = (Label)GridView1.Rows[i].FindControl(test4);
        Label Test5 = (Label)GridView1.Rows[i].FindControl(test5);
        Label Test6 = (Label)GridView1.Rows[i].FindControl(test6);
        Label ACT1 = (Label)GridView1.Rows[i].FindControl(act1);
        Label ACT2 = (Label)GridView1.Rows[i].FindControl(act2);
        Label HW_CW = (Label)GridView1.Rows[i].FindControl(hw_cw);
        Label ATT = (Label)GridView1.Rows[i].FindControl(att);
        Label Total = (Label)GridView1.Rows[i].FindControl(total);
        Label TenPercent = (Label)GridView1.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView1.Rows[i].FindControl(grade);

        sql = "Select Test1,Test2,Test3,Test4,Test5,Test6,ACT1,ACT2,HW_CW,ATT,Total,TenPercent,Grade from CCE_New1 where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";

        Test1.Text = oo.ReturnTag(sql, "Test1").ToString();
        Test2.Text = oo.ReturnTag(sql, "Test2").ToString();
        Test3.Text = oo.ReturnTag(sql, "Test3").ToString();
        Test4.Text = oo.ReturnTag(sql, "Test4").ToString();
        Test5.Text = oo.ReturnTag(sql, "Test5").ToString();
        Test6.Text = oo.ReturnTag(sql, "Test6").ToString();
        ACT1.Text = oo.ReturnTag(sql, "ACT1").ToString();
        ACT2.Text = oo.ReturnTag(sql, "ACT2").ToString();
        HW_CW.Text = oo.ReturnTag(sql, "HW_CW").ToString();
        ATT.Text = oo.ReturnTag(sql, "ATT").ToString();
        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        TenPercent.Text = oo.ReturnTag(sql, "TenPercent").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

        double tenPercent = 0;
        if (oo.ReturnTag(sql, "TenPercent") != string.Empty)
        {
            tenPercent = Convert.ToDouble(oo.ReturnTag(sql, "TenPercent").ToString());
        }

        return tenPercent;

    }

    public void loadFA1Data3(int i, string Eval, string Srno, string test1, string test2, string test3,string test4,string test5,string test6, string act1, string act2, string hw_cw, string att, string total, string percent, string grade)
    {
        Label srno = (Label)GridView3.Rows[i].FindControl(Srno);
        Label Test1 = (Label)GridView3.Rows[i].FindControl(test1);
        Label Test2 = (Label)GridView3.Rows[i].FindControl(test2);
        Label Test3 = (Label)GridView3.Rows[i].FindControl(test3);
        Label Test4 = (Label)GridView3.Rows[i].FindControl(test4);
        Label Test5 = (Label)GridView3.Rows[i].FindControl(test5);
        Label Test6 = (Label)GridView3.Rows[i].FindControl(test6);
        Label ACT1 = (Label)GridView3.Rows[i].FindControl(act1);
        Label ACT2 = (Label)GridView3.Rows[i].FindControl(act2);
        Label HW_CW = (Label)GridView3.Rows[i].FindControl(hw_cw);
        Label ATT = (Label)GridView3.Rows[i].FindControl(att);
        Label Total = (Label)GridView3.Rows[i].FindControl(total);
        Label TenPercent = (Label)GridView3.Rows[i].FindControl(percent);
        Label Grade = (Label)GridView3.Rows[i].FindControl(grade);

        sql = "Select Test1,Test2,Test3,Test4,Test5,Test6,ACT1,ACT2,HW_CW,ATT,Total,TenPercent,Grade from CCE_New1 where SrNo='" + srno.Text + "' and Evaluation='" + Eval + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";

        Test1.Text = oo.ReturnTag(sql, "Test1").ToString();
        Test2.Text = oo.ReturnTag(sql, "Test2").ToString();
        Test3.Text = oo.ReturnTag(sql, "Test3").ToString();
        Test4.Text = oo.ReturnTag(sql, "Test4").ToString();
        Test5.Text = oo.ReturnTag(sql, "Test5").ToString();
        Test6.Text = oo.ReturnTag(sql, "Test6").ToString();
        ACT1.Text = oo.ReturnTag(sql, "ACT1").ToString();
        ACT2.Text = oo.ReturnTag(sql, "ACT2").ToString();
        HW_CW.Text = oo.ReturnTag(sql, "HW_CW").ToString();
        ATT.Text = oo.ReturnTag(sql, "ATT").ToString();
        Total.Text = oo.ReturnTag(sql, "Total").ToString();
        TenPercent.Text = oo.ReturnTag(sql, "TenPercent").ToString();
        Grade.Text = oo.ReturnTag(sql, "Grade").ToString();

    }

    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, "FA1", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, "FA2", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 3, "SA1", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 2, "FA1+FA2+SA1", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, "FA3", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, "FA4", System.Drawing.Color.LightGray.Name);
            //AddMergedCells(objgridviewrow, objtablecell, 1, "", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 3, "SA2", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 2, "FA3+FA4+SA2", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 2, "FA1+FA2+FA3+FA4", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 2, "SA1+SA2", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 2, "FA+SA", System.Drawing.Color.LightGray.Name);

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
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, drpEval.SelectedItem.ToString().ToUpper(), System.Drawing.Color.LightGray.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, drpEval.SelectedItem.ToString().ToUpper(), System.Drawing.Color.LightGray.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.CssClass = "valign-m";
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response,drpEval.SelectedItem.Text+"_"+drpSubject.SelectedItem.Text+"_MarksEntryReport.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel(drpEval.SelectedItem.Text+"_"+drpSubject.SelectedItem.Text+"_MarksEntryReport.xls", GridView1);
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('../Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 4, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, "#", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "S.R.No.", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 1, "Student's Name", System.Drawing.Color.LightGray.Name);
            AddMergedCells(objgridviewrow, objtablecell, 8, drpEval.SelectedItem.ToString().ToUpper(), System.Drawing.Color.LightGray.Name);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }
}