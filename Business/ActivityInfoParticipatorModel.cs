using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Data;
using Injection;

namespace Business
{
    public class ActivityInfoParticipatorModel : BaseModel<ActivityInfoParticipator>, IActivityInfoParticipatorModel
    {

        /// <summary>
        /// 根据用户ID 与用户类型 判断是否报过名？
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public Result GetUserIsSignUP(int UserID, int UserType,int AID)
        {
            var acinfo = List().Any(a => a.UserID == UserID && a.EnumAdvertorialUType == UserType&&a.ActivityInfoID==AID);
            Result result = new Result();
            if (acinfo)
            {
                result.HasError = true;
            }
            return result;
        }
        /// <summary>
        /// 根据电话 活动ID 判断是否报过名
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        public Result GetUserIsSignUP2(string phone,int AID)
        {
            var acinfo = List().Any(a => a.ActivityInfoID == AID && a.Phone == phone);
            Result result = new Result();
            if (acinfo)
            {
                result.HasError = true;
            }
            return result;
        }

        /// <summary>
        /// 根据Email 活动ID 判断是否报过名
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        public Result GetUserIsSignUP3(string Email, int AID)
        {
            var acinfo = List().Any(a => a.ActivityInfoID == AID && a.Email == Email);
            Result result = new Result();
            if (acinfo)
            {
                result.HasError = true;
            }
            return result;
        }

        /// <summary>
        /// 根据活动ID获取表面人信息
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<ActivityInfoParticipator> GetAIPList(int ActivityID, int AMID)
        {
            var list = List(true).Where(a => a.ActivityInfo.AccountMainID == AMID && a.ActivityInfoID == ActivityID);
            return list;
        }


        /// <summary>
        /// 获取报名 报表数据 12天
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetReportInfo(int ID,string BeginDate,string EndDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Cnt", typeof(int));
            DateTime beginDT = Convert.ToDateTime(BeginDate);
            DateTime endDT = Convert.ToDateTime(EndDate);
            for (int i = 0; i < 12; i++)
            {
                DataRow row = dt.NewRow();
                row["Date"] = beginDT.AddDays(i).ToString("yyyy-MM-dd");
                row["Cnt"] = 0;
                dt.Rows.Add(row);
            }
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            string sql = string.Format("select Convert(varchar(50),joinDateTime,23) as CreateDate,count(*) as cnt from dbo.ActivityInfoParticipator "
                                      + " where ActivityInfoID= {0} and joinDateTime between '{1}' and '{2}' group by Convert(varchar(50),joinDateTime,23)"
                                      , ID, BeginDate, endDT.AddDays(1).ToString("yyyy-MM-dd"));
            var result = model.SqlQuery<_B_UserCount>(sql);

            foreach (var item in result)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (item.CreateDate == dt.Rows[i]["Date"].ToString())
                    {
                        dt.Rows[i]["Cnt"] = item.cnt;
                    }
                }
            }
            return dt;
        }
    }
}
