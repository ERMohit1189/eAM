using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class FeeReceiptAllDuplicate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();

    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "Admin")
        {
            MasterPageFile = "~/Master/admin_root-manager.master";
        }
        if (Session["Logintype"].ToString() == "Guardian")
        {
            MasterPageFile = "~/sp/sp_root-manager.master";
        }
    }
    string SessionName = ""; int BranchCode = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        BLL.BLLInstance.LoadHeader("Receipt", header1);
        BLL.BLLInstance.LoadHeader("Receipt", header2);
        if (IsPostBack) return;
        try
        {
            if (Request.QueryString.Count > 1)
            {
                string IsIfo = Request.QueryString["IsIfo"].ToString();
                if (IsIfo == "1")
                {
                    divsepration.Visible = false;
                    Table1.Visible = false;
                    LinkButton1.Visible = false;
                    LinkButton2.Visible = false;
                }
            }

            if (Request.QueryString.Count > 0)
            {
                string[] arr = Request.QueryString["RecieptSrNo"].ToString().Split('$');
                Label1.Text = arr[0].Replace("__", "/");
                SessionName = (arr[1].ToString() == "" ? "" : arr[1]);
                BranchCode = int.Parse(arr[2].ToString() == "" ? "0" : arr[2]);
            }
            else
            {
                if (Session["Logintype"].ToString() == "Guardian")
                {
                    Response.Redirect("CompositFeeDeposit_g.aspx");
                }
                else
                {
                    Response.Redirect("CompositFeeDeposit.aspx");
                }
            }
            if (SessionName == "" || Label1.Text == "" || BranchCode == 0)
            {
                if (Session["Logintype"].ToString() == "Guardian")
                {
                    Response.Redirect("CompositFeeDeposit_g.aspx");
                }
                else
                {
                    Response.Redirect("CompositFeeDeposit.aspx");
                }
            }
            if (Session["Logintype"].ToString() == "Guardian")
            {
                LinkButton2.PostBackUrl = "~/2/CompositFeeDeposit_g.aspx";
                Table1.Visible = false;
                divsepration.Visible = false;
            }

            lblDuplicateStudent.Text = "(DUPLICATE)";
            lblDuplicateSCHOOL.Text = "(DUPLICATE)";
            LaserPrint();

        }
        catch (Exception)
        {
            Response.Redirect("fee_depositAll.aspx");
        }

    }
    public void LaserPrint()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ReceiptNo", Label1.Text.ToString().Trim()));
            param.Add(new SqlParameter("@SessionName", SessionName.ToString().Trim()));
            param.Add(new SqlParameter("@BranchCode", BranchCode.ToString().Trim()));
            DataSet ds = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("FeeReceiptAll", param);
            ///
            if (ds != null)
            {
                string simbol = "";
                string simbolsql = "select top(1) CurrencySymbols from setting";
                if (oo.ReturnTag(simbolsql, "CurrencySymbols") != "")
                {
                    simbol = "&#" + oo.ReturnTag(simbolsql, "CurrencySymbols") + ";&nbsp;";
                }
                double totaldesc = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdfeeDetails.DataSource = ds.Tables[0];
                    grdfeeDetails.DataBind();

                    grdfeeDetails1.DataSource = ds.Tables[0];
                    grdfeeDetails1.DataBind();


                    for (int i = 0; i < grdfeeDetails.Rows.Count; i++)
                    {
                        Label lblamt = (Label)grdfeeDetails.Rows[i].FindControl("Label10");
                        double amt = 0;
                        double.TryParse(lblamt.Text, out amt);
                        if (amt < 0)
                        {
                            totaldesc = totaldesc + double.Parse(lblamt.Text.Replace("-", ""));
                        }
                    }


                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    double Total = 0;
                    Total = Total + double.Parse(ds.Tables[1].Rows[0]["totalAmount"].ToString());
                    Label lblTotalAmount1 = (Label)grdfeeDetails.FooterRow.FindControl("Label18");
                    Label lblTotalAmount2 = (Label)grdfeeDetails1.FooterRow.FindControl("Label18_2");
                    lblTotalAmount1.Text = simbol + (Total - totaldesc).ToString("0.00");
                    lblTotalAmount2.Text = simbol + (Total - totaldesc).ToString("0.00");
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    decimal totalamount1 = 0;
                    decimal totalamount2 = 0;
                    Label lblTotalPaidAmount1 = (Label)grdfeeDetails.FooterRow.FindControl("Label14");
                    Label lblTotalPaidAmount2 = (Label)grdfeeDetails1.FooterRow.FindControl("Label14_2");
                    lblTotalPaidAmount1.Text = simbol + double.Parse(ds.Tables[3].Rows[0]["PaidAmount"].ToString()).ToString("0.00");
                    lblTotalPaidAmount2.Text = simbol + double.Parse(ds.Tables[3].Rows[0]["PaidAmount"].ToString()).ToString("0.00");
                    decimal value;
                    decimal.TryParse(double.Parse(ds.Tables[3].Rows[0]["PaidAmount"].ToString()).ToString("0.00"), out value);
                    totalamount1 += value;

                    Label lblAmountinwords1 = (Label)grdfeeDetails.FooterRow.FindControl("Label27");
                    lblAmountinwords1.Text = BAL.objBal.NumberToString(totalamount1);
                    decimal.TryParse(double.Parse(ds.Tables[3].Rows[0]["PaidAmount"].ToString()).ToString("0.00"), out value);
                    totalamount2 += value;

                    Label lblAmountinwords2 = (Label)grdfeeDetails1.FooterRow.FindControl("Label27_2");
                    lblAmountinwords2.Text = BAL.objBal.NumberToString(totalamount2);
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    Label lblUserName1 = (Label)grdfeeDetails.FooterRow.FindControl("lblUserName");
                    Label lblFooterDate1 = (Label)grdfeeDetails.FooterRow.FindControl("lblFooterDate");
                    lblUserName1.Text = ds.Tables[4].Rows[0]["LoginName"].ToString();
                    lblFooterDate1.Text = ds.Tables[4].Rows[0]["RecordDate"].ToString();

                    Label lblUserName2 = (Label)grdfeeDetails1.FooterRow.FindControl("lblUserName_2");
                    Label lblFooterDate2 = (Label)grdfeeDetails1.FooterRow.FindControl("lblFooterDate_2");
                    lblUserName2.Text = ds.Tables[4].Rows[0]["LoginName"].ToString();
                    lblFooterDate2.Text = ds.Tables[4].Rows[0]["RecordDate"].ToString();

                    Label lblRemark1_1 = (Label)grdfeeDetails.FooterRow.FindControl("lblRemark1");
                    Label lblRemark2_1 = (Label)grdfeeDetails.FooterRow.FindControl("lblRemark2");
                    lblRemark1_1.Text = ds.Tables[4].Rows[0]["Remark1"].ToString();
                    lblRemark2_1.Text = ds.Tables[4].Rows[0]["Remark2"].ToString();

                    Label lblRemark1_2 = (Label)grdfeeDetails1.FooterRow.FindControl("lblRemark1");
                    Label lblRemark2_2 = (Label)grdfeeDetails1.FooterRow.FindControl("lblRemark2");
                    lblRemark1_2.Text = ds.Tables[4].Rows[0]["Remark1"].ToString();
                    lblRemark2_2.Text = ds.Tables[4].Rows[0]["Remark2"].ToString();

                    HtmlTableRow tr_v_hr_1 = (HtmlTableRow)grdfeeDetails.FooterRow.FindControl("v_hr");
                    HtmlTableRow tr_v_hr_2 = (HtmlTableRow)grdfeeDetails1.FooterRow.FindControl("v_hr");

                    HtmlTableRow tr_v_r1_1 = (HtmlTableRow)grdfeeDetails.FooterRow.FindControl("v_r1");
                    HtmlTableRow tr_v_r1_2 = (HtmlTableRow)grdfeeDetails1.FooterRow.FindControl("v_r1");

                    HtmlTableRow tr_v_r2_1 = (HtmlTableRow)grdfeeDetails.FooterRow.FindControl("v_r2");
                    HtmlTableRow tr_v_r2_2 = (HtmlTableRow)grdfeeDetails1.FooterRow.FindControl("v_r2");

                    if (lblRemark1_1.Text == "" || lblRemark1_1.Text.Length == 0)
                    {
                        lblRemark1_1.Visible = false;
                        lblRemark1_2.Visible = false;
                    }
                    if (lblRemark2_1.Text == "" || lblRemark2_1.Text.Length == 0)
                    {
                        lblRemark2_1.Visible = false;
                        lblRemark2_2.Visible = false;
                    }
                    if ((lblRemark1_2.Text == "" || lblRemark1_2.Text.Length == 0) && (lblRemark2_1.Text == "" || lblRemark2_1.Text.Length == 0))
                    {
                        tr_v_hr_1.Visible = false;
                        tr_v_hr_2.Visible = false;
                    }
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    rptStudentDetails.DataSource = ds.Tables[4];
                    rptStudentDetails.DataBind();

                    rptStudentDetails1.DataSource = ds.Tables[4];
                    rptStudentDetails1.DataBind();



                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    Label lblBalanceAmount1 = (Label)grdfeeDetails.FooterRow.FindControl("Label30");
                    Label lblBalanceAmount2 = (Label)grdfeeDetails1.FooterRow.FindControl("Label30_2");
                    lblBalanceAmount1.Text = simbol + double.Parse(ds.Tables[5].Rows[0]["BalanceAmount"].ToString()).ToString("0.00");
                    lblBalanceAmount2.Text = simbol + double.Parse(ds.Tables[5].Rows[0]["BalanceAmount"].ToString()).ToString("0.00");

                }
                if (ds.Tables[7].Rows.Count > 0)
                {
                    Label Label133 = (Label)grdfeeDetails.FooterRow.FindControl("Label133");
                    Label Label13 = (Label)grdfeeDetails1.FooterRow.FindControl("Label13");

                    Label Label155 = (Label)grdfeeDetails.FooterRow.FindControl("Label155");
                    Label Label15 = (Label)grdfeeDetails1.FooterRow.FindControl("Label15");
                    Label133.Text = ds.Tables[7].Rows[0]["CancelledBy"].ToString();
                    Label13.Text = ds.Tables[7].Rows[0]["CancelledBy"].ToString();

                    Label155.Text = ds.Tables[7].Rows[0]["CancelledDate"].ToString();
                    Label15.Text = ds.Tables[7].Rows[0]["CancelledDate"].ToString();
                    if (ds.Tables[7].Rows[0]["CancelledDate"].ToString() != "")
                    {
                        grdfeeDetails1.FooterRow.FindControl("grdfeeDetailscancelleddiv1").Visible = true;
                        grdfeeDetails.FooterRow.FindControl("grdfeeDetailscancelleddiv").Visible = true;
                    }
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

