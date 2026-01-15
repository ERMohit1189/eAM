using System;
using System.Web.UI;

public partial class PrintFeeCard : Page
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
            string ss = "select id from FeeCardTemplate where BranchCode=" + Session["BranchCode"].ToString() + " and Template='Template 2'";
            if (oo.Duplicate(ss))
            {
                Response.Redirect("PrintFeeCardNew.aspx");
            }
            else
            {
                ss = "select id from FeeCardTemplate where BranchCode=" + Session["BranchCode"].ToString() + " and Template='Template 3'";
                if (oo.Duplicate(ss))
                {
                    Response.Redirect("PrintFeeCardWithDueMonth.aspx");
                }
            }
        }
        //var headerDiv = FindHtmlControlByIdInControl(this, "headerDiv");
        //BLL.BLLInstance.LoadReportCardHeader("Result", headerDiv);

    }
    //private HtmlControl FindHtmlControlByIdInControl(Control control, string id)
    //{
    //    foreach (Control childControl in control.Controls)
    //    {
    //        if (childControl.ID != null && childControl.ID.Equals(id, StringComparison.OrdinalIgnoreCase) && childControl is HtmlControl)
    //        {
    //            return (HtmlControl)childControl;
    //        }

    //        if (childControl.HasControls())
    //        {
    //            HtmlControl result = FindHtmlControlByIdInControl(childControl, id);
    //            if (result != null) return result;
    //        }
    //    }

    //    return null;
    //}
   
}