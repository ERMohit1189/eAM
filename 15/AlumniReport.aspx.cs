using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AlumniReport : Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;

    public AlumniReport()
    {
        con = new SqlConnection();
        oo = new Campus();
        sql = string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file

        if (!IsPostBack)
        {
            sql = " with yearlist as ";
            sql = sql + " ( ";
            sql = sql + " select 1950 as year ";
            sql = sql + " union all ";
            sql = sql + " select yl.year + 1 as year ";
            sql = sql + " from yearlist yl ";
            sql = sql + " where yl.year + 1 <= YEAR(GetDate()) ";
            sql = sql + " ) ";

            sql = sql + "  select year from yearlist order by year desc; ";
            BAL.objBal.FillDropDown_withValue(sql, drpLastYearAttended, "year", "year");
            drpLastYearAttended.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpLastYearAttended.SelectedValue = Session["LastAttendedYear"].ToString();
            filterDisplay(drpLastYearAttended.SelectedValue);
        }
    }
    
    public void filterDisplay(string LastYearAttended)
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            if (LastYearAttended!="")
            {
                param.Add(new SqlParameter("@LastAttendedYear", LastYearAttended));
            }
            param.Add(new SqlParameter("@ContactNo", Session["LoginName"]));
            param.Add(new SqlParameter("@Action", "SelectForAlumni"));
            var ds = new DLL().Sp_SelectRecord_usingExecuteDataset("Sp_AlumniRegistration", param);
            if (ds!=null && ds.Tables.Count>0)
            {
                GrdDiscountDetails.DataSource = ds;
                GrdDiscountDetails.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void drpLastYearAttended_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterDisplay(drpLastYearAttended.SelectedValue);
    }
    public override void Dispose()
    {
        con.Dispose();
        oo.Dispose();
    }

   
}