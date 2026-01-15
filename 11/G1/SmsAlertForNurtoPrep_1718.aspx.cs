using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using c4SmsNew;
using System.Threading;

public partial class common_SmsAlertFroNurtoPrep_1718 : Page
{
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
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = "Select CityName From CollegeMaster cog Inner join CityMaster cm on cm.Id=cog.CityId where cog.BranchCode=" + Session["BranchCode"] + "";
            string cityname = BAL.objBal.ReturnTag(sql, "CityName");
            lblCity.Text = cityname;
            loadclass();
            loadsection();
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
    }

    string sql = "";

    public void loadclass()
    {
        if (Session["Logintype"].ToString() == "Admin")
        {
            sql = "Select Distinct ClassName,cm.Id as Id,CIDOrder from ClassMaster cm ";
            sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=cm.Id";
            sql +=  " where cm.SessionName='" + Session["SessionName"] + "' and t1.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and t1.SessionName='" + Session["SessionName"] + "' and GroupId='G1' Order by CIDOrder";
            BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");

        }
        else
        {
            if (Session["SessionName"].ToString() == "2015-2016")
            {
                sql = "Select Distinct ClassName,sctm.ClassId as Id,CIDOrder from SubjectClassTeacherMaster sctm";
                sql +=  " inner join ClassMaster cm on cm.Id=sctm.ClassId";
                sql +=  " inner join dt_ClassGroupMaster t1 on t1.ClassId=sctm.ClassId";
                sql +=  " where GroupId='G2' and cm.SessionName='" + Session["SessionName"] + "' and cm.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and sctm.BranchCode=" + Session["BranchCode"] + "  and t1.SessionName='" + Session["SessionName"] + "' and sctm.SessionName='" + Session["SessionName"] + "' and Ecode='" + Session["LoginName"].ToString() + "' and ClassTeacher='Yes' Order by CIDOrder";
                BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
            else
            {
                sql = "Select ClassName,cm.Id from ClassTeacherMaster T1";
                sql +=  " inner join ClassMaster cm on cm.Id=T1.ClassId and cm.SessionName=t1.SessionName";
                sql +=  " inner join dt_ClassGroupMaster T2 on T2.ClassId=T1.ClassId and cm.SessionName=T2.SessionName";
                sql +=  " where GroupId='G1' and EmpCode='" + Session["LoginName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and t2.BranchCode=" + Session["BranchCode"] + " and t1.BranchCode=" + Session["BranchCode"] + " and IsClassTeacher=1  and t2.SessionName='" + Session["SessionName"] + "' and T1.SessionName='" + Session["SessionName"] + "' Order by CIDOrder";
                BAL.objBal.FillDropDown_withValue(sql, drpclass, "ClassName", "Id");
            }
        }
    }
    public void loadsection()
    {
        sql = "Select SectionName,Id from SectionMaster where ClassNameId='" + drpclass.SelectedValue.ToString() + "' and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"].ToString() + "'";
        BAL.objBal.FillDropDown_withValue(sql, drpsection, "SectionName", "Id");
        drpsection.Items.Insert(0, "<--Select-->");
    }
    protected void drpclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadsection();
    }

    public void loadSubjects()
    {
        string sql = "Select sm.id SubjectId,SubjectName,'' marks from SubjectMaster  sm";
        sql +=  " inner join S02_SubjectPaperMaster spm on spm.S02ID = sm.PaperID and spm.SessionName=sm.SessionName";
        sql +=  " inner join SubjectGroupMaster sgm on sgm.Id = sm.GroupId and sgm.SessionName=sm.SessionName";
        sql +=  " where sm.ClassId = '" + drpclass.SelectedValue.ToString() + "' and spm.BranchCode=" + Session["BranchCode"] + " and sgm.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and sgm.SessionName='" + Session["SessionName"] + "'  and spm.SessionName='" + Session["SessionName"] + "' and sm.SectionName = '" + drpsection.SelectedItem.Text.ToString() + "' and sm.SessionName = '" + Session["SessionName"].ToString() + "'";
        sql +=  " and sgm.IsForOnlyExam = 1 and spm.IsForExam = 1 order by sgm.DisplayOrder Asc";

        DataTable dt;
        dt = BAL.objBal.Fetchdata(sql);

        rptSubject.DataSource = dt;
        rptSubject.DataBind();
    }

    public void loadStudents()
    {
        sql = "select (SG.FirstName+' '+SG.MiddleName+' '+SG.LastName) as StudentName,sg.srno  as srno,'' marks,SF.FamilyContactNo contactno from StudentGenaralDetail SG ";
        sql +=  "   left join StudentFamilyDetails SF on SG.srno=SF.srno";
        sql +=  "   left join StudentOfficialDetails SO on SG.srno=SO.srno";
        sql +=  "   left join ClassMaster CM on SO.AdmissionForClassId=CM.Id";
        sql +=  "   left join SectionMaster SC on SO.SectionId=SC.Id";
        sql +=  "   where sc.SectionName='" + drpsection.SelectedItem.Text + "' and  sg.SessionName='" + Session["SessionName"].ToString() + "' and ";
        sql +=  "   so.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "' and cm.SessionName='" + Session["SessionName"].ToString() + "'";
        sql +=  "   and SC.SessionName='" + Session["SessionName"].ToString() + "' and cm.BranchCode=" + Session["BranchCode"] + " and so.BranchCode=" + Session["BranchCode"] + " and sc.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + "  and";
        sql +=  "   sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql +=  "   and SO.Withdrwal is null and  isnull(SO.Promotion,'')<>'Cancelled' and SO.AdmissionForClassId='" + drpclass.SelectedValue.ToString() + "' order by sg.FirstName";

        rptStudents.DataSource = BAL.objBal.GridFill(sql);
        rptStudents.DataBind();

        for (int i = 0; i < rptStudents.Items.Count; i++)
        {
            Repeater rptMarks = (Repeater)rptStudents.Items[i].FindControl("rptMarks");

            sql = "Select sm.id SubjectId,SubjectName,'' marks from SubjectMaster  sm inner join S02_SubjectPaperMaster spm on spm.S02ID = sm.PaperID";
            sql +=  " inner join SubjectGroupMaster sgm on sgm.Id = sm.GroupId";
            sql +=  " where sm.ClassId = '" + drpclass.SelectedValue.ToString() + "' and sgm.BranchCode=" + Session["BranchCode"] + " and spm.BranchCode=" + Session["BranchCode"] + " and sm.BranchCode=" + Session["BranchCode"] + " and sgm.SessionName='" + Session["SessionName"] + "' and sm.SectionName = '" + drpsection.SelectedItem.Text.ToString() + "' and sm.SessionName = '" + Session["SessionName"].ToString() + "'";
            sql +=  " and sgm.IsForOnlyExam = 1 and spm.IsForExam = 1 order by sgm.DisplayOrder Asc";

            DataTable dt;
            dt = BAL.objBal.Fetchdata(sql);

            rptMarks.DataSource = dt;
            rptMarks.DataBind();
        }
    }

    protected void drpsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadSubjects();
        loadStudents();
        SetMarks();
    }

    protected void drpEval_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetMarks();
    }

    public void SetMarks()
    {
        if (rptSubject.Items.Count > 0)
        {
            setTitle();
        }
        for (int i = 0; i < rptSubject.Items.Count; i++)
        {
            Label lblSubjectId = (Label)rptSubject.Items[i].FindControl("lblSubjectId");
            string mmmarks;
            mmmarks = GetMMarks(drpEval.SelectedValue.ToString(), lblSubjectId.Text.Trim());

            Label lblMM1 = (Label)rptSubject.Items[i].FindControl("lblMM1");

            lblMM1.Text = mmmarks;

            for (int j = 0; j < rptStudents.Items.Count; j++)
            {
                string testmarks;

                Label lblsrno = (Label)rptStudents.Items[j].FindControl("lblsrno");
                Repeater rptMarks = (Repeater)rptStudents.Items[j].FindControl("rptMarks");

                testmarks = GetTestMarks(drpEval.SelectedValue.ToString(), lblSubjectId.Text.Trim(), lblsrno.Text.Trim());

                Label lblM1 = (Label)rptMarks.Items[i].FindControl("lblM1");
                lblM1.Text = testmarks.ToString();
            }
        }
    }

    public string GetMMarks(string Eval, string subjectid)
    {
        string mmt1 = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Subjectid", subjectid));
        param.Add(new SqlParameter("@Evaluation", Eval));
        param.Add(new SqlParameter("@Test", drptest.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_CCENurtoPrepMMMarksForMessage", param);

        if(ds!=null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if(dt.Rows.Count>0)
            {
                mmt1 = dt.Rows[0][0].ToString();
            }
        }
       
        return mmt1;
    }


    public string GetTestMarks(string Eval, string subjectid,string srno)
    {
        string obtmarks = "";

        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Classid", drpclass.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SectionName", drpsection.SelectedItem.Text));
      
        param.Add(new SqlParameter("@Evaluation", Eval));
        param.Add(new SqlParameter("@Test", drptest.SelectedValue.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));

        param.Add(new SqlParameter("@Srno", srno));
        param.Add(new SqlParameter("@Subjectid", subjectid));

        DataSet ds = new DataSet();

        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_CCENurtoPrepMarksForMessage", param);

        if (ds != null)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                obtmarks = dt.Rows[0][0].ToString();
            }
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
        PrintHelper_New.ctrl = divExport;
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

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for(int i=0;i< rptStudents.Items.Count;i++)
        {
            CheckBox chk = (CheckBox)rptStudents.Items[i].FindControl("chk");
            chk.Checked = chkAll.Checked;
        }
    }



    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(lnkSubmit, GetType(), "", "createHTML();", true);
        string msg = "SMS sent successfully.";
        string symbol = "S";
        try
        {
            sendSms();
        }
        catch(Exception ex)
        {
            msg = ex.Message.Replace(",","").Replace("'","");
            symbol = "W";
        }     
        Campus camp = new Campus(); camp.msgbox(lnkSubmit, divmsg, msg, symbol);
    }

    public void sendSms()
    {
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        for (int i = 0; i < rptStudents.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)rptStudents.Items[i].FindControl("chk");
            if (chk.Checked)
            {
                HiddenField hdMsg = (HiddenField)rptStudents.Items[i].FindControl("hdMsg");
                TextBox txtMulti = (TextBox)rptStudents.Items[i].FindControl("txtMulti");
                Label lblContactNo = (Label)rptStudents.Items[i].FindControl("lblContactNo");
                string textMsg = hdMsg.Value;

                sadpNew.Send(textMsg, lblContactNo.Text.Trim(), "");
            }
        }


    }
}