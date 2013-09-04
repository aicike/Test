using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class ConversationModel : BaseModel<Conversation>, IConversationModel
    {

        /// <summary>
        /// 查找会话ID
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        public int GetCID(string AccountMainID, string AID, string UID, string Ctype)
        {
            IQueryable<Conversation> ICon = null;
            if (Ctype == "0")
            {
                ICon = List().Where(a => a.AccountMainID == AccountMainID && a.Ctype == Ctype && a.User1ID == AID && a.User2ID == UID);
            }
            else
            {
                ICon = List().Where(a => a.AccountMainID == AccountMainID && a.Ctype == Ctype && (a.User1ID == AID || a.User1ID == UID) && (a.User2ID == UID || a.User2ID == AID));
            }

            if (ICon.Count() > 0)
            {
                return ICon.First().ID;
            }
            else
            {
                Conversation conver = new Conversation();
                conver.AccountMainID = AccountMainID;
                conver.User1ID = AID;
                conver.User2ID = UID;
                conver.Ctype = Ctype;
                base.Add(conver);
                return conver.ID;
            }
        }

        
    }
}
