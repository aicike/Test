using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class OrderDetail : IBaseEntity
    {

        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }


        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string ProductImg { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; } 

        /// <summary>
        /// 价钱
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }
    }
}
