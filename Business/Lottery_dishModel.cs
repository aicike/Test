using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class Lottery_dishModel : BaseModel<Lottery_dish>, ILottery_dishModel
    {
        public IQueryable<Lottery_dish> List(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }

        //public Result Add(Lottery_dish lottery_dish)
        //{

        //    return null;
        
        //}
    }
}
