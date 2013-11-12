using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ConversationDetailed : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        //售楼部ID
        [Display(Name = "售楼部")]
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        //会话ID
        [Display(Name = "会话ID")]
        public int ConversationID { get; set; }
        public virtual Conversation Conversation { get; set; }

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

        //会话类型 0：单人会话 1：多人会话
        [Display(Name = "会话类型")]
        public int CType { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "添加日期")]
        public DateTime SetDate { get; set; }
    }
}
