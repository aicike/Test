using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class AccountRoleModel : BaseModel<Account_Role>, IAccountRoleModel
    {
        [Transaction]
        public Result Add(List<int> roleIDs, int accountID)
        {
            Result result = new Result();
            if (roleIDs.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("DELETE Account_Role WHERE AccountID= {0}; INSERT INTO dbo.Account_Role ", accountID);
                for (int i = 0; i < roleIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.AppendFormat("SELECT 0,{0},{1}", roleIDs[i], accountID);
                    }
                    else
                    {
                        sb.AppendFormat(" UNION ALL SELECT 0,{0},{1}", roleIDs[i], accountID);
                    }
                }
                int j = base.SqlExecute(sb.ToString());
                if (j <= 0)
                {
                    result.Error = "绑定角色失败。";
                }
            }
            else
            {
                result.Error = "请选择角色进行绑定。";
            }
            return result;
        }
    }
}
