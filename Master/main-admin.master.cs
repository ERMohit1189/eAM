using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class admin_main_admin : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["BranchCode"] as string))
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"] != null && Session["Logintype"].ToString() == "FromAdmission")
        {
            Response.Redirect("~/ap/default.aspx");
        }
        Session["logout"] = null;
        if (!IsPostBack)
        {
            if (Session["LoginType"].ToString().ToLower() == "staff" || Session["LoginType"].ToString().ToLower() == "guardian" || Session["LoginType"].ToString().ToLower() == "student")
            {
                DrpSessionName.Enabled = false;
            }
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Queryfor", "S"));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            DataSet ds = new DataSet();
            ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Get_GenralInfo", param);

            DrpSessionName.DataSource = ds.Tables[1];
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();

            DrpSessionName.SelectedValue = Session["SessionName"].ToString();

            //current session

            Head1.DataBind();
            ADDTopMenuItem();
            sql = "select BrandName from BrandTab";
            lblBranding.Text = oo.ReturnTag(sql, "BrandName");
            sql = "select CollegeShortNa,Website,CologeLogoPath from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
            string kk = "select top(1) ShortCode from BranchTab where BranchId = " + Session["BranchCode"] + "";
            lblCollegeShortName.Text = oo.ReturnTag(kk, "ShortCode");
            lblUsername.Text = Session["DisplayName"].ToString();
            if (Session["ImageUrl"].ToString() == "")
            {
                imgUser.ImageUrl = "../img/user-pic/user-pic.jpg";
            }
            else
            {
                imgUser.ImageUrl = Session["ImageUrl"].ToString();
            }
            schoollogolink.HRef = "http://" + oo.ReturnTag(sql, "Website");
            schoollogolink.Target = "_blank";
            if (oo.ReturnTag(sql, "CologeLogoPath") != "")
            {
                Image1.ImageUrl = oo.ReturnTag(sql, "CologeLogoPath") + "?print=" + DateTime.Now;
            }
            else
            {
                Image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
            }
            string isPrintPage = Request.QueryString["print"];

            if (isPrintPage == null)
            {
                isPrintPage = "Print=1";
            }
            string checkRedirectpath = Request.QueryString["check"];

            if (BLL.BLLInstance.checkUrl(Request.Url.AbsoluteUri, Session["LoginId"].ToString(), Session["Logintype"].ToString(), isPrintPage, checkRedirectpath) == false)
            {
                Response.Redirect("~/403.aspx");
            }

            var paramss = new List<SqlParameter>
            {
                new SqlParameter("@Time", 2),
            };
            var msg = DLL.objDll.Sp_Insert_Update_Delete_usingExecuteNonQuery("KillSession", paramss);

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

    private void ADDTopMenuItem()
    {
        string sql = "";
        string isfor = "", value = "", table = "", logintypeid = "0";
        if (Session["Logintype"].ToString().Trim() == "Admin")
        {
            isfor = "Admin";
            value = "1";
            table = "menu_permission";
            logintypeid = "2";
        }
        else if (Session["Logintype"].ToString().Trim() == "SuperAdmin")
        {
            isfor = "SuperAdmin";
            value = "1";
            table = "super_admin_menupermission";
        }
        else if (Session["Logintype"].ToString().Trim() == "Staff")
        {
            isfor = "Staff";
            value = "1";
            table = "menu_permission";
            logintypeid = "3";
        }
        sql = "SELECT MenuID,text,ParentClassName FROM Menueam Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and Login_Id='" + Session["LoginId"].ToString() + "' and Permission_Value='Yes' and ParentId is NUll and isnull(Permission,0)=1";
        if (logintypeid != "0")
        {
            sql = sql + " and LoginTypeId='" + logintypeid + "'";
        }
        sql = sql + " and Menueam.Status=1 order by Menueam.MenuOrder asc";
        DataSet dsa = new DataSet();
        dsa = oo.GridFill(sql);
        Repeater1.DataSource = oo.GridFill(sql);
        Repeater1.DataBind();
        if (Repeater1.Items.Count > 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Repeater rp2 = (Repeater)Repeater1.Items[i].FindControl("Repeater2");
                Label lblId = (Label)Repeater1.Items[i].FindControl("Label2");
                AddfirstChildMenu(rp2, lblId.Text.Trim(), table, isfor, value);
            }

        }
    }

    private void AddfirstChildMenu(Repeater rp, string parentid, string table, string isfor, string value)
    {
        sql = "SELECT MenuID,text,('" + isfor + "'+'/') as root,(Case When ISNULL(Url,'')='' Then '' Else Url End ) as URL,ParentClassName,ChildClassName FROM Menueam ";
        sql = sql + " Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and ParentId is not NUll and Login_Id='" + Session["LoginId"].ToString() + "' and Permission_Value='Yes' and ParentID='" + parentid + "' and Menueam.Status=1 order by Menueam.MenuOrder asc";
        rp.DataSource = oo.GridFill(sql);
        rp.DataBind();
        if (rp.Items.Count > 0)
        {
            for (int i = 0; i < rp.Items.Count; i++)
            {
                Repeater rp3 = (Repeater)rp.Items[i].FindControl("Repeater3");
                Label lblId = (Label)rp.Items[i].FindControl("Label2");
                AddSecondChildNode(rp3, lblId.Text.Trim(), table, isfor, value);
                if (rp3.Items.Count == 0)
                {
                    HtmlGenericControl sub_child_menu_badge = (HtmlGenericControl)rp.Items[i].FindControl("sub_child_menu_badge");
                    sub_child_menu_badge.Visible = false;
                    HtmlGenericControl sub_child_menu = (HtmlGenericControl)rp.Items[i].FindControl("sub_child_menu");
                    sub_child_menu.Visible = false;
                }
            }

        }

    }

    private void AddSecondChildNode(Repeater rp, string parentid, string table, string isfor, string value)
    {
        sql = "SELECT MenuID,text,('" + isfor + "'+'/') as root,(Case When ISNULL(Url,'')='' Then '' Else Url End ) as URL,ParentClassName,ChildClassName FROM Menueam ";
        sql = sql + " Inner join " + table + " mp on mp.Menu_Id=Menueam.MenuID";
        sql = sql + " where " + isfor + "=" + value + " and ParentId is not NUll and Login_Id='" + Session["LoginId"].ToString() + "' and Permission_Value='Yes' and ParentID='" + parentid + "' and Menueam.Status=1 order by Menueam.MenuOrder asc";
        rp.DataSource = oo.GridFill(sql);
        rp.DataBind();
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect("~/default.aspx");
    }

    protected void lnkLogout1_Click(object sender, EventArgs e)
    {
        Session["logout"] = "You have been logged out successfully.<br/> Thank you for using eAM.";
        Response.Redirect(ResolveClientUrl("~/default.aspx"));
    }

    protected void DrpSessionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionName"] = DrpSessionName.SelectedItem.ToString();
        var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
        Response.Redirect(ResolveClientUrl(path));
    }
}
