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

        [Display(Name = "户型图")]
        [Required(ErrorMessage = "请上传户型图")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string HouseTypeImagePath { get; set; }

        [Display(Name = "户型说明")]
        [Required(ErrorMessage = "请输入户型说明")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string HouseTypeDescription { get; set; }

        [Display(Name = "所属售楼部")]
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        public virtual ICollection<AccountMainHouseInfoDetail> AccountMainHouseInfoDetails { get; set; }
    }
}
