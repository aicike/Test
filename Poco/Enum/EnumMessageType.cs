using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EnumMessageType
    {
        /// <summary>
        /// 文本库
        /// </summary>
        Text=0,
        /// <summary>
        /// 图片库
        /// </summary>
        Image=1,
        /// <summary>
        /// 视频库
        /// </summary>
        Video=2,
        /// <summary>
        /// 音频库
        /// </summary>
        Voice=3,
        /// <summary>
        /// 图文库
        /// </summary>
        ImageText=4,
        /// <summary>
        /// 自动回复文本
        /// </summary>
        TextReply
    }
}
