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
        public Result Add(Account_AccountMain account_accountMain, System.Web.HttpPostedFileBase HeadImagePathFile)
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
            account.RoleID = account_accountMain.Account.RoleID;
            account.AccountStatusID = account_accountMain.Account.AccountStatusID;
            account.IsActivated = account_accountMain.Account.IsActivated;
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            result = accountModel.Add(account, account_accountMain.AccountMainID, HeadImagePathFile);
            if (result.HasError == false)
            {
                Account_AccountMain entity = new Account_AccountMain();
                entity.AccountMainID = account_accountMain.AccountMainID;
                entity.AccountID = account.ID;
                result = base.Add(entity);
            }
            return result;
        }

        public bool CheckIsExistAccountAdmin(int accountMainID, int? accountID = null)
        {

            int accountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            if (accountID != null && accountID.HasValue)
            {

                var accountIDs = List().Where(a => a.AccountMainID == accountMainID
                    && (a.Account.Role.Token.Equals(SystemConst.Business.AccountAdmin) || a.Account.Role.ID == 1)
                    && a.Account.SystemStatus == (int)EnumSystemStatus.Active
                    && a.Account.AccountStatusID == accountStatusID).Select(a => a.AccountID);
                if (accountIDs.Count() > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return List().Any(a => a.AccountMainID == accountMainID && (a.Account.Role.Token.Equals(SystemConst.Business.AccountAdmin) || a.Account.Role.ID == 1) && a.Account.SystemStatus == (int)EnumSystemStatus.Active && a.Account.AccountStatusID == accountStatusID);
        }

        public List<Account> GetAccountListByAccountMainID(int accountMainID)
        {
            string accountStatus = EnumAccountStatus.Enabled.ToString();
            return List().Where(a => a.AccountMainID == accountMainID &&
                a.Account.SystemStatus == (int)EnumSystemStatus.Active &&
                a.Account.AccountStatus.Token.Equals(accountStatus) &&
                a.AccountMain.AccountStatus.Token.Equals(accountStatus) &&
                a.Account.Role.IsCanFindByUser.HasValue &&
                a.Account.Role.IsCanFindByUser.Value)
                .Select(a => a.Account).ToList();
        }
    }
}
