using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class _B_PropertyFee
    {
        /// <summary>
        /// ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// AMID
        /// </summary>
        public int AMID { get; set; }

        /// <summary>
        /// 缴费月份
        /// </summary>
        public string PayDate { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// 是否已缴费
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        public string RoomNumber { get; set; }

        /// <summary>
        /// 管理费
        /// </summary>
        public double ManagerFee { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>
        public double ServiceFee { get; set; }

        /// <summary>
        /// 电梯费
        /// </summary>
        public double ElevatorFee { get; set; }

        /// <summary>
        /// 水费
        /// </summary>
        public double WaterFee { get; set; }

        /// <summary>
        /// 卫生费
        /// </summary>
        public double HealthFee { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        public double OrterFee { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string plates { get; set; }

        /// <summary>
        /// 楼号
        /// </summary>
        public string BuildingNum { get; set; }
    }
}
