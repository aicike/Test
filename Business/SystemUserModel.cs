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
                SystemUser entity = new SystemUser();
                entity.ID = systemUser.ID;
                entity.SystemStatus = systemUser.SystemStatus;
                entity.Name = systemUser.Name;
                entity.Email = systemUser.Email;
                entity.LoginPwd = systemUser.LoginPwd;
                entity.HeadImage = systemUser.HeadImage;
                entity.Phone = systemUser.Phone;
                entity.AccountStatusID = systemUser.AccountStatusID;
                entity.SystemUserRoleID = systemUser.SystemUserRoleID;
                HttpContext.Current.Session[SystemConst.Session.LoginSystemUser] = entity;
            }
            return result;
        }
    }
}
