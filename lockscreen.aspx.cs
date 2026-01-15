using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lockscreen : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["LoginNamelock"] = Session["LoginName"];
            string sqls = "select top(1) SessionID, SessionName from SessionMaster where BranchCode=" + Session["BranchCode"] + " order by SessionID desc";
            Session["SessionNamelock"] = oo.ReturnTag(sqls, "SessionName");
            Session["BranchCodelock"] = Session["BranchCode"];
            ////if (Session["LoginName"].ToString() == "" || Session["BranchCode"].ToString() == "" || Session["SessionName"].ToString() == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
            ////{
            ////    Response.Redirect("default.aspx");
            ////}
            Session["SessionName"] = Session["LoginName"] = Session["BranchCode"] = "";
            imgUser.ImageUrl = Session["ImageUrl"].ToString();

            sql = "select BrandName from BrandTab";
            if (oo.ReturnTag(sql, "BrandName")!="")
            {
                lblCompanyName.Visible = true;
                Label1.Text = oo.ReturnTag(sql, "BrandName");
            }
        }
    } 
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton1.Enabled = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "btt();", true);
        string sql1 = "";
        sql = "select Distinct lt.loginTypeId from LoginTab lt where (lt.IsActive=1 or lt.IsActive is Null) and";
        sql = sql + " lt.LoginName ='" + Session["LoginNamelock"].ToString() + "' and lt.Pass='" + txtPassword.Text + "'";
        string str = oo.ReturnTag(sql, "loginTypeId");
        if (str == "6" || str == "5")
        {
            sql1 = "Select Distinct LoginName,LoginId,lt.loginTypeId from LoginTab  lt  left join BranchTab bt on lt.BranchId=bt.BranchId";
            sql1 = sql1 + " where (lt.IsActive=1 or lt.IsActive is Null) and";
            sql1 = sql1 + " lt.LoginName ='" + Session["LoginNamelock"].ToString() + "' and lt.Pass='" + txtPassword.Text + "' and lt.BranchId='" + Session["BranchCodelock"].ToString() + "'";
        }
        else
        {
            sql1 = "select Distinct LoginName,LoginId,lt.loginTypeId from LoginTab  lt  left join BranchTab bt on lt.BranchId=bt.BranchId left join menu_permission mp on mp.Login_Id=lt.LoginId";
            sql1 = sql1 + " where (lt.IsActive=1 or lt.IsActive is Null) and lt.LoginName ='" + Session["LoginNamelock"].ToString() + "'";
            sql1 = sql1 + " and lt.Pass='" + txtPassword.Text + "' and lt.BranchId='" + Session["BranchCodelock"].ToString() + "'";
        }
        Session["LoginName"] = Session["LoginNamelock"];
        Session["LoginId"] = oo.ReturnTag(sql1, "LoginId");
        Session["BranchCode"] = Session["BranchCodelock"];
        Session["SessionName"] = Session["SessionNamelock"];
        sql = "select BranchName from BranchTab where BranchId=" + Session["BranchCodelock"] + "";
        Session["BranchName"] = oo.ReturnTag(sql1, "BranchName");
        sql = "Select  SessionId from SessionMaster  where SessionName='" + Session["SessionName"] + "'";
        Session["SessionID"] = oo.ReturnTag(sql, "SessionId");

        Page.Validate();
        if (Page.IsValid)
        {
            if (oo.Duplicate(sql1))
            {
                alert_danger.Style.Add("display", "none");
                lbllog.Text = "Log in successful. Loading Dashboard.";
                alert_success.Style.Add("display", "block");
                System.Threading.Thread.Sleep(200);
                if (str == "1")
                {
                    Session["Expire"] = "TimeOUT";
                    Session["Logintype"] = "SuperAdmin";
                    sql = "Select ISNULL(DisplayName,Name) as DisplayName from NewSuperAdminInformation where userid='" + Session["LoginName"].ToString() + "' ";
                    if (oo.ReturnTag(sql, "DisplayName") == "")
                    {
                        Session["DisplayName"] = Session["LoginName"];
                    }
                    else
                    {
                        Session["DisplayName"] = oo.ReturnTag(sql, "DisplayName");
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + BLL.GetSiteRoot() + "/dashboard-main.aspx';", true);

                }
                else if (str == "2")
                {
                    Session["Expire"] = "TimeOUT";
                    Session["Logintype"] = "Admin";
                    sql = "Select ISNULL(DisplayName,(Case When CHARINDEX(' ',Name)=0 then Name Else Left(Name,CHARINDEX(' ',Name)) End)) as DisplayName,";
                    sql = sql + " PhotoPath from NewAdminInformation where userid='" + Session["LoginName"].ToString() + "' ";
                    if (oo.ReturnTag(sql, "DisplayName") == "")
                    {
                        Session["DisplayName"] = Session["LoginName"];
                    }
                    else
                    {
                        Session["DisplayName"] = oo.ReturnTag(sql, "DisplayName");
                    }
                    if (oo.ReturnTag(sql, "PhotoPath") == string.Empty)
                    {
                        Session["ImageUrl"] = "~/img/user-pic/user-pic.jpg";
                    }
                    else
                    {
                        Session["ImageUrl"] = oo.ReturnTag(sql, "PhotoPath");
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + BLL.GetSiteRoot() + "/dashboard-main.aspx';", true);
                }
                else if (str == "3")
                {
                    Session["Expire"] = "TimeOUT";
                    Session["Logintype"] = "Staff";
                    sql = "Select PhotoPath from EmpGeneralDetail where Ecode='" + Session["LoginName"].ToString() + "' ";
                    Session["DisplayName"] = Session["LoginName"];
                    if (oo.ReturnTag(sql, "PhotoPath") == string.Empty)
                    {
                        Session["ImageUrl"] = "~/img/user-pic/user-pic.jpg";
                    }
                    else
                    {
                        Session["ImageUrl"] = oo.ReturnTag(sql, "PhotoPath");
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + BLL.GetSiteRoot() + "/dashboard-main.aspx';", true);
                }
                else if (str == "4")
                {
                    Session["Expire"] = "TimeOUT";
                    Session["Logintype"] = "Student";
                    Response.Redirect("~/13/studashboard.aspx");
                }
                else if (str == "5")
                {
                    Session["Expire"] = "TimeOUT";
                    Session["Logintype"] = "Guardian";
                    Session["ImageUrl"] = "~/img/user-pic/user-pic.jpg";
                    Response.Redirect("~/sp/sp-dashboard.aspx");
                }
                else if (str == "6")
                {
                    Session["Expire"] = "TimeOUT";
                    Session["Logintype"] = "Administrator";
                    Session["ImageUrl"] = "~/img/user-pic/user-pic.jpg";
                    Session["DisplayName"] = Session["LoginName"].ToString();
                    Response.Redirect("~/Administrator/dashboard.aspx");
                }
            }
            else
            {
                LinkButton1.Enabled = true;
                alert_success.Style.Add("display", "none");
                alert_danger.Style.Add("display", "block");
            }
        }
        else
        {
            LinkButton1.Enabled = true;
            return;
        }
    }
}