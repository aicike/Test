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
    public class Property_HouseModel : BaseModel<Property_House>, IProperty_HouseModel
    {
        [Transaction]
        public new Result Delete(int id)
        {
            var result = base.Delete(id);
            if (!result.HasError)
            {
                var propertyUserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
                result = propertyUserModel.DeleteByPropertyHouseID(id);
            }
            return result;
        }

        public List<string> GetBuildingNum(int amid)
        {
            return List().Where(a => a.AccountMainID == amid).Select(a => a.BuildingNum).Distinct().ToList();
        }

        public List<string> GetCellNum(int amid,string buildingNum)
        {
            return List().Where(a => a.AccountMainID == amid && a.BuildingNum == buildingNum).Select(a => a.CellNum).Distinct().ToList();
        }

        public List<string> GetRoomNumber(int amid, string buildingNum, string cellNum)
        {
            return List().Where(a => a.AccountMainID == amid && a.BuildingNum == buildingNum && a.CellNum == cellNum).Select(a => a.RoomNumber).Distinct().ToList();
        }


        public Property_House GetByShortNo(string shortNum)
        {
            var tempValue = shortNum.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            Property_House pu = null;
            if (tempValue.Length == 3)
            {
                string BuildingNum = tempValue[0];
                string CellNum = tempValue[1];
                string RoomNumber = tempValue[2];
                pu = List().Where(a => a.BuildingNum == BuildingNum && a.CellNum == CellNum && a.RoomNumber == RoomNumber).FirstOrDefault();
            }
            return pu;
        }
    }
}
