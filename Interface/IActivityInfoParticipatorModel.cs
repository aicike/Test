using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Data;

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
        Result GetUserIsSignUP(int UserID, int UserType, int AID);

        /// <summary>
        /// 根据活动ID获取表面人信息
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<ActivityInfoParticipator> GetAIPList(int ActivityID, int AMID);


        /// <summary>
        /// 根据电话 活动ID 判断是否报过名
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        Result GetUserIsSignUP2(string phone, int AID);

         /// <summary>
        /// 根据Email 活动ID 判断是否报过名
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        Result GetUserIsSignUP3(string Email, int AID);

        /// <summary>
        /// 获取报名 报表数据 12天
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        DataTable GetReportInfo(int ID, string BeginDate, string EndDate);
    }
}
