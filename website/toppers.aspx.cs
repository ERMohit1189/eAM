using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class toppers : System.Web.UI.Page
{
    
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //  sql = "Select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo ,ID,Titel,Path,ISNULL(DocName,Path) as DocName from Toppers order by Id desc";
            Repeater1.DataSource = null;// BLL.obj_bll1.gettoppers();
            Repeater1.DataBind();
        }
    }
}