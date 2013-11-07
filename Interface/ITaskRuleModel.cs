using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ITaskRuleModel : IBaseModel<TaskRule>
    {
        IQueryable<TaskRule> GetByAccountMainID(int accountMainID);
    }
}
