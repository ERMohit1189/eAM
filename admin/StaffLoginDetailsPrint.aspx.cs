using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_StaffLoginDetailsPrint : System.Web.UI.Page
{
    private readonly Campus _oo;
    public admin_StaffLoginDetailsPrint()
    {
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            // BLL.BLLInstance.loadCourseWithoutSession(ddlCourse, Session["SessionName"].ToString());
            // BLL.BLLInstance.loadClassUseingCourse(ddlClass, ddlCourse.SelectedValue, Session["SessionName"].ToString());
            // BLL.BLLInstance.loadSection(ddlSection, Session["SessionName"].ToString(), ddlClass.SelectedValue);
            //BLL.BLLInstance.loadBranch(ddlBranch, Session["SessionName"].ToString(), ddlClass.SelectedValue);
            //userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
            BindDepartment();
            BindDesignation();
        }
    }
    public void BindDepartment()
    {
       string sql = "Select  EmpDepId,EmpDepName from EmpDepMaster ";
        sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(sql, ddlClass, "EmpDepName", "EmpDepId");
        ddlClass.Items.Insert(0, new ListItem("<--Select All-->", "0"));
    }
    public void BindDesignation()
    {
        string sql = "Select  DesId, DesName from DesMaster ";
        sql = sql + " where BranchCode=" + Session["BranchCode"].ToString() + "";
        _oo.FillDropDown_withValue(sql, ddlCourse, "DesName", "DesId");
        ddlCourse.Items.Insert(0, new ListItem("<--Select All-->", "0"));
    }
    public void userPasswordDetails(string classid, string courseid)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Designation", courseid.ToString()));
        param.Add(new SqlParameter("@DepartmentName", classid.ToString()));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        rptLoginDetails.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GetStaffLoginDetails_sp", param);
        rptLoginDetails.DataBind();
        

    }
   

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
    }

    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExporttolandscapePdf(Response, "GuardianLoginDetails", divExport);
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportTolandscapeWord(Response, "GuardianLoginDetails", divExport);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        BAL.objBal.ExportDivToExcelWithFormatting(Response, "GuardianLoginDetails.xls", divExport, Server.MapPath("~/Admin/css/style.css"));
    }

    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = divExport;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);

    }

   

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        userPasswordDetails(ddlClass.SelectedItem.Text, ddlCourse.SelectedItem.Text);

    }
}