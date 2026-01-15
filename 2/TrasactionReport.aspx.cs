using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrasactionReport : Page
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
    
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void LoadReport()
    {
        divExport.Visible = true;
        abc.Visible = true;
        string fromdate = fromDDDates.SelectedItem.Text + "-" + fromDDMonths.SelectedItem.Text + "-" + fromDDYears.SelectedItem.Text;
        string todate = toDDDates.SelectedItem.Text + "-" + toDDMonths.SelectedItem.Text + "-" + toDDYears.SelectedItem.Text;
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@FromDate", fromdate));
        param.Add(new SqlParameter("@ToDate", todate));
        if (DdlpaymentMode.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@PaymentMode", DdlpaymentMode.SelectedValue));
        }
        if (drpStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Status", drpStatus.SelectedValue));
        }
        if (DrpSessionName.SelectedIndex!=0)
        {
            param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        }
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_TransactionReport", param);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (GridView1.Rows.Count==0)
        {
            divExport.Visible = false;
            abc.Visible = false;
        }
        heading.Text = "Trasaction Report";
        string sqs = "select format(getdate(), 'dd-MMM-yyyy hh:mm:ss tt') getdate";
        lblRegister.Text = "Date : " + oo.ReturnTag(sqs, "getdate") + (DdlpaymentMode.SelectedIndex == 0 ? "" : " | Mode : " + DdlpaymentMode.SelectedItem.Text) + (drpStatus.SelectedIndex == 0 ? "" : " | Status : " + drpStatus.SelectedItem.Text);
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
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToWord(Response, "TransactionReport.doc", divExport);
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            oo.ExportToExcel("TransactionReport.xls", GridView1);
        }
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

}