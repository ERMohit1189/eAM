using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class website_news_archive : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // string sql = "select ROW_NUMBER() OVER (ORDER BY NoticeId ASC) AS SrNo ,NoticeId,convert(nvarchar,NoticeDate,100) as NoticeDate,NoticeHeading,NoticeMessage,convert(nvarchar,NoticeToDate,100) as NoticeToDate from NoticeInformation  order by noticeId desc";
            Repeater1.DataSource = null;// BLL.obj_bll1.getnews();
            Repeater1.DataBind();
        }
    }
}