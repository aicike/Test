using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IRepairInfoModel : IBaseModel<RepairInfo>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<RepairInfo> GetInfo(int AMID);
    }
}
