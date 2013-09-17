using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAutoMessage_KeywordModel : IBaseModel<AutoMessage_Keyword>
    {
        Result Add(AutoMessage_Keyword entity, string keys,string messageFileIDs, string messageImageTextIDs, int accountMainID,List<Files> files);
        Result Edit(int keyID, string ruleName, int projectID, string keys,string messageFileIDs, string messageImageTextIDs, int accountMainID, bool isFistAutoMessage,List<Files> files);
        IQueryable<AutoMessage_Keyword> List(int accoutMainID);
        int GetRuleNo(int accountMainID, int? parentAutoMessage_KeywordID);
        string GetFullRuleNo(int accountMainID, string fullRuleNo);
        AutoMessage_Keyword GetByID_AccountMainID(int id, int accountMainID);
        Result Delete(int id, int accountMainID);
        List<AutoMessage_Keyword> GetFirstAutoMessage(int accountMainID);
        List<AutoMessage_Keyword> GetAutoMessageByKey(int accountMainID,string key);

        /// <summary>
        /// 根据项目ID查询是否被设关键字
        /// </summary>
        /// <param name="HousesID"></param>
        /// <returns></returns>
        bool GetKeyByHouseID(int HousesID);
    }
}
