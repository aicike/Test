using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Interface;
using Injection;
using Poco;

namespace Web.Controllers
{
    public class AccountController : ManageAccountController
    {
        public ActionResult Index(int? roleID, int? index)
        {
            //角色列表
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleList(LoginAccount.CurrentAccountMainID);
            ViewBag.RoleList = roleList;
            //项目信息
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get(LoginAccount.CurrentAccountMainID);
            ViewBag.AccountMain = entity;
            //项目管理员
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var accountAdminIquery = accountModel.GetAccountAdminListByAccountMain(entity);
            IQueryable<Poco.Account> accountList = null; //项目成员
            //根据角色查找账号列表
            if (roleID.HasValue)
            {
                accountList = accountModel.GetAccountListByAccountMain_RoleID(LoginAccount.CurrentAccountMainID, roleID.Value);
            }
            else
            {
                accountList = accountAdminIquery;
            }
            var pageList = accountList.ToPagedList(index ?? 1, 15);
            ViewBag.RoleID = roleID.HasValue ? roleID.Value : 1;
            ViewBag.AccountAdminList = accountAdminIquery.ToList();
            ViewBag.HostName = LoginAccount.HostName;
            return View(pageList);
        }

    }
}
