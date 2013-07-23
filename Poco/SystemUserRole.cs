using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class SystemUserRole : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "角色")]
        [Required(ErrorMessage = "请输入角色")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        public int? ParentSystemUserRoleID { get; set; }

        public virtual SystemUserRole ParentSystemUserRole { get; set; }

        public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; }

        public virtual ICollection<SystemUser> SystemUsers { get; set; }

        public virtual ICollection<SystemUserRole_Option> SystemUserRole_Options { get; set; }

        public virtual ICollection<SystemUserRole_SystemUserMenu> SystemUserRole_SystemUserMenus { get; set; }
    }
}
