using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Interface;
using Injection;
using Poco;
using Poco.Enum;

namespace Web.Areas.System.Controllers
{
    public class AccountManageController : ManageSystemUserController
    {
        public ActionResult Index(int? id,int accountMainId, int? roleID)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();
            //角色列表
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleList(accountMainId);
            ViewBag.RoleList = roleList;
            //项目信息
            var entity = accountMainModel.Get(accountMainId);
            ViewBag.AccountMain = entity;
            //项目管理员
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var accountAdminIquery = accountModel.GetAccountAdminListByAccountMain(entity);
            IQueryable<Poco.Account> accountList = null; //项目成员
            //根据角色查找账号列表
            if (roleID.HasValue)
            {
                accountList = accountModel.GetAccountListByAccountMain_RoleID(entity, roleID.Value);
            }
            else
            {
                accountList = accountAdminIquery;
            }
            var pageList = accountList.ToPagedList(id ?? 1, 15);
            ViewBag.RoleID = roleID.HasValue ? roleID.Value : 1;
            ViewBag.AccountAdminList = accountAdminIquery.ToList();
            return View(pageList);
        }

        public ActionResult AddAccount(int accountMainId)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleList(null);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            List<SelectListItem> newRolesList = new List<SelectListItem>();
            newRolesList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newRolesList.AddRange(selectListRoles);
            ViewData["Roles"] = newRolesList;
            ViewBag.AccountMainID = accountMainId;
            return View();
        }

        [HttpPost]
        public ActionResult AddAccount(Account_AccountMain account_accountMain, HttpPostedFileBase HeadImagePathFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, account_accountMain.AccountMainID).NotAuthorizedPage();
            IAccount_AccountMainModel model = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            var result = model.Add(account_accountMain, HeadImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "AccountManage", new { Area = "System", accountMainId = account_accountMain.AccountMainID });
        }

        public ActionResult EditAccount(int accountMainID, int accountID)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainID).NotAuthorizedPage();

            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var entity = model.Get(accountID);

            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleList(null);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            List<SelectListItem> newRolesList = new List<SelectListItem>();
            newRolesList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newRolesList.AddRange(selectListRoles);
            ViewData["Roles"] = newRolesList;
            ViewBag.AccountMainID = accountMainID;
            return View(entity);
        }

        [HttpPost]
        public ActionResult EditAccount(Poco.Account account, int accountMainId, HttpPostedFileBase HeadImagePathFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();

            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Edit(account, accountMainId, HeadImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "AccountManage", new { Area = "System", accountMainId = accountMainId });
        }

        [AllowCheckPermissions(false)]
        public string CheckIsExistAdmin(int accountMainID, int? accountID)
        {
            var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID, accountID))
            {
                return AlertJS_NoTag(new Dialog("只能有一个有效的管理员账号，请选择分配其他角色。"));
            }
            return "";
        }

        public ActionResult DeleteAccount(int id, int accountMainId)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();

            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult SetAccountStatus(int accountID, int accountMainID, string status)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainID).NotAuthorizedPage();

            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            EnumAccountStatus accountStatus = (EnumAccountStatus)Enum.Parse(typeof(EnumAccountStatus), status);
            var result = model.ChangeStatus(accountID, accountStatus, accountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }
    }
}
