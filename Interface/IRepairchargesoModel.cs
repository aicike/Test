using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    /// <summary>
    /// 收费维修
    /// </summary>
    public interface IRepairchargesoModel : IBaseModel<Repairchargeso>
    {
        /// <summary>
        /// 获取列表页
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<Repairchargeso> GetList(int AMID);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Repairchargeso Getinfo(int ID, int AMID);
    }
}
