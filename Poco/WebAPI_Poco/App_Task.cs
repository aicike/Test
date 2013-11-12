using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Task
    {
        public int ID { get; set; }

        public int AccountMainID { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public int CreateAccountID { get; set; }

        public string CreateAccount { get; set; }

        /// <summary>
        /// 执行者
        /// </summary>
        public int ReceiverAccountID { get; set; }

        public string ReceiverAccount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 任务截止时间
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int EnumTaskStatus { get; set; }

        public string EnumTaskStatusValue { get; set; }

        /// <summary>
        /// 任务说明，要求等
        /// </summary>
        public string TaskSpecification { get; set; }
    }
}
