using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class PropertyFeeInfoModel : BaseModel<PropertyFeeInfo>, IPropertyFeeInfoModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="Date">缴费日期</param>
        /// <param name="Unit">单元</param>
        /// <param name="RoomNumber">房号</param>
        /// <param name="OwnerName">业主姓名</param>
        /// <param name="OwnerPhone">业主电话</param>
        /// <returns></returns>
        public IQueryable<PropertyFeeInfo> GetPropertyFeeInfo(int AMID, string Date, string Unit, string RoomNumber, string OwnerName, string OwnerPhone)
        {
            var list = List().Where(a=>a.AccountMainID==AMID);
            if (!string.IsNullOrEmpty(Date))
            {
                list = list.Where(a => a.PayDate.Contains(Date));
            }
            if (!string.IsNullOrEmpty(Unit))
            {
                list = list.Where(a => a.Unit.Contains(Unit.Trim()));
            }
            if (!string.IsNullOrEmpty(RoomNumber))
            {
                list = list.Where(a => a.RoomNumber.Contains(RoomNumber.Trim()));
            }
            if (!string.IsNullOrEmpty(OwnerName))
            {
                list = list.Where(a => a.OwnerName.Contains(OwnerName.Trim()));
            }
            if (!string.IsNullOrEmpty(OwnerPhone))
            {
                list = list.Where(a => a.OwnerPhone.Contains(OwnerPhone.Trim()));
            }
            return list;
        }
    }
}
