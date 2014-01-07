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
    public class ActivityInfoModel : BaseModel<ActivityInfo>, IActivityInfoModel
    {

        /// <summary>
        /// 根据 AMID 获取列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<ActivityInfo> GetListByAMID(int AMID)
        {
            var list = List().Where(a => a.AccountMainID == AMID);
            return list;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Result SetMainStatus(int AID, int AccountMainID, int status)
        {
            int Stype = 0;
            //status 0 开启 1停止
            if (status == 0)
            {
                Stype = 1;
            }
            string sql = string.Format("update ActivityInfo set status = {0} where ID={1} and AccountMainID={2}", Stype, AID, AccountMainID);
            Result result = new Result();
            if (SqlExecute(sql) <= 0)
            {
                result.HasError = true;
                result.Error = "设置失败，请稍后再试！";
            }
            return result;

        }

        /// <summary>
        /// 根据ID获取活动信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        public ActivityInfo GetActivityByID(int AID, int AccountMainID)
        {
            var activity = List().Where(a=>a.AccountMainID==AccountMainID&& a.ID==AID).FirstOrDefault();
            return activity;
        }

        /// <summary>
        /// 删除活动 级联删除
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AccountMainID"></param>
        /// <returns></returns>
        [Transaction]
        public Result DeleteActivityByID(int AID, int AccountMainID)
        {
            var account = List().Where(a => a.ID == AID && a.AccountMainID == AccountMainID).FirstOrDefault();
            Result result = new Result();
            try
            {
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string sql = string.Format("delete ActivityInfoParticipator where ActivityInfoID={0} delete ActivityInfo where ID = {0}", account.ID);
                commonModel.SqlExecute(sql);
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
