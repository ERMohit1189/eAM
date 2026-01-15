using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;

public partial class staff_MarkEntryReportVItoVIII_1617 : System.Web.UI.Page
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
        if (Session["SessionName"].ToString() == "2017-2018")
        {
            Response.Redirect("MarksEntryReportVItoVIII_1718.aspx?check=MarkEntryReport_VItoVIII");
        }
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader); 
        BLL.BLLInstance.LoadHeader("Examination", header);
        if (!IsPostBack)
        {
            loadclass();
            loadsection();
            loadSubject();
        }
    }



    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql = sql + " where cm.SessionName='" + Session["SessionName"] + "' and GroupId='G4' Order by CIDOrder";
            oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
        }
        else
        {
            sql = "Select Count(*) Count from ClassTeacherMaster ctm";
            sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
            sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
            sql = sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and t1.GroupId='G4'";

            Session["count"] = BAL.objBal.ReturnTag(sql, "Count");

            if (Convert.ToInt16(Session["count"].ToString()) > 0)
            {
                sql = "Select Distinct ClassName,cm.Id,CIDOrder from ClassTeacherMaster ctm";
                sql = sql + " inner join ClassMaster cm on cm.Id=ctm.ClassId and cm.SessionName=ctm.SessionName";
                sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id and cm.SessionName=ctm.SessionName";
                sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
                sql = sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and t1.GroupId='G4'";
                sql = sql + " UNION ";
                sql = sql + " Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql = sql + " where GroupId='G4'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

            }
            else
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql = sql + " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql = sql + " where GroupId='G4'  and cm.SessionName='" + Session["SessionName"].ToString() + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' Order by CIDOrder";
                oo.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
        }
    }

    public void loadsection()
    {
        sql = "Select Count(*) Count from ClassTeacherMaster ctm";
        sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
        sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
        sql = sql + " and t1.GroupId='G4' and ctm.ClassId='" + drpclass.SelectedValue.ToString() + "'";

        Session["count"] = BAL.objBal.ReturnTag(sql, "Count");


        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select SectionName,id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
            drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }
        else
        {
            if (Convert.ToInt16(Session["count"].ToString()) > 0)
            {
                sql = "Select Distinct SectionName,sm.Id from ClassTeacherMaster ctm";
                sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId and t1.SessionName=ctm.SessionName";
                sql = sql + " inner join SectionMaster sm on sm.Id=ctm.SectionId and sm.SessionName=ctm.SessionName";
                sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
                sql = sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' and t1.GroupId='G4' and ctm.ClassId='" + drpclass.SelectedValue.ToString() + "'";
                sql = sql + " UNION";
                sql = sql + " Select Distinct sm.SectionName,sm.id from SubjectClassTeacherMaster sctm";
                sql = sql + " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.SectionName=sctm.SectionName and sm.SessionName=sctm.SessionName ";
                sql = sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";

                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");

                drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));

                //oo.FillDropDown(sql, drpsection, "SectionName");

            }
            else
            {
                sql = "Select Distinct sm.SectionName,sm.id from SubjectClassTeacherMaster sctm";
                sql = sql + " inner join SectionMaster sm on sm.ClassNameId=sctm.ClassId and sm.SessionName=sctm.SessionName ";
                sql = sql + " and sm.SectionName=sctm.SectionName";
                sql = sql + " where sctm.BranchCode=" + Session["BranchCode"].ToString() + " and sctm.SessionName='" + Session["SessionName"].ToString() + "' and ClassId='" + drpclass.SelectedValue.ToString() + "' and Ecode='" + Session["LoginName"].ToString() + "'";
                oo.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
                drpsection.Items.Insert(0, new ListItem("<--Select-->", "0"));
            }
        }
    }

    private void loadSubjectGroup()
    {

        sql = "Select Count(*) Count from ClassTeacherMaster ctm";
        sql = sql + " inner join dt_ClassGroupMaster t1 on t1.ClassId=ctm.ClassId";
        sql = sql + " where EmpCode='" + Session["LoginName"].ToString() + "' and IsClassTeacher=1";
        sql = sql + " and ctm.SessionName='" + Session["SessionName"].ToString() + "' ";
        sql = sql + " and Sectionid='" + drpsection.SelectedValue.ToString() + "' and t1.GroupId='G4'";

        Session["count"] = BAL.objBal.ReturnTag(sql, "Count");


        if (Session["Logintype"].ToString() == "Admin" || Convert.ToInt16(Session["count"].ToString()) > 0)
        {
            sql = "Select SubjectGroup,Id from SubjectGroupMaster where ClassId='" + drpclass.SelectedValue.ToString() + "' and SectionName='" + drpsection.SelectedItem.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "' and IsForOnlyExam=1";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select  Distinct sgm.SubjectGroup,sgm.Id from SubjectClassTeacherMaster sctm";
            sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid and sm.SessionName=sctm.SessionName";
            sql = sql + " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sctm.SessionName";
            sql = sql + " Inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sctm.SessionName";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
            sql = sql + " and sctm.SectionName='" + drpsection.SelectedItem.Text.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and (IsForExam is null or IsForExam ='1')";

            oo.FillDropDown_withValue(sql, drpSubjectGroup, "SubjectGroup", "Id");
            drpSubjectGroup.Items.Insert(0, "<--Select-->");
        }
    }

    public void loadSubject()
    {
        if (Session["Logintype"].ToString() == "Admin" || Convert.ToInt16(Session["count"].ToString()) > 0)
        {
            //sql = "Select SubjectName,Id from SubjectMaster where GroupId='" + drpSubjectGroup.SelectedValue.ToString() + "' and SessionName='" + Session["SessionName"].ToString() + "'";
            sql = "Select SubjectName,sm.Id from SubjectMaster sm";
            sql = sql + " inner join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID and spm.SessionName=sm.SessionName";
            sql = sql + " inner join SubjectGroupMaster sgm on sgm.Id=spm.SubjectGroupID and sgm.SessionName=sm.SessionName";
            sql = sql + " where sgm.Id='" + drpSubjectGroup.SelectedValue.ToString() + "' and sm.SessionName='" + Session["SessionName"].ToString() + "' and (IsForExam=1 or IsForExam is null)  order by sm.id";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
        else
        {
            sql = "Select sm.SubjectName,sctm.Subjectid as Id from SubjectClassTeacherMaster sctm";
            sql = sql + " inner join SubjectMaster sm on sm.Id=sctm.Subjectid";
            sql = sql + " Left join S02_SubjectPaperMaster spm on spm.S02ID=sm.PaperID";
            sql = sql + " where Ecode='" + Session["LoginName"].ToString() + "' and sctm.ClassId='" + drpclass.SelectedValue.ToString() + "'  ";
            sql = sql + " and sctm.SectionName='" + drpsection.SelectedItem.ToString() + "' and sctm.SessionName='" + Session["SessionName"].ToString() + "'";
            sql = sql + " and (IsForExam is null or IsForExam ='1') and spm.SubjectGroupID='" + drpSubjectGroup.SelectedValue.ToString() + "'";

            oo.FillDropDown_withValue(sql, drpSubject, "SubjectName", "Id");
            drpSubject.Items.Insert(0, "<--Select-->");
        }
    }

    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
        loadSubject();
  
    }
    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjectGroup();
        loadSubject();
        loadgrid();
    }

    public void loadgrid()
    {
        sql = "select   So.srno,(SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as Name from StudentGenaralDetail SG ";
        sql = sql + "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql = sql + "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql = sql + "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql = sql + "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql = sql + "   where  CM.ClassName='" + drpclass.SelectedItem.ToString() + "'";
        sql = sql + "   and SC.SectionName='" + drpsection.SelectedItem.ToString() + "' and sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql = sql + "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + "   and SC.SessionName='" + Session["SessionName"].ToString() + "'  and";
        sql = sql + "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "   and SO.Withdrwal is null order by FirstName Asc";
        if (drpEval.SelectedValue == "FA1" || drpEval.SelectedValue == "FA2" || drpEval.SelectedValue == "FA3" || drpEval.SelectedValue == "FA4")
        {
            GridView2.DataSource = oo.GridFill(sql);
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
        }
        else
        {
            GridView3.DataSource = oo.GridFill(sql);
            GridView3.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }

        if (GridView2.Rows.Count > 0)
        {
            table3.Visible = true;
            table4.Visible = false;
        }
        else
        {
            table4.Visible = true;
            table3.Visible = false; 
        }

        setLableText();
        
    }

    public void setLableText()
    {
        lblSubject.Text = drpSubject.SelectedItem.Text.Trim();
        lblClass.Text = drpclass.SelectedItem.Text.Trim();
        lblSection.Text = drpsection.SelectedItem.Text.Trim();
        lblEval.Text = drpEval.SelectedItem.Text.Trim();
        lblDate.Text = DateTime.Today.ToString("dd MMM yyyy");
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

          
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
       oo.ExportToWord(Response,drpEval.SelectedItem.Text+"_"+drpSubject.SelectedItem.Text+"_MarksEntryReport_VItoVIII.doc", divExport);       
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, drpEval.SelectedItem.Text + "_" + drpSubject.SelectedItem.Text + "_MarksEntryReport_VItoVIII.xls", divExport, Server.MapPath("~/css/theme.min.css"));  
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
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
        if (drpEval.SelectedValue == "FA1" || drpEval.SelectedValue == "FA2" || drpEval.SelectedValue == "FA3" || drpEval.SelectedValue == "FA4")
        {
            loaddataforgridview2();
        }
        else
        {
            loaddataforgridview3();
        }
        ScriptManager.RegisterClientScriptBlock(drpEval, this.GetType(), "scroll", "DoubleScroll('doublescroll');", true);
       
    }

    #region load MaxMarks

    private void loadFAMM()
    {
        if (GridView2.Rows.Count > 0)
        {

            Label lblMM1 = (Label)GridView2.HeaderRow.FindControl("PPTMM1");
            Label lblMM2 = (Label)GridView2.HeaderRow.FindControl("PPTMM2");
            Label lblMM3 = (Label)GridView2.HeaderRow.FindControl("PPTMM3");
            Label lblMM4 = (Label)GridView2.HeaderRow.FindControl("PPTMM4");

            Label lblA1MM = (Label)GridView2.HeaderRow.FindControl("CWMM");
            Label lblA2MM = (Label)GridView2.HeaderRow.FindControl("LabMM");
            Label lblA3MM = (Label)GridView2.HeaderRow.FindControl("WBMM");
            Label lblA4MM = (Label)GridView2.HeaderRow.FindControl("MBMM");
            Label lblA5MM = (Label)GridView2.HeaderRow.FindControl("WAMM");

            Label lblG1MM = (Label)GridView2.HeaderRow.FindControl("MMMM");
            Label lblG2MM = (Label)GridView2.HeaderRow.FindControl("DebMM");
            Label lblG3MM = (Label)GridView2.HeaderRow.FindControl("ResMM");
            Label lblG4MM = (Label)GridView2.HeaderRow.FindControl("GroupMM");
            Label lblG5MM = (Label)GridView2.HeaderRow.FindControl("HouseMM");
            Label lblG6MM = (Label)GridView2.HeaderRow.FindControl("WrittenMM");

            sql = "Select *from CCEMMarksForVItoX where Eval='" + drpEval.SelectedItem.Text.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            lblMM1.Text = oo.ReturnTag(sql, "P1") == string.Empty ? "20" : oo.ReturnTag(sql, "P1");
            lblMM2.Text = oo.ReturnTag(sql, "P2") == string.Empty ? "20" : oo.ReturnTag(sql, "P2");
            lblMM3.Text = oo.ReturnTag(sql, "P3") == string.Empty ? "20" : oo.ReturnTag(sql, "P3");
            lblMM4.Text = oo.ReturnTag(sql, "P4") == string.Empty ? "20" : oo.ReturnTag(sql, "P4");

            lblA1MM.Text = oo.ReturnTag(sql, "A1") == string.Empty ? "5" : oo.ReturnTag(sql, "A1");
            lblA2MM.Text = oo.ReturnTag(sql, "A2") == string.Empty ? "5" : oo.ReturnTag(sql, "A2");
            lblA3MM.Text = oo.ReturnTag(sql, "A3") == string.Empty ? "5" : oo.ReturnTag(sql, "A3");
            lblA4MM.Text = oo.ReturnTag(sql, "A4") == string.Empty ? "5" : oo.ReturnTag(sql, "A4");
            lblA5MM.Text = oo.ReturnTag(sql, "A5") == string.Empty ? "10" : oo.ReturnTag(sql, "A5");

            lblG1MM.Text = oo.ReturnTag(sql, "G1") == string.Empty ? "5" : oo.ReturnTag(sql, "G1");
            lblG2MM.Text = oo.ReturnTag(sql, "G2") == string.Empty ? "5" : oo.ReturnTag(sql, "G2");
            lblG3MM.Text = oo.ReturnTag(sql, "G3") == string.Empty ? "5" : oo.ReturnTag(sql, "G3");
            lblG4MM.Text = oo.ReturnTag(sql, "G4") == string.Empty ? "5" : oo.ReturnTag(sql, "G4");
            lblG5MM.Text = oo.ReturnTag(sql, "G5") == string.Empty ? "5" : oo.ReturnTag(sql, "G5");
            lblG6MM.Text = oo.ReturnTag(sql, "G6") == string.Empty ? "10" : oo.ReturnTag(sql, "G6");
        }
    }

    private void loadSAMM()
    {
        if (GridView3.Rows.Count > 0)
        {

            Label lblSATMM = (Label)GridView3.HeaderRow.FindControl("SATMM");

            sql = "Select SAT from CCEMMarksForVItoX where Eval='" + drpEval.SelectedItem.Text.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            lblSATMM.Text = oo.ReturnTag(sql, "SAT");

        }
    }

    #endregion
    public void loaddataforgridview2()
    {
        loadFAMM();
        foreach (GridViewRow gvr in GridView2.Rows)
        {
            Label Label18 = (Label)gvr.FindControl("Label18");

            Label Label17 = (Label)gvr.FindControl("Label17");
            Label Label30 = (Label)gvr.FindControl("Label30");
            Label Label31 = (Label)gvr.FindControl("Label31");
            Label Label32 = (Label)gvr.FindControl("Label32");

            Label Label33 = (Label)gvr.FindControl("Label33");
            Label Label34 = (Label)gvr.FindControl("Label34");
            Label Label35 = (Label)gvr.FindControl("Label35");
            Label Label36 = (Label)gvr.FindControl("Label36");
            Label Label37 = (Label)gvr.FindControl("Label37");

            Label Label38 = (Label)gvr.FindControl("Label38");
            Label Label39 = (Label)gvr.FindControl("Label39");
            Label Label40 = (Label)gvr.FindControl("Label40");
            Label Label41 = (Label)gvr.FindControl("Label41");
            Label Label42 = (Label)gvr.FindControl("Label42");
            Label Label43 = (Label)gvr.FindControl("Label43");

            Label lblsrno = (Label)gvr.FindControl("Label16");
            Label lblTotal = (Label)gvr.FindControl("Label2");
            TextBox lblPer = (TextBox)gvr.FindControl("txtSA1tenper");

            Label lblGrade = (Label)gvr.FindControl("Label4");

            sql = "Select Id,T1,T2,T3,T4,T5,T6,SAT,IA1,IA2,IA3,IA4,IA5,GA1,GA2,GA3,GA4,GA5,GA6 from CCEVItoX where SRNO='" + lblsrno.Text + "' and Evaluation='" + drpEval.SelectedItem.Text + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";

            Label18.Text = oo.ReturnTag(sql, "Id");
            Label17.Text = oo.ReturnTag(sql, "T1");
            Label30.Text = oo.ReturnTag(sql, "T2");
            Label31.Text = oo.ReturnTag(sql, "T3");
            Label32.Text = oo.ReturnTag(sql, "T4");

            Label33.Text = oo.ReturnTag(sql, "IA1");
            Label34.Text = oo.ReturnTag(sql, "IA2");
            Label35.Text = oo.ReturnTag(sql, "IA3");
            Label36.Text = oo.ReturnTag(sql, "IA4");
            Label37.Text = oo.ReturnTag(sql, "IA5");


            Label38.Text = oo.ReturnTag(sql, "GA1");
            Label39.Text = oo.ReturnTag(sql, "GA2");
            Label40.Text = oo.ReturnTag(sql, "GA3");
            Label41.Text = oo.ReturnTag(sql, "GA4");
            Label42.Text = oo.ReturnTag(sql, "GA5");
            Label43.Text = oo.ReturnTag(sql, "GA6");


            pptMarksEntry(gvr.RowIndex, "Label27", "Label29", "Label17", "Label30", "Label31", "Label32", "lblMM1");

            GAMarksEntry(gvr.RowIndex, "Label23", "Label25", "Label38", "Label39", "Label40", "Label41", "Label42", "Label43", "lblMM3");

            IAMarksEntry(gvr.RowIndex, "Label19", "Label21", "Label33", "Label34", "Label35", "Label36", "Label37", "lblMM2");
        }
    }

    public void loaddataforgridview3()
    {
        loadSAMM();
        if (GridView3.Rows.Count > 0)
        {
            string txtPPTMM1 = "0";

            sql = "Select SAT from CCEMMarksForVItoX where Eval='" + drpEval.SelectedItem.Text.ToString() + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            txtPPTMM1 = oo.ReturnTag(sql, "SAT");

            foreach (GridViewRow gvr in GridView3.Rows)
            {
                Label Label4 = (Label)gvr.FindControl("Label4");
                Label Label18 = (Label)gvr.FindControl("Label18");
                Label Label45 = (Label)gvr.FindControl("Label45");
                Label lblsrno = (Label)gvr.FindControl("Label16");
                Label Label46 = (Label)gvr.FindControl("Label46");
                sql = "Select Id,SAT from CCEVItoX where SRNO='" + lblsrno.Text + "' and Evaluation='" + drpEval.SelectedItem.Text + "' and SubjectId='" + drpSubject.SelectedValue.ToString() + "'";
                Label18.Text = oo.ReturnTag(sql, "Id");
                Label45.Text = oo.ReturnTag(sql, "SAT");
                double flag;
                if (Label45.Text == "")
                {

                }
                else
                {
                    if (Label45.Text.ToUpper() == "NAD" || Label45.Text.ToUpper() == "ML")
                    {
                        Label46.Text = "NP";
                        Label4.Text = "NP";
                    }
                    else
                    {
                        Label46.Text = Math.Round((((double.TryParse(Label45.Text, out flag) ? flag : 0) * 30) / (double.TryParse(txtPPTMM1, out flag)?flag:0)), 1).ToString(CultureInfo.CurrentCulture);
                        Label4.Text = grade(Math.Round((((double.TryParse(Label45.Text, out flag) ? flag : 0) * 30) / ((double.TryParse(txtPPTMM1, out flag) ? flag : 0)*3)), 1));
                    }

                }

            }
        }
    }
    public void pptMarksEntry(int i, string lbltotal, string lblper, string test1, string test2, string test3, string test4, string lblGtotal)
    {

        bool result;
        GridViewRow currentrow = (GridViewRow)GridView2.Rows[i];

        Label lblMM1 = (Label)GridView2.HeaderRow.FindControl("PPTMM1");
        Label lblMM2 = (Label)GridView2.HeaderRow.FindControl("PPTMM2");
        Label lblMM3 = (Label)GridView2.HeaderRow.FindControl("PPTMM3");
        Label lblMM4 = (Label)GridView2.HeaderRow.FindControl("PPTMM4");

        Label Label2 = (Label)currentrow.FindControl(lbltotal);
        Label Label3 = (Label)currentrow.FindControl(lblper);


        Label Test1 = (Label)currentrow.FindControl(test1);
        Label Test2 = (Label)currentrow.FindControl(test2);
        Label Test3 = (Label)currentrow.FindControl(test3);
        Label Test4 = (Label)currentrow.FindControl(test4);

        double numTest1 = 0, numTest2 = 0, numTest3 = 0, numTest4 = 0;
        int mm = 0;

        if (Test1.Text.ToUpper() == "ML" || Test1.Text.ToUpper() == "NAD" || Test1.Text == string.Empty)
        {

        }
        else if (Test1.Text != "")
        {
            result = double.TryParse(Test1.Text, out numTest1);
            if (result == true)
            {
                numTest1 = Convert.ToDouble(Test1.Text);
            }
            else
            {
                numTest1 = 0;
            }
            mm = mm + (int.TryParse(lblMM1.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest1 = 0;
        }
        if (Test2.Text.ToUpper() == "ML" || Test2.Text.ToUpper() == "NAD" || Test2.Text == string.Empty)
        {

        }
        else if (Test2.Text != "")
        {
            result = double.TryParse(Test2.Text, out numTest2);
            if (result == true)
            {
                numTest2 = Convert.ToDouble(Test2.Text);
            }
            else
            {

                numTest2 = 0;

            }
            mm = mm + (int.TryParse(lblMM2.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest2 = 0;
        }
        if (Test3.Text.ToUpper() == "ML" || Test3.Text.ToUpper() == "NAD" || Test3.Text == string.Empty)
        {

        }
        else if (Test3.Text != "")
        {
            result = double.TryParse(Test3.Text, out numTest3);
            if (result == true)
            {
                numTest3 = Convert.ToDouble(Test3.Text);
            }
            else
            {
                numTest3 = 0;
            }
            mm = mm + (int.TryParse(lblMM3.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest3 = 0;
        }

        if (Test4.Text.ToUpper() == "ML" || Test4.Text.ToUpper() == "NAD" || Test4.Text == string.Empty)
        {

        }
        else if (Test4.Text != "")
        {
            result = double.TryParse(Test4.Text, out numTest4);
            if (result == true)
            {
                numTest4 = Convert.ToDouble(Test4.Text);
            }
            else
            {
                numTest4 = 0;

            }
            mm = mm + (int.TryParse(lblMM4.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest4 = 0;
        }
        if ((Test1.Text.ToUpper() == "NAD" || Test1.Text.ToUpper() == "ML" || Test1.Text == "") && (Test2.Text.ToUpper() == "NAD" || Test2.Text.ToUpper() == "ML" || Test2.Text == "") && (Test3.Text.ToUpper() == "NAD" || Test3.Text.ToUpper() == "ML" || Test3.Text == "") && (Test4.Text.ToUpper() == "NAD" || Test4.Text.ToUpper() == "ML" || Test4.Text == ""))
        {
            if (Test1.Text == "" && Test2.Text == "" && Test3.Text == "" && Test4.Text == "")
            {
                Label2.Text = "";
                Label3.Text = "";
            }
            else
            {
                Label2.Text = "NP";
                Label3.Text = "NP";
            }

        }
        else
        {
            double PPTMarks = 0;
            PPTMarks = numTest1 + numTest2 + numTest3 + numTest4;

            Label2.Text = PPTMarks.ToString(CultureInfo.CurrentCulture);
            Label3.Text = Math.Round(((PPTMarks * 10) / mm), 1).ToString(CultureInfo.CurrentCulture);
        }

 
        Label lblGTotal = (Label)currentrow.FindControl(lblGtotal);
        lblGTotal.Text = mm.ToString();



        calculateFAallMarks(i);


    }

    public void IAMarksEntry(int i, string lbltotal, string lblper, string test1, string test2, string test3, string test4, string test5, string lblGtotal)
    {
        bool result;
        GridViewRow currentrow = (GridViewRow)GridView2.Rows[i];

        Label lblMM1 = (Label)GridView2.HeaderRow.FindControl("CWMM");
        Label lblMM2 = (Label)GridView2.HeaderRow.FindControl("LabMM");
        Label lblMM3 = (Label)GridView2.HeaderRow.FindControl("WBMM");
        Label lblMM4 = (Label)GridView2.HeaderRow.FindControl("MBMM");
        Label lblMM5 = (Label)GridView2.HeaderRow.FindControl("WAMM");

        Label Label2 = (Label)currentrow.FindControl(lbltotal);
        Label Label3 = (Label)currentrow.FindControl(lblper);


        Label Test1 = (Label)currentrow.FindControl(test1);
        Label Test2 = (Label)currentrow.FindControl(test2);
        Label Test3 = (Label)currentrow.FindControl(test3);
        Label Test4 = (Label)currentrow.FindControl(test4);
        Label Test5 = (Label)currentrow.FindControl(test5);

        double numTest1 = 0, numTest2 = 0, numTest3 = 0, numTest4 = 0, numTest5 = 0;
        int mm = 0;
        if (Test1.Text.ToUpper() == "ML".ToUpper() || Test1.Text.ToUpper() == "NAD" || Test1.Text == string.Empty)
        {

        }
        else if (Test1.Text != "")
        {

            result = double.TryParse(Test1.Text, out numTest1);
            if (result == true)
            {
                numTest1 = Convert.ToDouble(Test1.Text);
            }
            else
            {
                numTest1 = 0;
            }
            mm = mm + (int.TryParse(lblMM1.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest1 = 0;
        }
        if (Test2.Text.ToUpper() == "ML".ToUpper() || Test2.Text.ToUpper() == "NAD" || Test2.Text == string.Empty)
        {

        }
        else if (Test2.Text != "")
        {
            result = double.TryParse(Test2.Text, out numTest2);
            if (result == true)
            {
                numTest2 = Convert.ToDouble(Test2.Text);
            }
            else
            {

                numTest2 = 0;

            }
            mm = mm + (int.TryParse(lblMM2.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest2 = 0;
        }
        if (Test3.Text.ToUpper() == "ML".ToUpper() || Test3.Text.ToUpper() == "NAD" || Test3.Text == string.Empty)
        {

        }
        else if (Test3.Text != "")
        {
            result = double.TryParse(Test3.Text, out numTest3);
            if (result == true)
            {
                numTest3 = Convert.ToDouble(Test3.Text);
            }
            else
            {
                numTest3 = 0;
            }
            mm = mm + (int.TryParse(lblMM3.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest3 = 0;
        }

        if (Test4.Text.ToUpper() == "ML".ToUpper() || Test4.Text.ToUpper() == "NAD" || Test4.Text == string.Empty)
        {

        }
        else if (Test4.Text != "")
        {
            result = double.TryParse(Test4.Text, out numTest4);
            if (result == true)
            {
                numTest4 = Convert.ToDouble(Test4.Text);
            }
            else
            {
                numTest4 = 0;

            } 
            mm = mm + (int.TryParse(lblMM4.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest4 = 0;
        }

        if (Test5.Text.ToUpper() == "ML".ToUpper() || Test5.Text.ToUpper() == "NAD" || Test5.Text == string.Empty)
        {

        }
        else if (Test5.Text != "")
        {
            result = double.TryParse(Test5.Text, out numTest5);
            if (result == true)
            {
                numTest5 = Convert.ToDouble(Test5.Text);
            }
            else
            {
                numTest5 = 0;

            }
            mm = mm + (int.TryParse(lblMM5.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest5 = 0;
        }

        if ((Test1.Text.ToUpper() == "NAD" || Test1.Text.ToUpper() == "ML" || Test1.Text == "") && (Test2.Text.ToUpper() == "NAD" || Test2.Text.ToUpper() == "ML" || Test2.Text == "") && (Test3.Text.ToUpper() == "NAD" || Test3.Text.ToUpper() == "ML" || Test3.Text == "") && (Test4.Text.ToUpper() == "NAD" || Test4.Text.ToUpper() == "ML" || Test4.Text == "") && (Test5.Text.ToUpper() == "NAD" || Test5.Text.ToUpper() == "ML" || Test5.Text == ""))
        {
            if (Test1.Text == "" && Test2.Text == "" && Test3.Text == "" && Test4.Text == "" && Test5.Text == "")
            {
                Label2.Text = "";
                Label3.Text = "";
            }
            else
            {
                Label2.Text = "NP";
                Label3.Text = "NP";
            }
        }
        else
        {
            double PPTMarks = 0;
            PPTMarks = numTest1 + numTest2 + numTest3 + numTest4 + numTest5;

            Label2.Text = PPTMarks.ToString(CultureInfo.CurrentCulture);
            Label3.Text = Math.Round(((PPTMarks * 10) / mm), 1).ToString(CultureInfo.CurrentCulture);
        }

        Label lblGTotal = (Label)currentrow.FindControl(lblGtotal);
        lblGTotal.Text = mm.ToString();



        calculateFAallMarks(i);
    }

    public void GAMarksEntry(int i, string lbltotal, string lblper, string test1, string test2, string test3, string test4, string test5, string test6, string lblGtotal)
    {
        bool result;
        GridViewRow currentrow = (GridViewRow)GridView2.Rows[i];


        Label lblMM1 = (Label)GridView2.HeaderRow.FindControl("MMMM");
        Label lblMM2 = (Label)GridView2.HeaderRow.FindControl("DebMM");
        Label lblMM3 = (Label)GridView2.HeaderRow.FindControl("ResMM");
        Label lblMM4 = (Label)GridView2.HeaderRow.FindControl("GroupMM");
        Label lblMM5 = (Label)GridView2.HeaderRow.FindControl("HouseMM");
        Label lblMM6 = (Label)GridView2.HeaderRow.FindControl("WrittenMM");

        Label Label2 = (Label)currentrow.FindControl(lbltotal);
        Label Label3 = (Label)currentrow.FindControl(lblper);


        Label Test1 = (Label)currentrow.FindControl(test1);
        Label Test2 = (Label)currentrow.FindControl(test2);
        Label Test3 = (Label)currentrow.FindControl(test3);
        Label Test4 = (Label)currentrow.FindControl(test4);
        Label Test5 = (Label)currentrow.FindControl(test5);
        Label Test6 = (Label)currentrow.FindControl(test6);

        double numTest1 = 0, numTest2 = 0, numTest3 = 0, numTest4 = 0, numTest5 = 0, numTest6 = 0;
        int mm = 0;
        if (Test1.Text.ToUpper() == "ML".ToUpper() || Test1.Text.ToUpper() == "NAD" || Test1.Text == string.Empty)
        {

        }
        else if (Test1.Text != "")
        {

            result = double.TryParse(Test1.Text, out numTest1);
            if (result == true)
            {
                numTest1 = Convert.ToDouble(Test1.Text);
            }
            else
            {
                numTest1 = 0;
            }
            mm = mm + (int.TryParse(lblMM1.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest1 = 0;
        }
        if (Test2.Text.ToUpper() == "ML".ToUpper() || Test2.Text.ToUpper() == "NAD" || Test2.Text == string.Empty)
        {

        }
        else if (Test2.Text != "")
        {
            result = double.TryParse(Test2.Text, out numTest2);
            if (result == true)
            {
                numTest2 = Convert.ToDouble(Test2.Text);
            }
            else
            {

                numTest2 = 0;

            }
            mm = mm + (int.TryParse(lblMM2.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest2 = 0;
            //mm = mm + 5;
        }
        if (Test3.Text.ToUpper() == "ML".ToUpper() || Test3.Text.ToUpper() == "NAD" || Test3.Text == string.Empty)
        {

        }
        else if (Test3.Text != "")
        {
            result = double.TryParse(Test3.Text, out numTest3);
            if (result == true)
            {
                numTest3 = Convert.ToDouble(Test3.Text);
            }
            else
            {
                numTest3 = 0;
            }
            mm = mm + (int.TryParse(lblMM3.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest3 = 0;
            //mm = mm + 5;
        }

        if (Test4.Text.ToUpper() == "ML".ToUpper() || Test4.Text.ToUpper() == "NAD" || Test4.Text == string.Empty)
        {

        }
        else if (Test4.Text != "")
        {
            result = double.TryParse(Test4.Text, out numTest4);
            if (result == true)
            {
                numTest4 = Convert.ToDouble(Test4.Text);
            }
            else
            {
                numTest4 = 0;

            }
            mm = mm + (int.TryParse(lblMM4.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest4 = 0;
            //mm = mm + 5;
        }

        if (Test5.Text.ToUpper() == "ML".ToUpper() || Test5.Text.ToUpper() == "NAD" || Test5.Text == string.Empty)
        {

        }
        else if (Test5.Text != "")
        {
            result = double.TryParse(Test5.Text, out numTest5);
            if (result == true)
            {
                numTest5 = Convert.ToDouble(Test5.Text);
            }
            else
            {
                numTest5 = 0;

            }
            mm = mm + (int.TryParse(lblMM5.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest5 = 0;
            //mm = mm + 5;
        }

        if (Test6.Text.ToUpper() == "ML".ToUpper() || Test6.Text.ToUpper() == "NAD" || Test6.Text == string.Empty)
        {

        }
        else if (Test6.Text != "")
        {
            result = double.TryParse(Test6.Text, out numTest6);
            if (result == true)
            {
                numTest6 = Convert.ToDouble(Test6.Text);
            }
            else
            {
                numTest6 = 0;

            }
            mm = mm + (int.TryParse(lblMM6.Text, out mm) ? mm : 0);
        }
        else
        {
            numTest6 = 0;
            //mm = mm + 10;
        }

        //string mmmarks;
        if ((Test1.Text.ToUpper() == "NAD" || Test1.Text.ToUpper() == "ML" || Test1.Text == "") && (Test2.Text.ToUpper() == "NAD" || Test2.Text.ToUpper() == "ML" || Test2.Text == "") && (Test3.Text.ToUpper() == "NAD" || Test3.Text.ToUpper() == "ML" || Test3.Text == "") && (Test4.Text.ToUpper() == "NAD" || Test4.Text.ToUpper() == "ML" || Test4.Text == "") && (Test5.Text.ToUpper() == "NAD" || Test5.Text.ToUpper() == "ML" || Test5.Text == "") && (Test6.Text.ToUpper() == "NAD" || Test6.Text.ToUpper() == "ML" || Test6.Text == ""))
        {
            if (Test1.Text == "" && Test2.Text == "" && Test3.Text == "" && Test4.Text == "" && Test5.Text == "" && Test6.Text == "")
            {
                Label2.Text = "";
                Label3.Text = "";
               
            }
            else
            {
                Label2.Text = "NP";
                Label3.Text = "NP";
         
            }
        }
        else
        {
            double PPTMarks = 0;
            PPTMarks = numTest1 + numTest2 + numTest3 + numTest4 + numTest5 + numTest6;

            Label2.Text = PPTMarks.ToString(CultureInfo.CurrentCulture);
            Label3.Text = Math.Round(((PPTMarks * 10) / mm), 1).ToString(CultureInfo.CurrentCulture);
        }

        Label lblGTotal = (Label)currentrow.FindControl(lblGtotal);
        lblGTotal.Text = mm.ToString();

        calculateFAallMarks(i);

    }

    private void calculateFAallMarks(int i)
    {
        double flage = 0;
        double GetTtenPer = 0;
        GridViewRow currentrow = (GridViewRow)GridView2.Rows[i];
        Label lblMM1 = (Label)currentrow.FindControl("lblMM1");
        Label lblMM2 = (Label)currentrow.FindControl("lblMM2");
        Label lblMM3 = (Label)currentrow.FindControl("lblMM3");

        Label Label27 = (Label)currentrow.FindControl("Label27");
        Label Label19 = (Label)currentrow.FindControl("Label19");
        Label Label23 = (Label)currentrow.FindControl("Label23");

        Label Label29 = (Label)currentrow.FindControl("Label29");
        Label Label21 = (Label)currentrow.FindControl("Label21");
        Label Label25 = (Label)currentrow.FindControl("Label25");

        Label txtper = (Label)currentrow.FindControl("Label44");

        Label lblgrade = (Label)currentrow.FindControl("Label4");

        Label lblGTotal = (Label)currentrow.FindControl("Label2");
        if (Label27.Text == string.Empty && Label19.Text == string.Empty && Label23.Text == string.Empty)
        {

            txtper.Text = "";
            lblgrade.Text = "";
            lblGTotal.Text = "";

        }
        else if (Label27.Text == "NP" && Label19.Text == "NP" && Label23.Text == "NP")
        {

            txtper.Text = "NP";
            lblgrade.Text = "NP";
            lblGTotal.Text = "NP";

        }
        else if (Label27.Text == "NP" && Label19.Text == "NP" && Label23.Text == string.Empty)
        {

            txtper.Text = "NP";
            lblgrade.Text = "NP";
            lblGTotal.Text = "NP";

        }
        else if (Label27.Text == string.Empty && Label19.Text == "NP" && Label23.Text == "NP")
        {

            txtper.Text = "NP";
            lblgrade.Text = "NP";
            lblGTotal.Text = "NP";

        }
        else if (Label27.Text == "NP" && Label19.Text == string.Empty && Label23.Text == "NP")
        {

            txtper.Text = "NP";
            lblgrade.Text = "NP";
            lblGTotal.Text = "NP";

        }

        else
        {
            int devideby = 3;

            GetTtenPer = (double.TryParse(Label29.Text, out flage) ? flage : 0) + (double.TryParse(Label21.Text, out flage) ? flage : 0) + (double.TryParse(Label25.Text, out flage) ? flage : 0);
            lblGTotal.Text = GetTtenPer.ToString(CultureInfo.CurrentCulture);

            if (Label29.Text != string.Empty && Label21.Text != string.Empty && Label25.Text != string.Empty)
            {
                if (Label29.Text != "NP" && Label21.Text != "NP" && Label25.Text != "NP")
                {
                    devideby = 3;
                }
                else if ((Label29.Text != "NP") && (Label21.Text == "NP") && (Label25.Text == "NP"))
                {
                    devideby = 1;
                }
                else if ((Label29.Text != "NP") && (Label21.Text == "NP") && (Label25.Text != "NP"))
                {
                    devideby = 2;
                }
                else if ((Label29.Text != "NP") && (Label21.Text != "NP") && (Label25.Text == "NP"))
                {
                    devideby = 2;
                }
                else if ((Label29.Text == "NP") && (Label21.Text != "NP") && (Label25.Text != "NP"))
                {
                    devideby = 2;
                }
                else if ((Label29.Text == "NP") && (Label21.Text != "NP") && (Label25.Text == "NP"))
                {
                    devideby = 1;
                }
                else if ((Label29.Text == "NP") && (Label21.Text == "NP") && (Label25.Text != "NP"))
                {
                    devideby = 1;
                }
                else
                {
                    devideby = 3;
                }
            }
            else if (Label29.Text != string.Empty && Label21.Text == string.Empty && Label25.Text == string.Empty)
            {
                devideby = 1;
            }
            else if (Label29.Text == string.Empty && Label21.Text != string.Empty && Label25.Text == string.Empty)
            {
                devideby = 1;
            }
            else if (Label29.Text == string.Empty && Label21.Text == string.Empty && Label25.Text != string.Empty)
            {
                devideby = 1;
            }

            else if (Label29.Text != string.Empty && Label21.Text != string.Empty && Label25.Text == string.Empty)
            {
                devideby = 2;
                if ((Label29.Text == "NP") && (Label21.Text != "NP"))
                {
                    devideby = 1;
                }
                else if ((Label29.Text != "NP") && (Label21.Text == "NP"))
                {
                    devideby = 1;
                }
            }

            else if (Label29.Text == string.Empty && Label21.Text != string.Empty && Label25.Text != string.Empty)
            {
                devideby = 2;
                if ((Label21.Text == "NP") && (Label25.Text != "NP"))
                {
                    devideby = 1;
                }
                else if ((Label21.Text != "NP") && (Label25.Text == "NP"))
                {
                    devideby = 1;
                }
            }

            else if (Label29.Text != string.Empty && Label21.Text == string.Empty && Label25.Text != string.Empty)
            {
                devideby = 2;
                if ((Label29.Text == "NP") && (Label25.Text != "NP"))
                {
                    devideby = 1;
                }
                else if ((Label29.Text != "NP") && (Label25.Text == "NP"))
                {
                    devideby = 1;
                }
            }


            double per = Math.Round(GetTtenPer / devideby, 1);
            txtper.Text = per.ToString(CultureInfo.CurrentCulture);
            lblgrade.Text = grade(per);
        }
    }

    protected void drpSubjectGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgrid();
        loadSubject();
    }

    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpEval.SelectedValue == "FA1" || drpEval.SelectedValue == "FA2" || drpEval.SelectedValue == "FA3" || drpEval.SelectedValue == "FA4")
        {
            loaddataforgridview2();
        }
        else
        {
            loaddataforgridview3();
        }
        setLableText();
    }  



}