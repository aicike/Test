using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.Enum;

namespace Poco
{
    public class PushMsg : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 文本推送
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int EnumMessageType { get; set; }

        /// <summary>
        /// 推送消息对象
        /// </summary>
        public int? LibraryID { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime PushTime { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        public int PushStatus { get; set; }

        /// <summary>
        /// 推送类型，全部或指定
        /// </summary>
        public int EnumPushType { get; set; }

        /// <summary>
        /// 隶属售楼部
        /// </summary>
        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 推送者
        /// </summary>
        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<PushMsgDetail> PushMsgDetails { get; set; }

        public string GetPushStatus()
        {
            string value = null;
            switch (PushStatus)
            {
                case (int)EnumPushStatus.Sending:
                    value = "正在发送";
                    break;
                case (int)EnumPushStatus.Fail:
                    value = "发送失败";
                    break;
                case (int)EnumPushStatus.Success:
                    value = "发送成功";
                    break;
            }
            return value;
        }

        /// <summary>
        /// 业务字段，不会在数据库生成
        /// </summary>
        public string HtmlShow { get; set; }
    }
}
