using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class OrderMIntermediate : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int OrderID { get; set; }
        public virtual Order Order { get; set; }


        public string MTypeName { get; set; }

        public int MTypeDateCnt { get; set; }

        public int MTypeCount { get; set; }

    }
}
