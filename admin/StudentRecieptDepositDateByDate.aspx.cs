using System.Web.UI;
using System.Data.SqlClient;
using System;

public partial class admin_StudentRecieptDepositDateByDate : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
        }
    }
    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}