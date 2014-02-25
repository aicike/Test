using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Poco.Enum;
using Common;
using Business;

namespace Web.Controllers
{
    public class AccountMainController : ManageAccountController
    {
        #region 项目管理

        public ActionResult Index(int? id)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMain = accountMainModel.Get(LoginAccount.CurrentAccountMainID);
            if (accountMain.IsOrganization.HasValue == false || accountMain.IsOrganization.Value == false)
            {
                false.NotAuthorizedPage();
            }
            var list = accountMainModel.ListForOrganization(LoginAccount.ID).ToPagedList(id ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目和账号管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目和账号管理-添加项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult Add(AccountMain accountMain, HttpPostedFileBase LogoImagePathFile, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMain.ParentAccountMainID = LoginAccount.CurrentAccountMainID;
            accountMain.SalePhone = accountMain.Phone;
            var result = accountMainModel.Add(accountMain, LogoImagePathFile, AndroidPathFile, AndroidSellPathFile, AppLogoImageFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "AccountMain", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Edit(int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.List().Where(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == id).FirstOrDefault();
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目和账号管理-修改项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(AccountMain accountMain, HttpPostedFileBase LogoImagePathFile, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMain.ParentAccountMainID = LoginAccount.CurrentAccountMainID;
            accountMain.SalePhone = accountMain.Phone;
            var result = accountMainModel.Edit(accountMain, LogoImagePathFile, AndroidPathFile, AndroidSellPathFile, AppLogoImageFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "AccountMain", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Delete(int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == id).NotAuthorizedPage();
            var result = accountMainModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult SetAccountMainStatus(int accountMainID, string status)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).NotAuthorizedPage();
            EnumAccountStatus accountStatus = (EnumAccountStatus)Enum.Parse(typeof(EnumAccountStatus), status);
            var result = accountMainModel.ChangeStatus(accountMainID, accountStatus);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        [AllowCheckPermissions(false)]
        public string GetHostName(string value)
        {
            return ChineseSpell.GetFirstCharSpell(value);
        }
        #endregion

        #region 项目角色管理

        public ActionResult Role(int accountMainID, int? id)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).NotAuthorizedPage();
            var list = roleModel.GetListByAMID(accountMainID).ToPagedList(id ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.AccountMainID = accountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        [HttpGet]
        public ActionResult AddRole(int AccountMainID)
        {
            ViewBag.AccountMainID = AccountMainID;
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-添加角色", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(int AccountMainID, Role role)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            role.IsCanDelete = true;
            role.AccountMainID = AccountMainID;
            var result = roleModel.Add(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Role", "AccountMain", new { HostName = LoginAccount.HostName, accountMainID = role.AccountMainID }) + "'");
        }

        [HttpGet]
        public ActionResult EditRole(int id, int AccountMainID)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var role = roleModel.List().Where(a => a.ID == id && a.AccountMainID == AccountMainID).FirstOrDefault();
            (role != null).NotAuthorizedPage();
            role.IsCanDelete.NotAuthorizedPage();
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-修改角色", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(Role role)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.Edit(role);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Role", "AccountMain", new { HostName = LoginAccount.HostName, accountMainID = role.AccountMainID }) + "'");
        }

        [AllowCheckPermissions(false)]
        public string IsCanFindByUser(int id, bool value, int AccountMainID)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var result = roleModel.IsCanFindByUser(id, value);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Role", "AccountMain", new { HostName = LoginAccount.HostName, accountMainID = AccountMainID }) + "'";
        }

        public ActionResult DeleteRole(int id, int AccountMainID)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            roleModel.List().Any(a => a.ID == id && a.AccountMainID == AccountMainID).NotAuthorizedPage();
            var result = roleModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Role", "AccountMain", new { HostName = LoginAccount.HostName, accountMainID = AccountMainID }) + "'");
        }

        public ActionResult PowerRole(int id, int AccountMainID)
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            IMenuModel menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
            IMenuOptionModel menuOptionModel = Factory.Get<IMenuOptionModel>(SystemConst.IOC_Model.MenuOptionModel);
            ViewBag.AccountMainID = AccountMainID;

            //当前角色
            var role = roleModel.List().Where(a => a.ID == id && a.AccountMainID == AccountMainID).FirstOrDefault();
            (role != null).NotAuthorizedPage();
            ViewBag.Role = role;

            //获取当前管理员角色ID
            int AdminPID = roleModel.GetRoleIscandelete(LoginAccount.CurrentAccountMainID);

            //管理员角色所操作的菜单
            var AdminRoleMenuList = menuModel.GetAllMenuByRoleID(AdminPID).Select(a => a.ID).ToList();


            //管理员角色所操作的功能
            var AdminRoleOptionList = menuOptionModel.GetAllOptionByRoleID(AdminPID).Select(a => a.ID).ToList();

            ViewBag.AdminMenyOptionIDS = AdminRoleOptionList;

            //可分配的菜单
            //var menus = menuModel.List_Cache().Where(a => AdminRoleMenuList.Contains(a.ID) && a.ParentMenuID.HasValue == false).OrderBy(a => a.Order).ToList();
            var menus = menuModel.List().Where(a => AdminRoleMenuList.Contains(a.ID) && a.ParentMenuID.HasValue == false).OrderBy(a => a.Order).ToList();
            menus = menus.Where(a => a.Token != "Token_Project_M2").ToList();
            foreach (var item in menus)
            {
                item.Menus = item.Menus.Where(a => a.Token != "Token_Project_M2").ToList();
            }

            //可分配的二级菜单ID
            //var menusOption = menuModel.List_Cache().Where(a => AdminRoleOptionList.Contains(a.ID) && a.ParentMenuID.HasValue).Select(a => a.ID).ToList();
            var menusOption = menuModel.List_Cache().Where(a => AdminRoleMenuList.Contains(a.ID) && a.ParentMenuID.HasValue).Select(a => a.ID).ToList();
            ViewBag.AdminMenyIDS = menusOption;


            //当前角色所操作的菜单
            var currentRoleMenuList = menuModel.GetAllMenuByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleMenuList = currentRoleMenuList;
            //当前角色所操作的功能
            var currentRoleOptionList = menuOptionModel.GetAllOptionByRoleID(id).Select(a => a.ID).ToList();
            ViewBag.CurrentRoleOptionList = currentRoleOptionList;



            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "角色管理-权限控制", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(menus);
        }

