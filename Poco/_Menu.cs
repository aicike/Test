using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Menu : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Name { get; set; }

        [Display(Name = "Area")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Area { get; set; }

        [Display(Name = "Controller")]
        [Required]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Controller { get; set; }

        [Display(Name = "Action")]
        [Required]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Action { get; set; }

        public int Order { get; set; }

        public int? ParentMenuID { get; set; }

        public virtual Menu ParentMenu { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<MenuOption> MenuOptions { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
