using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Message : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "文字")]
        [Required(ErrorMessage = "请输入文字")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string TextContent { get; set; }

        [Display(Name = "消息发送方向")]
        public int EnumMessageSendDirectionID { get; set; }
        public virtual LookupOption EnumMessageSendDirection { get; set; }

        [Display(Name = "消息类型")]
        public int EnumMessageTypeID { get; set; }
        public virtual LookupOption EnumMessageType { get; set; }

        /// <summary>
        /// 引用的消息的主键值(可以是文本库，音频，视频，图文)
        /// </summary>
        public int MessageObjID { get; set; }

        /// <summary>
        /// 消息对象，需要修改get方法
        /// </summary>
        public IBaseEntity MessageObj { get; private set; }

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

        [Display(Name = "发送时间")]
        public DateTime SendTime { get; set; }

        [Display(Name = "是否接收")]
        public bool IsReceive { get; set; }

        [Display(Name = "发送时间")]
        public DateTime ReceiveTime { get; set; }
    }
}
