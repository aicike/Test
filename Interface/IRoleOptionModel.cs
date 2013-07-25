using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IRoleOptionModel : IBaseModel<RoleOption>
    {
        List<RoleOption> List_Cache();
    }
}
