using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// web端通知
    /// </summary>
    public class WebNotice : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int? MenuID { get; set; }

        public virtual Menu Menu { get; set; }

        public int Count { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
