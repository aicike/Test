using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class OrderUserInfoMap : EntityTypeConfiguration<OrderUserInfo>
    {
        public OrderUserInfoMap()
        {
            this.Ignore(a => a.IsUpdate);
        }
    }
}
