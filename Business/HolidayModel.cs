using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class HolidayModel : BaseModel<Holiday>, IHolidayModel
    {


        public IQueryable<Holiday> GetListByAMID(int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID);
        }
    }
}
