using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IActivityInfoParticipatorModel : IBaseModel<ActivityInfoParticipator>
    {
        /// <summary>
        /// 根据用户ID 与用户类型 判断是否报过名？
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        Result GetUserIsSignUP(int UserID, int UserType);

        /// <summary>
        /// 根据活动ID获取表面人信息
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<ActivityInfoParticipator> GetAIPList(int ActivityID, int AMID);
    }
}
