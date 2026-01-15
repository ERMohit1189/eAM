using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CancelledReceiptReport : Page
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

        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex == 0)
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
   
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "ReceiptCancelledReport", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "ReceiptCancelledReport.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "ReceiptCancelledReport", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        loadData();
    }

    protected void loadData()
    {
        var studentId = Request.Form[hfStudentId.UniqueID];
        if (string.IsNullOrEmpty(studentId))
        {
            studentId = "";
        }
        List<SqlParameter> param = new List<SqlParameter>();
        string fromdate = fromDDDates.SelectedItem.Text + "-" + fromDDMonths.SelectedItem.Text + "-" + fromDDYears.SelectedItem.Text;
        string todate = toDDDates.SelectedItem.Text + "-" + toDDMonths.SelectedItem.Text + "-" + toDDYears.SelectedItem.Text;
        var days = (DateTime.Parse(todate) - DateTime.Parse(fromdate)).TotalDays;
        if (days > 370)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Date range should not more than a financial year.", "A");
        }
        else
        {
            if (studentId != "")
            {
                param.Add(new SqlParameter("@SrNo", studentId.Trim()));
            }
            if (DrpSessionName.SelectedIndex!=0)
            {
                param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
            }
            param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
            param.Add(new SqlParameter("@fromdate", fromdate));
            param.Add(new SqlParameter("@todate", todate));
            param.Add(new SqlParameter("@Module", DrpTables.SelectedItem.Text));
            heading.Text = "Cancelled Receipt Report";
            if (DrpTables.SelectedIndex != 0)
            {
                heading.Text = heading.Text + " of " + DrpTables.SelectedItem.Text;
            }
            heading.Text = heading.Text + " from " + fromdate + " to " + todate + "";

            DataTable dt;
            DataSet ds;
            string str = "";
            ds = new DLL().Sp_SelectRecord_usingExecuteDataset("CancelledReceiptReport", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    divExport.Visible = true;
                    if (DrpSessionName.SelectedIndex != 0)
                    {
                        str = str+ "Session : " + DrpSessionName.SelectedItem.Text;
                    }
                    if (studentId != "")
                    {
                        if (DrpSessionName.SelectedIndex != 0)
                        {
                            str = str + " | Name : " + txtSearch.Text.Trim();
                        }
                        else
                        {
                            str = str + "Name : " + txtSearch.Text.Trim();
                        }
                    }
                    lblRegister.Text = str;
                    dt = ds.Tables[0];
                    abc.Visible = true;
                    GrdDisplay.DataSource = dt;
                    GrdDisplay.DataBind();
                    double amt = 0;
                    for (int i = 0; i < GrdDisplay.Rows.Count; i++)
                    {
                        Label amount = (Label)GrdDisplay.Rows[i].FindControl("amount");
                        amt = amt + double.Parse(amount.Text);
                    }
                    Label FooterTotal = (Label)GrdDisplay.FooterRow.FindControl("FooterTotal");
                    FooterTotal.Text = amt.ToString("0.00");
                }
                else
                {
                    divExport.Visible = false;
                    abc.Visible = false;
                }
            }

        }
    }
}