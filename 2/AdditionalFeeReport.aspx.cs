using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdditionalFeeReport : Page
{
    Campus oo = new Campus();
    protected void Page_PreInIt(object sender, EventArgs e)
    {
        if (Session["Logintype"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (Session["Logintype"].ToString() == "SuperAdmin")
        {
            MasterPageFile = "~/50/sadminRootManager.master";
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Campus camp = new Campus(); camp.LoadLoader(loader);
        BLL.BLLInstance.LoadHeader("Report", header);
        if (!IsPostBack)
        {
            abc.Visible = false;
            oo.AddDateMonthYearDropDown(fromDDYears, fromDDMonths, fromDDDates);
            oo.AddDateMonthYearDropDown(toDDYears, toDDMonths, toDDDates);
            oo.FindCurrentDateandSetinDropDown(fromDDYears, fromDDMonths, fromDDDates);
            oo.FindCurrentDateandSetinDropDown(toDDYears, toDDMonths, toDDDates);
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlBranch.SelectedValue = Session["BranchCode"].ToString();

            string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
            var dt2 = oo.Fetchdata(sqls);
            DrpSessionName.DataSource = dt2;
            DrpSessionName.DataTextField = "SessionName";
            DrpSessionName.DataValueField = "SessionName";
            DrpSessionName.DataBind();
            DrpSessionName.Items.Insert(0, new ListItem("<--All-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = true;
            }
            else
            {
                divBranch.Visible = true;
                divSession.Visible = true;
            }
            loadUser();
        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();
        if (ddlBranch.SelectedIndex==0)
        {
            DrpSessionName.Items.Clear();
            DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
            return;
        }
        string sql = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
        var dt2 = oo.Fetchdata(sql);
        DrpSessionName.DataSource = dt2;
        DrpSessionName.DataTextField = "SessionName";
        DrpSessionName.DataValueField = "SessionName";
        DrpSessionName.DataBind();
        DrpSessionName.Items.Insert(0, new ListItem("<--Select Session-->", ""));
        DrpSessionName.SelectedIndex = (DrpSessionName.Items.Count - 1);
        if (Session["LoginType"].ToString() == "Admin")
        {
            DrpSessionName.SelectedValue = Session["SessionName"].ToString();
        }
        
    }
    protected void loadUser()
    {
        string sql = "";
        if (Session["LoginType"].ToString() == "Admin")
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + Session["BranchCode"] + "";
        }
        else
        {
            sql = "Select UserId From NewAdminInformation where BranchCode=" + ddlBranch.SelectedValue + "";
        }
        oo.FillDropDownWithOutSelect(sql, DropDownList1, "UserId");
        DropDownList1.Items.Insert(0, new ListItem("All", ""));
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = txtSearch.Text.Trim();
        }
    }
    protected void fromDDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(fromDDYears, fromDDMonths, fromDDDates);
    }
    protected void fromDDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(fromDDYears, fromDDMonths, fromDDDates);

    }
    protected void toDDYearC_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(toDDYears, toDDMonths, toDDDates);
    }
    protected void toDDMonthC_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(toDDYears, toDDMonths, toDDDates);

    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "AditionalFeeReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "AditionalFeeReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "AditionalFeeReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string sts = "";
        if (DrpSessionName.SelectedIndex != 0)
        {
            sts = sts+ "Session : " + DrpSessionName.SelectedItem.Text;
        }
            var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(txtSearch.Text))
        {
            studentId = "";
        }
        List<SqlParameter> param = new List<SqlParameter>();
        string fromdate = fromDDDates.SelectedItem.Text+"-"+ fromDDMonths.SelectedItem.Text+"-"+ fromDDYears.SelectedItem.Text;
        string todate = toDDDates.SelectedItem.Text+"-"+ toDDMonths.SelectedItem.Text+"-"+ toDDYears.SelectedItem.Text;
        param.Add(new SqlParameter("@FromDate", fromdate));
        param.Add(new SqlParameter("@ToDate", todate));
        if (DdlpaymentMode.SelectedItem.Text != "All")
        {
            if (DrpSessionName.SelectedIndex != 0)
            {
                sts = sts + " | Mode : " + DdlpaymentMode.SelectedItem.Text;
            }
            else
            {
                sts = sts + "Mode : " + DdlpaymentMode.SelectedItem.Text;
            }
        }
        if (DdlpaymentMode.SelectedItem.Text != "All")
        {
            
            param.Add(new SqlParameter("@PaymentMode", DdlpaymentMode.SelectedValue));
        }
        if (drpStatus.SelectedItem.Text!="All")
        {
            sts = sts + " | Status : " + drpStatus.SelectedItem.Text;
            param.Add(new SqlParameter("@Status", drpStatus.SelectedItem.Text));
        }
        if (DrpSessionName.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        }
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        
        heading.Text = "Additional Fee Report from " + fromdate + " to " + todate + "";
        if (studentId!="")
        {
            param.Add(new SqlParameter("@SrNo", studentId));
            string sqls = "select top(1) Name +' (S.R. No.-'+ SrNo+')' name from AllStudentRecord_UDF('', " + ddlBranch.SelectedValue + ") where srno='"+ studentId + "' order by id desc";
            string stuName = oo.ReturnTag(sqls, "name");
            sts = sts + " | For : " + stuName;
        }
        if (DropDownList1.SelectedIndex!=0)
        {
            sts = sts + " | User : " + DropDownList1.SelectedValue;
            param.Add(new SqlParameter("@LoginName", DropDownList1.SelectedValue));
        }
        lblRegister.Text = sts;
        DataTable dt;
        DataSet ds;
        ds = new DLL().Sp_SelectRecord_usingExecuteDataset("AdditionalFeeReportProc", param);
        if (ds!=null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
            {
                divExport.Visible = true;
                dt = ds.Tables[0];
                abc.Visible = true;
                GrdDisplay.DataSource = dt;
                GrdDisplay.DataBind();
                double amt = 0, fine = 0, Concession = 0, TotAmt = 0, Paid = 0;
                for (int i = 0; i < GrdDisplay.Rows.Count; i++)
                {
                    Label amount = (Label)GrdDisplay.Rows[i].FindControl("amount");
                    Label BounceCharges = (Label)GrdDisplay.Rows[i].FindControl("BounceCharges");
                    Label Exemption = (Label)GrdDisplay.Rows[i].FindControl("Exemption");
                    Label total = (Label)GrdDisplay.Rows[i].FindControl("total");
                    Label ReceivedAmount = (Label)GrdDisplay.Rows[i].FindControl("ReceivedAmount");
                    amt = amt + double.Parse(amount.Text);
                    if (BounceCharges.Text!="")
                    {
                        fine = fine + double.Parse(BounceCharges.Text);
                    }
                    if (Exemption.Text!="")
                    {
                        Concession = Concession + double.Parse(Exemption.Text);
                    }
                    if (total.Text != "")
                    {
                        TotAmt = TotAmt + double.Parse(total.Text);
                    }
                    Paid = Paid + double.Parse(ReceivedAmount.Text);
                }
                Label Footeramount = (Label)GrdDisplay.FooterRow.FindControl("Footeramount");
                Label FooterBounceCharges = (Label)GrdDisplay.FooterRow.FindControl("FooterBounceCharges");
                Label FooterExemption = (Label)GrdDisplay.FooterRow.FindControl("FooterExemption");
                Label Footertotal = (Label)GrdDisplay.FooterRow.FindControl("Footertotal");
                Label FooterReceivedAmount = (Label)GrdDisplay.FooterRow.FindControl("FooterReceivedAmount");
                Footeramount.Text = amt.ToString("0.00");
                FooterBounceCharges.Text = fine.ToString("0.00");
                FooterExemption.Text = Concession.ToString("0.00");
                Footertotal.Text= TotAmt.ToString("0.00");
                FooterReceivedAmount.Text = Paid.ToString("0.00");
            }
            else
            {
                divExport.Visible = false;
                abc.Visible = false;
            }
        }
       
    }
}