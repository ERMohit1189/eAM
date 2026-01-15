using System;

public partial class ap_main_apBlank : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            Head1.DataBind();
            string _sql = "select BrandName from BrandTab";
            lblBranding.Text = BAL.objBal.ReturnTag(_sql, "BrandName");
        }
    }

    protected void lnkAddNewForm_OnClick(object sender, EventArgs e)
    {
        Session["mobilenochk"] = Request.QueryString["txtno"];
        Response.Redirect("../2/paf.aspx?check=admintion&id=" + Request.QueryString["txtno"] + "",
            false);
    }

    protected void lnkLogout1_OnClick(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/ap/default.aspx");
    }

    protected void lnkRegistration_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ap/Admission_Details.aspx?txtno="+ Request.QueryString["id"] + "", false);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/ap/default.aspx");
    }
}
