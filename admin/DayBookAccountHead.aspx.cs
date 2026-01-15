using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class Admin_DayBookDayBookAccountHead : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["Menu"].ConnectionString;

        con = oo.dbGet_connection();


        if (!IsPostBack)
        {
            sql = "select CommonAccountHead from CommonHeadMaster ";
            oo.FillDropDown(sql, drpAccType, "CommonAccountHead");
            Panel1.Visible = false; 

        }


    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        int i = 0;
        sql = "select  ROW_NUMBER() OVER (ORDER BY SrNo ASC) AS SrNo  ,CONVERT(nvarchar,CurrentDate,106) as CurrentDate,OpeningBalance,IncomeHead , Description,";
        sql = sql + "    Amount,Dr,Cr ,case when EmpId ='<--Select-->' then IncomeHead else EmpName+'('+Designation+')' End as Particulars ";
        sql = sql + "        from DayBookRecord   ";
        sql = sql + "    where Branch='"+Session["BranchName"].ToString() + "'";
        sql = sql + "  and IncomeHead='" + drpAccType.SelectedItem.ToString() + "'";
        sql = sql + " order by SrNo asc";

        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        Panel1.Visible = true;



        if (GridView1.Rows.Count == 0)
        {
            oo.MessageBox("Sorry, No Record(s) found!", this.Page);

            Panel1.Visible = false;
            Label2.Visible = false;
        }

        else
        {
            Label2.Visible = true;
            Label2.Text = "Account Head :"+drpAccType.SelectedItem.ToString();
            Label lblDrSum = (Label)GridView1.FooterRow.FindControl("lblDrSum");
            Label lblCrSum = (Label)GridView1.FooterRow.FindControl("lblCrSum");
            double cr = 0, dr = 0;
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label lbl4 = (Label)GridView1.Rows[i].FindControl("Label4");//Cr
                Label lbl5 = (Label)GridView1.Rows[i].FindControl("Label3");//Dr
                try
                {
                    cr = cr + Convert.ToDouble(lbl4.Text);
                }
                catch (Exception) { }
                try
                {
                    dr = dr + Convert.ToDouble(lbl5.Text);
                }
                catch (Exception) { }

            }
            lblCrSum.Text = cr.ToString();
            lblDrSum.Text = dr.ToString();
        }
       
    }
    
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel("SaleParty.xls", GridView1);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        

        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "SaleParty", divExport);
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