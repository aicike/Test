using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class AutoMessage_Reply : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "消息")]
        [Required(ErrorMessage = "请输入消息")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string Content { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
