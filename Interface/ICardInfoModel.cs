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
        /// 获取卡片列表 卡号模糊查询
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="cardNum"></param>
        /// <param name="qz">前缀</param>
        /// <returns></returns>
        IQueryable<CardInfo> getList(int AccountMainID, string cardNum, string qz);

        /// <summary>
        ///  获取卡片列表
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <param name="cardNum"></param>
        /// <param name="qz"></param>
        /// <returns></returns>
        IQueryable<CardInfo> getListEQ(int AccountMainID, string cardNum, int qz);
        

        /// <summary>
        /// 校验卡号是否重复
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="qz"></param>
        /// <returns></returns>
        bool ckbCardRepeat(string cardNum, int qz, int AccountMainID);

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

        /// <summary>
        /// 根据卡号 前缀ID获取卡片信息
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="qz"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        CardInfo GetCardInfoBy(string cardNum, int qz, int AccountMainID);

        /// <summary>
        /// 根据卡号 前缀名获取卡片信息
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="qz"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        CardInfo GetCardInfoBy(string cardNum, string qz, int AccountMainID);

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="money"></param>
        /// <param name="userID"></param>
        /// <param name="cardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result Recharge(decimal money, int cardID, int AccountMainID);

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="Money"></param>
        /// <param name="CardID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="UserID"></param>
        /// <param name="vipinfoID"></param>
        /// <returns></returns>
        Result Consumption(decimal Money, int CardID, int AccountMainID, int UserID, int vipinfoID,decimal YE);

    }
}
