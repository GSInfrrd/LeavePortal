using System.Net.Mail;
using System.Configuration;
using System;
using System.Net.Mime;
using System.Web;

namespace LMS_WebAPI_Utils
{
    public class MailUtility
    {
        public static bool sendmail(string MailTo, string CcMailId, string MailSubject, string maildetails, string logoPath)
        {
            try
            {

                MailMessage mail = new MailMessage();
                string environment = ConfigurationManager.AppSettings["Environment"];
                if(environment==Environment.QA.Description() || environment==Environment.Dev.Description())
                {
                    string mailReceiverId = ConfigurationManager.AppSettings["MailReceiverId"];
                    mail.To.Add(mailReceiverId);
                    mail.CC.Add(mailReceiverId);
                    mail.Subject = environment + "_"+ MailSubject;
                }
                else
                { 
                mail.To.Add(MailTo);
                mail.CC.Add(CcMailId);
                mail.Subject = MailSubject;
                }
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromMailId"]);

                var inlineLogo = new LinkedResource(logoPath);
                inlineLogo.ContentId = "company-logo";

                var plainView = AlternateView.CreateAlternateViewFromString(maildetails, null, "text/plain");

                var view = AlternateView.CreateAlternateViewFromString(maildetails, null, "text/html");
                view.LinkedResources.Add(inlineLogo);
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(view);

                //Read from Resource File/Config files
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
                //string fromMailId = ConfigurationManager.AppSettings["FromMailId"];
                string fromMailIdPassword = ConfigurationManager.AppSettings["FromMailIdPassword"];
                smtp.Credentials = new System.Net.NetworkCredential
                (ConfigurationManager.AppSettings["FromMailId"], fromMailIdPassword);// Enter seders User name and password
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                smtp.Send(mail);

                return true;
            }
            catch
            {
                //Log
                return false;
            }

        }

    }
}
