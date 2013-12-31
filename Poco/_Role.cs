using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Obsolete("表更改 请检查接口")]
    [Serializable]
    public class Role : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "角色")]
        [Required(ErrorMessage = "请输入角色")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Name { get; set; }

        public int? ParentRoleID { get; set; }

        public virtual Role ParentRole { get; set; }

        public bool IsCanDelete { get; set; }

        /// <summary>
        /// 是否可以被用户App查询
        /// </summary>
        public bool? IsCanFindByUser { get; set; }

        [Display(Name = "Token")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Token { get; set; }

        public virtual AccountMain AccountMain { get; set; }
        public int AccountMainID { get; set; }

        public virtual ICollection<Role> SubRoles { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public virtual ICollection<RoleOption> RoleOptions { get; set; }

        public virtual ICollection<Account_Role> Account_Roles { get; set; }
    }
}
