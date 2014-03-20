using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class ActivityInfoSignInModel : BaseModel<ActivityInfoSignIn>, IActivityInfoSignInModel
    {
        /// <summary>
        /// 根据活动ID获取签到人信息
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<ActivityInfoSignIn> GetAIPList(int ActivityID, int AMID)
        {
            return List(true).Where(a => a.ActivityInfo.AccountMainID == AMID && a.ActivityInfoID == ActivityID);
        }
    }
}
