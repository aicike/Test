using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Data;

namespace Interface
{
    public interface IMessageModel :IBaseModel<Message>
    {

        IQueryable<Message> GetList(int SID);

        IQueryable<Message> GetHistoryList(int SID, int UID);

        int UpAndDelData(int SID, int UID);

        int getUnreadCnt(int AccountID);

        /// <summary>
        /// 获取一星期内每日接收消息数
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        DataTable getDayMessCnt(int AccountID);


    }
}
