using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Feedback : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "意见或建议")]
        public string Content { get; set; }

        [Display(Name = "联系方式")]
        public string contact { get; set; }

        [Display(Name = "客户端")]
        public string client { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }
    }
}
