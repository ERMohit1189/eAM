using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ReriodAllotmentReport : Page
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
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            sql = " select * from classmaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            _oo.FillDropDown_withValue(sql, ddlClass, "ClassName", "id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        sql = " select id, BranchName from BranchMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classid=" + ddlClass.SelectedValue + "";
        _oo.FillDropDown_withValue(sql, ddlBranch, "BranchName", "id");
        ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));

        sql = " select * from sectionmaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and Classnameid="+ddlClass.SelectedValue+"";
        _oo.FillDropDown_withValue(sql, ddlSection, "sectionName", "id");
        ddlSection.Items.Insert(0, new ListItem("<--Select-->", ""));
        LoadData();
    }
    protected void btnViewt_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    public void LoadData()
    {

        sql = "select (ClassName+' '+case when bm.IsDisplay=1 then BranchName else '' end+' ('+SectionName+')') class, ps.Medium, SubjectName+' ('+tsm.SubjectCode+')' SubjectName, ";
        sql = sql + " PaperName+' ('+pm.PaperCode+')' PaperName, Period, Name+' ('+Ecode+')' EmpName, (tpms.StartFrom+'-To-'+tpms.EndTo) duration, ps.LoginName, ";
        sql = sql + " format(ps.RecordedDate, 'dd-MMM-yyyy hh:mm:ss tt')RecordedDate from TTPeriodAllotToStaff ps ";
        sql = sql + " inner join ClassMaster cm on cm.id=ps.ClassId and cm.SessionName=ps.SessionName and cm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join BranchMaster bm on bm.id=ps.BranchId and bm.ClassId=ps.ClassId and bm.SessionName=ps.SessionName and bm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join SectionMaster sm on sm.id=ps.SectionId and sm.ClassNameId=ps.ClassId and sm.SessionName=ps.SessionName and sm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTSubjectMaster tsm on tsm.id=ps.SubjectId and tsm.ClassId=ps.ClassId and tsm.BranchId=ps.BranchId and tsm.Medium=ps.Medium and tsm.SessionName=ps.SessionName ";
        sql = sql + " and tsm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTPaperMaster pm on pm.id=ps.PaperId and pm.ClassId=ps.ClassId and pm.BranchId=ps.BranchId and pm.Medium=ps.Medium and pm.SessionName=ps.SessionName ";
        sql = sql + " and pm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTPeriodMaster tpm on tpm.id=ps.PeriodId and tpm.BranchCode=ps.BranchCode ";
        sql = sql + " inner join TTPeriodMapping tpms on tpms.Periodid=tpm.id and tpms.ClassId=ps.ClassId and tpms.SectionId=ps.SectionId and tpms.SessionName=ps.SessionName ";
        sql = sql + " and tpms.BranchCode=tpm.BranchCode ";
        sql = sql + " inner join GetAllStaffRecords_UDF(1) asr on asr.Ecode=ps.EmpCode and asr.BranchCode=ps.BranchCode ";
        sql = sql + " where ps.SessionName='" + Session["SessionName"] + "' and ps.BranchCode=" + Session["BranchCode"] + " ";
        if (ddlClass.SelectedIndex != 0)
        {
            sql = sql + " and ps.Classid=" + ddlClass.SelectedValue + " ";
        }
        if (ddlBranch.SelectedIndex != 0)
        {
            sql = sql + "  and ps.BranchId=" + ddlBranch.SelectedValue + " ";
        }
        if (ddlSection.SelectedIndex != 0)
        {
            sql = sql + "  and ps.sectionid=" + ddlSection.SelectedValue + " ";
        }
        var dt = _oo.Fetchdata(sql);
        Grid.DataSource = dt;
        Grid.DataBind();
        if (Grid.Rows.Count>0)
        {
            lblRegister.Text = "";
            int sts = 0;
            string ss = "select format(getdate(), 'dd MMM yyyy hh:mm:ss tt') dates";
            heading.Text = "Period Allotment Report";
            if (ddlClass.SelectedIndex!=0)
            {
                sts = sts + 1;
                lblRegister.Text = lblRegister.Text + "Class : " + ddlClass.SelectedItem.Text;
            }
            if (ddlBranch.SelectedIndex != 0)
            {
                sql = "select IsDisplay from BranchMaster where BranchCode=" + Session["BranchCode"]+ " and SessionName='" + Session["SessionName"] + "' and classid="+ddlClass.SelectedValue+"";
                string IsDisplay = _oo.ReturnTag(sql, "IsDisplay").ToString();
                if (IsDisplay.ToLower()=="true")
                {
                    lblRegister.Text = lblRegister.Text + " " + ddlBranch.SelectedItem.Text;
                }
            }
            if (ddlSection.SelectedIndex != 0)
            {
                lblRegister.Text = lblRegister.Text + " (" + ddlSection.SelectedItem.Text+")";
            }
            if (sts >0)
            {
                lblRegister.Text = lblRegister.Text + " | Date : " + _oo.ReturnTag(ss, "dates");
            }
            Panel2.Visible = true;
            abc.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "lineBreak();", true);
        }
        else
        {
            Panel2.Visible = false;
            abc.Visible = false;
        }
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "PeriodAllotmentReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        _oo.ExportDivToExcelWithFormatting(Response, "PeriodAllotmentReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "PeriodAllotmentReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}