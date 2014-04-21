using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 收费维修
    /// </summary>
    public class Repairchargeso : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        [Display(Name = "收费项目名称")]
        [Required(ErrorMessage = "请输入项目名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 收费金额
        /// </summary>
        [Display(Name = "收费金额")]
        [Required(ErrorMessage = "请输入收费金额")]
        public decimal Price { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime GetDate { get; set; }
    }
}
