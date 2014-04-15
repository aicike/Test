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
    }
}
