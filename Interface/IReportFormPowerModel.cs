using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IReportFormPowerModel : IBaseModel<ReportFormPower>
    {
        IQueryable<ReportFormPower> GetReportByAMID(int AMID);
        void Edit(int AMID,int[] RIDs);
    }
}
