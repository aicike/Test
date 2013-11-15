using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IConversationDetailedModel : IBaseModel<ConversationDetailed>
    {
        /// <summary>
        /// 查找会话ID（单人会话）
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        int GetConversationIDOne(string AccountMainID, string AID, string UID, string Ctype);


         /// <summary>
        /// 查找会话ID（单人会话）包含伪删除的
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        int GetConversationIDOneW(string AccountMainID, string AID, string UID, string Ctype);

        /// <summary>
        /// 添加会话ID用户
        /// </summary>
        /// <param name="CID">会话ID</param>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserType">用户类型 枚举</param>
        /// <param name="Ctype">会话类型 0：单人会话 1：多人会话</param>
        /// <returns></returns>
        Result InsertOne(int CID, int AccountMainID, int UserID, int UserType, int Ctype);


        /// <summary>
        /// 获取该用户的所有会话ID
        /// </summary>
        /// <param name="UserType">用户类型 EnumClientUserType</param>
        /// <param name="UID">用户ID</param>
        /// <param name="AccountMainID">客户ID</param>
        /// <returns></returns>
        IQueryable<ConversationDetailed> GetUserAllSID(int UserType, int UID, int AccountMainID);

        /// <summary>
        /// 删除会话ID 伪删除
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        Result delSid(int SID);

        /// <summary>
        /// 启用原会话ID
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        Result StartSID(int SID);
    }
}
