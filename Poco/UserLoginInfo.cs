using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class UserLoginInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "地址")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Address { get; set; }

        [Display(Name = "电话")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Phone { get; set; }

        [Display(Name = "头像")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string HeadImagePath { get; set; }

        [Display(Name = "证件号码")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string IdentityCard { get; set; }

        [Display(Name = "邮箱")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "请输入有效的邮箱")]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(100)]
        public string LoginPwd { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "长度大于6小于20")]
        [RegularExpression("^[a-zA-Z0-9_\u4E00-\u9FA5]*$", ErrorMessage = "请输入有效的密码")]
        public string LoginPwdPage { get; set; }

        /// <summary>
        /// 找回密码URL
        /// </summary>
        public string FindPwdCode { get; set; }

        public DateTime? FindPwdTime { get; set; }

        public bool? FindPwdValidity { get; set; }

        /// <summary>
        /// 业务字段，不会再数据库中生成
        /// （一个userloginInfo对应多个User,但一个APP登录只绑定一个跟AccountMain关联的信息）
        /// </summary>
        public User CurrenRelatedUser { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
