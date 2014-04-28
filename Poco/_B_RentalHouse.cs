using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class _B_RentalHouse
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 月租
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 户型
        /// </summary>
        public string HouseType { get; set; }

        /// <summary>
        /// 装修
        /// </summary>
        public string Decoration { get; set; }

        /// <summary>
        /// 详细信息URL
        /// </summary>
        public string URL { get; set; }
    }
}
