using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class LookupOptionModel : BaseModel<LookupOption>, ILookupOptionModel
    {
        public int GetIdByToken(Enum value)
        {
            var str = value.ToString();
            var entity = List().Where(a => a.Token == str).FirstOrDefault();
            return entity.ID;
        }
    }
}
