using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class MessageGroupChat : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public int MessageID { get; set; }
        public virtual Message Message { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public int UserID { get; set; }

        /// <summary>
        /// 用户类型 枚举 EnumClientUserType
        /// </summary>
        [Display(Name = "用户类型")]
        public int UserType { get; set; }

        /// <summary>
        /// 会话ID
        /// </summary>
        public int SID { get; set; }
        

    }
}
