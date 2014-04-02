using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAccountMainHouseInfo : IBaseModel<AccountMainHouseInfo>
    {
        /// <summary>
        /// 售楼部根据AccountMainHouseID获取楼号信息
        /// </summary>
        /// <param name="accountMainID"></param>
        /// <returns></returns>
        IQueryable<AccountMainHouseInfo> GetListByAccountMainHouseID(int AccountMainHouseID);

        /// <summary>
        /// 物业根据AccountMainID获取楼号信息
        /// </summary>
        /// <param name="accountMainID"></param>
        /// <returns></returns>
        IQueryable<AccountMainHouseInfo> GetListByAccountMainID(int accountMainID);

        Result DelteAll(int HousesInfoID);
    }
}
