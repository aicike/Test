using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ILookupOptionModel : IBaseModel<LookupOption>
    {
        List<LookupOption> List_Cache();
        int GetIdByToken(Enum value);
    }
}
