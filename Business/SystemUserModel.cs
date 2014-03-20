using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Common;
using System.Web;
using Injection.Transaction;
using Poco.Enum;
using System.IO;
using Injection;

namespace Business
{
    public class SystemUserModel : BaseModel<SystemUser>, ISystemUserModel
    {
        public Result Login(string email, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var systemUser = List().Where(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd)).FirstOrDefault();
            if (systemUser == null)
            {
                result.Error = "用户名或密码错误";
            }
            else
            {
                SystemUser entity = new SystemUser();
                entity.ID = systemUser.ID;
                entity.SystemStatus = systemUser.SystemStatus;
                entity.Name = systemUser.Name;
                entity.Email = systemUser.Email;
                entity.LoginPwd = systemUser.LoginPwd;
                entity.HeadImage = systemUser.HeadImage;
                entity.Phone = systemUser.Phone;
                entity.AccountStatusID = systemUser.AccountStatusID;
                entity.SystemUserRoleID = systemUser.SystemUserRoleID;
                HttpContext.Current.Session[SystemConst.Session.LoginSystemUser] = entity;
            }
            return result;
        }

        public Result ChangeStatus(int ID, Poco.Enum.EnumAccountStatus status)
        {
            Result result = new Result();
            var entity = Get(ID);
            base.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(status);
            return Edit(entity);
        }

        [Transaction]
        public Result Add(SystemUser user, HttpPostedFileBase HeadImagePathFile)
        {
            Result result = new Result();
            if (user.SystemUserRoleID == 1)
            {
                result.Error = "不能添加该角色账号。";
                return result;
            }
            user.HeadImage = SystemConst.Business.DefaultHeadImage;
            user.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            user.LoginPwd = DESEncrypt.Encrypt(user.LoginPwdPage);
            result = base.Add(user);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    var p = "~/File/platform/";
                    var path = HttpContext.Current.Server.MapPath(p);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}", token, HeadImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, width, HeadImagePathFile.FileName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", path, imageThumbnailName);
                    HeadImagePathFile.SaveAs(imagePath);
                    //缩略图
                    if (Tool.Thumbnail(imagePath, imageThumbnailPath, width))
                    {
                        user.HeadImage = p + imageThumbnailName;
                        //删除原头像
                        File.Delete(imagePath);
                    }
                    else
                    {
                        user.HeadImage = p + imageName;
                    }
                    result = Edit(user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public Result Edit(SystemUser user, HttpPostedFileBase HeadImagePathFile)
        {
            Result result = new Result();
            //检查邮箱是否唯一
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var isOk = model.CheckIsUnique("Account", "Email", user.Email, user.ID);
            if (isOk == false)
            {
                result.Error = "该邮箱已被其他账号使用，请修改邮箱。";
                return result;
            }
            user.LoginPwd = DESEncrypt.Encrypt(user.LoginPwdPage);
            result = base.Edit(user);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    //删除原头像
                    var file = HttpContext.Current.Server.MapPath(user.HeadImage);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    var p = "~/File/platform/";
                    var path = HttpContext.Current.Server.MapPath(p);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}", token, HeadImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, width, HeadImagePathFile.FileName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", path, imageThumbnailName);
                    HeadImagePathFile.SaveAs(imagePath);
                    //缩略图
                    if (Tool.Thumbnail(imagePath, imageThumbnailPath, width))
                    {
                        user.HeadImage = p + imageThumbnailName;
                        //删除原头像
                        File.Delete(imagePath);
                    }
                    else
                    {
                        user.HeadImage = p + imageName;
                    }
                    result = Edit(user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }


        //删除角色校验
        public bool ChickDeleteRole(int roleID)
        {
            var list = List().Any(a => a.SystemUserRoleID == roleID);
            if (list)
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
