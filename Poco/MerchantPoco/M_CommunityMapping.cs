using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.MerchantPoco;

namespace Poco.MerchantPoco
{
    /// <summary>
    /// 商户发布信息和小区映射表
    /// </summary>
    public class M_CommunityMapping : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 小区ID
        /// </summary>
        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 周边外卖
        /// </summary>
        public int? M_TakeOutID { get; set; }
        public virtual M_TakeOut M_TakeOut { get; set; }
    }
}
