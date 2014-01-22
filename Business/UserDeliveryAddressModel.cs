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
        public List<UserDeliveryAddress> GetListByUserID(int userID, int amid)
        {
            return List().Where(a => a.UserID == userID && a.AccountMainID == amid).ToList();
        }

        public Result Delete(int amid, int userID, int udaID)
        {
            Result result = new Result();
            var obj = List().Where(a => a.UserID == userID && a.AccountMainID == amid && a.ID == udaID).FirstOrDefault();
            if (obj == null)
            {
                result.Error = "参数错误，无法操作。";
            }
            result = Delete(udaID);
            return result;
        }

        public UserDeliveryAddress Get(int amid, int userID, int udaID)
        {
            return List().Where(a => a.UserID == userID && a.AccountMainID == amid && a.ID == udaID).FirstOrDefault();
        }
    }
}