        [HttpPost]
        public ActionResult PowerRole(int AccountMainID)
        {
            var roleID = Convert.ToInt32(Request["hidRoleID"]);

            var checkboxMenu = Request["checkboxMenu"];
            var checkboxOption = Request["checkboxOption"];

            int[] menuIDArray = null;
            if (!string.IsNullOrEmpty(checkboxMenu))
            {
                menuIDArray = checkboxMenu.Split(',').ConvertToIntArray();
            }

            int[] optionIDArray = null;
            if (!string.IsNullOrEmpty(checkboxOption))
            {
                optionIDArray = checkboxOption.Split(',').ConvertToIntArray();
            }

            var accountMainServiceModel = Factory.Get<IAccountMain_ServiceModel>(SystemConst.IOC_Model.AccountMain_ServiceModel);
            int[] serviceIDArray = accountMainServiceModel.List().Where(a => a.AccountMainID == LoginAccount.CurrentAccountMainID).Select(a => a.ServiceID).ToArray();
            try
            {
                IRoleMenuModel roleMenuModel = Factory.Get<IRoleMenuModel>(SystemConst.IOC_Model.RoleMenuModel);
                roleMenuModel.BindPermission(AccountMainID, roleID, menuIDArray, optionIDArray, serviceIDArray);
            }
            catch (Exception ex)
            {
                return Alert(new Dialog(ex.Message));
            }
            return Content(AlertJS(new Dialog("绑定成功。", Url.Action("PowerRole", "AccountMain", new { id = roleID, AccountMainID = AccountMainID }))));
        }

