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
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Imaging;

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
        /// <summary>
        /// 获取项目成员(不包含管理员)
        /// </summary>
        public IList<Account> GetAccountListNoAdminByAccountMain(int accountMainID)
        {
            var entity = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            return entity.List().Where(a => a.AccountMainID == accountMainID & a.Account.RoleID != 1).Select(a => a.Account).ToList();
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
            return GetAccountListByAccountMain(accountMainID).Where(a => a.RoleID == 1 && a.Role.IsCanDelete == false);
        }

        public IQueryable<Account> GetAccountAdminListByAccountMain(AccountMain accountMain)
        {
            var Rolemodel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            int Roleid = 0;
            var Role = Rolemodel.GetListByAMID(accountMain.ID).Where(a => a.IsCanDelete == false);
            if (Role != null)
            {
                Roleid = Role.FirstOrDefault().ID;

            }
            return accountMain.Account_AccountMains.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Select(a => a.Account).Where(a => a.RoleID == Roleid).AsQueryable();
        }
        public IQueryable<Account> GetAccountAdminListByAccountMain(AccountMain accountMain, int RoleID, int AccountID)
        {
            return accountMain.Account_AccountMains.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Select(a => a.Account).Where(a => a.RoleID == RoleID && a.ID != AccountID).AsQueryable();
        }
        /// <summary>
        /// 根据角色获取项目成员
        /// </summary>
        public IQueryable<Account> GetAccountListByAccountMain_RoleID(int accountMainID, int roleID, int accountID)
        {
            return GetAccountListByAccountMain(accountMainID).Where(a => a.RoleID == roleID && a.ID != accountID);
        }

        public IQueryable<Account> GetAccountListByAccountMain_RoleID(AccountMain accountMain, int roleID)
        {
            return accountMain.Account_AccountMains.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active).Select(a => a.Account).Where(a => a.RoleID == roleID).AsQueryable();
        }

        [Transaction]
        public Result Add(Account account, int accountMainID, System.Web.HttpPostedFileBase HeadImagePathFile)
        {
            Result result = new Result();
            if (account.RoleID == 1)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID))
                {
                    result.Error = SystemConst.Notice.MultipleAccountMainAdminAccount;
                    return result;
                }
            }
            account.HeadImagePath = SystemConst.Business.DefaultHeadImage;
            account.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwdPage);
            account.IsActivated = true;
            result = base.Add(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}", token, HeadImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, width, HeadImagePathFile.FileName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", accountPath, imageThumbnailName);
                    HeadImagePathFile.SaveAs(imagePath);
                    //缩略图
                    if (Tool.Thumbnail(imagePath, imageThumbnailPath, width))
                    {
                        account.HeadImagePath = path + imageThumbnailName;
                        //删除原头像
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }
                    else
                    {
                        account.HeadImagePath = path + imageName;
                    }
                    result = Edit(account);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (result.HasError == false)
            {
                var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
                result = groupModel.AddDefaultGroup(account.ID, accountMainID);
            }
            if (result.HasError == false)
            {
                SMS_Model smsModel = new SMS_Model();
                smsModel.Send_AccountRegister(account.ID);
            }
            return result;
        }

        public Result Add(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            //获取项目管理员角色ID
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainID);
            Result result = new Result();
            if (account.RoleID == sysRoleID)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID))
                {
                    result.Error = SystemConst.Notice.MultipleAccountMainAdminAccount;
                    return result;
                }
            }
            account.HeadImagePath = SystemConst.Business.DefaultHeadImage;
            account.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwd);
            account.IsActivated = true;
            result = base.Add(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HeadImagePathFile.FileName.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageName2 = string.Format("{0}_M_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                    HeadImagePathFile.SaveAs(imagePath);

                    account.HeadImagePath = path + imageName;
                    if (width > 0)
                    {
                        System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                        {
                            int ToWidth = width;
                            int ToHeight = height;
                            int ToX1 = x1;
                            int ToY1 = y1;

                            Bitmap sourceBitmap = new Bitmap(imagePath);

                            int YW = sourceBitmap.Width;
                            int YH = sourceBitmap.Height;

                            if (YH != Theight)
                            {
                                double ratio = double.Parse(YH.ToString()) / double.Parse(Theight.ToString());
                                //ratio = Math.Round(ratio, 2);
                                ToWidth = (int)(ToWidth * ratio);
                                ToHeight = (int)(ToHeight * ratio);
                                ToX1 = (int)(ToX1 * ratio);
                                ToY1 = (int)(ToY1 * ratio);
                            }
                            Bitmap resultBitmap = new Bitmap(ToWidth, ToHeight);

                            using (Graphics g = Graphics.FromImage(resultBitmap))
                            {
                                Rectangle resultRectangle = new Rectangle(0, 0, ToWidth, ToHeight);
                                Rectangle sourceRectangle = new Rectangle(0 + ToX1, 0 + ToY1, ToWidth, ToHeight);
                                g.DrawImage(sourceBitmap, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
                            }
                            EncoderParameters ep = new EncoderParameters();

                            resultBitmap.Save(imagePath2, sourceBitmap.RawFormat);
                            resultBitmap.Dispose();
                            sourceBitmap.Dispose();
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }
                        });
                        t.Start();
                        account.HeadImagePath = path + imageName2;
                    }

                    result = Edit(account);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (result.HasError == false)
            {
                var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
                result = groupModel.AddDefaultGroup(account.ID, accountMainID);
            }
            return result;
        }

        public Result Edit(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            //获取项目管理员角色ID
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainID);
            Result result = new Result();
            if (account.RoleID == sysRoleID)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID, account.ID))
                {
                    result.Error = SystemConst.Notice.MultipleAccountMainAdminAccount;
                    return result;
                }
            }
            //检查邮箱是否唯一
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var isOk = model.CheckIsUnique("Account", "Email", account.Email, account.ID);
            if (isOk == false)
            {
                result.Error = "该邮箱已被其他账号使用，请修改邮箱。";
                return result;
            }
            //account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwdPage);
            result = base.Edit(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    //删除原头像
                    if (account.HeadImagePath != "~/Images/default_Avatar.png")
                    {
                        var file = HttpContext.Current.Server.MapPath(account.HeadImagePath);
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HeadImagePathFile.FileName.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageName2 = string.Format("{0}_M_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                    HeadImagePathFile.SaveAs(imagePath);

                    account.HeadImagePath = path + imageName;
                    if (width > 0)
                    {
                        System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                        {
                            int ToWidth = width;
                            int ToHeight = height;
                            int ToX1 = x1;
                            int ToY1 = y1;

                            Bitmap sourceBitmap = new Bitmap(imagePath);

                            int YW = sourceBitmap.Width;
                            int YH = sourceBitmap.Height;


                            if (YH != Theight)
                            {
                                double ratio = double.Parse(YH.ToString()) / double.Parse(Theight.ToString());
                                //ratio = Math.Round(ratio, 2);
                                ToWidth = (int)(ToWidth * ratio);
                                ToHeight = (int)(ToHeight * ratio);
                                ToX1 = (int)(ToX1 * ratio);
                                ToY1 = (int)(ToY1 * ratio);
                            }
                            Bitmap resultBitmap = new Bitmap(ToWidth, ToHeight);

                            using (Graphics g = Graphics.FromImage(resultBitmap))
                            {
                                Rectangle resultRectangle = new Rectangle(0, 0, ToWidth, ToHeight);
                                Rectangle sourceRectangle = new Rectangle(0 + ToX1, 0 + ToY1, ToWidth, ToHeight);
                                g.DrawImage(sourceBitmap, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
                            }
                            EncoderParameters ep = new EncoderParameters();

                            resultBitmap.Save(imagePath2, sourceBitmap.RawFormat);
                            resultBitmap.Dispose();
                            sourceBitmap.Dispose();
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }
                        });
                        t.Start();
                        account.HeadImagePath = path + imageName2;
                    }


                    result = Edit(account);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public Result SetEdit(Account account, int accountMainID, HttpPostedFileBase HeadImagePathFile, int x1, int y1, int width, int height, int Twidth, int Theight)
        {
            //获取项目管理员角色ID
            IRoleModel roleModel = Factory.Get<IRoleModel>(SystemConst.IOC_Model.RoleModel);
            int sysRoleID = roleModel.GetRoleIscandelete(accountMainID);
            Result result = new Result();
            if (account.RoleID == sysRoleID)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID, account.ID))
                {
                    result.Error = SystemConst.Notice.MultipleAccountMainAdminAccount;
                    return result;
                }
            }
            //检查邮箱是否唯一
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var isOk = model.CheckIsUnique("Account", "Email", account.Email, account.ID);
            if (isOk == false)
            {
                result.Error = "该邮箱已被其他账号使用，请修改邮箱。";
                return result;
            }
            account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwdPage);
            result = base.Edit(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    //删除原头像
                    if (account.HeadImagePath != "~/Images/default_Avatar.png")
                    {
                        var file = HttpContext.Current.Server.MapPath(account.HeadImagePath);
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HeadImagePathFile.FileName.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageName2 = string.Format("{0}_M_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imagePath2 = string.Format("{0}\\{1}", accountPath, imageName2);
                    HeadImagePathFile.SaveAs(imagePath);

                    account.HeadImagePath = path + imageName;
                    if (width > 0)
                    {
                        System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                        {
                            int ToWidth = width;
                            int ToHeight = height;
                            int ToX1 = x1;
                            int ToY1 = y1;

                            Bitmap sourceBitmap = new Bitmap(imagePath);

                            int YW = sourceBitmap.Width;
                            int YH = sourceBitmap.Height;


                            if (YH != Theight)
                            {
                                double ratio = double.Parse(YH.ToString()) / double.Parse(Theight.ToString());
                                //ratio = Math.Round(ratio, 2);
                                ToWidth = (int)(ToWidth * ratio);
                                ToHeight = (int)(ToHeight * ratio);
                                ToX1 = (int)(ToX1 * ratio);
                                ToY1 = (int)(ToY1 * ratio);
                            }
                            Bitmap resultBitmap = new Bitmap(ToWidth, ToHeight);

                            using (Graphics g = Graphics.FromImage(resultBitmap))
                            {
                                Rectangle resultRectangle = new Rectangle(0, 0, ToWidth, ToHeight);
                                Rectangle sourceRectangle = new Rectangle(0 + ToX1, 0 + ToY1, ToWidth, ToHeight);
                                g.DrawImage(sourceBitmap, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
                            }
                            EncoderParameters ep = new EncoderParameters();

                            resultBitmap.Save(imagePath2, sourceBitmap.RawFormat);
                            resultBitmap.Dispose();
                            sourceBitmap.Dispose();
                            if (File.Exists(imagePath))
                            {
                                File.Delete(imagePath);
                            }
                        });
                        t.Start();
                        account.HeadImagePath = path + imageName2;
                    }


                    result = Edit(account);
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
            Result result = new Result();
            if (account.RoleID == 1)
            {
                var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
                if (account_accountMainModel.CheckIsExistAccountAdmin(accountMainID, account.ID))
                {
                    result.Error = SystemConst.Notice.MultipleAccountMainAdminAccount;
                    return result;
                }
            }
            //检查邮箱是否唯一
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var isOk = model.CheckIsUnique("Account", "Email", account.Email, account.ID);
            if (isOk == false)
            {
                result.Error = "该邮箱已被其他账号使用，请修改邮箱。";
                return result;
            }
            account.LoginPwd = DESEncrypt.Encrypt(account.LoginPwdPage);
            result = base.Edit(account);
            if (result.HasError == false && HeadImagePathFile != null)
            {
                try
                {
                    //删除原头像
                    if (account.HeadImagePath != "~/Images/default_Avatar.png")
                    {
                        var file = HttpContext.Current.Server.MapPath(account.HeadImagePath);
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                    }
                    var path = string.Format(SystemConst.Business.PathAccount, accountMainID);
                    var accountPath = HttpContext.Current.Server.MapPath(path);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var width = 80;
                    var imageName = string.Format("{0}_{1}", token, HeadImagePathFile.FileName);
                    var imagePath = string.Format("{0}\\{1}", accountPath, imageName);
                    var imageThumbnailName = string.Format("{0}_{1}_{2}", token, width, HeadImagePathFile.FileName);
                    var imageThumbnailPath = string.Format("{0}\\{1}", accountPath, imageThumbnailName);
                    HeadImagePathFile.SaveAs(imagePath);
                    //缩略图
                    if (Tool.Thumbnail(imagePath, imageThumbnailPath, width))
                    {
                        account.HeadImagePath = path + imageThumbnailName;
                        //删除原头像
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }
                    else
                    {
                        account.HeadImagePath = path + imageName;
                    }
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
            if (entity.HeadImagePath != "~/Images/default_Avatar.png")
            {
                var file = HttpContext.Current.Server.MapPath(entity.HeadImagePath);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
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

        public Result ChangeStatus(int accountID, EnumAccountStatus status, int accountMainID)
        {
            Result result = new Result();
            var account_accountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            if (status == EnumAccountStatus.Enabled && account_accountMainModel.CheckIsExistAccountAdmin(accountMainID, accountID))
            {
                result.Error = SystemConst.Notice.MultipleAccountMainAdminAccount;
                return result;
            }
            var entity = Get(accountID);
            base.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(status);
            return Edit(entity);
        }

        public Result Login(string phone_email, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var account = List().Where(a => (a.Email.Equals(phone_email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd)) ||
                (a.Phone.Equals(phone_email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd))).FirstOrDefault();
            if (account == null)
            {
                result.Error = "用户名或密码错误。";
                return result;
            }
            var accountMain = account.Account_AccountMains.FirstOrDefault();
            if (accountMain.AccountMain.AccountStatus.Token.Equals(EnumAccountStatus.Disabled.ToString(), StringComparison.CurrentCultureIgnoreCase)
                || account.AccountStatus.Token.Equals(EnumAccountStatus.Disabled.ToString(), StringComparison.CurrentCultureIgnoreCase)
            || accountMain.SystemStatus == (int)EnumSystemStatus.Delete)
            {
                result.Error = "账号不可用。";
                return result;
            }
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
            entity.CurrentAccountMainName = account.Account_AccountMains.FirstOrDefault().AccountMain.Name;
            entity.LogoPath = account.Account_AccountMains.FirstOrDefault().AccountMain.LogoImagePath;
            entity.LogoThumbnailPath = account.Account_AccountMains.FirstOrDefault().AccountMain.LogoImageThumbnailPath;
            HttpContext.Current.Session[SystemConst.Session.LoginAccount] = entity;
            return result;
        }

        public Result LoginApp(string email, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var account = List().Where(a => (a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd)) ||
                (a.Phone.Equals(email, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(pwd))).FirstOrDefault();
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
                result.Entity = entity;
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

        public Result ResetPwd(int id, string pwd)
        {
            string sql = string.Format("update Account set loginPwd = '{0}' where id={1}", pwd, id);
            int cnt = base.SqlExecute(sql);
            Result result = new Result();
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Entity = "修改失败！";
            }
            return result;
        }

        /// <summary>
        /// 获取下级账号
        /// </summary>
        /// <param name="accountID">父级账号</param>
        /// <returns></returns>
        public List<Account> GetSubAccounts(int accountID)
        {
            return List().Where(a => a.ParentAccountID == accountID).ToList();
        }
    }
}
