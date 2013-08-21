using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 消息模式
    /// </summary>
    public enum EnumMsgModel
    {
        /// <summary>
        /// 自助式问答
        /// </summary>
        AutoMessage = 0,
        /// <summary>
        /// 推送
        /// </summary>
        Push = 1,
        /// <summary>
        /// 聊天
        /// </summary>
        Call = 2
    }
}
