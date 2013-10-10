using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface IAccount_AccountMainModel : IBaseModel<Account_AccountMain>
    {
        Result Add(Account_AccountMain account_accountMain, HttpPostedFileBase HeadImagePathFile);
        Result Add(Account_AccountMain account_accountMain, HttpPostedFileBase HeadImagePathFile, int w, int h, int x1, int y1, int tw, int th);

        bool CheckIsExistAccountAdmin(int accountMainID,int? accountID=null);
        List<Account> GetAccountListByAccountMainID(int accountMainID);
    }
}
