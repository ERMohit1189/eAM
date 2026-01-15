using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

public partial class admin_AllGeneralEnquiry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["LoginName"] == "" || Session["BranchCode"] == "" || Session["SessionName"] == "" || Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        //{
        //    Response.Redirect("default.aspx");
        //}

        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(FromYY, FromMM, FromDD);
            oo.AddDateMonthYearDropDown(ToYY, ToMM, ToDD);

            oo.FindCurrentDateandSetinDropDown(FromYY, FromMM, FromDD);
            oo.FindCurrentDateandSetinDropDown(ToYY, ToMM, ToDD);


        }
    }

    protected void FromYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromYY, FromMM, FromDD);
    }
    protected void FromDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ToYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToYY, ToMM, ToDD);
    }
    protected void ToDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        string todate = "", fromdate = "";
        todate = ToYY.SelectedItem.ToString() + "/" + ToMM.SelectedItem.ToString() + "/" + ToDD.SelectedItem.ToString();
        fromdate = FromYY.SelectedItem.ToString() + "/" + FromMM.SelectedItem.ToString() + "/" + FromDD.SelectedItem.ToString();


        sql = "select ROW_NUMBER() OVER (ORDER BY ad.Id desc) AS SrNo,ad.Id,Cm.CountryName,CS.CityName,Sm.StateName, Ad.Subject ,Ad.Name ,Ad.ContactNo ,Ad.MobileNo ,Ad.EMail ,";
        sql = sql + "  Ad.Address,Ad.EnquiryNo  from GeneralEnquiry AD";
        sql = sql + " left join CountryMaster CM on AD.CountryId=CM.Id";
        sql = sql + " left join StateMaster SM on AD.StateId=SM.Id";
        sql = sql + " left join CityMaster CS on AD.CityId=CS.Id ";
        sql = sql + " where ad.SessionName='" + Session["SessionName"].ToString() + "' and ad.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and Ad.Date between '" + fromdate + "'   and   '" + todate + "'  ";
        sql = sql + "    order by ad.Id desc";

        DataSet ds = new DataSet();
        ds = oo.GridFill(sql);

        if (ds != null)
        {
            dt = ds.Tables[0];
        }

        if (dt.Rows.Count > 0)
        {
            List<string> columnlist = new List<string>();
            columnlist.Add("#"); columnlist.Add("Enquiry No."); columnlist.Add("Purpose of Visit"); columnlist.Add("Visitor's Name");
            columnlist.Add("Contact No."); columnlist.Add("City"); columnlist.Add("E-mail");

            createDataTable(columnlist,dt, ltrshow);
        }
        else
        {

        }

    }

    public void createDataTable(List<string> columnlist,DataTable dt,Literal ltrlist)
    {
        string n = "<table id='example' class='table table-striped text-center table-hover no-head-border table-bordered pro-table table-header-group display nowrap' cellspacing='0' width='100%'> <thead> <tr>";
        foreach(string columnname in columnlist)
        {
            n = n + "<th>" + columnname + "</th>";
        }
        n = n + "</tr> </thead><tbody>";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            n += "<tr>";
            for (int j = 0; j < columnlist.Count ; j++)
            {
                n += "<td>" + dt.Rows[i][j] + "</td>";
            }
            n += "</tr>";
        }

        ltrlist.Text = n + "</tbody></table>";
    }

    public void createDataTableUsingTableColumn(DataTable dt, Literal ltrlist)
    {
        string n = "<table id='example' class='table table-striped text-center table-hover no-head-border table-bordered pro-table table-header-group display nowrap' cellspacing='0' width='100%'> <thead> <tr>";
        foreach (DataColumn column in dt.Columns)
        {
            n = n + "<th>" + column.ColumnName + "</th>";
        }
        n = n + "</tr> </thead><tbody>";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            n += "<tr>";
            foreach (DataColumn column in dt.Columns)
            {
                n += "<td>" + dt.Rows[i][column.ColumnName] + "</td>";
            }
            n += "</tr>";
        }
        ltrlist.Text = n + "</tbody></table>";
    }
}
