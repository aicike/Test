using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 项目表
    /// </summary>
    public class AccountMainHouses : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        [Display(Name = "项目名称")]
        [Required(ErrorMessage = "请输入项目名称")]
        [RemotePlus("CheckHousesRepeat", "Ajax", "", "Default", ErrorMessage = "名称已存在")]
     
        public string HName { get; set; }

        [Display(Name = "项目介绍")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        [Required(ErrorMessage = "请输入项目介绍")]
        //[RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string HIntroduce { get; set; }

        [Display(Name = "项目地址")]
        [Required(ErrorMessage = "请输入楼盘地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string HAddress { get; set; }

        [Display(Name = "栋数")]
        [RegularExpression(@"\d+", ErrorMessage = "请输入数字")]
        public int HHouseCount { get; set; }

        [Display(Name = "总户数")]
        [RegularExpression(@"\d+", ErrorMessage = "请输入数字")]
        public int HHouseholdsCount { get; set; }

        [Display(Name = "车位数")]
        [RegularExpression(@"\d+", ErrorMessage = "请输入数字")]
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
        public string HOccupyArea { get; set; }

        [Display(Name = "建筑面积")]
        public string HBuildingArea { get; set; }

        [Display(Name = "绿化面积")]
        public string HGreeningArea { get; set; }

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

        [Display(Name = "建筑类别")]
        [Required(ErrorMessage = "请选择建筑类别")]
        public string BuildingType { get; set; }

        [Display(Name = "装修情况")]
        [Required(ErrorMessage = "请选择装修情况")]
        public string Decoration { get; set; }

        [Display(Name = "交通状况")]
        [Required(ErrorMessage = "请输入交通状况")]
        [StringLength(1000, ErrorMessage = "长度小于1000")]
        public string Traffic { get; set; }

        [Display(Name = "物业公司")]
        [Required(ErrorMessage = "请输入物业公司")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string ProPertyCom { get; set; }


        [Display(Name = "物业费")]
        [Required(ErrorMessage = "请输入物业费")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string PropetyFee { get; set; }


        [Display(Name = "预售证")]
        [Required(ErrorMessage = "请输入预售证")]
        [StringLength(2000, ErrorMessage = "长度小于2000")]
        public string PreSalePermit { get; set; }

        /// <summary>
        /// 销售状态 0 在售 1已售完
        /// </summary>
        [Display(Name = "销售状态")]
        [Required(ErrorMessage = "请选择销售状态")]
        public bool SalesState { get; set; }

        [Display(Name = "售楼电话")]
        [Required(ErrorMessage = "请输入售楼电话")]
        public string TelSales { get; set; }

        public virtual ICollection<AccountMainHouseInfo> AccountMainHouseInfos { get; set; }

        public virtual ICollection<AccountMainHouseType> AccountMainHouseTypes { get; set; }

        public virtual ICollection<AutoMessage_Keyword> AutoMessage_Keywords { get; set; }

        public virtual ICollection<AccountMainHouseInfoDetail> AccountMainHouseInfoDetails { get; set; }
        
    }
}
