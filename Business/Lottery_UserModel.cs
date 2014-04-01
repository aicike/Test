using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class Lottery_UserModel : BaseModel<Lottery_User>, ILottery_UserModel
    {
        [Transaction]
        public new Result Add(Lottery_User lottery_User)
        {
            //更改奖品剩余数量

            return base.Add(lottery_User);
        }
    }
}
