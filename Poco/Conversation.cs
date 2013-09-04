using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class Conversation : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        //售楼部ID
        [Display(Name = "售楼部")]
        public string AccountMainID { get; set; }

        //AccountUser 时 存的AccountID 
        [Display(Name = "用户ID1")]
        public string User1ID { get; set; }

        //AccountUser 时 存的UserID 
        [Display(Name = "用户ID2")]
        public string User2ID { get; set; }

        /// <summary>
        /// 消息分类 0 ：AccountUser，1：Account ，2：User
        /// </summary>
        [Display(Name="消息分类")]
        public string Ctype { get; set; }


        public virtual ICollection<Message> Message { get; set; }

        public virtual ICollection<PendingMessages> PendingMessages { get; set; }

    }
}
