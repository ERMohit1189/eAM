using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Planner : System.Web.UI.Page
{
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        
        if (Session["Logintype"].ToString() == "Guardian")
        {
            this.MasterPageFile = "~/sp/sp_root-manager.master";
        }
        else if (Session["Logintype"].ToString() == "Student")
        {
            this.MasterPageFile = "~/13/stuRootManager.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void loadReport()
    {
        if (rbList.SelectedIndex == 0)
        {
            Response.Redirect("Planner.aspx");
        }
        else
        {
            Response.Redirect("~/11/PlannerReport.aspx?print=1");
        }
    }
    protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadReport();
    }
}