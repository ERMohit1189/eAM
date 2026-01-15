using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class sp_StudentRecipetForConvenceOnline_duplicate : Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Session["RecieptNoSession"].ToString();
        BLL.BLLInstance.LoadHeader("Receipt", header1);
        if (!IsPostBack)
        {
            try
            {
                BLL obj = new BLL();
             
                LaserPrint();
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "onclick", "var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}", true);
    }

    public void LaserPrint()
    {
        sql = "select fd.SrNo as SrNo ,sg.FirstName as FirstName,sg.MiddleName as MiddleName,fd.MOP,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,";
        sql = sql + " convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
        sql = sql + " fd.LateFeeAmount as LateFeeAmount,fd.RemainingAmount as RemainingAmount ,fd.Cocession as Cocession ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,";
        sql = sql + " fd.BusConvience as BusConvience,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section,fd.DiscountName as DiscountName,";
        sql = sql + " fd.DiscountAmount as DiscountAmount from FeeDeposite Fd ";
        sql = sql + " left join StudentGenaralDetail sg on Fd.StEnRCode=sg.StEnRCode";
        sql = sql + " left join StudentFamilyDetails SF on Fd.StEnRCode=SF.StEnRCode";
        sql = sql + " left join StudentOfficialDetails SOD on Fd.StEnRCode=SOD.StEnRCode";
        sql = sql + " where fd.RecieptSrNo='" + Label1.Text + "'";
        sql = sql + " and sg.SessionName='" + Session["SessionName"].ToString() + "' and sf.BranchCode = " + Session["BranchCode"] + " and sg.BranchCode = " + Session["BranchCode"] + " and sod.BranchCode = " + Session["BranchCode"] + " and fd.BranchCode = " + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + " and fd.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "'";
        sql = sql + " and sod.SessionName='" + Session["SessionName"].ToString() + "'";
     

            string totalfee = BAL.objBal.ReturnTag(sql, "BusConvience");
            string srno = BAL.objBal.ReturnTag(sql, "SrNo");
            DataTable dt = new DataTable();
            DataRow dr;
            DataColumn dc = new DataColumn();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("serialno", typeof(string));
                dt.Columns.Add("FeeType", typeof(string));
                dt.Columns.Add("FeePayment", typeof(string));
            }

            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "Conveyance Amount";
            dr[2] = totalfee;
            dt.Rows.Add(dr);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        try
        {

            Label Label34 = (Label)GridView1.HeaderRow.FindControl("Label34");
            Label34.Text = BAL.objBal.ReturnTag(sql, "FeeDepositeDate");

        }

        catch (Exception) { }
        try
        {
            Label Label9 = (Label)GridView1.HeaderRow.FindControl("Label9");
            Label9.Text = BAL.objBal.ReturnTag(sql, "Class");
        }
        catch (Exception) { }

        try
        {
            Label Label19 = (Label)GridView1.HeaderRow.FindControl("Label19");
            Label19.Text = BAL.objBal.ReturnTag(sql, "section");
        }
        catch (Exception) { }

        try
        {
            Label Label24 = (Label)GridView1.HeaderRow.FindControl("Label24");
            Label24.Text = BAL.objBal.ReturnTag(sql, "SrNo");
        }
        catch (Exception) { }

        try
        {
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label11.Text = BAL.objBal.ReturnTag(sql, "FeeMonth");
        }
        catch (Exception) { }


        try
        {
            Label Label8 = (Label)GridView1.HeaderRow.FindControl("Label8");
            Label8.Text = BAL.objBal.ReturnTag(sql, "FirstName");
        }
        catch (Exception) { }

        try
        {

            Label Label15 = (Label)GridView1.HeaderRow.FindControl("Label15");
            Label15.Text = BAL.objBal.ReturnTag(sql, "MiddleName");
        }
        catch (Exception) { }
        try
        {

            Label Label23 = (Label)GridView1.HeaderRow.FindControl("Label23");
            Label23.Text = BAL.objBal.ReturnTag(sql, "FatherName");

        }
        catch (Exception) { }
        try
        {

            Label Label16 = (Label)GridView1.HeaderRow.FindControl("Label16");
            Label16.Text = BAL.objBal.ReturnTag(sql, "LastName");

        }
        catch (Exception) { }

        try
        {

            Label Label7 = (Label)GridView1.HeaderRow.FindControl("Label7");
            Label7.Text = Label1.Text;
        }
        catch (Exception) { }

        try
        {

            Label Label17 = (Label)GridView1.FooterRow.FindControl("Label17");
            Label17.Text = BAL.objBal.ReturnTag(sql, "LateFeeAmount");
        }
        catch (Exception) { }

        try
        {


            Label Label30 = (Label)GridView1.FooterRow.FindControl("Label30");

            if (BAL.objBal.ReturnTag(sql, "RemainingAmount") == "")
            {
                Label30.Text = "0.00";
            }
            else
            {
                Label30.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");
            }
          
        }
        catch (Exception) { }


        try
        {

            Label Label33 = (Label)GridView1.FooterRow.FindControl("Label33");
            Label33.Text = BAL.objBal.ReturnTag(sql, "Cocession");

        }
        catch (Exception) { }

        try
        {
            Label Label18 = (Label)GridView1.FooterRow.FindControl("Label18");
            Label18.Text = BAL.objBal.ReturnTag(sql, "RecievedAmount");

        }

        catch (Exception) { }

        try
        {


            Label Label27 = (Label)GridView1.FooterRow.FindControl("Label27");
            Label27.Text = BAL.objBal.ReturnTag(sql, "AmountInWords");

        }
        catch (Exception) { }
        try
        {

            Label Label29 = (Label)GridView1.HeaderRow.FindControl("Label29");
            Label29.Text = BAL.objBal.ReturnTag(sql, "MOP");

        }
        catch (Exception) { }

        try
        {
            Label lblPreviousBalance = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");
            lblPreviousBalance.Text = Session["LastMonthRemainsAmt"].ToString();
        }
        catch (Exception) { }

        //findTotalAmount

        int i = 0;
        double ta = 0;
        try
        {
            for (i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                Label ll = (Label)GridView1.Rows[i].FindControl("Label13");
                ta = ta + Convert.ToDouble(ll.Text.Trim());

            }
            try
            {
                Label lbllateFeesAmt = (Label)GridView1.FooterRow.FindControl("Label17");
                ta = ta + Convert.ToDouble(lbllateFeesAmt.Text);
            }
            catch (Exception) { }

        }
        catch (Exception) { }
        //Find Total Amount of Previous Balance First Grid

        try
        {
            Label lblPreviousBalance1 = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");
            ta = ta + Convert.ToDouble(lblPreviousBalance1.Text);
        }


        catch (Exception) { }





        //End  Total Amount of Previous Balance First Grid

        try
        {
            Label lfTotalAmt = (Label)GridView1.FooterRow.FindControl("lblAmttotal");
            lfTotalAmt.Text = ta.ToString();
        }
        catch (Exception) { }

        //Total Amount calculation
        //Label lfTotalAmt = (Label)GridView1.FooterRow.FindControl("lblAmttotal");
        Label Label341 = (Label)GridView1.FooterRow.FindControl("Label341");
        Label concess = (Label)GridView1.FooterRow.FindControl("Label33");

        try
        {
            Label341.Text = (Convert.ToDouble(ta.ToString()) - Convert.ToDouble(concess.Text.ToString())).ToString();
        }
        catch (Exception) { }

        ta = 0;
        //findTotalAmount  for gridview2
     
        //ta = ta + busconvence;

        //Total Amount calculation


        //Payable Amount

        //Discount Add 
        try
        {
            Label lblDiscount = (Label)GridView1.FooterRow.FindControl("lblDiscount");
            Label lblDiscountValue = (Label)GridView1.FooterRow.FindControl("lblDiscountValue");
            lblDiscount.Text = BAL.objBal.ReturnTag(sql, "DiscountName");
            lblDiscountValue.Text = BAL.objBal.ReturnTag(sql, "DiscountAmount");

        }

        catch (Exception) { }

        ArrierAmt();
        if (Session["RCancel"] == null)
        {
            lblCancel.Visible = false;
        }
        else
        {
            lblCancel.Text = "(" + Session["RCancel"].ToString() + ")";
        }



    }


    public void ArrierAmt()
    {
        string amt = "";

        try
        {
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label lblSrno = (Label)GridView1.HeaderRow.FindControl("Label24");
            Label lblPrevbalance = (Label)GridView1.FooterRow.FindControl("lblPrevbalance");
            Label lblPreviousBalance = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");//ForBalanceValue



            if (Label11.Text.ToUpper() == "JULY" || Label11.Text.ToUpper() == "APRIL")
            {
                sql = "select  ArrierAmt  from FeeDeposite where srno=" + "'" + lblSrno.Text.Trim() + "' and BranchCode = " + Session["BranchCode"] + " and RecieptSrNo='" + Label1.Text + "'";
                amt = BAL.objBal.ReturnTag(sql, "ArrierAmt");

                if (amt == "")
                {
                    lblPrevbalance.Text = "Balance Amount (Previous) : ";
                }
                else
                {
                    lblPrevbalance.Text = "Balance Amount (Arrear) : ";
                    lblPreviousBalance.Text = amt;

                }
                //Balance Amount (Previous) : 
            }
            else
            {
                lblPrevbalance.Text = "Balance Amount (Previous) : ";
            }

        }
        catch (Exception) { }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveClientUrl("~/admin/fee_deposit.aspx")); 
    }
}