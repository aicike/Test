using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 文本库
    /// </summary>
    [Serializable]
    public class LibraryText : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Title { get; set; }

        [Display(Name = "正文")]
        [Required(ErrorMessage = "请输入正文")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Content { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
