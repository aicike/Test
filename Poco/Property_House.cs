using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Property_House : IBaseEntity
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
    }
}
