using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPropertyOrderDetailModel : IBaseModel<PropertyOrderDetail>
    {
        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result AddOrderDatail(int[] IDS, int AMID, int PID);

        /// <summary>
        /// 根据物业费id查询是否已经提交过交过订单
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result GetProperIsUP(int[] IDS, int AMID);


        /// <summary>
        /// 添加停车费费明细
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result AddOrderDatail_ParkingFee(int[] IDS, int AMID, int PID);

        /// <summary>
        /// 根据停车费id查询是否已经提交过交过订单
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result GetProperIsUP_ParkingFeeID(int[] IDS, int AMID);
    }
}
