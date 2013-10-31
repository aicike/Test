using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class Order : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNum { get; set; }

      


        /// <summary>
        /// 下单人ID
        /// </summary>
        public int OrderUserID { get; set; }

        /// <summary>
        /// 下单人类型 1销售 2用户
        /// </summary>
        public int OrderUserType { get; set; }

        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 送货开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 送货截止日期
        /// </summary>
        public DateTime EndDate { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 总价钱
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 表单状态 枚举
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 送货类型 枚举
        /// </summary>
        public int DeliveryType { get; set; }



        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual ICollection<OrderMIntermediate> OrderMIntermediate { get; set; }

    }
}
