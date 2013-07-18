using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class UserLoginInfoMap : EntityTypeConfiguration<UserLoginInfo>
    {
        public UserLoginInfoMap()
        {
            this.Ignore(a => a.LoginPwdPage);
        }
    }
}
