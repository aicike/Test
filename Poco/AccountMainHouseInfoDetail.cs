using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 房间信息
    /// </summary>
    [Serializable]
    public class AccountMainHouseInfoDetail : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainHouseInfoID { get; set; }
        public virtual AccountMainHouseInfo AccountMainHouseInfo { get; set; }

        [Display(Name = "楼层")]
        public int Layer { get; set; }

        [Display(Name = "房间号")]
        [Required(ErrorMessage = "请输入房间号")]
        public string RoomNumber { get; set; }

        [Display(Name = "户型")]
        [Required(ErrorMessage="请选择户型")]
        [Range(1, double.MaxValue, ErrorMessage = "请选择户型")]
        public int AccountMainHouseTypeID { get; set; }
        public virtual AccountMainHouseType AccountMainHouseType { get; set; }
        
        [Display(Name = "建筑面积")]
        public double BuildingArea { get; set; }

        [Display(Name = "实际面积")]
        public double RealityArea { get; set; }

        [Display(Name = "公摊比例")]
        public double GongTan { get; set; }

        
        [Display(Name = "售出状态")]
        public int EnumSoldStateID { get; set; }
        public virtual LookupOption EnumSoldState { get; set; }


        [Display(Name = "单价")]
        public decimal Price { get; set; }

        [Display(Name = "总价")]
        public decimal TotalPrice { get; set; }
    }
}
