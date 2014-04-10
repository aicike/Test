using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IRepairOperationModel : IBaseModel<RepairOperation>
    {
         /// <summary>
        /// 根据PID获取数据
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        IQueryable<RepairOperation> GetInfoByPID(int PID);
    }
}
