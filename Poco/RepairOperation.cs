using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 报修操作表
    /// </summary>
    public class RepairOperation : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 报修表ID
        /// </summary>
        [Display(Name = "报修表ID")]
        public int RepairInfoID { get; set; }
        public virtual RepairInfo RepairInfo { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        [Display(Name = "操作日期")]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [Display(Name = "操作内容")]
        public string OperationContent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remarks { get; set; }

    }
}
