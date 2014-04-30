using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IExpressCollectionModel : IBaseModel<ExpressCollection>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<ExpressCollection> GetListByAMID(int AMID);

        /// <summary>
        /// 获取列表(条件查询)
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="OddNumber"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        IQueryable<ExpressCollection> GetListByAMID(int AMID, string OddNumber, string phone);

        /// <summary>
        /// 根据ID 获取详细信息
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        ExpressCollection GetInfoBuID(int AMID, int ID);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AMID"></param>
        /// <param name="EnumExpressStatus"></param>
        /// <returns></returns>
        Result UpdStatus(int ID, int AMID, int EnumExpressStatus);

        /// <summary>
        /// 根据单号或电话查询快递
        /// </summary>
        /// <param name="AMID"></param>
        /// <param name="OddNumber"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        IQueryable<ExpressCollection> GetExpress(int AMID, string OddNumber, string Phone);
        
    }
}
