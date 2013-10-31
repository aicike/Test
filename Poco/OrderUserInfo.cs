using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class OrderUserInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }


        /// <summary>
        /// 省份
        /// </summary>
        public int ProvinceID { get; set; }
        public virtual Province Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public int CityID { get; set; }
        public virtual City City { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public int DistrictID { get; set; }
        public virtual District District { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string RPhone { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string TelePhone { get; set; }

        public virtual ICollection<Order> Order { get; set; }

    }
}
