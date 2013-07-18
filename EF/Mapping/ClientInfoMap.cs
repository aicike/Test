using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class ClientInfoMap : EntityTypeConfiguration<ClientInfo>
    {
        public ClientInfoMap()
        {
            this.HasRequired(a => a.EnumClientSystemType)
            .WithMany(a => a.ClientInfoEnumClientSystemType)
            .HasForeignKey(a => a.EnumClientSystemTypeID);

            this.HasRequired(a => a.EnumClientSystemType)
            .WithMany(a => a.ClientInfoEnumClientSystemType)
            .HasForeignKey(a => a.EnumClientSystemTypeID);
        }
    }
}
