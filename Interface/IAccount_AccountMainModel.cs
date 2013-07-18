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
    }
}
