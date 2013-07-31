using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ITextReplyModel : IBaseModel<TextReply>
    {
        Result AddList(IList<TextReply> textReplys);
        Result DeleteByAutoMessage_KeywordID(int autoMessage_KeywordID);
    }
}
