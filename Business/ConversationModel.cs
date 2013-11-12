using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Data;
using Injection;
using Injection.Transaction;
using Poco.Enum;

namespace Business
{
    public class ConversationModel : BaseModel<Conversation>, IConversationModel
    {

        /// <summary>
        /// 查找创建会话ID（单人会话）
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        [Transaction]
        public int GetCID(string AccountMainID, string AID, string UID, string Ctype)
        {
            if (!string.IsNullOrEmpty(Ctype))
            {
                var cdModel = Factory.Get<IConversationDetailedModel>(SystemConst.IOC_Model.ConversationDetailedModel);
                int cid = cdModel.GetConversationIDOne(AccountMainID, AID, UID, Ctype);
                if (cid != 0)
                {
                    return cid;
                }
                else
                {
                    Conversation conversation = new Conversation();
                    conversation.AccountMainID = int.Parse(AccountMainID);
                    conversation.CType = 0;
                    conversation.Cname = "";
                    conversation.Cimg = "";
                    base.Add(conversation);

                    switch (Ctype)
                    { 
                        case "0":
                            cdModel.InsertOne(conversation.ID, int.Parse(AccountMainID), int.Parse(AID), (int)EnumClientUserType.Account, 0);
                            cdModel.InsertOne(conversation.ID, int.Parse(AccountMainID), int.Parse(UID), (int)EnumClientUserType.User, 0);
                            break;

                        case "1":
                            cdModel.InsertOne(conversation.ID, int.Parse(AccountMainID), int.Parse(AID), (int)EnumClientUserType.Account, 0);
                            cdModel.InsertOne(conversation.ID, int.Parse(AccountMainID), int.Parse(UID), (int)EnumClientUserType.Account, 0);
                            break;

                        case"2":
                            cdModel.InsertOne(conversation.ID, int.Parse(AccountMainID), int.Parse(AID), (int)EnumClientUserType.User, 0);
                            cdModel.InsertOne(conversation.ID, int.Parse(AccountMainID), int.Parse(UID), (int)EnumClientUserType.User, 0);
                            break;
                    }

                    return conversation.ID;
                }
            }
            else
            {
                return -1;
            }
        }




        //public Result DelCID(string uid, string AID, string AccountMainID)
        //{
        //    IQueryable<Conversation> ICon = null;
        //    ICon = List().Where(a => a.AccountMainID == AccountMainID && a.Ctype == "0" && a.User1ID == AID && a.User2ID == uid);
        //    Result result = new Result();
        //    int SID = 0;
        //    if (ICon.Count() > 0)
        //    {
        //        SID = ICon.FirstOrDefault().ID;
        //        result = base.Delete(SID);
        //    }
        //    return result;
        //}


        //public Result StartCID(string uid, string AID, string AccountMainID)
        //{
        //    Result result = new Result();
        //    IQueryable<Conversation> ICon = null;
        //    ICon = GlobalList().Where(a => a.AccountMainID == AccountMainID && a.Ctype == "0" && a.User1ID == AID && a.User2ID == uid );
        //    int OK = 0;
        //    if (ICon.Count() > 0)
        //    {
        //        string sql = "update Conversation set SystemStatus = 0 where id=" + ICon.FirstOrDefault().ID;
        //        OK = base.SqlExecute(sql);
        //    }
        //    return result;
        //}



        public Result DelCID(string uid, string AID, string AccountMainID)
        {
            throw new NotImplementedException();
        }

        public Result StartCID(string uid, string AID, string AccountMainID)
        {
            throw new NotImplementedException();
        }
    }
}
