using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class Lottery_dish_detailModel : BaseModel<Lottery_dish_detail>, ILottery_dish_detailModel
    {
        public Result DeleteByLottery_dishID(int dishID)
        {
            Result result = new Result();
            try
            {
                string sql = "DELETE dbo.Lottery_dish_detail WHERE Lottery_dishID=" + dishID;
                int i = base.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public List<Lottery_dish_detail> GetListByDishID(int dishID)
        {
            var list= List().Where(a => a.Lottery_dishID == dishID).ToList();
            return list;
        }
    }
}
