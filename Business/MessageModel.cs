using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class MessageModel : BaseModel<Message>, IMessageModel
    {
        /// <summary>
        /// 查询当前数据
        /// </summary>
        /// <param name="SID">售楼部ID</param>
        /// <param name="UID">当前聊天人ID</param>
        /// <returns></returns>
        public IQueryable<Message> GetList(int SID, int UID)
        {
            var list = List().Where(a => (a.FromAccountID == SID && a.ToUserID == UID) || (a.ToAccountID == SID && a.FromUserID == UID)).OrderByDescending(a => a.SendTime);
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
            var list = List().Where(a => (a.FromAccountID != SID && a.FromAccountID != null && a.ToUserID == UID) || (a.ToAccountID == SID && a.FromUserID != UID && a.FromUserID != null)).OrderByDescending(a => a.SendTime);
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



    }
}
