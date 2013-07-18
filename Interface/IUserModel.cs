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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginToken">邮箱</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        Result Login(string email, string pwd);
    }
}
