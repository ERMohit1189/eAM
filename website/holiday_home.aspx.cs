using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class website_holiday_home : System.Web.UI.Page
{
    
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repeater1.DataSource = null;// BLL.obj_bll1.getholidayhome();
            Repeater1.DataBind();
        }
    }
}