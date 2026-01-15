using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PeriodAllotToStaff : Page
{
    string sql = "", _sql = "", Sql = "";
    Campus _oo = new Campus();
    private SqlConnection _con;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        _con = new SqlConnection();
        _con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            sql = " select * from classmaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            sql = " select id, Period from TTPeriodMaster where BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(sql, ddlPeriod, "Period", "id");
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlPeriod.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlStaff.Items.Insert(0, new ListItem("<--Select-->", ""));

        }
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {

        sql = " select * from BranchMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid=" + ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select * from sectionmaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classnameid=" + ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(sql, ddlSection, "sectionName", "id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();

    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select * from MediumMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(sql, ddlMedium, "Medium", "Medium");
        ddlMedium.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }

    protected void ddlMedium_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select * from TTSubjectMaster where Classid=" + ddlClass.SelectedValue + " and BranchId=" + ddlBranch.SelectedValue + " and Medium='" + ddlMedium.SelectedValue + "' and ApplicableFor<>'Exam' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(sql, ddlSubject, "SubjectName", "id");
        ddlSubject.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select * from TTPaperMaster where Classid=" + ddlClass.SelectedValue + " and BranchId=" + ddlBranch.SelectedValue + " and SubjectId=" + ddlSubject.SelectedValue + " and Medium='" + ddlMedium.SelectedValue + "' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
        _oo.FillDropDown_withValue(sql, ddlPaper, "PaperName", "id");
        ddlPaper.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }

    protected void ddlPaper_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select pm.Period+' ('+pms.StartFrom+'-'+pms.EndTo+')'Period,pm.id from TTPeriodMaster pm inner join TTPeriodMapping pms on pms.Periodid=pm.id and pm.BranchCode=pms.BranchCode ";
        sql = sql + " where Classid=" + ddlClass.SelectedValue + " and SectionId=" + ddlSection.SelectedValue + " and Pms.SessionName='" + Session["SessionName"] + "' and pms.BranchCode=" + Session["BranchCode"] + " and pm.PeriodType not in ('Assembly', 'Lunch Period')";
        _oo.FillDropDown_withValue(sql, ddlPeriod, "Period", "id");
        ddlPeriod.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = "select DesignationAllowed from TTRulesOfTimeTable where BranchCode=" + Session["BranchCode"] + "";
        string DesignationAllowed=_oo.ReturnTag(sql, "DesignationAllowed");
        sql = "select Name+ ' ('+Ecode+')' Name, Ecode from GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") where Designation in ";
        sql = sql + " (select EmpDesName from A02_EmpShiftMaster sf ";
        sql = sql + " inner join EmpDesMaster ed on ed.EmpDesId=sf.EmpDesId and ed.BranchCode=sf.BranchCode  ";
        sql = sql + " where convert(datetime,FromTime)<= ";
        sql = sql + " (select convert(datetime,StartFrom) from TTPeriodMaster pm ";
        sql = sql + " inner join TTPeriodMapping pmap on pmap.Periodid=pm.id and pmap.BranchCode=pm.BranchCode  ";
        sql = sql + " where pmap.Classid=" + ddlClass.SelectedValue + " and pmap.SectionId=" + ddlSection.SelectedValue + " and pmap.Periodid=" + ddlPeriod.SelectedValue + " and pmap.SessionName='" + Session["SessionName"] + "' and pmap.BranchCode=" + Session["BranchCode"] + ") ";
        sql = sql + " and convert(datetime,ToTime)>= ";
        sql = sql + " (select convert(datetime,EndTo) from TTPeriodMaster pm ";
        sql = sql + " inner join TTPeriodMapping pmap on pmap.Periodid=pm.id and pmap.BranchCode=pm.BranchCode ";
        sql = sql + " where pmap.Classid=" + ddlClass.SelectedValue + " and pmap.SectionId=" + ddlSection.SelectedValue + " and pmap.Periodid=" + ddlPeriod.SelectedValue + " and pmap.SessionName='" + Session["SessionName"] + "' and pmap.BranchCode=" + Session["BranchCode"] + ") and sf.BranchCode=" + Session["BranchCode"] + " and sf.EmpDesId in ("+ DesignationAllowed + "))  and isnull(Withdrwal,'')='' order by Ecode asc ";

        _oo.FillDropDown_withValue(sql, ddlStaff, "Name", "Ecode");
        ddlStaff.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_isClassteacher.InnerHtml = "";
        _sql = " declare @StartFrom nvarchar(50) declare @EndTo nvarchar(50) ";
        _sql = _sql + " select @StartFrom=StartFrom, @EndTo=EndTo from TTPeriodMapping where Classid=" + ddlClass.SelectedValue + " and SectionId=" + ddlSection.SelectedValue + " and Periodid=" + ddlPeriod.SelectedValue + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
        _sql = _sql + " select convert(datetime, StartFrom) from TTPeriodAllotToStaff ps ";
        _sql = _sql + " inner join TTPeriodMapping prms on prms.Periodid=prms.Periodid and prms.Classid=ps.ClassId and  ";
        _sql = _sql + " prms.SectionId=ps.SectionId and prms.SessionName=ps.SessionName and prms.BranchCode=ps.BranchCode ";
        _sql = _sql + " where EmpCode='" + ddlStaff.SelectedValue + "' and ps.SessionName='" + Session["SessionName"] + "' and ps.BranchCode=" + Session["BranchCode"] + " and (convert(datetime, StartFrom) between convert(datetime, @StartFrom) and convert(datetime, @EndTo) ";
        _sql = _sql + " or convert(datetime, EndTo) between convert(datetime, @StartFrom) and convert(datetime, @EndTo)) ";
        if (_oo.Duplicate(_sql))
        {
            btnInsert.Visible = false;
            ddlStaff.SelectedIndex = 0;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Already period alloted for this time to selected staff!!", "A");
            return;
        }
        else
        {
            btnInsert.Visible = true;
            sql = "select 1 from ClassTeacherMaster where EmpCode='" + ddlStaff.SelectedValue + "' and ClassId=" + ddlClass.SelectedValue + " and BranchId=" + ddlBranch.SelectedValue + " and SectionId=" + ddlSection.SelectedValue + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'  and IsClassTeacher=1 ";
            if (_oo.Duplicate(sql))
            {
                chkIsClassTeacher.SelectedIndex = 0;
                div_isClassteacher.InnerHtml = "Class Teacher";
            }
            else
            {
                chkIsClassTeacher.SelectedIndex = 1;
                sql = "select 1 from ICSESubjectTeacherAllotment where EmpCode='" + ddlStaff.SelectedValue + "' and ClassId=" + ddlClass.SelectedValue + " and BranchId=" + ddlBranch.SelectedValue + " and SectionId=" + ddlSection.SelectedValue + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "' ";
                if (_oo.Duplicate(sql))
                {
                    chkIsClassTeacher.SelectedIndex = 1;
                    div_isClassteacher.InnerHtml = "Subject Teacher";
                }
                else
                {
                    chkIsClassTeacher.SelectedIndex = 1;
                }
            }
        }
    }
    public void LoadData()
    {
        sql = " select ps.id, ps.classid, ps.BranchId, ps.SectionId, ps.SubjectId, ps.PaperId, ps.PeriodId, prm.Period, ClassName+' '+case when bm.IsDisplay=1 then BranchName else '' end+' ('+SectionName+')' ClassName, ";
        sql = sql + " ps.Medium, SubjectName+' ('+ps.SubjectCode+')' SubjectName, PaperName+' ('+ps.PaperCode+')' PaperName, ps.EmpCode, asr.Name,  ";
        sql = sql + " case when IsClassTeacher=1 then 'Yes' else 'No' end IsClassTeacher, (StartFrom+' -To- '+EndTo) Deuration, ps.LoginName,  ";
        sql = sql + " format(ps.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt') RecordedDate from TTPeriodAllotToStaff ps ";
        sql = sql + " inner join ClassMaster cm on cm.Id=ps.ClassId and cm.SessionName=ps.SessionName and cm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join BranchMaster bm on bm.Id=ps.BranchId and bm.Classid=ps.ClassId and bm.SessionName=ps.SessionName and bm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join SectionMaster sm on sm.Id=ps.SectionId and sm.ClassNameId=ps.ClassId and sm.SessionName=ps.SessionName and sm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTSubjectMaster tsm on tsm.Id=ps.SubjectId and tsm.Classid=ps.ClassId and tsm.BranchId=ps.BranchId and tsm.SessionName=ps.SessionName and tsm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTPaperMaster pm on pm.Id=ps.PaperId and pm.Classid=ps.ClassId and pm.BranchId=ps.BranchId and pm.SessionName=ps.SessionName and pm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTPeriodMaster prm on prm.Id=ps.PeriodId and prm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTPeriodMapping prms on prms.Periodid=prm.id and prms.Classid=ps.ClassId and prms.SectionId=ps.SectionId and prms.SessionName=ps.SessionName and prms.BranchCode=ps.BranchCode ";
        sql = sql + " inner join GetAllStaffRecords_UDF(1) asr on asr.Ecode=ps.EmpCode and asr.BranchCode=ps.BranchCode ";
        sql = sql + " where ps.SessionName='" + Session["SessionName"] + "' and ps.BranchCode=" + Session["BranchCode"] + "";
        sql = sql + " and ps.Classid=" + ddlClass.SelectedValue + " ";
        if (ddlBranch.SelectedIndex != 0)
        {
            sql = sql + "  and ps.branchid=" + ddlBranch.SelectedValue + " ";
        }
        if (ddlSection.SelectedIndex != 0)
        {
            sql = sql + "  and ps.sectionid=" + ddlSection.SelectedValue + " ";
        }
        if (ddlMedium.SelectedIndex != 0)
        {
            sql = sql + "  and ps.Medium='" + ddlMedium.SelectedValue + "' ";
        }
        if (ddlSubject.SelectedIndex != 0)
        {
            sql = sql + "  and ps.subjectid=" + ddlSubject.SelectedValue + " ";
        }
        if (ddlPaper.SelectedIndex != 0)
        {
            sql = sql + "  and ps.paperid=" + ddlPaper.SelectedValue + " ";
        }
        if (ddlPeriod.SelectedIndex != 0)
        {
            sql = sql + "  and ps.PeriodId=" + ddlPeriod.SelectedValue + " ";
        }

        sql = sql + " order by ps.classid, ps.PeriodId asc";
        var dt = _oo.Fetchdata(sql);
        gvTimeTableRule.DataSource = dt;
        gvTimeTableRule.DataBind();

    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        _sql = "SELECT classid FROM TTPeriodAllotToStaff WHERE classid=" + ddlClass.SelectedValue.Trim() + " and BranchId=" + ddlBranch.SelectedValue.Trim() + " and SectionId=" + ddlSection.SelectedValue.Trim() + " and SubjectId=" + ddlSubject.SelectedValue.Trim() + " ";
        _sql = _sql + " and PaperId=" + ddlPaper.SelectedValue.Trim() + " and Periodid=" + ddlPeriod.SelectedValue.Trim() + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        if (_oo.Duplicate(_sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Duplicate allotment!", "A");
            return;
        }

        else
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "TTPeriodAllotToStaffProc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = _con;
                cmd.Parameters.AddWithValue("@Classid", ddlClass.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SectionId", ddlSection.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Medium", ddlMedium.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@SubjectId", ddlSubject.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@PaperId", ddlPaper.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Periodid", ddlPeriod.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@EmpCode", ddlStaff.SelectedValue);
                cmd.Parameters.AddWithValue("@IsClassTeacher", chkIsClassTeacher.SelectedIndex == 0 ? "1" : "0");
                cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
                cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
                cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
                cmd.Parameters.AddWithValue("@Action", "insert");
                try
                {
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    _con.Close();
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                    LoadData();
                    Reset();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
    public void Reset()
    {
        ddlPeriod.SelectedIndex = 0;
        ddlStaff.Items.Clear();
        ddlStaff.Items.Insert(0, new ListItem("<--Select-->", ""));
        chkIsClassTeacher.SelectedIndex = 1;
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label37");
        Label classid = (Label)chk.NamingContainer.FindControl("classid");
        Label PeriodId = (Label)chk.NamingContainer.FindControl("PeriodId");
        Label EmpCode = (Label)chk.NamingContainer.FindControl("EmpCode");
        classidDelete.Text = classid.Text;
        PeriodIdDelete.Text = PeriodId.Text;
        EmpCodeDelete.Text = EmpCode.Text;
        lblValue.Text = lblId.Text;
        mpeDelete.Show();
        btnNo.Focus();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        DeleteRecord();
    }


    public void DeleteRecord()
    {
        _sql = "Delete from TTPeriodAllotToStaff where Id=" + lblValue.Text + " and BranchCode=" + Session["BranchCode"] + " and SessionName='" + Session["SessionName"] + "'";
        _sql = _sql + " and classid=" + classidDelete.Text + " and PeriodId=" + PeriodIdDelete.Text + " and EmpCode='" + EmpCodeDelete.Text + "' ";
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = _sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _con;
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Deleted successfully.", "S");
                LoadData();
                Reset();
            }
            catch (Exception)
            {
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        Label lblId = (Label)chk.NamingContainer.FindControl("Label36");
        Label classid = (Label)chk.NamingContainer.FindControl("classid");
        Label BranchId = (Label)chk.NamingContainer.FindControl("BranchId");
        Label SectionId = (Label)chk.NamingContainer.FindControl("SectionId");
        Label SubjectId = (Label)chk.NamingContainer.FindControl("SubjectId");
        Label PaperId = (Label)chk.NamingContainer.FindControl("PaperId");
        Label PeriodId = (Label)chk.NamingContainer.FindControl("PeriodId");
        Label IsClassTeacher = (Label)chk.NamingContainer.FindControl("IsClassTeacher");
        Label EmpCode = (Label)chk.NamingContainer.FindControl("EmpCode");
        string ss = lblId.Text;
        lblID.Text = ss;
        classidEdit.Text = classid.Text;
        BranchIdEdit.Text = BranchId.Text;
        SectionIdEdit.Text = SectionId.Text;
        PeriodIdEdit.Text = PeriodId.Text;
        SubjectIdEdit.Text = SubjectId.Text;
        PaperIdEdit.Text = PaperId.Text;
        EmpCodeOldEdit.Text = EmpCode.Text;
        chkIsClassTeacherEdit.SelectedIndex = (IsClassTeacher.Text.ToLower() == "yes" ? 0 : 1);
        sql = "select DesignationAllowed from TTRulesOfTimeTable where BranchCode=" + Session["BranchCode"] + "";
        string DesignationAllowed = _oo.ReturnTag(sql, "DesignationAllowed");
        sql = "select Name+ ' ('+Ecode+')' Name, Ecode from GetAllStaffRecords_UDF(" + Session["BranchCode"] + ") where Designation in ";
        sql = sql + " (select EmpDesName from A02_EmpShiftMaster sf ";
        sql = sql + " inner join EmpDesMaster ed on ed.EmpDesId=sf.EmpDesId and ed.BranchCode=sf.BranchCode  ";
        sql = sql + " where convert(datetime,FromTime)<= ";
        sql = sql + " (select convert(datetime,StartFrom) from TTPeriodMaster pm ";
        sql = sql + " inner join TTPeriodMapping pmap on pmap.Periodid=pm.id and pmap.BranchCode=pm.BranchCode  ";
        sql = sql + " where pmap.Classid=" + classidEdit.Text + " and pmap.SectionId=" + SectionIdEdit.Text + " and pmap.Periodid=" + PeriodIdEdit.Text + " and pmap.SessionName='" + Session["SessionName"] + "' and pmap.BranchCode=" + Session["BranchCode"] + ") ";
        sql = sql + " and convert(datetime,ToTime)>= ";
        sql = sql + " (select convert(datetime,EndTo) from TTPeriodMaster pm ";
        sql = sql + " inner join TTPeriodMapping pmap on pmap.Periodid=pm.id and pmap.BranchCode=pm.BranchCode ";
        sql = sql + " where pmap.Classid=" + classidEdit.Text + " and pmap.SectionId=" + SectionIdEdit.Text + " and pmap.Periodid=" + PeriodIdEdit.Text + " and pmap.SessionName='" + Session["SessionName"] + "' and pmap.BranchCode=" + Session["BranchCode"] + ") and sf.BranchCode=" + Session["BranchCode"] + " and sf.EmpDesId in (" + DesignationAllowed + "))  and isnull(Withdrwal,'')='' order by Ecode asc ";

        _oo.FillDropDown_withValue(sql, ddlStaffEdit, "Name", "Ecode");
        ddlStaffEdit.Items.Insert(0, new ListItem("<--Select-->", ""));
        ddlStaffEdit.SelectedValue = EmpCode.Text;
        Panel1_ModalPopupExtender.Show();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }


    protected void ddlStaffEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sql = " declare @StartFrom nvarchar(50) declare @EndTo nvarchar(50) ";
        _sql = _sql + " select @StartFrom=StartFrom, @EndTo=EndTo from TTPeriodMapping where Classid=" + classidEdit.Text + " and SectionId=" + SectionIdEdit.Text + " and Periodid=" + PeriodIdEdit.Text + " and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " ";
        _sql = _sql + " select convert(datetime, StartFrom) from TTPeriodAllotToStaff ps ";
        _sql = _sql + " inner join TTPeriodMapping prms on prms.Periodid=prms.Periodid and prms.Classid=ps.ClassId and  ";
        _sql = _sql + " prms.SectionId=ps.SectionId and prms.SessionName=ps.SessionName and prms.BranchCode=ps.BranchCode ";
        _sql = _sql + " where EmpCode='" + ddlStaffEdit.SelectedValue + "' and ps.SessionName='" + Session["SessionName"] + "' and ps.BranchCode=" + Session["BranchCode"] + " and (convert(datetime, StartFrom) between convert(datetime, @StartFrom) and convert(datetime, @EndTo) ";
        _sql = _sql + " or convert(datetime, EndTo) between convert(datetime, @StartFrom) and convert(datetime, @EndTo)) ";
        if (_oo.Duplicate(_sql))
        {
            btnInsert.Visible = false;
            ddlStaffEdit.SelectedIndex = 0;
            Campus camp = new Campus(); camp.msgbox(Page, msgbox2, "Already period alloted for this time to selected staff!!", "A");

            return;
        }
        else
        {
            btnInsert.Visible = true;
        }
        Panel1_ModalPopupExtender.Show();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "TTPeriodAllotToStaffProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Parameters.AddWithValue("@Classid", classidEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchId", BranchIdEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@SectionId", SectionIdEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@SubjectId", SubjectIdEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@PaperId", PaperIdEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@Periodid", PeriodIdEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@EmpCodeOld", EmpCodeOldEdit.Text.Trim());
            cmd.Parameters.AddWithValue("@EmpCode", ddlStaffEdit.SelectedValue);
            cmd.Parameters.AddWithValue("@IsClassTeacher", chkIsClassTeacherEdit.SelectedIndex == 0 ? "1" : "0");
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@SessionName", Session["SessionName"].ToString());
            cmd.Parameters.AddWithValue("@BranchCode", Session["BranchCode"].ToString());
            cmd.Parameters.AddWithValue("@Action", "update");
            try
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Updated successfully.", "S");
                LoadData();
                Reset();
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
}