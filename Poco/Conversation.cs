using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class Conversation : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        [Display(Name = "售楼部")]
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 会话类型 0：单人会话 1：多人会话
        /// </summary>
        [Display(Name = "会话类型")]
        public int CType { get; set; }

        //会话名称 多人会话有值
        [Display(Name = "会话名称")]
        public string Cname { get; set; }

        //会话头像 多人会话有值
        [Display(Name = "会话头像")]
        public string Cimg { get; set; }

        public virtual ICollection<Message> Message { get; set; }

        public virtual ICollection<PendingMessages> PendingMessages { get; set; }

        public virtual ICollection<ConversationDetailed> ConversationDetailed { get; set; }


    }
}
