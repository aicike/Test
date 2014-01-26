using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class AccountMain_Service : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }

        public AccountMain AccountMain { get; set; }

        public int ServiceID { get; set; }

        public Service Service { get; set; }
    }
}
