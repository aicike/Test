using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class TemporayInstantMes : IBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int FromUserID { get; set; }
        /// <summary>
        /// 最后聊天时间
        /// </summary>
        public DateTime MData { get; set; }
        /// <summary>
        /// 未读邮件数
        /// </summary>
        public int NoSend { get; set; }
        /// <summary>
        /// 最后聊天内容
        /// </summary>
        public string Tcontent { get; set; }
        /// <summary>
        /// 姓名备注
        /// </summary>
        public string UMark { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadImagePath { get; set; }



        public int ID { get; set; }

        public int SystemStatus { get; set; }
    }
}
