using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISurveyAnswerModel : IBaseModel<SurveyAnswer>
    {
        /// <summary>
        /// 根据主表ID 查询调研回答
        /// </summary>
        /// <param name="SMID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<SurveyAnswer> GetListBySMID(int SMID, int AccountMainID);


        /// <summary>
        /// 录入答案
        /// </summary>
        /// <param name="sa">列表</param>
        /// <param name="UID">？用户</param>
        /// <param name="Utype">？0用户端，1销售端</param>
        /// <param name="SAUID">？记名投票用户信息ID</param>
        /// <returns></returns>
        Result InsertAnswer(List<SurveyAnswer> sa, int? UID, int? Utype,int ?SAUID);

        /// <summary>
        /// 查询调查问卷回答平均分
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        int GetAverageBySMID(int SMID);

        /// <summary>
        /// 查询调查题目回答平均分
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        int GetAverageByTID(int TID);

        /// <summary>
        /// 根据选项ID 获取回答次数
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        int GetAverageCntByOID(int OID);

        /// <summary>
        /// 获取选项回答百分比
        /// </summary>
        /// <param name="OID"></param>
        /// <param name="TID"></param>
        /// <returns></returns>
        double GetAveragePercentage(int TID,int OID);


        /// <summary>
        /// 根据调查ID 获取回答人列表
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        IQueryable<SurveyAnswer> GetAnswerUserList(int SMID, int AccountMainID);

        /// <summary>
        /// 根据UserCode获取回答信息
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        IQueryable<SurveyAnswer> GetAnswerInfoByUserCode(string UserCode);

        /// <summary>
        /// 根据Ucode 获取提交人评分
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        int GetAnswerUserScore(string UserCode);
    }
}
