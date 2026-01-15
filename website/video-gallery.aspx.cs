using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class website_video_gallery : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;
        // Campus camp = new Campus(); camp.LoadLoader(loader);
        con = oo.dbGet_connection();
        try
        {
            if (!IsPostBack)
            {
                getdata();
                //Selectjobtitel();
            }
        }
        catch
        {
        }
    }

    public void getdata()
    {
        try
        {
            string sql = String.Empty;
            // sql = "Select Id from videogallery order by Id desc";
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    Label Label1 = (Label)Repeater1.Items[i].FindControl("lblId");
                    Label lblTitel = (Label)Repeater1.Items[i].FindControl("Label1");
                    HtmlGenericControl iframe1 = (HtmlGenericControl)Repeater1.Items[i].FindControl("iframe1");
                    sql = "Select Youtubeframe,Title from videogallery where id='" + Label1.Text + "'";
                    string[] str = oo.ReturnTag(sql, "Youtubeframe").Split('/');
                    lblTitel.Text = oo.ReturnTag(sql, "Title");
                    HiddenField1.Value = "https://www.youtube.com/embed/" + str[str.Length - 1] + "?rel=0&amp;autoplay=0&showinfo=0&modestbranding=1";
                    iframe1.Attributes.Add("src", HiddenField1.Value.ToString());
                    iframe1.Attributes.Add("title", oo.ReturnTag(sql, "Title"));
                }
            }
        }
        catch (Exception ex)
        { throw ex; }
    }

}