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

public partial class PettyCashReport : Page
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
            oo.AddDateMonthYearDropDown(FromDDYears, FromDDMonths, FromDDDates);
            oo.AddDateMonthYearDropDown(ToDDCYears, ToDDMonths, ToDDDates);
            oo.FindCurrentDateandSetinDropDown(FromDDYears, FromDDMonths, FromDDDates);
            oo.FindCurrentDateandSetinDropDown(ToDDCYears, ToDDMonths, ToDDDates);

            loadData();
        }
    }
    protected void FromDDYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(FromDDYears, FromDDMonths, FromDDDates);
    }
    protected void FromDDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(FromDDYears, FromDDMonths, FromDDDates);
    }
    
    protected void ToDDCYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.YearDropDown(ToDDCYears, ToDDMonths, ToDDDates);
    }
    protected void ToDDMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        oo.MonthDropDown(ToDDCYears, ToDDMonths, ToDDDates);

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
        string date = FromDDDates.SelectedValue + "-" + FromDDMonths.SelectedValue + "-" + FromDDYears.SelectedValue;
        string Chequedate = ToDDDates.SelectedValue + "-" + ToDDMonths.SelectedValue + "-" + ToDDCYears.SelectedValue;
        sql = "select pc.*, (EFatherName+' '+EMiddleName+' '+ELastName) name, bt.BranchName from AccPettycash pc ";
        sql = sql + " left join NewAdminInformation info on info.UserId = pc.UserName and info.BranchCode = pc.BranchCode ";
        sql = sql + " left join EmpGeneralDetail eg on eg.Ecode = pc.UserName and eg.BranchCode = pc.BranchCode ";
        sql = sql + " inner join BranchTab bt on bt.BranchId = pc.BranchCode where convert(date, Pc.RecordDate) between convert(date, '"+ date+ "') and convert(date, '" + Chequedate + "') ";
        
        if (ddlBranch.SelectedIndex != 0)
        {
            sql = sql + " and pc.BranchCode=" + ddlBranch.SelectedValue + " ";
            
            if (ddlUser.SelectedIndex != 0)
            {
                sql = sql + " and pc.username= '" + ddlUser.Text + "' ";
            }
        }
        if (ddlMode.SelectedIndex != 0)
        {
            sql = sql + " and pc.PaymentMode= '" + ddlMode.SelectedValue + "' ";
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
        
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        loadData();
    }
    
}