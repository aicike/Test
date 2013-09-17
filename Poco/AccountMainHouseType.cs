using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class AccountMainHouseType : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "户型名称")]
        [Required(ErrorMessage = "请输入户型名称")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string HName { get; set; }

        [Display(Name = "户型图")]
        [Required(ErrorMessage = "请上传户型图")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string HouseTypeImagePath { get; set; }

        [Display(Name = "户型说明")]
        [Required(ErrorMessage = "请输入户型说明")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string HouseTypeDescription { get; set; }

        [Display(Name = "所属楼盘")]
        public int AccountMainHousesID { get; set; }
        public virtual AccountMainHouses AccountMainHouses { get; set; }

        public virtual ICollection<AccountMainHouseInfoDetail> AccountMainHouseInfoDetails { get; set; }
    }
}
