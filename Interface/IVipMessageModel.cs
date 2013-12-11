using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IVipMessageModel : IBaseModel<VIPInfo>
    {
        /// <summary>
        /// 获取用户列表
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
    }
}
