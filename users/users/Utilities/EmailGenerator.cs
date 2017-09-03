using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace users.Utilities
{
    public class EmailGenerator
    {
        static Configuration config;
        public EmailGenerator()
        {
            config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        }

        public static bool SendMail(MailMessage msg)
        {
            try
            {

                MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

                msg.From = new MailAddress(settings.Smtp.From, "RxShopy");
                SmtpClient client = new SmtpClient(settings.Smtp.Network.Host);
                client.Port = settings.Smtp.Network.Port;
                client.EnableSsl = settings.Smtp.Network.EnableSsl;
                client.Timeout = 900000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
                client.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
                client.Send(msg);
                msg.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApiDataException(500, ex.Message, HttpStatusCode.BadRequest);
            }            
        }

        /*
        public bool ConfigMail(string to, string cc, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();            
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));
            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }
        */
        public bool ConfigMail(string to, bool isHtml, string subject, string body)
        {
            try
            {
                //var isSmtp = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSMTP"]);
                if (true)
                {
                    MailMessage msg = new MailMessage();
                    msg.Headers.Add("X-Mailgun-Campaign-Id", "funqu");
                    msg.To.Add(new MailAddress(to));
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.BodyEncoding = UTF8Encoding.UTF8;
                    msg.IsBodyHtml = isHtml;

                    return SendMail(msg);
                }
                else
                {
                    /*
                    var response = RestEmailSender.SendEmail(to, subject, body);
                    if (response.StatusCode == HttpStatusCode.OK)
                        return true;
                    else
                        return false;*/
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(500, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        /*
        public bool ConfigMail(string to, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));

            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string cc, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));

            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }
        */
    }
}