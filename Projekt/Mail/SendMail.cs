using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using Microsoft.Ajax.Utilities;

namespace Projekt.Mail
{
    public class SendMail
    {
        public static void SendMails(string to, string subject, string body)
        {
            var message = new System.Net.Mail.MailMessage(ConfigurationManager.AppSettings["sender"], to)
            {
                Subject = subject,
                Body = body
            };
            var smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpHost"],
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["sender"],
                    ConfigurationManager.AppSettings["passwd"]),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}