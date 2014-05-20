using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 信息审核状态
    /// </summary>
    public enum EnumDataStatus
    {
        /// <summary>
        /// 等待付款
        /// </summary>
        WaitPayMent,  
        /// <summary>
        /// 未提交审核
        /// </summary>
        None,
        /// <summary>
        /// 审核未通过
        /// </summary>
        Disabled,
        /// <summary>
        /// 已付款，审核通过
        /// </summary>
        Enabled,
    }
}
