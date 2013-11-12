using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class Message : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "文字")]
        [Required(ErrorMessage = "请输入文字")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string TextContent { get; set; }

        [Display(Name = "消息发送方向")]
        public int EnumMessageSendDirectionID { get; set; }

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

        [Display(Name = "发送时间")]
        public DateTime SendTime { get; set; }

        [Display(Name = "是否接收")]
        public bool IsReceive { get; set; }

        [Display(Name = "发送时间")]
        public DateTime ReceiveTime { get; set; }
        
        [Display(Name="文件路径")]
        public string FileUrl { get; set; }

        [Display(Name = "语音mp3路径")]
        public string voiceMP3Url { get; set; }

        [Display(Name = "语音 视频 时间长度")]
        public string FileLength { get; set; }

        [Display(Name = "图文ID")]
        public int? LibraryImageTextsID { get; set; }
        public virtual LibraryImageText LibraryImageTexts { get; set; }

        [Display(Name = "会话ID")]
        public int ConversationID { get; set; }
        public virtual Conversation Conversation { get; set; }

        //会话类型 0：单人会话 1：多人会话
        [Display(Name = "会话类型")]
        public int CType { get; set; }

        public virtual ICollection<PendingMessages> PendingMessages { get; set; }

        public virtual ICollection<MessageGroupChat> MessageGroupChat { get; set; }

    }
}
