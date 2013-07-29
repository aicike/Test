using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IKeywordModel : IBaseModel<Keyword>
    {
        Result AddList(IList<Keyword> keywords);
    }
}
