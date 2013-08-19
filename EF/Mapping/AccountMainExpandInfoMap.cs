using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Mapping
{
    public class AccountMainExpandInfoMap : EntityTypeConfiguration<AccountMainExpandInfo>
    {
        public AccountMainExpandInfoMap()
        {
            //HasRequired(a => a.AccounMain)
            //    .WithOptional(u => u.AccountMainExpandInfo);
        }
    }
}
