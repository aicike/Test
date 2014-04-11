using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Complaint : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 所属客户
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }


        /// <summary>
        /// 投诉人ID
        /// </summary>
        public int? UserID { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// 是否匿名
        /// </summary>
        [Display(Name = "是否匿名")]
        public bool IsAnonymous { get; set; }


        /// <summary>
        /// 投诉状态
        /// </summary>
        [Display(Name = "投诉状态")]
        public int EnumComplaintStatus { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        [Display(Name = "评分")]
        public int EnumRepairScore { get; set; }

        /// <summary>
        /// 投诉内容
        /// </summary>
        [Display(Name = "投诉内容")]
        public string ComplaintContetn { get; set; }

        /// <summary>
        /// 投诉时间
        /// </summary>
        [Display(Name = "投诉时间")]
        public DateTime ComplaintDate { get; set; }

        public virtual ICollection<ComplaintReply> ComplaintReply { get; set; }
    }
}
