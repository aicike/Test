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
        Result Delete(int amid, int userID, int udaID);
        UserDeliveryAddress Get(int amid, int userID, int udaID);
    }
}
