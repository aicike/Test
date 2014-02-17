using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Common;
using Poco.Enum;

namespace Business
{
    public class AccountMainOrganizationModel : BaseModel<AccountMainOrganization>, IAccountMainOrganizationModel
    {
        public Result Login(string loginName, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var obj = List().Where(a => (a.Email.Equals(loginName, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd)) ||
               (a.Phone.Equals(loginName, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd))).FirstOrDefault();
            if (obj == null)
            {
                result.Error = "用户名或密码错误。";
                return result;
            }
            string accountStatus = EnumAccountStatus.Disabled.ToString();
            if (obj.AccountStatus.Token.Equals(accountStatus, StringComparison.CurrentCultureIgnoreCase))
            {
                result.Error = "账号不可用。";
                return result;
            }
            result.Entity = obj;
            return result;
        }
    }
}
