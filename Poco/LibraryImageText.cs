using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class LibraryImageText : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Title { get; set; }

        [Display(Name = "封面图片")]
        [Required(ErrorMessage = "请上传封面图片")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string ImagePath { get; set; }

        [Display(Name = "摘要")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Summary { get; set; }

        [Display(Name = "正文")]
        [Required(ErrorMessage = "请输入正文")]
        public string Content { get; set; }
        
        public int? LibraryImageTextParentID { get; set; }

        public virtual LibraryImageText LibraryImageTextParent { get; set; }

        public virtual ICollection<LibraryImageText> LibraryImageTexts { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<PendingMessages> PendingMessagess { get; set; }
    }
}
