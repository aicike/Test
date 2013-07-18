using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Poco.Enum;
using Common;
using Injection.Transaction;

namespace Business
{
    public class UserModel : BaseModel<User>, IUserModel
    {
        public IQueryable<User> GetUserByAccountID(int accountID, int groupID)
        {
            var userGroupModel = Factory.Get<IUser_GroupModel>(SystemConst.IOC_Model.User_GroupModel);
            return List().Where(a => a.User_Groups.Any(b => b.GroupID == groupID && b.Group.AccountID == accountID && b.Group.SystemStatus == (int)EnumSystemStatus.Active));
        }

        public new Result Add(User user)
        {
            if (user.LoginPwd.Length > 0)
            {
                user.LoginPwd = DESEncrypt.Encrypt(user.LoginPwd);
            }
            user.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            return base.Add(user);
        }

        [Transaction]
        public Result Login(string email, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var user = List().Where(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && a.LoginPwd == pwd).FirstOrDefault();
            if (user == null)
            {
                result.Error = "邮箱或密码错误，登录失败。";
            }
            result.Entity = user;
            return result;
        }
    }
}
