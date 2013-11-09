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
        None=0,
        /// <summary>
        /// 执行中
        /// </summary>
        Process=1,
        /// <summary>
        /// 任务完成
        /// </summary>
        Finish=2,
        /// <summary>
        /// 任务未完成
        /// </summary>
        UnFinish=3,
        /// <summary>
        /// 任务取消
        /// </summary>
        Cancel=4
    }
}
