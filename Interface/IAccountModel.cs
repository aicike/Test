using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;
using Poco.Enum;
using Poco.WebAPI_Poco;

namespace Interface
{
    public interface IAccountModel : IBaseModel<Account>
    {
        /// <summary>
        /// 项目成员
        /// </summary>
        IQueryable<Account> GetAccountListByAccountMain(int accountMainID);
        IList<Account> GetAccountListNoAdminByAccountMain(int accountMainID);
        IQueryable<Account> GetAccountListByAccountMain(AccountMain accountMain);
        /// <summary>
        /// 根据角色获取项目成员
        /// </summary>
        IQueryable<Account> GetAccountListByAccountMain_RoleID(int accountMainID, int roleID, int accountID);
        IQueryable<Account> GetAccountListByAccountMain_RoleID(AccountMain accountMain, int roleID);
        /// <summary>
        /// 项目管理员
        /// </summary>
        IQueryable<Account> GetAccountAdminListByAccountMain(int accountMainID);
        IQueryable<Account> GetAccountAdminListByAccountMain(AccountMain accountMain);
        IQueryable<Account> GetAccountAdminListByAccountMain(AccountMain accountMain, int RoleID, int accountID);

        Result Add(Account account, int accountMainID, List<int> roleIDs, HttpPostedFileBase HeadImagePathFile);
        Result Add(Account account, int accountMainID, List<int> roleIDs,HttpPostedFileBase HeadImagePathFile,int x1,int y1,int width,int height,int Twidth,int Theight);
        Result Add(Account account, int accountMainID, List<int> roleIDs, int x1, int y1, int width, int height, int Twidth, int Theight);


        Result Edit(Account account, int accountMainID, List<int> roleIDs, HttpPostedFileBase HeadImagePathFile);
        Result Edit(Account account, int accountMainID, List<int> roleIDs, HttpPostedFileBase HeadImagePathFile, int x1, int y1, int width, int height, int Twidth, int Theight);
        Result Edit(Account account, int accountMainID, List<int> roleIDs, int x1, int y1, int width, int height, int Twidth, int Theight);


        //Result SetEdit(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile, int x1, int y1, int width, int height, int Twidth, int Theight);
        Result SetEdit(Account account, int accountMainID, int x1, int y1, int width, int height, int Twidth, int Theight);


        Result ChangeStatus(int accountID, EnumAccountStatus status, int accountMainID);

        Result Login(string email_phone, string pwd);

        Result LoginApp(string phone_email, string pwd);

        Result Delete(int id);

        Result ResetPwd(int id, string pwd);

        bool CheckHasPermissions_User(int accountID, int userID);

        /// <summary>
        /// 获取下级账号
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        List<Account> GetSubAccounts(int accountID);

        List<App_Menu> CheckAppPermission(List<int> menuIDs, int accountID);

        Result MicroSite_Add(Account account, int accountMainID, int roleID);
    }
}