using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class CardInfoModel : BaseModel<CardInfo>, ICardInfoModel
    {

        public IQueryable<CardInfo> getList(int AccountMainID, string cardNum, string qz)
        {
            var list = List().Where(a=>a.AccountMainID==AccountMainID);

            if (!string.IsNullOrEmpty(qz))
            {
                list = list.Where(a => a.CardPrefix == qz);
            }

            if (!string.IsNullOrEmpty(cardNum))
            {
                list = list.Where(a => a.CardNum.Contains(cardNum.Trim()));
            }

            return list;
        }


        public bool ckbCardRepeat(string cardNum, string qz,int AccountMainID)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID && a.CardNum == cardNum && a.CardPrefix == qz);
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
    }
}
