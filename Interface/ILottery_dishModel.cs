using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface ILottery_dishModel : IBaseModel<Lottery_dish>
    {
        IQueryable<Lottery_dish> List(int accountMainID);

        Result Add(Lottery_dish entity, List<Lottery_dish_detail> items, HttpFileCollection files);
    }
}
