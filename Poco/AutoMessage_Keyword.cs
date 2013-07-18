﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AutoMessage_Keyword : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "规则名称")]
        [Required(ErrorMessage = "请输入规则名称")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string RuleName { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

        public virtual ICollection<KeywordAutoMessage> KeywordAutoMessages { get; set; }
    }

    /// <summary>
    /// 关键字
    /// </summary>
    public class Keyword : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AutoMessage_KeywordID { get; set; }

        public virtual AutoMessage_Keyword AutoMessage_Keyword { get; set; }

        [Display(Name = "关键字")]
        [Required(ErrorMessage = "请输入关键字")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Token { get; set; }
    }

    /// <summary>
    /// 普通文本自动回复
    /// </summary>
    public class TextReply : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "消息")]
        [Required(ErrorMessage = "请输入消息")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string Content { get; set; }
    }

    /// <summary>
    /// 关键字回复内容
    /// </summary>
    public class KeywordAutoMessage : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "消息类型")]
        public int EnumMessageTypeID { get; set; }

        public virtual LookupOption EnumMessageType { get; set; }

        /// <summary>
        /// 引用的消息的主键值
        /// </summary>
        public int MessageID { get; set; }

        public int AutoMessage_KeywordID { get; set; }

        public virtual AutoMessage_Keyword AutoMessage_Keyword { get; set; }

        [Display(Name = "排序")]
        public int Order { get; set; }
    }
}
