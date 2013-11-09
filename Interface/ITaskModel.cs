using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ITaskModel : IBaseModel<Task>
    {
        IQueryable<Task> GetByCreateAccountID(int createAccountID);

        IQueryable<Task> GetMyTasks(int accountID);
    }
}
