using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 停车费
    /// </summary>
    public class ParkingFee : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 业主姓名
        /// </summary>
        [Display(Name = "业主姓名")]
        [Required(ErrorMessage = "请输入业主姓名")]
        public string OwnerName { get; set; }

        /// <summary>
        /// 业主电话
        /// </summary>
        [Display(Name = "业主电话")]
        [Required(ErrorMessage = "请输入业主电话")]
        public string OwnerPhone { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        [Display(Name = "单元")]
        [Required(ErrorMessage = "请输入单元")]
        public string Unit { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        [Display(Name = "房号")]
        [Required(ErrorMessage = "请输入房号")]
        public string RoomNumber { get; set; }

        /// <summary>
        /// 缴费月份
        /// </summary>
        [Display(Name = "缴费月份")]
        [Required(ErrorMessage = "请输入缴费月份")]
        public string PayDate { get; set; }

        /// <summary>
        /// 停车费
        /// </summary>
        [Display(Name = "停车费")]
        public double ParkingFees { get; set; }

        /// <summary>
        /// 是否已缴费
        /// </summary>
        [Display(Name = "是否已缴费")]
        public bool IsPay { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remarks { get; set; }

        /// <summary>
        /// 导入日期
        /// </summary>
        [Display(Name = "导入日期")]
        public DateTime importDate { get; set; }

        
        public virtual ICollection<PropertyOrderDetail> PropertyOrderDetail { get; set; }


    }
}
