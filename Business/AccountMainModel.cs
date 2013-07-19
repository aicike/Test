using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web;
using System.IO;
using Injection;
using Poco.Enum;
using Common;
using Injection.Transaction;

namespace Business
{
    public class AccountMainModel : BaseModel<AccountMain>, IAccountMainModel
    {
        public IQueryable<AccountMain> List_Permission(int loginSystemUserID = 0)
        {
            if (loginSystemUserID != 0)
            {
                var systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
                var loginSystemUser = systemUserModel.Get(loginSystemUserID);
                if (loginSystemUser == null)
                {
                    throw new ApplicationException(SystemConst.Notice.NotAuthorized);
                }
                if (loginSystemUser.SystemUserRoleID == 1)
                {
                    return base.List().OrderByDescending(a => a.ID);
                }
                else
                {
                    return base.List().Where(a => a.SystemUserID == loginSystemUserID).OrderByDescending(a => a.ID);
                }
            }
            else
            {
                return base.List().OrderByDescending(a => a.ID);
            }
        }

        public AccountMain Get_Permission(int id, int loginSystemUserID)
        {
            if (!CheckHasPermissions(loginSystemUserID, id))
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            return Get(id);
        }

        [Transaction]
        public Result Add(AccountMain accountMain, HttpPostedFileBase LogoImagePath, int createUserID)
        {
            accountMain.SystemUserID = createUserID;
            accountMain.CreateTime = DateTime.Now;
            accountMain.LogoImagePath = SystemConst.Business.DefaultLogo;
            accountMain.LogoImageThumbnailPath = SystemConst.Business.DefaultLogo;
            accountMain.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            var result = base.Add(accountMain);
            if (result.HasError == false && LogoImagePath != null)
            {
                try
                {
                    var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));//该开发商物理路径
                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }
                    //3个文件目录
                    var basePath = string.Format("{0}\\{1}", path, "Base");//Base目录物理路径
                    if (Directory.Exists(basePath) == false)
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    var accountPath = string.Format("{0}\\{1}", path, "Account");//Account目录物理路径
                    if (Directory.Exists(accountPath) == false)
                    {
                        Directory.CreateDirectory(accountPath);
                    }
                    var fileLibraryPath = string.Format("{0}\\{1}", path, "FileLibrary");//FileLibrary目录物理路径
                    if (Directory.Exists(fileLibraryPath) == false)
                    {
                        Directory.CreateDirectory(fileLibraryPath);
                    }
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}", token, LogoImagePath.FileName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, width, LogoImagePath.FileName);
                    var imagePath = string.Format("{0}\\{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", basePath, imageThumbnailName);
                    LogoImagePath.SaveAs(imagePath);
                    //缩略图
                    Tool.Thumbnail(imagePath, imageThumbnailPath, width);
                    accountMain.LogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageThumbnailName;
                    result = Edit(accountMain);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        [Transaction]
        public Result Edit_Permission(AccountMain accountMain, HttpPostedFileBase LogoImagePath, int loginSystemUserID = 0)
        {
            if (!CheckHasPermissions(loginSystemUserID, accountMain.ID))
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            var result = base.Edit(accountMain);
            if (result.HasError == false && LogoImagePath != null)
            {
                try
                {
                    //删除原logo及缩略图
                    var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));
                    var basePath = string.Format("{0}\\{1}", path, "Base");
                    var logoImageAbsolutePath = HttpContext.Current.Server.MapPath(accountMain.LogoImagePath);
                    if (File.Exists(logoImageAbsolutePath))
                    {
                        File.Delete(logoImageAbsolutePath);
                    }
                    var logoImageThumbnailPath = HttpContext.Current.Server.MapPath(accountMain.LogoImageThumbnailPath);
                    if (File.Exists(logoImageThumbnailPath))
                    {
                        File.Delete(logoImageThumbnailPath);
                    }
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}", token, LogoImagePath.FileName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, width, LogoImagePath.FileName);
                    var imagePath = string.Format("{0}\\{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", basePath, imageThumbnailName);
                    LogoImagePath.SaveAs(imagePath);
                    //缩略图
                    Tool.Thumbnail(imagePath, imageThumbnailPath, width);
                    accountMain.LogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageThumbnailName;
                    result = Edit(accountMain);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (result.HasError == false)
            {
                IAccountMainExpandInfoModel accountMainExpandInfoModel = Factory.Get<IAccountMainExpandInfoModel>(SystemConst.IOC_Model.AccountMainExpandInfoModel);
                result = accountMainExpandInfoModel.Edit(accountMain.AccountMainExpandInfo);
            }
            return result;
        }

        [Transaction]
        public Result Delete_Permission(int id, int loginSystemUserID = 0)
        {
            if (!CheckHasPermissions(loginSystemUserID, id))
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            //删除文件目录
            var directory = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", id));
            if (Directory.Exists(directory))
            {
                //Directory.Delete(directory, true);是否真正删除
            }
            var result = base.Delete(id);
            if (result.HasError == false)
            {
                IAccountMainExpandInfoModel accountMainExpandInfoModel = Factory.Get<IAccountMainExpandInfoModel>(SystemConst.IOC_Model.AccountMainExpandInfoModel);
                result = accountMainExpandInfoModel.Delete(id);
            }
            if (result.HasError == false)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                var account_accountMain_ids = account_accountMainModel.List().Where(a => a.AccountMainID == id).Select(a => a.ID).ToArray();
                var account_ids = account_accountMainModel.List().Where(a => a.AccountMainID == id).Select(a => a.AccountID).ToArray();
                foreach (var item in account_accountMain_ids)
                {
                    account_accountMainModel.Delete(item);
                }
                var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                foreach (var item in account_ids)
                {
                    accountModel.Delete(item);
                }
            }
            return result;
        }

        public Result ChangeStatus_Permission(int accountMainID, EnumAccountStatus status, int loginSystemUserID = 0)
        {
            if (!CheckHasPermissions(loginSystemUserID, accountMainID))
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            var entity = Get(accountMainID);
            entity.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(status);
            return Edit(entity);
        }

        /// <summary>
        /// 严查是否有权限操作数据
        /// </summary>
        /// <param name="accountMainID">数据ID</param>
        /// <param name="loginSystemUserID">授权用户ID（0：为不检查）</param>
        /// <returns>ture:有权 false:无权</returns>
        public bool CheckHasPermissions(int loginSystemUserID, params int[] accountMainID)
        {
            if (loginSystemUserID != 0)
            {
                var systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
                var loginSystemUser = systemUserModel.Get(loginSystemUserID);
                if (loginSystemUser == null)
                {
                    return false;
                }
                if (loginSystemUser.SystemUserRoleID == 1)
                {
                    return true;
                }
                else
                {
                    return base.List().Any(a => accountMainID.Contains(a.ID) && a.SystemUserID == loginSystemUserID);
                }
            }
            else
            {
                return true;
            }
        }

        public double GetFileLimitUseInfo(int accountMainID)
        {
            var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMainID));//该开发商物理路径
            if (Directory.Exists(path) == false)
            {
                throw new ApplicationException("没有找到该项目的文件路径。");
            }
            Scripting.FileSystemObject fso = new Scripting.FileSystemObjectClass();
            double value_Byte = Convert.ToDouble(fso.GetFolder(path).Size);
            double value_GB = value_Byte / 1024 / 1024 / 1024;
            return value_GB;
        }

        public AccountMainLibraryInfo GetAccountMainLibraryInfo(int accountMainID)
        {
            var accountMain = Get(accountMainID);
            if (accountMain != null)
            {
                AccountMainLibraryInfo entity = new AccountMainLibraryInfo();
                entity.AccountMainID = accountMainID;
                entity.LibraryTextCount = accountMain.LibraryTexts.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                entity.LibraryImageCount = accountMain.LibraryImages.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                entity.LibraryImageTextCount = accountMain.LibraryImageTexts.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                entity.LibraryVideoCount = accountMain.LibraryVideos.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                entity.LibraryVoiceCount = accountMain.LibraryVoices.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                return entity;
            }
            return null;
        }
    }
}
