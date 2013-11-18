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
                    result.Entity = new App_User() { ID = user.ID, Name = user.UserLoginInfo.Name, Email = user.UserLoginInfo.Email, Pwd = Common.DESEncrypt.Decrypt(user.UserLoginInfo.LoginPwd), HeadImagePath = headImg };
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
                var cl = List().Where(a => a.ClientID == clientID).FirstOrDefault();
                var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
                if (cl == null)
                {
                    //添加UserLoginInfo,User,ClientInfo
                    App_UserLoginInfo userloginInfo = new App_UserLoginInfo();
                    userloginInfo.Email = "";
                    userloginInfo.Pwd = "pass123!";
                    userloginInfo.Name = DateTime.Now.ToString("MMddHH:mm:ss");
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
                    var hasUser = userModel.List().Any(a => a.ID == cl.EntityID.Value && a.AccountMainID == accountMainID);
                    if (hasUser)
                    {
                        //已有账号
                        var userLoginInfo = ulim.GetByClientID(clientID);
                        string headImg = null;
                        if (string.IsNullOrEmpty(userLoginInfo.HeadImagePath) == false)
                        {
                            headImg = SystemConst.WebUrlIP + userLoginInfo.HeadImagePath.Replace("~", "");
                        }
                        else
                        {
                            headImg = "";
                        }
                        result.Entity = new App_User() { ID = cl.EntityID.Value, Name = userLoginInfo.Name, Email = userLoginInfo.Email, Pwd = Common.DESEncrypt.Decrypt(userLoginInfo.LoginPwd), HeadImagePath = headImg };
                    }
                    else
                    {
                        //新注册
                        //只添加User,ClientInfo 不添加,UserLoginInfo
                        App_UserLoginInfo userloginInfo = new App_UserLoginInfo();
                        userloginInfo.Email = "";
                        userloginInfo.Pwd = "pass123!";
                        userloginInfo.Name = DateTime.Now.ToString("MMddHH:mm:ss");
                        userloginInfo.AccountMainID = accountMainID;
                        userloginInfo.ClientID = clientID;
                        userloginInfo.EnumClientSystemType = (int)EnumClientSystemType.Android;
                        userloginInfo.EnumClientUserType = (int)EnumClientUserType.User;
                        result = ulim.Register2(userloginInfo, cl.ID);
                    }
                }
            }
            return result;
        }


        public ClientInfo GetByClientID(string clientID)
        {
            return List().Where(a => a.ClientID.Equals(clientID)).FirstOrDefault();
        }
    }
}
