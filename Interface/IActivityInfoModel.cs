using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IActivityInfoModel : IBaseModel<ActivityInfo>
    {

        /// <summary>
        /// 根据 AMID 获取列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        IQueryable<ActivityInfo> GetListByAMID(int AMID);


         /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Result SetMainStatus(int AID, int AccountMainID, int status);

        /// <summary>
        /// 根据ID获取活动信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        ActivityInfo GetActivityByID(int AID, int AccountMainID);

        /// <summary>
        /// 删除活动 级联删除
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result DeleteActivityByID(int AID, int AccountMainID);
    }
}
