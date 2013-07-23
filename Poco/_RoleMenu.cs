using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    [Serializable]
    public class RoleMenu : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }

        public int MenuID { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
