using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListOfAlumni : Page
{
    private SqlConnection con;
    private readonly Campus oo;
    private string sql;

    public ListOfAlumni()
    {
        con = new SqlConnection();
        oo = new Campus();
        sql = string.Empty;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);

        if (!IsPostBack)
        {
            abc.Visible = false;
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
        }
    }
    
    public void filterDisplay()
    {
        divExport.Visible = false;
        abc.Visible = false;
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            if (drpLastYearAttended.SelectedValue != "")
            {
                param.Add(new SqlParameter("@LastAttendedYear", drpLastYearAttended.SelectedValue));
            }
            if (txtSearch.Text.Trim() != "")
            {
                param.Add(new SqlParameter("@Searchtext", txtSearch.Text.Trim()));
            }
            if (drpStatus.SelectedValue != "")
            {
                param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
            }
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            param.Add(new SqlParameter("@Action", "Select"));
            var ds = new DLL().Sp_SelectRecord_usingExecuteDataset("Sp_AlumniRegistration", param);
            if (ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                GrdDiscountDetails.DataSource = ds;
                GrdDiscountDetails.DataBind();
                divExport.Visible = true;
                abc.Visible = true;
                heading.Text = "List of Alumni";
                string str = "";
                if (drpLastYearAttended.SelectedIndex != 0)
                {
                    str = str + "Year of Passing : " + drpLastYearAttended.SelectedItem.Text;
                }
                if (drpLastYearAttended.SelectedIndex != 0 && drpStatus.SelectedIndex != 0)
                {
                    str = str + " | Status : " + drpStatus.SelectedItem.Text;
                }
                if (drpLastYearAttended.SelectedIndex == 0 && drpStatus.SelectedIndex != 0)
                {
                    str = str + "Status : " + drpStatus.SelectedItem.Text;
                }
                lblRegister.Text = str;
            }
        }
        catch (Exception)
        {
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        filterDisplay();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "ListofAlumni", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "ListofAlumni.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "ListofAlumni", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    public override void Dispose()
    {
        con.Dispose();
        oo.Dispose();
    }

   
}