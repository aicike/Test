﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 物业费信息
    /// </summary>
    public class PropertyFeeInfo : IBaseEntity
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
        /// 管理费
        /// </summary>
        [Display(Name = "管理费")]
        public double? ManagerFee { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>
        [Display(Name = "服务费")]
        public double? ServiceFee { get; set; }

        /// <summary>
        /// 停车费
        /// </summary>
        [Display(Name = "停车费")]
        public double? ParkingFee { get; set; }

        /// <summary>
        /// 电梯费
        /// </summary>
        [Display(Name = "电梯费")]
        public double? ElevatorFee { get; set; }

        /// <summary>
        /// 水费
        /// </summary>
        [Display(Name = "水费")]
        public double? WaterFee { get; set; }

        /// <summary>
        /// 卫生费
        /// </summary>
        [Display(Name = "卫生费")]
        public double? HealthFee { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        [Display(Name = "其他费用")]
        public double? OrterFee { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        [Display(Name = "合计金额")]
        public double? Total { get; set; }

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

    }
}