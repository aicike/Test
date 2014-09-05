using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IProperty_HouseModel : IBaseModel<Property_House>
    {
        //public Property_House GetByHouseShortNum(string shortNum);

        List<string> GetBuildingNum(int amid);

        List<string> GetCellNum(int amid, string buildingNum);

        List<string> GetRoomNumber(int amid, string buildingNum, string cellNum);

        Property_House GetByShortNo(string shortNum);
    }
}
