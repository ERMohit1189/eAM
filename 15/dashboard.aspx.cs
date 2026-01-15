using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Data;
public partial class dashboard : System.Web.UI.Page
{
    Campus oo = new Campus();
    public SqlConnection con;
    public SqlCommand cmd = new SqlCommand();
    string sql = "";
    public void MakeConnection()
    {
        con = new SqlConnection();
        try
        {
            cmd = new SqlCommand();
            con = oo.dbGet_connection();
            con.Open();
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("~/Alumni/default.aspx");
        }
        if (!IsPostBack)
        {
            LoadBulletinWd(w1);
        }
    }
    public string LoadBulletinWd(Control parentId)
    {
        var msg = "";
        try
        {
            const string path = "~/Alumni/bulletin.ascx";
            var UC = LoadControl(path);
            parentId.Controls.Add(UC);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return msg;
    }
}