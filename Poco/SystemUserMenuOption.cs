using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class SystemUserMenuOption : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int SystemUserMenuID { get; set; }

        public virtual SystemUserMenu SystemUserMenu { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        [Display(Name = "Action")]
        [Required]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Action { get; set; }

        public int Order { get; set; }

        public virtual ICollection<SystemUserRole_Option> SystemUserRole_Options { get; set; }
    }
}