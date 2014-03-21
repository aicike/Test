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
            return List(true).Where(a => a.AccountMainID == AccountMainID);
        }


        public IQueryable<Holiday> GetListByDateAndAMID(int AccountMainID, DateTime BeginDate, DateTime EndDate)
        {
            return  List(true).Where(a => a.AccountMainID == AccountMainID && a.HoliDayValue >= BeginDate && a.HoliDayValue <= EndDate);
        }
    }
}
