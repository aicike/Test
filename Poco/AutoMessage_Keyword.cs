using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class AutoMessage_Keyword : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "规则名称")]
        [Required(ErrorMessage = "请输入规则名称")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string RuleName { get; set; }

        [Display(Name = "规则编号")]
        public int RuleNo { get; set; }

        [Display(Name = "规则编号")]
        [Required(ErrorMessage = "请输入规则编号")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string FullRuleNo { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        public int? ParentAutoMessage_KeywordID { get; set; }

        public int AccountMainHousesID { get; set; }
        public virtual AccountMainHouses AccountMainHouses { get; set; }

        /// <summary>
        /// 是否是第一条自助回复信息
        /// </summary>
        public bool? IsFistAutoMessage { get; set; }

        public virtual AutoMessage_Keyword ParentAutoMessage_Keyword { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

        public virtual ICollection<KeywordAutoMessage> KeywordAutoMessages { get; set; }

        public virtual ICollection<AutoMessage_Keyword> AutoMessage_KeywordsKeyword { get; set; }

        public virtual ICollection<AutoMessage_User> AutoMessage_Users { get; set; }
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
    /// 关键字回复内容
    /// </summary>
    public class KeywordAutoMessage : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "消息类型")]
        public int EnumMessageTypeID { get; set; }

        public virtual LookupOption EnumMessageType { get; set; }

        [Display(Name = "文字消息")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string TextReply { get; set; }

        /// <summary>
        /// 引用的消息的主键值
        /// </summary>
        public int MessageID { get; set; }

        public int AutoMessage_KeywordID { get; set; }

        public virtual AutoMessage_Keyword AutoMessage_Keyword { get; set; }

        [Display(Name = "排序")]
        public int Order { get; set; }

        /// <summary>
        /// 业务字段，不会再数据库中生成
        /// </summary>
        public string SendTime { get; set; }
    }
}
