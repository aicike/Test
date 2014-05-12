using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.MerchantPoco
{
    public class M_TakeOutDetail:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int M_TakeOutID { get; set; }

        public virtual M_TakeOut M_TakeOut { get; set; }
    }
}
