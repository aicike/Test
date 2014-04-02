using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ILottery_dish_detailModel : IBaseModel<Lottery_dish_detail>
    {
        Result DeleteByLottery_dishID(int dishID);
        List<Lottery_dish_detail> GetListByDishID(int dishID);
    }
}
