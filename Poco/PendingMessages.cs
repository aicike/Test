using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class PendingMessages : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        /// <summary>
        /// 消息方向
        /// </summary>
        public string MSD { get; set; }

        [Display(Name = "消息类型")]
        public int EnumMessageTypeID { get; set; }

        /// <summary>
        /// （发送）售楼部账号
        /// </summary>
        public int? FromAccountID { get; set; }
        public virtual Account FromAccount { get; set; }

        /// <summary>
        /// （发送）购房者账号
        /// </summary>
        public int? FromUserID { get; set; }
        public virtual User FromUser { get; set; }

        /// <summary>
        /// （接收）售楼部账号
        /// </summary>
        public int? ToAccountID { get; set; }
        public virtual Account ToAccount { get; set; }

        /// <summary>
        /// （接收）购房者账号
        /// </summary>
        public int? ToUserID { get; set; }
        public virtual User ToUser { get; set; }

        //[Display(Name = "发送时间")]
        public DateTime SendTime { get; set; }

        //[Display(Name = "发送内容")]
        public string Content { get; set; }

        [Display(Name = "文件路径")]
        public string FileUrl { get; set; }

        /// <summary>
        /// 对应的消息ID
        /// </summary>
        public int MessageID { get; set; }
        public virtual Message Message { get; set; }

        [Display(Name = "图文ID")]
        public int? LibraryImageTextsID { get; set; }
        public virtual LibraryImageText LibraryImageTexts { get; set; } 

    }
}
