using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class OrderMIntermediate : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        /// <summary>
        /// 订单类型名称
        /// </summary>
        public string MTypeName { get; set; }

        /// <summary>
        /// 订购天数
        /// </summary>
        public int MTypeDateCnt { get; set; }
        /// <summary>
        /// 每天配送瓶数
        /// </summary>
        public int MTypeCount { get; set; }

        /// <summary>
        /// 剩余配送天数
        /// </summary>
        public int surplusDay { get; set; }

    }
}
