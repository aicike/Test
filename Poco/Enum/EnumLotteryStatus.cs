using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 中奖情况
    /// </summary>
    public enum EnumLotteryStatus
    {
        /// <summary>
        /// 未领奖
        /// </summary>
        UnGet=0,
        /// <summary>
        /// 领奖
        /// </summary>
        Get=1,
        /// <summary>
        /// 无效，取消
        /// </summary>
        Invalid=2
    }
}
