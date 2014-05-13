using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;

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
            var list = List().Where(a => a.SurveyTroubleID == TID && a.SurveyTrouble.SurveyMain.AccountMainID == AccountMainID);
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
            //总分
            try
            {
                var FullMarks = List().Where(a => a.SurveyTrouble.SurveyMainID == SMID).GroupBy(a => a.SurveyTroubleID).Select(a => a.Max(b => b.Fraction)).Sum();
                bss.FullMarks = FullMarks;
            }
            catch
            {
                bss.FullMarks = 0;
            }

            //平均分
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            var Average = AnswerModel.GetAverageBySMID(SMID);
            bss.Average = Average;
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
            //总分
            try
            {
                var FullMarks = List().Where(a => a.SurveyTroubleID == TroubleID).GroupBy(a => a.SurveyTroubleID).Select(a => a.Max(b => b.Fraction)).Sum();
                bss.FullMarks = FullMarks;
            }
            catch
            {
                bss.FullMarks = 0;
            }
            //平均分
            var AnswerModel = Factory.Get<ISurveyAnswerModel>(SystemConst.IOC_Model.SurveyAnswerModel);
            var Average = AnswerModel.GetAverageByTID(TroubleID);
            bss.Average = Average;


            return bss;
        }

        /// <summary>
        /// 根据调查ID 获取总分
        /// </summary>
        /// <param name="SMID"></param>
        /// <returns></returns>
        public int GetSurveySum(int SMID)
        {
            try
            {
                var FullMarks = List().Where(a => a.SurveyTrouble.SurveyMainID == SMID).GroupBy(a => a.SurveyTroubleID).Select(a => a.Max(b => b.Fraction)).Sum();
                return FullMarks;
            }
            catch {
                return 0;
            }
           
        }
    }
}
