using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Poco.Enum;
using Common;
using Injection.Transaction;
using Poco.WebAPI_Poco;
using System.Web;
using System.Text.RegularExpressions;

namespace Business
{
    public class UserLoginInfoModel : BaseModel<UserLoginInfo>, IUserLoginInfoModel
    {
        private readonly object obj = new object();

        public new Result Edit(UserLoginInfo userLogiInfo)
        {
            Result result = new Result();
            bool isExist = List().Any(a => a.Email.Equals(userLogiInfo.Email, StringComparison.CurrentCultureIgnoreCase) && a.ID != userLogiInfo.ID && a.Email.Length > 0);
            if (isExist)
            {
                result.Error = "该邮箱已存在，无法保存。";
                return result;
            }
            return base.Edit(userLogiInfo);
        }

        [Transaction]
        public Result Register(App_UserLoginInfo userLoginInfo)
        {
            lock (obj)
            {
                Result result = new Result();
                bool isExist = false;

                UserLoginInfo oldUserLoginInfo = null;
                if (userLoginInfo.UserID.HasValue)
                {
                    oldUserLoginInfo = GetByUserID(userLoginInfo.UserID.Value);
                    if (oldUserLoginInfo != null)
                    {
                        isExist = ExistEmail(userLoginInfo.Email, oldUserLoginInfo.ID);
                    }
                }
                else
                {
                    isExist = ExistEmail(userLoginInfo.Email);
                }
                //检查邮箱是否通过，是否可以注册
                if (isExist)
                {
                    result.Error = "该邮箱已存在,不能创建账号.";
                    return result;
                }
                if (userLoginInfo.UserID.HasValue)
                {
                    if (oldUserLoginInfo != null)
                    {
                        isExist = ExistPhone(userLoginInfo.Phone, oldUserLoginInfo.ID);
                    }
                }
                else
                {
                    isExist = ExistPhone(userLoginInfo.Phone);
                }
                //检查电话是否通过，是否可以注册
                if (isExist)
                {
                    result.Error = "该电话已存在,不能创建账号.";
                    return result;
                }

                int userLoginInfoID = 0;
                if (userLoginInfo.UserID.HasValue && oldUserLoginInfo != null)
                {
                    //已有账号，需要修改userLoginInfo信息
                    oldUserLoginInfo.Email = userLoginInfo.Email;
                    oldUserLoginInfo.LoginPwd = DESEncrypt.Encrypt(userLoginInfo.Pwd);
                    oldUserLoginInfo.LoginPwdPage = "000000";
                    oldUserLoginInfo.Phone = userLoginInfo.Phone;
                    oldUserLoginInfo.Name = userLoginInfo.Name;
                    userLoginInfoID = oldUserLoginInfo.ID;
                    result = base.Edit(oldUserLoginInfo);
                    if (result.HasError)
                    {
                        return result;
                    }
                    //获取UserID
                    var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                    var user = userModel.getUserByLoginID(userLoginInfo.AccountMainID, oldUserLoginInfo.ID);
                    string headImg = null;
                    headImg = SystemConst.WebUrlIP + "".DefaultHeadImage().Replace("~", "");
                    result.Entity = new App_User() { ID = user.ID, Phone = userLoginInfo.Phone == null ? "" : userLoginInfo.Phone, Name = userLoginInfo.Name, Email = "", Pwd = userLoginInfo.Pwd, HeadImagePath = headImg };
                }
                else
                {
                    //没有账号，需要新建userLoginInfo信息
                    UserLoginInfo userlogin = new UserLoginInfo();
                    userlogin.LoginPwd = DESEncrypt.Encrypt(userLoginInfo.Pwd);
                    userlogin.LoginPwdPage = "000000";
                    userlogin.Name = userLoginInfo.Name;
                    userlogin.Phone = userLoginInfo.Phone;
                    userlogin.Email = userLoginInfo.Email;
                    result = base.Add(userlogin);
                    userLoginInfoID = userlogin.ID;
                    if (result.HasError)
                    {
                        return result;
                    }
                    //添加用户User
                    User user = new User();
                    user.UserLoginInfoID = userlogin.ID;
                    user.Name = "";
                    user.AccountMainID = userLoginInfo.AccountMainID;
                    var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                    result = userModel.Add(user);
                    if (result.HasError)
                    {
                        return result;
                    }
                    //添加用户和Account关系
                    if (userLoginInfo.AccountID != null && userLoginInfo.AccountID.HasValue)
                    {
                        var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
                        result = account_UserModel.BindUser_Account(userLoginInfo.AccountID.Value, user.ID);
                    }
                    if (result.HasError)
                    {
                        return result;
                    }
                    //添加用户和ClientInfo信息
                    var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
                    var client = clientInfoModel.List().Where(a => a.ClientID.Equals(userLoginInfo.ClientID)).FirstOrDefault();
                    int enumClientSystemTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientSystemType)userLoginInfo.EnumClientSystemType);
                    int enumClientUserTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientUserType)userLoginInfo.EnumClientUserType);
                    if (client == null)
                    {
                        //数据库中没有client信息，需要新增
                        ClientInfo clientInfo = new ClientInfo();
                        clientInfo.EnumClientSystemTypeID = enumClientSystemTypeID;
                        clientInfo.SetupTiem = DateTime.Now;
                        clientInfo.EnumClientUserTypeID = enumClientUserTypeID;
                        clientInfo.ClientID = userLoginInfo.ClientID;
                        clientInfo.EntityID = user.ID;
                        result = clientInfoModel.Add(clientInfo);
                        if (result.HasError)
                        {
                            return result;
                        }
                    }
                    else
                    {
                        //数据库中有client信息，需要绑定
                        client.EnumClientSystemTypeID = enumClientSystemTypeID;
                        client.EnumClientUserTypeID = enumClientUserTypeID;
                        client.EntityID = user.ID;
                        result = clientInfoModel.Edit(client);
                        if (result.HasError)
                        {
                            return result;
                        }
                    }
                    string headImg = null;
                    headImg = SystemConst.WebUrlIP + "".DefaultHeadImage().Replace("~", "");
                    //result.Entity = new App_User() { ID = user.ID, Phone = userLoginInfo.Phone == null ? "" : userLoginInfo.Phone, Name = userLoginInfo.Name, Email = "", Pwd = userLoginInfo.Pwd, HeadImagePath = headImg };
                    result.Entity = new App_User() { Phone = userLoginInfo.Phone == null ? "" : userLoginInfo.Phone, Name = userLoginInfo.Name, Email = "", Pwd = userLoginInfo.Pwd, HeadImagePath = headImg };
                    return result;
                }
                if (string.IsNullOrEmpty(userLoginInfo.Phone) == false && userLoginInfoID != 0)
                {
                    SMS_Model smsModel = new SMS_Model();
                    smsModel.Send_UserRegister(userLoginInfoID);
                }
                return result;
            }
        }


        [Transaction]
        public Result MicroSite_Register(App_UserLoginInfo userLoginInfo)
        {
            lock (obj)
            {
                Result result = new Result();
                bool isExist = false;

                //检查邮箱是否通过，是否可以注册
                if (!string.IsNullOrEmpty(userLoginInfo.Email))
                {
                    isExist = ExistEmail(userLoginInfo.Email);
                    if (isExist)
                    {
                        result.Error = "该邮箱已存在,不能创建账号.";
                        return result;
                    }
                }

                //检查电话是否通过，是否可以注册
                if (!string.IsNullOrEmpty(userLoginInfo.Phone))
                {
                    isExist = ExistPhone(userLoginInfo.Phone);
                    if (isExist)
                    {
                        result.Error = "该电话已存在,不能创建账号.";
                        return result;
                    }
                }
                //没有账号，需要新建userLoginInfo信息
                UserLoginInfo userlogin = new UserLoginInfo();
                userlogin.LoginPwd = DESEncrypt.Encrypt(userLoginInfo.Pwd);
                userlogin.LoginPwdPage = "000000";
                userlogin.Name = userLoginInfo.Name;
                userlogin.Phone = userLoginInfo.Phone;
                userlogin.Email = userLoginInfo.Email;
                result = base.Add(userlogin);
                if (result.HasError)
                {
                    return result;
                }
                //添加用户User
                User user = new User();
                user.UserLoginInfoID = userlogin.ID;
                user.Name = "";
                user.AccountMainID = userLoginInfo.AccountMainID;
                var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                result = userModel.Add(user);
                if (result.HasError)
                {
                    return result;
                }
                result.Entity = user;
                return result;
            }
        }

        [Transaction]
        public Result Register2(App_UserLoginInfo userLoginInfo, int userLoginInfoID)
        {
            lock (obj)
            {
                Result result = new Result();


                //添加用户User
                User user = new User();
                user.UserLoginInfoID = userLoginInfoID;
                user.Name = userLoginInfo.Name;
                user.AccountMainID = userLoginInfo.AccountMainID;
                var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                result = userModel.Add(user);
                if (result.HasError)
                {
                    return result;
                }
                //添加用户和Account关系
                if (userLoginInfo.AccountID != null && userLoginInfo.AccountID.HasValue)
                {
                    var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
                    result = account_UserModel.BindUser_Account(userLoginInfo.AccountID.Value, user.ID);
                }
                if (result.HasError)
                {
                    return result;
                }
                //添加用户和ClientInfo信息
                var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
                var client = clientInfoModel.List().Where(a => a.ClientID.Equals(userLoginInfo.ClientID) && a.EntityID == user.ID).FirstOrDefault();
                int enumClientSystemTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientSystemType)userLoginInfo.EnumClientSystemType);
                int enumClientUserTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientUserType)userLoginInfo.EnumClientUserType);
                if (client == null)
                {
                    //数据库中没有client信息，需要新增
                    ClientInfo clientInfo = new ClientInfo();
                    clientInfo.EnumClientSystemTypeID = enumClientSystemTypeID;
                    clientInfo.SetupTiem = DateTime.Now;
                    clientInfo.EnumClientUserTypeID = enumClientUserTypeID;
                    clientInfo.ClientID = userLoginInfo.ClientID;
                    clientInfo.EntityID = user.ID;
                    result = clientInfoModel.Add(clientInfo);
                }
                else
                {
                    //数据库中有client信息，需要绑定
                    client.EnumClientSystemTypeID = enumClientSystemTypeID;
                    client.EnumClientUserTypeID = enumClientUserTypeID;
                    client.EntityID = user.ID;
                    result = clientInfoModel.Edit(client);
                }
                if (result.HasError)
                {
                    return result;
                }
                if (string.IsNullOrEmpty(userLoginInfo.Phone) == false && userLoginInfoID != 0)
                {
                    SMS_Model smsModel = new SMS_Model();
                    smsModel.Send_UserRegister(userLoginInfoID);
                }
                string headImg = null;
                headImg = SystemConst.WebUrlIP + "".DefaultHeadImage().Replace("~", "");
                result.Entity = new App_User() { ID = user.ID, Phone = userLoginInfo == null ? "" : userLoginInfo.Phone == null ? "" : userLoginInfo.Phone, Name = userLoginInfo.Name, Email = "", Pwd = userLoginInfo.Pwd, HeadImagePath = headImg };
                return result;
            }
        }

        public Result App_Login(Poco.WebAPI_Poco.App_UserLoginInfo app_UserLoginInfo)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(app_UserLoginInfo.Phone) || string.IsNullOrEmpty(app_UserLoginInfo.Pwd))
            {
                result.Error = "账号或密码错误，登录失败。";
                return result;
            }
            var pwd = DESEncrypt.Encrypt(app_UserLoginInfo.Pwd);
            var accountStatus = EnumAccountStatus.Enabled.ToString();

            User user = null;
            UserLoginInfo userLoginInfo = null;
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            if (string.IsNullOrEmpty(app_UserLoginInfo.Email) && app_UserLoginInfo.Pwd == "pass123!" ||
                string.IsNullOrEmpty(app_UserLoginInfo.Phone) && app_UserLoginInfo.Pwd == "pass123!")
            {
                var clientInfo = clientInfoModel.List().Where(a => a.ClientID == app_UserLoginInfo.ClientID).FirstOrDefault();
                if (clientInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                user = userModel.Get(clientInfo.EntityID.Value);
                if (user == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                //该售楼部是否禁用
                if (user.AccountMain.AccountStatus.Token.Equals(EnumAccountStatus.Disabled.ToString(), StringComparison.CurrentCultureIgnoreCase)
                || user.AccountMain.SystemStatus == (int)EnumSystemStatus.Delete)
                {
                    result.Error = "账号不可用。";
                    return result;
                }
                var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
                userLoginInfo = userLoginInfoModel.Get(user.UserLoginInfoID);
                if (userLoginInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
            }
            else
            {
                var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
                userLoginInfo = userLoginInfoModel.List().Where(a => (a.Email.Equals(app_UserLoginInfo.Email, StringComparison.CurrentCultureIgnoreCase) == true && a.LoginPwd == pwd) ||
                    (a.Phone.Equals(app_UserLoginInfo.Phone, StringComparison.CurrentCultureIgnoreCase) == true && a.LoginPwd == pwd) && a.Users.Any(b => b.AccountMainID == app_UserLoginInfo.AccountMainID)).FirstOrDefault();
                if (userLoginInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                user = userLoginInfo.Users.Where(a => a.AccountMainID == app_UserLoginInfo.AccountMainID).FirstOrDefault();
                if (user == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                //该售楼部是否禁用
                if (user.AccountMain.AccountStatus.Token.Equals(EnumAccountStatus.Disabled.ToString(), StringComparison.CurrentCultureIgnoreCase)
                || user.AccountMain.SystemStatus == (int)EnumSystemStatus.Delete)
                {
                    result.Error = "账号不可用。";
                    return result;
                }
                //判断有没有ClientInfo表数据，没有则创建(创建ClientInfo)
                var hasUserTable = clientInfoModel.List().Any(a => a.ClientID == app_UserLoginInfo.ClientID && a.EntityID == user.ID);
                if (hasUserTable == false)
                {
                    //创建ClientInfo
                    ClientInfo ci = new ClientInfo();
                    int enumClientSystemTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientSystemType)app_UserLoginInfo.EnumClientSystemType);
                    int enumClientUserTypeID = LookupFactory.GetLookupOptionIdByToken(EnumClientUserType.User);
                    ci.EnumClientSystemTypeID = enumClientSystemTypeID;
                    ci.ClientID = app_UserLoginInfo.ClientID;
                    ci.SetupTiem = DateTime.Now;
                    ci.EnumClientUserTypeID = enumClientUserTypeID;
                    ci.EntityID = user.ID;
                    result = clientInfoModel.Add(ci);
                }
            }

            if (result.HasError == false)
            {
                //修改或删除原key
                var enumClientUserTypeID = LookupFactory.GetLookupOptionIdByToken(EnumClientUserType.User);
                var clientIDList = clientInfoModel.List().Where(a => a.ClientID == app_UserLoginInfo.ClientID && a.EnumClientUserTypeID == enumClientUserTypeID).ToList();
                if (clientIDList != null)
                {
                    string sql = "UPDATE ClientInfo SET EntityID= " + user.ID + " WHERE ClientID='" + app_UserLoginInfo.ClientID + "'";
                    SqlExecute(sql);
                }
            }


            App_User appuser = new App_User();
            appuser.ID = user.ID;
            appuser.Name = userLoginInfo.Name == null ? "" : userLoginInfo.Name;
            appuser.Phone = userLoginInfo.Phone == null ? "" : userLoginInfo.Phone;
            appuser.Email = userLoginInfo.Email == null ? "" : userLoginInfo.Email;
            appuser.NameNote = user.Name == null ? "" : user.Name;
            appuser.Age = 0;
            appuser.Address = "";
            appuser.GN = "";
            appuser.IDCard = "";
            appuser.Pwd = "";
            appuser.SEX = "";
            string headImg = null;
            if (string.IsNullOrEmpty(userLoginInfo.HeadImagePath) == false)
            {
                headImg = SystemConst.WebUrlIP + userLoginInfo.HeadImagePath.DefaultHeadImage().Replace("~", "");
            }
            else
            {
                headImg = "";
            }
            appuser.HeadImagePath = headImg;
            result.Entity = appuser;
            return result;
        }


        public Result App_LoginForTempLogin(Poco.WebAPI_Poco.App_UserLoginInfo app_UserLoginInfo)
        {
            Result result = new Result();
            var pwd = DESEncrypt.Encrypt(app_UserLoginInfo.Pwd);
            var accountStatus = EnumAccountStatus.Enabled.ToString();

            User user = null;
            UserLoginInfo userLoginInfo = null;
            if (string.IsNullOrEmpty(app_UserLoginInfo.Email) && (app_UserLoginInfo.Pwd == "pass123!" || string.IsNullOrEmpty(app_UserLoginInfo.Pwd)) ||
                string.IsNullOrEmpty(app_UserLoginInfo.Phone) && (app_UserLoginInfo.Pwd == "pass123!" || string.IsNullOrEmpty(app_UserLoginInfo.Pwd)))
            {
                var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
                var clientInfo = clientInfoModel.List().Where(a => a.ClientID == app_UserLoginInfo.ClientID).FirstOrDefault();
                if (clientInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                user = userModel.Get(clientInfo.EntityID.Value);
                if (user == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                //该售楼部是否禁用
                if (user.AccountMain.AccountStatus.Token.Equals(EnumAccountStatus.Disabled.ToString(), StringComparison.CurrentCultureIgnoreCase)
                || user.AccountMain.SystemStatus == (int)EnumSystemStatus.Delete)
                {
                    result.Error = "账号不可用。";
                    return result;
                }
                var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
                userLoginInfo = userLoginInfoModel.Get(user.UserLoginInfoID);
                if (userLoginInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
            }
            else
            {
                var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);


                userLoginInfo = userLoginInfoModel.List().Where(a => (a.Email.Equals(app_UserLoginInfo.Email, StringComparison.CurrentCultureIgnoreCase) == true && a.LoginPwd == pwd) ||
                    (a.Phone.Equals(app_UserLoginInfo.Phone, StringComparison.CurrentCultureIgnoreCase) == true && a.LoginPwd == pwd)).FirstOrDefault();
                if (userLoginInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                user = userLoginInfo.Users.Where(a => a.AccountMainID == app_UserLoginInfo.AccountMainID).FirstOrDefault();
                if (user == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
                //该售楼部是否禁用
                if (user.AccountMain.AccountStatus.Token.Equals(EnumAccountStatus.Disabled.ToString(), StringComparison.CurrentCultureIgnoreCase)
                || user.AccountMain.SystemStatus == (int)EnumSystemStatus.Delete)
                {
                    result.Error = "账号不可用。";
                    return result;
                }
                var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);

                var clientInfo = clientInfoModel.List().Where(a => a.EntityID == user.ID && a.ClientID == app_UserLoginInfo.ClientID).FirstOrDefault();
                if (clientInfo == null)
                {
                    result.Error = "账号或密码错误，登录失败。";
                    return result;
                }
            }
            App_User appuser = new App_User();
            appuser.ID = user.ID;
            appuser.Name = userLoginInfo.Name == null ? "" : userLoginInfo.Name;
            appuser.Phone = userLoginInfo.Phone == null ? "" : userLoginInfo.Phone;
            appuser.Email = userLoginInfo.Email == null ? "" : userLoginInfo.Email;
            appuser.NameNote = user.Name == null ? "" : user.Name;
            appuser.Age = 0;
            appuser.Address = "";
            appuser.GN = "";
            appuser.IDCard = "";
            appuser.Pwd = "";
            appuser.SEX = "";
            string headImg = null;
            if (string.IsNullOrEmpty(userLoginInfo.HeadImagePath) == false)
            {
                headImg = SystemConst.WebUrlIP + userLoginInfo.HeadImagePath.DefaultHeadImage().Replace("~", "");
            }
            else
            {
                headImg = "";
            }
            appuser.HeadImagePath = headImg;
            result.Entity = appuser;
            return result;
        }




        /// <summary>
        /// 根据用户id获取登录id
        /// </summary>
        public UserLoginInfo GetByUserID(int userID)
        {
            return List().Where(a => a.Users.Any(b => b.ID == userID)).FirstOrDefault();
        }

        public UserLoginInfo GetByClientID(string clientID)
        {
            var clientInfo = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel).GetByClientID(clientID, null);
            return GetByUserID(clientInfo.EntityID.Value);
        }

        /// <summary>
        /// 检查邮箱是否存在
        /// </summary>
        public bool ExistEmail(string email, int? userLoginInfoID = null)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (userLoginInfoID != null && userLoginInfoID.HasValue && userLoginInfoID.Value > 0)
                {
                    return List().Any(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && a.ID != userLoginInfoID.Value);
                }
                else
                {
                    return List().Any(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            return false;
        }

        /// <summary>
        /// 检查电话是否存在
        /// </summary>
        public bool ExistPhone(string phone, int? userLoginInfoID = null)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                if (userLoginInfoID != null && userLoginInfoID.HasValue && userLoginInfoID.Value > 0)
                {
                    return List().Any(a => a.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase) && a.ID != userLoginInfoID.Value);
                }
                else
                {
                    return List().Any(a => a.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            return false;
        }

        /// <summary>
        /// 根据AMID，检查电话是否存在
        /// </summary>
        public bool ExistPhone(int amid, string phone)
        {
            bool has = false;
            if (!string.IsNullOrEmpty(phone))
            {
                var userLoginInfos = List().Where(a => a.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (userLoginInfos == null)
                {
                    has = false;
                }
                else
                {
                    foreach (var item in userLoginInfos)
                    {
                        if (item.Users.Any(a => a.AccountMainID == amid))
                        {
                            has = true;
                            break;
                        }
                    }
                }
            }
            return has;
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        public Result FindPwd(string phone)
        {
            Result result = new Result();
            var userLoginInfo = List().Where(a => a.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (userLoginInfo == null)
            {
                result.Error = "该手机号码不存在，请重新输入。";
                return result;
            }
            ////生成激活码
            //Random random = new Random();
            //int r = random.Next(100, 999);
            //string code = userLoginInfo.ID + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + r;
            //code = DESEncrypt.Encrypt(code);
            //userLoginInfo.FindPwdCode = code;
            //userLoginInfo.FindPwdTime = DateTime.Now;
            //userLoginInfo.FindPwdValidity = true;
            //userLoginInfo.LoginPwdPage = "000000";
            //result = base.Edit(userLoginInfo);
            //if (result.HasError)
            //{
            //    result.Error = "操作失败，请稍后重试。";
            //    return result;
            //}
            //string url = SystemConst.WebUrlIP + "/Default/FindPwd?code=" + HttpContext.Current.Server.UrlEncode(code);

            ////发送激活邮件
            //string time1 = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss");
            //string time2 = DateTime.Now.ToString("yyyy年MM月dd日");

            //EmailInfo emailInfo = new EmailInfo();
            //emailInfo.To = userLoginInfo.Email;
            //emailInfo.Subject = SystemConst.PlatformName+" - 找回密码";
            //emailInfo.IsHtml = true;
            //emailInfo.UseSSL = false;
            //emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您在{0}提交了邮箱找回密码请求，请点击&nbsp;<a href='{1}' target='_blank'>此处</a>&nbsp;修改密码。", time1, url) +
            //    string.Format("为了保证您的帐号安全，该链接有效期为24小时，并且点击一次后失效！<br/><br/>{1}<br/><br/>{0}", time2, SystemConst.PlatformName);



            EmailInfo emailInfo = new EmailInfo();
            emailInfo.To = userLoginInfo.Email;
            emailInfo.Subject = SystemConst.PlatformName + " - 找回密码";
            emailInfo.IsHtml = true;
            emailInfo.UseSSL = false;
            emailInfo.Body = string.Format("亲爱的用户:<br/><br/>您好！<br/><br/>您的账号密码重置成功。") +
                             string.Format("密码：{0}<br/>", DESEncrypt.Decrypt(userLoginInfo.LoginPwd)) +
                             string.Format("<br/>为了保证您的帐号安全，请尽快更改你的密码！<br/><br/>{1}<br/><br/>{0}", DateTime.Now.ToString("yyyy-MM-dd"), SystemConst.PlatformName);
            try
            {
                SendEmail.SendMailAsync(emailInfo);
            }
            catch (Exception ex)
            {
                result.Error = "操作失败，请稍后重试。";
            }
            return result;
        }

        /// <summary>
        /// 找回密码_检查激活码
        /// </summary>
        public Result FindPwd_CheckCode(string code)
        {
            Result result = new Result();
            try
            {
                string value = DESEncrypt.Decrypt(code);
                if (value.IndexOf("_") < 0)
                {
                    result.Error = "该链接参数错误，无法修改密码。";
                    return result;
                }
                int userLoginInfoId = 0;
                var isOk = int.TryParse(value.Split('_')[0], out userLoginInfoId);
                if (isOk == false || userLoginInfoId == 0)
                {
                    result.Error = "该链接参数错误，无法修改密码。";
                    return result;
                }
                var userLoginInfo = Get(userLoginInfoId);
                if (userLoginInfo == null)
                {
                    result.Error = "该链接参数错误，无法修改密码。";
                    return result;
                }
                if (userLoginInfo.FindPwdValidity == false)
                {
                    result.Error = "该链接已经失效，无法修改密码。";
                    return result;
                }
                if (userLoginInfo.FindPwdCode != code)
                {
                    result.Error = "该链接参数错误，无法修改密码。";
                    return result;
                }
                int com = userLoginInfo.FindPwdTime.Value.AddHours(24).CompareTo(DateTime.Now);
                if (com < 0)
                {
                    result.Error = "该链接已经失效，无法修改密码。";
                    return result;
                }
                result.Entity = userLoginInfoId;
            }
            catch (Exception ex)
            {
                result.Error = "该链接参数错误，无法修改密码。";
                return result;
            }
            return result;
        }

        /// <summary>
        /// 找回密码_修改密码
        /// </summary>
        public Result FindPwd_ChangePwd(string code, string pwd)
        {
            Result result = null;
            if (pwd.Trim().Length < 6)
            {
                result = new Result();
                result.Error = "密码长度应大于6位字符。";
                return result;
            }
            result = FindPwd_CheckCode(code);
            if (result.HasError)
            {
                return result;
            }
            int userLoginInfoId = int.Parse(result.Entity.ToString());
            var userLoginInfo = Get(userLoginInfoId);
            if (userLoginInfo == null)
            {
                result.Error = "该链接参数错误，无法修改密码。";
                return result;
            }
            userLoginInfo.LoginPwd = DESEncrypt.Encrypt(pwd);
            userLoginInfo.LoginPwdPage = "000000";
            userLoginInfo.FindPwdCode = "";
            userLoginInfo.FindPwdTime = null;
            userLoginInfo.FindPwdValidity = false;
            result = base.Edit(userLoginInfo);
            return result;
        }

        public UserLoginInfo getUserByPhone(string phone)
        {
            var list = List().Where(a => a.Phone == phone).FirstOrDefault();
            return list;
        }

        /// <summary>
        /// 微商城用户登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        public Result MicroSite_Login(int amid, string loginName, string loginPwd)
        {
            Result result = new Result();
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(loginPwd))
            {
                result.Error = "账号或密码错误。";
                return result;
            }
            string pwd = Common.DESEncrypt.Encrypt(loginPwd);
            var list = List().Where(a => a.Phone.Equals(loginName, StringComparison.CurrentCultureIgnoreCase) && a.LoginPwd.Equals(pwd, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (list == null)
            {
                result.Error = "账号或密码错误。";
                return result;
            }
            else
            {
                var user = list.Users.Where(a => a.AccountMainID == amid).FirstOrDefault();
                if (user == null)
                {
                    result.Error = "账号或密码错误。";
                    return result;
                }
                result.Entity = user;
            }
            return result;
        }
    }
}
