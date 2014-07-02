using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IComplaintModel : IBaseModel<Complaint>
    {
        /// <summary>
        /// 获取投诉列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<Complaint> GetList(int AMID);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="EnumComplaintStatus">枚举</param>
        /// <returns></returns>
        Result UpdStatus(int CID, int EnumComplaintStatus);

         /// <summary>
        /// 获取用户投诉列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<Complaint> GetListByUserID(int UserID, int AMID);

        /// <summary>
        /// 根据投诉ID 获取信息
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Complaint GetComplaintInfo(int CID, int AMID);

        /// <summary>
        /// 更改评分
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="EnumRepairScore"></param>
        /// <returns></returns>
        Result UpdScore(int RID, int EnumRepairScore);

         /// <summary>
        /// 添加操作备注
        /// </summary>
        /// <param name="RID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        Result AddRemark(int RID, string Content);
    }
}
