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
using Business;
using Common;

namespace Web.Controllers
{
    public class AccountController : ManageAccountController
    {
        public ActionResult Index(int? id, int? roleID)
        {
            //角色列表
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleListNoaID(LoginAccount.CurrentAccountMainID, LoginAccount.ID);
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
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get(LoginAccount.CurrentAccountMainID);
            ViewBag.AccountMain = entity;

            //项目管理员
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            //IQueryable<Account> accountAdminIquery;
            IQueryable<Poco.Account> accountList = null; //项目成员

            //获取项目管理员角色ID
            int sysRoleID = roleModel.GetRoleIscandelete(LoginAccount.CurrentAccountMainID);
            //根据角色查找账号列表
            if (roleID.HasValue)
            {
                if (roleID == sysRoleID)
                {
                    accountList = accountModel.GetAccountListByAccountMain_RoleID(LoginAccount.CurrentAccountMainID, roleList.FirstOrDefault().ID, LoginAccount.ID);
                }
                else
                {
                    accountList = accountModel.GetAccountListByAccountMain_RoleID(LoginAccount.CurrentAccountMainID, roleID.Value, LoginAccount.ID);
                }
            }
            else
            {
                if (roleList.FirstOrDefault() != null)
                {
                    accountList = accountModel.GetAccountAdminListByAccountMain(entity, roleList.FirstOrDefault().ID, LoginAccount.ID);
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
            string webTitle = string.Format(SystemConst.Business.WebTitle, "账号管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(pageList);
        }

        public ActionResult Add()
        {
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleList(LoginAccount.CurrentAccountMainID);
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
            ViewBag.RoleList = roles;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Account_AccountMain account_accountMain, int w, int h, int x1, int y1, int tw, int th)
        {
            IAccount_AccountMainModel model = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            CommonModel cm = new CommonModel();

            account_accountMain.Account.LoginPwd = cm.CreateRandom("", 6);
            var result = model.Add(account_accountMain, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            EmailInfo emailInfo = new EmailInfo();
            emailInfo.To = account_accountMain.Account.Email;
            emailInfo.Subject = "ImTimely - 账号注册成功";
            emailInfo.IsHtml = true;
            emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的ImTimely账号已创建成功,<a href='http://{0}.ImTimely.com' target='_blank'>请点击此处</a>&nbsp;登录。", LoginAccount.HostName) +
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
            return RedirectToAction("Index", "Account", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Edit(int id)
        {
            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var entity = model.Get(id);

            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleList(LoginAccount.CurrentAccountMainID);
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
            ViewBag.RoleList = roles;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(Poco.Account account,  int w, int h, int x1, int y1, int tw, int th)
        {
            account.LoginPwdPage = "aaaaaa";
            account.LoginPwdPageCompare = "aaaaaa";
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Edit(account, LoginAccount.CurrentAccountMainID, x1, y1, w, h, tw, th);
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


        public ActionResult ResetPwd(int id, string mail)
        {
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            CommonModel cm = new CommonModel();

            string LoginPwd = cm.CreateRandom("", 6);
            Result result = model.ResetPwd(id, DESEncrypt.Encrypt(LoginPwd));
            if (result.HasError == true)
            {
                return Alert(new Dialog("密码重置失败 请重试"));
            }
            EmailInfo emailInfo = new EmailInfo();
            emailInfo.To = mail;
            emailInfo.Subject = "ImTimely - 密码重置成功";
            emailInfo.IsHtml = true;
            emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的ImTimely账号密码重置成功,<a href='http://{0}.ImTimely.com' target='_blank'>请点击此处</a>&nbsp;登录。", LoginAccount.HostName) +
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
            SMS_Model sms = new SMS_Model();
            sms.Send_AccountRegisterPWD(id);

            return Alert(new Dialog("密码重置成功！"));
        }
    }
}
