using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IRoleModel : IBaseModel<Role>
    {
        int GetRoleIscandelete(int AcountMainID);

        List<Role> GetRoleList(int? accountMainID);

        List<Role> GetRoleListNoaID(int accountMainID, int AccountID);

        List<Role> GetRoleListAll(int? accountMainID);

        IQueryable<Role> GetListByAMID(int AccountMainID);

        Result IsCanFindByUser(int id, bool value);
    }
}