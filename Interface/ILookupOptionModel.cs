using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ILookupOptionModel : IBaseModel<LookupOption>
    {
        int GetIdByToken(Enum value);
    }
}
