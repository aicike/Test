using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IActivityInfoSignInModel : IBaseModel<ActivityInfoSignIn>
    {
        /// <summary>
        /// 根据活动ID获取签到人信息
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<ActivityInfoSignIn> GetAIPList(int ActivityID, int AMID);
    }
}
