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

        /// <summary>
        /// 校验卡号是否重复
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="qz"></param>
        /// <returns></returns>
        bool ckbCardRepeat(string cardNum, string qz, int AccountMainID);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="CardIDs"></param>
        /// <returns></returns>
        Result DelAll(string CardIDs);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status">0冻结，1正常</param>
        /// <returns></returns>
        Result SetStatus(int ID, int Status);

    }
}
