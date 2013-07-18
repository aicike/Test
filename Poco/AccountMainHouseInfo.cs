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
    public class AccountMainHouseInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        [Display(Name = "层数")]
        public int NumberOfLayers { get; set; }

        [Display(Name = "电梯数")]
        public int NumberOfTheElevator { get; set; }

        [Display(Name = "每层户数")]
        public int NumberOfFamily { get; set; }

        public virtual ICollection<AccountMainHouseInfoDetail> AccountMainHouseInfoDetails { get; set; }
    }
}
