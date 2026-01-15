using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;

public partial class SuperAdmin_StaffLoginDetails : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            BLL.BLLInstance.loadCourseWithoutSession(ddlCourse, Session["SessionName"].ToString());
            BLL.BLLInstance.loadClassUseingCourse(ddlClass, ddlCourse.SelectedValue, Session["SessionName"].ToString());
            BLL.BLLInstance.loadSection(ddlSection, Session["SessionName"].ToString(), ddlClass.SelectedValue);
            BLL.BLLInstance.loadBranch(ddlBranch, Session["SessionName"].ToString(), ddlClass.SelectedValue);
            //userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
        }
    }

    public void userPasswordDetails(string classid, string sectionid, string branchid,string courseid)
    {
        List<SqlParameter> param = new List<SqlParameter>();

        param.Add(new SqlParameter("@Id", "-1"));
        param.Add(new SqlParameter("@ClassId", classid));
        param.Add(new SqlParameter("@SectionId", sectionid));
        param.Add(new SqlParameter("@BranchId", branchid));
        param.Add(new SqlParameter("@Courseid", courseid));
        param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
        param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

        rptLoginDetails.DataSource = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("GuardianLoginandPassword_Proc", param);
        rptLoginDetails.DataBind();
        if (rptLoginDetails.Items.Count > 0)
        {
                UpdatePanel1.Visible = true;
            for (int i = 0; i < rptLoginDetails.Items.Count; i++)
            {
                Label lblNote = (Label)rptLoginDetails.Items[i].FindControl("lblNote");
                lblNote.Text = txtNote.Text;
            }
            
        }

    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadSection(ddlSection, Session["SessionName"].ToString(), ddlClass.SelectedValue);
        BLL.BLLInstance.loadBranch(ddlBranch, Session["SessionName"].ToString(), ddlClass.SelectedValue);
        //userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
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

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL.BLLInstance.loadClassUseingCourse(ddlClass, ddlCourse.SelectedValue, Session["SessionName"].ToString());
        //userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        userPasswordDetails(ddlClass.SelectedValue, ddlSection.SelectedValue, ddlBranch.SelectedValue, ddlCourse.SelectedValue);
        
    }
}