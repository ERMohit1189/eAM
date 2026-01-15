using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;

public partial class admin_main_admin : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con = oo.dbGet_connection();
        if (Session["Logintype"].ToString() != "Administrator")
        {
            Response.Redirect("~/403.aspx");
        }
        Session["logout"] = null;
        //Image1.ImageUrl = "~/DisplayImage.ashx?UserLoginID=" + 1;
        if (!IsPostBack)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //param.Add(new SqlParameter("@Queryfor", "S"));
            //param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            //DataSet ds = new DataSet();
            //ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);

            //DrpSessionName.DataSource = ds.Tables[1];
            //DrpSessionName.DataTextField = "SessionName";
            //DrpSessionName.DataValueField = "SessionName";
            //DrpSessionName.DataBind();

            //DrpSessionName.SelectedValue = Session["SessionName"].ToString();



            Head1.DataBind();
            sql = "select BrandName from BrandTab";
            lblBranding.Text = oo.ReturnTag(sql, "BrandName");
            //sql = "select CollegeShortNa,Website from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            //lblCollegeShortName.Text = oo.ReturnTag(sql, "CollegeShortNa");
            lblUsername.Text = Session["DisplayName"].ToString();
            //lblSessionName.Text = Session["SessionName"].ToString();
            imgUser.ImageUrl = Session["ImageUrl"].ToString();
            //schoollogolink.HRef = "http://" + oo.ReturnTag(sql, "Website");
            //schoollogolink.Target = "_blank";
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect(ResolveClientUrl("~/default.aspx"));
    }

    protected void lnkLogout1_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect(ResolveClientUrl("~/default.aspx"));
    }
    //protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
    //    Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    //}
}
