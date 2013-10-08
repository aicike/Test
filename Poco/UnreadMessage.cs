using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class UnreadMessage
    {
        /// <summary>
        /// 发送人ID（售楼部或用户ID）
        /// </summary>
        public string I { get; set; }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        public string N { get; set; }

        /// <summary>
        /// 用户姓名备注
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// 发送人头像
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// 发送人类型 u 用户 s 售楼部
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// 未读消息数量
        /// </summary>
        public int C { get; set; }

        /// <summary>
        /// 最后一条消息发送时间
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// 消息类型 0：文本 1：图片 2：视频 3：音频 4：图文
        /// </summary>
        public int E { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string CT { get; set; }
        
        /// <summary>
        /// 消息方向  0:售楼代表对用户  1：用户对售楼代表  2：售楼代表对售楼代表  3：用户对用户
        /// </summary>
        public int M { get; set; }

        /// <summary>
        /// 会话ID
        /// </summary>
        public int S { get; set; }

    }
}
