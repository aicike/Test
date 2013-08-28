using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class UnreadMessage
    {
        string fromAccountID;
        /// <summary>
        /// 售楼部发送人ID
        /// </summary>
        public string FromAccountID
        {
            get { return fromAccountID; }
            set { fromAccountID = value; }
        }
        string fromUserID;
        /// <summary>
        /// 用户发送人ID
        /// </summary>
        public string FromUserID
        {
            get { return fromUserID; }
            set { fromUserID = value; }
        }
        string sendTime;
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }
        string sendCnt;
        /// <summary>
        /// 条数
        /// </summary>
        public string SendCnt
        {
            get { return sendCnt; }
            set { sendCnt = value; }
        }
        string eID;
        /// <summary>
        /// 消息类型 0：文本库 1：图片库 2：视频库 3：音频库 4：图文库 
        /// </summary>
        public string EID
        {
            get { return eID; }
            set { eID = value; }
        }
        string content;
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
