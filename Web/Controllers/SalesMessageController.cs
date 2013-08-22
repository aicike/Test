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

        [AllowCheckPermissions(false)]
        public ActionResult SalesUserList(int? id,int accountID, int GruopID)
        {
            //数据
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var UserList = userModel.GetUserByAccountID(accountID, GruopID).ToPagedList(id??1, 10);
            //售楼人员姓名
            var AccountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = AccountModel.Get(accountID);
            ViewBag.AccountName = account.Name;
            //组名
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var group = groupModel.Get(GruopID);
            ViewBag.GroupName = group.GroupName;

            ViewBag.AccountID = accountID;
            return View(UserList);
        }


        public ActionResult SelectAtoUmes(int ?id ,int accountID,int UserID)
        {
            //获取聊天记录
            var MessageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            var message = MessageModel.GetList(accountID, UserID).ToPagedList(id??1, 30);
            ViewBag.ID = id;
            return View(message);
        }
    }
}
