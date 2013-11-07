using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Order
    {
        public int OrderID { get; set; }

        public string OrderNum { get; set; }

        public string OrderStatus { get; set; }

        public int OrderUserInfoID { get; set; }

        public string OrderUserName { get; set; }

        public string OrderUserPhone { get; set; }

        public string OrderUserAddress { get; set; }

        public string OrderDate { get; set; }

        public string BeginDate { get; set; }

        public string DistrictName { get; set; }

        /// <summary>
        /// 配送截止时间
        /// </summary>
        public string EndDate { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        public double TotalPrice { get; set; }

        public string DeliveryType { get; set; }
    }
}
