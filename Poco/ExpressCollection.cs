using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ExpressCollection : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccounMain { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        [Required(ErrorMessage = "请输入单号")]
        public string OddNumber { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
         [Required(ErrorMessage = "请输入电话")]
        public string Phone { get; set; }

        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int EnumExpressStatus { get; set; }

    }
}
