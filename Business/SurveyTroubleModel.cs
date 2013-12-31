using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class SurveyTroubleModel : BaseModel<SurveyTrouble>, ISurveyTroubleModel
    {
        /// <summary>
        /// 根据主表ID查询问题列表
        /// </summary>
        /// <param name="SMainID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<SurveyTrouble> GetListByMainID(int SMainID,int AccountMainID)
        {
            var list = List().Where(a => a.SurveyMainID == SMainID&&a.SurveyMain.AccountMainID==AccountMainID).OrderBy(a=>a.TroubleNumber);
            return list;
        }

        /// <summary>
        /// 删除调查问题 级联删除
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DelSurveyTrouble(int TID, int AccountMainID)
        {
            Result result = new Result();
            var Trouble = List().Where(a => a.ID == TID && a.SurveyMain.AccountMainID == AccountMainID).FirstOrDefault();

            try
            {
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string sql = string.Format("update SurveyTrouble set TroubleNumber = (TroubleNumber-1) where SurveyMainID = {1} and TroubleNumber>{2} delete SurveyAnswer where SurveyTroubleID ={0}  delete SurveyOption where SurveyTroubleID ={0}  delete SurveyTrouble where ID = {0}", Trouble.ID, Trouble.SurveyMainID, Trouble.TroubleNumber);
                commonModel.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            

            return result;
        }

        /// <summary>
        /// 根据ID 获取题目信息
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public SurveyTrouble GetInfoByID(int TID, int AccountMainID)
        {
            var st = List().Where(a => a.ID == TID && a.SurveyMain.AccountMainID == AccountMainID).FirstOrDefault();
            return st;
        }


        /// <summary>
        /// 更改 问题与选项
        /// </summary>
        /// <param name="surveytrouble"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result EditTroubleOrOption(SurveyTrouble surveytrouble, List<SurveyOption> Bos,int AccountMainID)
        {
            Result result = new Result();
            var Trouble =base.Get(surveytrouble.ID);
            string sql = "";
            //向前移动
            if (Trouble.TroubleNumber > surveytrouble.TroubleNumber) 
            {
                sql = string.Format("update SurveyTrouble set TroubleNumber = (TroubleNumber+1) where SurveyMainID = {0} and  TroubleNumber>={1} and TroubleNumber<{2}",
                                    Trouble.SurveyMainID, surveytrouble.TroubleNumber, Trouble.TroubleNumber);
            }
            //向后移动
            else if (Trouble.TroubleNumber < surveytrouble.TroubleNumber)
            {
                sql = string.Format("update SurveyTrouble set TroubleNumber = (TroubleNumber-1) where SurveyMainID = {0} and  TroubleNumber>{1} and TroubleNumber<={2}",
                                       Trouble.SurveyMainID, Trouble.TroubleNumber, surveytrouble.TroubleNumber);
            }
            try
            {
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;   
                string delOption = string.Format("  delete SurveyOption where SurveyTroubleID ={0}", Trouble.ID);
                commonModel.SqlExecute(sql + delOption);

                
                result = base.Edit(surveytrouble);

                if (Bos != null && Bos.Count > 0)
                {
                    StringBuilder stringBuilderSql = new StringBuilder("INSERT INTO dbo.SurveyOption( SystemStatus ,SurveyTroubleID ,[Option] ,Fraction) ");
                    foreach (var item in Bos)
                    {
                        stringBuilderSql.AppendFormat(" SELECT 0,'{0}','{1}','{2}' UNION ALL", Trouble.ID, item.Option, item.Fraction);
                    }
                    var OptionSql = stringBuilderSql.ToString();
                    OptionSql = OptionSql.Remove(OptionSql.Length - " UNION ALL".Length);
                    commonModel.SqlExecute(OptionSql);
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
