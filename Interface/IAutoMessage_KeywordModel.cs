using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAutoMessage_KeywordModel : IBaseModel<AutoMessage_Keyword>
    {
        Result Add(string ruleName, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs,int accountMainID);
        IQueryable<AutoMessage_Keyword> List(int accoutMainID);
    }
}
