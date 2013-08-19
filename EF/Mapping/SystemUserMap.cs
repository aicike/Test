using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class SystemUserMap : EntityTypeConfiguration<SystemUser>
    {
        public SystemUserMap()
        {
            this.Ignore(a => a.LoginPwdPage);
            this.Ignore(a => a.LoginPwdPageCompare);
        }
    }
}
