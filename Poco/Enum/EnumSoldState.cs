using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 房屋售出状态
    /// </summary>
    public enum EnumSoldState
    {

        /// <summary>
        /// 未售出
        /// </summary>
        Unsold,
        /// <summary>
        /// 已售出
        /// </summary>
        HasSold,
        /// <summary>
        /// 已预定
        /// </summary>
        Scheduled,
        /// <summary>
        /// 预留
        /// </summary>
        Reserve
    }
}
