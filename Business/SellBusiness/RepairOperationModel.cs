using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class RepairOperationModel : BaseModel<RepairOperation>, IRepairOperationModel
    {

        /// <summary>
        /// 根据PID获取数据
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public IQueryable<RepairOperation> GetInfoByPID(int PID)
        {
            var list = List().Where(a=>a.RepairInfoID==PID).OrderByDescending(a=>a.OperationDate);
            return list;
        }
    }
}
