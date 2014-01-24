using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderDetailModel : IBaseModel<OrderDetail>
    {
        IQueryable<OrderDetail> GetOrderDetailByOrderID(int OrderID);
        

        /// <summary>
        /// 批量添加产品信息到中间表中
        /// </summary>
        /// <param name="ods"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Result AddOrderDetail(List<OrderDetail> ods, int orderID);
    }
}
