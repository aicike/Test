using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class CardPrefix : IBaseEntity
    {

        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        [Display(Name = "卡前缀")]
        [Required(ErrorMessage = "请输入卡前缀")]
        public string PrefixName { get; set; }



        public virtual ICollection<CardInfo> CardInfo { get; set; }

    }
}
