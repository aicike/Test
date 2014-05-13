using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;
using Poco.Enum;

namespace Business
{
    public class CardInfoModel : BaseModel<CardInfo>, ICardInfoModel
    {

        public IQueryable<CardInfo> getList(int AccountMainID, string cardNum, string qz)
        {
            var list = List(true).Where(a=>a.AccountMainID==AccountMainID);

            if (!string.IsNullOrEmpty(qz))
            {
                list = list.Where(a => a.CardPrefix.PrefixName == qz);
            }

            if (!string.IsNullOrEmpty(cardNum))
            {
                list = list.Where(a => a.CardNum.Contains(cardNum.Trim()));
            }

            return list;
        }


        public IQueryable<CardInfo> getListEQ(int AccountMainID, string cardNum, int qz)
        {
            var list = List(true).Where(a => a.AccountMainID == AccountMainID);

           
                list = list.Where(a => a.CardPrefixID == qz);
         

            if (!string.IsNullOrEmpty(cardNum))
            {
                list = list.Where(a => a.CardNum.Contains(cardNum.Trim()));
            }

            return list;
        }

        public bool ckbCardRepeat(string cardNum, int qz,int AccountMainID)
        {
            return List().Any(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefixID == qz);
        }

        public Result DelAll(string CardIDs)
        {
            Result result = new Result();
            string sql = "delete CardInfo where ID in(" + CardIDs + ")";
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
            return result;
        }


        public Result SetStatus(int ID, int Status)
        {
            Result result = new Result();
            string sql = "update CardInfo set Status = "+Status+"  where ID =" + ID ;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
            return result;
        }


        public CardInfo GetCardInfoBy(string cardNum, int qz, int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefixID == qz).FirstOrDefault();
        }


        public CardInfo GetCardInfoBy(string cardNum, string qz, int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefix.PrefixName == qz).First();
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="money"></param>
        /// <param name="userID"></param>
        /// <param name="cardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result Recharge(decimal money, int cardID, int AccountMainID,int AccountID)
        {
            Result result = new Result();
            string sql = "update CardInfo set Balance =( Balance+" + money + ")  where AccountMainID =" + AccountMainID + " and ID=" + cardID;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
            else
            {
                var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
                var vipinfo = vipModel.GetInfoBYCardID(cardID, AccountMainID);

                IVIPInfoExpenseDetailModel model = Factory.Get<IVIPInfoExpenseDetailModel>(SystemConst.IOC_Model.VIPInfoExpenseDetailModel);
                VIPInfoExpenseDetail vipdetail = new VIPInfoExpenseDetail();
                vipdetail.SystemStatus = 0;
                vipdetail.ExpenseDate = DateTime.Now;
                vipdetail.ExpensePrice = money;
                vipdetail.VIPInfoID = vipinfo.ID;
                vipdetail.UserID = vipinfo.UserID ?? 0;
                vipdetail.EnumVIPOperate = (int)EnumVIPOperate.Recharge;
                vipdetail.AccountID = AccountID;
                result = model.Add(vipdetail);

            }
            return result;
        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="Money"></param>
        /// <param name="CardID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="UserID"></param>
        /// <param name="vipinfoID"></param>
        /// <returns></returns>
        [Transaction]
        public Result Consumption(decimal Money, int CardID, int AccountMainID, int AccountID)
        {
            Result result = new Result();
            string sql = "update CardInfo set Balance =( Balance-" + Money + ")  where AccountMainID =" + AccountMainID + " and ID=" + CardID;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
            else
            {
                var vipModel = Factory.Get<IVipInfoModel>(SystemConst.IOC_Model.VipInfoModel);
                var vipinfo = vipModel.GetInfoBYCardID(CardID, AccountMainID);

                IVIPInfoExpenseDetailModel model = Factory.Get<IVIPInfoExpenseDetailModel>(SystemConst.IOC_Model.VIPInfoExpenseDetailModel);
                VIPInfoExpenseDetail vipdetail = new VIPInfoExpenseDetail();
                vipdetail.SystemStatus = 0;
                vipdetail.ExpenseDate = DateTime.Now;
                vipdetail.ExpensePrice = Money;
                vipdetail.VIPInfoID = vipinfo.ID;
                vipdetail.UserID = vipinfo.UserID ?? 0;
                vipdetail.EnumVIPOperate = (int)EnumVIPOperate.Consume;
                vipdetail.AccountID = AccountID;
                result = model.Add(vipdetail);
            }
            return result;
        }
    }
}
