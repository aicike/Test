using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class MenuOption : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int MenuID { get; set; }

        public virtual Menu Menu { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        [Display(Name = "Action")]
        [Required]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Action { get; set; }

        public int Order { get; set; }

        public virtual ICollection<RoleOption> RoleOptions { get; set; }
    }
}
