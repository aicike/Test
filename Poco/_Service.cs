using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Service : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [Display(Name = "服务")]
        [Required(ErrorMessage = "请输入服务名称")]
        [StringLength(100, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        /// <summary>
        /// 该服务是否是独立站点
        /// </summary>
        public bool IsIndependentSite { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<AccountMain_Service> AccountMain_Services { get; set; }

    }
}
