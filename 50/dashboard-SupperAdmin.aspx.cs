using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;


public partial class dashboardSupperAdmin : System.Web.UI.Page
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["SessionName"] == null || Session["LoginName"] == null)
        //{
        //    Response.Redirect("default.aspx");
        //}
    }
}