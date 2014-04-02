using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 单元
    /// </summary>
    [Serializable]
    public class AccountMainHouseInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int? AccountMainHousessID { get; set; }
        public virtual AccountMainHouses AccountMainHousess { get; set; }

        [Display(Name = "楼号")]
        [Required(ErrorMessage = "请输入楼号")]
        public string Building { get; set; }

        [Display(Name = "单元")]
        [Required(ErrorMessage = "请输入单元")]
        public string Cell { get; set; }

        [Display(Name = "层数")]
        public int NumberOfLayers { get; set; }

        [Display(Name = "电梯数")]
        public int NumberOfTheElevator { get; set; }

        [Display(Name = "户数")]
        [Required(ErrorMessage = "请输入户数")]
        public string NumberOfFamily { get; set; }

        public virtual ICollection<AccountMainHouseInfoDetail> AccountMainHouseInfoDetails { get; set; }

        public virtual ICollection<Property_House> Property_Houses { get; set; }
    }
}
