using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class LookupOption : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int LookupID { get; set; }

        public virtual Lookup Lookup { get; set; }

        /// <summary>
        /// 固定值，不可更改
        /// </summary>
        [Display(Name = "Token")]
        [Required(ErrorMessage = "请输入Token值")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Token { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Value { get; set; }

        public virtual ICollection<AccountMain> AccountMains { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<SystemUser> SystemUsers { get; set; }

        public virtual ICollection<KeywordAutoMessage> KeywordAutoMessages { get; set; }

        public virtual ICollection<Message> MessageEnumMessageType { get; set; }

        public virtual ICollection<Message> MessageEnumMessageSendDirection { get; set; }

        public virtual ICollection<ClientInfo> ClientInfoEnumClientSystemType { get; set; }

        public virtual ICollection<ClientInfo> ClientInfoEnumAccountStatus { get; set; }
    }
}
