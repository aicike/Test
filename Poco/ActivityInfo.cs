using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ActivityInfo:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 活动标题
        /// </summary>
        [Display(Name = "活动标题")]
        [Required(ErrorMessage = "请输入活动标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Title { get; set; }

        /// <summary>
        /// 活动备注或描述
        /// </summary>
        [Display(Name = "活动备注或描述")]
        [Required(ErrorMessage = "请输入活动备注或描述")]
        public string Remarks { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 状态 0：开启 1：停用
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }

        public virtual ICollection<ActivityInfoParticipator> ActivityInfoParticipators { get; set; }
    }
}
