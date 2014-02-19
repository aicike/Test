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
using Poco.WebAPI_Poco;
using System.Drawing;

namespace Business
{
    public class AccountMainModel : BaseModel<AccountMain>, IAccountMainModel
    {
        /// <summary>
        /// 根据组织或集团账号，查找所有该集团下AccountMain
        /// </summary>
        /// <returns></returns>
        public IQueryable<AccountMain> ListForOrganization(int organization_accountMainID)
        {
            return List().Where(a => a.ParentAccountMainID == organization_accountMainID);
        }

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

        /// <summary>
        /// 平台添加方法
        /// </summary>
        [Transaction]
        public Result Add(AccountMain accountMain, HttpPostedFileBase LogoImagePath, int createUserID, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile)
        {
            accountMain.SystemUserID = createUserID;
            accountMain.CreateTime = DateTime.Now;
            accountMain.LogoImagePath = SystemConst.Business.DefaultLogo;
            accountMain.LogoImageThumbnailPath = SystemConst.Business.DefaultLogo;
            accountMain.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            CommonModel com = new CommonModel();
            accountMain.RandomCode = com.CreateRandom("", 6);
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
                    var basePath = string.Format("{0}/{1}", path, "Base");//Base目录物理路径
                    if (Directory.Exists(basePath) == false)
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    var accountPath = string.Format("{0}/{1}", path, "Account");//Account目录物理路径
                    if (Directory.Exists(accountPath) == false)
                    {
                        Directory.CreateDirectory(accountPath);
                    }
                    var fileLibraryPath = string.Format("{0}/{1}", path, "FileLibrary");//FileLibrary目录物理路径
                    if (Directory.Exists(fileLibraryPath) == false)
                    {
                        Directory.CreateDirectory(fileLibraryPath);
                    }
                    var LastName = com.CreateRandom("", 5) + LogoImagePath.FileName.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var height = 58;
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, height, LastName);
                    var appimgName = string.Format("{0}_{1}_APP{2}", token, height, LastName);
                    var imagePath = string.Format("{0}/{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}/{1}", basePath, imageThumbnailName);
                    var appimgPath = string.Format("{0}/{1}", basePath, appimgName);
                    LogoImagePath.SaveAs(imagePath);
                    //缩略图
                    bool IsOK = Tool.Thumbnail(imagePath, height, imageThumbnailPath);
                    accountMain.LogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    if (IsOK)
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageThumbnailName;
                    }
                    else
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    }

                    if (AndroidPathFile != null)
                    {
                        var androidPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                        var androidPathDown = string.Format("{0}/{1}_{2}", androidPath, token, AndroidPathFile.FileName.GetFileName());
                        if (Directory.Exists(androidPath) == false)
                        {
                            Directory.CreateDirectory(androidPath);
                        }
                        var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                        var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidPathFile.FileName.GetFileName());
                        AndroidPathFile.SaveAs(androidPathDown);
                        accountMain.AndroidDownloadPath = Downpath2;
                    }

                    if (AndroidSellPathFile != null)
                    {
                        var androidSellPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                        var androidSellPathDown = string.Format("{0}/{1}_{2}", androidSellPath, token, AndroidSellPathFile.FileName.GetFileName());
                        if (Directory.Exists(androidSellPath) == false)
                        {
                            Directory.CreateDirectory(androidSellPath);
                        }
                        var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                        var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidSellPathFile.FileName.GetFileName());
                        AndroidSellPathFile.SaveAs(androidSellPathDown);
                        accountMain.AndroidSellDownloadPath = Downpath2;
                    }

                    if (AppLogoImageFile != null)
                    {

                        int dataLengthToRead = (int)AppLogoImageFile.InputStream.Length;//获取下载的文件总大小
                        byte[] buffer = new byte[dataLengthToRead];


                        int r = AppLogoImageFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                        Stream tream = new MemoryStream(buffer);
                        Image img = Image.FromStream(tream);

                        Tool.SuperGetPicThumbnail(img, appimgPath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                        accountMain.AppLogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + appimgName;
                    }
                    result = Edit(accountMain);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                string sql = string.Format("INSERT INTO dbo.Role ( SystemStatus, NAME,IsCanDelete,Token,AccountMainID ) VALUES  ( 0,'管理员',0,'AccountAdmin',{0})"
                           + " insert into classify(SystemStatus,AccountMainID,Name,ParentID,[Level],sort,Subordinate) values(0,{0},'{1}',0,0,0,0)", accountMain.ID, accountMain.Name);
                base.SqlExecute(sql);
            }
            return result;
        }

        /// <summary>
        /// 组织或机构，自己添加项目方法
        /// </summary>
        [Transaction]
        public Result Add(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile)
        {
            accountMain.CreateTime = DateTime.Now;
            accountMain.LogoImagePath = SystemConst.Business.DefaultLogo;
            accountMain.LogoImageThumbnailPath = SystemConst.Business.DefaultLogo;
            accountMain.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            CommonModel com = new CommonModel();
            accountMain.RandomCode = com.CreateRandom("", 6);
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
                    var basePath = string.Format("{0}/{1}", path, "Base");//Base目录物理路径
                    if (Directory.Exists(basePath) == false)
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    var accountPath = string.Format("{0}/{1}", path, "Account");//Account目录物理路径
                    if (Directory.Exists(accountPath) == false)
                    {
                        Directory.CreateDirectory(accountPath);
                    }
                    var fileLibraryPath = string.Format("{0}/{1}", path, "FileLibrary");//FileLibrary目录物理路径
                    if (Directory.Exists(fileLibraryPath) == false)
                    {
                        Directory.CreateDirectory(fileLibraryPath);
                    }
                    var LastName = com.CreateRandom("", 5) + LogoImagePath.FileName.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var height = 58;
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, height, LastName);
                    var appimgName = string.Format("{0}_{1}_APP{2}", token, height, LastName);
                    var imagePath = string.Format("{0}/{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}/{1}", basePath, imageThumbnailName);
                    var appimgPath = string.Format("{0}/{1}", basePath, appimgName);
                    LogoImagePath.SaveAs(imagePath);
                    //缩略图
                    bool IsOK = Tool.Thumbnail(imagePath, height, imageThumbnailPath);
                    accountMain.LogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    if (IsOK)
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageThumbnailName;
                    }
                    else
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    }

                    if (AndroidPathFile != null)
                    {
                        var androidPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                        var androidPathDown = string.Format("{0}/{1}_{2}", androidPath, token, AndroidPathFile.FileName.GetFileName());
                        if (Directory.Exists(androidPath) == false)
                        {
                            Directory.CreateDirectory(androidPath);
                        }
                        var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                        var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidPathFile.FileName.GetFileName());
                        AndroidPathFile.SaveAs(androidPathDown);
                        accountMain.AndroidDownloadPath = Downpath2;
                    }

                    if (AndroidSellPathFile != null)
                    {
                        var androidSellPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                        var androidSellPathDown = string.Format("{0}/{1}_{2}", androidSellPath, token, AndroidSellPathFile.FileName.GetFileName());
                        if (Directory.Exists(androidSellPath) == false)
                        {
                            Directory.CreateDirectory(androidSellPath);
                        }
                        var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                        var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidSellPathFile.FileName.GetFileName());
                        AndroidSellPathFile.SaveAs(androidSellPathDown);
                        accountMain.AndroidSellDownloadPath = Downpath2;
                    }

                    if (AppLogoImageFile != null)
                    {

                        int dataLengthToRead = (int)AppLogoImageFile.InputStream.Length;//获取下载的文件总大小
                        byte[] buffer = new byte[dataLengthToRead];


                        int r = AppLogoImageFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                        Stream tream = new MemoryStream(buffer);
                        Image img = Image.FromStream(tream);

                        Tool.SuperGetPicThumbnail(img, appimgPath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                        accountMain.AppLogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + appimgName;
                    }
                    result = Edit(accountMain);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                string sql = string.Format("INSERT INTO dbo.Role ( SystemStatus, NAME,IsCanDelete,Token,AccountMainID ) VALUES  ( 0,'管理员',0,'AccountAdmin',{0})"
                           + " insert into classify(SystemStatus,AccountMainID,Name,ParentID,[Level],sort,Subordinate) values(0,{0},'{1}',0,0,0,0)", accountMain.ID, accountMain.Name);
                base.SqlExecute(sql);
            }
            return result;
        }
        
        [Transaction]
        public Result MicroSite_Add(AccountMain accountMain)
        {
            accountMain.LogoImagePath = SystemConst.Business.DefaultLogo;
            accountMain.LogoImageThumbnailPath = SystemConst.Business.DefaultLogo;
            var result = base.Add(accountMain);
            if (result.HasError == false)
            {
                try
                {
                    var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));//该开发商物理路径
                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }
                    //3个文件目录
                    var basePath = string.Format("{0}/{1}", path, "Base");//Base目录物理路径
                    if (Directory.Exists(basePath) == false)
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    var accountPath = string.Format("{0}/{1}", path, "Account");//Account目录物理路径
                    if (Directory.Exists(accountPath) == false)
                    {
                        Directory.CreateDirectory(accountPath);
                    }
                    var fileLibraryPath = string.Format("{0}/{1}", path, "FileLibrary");//FileLibrary目录物理路径
                    if (Directory.Exists(fileLibraryPath) == false)
                    {
                        Directory.CreateDirectory(fileLibraryPath);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (result.HasError == false)
            {
                try
                {
                    string sql = string.Format("INSERT INTO dbo.Role ( SystemStatus, NAME,IsCanDelete,Token,AccountMainID ) VALUES  ( 0,'管理员',0,'AccountAdmin',{0})"
                               + " insert into classify(SystemStatus,AccountMainID,Name,ParentID,[Level],sort,Subordinate) values(0,{0},'{1}',0,0,0,0)", accountMain.ID, accountMain.Name);
                    base.SqlExecute(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        [Transaction]
        public Result Edit_Permission(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile, int loginSystemUserID = 0)
        {
            if (!CheckHasPermissions(loginSystemUserID, accountMain.ID))
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }

            var result = base.Edit(accountMain);
            if (result.HasError == false && AppLogoImageFile != null)
            {

                var AppLogoImageAbsolute = HttpContext.Current.Server.MapPath(accountMain.AppLogoImagePath);
                if (File.Exists(AppLogoImageAbsolute))
                {
                    File.Delete(AppLogoImageAbsolute);
                }
                CommonModel com2 = new CommonModel();
                var LastName = com2.CreateRandom("", 5) + AppLogoImageFile.FileName.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));
                var basePath = string.Format("{0}/{1}", path, "Base");
                var appimgName = string.Format("{0}_APP{1}", token, LastName);
                var appimgPath = string.Format("{0}/{1}", basePath, appimgName);

                int dataLengthToRead = (int)AppLogoImageFile.InputStream.Length;//获取下载的文件总大小
                byte[] buffer = new byte[dataLengthToRead];


                int r = AppLogoImageFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                Stream tream = new MemoryStream(buffer);
                Image img = Image.FromStream(tream);

                Tool.SuperGetPicThumbnail(img, appimgPath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                accountMain.AppLogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + appimgName;
                result = base.Edit(accountMain);
            }
            if (result.HasError == false && LogoImagePath != null)
            {
                try
                {
                    //删除原logo及缩略图
                    var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));
                    var basePath = string.Format("{0}/{1}", path, "Base");
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
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + LogoImagePath.FileName.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var height = 58;
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, height, LastName);
                    var appimgName = string.Format("{0}_{1}_APP{2}", token, height, LastName);
                    var imagePath = string.Format("{0}/{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}/{1}", basePath, imageThumbnailName);
                    var appimgPath = string.Format("{0}/{1}", basePath, appimgName);
                    LogoImagePath.SaveAs(imagePath);
                    //缩略图
                    bool IsOK = Tool.Thumbnail(imagePath, height, imageThumbnailPath);
                    accountMain.LogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    if (IsOK)
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageThumbnailName;
                    }
                    else
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    }


                    result = Edit(accountMain);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            try
            {
                if (result.HasError == false && AndroidPathFile != null)
                {
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //删除原路径
                    var androidAbsolutePath = HttpContext.Current.Server.MapPath(accountMain.AndroidDownloadPath);
                    if (File.Exists(androidAbsolutePath))
                    {
                        File.Delete(androidAbsolutePath);
                    }
                    var androidPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                    var androidPathDown = string.Format("{0}/{1}_{2}", androidPath, token, AndroidPathFile.FileName.GetFileName());
                    if (Directory.Exists(androidPath) == false)
                    {
                        Directory.CreateDirectory(androidPath);
                    }
                    var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                    var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidPathFile.FileName.GetFileName());

                    AndroidPathFile.SaveAs(androidPathDown);
                    accountMain.AndroidDownloadPath = Downpath2;
                }

                if (result.HasError == false && AndroidSellPathFile != null)
                {
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //删除原路径
                    var androidAbsolutePath = HttpContext.Current.Server.MapPath(accountMain.AndroidSellDownloadPath);
                    if (File.Exists(androidAbsolutePath))
                    {
                        File.Delete(androidAbsolutePath);
                    }
                    var androidPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                    var androidPathDown = string.Format("{0}/{1}_{2}", androidPath, token, AndroidSellPathFile.FileName.GetFileName());
                    if (Directory.Exists(androidPath) == false)
                    {
                        Directory.CreateDirectory(androidPath);
                    }
                    var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                    var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidSellPathFile.FileName.GetFileName());

                    AndroidSellPathFile.SaveAs(androidPathDown);
                    accountMain.AndroidSellDownloadPath = Downpath2;
                }

                result = Edit(accountMain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (result.HasError == false)
            {
                //IAccountMainExpandInfoModel accountMainExpandInfoModel = Factory.Get<IAccountMainExpandInfoModel>(SystemConst.IOC_Model.AccountMainExpandInfoModel);
                //result = accountMainExpandInfoModel.Edit(accountMain.AccountMainExpandInfo);
            }
            return result;
        }

        [Transaction]
        public Result Edit_ByAccountMain(AccountMain accountMain, HttpPostedFileBase LogoImagePath, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile)
        {
            var result = base.Edit(accountMain);
            if (result.HasError == false && LogoImagePath != null)
            {
                try
                {
                    //删除原logo及缩略图
                    var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));
                    var basePath = string.Format("{0}/{1}", path, "Base");
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
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + LogoImagePath.FileName.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var height = 58;
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, height, LastName);
                    var imagePath = string.Format("{0}/{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}/{1}", basePath, imageThumbnailName);
                    LogoImagePath.SaveAs(imagePath);
                    //缩略图
                    bool IsOK = Tool.Thumbnail(imagePath, height, imageThumbnailPath);
                    accountMain.LogoImagePath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    if (IsOK)
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageThumbnailName;
                    }
                    else
                    {
                        accountMain.LogoImageThumbnailPath = string.Format(SystemConst.Business.PathBase, accountMain.ID) + imageName;
                    }

                    result = Edit(accountMain);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            try
            {
                if (result.HasError == false && AndroidPathFile != null)
                {
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //删除原路径
                    var androidAbsolutePath = HttpContext.Current.Server.MapPath(accountMain.AndroidDownloadPath);
                    if (File.Exists(androidAbsolutePath))
                    {
                        File.Delete(androidAbsolutePath);
                    }
                    var androidPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                    var androidPathDown = string.Format("{0}/{1}_{2}", androidPath, token, AndroidPathFile.FileName.GetFileName());
                    if (Directory.Exists(androidPath) == false)
                    {
                        Directory.CreateDirectory(androidPath);
                    }
                    var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                    var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidPathFile.FileName.GetFileName());

                    AndroidPathFile.SaveAs(androidPathDown);
                    accountMain.AndroidDownloadPath = Downpath2;
                }

                if (result.HasError == false && AndroidSellPathFile != null)
                {
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //删除原路径
                    var androidAbsolutePath = HttpContext.Current.Server.MapPath(accountMain.AndroidSellDownloadPath);
                    if (File.Exists(androidAbsolutePath))
                    {
                        File.Delete(androidAbsolutePath);
                    }
                    var androidPath = HttpContext.Current.Server.MapPath(string.Format("~/Download/{0}", accountMain.ID));
                    var androidPathDown = string.Format("{0}/{1}_{2}", androidPath, token, AndroidSellPathFile.FileName.GetFileName());
                    if (Directory.Exists(androidPath) == false)
                    {
                        Directory.CreateDirectory(androidPath);
                    }
                    var Downpath = string.Format("~/Download/{0}", accountMain.ID);
                    var Downpath2 = string.Format("{0}/{1}_{2}", Downpath, token, AndroidSellPathFile.FileName.GetFileName());

                    AndroidSellPathFile.SaveAs(androidPathDown);
                    accountMain.AndroidSellDownloadPath = Downpath2;
                }

                result = Edit(accountMain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (result.HasError == false)
            {
                //IAccountMainExpandInfoModel accountMainExpandInfoModel = Factory.Get<IAccountMainExpandInfoModel>(SystemConst.IOC_Model.AccountMainExpandInfoModel);
                //result = accountMainExpandInfoModel.Edit(accountMain.AccountMainExpandInfo);
            }
            return result;
        }

        [Transaction]
        public Result Edit_ByAccountMain(AccountMain accountMain)
        {

            var ThAccountmain = this.Get(accountMain.ID);

            var result = base.Edit(accountMain);


            if (result.HasError == false && accountMain.LogoImagePath != ThAccountmain.LogoImagePath)
            {
                try
                {
                    //删除原logo及缩略图
                    var path = HttpContext.Current.Server.MapPath(string.Format("~/File/{0}", accountMain.ID));
                    var basePath = string.Format("{0}/{1}", path, "Base");
                    var logoImageAbsolutePath = HttpContext.Current.Server.MapPath(ThAccountmain.LogoImagePath);
                    if (File.Exists(logoImageAbsolutePath))
                    {
                        File.Delete(logoImageAbsolutePath);
                    }
                    var logoImageThumbnailPath = HttpContext.Current.Server.MapPath(ThAccountmain.LogoImageThumbnailPath);
                    if (File.Exists(logoImageThumbnailPath))
                    {
                        File.Delete(logoImageThumbnailPath);
                    }
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + accountMain.LogoImagePath.GetFileSuffix();
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var height = 58;
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, height, LastName);
                    var imagePath = string.Format("{0}/{1}", basePath, imageName);
                    var imageThumbnailPath = string.Format("{0}/{1}", basePath, imageThumbnailName);

                    var lsImgPath = accountMain.LogoImagePath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);




                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    //缩略图
                    Tool.SuperGetPicThumbnail(imagePath, imageThumbnailPath, 70, 0, 58, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);


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
                //IAccountMainExpandInfoModel accountMainExpandInfoModel = Factory.Get<IAccountMainExpandInfoModel>(SystemConst.IOC_Model.AccountMainExpandInfoModel);
                //result = accountMainExpandInfoModel.Edit(accountMain.AccountMainExpandInfo);
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
                entity.LibraryImageTextCount = accountMain.LibraryImageTexts.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active && a.LibraryImageTextParentID.HasValue == false).Count();
                entity.LibraryVideoCount = accountMain.LibraryVideos.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                entity.LibraryVoiceCount = accountMain.LibraryVoices.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Count();
                return entity;
            }
            return null;
        }

        public AppVersionInfo CheckAppVersion(EnumClientSystemType type, int amid, string version)
        {
            var accountMain = Get(amid);
            AppVersionInfo versionInfo = new AppVersionInfo();
            versionInfo.HasNewVersion = false;
            string appName = null;
            string v = null;
            if (accountMain != null)
            {
                switch (type)
                {
                    case EnumClientSystemType.IOS:
                        v = accountMain.IOSVersion;

                        break;
                    case EnumClientSystemType.Android:
                        v = accountMain.AndroidVersion;
                        if (string.IsNullOrEmpty(accountMain.AndroidDownloadPath) == false)
                        {
                            versionInfo.VersionCode = accountMain.AndroidVersion;
                            appName = accountMain.AndroidDownloadPath.Substring(accountMain.AndroidDownloadPath.LastIndexOf('_') + 1);
                            appName = appName.Substring(0, appName.LastIndexOf('.')) + versionInfo.VersionCode + ".apk";
                            string path = (accountMain.AndroidDownloadPath.Replace("~", ""));
                            path = path.Substring(0, path.LastIndexOf('/') + 1) + appName;
                            versionInfo.AppPath = SystemConst.WebUrlIP + (accountMain.AndroidDownloadPath.Replace("~", ""));
                            versionInfo.AppName = appName;
                        }
                        break;
                }
            }
            int rawNum = 0;
            int appNum = 0;
            if (string.IsNullOrEmpty(v) == false)
            {
                rawNum = Convert.ToInt32(v.Replace(".", ""));
            } if (string.IsNullOrEmpty(version) == false)
            {
                appNum = Convert.ToInt32(version.Replace(".", ""));
            }
            if (rawNum > appNum)
            {
                versionInfo.HasNewVersion = true;
            }
            return versionInfo;
        }

        public AppVersionInfo CheckAppSellVersion(EnumClientSystemType type, int amid, string version)
        {
            var accountMain = Get(amid);
            AppVersionInfo versionInfo = new AppVersionInfo();
            versionInfo.HasNewVersion = false;
            string appName = null;
            string v = null;
            if (accountMain != null)
            {
                switch (type)
                {
                    case EnumClientSystemType.IOS:
                        v = accountMain.IOSVersion;

                        break;
                    case EnumClientSystemType.Android:
                        v = accountMain.AndroidSellVersion;
                        if (string.IsNullOrEmpty(accountMain.AndroidSellDownloadPath) == false)
                        {
                            versionInfo.VersionCode = accountMain.AndroidSellVersion;
                            appName = accountMain.AndroidSellDownloadPath.Substring(accountMain.AndroidSellDownloadPath.LastIndexOf('_') + 1);
                            appName = appName.Substring(0, appName.LastIndexOf('.')) + versionInfo.VersionCode + ".apk";
                            string path = (accountMain.AndroidSellDownloadPath.Replace("~", ""));
                            path = path.Substring(0, path.LastIndexOf('/') + 1) + appName;
                            versionInfo.AppPath = SystemConst.WebUrlIP + (accountMain.AndroidSellDownloadPath.Replace("~", ""));
                            versionInfo.AppName = appName;
                        }
                        break;
                }
            }
            int rawNum = 0;
            int appNum = 0;
            if (string.IsNullOrEmpty(v) == false)
            {
                rawNum = Convert.ToInt32(v.Replace(".", ""));
            } if (string.IsNullOrEmpty(version) == false)
            {
                appNum = Convert.ToInt32(version.Replace(".", ""));
            }
            if (rawNum > appNum)
            {
                versionInfo.HasNewVersion = true;
            }
            return versionInfo;
        }


        /// <summary>
        /// 验证随机码与物业是否匹配 true：匹配，false:错误
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="RandomCode"></param>
        /// <returns></returns>
        public bool CheckPropertyRandomCode(int AccountMainID, string RandomCode)
        {
            var list = this.List().Where(a => a.ID == AccountMainID && a.RandomCode == RandomCode);
            if (list.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
