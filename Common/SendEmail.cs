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
        public static object obj;
        static SendEmail()
        {
            obj = new object();
        }

        public static void SendMailAsync(EmailInfo email)
        {
            ThreadPool.QueueUserWorkItem(delegate { SendMail(email); });
        }
        public static int exceptionCount = 0;

        private static void SendMail(EmailInfo email)
        {
            lock (obj)
            {
                exceptionCount = 0;
                var emailConfig = System.Configuration.ConfigurationManager.AppSettings["EmailInfo"].Split('|');
                var smtpClientConfig = System.Configuration.ConfigurationManager.AppSettings["SMTPClient"];
                var portConfig = System.Configuration.ConfigurationManager.AppSettings["SMTPPort"];

                if (emailConfig != null)
                {
                    var emailAddress = emailConfig[0];
                    var emailPwd = emailConfig[1];

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
                            AlternateView BodyView = AlternateView.CreateAlternateViewFromString(email.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
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

                        message.SubjectEncoding = Encoding.UTF8;
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = email.IsHtml;

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
                                //smtpClient.Timeout = 10000;
                                try
                                {
                                    if (exceptionCount < 3)
                                    {
                                        smtpClient.Send(message);
                                        exceptionCount = 0;
                                    }
                                    else
                                    {
                                        smtpClient.Dispose();
                                    }

                                }
                                catch
                                {
                                    exceptionCount++;
                                }
                                finally
                                {
                                    try
                                    {
                                        if (exceptionCount >= 3)
                                        {
                                            smtpClient.Dispose();
                                            exceptionCount = 0;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        exceptionCount = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}