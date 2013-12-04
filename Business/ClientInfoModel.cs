using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Poco.Enum;
using Injection.Transaction;
using Poco.WebAPI_Poco;

namespace Business
{
    public class ClientInfoModel : BaseModel<ClientInfo>, IClientInfoModel
    {
        /// <summary>
        /// User App
        /// </summary>
        [Transaction]
        public Result PostClientID(string clientID, int accountMainID, int? userID)
        {
            Result result = new Result();
            bool isHas = true;

            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            bool isHasUser = false;
            if (userID.HasValue)
            {
                //判断userID是否存在
                var user = userModel.Get(userID.Value);
                if (user != null)
                {
                    isHasUser = true;
                    string headImg = null;
                    if (string.IsNullOrEmpty(user.UserLoginInfo.HeadImagePath) == false)
                    {
                        headImg = SystemConst.WebUrlIP + user.UserLoginInfo.HeadImagePath.Replace("~", "");
                    }
                    else
                    {
                        headImg = "";
                    }
                    result.Entity = new App_User() { ID = user.ID, Phone = user.UserLoginInfo == null ? "" : user.UserLoginInfo.Phone == null ? "" : user.UserLoginInfo.Phone, Name = user.UserLoginInfo.Name, Email = user.UserLoginInfo.Email, Pwd = Common.DESEncrypt.Decrypt(user.UserLoginInfo.LoginPwd), HeadImagePath = headImg };
                }
            }

            if (userID != null && userID.HasValue && userID > 0 && isHasUser)
            {
                //和user表绑定
                isHas = List().Any(a => a.ClientID == clientID && a.EntityID == userID);

                if (isHas == false)
                {
                    ClientInfo clientInfo = new ClientInfo();
                    clientInfo.ClientID = clientID;
                    clientInfo.EntityID = userID;
                    result = base.Add(clientInfo);
                }
            }
            else
            {
                //保存clientID信息，临时注册
                CommonModel com = new CommonModel();
                
                var userids = List().Where(a => a.ClientID == clientID).Select(a => a.EntityID).ToList();
                var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
                if (userids == null || userids.Count == 0)
                {
                    //添加UserLoginInfo,User,ClientInfo
                    App_UserLoginInfo userloginInfo = new App_UserLoginInfo();
                    userloginInfo.Email = "";
                    userloginInfo.Pwd = "pass123!";
                    //userloginInfo.Name = DateTime.Now.ToString("MMddHHmmss");

                    userloginInfo.Name = "游客" + com.CreateRandom("0123456789", 4);
                    userloginInfo.AccountMainID = accountMainID;
                    userloginInfo.ClientID = clientID;
                    userloginInfo.EnumClientSystemType = (int)EnumClientSystemType.Android;
                    userloginInfo.EnumClientUserType = (int)EnumClientUserType.User;
                    result = ulim.Register(userloginInfo);
                  
                }
                else
                {
                    //判断ClientInfo的accountMainID是否与传递过来的amid相同
                    //相同则有账号信息，不同则为其他售楼部新增用户，需要新注册
                    var user = userModel.List().Where(a => userids.Contains(a.ID) && a.AccountMainID == accountMainID).FirstOrDefault();
                    UserLoginInfo userLoginInfo = null;
                    if (user != null)
                    {
                        userLoginInfo = ulim.GetByUserID(user.ID);
                        //已有账号
                        string headImg = null;
                        if (string.IsNullOrEmpty(userLoginInfo.HeadImagePath) == false)
                        {
                            headImg = SystemConst.WebUrlIP + userLoginInfo.HeadImagePath.Replace("~", "");
                        }
                        else
                        {
                            headImg = "";
                        }
                        result.Entity = new App_User() { ID = user.ID, Phone = userLoginInfo == null ? "" : userLoginInfo.Phone == null ? "" : userLoginInfo.Phone, Name = userLoginInfo.Name, Email = userLoginInfo.Email, Pwd = Common.DESEncrypt.Decrypt(userLoginInfo.LoginPwd), HeadImagePath = headImg };
                    }
                    else
                    {
                        userLoginInfo = ulim.GetByClientID(clientID);
                        //新注册
                        //只添加User,ClientInfo 不添加,UserLoginInfo
                        App_UserLoginInfo userloginInfo = new App_UserLoginInfo();
                        userloginInfo.Email = userLoginInfo.Email;
                        userloginInfo.Pwd = userLoginInfo.LoginPwd;
                        userloginInfo.Name = userLoginInfo.Name;
                        userloginInfo.AccountMainID = accountMainID;
                        userloginInfo.ClientID = clientID;
                        userloginInfo.Phone = userLoginInfo.Phone;
                        userloginInfo.EnumClientSystemType = (int)EnumClientSystemType.Android;
                        userloginInfo.EnumClientUserType = (int)EnumClientUserType.User;
                        result = ulim.Register2(userloginInfo, userLoginInfo.ID);
                    }
                }
            }
            return result;
        }


        public ClientInfo GetByClientID(string clientID, int? userID)
        {
            if (userID.HasValue == false)
            {
                return List().Where(a => a.ClientID.Equals(clientID)).FirstOrDefault();
            }
            else
            {
                return List().Where(a => a.ClientID.Equals(clientID) && a.EntityID.Value == userID.Value).FirstOrDefault();
            }
        }
    }
}
