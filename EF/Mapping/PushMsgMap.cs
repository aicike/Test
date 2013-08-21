using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class PushMsgMap : EntityTypeConfiguration<PushMsg>
    {
        public PushMsgMap()
        {
            this.Ignore(a => a.HtmlShow);
        }
    }
}
