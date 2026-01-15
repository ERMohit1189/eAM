using System;
using System.Web;

public partial class admin_server_SessionChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string SessionName = Request.Form["SessionName"].ToString();
            Session["SessionName"] = SessionName.ToString();
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        catch (Exception)
        {
        }
    }
}