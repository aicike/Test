using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 报修主表
    /// </summary>
    public class RepairInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }


        /// <summary>
        /// 报修分类 0室内 1公共区域
        /// </summary>
        [Display(Name = "报修分类 0室内 1公共区域")]
        public int RepairType { get; set; }

        /// <summary>
        /// 报修内容
        /// </summary>
        [Display(Name = "报修内容")]
        [Required(ErrorMessage = "请输入报修内容")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string RepairContent { get; set; }

        /// <summary>
        /// 报修人姓名
        /// </summary>
        [Display(Name = "报修人姓名")]
        [Required(ErrorMessage = "请输入报修人姓名")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string RepairName { get; set; }

        /// <summary>
        /// 报修人电话
        /// </summary>
        [Display(Name = "报修人电话")]
        [Required(ErrorMessage = "请输入报修人电话")]
        public string RepairPhone { get; set; }

        /// <summary>
        /// 报修人ID
        /// </summary>
        public int UserID { get; set; }
        public virtual User User { get; set; }


        /// <summary>
        /// 报修日期
        /// </summary>
        [Display(Name = "报修日期")]
        public DateTime RepairDate { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        [Display(Name = "单元")]
        public string Unit{get;set;}

        /// <summary>
        /// 房号
        /// </summary>
        [Display(Name = "房号")]
        public string RoomNumber { get; set; }


        /// <summary>
        /// 报修状态 枚举
        /// </summary>
        [Display(Name = "报修状态")]
        public int EnumRepairStatus { get; set; }

        /// <summary>
        /// 评分 枚举
        /// </summary>
        [Display(Name = "评分 满分5分")]
        public int EnumRepairScore { get; set; }

        /// <summary>
        /// 报修负责人ID
        /// </summary>
        public int? AccountID { get; set; }
        public virtual Account Account { get; set; }

        public virtual ICollection<RepairOperation> RepairOperation { get; set; }
    }
}
