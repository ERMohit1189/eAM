using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;

public partial class mainStuExam : System.Web.UI.MasterPage
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission")
        {
            Response.Redirect("~/ap/default.aspx");
        }
        Session["logout"] = null;
        if (!IsPostBack)
        {
            lblUsername.Text = Session["LoginName"].ToString();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);
            
            Head1.DataBind();
            imgUser.ImageUrl = Session["ImageUrl"].ToString();
            sql = "select BrandName from BrandTab";
            lblBranding.Text = BAL.objBal.ReturnTag(sql, "BrandName");
            
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }
    
}
