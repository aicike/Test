using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IHolidayModel : IBaseModel<Holiday>
    {
        IQueryable<Holiday> GetListByAMID(int AccountMainID);
    }
}
