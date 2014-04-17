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

        /// <summary>
        /// 根据ID 获取信息
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        RepairInfo GetInfoByID(int RID, int AMID);

        /// <summary>
        /// 修改报修状态
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="EunmRepairStatus"></param>
        /// <returns></returns>
        Result UpdStatus(int RID, int EnumRepairStatus);

        /// <summary>
        /// 添加操作备注
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        Result AddRemark(int RID, string Content);

        /// <summary>
        /// 更改负责人
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        Result UpdAccount(int RID, int AccountID);

        /// <summary>
        /// 根据用户ID获取报修信息 20条
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<RepairInfo> GetListByUserID(int UserID, int AMID);

        /// <summary>
        /// 更改评分
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="EnumRepairScore"></param>
        /// <returns></returns>
        Result UpdScore(int RID, int EnumRepairScore);
    }
}
