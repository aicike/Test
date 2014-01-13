using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace MicroSite_EF.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.Ignore(a => a.Business_AccountID);
            this.Ignore(a => a.Business_GroupID);
        }
    }
}
