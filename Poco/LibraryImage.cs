using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class LibraryImage : IBaseEntity, ILibrary
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "图片名称")]
        [Required(ErrorMessage = "请输入图片名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string FileName { get; set; }

        [Display(Name = "图片文件")]
        [Required(ErrorMessage = "请上传图片")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string FilePath { get; set; }

        public int AccountMainID { get; set; }

        public AccountMain AccountMain { get; set; }
    }
}
