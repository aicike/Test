using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class PushMsgDetail : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int PushMsgID { get; set; }

        public virtual PushMsg PushMsg { get; set; }

        /// <summary>
        /// 接受者类型
        /// </summary>
        public int EnumClientUserType { get; set; }
        
        /// <summary>
        /// 接收者ID
        /// </summary>
        public int ReceiveID { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 接收状态
        /// </summary>
        public int EnumReceiveStatus { get; set; }
    }
}
