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
            return base.List().OrderBy(a => a.ID);
        }

        public List<SystemUserRole> GetRoleWithoutSuperAdmin()
        {
            return base.List().Where(a => a.ID != 1).OrderBy(a => a.ID).ToList();
        }
    }
}
