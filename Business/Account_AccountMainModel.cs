using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Injection.Transaction;
using Poco.Enum;

namespace Business
{
    public class Account_AccountMainModel : BaseModel<Account_AccountMain>, IAccount_AccountMainModel
    {
        [Transaction]
        public Result Add(Account_AccountMain account_accountMain, List<int> roleID, System.Web.HttpPostedFileBase HeadImagePathFile)
        {
            Result result = new Result();
            Account account = new Account();
            account.Name = account_accountMain.Account.Name;
            account.LoginPwd = account_accountMain.Account.Name;
            account.LoginPwdPage = account_accountMain.Account.LoginPwdPage;
            account.LoginPwdPageCompare = account_accountMain.Account.LoginPwdPageCompare;
            account.Phone = account_accountMain.Account.Phone;
            account.HeadImagePath = account_accountMain.Account.HeadImagePath;
            account.Email = account_accountMain.Account.Email;
            account.AccountStatusID = account_accountMain.Account.AccountStatusID;
            account.IsActivated = account_accountMain.Account.IsActivated;
            if (account_accountMain.Account.ParentAccountID.HasValue && account_accountMain.Account.ParentAccountID.Value > 0)
            {
                account.ParentAccountID = account_accountMain.Account.ParentAccountID;
            }
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            result = accountModel.Add(account, account_accountMain.AccountMainID, roleID, HeadImagePathFile);
            if (result.HasError == false)
            {
                Account_AccountMain entity = new Account_AccountMain();
                entity.AccountMainID = account_accountMain.AccountMainID;
                entity.AccountID = account.ID;
                result = base.Add(entity);
            }
            if (result.HasError == false)
            {
                SMS_Model smsModel = new SMS_Model();
                smsModel.Send_AccountRegister(account.ID);
            }
            return result;
        }

        /// <summary>
        /// ture 已存在管理员，false不存在管理员
        /// </summary>
        /// <param name="accountMainID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public bool CheckIsExistAccountAdmin(int accountMainID, int? accountID = null)
        {

            int accountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            if (accountID != null && accountID.HasValue)
            {
                var accountRoleModel = Factory.Get<IAccountRoleModel>(SystemConst.IOC_Model.AccountRoleModel);
                var isAdmin = accountRoleModel.List().Any(a => a.AccountID == accountID && a.Role.Token.Equals(SystemConst.Business.AccountAdmin));
                if (isAdmin)
                {
                    var accountIDs = List().Where(a => a.AccountMainID == accountMainID
                                                   && (a.Account.Account_Roles.Any(b => b.Role.Token.Equals(SystemConst.Business.AccountAdmin)) || a.Account.Account_Roles.Any(b => b.Role.ID == 1))
                                                   && a.Account.SystemStatus == (int)EnumSystemStatus.Active
                                                   && a.Account.AccountStatusID == accountStatusID
                                                   && a.Account.ID != accountID).Select(a => a.AccountID).ToList();
                    if (accountIDs.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return List().Any(a => a.AccountMainID == accountMainID && (a.Account.Account_Roles.Any(b => b.Role.Token.Equals(SystemConst.Business.AccountAdmin)) || a.Account.Account_Roles.Any(b => b.Role.ID == 1)) && a.Account.SystemStatus == (int)EnumSystemStatus.Active && a.Account.AccountStatusID == accountStatusID);
        }

        public List<Account> GetAccountListByAccountMainID(int accountMainID)
        {
            string accountStatus = EnumAccountStatus.Enabled.ToString();
            return List().Where(a => a.AccountMainID == accountMainID &&
                a.Account.SystemStatus == (int)EnumSystemStatus.Active &&
                a.Account.AccountStatus.Token.Equals(accountStatus) &&
                a.AccountMain.AccountStatus.Token.Equals(accountStatus) &&
                a.Account.Account_Roles.Any(b => b.Role.IsCanFindByUser.Value == true))
                .Select(a => a.Account).ToList();
        }

        [Transaction]
        public Result Add(Account_AccountMain account_accountMain, List<int> roleID, System.Web.HttpPostedFileBase HeadImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            Account account = new Account();
            account.Name = account_accountMain.Account.Name;
            account.LoginPwd = account_accountMain.Account.LoginPwd;
            account.LoginPwdPage = "aaaaaa";
            account.LoginPwdPageCompare = "aaaaaa";
            account.Phone = account_accountMain.Account.Phone;
            account.HeadImagePath = account_accountMain.Account.HeadImagePath;
            account.Email = account_accountMain.Account.Email;
            account.AccountStatusID = account_accountMain.Account.AccountStatusID;
            account.IsActivated = account_accountMain.Account.IsActivated;
            if (account_accountMain.Account.ParentAccountID.HasValue && account_accountMain.Account.ParentAccountID.Value > 0)
            {
                account.ParentAccountID = account_accountMain.Account.ParentAccountID;
            }
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            result = accountModel.Add(account, account_accountMain.AccountMainID, roleID, HeadImagePathFile, x1, y1, w, h, tw, th);
            if (result.HasError == false)
            {
                Account_AccountMain entity = new Account_AccountMain();
                entity.AccountMainID = account_accountMain.AccountMainID;
                entity.AccountID = account.ID;
                result = base.Add(entity);
            }
            if (result.HasError == false)
            {
                SMS_Model smsModel = new SMS_Model();
                smsModel.Send_AccountRegister(account.ID);
            }
            return result;
        }


        [Transaction]
        public Result Add(Account_AccountMain account_accountMain, List<int> roleID, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            Account account = new Account();
            account.Name = account_accountMain.Account.Name;
            account.LoginPwd = account_accountMain.Account.LoginPwd;
            account.LoginPwdPage = "aaaaaa";
            account.LoginPwdPageCompare = "aaaaaa";
            account.Phone = account_accountMain.Account.Phone;
            account.HeadImagePath = account_accountMain.Account.HeadImagePath;
            account.Email = account_accountMain.Account.Email;
            account.AccountStatusID = account_accountMain.Account.AccountStatusID;
            account.IsActivated = account_accountMain.Account.IsActivated;
            if (account_accountMain.Account.ParentAccountID.HasValue && account_accountMain.Account.ParentAccountID.Value > 0)
            {
                account.ParentAccountID = account_accountMain.Account.ParentAccountID;
            }
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            result = accountModel.Add(account, account_accountMain.AccountMainID, roleID, x1, y1, w, h, tw, th);
            if (result.HasError == false)
            {
                Account_AccountMain entity = new Account_AccountMain();
                entity.AccountMainID = account_accountMain.AccountMainID;
                entity.AccountID = account.ID;
                result = base.Add(entity);
            }
            if (result.HasError == false)
            {
                SMS_Model smsModel = new SMS_Model();
                smsModel.Send_AccountRegister(account.ID);
            }
            return result;
        }

        [Transaction]
        public Result MicroSite_Add(Account account, int accountMainID, int roleID)
        {
            Result result = new Result();
            account.LoginPwd = account.LoginPwdPage;
            account.IsActivated = true;
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            result = accountModel.MicroSite_Add(account, accountMainID, roleID);
            if (result.HasError == false)
            {
                Account_AccountMain entity = new Account_AccountMain();
                entity.AccountMainID = accountMainID;
                entity.AccountID = account.ID;
                result = base.Add(entity);
            }
            return result;
        }
    }
}
