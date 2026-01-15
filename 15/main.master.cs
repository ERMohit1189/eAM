using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;

public partial class main : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        con = oo.dbGet_connection();
        try
        {
            if (string.IsNullOrEmpty(Session["LoginName"].ToString()))
            {
                Response.Redirect("~/Alumni/default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Alumni/default.aspx");
        }
        Session["logout"] = null;
        if (!IsPostBack)
        {
            Head1.DataBind();
            sql = "select BrandName from BrandTab";
            lblBranding.Text = oo.ReturnTag(sql, "BrandName");
            sql = "select CollegeShortNa,Website, CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            lblUsername.Text = Session["DisplayName"].ToString();
            imgUser.ImageUrl = "../Alumni/" + Session["ImageUrl"].ToString();
            schoollogolink.HRef = "http://" + oo.ReturnTag(sql, "Website");
            schoollogolink.Target = "_blank";
            lblCollegeShortName.Text = oo.ReturnTag(sql, "CollegeShortNa");
            if (oo.ReturnTag(sql, "CologeLogoPath") != "")
            {
                Image1.ImageUrl = oo.ReturnTag(sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            else
            {
                Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
            }
        }
    }
}
