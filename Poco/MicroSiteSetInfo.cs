using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class MicroSiteSetInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 用户下单，是否需要邮件通知
        /// </summary>
        public bool IsNeedSendEmail_Order { get; set; }

        /// <summary>
        /// 用户付款，是否需要邮件通知
        /// </summary>
        public bool IsNeedSendEmail_Pay { get; set; }

        /// <summary>
        /// 用户取消订单，是否需要邮件通知
        /// </summary>
        public bool IsNeedSendEmail_CancelOrder { get; set; }
    }
}
