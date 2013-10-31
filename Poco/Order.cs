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
        /// 订单人收货信息
        /// </summary>
        public int OrderUserInfoID { get; set; }
        public virtual OrderUserInfo OrderUserInfo { get; set; }

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
