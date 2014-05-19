using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Common;
using Injection;

namespace Business
{
    public class RoleOptionModel : BaseModel<RoleOption>, IRoleOptionModel
    {
        public List<RoleOption> List_Cache()
        {
            /*
             * 屏蔽缓存
            var obj = CacheModel.GetCache<List<RoleOption>>(SystemConst.Cache.RoleOption);
            if (obj != null)
            {
                return obj;
            }*/
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.RoleOption, list);
            return list;
        }


        public void ReSetCache()
        {
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.RoleOption, list);
        }
    }
}
