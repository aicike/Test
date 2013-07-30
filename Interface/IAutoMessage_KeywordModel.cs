using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAutoMessage_KeywordModel : IBaseModel<AutoMessage_Keyword>
    {
        Result Add(AutoMessage_Keyword entity, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs, int accountMainID);
        IQueryable<AutoMessage_Keyword> List(int accoutMainID);
        int GetRuleNo(int accountMainID, int? parentAutoMessage_KeywordID);
        string GetFullRuleNo(int accountMainID, string fullRuleNo);
        AutoMessage_Keyword GetByID_AccountMainID(int id, int accountMainID);
    }
}
