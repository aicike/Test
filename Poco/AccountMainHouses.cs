using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AccountMainHouses : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }


        [Display(Name = "楼盘名称")]
        [Required(ErrorMessage = "请输入楼盘名称")]
        [RemotePlus("CheckHousesRepeat", "Ajax", "", "Default", ErrorMessage = "名称已存在")]
        public string HName { get; set; }

        [Display(Name = "楼盘介绍")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        public string HIntroduce { get; set; }

        [Display(Name = "楼盘地址")]
        [Required(ErrorMessage = "请输入楼盘地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string HAddress { get; set; }

        [Display(Name = "单元数")]
        public int HHouseCount { get; set; }

        [Display(Name = "总户数")]
        public int? HHouseholdsCount { get; set; }

        [Display(Name = "车位数")]
        public int HParkingCount { get; set; }

        [Display(Name = "开盘时间")]
        [Required(ErrorMessage = "请输入开盘时间")]
        public DateTime HOpeningDate { get; set; }

        [Display(Name = "入住时间")]
        [Required(ErrorMessage = "请输入入住时间")]
        public DateTime HCheckInDate { get; set; }

        [Display(Name = "竣工时间")]
        [Required(ErrorMessage = "请输入竣工时间")]
        public DateTime HCompletedDate { get; set; }

        [Display(Name = "占用面积")]
        public double? HOccupyArea { get; set; }

        [Display(Name = "建筑面积")]
        public double? HBuildingArea { get; set; }

        [Display(Name = "绿化面积")]
        public double? HGreeningArea { get; set; }

        [Display(Name = "产权年限")]
        [Required(ErrorMessage = "请输入产权年限 ")]
        [Range(1, 500, ErrorMessage = "请输入正确的产权年限")]
        public int PropertyRight { get; set; }

        [Display(Name = "开发商")]
        [Required(ErrorMessage = "请输入开发商")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string HBuildCompany { get; set; }

        [Display(Name = "投资商")]
        [Required(ErrorMessage = "请输入投资商")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string HInvestor { get; set; }


        public virtual ICollection<AccountMainHouseInfo> AccountMainHouseInfos { get; set; }
    }
}
