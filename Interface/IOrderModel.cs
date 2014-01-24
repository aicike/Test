using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;

namespace Interface
{
    public interface IOrderModel : IBaseModel<Order>
    {
        IQueryable<Order> GetByAccountMianID(int accountMainID);

        IQueryable<Order> GetByAccountID(int accountID);

        IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete);

        IQueryable<Order> GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status);

        Result AddOrder(Order rorder, OrderUserInfo orderUserInfo, int productID, int count, int OrderMTypeID);

        string GetOrderStatusName(EnumOrderStatus orderStatus);

        string GeDeliveryTypeName(EnumDeliveryType deliveryType);
        
        Result SetOrderStatus(int id, int status);
        
        /// <summary>
        /// 微商城，获取待收货订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        IQueryable<Order> MicroSite_GetByUserID_WaitPayMent(int amid, int userID);
    }
}
