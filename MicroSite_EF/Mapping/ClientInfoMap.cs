using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace MicroSite_EF.Mapping
{
    public class ClientInfoMap : EntityTypeConfiguration<ClientInfo>
    {
        public ClientInfoMap()
        {
            this.HasOptional(a => a.EnumClientSystemType)
            .WithMany(a => a.ClientInfoEnumClientSystemType)
            .HasForeignKey(a => a.EnumClientSystemTypeID);

            this.HasOptional(a => a.EnumClientSystemType)
            .WithMany(a => a.ClientInfoEnumClientSystemType)
            .HasForeignKey(a => a.EnumClientSystemTypeID);
        }
    }
}
