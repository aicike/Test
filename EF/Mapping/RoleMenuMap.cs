using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class RoleMenuMap : EntityTypeConfiguration<RoleMenu>
    {
        public RoleMenuMap()
        {

            this.HasRequired(a => a.Role)
            .WithMany(a => a.RoleMenus).WillCascadeOnDelete(true);


        }
    }
}
