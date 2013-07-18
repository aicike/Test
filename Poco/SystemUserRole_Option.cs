using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class SystemUserRole_Option : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int SystemUserRoleID { get; set; }

        public virtual SystemUserRole Role { get; set; }

        public int SystemUserMenuOptionID { get; set; }

        public virtual SystemUserMenuOption SystemUserMenuOption { get; set; }
    }
}
