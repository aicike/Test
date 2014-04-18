using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    /// <summary>
    /// 收费维修
    /// </summary>
    public class RepairchargesoModel : BaseModel<Repairchargeso>, IRepairchargesoModel
    {
        /// <summary>
        /// 获取列表页
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<Repairchargeso> GetList(int AMID)
        { 
            var list = List(true).Where(a=>a.AccountMainID==AMID);
            return list;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Repairchargeso Getinfo(int ID,int AMID)
        {
            var item = List().Where(a => a.ID == ID && a.AccountMainID == AMID).FirstOrDefault();
            return item;
        }
    }
}
