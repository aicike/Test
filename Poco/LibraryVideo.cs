using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class LibraryVideo : IBaseEntity, ILibrary
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "视频名称")]
        [Required(ErrorMessage = "请输入视频名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string FileName { get; set; }

        [Display(Name = "视频文件")]
        [Required(ErrorMessage = "请上传文件")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string FilePath { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
