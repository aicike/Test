using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class UserDeliveryAddressModel : BaseModel<UserDeliveryAddress>, IUserDeliveryAddressModel
    {
        public List<UserDeliveryAddress> GetListByUserID(int userID,int amid)
        {
            return List().Where(a => a.UserID == userID&&a.AccountMainID==amid).ToList();
        }
    }
}
