using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ActivityInfoParticipator : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int ActivityInfoID { get; set; }

        public virtual ActivityInfo ActivityInfo { get; set; }

        [Display(Name = "用户类型")]
        public int? EnumAdvertorialUType { get; set; }

        [Display(Name = "用户ID")]
        public int? UserID { get; set; }

        [Display(Name = "用户电话")]
        [StringLength(15, ErrorMessage = "长度小于15")]
        public string Phone { get; set; }

        [Display(Name = "用户名称")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string Name { get; set; }

        [Display(Name = "用户邮箱")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string Email { get; set; }

        [Display(Name = "公司")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Company { get; set; }

        [Display(Name = "职位")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string Position { get; set; }

        [Display(Name = "参与时间")]
        public DateTime JoinDateTime { get; set; }
    }
}
