﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Injection.Transaction;

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
    }
}
