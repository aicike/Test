using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class PropertyOrderDetail : IBaseEntity
    {

        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 物业费ID
        /// </summary>
        public int? PropertyFeeInfoID { get; set; }
        public virtual PropertyFeeInfo PropertyFeeInfo { get; set; }

        /// <summary>
        /// 费用标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }

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
