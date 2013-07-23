using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    [Serializable]
    public class SystemUserRole_SystemUserMenu : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int SystemUserRoleID { get; set; }

        public virtual SystemUserRole SystemUserRole { get; set; }

        public int SystemUserMenuID { get; set; }

        public virtual SystemUserMenu SystemUserMenu { get; set; }
    }
}
