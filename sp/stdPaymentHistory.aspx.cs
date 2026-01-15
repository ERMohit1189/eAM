using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System;

public partial class admin_AllStudentReceiptMonthDate : Page
{
    string sql = "";
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con = BAL.objBal.dbGet_connection();
        if (!IsPostBack)
        {
            loadPreviousHistory();
        }
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        string ss = "";

        string Month = "";

        LinkButton chk = (LinkButton)sender;
        ss = chk.ToolTip.ToString();
        lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Label44.Visible = true; Panel7.Visible = true;

        sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
        sql = sql + " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite";
        sql = sql + " where srno='" +Session["Srno"].ToString()+ "'  and RecieptSrNo='" + chk.Text + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

        if (BAL.objBal.ReturnTag(sql, "BalanceMode").Trim() == "BalancePaid")
        {
            int i;
            string mon = "";

            sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            sql = sql + " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount from FeeDeposite";
            sql = sql + " where srno='" + Session["Srno"].ToString() + "'  and RecieptSrNo='" + chk.Text + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            if (BAL.objBal.ReturnTag(sql, "BalanceMode").Trim() == "BalancePaid")
            {

                Label32.Text = BAL.objBal.ReturnTag(sql, "CurrentAmount");
                Label34.Text = BAL.objBal.ReturnTag(sql, "RecievedAmount");
                Label35.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");

                lblID0.Text = chk.Text;
                if (BAL.objBal.ReturnTag(sql, "Cancel").Trim() == "Y")
                {
                    lblcancel0.Text = "CANCELLED";
                    Session["RCancel"] = "CANCELLED";
                }
                else
                {
                    lblcancel0.Text = "";
                    Session["RCancel"] = "";
                }
               
                int k = Convert.ToInt32(ss) - 1;

                sql = "   select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount, ";
                sql = sql + " Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite   ";
                sql = sql + " where  srno='" +Session["Srno"].ToString()+ "'  and id<=" + k.ToString();
                sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                sql = sql + " order by id  desc ";

                try
                {
                    Label36.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");

                }
                catch (Exception) { }

                try
                {

                    for (i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {

                        LinkButton lnkRecieptNo = (LinkButton)GridView1.Rows[i].FindControl("LinkButton4");
                        if (lnkRecieptNo.Text == chk.Text)
                        {
                            Label lblmonth = (Label)GridView1.Rows[i].FindControl("Label20");

                            mon = lblmonth.Text;
                        }
                    }

                    sql = "select  SUM(RecievedAmount  ) as TotalRecivedAmt from FeeDeposite  where srno='" + Session["Srno"].ToString() + "'  and   ";
                    sql = sql + "  FeeMonth='" + mon + "' and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
                    Label Label33 = new Label();
                    Label33.Text = BAL.objBal.ReturnTag(sql, "TotalRecivedAmt");
                }
                catch (Exception) { }


                try
                {


                    sql = "     select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount, ";
                    sql = sql + " Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite where  srno='" + Session["Srno"].ToString() + "'";
                    sql = sql + " and  FeeMonth='" + mon + "'  and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

                    Label37.Text = BAL.objBal.ReturnTag(sql, "CurrentAmount");
                }
                catch (Exception) { }
                Panel5_ModalPopupExtender.Show();
            }
        }
        else
        {

            sql = "select id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel, BusConvience,  FeeMonth,TotalFeeAmount,";
            sql = sql + " Cocession,RecievedAmount,CurrentAmount,LateFeeAmount,RemainingAmount,Remark,Class,Section,BalanceMode,DiscountName,DiscountAmount,NextDueAmount";
            sql = sql + " from FeeDeposite  where srno='" +Session["Srno"].ToString()+ "'  and RecieptSrNo='" + chk.Text + "'";
            sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";

            lblTotalFee.Text = BAL.objBal.ReturnTag(sql, "TotalFeeAmount");
            lblConcession.Text = BAL.objBal.ReturnTag(sql, "Cocession");
            lblPaidAmount.Text = BAL.objBal.ReturnTag(sql, "RecievedAmount");
            lblBalace.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");
            lblRemark.Text = BAL.objBal.ReturnTag(sql, "Remark");
            lblLate.Text = BAL.objBal.ReturnTag(sql, "LateFeeAmount");
            Label31.Text = BAL.objBal.ReturnTag(sql, "BusConvience");
            Month = BAL.objBal.ReturnTag(sql, "FeeMonth");

            Session["NextDueAmt"] = BAL.objBal.ReturnTag(sql, "NextDueAmount");
            try
            {
                lblDiscountPanel.Visible = true; lblDiscountPanelValue.Visible = true; Label44.Visible = true; Panel7.Visible = true;
                lblDiscountPanel.Text = BAL.objBal.ReturnTag(sql, "DiscountName");
                lblDiscountPanelValue.Text = BAL.objBal.ReturnTag(sql, "DiscountAmount");
                if (lblDiscountPanel.Text != "")
                {
                    Panel7.Visible = true;
                }
                else
                {
                    Panel7.Visible = false;
                }
            }
            catch (Exception) { lblDiscountPanel.Visible = false; lblDiscountPanelValue.Visible = false; Label44.Visible = false; Panel7.Visible = false; }
            if (BAL.objBal.ReturnTag(sql, "BusConvience") == "")
            {
                Label31.Text = "0";
            }
            else
            {
                Label31.Text = BAL.objBal.ReturnTag(sql, "BusConvience");
            }
            lblID.Text = chk.Text;
            if (BAL.objBal.ReturnTag(sql, "Cancel").Trim() == "Y")
            {
                lblcancel.Text = "CANCELLED";
                Session["RCancel"] = "CANCELLED";
            }
            else
            {
                lblcancel.Text = "";
                Session["RCancel"] = "";
            }

            int k = Convert.ToInt32(ss) - 1;

            sql = "select top 1 id,StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as FeeDepositeDate,RecieptSrNo,Cancel,   FeeMonth,TotalFeeAmount,";
            sql = sql + " Cocession,RecievedAmount,CurrentAmount,RemainingAmount,Remark,Class,Section from FeeDeposite   ";
            sql = sql + " where  srno='" +Session["Srno"].ToString()+ "'  and id<=" + k.ToString();
            sql = sql + " and Cancel is null and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
            sql = sql + " order by RecieptSrNo  desc ";

            try
            {
                Label25.Text = BAL.objBal.ReturnTag(sql, "RemainingAmount");
                double a = Convert.ToDouble(Label25.Text);
            }
            catch (Exception) { Label25.Text = "0"; con.Close(); }
            GridViewRow currentrow = ((GridViewRow)((Control)sender).Parent.Parent);
            Label Label20 = (Label)currentrow.FindControl("Label20");
            Session["Installment"] = Label20.Text;
            Panel4_ModalPopupExtender.Show();

        }
    }
    private void loadPreviousHistory()
    {
        int i = 0; double sum = 0;

        sql = "select ROW_NUMBER() OVER (ORDER BY Id ASC) AS SrNo,Id, StEnRCode,SrNo,convert(nvarchar,FeeDepositeDate,106) as ";
        sql = sql + " FeeDepositeDate,RecieptSrNo,FeeMonth,RecievedAmount,RemainingAmount,Class,MOP,Section,";
        sql = sql + " case when Cancel='Y' then 'Cancelled' else Status end  as Cancel from FeeDeposite  where srno='" + Session["Srno"].ToString() + "'";
        sql = sql + " and SessionName='" + Session["SessionName"].ToString() + "' and BranchCode=" + Session["BranchCode"].ToString() + "";
        GridView1.DataSource = BAL.objBal.GridFill(sql);
        GridView1.DataBind();

        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {

            Label lblSta = (Label)GridView1.Rows[i].FindControl("Label29");
            Label lblpaidAmt = (Label)GridView1.Rows[i].FindControl("Label21");


            if (lblSta.Text.Trim() == "Paid")
            {
                try
                {
                    sum = sum + Convert.ToDouble(lblpaidAmt.Text);
                }
                catch (Exception) { }
            }


        }

        if (GridView1.Rows.Count > 0)
        {
            Label FooterPaidAmt = (Label)GridView1.FooterRow.FindControl("Label39");
            FooterPaidAmt.Text = sum.ToString();
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        Session["RecieptNoSession"] = lblID.Text;
        Session["LastMonthRemainsAmt"] = Label25.Text;
        Response.Redirect("StudentReceiptGenerateOnline_duplicate.aspx?print=1");
    }
}