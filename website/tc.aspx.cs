using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class website_tc : System.Web.UI.Page
{
    string sql = "";
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ifram1.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSrno.Text))
        {
            sql = "Select PdfPath from TCDetails where srno='" + txtSrno.Text.Trim() + "'";
            if (oo.ReturnTag(sql, "PdfPath") != "")
            {
                ifram1.Attributes.Add("src", oo.ReturnTag(sql, "PdfPath"));
                ifram1.Visible = true;
            }
            else
            {
                ifram1.Attributes.Add("src", "");
                ifram1.Visible = false;
                oo.MessageBox("Sorry, No record(s) found!", this.Page);
            }
        }
        else
        {
            oo.MessageBox("Please, Enter S.R.No.!", this.Page);
        }
    }
}