using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class PendingMessagesModel : BaseModel<PendingMessages>, IPendingMessagesModel
    {

        /// <summary>
        /// 获取未读消息数
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public string SendMessageCount(int SID)
        {
            return List().Where(a => a.ToAccountID == SID).Count().ToString();
        }
    }
}
