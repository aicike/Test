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

        Result Edit(Lottery_dish entity, List<Lottery_dish_detail> items, HttpFileCollection files);

        /// <summary>
        /// 获取所有启用的大转盘活动
        /// </summary>
        /// <param name="accountMainID"></param>
        /// <returns></returns>
        IQueryable<Lottery_dish> List_ActiveStatus(int accountMainID);
        
        /// <summary>
        /// 设置启用0   禁用1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Result ChangeStatus(int id, int status);
    }
}
