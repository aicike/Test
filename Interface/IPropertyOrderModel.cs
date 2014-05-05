using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPropertyOrderModel : IBaseModel<PropertyOrder>
    {
         /// <summary>
        /// 提交物业订单
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="AMID"></param>
        /// <param name="UserID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        Result UpPropertyOrder(int[] IDS, int AMID, int UserID);

         /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="EnumOrderStatus"></param>
        /// <returns></returns>
        Result UPdateStatus(string orderNum, int EnumOrderStatus);

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="Date">费用月份</param>
        /// <param name="UserName">业主姓名</param>
        /// <param name="RoomNumber"></param>
        /// <param name="Unit"></param>
        /// <param name="EnumPropertyOrderType">订单类型</param>
        /// <returns></returns>
        IQueryable<PropertyOrder> GetList(int AMID, string Date, string UserName, string RoomNumber, string Unit, int EnumPropertyOrderType);

         /// <summary>
        /// 提交停车费订单
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Result UpParkingFeeOrder(int[] IDS, int AMID, int UserID);
    }
}