        #endregion

        #region 项目账号管理

        public ActionResult Account(int accountMainID, int? id, int? roleID)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).NotAuthorizedPage();
            //角色列表
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleAllList(accountMainID);
            ViewBag.RoleList = roleList;
            int RoleShowID = 0;
            if (roleList != null)
            {
                if (roleList.Count() > 0)
                {
                    RoleShowID = roleList.FirstOrDefault().ID;
                }
            }
            //项目信息
            var entity = accountMainModel.Get(accountMainID);
            ViewBag.AccountMain = entity;

            //项目管理员
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            IQueryable<Poco.Account> accountList = null; //项目成员

            //获取项目管理员角色ID
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainID);
            //根据角色查找账号列表
            if (roleID.HasValue)
            {
                if (roleID == sysRoleID)
                {
                    accountList = accountModel.GetAccountListByAccountMain_RoleID(accountMainID, roleList.FirstOrDefault().ID,0);
                }
                else
                {
                    accountList = accountModel.GetAccountListByAccountMain_RoleID(accountMainID, roleID.Value, 0);
                }
            }
            else
            {
                if (roleList.FirstOrDefault() != null)
                {
                    accountList = accountModel.GetAccountAdminListByAccountMain(entity, roleList.FirstOrDefault().ID, 0);
                }
            }
            PagedList<Account> pageList = new PagedList<Account>(new List<Account>(), 1, 15);
            if (accountList != null)
            {
                pageList = accountList.ToPagedList(id ?? 1, 15);

                ViewBag.AccountAdminList = accountList.ToList();
            }
            ViewBag.RoleID = roleID.HasValue ? roleID.Value : RoleShowID;
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理", entity.Name, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(pageList);
        }


        public ActionResult AddAccount(int accountMainID)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMain= accountMainModel.List().Where(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).FirstOrDefault();
            (accountMain!=null).NotAuthorizedPage();
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleListAll(accountMainID);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            ViewData["Roles"] = selectListRoles;
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.AccountMainID = accountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理-添加账号", accountMain.Name, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.RoleList = roles;
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainID);
            ViewBag.AdminRoleID = sysRoleID;
            return View();
        }

        [HttpPost]
        public ActionResult AddAccount(Account_AccountMain account_accountMain, string roleIDs, int w, int h, int x1, int y1, int tw, int th)
        {
            IAccount_AccountMainModel model = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            CommonModel cm = new CommonModel();

            account_accountMain.Account.LoginPwd = cm.CreateRandom("", 6);

            List<int> RoleIDs = roleIDs.ConvertToIntArray(',').ToList();
            var result = model.Add(account_accountMain, RoleIDs, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            if (!string.IsNullOrEmpty(account_accountMain.Account.Email))
            {
                EmailInfo emailInfo = new EmailInfo();
                emailInfo.To = account_accountMain.Account.Email;
                emailInfo.Subject = "ImTimely - 账号注册成功";
                emailInfo.IsHtml = true;
                emailInfo.UseSSL = true;
                emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的ImTimely账号已创建成功,<a href='http://{0}.{1}' target='_blank'>请点击此处</a>&nbsp;登录。", LoginAccount.HostName, SystemConst.WebUrl) +
                                 string.Format("登录名为您当前邮箱账号。<br/> 密码：{0}<br/>", account_accountMain.Account.LoginPwd) +
                                 string.Format("<br/>为了保证您的帐号安全，请尽快更改你的密码！(登录后点击设置更改)<br/><br/>IMtimely<br/><br/>{0}", DateTime.Now.ToString("yyyy-MM-dd"));
                try
                {
                    SendEmail.SendMailAsync(emailInfo);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("邮件发送失败！请在列表页重新生成密码！");
                }
            }
            return RedirectToAction("Account", "AccountMain", new { HostName = LoginAccount.HostName, accountMainID = account_accountMain .AccountMainID});
        }

        [AllowCheckPermissions(false)]
        public string CheckIsExistAdmin(int accountMainID,int? accountID)
        {
            var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID, accountID))
            {
                return AlertJS_NoTag(new Dialog("只能有一个有效的管理员账号，请选择分配其他角色。"));
            }
            return "";
        }

        public ActionResult EditAccount(int accountMainID,int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMain = accountMainModel.List().Where(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).FirstOrDefault();
            (accountMain != null).NotAuthorizedPage();
            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var entity = model.Get(id);
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleListAll(accountMainID);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            ViewData["Roles"] = selectListRoles;
            ViewData["SelRoles"] = entity.Account_Roles.Select(a => a.RoleID).ToList();
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.AccountMainID = accountMainID;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理-修改账号", accountMain.Name, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.RoleList = roles;
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainID);
            ViewBag.AdminRoleID = sysRoleID;
            return View(entity);
        }

        [HttpPost]
        public ActionResult EditAccount(Poco.Account account,int accountMainID, string roleIDs, int w, int h, int x1, int y1, int tw, int th)
        {
            account.LoginPwdPage = "aaaaaa";
            account.LoginPwdPageCompare = "aaaaaa";
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            List<int> RoleIDs = roleIDs.ConvertToIntArray(',').ToList();
            var result = model.Edit(account, accountMainID, RoleIDs, x1, y1, w, h, tw, th);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Account", "AccountMain", new { HostName = LoginAccount.HostName, accountMainID = accountMainID });
        }

        public ActionResult DeleteAccount(int accountMainID,int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).NotAuthorizedPage();
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult SetAccountStatus(int accountMainID,int accountID, string status)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).NotAuthorizedPage();
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            EnumAccountStatus accountStatus = (EnumAccountStatus)Enum.Parse(typeof(EnumAccountStatus), status);
            var result = model.ChangeStatus(accountID, accountStatus, accountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }


        public ActionResult ResetPwd(int accountMainID,int id, string mail)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.List().Any(a => a.ParentAccountMainID == LoginAccount.CurrentAccountMainID && a.ID == accountMainID).NotAuthorizedPage();
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            CommonModel cm = new CommonModel();

            string LoginPwd = cm.CreateRandom("", 6);
            Result result = model.ResetPwd(id, DESEncrypt.Encrypt(LoginPwd));
            if (result.HasError == true)
            {
                return Alert(new Dialog("密码重置失败 请重试"));
            }
            if (!string.IsNullOrEmpty(mail))
            {
                EmailInfo emailInfo = new EmailInfo();
                emailInfo.To = mail;
                emailInfo.Subject = "ImTimely - 密码重置成功";
                emailInfo.IsHtml = true;
                emailInfo.UseSSL = true;
                emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的ImTimely账号密码重置成功,<a href='http://{0}.{1}' target='_blank'>请点击此处</a>&nbsp;登录。", LoginAccount.HostName, SystemConst.WebUrl) +
                                 string.Format("登录名为您当前邮箱账号。<br/> 密码：{0}<br/>", LoginPwd) +
                                 string.Format("<br/>为了保证您的帐号安全，请尽快更改你的密码！(登录后点击设置更改)<br/><br/>IMtimely<br/><br/>{0}", DateTime.Now.ToString("yyyy-MM-dd"));
                try
                {
                    SendEmail.SendMailAsync(emailInfo);
                }
                catch (Exception ex)
                {
                    return Alert(new Dialog("邮件发送失败！请重新生成密码！"));
                }
            }
            SMS_Model sms = new SMS_Model();
            sms.Send_AccountRegisterPWD(id);
            return Alert(new Dialog("密码重置成功！"));
        }
        #endregion
    }
}
