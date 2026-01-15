using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class website_testimonials : System.Web.UI.Page
{ 
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
        }
    }

    protected void loadData()
    {
        //sql = "select ROW_NUMBER() OVER (ORDER BY NoticeId ASC) AS SrNo ,NoticeId,NoticeHeading,NoticeMessage,StudentName,Class,Batch,ImagePath from TestimonialInformatio order by noticeId desc";
        Repeater1.DataSource = null;// BLL.obj_bll1.gettestimonials(); 
        Repeater1.DataBind();
    }

}