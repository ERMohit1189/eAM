using c4SmsNew;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSBAL;

public partial class BalanceFeeReminder : Page
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
            string sql = "Select BranchId, BranchName from Branchtab";
            var dt = oo.Fetchdata(sql);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            if (Session["LoginType"].ToString() == "Admin")
            {
                divBranch.Visible = false;
                divSession.Visible = false;
                ddlBranch.SelectedValue = Session["BranchCode"].ToString();

                string sqls = "select SessionName from SessionMaster where BranchCode=" + ddlBranch.SelectedValue + "";
                var dt2 = oo.Fetchdata(sqls);
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

            DrpSessionName.Items.Insert(0, new ListItem("<--Select-->", ""));

            sql = "Select id, ClassName from ClassMaster";
            sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + "";
            sql += "  order by Id";
            oo.FillDropDown_withValue(sql, DrpClass, "ClassName", "id");
            DrpClass.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));
            drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
            ddlFeeCategory.Items.Insert(0, new ListItem("<-- Select-->", ""));
            drpInstallment.Items.Insert(0, new ListItem("<-- Select-->", ""));
        }
        msg1.InnerHtml = "";
        string sql1 = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
        if (oo.ReturnTag(sql1, "HitValue") != "")
        {
            if (oo.ReturnTag(sql1, "HitValue").ToLower() == "false")
            {
                divMain.Visible = false;
                msg1.InnerHtml = "SMS Panel is not active!";
            }
            else
            {
                decimal smsBalance = checkSMSBalance();
                if (smsBalance < 1)
                {
                    divMain.Visible = false;
                    msg1.InnerHtml = "Dear user, you've insufficient credit (0) balance in your SMS Account.";
                }
            }
        }
        else
        {
            divMain.Visible = false;
            msg1.InnerHtml = "SMS Panel is not active!";
        }
    }

    private decimal checkSMSBalance()
    {
        var sadpNew = new SMSAdapterNew();
        string sql = "Select userid, Password, Panelurl from SmsPanelSetting where BranchCode=" + Session["BranchCode"] + "";
        string referalUrl = oo.ReturnTag(sql, "Panelurl").ToString().Replace("send", "balance");
        string user = oo.ReturnTag(sql, "userid");
        string pass = oo.ReturnTag(sql, "Password");
        string sms_response = sadpNew.apicall(referalUrl + "?username=" + user + "&password=" + pass + "");
        SMSBalance balanceSMS = JsonConvert.DeserializeObject<SMSBalance>(sms_response);
        if (balanceSMS.data.total_credit > 0)
        {
            return balanceSMS.data.total_credit;
        }
        else
        {
            return balanceSMS.data.total_balance;
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
    protected void loadFeeCategory()
    {

        string sql1 = "Select FeeGroupName,Id from FeeGroupMaster";
        sql1 = sql1 + "  where SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue + "";
        oo.FillDropDown_withValue(sql1, ddlFeeCategory, "FeeGroupName", "Id");
        ddlFeeCategory.Items.Insert(0, new ListItem("<-- Select-->", ""));

    }
    protected void ddlFeeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadFeeMonth();
    }
    public void loadFeeMonth()
    {
        string sql = "select Monthid, MonthName from MonthMaster where (CardType='" + ddlFeeCategory.SelectedItem.ToString() + "' or CardType='" + ddlFeeCategory.SelectedValue.ToString() + "')";
        sql += " and ClassId='" + DrpClass.SelectedValue.ToString() + "' and MOD='I' and SessionName='" + DrpSessionName.SelectedValue.ToString() + "' and BranchCode=" + ddlBranch.SelectedValue.ToString() + " or monthid=0";
        sql += "  order by MonthId";
        oo.FillDropDown_withValue(sql, drpInstallment, "MonthName", "Monthid");
        drpInstallment.Items.Insert(0, new ListItem("<-- Select-->", ""));
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        LoadReport();
    }
    protected void LoadReport()
    {

        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@FeeGroup", drpFeeGroup.SelectedValue));
        param.Add(new SqlParameter("@FeeCategoryId", ddlFeeCategory.SelectedValue));
        param.Add(new SqlParameter("@InstallmentIds", drpInstallment.SelectedValue));
        param.Add(new SqlParameter("@ClassId", DrpClass.SelectedValue));
        if (drpSection.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@SectionId", drpSection.SelectedValue));
        }
        if (drpBranch.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@BranchId", drpBranch.SelectedValue));
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            param.Add(new SqlParameter("@Withdrwal", ddlStatus.SelectedValue));
        }
        param.Add(new SqlParameter("@SessionName", DrpSessionName.SelectedItem.Text));
        param.Add(new SqlParameter("@BranchCode", ddlBranch.SelectedValue));
        heading.Text = "Fee Overdue Reminder of Class : " + DrpClass.SelectedItem.Text + " " + (drpBranch.SelectedValue != "" ? drpBranch.SelectedItem.Text : "") + " " + (drpSection.SelectedValue != "" ? "(" + drpSection.SelectedItem.Text + ")" : "") + "";
        DataSet ds = new DLL().Sp_SelectRecord_usingExecuteDataset("sp_BalanceFeeReminder", param);
        DataTable dt;
        if (ds != null)
        {
            string ss = "select format(getdate(), 'dd MMM yyyy') dates";
            lblRegister.Text = "Fee Category : " + ddlFeeCategory.SelectedItem.Text + " | Till Installment : " + drpInstallment.SelectedItem.Text + " | Date : " + oo.ReturnTag(ss, "dates") + (ddlStatus.SelectedIndex == 0 ? "" : " | Status : " + ddlStatus.SelectedItem.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
            {
                LinkButton1.Visible = false;
                Panel2.Visible = true;

                abc.Visible = true;
                dt = ds.Tables[0];

                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.Rows[GridView1.Rows.Count - 1].Cells[0].Text = "";
                GridView1.Rows[GridView1.Rows.Count - 1].Cells[1].Text = "";
            }
            else
            {
                Panel2.Visible = false;
                LinkButton1.Visible = false;
                abc.Visible = false;
            }
        }
    }

    protected void DrpClass_SelectedIndexChanged(object sender, EventArgs e)
    {

        string sql = "select id, SectionName from SectionMaster ";
        sql += "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassNameId=" + DrpClass.SelectedValue + "";
        sql += "  order by Id";
        oo.FillDropDown_withValue(sql, drpSection, "SectionName", "id");
        drpSection.Items.Insert(0, new ListItem("<--Select-->", ""));

        string sqls = "select id, BranchName from BranchMaster ";
        sqls = sqls + "  where SessionName='" + Session["SessionName"] + "' and BranchCode=" + Session["BranchCode"] + " and ClassId=" + DrpClass.SelectedValue + "";
        sqls = sqls + "  order by Id";
        oo.FillDropDown_withValue(sqls, drpBranch, "BranchName", "id");
        drpBranch.Items.Insert(0, new ListItem("<--Select-->", ""));
        loadFeeCategory();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        oo.ExportTolandscapeWord(Response, "BalanceFeeReminder", gdv1);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        oo.ExportDivToExcelWithFormatting(Response, "BalanceFeeReminder.xls", gdv1, Server.MapPath("~/Admin/css/style.css"));
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        oo.ExporttolandscapePdf(Response, "BalanceFeeReminder", abc);
    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        PrintHelper_New.ctrl = abc;
        ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>var winpop=window.open('Print_New.aspx','_blank'); if(!winpop || winpop.closed){alert('Please allow pop-up blocker from browser settings!')}</script>");
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)sender;
        CheckBox chkAll = (CheckBox)chk.NamingContainer.FindControl("chkAll");
        int cnt = 0;
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            if (chk.Checked)
            {
                cnt = cnt + 1;
                CheckBox chks = (CheckBox)GridView1.Rows[i].FindControl("chk");
                chks.Checked = true;
            }
            else
            {
                CheckBox chks = (CheckBox)GridView1.Rows[i].FindControl("chk");
                chks.Checked = false;
            }
        }
        if (cnt > 0)
        {
            LinkButton1.Visible = true;
        }
        else
        {
            LinkButton1.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "makeGrid();", true);
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)sender;
        CheckBox chkAll = (CheckBox)GridView1.HeaderRow.FindControl("chkAll");
        int cnt = 0;
        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            CheckBox chks = (CheckBox)GridView1.Rows[i].FindControl("chk");
            if (chks.Checked)
            {
                cnt = cnt + 1;
            }
        }
        if (cnt == GridView1.Rows.Count - 1)
        {
            chkAll.Checked = true;

        }
        else
        {
            chkAll.Checked = false;
        }
        if (cnt > 0)
        {
            LinkButton1.Visible = true;
        }
        else
        {
            LinkButton1.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "makeGrid();", true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int totalCount = 0;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox chks = (CheckBox)gvr.FindControl("chk");
            totalCount += chks.Checked ? 1 : 0;
        }
        decimal smsBalance = checkSMSBalance();
        if (smsBalance >= totalCount)
        {
            int cnt = 0;
            if (GridView1.Rows.Count > 0)
            {
                string sql = "";
                sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + Session["BranchCode"] + "";
                if (oo.ReturnTag(sql, "HitValue") != "")
                {
                    if (oo.ReturnTag(sql, "HitValue") == "true")
                    {
                        sql = "Select SmsSent From SmsEmailMaster where Id='9' and BranchCode=" + Session["BranchCode"] + "";
                        if (oo.ReturnTag(sql, "SmsSent").Trim() == "true")
                        {
                            string user = "", Pass = "";
                            sql = "Select UserId,Password from SmsPanelSetting where BranchCode=" + Session["BranchCode"] + "";
                            user = oo.ReturnTag(sql, "UserId");
                            Pass = oo.ReturnTag(sql, "Password");
                            sql = "Select CollegeShortNa  from CollegeMaster where BranchCode=" + Session["BranchCode"] + "";
                            string collegename = oo.ReturnTag(sql, "CollegeShortNa");
                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                CheckBox Chk = (CheckBox)GridView1.Rows[i].FindControl("Chk");
                                if (Chk.Checked)
                                {
                                    string lblcontactno = GridView1.Rows[i].Cells[5].Text;
                                    string Name = GridView1.Rows[i].Cells[3].Text;
                                    string lbltotalValue = GridView1.Rows[i].Cells[10].Text;
                                    string lblsrno = GridView1.Rows[i].Cells[2].Text;

                                    if (lblcontactno != "")
                                    {
                                        if (lbltotalValue != "0" && lbltotalValue != "")
                                        {
                                            cnt = cnt + 1;
                                            //SendFeesSms(lblcontactno, lbltotalValue, Name, lblsrno, user, Pass, collegename);
                                            ComposeSMS(lblsrno, lbltotalValue);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Campus camp = new Campus(); camp.msgbox(Page, msgbox, "SMS Panel is not active!", "A");

                        }
                    }
                    else
                    {
                        Campus camp = new Campus(); camp.msgbox(Page, msgbox, "SMS Panel is not active!", "A");

                    }

                }
                else
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "SMS Panel is not active!", "A");

                }
                if (cnt > 0)
                {
                    Campus camp = new Campus(); camp.msgbox(Page, msgbox, "SMS sent successfully.", "S");

                }
            }
        }
        else
        {
            msgbox.InnerHtml = "Dear user, you've insufficient credit " + totalCount + " balance in your SMS Account.";
        }
    }

    public void ComposeSMS(string srno, string amount)
    {
        //  string userid = txtUserId.Text.ToString().Replace(" ", "");
        try
        {
            List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@SessionName",Session["SessionName"]),
                    new SqlParameter("@BranchCode",Session["BranchCode"]),
                    new SqlParameter("@SrNo",srno.ToString().Replace(" ", "")),
                    new SqlParameter("@Amount",amount.ToString().Replace(" ", ""))


                };
            DataSet ds = oo.ReturnDataSet("USP_BalanceFeeReminderTemplate", param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                string msg = SendSms(ds);
            }

        }
        catch
        {

        }

    }

    public string SendSms(DataSet ds)
    {
        string msg;
        try
        {
            DataTable data = new DataTable();
            data = ds.Tables[0];

            var contactNo = data.Rows[0]["ContactNo"].ToString();

            DataTable template = new DataTable();
            template = ds.Tables[1];

            msg = template.Rows[0][0].ToString();
            string[] param = template.Rows[0][1].ToString().Split(',');

            string[] daynamicVariables = msg.Split(new char[0]);
            foreach (var para in param)
            {
                string value = data.Rows[0][para].ToString();
                for (int i = 0; i < daynamicVariables.Count(); i++)
                {
                    if (daynamicVariables[i].ToString() == "{{{}}}")
                    {
                        daynamicVariables[i] = value;
                        break;
                    }
                }
            }

            msg = string.Join(" ", daynamicVariables);

            new SMS().SendSms(contactNo, msg, "9", Session["BranchCode"].ToString());

        }
        catch
        {
            msg = "";
        }
        return msg;
    }
    public void SendFeesSms(string FmobileNo, string Amount, string studentName, string srno, string user, string Pass, string collegename)
    {
        SMSAdapterNew sadpNew = new SMSAdapterNew();
        string mess = "";
        mess = "Dear Guardian, INR " + Amount + " is Balance towards the Fee of " + studentName + ". Please ignore if already paid. Mahavir Vidya Mandir Inter College.";
        string sms_response = "";
        sms_response = sadpNew.Send(mess, FmobileNo, "5");
    }
}