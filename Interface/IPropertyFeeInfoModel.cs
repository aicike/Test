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
        /// 根据电话 获取年份物业费
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="RoomNumber"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        List<PropertyFeeInfo> GetPropertyFeeInfo(int AMID, string RoomNumber, string Phone, int Year);

        /// <summary>
        /// 根据ID 获取详细信息
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        PropertyFeeInfo GetInfoByID(int RID, int AMID);

        /// <summary>
        /// 根据IDS 获取列表
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        List<PropertyFeeInfo> GetPropertyFeeByIDS(int[] IDS, int AMID);

        /// <summary>
        /// 根据IDS 获取总金额
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        double GetPriceByIDS(int[] IDS, int AMID);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result delAll(string IDS, int AMID);

        /// <summary>
        /// 数据导入验证
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="LH"></param>
        /// <param name="DY"></param>
        /// <param name="FH"></param>
        /// <returns></returns>
        Result DBImportCheck(int AMID, string PayDate, string Name, string Phone, string LH, string DY, string FH);

        /// <summary>
        /// 获取一个月的数据
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="PayDate"></param>
        /// <returns></returns>
        List<PropertyFeeInfo> GetAllByPayDay(int AMID, string PayDate);
    }
}
