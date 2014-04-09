using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 报修状态
    /// </summary>
    public enum EnumRepairStatus
    {
        /// <summary>
        /// 审核中
        /// </summary>
        Audit,
        /// <summary>
        /// 已分配
        /// </summary>
        Allocated,
        /// <summary>
        /// 已完成
        /// </summary>
        completed,
        /// <summary>
        /// 驳回
        /// </summary>
        Reject
    }
}
