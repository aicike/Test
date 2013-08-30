using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class AutoMessage_User : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int UserID { get; set; }

        public DateTime SendTime { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        public int? AutoMessage_KeywordID { get; set; }

        public virtual AutoMessage_Keyword AutoMessage_Keyword { get; set; }

        public string Question { get; set; }
    }
}
