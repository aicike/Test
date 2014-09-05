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
        [Required(ErrorMessage = "请输入停车费")]
        public double ParkingFees { get; set; }

        /// <summary>
        /// 是否已缴费
        /// </summary>
        [Display(Name = "是否已缴费")]
        public bool IsPay { get; set; }


        /// <summary>
        /// 导入日期
        /// </summary>
        [Display(Name = "导入日期")]
        public DateTime importDate { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Required(ErrorMessage = "请输入车牌号")]
        public string plates { get; set; }


        /// <summary>
        /// 楼号
        /// </summary>
        [Display(Name = "楼号")]
        [Required(ErrorMessage = "请输入楼号")]
        public string BuildingNum { get; set; }

        /// <summary>
        /// 缩写
        /// </summary>
        [Display(Name = "缩写")]
        [Required(ErrorMessage = "请输入缩写")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// 停车类型
        /// </summary>
        [Display(Name = "停车类型")]
        [Required(ErrorMessage = "请输入停车类型")]
        public string ParkingType { get; set; }
        
        
        public virtual ICollection<PropertyOrderDetail> PropertyOrderDetail { get; set; }


    }
}
