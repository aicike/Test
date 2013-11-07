using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 定义的任务
    /// </summary>
    public class TaskRule: IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Display(Name = "任务名称")]
        [StringLength(400, ErrorMessage = "长度小于200")]
        public string TaskInfoName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int EnumTaskType { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
