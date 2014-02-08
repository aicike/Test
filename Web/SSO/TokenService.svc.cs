using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.ServiceModel.Activation;

namespace Web.SSO
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
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
