using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class Property_UserModel : BaseModel<Property_User>, IProperty_UserModel
    {
        public IQueryable<Property_User> GetListByAccountMainID(int accountMainID)
        {
            return base.List(true).Where(a => a.AccountMainID == accountMainID);
        }

        [Transaction]
        public new Result Edit(Property_User pu)
        {
            var result = base.Edit(pu);
            if (result.HasError)
            {
                return result;
            }
            if (pu.Property_House != null)
            {
                var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
                result = property_HouseModel.Edit(pu.Property_House);
            }
            return result;
        }

        [Transaction]
        public Result Delete(int property_UserID, int propertyHouseID)
        {
            Result result = new Result();
            try
            {
                if (property_UserID > 0)
                {
                    string sql = "DELETE dbo.Property_User WHERE ID=" + property_UserID;
                    int i = base.SqlExecute(sql);
                    if (i > 0 && propertyHouseID > 0)
                    {
                        sql = "DELETE dbo.Property_House WHERE ID=" + propertyHouseID;
                        base.SqlExecute(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据房号获取物业登记的业主信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<Property_User> GetUserPhoneByRoomNum(int amid, string roomNum)
        {
            return GetListByAccountMainID(amid).Where(a => a.Property_House.RoomNumber == roomNum).ToList();
        }

        /// <summary>
        /// 根据业主电话获取相关信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<Property_User> GetHouseByUserPhone(int amid, string userPhone)
        {
            return GetListByAccountMainID(amid).Where(a => a.Phone == userPhone).ToList();
        }
    }
}
