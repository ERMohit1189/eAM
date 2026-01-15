using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


/// <summary>
/// Summary description for Email
/// </summary>
namespace DPSAdmin.biz
{

    public class Email
    {
        public Email()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static string GetMailServer
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["mailServer"].ToString();
            }
        }

        public static void SendMail(string FromEmail, string fromUser, string ToEmail, string Message, string subject)
        {
            try
            {
                System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage(FromEmail, ToEmail, Message, subject);
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                oMessage.IsBodyHtml = true;
                smtp.UseDefaultCredentials = true;
                smtp.Send(oMessage);
                oMessage.Dispose();
            }
            catch (SmtpException)
            {
                
            }
        }
    }
}
