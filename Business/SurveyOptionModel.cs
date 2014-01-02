using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class SurveyOptionModel : BaseModel<SurveyOption>, ISurveyOptionModel
    {

        /// <summary>
        /// 根据题目ID 获取列表信息
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<SurveyOption> GetListBYTroubleID(int TID, int AccountMainID)
        {
            var list = List().Where(a => a.SurveyTroubleID == TID && a.SurveyTrouble.SurveyMain.AccountMainID == AccountMainID).OrderBy(a => a.ID);
            return list;
        }

        /// <summary>
        /// 根据调查问卷ID 查询调查总分与平均分
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        public _B_SurveyScore GetSurveyFraction(int SMID)
        {
            _B_SurveyScore bss = new _B_SurveyScore();




            return bss;
        }

        /// <summary>
        /// 根据题目ID 查询题目总分与平均分
        /// </summary>
        /// <param name="TroubleID"></param>
        /// <returns></returns>
        public _B_SurveyScore GetTroubleFraction(int TroubleID)
        {
            _B_SurveyScore bss = new _B_SurveyScore();




            return bss;
        }
    }
}
