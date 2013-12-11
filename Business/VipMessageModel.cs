using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;

namespace Business
{
    public class VipMessageModel : BaseModel<VIPInfo>, IVipMessageModel
    {

        public IQueryable<VIPInfo> getList(int AccountMainID, string cardNum, string phoneNum)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID);

            if (!string.IsNullOrEmpty(cardNum))
            {
                list = list.Where(a => a.User.UserLoginInfo.Phone.Contains(phoneNum.Trim()));
            }

            if (!string.IsNullOrEmpty(phoneNum))
            {
                list = list.Where(a => a.CardNumber.Contains(cardNum.Trim()));
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AccountMainID"></param>
        /// <returns>-1 为注册 0 已注册</returns>
        public int CheckPhoneGetID(string phone, int AccountMainID)
        {
            var userloginModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var userLogin = userloginModel.getUserByPhone(phone.Trim());

            if (userLogin != null)
            {
                var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                var user = userModel.getUserByLoginID(AccountMainID, userLogin.ID);
                if (user != null)
                {
                    var list = List().Where(a => a.UserID == user.ID);
                    if (list.Count() > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return user.ID;
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }



        }


        public VIPInfo getByCardNum(string cardNum, int AccountMainID)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID&& a.CardNumber==cardNum.Trim());
            return list.FirstOrDefault();
        }
    }
}
