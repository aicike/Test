using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ILottery_dishModel : IBaseModel<Lottery_dish>
    {
        IQueryable<Lottery_dish> List(int accountMainID);
    }
}
