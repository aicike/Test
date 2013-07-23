using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Poco.Enum;
using System.Web;
using System.IO;
using Common;
using Injection.Transaction;

namespace Business
{
    public class AccountModel : BaseModel<Account>, IAccountModel
    {
        /// <summary>
        /// 获取项目成员
        /// </summary>
        public IQueryable<Account> GetAccountListByAccountMain(int accountMainID)
        {
            var entity = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            return entity.List().Where(a => a.AccountMainID == accountMainID).Select(a => a.Account);
        }
        public IQueryable<Account> GetAccountListByAccountMain(AccountMain accountMain)
        {
            return accountMain.Account_AccountMains.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Select(a => a.Account).AsQueryable();
        }
        /// <summary>
        /// 获取项目管理员
        /// </summary>
        public IQueryable<Account> GetAccountAdminListByAccountMain(int accountMainID)
        {
            return GetAccountListByAccountMain(accountMainID).Where(a => a.RoleID == 1);
        }

        public IQueryable<Account> GetAccountAdminListByAccountMain(AccountMain accountMain)
        {
            return accountMain.Account_AccountMains.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Select(a => a.Account).Where(a => a.RoleID == 1).AsQueryable();
        }
        /// <summary>
        /// 根据角色获取项目成员
        /// </summary>
        public IQueryable<Account> GetAccountListByAccountMain_RoleID(int accountMainID, int roleID)
        {
            return GetAccountListByAccountMain(accountMainID).Where(a => a.RoleID == roleID);
        }

        public IQueryable<Account> GetAccountListByAccountMain_RoleID(AccountMain accountMain, int roleID)
        {
            return accountMain.Account_AccountMains.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Select(a => a.Account).Where(a => a.RoleID == roleID).AsQueryable();
        }

        [Transaction]
        public Result Add(Account account, int accountMainID, System.Web.HttpPostedFileBase HeadImagePathFile)
        {
            account.HeadImagePath = SystemConst.Business.DefaultHeadImage;
            account.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwdPage);
            account.IsActivated = true;
            var result = base.Add(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}_{2}", account.Name, token, HeadImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}_{3}", account.Name, token, width, HeadImagePathFile.FileName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", accountPath, imageThumbnailName);
                    HeadImagePathFile.SaveAs(imagePath);
                    //缩略图
                    Tool.Thumbnail(imagePath, imageThumbnailPath, width);
                    account.HeadImagePath = path + imageThumbnailName;
                    result = Edit(account);
                    if (result.HasError == false)
                    {
                        var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
                        result = groupModel.AddDefaultGroup(account.ID, accountMainID);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public Result Edit(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile)
        {
            account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwdPage);
            var result = base.Edit(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    //删除原头像
                    var file = HttpContext.Current.Server.MapPath(account.HeadImagePath);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}_{2}", account.Name, token, width, HeadImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}_{3}", account.Name, token, width, HeadImagePathFile.FileName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", accountPath, imageThumbnailName);
                    HeadImagePathFile.SaveAs(imagePath);
                    //缩略图
                    Tool.Thumbnail(imagePath, imageThumbnailPath, width);
                    account.HeadImagePath = path + imageThumbnailName;
                    result = Edit(account);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        [Transaction]
        public new Result Delete(int id)
        {
            var entity = Get(id);
            //删除原头像
            var file = HttpContext.Current.Server.MapPath(entity.HeadImagePath);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            var result = base.Delete(id);
            if (result.HasError == false)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                var ids = account_accountMainModel.List().Where(a => a.AccountID == id).Select(a => a.ID).ToArray();
                foreach (var item in ids)
                {
                    account_accountMainModel.Delete(item);
                }
            }
            if (result.HasError == false)
            {
                //删除默认分组
                var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
                result = groupModel.DeleteByAccountID(id);
            }

            return result;
        }

        public Result ChangeStatus(int accountID, EnumAccountStatus status)
        {
            var entity = Get(accountID);
            base.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(status);
            return Edit(entity);
        }

        public Result Login(string email, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var account = List().Where(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd)).FirstOrDefault();
            if (account == null)
            {
                result.Error = "用户名或密码错误";
            }
            else
            {
                //todo:以后可能会出现一个销售账号管理多个售楼部，需要先选择售楼部才能登录系统，目前默认选择第一个售楼部。
                Account entity = new Account();
                entity.ID = account.ID;
                entity.SystemStatus = account.SystemStatus;
                entity.Name = account.Name;
                entity.LoginPwd = account.LoginPwd;
                entity.Phone = account.Phone;
                entity.HeadImagePath = account.HeadImagePath;
                entity.Email = account.Email;
                entity.RoleID = account.RoleID;
                entity.AccountStatusID = account.AccountStatusID;
                entity.IsActivated = account.IsActivated;
                entity.HostName = account.Account_AccountMains.FirstOrDefault().AccountMain.HostName;
                entity.CurrentAccountMainID = account.Account_AccountMains.FirstOrDefault().AccountMain.ID;
                HttpContext.Current.Session[SystemConst.Session.LoginAccount] = entity;
            }
            return result;
        }

        /// <summary>
        /// 检查是否有权限操作该数据数据
        /// </summary>
        /// <param name="accountMainID">售楼部登录账号ID</param>
        /// <returns>ture:有权 false:无权</returns>
        public bool CheckHasPermissions_User(int accountID, int userID)
        {
            return true;
        }
    }
}
