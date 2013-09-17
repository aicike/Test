using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 自动消息，被添加时自动回复
    /// </summary>
    [Serializable]
    public class AutoMessage_Add : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "消息")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Content { get; set; }
        
        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
