﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Poco
{
    [Serializable]
    public class Account : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Name { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(100)]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
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

        [Display(Name = "电话")]
        [Required(ErrorMessage = "请输入电话")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        [RegularExpression(@"(1[3,5,8][0-9])\d{8}", ErrorMessage = "请输入正确的电话号码。")]
        public string Phone { get; set; }

        [Display(Name = "头像")]
        [StringLength(200, ErrorMessage = "长度小于300")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string HeadImagePath { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入邮箱")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "请输入有效的邮箱")]
        [RemotePlus("CheckIsUniqueAccountEmail", "Ajax", "", "Default", ErrorMessage = "邮箱已存在")]
        public string Email { get; set; }

        [Display(Name = "角色")]
        [RegularExpression(@"\d+", ErrorMessage = "请选择角色")]
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

        /// <summary>
        /// 账号类型，启用禁用
        /// </summary>
        [Display(Name = "状态")]
        public int AccountStatusID { get; set; }

        public virtual LookupOption AccountStatus { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [Display(Name = "是否激活")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentAccountID { get; set; }

        public virtual Account ParentAccount { get; set; }








        /// <summary>
        /// 业务字段，不会在数据库中生成该字段
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 业务字段，不会在数据库中生成该字段
        /// </summary>
        public int CurrentAccountMainID { get; set; }

        /// <summary>
        /// 业务字段，不会在数据库中生成该字段
        /// </summary>
        public string CurrentAccountMainName { get; set; }

        /// <summary>
        /// 业务字段，不会在数据库中生成该字段
        /// </summary>
        public string LogoPath { get; set; }

        /// <summary>
        /// 业务字段，不会在数据库中生成该字段
        /// </summary>
        public string LogoThumbnailPath { get; set; }

        public virtual ICollection<ActivateEmail> ActivateEmails { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<SystemMessage> SystemMessages { get; set; }

        public virtual ICollection<Message> SendMessages { get; set; }

        public virtual ICollection<Message> ReceiveMessages { get; set; }

        public virtual ICollection<Account_AccountMain> Account_AccountMains { get; set; }

        public virtual ICollection<Account_User> Account_User { get; set; }

        public virtual ICollection<PendingMessages> SendPendingMessages { get; set; }

        public virtual ICollection<PendingMessages> ReceivePendingMessages { get; set; }

        public virtual ICollection<PushMsg> PushMsgs { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Task> Tasks_Creater { get; set; }

        public virtual ICollection<Task> Tasks_Receiver { get; set; }

        public virtual ICollection<TaskDetail> TaskDetails { get; set; }

        public virtual ICollection<TaskAccount> TaskAccounts { get; set; }
    }
}
