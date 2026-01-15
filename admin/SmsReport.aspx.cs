using System;
using System.Data.SqlClient;
using System.Web.UI;
using c4SmsNew;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using SMSBAL;

public partial class SuperAdmin_SmsReport : Page
{
    private SqlConnection _con;
    private readonly Campus _oo;
    private string sql = string.Empty;
    public static string url = "";
    string user = "";
    string msgid = "";
    string referalUrl = "";
    string msgtype = "";

    string pass = "";
    public SuperAdmin_SmsReport()
    {
        _con = new SqlConnection();
        _oo = new Campus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["LoginName"].ToString()) || string.IsNullOrEmpty(Session["BranchCode"].ToString()) || string.IsNullOrEmpty(Session["SessionName"].ToString()))
        {
            Response.Redirect("default.aspx");
        }
        //_con = _oo.dbGet_connection();
        Campus camp = new Campus(); camp.LoadLoader(loader);  //in cs file
        if (!IsPostBack)
        {
            var isInternetConnectionAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (!isInternetConnectionAvailable)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "alertmsg()", true);
            }
        }
    }

   

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        bool bb = NetworkInterface.GetIsNetworkAvailable();

        if (bb == false)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Internet connections are not available", "A");
        }
        else
        {
            SmsReport(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        bool bb = NetworkInterface.GetIsNetworkAvailable();
        if (bb == false)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Internet connections are not available", "A");
        }
        else
        {
            CheckBalance();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCheckAPIBalance.Text = "";
        lblStatus.InnerHtml = "";
        if (RadioButtonList1.SelectedIndex == 0)
        {
            panel12.Visible = true;
            panel1.Visible = false;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            panel12.Visible = false;
            panel1.Visible = true;

        }
    }


    public void SmsReport(string from, string to)
    {
        sql = "SELECT DATEDIFF(DAY,'" + txtFromDate.Text.Trim() + "', '" + txtToDate.Text.Trim() + "') AS days";
        int days=int.Parse(_oo.ReturnTag(sql, "days"));
        if (days>7)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Dear User, you can view last 7 days reports only!", "A");
            return;
        }
        if (days <0)
        {
            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "Invalid date selected!", "A");
            return;
        }
        var sms_response = "";
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(sql, "HitValue") == "true")
            {
                var sadpNew = new SMSAdapterNew();
                sql = "Select userid, Password, Panelurl from SmsPanelSetting where BranchCode=" + Session["BranchCode"] + "";
                referalUrl = _oo.ReturnTag(sql, "Panelurl").ToString().Replace("send", "report"); 
                user = _oo.ReturnTag(sql, "userid");
                string Password = _oo.ReturnTag(sql, "Password");
                string api = referalUrl + "?username=" + user + "&password=" + Password + "&start_date=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + " 00:01:01" + "&end_date=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + " 23:59:59";
                sms_response = sadpNew.apicall(api);
                if (sms_response!="0")
                {
                    SMSReport persons = JsonConvert.DeserializeObject<SMSReport>(sms_response);
                    string table = "<table>";
                    for (int i = 0; i < persons.data.Count; i++)
                    {
                        string stt = "[job_id : " + persons.data[i].job_id + "], [mobile : " + persons.data[i].mobile + "], [status : " + persons.data[i].status + "], [message : " + persons.data[i].message + "]";
                        table = table + "<tr style='border:1px solid #ccc; padding:10px;'><td style='border-right:1px solid #ccc; padding:10px;'>" + (i + 1) + "</td><td>&nbsp;" + stt + "</td></tr>";
                    }
                    table = table + "</table>";
                    lblStatus.InnerHtml = table;
                }               
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "SMS Panel is not active!", "A");
            }
        }
        
    }
    public void CheckBalance()
    {
        sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (_oo.ReturnTag(sql, "HitValue") != "")
        {
            if (_oo.ReturnTag(sql, "HitValue") == "true")
            {
                var sadpNew = new SMSAdapterNew();
                sql = "Select userid, Password, Panelurl from SmsPanelSetting where BranchCode=" + Session["BranchCode"] + "";
                referalUrl = _oo.ReturnTag(sql, "Panelurl").ToString().Replace("send", "balance");
                user = _oo.ReturnTag(sql, "userid");
                pass = _oo.ReturnTag(sql, "Password");
                var sms_response = sadpNew.apicall(referalUrl + "?username=" + user + "&password=" + pass + "");
                SMSBalance balanceSMS = JsonConvert.DeserializeObject<SMSBalance>(sms_response);

                if(balanceSMS.data.total_credit > 0)
                {
                    if (balanceSMS.data.total_credit >= 10000)
                    {
                        lblCheckAPIBalance.Text = balanceSMS.data.total_balance.ToString() + " SMS LEFT";
                        lblCheckAPIBalance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00b707");
                        message.Visible = false;
                    }
                    if (balanceSMS.data.total_credit < 10000)
                    {
                        lblCheckAPIBalance.Text = balanceSMS.data.total_balance.ToString() + " SMS LEFT";
                        lblCheckAPIBalance.ForeColor = message.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff9000");
                        message.Visible = false;
                    }
                    if (balanceSMS.data.total_credit <= 1)
                    {
                        lblCheckAPIBalance.Text = balanceSMS.data.total_balance.ToString() + " SMS LEFT";
                        lblCheckAPIBalance.ForeColor = message.ForeColor = System.Drawing.ColorTranslator.FromHtml("#f10a0a");
                        message.Text = "Note: your sms credit is getting low.";
                        message.Visible = true;
                    }
                }
                else
                {
                    if (balanceSMS.data.total_balance >= 5000)
                    {
                        lblCheckAPIBalance.Text = balanceSMS.data.total_balance.ToString() + " AMOUNT LEFT";
                        lblCheckAPIBalance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00b707");
                        message.Visible = false;
                    }
                    if (balanceSMS.data.total_balance < 5000)
                    {
                        lblCheckAPIBalance.Text = balanceSMS.data.total_balance.ToString() + " AMOUNT LEFT";
                        lblCheckAPIBalance.ForeColor = message.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff9000");
                        //message.Text = "Note: your sms credit is getting low.";
                        message.Visible = false;
                    }
                    if (balanceSMS.data.total_balance <= 1)
                    {
                        lblCheckAPIBalance.Text = balanceSMS.data.total_balance.ToString() + " AMOUNT LEFT";
                        lblCheckAPIBalance.ForeColor = message.ForeColor = System.Drawing.ColorTranslator.FromHtml("#f10a0a");
                        message.Text = "Note: your sms credit is getting low.";
                        message.Visible = true;
                    }
                }
            }
            else
            {
                Campus camp = new Campus(); camp.msgbox(Page, msgbox, "SMS Panel is not active!", "A");
            }
        }
    }
}