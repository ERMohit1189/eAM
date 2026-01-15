using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;

public partial class mainsadmin : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission")
        {
            Response.Redirect("~/ap/default.aspx");
        }
        con = oo.dbGet_connection();
        try
        {
            if (string.IsNullOrEmpty(Session["LoginName"].ToString()))
            {
                Response.Redirect("../default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("../default.aspx");
        }
        Session["logout"] = null;
        if (!IsPostBack)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);

            DrpSessionName.DataSource = ds.Tables[1];
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
            //}

            Head1.DataBind();
            sql = "select BrandName from BrandTab";
            lblBranding.Text = oo.ReturnTag(sql, "BrandName");
            sql = "select CollegeShortNa,Website, CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            lblUsername.Text = Session["DisplayName"].ToString();
            imgUser.ImageUrl = Session["ImageUrl"].ToString();
            schoollogolink.HRef = "http://" + oo.ReturnTag(sql, "Website");
            schoollogolink.Target = "_blank";
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        
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
    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }
}
