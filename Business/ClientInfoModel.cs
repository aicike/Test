using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Poco.Enum;
using Injection.Transaction;

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
        public void PostClientID(string clientID, int? userID)
        {
            bool isHas = true;
            if (userID!=null&&userID.HasValue&&userID > 0)
            {
                isHas = List().Any(a => a.ClientID == clientID && a.EntityID == userID);

                if (isHas == false)
                {
                    ClientInfo clientInfo = new ClientInfo();
                    clientInfo.ClientID = clientID;
                    clientInfo.EntityID = userID;
                    var result = base.Add(clientInfo);
                }
            }
            else
            {
                isHas = List().Any(a => a.ClientID == clientID);
                if (isHas == false)
                {
                    ClientInfo clientInfo = new ClientInfo();
                    clientInfo.ClientID = clientID;
                    var result = base.Add(clientInfo);
                }
            }
        }
    }
}
