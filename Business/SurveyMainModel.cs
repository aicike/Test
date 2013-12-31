using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;
using Injection.Transaction;

namespace Business
{
    public class SurveyMainModel : BaseModel<SurveyMain>, ISurveyMainModel
    {

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<SurveyMain> GetList(int AccountMainID)
        {
            var list = List().Where(a => a.AccountMainID == AccountMainID);

            return list;
        }

        /// <summary>
        /// 列表首页
        /// </summary>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public IQueryable<_B_SurveyMain> SelList(int AccountMainID)
        {
            string sql = string.Format(@" select a.* ,(select count(*) from SurveyAnswer where SurveyTroubleID = (select top 1 ID from SurveyTrouble where SurveyMainid = a.ID ))  as  Counts from 
                                         (select ID,SurveyTitle,CreateDate,Status,EnumSurveyMainType from dbo.SurveyMain where accountmainid={0}) a order by ID desc", AccountMainID);


            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var result = model.SqlQuery<_B_SurveyMain>(sql);

            return result;
        }

        /// <summary>
        /// 删除调查问卷 级联删除
        /// </summary>
        /// <param name="MainID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DelSurveyMain(int MainID, int AccountMainID)
        {
            Result result = new Result();
            var TroubleModel = Factory.Get<ISurveyTroubleModel>(SystemConst.IOC_Model.SurveyTroubleModel);
            var Trouble = TroubleModel.GetListByMainID(MainID, AccountMainID).Select(a => a.ID).ToList();
            string ids = "0";
            foreach (var item in Trouble)
            {
                ids += item + ",";
            }
            ids = ids.TrimEnd(',');
            try
            {
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string DelMainSql = string.Format("delete SurveyAnswer where SurveyTroubleID in ({0})  delete SurveyOption where SurveyTroubleID in ({0})  delete SurveyTrouble where ID in ({0})  delete SurveyMain where ID ={1} and AccountMainID = {2}"
                                                    , ids, MainID, AccountMainID);
                commonModel.SqlExecute(DelMainSql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }


        /// <summary>
        /// 根据ID获取调查问卷信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public SurveyMain GetSurveyMainByID(int ID, int AccountMainID)
        {
            var survey = List().Where(a => a.ID == ID && a.AccountMainID == AccountMainID).FirstOrDefault();
            return survey;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="MainID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Result SetMainStatus(int MainID, int AccountMainID, int status)
        {
            int Stype = 0;
            //status 0 开启 1停止
            if (status == 0)
            {
                Stype = 1;
            }
            string sql = string.Format("update SurveyMain set status = {0} where ID={1} and AccountMainID={2}", Stype,MainID, AccountMainID);
            Result result = new Result();
            if (SqlExecute(sql)<=0)
            {
                result.HasError = true;
                result.Error = "设置失败，请稍后再试！";
            }
            return result;
            
        }
    }
}
