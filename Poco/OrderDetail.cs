﻿using System;
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
        public virtual Product Product { get; set; }


        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品类型名称
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














    }
}