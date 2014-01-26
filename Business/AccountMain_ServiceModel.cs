using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class AccountMain_ServiceModel : BaseModel<AccountMain_Service>, IAccountMain_ServiceModel
    {
        public Result Add(int accountMainID, int[] serviceID)
        {
            Result result = new Result();
            StringBuilder sb = new StringBuilder("DELETE dbo.AccountMain_Service WHERE AccountMainID="+accountMainID+"; INSERT dbo.AccountMain_Service( SystemStatus ,AccountMainID ,ServiceID) ");
            foreach (var item in serviceID)
            {
                sb.Append(string.Format(" SELECT 0,{0},{1} UNION ALL", accountMainID, item));
            }
            var sql = sb.ToString();
            sql = sql.Remove(sql.Length - " UNION ALL".Length);
            int i = base.SqlExecute(sql);
            return result;
        }

        public List<AccountMain_Service> GetListByAccountMainID(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID).ToList();
        }
    }
}
