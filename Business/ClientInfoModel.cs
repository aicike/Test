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
        //[Transaction]
        //public Result Login(string IMEI, int accountMainID, Poco.Enum.EnumClientSystemType osType, Poco.Enum.EnumClientUserType userType)
        //{
        //    Result resule = new Result();
        //    //1.先查找系统中是否有IMEI信息
        //    int userTypeID= LookupFactory.GetLookupOptionIdByToken(userType);
        //    var clientInfo= List().Where(a => a.IMEI.Equals(IMEI, StringComparison.CurrentCultureIgnoreCase)&&a.ClientInfo_Users.Any(b=>b.AccountMainID==accountMainID&&b.EnumClientUserTypeID==userTypeID)).FirstOrDefault();
        //    if (clientInfo!=null)
        //    {
        //        //2.如果有数据，说明该终端之前已经创建过账号，无需新创建，只是登录.
        //        Result result = new Result();
        //        resule.Entity = clientInfo;
        //    }
        //    else
        //    {
        //        //3.没有数据，需要新建一个账号
        //        var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
        //        User user = new User();
        //        resule = userModel.Add(user);
        //        if (resule.HasError)
        //        {
        //            return resule;
        //        }
        //        //4.添加客户端信息及关系
        //        ClientInfo newClientInfo = new ClientInfo();
        //        newClientInfo.IMEI = IMEI;
        //        newClientInfo.EnumClientSystemTypeID = LookupFactory.GetLookupOptionIdByToken(osType);
        //        newClientInfo.EnumAccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
        //        newClientInfo.SetupTiem = DateTime.Now;
        //        resule = base.Add(newClientInfo);
        //        if (resule.HasError)
        //        {
        //            return resule;
        //        }
        //        var clientInfoUserModel = Factory.Get<IClientInfo_UserModel>(SystemConst.IOC_Model.ClientInfo_UserModel);
        //        ClientInfoUser clientUser = new ClientInfoUser();
        //        clientUser.EnumClientUserTypeID =userTypeID;
        //        clientUser.EntityID = user.ID;
        //        clientUser.ClientInfoID = newClientInfo.ID;
        //        resule = clientInfoUserModel.Add(clientUser);
        //    }
        //    return resule;
        //}

        /// <summary>
        /// User App
        /// </summary>
        public Result PostClientID(string clientID, int accountMainID, int? userID)
        {
            Result result = new Result();
            bool isHas = true;
            if (userID != null && userID.HasValue && userID > 0)
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
                if (cl == null)
                {
                    //添加UserLoginInfo,User,ClientInfo
                    var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
                    App_UserLoginInfo userloginInfo = new App_UserLoginInfo();
                    userloginInfo.Email = "";
                    userloginInfo.Pwd = "pass123!";
                    userloginInfo.Name = "匿名";
                    userloginInfo.AccountMainID = accountMainID;
                    userloginInfo.ClientID = clientID;
                    userloginInfo.EnumClientSystemType = (int)EnumClientSystemType.Android;
                    userloginInfo.EnumClientUserType = (int)EnumClientUserType.User;
                    result = ulim.Register(userloginInfo);
                }
                else
                {
                    result.Entity = cl.EntityID;
                }
            }
            return result;
        }
    }
}
