using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco.MerchantPoco
{
    /// <summary>
    /// 周边外卖详细内容
    /// </summary>
    public class M_TakeOutDetail:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int M_TakeOutID { get; set; }

        public virtual M_TakeOut M_TakeOut { get; set; }

        /// <summary>
        /// 外卖名称
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Title { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Display(Name = "单价")]
        [Required(ErrorMessage = "请输入销售单价")]
        [RegularExpression(@"^(\d+(.\d{1,2})|\d+)$", ErrorMessage = "请输入有效的金额")]
        public decimal Price { get; set; }
    }
}
