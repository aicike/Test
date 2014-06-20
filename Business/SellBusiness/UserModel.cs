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
            var list = List(true).Where(a => a.Account_Users.Any(b => b.AccountID == accountID && b.SystemStatus == (int)EnumSystemStatus.Active && b.GroupID == groupID));
            return list;
        }

        public List<User> GetUserListByAccountID(int accountMainID, int accountID)
        {
            return List().Where(a => a.AccountMainID == accountMainID && a.Account_Users.Any(b => b.AccountID == accountID)).OrderBy(a=>a.Account_Users.FirstOrDefault().GroupID).ToList();
        }

        public new Result Add(User user)
        {
            user.CreateDate = DateTime.Now;
            user.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            return base.Add(user);
        }

        public Result UpdateUserInfo(User user)
        {
            return base.Edit(user);
        }
        
        public User getUserByLoginID(int AccountMainID, int LoginID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID && a.UserLoginInfoID == LoginID).FirstOrDefault();
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<User> GetAllUser(int AMID)
        { 
            var list = List().Where(a=>a.AccountMainID==AMID);
            return list;
        }

    }
}
