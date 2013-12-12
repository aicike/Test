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

        /// 根据卡号 查询会员
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        VIPInfo getByCardNum(string cardNum,int AccountMainID);


        /// <summary>
        /// 校验是否绑定
        /// </summary>
        /// <param name="CardIDs">卡ID</param>
        /// <returns>true:已绑定 false：未绑定</returns>
        bool ckbIsbind(int[] CardIDs);
    }
}
