using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Group : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "组名")]
        [Required(ErrorMessage = "请输入组名")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string GroupName { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Display(Name = "创建者")]
        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        /// <summary>
        /// 所属售楼部
        /// </summary>
        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        public bool IsDefaultGroup { get; set; }

        /// <summary>
        /// 是否可以删除，默认创建的分组不能删除
        /// </summary>
        public bool IsCanDelete { get; set; }

        public virtual ICollection<User_Group> User_Groups { get; set; }
    }
}
