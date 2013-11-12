using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP.Xml.Dom;

namespace Poco
{
    /// <summary>
    /// XMPP消息体
    /// </summary>
    public class XMPP_Body_SMS:Element
    {
        public XMPP_Body_SMS()
        {
            this.TagName = "XMPP_Body_SMS";
            this.Namespace = "agsoftware:XMPP_Body_SMS";
        }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content
        {
            get { return GetTag("content"); }
            set { SetTag("content", value); }
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone
        {
            get { return GetTag("phone"); }
            set { SetTag("phone", value); }
        }
    }
}
