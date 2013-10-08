using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Data;

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
            if (!string.IsNullOrEmpty(Ctype))
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
            else {
                return -1;
            }
        }

        public IQueryable<Conversation> GetAllCID(string AoU, int UID)
        {
            string sql = "";
            if (AoU == "s")
            {
                sql = string.Format("select * from dbo.[Conversation] where (ctype='0' and User1ID='{0}') or (ctype='1' and User1ID='{0}') or(ctype ='1' and User2ID = '{0}')", UID);
            }
            else
            {
                sql = string.Format("select * from dbo.[Conversation] where (ctype='0' and User2ID='{0}') or (ctype='2' and User1ID='{0}') or(ctype ='2' and User2ID = '{0}')", UID);
            }
            return base.SqlQuery(sql);
        }



        public Result DelCID(string uid, string AID, string AccountMainID)
        {
            IQueryable<Conversation> ICon = null;
            ICon = List().Where(a => a.AccountMainID == AccountMainID && a.Ctype == "0" && a.User1ID == AID && a.User2ID == uid);
            Result result = new Result();
            int SID = 0;
            if (ICon.Count() > 0)
            {
                SID = ICon.FirstOrDefault().ID;
                result = base.Delete(SID);
            }
            return result;
        }


        public Result StartCID(string uid, string AID, string AccountMainID)
        {
            Result result = new Result();
            IQueryable<Conversation> ICon = null;
            ICon = GlobalList().Where(a => a.AccountMainID == AccountMainID && a.Ctype == "0" && a.User1ID == AID && a.User2ID == uid );
            int OK = 0;
            if (ICon.Count() > 0)
            {
                string sql = "update Conversation set SystemStatus = 0 where id=" + ICon.FirstOrDefault().ID;
                OK = base.SqlExecute(sql);
            }
            return result;
        }
    }
}
