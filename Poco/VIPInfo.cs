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

        [Display(Name = "卡号")]
        [Required(ErrorMessage = "请输入卡号")]
        public string CardNumber { get; set; }

        [Display(Name = "余额")]
        [Required(ErrorMessage = "请输入余额")]
        public decimal Balance { get; set; }

        [Display(Name = "开卡日期")]
        [Required(ErrorMessage = "请输入开卡日期")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "vip类型")]
        public int? VIPType { get; set; }

        [Display(Name = "积分")]
        public int score{get;set;}

        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}
