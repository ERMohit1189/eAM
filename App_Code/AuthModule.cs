using System;
using System.Data.SqlClient;
using System.Web;

public class AuthModule : IHttpModule
{
    SqlConnection con;
    Campus oo = new Campus();
    public AuthModule()
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
    }
    public void Init(HttpApplication context)
    {
        context.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
    }

    private void OnPreRequestHandlerExecute(object sender, EventArgs e)
    {
        HttpApplication app = (HttpApplication)sender;
        HttpContext context = app.Context;

        if (context.CurrentHandler is System.Web.UI.Page)
        {
            //string LoginTypeId = "6";
            //string path = context.Request.AppRelativeCurrentExecutionFilePath.ToLower(); // e.g. ~/admin/dashboard.aspx
            string path = context.Request.AppRelativeCurrentExecutionFilePath.ToLower().Replace("~", "").TrimStart('/').Split('?')[0];
            // Pages to skip
            if (path.Contains("default.aspx"))
            {
                return;
            }
            if (context.Session == null || context.Session["LoginId"] == null || context.Session["LoginTypeId"] == null)
            {
                if (path == "forgot-password.aspx" || path == "termsand_conditions.aspx"
                    || path == "privacy_policy.aspx" || path == "product_services_pricing.aspx"
                    || path == "contact_us.aspx" || path == "refund_policy.aspx" || path == "ugc_license.aspx")
                {
                    return;
                }
                else
                {
                    context.Response.Redirect("~/default.aspx");
                }
                //  context.Response.Redirect("../default.aspx");


                //Response.Redirect("default.aspx");

            }
            else
            {
                string LoginTypeId = context.Session["LoginTypeId"].ToString();
                if (LoginTypeId == "2" || LoginTypeId == "3")
                {
                    if (
                            path.Contains("server") ||
                            path == "dashboard-main.aspx" ||
                            path == "chartimg.axd" ||
                            path == "planner.aspx" ||
                            path == "your_account.aspx" ||
                            path == "profile.aspx?print=1" ||
                            path == "profile.aspx" ||
                            path == "ChangePassword.aspx" ||
                            path == "ChangePassword.aspx" ||
                            path == "changepassword.aspx" ||
                            path == "forgot-password.aspx" ||
                            path == "changepassword.aspx" ||
                            path == "lockscreen.aspx" ||
                            path == "userguide.aspx" ||
                            path == "2/FeeReceiptAllDuplicate.aspx" ||
                            path == "2/feereceiptall.aspx" ||
                            path == "suggestanidea.aspx" ||
                            path == "4/print_new.aspx" ||
                            path == "2/print_new.aspx" ||
                            path == "2/feereceiptallduplicate.aspx" ||
                            path == "11/studentregview.aspx" ||
                            path == "2/ccprint.aspx" ||
                            path == "2/ccprint.aspx" ||
                            path == "2/print_new.aspx" ||
                            path == "2/otherfeereceipt.aspx" ||
                            path == "2/pafreciept_duplicate.aspx" ||
                            path == "2/admtemplate2.aspx" ||
                            path == "2/tccollection.aspx" ||
                            path == "2/tcreciept.aspx" ||
                            path == "2/tccbse_english.aspx" ||
                            path == "2/tccbse_hindi.aspx" ||
                            path == "2/tcicse.aspx" ||
                            path == "2/tccountersign.aspx" ||
                            path == "2/tccountersignhindi.aspx" ||
                            path == "1/studentidcardnarmal.aspx" ||
                            path == "1/print_new.aspx" ||
                            path == "11/print_new.aspx" ||
                            path == "2/admtemplate3.aspx" ||
                            path == "2/pafreciept_duplicate.aspx" ||
                            path == "2/ccreciept.aspx" ||
                            path == "2/cccollection.aspx" ||
                            path == "2/additionalfeesdeposit.aspx" ||
                            path == "2/additionalfeereceipt_duplicate.aspx" ||
                            path == "2/otherfeehead.aspx" ||
                            path == "11/newsreport.aspx" ||
                            path == "11/attendancereport.aspx" ||
                            path == "8/empattendancereport.aspx" ||
                            path == "2/admtemplate3.aspx" ||
                            path == "16/g6/server/ddlmedium_markentry_6.aspx" ||
                            path == "16/g6/server/ddlclass_markentry_6.aspx" ||
                            path == "16/g6/server/ddlbranch_markentry_6.aspx" ||
                            path == "16/g6/server/ddlstream_6.aspx" ||
                            path == "16/g6/server/ddlsection_markentry_6.aspx" ||
                            path == "16/g6/server/ddlsubject_markentry_6_medium.aspx" ||
                            path == "16/g6/server/ddlpaper_markentry_6_medium.aspx" ||
                            path == "16/g6/server/loadmarksentry.aspx" ||
                            path == "16/g6/server/savemarksentry.aspx" ||
                            path == "17/cumulativereport.aspx" ||
                            path == "admin/server/sessionchange.aspx" ||
                            path == "11/plannerreport.aspx" ||
                            path == "planner.aspx" ||
                            path == "planner1.aspx"
                        )
                    {
                        return;
                    }
                    else
                    {
                        // If session is null or not logged in
                        if (context.Session == null || context.Session["LoginId"] == null)
                        {
                            // context.Response.Redirect("../default.aspx");
                            context.Response.Redirect("~/default.aspx");
                            //Response.Redirect("default.aspx");
                            return;
                        }
                        string loginID = context.Session["LoginID"].ToString();
                        if (!IsPageAccessAllowed(loginID, path))
                        {
                            // context.Response.Redirect("../default.aspx");
                            context.Response.Redirect("~/default.aspx");
                            return;
                        }
                    }

                }
                else
                {
                    return;
                }
            }




        }
    }

    private bool IsPageAccessAllowed(string loginID, string pagePath)
    {
        bool hasAccess = false;

        //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;

        // using (SqlConnection conn = new SqlConnection(connectionString)
        // {
        string query = @"
            SELECT COUNT(*) 
            FROM menu_permission mp
            INNER JOIN Menueam mm ON mp.menu_id = mm.MenuID 
            WHERE mp.Login_Id = @LoginID AND LOWER(mm.Url) = @PageUrl and mp.Permission_Value='Yes'";


        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@LoginID", loginID);
            cmd.Parameters.AddWithValue("@PageUrl", pagePath);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            hasAccess = count > 0;
        }
        //}

        return hasAccess;
    }

    public void Dispose() { }
}
