using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISurveyOptionModel : IBaseModel<SurveyOption>
    {
        /// <summary>
        /// 根据题目ID 获取列表信息
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<SurveyOption> GetListBYTroubleID(int TID, int AccountMainID);

        /// <summary>
        /// 根据调查问卷ID 查询调查总分与平均分
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        _B_SurveyScore GetSurveyFraction(int SMID);

        /// <summary>
        /// 根据题目ID 查询题目总分与平均分
        /// </summary>
        /// <param name="TroubleID"></param>
        /// <returns></returns>
        _B_SurveyScore GetTroubleFraction(int TroubleID);


        /// <summary>
        /// 根据调查ID 获取总分
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        int GetSurveySum(int SMID);
    }
}
