using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class SMS : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public DateTime SendDate { get; set; }

        public string SendContent { get; set; }

        public string SendPhone { get; set; }

        public int SMS_Status { get; set; }
    }
}
