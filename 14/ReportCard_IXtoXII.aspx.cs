using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ReportCard_IXtoXII : Page
{
    string sql = "";
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        if (Session["Logintype"].ToString() == "Admin")
        {
            this.MasterPageFile = "~/Master/admin_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Staff")
        {
            this.MasterPageFile = "~/Staff/staff_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/Sp/sp_root-manager.master";
            
        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
        }
        var headerDiv = FindHtmlControlByIdInControl(this, "headerDiv");
        BLL.BLLInstance.LoadReportCardHeader("Result", headerDiv);
        
        if (Session["Logintype"].ToString() == "Guardian")
        {
            if (!IsPostBack)
            {
                string sqls = "select * from tbl_ShowReportCardToGarduin where SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + " and SrNo='" + Session["Srno"].ToString() + "' and status='1'";
                if (!oo.Duplicate(sqls))
                {
                    Response.Redirect("~/sp/sp-dashboard.aspx");
                }
                divEval.Visible = true;
                Div1.Visible = false;
                sql = "select ClassId, ClassName, SectionName, SectionId, BranchId, blocked from AllStudentRecord_UDF('" + Session["SessionName"].ToString() + "'," + Session["BranchCode"].ToString() + ") where SrNo='" + Session["Srno"].ToString() + "'";
                lblClassid.Text = oo.ReturnTag(sql, "ClassId");
                lblSectionName.Text = oo.ReturnTag(sql, "SectionName");
                lblSectionId.Text = oo.ReturnTag(sql, "SectionId");
                lblBranchId.Text = oo.ReturnTag(sql, "BranchId");
                lblClassName.Text = oo.ReturnTag(sql, "ClassName");
                if (oo.ReturnTag(sql, "blocked").ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "LoadReportCardForG();", true);
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Student account is not active";
                }
            }
        }
    }
    private HtmlControl FindHtmlControlByIdInControl(Control control, string id)
    {
        foreach (Control childControl in control.Controls)
        {
            if (childControl.ID != null && childControl.ID.Equals(id, StringComparison.OrdinalIgnoreCase) && childControl is HtmlControl)
            {
                return (HtmlControl)childControl;
            }

            if (childControl.HasControls())
            {
                HtmlControl result = FindHtmlControlByIdInControl(childControl, id);
                if (result != null) return result;
            }
        }

        return null;
    }
}