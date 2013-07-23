using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class SystemMessage : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "消息")]
        [Required(ErrorMessage = "消息不能为空")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string Message { get; set; }

        public DateTime SendDate { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
