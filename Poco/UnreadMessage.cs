using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class UnreadMessage
    {
        string fromID;
        /// <summary>
        /// 发送人ID（售楼部或用户ID）
        /// </summary>
        public string FromID
        {
            get { return fromID; }
            set { fromID = value; }
        }
        string messageCnt;
        /// <summary>
        /// 未读消息数量
        /// </summary>
        public string MessageCnt
        {
            get { return messageCnt; }
            set { messageCnt = value; }
        }
        string sendTime;
        /// <summary>
        /// 最后一条消息发送时间
        /// </summary>
        public string SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }
        string eID;
        /// <summary>
        /// 消息类型 0：文本 1：图片 2：视频 3：音频 4：图文
        /// </summary>
        public string EID
        {
            get { return eID; }
            set { eID = value; }
        }
        string content;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        string mSD;
        /// <summary>
        /// 消息方向  0:售楼代表对用户  1：用户对售楼代表  2：售楼代表对售楼代表  3：用户对用户
        /// </summary>
        public string MSD
        {
            get { return mSD; }
            set { mSD = value; }
        }

        string sID;
        /// <summary>
        /// 会话ID
        /// </summary>
        public string SID
        {
            get { return sID; }
            set { sID = value; }
        }
        
    }
}
