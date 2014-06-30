using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class ComplaintModel : BaseModel<Complaint>, IComplaintModel
    {
        /// <summary>
        /// 获取投诉列表
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<Complaint> GetList(int AMID)
        {
            var list = List(true).Where(a=>a.AccountMainID==AMID);
            return list;
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="EnumComplaintStatus">枚举</param>
        /// <returns></returns>
        public Result UpdStatus(int CID, int EnumComplaintStatus)
        {
            Result result = new Result();
            string sql = string.Format("update Complaint set EnumComplaintStatus ={0} where ID={1}",EnumComplaintStatus,CID);
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "更改状态失败！";
            }
            return result;
        }

        /// <summary>
        /// 获取用户投诉列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public IQueryable<Complaint> GetListByUserID(int UserID, int AMID)
        {
            var list = List().Where(a => a.UserID == UserID && a.AccountMainID == AMID).OrderByDescending(a => a.ComplaintDate).Take(20);
            return list;
        }

        /// <summary>
        /// 根据投诉ID 获取信息
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Complaint GetComplaintInfo(int CID,int AMID)
        {
            var comp = List().Where(a=>a.AccountMainID==AMID&&a.ID==CID).FirstOrDefault();
            return comp;
        }
    }
}
