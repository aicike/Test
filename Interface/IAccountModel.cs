using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;
using Poco.Enum;

namespace Interface
{
    public interface IAccountModel : IBaseModel<Account>
    {
        /// <summary>
        /// 项目成员
        /// </summary>
        IQueryable<Account> GetAccountListByAccountMain(int accountMainID);
        IQueryable<Account> GetAccountListByAccountMain(AccountMain accountMain);
        /// <summary>
        /// 根据角色获取项目成员
        /// </summary>
        IQueryable<Account> GetAccountListByAccountMain_RoleID(int accountMainID, int roleID);
        IQueryable<Account> GetAccountListByAccountMain_RoleID(AccountMain accountMain, int roleID);
        /// <summary>
        /// 项目管理员
        /// </summary>
        IQueryable<Account> GetAccountAdminListByAccountMain(int accountMainID);
        IQueryable<Account> GetAccountAdminListByAccountMain(AccountMain accountMain);

        Result Add(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile);

        Result Edit(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile);

        Result ChangeStatus(int accountID, EnumAccountStatus status, int accountMainID);

        Result Login(string email, string pwd);

        Result Delete(int id);

        bool CheckHasPermissions_User(int accountID, int userID);
    }
}