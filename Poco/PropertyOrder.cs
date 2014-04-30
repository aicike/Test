using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class PropertyOrder : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccounMain { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 下单人ID
        /// </summary>
        public int OrderUserID { get; set; }

        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? Payment { get; set; }

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
        /// 物业订单类型
        /// </summary>
        public int EnumPropertyOrderType { get; set; }


        public virtual ICollection<PropertyOrderDetail> PropertyOrderDetail { get; set; }
    }
}
