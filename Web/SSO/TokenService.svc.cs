using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace Web.SSO
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“TokenService”。
    public class TokenService : ITokenService
    {
        public object TokenGetCredence(string tokenValue)
        {
            object o = null;

            DataTable dt = CacheManager.GetCacheTable();
            if (dt != null)
            {
                DataRow[] dr = dt.Select("token = '" + tokenValue + "'");
                if (dr.Length > 0)
                {
                    o = dr[0]["info"];
                }
            }

            return o;
        }

        public void ClearToken(string tokenValue)
        {
            DataTable dt =CacheManager.GetCacheTable();
            if (dt != null)
            {
                DataRow[] dr = dt.Select("token = '" + tokenValue + "'");
                if (dr.Length > 0)
                {
                    dt.Rows.Remove(dr[0]);
                }
            }
        }
    }
}
