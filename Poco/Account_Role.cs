using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class Account_Role : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }
    }
}
