using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_OrderMType
    {
        public int ID { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 订购总天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 每天配送瓶数
        /// </summary>
        public int CountPerDay { get; set; }
    }
}
