using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.MasterPage
{
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            CancelUnexpectedRePost();
            sql = "select BrandName from BrandTab where BranchId=" + Session["BranchCode"].ToString() + "";
            Label1.Text = oo.ReturnTag(sql, "BrandName");
        }
        catch (Exception) { }
    }
    private void CancelUnexpectedRePost()
    {
        string clientCode = _repostcheckcode.Value;

        //Get Server Code from session (Or Empty if null)
        string serverCode = Session["_repostcheckcode"] as string ?? "";

        if (!IsPostBack || clientCode.Equals(serverCode))
        {
            //Codes are equals - The action was initiated by the user
            //Save new code (Can use simple counter instead Guid)
            string code = Guid.NewGuid().ToString();
            _repostcheckcode.Value = code;
            Session["_repostcheckcode"] = code;
        }
        else
        {
            //Unexpected action - caused by F5 (Refresh) button
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}
