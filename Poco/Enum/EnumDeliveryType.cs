using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    public enum EnumDeliveryType
    {
        /// <summary>
        /// 仅工作日送
        /// </summary>
        WorkingDay = 0,
        /// <summary>
        /// 仅休息日(周六-周日)送
        /// </summary>
        OffDay = 1,
        /// <summary>
        /// 每日送
        /// </summary>
        EveryDay = 2
    }
}
