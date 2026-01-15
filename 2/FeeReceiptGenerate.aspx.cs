using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class StudentReceiptGenerate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null) { Response.Redirect("~/default.aspx"); }
        // ReSharper disable once PossibleNullReferenceException
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (Session["Logintype"].ToString())
        {
            case "Admin":
                MasterPageFile = "~/Master/admin_root-manager.master";
                break;
            case "Guardian":
                MasterPageFile = "~/sp/sp_root-manager.master";
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        BLL.BLLInstance.LoadHeader("Receipt", header1);
        BLL.BLLInstance.LoadHeader("Receipt", header2);
        if (IsPostBack) return;
        try
        {
            if(Request.QueryString["RecieptSrNo"]!=null)
            {
                if(Request.QueryString["RecieptSrNo"]!=string.Empty)
                {
                    Label1.Text = Request.QueryString["RecieptSrNo"].ToString().Replace("__", "/");
                    Session["isDuplicate"] = "Yes";
                }
                else
                {
                    if (Session["RecieptSrNo"] == null)
                    {
                        Response.Redirect("fee_deposit.aspx");
                    }
                    Label1.Text = Session["RecieptSrNo"].ToString();
                }
            }
            else
            {
                if (Session["RecieptSrNo"] == null)
                {
                    Response.Redirect("fee_deposit.aspx");
                }
                Label1.Text = Session["RecieptSrNo"].ToString();
            }

            if (Request.QueryString["rdto"] != null)
            {
                if (Request.QueryString["rdto"] != string.Empty && Request.QueryString["rdto"] == "tf")
                {
                    LinkButton2.PostBackUrl = "~/3/TransportFeeDeposit.aspx";
                }
            }

            if (Session["Logintype"].ToString() == "Guardian")
            {
                Table1.Visible = false;
                divsepration.Visible = false;
                lblSTUDENTCOPY.Text = "";
            }
          
            // ReSharper disable once PossibleNullReferenceException

            if(Session["isDuplicate"].ToString()=="Yes")
            {
                lblDuplicateStudent.Text = "(DUPLICATE)";
                lblDuplicateSCHOOL.Text = "(DUPLICATE)";
            }
            else
            {
                lblDuplicateStudent.Text = "";
                lblDuplicateSCHOOL.Text = "";
            }
            LaserPrint();

        }
        catch (Exception)
        {
            // ignored
        }

    }
    public void LaserPrint()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@RecieptSrNo", Label1.Text.ToString()));
            param.Add(new SqlParameter("@SessionName", Session["SessionName"].ToString()));
            param.Add(new SqlParameter("@BranchCode", Session["BranchCode"].ToString()));

            DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("USP_Get_FeeReceipt", param);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptStudentDetails.DataSource = ds.Tables[0];
                    rptStudentDetails.DataBind();

                    rptStudentDetails1.DataSource = ds.Tables[0];
                    rptStudentDetails1.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    grdfeeDetails.DataSource = ds.Tables[1];
                    grdfeeDetails.DataBind();

                    grdfeeDetails1.DataSource = ds.Tables[1];
                    grdfeeDetails1.DataBind();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    decimal totalamount1 = 0;
                    decimal totalamount2 = 0;

                    Label lblTotalAmount1 = (Label) grdfeeDetails.FooterRow.FindControl("Label18");
                    lblTotalAmount1.Text = ds.Tables[2].Rows[0]["TotalAmount"].ToString();

                    Label lblBalanceAmount1 = (Label) grdfeeDetails.FooterRow.FindControl("Label30");
                    lblBalanceAmount1.Text = ds.Tables[2].Rows[0]["BalanceAmount"].ToString();

                    Label lblTotalPaidAmount1 = (Label) grdfeeDetails.FooterRow.FindControl("Label14");
                    lblTotalPaidAmount1.Text = ds.Tables[2].Rows[0]["TotalPaidAmount"].ToString();

                    decimal value;
                    decimal.TryParse(lblTotalPaidAmount1.Text, out value);
                    totalamount1 += value;

                    Label lblAmountinwords1 = (Label) grdfeeDetails.FooterRow.FindControl("Label27");
                    lblAmountinwords1.Text = BAL.objBal.NumberToString(Convert.ToInt64(totalamount1));

                    Label lblNexDueAmt = (Label)grdfeeDetails.FooterRow.FindControl("lblNexDueAmt");
                    lblNexDueAmt.Text = ds.Tables[0].Rows[0]["NextDueAmount"].ToString();

                    Label lblUserName = (Label)grdfeeDetails.FooterRow.FindControl("lblUserName");
                    lblUserName.Text = ds.Tables[3].Rows[0]["LoginName"].ToString();

                    Label lblFooterDate = (Label)grdfeeDetails.FooterRow.FindControl("lblFooterDate");
                    lblFooterDate.Text = ds.Tables[3].Rows[0]["RecordDate"].ToString();

                    //=============
                    Label lblTotalAmount2 = (Label) grdfeeDetails1.FooterRow.FindControl("Label18_2");
                    lblTotalAmount2.Text = ds.Tables[2].Rows[0]["TotalAmount"].ToString();

                    Label lblBalanceAmount2 = (Label) grdfeeDetails1.FooterRow.FindControl("Label30_2");
                    lblBalanceAmount2.Text = ds.Tables[2].Rows[0]["BalanceAmount"].ToString();

                    Label lblTotalPaidAmount2 = (Label) grdfeeDetails1.FooterRow.FindControl("Label14_2");
                    lblTotalPaidAmount2.Text = ds.Tables[2].Rows[0]["TotalPaidAmount"].ToString();

                    decimal.TryParse(lblTotalPaidAmount2.Text, out value);
                    totalamount2 += value;

                    Label lblAmountinwords2 = (Label) grdfeeDetails1.FooterRow.FindControl("Label27_2");
                    lblAmountinwords2.Text = BAL.objBal.NumberToString(Convert.ToInt64(totalamount2));

                    Label lblUserName1 = (Label)grdfeeDetails1.FooterRow.FindControl("lblUserName_2");
                    lblUserName1.Text = ds.Tables[3].Rows[0]["LoginName"].ToString();
                    
                    Label lblFooterDate1 = (Label)grdfeeDetails1.FooterRow.FindControl("lblFooterDate_2");
                    lblFooterDate1.Text = ds.Tables[3].Rows[0]["RecordDate"].ToString();
                    
                    Label lblNexDueAmt1 = (Label) grdfeeDetails1.FooterRow.FindControl("lblNexDueAmt_2");
                    lblNexDueAmt1.Text = ds.Tables[0].Rows[0]["NextDueAmount"].ToString();

                }

            }
        }
        catch (Exception ex)
        {
            //ignored
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
}

