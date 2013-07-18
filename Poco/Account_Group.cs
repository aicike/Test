using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class Account_Group : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public int GroupID { get; set; }

        public virtual Group Group { get; set; }
    }
}
