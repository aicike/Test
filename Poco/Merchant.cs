using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Poco
{
    /// <summary>
    /// 商户
    /// </summary>
    public class Merchant : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "商户名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [Required(ErrorMessage = "请输入商户名称")]
        public string Name { get; set; }

        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string Address { get; set; }

        [Display(Name = "电话（用于登陆）")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        [Required(ErrorMessage = "请输入电话")]
        public string Phone { get; set; }

        [Display(Name = "Logo")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string LogoImagePath { get; set; }

        [Display(Name = "显示logo")]
        public string LogoShow { get; set; }



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
        [Compare("LoginPwd", ErrorMessage = "密码不一致")]
        public string LoginPwdPage { get; set; }


    }
}
