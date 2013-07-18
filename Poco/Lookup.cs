using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 系统Lookup数据
    /// </summary>
    public class Lookup : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 固定值，不可更改
        /// </summary>
        [Display(Name = "Token")]
        [Required(ErrorMessage = "请输入Token值")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Token { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Value { get; set; }

        public virtual ICollection<LookupOption> LookupOptions { get; set; }
    }
}
