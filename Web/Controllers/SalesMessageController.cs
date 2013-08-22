using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace Web.Controllers
{
    public class SalesMessageController : ManageAccountController
    {
        //
        // GET: /SalesMessage/

        public ActionResult Index()
        {
            var AccountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var AccountList = AccountModel.GetAccountListNoAdminByAccountMain(LoginAccount.CurrentAccountMainID);
            ViewBag.HostName = LoginAccount.HostName; 
            return View(AccountList);
        }


        public ActionResult SalesUserList(int id, int accountID, int GruopID)
        {
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var UserList = userModel.GetUserByAccountID(accountID, GruopID);
            return View();
        }

    }
}
