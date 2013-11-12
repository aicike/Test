using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Poco.Enum;
using Injection;

namespace Business
{
    public class ConversationDetailedModel : BaseModel<ConversationDetailed>, IConversationDetailedModel
    {


        /// <summary>
        /// 查找会话ID（单人会话）
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        public int GetConversationIDOne(string AccountMainID, string AID, string UID, string Ctype)
        {
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            string sql = "select ConversationID from ConversationDetailed where Ctype=0 and AccountMainID = {0} and ((UserID={1} and UserType={2}) or (UserID ={3} and UserType={4})) group by ConversationID having count(ConversationID)>1";
       
            switch (Ctype)
            {
                //售楼部与用户间对话
                case "0":
                    sql = string.Format(sql, AccountMainID, AID, (int)EnumClientUserType.Account, UID, (int)EnumClientUserType.User);
                    break;
                //售楼部与售楼部间对话
                case "1":
                    sql = string.Format(sql, AccountMainID, AID, (int)EnumClientUserType.Account, UID, (int)EnumClientUserType.Account);
                    break;
                //用户与用户间对话
                case "2":
                    sql = string.Format(sql, AccountMainID, AID, (int)EnumClientUserType.User, UID, (int)EnumClientUserType.User);
                    break;
            }

            int  CID = commonModel.SqlQuery<int>(sql).FirstOrDefault();
            return CID;

        }

        /// <summary>
        /// 添加会话ID用户
        /// </summary>
        /// <param name="CID">会话ID</param>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserType">用户类型 枚举</param>
        /// <param name="Ctype">会话类型 0：单人会话 1：多人会话</param>
        /// <returns></returns>
        public Result InsertOne(int CID,int AccountMainID, int UserID, int UserType, int Ctype)
        {
            Result result = new Result();
            ConversationDetailed cd = new ConversationDetailed();
            cd.AccountMainID = AccountMainID;
            cd.ConversationID = CID;
            cd.CType = Ctype;
            cd.UserID = UserID;
            cd.UserType = UserType;
            cd.SetDate = DateTime.Now;
            return base.Add(cd);

        }

        /// <summary>
        /// 获取该用户的所有会话ID
        /// </summary>
        /// <param name="UserType">用户类型 EnumClientUserType</param>
        /// <param name="UID">用户ID</param>
        /// <returns></returns>
        public IQueryable<ConversationDetailed> GetUserAllSID(int UserType, int UID,int AccountMainID)
        {
            var list = List().Where(a=>a.AccountMainID == AccountMainID&& a.UserType==UserType && a.UserID==UID);
            return list;
        }
    }
}
