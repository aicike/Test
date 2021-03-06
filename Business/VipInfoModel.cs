﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;

namespace Business
{
    public class VipInfoModel : BaseModel<VIPInfo>, IVipInfoModel
    {
        public VIPInfo GetVIPInfoByID(int userID)
        {
            return List().Where(a => a.UserID == userID).FirstOrDefault();
        }

        public IQueryable<VIPInfo> getList(int AccountMainID, string cardNum, string phoneNum)
        {
            var list = List(true).Where(a => a.AccountMainID == AccountMainID);

            if (!string.IsNullOrEmpty(phoneNum))
            {
                list = list.Where(a => a.User.UserLoginInfo.Phone.Contains(phoneNum.Trim()));
            }

            if (!string.IsNullOrEmpty(cardNum))
            {
                list = list.Where(a => a.CardInfo.CardNum.Contains(cardNum.Trim()));
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

        /// <summary>
        /// 校验是否绑定
        /// </summary>
        /// <param name="CardIDs">卡ID</param>
        /// <returns>true:已绑定 false：未绑定</returns>
        public bool ckbIsbind(int[] CardIDs)
        {
            return List().Any(a => CardIDs.Contains(a.CardInfoID));
        }


        /// <summary>
        /// 修改用户的卡信息
        /// </summary>
        /// <param name="VIPID"></param>
        /// <param name="CardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public Result EditCID(int VIPID, int CardID, int AccountMainID)
        {
            Result result = new Result();
            string sql = "update VIPInfo set CardInfoID  = " + CardID + "  where ID =" + VIPID + " and AccountMainID =" + AccountMainID;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
            return result;
        }





        /// <summary>
        /// 根据卡ID 查询vip信息
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public VIPInfo GetInfoBYCardID(int CardID, int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID && a.CardInfoID == CardID).FirstOrDefault();
        }
    }
}
