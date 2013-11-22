using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class OrderMType : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [DropDownList(ErrorMessage = "请添加名称")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string Name { get; set; }

        [Display(Name = "订购天数")]
        [DropDownList(ErrorMessage = "请添加订购天数")]
        public int DateCnt { get; set;}

        [Display(Name = "每天配送瓶数")]
        [DropDownList(ErrorMessage = "请添加每天配送瓶数")]
        public int Count { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }
    }
}
