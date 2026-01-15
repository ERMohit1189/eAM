using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class website_media_gallery : System.Web.UI.Page
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
        //DataTable dt = new DataTable();
        //string strQuery = "Select  ROW_NUMBER() OVER (ORDER BY ImageId ASC) AS SrNo , ImageId,Title,ImagePath from MediaPhoto";
        //SqlCommand cmd = new SqlCommand(strQuery);
        //SqlDataAdapter sda = new SqlDataAdapter();
        //cmd.CommandType = CommandType.Text;
        //cmd.Connection = con;
        //try
        //{
        //    con.Open();
        //    sda.SelectCommand = cmd;
        //    sda.Fill(dt);
        //}
        //catch (SqlException) { }

        //Bind resulted DataSource into the Repeater
        Repeater1.DataSource = null;// BLL.obj_bll1.getmediagallery();
        Repeater1.DataBind();

    }

}