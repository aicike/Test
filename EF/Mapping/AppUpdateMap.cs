using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Mapping
{
    public class AppUpdateMap : EntityTypeConfiguration<AppUpdate>
    {
        public AppUpdateMap()
        {
            HasRequired(a => a.AccounMain)
                .WithOptional(u => u.AppUpdate);
        }
    }
}
