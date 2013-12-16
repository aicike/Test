using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class CardInfoModel : BaseModel<CardInfo>, ICardInfoModel
    {

        public IQueryable<CardInfo> getList(int AccountMainID, string cardNum, string qz)
        {
            var list = List().Where(a=>a.AccountMainID==AccountMainID);

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
            var list = List().Where(a => a.AccountMainID == AccountMainID);

           
                list = list.Where(a => a.CardPrefixID == qz);
         

            if (!string.IsNullOrEmpty(cardNum))
            {
                list = list.Where(a => a.CardNum.Contains(cardNum.Trim()));
            }

            return list;
        }

        public bool ckbCardRepeat(string cardNum, int qz,int AccountMainID)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefixID == qz);
            if (list.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            var list = List().Where(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefixID == qz);
            return list.FirstOrDefault();
        }


        public CardInfo GetCardInfoBy(string cardNum, string qz, int AccountMainID)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefix.PrefixName == qz);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="money"></param>
        /// <param name="userID"></param>
        /// <param name="cardID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public Result Recharge(decimal money, int cardID, int AccountMainID)
        {
            Result result = new Result();
            string sql = "update CardInfo set Balance =( Balance+" + money + ")  where AccountMainID =" + AccountMainID + " and ID=" + cardID;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
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
        public Result Consumption(decimal Money, int CardID, int AccountMainID, int UserID, int vipinfoID, decimal YE)
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
                IVIPInfoExpenseDetailModel model = Factory.Get<IVIPInfoExpenseDetailModel>(SystemConst.IOC_Model.VIPInfoExpenseDetailModel);
                VIPInfoExpenseDetail vipdetail = new VIPInfoExpenseDetail();
                vipdetail.SystemStatus = 0;
                vipdetail.ExpenseDate = DateTime.Now;
                vipdetail.ExpensePrice = Money;
                vipdetail.VIPInfoID = vipinfoID;
                vipdetail.UserID = UserID;
                vipdetail.Balance = YE;
                model.Add(vipdetail);
            }
            return result;
        }
    }
}
