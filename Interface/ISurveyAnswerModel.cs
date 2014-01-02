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
        /// <returns></returns>
        Result InsertAnswer(List<SurveyAnswer> sa, int? UID, int? Utype);

    }
}
