using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Poco
{
    /// <summary>
    /// 平台账号
    /// </summary>
    [Serializable]
    public class SystemUser : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        public string Name { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "请输入有效的邮箱")]
        [RemotePlus("CheckIsUniqueSystemUserEmail", "Ajax", "", "Default", ErrorMessage = "邮箱已存在")]
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
        
        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请输入确认密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "长度大于6小于20")]
        [RegularExpression("^[a-zA-Z0-9_\u4E00-\u9FA5]*$", ErrorMessage = "请输入有效的密码")]
        [Compare("LoginPwdPage", ErrorMessage = "密码不一致")]
        public string LoginPwdPageCompare { get; set; }

        [Display(Name = "头像")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string HeadImage { get; set; }

        [Display(Name = "电话")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Phone { get; set; }

        /// <summary>
        /// 账号类型，启用禁用
        /// </summary>
        public int AccountStatusID { get; set; }

        public virtual LookupOption AccountStatus { get; set; }

        [Display(Name = "角色")]
        [RegularExpression(@"\d+", ErrorMessage = "请选择角色")]
        public int SystemUserRoleID { get; set; }

        public virtual SystemUserRole SystemUserRole { get; set; }

        public virtual ICollection<AccountMain> AccountMains { get; set; }

        public virtual ICollection<M_TakeOut> M_TakeOuts { get; set; }
    }
}
