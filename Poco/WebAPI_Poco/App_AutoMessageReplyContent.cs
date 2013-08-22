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

        /// <summary>
        /// 只有图文消息使用的字段
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 只有图文消息使用的字段
        /// </summary>
        public string SubContent { get; set; }

        /// <summary>
        /// 消息模式
        /// </summary>
        public int EnumMsgModel { get; set; }
        /// <summary>
        /// 消息模式对象的消息对象ID
        /// </summary>
        public int MsgID { get; set; }

        public string SendTime { get; set; }
    }
}
