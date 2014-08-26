using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class ExpressNotice : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccounMain { get; set; }

        public string Notice { get; set; }
    }
}
