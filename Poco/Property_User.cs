using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 业主信息
    /// </summary>
    public class Property_User :IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; } 

        /// <summary>
        /// 房屋信息
        /// </summary>
        public int Property_HouseID { get; set; }

        public virtual Property_House Property_House { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public int? UserLoginInfoID { get; set; }

        public virtual UserLoginInfo UserLoginInfo { get; set; }

        [Display(Name = "业主姓名")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "业主电话")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Phone { get; set; }
    }
}
