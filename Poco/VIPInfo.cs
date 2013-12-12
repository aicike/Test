using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class VIPInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }

        public int CardInfoID { get; set; }
        public virtual CardInfo CardInfo { get; set; }

        [Display(Name = "开卡日期")]
        [Required(ErrorMessage = "请输入开卡日期")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "vip类型")]
        public int? VIPType { get; set; }

        [Display(Name = "积分")]
        public int score { get; set; }

        /// <summary>
        /// 0:冻结 1：正常
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }

        public virtual ICollection<VIPInfoExpenseDetail> ExpenseDetails { get; set; }
    }
}
