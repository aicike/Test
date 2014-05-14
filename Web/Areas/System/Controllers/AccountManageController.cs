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
using Business;
using Common;

namespace Web.Areas.System.Controllers
{
    public class AccountManageController : ManageSystemUserController
    {
        public ActionResult Index(int? id, int accountMainId, int? roleID)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();
            //角色列表
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roleList = roleModel.GetRoleListAll(accountMainId);
            ViewBag.RoleList = roleList;

            int roleshowID = roleList.Where(a => a.IsCanDelete == false).FirstOrDefault().ID;

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
            ViewBag.RoleID = roleID.HasValue ? roleID.Value : roleshowID;
            ViewBag.AccountAdminList = accountAdminIquery.ToList();
            return View(pageList);
        }

        public ActionResult AddAccount(int accountMainId)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            var roles = roleModel.GetRoleListAll(accountMainId);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            ViewData["Roles"] = selectListRoles;
            ViewBag.AccountMainID = accountMainId;
            return View();
        }

        [HttpPost]
        public ActionResult AddAccount(Account_AccountMain account_accountMain, string roleIDs, HttpPostedFileBase HeadImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, account_accountMain.AccountMainID).NotAuthorizedPage();
            var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            //获取项目管理员角色ID
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            int sysRoleID = roleModel.GetRoleIscandelete(account_accountMain.AccountMainID);

            List<int> roleIDList = roleIDs.ConvertToIntArray(',').ToList();

            if (roleIDList.Contains(sysRoleID))
            {
                if (account_accountMainModel.CheckIsExistAccountAdmin(account_accountMain.AccountMainID, null))
                {
                    throw new ApplicationException("只能有一个有效的管理员账号，请选择分配其他角色。");
                }
            }
            CommonModel cm = new CommonModel();
            account_accountMain.Account.LoginPwd = cm.CreateRandom("", 6);

            IAccount_AccountMainModel model = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            var result = model.Add(account_accountMain, roleIDList, HeadImagePathFile, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }

            var Accountmain = accountMainModel.Get(account_accountMain.AccountMainID);

            if (!string.IsNullOrEmpty(account_accountMain.Account.Email))
            {
                EmailInfo emailInfo = new EmailInfo();
                emailInfo.To = account_accountMain.Account.Email;
                emailInfo.Subject = "ImTimely - 账号注册成功";
                emailInfo.IsHtml = true;
                emailInfo.UseSSL = false;
                emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的ImTimely账号已创建成功,<a href='http://{0}.{1}' target='_blank'>请点击此处</a>&nbsp;登录。", Accountmain.HostName,SystemConst.WebUrl) +
                                 string.Format("登录名为您当前邮箱账号。<br/> 密码：{0}<br/>", account_accountMain.Account.LoginPwd) +
                                 string.Format("<br/>为了保证您的帐号安全，请尽快更改你的密码！(登录后点击设置更改)<br/><br/>ImTimely<br/><br/>{0}", DateTime.Now.ToString("yyyy-MM-dd"));
                try
                {
                    SendEmail.SendMailAsync(emailInfo);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("邮件发送失败！请在列表页重新生成密码！");
                }
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
            var roles = roleModel.GetRoleListAll(accountMainID);
            var selectListRoles = new SelectList(roles, "ID", "Name");
            ViewData["Roles"] = selectListRoles;
            ViewData["SelRoles"] = entity.Account_Roles.Select(a => a.RoleID).ToList();
            ViewBag.AccountMainID = accountMainID;
            return View(entity);
        }

        [HttpPost]
        public ActionResult EditAccount(Poco.Account account, int accountMainId,string roleIDs, HttpPostedFileBase HeadImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainModel.CheckHasPermissions(LoginSystemUser.ID, accountMainId).NotAuthorizedPage();

            //获取项目管理员角色ID
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainId);

            List<int> roleIDList = roleIDs.ConvertToIntArray(',').ToList();
            if (roleIDList.Contains(sysRoleID))
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainId, account.ID))
                {
                    throw new ApplicationException("只能有一个有效的管理员账号，请选择分配其他角色。");
                }
            }

            account.LoginPwdPage = "aaaaaa";
            account.LoginPwdPageCompare = "aaaaaa";
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = model.Edit(account, accountMainId, roleIDList, HeadImagePathFile, x1, y1, w, h, tw, th);
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

        public ActionResult ResetPwd(int id, string mail)
        {
            IAccountModel model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            CommonModel cm = new CommonModel();
            var account = model.Get(id);
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
                emailInfo.UseSSL = false;
                emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的ImTimely账号密码重置成功,<a href='http://{0}.ImTimely.com' target='_blank'>请点击此处</a>&nbsp;登录。", account.Account_AccountMains.FirstOrDefault().AccountMain.HostName) +
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

    }
}
