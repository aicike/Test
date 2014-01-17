﻿using System;
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
        public Result GetUserIsSignUP(int UserID, int UserType)
        {
            var acinfo = List().Where(a => a.UserID == UserID && a.EnumAdvertorialUType == UserType);
            Result result = new Result();
            if (acinfo.Count() > 0)
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
            var list = List().Where(a => a.ActivityInfo.AccountMainID == AMID && a.ActivityInfoID == ActivityID);
            return list;
        }
    }
}