using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.IO;

public partial class Product_Services_Pricing : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sql = "select * from feeStructure";
        string fname = oo.ReturnTag(sql, "PDFName").ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "addPDF('" + fname + "');", true);
    }
}