using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class website_gallery : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;
        Session["AlbumName"] = Request.QueryString["AlbumName"];
        con = oo.dbGet_connection();
        try
        {
            if (!IsPostBack)
            {
                Label2.Text = Session["AlbumName"].ToString();
                BindDataList();
            }
        }
        catch
        {
        }
    }

    private void BindDataList()
    {
        Repeater1.DataSource = null;
        Repeater1.DataBind();

    }

}