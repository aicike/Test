using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.Ignore(a => a.LoginPwdPage);
            this.Ignore(a => a.LoginPwdPageCompare);
            this.Ignore(a => a.HostName);
            this.Ignore(a => a.CurrentAccountMainID);            
        }
    }
}
