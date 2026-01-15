using c4SmsNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;

public partial class forgot_password : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string sql = "";
    string referalUrl = "";
    Campus oo = new Campus();
    BLL obj = new BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sql = "select BrandName from BrandTab where isnull(BrandName, '')!=''";
            if (oo.Duplicate(sql))
            {
                divCompany.Visible = true;
                Label1.Text = oo.ReturnTag(sql, "BrandName");

            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        var param = new List<SqlParameter>
        {
            new SqlParameter("@username",txtUsername.Text.Trim()),
            new SqlParameter("@contactNo",txtContactno.Text.Trim())
        };

        string pass = DLL.objDll.Sp_SelectRecord_usingExecuteScalar("USP_Forgotpass", param).ToString();

        if (pass != string.Empty)
        {
            alert_danger.Style.Add("display", "none");
            alert_success.Style.Add("display", "block");

            ComposeSMS();
        }
        else
        {
            alert_danger.Style.Add("display", "block");
            alert_success.Style.Add("display", "none");
        }
    }

    public void ComposeSMS()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@ContactNo",txtContactno.Text.Trim()),
                new SqlParameter("@UserName",txtUsername.Text.Trim())

            };
            DataSet ds = oo.ReturnDataSet("USP_ForgotPasswordTemplate", param.ToArray());
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
            var branchCode = data.Rows[0]["BranchCode"].ToString();

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

            SendSms(contactNo, msg, "16", branchCode);

        }
        catch
        {
            msg = "";
        }
        return msg;
    }

    public string SendSms(string contactNo, string msg, string smsPageId, string branchCode)
    {
        string res = "0";
        try
        {
            sql = "Select HitValue from SMSActivateDeactivate where BranchCode=" + branchCode + "";
            if (oo.ReturnTag(sql, "HitValue") == "") return res;
            if (oo.ReturnTag(sql, "HitValue") != "true") return res;
            string sql1 = "Select SmsSent From SmsEmailMaster Where Id='" + smsPageId + "' and BranchCode=" + branchCode + "";
            if (oo.ReturnTag(sql1, "SmsSent").Trim() == "true")
            {
                if (contactNo == "") return "0";
                Send(msg, contactNo, smsPageId, branchCode);
                res = "1";
            }
        }
        catch (Exception ex)
        {
            // ignored
        }

        return res;
    }
    public string Send(string message, string mobile, string title, string branchCode)//here we pass a structure  
    {
        //mobile = "9889405555";
        string result = "";
        sql = "Select HitValue from SMSActivateDeactivate where branchcode=" + branchCode + "";
        if (oo.ReturnTag(sql, "HitValue").ToString().ToLower() != "" && oo.ReturnTag(sql, "HitValue").ToString().ToLower() == "true")
        {

            string user = "", Pass = "";
            sql = "Select top(1) UserId,Password from SmsPanelSetting where branchcode=" + branchCode + "";
            user = oo.ReturnTag(sql, "UserId");
            Pass = oo.ReturnTag(sql, "Password");

            string Username = user;
            string Password = Pass;

            string sender = "";
            sql = "Select top(1) ClientIDPass,SenderIdPass from SmsPanelSetting where branchcode=" + branchCode + "";
            sender = oo.ReturnTag(sql, "SenderIdPass");

            //string templateId = "";
            //sql = "SELECT top(1) TemplateID FROM SmsFormat where HeaderCLIassociated='" + sender+"' and id=100";
            //templateId = oo.ReturnTag(sql, "TemplateID");

            string templateId = "";
            sql = "SELECT Template FROM SmsEmailMaster where Id='" + title + "' and BranchCode=" + branchCode;
            DataTable dt = oo.Fetchdata(sql);
            templateId = dt.Rows[0][0].ToString();

            sql = "Select top(1) priority,smstype from SmsPanelSetting where branchcode=" + branchCode + "";
            string priorty = oo.ReturnTag(sql, "priority");
            string smstype = oo.ReturnTag(sql, "smstype");

            sql = "select top(1) CountryName from CollegeMaster where  branchcode=" + branchCode + "";
            string CountryName = oo.ReturnTag(sql, "CountryName");

            sql = "Select PanelUrl from SmsPanelSetting where branchcode=" + branchCode + "";
            referalUrl = oo.ReturnTag(sql, "PanelUrl");

            result = apicall(referalUrl + "?username=" + Username + "&password=" + Password + "&mobile=" + mobile + "&sendername=" + sender + "&message=" + message + "&templateid=" + templateId);

        }
        else
        {
            result = "Sorry, SMS Panel is Deactivated!";
        }
        return result;
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());

            string results = sr.ReadToEnd();

            sr.Close();
            return results;
        }
        catch (Exception ex)
        {
            return "0";
        }
    }

    public void SendFeesSms(string fmobileNo, string pass)
    {
        try
        {
            var sadpNew = new SMSAdapterNew();
            var mess = "Your Login password is: " + pass;
            if (fmobileNo == "") return;
            sadpNew.Send(mess, fmobileNo, "");
        }
        catch
        {
            // ignored
        }
    }
}