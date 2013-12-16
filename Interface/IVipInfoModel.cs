using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IVipInfoModel : IBaseModel<VIPInfo>
    {
        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="cardNum"></param>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        IQueryable<VIPInfo> getList(int AccountMainID,string cardNum, string phoneNum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AccountMainID"></param>
        /// <returns>-1 为注册 0 已注册</returns>
        int CheckPhoneGetID(string phone,int AccountMainID);

        VIPInfo GetVIPInfoByID(int userID);



        /// <summary>
        /// 校验是否绑定
        /// </summary>
        /// <param name="CardIDs">卡ID</param>
        /// <returns>true:已绑定 false：未绑定</returns>
        bool ckbIsbind(int[] CardIDs);

        /// <summary>
        /// 修改用户的卡信息
        /// </summary>
        /// <param name="VIPID"></param>
        /// <param name="CardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result EditCID(int VIPID, int CardID, int AccountMainID);

        /// <summary>
        /// 根据卡ID 查询vip信息
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        VIPInfo GetInfoBYCardID(int CardID, int AccountMainID);
    }
}
