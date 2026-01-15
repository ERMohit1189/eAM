using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class FeeReceiptAllT2 : System.Web.UI.Page
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

        //BLL.BLLInstance.LoadHeader("Receipt", header1);
        //BLL.BLLInstance.LoadHeader("Receipt", header2);
        if (IsPostBack) return;
        try
        {
            if (Request.QueryString.Count > 1)
            {
                string IsIfo = Request.QueryString["IsIfo"].ToString();
                if (IsIfo == "1")
                {
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
                Response.Redirect("fee_depositAll.aspx");
            }
            if (SessionName == "" || Label1.Text == "" || BranchCode == 0)
            {
                Response.Redirect("fee_depositAll.aspx");
            }
            if (Session["Logintype"].ToString() == "Guardian")
            {
                Table1.Visible = false;
            }

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
            List<SqlParameter> paramss = new List<SqlParameter>();
            paramss.Add(new SqlParameter("@Id", 1));
            paramss.Add(new SqlParameter("@BranchCode", Session["BranchCode"]));
            DataSet dss = DLL.objDll.Sp_SelectRecord_usingExecuteDataset("InstituteMasterProc", paramss);
            if (dss.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = dss.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblInstitute1.Text = dt.Rows[0]["collegename"].ToString();
                    lblInstitute2.Text = dt.Rows[0]["collegename"].ToString();
                    lblAddress1.Text = dt.Rows[0]["address2"].ToString();
                    lblAddress2.Text = dt.Rows[0]["address2"].ToString();
                    lblContactno1.Text = dt.Rows[0]["phone"].ToString();
                    lblContactno2.Text = dt.Rows[0]["phone"].ToString();
                    lblEmail1.Text = dt.Rows[0]["email"].ToString();
                    lblEmail2.Text = dt.Rows[0]["email"].ToString();
                    string logo = dt.Rows[0]["collegeLogo"].ToString().Replace("~/", "../");
                    if (dt.Rows[0]["collegeLogo"].ToString() == "")
                    {
                        image1.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
                        image2.ImageUrl = "~/uploads/CollegeLogo/DefaultCollegeLogo.png";
                    }
                    else
                    {
                        image1.ImageUrl = logo;// BAL.objBal.GetImageUrl(ResolveClientUrl(dt.Rows[0]["collegeLogo"].ToString()), Request.Url.AbsoluteUri.Split('/'));
                        image2.ImageUrl = logo;// BAL.objBal.GetImageUrl(ResolveClientUrl(dt.Rows[0]["collegeLogo"].ToString()), Request.Url.AbsoluteUri.Split('/'));
                    }

                }
            }
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
                    lblTotalAmount1.Text = simbol + (Total- totaldesc).ToString("0.00");
                    lblTotalAmount2.Text = simbol + (Total- totaldesc).ToString("0.00");
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