using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ICardPrefixModel : IBaseModel<CardPrefix>
    {
        IQueryable<CardPrefix> getList(int AccountMainID);

        Result CheckName(string PName, int AccountMainID);
    }
}
