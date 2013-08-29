using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Chat
    {
        public int ID { get; set; }

        public string SendTime { get; set; }

        public string Content { get; set; }

        public int Type { get; set; }

        public bool IsRead { get; set; }

        public int SendDirection { get; set; }
        
        public string FileTitle { get; set; }

        public string FileUrl { get; set; }

        /// <summary>
        /// 只有图文消息使用的字段
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 只有图文消息使用的字段
        /// </summary>
        public string SubContent { get; set; }
    }
}
