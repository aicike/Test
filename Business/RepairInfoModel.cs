using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class RepairInfoModel : BaseModel<RepairInfo>, IRepairInfoModel
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<RepairInfo> GetInfo(int AMID)
        {
            var list = List().Where(a=>a.AccountMainID==AMID).OrderBy(a=>a.EnumRepairStatus);
            return list;
        }
    }
}
