using System;
using System.Threading;

public partial class API_Response : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["SessionName"] = Request.QueryString["SessionName"].ToString();
        Session["BranchCode"] = Request.QueryString["BranchCode"].ToString();
        Session["Srno"] = Request.QueryString["SrNo"].ToString();
        Session["Logintype"] = Request.QueryString["Logintype"].ToString();
        Session["LoginName"] = Request.QueryString["LoginName"].ToString();
        Session["ImageUrl"] = Request.QueryString["ImageUrl"].ToString();
        Response.Redirect("../2/CompositFeeDeposit_g.aspx");
    }
}
    