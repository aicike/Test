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
                list = list.Where(a => a.CardPrefix.Contains(cardNum.Trim()));
            }

            return list;
        }
    }
}
