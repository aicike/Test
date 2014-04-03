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
    public class Property_House : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int? AccountMainHouseInfoID { get; set; }
        public virtual AccountMainHouseInfo AccountMainHouseInfo { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 楼号
        /// </summary>
        [Display(Name = "楼号")]
        [Required(ErrorMessage = "请输入楼号")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string BuildingNum { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        [Display(Name = "单元")]
        [Required(ErrorMessage = "请输入单元")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string CellNum { get; set; }


        [Display(Name = "房间号")]
        [Required(ErrorMessage = "请输入房间号")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string RoomNumber { get; set; }

        public virtual ICollection<Property_User> Property_Users { get; set; }
    }
}
