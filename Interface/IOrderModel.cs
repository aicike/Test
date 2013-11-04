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

        Result AddOrder(Order rorder, OrderUserInfo orderUserInfo, int productID, int count);

        string GetOrderStatusName(EnumOrderStatus orderStatus);

        string GeDeliveryTypeName(EnumDeliveryType deliveryType);
    }
}
