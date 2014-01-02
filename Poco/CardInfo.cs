using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class CardInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public int CardPrefixID { get; set; }
        public virtual CardPrefix CardPrefix { get; set; }


        [Display(Name = "卡号")]
        [Required(ErrorMessage = "请输入卡号")]
        public string CardNum { get; set; }

        [Display(Name = "金额")]
        [Required(ErrorMessage = "请输入金额")]
        [RegularExpression(@"^(([1-9]\d{0,9})|0)(\.\d{1,2})?$", ErrorMessage = "请输入正确的金额。")]
        public decimal Balance { get; set; }

        [Display(Name = "注册日期")]
        [Required(ErrorMessage = "请输入注册日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 0:冻结 1：正常
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }


        public virtual ICollection<VIPInfo> VIPInfo { get; set; }

    }
}
