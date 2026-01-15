using System;
using System.Data.SqlClient;

public partial class mainblank : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            Head1.DataBind();
            sql = "select BrandName from BrandTab";
            lblBranding.Text = oo.ReturnTag(sql, "BrandName");
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
        Response.Redirect("~/default.aspx");
    }

    protected void lnkRegistration_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ap/Admission_Details.aspx?txtno="+ Request.QueryString["id"] + "", false);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/default.aspx");
    }
}
