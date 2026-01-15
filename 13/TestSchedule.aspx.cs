using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class TestSchedule : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string _sql, _sql1 = String.Empty;
    public TestSchedule()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        
        _con = _oo.dbGet_connection();
        _oo.LoadLoader(loader);
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header1);
        if (!IsPostBack)
        {
            loadClass();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void loadClass()
    {
        if (Session["Logintype"].ToString().ToLower() == "staff")
        {
            _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and id in (select distinct classid from OT_AllotTestToStaff where ECode='"+ Session["LoginName"]+"' and SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + ") order by CIDOrder";
            _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        else
        {
            _sql = "select Id, ClassName from ClassMaster where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " order by CIDOrder";
            _oo.FillDropDown_withValue(_sql, ddlClass, "ClassName", "Id");
            ddlClass.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
   
    protected void btnView_Click(object sender, EventArgs e)
    {

        Grd.DataSource = null;
        Grd.DataBind();
        LoadData();
    }
    protected void LoadData()
    {
        _sql = "select tm.TermName, sm.Subject, pm.Paper, em.ExamName, format(em.ExamStart, 'dd-MMM-yyyy hh:mm tt')ExamStart, format(em.ExamEnd, 'dd-MMM-yyyy hh:mm tt') ExamEnd, format(em.ResultShow, 'dd-MMM-yyyy hh:mm tt')ResultShow, format(em.ResultHide, 'dd-MMM-yyyy hh:mm tt') ResultHide from OT_ExamMaster em ";
        _sql = _sql + " inner join OT_SubjectMaster sm on sm.id=em.SubjectId and sm.SessionName=em.SessionName and sm.Branchcode=em.Branchcode ";
        _sql = _sql + " inner join OT_PaperMaster pm on pm.id=em.PaperId and pm.SessionName=em.SessionName and pm.Branchcode=em.Branchcode ";
        _sql = _sql + " inner join OT_TermMaster tm on tm.id=em.TermId and tm.SessionName=em.SessionName and tm.Branchcode=em.Branchcode";
        _sql = _sql + " where em.classId=" + ddlClass.SelectedValue + " and  em.SessionName='" + Session["SessionName"] + "' and em.Branchcode=" + Session["BranchCode"] + " ";
        Grd.DataSource = _oo.GridFill(_sql);
        Grd.DataBind();
        if (Grd.Rows.Count > 0)
        {
            listdisplay.Visible = true;
        }
        else
        {
            listdisplay.Visible = false;
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        _oo.ExporttolandscapePdf(Response, "TestScheduleReport", gdv);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        _oo.ExportTolandscapeWord(Response, "TestScheduleReport", gdv);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        Grd.Style.Add("text-transform", "uppercase");
        _oo.ExportDivToExcelWithFormatting(Response, "TestScheduleReport.xls", gdv, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        //Grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        PrintHelper_New.ctrl = gdv;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
        //LoadData();
    }
    public override void Dispose()
    {
        _con.Dispose();
        _oo.Dispose();
    }

    
}