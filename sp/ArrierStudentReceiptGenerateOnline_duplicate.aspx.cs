using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class sp_ArrierStudentReceiptGenerateOnline_duplicate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        BLL.BLLInstance.LoadHeader("Receipt", header);
        Label1.Text = Session["RecieptNoSession"].ToString();
        if (!IsPostBack)
        {
            try
            {
                Session["FD"] = "FD";
                LaserPrint();
            }
            catch (Exception) { }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        // LaserPrint();
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    public void LaserPrint()
    {
        double busconvence = 0;
        sql = "select  fd.SrNo as SrNo ,sg.FirstName as FirstName,sg.MiddleName as MiddleName,Fd.MOP,SOD.Medium as Medium ,sg.LastName as LastName,SF.FatherName as FatherName,convert(nvarchar,fd.FeeDepositeDate,106) as FeeDepositeDate,sod.TypeOFAdmision as TypeOfAdmission ,";
        sql = sql + "  fd.LateFeeAmount as LateFeeAmount,Convert(nvarchar(25),fd.RecordDate,100) RecordDate,fd.LoginName,fd.RemainingAmount as RemainingAmount ,sod.Branch,cm.Id as Classid,sod.MODForFeeDeposit,fd.Cocession as Cocession ,fd.RecievedAmount as RecievedAmount,fd.AmountInWords,fd.BusConvience as BusConvience,fd.FeeMonth as FeeMonth,fd.Class as Class,fd.section as section,fd.DiscountName as DiscountName,fd.DiscountAmount as DiscountAmount,bm.BranchName from FeeDeposite Fd ";
        sql = sql + "  left join StudentGenaralDetail sg on Fd.srno=sg.srno";
        sql = sql + "  left join StudentFamilyDetails SF on Fd.srno=SF.srno";
        sql = sql + "  left join StudentOfficialDetails SOD on Fd.srno=SOD.srno";
        sql = sql + "  left join classMaster cm on cm.id=sod.AdmissionForClassId and cm.SessionName=sod.SessionName";
        sql = sql + "  left join BranchMaster bm on bm.Classid=sod.AdmissionForClassId and bm.SessionName=sod.SessionName  and bm.isdisplay='true'";
        sql = sql + "  where fd.RecieptSrNo='" + Label1.Text + "'";
        sql = sql + "  and sg.SessionName='" + Session["SessionName"].ToString() + "' and bm.BranchCode=" + Session["BranchCode"] + " and sod.BranchCode=" + Session["BranchCode"] + " and cm.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"] + " and sf.BranchCode=" + Session["BranchCode"] + " and sg.BranchCode=" + Session["BranchCode"].ToString() + "";
        sql = sql + "  and fd.SessionName='" + Session["SessionName"].ToString() + "' and sf.SessionName='" + Session["SessionName"].ToString() + "'  and sod.SessionName='" + Session["SessionName"].ToString() + "'";


        //sql1 = " select ROW_NUMBER() OVER (ORDER BY Id ASC) AS serialno,Id,fa.Month, fa.FeeParticular,fa.Class,fa.FeeType,fa.FeePayment,FM.Medium,Fa.CardType ,fm.NoOfmonths as NoOfmonths from FeeAllotedForClassWise fa ";
        //sql1 = sql1 + " left join feemaster  fm on fa.Medium=fm.medium  and fa.FeeName=fm.FeeName  ";
        //sql1 = sql1 + " where fa.Class='" + oo.ReturnTag(sql, "Class") + "'  and fa.SessionName='" + Session["SessionName"].ToString() + "'  and fa.BranchCode=" + Session["BranchCode"].ToString() + "";
        //sql1 = sql1 + "and fa.CardType='" + Session["CardType"].ToString() + "' and   fa.AdmissionType='" + oo.ReturnTag(sql, "TypeOfAdmission") + "'  and fa.Medium='" + oo.ReturnTag(sql, "Medium") + "'";
        //sql1 = sql1 + " and fm.SessionName='" + Session["SessionName"].ToString() + "'";



        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
        Label Label25 = (Label)GridView1.Rows[0].FindControl("Label25");
        Label Label12 = (Label)GridView1.Rows[0].FindControl("Label12");
        Label Label13 = (Label)GridView1.Rows[0].FindControl("Label13");
        Label25.Text = "1";
        Label12.Text = "Arrier";
        Label13.Text = oo.ReturnTag(sql, "RecievedAmount").ToString();

        try
        {
            Label lblFooterDate = (Label)GridView1.FooterRow.FindControl("lblFooterDate");
            Label lblUserName = (Label)GridView1.FooterRow.FindControl("lblUserName");

            lblFooterDate.Text = oo.ReturnTag(sql, "RecordDate");
            lblUserName.Text = oo.ReturnTag(sql, "LoginName");

        }
        catch (Exception) { }

        try
        {

            Label Label34 = (Label)GridView1.HeaderRow.FindControl("Label34");
            Label34.Text = oo.ReturnTag(sql, "FeeDepositeDate");

        }

        catch (Exception) { }
        try
        {
            Label Label9 = (Label)GridView1.HeaderRow.FindControl("Label9");
            Label9.Text = oo.ReturnTag(sql, "Class");

            Label lblBranch = (Label)GridView1.HeaderRow.FindControl("lblBranch");
            lblBranch.Text = oo.ReturnTag(sql, "BranchName");
        }
        catch (Exception) { }

        try
        {
            Label Label19 = (Label)GridView1.HeaderRow.FindControl("Label19");
            Label19.Text = oo.ReturnTag(sql, "section");
        }
        catch (Exception) { }

        try
        {
            Label Label24 = (Label)GridView1.HeaderRow.FindControl("Label24");
            Label24.Text = oo.ReturnTag(sql, "SrNo");
        }
        catch (Exception) { }

        try
        {
            Label Label11 = (Label)GridView1.HeaderRow.FindControl("Label11");
            Label11.Text = oo.ReturnTag(sql, "FeeMonth");
        }
        catch (Exception) { }


        try
        {
            Label Label8 = (Label)GridView1.HeaderRow.FindControl("Label8");
            Label8.Text = oo.ReturnTag(sql, "FirstName");
        }
        catch (Exception) { }

        try
        {

            Label Label15 = (Label)GridView1.HeaderRow.FindControl("Label15");
            Label15.Text = oo.ReturnTag(sql, "MiddleName");

        }
        catch (Exception) { }
        try
        {

            Label Label23 = (Label)GridView1.HeaderRow.FindControl("Label23");
            Label23.Text = oo.ReturnTag(sql, "FatherName");

        }
        catch (Exception) { }
        try
        {

            Label Label16 = (Label)GridView1.HeaderRow.FindControl("Label16");
            Label16.Text = oo.ReturnTag(sql, "LastName");

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
            Label17.Text = oo.ReturnTag(sql, "LateFeeAmount");
        }
        catch (Exception) { }


        try
        {

            Label lblBusConvence = (Label)GridView1.FooterRow.FindControl("lblBusConvence");
            if (oo.ReturnTag(sql, "BusConvience") == "")
            {
                lblBusConvence.Text = "0.00";
                busconvence = 0;
            }
            else
            {
                lblBusConvence.Text = oo.ReturnTag(sql, "BusConvience");
                busconvence = Convert.ToDouble(lblBusConvence.Text);
            }
        }
        catch (Exception) { }


        try
        {


            Label Label30 = (Label)GridView1.FooterRow.FindControl("Label30");

            if (oo.ReturnTag(sql, "RemainingAmount") == "")
            {
                Label30.Text = "0.00";
            }
            else
            {
                Label30.Text = oo.ReturnTag(sql, "RemainingAmount");
            }
        }
        catch (Exception) { }


        try
        {

            Label Label33 = (Label)GridView1.FooterRow.FindControl("Label33");
            Label33.Text = oo.ReturnTag(sql, "Cocession");

        }
        catch (Exception) { }

        try
        {
            Label Label18 = (Label)GridView1.FooterRow.FindControl("Label18");
            Label18.Text = oo.ReturnTag(sql, "RecievedAmount");

        }

        catch (Exception) { }

        try
        {

            Label Label27 = (Label)GridView1.FooterRow.FindControl("Label27");
            Label27.Text = oo.ReturnTag(sql, "AmountInWords");
        }
        catch (Exception) { }
        try
        {

            Label Label29 = (Label)GridView1.HeaderRow.FindControl("Label29");
            Label29.Text = oo.ReturnTag(sql, "Medium");

        }
        catch (Exception) { }

        try
        {
            Label lblPreviousBalance = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");
            lblPreviousBalance.Text = Session["LastMonthRemainsAmt"].ToString();
        }
        catch (Exception) { }

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
       
        try
        {
            Label lblPreviousBalance1 = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");
            ta = ta + Convert.ToDouble(lblPreviousBalance1.Text);
        }


        catch (Exception) { }

        ta = ta + busconvence;
        try
        {
            Label lfTotalAmt = (Label)GridView1.FooterRow.FindControl("lblAmttotal");
            lfTotalAmt.Text = ta.ToString();
        }
        catch (Exception) { }
        //Total Amount calculation


        ta = 0;
        //findTotalAmount  for gridview2

        Label Label341 = (Label)GridView1.FooterRow.FindControl("Label341");
        Label concess = (Label)GridView1.FooterRow.FindControl("Label33");

        try
        {
            Label341.Text = Convert.ToDouble(concess.Text.ToString()).ToString();
        }
        catch (Exception) { }

        //Discount Add 
        try
        {
            Label lblDiscount = (Label)GridView1.FooterRow.FindControl("lblDiscount");
            Label lblDiscountValue = (Label)GridView1.FooterRow.FindControl("lblDiscountValue");
            lblDiscount.Text = oo.ReturnTag(sql, "DiscountName");
            lblDiscountValue.Text = oo.ReturnTag(sql, "DiscountAmount");

        }

        catch (Exception) { }

        string Namt = "";
        try
        {
            Namt = Session["NextDueAmt"].ToString();
            Label lblNexDueAmt = (Label)GridView1.FooterRow.FindControl("lblNexDueAmt");
            lblNexDueAmt.Text = "Next Due Amount :" + Namt;
            Session["NextDueAmt"] = "";
        }
        catch (Exception) { }


        ArrierAmt();
        if ((string)Session["RCancel"] == string.Empty)
        {
            lblCancel.Visible = false;
        }
        else
        {
            //lblCancel.Text = "(" + Session["RCancel"].ToString() + ")";
            //lblCancelStudent.Text = "(" + Session["RCancel"].ToString() + ")";
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
            Label Label13 = (Label)GridView1.Rows[0].FindControl("Label13");
            amt = Label13.Text;
            Label lblPreviousBalance = (Label)GridView1.FooterRow.FindControl("lblPreviousBalance");//ForBalanceValue

            lblPrevbalance.Text = "Balance Amount (Arrear) : ";
            lblPreviousBalance.Text = amt;


        }
        catch (Exception) { }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect(ResolveClientUrl("~/admin/fee_deposit.aspx")); 
    }

}