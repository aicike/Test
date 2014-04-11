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
            var list = List().Where(a => a.AccountMainID == AMID).OrderByDescending(a => a.RepairDate);
            return list;
        }

        /// <summary>
        /// 根据ID 获取信息
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public RepairInfo GetInfoByID(int RID, int AMID)
        {
            var repair = List().Where(a => a.AccountMainID == AMID && a.ID == RID);
            return repair.FirstOrDefault();
        }

        /// <summary>
        /// 修改报修状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="EunmRepairStatus"></param>
        /// <returns></returns>
        public Result UpdStatus(int RID, int EnumRepairStatus)
        {
            Result result = new Result();
            string sql = string.Format("update RepairInfo set EnumRepairStatus ={0} where ID={1}", EnumRepairStatus,RID);
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "修改状态出错！";
            }
            return result;
        }

        /// <summary>
        /// 添加操作备注
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public Result AddRemark(int RID, string Content)
        {
            Result result = new Result();
            string sql = string.Format("insert into RepairOperation(SystemStatus,RepairInfoID,OperationDate,OperationContent,Remarks) values(0,{0},getDate(),'无','{1}')",RID,Content);
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "添加操作备注失败！";
            }
            return result;
        }

        /// <summary>
        /// 更改负责人
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public Result UpdAccount(int RID, int AccountID)
        {
            Result result = new Result();
            string sql = string.Format("update RepairInfo set AccountID={0} where ID = {1}",AccountID,RID);
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "更改负责人失败！";
            }
            return result;
        }
    }
}
