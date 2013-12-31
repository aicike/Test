using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISurveyTroubleModel : IBaseModel<SurveyTrouble>
    {
        /// <summary>
        /// 根据主表ID查询问题列表
        /// </summary>
        /// <param name="SMainID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        IQueryable<SurveyTrouble> GetListByMainID(int SMainID, int AccountMainID);

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result DelSurveyTrouble(int TID, int AccountMainID);

        /// <summary>
        /// 根据ID 获取题目信息
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        SurveyTrouble GetInfoByID(int TID, int AccountMainID);

        /// <summary>
        /// 更改 问题与选项
        /// </summary>
        /// <param name="surveytrouble"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        Result EditTroubleOrOption(SurveyTrouble surveytrouble,List<SurveyOption> Bos, int AccountMainID);
    }
}
