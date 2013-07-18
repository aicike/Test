using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class Account_AccountMain : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }
    }
}
