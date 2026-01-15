using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System;
using System.Net.Mail;
using c4SmsNew;
using System.IO;
using System.Collections.Generic;
using System.Web.UI;

public partial class PettyCash : Page
{
    SqlConnection con = new SqlConnection();
    Campus oo = new Campus();
    string sql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sql1 = "";
        con = oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);

        if (!IsPostBack)
        {
            ddlUser.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select Branch-->", ""));
            oo.AddDateMonthYearDropDown(DDYears, DDMonths, DDDates);
            oo.AddDateMonthYearDropDown(DDChequeYears, DDChequeMonths, DDChequeDates);
            oo.FindCurrentDateandSetinDropDown(DDYears, DDMonths, DDDates);
            oo.FindCurrentDateandSetinDropDown(DDChequeYears, DDChequeMonths, DDChequeDates);

            loadData();
        }
    }
    protected void DDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDYears, DDMonths, DDDates);
        loadData();
    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDYears, DDMonths, DDDates);
        loadData();
    }
    protected void DDDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void DDChequeYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(DDChequeYears, DDChequeMonths, DDChequeDates);
    }
    protected void DDChequeMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(DDChequeYears, DDChequeMonths, DDChequeDates);

    }
    protected void ddluserType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        LoadUser();
    }
    protected void LoadUser()
    {
        if (ddluserType.SelectedIndex == 1)
        {
            sql = "select name+' ('+l.LoginName+')' LoginNames, l.LoginName from LoginTab l left join NewAdminInformation na on na.UserId=l.LoginName where LoginTypeId='" + ddluserType.SelectedValue.ToString() + "' and BranchId=" + ddlBranch.SelectedValue + " and (IsActive is NULL or IsActive=1) ";
            if (ddluserType.SelectedIndex == 1)
            {
                sql = sql + " order by CASE WHEN ISNUMERIC(Right(LoginName,3))=1 THEN Right(LoginName,3) ELSE 0 END";
            }
            oo.FillDropDown_withValue(sql, ddlUser, "LoginNames", "LoginName");
            ddlUser.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
        if (ddluserType.SelectedIndex == 2)
        {
            sql = "select (Name+' ('+Ecode+')') LoginName, Ecode, EmpId from GetAllStaffRecords_UDF(" + ddlBranch.SelectedValue + ") asr inner join LoginTab lt on lt.LoginName=asr.Ecode and asr.BranchCode=lt.BranchId where lt.LoginTypeId='" + ddluserType.SelectedValue.ToString() + "' and lt.BranchId=" + ddlBranch.SelectedValue + " and (lt.IsActive is NULL or lt.IsActive=1) ";
            oo.FillDropDown_withValue(sql, ddlUser, "LoginName", "Ecode");
            ddlUser.Items.Insert(0, new ListItem("<--Select-->", ""));
        }
    }
    public void loadData()
    {
        string date = DDDates.SelectedValue + "-" + DDMonths.SelectedValue + "-" + DDYears.SelectedValue;
        sql = "select pc.*, (EFatherName+' '+EMiddleName+' '+ELastName) name, bt.BranchName from AccPettycash pc ";
        sql = sql + " left join NewAdminInformation info on info.UserId = pc.UserName and info.BranchCode = pc.BranchCode ";
        sql = sql + " left join EmpGeneralDetail eg on eg.Ecode = pc.UserName and eg.BranchCode = pc.BranchCode ";
        sql = sql + " inner join BranchTab bt on bt.BranchId = pc.BranchCode where convert(date, RecordDate)= convert(date, getdate()) ";
        
        if (ddlBranch.SelectedIndex != 0)
        {
            sql = sql + " and pc.BranchCode=" + ddlBranch.SelectedValue + " ";
            
            if (ddlUser.SelectedIndex != 0)
            {
                sql = sql + " and pc.username= '" + ddlUser.Text + "' ";
            }
        }
        sql = sql + "  order by pc.date desc ";
        GridView1.DataSource = oo.GridFill(sql);
        GridView1.DataBind();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
        loadData();
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        divOtherTools.Visible = true;
        if (ddlMode.SelectedValue == "Cash")
        {
            divOtherTools.Visible = false;
        }
        if (ddlMode.SelectedValue == "Cheque" || ddlMode.SelectedValue == "DD")
        {
            lblChqDate.Text = "Instrument Date";
            lblChqNo.Text = "Instrument No.";
            lblBankName.Text = "Issuer";
        }
        else if (ddlMode.SelectedValue == "Online Transfer" || ddlMode.SelectedValue == "Other")
        {
            lblChqDate.Text = "Transaction Date";
            lblChqNo.Text = "Ref. No.";
        }
        else if (ddlMode.SelectedValue == "Card")
        {
            lblChqDate.Text = "Transaction Date";
            lblChqNo.Text = "Card No.";
            lblBankName.Text = "Issuer";
        }
        else
        {
            lblChqDate.Text = ddlMode.SelectedValue + " Date";
            lblChqNo.Text = ddlMode.SelectedValue + " No.";
        }
        if (ddlMode.SelectedValue == "Other")
        {
            lblBankName.Text = "Reference Name";
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        sql = "select HeadName from AccHeadMaster where HeadType='Income' and HeadCategory='General' and HeadName='Petty Cash' and BranchCode="+ ddlBranch.SelectedValue + "";
        if (!oo.Duplicate(sql))
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Please create Petty Cash under Income Head Type from Admin", "A");
            return;
        }
        string date = DDDates.SelectedValue + "-" + DDMonths.SelectedValue + "-" + DDYears.SelectedValue;
        string Chequedate = DDChequeDates.SelectedValue + "-" + DDChequeMonths.SelectedValue + "-" + DDChequeYears.SelectedValue;

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "AccPettycashProc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@UserName", ddlUser.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@name", ddlUser.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@date", date.Trim());
            cmd.Parameters.AddWithValue("@HeadType", "Expense");
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
            cmd.Parameters.AddWithValue("@PaymentMode", ddlMode.SelectedValue);
            cmd.Parameters.AddWithValue("@ChequeDate", Chequedate.Trim());
            cmd.Parameters.AddWithValue("@ChequeNo", txtChqNo.Text.Trim());
            cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", drpStatus.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@Remark", txtARemark.Text.Trim());
            cmd.Parameters.AddWithValue("@BranchCode", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@LoginName", Session["LoginName"].ToString());
            cmd.Parameters.AddWithValue("@Action", "insert");
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Submitted successfully.", "S");
                loadData();
                Reset();
            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void Reset()
    {
        ddluserType.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlMode.SelectedIndex = 0;
        ddlUser.Items.Clear();
        ddlUser.Items.Insert(0, new ListItem("<--Select-->", ""));
        txtAmount.Text = "";
        txtChqNo.Text = "";
        txtBankName.Text = "NA";
        txtARemark.Text = "";
    }
}