
using System;
using System.Data;
using System.IO;
using System.Net;

namespace c4SmsNew
{
    public class SMSAdapterNew
    {

        string branchCode = "";
        public static string kk = "";
        string sql = "";
        string referalUrl = "";
        Campus oo = new Campus();
        BLL obj = new BLL();
        public SMSAdapterNew()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13; // Or just Tls12 if Tls13 is not needed/supported
            sql = "Select PanelUrl from SmsPanelSetting where branchcode=" + obj.branchCode() + "";
            kk = oo.ReturnTag(sql, "PanelUrl");
            referalUrl = kk.ToString();
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string clientid
        {
            get { return clientid; }
            set { clientid = value; }
        }

        public string Send(string message, string mobile, string title = "0")//here we pass a structure  
        {
            //mobile = "9889405555";
            string result = "";
            sql = "Select HitValue from SMSActivateDeactivate where branchcode=" + obj.branchCode() + "";
            if (oo.ReturnTag(sql, "HitValue").ToString().ToLower() != "" && oo.ReturnTag(sql, "HitValue").ToString().ToLower() == "true")
            {

                string user = "", Pass = "";
                sql = "Select top(1) UserId,Password from SmsPanelSetting where branchcode=" + obj.branchCode() + "";
                user = oo.ReturnTag(sql, "UserId");
                Pass = oo.ReturnTag(sql, "Password");

                Username = user;
                Password = Pass;

                string sender = "";
                sql = "Select top(1) ClientIDPass,SenderIdPass from SmsPanelSetting where branchcode=" + obj.branchCode() + "";
                sender = oo.ReturnTag(sql, "SenderIdPass");

                //string templateId = "";
                //sql = "SELECT top(1) TemplateID FROM SmsFormat where HeaderCLIassociated='" + sender+"' and id=100";
                //templateId = oo.ReturnTag(sql, "TemplateID");

                string templateId = "";
                sql = "SELECT Template FROM SmsEmailMaster where BranchCode='" + obj.branchCode() + "' and Id=" + title;
                DataTable dt = oo.Fetchdata(sql);
                templateId = dt.Rows[0][0].ToString();

                sql = "Select top(1) priority,smstype from SmsPanelSetting where branchcode=" + obj.branchCode() + "";
                string priorty = oo.ReturnTag(sql, "priority");
                string smstype = oo.ReturnTag(sql, "smstype");

                sql = "select top(1) CountryName from CollegeMaster where  branchcode=" + obj.branchCode() + "";
                string CountryName = oo.ReturnTag(sql, "CountryName");
                if (CountryName == "163")
                {
                    result = apicall(referalUrl + "message=" + message + "&to=" + mobile + "&sender=" + sender + "&type=0&routing=3&token=MH3mFIaA2r016enOVhPOqiYm78o6nujgLuvamG8ZnqpqdeBjzsijfX7UpAIbQkEfpjhDfDptOdrdFPNVv8LudiWbym3gBe6YjZWj");
                    //result = apicall(referalUrl + "sender=" + sender + "&to=" + mobile + "&message=" + message+ "&type=0&routing=3&token=MH3mFIaA2r016enOVhPOqiYm78o6nujgLuvamG8ZnqpqdeBjzsijfX7UpAIbQkEfpjhDfDptOdrdFPNVv8LudiWbym3gBe6YjZWj");
                }
                else
                {
                    if (referalUrl == "http://148.251.80.111:5665/api/SendSMSMulti")// for RJP New panel
                    {
                        result = apicall(referalUrl + "?api_id=" + Username + "&api_password=" + Password + "&sms_type=T&encoding=T&sender_id=" + sender + "&phonenumber=" + mobile + "&textmessage=" + message);
                    }
                    else if (referalUrl == "http://103.233.79.246//submitsms.jsp")
                    {
                        result = apicall(referalUrl + "?user=" + Username + "&key=" + Password + "&mobile=" + mobile + "&message=" + message + "&senderid=" + sender + "&accusage=" + priorty);
                    }
                    else
                    {
                        if (priorty == string.Empty && smstype == string.Empty)
                        {
                            result = apicall(referalUrl + "?username=" + Username + "&password=" + Password + "&phone_number=" + mobile + "&sender_name=" + sender + "&message=" + message + "&template_id=" + templateId);
                        }
                        else
                        {
                            result = apicall(referalUrl + "?user=" + Username + "&pass=" + Password + "&sender=" + sender + "&phone=" + mobile + "&text=" + message + "&priority=" + priorty + "&stype=" + smstype);
                        }
                    }
                }
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
    }
}
