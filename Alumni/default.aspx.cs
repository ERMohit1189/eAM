using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

public partial class defaultAlumani : System.Web.UI.Page
{
    SqlConnection con;
    Campus oo = new Campus();
    string sql = "";//   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con= new SqlConnection();
            con = BAL.objBal.dbGet_connection();
            txtUserName.Focus();

            if (!IsPostBack)
            {
                hdnSubscription.Value = "";
                sql = "SELECT COUNT(*) CNT FROM tblManagerInfo";
                if (oo.ReturnTag(sql, "CNT") == "0")
                {
                    Expbody.Visible = true;
                    alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Subscription expired, please contact administrator!";
                    hdnSubscription.Value = "Exp";
                }
                else
                {
                    sql = "SELECT (case when convert(date, dob)<convert(date, getdate()) then 'Exp' else 'Liv' end)sts FROM tblManagerInfo";
                    if (oo.ReturnTag(sql, "sts") == "Exp")
                    {
                        Expbody.Visible = true;
                        alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Subscription expired, please contact administrator!";
                        hdnSubscription.Value = "Exp";
                    }
                }
                Session["LoginName"] = "";
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                chkRememberMe.Checked = false;
                txtUserName.Focus();
            }
        }
        catch(Exception ex)
        {
            alert_success.Style.Add("display", "none");
            alert_danger.Style.Add("display", "block");
            lblError.Text = "Please, try Again!";
        }
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        LinkButton1.Enabled = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "btt();", true);
        if (chkRememberMe.Checked)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
        }

        Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
        Response.Cookies["Password"].Value = txtPassword.Text.Trim();

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@ContactNo", txtUserName.Text.Trim()));
        param.Add(new SqlParameter("@Password", txtPassword.Text.Trim()));
        param.Add(new SqlParameter("@Action", "Login"));
        ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("Sp_AlumniRegistration", param);
        Page.Validate();
        if (Page.IsValid)
        {
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["loginTypeId"].ToString() != "6" && hdnSubscription.Value == "Exp")
                    {
                        alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Portal Closed!";
                        hdnSubscription.Value = "Exp";
                        return;
                    }
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "Inactive")
                    {
                        alert.InnerHtml = "<i class='fa fa-warning'></i><br /> You are inactive, please contact to branch!";
                        hdnSubscription.Value = "Exp";
                        return;
                    }
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "Rejected")
                    {
                        alert.InnerHtml = "<i class='fa fa-warning'></i><br /> Your Alumni Registration has been rejrcted, please contact to branch!";
                        hdnSubscription.Value = "Exp";
                        return;
                    }
                    Session["DisplayName"] = ds.Tables[0].Rows[0]["Fname"].ToString();
                    Session["LoginName"] = txtUserName.Text.Trim();
                    Session["Password"] = txtPassword.Text.Trim();
                    Session["LoginTypeId"] = "0";
                    Session["ImageUrl"]= ds.Tables[0].Rows[0]["RecentPhoto"].ToString();
                    Session["LastAttendedYear"] = ds.Tables[0].Rows[0]["LastAttendedYear"].ToString();
                    Session["BranchCode"] = (ds.Tables[0].Rows[0]["BranchCode"].ToString() == "" ? null : ds.Tables[0].Rows[0]["BranchCode"].ToString());
                    
                    alert_danger.Style.Add("display", "none");
                    lbllog.Text = "Log in successful. Loading Dashboard.";
                    alert_success.Style.Add("display", "block");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "window.location='" + Campus.GetSiteRoot() + ds.Tables[0].Rows[0]["Url"].ToString() + "';", true);
                }
                else
                {
                    alert_success.Style.Add("display", "none");
                    alert_danger.Style.Add("display", "block");
                    lblError.Text = "You've entered incorrect Username or Password.";
                    LinkButton1.Enabled = true;
                }
            }
            else
            {
                alert_success.Style.Add("display", "none");
                alert_danger.Style.Add("display", "block");
                lblError.Text = "You've entered incorrect Username or Password.";
                LinkButton1.Enabled = true;
            }
        }
        else
        {
            return;
        }

    }

    public static string GetSiteRoot()
    {
        string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (port == null || port == "80" || port == "443")
            port = "";
        else
            port = ":" + port;

        string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (protocol == null || protocol == "0")
            protocol = "http://";
        else
            protocol = "https://";

        string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

        if (sOut.EndsWith("/"))
        {
            sOut = sOut.Substring(0, sOut.Length - 1);
        }

        return sOut;
    }
}
