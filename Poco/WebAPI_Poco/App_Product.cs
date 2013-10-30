using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Product
    {
        public int ID { get; set; }

        public string imgFilePath { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
