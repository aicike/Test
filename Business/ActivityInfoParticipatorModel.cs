using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class ActivityInfoParticipatorModel : BaseModel<ActivityInfoParticipator>, IActivityInfoParticipatorModel
    {

        /// <summary>
        /// 根据用户ID 与用户类型 判断是否报过名？
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public Result GetUserIsSignUP(int UserID, int UserType,int AID)
        {
            var acinfo = List().Any(a => a.UserID == UserID && a.EnumAdvertorialUType == UserType&&a.ActivityInfoID==AID);
            Result result = new Result();
            if (acinfo)
            {
                result.HasError = true;
            }
            return result;
        }
        /// <summary>
        /// 根据电话 活动ID 判断是否报过名
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        public Result GetUserIsSignUP2(string phone,int AID)
        {
            var acinfo = List().Any(a => a.ActivityInfoID == AID && a.Phone == phone);
            Result result = new Result();
            if (acinfo)
            {
                result.HasError = true;
            }
            return result;
        }

        /// <summary>
        /// 根据Email 活动ID 判断是否报过名
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        public Result GetUserIsSignUP3(string Email, int AID)
        {
            var acinfo = List().Any(a => a.ActivityInfoID == AID && a.Email == Email);
            Result result = new Result();
            if (acinfo)
            {
                result.HasError = true;
            }
            return result;
        }

        /// <summary>
        /// 根据活动ID获取表面人信息
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<ActivityInfoParticipator> GetAIPList(int ActivityID, int AMID)
        {
            var list = List(true).Where(a => a.ActivityInfo.AccountMainID == AMID && a.ActivityInfoID == ActivityID);
            return list;
        }
    }
}
