using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ActivateEmail : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "请输入有效的邮箱")]
        public string Email { get; set; }

        [Display(Name = "发送日期")]
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        [Display(Name = "激活码")]
        [Required(ErrorMessage = "请输入激活码")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Token { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUsed { get; set; }
    }
}
