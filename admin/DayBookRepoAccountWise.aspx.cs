using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class Admin_DayBookRepoAccountWise : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;
        con = oo.dbGet_connection();
        if (Session["SessionName"] == null || Session["LoginName"] == null || Session["BranchCode"] == null)
        {
            Response.Redirect("default.aspx");
        }


        if (!IsPostBack)
        {
            oo.AddDateMonthYearDropDown(drpFYY, DrpFMM, DrpFDD);
            oo.AddDateMonthYearDropDown(DrpTYY, DrpTMM, DrpTDD);
            oo.FindCurrentDateandSetinDropDown(drpFYY, DrpFMM, DrpFDD);
            oo.FindCurrentDateandSetinDropDown(DrpTYY, DrpTMM, DrpTDD);
            //lblOpeningBal.Visible = false;
            //lblOpengBalLabel.Visible = false;


            sql = "select OpeningBalance from Daybookrecord where RecordNumber=(select max(RecordNumber) from DayBookRecord)";
            sql = sql + "  and Branch='" + Session["BranchName"].ToString() + "'";
            lblOpengBal.Text = oo.ReturnTag(sql, "OpeningBalance");
            Panel1.Visible = false;


            sql = "Select ExpenseAccountCode,ExpenseAccountHead,Remark from ExpenseHeadMaster";
            //oo.FillDropDown(sql, DropDownList1,"ExpenseAccountHead");



        }


    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        string Fdate1 = "", Tdate = "";

        Fdate1 = drpFYY.SelectedItem.ToString() + " " + DrpFMM.SelectedItem.ToString() + " ";
        if (DrpFDD.SelectedItem.ToString().Length == 1)
        {
            Fdate1 = Fdate1 + "0" + DrpFDD.SelectedItem.ToString();
        }
        else
        {
            Fdate1 = Fdate1 + DrpFDD.SelectedItem.ToString();
        }



        Tdate = DrpTYY.SelectedItem.ToString() + " " + DrpTMM.SelectedItem.ToString() + " ";
        if (DrpTDD.SelectedItem.ToString().Length == 1)
        {
            Tdate = Tdate + "0" + DrpTDD.SelectedItem.ToString();
        }
        else
        {
            Tdate = Tdate + DrpTDD.SelectedItem.ToString();
        }


        sql = "select  ROW_NUMBER() OVER (ORDER BY SrNo ASC) AS SrNo  ,CONVERT(nvarchar,CurrentDate,106) as CurrentDate,OpeningBalance,IncomeHead , ";
        sql = sql + "    Amount,Dr,Cr ,case when EmpId ='<--Select-->' then IncomeHead else EmpName+'('+Designation+')' End as Particulars ";
        sql = sql + "        from DayBookRecord   ";
        sql = sql + "    where ";
        sql = sql + "  CurrentDate between '" + Fdate1 + "'  and  '" + Tdate + "'";
        sql = sql + " and branch='" + Session["BranchName"].ToString() + "'";
        sql = sql + " order by CurrentDate desc";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        Panel1.Visible = true;

        lblResult.Text="";
        // Label lblOpeningBal = (Label)GridView1.HeaderRow.FindControl("lblOpeningBal");

        if (GridView1.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);
            lblResult.Text = "Sorry, No Record(s) found!";
            Panel1.Visible = false;
        }
        else
        {



            //sql = "select top 1 ROW_NUMBER() OVER (ORDER BY SrNo ASC) AS SrNo ,CONVERT(nvarchar,CurrentDate,106) as CurrentDate,OpeningBalance,IncomeHead , ";
            //sql = sql + "    Amount,Dr,Cr ,case when EmpId ='<--Select-->' then IncomeHead else EmpName+'('+Designation+')' End as Particulars ";
            //sql = sql + "        from DayBookRecord   ";
            //sql = sql + "    where ";
            //sql = sql + "  CurrentDate between '" + Fdate1 + "'  and  '" + Tdate + "'";
            //sql = sql + " and branch='" + Session["branch"].ToString() + "'";



            //sql = "select top 1 ROW_NUMBER() OVER (ORDER BY SrNo ASC) AS SrNo ,CONVERT(nvarchar,CurrentDate,106) as CurrentDate,OpeningBalance,IncomeHead , ";
            //sql = sql + "    Amount,Dr,Cr ,case when EmpId ='<--Select-->' then IncomeHead else EmpName+'('+Designation+')' End as Particulars ";
            //sql = sql + "        from DayBookRecord   ";
            //sql = sql + "    where ";
            //sql = sql + "  CurrentDate between '" + Fdate1 + "'  and  '" + Tdate + "'";
            //sql = sql + " and  CurrentDate=(select max(currentDate) from DayBookRecord where  CurrentDate between '" + Fdate1 + "' and '" + Tdate + "'  and dr=0)";
            //sql = sql + "  and Branch='" + Session["branch"].ToString() + "'";

            //lblOpeningBal.Visible = true;
            //lblOpengBalLabel.Visible = true;
            try
            {
                Label lblClosingBal = (Label)GridView1.FooterRow.FindControl("lblClosingBal");

                lblClosingBal.Text = oo.ReturnTag(sql, "OpeningBalance");
            }
            catch (Exception) { }
            double Crsum = 0;
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lCr = (Label)GridView1.Rows[i].FindControl("Label4");
                Crsum = Crsum + Convert.ToDouble(lCr.Text);
            }

            Label ll = (Label)GridView1.FooterRow.FindControl("lblCrSum");
            ll.Text = Crsum.ToString();

            //Find the Previous balance Opening Balance   first row read
            string openbal = "";

            double dd = 0;
            Label lbldr = (Label)GridView1.Rows[0].FindControl("Label3");
            Label lblcr = (Label)GridView1.Rows[0].FindControl("Label4");
            Label lblbal = (Label)GridView1.Rows[0].FindControl("Label5");
            if (lbldr.Text == "0")
            {
                dd = Convert.ToDouble(lblbal.Text) - Convert.ToDouble(lblcr.Text);
                openbal = dd.ToString();

            }

            if (lblcr.Text == "0")
            {
                dd = Convert.ToDouble(lblbal.Text) + Convert.ToDouble(lbldr.Text);
                openbal = dd.ToString();

            }


            try
            {
                Label Label7 = (Label)GridView1.FooterRow.FindControl("Label7");

                Label7.Text = openbal;
            }
            catch (Exception) { }
            double dd1 = 0;
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label Label3 = (Label)GridView1.Rows[i].FindControl("Label3");
                dd1 = dd1 + Convert.ToDouble(Label3.Text);
            }

            try
            {
                Label lblDrTotal = (Label)GridView1.FooterRow.FindControl("lblDrTotal");

                lblDrTotal.Text = dd1.ToString();
            }
            catch (Exception) { }





        }
    }
    protected void drpFYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(drpFYY, DrpFMM, DrpFDD);
    }
    protected void DrpTYY_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DrpTYY, DrpTMM, DrpTDD);
    }
    protected void DrpFMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(drpFYY, DrpFMM, DrpFDD);
    }
    protected void DrpFDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DrpTMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DrpTYY, DrpTMM, DrpTDD);
    }
    protected void DrpTDD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "SaleParty", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel("SaleParty.xls", GridView1);
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {

        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}