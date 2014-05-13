using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class SystemUserRoleModel : BaseModel<SystemUserRole>, ISystemUserRoleModel
    {
        public new IQueryable<SystemUserRole> List()
        {
            return base.List();
        }

        public List<SystemUserRole> GetRoleWithoutSuperAdmin()
        {
            return base.List().Where(a => a.ID != 1).ToList();
        }


    }
}
