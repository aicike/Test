using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Panorama : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string Name { get; set; }

        [Display(Name = "360度全景图")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string FullImage { get; set; }

        [Display(Name = "图片右")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string ImageRight { get; set; }

        [Display(Name = "图片左")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string ImageLeft { get; set; }

        [Display(Name = "图片上")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string ImageTop { get; set; }

        [Display(Name = "图片下")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string ImageBottom { get; set; }

        [Display(Name = "图片前")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string ImageBlack { get; set; }

        [Display(Name = "图片前")]
        [StringLength(500, ErrorMessage = "长度小于1000")]
        public string ImageFront { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
