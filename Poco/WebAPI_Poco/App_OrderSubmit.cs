using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_OrderSubmit
    {
        #region 订单信息

        public int AccountMainID { get; set; }

        public int OrderUserID { get; set; }

        /// <summary>
        /// 下单人类型 1销售 2用户
        /// </summary>
        public int OrderUserType { get; set; }

        /// <summary>
        /// 配送开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }

        public int ProductID { get; set; }

        public int Count { get; set; }

        public int OrderMTypeID { get; set; }

        public int DeliveryType { get; set; }

        #endregion

        #region 订单人收货信息

        /// <summary>
        /// 收货人ID
        /// </summary>
        public int OrderUserInfoID { get; set; }

        public int ProvinceID { get; set; }

        public int CityID { get; set; }

        public int DistrictID { get; set; }

        public string Address { get; set; }

        public string Receiver { get; set; }

        public string RPhone { get; set; }

        public string TelePhone { get; set; }

        #endregion

    }
}
