using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Interface;
using Injection;
using Poco;
using Poco.Enum;

namespace Web.Controllers
{
    public class AccountController : ManageAccountController
    {
        public ActionResult Index( int? id,int? roleID)
        {
            //角色列表
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleListNoaID(LoginAccount.CurrentAccountMainID,LoginAccount.ID);
            ViewBag.RoleList = roleList;
            //项目信息
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get(LoginAccount.CurrentAccountMainID);
            ViewBag.AccountMain = entity;
            //项目管理员
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            //IQueryable<Account> accountAdminIquery;
            IQueryable<Poco.Account> accountList = null; //项目成员
            //根据角色查找账号列表
            if (roleID.HasValue)
            {
                if (roleID == 1)
                {
                    accountList = accountModel.GetAccountListByAccountMain_RoleID(LoginAccount.CurrentAccountMainID, roleList.FirstOrDefault().ID,LoginAccount.ID);
                }
                else
                {
                    accountList = accountModel.GetAccountListByAccountMain_RoleID(LoginAccount.CurrentAccountMainID, roleID.Value, LoginAccount.ID);
                }
            }
            else
            {

                accountList = accountModel.GetAccountAdminListByAccountMain(entity, roleList.FirstOrDefault().ID,LoginAccount.ID);
            }
            var pageList = accountList.ToPagedList(id ?? 1, 15);
            ViewBag.RoleID = roleID.HasValue ? roleID.Value : roleList.FirstOrDefault().ID;
            ViewBag.AccountAdminList = accountList.ToList();
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(pageList);
        }

        public ActionResult Add()
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleList(null);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            List<SelectListItem> newRolesList = new List<SelectListItem>();
            newRolesList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newRolesList.AddRange(selectListRoles);
            ViewData["Roles"] = newRolesList;
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.AccountMainID = LoginAccount.CurrentAccountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理-添加账号", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Account_AccountMain account_accountMain, HttpPostedFileBase HeadImagePathFile)
        {
            IAccount_AccountMainModel model = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            var result = model.Add(account_accountMain, HeadImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "Account", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Edit(int id)
        {
            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var entity = model.Get(id);

            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleList(null);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            List<SelectListItem> newRolesList = new List<SelectListItem>();
            newRolesList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newRolesList.AddRange(selectListRoles);
            ViewData["Roles"] = newRolesList;
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.AccountMainID = LoginAccount.CurrentAccountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理-修改账号", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(Poco.Account account, HttpPostedFileBase HeadImagePathFile)
        {
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Edit(account, LoginAccount.CurrentAccountMainID, HeadImagePathFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "Account", new { HostName = LoginAccount.HostName });
        }

        [AllowCheckPermissions(false)]
        public string CheckIsExistAdmin(int? accountID)
        {
            var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            if (account_accountMainModel.CheckIsExistAccountAdmin(LoginAccount.CurrentAccountMainID, accountID))
            {
                return AlertJS_NoTag(new Dialog("只能有一个有效的管理员账号，请选择分配其他角色。"));
            }
            return "";
        }

        public ActionResult Delete(int id)
        {
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult SetAccountStatus(int accountID, string status)
        {
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            EnumAccountStatus accountStatus = (EnumAccountStatus)Enum.Parse(typeof(EnumAccountStatus), status);
            var result = model.ChangeStatus(accountID, accountStatus, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }
    }
}
