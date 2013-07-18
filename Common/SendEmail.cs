using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using Poco;
using System.Web;

namespace Common
{
    public static class SendEmail
    {
        public static void SendMailAsync(EmailInfo email)
        {
            ThreadPool.QueueUserWorkItem(delegate { SendMail(email); });
        }

        public static void SendMail(EmailInfo email)
        {
            var emailConfig = System.Configuration.ConfigurationManager.AppSettings["EmailInfo"];
            var smtpClientConfig = System.Configuration.ConfigurationManager.AppSettings["SMTPClient"];
            var portConfig = System.Configuration.ConfigurationManager.AppSettings["SMTPPort"];

            if (emailConfig != null)
            {
                var emailInfo = DESEncrypt.Decrypt(emailConfig);
                var emailAddress = emailInfo.Split('|')[0];
                var emailPwd = emailInfo.Split('|')[1];

                using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                {
                    char[] Splitter = { ',', ';' };
                    string[] AddressCollection = email.To.Split(Splitter);
                    for (int x = 0; x < AddressCollection.Length; ++x)
                    {
                        if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                            message.To.Add(AddressCollection[x]);
                    }
                    if (!string.IsNullOrEmpty(email.CC))
                    {
                        AddressCollection = email.CC.Split(Splitter);
                        for (int x = 0; x < AddressCollection.Length; ++x)
                        {
                            if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                                message.CC.Add(AddressCollection[x]);
                        }
                    }
                    if (!string.IsNullOrEmpty(email.Bcc))
                    {
                        AddressCollection = email.Bcc.Split(Splitter);
                        for (int x = 0; x < AddressCollection.Length; ++x)
                        {
                            if (!string.IsNullOrEmpty(AddressCollection[x].Trim()))
                                message.Bcc.Add(AddressCollection[x]);
                        }
                    }
                    message.Subject = email.Subject;
                    message.From = new System.Net.Mail.MailAddress((emailAddress));
                    message.Body = email.Body;

                    if (email.EmbeddedResources != null)
                    {
                        AlternateView BodyView = AlternateView.CreateAlternateViewFromString(email.Body, null, MediaTypeNames.Text.Html);
                        foreach (LinkedResource Resource in email.EmbeddedResources)
                        {
                            BodyView.LinkedResources.Add(Resource);
                        }
                        message.AlternateViews.Add(BodyView);
                    }

                    if (email.Priority.HasValue)
                    {
                        message.Priority = email.Priority.Value;
                    }
                    else
                        email.Priority = MailPriority.High;

                    message.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    message.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    message.IsBodyHtml = true;

                    if (email.Attachments != null)
                    {
                        foreach (Attachment TempAttachment in email.Attachments)
                        {
                            message.Attachments.Add(TempAttachment);
                        }
                    }
                    if (smtpClientConfig != null)
                    {
                        using (SmtpClient smtpClient = new SmtpClient(smtpClientConfig))
                        {
                            if (portConfig != null && Int32.Parse(portConfig) > 0)
                                smtpClient.Port = Int32.Parse(portConfig);
                            else smtpClient.Port = 29;
                            smtpClient.UseDefaultCredentials = true;
                            if (email.UseSSL)
                                smtpClient.EnableSsl = true;
                            else
                                smtpClient.EnableSsl = false;

                            smtpClient.Credentials = new System.Net.NetworkCredential(emailAddress, emailPwd);
                            smtpClient.Send(message);
                        }
                    }
                }
            }
        }

        public static void SendMailDetail(string emailTo, string subject, string body)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["Host"];
            string validateUrl = url + "#";
            var context = string.Format(body, ", please click " + "<a href='" + validateUrl + "'>here</a>");
            EmailInfo emailInfo = new EmailInfo();
            //Bid distrubit user order
            emailInfo = new EmailInfo();
            emailInfo.To = emailTo;
            emailInfo.Priority = System.Net.Mail.MailPriority.High;
            emailInfo.Subject = subject;
            emailInfo.UseSSL = true;
            emailInfo.Body = context;
            SendEmail.SendMailAsync(emailInfo);
        }

        #region Send Email

        public static void SendMailDetail(string emailTo, string subject, string body,string strUrl)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["Host"];
            string validateUrl = url +"/"+ strUrl;
            var context = string.Format(body, ", please click " + "<a href='" + validateUrl + "'>here</a>");
            EmailInfo emailInfo = new EmailInfo();
            //Bid distrubit user order
            emailInfo = new EmailInfo();
            emailInfo.To = emailTo;
            emailInfo.Priority = System.Net.Mail.MailPriority.High;
            emailInfo.Subject = subject;
            emailInfo.UseSSL = true;
            emailInfo.Body = context;
            SendEmail.SendMailAsync(emailInfo);
        }
        #endregion
    }
}