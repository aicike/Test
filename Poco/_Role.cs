using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Role : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "角色")]
        [Required(ErrorMessage = "请输入角色")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        public int? ParentRoleID { get; set; }

        public virtual Role ParentRole { get; set; }

        public bool IsCanDelete { get; set; }

        public virtual ICollection<Role> SubRoles { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public virtual ICollection<RoleOption> RoleOptions { get; set; }
    }
}
