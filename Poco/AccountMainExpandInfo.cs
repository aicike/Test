using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 扩展信息表（不同业务字段）
    /// </summary>
    public class AccountMainExpandInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccounMain { get; set; }

        [Display(Name = "项目地址")]
        [Required(ErrorMessage = "请输入项目地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string ProductAddress { get; set; }

        [Display(Name = "售楼地址")]
        [Required(ErrorMessage = "请输入售楼地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string SaleAddress { get; set; }

        [Display(Name = "开盘时间")]
        [Required(ErrorMessage = "请输入开盘时间")]
        public DateTime OpeningDate { get; set; }

        [Display(Name = "入住时间")]
        [Required(ErrorMessage = "请输入入住时间")]
        public DateTime CheckInDate { get; set; }

        [Display(Name = "竣工时间")]
        [Required(ErrorMessage = "请输入竣工时间")]
        public DateTime CompletedDate { get; set; }

        [Display(Name = "开发商")]
        [Required(ErrorMessage = "请输入开发商")]
        [StringLength(100, ErrorMessage = "长度小于100")]
        public string BuildCompany { get; set; }
        
        [Display(Name = "投资商")]
        [Required(ErrorMessage = "请输入投资商")]
        [StringLength(100, ErrorMessage = "长度小于100")]
        public string Investor { get; set; }
        

        [Display(Name = "项目介绍")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string Description { get; set; }

        [Display(Name = "项目配套")]
        [StringLength(2000, ErrorMessage = "长度小于4000")]
        public string ProjectSupport { get; set; }

        [Display(Name = "交通状况")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        public string TrafficInfo { get; set; }

        [Display(Name = "建材装修")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        public string BuildingMaterials { get; set; }

        [Display(Name = "楼层概述")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        public string FloorInfo { get; set; }

        [Display(Name = "车位信息")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        public string StallInfo { get; set; }

        [Display(Name = "占用面积")]
        public double? OccupyArea { get; set; }

        [Display(Name = "建筑面积")]
        public double? BuildingArea { get; set; }

        [Display(Name = "工程进度")]
        [StringLength(4000, ErrorMessage = "长度小于4000")]
        public string ProjectSchedule { get; set; }

        [Display(Name = "产权年限")]
        [Required(ErrorMessage = "请输入产权年限 ")]
        [Range(1,500,ErrorMessage="请输入正确的产权年限")]
        public int PropertyRight { get; set; }

        [Display(Name = "总户数")]
        public int? HouseholdsCount { get; set; }

    }
}
