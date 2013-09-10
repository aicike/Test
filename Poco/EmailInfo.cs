using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;

namespace Poco
{
    public class EmailInfo
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<LinkedResource> EmbeddedResources { get; set; }

        public MailPriority? Priority { get; set; }

        public bool UseSSL { get; set; }

        public string CC { get; set; }

        public string Bcc { get; set; }

        public bool IsHtml
        {
            get
            {
                return isHtml;
            }
            set
            {
                isHtml = value;
            }
        }
        private bool isHtml = false;
    }
}
