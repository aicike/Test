using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum EnumTaskStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        None,
        /// <summary>
        /// 执行中
        /// </summary>
        Process,
        /// <summary>
        /// 任务完成
        /// </summary>
        Finish,
        /// <summary>
        /// 任务未完成
        /// </summary>
        UnFinish,
        /// <summary>
        /// 任务取消
        /// </summary>
        Cancel
    }
}
