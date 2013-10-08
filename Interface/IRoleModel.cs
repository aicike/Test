using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IRoleModel : IBaseModel<Role>
    {
        List<Role> GetRoleList(int? accountMainID);

        List<Role> GetRoleListNoaID(int accountMainID, int AccountID);

        List<Role> GetRoleListAll(int? accountMainID);

        Result IsCanFindByUser(int id, bool value);
    }
}