using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Pay
    {
        public int ID { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string PayAccount { get; set; }

        /// <summary>
        /// 合作者身份ID
        /// </summary>
        public string IdentityID { get; set; }

        /// <summary>
        /// 商户私钥
        /// </summary>
        public string MerchantRivateKey { get; set; }

        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public string PayPublicKey { get; set; }
    }
}
