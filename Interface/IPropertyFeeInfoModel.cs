using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPropertyFeeInfoModel : IBaseModel<PropertyFeeInfo>
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
        IQueryable<PropertyFeeInfo> GetPropertyFeeInfo(int AMID, string Date, string Unit, string RoomNumber, string OwnerName, string OwnerPhone);


        /// <summary>
        /// 根据房号 获取年份物业费
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="RoomNumber"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        List<PropertyFeeInfo> GetPropertyFeeInfo(int AMID, string RoomNumber, int Year);

        /// <summary>
        /// 根据ID 获取详细信息
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        PropertyFeeInfo GetInfoByID(int RID, int AMID);
    }
}
