using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;
using System.Data;

namespace Business
{
    public class MessageModel : BaseModel<Message>, IMessageModel
    {
        /// <summary>
        /// 查询当前聊天数据
        /// </summary>
        /// <param name="SID">售楼部ID</param>
        /// <param name="UID">当前聊天人ID</param>
        /// <returns></returns>
        public IQueryable<Message> GetList(int SID)
        {
            var list = List().Where(a => (a.ConversationID == SID));
            return list;
        }


        /// <summary>
        /// 查询历史消息
        /// </summary>
        /// <param name="SID">售楼部用户id</param>
        /// <param name="UID">当前用户id</param>
        /// <returns></returns>
        public IQueryable<Message> GetHistoryList(int SID, int UID)
        {
            var list = List().Where(a => (a.FromAccountID != SID && a.FromAccountID != null && a.ToUserID == UID) || (a.ToAccountID != SID && a.FromUserID == UID && a.ToAccountID != null)).OrderByDescending(a => a.SendTime);
            return list;
        }

        /// <summary>
        /// 修改聊天状态 并删除未读记录
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public int UpAndDelData(int SID, int UID)
        {
            string sql = "update dbo.Message set isReceive = 1 where  fromUserID = " + UID + " and ToAccountID =" + SID
                        + " delete PendingMessages where  FromUserID =  " + UID + " and ToAccountID = " + SID;
            return base.SqlExecute(sql);
        }

        /// <summary>
        /// 获取售楼代表未读消息数
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public int getUnreadCnt(int AccountID)
        {
            var list = List().Where(a => a.ToAccountID == AccountID && a.IsReceive==false);
            if (list != null)
            {
                return list.Count();
            }
            return 0;
        }

        /// <summary>
        /// 获取一星期内每日接收消息数
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public DataTable getDayMessCnt(int AccountID)
        {
            //结束日期
            string EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            //起始日期
            string BeGinDate = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            string sql = string.Format(@"select sendTime,count(sendTime) as cnt from (select Convert(varchar(50),sendTime,23) as sendTime from dbo.[Message] 
                                        where toAccountID= {0} and sendTime > '{1}' and sendTime <'{2}' )a group by a.sendTime", AccountID, BeGinDate, EndDate);
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var result = model.SqlQuery<_B_MessageDayNumber>(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("sendTime");
            dt.Columns.Add("cnt");
            for (int i = 6; i >= 0; i--)
            {
                DataRow row = dt.NewRow();
                row["sendTime"] = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                row["cnt"] = "0";
                dt.Rows.Add(row);
            }
            foreach (var item in result)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (item.sendTime == dt.Rows[i]["sendTime"].ToString())
                    {
                        dt.Rows[i]["cnt"] = item.cnt;
                    }
                }
            }

            return dt;
        }

    }
}
