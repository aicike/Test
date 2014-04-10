using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IProperty_UserModel : IBaseModel<Property_User>
    {
        IQueryable<Property_User> GetListByAccountMainID(int accountMainID);

        Result Delete(int property_UserID, int propertyHouseID);

        /// <summary>
        /// 根据房号获取物业登记的业主信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        List<Property_User> GetUserPhoneByRoomNum(int amid, string roomNum);

        /// <summary>
        /// 根据业主电话获取相关信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        List<Property_User> GetHouseByUserPhone(int amid, string userPhone);
    }
}
