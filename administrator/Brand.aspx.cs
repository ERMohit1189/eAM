using System.Data;
using System.Data.SqlClient;
using System;

public partial class BrandName : System.Web.UI.Page

{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginName"] == null)
        {
            Response.Redirect("default.aspx");
        }

        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        con = oo.dbGet_connection();
        if (!IsPostBack)
        {
            sql = "select BrandName from brandtab";
            txtBrandName.Text = oo.ReturnTag(sql, "BrandName");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "BrandTabProc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BrandName", txtBrandName.Text);
        cmd.Connection = con;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //oo.MessageBox("Updated successfully", this.Page);
            Campus camp = new Campus(); camp.msgbox(this.Page, msgbox, "Updated successfully", "S");       

            
            sql = "select BrandName from brandtab";
            txtBrandName.Text = oo.ReturnTag(sql, "BrandName");
        }
        catch (SqlException) { }

    }
}