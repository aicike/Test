using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IUserModel : IBaseModel<User>
    {
        /// <summary>
        /// 根据accountID和分组ID获取分组下用户
        /// </summary>
        /// <param name="accountID">现在其他账号无权访问该组下数据，需要用accoutID做限制</param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        IQueryable<User> GetUserByAccountID(int accountID, int groupID);

        Result UpdateUserInfo(User user);

        /// <summary>
        /// 根据 accountMainID和 accountID获取全部用户列表
        /// </summary>
        List<User> GetUserListByAccountID(int accountMainID, int accountID);
    }
}
