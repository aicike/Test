using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 售楼部-用户表
    /// </summary>
    [Serializable]
    public class User : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "备注名称")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        public string Name { get; set; }

        [Display(Name = "电话")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Phone { get; set; }

        [Display(Name = "性别")]
        public int? SEX { get; set; }

        /// <summary>
        /// 0:女，1:男 ，2：保密
        /// </summary>
        [Display(Name = "年龄")]
        public int? Age { get; set; }

        [Display(Name = "身份证")]
        [StringLength(18, ErrorMessage = "长度小于18")]
        public string IDCard { get; set; }


        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string Address { get; set; }
        
        

        /// <summary>
        /// 账号类型，启用禁用
        /// </summary>
        public int AccountStatusID { get; set; }

        public virtual LookupOption AccountStatus { get; set; }

        [Display(Name = "证件号码")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string IdentityCard { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }


        public int? UserTagID { get; set; }
        public virtual UserTag UserTag { get; set; }

        /// <summary>
        /// 业务字段，不会再数据库中生成
        /// 当前被哪个AccountID操作
        /// </summary>
        public int Business_AccountID { get; set; }

        /// <summary>
        /// 业务字段，不会再数据库中生成
        /// （一个用户可以被多个Account所维护，也就是说会有多个不同Account创建的分组关系，但一个Account控制的
        /// user只能有一个分组信息，该字段标示目前Account所创建的GroupID,并隶属于该GroupID）
        /// </summary>
        public int? Business_GroupID { get; set; }

        public int UserLoginInfoID { get; set; }



        public virtual UserLoginInfo UserLoginInfo { get; set; }

        public virtual ICollection<SystemMessage> SystemMessages { get; set; }

        public virtual ICollection<Message> SendMessages { get; set; }

        public virtual ICollection<Message> ReceiveMessages { get; set; }

        public virtual ICollection<Account_User> Account_Users { get; set; }

        public virtual ICollection<PendingMessages> SendPendingMessages { get; set; }

        public virtual ICollection<PendingMessages> ReceivePendingMessages { get; set; }

        public virtual ICollection<VIPInfo> VIPInfo { get; set; }

        public virtual ICollection<VIPInfoExpenseDetail> VIPInfoExpenseDetails { get; set; }

        public virtual ICollection<UserDeliveryAddress> UserDeliveryAddress { get; set; }
        
    }
}
