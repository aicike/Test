using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class UserDeliveryAddress : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

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
        [StringLength(300, ErrorMessage = "长度小于300")]
        public string Address { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [StringLength(10, ErrorMessage = "长度小于10")]
        public string Receiver { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [RegularExpression(@"(1[3,5,8][0-9])\d{8}", ErrorMessage = "请输入正确的移动电话")]
        public string RPhone { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string TelePhone { get; set; }
    }
}
