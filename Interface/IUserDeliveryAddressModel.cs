using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IUserDeliveryAddressModel : IBaseModel<UserDeliveryAddress>
    {
        List<UserDeliveryAddress> GetListByUserID(int userID, int amid);
    }
}
