using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ComplaintReply : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }
         
        public int ComplaintID { get; set; }
        public virtual Complaint Complaint { get; set; }

        /// <summary>
        /// 答复内容
        /// </summary>
        [Display(Name = "答复内容")]
        public string ReplyContent { get; set; }

        /// <summary>
        /// 答复时间
        /// </summary>
        [Display(Name = "答复时间")]
        public DateTime ReplyDate { get; set; }

        /// <summary>
        /// 答复人
        /// </summary>
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }
    }
}
