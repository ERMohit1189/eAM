using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.Services;

public partial class website_CAREER : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.LCID = 2057;

        con = oo.dbGet_connection();
        //  Campus camp = new Campus(); camp.LoadLoader(loader);
        //if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        //{
        //    Response.Redirect("default.aspx");
        //}
        //  Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            Session["JobId"] = "";
            loadcareer();
        }
    }

    public void loadcareer()
    {
      //  sql = "Select JobId,convert(nvarchar,Fdate,106) Fdate,convert(nvarchar,Tdate,106) as Tdate,JobTitle,DepartMent,Post,Experience,Qualification,Salary,NoofPosition,JobDescription from PostJob";
        try
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "USP_PostJob";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@QueryFor", "S");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            else
            { }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        Session["JobId"] = lnk.ToolTip;
        Response.Redirect("jobapply.aspx");
    }

}