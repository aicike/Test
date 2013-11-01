using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Order
    {
        public int ID { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 下单日期
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// 送货开始日期
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 送货截止日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 订单人收货信息
        /// </summary>
        public int OrderUserInfoID { get; set; }

        /// <summary>
        /// 订单人名称
        /// </summary>
        public string OrderUserInfoName { get; set; }

        /// <summary>
        /// 订单人电话
        /// </summary>
        public string OrderUserInfoPhone { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 总价钱
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 表单状态 枚举
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 送货类型 枚举
        /// </summary>
        public int DeliveryType { get; set; }
    }
}
