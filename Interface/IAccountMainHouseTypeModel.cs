using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface IAccountMainHouseTypeModel : IBaseModel<AccountMainHouseType>
    {
        IQueryable<AccountMainHouseType> GetList(int AccountMainHouseID);

        Result AddInfo(AccountMainHouseType HouseType, int accountMainID, HttpPostedFileBase HouseImagePath);

        Result AddInfo(AccountMainHouseType HouseType, int accountMainID);

        Result EditInfo(AccountMainHouseType HouseType, int accountMainID, HttpPostedFileBase HouseImagePath);

        Result EditInfo(AccountMainHouseType HouseType, int accountMainID);

        Result DeleteInfo(int id);

        Result DelteAll(int HousesTypeID);
    }
}
