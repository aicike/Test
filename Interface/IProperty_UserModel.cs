using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IProperty_UserModel : IBaseModel<Property_User>
    {
        IQueryable<Property_User> GetListByAccountMainID(int accountMainID);
    }
}
