using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Common;

namespace Business
{
    public class LookupOptionModel : BaseModel<LookupOption>, ILookupOptionModel
    {
        public List<LookupOption> List_Cache()
        {
            var obj = CacheModel.GetCache<List<LookupOption>>(SystemConst.Cache.LookupOption);
            if (obj != null)
            {
                return obj;
            }
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.LookupOption, list);
            return list;
        }

        public int GetIdByToken(Enum value)
        {
            var str = value.ToString();
            var entity = List_Cache().Where(a => a.Token == str).FirstOrDefault();
            return entity.ID;
        }
    }
}
