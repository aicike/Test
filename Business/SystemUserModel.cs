using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Common;
using System.Web;

namespace Business
{
    public class SystemUserModel : BaseModel<SystemUser>, ISystemUserModel
    {
        public Result Login(string email, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var systemUser = List().Where(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd)).FirstOrDefault();
            if (systemUser == null)
            {
                result.Error = "用户名或密码错误";
            }
            else
            {
                HttpContext.Current.Session[SystemConst.Session.LoginSystemUser] = systemUser;
            }
            return result;
        }
    }
}
