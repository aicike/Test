using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 指定的任务
    /// </summary>
    public class Task : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int TaskRuleID { get; set; }

        public virtual TaskRule TaskRule { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public int CreateAccountID { get; set; }

        public Account CreateAccount { get; set; }

        /// <summary>
        /// 执行者
        /// </summary>
        public int ReceiverAccountID { get; set; }

        public Account ReceiverAccount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 任务截止时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int EnumTaskStatus { get; set; }

        /// <summary>
        /// 任务说明，要求等
        /// </summary>
        [Display(Name = "任务说明")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string TaskSpecification { get; set; }

        /// <summary>
        /// 任务量，根据任务类型不同，格式也不同，任务类型为数字时必须填写，为百分数时默认100%
        /// 如：
        /// 任务类型为数字,则按照总数进行统计
        /// 任务类型为百分数，则按照百分数进行统计
        /// 任务类型为字符串，不统计
        /// </summary>
        public double? Quantity { get; set; }

        public virtual ICollection<TaskDetail> TaskDetails { get; set; }
    }
}
