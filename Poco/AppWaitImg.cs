using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AppWaitImg : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        [Display(Name = "等待图片")]
        [Required(ErrorMessage = "请选择一张图片")]
        public string ImgPath { get; set; }
    }
}
