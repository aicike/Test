using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class PayInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        [Display(Name = "支付宝账号")]
        [Required(ErrorMessage = "请输入支付宝账号")]
        public string PayAccount { get; set; }

        /// <summary>
        /// 合作者身份ID
        /// </summary>
        [Display(Name = "合作者身份ID")]
        [Required(ErrorMessage = "请输入合作者身份ID")]
        public string IdentityID { get; set; }

        /// <summary>
        /// 商户私钥
        /// </summary>
        [Display(Name = "商户私钥")]
        [Required(ErrorMessage = "请输入商户私钥")]
        public string MerchantRivateKey { get; set; }

        /// <summary>
        /// 支付宝公钥
        /// </summary>
        [Display(Name = "支付宝公钥")]
        [Required(ErrorMessage = "请输入支付宝公钥")]
        public string PayPublicKey { get; set; }
    }
}
