using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class TaskDetail : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int TaskID { get; set; }

        public virtual Task Task { get; set; }

        /// <summary>
        /// 完成情况
        /// </summary>
        [Display(Name = "完成情况")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string Content { get; set; }

        /// <summary>
        /// 完成情况记录时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 本次完成量，只有当任务类型为非字符串时，该字段才有效
        /// </summary>
        public double? Quantity { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public int AccountID { get; set; }

        public virtual Account Account { get; set; }
    }
}
