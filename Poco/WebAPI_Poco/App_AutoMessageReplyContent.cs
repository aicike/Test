using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_AutoMessageReplyContent
    {
        public int ID { get; set; }

        /// <summary>
        /// Text,Image,Video,Voice,ImageText
        /// </summary>
        public int Type { get; set; }

        public string FileTitle { get; set; }

        public string FileUrl { get; set; }

        public string Content { get; set; }
    }
}
