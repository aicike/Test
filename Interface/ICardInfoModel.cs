using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ICardInfoModel : IBaseModel<CardInfo>
    {
        /// <summary>
        /// 获取卡片列表
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="cardNum"></param>
        /// <param name="qz">前缀</param>
        /// <returns></returns>
        IQueryable<CardInfo> getList(int AccountMainID, string cardNum, string qz);
    }
}
