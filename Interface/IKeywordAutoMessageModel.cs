using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IKeywordAutoMessageModel : IBaseModel<KeywordAutoMessage>
    {
        Result AddList(List<KeywordAutoMessage> list);
        Result DeleteByAutoMessage_KeywordID(int autoMessage_KeywordID);
        
    }
}
