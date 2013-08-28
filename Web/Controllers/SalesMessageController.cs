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
            var UserList = userModel.GetUserByAccountID(accountID, GruopID).ToPagedList(id??1, 15);
            //售楼人员姓名
            var AccountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = AccountModel.Get(accountID);
            ViewBag.AccountName = account.Name;
            //组名
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var group = groupModel.Get(GruopID);
            ViewBag.GroupName = group.GroupName;

            //所有售楼人员
            var AccountList = AccountModel.GetAccountListNoAdminByAccountMain(LoginAccount.CurrentAccountMainID);
            ViewBag.AccountList = AccountList;

            ViewBag.AccountID = accountID;
            ViewBag.GroupID = GruopID;
            ViewBag.HostName = LoginAccount.HostName; 
            return View(UserList);
        }


        public ActionResult SelectAtoUmes(int ?id ,int accountID,int UserID)
        {
            //获取聊天记录
            var MessageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            var message = MessageModel.GetList(accountID, UserID).ToPagedList(id??1, 30);

            //售楼人员姓名
            var AccountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = AccountModel.Get(accountID);
            ViewBag.AccountName = account.Name;

            //客户姓名
            var UserModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var User = UserModel.Get(UserID);
            if (User.Name == User.UserLoginInfo.Name)
            {
                ViewBag.UserName = User.Name;
            }
            else
            {
                ViewBag.UserName = User.UserLoginInfo.Name + "(" + User.Name + ")";
            }

            ViewBag.ID = id;
            return View(message);
        }

        /// <summary>
        /// 修改用户所属销售代表
        /// </summary>
        /// <param name="AmiaccountID">目标销售代表</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="groupID">当前分组</param>
        /// <param name="AccountID">当前销售代表</param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult UpUserAccount(int AmiaccountID, int UserID, int groupID,int AccountID)
        {
            //获取目标售楼代表默认分组
            var GroupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var Group = GroupModel.GetGroupListByAccountID(AmiaccountID).Where(a => a.IsDefaultGroup);

            var Account_userModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            int cnt = Account_userModel.UpdUserTooAccount(UserID,AmiaccountID,groupID);

            return RedirectToAction("SalesUserList", "SalesMessage", new { HostName = LoginAccount.HostName, accountID = AccountID, GruopID = groupID });
        }
    }
}
