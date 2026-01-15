using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class website_photo_gallery : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;
        con = oo.dbGet_connection();
        try
        {
            if (!IsPostBack)
            {
                BindDataList();
            }
        }
        catch
        {
        }
    }


private void BindDataList()
{
        Repeater1.DataSource = null;// BLL.obj_bll1.getphotogallery();
    Repeater1.DataBind();
}

protected void LinkButton1_Click(object sender, EventArgs e)
{
    LinkButton lnk = (LinkButton)sender;
    Response.Redirect("gallery.aspx?AlbumName=" + lnk.Text);
}

protected void ImageButton1_Click(object sender, EventArgs e)
{
    ImageButton imgbtn = (ImageButton)sender;
    Response.Redirect("gallery.aspx?AlbumName=" + imgbtn.AlternateText);
}

}