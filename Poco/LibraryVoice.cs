using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class LibraryVoice : IBaseEntity, ILibrary
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "音频名称")]
        [Required(ErrorMessage = "请输入音频名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string FileName { get; set; }

        [Display(Name = "音频文件")]
        [Required(ErrorMessage = "请上传音频")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string FilePath { get; set; }

        //转换后的文件地址
        public string FileMp3Path { get; set; }
        //时长 秒
        public string FileLength { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
