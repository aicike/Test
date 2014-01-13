using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace MicroSite_EF.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.Ignore(a => a.LoginPwdPage);
            this.Ignore(a => a.LoginPwdPageCompare);
            this.Ignore(a => a.HostName);
            this.Ignore(a => a.CurrentAccountMainID);
            this.Ignore(a => a.CurrentAccountMainName);
            this.Ignore(a => a.LogoPath);
            this.Ignore(a => a.LogoThumbnailPath);
            this.Ignore(a => a.RoleIDs);
            this.Ignore(a => a.IsSuperAdmin);
        }
    }
}
